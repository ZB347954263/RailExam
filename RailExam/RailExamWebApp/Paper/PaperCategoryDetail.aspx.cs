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
using RailExam.BLL;
using RailExam.Model;
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;

namespace RailExamWebApp.Paper
{
    public partial class PaperCategoryDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (fvPaperCategory.CurrentMode == FormViewMode.Insert)
            {
                if (hfInsert.Value == "-1")
                {
                    ((HiddenField)fvPaperCategory.FindControl("hfParentId")).Value = Request.QueryString["id"];
                }
                else
                {
                    ((HiddenField)fvPaperCategory.FindControl("hfParentId")).Value = hfInsert.Value;
                }
            }
        }

        protected void fvPaperCategory_ItemDeleted(object sender, FormViewDeletedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvPaperCategoryChangeCallBack.callback(-1, 'Rebuild');                        
            if(window.parent.tvPaperCategory.get_nodes().get_length() > 0)
            {
                window.parent.tvPaperCategory.get_nodes().getNode(0).select();
            }",
                true);
        }

        protected void fvPaperCategory_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvPaperCategoryChangeCallBack.callback(-1, 'Rebuild');                        
            if(window.parent.tvPaperCategory.get_nodes().get_length() > 0)
            {
                window.parent.tvPaperCategory.get_nodes().getNode(0).select();
            }",
                true);
        }

        protected void fvPaperCategory_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvPaperCategoryChangeCallBack.callback(" + e.Keys["PaperCategoryId"] + @", 'Rebuild');                        
            if(window.parent.tvPaperCategory.get_nodes().get_length() > 0)
            {
                window.parent.tvPaperCategory.get_nodes().getNode(0).select();
            }",
                true);
        }
    }
}
