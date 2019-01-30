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
using RailExam.BLL;

namespace RailExamWebApp
{
	public partial class SystemVersionInfo : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				SystemVersionBLL objBll = new SystemVersionBLL();
                lblVersion.Text = objBll.GetVersion().ToString("0.0");
                lblVersionCenter.Text = objBll.GetVersionToServer().ToString("0.0");
			}
		}
	}
}
