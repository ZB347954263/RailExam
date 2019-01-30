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
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;

namespace RailExamWebApp.Exercise
{
    public partial class ExerciseManage : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strBookID = Request.QueryString.Get("id");
            if (!string.IsNullOrEmpty(strBookID))
            {
                hfBookID.Value = strBookID;
                BindTree(strBookID);
            }
        }

        private void BindTree(string strBookID)
        {
            BookBLL bookBLL = new BookBLL();

            RailExam.Model.Book book = bookBLL.GetBook(int.Parse(strBookID));

            TreeViewNode tvn1 = new TreeViewNode();
            tvn1.ID = "0";
            tvn1.Value = book.bookId.ToString();
            tvn1.Text = book.bookName;
            tvn1.ToolTip = book.bookName;
            tvBookChapter.Nodes.Add(tvn1);

            hfChapterID.Value = "0";

            BookChapterBLL bookChapterBLL = new BookChapterBLL();

            IList<RailExam.Model.BookChapter> bookChapterList = bookChapterBLL.GetBookChapterByBookID(int.Parse(strBookID));

            if (bookChapterList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (RailExam.Model.BookChapter bookChapter in bookChapterList)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = bookChapter.ChapterId.ToString();
                    tvn.Value = bookChapter.ChapterId.ToString();
                    tvn.Text = bookChapter.ChapterName;
                    tvn.ToolTip = bookChapter.ChapterName;

                    if (bookChapter.ParentId == 0)
                    {
                        //tvBookChapter.Nodes.Add(tvn);
                        tvBookChapter.FindNodeById(bookChapter.ParentId.ToString()).Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            tvBookChapter.FindNodeById(bookChapter.ParentId.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvBookChapter.Nodes.Clear();
                            SessionSet.PageMessage = "数据错误！";

                            return;
                        }
                    }
                }
            }

            tvBookChapter.DataBind();
        	tvBookChapter.ExpandAll();
        }
    }
}