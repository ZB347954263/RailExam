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
using RailExamWebApp.Common.Class;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using System.Collections.Generic;
using RailExam.BLL;

namespace RailExamWebApp.RandomExamTai
{
	public partial class ComputerRoomManage: System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }
				BindOrgTree("");
			}
		}

		private void BindOrgTree(string selectId)
		{
            OrganizationBLL organizationsBLL = new OrganizationBLL();

            if (PrjPub.CurrentLoginUser.SuitRange == 1)
            {
                IList<RailExam.Model.Organization> organizations = organizationsBLL.GetOrganizationsByLevel(2);

                foreach (RailExam.Model.Organization organization in organizations)
                {
                    if (organization.LevelNum == 1)
                    {
                        continue;
                    }
                    TreeViewNode tvNode = new TreeViewNode();
                    tvNode.ID = organization.OrganizationId.ToString();
                    tvNode.Text = organization.ShortName;
                    tvView.Nodes.Add(tvNode);
                }
            }
            else
            {
                TreeViewNode tvNode = new TreeViewNode();
                tvNode.ID = PrjPub.CurrentLoginUser.StationOrgID.ToString();
                tvNode.Text = organizationsBLL.GetOrganization(PrjPub.CurrentLoginUser.StationOrgID).ShortName;
                tvView.Nodes.Add(tvNode);
            }
		}

	}
}
