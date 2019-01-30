using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
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
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
    public partial class Employee : PageBase
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
                BindOrganizationTree();
            }
            else
            {
                if (Request.Form.Get("Refresh") == "true")
                {
                    BindOrganizationTree();
                }

                if (hfRefresh.Value != "")
                {
                   DownloadExcel(hfRefresh.Value);
                }
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

        private void BindOrganizationTree()
        {
            OrganizationBLL organizationBLL = new OrganizationBLL();

			if (PrjPub.IsServerCenter && PrjPub.CurrentLoginUser.SuitRange == 1)
			{
                IList<RailExam.Model.Organization> organizationList = organizationBLL.GetOrganizations();

                Pub.BuildComponentArtTreeView(tvView, (IList)organizationList, "OrganizationId",
                    "ParentId", "ShortName", "FullName", "IdPath", null, null, null);
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
                IList<RailExam.Model.Organization> organizationList =
                    organizationBLL.GetOrganizations(stationID);

                if (organizationList.Count > 0)
                {
                    TreeViewNode tvn = null;

                    foreach (RailExam.Model.Organization organization in organizationList)
                    {
                        tvn = new TreeViewNode();
                        tvn.ID = organization.OrganizationId.ToString();
                        tvn.Value = organization.IdPath.ToString();
                        tvn.Text = organization.ShortName;
                        tvn.ToolTip = organization.FullName;

                        if (organization.ParentId == 1)
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
                                SessionSet.PageMessage = "数据错误！";
                                return;
                            }
                        }
                    }
                }

                tvView.DataBind();
            }
           
            if(tvView.Nodes.Count > 0)
            {
                tvView.Nodes[0].Expanded = true;
            }
        }

        private void BindPostTree()
        {
            PostBLL postBLL = new PostBLL();
            IList<RailExam.Model.Post> postList = postBLL.GetPosts();

            Pub.BuildComponentArtTreeView(tvView, (IList)postList, "PostId",
                "ParentId", "PostName", "PostName", "IdPath", null, null, null);

            //tvView.ExpandAll();
        }

        protected void checkedBtnsChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {
            switch (e.Parameter)
            {
                case "rbnOrg":
                    {
                        tvView.Nodes.Clear();
                        BindOrganizationTree();
                        break;
                    }
                case "rbnPost":
                    {
                        tvView.Nodes.Clear();
                        BindPostTree();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            tvView.RenderControl(e.Output);
        }
    }
}