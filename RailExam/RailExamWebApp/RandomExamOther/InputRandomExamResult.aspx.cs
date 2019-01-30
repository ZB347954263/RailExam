using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamOther
{
	public partial class InputRandomExamResult : PageBase
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
				if (PrjPub.HasEditRight("登记成绩"))
				{
					HfUpdateRight.Value = "True";
				}
				else
				{
					HfUpdateRight.Value = "False";
				}

				if (PrjPub.IsServerCenter)
				{
					hfOrgID.Value = PrjPub.CurrentLoginUser.StationOrgID.ToString();
					if (PrjPub.CurrentLoginUser.IsAdmin)
					{
						hfIsAdmin.Value = "True";
					}
					else
					{
						hfIsAdmin.Value = "False";
					}
				}
				else
				{
					hfIsAdmin.Value = "False";
				}
			}

			string strRefresh = Request.Form.Get("Refresh");
			if (strRefresh != null && strRefresh != "")
			{
				examsGrid.DataBind();
			}
		}

		protected void searchExamCallBack_Callback(object sender, CallBackEventArgs e)
		{
			examsGrid.DataBind();
			examsGrid.RenderControl(e.Output);
		}
	}
}
