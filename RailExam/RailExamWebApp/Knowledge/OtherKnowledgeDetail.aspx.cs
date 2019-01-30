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

namespace RailExamWebApp.Knowledge
{
    public partial class OtherKnowledgeDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (fvOtherKnowledge.CurrentMode == FormViewMode.Insert)
            {
                if (hfInsert.Value == "-1")
                {
                    ((HiddenField)fvOtherKnowledge.FindControl("hfParentId")).Value = Request.QueryString["id"];
                }
                else
                {
                    ((HiddenField)fvOtherKnowledge.FindControl("hfParentId")).Value = hfInsert.Value;
                }
            }
        }

        protected void fvOtherKnowledge_ItemDeleted(object sender, FormViewDeletedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvOtherKnowledgeChangeCallBack.callback(-1, 'Rebuild');                        
            if(window.parent.tvOtherKnowledge.get_nodes().get_length() > 0)
            {
                window.parent.tvOtherKnowledge.get_nodes().getNode(0).select();
            }",
                true);
        }

        protected void fvOtherKnowledge_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvOtherKnowledgeChangeCallBack.callback(-1, 'Rebuild');                        
            if(window.parent.tvOtherKnowledge.get_nodes().get_length() > 0)
            {
                window.parent.tvOtherKnowledge.get_nodes().getNode(0).select();
            }",
                true);
        }

        protected void fvOtherKnowledge_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvOtherKnowledgeChangeCallBack.callback(" + e.Keys["OtherKnowledgeID"] + @", 'Rebuild');                        
            if(window.parent.tvOtherKnowledge.get_nodes().get_length() > 0)
            {
                window.parent.tvOtherKnowledge.get_nodes().getNode(0).select();
            }",
                true);
        }
    }
}