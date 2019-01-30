using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Exam
{
    public partial class SelectExamResult :  PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                hfIsServer.Value = PrjPub.IsServerCenter.ToString();
                BindOrgTree();
            }
        }

        private void BindOrgTree()
        {
            OrganizationBLL organizationBLL = new OrganizationBLL();

            int railSystemID = PrjPub.GetRailSystemId();

            if ((PrjPub.CurrentLoginUser.SuitRange == 1 || railSystemID != 0) && PrjPub.IsServerCenter)
            {
                IList<RailExam.Model.Organization> organizationList = organizationBLL.GetOrganizationsByLevel(2);

                string strOwnIDs = string.Empty;
                if (railSystemID != 0)
                {
                    IList<RailExam.Model.Organization> organizationList1 = organizationBLL.GetOrganizations(PrjPub.CurrentLoginUser.StationOrgID);
                    foreach (RailExam.Model.Organization organization in organizationList1)
                    {
                        strOwnIDs += strOwnIDs == string.Empty
                                         ? organization.OrganizationId.ToString()
                                         : "," + organization.OrganizationId;
                    }
                }

                if (organizationList.Count > 0)
                {
                    TreeViewNode tvn = null;

                    foreach (Organization organization in organizationList)
                    {
                        if ((organization.IdPath + "/").IndexOf("/1/") >= 0 && organization.LevelNum != 1 && railSystemID != 0 && railSystemID != organization.RailSystemID)
                        {
                            continue;
                        }

                        if (organization.LevelNum != 1 && strOwnIDs != string.Empty && (organization.IdPath + "/").IndexOf("/1/") < 0 && ("," + strOwnIDs + ",").IndexOf("," + organization.OrganizationId + ",") < 0)
                        {
                            continue;
                        }

                        tvn = new TreeViewNode();
                        tvn.ID = organization.OrganizationId.ToString();
                        tvn.Value = organization.OrganizationId.ToString();
                        tvn.Text = organization.ShortName;
                        tvn.ToolTip = organization.FullName;

                        if (organization.ParentId == 0)
                        {
                            tvView.Nodes.Add(tvn);
                        }
                        else
                        {
                            try
                            {
                                tvView.FindNodeById(organization.ParentId.ToString()).Nodes.Add(tvn);
                            }
                            catch
                            {
                                tvView.Nodes.Clear();
                                SessionSet.PageMessage = "Êý¾Ý´íÎó£¡";
                                return;
                            }
                        }
                    }
                }
                if (tvView.Nodes.Count > 0)
                {
                    tvView.Nodes[0].Expanded = true;
                }
            }
            else
            {
				string strOrgID;
				if (PrjPub.IsServerCenter)
				{
					strOrgID = PrjPub.CurrentLoginUser.StationOrgID.ToString();
				}
				else
				{
					strOrgID = ConfigurationManager.AppSettings["StationID"].ToString();
				}
				int stationID = organizationBLL.GetStationOrgID(Convert.ToInt32(strOrgID));
                RailExam.Model.Organization organization=organizationBLL.GetOrganization(stationID);
                 TreeViewNode tvn = null;

                 tvn = new TreeViewNode();
                 tvn.ID = organization.OrganizationId.ToString();
                 tvn.Value = organization.IdPath.ToString();
                 tvn.Text = organization.ShortName;
                 tvn.ToolTip = organization.FullName;
                tvView.Nodes.Add(tvn);
             }

            tvView.DataBind();
        }
    }
}
