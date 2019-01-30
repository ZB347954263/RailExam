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
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
    public partial class Organization : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !tvOrganizationChangeCallBack.IsCallback)
            {
				if (PrjPub.CurrentLoginUser == null)
				{
					Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
					return;
				}

                if (PrjPub.HasEditRight("组织机构") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }
                if (PrjPub.HasDeleteRight("组织机构") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }

                BindOrganizationTree();
            }
        }

        private void BindOrganizationTree()
        {
            OrganizationBLL organizationBLL = new OrganizationBLL();

            IList<RailExam.Model.Organization> organizationList = new List<RailExam.Model.Organization>();

            int railSystemID = 0;
            if (PrjPub.IsServerCenter)
            {
                SystemRoleBLL roleBll = new SystemRoleBLL();
                SystemRole role = roleBll.GetRole(PrjPub.CurrentLoginUser.RoleID);
                railSystemID = role.RailSystemID;
            }

            if (PrjPub.IsServerCenter && (PrjPub.CurrentLoginUser.SuitRange == 1 || railSystemID != 0))
            {
                SystemRoleBLL roleBll = new SystemRoleBLL();
                SystemRole role = roleBll.GetRole(PrjPub.CurrentLoginUser.RoleID);

                organizationList = organizationBLL.GetOrganizations();

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
                    tvn = new TreeViewNode();
                    //tvn.ID = "0";
                    //tvn.Value = "0";
                    //tvn.Text = PrjPub.GetRailNameBao();
                    //tvn.ToolTip = PrjPub.GetRailNameBao();
                    //tvOrganization.Nodes.Add(tvn);

                    foreach (RailExam.Model.Organization organization in organizationList)
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



                        try
                        {
                            if (organization.ParentId == 0)
                            {
                                tvn.Expanded = true;
                                tvOrganization.Nodes.Add(tvn);
                            }
                            else
                            {
                                tvOrganization.FindNodeById(organization.ParentId.ToString()).Nodes.Add(tvn);
                            }                     
                        }
                        catch
                        {
                            tvOrganization.Nodes.Clear();
                            SessionSet.PageMessage = "数据错误！";
                            return;
                        }
                    }
                }

                //Pub.BuildComponentArtTreeView(tvOrganization, (IList)organizationList, "OrganizationId",
                //    "ParentId", "ShortName", "FullName", "OrganizationId", null, null, null);
            }
            else
            {
            	string strOrgID;
				if(PrjPub.IsServerCenter)
				{
					strOrgID = PrjPub.CurrentLoginUser.StationOrgID.ToString();
				}
				else
				{
					strOrgID = ConfigurationManager.AppSettings["StationID"].ToString();
				}
				int stationID = organizationBLL.GetStationOrgID(Convert.ToInt32(strOrgID));

                organizationList =
                    organizationBLL.GetOrganizations(stationID);

                if (organizationList.Count > 0)
                {
                    TreeViewNode tvn = null;
                    tvn = new TreeViewNode();
                    tvn.ID = "0";
                    tvn.Value = "0";
                    tvn.Text = PrjPub.GetRailNameBao();
                    tvn.ToolTip = PrjPub.GetRailNameBao();
                    tvOrganization.Nodes.Add(tvn);

                    foreach (RailExam.Model.Organization organization in organizationList)
                    {
                        tvn = new TreeViewNode();
                        tvn.ID = organization.OrganizationId.ToString();
                        tvn.Value = organization.OrganizationId.ToString();
                        tvn.Text = organization.ShortName;
                        tvn.ToolTip = organization.FullName;

                        if (organization.LevelNum == 2)
                        {
                            tvn.Expanded = true;
                        }

                        try
                        {
                            if (organization.LevelNum == 2)
                            {
                                tvOrganization.FindNodeById("0").Nodes.Add(tvn);
                            }
                            else
                            {
                                tvOrganization.FindNodeById(organization.ParentId.ToString()).Nodes.Add(tvn);
                            }
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

			if (tvOrganization.Nodes.Count > 0)
			{
				tvOrganization.Nodes[0].Expanded = true;
			}
        }

        #region //ComponentArt CallBack Methods

        [ComponentArtCallbackMethod]
        public bool tvOrganizationNodeMove(int organizationId, string direction)
        {
            OrganizationBLL organizationBLL = new OrganizationBLL();

            if (direction.ToUpper() == "UP")
            {
                return organizationBLL.MoveUp(organizationId);
            }
            else if (direction.ToUpper() == "DOWN")
            {
                return organizationBLL.MoveDown(organizationId);
            }
            else
            {
                SessionSet.PageMessage = "未知移动方向！";
            }

            return false;
        }

        protected void tvOrganizationChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {
            OrganizationBLL objOrgBll = new OrganizationBLL();
            if (e.Parameters[1] == "Insert")
            {
                string strSql = "Org_ID = (select Max(Org_ID) from Org where Parent_ID="+ e.Parameters[0] +")";
                IList<RailExam.Model.Organization> objList = objOrgBll.GetOrganizationsByWhereClause(strSql);
                hfMaxID.Value = objList[0].OrganizationId.ToString();
                hfMaxID.RenderControl(e.Output);
            }
            else if (e.Parameters[1] == "Update" || e.Parameters[1] == "Down" || e.Parameters[1] == "Up" || e.Parameters[1] == "Delete")
            {
                hfMaxID.Value = e.Parameters[0];
                hfMaxID.RenderControl(e.Output);
            }

            tvOrganization.Nodes.Clear();
            BindOrganizationTree();

            tvOrganization.RenderControl(e.Output);
        }

        #endregion
    }
}