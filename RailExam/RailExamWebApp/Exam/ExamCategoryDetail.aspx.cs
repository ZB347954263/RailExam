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
using DSunSoft.Web.UI;

namespace RailExamWebApp.Exam
{
    public partial class ExamCategoryDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (fvExamCategory.CurrentMode == FormViewMode.Insert)
            {
                if (hfInsert.Value == "-1")
                {
                    ((HiddenField)fvExamCategory.FindControl("hfParentId")).Value = Request.QueryString["id"];
                }
                else
                {
                    ((HiddenField)fvExamCategory.FindControl("hfParentId")).Value = hfInsert.Value;
                }
            }
        }

        protected void fvExamCategory_ItemDeleted(object sender, FormViewDeletedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvExamCategoryChangeCallBack.callback(-1, 'Rebuild');
            if(window.parent.tvExamCategory.get_nodes().get_length() > 0)
            {
                window.parent.tvExamCategory.get_nodes().getNode(0).select();
            }",
                true);
        }

        protected void fvExamCategory_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvExamCategoryChangeCallBack.callback(-1, 'Rebuild');
            if(window.parent.tvExamCategory.get_nodes().get_length() > 0)
            {
                window.parent.tvExamCategory.get_nodes().getNode(0).select();
            }",
                true);
        }

        protected void fvExamCategory_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvExamCategoryChangeCallBack.callback(" + e.Keys["ExamCategoryId"] + @", 'Rebuild');
            if(window.parent.tvExamCategory.get_nodes().get_length() > 0)
            {
                window.parent.tvExamCategory.get_nodes().getNode(0).select();
            }",
                true);
        }
    }
}
