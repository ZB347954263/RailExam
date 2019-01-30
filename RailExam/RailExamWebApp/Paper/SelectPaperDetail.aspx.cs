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

namespace RailExamWebApp.Paper
{
    public partial class SelectPaperDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Grid1.DataBind();
        }
    }
}