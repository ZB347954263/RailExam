using System;
using System.Data;
using System.Configuration;
using System.Collections;
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
using ComponentArt.Web.UI;
using System.Collections.Generic;

namespace RailExamWebApp.Main
{
    public partial class FocalPointStudyInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string subjectID = Request.QueryString.Get("id");
				string classID = Request.QueryString.Get("classID");
                if (!String.IsNullOrEmpty(subjectID))
                {
                    BindGrid(subjectID,"subject");
                }
				if (!string.IsNullOrEmpty(classID))
			    {
					BindGrid(classID, "class");
			    }
            }
        }

        private void BindGrid(string subjectID,string type)
        {
            BookBLL bookBLL = new BookBLL();
            IList<RailExam.Model.Book> books = new List<RailExam.Model.Book>();

            OracleAccess oracle = new OracleAccess();
            string sql = String.Format("select book_id from ZJ_TRAIN_CLASS_SUBJECT_BOOK t where train_class_subject_id = {0}", subjectID);
			if (type == "class")
				sql =string.Format(
							@"
                              select book_id from ZJ_TRAIN_CLASS_SUBJECT_BOOK t 
							  inner join zj_train_class_subject cs 
								   on cs.train_class_subject_id=t.train_class_subject_id
							  where cs.train_class_id={0}
                             ",subjectID);
			DataSet dsBookID = oracle.RunSqlDataSet(sql);
            if (dsBookID != null && dsBookID.Tables.Count > 0)
            {
                foreach (DataRow row in dsBookID.Tables[0].Rows)
                {
                    string bookID = row[0].ToString();
                    if (!String.IsNullOrEmpty(bookID))
                    {
                        RailExam.Model.Book book = bookBLL.GetBook(Int32.Parse(bookID));
                        if (book != null)
                        {
                            books.Add(book);
                        }
                    }
                }
            }

            if (books.Count > 0)
            {
                foreach (RailExam.Model.Book book in books)
                {
                    if (book.bookName.Length <= 15)
                    {
                        book.bookName = "<a onclick=OpenIndex('" + book.bookId + "') href=# title=" + book.bookName + " > " + book.bookName + " </a>";
                    }
                    else
                    {
                        book.bookName = "<a onclick=OpenIndex('" + book.bookId + "') href=# title=" + book.bookName + " > " + book.bookName.Substring(0, 15) + "...</a>";
                    }
                }

                Grid1.DataSource = books;
                Grid1.DataBind();
            }
        }
    }
}
