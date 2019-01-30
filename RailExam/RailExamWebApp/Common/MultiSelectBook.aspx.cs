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
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;

namespace RailExamWebApp.Common
{
	public partial class MultiSelectBook : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				BindTree();
			}
		}

		private void BindTree()
		{
			string strBookID = Request.QueryString.Get("BookID");
			string strSelectedID = Request.QueryString.Get("StrategyID");
			string strItemTypeID = Request.QueryString.Get("itemTypeID");

			BookBLL objBll = new BookBLL();
			RailExam.Model.Book obj = objBll.GetBook(Convert.ToInt32(strBookID));

			ItemBLL objItemBll = new ItemBLL();

			TreeViewNode node = new TreeViewNode();
			int m = objItemBll.GetItemsByBookID(obj.bookId, Convert.ToInt32(strItemTypeID));
			if (m > 0)
			{
				node.Text = obj.bookName + "£®" + m + "Ã‚£©";
			}
			else
			{
				node.Text = obj.bookName;
			} 
			node.ID = obj.bookId.ToString();

			tvBook.Nodes.Add(node);

			BookChapterBLL objChapterBll = new BookChapterBLL();
			IList<RailExam.Model.BookChapter> objChapterList = objChapterBll.GetBookChapterByBookID(Convert.ToInt32(strBookID));

			TreeViewNode tvn = null;

			foreach (BookChapter bookChapter in objChapterList)
			{
                if (bookChapter.IsMotherItem)
                {
                    continue;
                }

				tvn = new TreeViewNode();
				tvn.ID = bookChapter.ChapterId.ToString();
				tvn.Value = bookChapter.ChapterId.ToString();
				int n = objItemBll.GetItemsByBookChapterIdPath(bookChapter.IdPath,Convert.ToInt32(strItemTypeID));
				if (n > 0)
				{
					tvn.Text = bookChapter.ChapterName + "£®" + n + "Ã‚£©";
				}
				else
				{
					tvn.Text = bookChapter.ChapterName;
				}

				tvn.ToolTip = bookChapter.ChapterName;
				tvn.ShowCheckBox = true;

				if (Request.QueryString.Get("StrategyID") != null)
				{
					string str = "," + strSelectedID+ ",";
					if (str.IndexOf("," + tvn.ID + ",") != -1)
					{
						tvn.Checked = true;
					}
				}

				tvn.Attributes.Add("isChapter", "true");
				tvn.ImageUrl = "~/App_Themes/" + StyleSheetTheme + "/Images/TreeView/Chapter.gif";

				if (bookChapter.ParentId == 0)
				{
					tvBook.FindNodeById(bookChapter.BookId.ToString()).Nodes.Add(tvn);
				}
				else
				{
					tvBook.FindNodeById(bookChapter.ParentId.ToString()).Nodes.Add(tvn);
				}
			}

			if (tvBook.Nodes.Count > 0)
			{
				tvBook.Nodes[0].Expanded = true;
			}
		}
	}
}
