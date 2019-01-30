using System;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Book
{
    public partial class BookChapterUpdate : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)            
            {
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }
                string strMode = Request.QueryString.Get("Mode");
                if(strMode != "" && strMode!=null && strMode == "ReadOnly")
                {
                    btnSave.Visible = false;
                    CancelButton.Visible = true;
                }
                else
                {
                    btnSave.Visible = true;
                    CancelButton.Visible = false;
                }

                string strUpdateObject = Request.QueryString.Get("Object");
                string strBookChapterUpdateID = Request.QueryString.Get("id");
                string bookID = Request.QueryString.Get("BookID");
                string chapterID = Request.QueryString.Get("ChapterID");
                ViewState["BookID"] = bookID;
                ViewState["ChapterID"] = chapterID;

                string chapterName = "";

                if (!string.IsNullOrEmpty(strBookChapterUpdateID)) //修改
                {
                    FillPage(int.Parse(strBookChapterUpdateID));
                }
                else
                {
                    if (ViewState["ChapterID"].ToString() != "0")
                    {
                        BookChapterBLL objChapterBll = new BookChapterBLL();
                        chapterName =
                            objChapterBll.GetBookChapter(Convert.ToInt32(chapterID)).ChapterName;
                    }

                    BookBLL objBook = new BookBLL();
                    lblBookName.Text = objBook.GetBook(Convert.ToInt32(bookID)).bookName;
                    lblPerson.Text = PrjPub.CurrentLoginUser.EmployeeName;
                    lblDate.Text = DateTime.Today.ToLongDateString();

                    if (strUpdateObject == "delbook")
                    {
                        lblChapterName.Text = "删除教材《" + lblBookName.Text + "》";
                    }
                    else if (strUpdateObject == "delchapter")
                    {
                        lblChapterName.Text = "删除章节" + chapterName;
                    }
                    else if (strUpdateObject == "insertchapterinfo")
                    {
                        chapterName = Server.UrlDecode(Request.QueryString.Get("newchaptername"));
                        lblChapterName.Text = "新增《" + chapterName + "》章节基本信息";
                    }
                    else if (strUpdateObject == "bookinfo")
                    {
                        lblChapterName.Text = PrjPub.BOOKUPDATEOBJECT_BOOKINFO;
                    }
                    else if (strUpdateObject == "bookcover")
                    {
                        lblChapterName.Text = PrjPub.BOOKUPDATEOBJECT_BOOKCOVER;
                    }
                    else if (strUpdateObject == "updatechapterinfo")
                    {
                        lblChapterName.Text = "修改《" + chapterName + "》章节基本信息";
                    }
                    else if (strUpdateObject == "chaptercontent")
                    {
                        lblChapterName.Text = "《" + chapterName + "》" + PrjPub.BOOKUPDATEOBJECT_CHAPTERCONTENT;
                    }

                    ViewState["AddFlag"] = 1;
                }
            }
        }

        private void FillPage(int nBookUpdateID)
        {
            BookUpdateBLL BookUpdateBLL = new BookUpdateBLL();

            BookUpdate BookUpdate = BookUpdateBLL.GetBookUpdate(nBookUpdateID);

            if (BookUpdate != null)
            {
                lblPerson.Text = BookUpdate.updatePerson;
                lblDate.Text = BookUpdate.updateDate.ToShortDateString();
                txtCause.Text = BookUpdate.updateCause;
                txtContent.Text = BookUpdate.updateContent;
                txtMemo.Text = BookUpdate.Memo;

                string strUpdateObject = Request.QueryString.Get("Object");
                if (strUpdateObject == "delbook")
                {
                    lblBookName.Text = BookUpdate.BookNameBak;
                    lblChapterName.Text = "删除教材《" + BookUpdate.BookNameBak+ "》";
                }
                else if (strUpdateObject == "delchapter")
                {
                    lblBookName.Text = BookUpdate.BookName;
                    lblChapterName.Text = "删除章节" + BookUpdate.ChapterNameBak;
                }
                else if (strUpdateObject == "insertchapterinfo")
                {
                    lblBookName.Text = BookUpdate.BookName;
                    lblChapterName.Text = "新增《" + BookUpdate.ChapterNameBak + "》章节基本信息";
                }
                else if (strUpdateObject == "bookinfo")
                {
                    lblChapterName.Text = PrjPub.BOOKUPDATEOBJECT_BOOKINFO;
                }
                else if (strUpdateObject == "bookcover")
                {
                    lblChapterName.Text = PrjPub.BOOKUPDATEOBJECT_BOOKCOVER;
                    lblBookName.Text = BookUpdate.BookName;
                }
                else if (strUpdateObject == "updatechapterinfo")
                {
                    lblChapterName.Text = "修改《" + BookUpdate.ChapterNameBak + "》章节基本信息";
                    lblBookName.Text = BookUpdate.BookName;
                }
                else if (strUpdateObject == "chaptercontent")
                {
                    lblChapterName.Text = "《" + BookUpdate.ChapterNameBak + "》" + PrjPub.BOOKUPDATEOBJECT_CHAPTERCONTENT;
                    lblBookName.Text = BookUpdate.BookName;
                }
            }

            ViewState["AddFlag"] = 0;
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            BookUpdateBLL BookUpdateBLL = new BookUpdateBLL();
            BookUpdate BookUpdate = new BookUpdate();

            string strUpdateObject = Request.QueryString.Get("Object");

            if(txtCause.Text == string.Empty)
            {
                SessionSet.PageMessage = "更改原因不能为空！";
                return;
            }

            if(txtContent.Text == string.Empty)
            {
                SessionSet.PageMessage = "更改内容不能为空！";
                return;
            }

            if (ViewState["AddFlag"].ToString() == "1")     //新增
            {
                BookBLL objBookBll = new BookBLL();
                BookUpdate.BookId = int.Parse(ViewState["BookID"].ToString());
                BookUpdate.ChapterId = int.Parse(ViewState["ChapterID"].ToString());
                BookUpdate.updatePerson = lblPerson.Text;
                BookUpdate.updateDate = DateTime.Parse(lblDate.Text);
                BookUpdate.updateCause = txtCause.Text;
                BookUpdate.updateContent = txtContent.Text;
                BookUpdate.Memo = txtMemo.Text;
                BookUpdate.BookNameBak = objBookBll.GetBook(Convert.ToInt32(ViewState["BookID"].ToString())).bookName;
                if(ViewState["ChapterID"].ToString() != "0")
                {
                    BookChapterBLL objChapterBll = new BookChapterBLL();
                    BookUpdate.ChapterNameBak =
                        objChapterBll.GetBookChapter(Convert.ToInt32(ViewState["ChapterID"].ToString())).ChapterName;
                }
                if (strUpdateObject == "insertchapterinfo")
                {
                    BookUpdate.ChapterNameBak = Server.UrlDecode(Request.QueryString.Get("newchaptername"));
                    BookUpdate.UpdateObject = PrjPub.BOOKUPDATEOBJECT_INSERTCHAPTERINFO;
                }
                else if(strUpdateObject == "updatechapterinfo")
                {
                    BookUpdate.UpdateObject = PrjPub.BOOKUPDATEOBJECT_UPDATECHAPTERINFO;
                }
                else if (strUpdateObject == "chaptercontent")
                {
                    BookUpdate.UpdateObject = PrjPub.BOOKUPDATEOBJECT_CHAPTERCONTENT;
                }
                else if(strUpdateObject == "delbook")
                {
                    BookUpdate.UpdateObject = PrjPub.BOOKUPDATEOBJECT_DELBOOK;
                }
                else if(strUpdateObject == "delchapter")
                {
                    BookUpdate.UpdateObject = PrjPub.BOOKUPDATEOBJECT_DELCHAPTER;
                }
                else
                {
                    BookUpdate.UpdateObject = lblChapterName.Text;
                }
                BookUpdateBLL.AddBookUpdate(BookUpdate);

                 Response.Write("<script>top.returnValue='true';window.close();</script>");
            }
            else        //修改
            {
                BookUpdate.BookId = int.Parse(ViewState["BookID"].ToString());
                BookUpdate.ChapterId = int.Parse(ViewState["ChapterID"].ToString());
                BookUpdate.updatePerson = lblPerson.Text;
                BookUpdate.updateDate = DateTime.Parse(lblDate.Text);
                BookUpdate.updateCause = txtCause.Text;
                BookUpdate.updateContent = txtContent.Text;
                BookUpdate.Memo = txtMemo.Text;

                BookUpdate.bookUpdateId = int.Parse(Request.QueryString.Get("id"));
                BookUpdateBLL.UpdateBookUpdate(BookUpdate);

				if(PrjPub.IsWuhan())
				{
					Response.Write("<script>window.opener.form1.RefreshGrid.value='true';window.opener.form1.submit();window.close();</script>");
				}
				else
				{
					Response.Write("<script>top.returnValue='true';window.close();</script>");
				}
            }
        }
    }
}