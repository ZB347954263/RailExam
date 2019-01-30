using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Item
{
	public partial class ItemBookChapter : PageBase
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
				string strBookID = Request.QueryString.Get("id");
				ViewState["BookID"] = strBookID;
				if (!string.IsNullOrEmpty(strBookID))
				{
					hfBookID.Value = strBookID;

					BookBLL objbookbill = new BookBLL();
					RailExam.Model.Book objBook = objbookbill.GetBook(Convert.ToInt32(hfBookID.Value));

					if (PrjPub.CurrentLoginUser.SuitRange == 0 && PrjPub.IsServerCenter)
					{
						if (PrjPub.CurrentLoginUser.StationOrgID != objBook.publishOrg)
						{
							Response.Write("<script>alert('您没有导入当前教材章节试题的权限！');window.close();</script>");
							return;
						}
					}

					BindTree();
				}

				string strPath = Server.MapPath("../Online/Book/" + ViewState["BookID"].ToString());
				if (!Directory.Exists(strPath))
				{
					Directory.CreateDirectory(strPath);
					Directory.CreateDirectory(strPath + "/Upload");
					CopyTemplate(Server.MapPath("../Online/Book/template/"),
					             Server.MapPath("../Online/Book/" + ViewState["BookID"].ToString() + "/"));
				}
			}
		}

		private void BindTree()
		{
			//添加书名
			BookBLL bookBLL = new BookBLL();

			RailExam.Model.Book book = bookBLL.GetBook(Convert.ToInt32(ViewState["BookID"].ToString()));

			TreeViewNode tvn1 = new TreeViewNode();
			tvn1.ID = "0";
			tvn1.Value = ViewState["BookID"].ToString();
			tvn1.Text = book.bookName;
			tvn1.ToolTip = book.bookName;
			tvBookChapter.Nodes.Add(tvn1);

			//添加章节
			BookChapterBLL bookChapterBLL = new BookChapterBLL();

			IList<RailExam.Model.BookChapter> bookChapterList = bookChapterBLL.GetBookChapterByBookID(Convert.ToInt32(ViewState["BookID"].ToString()));

			if (bookChapterList.Count > 0)
			{
				TreeViewNode tvn = null;

				foreach (RailExam.Model.BookChapter bookChapter in bookChapterList)
				{
					tvn = new TreeViewNode();
					tvn.ID = bookChapter.ChapterId.ToString();
					tvn.Value = bookChapter.BookId.ToString();
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
			BookChapterBLL objBookChapter = new BookChapterBLL();
			RailExam.Model.BookChapter obj = new RailExam.Model.BookChapter();
			obj = objBookChapter.GetBookChapterInfo(int.Parse(e.Parameters[0]));
			int cout = tvBookChapter.FindNodeById(obj.ParentId.ToString()).Nodes.Count;

			if (e.Parameters[1] == "MoveUp")
			{
				if (obj.OrderIndex <= cout && obj.OrderIndex >= 2)
				{
					obj.OrderIndex--;
					if (e.Parameters[2] == "Edit")
					{
						obj.IsEdit = true;
					}
					else
					{
						obj.IsEdit = false;
					}
					objBookChapter.UpdateBookChapter(obj);

					obj = objBookChapter.GetBookChapter(int.Parse(tvBookChapter.FindNodeById(e.Parameters[0]).PreviousSibling.ID));
					obj.OrderIndex++;
					objBookChapter.UpdateBookChapter(obj);
				}
			}
			if (e.Parameters[1] == "MoveDown")
			{
				if (obj.OrderIndex <= cout - 1 && obj.OrderIndex >= 1)
				{
					obj.OrderIndex++;
					if (e.Parameters[2] == "Edit")
					{
						obj.IsEdit = true;
					}
					else
					{
						obj.IsEdit = false;
					}
					objBookChapter.UpdateBookChapter(obj);

					obj = objBookChapter.GetBookChapter(int.Parse(tvBookChapter.FindNodeById(e.Parameters[0]).NextSibling.ID));
					obj.OrderIndex--;
					objBookChapter.UpdateBookChapter(obj);
				}
			}
			if (e.Parameters[1] == "Insert")
			{
				int maxID = objBookChapter.GetMaxChapterIDByBookID(Convert.ToInt32(hfBookID.Value));
				hfMaxID.Value = maxID.ToString();
				hfMaxID.RenderControl(e.Output);
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
