using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;
using RailExam.BLL;

namespace RailExamWebApp.Item
{
	public partial class ItemBookChapterDetail : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			hfChapterID.Value = Request.QueryString["id"];

			if (fvBookChapter.CurrentMode == FormViewMode.Insert)
			{
				if (hfInsert.Value == "-1")
				{
					((HiddenField)fvBookChapter.FindControl("hfParentID")).Value = Request.QueryString["ParentID"];
					((HiddenField)fvBookChapter.FindControl("hfBookID")).Value = Request.QueryString["BookID"];
				}
				else
				{
					((HiddenField)fvBookChapter.FindControl("hfParentId")).Value = hfInsert.Value;
					((HiddenField)fvBookChapter.FindControl("hfBookID")).Value = Request.QueryString["BookID"];
				}
				if (Request.QueryString["Mode"] == "Edit")
				{
					((HiddenField)fvBookChapter.FindControl("hfIsEdit")).Value = "true";
				}
				else
				{
					((HiddenField)fvBookChapter.FindControl("hfIsEdit")).Value = "false";
				}
			}
			else if (fvBookChapter.CurrentMode == FormViewMode.Edit)
			{
				if (Request.QueryString["Mode"] == "Edit")
				{
					((HiddenField)fvBookChapter.FindControl("hfIsEdit")).Value = "true";
				}
				else
				{
					((HiddenField)fvBookChapter.FindControl("hfIsEdit")).Value = "false";
				}
			}

			if(!IsPostBack)
			{
				BindGrid();
			}

			string strRefreshGrid = Request.Form.Get("RefreshGrid");
			if (strRefreshGrid != null & strRefreshGrid != "")
			{
				fvBookChapter.DataBind();
				BindGrid();
			}

			string strRefresh = Request.Form.Get("Refresh");
			if (strRefresh != null & strRefresh != "")
			{
				ItemBLL objItemBll = new ItemBLL();
				objItemBll.DeleteItem(Convert.ToInt32(strRefresh));
				BindGrid();
			}
		}

		private void BindGrid()
		{
			if(!string.IsNullOrEmpty(hfChapterID.Value) && hfChapterID.Value != "0")
			{
				int bookID = Convert.ToInt32(Request.QueryString["BookID"]);
				int chapterID = Convert.ToInt32(hfChapterID.Value);

				ItemBLL objItemBll = new ItemBLL();
				IList<RailExam.Model.Item> objItemList = objItemBll.GetItemsByBookChapterId(bookID, chapterID, 0, int.MaxValue);

				itemsGrid.DataSource = objItemList;
				itemsGrid.DataBind();
			}
			else
			{
				int bookID = Convert.ToInt32(Request.QueryString["BookID"]);

				ItemBLL objItemBll = new ItemBLL();
				IList<RailExam.Model.Item> objItemList = objItemBll.GetItemsByBookBookId(bookID);

				itemsGrid.DataSource = objItemList;
				itemsGrid.DataBind();
			}
		}

		private void DeleteData(int nBookChapterUpdateID)
		{
			BookUpdateBLL BookChapterUpdateBLL = new BookUpdateBLL();
			ItemBLL objItemBll = new ItemBLL();
			objItemBll.DeleteItemByChapterID(nBookChapterUpdateID,"");
			BookChapterUpdateBLL.DeleteBookUpdate(nBookChapterUpdateID);
			BindGrid();
		}

		protected void fvBookChapter_ItemInserted(object sender, FormViewInsertedEventArgs e)
		{
			ClientScript.RegisterStartupScript(GetType(),
				"jsSelectFirstNode",
				@"window.parent.tvBookChapterChangeCallBack.callback(-1, 'Insert');                        
            if(window.parent.tvBookChapter.get_nodes().get_length() > 0)
            {
                window.parent.tvBookChapter.get_nodes().getNode(0).select();
            }",
				true);
		}

		protected void fvBookChapter_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
		{
			ClientScript.RegisterStartupScript(GetType(),
			 "jsSelectFirstNode",
			 @"window.parent.tvBookChapterChangeCallBack.callback(" + e.Keys["ChapterId"] + @", 'Rebuild');                        
                if(window.parent.tvBookChapter.get_nodes().get_length() > 0)
                {
                    window.parent.tvBookChapter.get_nodes().getNode(0).select();
                }",
			  true);
		}

		protected void fvBookChapter_ItemDeleted(object sender, FormViewDeletedEventArgs e)
		{
			ClientScript.RegisterStartupScript(GetType(),
				"jsSelectFirstNode",
				@"window.parent.tvBookChapterChangeCallBack.callback(-1, 'Rebuild');                        
            if(window.parent.tvBookChapter.get_nodes().get_length() > 0)
            {
                window.parent.tvBookChapter.get_nodes().getNode(0).select();
            }",
				true);
		}

		protected void fvBookChapter_ItemDeleting(object sender, FormViewDeleteEventArgs e)
		{
			DeleteData(Convert.ToInt32(e.Keys[0].ToString()));
		}
	}
}
