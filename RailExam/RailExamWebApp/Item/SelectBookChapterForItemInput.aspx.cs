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
using RailExam.BLL;

namespace RailExamWebApp.Item
{
	public partial class SelectBookChapterForItemInput : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				BindKnowledgeTree();
			}
		}

		private void BindKnowledgeTree()
		{
			#region Bind knowledge tree

			KnowledgeBLL knowledgeBLL = new KnowledgeBLL();

			IList<RailExam.Model.Knowledge> knowledgeList = knowledgeBLL.GetKnowledges();

			if (knowledgeList.Count > 0)
			{
				TreeViewNode tvn = null;

				BookBLL bookBLL = new BookBLL();
				foreach (RailExam.Model.Knowledge knowledge in knowledgeList)
				{
					tvn = new TreeViewNode();
					tvn.ID = "knowledge"+knowledge.KnowledgeId.ToString();
					tvn.Value = knowledge.IdPath;
					tvn.Text = knowledge.KnowledgeName;
					tvn.ToolTip = knowledge.KnowledgeName;
					tvn.Attributes.Add("isKnowledge", "true");
					tvn.ImageUrl = "~/App_Themes/" + StyleSheetTheme + "/Images/TreeView/Knowledge.gif";
					//tvn.ContentCallbackUrl = "/RailExamBao/Common/GetKnowledgeBook.aspx?item=no&id=" + knowledge.IdPath;

					if (knowledge.ParentId == 0)
					{
						tvView.Nodes.Add(tvn);
					}
					else
					{
						try
						{
							tvView.FindNodeById("knowledge"+knowledge.ParentId.ToString()).Nodes.Add(tvn);
							IList<RailExam.Model.Knowledge> objList = knowledgeBLL.GetKnowledgesByParentID(knowledge.KnowledgeId);

							if(objList.Count == 0)
							{
								IList<RailExam.Model.Book> bookList = bookBLL.GetBookByKnowledgeIDPath(knowledge.IdPath);
								if (bookList.Count > 0)
								{
									foreach (RailExam.Model.Book book in bookList)
									{
										tvn = new TreeViewNode();
										tvn.ID = "book"+book.bookId.ToString();
										tvn.Value = book.bookId.ToString();
										tvn.Text = book.bookName;
										tvn.ToolTip = book.bookName;
										tvn.Attributes.Add("isBook", "true");
										tvn.ImageUrl = "~/App_Themes/" + StyleSheetTheme + "/Images/TreeView/Book.gif";

										tvView.FindNodeById("knowledge"+knowledge.KnowledgeId.ToString()).Nodes.Add(tvn);

										//添加章节
										BookChapterBLL bookChapterBLL = new BookChapterBLL();
										IList<RailExam.Model.BookChapter> bookChapterList = bookChapterBLL.GetBookChapterByBookID(book.bookId);

										if (bookChapterList.Count > 0)
										{
											foreach (RailExam.Model.BookChapter bookChapter in bookChapterList)
											{
												tvn = new TreeViewNode();
												tvn.ID = "chapter"+bookChapter.ChapterId.ToString();
												tvn.Value = bookChapter.BookId.ToString();
												tvn.Text = bookChapter.ChapterName;
												tvn.ToolTip = bookChapter.ChapterName;
												tvn.Attributes.Add("isChapter", "true");
												tvn.ImageUrl = "~/App_Themes/" + StyleSheetTheme + "/Images/TreeView/Chapter.gif";

												if (bookChapter.ParentId == 0)
												{
													tvView.FindNodeById("book"+bookChapter.BookId).Nodes.Add(tvn);
												}
												else
												{
													tvView.FindNodeById("chapter"+bookChapter.ParentId.ToString()).Nodes.Add(tvn);
												}
											}
										}
									}
								}
							}
						}
						catch
						{
							tvView.Nodes.Clear();
							SessionSet.PageMessage = "数据错误！";
							return;
						}
					}
				}
			}

			#endregion
		}

		protected void ddlViewChangeCallBack_Callback(object sender, CallBackEventArgs e)
		{
			tvView.RenderControl(e.Output);
		}
	}
}
