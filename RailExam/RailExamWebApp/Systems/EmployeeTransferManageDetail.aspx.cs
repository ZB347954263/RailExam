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
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
	public partial class EmployeeTransferManageDetail : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			hfLoginOrgID.Value = PrjPub.CurrentLoginUser.StationOrgID.ToString();

		}
	}
}
