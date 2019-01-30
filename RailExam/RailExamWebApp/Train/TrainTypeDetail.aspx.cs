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

namespace RailExamWebApp.Train
{
    public partial class TrainTypeDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (fvTrainType.CurrentMode == FormViewMode.Insert)
            {
                if (hfInsert.Value == "-1")
                {
                    ((HiddenField)fvTrainType.FindControl("hfParentId")).Value = Request.QueryString["id"];
                }
                else
                {
                    ((HiddenField)fvTrainType.FindControl("hfParentId")).Value = hfInsert.Value;
                }
            }

            string strDeleteID = Request.Form.Get("DeleteID");
            if (strDeleteID != "" && strDeleteID != null)
            {
                TrainTypeBLL objBll = new TrainTypeBLL();
				string strParentID = objBll.GetTrainTypeInfo(Convert.ToInt32(strDeleteID)).ParentID.ToString();
				int code = 0;
                objBll.DeleteTrainType(Convert.ToInt32(strDeleteID), ref code);

                if (code != 0)//code=2292
                {
                    SessionSet.PageMessage = "该培训类别已被引用，不能删除！";
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(),
                        "jsSelectFirstNode",
						@"window.parent.tvTrainTypeChangeCallBack.callback('-1','Delete','" + strParentID + @"','Rebuild');                       
                            if(window.parent.tvTrainType.get_nodes().get_length() > 0)
                            {
                                window.parent.tvTrainType.get_nodes().getNode(0).select();
                            }",
                        true);
                }
            }
        }

        protected void fvTrainType_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvTrainTypeChangeCallBack.callback('-1','Insert', 'Rebuild');                   
            if(window.parent.tvTrainType.get_nodes().get_length() > 0)
            {
                window.parent.tvTrainType.get_nodes().getNode(0).select();
            }",
                true);
        }

        protected void fvTrainType_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvTrainTypeChangeCallBack.callback(" + e.Keys["TrainTypeID"] + @", 'Rebuild');                        
            if(window.parent.tvTrainType.get_nodes().get_length() > 0)
            {
                window.parent.tvTrainType.get_nodes().getNode(0).select();
            }",
                true);
        }
    }
}