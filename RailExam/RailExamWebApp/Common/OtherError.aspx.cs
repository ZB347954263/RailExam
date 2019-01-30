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
	public partial class OtherError : System.Web.UI.Page
	{
		protected string _errorMessage = "请先使用同步器上传成绩答卷后，再使用本系统。";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString["error"] != null && Request.QueryString["error"] != string.Empty)
			{
				_errorMessage = Request.QueryString["error"];

				if (!_errorMessage.EndsWith("。"))
				{
					_errorMessage += "。";
				}
			}
		}
	}
}
