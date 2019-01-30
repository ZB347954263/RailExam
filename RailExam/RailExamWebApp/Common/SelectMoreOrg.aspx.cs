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
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;

namespace RailExamWebApp.Common
{
    public partial class SelectMoreOrg : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ArrayList orgIDAL = new ArrayList();
                BindOrganizationTree(orgIDAL);
            }
        }

        private void BindOrganizationTree(ArrayList orgidAL)
        {
            string strIds = Request.QueryString.Get("selectOrgID");
            string classId = Request.QueryString.Get("classId");

            OracleAccess access = new OracleAccess();

            string strSql = "select * from ZJ_Train_Plan_Post_Class_Org where Train_Plan_Post_Class_ID=" + classId;
            DataSet ds = access.RunSqlDataSet(strSql);

            if(ds.Tables[0].Rows.Count > 0)
            {
                strSql = "select org_id,short_name,full_name from  org "
                         + " where level_num=2 and Is_Effect=1 "
                         + " and org_id not in (select org_id from ZJ_Train_Plan_Post_Class_Org "
                         +" where Train_Plan_Post_Class_ID=" + classId
                         +") order by order_index";
            }
            else
            {
                strSql = "select org_id,short_name,full_name from  org "
                         + " where level_num=2 and Is_Effect=1  order by order_index";
            }

            DataTable dt =
                access.RunSqlDataSet(strSql).Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (DataRow r in dt.Rows)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = r["org_id"].ToString();
                    tvn.Value = r["org_id"].ToString();
                    tvn.Text = r["short_name"].ToString();
                    tvn.ToolTip = r["full_name"].ToString();
                    tvn.ShowCheckBox = true;

                    if((","+strIds+",").IndexOf(","+r["org_id"]+",")>=0)
                    {
                        tvn.Checked = true;
                    }

                    if (orgidAL.Count > 0)
                    {
                        if (orgidAL.IndexOf(r["org_id"]) != -1)
                        {
                            tvn.Checked = true;
                        }
                    }
                    try
                    {
                        tvOrg.Nodes.Add(tvn);
                    }
                    catch
                    {
                        tvOrg.Nodes.Clear();
                        SessionSet.PageMessage = "数据错误！";
                        return;
                    }
                }
                tvOrg.DataBind();
                if (tvOrg.Nodes.Count > 0)
                {
                    tvOrg.Nodes[0].Expanded = true;
                }
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            List<string> lst = new List<string>();
            foreach (TreeViewNode tn in tvOrg.Nodes)
            {
                if (tn.Checked)
                {
                    lst.Add(tn.ID);
                }
            }
            string strArr = string.Join(",",lst.ToArray());
            if(lst.Count<=0)
                ClientScript.RegisterClientScriptBlock(GetType(),"","alert('请选择单位！')",true);
            else
                ClientScript.RegisterClientScriptBlock(GetType(), "", "col('" + strArr + "')", true);
            
        }
    }
}
