using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;

namespace RailExamWebApp.AssistBook
{
    public partial class AssistBookCategoryDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (fvAssistBookCategory.CurrentMode == FormViewMode.Insert)
            {
                if (hfInsert.Value == "-1")
                {
                    ((HiddenField)fvAssistBookCategory.FindControl("hfParentId")).Value = Request.QueryString["id"];
                }
                else
                {
                    ((HiddenField)fvAssistBookCategory.FindControl("hfParentId")).Value = hfInsert.Value;
                }
            }

            string strDeleteID = Request.Form.Get("DeleteID");
            if (strDeleteID != "" && strDeleteID != null)
            {
                AssistBookCategoryBLL objBll = new AssistBookCategoryBLL();
                int code = 0;
                objBll.DeleteAssistBookCategory(Convert.ToInt32(strDeleteID), ref code);

                if (code != 0)//code=2292
                {
                    SessionSet.PageMessage = "该辅导教材体系已被引用，不能删除！";
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(),
                    "jsSelectFirstNode",
                    @" if(window.parent.tvAssistBookCategory.get_nodes().get_length() == 1)
                          {
                                if(window.parent.tvAssistBookCategory.get_nodes().getNode(0).get_nodes().get_length() ==0 )
                                {
                                     window.parent.document.getElementById('fvAssistBookCategory_EditButton').style.display = 'none';
                                     window.parent.document.getElementById('fvAssistBookCategory_DeleteButton').style.display = 'none';
                                    var theFrame = window.parent.frames['ifAssistBookCategoryDetail'];
                                    theFrame.location = 'AssistBookCategoryDetail.aspx?id=0';
                                }
                                else
                                {
                                    window.parent.tvAssistBookCategory.get_nodes().getNode(0).select();
                                } 
                           } 
                            else
                            {
                                window.parent.tvAssistBookCategory.get_nodes().getNode(0).select();
                            }
                            window.parent.tvAssistBookCategoryChangeCallBack.callback(-1, 'Rebuild');
                            ",
                    true);
                }
            }
        }

        protected void fvAssistBookCategory_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvAssistBookCategoryChangeCallBack.callback(-1, 'Rebuild');
                if(window.parent.tvAssistBookCategory.get_nodes().get_length() > 0)
                {
                    window.parent.tvAssistBookCategory.get_nodes().getNode(0).select();
                }",
                true);
        }

        protected void fvAssistBookCategory_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvAssistBookCategoryChangeCallBack.callback(" + e.Keys["AssistBookCategoryId"] + @", 'Rebuild');
            if(window.parent.tvAssistBookCategory.get_nodes().get_length() > 0)
            {
                window.parent.tvAssistBookCategory.get_nodes().getNode(0).select();
            }",
                true);
        }
    }
}
