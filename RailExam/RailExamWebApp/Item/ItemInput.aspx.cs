using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.OleDb;
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
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Item
{
	public partial class ItemInput : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack && !Grid1.IsCallback)
			{
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session���������µ�¼��ϵͳ��");
                    return;
                }
				//if(HfChapterId.Value != "")
				//{
				//    BookChapterBLL objBll = new BookChapterBLL();
				//    BookChapter obj = objBll.GetBookChapter(Convert.ToInt32(HfChapterId.Value));
				//    txtChapterName.Text = obj.NamePath;
				//}

				HfChapterId.Value = Request.QueryString.Get("cid");
				HfBookId.Value = Request.QueryString.Get("bid");

				if (!string.IsNullOrEmpty(HfBookId.Value) && HfBookId.Value != "-1")
				{
					BookBLL objbookbill = new BookBLL();
					RailExam.Model.Book objBook = objbookbill.GetBook(Convert.ToInt32(HfBookId.Value));

					if (PrjPub.CurrentLoginUser.SuitRange == 0 && PrjPub.IsServerCenter)
					{
						if (PrjPub.CurrentLoginUser.StationOrgID != objBook.publishOrg)
						{
							Response.Write("<script>alert('��û�е��뵱ǰ�̲��½������Ȩ�ޣ�');window.close();</script>");
							return;
						}
					}

					if (HfChapterId.Value != "-1")
					{
						BookChapterBLL objBll = new BookChapterBLL();
						BookChapter obj = objBll.GetBookChapter(Convert.ToInt32(HfChapterId.Value));
						txtChapterName.Text = objBook.bookName + ">>" + obj.NamePath;

						if (obj.IsCannotSeeAnswer)
						{
							ddlMode.Visible = true;
						}
						else
						{
							ddlMode.Visible = false;
							ddlMode.SelectedValue = "0";
						}
					}
					else
					{
						txtChapterName.Text = objBook.bookName;
						ddlMode.Visible = false;
						ddlMode.SelectedValue = "0";
					}
					lblTitle.Visible = true;
					btnDelAll.Visible = true;
				}
				else
				{
					lblTitle.Visible = false;
					btnDelAll.Visible = false;
				}

				if(!PrjPub.IsWuhanOnly())
				{
					ddlMode.Visible = false;
					ddlMode.SelectedValue = "0";
				}

				ddlMode.Visible = false;
				ddlMode.SelectedValue = "0";

				if (Session["table"] != null)
				{
					Session.Remove("table");
				}
			}

			string str = Request.Form.Get("Refresh");
			if(str != null && str == "refresh")
			{
				if (Session["table"] != null)
				{
					Grid1.DataSource = (DataTable)Session["table"];
					Grid1.DataBind();
				}
			}
		}


		protected void btnDelAll_Click(object sender, EventArgs e)
		{
			try
			{
				ItemBLL objbll = new ItemBLL();
				if (HfChapterId.Value != "-1")
				{
					objbll.DeleteItemByChapterID(Convert.ToInt32(HfChapterId.Value), txtChapterName.Text);
				}
				else
				{
					objbll.DeleteItemByBookID(Convert.ToInt32(HfBookId.Value), txtChapterName.Text);
				}
				SessionSet.PageMessage = "ɾ���ɹ���";
			}
			catch (Exception)
			{
				SessionSet.PageMessage = "ɾ��ʧ�ܣ�";
				return;
			}
		}
	}
}
