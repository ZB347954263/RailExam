using System;
using System.Collections.Generic;
using DSunSoft.Web.UI;
using RailExam.BLL;

namespace RailExamWebApp.Online.Study
{
    public partial class StudyBook : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string strTrainType = Request.QueryString.Get("TrainTypeID");
                string strKnowledge = Request.QueryString.Get("KnowledgeID");
                string strPostID = Request.QueryString.Get("PostID");
                string strOrgID = Request.QueryString.Get("OrgID");
                string strLeader = Request.QueryString.Get("Leader");
                string strTech = Request.QueryString.Get("Tech");

                ViewState["PostID"] = strPostID;
                ViewState["OrgID"] = strOrgID;
                ViewState["Leader"] = strLeader;
                ViewState["Tech"] = strTech;

                if(strTrainType != "" && strTrainType!=null )
                {
                    ViewState["TrainTypeID"] = strTrainType;
                    BindGridByTrainTypeID();
                	BindGridCourseByTrainTypeID();
                }

                if(strKnowledge != "" && strKnowledge != null)
                {
                    ViewState["KnowledgeID"] = strKnowledge;
                    BindGridByKnowledgeID();
                	BindGridCourseByCoursewareTypeID();
                }
            }
        }

        private void BindGridByKnowledgeID()
        {
            int knowledgeID = Convert.ToInt32(ViewState["KnowledgeID"].ToString());
            int postID = Convert.ToInt32(ViewState["PostID"].ToString());
            int orgID = Convert.ToInt32(ViewState["OrgID"].ToString());
            int techID = Convert.ToInt32(ViewState["Tech"].ToString());
            int leader = Convert.ToInt32(ViewState["Leader"].ToString());

            BookBLL bookBLL = new BookBLL();
            IList<RailExam.Model.Book> bookList = bookBLL.GetEmployeeStudyBookInfoByKnowledgeID(knowledgeID, orgID, postID, leader, techID, 0);

            if (bookList.Count > 0)
            {
                foreach (RailExam.Model.Book book in bookList)
                {
                    book.bookName = "<a onclick=OpenIndex('" + book.bookId + "') href=# title=" + book.bookName + " > " + book.bookName + " </a>";
                }

                Grid1.DataSource = bookList;
				Grid1.DataBind();
            }
        }

        private void BindGridByTrainTypeID()
        {
            int trainTypeID = Convert.ToInt32(ViewState["TrainTypeID"].ToString());
            int postID = Convert.ToInt32(ViewState["PostID"].ToString());
            int orgID = Convert.ToInt32(ViewState["OrgID"].ToString());
            int techID = Convert.ToInt32(ViewState["Tech"].ToString());
            int leader = Convert.ToInt32(ViewState["Leader"].ToString());

            BookBLL bookBLL = new BookBLL();
            IList<RailExam.Model.Book> bookList = bookBLL.GetEmployeeStudyBookInfoByTrainTypeID(trainTypeID,orgID,postID,leader,techID,0);

            if (bookList.Count > 0)
            {
                foreach (RailExam.Model.Book book in bookList)
                {
                     book.bookName = "<a onclick=OpenIndex('" + book.bookId + "') href=# title=" + book.bookName + " > " + book.bookName + " </a>";
                }

				Grid1.DataSource = bookList;
				Grid1.DataBind();
            }
        }

		private void BindGridCourseByTrainTypeID()
		{
			int trainTypeID = Convert.ToInt32(ViewState["TrainTypeID"].ToString());
			int postID = Convert.ToInt32(ViewState["PostID"].ToString());
			int orgID = Convert.ToInt32(ViewState["OrgID"].ToString());
			int techID = Convert.ToInt32(ViewState["Tech"].ToString());
			int leader = Convert.ToInt32(ViewState["Leader"].ToString());

			CoursewareBLL coursewareBLL = new CoursewareBLL();
			IList<RailExam.Model.Courseware> coursewareList = coursewareBLL.GetStudyCoursewareInfoByTrainTypeID(trainTypeID, orgID, postID, leader, techID, 0);


			if (coursewareList.Count > 0)
			{
				foreach (RailExam.Model.Courseware courseware in coursewareList)
				{
					courseware.CoursewareName = "<a onclick=OpenCourse('" + courseware.CoursewareID + "')  href=#  title=" + courseware.CoursewareName + ">" + courseware.CoursewareName + "</a>";
				}
				Grid2.DataSource = coursewareList;
				Grid2.DataBind();
			}
		}

		private void BindGridCourseByCoursewareTypeID()
		{
			int typeID = Convert.ToInt32(ViewState["KnowledgeID"].ToString());
			int postID = Convert.ToInt32(ViewState["PostID"].ToString());
			int orgID = Convert.ToInt32(ViewState["OrgID"].ToString());
			int techID = Convert.ToInt32(ViewState["Tech"].ToString());
			int leader = Convert.ToInt32(ViewState["Leader"].ToString());

			CoursewareBLL coursewareBLL = new CoursewareBLL();
			IList<RailExam.Model.Courseware> coursewareList = coursewareBLL.GetStudyCoursewareInfoByTypeID(typeID, orgID, postID, leader, techID, 0);

			if (coursewareList.Count > 0)
			{
				foreach (RailExam.Model.Courseware courseware in coursewareList)
				{
					courseware.CoursewareName = "<a onclick=OpenCourse('" + courseware.CoursewareID + "')  href=#  title=" + courseware.CoursewareName + ">" + courseware.CoursewareName + "</a>";
				}
				Grid2.DataSource = coursewareList;
				Grid2.DataBind();
			}
		}
    }
}
