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
using RailExam.BLL;
using RailExam.Model;
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Train
{
    public partial class TrainPlanEmployee : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["PlanID"] = Request.QueryString.Get("PlanID");
                ViewState["PlanName"] = Request.QueryString.Get("PlanName");
                BindOrgTree();
            }
        }

        private void BindOrgTree()
        {
            OrganizationBLL organizationBLL = new OrganizationBLL();
            IList<RailExam.Model.Organization> organizationList = organizationBLL.GetOrganizations(0, 0, "", 0, 0,
                                                                                    "", "", "", "", "", "",
                                                                                    "", "", "", "", 0, 100,
                                                                                    "LevelNum,OrderIndex ASC");

            Pub.BuildComponentArtTreeView(tvOrg, (IList)organizationList, "OrganizationId",
                "ParentId", "ShortName", "FullName", "OrganizationId", null, null, null);
        }

        protected void btnUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("TrainPlanCourse.aspx?PlanID=" + ViewState["PlanID"].ToString() + "&PlanName=" + ViewState["PlanName"].ToString());
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.opener.form1.Refresh.value='true' ;window.opener.form1.submit();window.close();</script>");
        }
    }
}