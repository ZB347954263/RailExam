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
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Online.Study
{
    public partial class StudyByKnowledgeDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if(Request.QueryString.Get("Book") != null)
                {
                    BindBook();
                }
                else
                {
                    BindCourse();
                }
            }
        }

        private void BindBook()
        {
            bool isGroup = PrjPub.CurrentStudent.IsGroupLearder;
            int tech = PrjPub.CurrentStudent.TechnicianTypeID;
            int orgID = PrjPub.CurrentStudent.OrgID;
            int postID = PrjPub.CurrentStudent.PostID;

            string strIDPath = Request.QueryString.Get("Book");

            BookBLL objBll =new BookBLL();
            IList<RailExam.Model.Book> objBook = objBll.GetBookByKnowledgeOnline(orgID, postID,strIDPath, isGroup, tech);


            if (objBook.Count > 0)
            {
                foreach (RailExam.Model.Book book in objBook)
                {
                    if (book.bookName.Length <= 15)
                    {
                        book.bookName = "<a onclick=OpenIndex('" + book.bookId + "') href=# title=" + book.bookName + " > " + book.bookName + " </a>";
                    }
                    else
                    {
                        book.bookName = "<a onclick=OpenIndex('" + book.bookId + "') href=# title=" + book.bookName + " > " + book.bookName.Substring(0, 15) + " </a>";
                    }
                }
            }

            gvBook.DataSource = objBook;
            gvBook.DataBind();

            gvBook.Visible = true;
            gvCourse.Visible = false;
        }

        private void BindCourse()
        {
            bool isGroup = PrjPub.CurrentStudent.IsGroupLearder;
            int tech = PrjPub.CurrentStudent.TechnicianTypeID;
            int orgID = PrjPub.CurrentStudent.OrgID;
            int postID = PrjPub.CurrentStudent.PostID;

            string strIDPath = Request.QueryString.Get("Courseware");
            CoursewareBLL objBll = new CoursewareBLL();
            IList<RailExam.Model.Courseware> objCourseware =
                objBll.GetCoursewaresByCoursewareTypeOnline(orgID, postID, strIDPath, isGroup, tech);

            if (objCourseware.Count > 0)
            {
                foreach (RailExam.Model.Courseware courseware in objCourseware)
                {
                    if (courseware.CoursewareName.Length <= 15)
                    {
                        courseware.CoursewareName =  courseware.CoursewareName;
                    }
                    else
                    {
                        courseware.CoursewareName = courseware.CoursewareName.Substring(0, 15) + "...";
                    }
                }
            }

            gvCourse.DataSource = objCourseware;
            gvCourse.DataBind();

            gvBook.Visible = false;
            gvCourse.Visible = true;
        }
    }
}
