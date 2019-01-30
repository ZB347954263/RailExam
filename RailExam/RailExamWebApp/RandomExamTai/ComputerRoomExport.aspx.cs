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

namespace RailExamWebApp.RandomExamTai
{
    public partial class ComputerRoomExport : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                OrganizationBLL orgBll = new OrganizationBLL();
                hfOrg.Value = Request.QueryString.Get("OrgID");
                txtOrg.Text = orgBll.GetOrganization(Convert.ToInt32(hfOrg.Value)).ShortName;
            }

            string str = Request.Form.Get("Refresh");
            if (str != null && str == "refresh")
            {
                if (Session["table"] != null)
                {
                    Grid1.DataSource = (DataTable)Session["table"];
                    Grid1.DataBind();
                }
            }
        }
    }
}
