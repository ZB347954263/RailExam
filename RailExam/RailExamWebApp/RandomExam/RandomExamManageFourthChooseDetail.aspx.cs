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

namespace RailExamWebApp.RandomExam
{
    public partial class RandomExamManageFourthChooseDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                hfRailSystemID.Value = PrjPub.GetRailSystemId().ToString();

                hfOrgID.Value = PrjPub.CurrentLoginUser.StationOrgID.ToString();
                HfExamCategoryId.Value = Request.QueryString.Get("id");
                Grid1.DataBind();
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Grid1.DataBind();
        }
    }
}
