using System;
using System.Collections.Generic;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Online.Study
{
    public partial class StudySelectedDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strKnowledge = Request.QueryString.Get("KnowledgeID");
                string strPostID = Request.QueryString.Get("PostID");
                string strOrgID = PrjPub.CurrentStudent.OrgID.ToString();
                string strLeader = PrjPub.CurrentStudent.TechnicianTypeID.ToString();
                string strTech = PrjPub.CurrentStudent.IsGroupLearder ? "1" : "0";

                ViewState["PostID"] = strPostID;
                ViewState["OrgID"] = strOrgID;
                ViewState["Leader"] = strLeader;
                ViewState["Tech"] = strTech;

                if (strKnowledge != "" && strKnowledge != null)
                {
                    ViewState["KnowledgeID"] = strKnowledge;
                    BindGridByKnowledgeID();
                    BindGridCourseByCoursewareTypeID();
                }
            }
        }

        private void BindGridByKnowledgeID()
        {
            int postID;
            if (!Int32.TryParse(ViewState["PostID"].ToString(), out postID))
            {
                return;
            }

            int knowledgeID = Convert.ToInt32(ViewState["KnowledgeID"]);

            int orgID = PrjPub.CurrentStudent.OrgID;
            int techID = PrjPub.CurrentStudent.TechnicianTypeID;
            int leader = PrjPub.CurrentStudent.IsGroupLearder ? 1 : 0;

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
        
        private void BindGridCourseByCoursewareTypeID()
        {
            int postID;
            if (!Int32.TryParse(ViewState["PostID"].ToString(), out postID))
            {
                return;
            }

            int typeID = Convert.ToInt32(ViewState["KnowledgeID"]);
            int orgID = PrjPub.CurrentStudent.OrgID;
            int techID = PrjPub.CurrentStudent.TechnicianTypeID;
            int leader = PrjPub.CurrentStudent.IsGroupLearder ? 1 : 0;

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
