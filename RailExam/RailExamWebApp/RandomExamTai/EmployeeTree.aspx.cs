using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
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
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamTai
{
    public partial class EmployeeTree : PageBase
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

                if (PrjPub.HasEditRight("职员管理") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }

                hfRole.Value = PrjPub.CurrentLoginUser.RoleID.ToString();

                BindOrganizationTree();
            }

            if (hfRefresh.Value != "")
            {
                DownloadExcel(hfRefresh.Value);
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

            if (PrjPub.IsServerCenter && (PrjPub.CurrentLoginUser.SuitRange == 1 || railSystemID !=0))
            {
                organizationList = organizationBLL.GetOrganizations();

                string strOwnIDs = string.Empty;
                if(railSystemID !=0)
                {
                    IList<RailExam.Model.Organization> organizationList1 = organizationBLL.GetOrganizations(PrjPub.CurrentLoginUser.StationOrgID);
                    foreach (Organization organization in organizationList1)
                    {
                        strOwnIDs += strOwnIDs == string.Empty
                                         ? organization.OrganizationId.ToString()
                                         : "," + organization.OrganizationId;
                    }
                }

                if (organizationList.Count > 0)
                {
                    TreeViewNode tvn = null;
                    //tvn = new TreeViewNode();
                    //tvn.ID = "0";
                    //tvn.Value = "0";
                    //tvn.Text = PrjPub.GetRailNameBao();
                    //tvn.ToolTip = PrjPub.GetRailNameBao();
                    //tvView.Nodes.Add(tvn);

                    foreach (RailExam.Model.Organization organization in organizationList)
                    {
                        if(!organization.IsEffect)
                        {
                            continue;
                        }

                        if ((organization.IdPath+"/").IndexOf("/1/")>=0 && organization.LevelNum != 1 && railSystemID != 0 && railSystemID != organization.RailSystemID)
                        {
                            continue;
                        }

                        if (organization.LevelNum != 1 && strOwnIDs != string.Empty && (organization.IdPath + "/").IndexOf("/1/") < 0 && ("," + strOwnIDs + ",").IndexOf("," + organization.OrganizationId + ",") < 0)
                        {
                            continue;
                        }

                        tvn = new TreeViewNode();
                        tvn.ID = organization.OrganizationId.ToString();
                        tvn.Value = organization.IdPath;
                        tvn.Text = organization.ShortName;
                        tvn.ToolTip = organization.FullName;
                        try
                        {
                            if (organization.ParentId == 0)
                            {
                                tvn.Expanded = true;
                                tvView.Nodes.Add(tvn);
                            }
                            else
                            {
                                tvView.FindNodeById(organization.ParentId.ToString()).Nodes.Add(tvn);
                            }          
                        }
                        catch
                        {
                            tvView.Nodes.Clear();
                            SessionSet.PageMessage = "数据错误！";
                            return;
                        }
                    }
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
                    tvView.Nodes.Add(tvn);

                    foreach (RailExam.Model.Organization organization in organizationList)
                    {
                        if (!organization.IsEffect)
                        {
                            continue;
                        }

                        tvn = new TreeViewNode();
                        tvn.ID = organization.OrganizationId.ToString();
                        tvn.Value = organization.IdPath;
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
                                tvView.FindNodeById("0").Nodes.Add(tvn);
                            }
                            else
                            {
                                tvView.FindNodeById(organization.ParentId.ToString()).Nodes.Add(tvn);
                            }
                        }
                        catch
                        {
                            tvView.Nodes.Clear();
                            SessionSet.PageMessage = "数据错误！";
                            return;
                        }
                    }
                }

            }
            tvView.DataBind();

            if (tvView.Nodes.Count > 0)
            {
                tvView.Nodes[0].Expanded = true;
            }
        }

        private void DownloadExcel(string strName)
        {
            string filename = Server.MapPath("/RailExamBao/Excel/" + strName + ".xls");

            if (File.Exists(filename))
            {
                FileInfo file = new FileInfo(filename);
                this.Response.Clear();
                this.Response.Buffer = true;
                this.Response.Charset = "utf-7";
                this.Response.ContentEncoding = Encoding.UTF7;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                this.Response.AddHeader("Content-Disposition",
                                        "attachment; filename=" + HttpUtility.UrlEncode(strName) + ".xls");
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度

                this.Response.AddHeader("Content-Length", file.Length.ToString());

                // 指定返回的是一个不能被客户端读取的流，必须被下载

                this.Response.ContentType = "application/ms-excel";

                // 把文件流发送到客户端

                this.Response.WriteFile(file.FullName);
            }
        }
    }
}
