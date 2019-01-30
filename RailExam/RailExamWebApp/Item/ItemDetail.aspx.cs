using System;
using System.Web.UI.WebControls;
using DSunSoft.Common;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;
using RailExamWebApp.Common.Control.Date;

namespace RailExamWebApp.Item
{
    public partial class ItemDetail : PageBase
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

                string strMode = Request.QueryString.Get("mode");
                string strBookId = Request.QueryString.Get("bid");
                string strChapterId = Request.QueryString.Get("cid");

                switch (strMode)
                {
                    case "readonly":
                        {
                            if (fvItem.CurrentMode != FormViewMode.ReadOnly)
                            {
                                fvItem.ChangeMode(FormViewMode.ReadOnly);
                            }
                            break;
                        }
                    case "insert":
                        {
							if (PrjPub.CurrentLoginUser.SuitRange == 0 && PrjPub.IsServerCenter)
							{
								BookBLL bookBLL = new BookBLL();
								RailExam.Model.Book objBook = bookBLL.GetBook(Convert.ToInt32(strBookId));
								if (PrjPub.CurrentLoginUser.StationOrgID != objBook.publishOrg)
								{
									Response.Write("<script>alert('您没有新增当前教材章节试题的权限！');window.close();</script>");
									return;
								}
							}

                        	if (fvItem.CurrentMode != FormViewMode.Insert)
                            {
                                fvItem.ChangeMode(FormViewMode.Insert);
                            }

                            if (!string.IsNullOrEmpty(strBookId) && !string.IsNullOrEmpty(strChapterId))
							{
								int employeeID = PrjPub.CurrentLoginUser.EmployeeID;
                                ItemConfigBLL itemConfigBLL = new ItemConfigBLL();
                                RailExam.Model.ItemConfig itemConfig = itemConfigBLL.GetItemConfig(employeeID);

                                ((DropDownList) fvItem.FindControl("ddlItemTypeInsert")).SelectedValue =
                                    itemConfig.DefaultTypeId.ToString();
                                ((DropDownList) fvItem.FindControl("ddlItemDifficultyInsert")).SelectedValue =
                                    itemConfig.DefaultDifficultyId.ToString();
                                ((TextBox) fvItem.FindControl("txtDefaultScoreInsert")).Text =
                                    itemConfig.DefaultScore.ToString(".00");
                                ((TextBox) fvItem.FindControl("txtCompleteTimeInsert")).Text =
                                    CommonTool.ConvertSecondsToTimeString(itemConfig.DefaultCompleteTime);
                                ((TextBox) fvItem.FindControl("txtCreatePersonInsert")).Text =
                                    PrjPub.CurrentLoginUser.EmployeeName;
                                ((DropDownList) fvItem.FindControl("ddlAnswerCountInsert")).SelectedValue =
                                    itemConfig.DefaultAnswerCount.ToString();
                                ((DropDownList) fvItem.FindControl("ddlStatusInsert")).SelectedValue =
                                    itemConfig.DefaultStatusId.ToString();
                                ((DropDownList) fvItem.FindControl("ddlHasPictureInsert")).SelectedValue =
                                    itemConfig.HasPicture.ToString();

                                ((HiddenField) fvItem.FindControl("hfBookId")).Value = strBookId;
                                ((HiddenField) fvItem.FindControl("hfChapterId")).Value = strChapterId;
                                ((HiddenField) fvItem.FindControl("hfCategoryId")).Value = "-1";

                                BookBLL objBll = new BookBLL();
							    string bookName = objBll.GetBook(Convert.ToInt32(strBookId)).bookName;
							    BookChapterBLL objChapterBll = new BookChapterBLL();
							    string chapterName = objChapterBll.GetBookChapter(Convert.ToInt32(strChapterId)).NamePath;
							    ((TextBox) fvItem.FindControl("txtBookChapterInsert")).Text = bookName + ">>" + chapterName;

                                if (PrjPub.CurrentLoginUser != null && PrjPub.CurrentLoginUser.StationOrgID > 0)
                                {
                                    ((HiddenField) fvItem.FindControl("hfOrganizationId")).Value =
                                        PrjPub.CurrentLoginUser.StationOrgID.ToString();
                                }

                                if (itemConfig.DefaultOutDateDate != DateTime.MinValue)
                                {
                                    ((Date) fvItem.FindControl("dateOutDateDateInsert")).DateValue =
                                        itemConfig.DefaultOutDateDate.ToString("yyyy-MM-dd");
                                }

                                ((Date) fvItem.FindControl("dateCreateTimeInsert")).DateValue =
                                    DateTime.Today.ToString("yyyy-MM-dd");
                            }
                            break;
                        }
                    case "edit":
                        {
                            //if(PrjPub.CurrentLoginUser.SuitRange == 0 && PrjPub.IsServerCenter)
                            //{
                            //    int itemID = Convert.ToInt32(Request.QueryString.Get("id"));
                            //    ItemBLL itemBLL = new ItemBLL();
                            //    RailExam.Model.Item objItem = itemBLL.GetItem(itemID);
                            //    BookBLL bookBLL = new BookBLL();
                            //    RailExam.Model.Book objBook = bookBLL.GetBook(objItem.BookId);
                            //    if(PrjPub.CurrentLoginUser.StationOrgID != objBook.publishOrg)
                            //    {
                            //        Response.Write("<script>alert('您没有修改该试题的权限！');window.close();</script>");
                            //        return;
                            //    }
                            //}

                            if (fvItem.CurrentMode != FormViewMode.Edit)
                            {
                                fvItem.ChangeMode(FormViewMode.Edit);
                            }

                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }

        protected void fvItem_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            Response.ClearContent();
            Response.Write("<script type='text/javascript'>window.opener.treeNodeSelectedCallBack.callback();"
                           + "self.close();</script>");
            Response.End();
        }

        protected void fvItem_ItemCommand(object s, FormViewCommandEventArgs e)
        {
            //验证页面
            //string msg = string.Empty;
            //foreach (IValidator iv in this.Validators)
            //{
            //    iv.Validate();
            //    if(iv.IsValid)
            //    {
            //        continue;
            //    }
            //    msg += iv.ErrorMessage;
            //}
            //if(msg != string.Empty)
            //{
            //    SessionSet.PageMessage = msg;

            //    return;
            //}

            if (e.CommandName.ToUpper() == "CONTINUE")
            {
                fvItem.InsertItem(false);

                Response.ClearContent();
                Response.Write("<script type='text/javascript'>window.opener.opener.treeNodeSelectedCallBack.callback();"
                               + "window.location.href='" + Request.Url.PathAndQuery + "';</script>");
                Response.End();
            }
            else if (e.CommandName.ToUpper() == "INSERT")
            {
                fvItem.InsertItem(false);

                Response.ClearContent();
                Response.Write("<script type='text/javascript'>window.opener.opener.treeNodeSelectedCallBack.callback();"
                               + "window.opener.close();self.close();</script>");
                Response.End();
            }
        }
    }
}