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
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
	public partial class EmployeeWithRailEdu : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{

				PostBLL PostBLL = new PostBLL();
				IList<RailExam.Model.Post> posts = PostBLL.GetPosts("POST_NEW");

				Pub.BuildComponentArtTreeView(tvPost, (IList)posts, "PostId", "ParentId", "PostName",
											  "PostName", "PostId", null, null, null);

				OrganizationBLL organizationBLL = new OrganizationBLL();
				IList<RailExam.Model.Organization> organizationList =
					organizationBLL.GetOrganizations("ORG_NEW");

				Pub.BuildComponentArtTreeView(tvOrganization, (IList)organizationList, "OrganizationId",
									"ParentId", "ShortName", "FullName", "OrganizationId", null, null, null);
				if (tvOrganization.Nodes.Count > 0)
				{
					tvOrganization.Nodes[0].Expanded = true;
				}
			}
		}

		protected void btnOK_Click(object sender, EventArgs e)
		{
			EmployeeRailEduBLL objBll = new EmployeeRailEduBLL();

		}

		protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
