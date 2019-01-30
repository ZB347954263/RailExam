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
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;
using RailExam.BLL;
using RailExam.Model;

namespace RailExamWebApp.Item
{
    public partial class ItemConfig : PageBase
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
                if (PrjPub.HasEditRight("参数配置") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }

				hfEmployeeID.Value = PrjPub.CurrentLoginUser.EmployeeID.ToString();

                dvItemConfig_OnModeChanged(null, null);
            }
        }

        protected void dvItemConfig_OnModeChanged(object s, EventArgs e)
        {
            if (dvItemConfig.CurrentMode == DetailsViewMode.ReadOnly)
            {
                editButton.Visible = true;
                updateButton.Visible = false;
                updateCancelButton.Visible = false;
            }
            else if (dvItemConfig.CurrentMode == DetailsViewMode.Edit)
            {
                editButton.Visible = false;
                updateButton.Visible = true;
                updateCancelButton.Visible = true;
            }
        }
    }
}