using System;
using System.Collections.Generic;
using DSunSoft.Web.UI;
using RailExam.BLL;

namespace RailExamWebApp.Online.Study
{
    public partial class StudyAssistBookDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string strTrainType = Request.QueryString.Get("TrainTypeID");
                string strKnowledge = Request.QueryString.Get("KnowledgeID");
                //string strPostID = Request.QueryString.Get("PostID");
                //string strOrgID = Request.QueryString.Get("OrgID");
                //string strLeader = Request.QueryString.Get("Leader");
                //string strTech = Request.QueryString.Get("Tech");

                //ViewState["PostID"] = strPostID;
                //ViewState["OrgID"] = strOrgID;
                //ViewState["Leader"] = strLeader;
                //ViewState["Tech"] = strTech;

                //if (strTrainType != "" && strTrainType != null)
                //{
                //    ViewState["TrainTypeID"] = strTrainType;
                //    BindGridByTrainTypeID();
                //}

                if (strKnowledge != "" && strKnowledge != null)
                {
                    ViewState["KnowledgeID"] = strKnowledge;
                    BindGridByCategoryID();
                }
            }
        }

        private void BindGridByCategoryID()
        {
            string assistCategoryID =ViewState["KnowledgeID"].ToString();
            //int postID = Convert.ToInt32(ViewState["PostID"].ToString());
            //int orgID = Convert.ToInt32(ViewState["OrgID"].ToString());
            //int techID = Convert.ToInt32(ViewState["Tech"].ToString());
            //int leader = Convert.ToInt32(ViewState["Leader"].ToString());

            AssistBookBLL bookBLL = new AssistBookBLL();
            IList<RailExam.Model.AssistBook> bookList = new List<RailExam.Model.AssistBook>();
            //if(assistCategoryID != "0")
            //{
                bookList = bookBLL.GetAssistBookByAssistBookCategoryID(Convert.ToInt32(assistCategoryID),1);
            //}
            //else
            //{
            //    bookList = bookBLL.GetAllAssistBookInfo(0);
            //}
           

            if (bookList.Count > 0)
            {
                foreach (RailExam.Model.AssistBook book in bookList)
                {
                    if (book.BookName.Length <= 15)
                    {
                        book.BookName = "<a onclick=OpenIndex('" + book.AssistBookId + "') href=# title=" + book.BookName + " > " + book.BookName + " </a>";
                    }
                    else
                    {
                        book.BookName = "<a onclick=OpenIndex('" + book.AssistBookId + "') href=# title=" + book.BookName + " > " + book.BookName.Substring(0, 15) + " </a>";
                    }
                }

                gvBook.DataSource = bookList;
                gvBook.DataBind();
            }
        }

        //private void BindGridByTrainTypeID()
        //{
        //    string trainTypeID = ViewState["TrainTypeID"].ToString();
        //    //int postID = Convert.ToInt32(ViewState["PostID"].ToString());
        //    //int orgID = Convert.ToInt32(ViewState["OrgID"].ToString());
        //    //int techID = Convert.ToInt32(ViewState["Tech"].ToString());
        //    //int leader = Convert.ToInt32(ViewState["Leader"].ToString());

        //    AssistBookBLL bookBLL = new AssistBookBLL();
        //    IList<RailExam.Model.AssistBook> bookList = bookBLL.GetAssistBookByTrainTypeIDPath(trainTypeID);
        //    if (bookList.Count > 0)
        //    {
        //        foreach (RailExam.Model.AssistBook book in bookList)
        //        {
        //            if (book.BookName.Length <= 15)
        //            {
        //                book.BookName = "<a onclick=OpenIndex('" + book.AssistBookId + "') href=# title=" + book.BookName + " > " + book.BookName + " </a>";
        //            }
        //            else
        //            {
        //                book.BookName = "<a onclick=OpenIndex('" + book.AssistBookId + "') href=# title=" + book.BookName + " > " + book.BookName.Substring(0, 15) + " </a>";
        //            }
        //        }

        //        gvBook.DataSource = bookList;
        //        gvBook.DataBind();
        //    }
        //}
    }
}
