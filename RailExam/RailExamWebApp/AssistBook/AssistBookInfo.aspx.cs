using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.AssistBook
{
    public partial class AssistBookInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
               if(!IsPostBack)
               {
                    if (PrjPub.HasEditRight("辅导教材") && PrjPub.IsServerCenter && PrjPub.CurrentLoginUser.SuitRange == 1)
                    {
                        HfUpdateRight.Value = "True";
                    }
                    else
                    {
                        HfUpdateRight.Value = "False";
                    }
                    if (PrjPub.HasEditRight("辅导教材") && PrjPub.IsServerCenter && PrjPub.CurrentLoginUser.SuitRange == 1)
                    {
                        HfDeleteRight.Value = "True";
                    }
                    else
                    {
                        HfDeleteRight.Value = "False";
                    }
                    HfOrgId.Value = PrjPub.CurrentLoginUser.OrgID.ToString();
                    BindGrid();
            }
            else
            {
                if (Request.Form.Get("Refresh") == "true")
                {
                    //ViewState["NowID"] = "false";
                    BindGrid();
                }
            }

            string strDeleteID = Request.Form.Get("DeleteID");
            if (strDeleteID != null && strDeleteID != "")
            {
                DelAssistBook(strDeleteID);
                BindGrid();
            }

            string strUpID = Request.Form.Get("UpID");
            if (strUpID != null && strUpID != "")
            {
                AssistBookBLL objBll = new AssistBookBLL();
                RailExam.Model.AssistBook obj = objBll.GetAssistBook(Convert.ToInt32(strUpID));
                obj.OrderIndex = obj.OrderIndex - 1;
                objBll.UpdateAssistBook(obj);
                BindGrid();
            }

            string strDownID = Request.Form.Get("DownID");
            if (strDownID != null && strDownID != "")
            {
                AssistBookBLL objBll = new AssistBookBLL();
                RailExam.Model.AssistBook obj = objBll.GetAssistBook(Convert.ToInt32(strDownID));
                obj.OrderIndex = obj.OrderIndex + 1;
                objBll.UpdateAssistBook(obj);
                BindGrid();
            }
        }


        private void DelAssistBook(string strID)
        {
            AssistBookBLL objBll = new AssistBookBLL();
            objBll.DeleteAssistBook(Convert.ToInt32(strID));
        }

        private void BindGrid()
        {
            AssistBookBLL bookBLL = new AssistBookBLL();
            IList<RailExam.Model.AssistBook> books = new List<RailExam.Model.AssistBook>();

            OrganizationBLL orgBll = new OrganizationBLL();
            int orgID = orgBll.GetStationOrgID(PrjPub.CurrentLoginUser.OrgID); 

            string strCategoryIDPath = Request.QueryString.Get("id");
            if (!string.IsNullOrEmpty(strCategoryIDPath))
            {
                if(strCategoryIDPath != "0" )
                {
                    books = bookBLL.GetAssistBookByAssistBookCategoryID(Convert.ToInt32(strCategoryIDPath), orgID);
                }
                else
                {
                    books = bookBLL.GetAllAssistBookInfo(0);
                }
            }

            //string strTrainTypeIDPath = Request.QueryString.Get("id1");
            //if (!string.IsNullOrEmpty(strTrainTypeIDPath))
            //{
            //    books = bookBLL.GetAssistBookByTrainTypeIDPath(strTrainTypeIDPath, PrjPub.CurrentLoginUser.OrgID);
            //}

            if (books.Count > 0)
            {
                foreach (RailExam.Model.AssistBook book in books)
                {
                    if (book.BookName.Length <= 15)
                    {
                        book.BookName = "<a onclick=OpenIndex('"+ book.AssistBookId +"') href=# title=" + book.BookName + " > " + book.BookName + " </a>";
                    }
                    else
                    {
                        book.BookName = "<a onclick=OpenIndex('" + book.AssistBookId + "') href=# title=" + book.BookName + " > " + book.BookName.Substring(0, 15) + " </a>";
                    }
                }

                Grid1.DataSource = books;
                Grid1.DataBind();
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            AssistBookBLL bookBLL = new AssistBookBLL();
            IList<RailExam.Model.AssistBook> books = new List<RailExam.Model.AssistBook>();

            OrganizationBLL orgBll = new OrganizationBLL();
            int orgID = orgBll.GetStationOrgID(PrjPub.CurrentLoginUser.OrgID);

            string strKnowledgeID = Request.QueryString.Get("id");

            if (!string.IsNullOrEmpty(strKnowledgeID))
            {
                if(strKnowledgeID != "0")
                {
                    string[] str1 = strKnowledgeID.Split(new char[] { '/' });
                    int nKnowledgeId = int.Parse(str1[str1.LongLength - 1].ToString());
                    books = bookBLL.GetAssistBookByAssistBookCategoryID(nKnowledgeId, txtAssistBookName.Text, txtKeyWords.Text, txtAuthors.Text, orgID);
                }
                else
                {
                    books = bookBLL.GetAssistBookByAssistBookCategoryID(0, txtAssistBookName.Text, txtKeyWords.Text, txtAuthors.Text, orgID);
                }
            }

            //string strTrainTypeID = Request.QueryString.Get("id1");

            //if (!string.IsNullOrEmpty(strTrainTypeID))
            //{
            //    string[] str2 = strTrainTypeID.Split(new char[] { '/' });
            //    int nTrainTypeID = int.Parse(str2[str2.LongLength - 1].ToString());
            //    books = bookBLL.GetAssistBookByTrainTypeID(nTrainTypeID, txtAssistBookName.Text, txtKeyWords.Text, txtAuthors.Text, PrjPub.CurrentLoginUser.OrgID);
            //}

            if (books != null)
            {
                Grid1.DataSource = books;
                Grid1.DataBind();
            }
        }

        protected void Grid1_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Get("id") == "0")
            {
                Grid1.Levels[0].Columns[1].Visible = false;
            }
        }
    }
}
