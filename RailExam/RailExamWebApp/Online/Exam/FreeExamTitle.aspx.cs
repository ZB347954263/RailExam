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
using DSunSoft.Web.UI;

namespace RailExamWebApp.Online.Exam
{
	public partial class FreeExamTitle : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				try
				{
					HfExamTime.Value = (Convert.ToInt32(Request.QueryString.Get("Time")) * 60).ToString();
				}
				catch
				{
					Response.Redirect("/RailExamBao/Common/Error.aspx?error=≤‚ ‘ ±º‰…Ë÷√¥ÌŒÛ£°");
					return;
				}
			}
		}

		protected void CallBack1_Callback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
		{
			lblServerDateTime.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
			lblServerDateTime.RenderControl(e.Output);
		}
	}
}
