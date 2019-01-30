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
    public partial class DialogPage : System.Web.UI.Page
    {
        public string _pageUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
			int s = Request.RawUrl.IndexOf("pageName=");
			_pageUrl = Request.RawUrl.Substring(s + 9);
        }
    }
}
