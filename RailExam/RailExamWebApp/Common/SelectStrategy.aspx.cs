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
using ComponentArt.Web.UI;

namespace RailExamWebApp.Common
{
    public partial class SelectStrategy : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {  
        }

        protected void cb1_Callback(object sender, CallBackEventArgs e)
        {
            Grid1.DataBind();
            Grid1.RenderControl(e.Output);
        }
    }
}
