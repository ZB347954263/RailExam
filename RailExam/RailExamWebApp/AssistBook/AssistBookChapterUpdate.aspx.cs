using System;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;
namespace RailExamWebApp.AssistBook
{
    public partial class AssistBookChapterUpdate : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strMode = Request.QueryString.Get("Mode");
                if (strMode != "" && strMode != null && strMode == "ReadOnly")
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
                        AssistBookChapterBLL objChapterBll = new AssistBookChapterBLL();
                        chapterName =
                            objChapterBll.GetAssistBookChapter(Convert.ToInt32(chapterID)).ChapterName;
                    }

                    AssistBookBLL objBook = new AssistBookBLL();
                    lblBookName.Text = objBook.GetAssistBook(Convert.ToInt32(bookID)).BookName;
                    lblPerson.Text = PrjPub.CurrentLoginUser.EmployeeName;
                    lblDate.Text = DateTime.Today.ToLongDateString();

                    if (strUpdateObject == "delbook")
                    {
                        lblChapterName.Text = "删除辅导教材《" + lblBookName.Text + "》";
                    }
                    else if (strUpdateObject == "delchapter")
                    {
                        lblChapterName.Text = "删除辅导教材章节" + chapterName;
                    }
                    else if (strUpdateObject == "insertchapterinfo")
                    {
                        chapterName = Server.UrlDecode(Request.QueryString.Get("newchaptername"));
                        lblChapterName.Text = "新增《" + chapterName + "》辅导教材章节基本信息";
                    }
                    else if (strUpdateObject == "bookinfo")
                    {
                        lblChapterName.Text = PrjPub.ASSISTBOOKUPDATEOBJECT_BOOKINFO;
                    }
                    else if (strUpdateObject == "bookcover")
                    {
                        lblChapterName.Text = PrjPub.ASSISTBOOKUPDATEOBJECT_BOOKCOVER;
                    }
                    else if (strUpdateObject == "updatechapterinfo")
                    {
                        lblChapterName.Text = "修改《" + chapterName + "》辅导教材章节基本信息";
                    }
                    else if (strUpdateObject == "chaptercontent")
                    {
                        lblChapterName.Text = "《" + chapterName + "》" + PrjPub.ASSISTBOOKUPDATEOBJECT_CHAPTERCONTENT;
                    }

                    ViewState["AddFlag"] = 1;
                }
            }
        }

        private void FillPage(int nBookUpdateID)
        {
            AssistBookUpdateBLL BookUpdateBLL = new AssistBookUpdateBLL();

            AssistBookUpdate BookUpdate = BookUpdateBLL.GetAssistBookUpdate(nBookUpdateID);

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
                    lblChapterName.Text = "删除辅导教材《" + BookUpdate.BookNameBak + "》";
                }
                else if (strUpdateObject == "delchapter")
                {
                    lblBookName.Text = BookUpdate.BookName;
                    lblChapterName.Text = "删除辅导教材章节" + BookUpdate.ChapterNameBak;
                }
                else if (strUpdateObject == "insertchapterinfo")
                {
                    lblBookName.Text = BookUpdate.BookName;
                    lblChapterName.Text = "新增《" + BookUpdate.ChapterNameBak + "》辅导教材章节基本信息";
                }
                else if (strUpdateObject == "bookinfo")
                {
                    lblChapterName.Text = PrjPub.ASSISTBOOKUPDATEOBJECT_BOOKINFO;
                }
                else if (strUpdateObject == "bookcover")
                {
                    lblChapterName.Text = PrjPub.ASSISTBOOKUPDATEOBJECT_BOOKCOVER;
                    lblBookName.Text = BookUpdate.BookName;
                }
                else if (strUpdateObject == "updatechapterinfo")
                {
                    lblChapterName.Text = "修改《" + BookUpdate.ChapterNameBak + "》辅导教材章节基本信息";
                    lblBookName.Text = BookUpdate.BookName;
                }
                else if (strUpdateObject == "chaptercontent")
                {
                    lblChapterName.Text = "《" + BookUpdate.ChapterNameBak + "》" + PrjPub.ASSISTBOOKUPDATEOBJECT_CHAPTERCONTENT;
                    lblBookName.Text = BookUpdate.BookName;
                }
            }

            ViewState["AddFlag"] = 0;
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            AssistBookUpdateBLL BookUpdateBLL = new AssistBookUpdateBLL();
            AssistBookUpdate BookUpdate = new AssistBookUpdate();

            string strUpdateObject = Request.QueryString.Get("Object");

            if (txtCause.Text == string.Empty)
            {
                SessionSet.PageMessage = "更改原因不能为空！";
                return;
            }

            if (txtContent.Text == string.Empty)
            {
                SessionSet.PageMessage = "更改内容不能为空！";
                return;
            }

            if (ViewState["AddFlag"].ToString() == "1")     //新增
            {
                AssistBookBLL objBookBll = new AssistBookBLL();
                BookUpdate.AssistBookId = int.Parse(ViewState["BookID"].ToString());
                BookUpdate.ChapterId = int.Parse(ViewState["ChapterID"].ToString());
                BookUpdate.updatePerson = lblPerson.Text;
                BookUpdate.updateDate = DateTime.Parse(lblDate.Text);
                BookUpdate.updateCause = txtCause.Text;
                BookUpdate.updateContent = txtContent.Text;
                BookUpdate.Memo = txtMemo.Text;
                BookUpdate.BookNameBak = objBookBll.GetAssistBook(Convert.ToInt32(ViewState["BookID"].ToString())).BookName;
                if (ViewState["ChapterID"].ToString() != "0")
                {
                    AssistBookChapterBLL objChapterBll = new AssistBookChapterBLL();
                    BookUpdate.ChapterNameBak =
                        objChapterBll.GetAssistBookChapter(Convert.ToInt32(ViewState["ChapterID"].ToString())).ChapterName;
                }
                if (strUpdateObject == "insertchapterinfo")
                {
                    BookUpdate.ChapterNameBak = Server.UrlDecode(Request.QueryString.Get("newchaptername"));
                    BookUpdate.UpdateObject = PrjPub.ASSISTBOOKUPDATEOBJECT_INSERTCHAPTERINFO;
                }
                else if (strUpdateObject == "updatechapterinfo")
                {
                    BookUpdate.UpdateObject = PrjPub.ASSISTBOOKUPDATEOBJECT_UPDATECHAPTERINFO;
                }
                else if (strUpdateObject == "chaptercontent")
                {
                    BookUpdate.UpdateObject = PrjPub.ASSISTBOOKUPDATEOBJECT_CHAPTERCONTENT;
                }
                else if (strUpdateObject == "delbook")
                {
                    BookUpdate.UpdateObject = PrjPub.ASSISTBOOKUPDATEOBJECT_DELBOOK;
                }
                else if (strUpdateObject == "delchapter")
                {
                    BookUpdate.UpdateObject = PrjPub.ASSISTBOOKUPDATEOBJECT_DELCHAPTER;
                }
                else
                {
                    BookUpdate.UpdateObject = lblChapterName.Text;
                }
                BookUpdateBLL.AddAssistBookUpdate(BookUpdate);

                Response.Write("<script>top.returnValue='true';window.close();</script>");
            }
            else        //修改
            {
                BookUpdate.AssistBookId = int.Parse(ViewState["BookID"].ToString());
                BookUpdate.ChapterId = int.Parse(ViewState["ChapterID"].ToString());
                BookUpdate.updatePerson = lblPerson.Text;
                BookUpdate.updateDate = DateTime.Parse(lblDate.Text);
                BookUpdate.updateCause = txtCause.Text;
                BookUpdate.updateContent = txtContent.Text;
                BookUpdate.Memo = txtMemo.Text;

                BookUpdate.AssistBookUpdateId = int.Parse(Request.QueryString.Get("id"));
                BookUpdateBLL.UpdateAssistBookUpdate(BookUpdate);

                Response.Write("<script>window.opener.form1.RefreshGrid.value='true';window.opener.form1.submit();window.close();</script>");
            }
        }
    }
}
