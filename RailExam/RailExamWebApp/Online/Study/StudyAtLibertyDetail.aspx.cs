using System;
using System.Collections.Generic;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Online.Study
{
    public partial class StudyAtLibertyDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strKnowledge = Request.QueryString.Get("KnowledgeID");
                string strPostID = Request.QueryString.Get("PostID");

                ViewState["PostID"] = strPostID;

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
            int knowledgeID = Convert.ToInt32(ViewState["KnowledgeID"]);

            BookBLL bookBLL = new BookBLL();
            IList<RailExam.Model.Book> bookList = bookBLL.GetBookByKnowledgeID(knowledgeID, 200);
            if (ViewState["PostID"] != null && ViewState["PostID"].ToString() != "undefined")
            {
                int postID = Int32.Parse(ViewState["PostID"].ToString());
                bookList = bookBLL.GetEmployeeStudyBookInfoByKnowledgeID(knowledgeID, 200, postID, 0, 1, 0);

            }

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
            int typeID = Convert.ToInt32(ViewState["KnowledgeID"]);

            CoursewareBLL coursewareBLL = new CoursewareBLL();
            IList<RailExam.Model.Courseware> coursewareList = coursewareBLL.GetCoursewaresByCoursewareTypeID(typeID, 200);

            if (ViewState["PostID"] != null && ViewState["PostID"].ToString() != "undefined")
            {
                int postID = Int32.Parse(ViewState["PostID"].ToString());
                coursewareList = coursewareBLL.GetStudyCoursewareInfoByTypeID(typeID, 200, postID, 0, 1, 0);
            }

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
