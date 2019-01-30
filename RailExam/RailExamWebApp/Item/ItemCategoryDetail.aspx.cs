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
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;

namespace RailExamWebApp.Item
{
    public partial class ItemCategoryDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (fvItemCategoryDetail.CurrentMode == FormViewMode.Insert)
            {
                if (hfInsert.Value == "-1")
                {
                    ((HiddenField)fvItemCategoryDetail.FindControl("hfParentId")).Value = Request.QueryString["id"];
                }
                else
                {
                    ((HiddenField)fvItemCategoryDetail.FindControl("hfParentId")).Value = hfInsert.Value;
                }
            }

            string strDeleteID = Request.Form.Get("DeleteID");
            if (strDeleteID != "" && strDeleteID != null)
            {
                ItemCategoryBLL objBll = new ItemCategoryBLL();
                int code = 0;
                objBll.DeleteItemCategory(Convert.ToInt32(strDeleteID), ref code);

                if (code != 0)//code=2292
                {
                    SessionSet.PageMessage = "该辅助类别已被引用，不能删除！";
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(),
                        "jsSelectFirstNode",
                        @"window.parent.tvItemCategoryChangeCallBack.callback(-1, 'Rebuild');                        
                            if(window.parent.tvItemCategory.get_nodes().get_length() > 0)
                            {
                                window.parent.tvItemCategory.get_nodes().getNode(0).select();
                            }",
                        true);
                }
            }
        }
        protected void fvItemCategoryDetail_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvItemCategoryChangeCallBack.callback(-1, 'Rebuild');                      
                if(window.parent.tvItemCategory.get_nodes().get_length() > 0)
                {
                    window.parent.tvItemCategory.get_nodes().getNode(0).select();
                }",
                true);
        }

        protected void fvItemCategoryDetail_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvItemCategoryChangeCallBack.callback(" + e.Keys["ItemCategoryId"] + @", 'Rebuild');",
                true);
        }
    }
}