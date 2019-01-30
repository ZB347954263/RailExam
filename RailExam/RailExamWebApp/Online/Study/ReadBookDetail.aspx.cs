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
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;

namespace RailExamWebApp.Online.Study
{
    public partial class ReadBookDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            	BindCoursewareGrid();
            }
        }

        private void BindGrid()
        {
            string strKnowledgeID = Request.QueryString.Get("id");
            IList<RailExam.Model.Book> bookList = new List<RailExam.Model.Book>();

            if (strKnowledgeID != null & strKnowledgeID != "")
            {
                BookBLL bookBLL = new BookBLL();
                bookList = bookBLL.GetBookByKnowledgeID(Convert.ToInt32(strKnowledgeID),1);
            }

            string strtypeID = Request.QueryString.Get("id1");
            if (strtypeID != null & strtypeID != "")
            {
                BookBLL bookBLL = new BookBLL();
                bookList = bookBLL.GetBookByTrainTypeID(Convert.ToInt32(strtypeID),1);
            }

            foreach (RailExam.Model.Book book in bookList)
            {
				book.bookName = "<a onclick=OpenIndex('" + book.bookId + "') href=# title=" + book.bookName + " > " + book.bookName + " </a>";
            }

            Grid1.DataSource = bookList;
            Grid1.DataBind();
        }

		private void BindCoursewareGrid()
		{
			CoursewareBLL coursewareBLL = new CoursewareBLL();
			IList<RailExam.Model.Courseware> coursewares = new List<RailExam.Model.Courseware>();

			string strKnowledgeID = Request.QueryString.Get("id");
			IList<RailExam.Model.Book> bookList = new List<RailExam.Model.Book>();

			if (strKnowledgeID != null & strKnowledgeID != "" & strKnowledgeID != "0")
			{
				coursewares = coursewareBLL.GetCoursewaresByCoursewareTypeID(Convert.ToInt32(strKnowledgeID), 1);
			}

			string strtypeID = Request.QueryString.Get("id1");
			if (strtypeID != null & strtypeID != "" && strtypeID != "0")
			{
				coursewares = coursewareBLL.GetCoursewaresByTrainTypeID(Convert.ToInt32(strtypeID), 1);
			}

			foreach (RailExam.Model.Courseware courseware in coursewares)
			{
				courseware.CoursewareName = "<a onclick=OpenCourse('" + courseware.CoursewareID + "')  href=#  title=" + courseware.CoursewareName + ">" + courseware.CoursewareName + "</a>";
			}
			Grid2.DataSource = coursewares;
			Grid2.DataBind();
		}
    }
}
