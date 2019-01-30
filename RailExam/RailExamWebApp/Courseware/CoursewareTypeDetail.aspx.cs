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
using RailExam.BLL;
using RailExam.Model;
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;

namespace RailExamWebApp.Courseware
{
    public partial class CoursewareTypeDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(fvCoursewareType.CurrentMode == FormViewMode.Insert)
            {
                if (hfInsert.Value == "-1")
                {
                    ((HiddenField)fvCoursewareType.FindControl("hfParentId")).Value = Request.QueryString["id"];
                }
                else
                {
                    ((HiddenField)fvCoursewareType.FindControl("hfParentId")).Value = hfInsert.Value;
                }
            }
            string strDeleteID = Request.Form.Get("DeleteID");
            if (strDeleteID != "" && strDeleteID != null)
            {
                CoursewareTypeBLL objBll = new CoursewareTypeBLL();
                int code = 0;
                objBll.DeleteCoursewareType(Convert.ToInt32(strDeleteID), ref code);

                if (code != 0)//code=2292
                {
                    SessionSet.PageMessage = "该课件体系已被引用，不能删除！";
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(),
                        "jsSelectFirstNode",
                        @"window.parent.tvCoursewareTypeChangeCallBack.callback(-1, 'Rebuild');
                            if(window.parent.tvCoursewareType.get_nodes().get_length() > 0)
                            {
                                window.parent.tvCoursewareType.get_nodes().getNode(0).select();
                            }",
                        true);
                }
            }
        }

        protected void fvCoursewareType_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvCoursewareTypeChangeCallBack.callback(" + e.Keys["CoursewareTypeId"] + @", 'Rebuild');
            if(window.parent.tvCoursewareType.get_nodes().get_length() > 0)
            {
                window.parent.tvCoursewareType.get_nodes().getNode(0).select();
            }",
                true);
        }

        protected void fvCoursewareType_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvCoursewareTypeChangeCallBack.callback(-1, 'Rebuild');
            if(window.parent.tvCoursewareType.get_nodes().get_length() > 0)
            {
                window.parent.tvCoursewareType.get_nodes().getNode(0).select();
            }",
                true);
        }
    }
}

