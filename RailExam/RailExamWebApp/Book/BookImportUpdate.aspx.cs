using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.UI;
using DSunSoft.Web.Global;
using System.Data.OleDb;
using System.IO;

namespace RailExamWebApp.Book
{
    public partial class BookImportUpdate : PageBase
    {
        private OleDbConnection objConnection = null;
        private OleDbDataAdapter objDataAdapter = null;
        private DataSet objDataSet = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["QuerySql"] = "";
                BindGrid();
                BindOrg();
            }
            btnInput.Attributes.Add("onclick", "return confirm('如果所选教材某章节在发布系统中被删除，则在本系统中与该章节相关的试题将被改为禁用状态，而与该章节相关的组卷策略项将被删除。您确定要导入该教材最新信息吗？');");
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("BookImportNew.aspx");
        }

        protected void btnInput_Click(object sender, EventArgs e)
        {
            string strSql;
            for (int i = 0; i < gvBook.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvBook.Rows[i].FindControl("chSelect");

                string strKnowledgeId = ((HiddenField)gvBook.Rows[i].FindControl("hfKownledgeId")).Value;

                if (chk.Checked)
                {
                    strSql = "select a.*,b.Short_Name from Book a inner join Org b on a.Publish_Org=b.Org_ID where a.Book_ID=" + gvBook.DataKeys[i].Value.ToString();
                    DataSet ds = RunSqlDataSet(strSql);
                    DataRow dr = ds.Tables[0].Rows[0];

                    BookBLL objBookbll = new BookBLL();
                    RailExam.Model.Book objBookNew = new RailExam.Model.Book();
                    objBookNew.bookId = Convert.ToInt32(dr["Book_ID"].ToString());

                    string strPath = Server.MapPath(ConfigurationManager.AppSettings["BookDesigner"].ToString() + objBookNew.bookId + "/");
                    string strAimPath = Server.MapPath("../Online/Book/" + objBookNew.bookId + "/");
                    if (!Directory.Exists(strPath))
                    {
                        SessionSet.PageMessage = "有教材还未创建内容，暂不能导入！";
                        return;
                    }

                    objBookNew.bookName = dr["Book_Name"].ToString();
                    objBookNew.bookNo = dr["Book_No"].ToString();
                    objBookNew.authors = dr["Authors"].ToString();
                    objBookNew.bookmaker = dr["BookMaker"].ToString();
                    objBookNew.publishOrg = Convert.ToInt32(dr["Publish_Org"].ToString());
                    objBookNew.publishOrgName = dr["Short_Name"].ToString();
                    objBookNew.revisers = dr["Revisers"].ToString();
                    objBookNew.coverDesigner = dr["Cover_Designer"].ToString();
                    objBookNew.keyWords = dr["Keywords"].ToString();
                    objBookNew.pageCount = Convert.ToInt32(dr["Page_Count"].ToString());
                    objBookNew.wordCount = Convert.ToInt32(dr["Word_Count"].ToString());
                    objBookNew.Description = dr["Description"].ToString();
                    objBookNew.url = "../Book/" + dr["Book_ID"].ToString() + "/index.html";
                    objBookNew.Memo = dr["Remark"].ToString();
                    objBookNew.knowledgeId = Convert.ToInt32(strKnowledgeId);

                    //objBookbll.UpdateBook(objBookNew, ref errorCode);

                    strSql = "select * from Book_Chapter where Book_ID=" + gvBook.DataKeys[i].Value.ToString();
                    ds = RunSqlDataSet(strSql);

                    BookChapterBLL objBookChapterBll = new BookChapterBLL();

                    //取得Access数据库中当前教材的所有的章节ID
                    ArrayList objAccessChapterList = new ArrayList();

                    //取得当前教材在oracle数据中所有章节ID
                    ArrayList objList = GetBookChapterList(Convert.ToInt32(gvBook.DataKeys[i].Value.ToString()));
                    foreach (DataRow dr1 in ds.Tables[0].Rows)
                    {
                        RailExam.Model.BookChapter objBookChapter = new RailExam.Model.BookChapter();

                        objBookChapter.ChapterId = Convert.ToInt32(dr1["Chapter_ID"].ToString());
                        objBookChapter.BookId = Convert.ToInt32(dr1["Book_ID"].ToString());
                        objBookChapter.ChapterName = dr1["Chapter_Name"].ToString();
                        objBookChapter.ParentId = Convert.ToInt32(dr1["Parent_ID"].ToString());
                        objBookChapter.LevelNum = Convert.ToInt32(dr1["Level_Num"].ToString());
                        objBookChapter.OrderIndex = Convert.ToInt32(dr1["Order_Index"].ToString());
                        objBookChapter.ReferenceRegulation = dr1["REFERENCE_REGULATION"].ToString();
                        objBookChapter.Description = dr1["Description"].ToString();
                        objBookChapter.Memo = dr1["Remark"].ToString();
                        objBookChapter.Url = "../Book/" + dr1["Book_ID"].ToString() + "/" + dr1["Chapter_ID"].ToString() + ".htm";

                        //如果该章节的ID已存在当前oracle数据库中则只需修改章节信息,否则添加信息
                        if (objList.IndexOf(objBookChapter.ChapterId) != -1)
                        {
                            objBookChapterBll.UpdateBookChapter(objBookChapter);
                        }
                        else
                        {
                            objBookChapterBll.AddBookChapter(objBookChapter);
                        }

                        objAccessChapterList.Add(objBookChapter.ChapterId);
                    }

                    //取得当前教材在oracle中的所有教材信息


                    IList<RailExam.Model.BookChapter> objOracleBookChapterList =
                        objBookChapterBll.GetBookChapterByBookID(objBookNew.bookId);

                    foreach (RailExam.Model.BookChapter chapter in objOracleBookChapterList)
                    {
                        //如果oracle数据库中某章节ID不在Access数据库中
                        if (objAccessChapterList.IndexOf(chapter.ChapterId) == -1)
                        {
                            ItemBLL objItemBll = new ItemBLL();
                            IList<RailExam.Model.Item> objItemList = objItemBll.GetItemsByBookBookId(objBookNew.bookId);

                            foreach (RailExam.Model.Item item in objItemList)
                            {
                                item.StatusId = 2;
                                objItemBll.UpdateItem(item);
                            }

                            objBookChapterBll.DeleteBookChapter(chapter.ChapterId);
                        }
                    }

                    if (Directory.Exists(Server.MapPath("../Online/Book/" + objBookNew.bookId + "/")))
                    {
                        //Directory.Delete(Server.MapPath("../Online/Book/" + objBookNew.bookId + "/"), true);
                        DeleteFile(Server.MapPath("../Online/Book/" + objBookNew.bookId + "/"));
                    }

                    CopyTemplate(strPath, strAimPath);
                }
            }
            BindGrid();
        }

        private ArrayList GetBookChapterList(int nBookID)
        {
            ArrayList objList = new ArrayList();
            BookChapterBLL objBll = new BookChapterBLL();
            IList<RailExam.Model.BookChapter> objBookChapterList = objBll.GetBookChapterByBookID(nBookID);

            foreach (RailExam.Model.BookChapter chapter in objBookChapterList)
            {
                objList.Add(chapter.ChapterId);
            }

            return objList;
        }
        
        private void DeleteFile(string srcPath)
        {
            string[] fileList = Directory.GetFileSystemEntries(srcPath);

            foreach (string file in fileList)
            {
                if (Directory.Exists(file))
                {
                    DeleteFile(file);
                }
                else
                {
                    File.Delete(file);
                }
            }
        }

        private void CopyTemplate(string srcPath, string aimPath)
        {
            if (!Directory.Exists(aimPath))
            {
                Directory.CreateDirectory(aimPath);
            }

            string[] fileList = Directory.GetFileSystemEntries(srcPath);

            foreach (string file in fileList)
            {
                if (Directory.Exists(file))
                {
                    CopyTemplate(file, aimPath + Path.GetFileName(file) + "\\");
                }
                else
                {
                    File.Copy(file, aimPath + Path.GetFileName(file), true);
                }
            }
        }

        private void BindOrg()
        {
            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "--编制单位--";
            ddlGroup.Items.Add(item);

            DataSet ds = RunSqlDataSet("select * from Org");

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListItem items = new ListItem();
                    items.Value = dr["Org_ID"].ToString();
                    items.Text = dr["Short_Name"].ToString();
                    ddlGroup.Items.Add(items);
                }
            }
        }

        private void BindGrid()
        {
            string strSql;

            strSql = "select a.*,b.Short_Name from Book a inner join Org b on a.Publish_Org=b.Org_ID where Publish_org=" + SessionSet.OrganizationID + ViewState["QuerySql"].ToString() + " order by Book_Name";
            DataSet ds = RunSqlDataSet(strSql);

            IList<RailExam.Model.Book> objList = new List<RailExam.Model.Book>();

            ArrayList objBookList = GetBookList();
            BookBLL objBookbll = new BookBLL();
            IList<RailExam.Model.Book> objAllBookList = objBookbll.GetAllBookInfo(SessionSet.OrganizationID);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int n = objBookList.IndexOf(Convert.ToInt32(dr["Book_ID"].ToString()));
                if (n != -1)
                {
                    RailExam.Model.Book objBookNew = new RailExam.Model.Book();
                    objBookNew.bookId = Convert.ToInt32(dr["Book_ID"].ToString());
                    objBookNew.bookName = dr["Book_Name"].ToString();
                    objBookNew.bookNo = dr["Book_No"].ToString();
                    objBookNew.authors = dr["Authors"].ToString();
                    objBookNew.bookmaker = dr["BookMaker"].ToString();
                    objBookNew.publishOrg = Convert.ToInt32(dr["Publish_Org"].ToString());
                    objBookNew.publishOrgName = dr["Short_Name"].ToString();
                    objBookNew.revisers = dr["Revisers"].ToString();
                    objBookNew.coverDesigner = dr["Cover_Designer"].ToString();
                    objBookNew.keyWords = dr["Keywords"].ToString();
                    objBookNew.pageCount = Convert.ToInt32(dr["Page_Count"].ToString());
                    objBookNew.wordCount = Convert.ToInt32(dr["Word_Count"].ToString());
                    objBookNew.Description = dr["Description"].ToString();
                    objBookNew.url = "../Book/" + dr["Book_ID"].ToString() + "/index.html";
                    objBookNew.Memo = dr["Remark"].ToString();

                    foreach (RailExam.Model.Book obj in objAllBookList)
                    {
                        if (obj.bookId == Convert.ToInt32(dr["Book_ID"].ToString()))
                        {
                            TrainTypeBLL objTrainTypeBll = new TrainTypeBLL();
                            objBookNew.knowledgeId = obj.knowledgeId;
                            objBookNew.knowledgeName = obj.knowledgeName;

                            BookTrainTypeBLL objBookTrainTypeBll = new BookTrainTypeBLL();
                            IList<BookTrainType> objBookTrainTypeList = objBookTrainTypeBll.GetBookTrainTypeByBookID(Convert.ToInt32(dr["Book_ID"].ToString()));
                            string strTypeName = "";
                            foreach (BookTrainType obj1 in objBookTrainTypeList)
                            {
                                if (strTypeName == "")
                                {
                                    strTypeName = strTypeName + GetType("/" + obj1.TrainTypeName, objTrainTypeBll.GetTrainTypeInfo(obj1.TrainTypeID).ParentID);
                                }
                                else
                                {
                                    strTypeName = strTypeName + "," + GetType("/" + obj1.TrainTypeName, objTrainTypeBll.GetTrainTypeInfo(obj1.TrainTypeID).ParentID);
                                }
                            }
                            objBookNew.trainTypeNames = strTypeName;
                        }
                    }

                    objList.Add(objBookNew);
                }
            }

            gvBook.DataSource = objList;
            gvBook.DataBind();
        }

        private string GetType(string strName, int nID)
        {
            string str = "";
            if (nID != 0)
            {
                TrainTypeBLL objTrainTypeBll = new TrainTypeBLL();
                TrainType objTrainType = objTrainTypeBll.GetTrainTypeInfo(nID);

                if (objTrainType.ParentID != 0)
                {
                    str = GetType("/" + objTrainType.TypeName, objTrainType.ParentID) + strName;
                }
                else
                {
                    str = objTrainType.TypeName + strName;
                }
            }

            return str;
        }

        private ArrayList GetBookList()
        {
            BookBLL objBookbll = new BookBLL();
            IList<RailExam.Model.Book> objList = objBookbll.GetAllBookInfo(SessionSet.OrganizationID);

            ArrayList objBookList = new ArrayList();
            foreach (RailExam.Model.Book obj in objList)
            {
                objBookList.Add(obj.bookId);
            }
            return objBookList;
        }

        private void Open()
        {
            if (objConnection == null)
            {
                objConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Data Source="
                    + Server.MapPath(ConfigurationManager.AppSettings["ConStr"].ToString()));
                objConnection.Open();
            }
            if (objConnection.State == System.Data.ConnectionState.Closed)
            {
                objConnection.Open();
            }
        }

        private void Close()
        {
            if (objConnection != null)
                objConnection.Close();
        }

        private DataSet RunSqlDataSet(string sql)
        {
            Open();
            objDataAdapter = new OleDbDataAdapter(sql, objConnection);
            objDataSet = new DataSet();
            objDataAdapter.Fill(objDataSet);
            Close();
            return objDataSet;
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string strQuery = " and 1=1 ";
            if (txtName.Text != "")
            {
                strQuery += " and Book_Name like '%" + txtName.Text + "%'";
            }

            if (txtNo.Text != "")
            {
                strQuery += " and Book_No like '%" + txtNo.Text + "%'";
            }

            if (txtAuthor.Text != "")
            {
                strQuery += " and Authors like '%" + txtAuthor.Text + "%'";
            }

            if (ddlGroup.SelectedValue != "0")
            {
                strQuery += " and a.Publish_Org=" + ddlGroup.SelectedValue;
            }

            if (txtKey.Text != "")
            {
                strQuery += " and KeyWords like '%" + txtKey.Text + "%'";
            }

            ViewState["QuerySql"] = strQuery;

            BindGrid();
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            Response.Redirect("BookImportDel.aspx");
        }
    }
}
