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
    public partial class BookImportDel : PageBase
    {
        private OleDbConnection objConnection = null;
        private OleDbDataAdapter objDataAdapter = null;
        private DataSet objDataSet = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }

            btnInput.Attributes.Add("onclick", "return confirm('如果删除该教材，与之相关的试题将被改为禁用状态，而与之相关的组卷策略项将被删除。您确定要删除该教材吗？');");
        }

        private void BindGrid()
        {
            ArrayList objList = GetNowBookList();

            BookBLL bookBLL = new BookBLL();
            IList<RailExam.Model.Book> AllBookList = bookBLL.GetAllBookInfo(SessionSet.OrganizationID);
            IList<RailExam.Model.Book> NewBookList = new List<RailExam.Model.Book>();

            foreach (RailExam.Model.Book book in AllBookList)
            {
                if (objList.IndexOf(book.bookId.ToString()) == -1)
                {
                    NewBookList.Add(book);
                }
            }

            TrainTypeBLL trainTypeBLL = new TrainTypeBLL();
            BookTrainTypeBLL bookTrainTypeBLL = new BookTrainTypeBLL();
            foreach (RailExam.Model.Book book in NewBookList)
            {
                IList<BookTrainType> bookTrainTypeList = bookTrainTypeBLL.GetBookTrainTypeByBookID(book.bookId);
                string strTrainTypeNames = "";
                foreach (BookTrainType bookTrainType in bookTrainTypeList)
                {
                    if (strTrainTypeNames == "")
                    {
                        strTrainTypeNames = strTrainTypeNames + GetTrainTypeName("/" + bookTrainType.TrainTypeName, trainTypeBLL.GetTrainTypeInfo(bookTrainType.TrainTypeID).ParentID);
                    }
                    else
                    {
                        strTrainTypeNames = strTrainTypeNames + "," + GetTrainTypeName("/" + bookTrainType.TrainTypeName, trainTypeBLL.GetTrainTypeInfo(bookTrainType.TrainTypeID).ParentID);
                    }
                }
                book.trainTypeNames = strTrainTypeNames;
            }

            gvBook.DataSource = NewBookList;
            gvBook.DataBind();
        }

        private string GetTrainTypeName(string strName, int nID)
        {
            string strTrainTypeName = string.Empty;
            if (nID != 0)
            {
                TrainTypeBLL trainTypeBLL = new TrainTypeBLL();
                TrainType trainType = trainTypeBLL.GetTrainTypeInfo(nID);

                if (trainType.ParentID != 0)
                {
                    strTrainTypeName = GetTrainTypeName("/" + trainType.TypeName, trainType.ParentID) + strName;
                }
                else
                {
                    strTrainTypeName = trainType.TypeName + strName;
                }
            }

            return strTrainTypeName;
        }

        private ArrayList GetNowBookList()
        {
            ArrayList objList = new ArrayList();

            string strSql = "select * from Book where Publish_org=" + SessionSet.OrganizationID ;
            DataSet ds = RunSqlDataSet(strSql);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                objList.Add(dr["Book_ID"].ToString());
            }

            return objList;
        }

        protected void lblUpdate_Click(object sender, EventArgs e)
        {
            Response.Redirect("BookImportUpdate.aspx");
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("BookImportNew.aspx");
        }

        protected void btnInput_Click(object sender, EventArgs e)
        {
            BookBLL bookBLL = new BookBLL();

            for (int i = 0; i < gvBook.Rows.Count; i++)
            {
                bool bChecked = ((CheckBox)gvBook.Rows[i].FindControl("chSelect")).Checked;

                if (bChecked)
                {
                    ItemBLL objBll = new ItemBLL();
                    IList<RailExam.Model.Item> objItemList = objBll.GetItemsByBookBookId(Convert.ToInt32(gvBook.DataKeys[i].Value.ToString()));

                    foreach (RailExam.Model.Item item in objItemList)
                    {
                        item.StatusId = 2;
                        objBll.UpdateItem(item);
                    }
                    if (Directory.Exists(Server.MapPath("../Online/Book/" + gvBook.DataKeys[i].Value + "/")))
                    {
                        DeleteFile(Server.MapPath("../Online/Book/" + gvBook.DataKeys[i].Value + "/"));
                    }
                    bookBLL.DeleteBook(Convert.ToInt32(gvBook.DataKeys[i].Value.ToString()));
                }
            }
            BindGrid();
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
    }
}
