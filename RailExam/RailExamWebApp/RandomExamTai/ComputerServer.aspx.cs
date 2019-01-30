using System;
using System.Collections.Generic;
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
using ComponentArt.Web.UI;

namespace RailExamWebApp.RandomExamTai
{
    public partial class ComputerServer : PageBase
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

                if (PrjPub.HasEditRight("站段服务器信息") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }

                if (PrjPub.HasDeleteRight("站段服务器信息") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }

                BindTree();
            }
        }

        //站段树绑定
        private void BindTree()
        {
            int railSystemID = PrjPub.GetRailSystemId();

            OrganizationBLL organizationsBLL = new OrganizationBLL();

            if (PrjPub.CurrentLoginUser.SuitRange == 1)
            {
                string strOwnIDs = string.Empty;
                if(railSystemID != 0)
                {
                    IList<RailExam.Model.Organization> organizationList1 = organizationsBLL.GetOrganizations(PrjPub.CurrentLoginUser.StationOrgID);
                    foreach (RailExam.Model.Organization organization in organizationList1)
                    {
                        strOwnIDs += strOwnIDs == string.Empty
                                         ? organization.OrganizationId.ToString()
                                         : "," + organization.OrganizationId;
                    }
                }

                IList<RailExam.Model.Organization> organizations = organizationsBLL.GetOrganizationsByLevel(2);

                foreach (RailExam.Model.Organization organization in organizations)
                {
                    if (organization.LevelNum == 1)
                    {
                        continue;
                    }

                    if ((organization.IdPath + "/").IndexOf("/1/") >= 0 && organization.LevelNum != 1 && railSystemID != 0 && railSystemID != organization.RailSystemID)
                    {
                        continue;
                    }

                    if (organization.LevelNum != 1 && strOwnIDs != string.Empty && (organization.IdPath + "/").IndexOf("/1/") < 0 && ("," + strOwnIDs + ",").IndexOf("," + organization.OrganizationId + ",") < 0)
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
