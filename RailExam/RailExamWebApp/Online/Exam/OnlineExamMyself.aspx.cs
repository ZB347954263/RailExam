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
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Online.Exam
{
    public partial class OnlineExamMyself : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			if(!IsPostBack)
			{
                //if (!PrjPub.IsShowOnline())
                //{
                //    Response.Redirect("/RailExamBao/Common/Error.aspx?error=您当前时间无权使用此功能");
                //}

				if(!PrjPub.IsServerCenter)
				{
					hfOrgID.Value = ConfigurationManager.AppSettings["StationID"];
					if(!string.IsNullOrEmpty(hfOrgID.Value))
					{
						OrganizationBLL objbll = new OrganizationBLL();
						hfOrgName.Value = objbll.GetOrganization(Convert.ToInt32(hfOrgID.Value)).ShortName;
					}
				}
			}
            txtPost.Text = hfPostName.Value;
            txtOrg.Text = hfOrgName.Value;
        }

        protected void btnStart_Click(object sender, ImageClickEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),"ShowExam","ShowExam()",true);
        }
    }
}
