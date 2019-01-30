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

namespace RailExamWebApp.Common
{
	public partial class Error : System.Web.UI.Page
	{
		protected string _errorMessage = "ϵͳ����Ԥ֪�Ĵ���";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["error"] != null && Request.QueryString["error"] != string.Empty)
			{
				_errorMessage = Request.QueryString["error"];

				if (!_errorMessage.EndsWith("��"))
				{
					_errorMessage += "��";
				}
			}
		}
	}
}
