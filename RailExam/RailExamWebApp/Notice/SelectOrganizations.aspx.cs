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
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.UI;
using DSunSoft.Web.Global;

namespace RailExamWebApp.Notice
{
    public partial class SelectOrganizations : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOrganizationTree();
            }
        }

        private void BindOrganizationTree()
        {
            OrganizationBLL organizationBLL = new OrganizationBLL();
            IList<RailExam.Model.Organization> organizationList = organizationBLL.GetOrganizations(0, 0, "", 0, 0,
                                                                                    "", "", "", "", "", "",
                                                                                    "", "", "", "", 0, 40,
                                                                                    "LevelNum,OrganizationId ASC");

            string strID = Request.QueryString["id"];
            string[] strIDS = { };
            if (!string.IsNullOrEmpty(strID))
            {
                strIDS = strID.Split(',');
            }

            if (organizationList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (Organization organization in organizationList)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = organization.OrganizationId.ToString();
                    tvn.Value = organization.OrganizationId.ToString();
                    tvn.Text = organization.ShortName;
                    tvn.ToolTip = organization.FullName;
                    tvn.ShowCheckBox = true;

                    foreach (string strOrgID in strIDS)
                    {
                        if (strOrgID == organization.OrganizationId.ToString())
                            tvn.Checked = true;
                    }

                    if (organization.ParentId == 0)
                    {
                        tvOrg.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            tvOrg.FindNodeById(organization.ParentId.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvOrg.Nodes.Clear();
                            SessionSet.PageMessage = "数据错误！";
                            return;
                        }
                    }
                }
            }

            tvOrg.DataBind();
            tvOrg.ExpandAll();
        }

        private void GetIDS(TreeViewNodeCollection nodes, ref string strIDS)
        {
            foreach (TreeViewNode node in nodes)
            {
                if (node.Checked)
                {
                    strIDS += node.ID + ",";
                }

                if (node.Nodes.Count > 0)
                {
                    GetIDS(node.Nodes, ref strIDS);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strIDS = string.Empty;
            string strScript = string.Empty;

            GetIDS(tvOrg.Nodes, ref strIDS);

            if (strIDS.Length > 1)
            {
                strIDS = strIDS.Remove(strIDS.Length - 1, 1);
            }

            strScript = "<script>window.opener.form1.FormView1_hfReceiveOrgIDS.value='" + strIDS
                      + "';window.close();</script>";

            Response.Write(strScript);
            Response.End();
        }
    }
}