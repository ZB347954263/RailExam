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
using RailExam.BLL;

namespace RailExamWebApp.Online.Exam
{
	public partial class SelectBookRange : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string strItemTypeID = Request.QueryString.Get("itemTypeID");
			if(strItemTypeID == null)
			{
				strItemTypeID = "1";
				hfItemType.Value = "1";
			}
			else
			{
				hfItemType.Value = strItemTypeID;
			}
			ItemBLL objItemBll = new ItemBLL();

			BookBLL bookBLL = new BookBLL();
			IList<RailExam.Model.Book> bookList = null;

			int postID = Convert.ToInt32(Request.QueryString.Get("PostID"));
			int orgID = Convert.ToInt32(Request.QueryString.Get("OrgID"));
			int leader = Convert.ToInt32(Request.QueryString.Get("Leader"));
			int techID = Convert.ToInt32(Request.QueryString.Get("Tech"));
			bookList = bookBLL.GetEmployeeStudyBookInfoByKnowledgeID(-1, orgID, postID, leader, techID, 0);

			if (bookList.Count > 0)
			{
				TreeViewNode tvn = null;

				foreach (RailExam.Model.Book book in bookList)
				{
					tvn = new TreeViewNode();
					tvn.ID = book.bookId.ToString();
					tvn.Value = book.bookId.ToString();

					int n = objItemBll.GetItemsByBookID(book.bookId, Convert.ToInt32(strItemTypeID));
					if (n > 0)
					{
						tvn.Text = book.bookName + "£®" + n + "Ã‚£©";
					}
					else
					{
						tvn.Text = book.bookName;
					}

					tvn.ToolTip = book.bookName;
					tvn.Attributes.Add("isBook", "true");
					tvn.ImageUrl = "/RailExamBao/App_Themes/" + StyleSheetTheme + "/Images/TreeView/Book.gif";

					tvn.ShowCheckBox = true;

					tvn.ContentCallbackUrl = "/RailExamBao/Common/GetBookChapter.aspx?itemTypeID=" + strItemTypeID + "&flag=2&id=" +
					                         book.bookId;

					tvBookChapter.Nodes.Add(tvn);
				}
			}
		}
	}
}
