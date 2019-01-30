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
    public partial class BookImportNew : PageBase
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

            GetValue();
        }

        private void BindGrid()
        {
            string strSql;

            strSql = "select a.*,b.Short_Name from Book a inner join Org b on a.Publish_Org=b.Org_ID where Publish_org=" + SessionSet.OrganizationID  + ViewState["QuerySql"].ToString() + " order by Book_Name";
            DataSet ds = RunSqlDataSet(strSql);

            IList<RailExam.Model.Book> bookList = new List<RailExam.Model.Book>();
            ArrayList bookIDList = GetBookIDList();

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (bookIDList.IndexOf(Convert.ToInt32(dr["Book_ID"].ToString())) == -1)
                {
                    RailExam.Model.Book book = new RailExam.Model.Book();
                    book.bookId = Convert.ToInt32(dr["Book_ID"].ToString());
                    book.bookName = dr["Book_Name"].ToString();
                    book.bookNo = dr["Book_No"].ToString();
                    book.authors = dr["Authors"].ToString();
                    book.bookmaker = dr["BookMaker"].ToString();
                    book.publishOrg = Convert.ToInt32(dr["Publish_Org"].ToString());
                    book.publishOrgName = dr["Short_Name"].ToString();
                    book.revisers = dr["Revisers"].ToString();
                    book.coverDesigner = dr["Cover_Designer"].ToString();
                    book.keyWords = dr["Keywords"].ToString();
                    book.pageCount = Convert.ToInt32(dr["Page_Count"].ToString());
                    book.wordCount = Convert.ToInt32(dr["Word_Count"].ToString());
                    book.Description = dr["Description"].ToString();
                    book.url = "../Book/" + dr["Book_ID"].ToString() + "/index.html";
                    book.Memo = dr["Remark"].ToString();

                    book.knowledgeId = 0;
                    book.knowledgeName = "";

                    bookList.Add(book);
                }
            }

            gvBook.DataSource = bookList;
            gvBook.DataBind();
        }

        private ArrayList GetBookIDList()
        {
            BookBLL bookBLL = new BookBLL();
            IList<RailExam.Model.Book> bookList = bookBLL.GetAllBookInfo(SessionSet.OrganizationID);

            ArrayList bookIDList = new ArrayList();

            foreach (RailExam.Model.Book book in bookList)
            {
                bookIDList.Add(book.bookId);
            }

            return bookIDList;
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


        private void GetValue()
        {
            for (int i = 0; i < gvBook.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox) gvBook.Rows[i].FindControl("chSelect");

                HiddenField hfKownledgeName = (HiddenField) gvBook.Rows[i].FindControl("hfKownledgeName");
                HiddenField hfTrainTypeName = (HiddenField) gvBook.Rows[i].FindControl("hfTrainTypeName");

                TextBox txtKownledgeName = (TextBox)gvBook.Rows[i].FindControl("txtKownledgeName");
                TextBox txtTrainTypeName = (TextBox)gvBook.Rows[i].FindControl("txtTrainTypeName");

                if(chk.Checked)
                {
                    txtKownledgeName.Text = hfKownledgeName.Value;
                    txtTrainTypeName.Text = hfTrainTypeName.Value;
                }
            }
        }

        protected void btnInput_Click(object sender, EventArgs e)
        {
            string strSql;

            for (int i = 0; i < gvBook.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvBook.Rows[i].FindControl("chSelect");

                HiddenField hfKownledgeName = (HiddenField)gvBook.Rows[i].FindControl("hfKownledgeName");
                HiddenField hfKownledgeId = (HiddenField)gvBook.Rows[i].FindControl("hfKownledgeId");
                HiddenField hfTrainTypeName = (HiddenField)gvBook.Rows[i].FindControl("hfTrainTypeName");
                HiddenField hfTrainTypeID = (HiddenField)gvBook.Rows[i].FindControl("hfTrainTypeID");

                if (chk.Checked && hfKownledgeName.Value.Trim() == "")
                {
                    SessionSet.PageMessage = "请选择教材体系！";
                    return;
                }

                if (chk.Checked && hfTrainTypeName.Value.Trim() == "")
                {
                    SessionSet.PageMessage = "请选择培训类别！";
                    return;
                }

                if (chk.Checked)
                {
                    strSql = "select a.*,b.Short_Name from Book a inner join Org b on a.Publish_Org=b.Org_ID where a.Book_ID=" + gvBook.DataKeys[i].Value.ToString();
                    DataSet ds = RunSqlDataSet(strSql);
                    DataRow dr = ds.Tables[0].Rows[0];

                    BookBLL bookBLL = new BookBLL();
                    RailExam.Model.Book book = new RailExam.Model.Book();

                    book.bookId = Convert.ToInt32(dr["Book_ID"].ToString());

                    string strPath = Server.MapPath(ConfigurationManager.AppSettings["BookDesigner"].ToString() + book.bookId + "/");
                    string strAimPath = Server.MapPath("../Online/Book/" + book.bookId + "/");
                    if (!Directory.Exists(strPath))
                    {
                        SessionSet.PageMessage = "有教材还未创建内容，暂不能导入！";
                        return;
                    }

                    book.bookName = dr["Book_Name"].ToString();
                    book.bookNo = dr["Book_No"].ToString();
                    book.authors = dr["Authors"].ToString();
                    book.bookmaker = dr["BookMaker"].ToString();
                    book.publishOrg = Convert.ToInt32(dr["Publish_Org"].ToString());
                    book.publishOrgName = dr["Short_Name"].ToString();
                    book.revisers = dr["Revisers"].ToString();
                    book.coverDesigner = dr["Cover_Designer"].ToString();
                    book.keyWords = dr["Keywords"].ToString();
                    book.pageCount = Convert.ToInt32(dr["Page_Count"].ToString());
                    book.wordCount = Convert.ToInt32(dr["Word_Count"].ToString());
                    book.Description = dr["Description"].ToString();
                    book.url = "../Book/" + dr["Book_ID"].ToString() + "/index.html";
                    book.Memo = dr["Remark"].ToString();
                    book.knowledgeId = Convert.ToInt32(hfKownledgeId.Value);
                    book.knowledgeName = hfKownledgeName.Value;

                    ArrayList typeIDAL = new ArrayList();
                    string[] strTypeID = hfTrainTypeID.Value.Split(',');
                    for (int j = 0; j < strTypeID.Length; j++)
                    {
                        typeIDAL.Add(strTypeID[j]);
                    }
                    book.trainTypeidAL = typeIDAL;

                    bookBLL.AddBook(book);

                    strSql = "select * from Book_Chapter where Book_ID=" + gvBook.DataKeys[i].Value.ToString() + " order by Level_Num,Order_Index";
                    ds = RunSqlDataSet(strSql);

                    BookChapterBLL bookChapterBll = new BookChapterBLL();
                    foreach (DataRow dr1 in ds.Tables[0].Rows)
                    {
                        RailExam.Model.BookChapter bookChapter = new RailExam.Model.BookChapter();

                        bookChapter.ChapterId = Convert.ToInt32(dr1["Chapter_ID"].ToString());
                        bookChapter.BookId = Convert.ToInt32(dr1["Book_ID"].ToString());
                        bookChapter.ChapterName = dr1["Chapter_Name"].ToString();
                        bookChapter.ParentId = Convert.ToInt32(dr1["Parent_ID"].ToString());
                        bookChapter.LevelNum = Convert.ToInt32(dr1["Level_Num"].ToString());
                        bookChapter.OrderIndex = Convert.ToInt32(dr1["Order_Index"].ToString());
                        bookChapter.ReferenceRegulation = dr1["REFERENCE_REGULATION"].ToString();
                        bookChapter.Description = dr1["Description"].ToString();
                        bookChapter.Memo = dr1["Remark"].ToString();
                        bookChapter.Url = "../Book/" + dr1["Book_ID"].ToString() + "/" + dr1["Chapter_ID"].ToString() + ".htm";

                        bookChapterBll.AddBookChapter(bookChapter);
                    }

                    CopyTemplate(strPath, strAimPath);
                }
            }
            BindGrid();
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

        protected void lblUpdate_Click(object sender, EventArgs e)
        {
            Response.Redirect("BookImportUpdate.aspx");
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

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string strQuery = " and 1=1 ";
            if (txtBookName.Text != "")
            {
                strQuery += " and Book_Name like '%" + txtBookName.Text + "%'";
            }

            if (txtBookNo.Text != "")
            {
                strQuery += " and Book_No like '%" + txtBookNo.Text + "%'";
            }

            if (txtAuthors.Text != "")
            {
                strQuery += " and Authors like '%" + txtAuthors.Text + "%'";
            }

            if (ddlGroup.SelectedValue != "0")
            {
                strQuery += " and a.Publish_Org=" + ddlGroup.SelectedValue;
            }

            if (txtKeyWord.Text != "")
            {
                strQuery += " and KeyWords like '%" + txtKeyWord.Text + "%'";
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
