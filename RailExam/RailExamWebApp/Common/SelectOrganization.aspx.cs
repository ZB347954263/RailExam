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
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Common
{
    public partial class SelectOrganization : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (PrjPub.CurrentLoginUser == null)
                //{
                //    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                //    return;
                //}
                BindItemCategoryTree();
            }
        }

        private void BindItemCategoryTree()
        {
            OrganizationBLL organizationsBLL = new OrganizationBLL();

            int railSystemID = 0;
            string strOwnIDs = string.Empty;
            if (PrjPub.CurrentLoginUser != null || PrjPub.CurrentStudent != null)
            {
                railSystemID = PrjPub.GetRailSystemId();
                if (railSystemID != 0)
                {
                    IList<RailExam.Model.Organization> organizationList1 =
                        organizationsBLL.GetOrganizations(PrjPub.CurrentLoginUser.StationOrgID);
                    foreach (RailExam.Model.Organization organization in organizationList1)
                    {
                        strOwnIDs += strOwnIDs == string.Empty
                                         ? organization.OrganizationId.ToString()
                                         : "," + organization.OrganizationId;
                    }
                }
            }

            if(Request.QueryString.Get("Type") == "Station")
            {
                IList<RailExam.Model.Organization> organizations = organizationsBLL.GetOrganizationsByLevel(2);

                foreach (RailExam.Model.Organization organization in organizations)
                {
                    if (!organization.IsEffect)
                    {
                        continue;
                    }

                    if (organization.LevelNum==1)
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
                    tvOrganization.Nodes.Add(tvNode);
                }
            }
			else if(Request.QueryString.Get("Type") == "Online")
			{
				IList<RailExam.Model.Organization> organizations = organizationsBLL.GetOrganizations();

                IList<RailExam.Model.Organization> organizationList = new List<Organization>();

                foreach (Organization organization in organizations)
			    {
                    if (!organization.IsEffect)
                    {
                        continue;
                    }

                    organizationList.Add(organization);
			    }

                Pub.BuildComponentArtTreeView(tvOrganization, (IList)organizationList, "OrganizationId",
											  "ParentId", "ShortName", "FullName", "OrganizationId", null, null, null);
			}
            else
            {
				if (PrjPub.IsServerCenter && PrjPub.CurrentLoginUser.SuitRange == 1)
				{
					IList<RailExam.Model.Organization> organizations = organizationsBLL.GetOrganizations();

                    foreach (RailExam.Model.Organization organization in organizations)
                    {
                        if (!organization.IsEffect)
                        {
                            continue;
                        }

                        if (organization.LevelNum != 1)
                        {
                            if ((organization.IdPath + "/").IndexOf("/1/") >= 0 && railSystemID != 0 && railSystemID != organization.RailSystemID)
                            {
                                continue;
                            }

                            if (strOwnIDs != string.Empty && (organization.IdPath + "/").IndexOf("/1/") < 0 && ("," + strOwnIDs + ",").IndexOf("," + organization.OrganizationId + ",") < 0)
                            {
                                continue;
                            }
                        }   

				        TreeViewNode tvn = new TreeViewNode();
                        tvn.ID = organization.OrganizationId.ToString();
                        tvn.Value = organization.OrganizationId.ToString();
                        tvn.Text = organization.ShortName;
                        tvn.ToolTip = organization.FullName;

                        if (organization.ParentId == 0)
                        {
                            tvn.Expanded = true;
                            tvOrganization.Nodes.Add(tvn);
                            continue;
                        }

                        try
                        {
                            tvOrganization.FindNodeById(organization.ParentId.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvOrganization.Nodes.Clear();
                            SessionSet.PageMessage = "数据错误！";
                            return;
                        }
                    }
				}
				else
				{
					string strOrgID;
					if (PrjPub.IsServerCenter)
					{
                        if(!string.IsNullOrEmpty(Request.QueryString.Get("OrgID")))
                        {
                            strOrgID = Request.QueryString.Get("OrgID");
                        }
                        else
                        {
                            strOrgID = PrjPub.CurrentLoginUser.StationOrgID.ToString();
                        }
					}
					else
					{
						strOrgID = ConfigurationManager.AppSettings["StationID"].ToString();
					}
					int stationID = organizationsBLL.GetStationOrgID(Convert.ToInt32(strOrgID));
					IList<RailExam.Model.Organization> organizationList =
						organizationsBLL.GetOrganizations(stationID);

					if (organizationList.Count > 0)
					{
						TreeViewNode tvn = null;

						foreach (RailExam.Model.Organization organization in organizationList)
						{
                            if (!organization.IsEffect)
                            {
                                continue;
                            }

							tvn = new TreeViewNode();
							tvn.ID = organization.OrganizationId.ToString();
							tvn.Value = organization.IdPath.ToString();
							tvn.Text = organization.ShortName;
							tvn.ToolTip = organization.FullName;

							if (organization.ParentId == 1)
							{
								tvOrganization.Nodes.Add(tvn);
							}
							else
							{
								try
								{
									tvOrganization.FindNodeById(organization.ParentId.ToString()).Nodes.Add(tvn);
								}
								catch
								{
									tvOrganization.Nodes.Clear();
									SessionSet.PageMessage = "数据错误！";
									return;
								}
							}
						}
					}

					tvOrganization.DataBind();
				}

				if (tvOrganization.Nodes.Count > 0)
				{
					tvOrganization.Nodes[0].Expanded = true;
				}
            }

            if (tvOrganization.Nodes.Count > 0)
            {
                tvOrganization.Nodes[0].Expanded = true;
            }
        }
    }
}
