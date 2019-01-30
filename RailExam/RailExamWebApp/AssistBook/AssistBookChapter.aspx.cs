using System;
using System.Collections.Generic;
using System.IO;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;

namespace RailExamWebApp.AssistBook
{
    public partial class AssistBookChapter : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strBookID = Request.QueryString.Get("id");
                ViewState["BookID"] = strBookID;
                if (!string.IsNullOrEmpty(strBookID))
                {
                    hfBookID.Value = strBookID;
                    BindTree();
                }

                string strPath = Server.MapPath("../Online/AssistBook/" + ViewState["BookID"].ToString());
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                    Directory.CreateDirectory(strPath + "/Upload");
                    CopyTemplate(Server.MapPath("../Online/AssistBook/template/"),
                                 Server.MapPath("../Online/AssistBook/" + ViewState["BookID"].ToString() + "/"));
                }
            }
        }

        private void BindTree()
        {
            //添加书名
            AssistBookBLL bookBLL = new AssistBookBLL();

            RailExam.Model.AssistBook book = bookBLL.GetAssistBook(Convert.ToInt32(ViewState["BookID"].ToString()));

            TreeViewNode tvn1 = new TreeViewNode();
            tvn1.ID = "0";
            tvn1.Value = ViewState["BookID"].ToString();
            tvn1.Text = book.BookName;
            tvn1.ToolTip = book.BookName;
            tvBookChapter.Nodes.Add(tvn1);

            //添加章节
            AssistBookChapterBLL bookChapterBLL = new AssistBookChapterBLL();

            IList<RailExam.Model.AssistBookChapter> bookChapterList = bookChapterBLL.GetAssistBookChapterByBookID(Convert.ToInt32(ViewState["BookID"].ToString()));

            if (bookChapterList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (RailExam.Model.AssistBookChapter bookChapter in bookChapterList)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = bookChapter.ChapterId.ToString();
                    tvn.Value = bookChapter.AssistBookId.ToString();
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

        protected void tvBookChapterChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {
            AssistBookChapterBLL objBookChapter = new AssistBookChapterBLL();
            RailExam.Model.AssistBookChapter obj = new RailExam.Model.AssistBookChapter();
            obj = objBookChapter.GetAssistBookChapterInfo(int.Parse(e.Parameters[0]));
            int cout = tvBookChapter.FindNodeById(obj.ParentId.ToString()).Nodes.Count;

            if (e.Parameters[1] == "MoveUp")
            {
                if (obj.OrderIndex <= cout && obj.OrderIndex >= 2)
                {
                    obj.OrderIndex--;
                    objBookChapter.UpdateAssistBookChapter(obj);

                    obj = objBookChapter.GetAssistBookChapter(int.Parse(tvBookChapter.FindNodeById(e.Parameters[0]).PreviousSibling.ID));
                    obj.OrderIndex++;
                    objBookChapter.UpdateAssistBookChapter(obj);
                }
            }
            if (e.Parameters[1] == "MoveDown")
            {
                if (obj.OrderIndex <= cout - 1 && obj.OrderIndex >= 1)
                {
                    obj.OrderIndex++;
                    objBookChapter.UpdateAssistBookChapter(obj);

                    obj = objBookChapter.GetAssistBookChapter(int.Parse(tvBookChapter.FindNodeById(e.Parameters[0]).NextSibling.ID));
                    obj.OrderIndex--;
                    objBookChapter.UpdateAssistBookChapter(obj);
                }
            }

            tvBookChapter.Nodes.Clear();
            BindTree();
            tvBookChapter.RenderControl(e.Output);
        }

        protected void tvBookChapterMoveCallBack_Callback(object sender, CallBackEventArgs e)
        {
            TreeViewNode node = tvBookChapter.FindNodeById(e.Parameters[0]);

            if (node != null && e.Parameters[1] == "CanMoveUp")
            {
                if (node.PreviousSibling != null)
                {
                    hfCanMove.Value = "true";
                    hfCanMove.RenderControl(e.Output);
                }
                else
                {
                    hfCanMove.Value = string.Empty;
                    hfCanMove.RenderControl(e.Output);
                }
            }
            else if (node != null && e.Parameters[1] == "CanMoveDown")
            {
                if (node.NextSibling != null)
                {
                    hfCanMove.Value = "true";
                    hfCanMove.RenderControl(e.Output);
                }
                else
                {
                    hfCanMove.Value = string.Empty;
                    hfCanMove.RenderControl(e.Output);
                }
            }
        }

        private static void CopyTemplate(string srcPath, string aimPath)
        {
            if (!Directory.Exists(aimPath))
            {
                Directory.CreateDirectory(aimPath);
            }

            string[] fileList = Directory.GetFileSystemEntries(srcPath);

            foreach (string file in fileList)
            {
                if (Directory.Exists(file))
                {
                    CopyTemplate(file, aimPath + Path.GetFileName(file) + "\\");
                }
                else
                {
                    File.Copy(file, aimPath + Path.GetFileName(file), true);
                }
            }
        }
    }
}
