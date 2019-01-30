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

namespace RailExamWebApp.Online.Study
{
    public partial class NewStudyInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid1();
                BindGrid2();
            }
        }

        private void BindGrid1()
        {
            BookBLL bookBLL = new BookBLL();
            IList<RailExam.Model.Book> bookList = bookBLL.GetBookInfoByDate(6);

            if (bookList.Count > 0)
            {
                foreach (RailExam.Model.Book book in bookList)
                {
                    book.bookName = "<a onclick=OpenIndex('" + book.bookId + "') href=# > " + book.bookName + "（" + book.publishOrgName + "，" + book.authors + "，" + book.publishDate.ToString("yyyy-MM-dd") + "）" + " </a>";
                }
            }

            gvBook.DataSource = bookList;
            gvBook.DataBind();
        }

        private void BindGrid2()
        {
            CoursewareBLL coursewareBLL = new CoursewareBLL();
            IList<RailExam.Model.Courseware> coursewareList = coursewareBLL.GetCoursewareInfoByDate(6);
            if (coursewareList.Count > 0)
            {
                foreach (RailExam.Model.Courseware courseware in coursewareList)
                {
                    courseware.CoursewareName = courseware.CoursewareName + "（" + courseware.ProvideOrgName + "，" + courseware.Authors + "，" + courseware.PublishDate.ToString("yyyy-MM-dd") + "）";
                }
            }
            gvCourse.DataSource = coursewareList;
            gvCourse.DataBind();
        }
    }
}