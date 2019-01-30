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
using ComponentArt.Web.UI;
using System.Collections.Generic;
using RailExamWebApp.Common.Class;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;

namespace RailExamWebApp.AssistBook
{
    public partial class InformationSystem : System.Web.UI.Page
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

                tvInformation.Nodes.Clear();
                GenerateInformationTree(null);

                if (PrjPub.HasEditRight("资料领域") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }
                if (PrjPub.HasDeleteRight("资料领域") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }
            }
        }

        private void GenerateInformationTree(ComponentArt.Web.UI.TreeViewNode parentNode)
        {
            string parentID = parentNode == null ? "0" : parentNode.ID;

            string sql = String.Format(
                "select information_system_id,"
                + " parent_id,"
                + " information_system_name"
                + " from INFORMATION_SYSTEM t"
                + " where (parent_id = {0})"
                + " order by order_index",
                parentID);
            OracleAccess ora = new OracleAccess();
            DataSet ds = ora.RunSqlDataSet(sql);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                ComponentArt.Web.UI.TreeViewNode newNode = new TreeViewNode();
                newNode.ID = Convert.ToString(row["information_system_id"]);
                newNode.Text = Convert.ToString(row["information_system_name"]);

                if (parentNode == null)
                {
                    tvInformation.Nodes.Add(newNode);
                }
                else
                {
                    parentNode.Nodes.Add(newNode);
                }

                GenerateInformationTree(newNode);
            }
        }

        [ComponentArtCallbackMethod]
        public bool tvInformationNodeMove(int informationId, string direction)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TREE_NODE_M";
            DbCommand dbCmd = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCmd, "p_table_name", DbType.String, "INFORMATION_SYSTEM");
            db.AddInParameter(dbCmd, "p_id_field_name", DbType.String, "INFORMATION_SYSTEM_ID");
            db.AddInParameter(dbCmd, "p_id", DbType.Int32, informationId);
            db.AddOutParameter(dbCmd, "p_result", DbType.Int32, 4);

            if (direction.ToUpper() == "UP")
            {                
                db.AddInParameter(dbCmd, "p_direction", DbType.Int32, 1);                
            }
            else if (direction.ToUpper() == "DOWN")
            {
                db.AddInParameter(dbCmd, "p_direction", DbType.Int32, 0);        
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), String.Empty, "alert('未知移动方向')", true);
                return false;
            }

            db.ExecuteNonQuery(dbCmd);

            return ((int)db.GetParameterValue(dbCmd, "p_result") == 1) ? true : false;
        }

        protected void tvInformationChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {
            if (e.Parameters[0] == "UP" || e.Parameters[0] == "DOWN")
            {
                hfMaxID.Value = e.Parameters[1];
                hfMaxID.RenderControl(e.Output);
            }

            tvInformation.Nodes.Clear();
            GenerateInformationTree(null);
            tvInformation.RenderControl(e.Output);
        }

        protected void imgbtnDel_Click(object sender, EventArgs e)
        {
            if (tvInformation.SelectedNode != null &&
                !String.IsNullOrEmpty(tvInformation.SelectedNode.ID))
            {
                string sql = String.Format("delete INFORMATION_SYSTEM where information_system_id = {0}", tvInformation.SelectedNode.ID);
                OracleAccess ora = new OracleAccess();
                try
                {
                    ora.ExecuteNonQuery(sql);
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), String.Empty, "alert('删除成功');location=location;", true);
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), String.Empty, "alert('" + ex.Message + "')", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), String.Empty, "alert('请选择节点')", true);
            }
        }

        protected void lnkbtnUp_Click(object sender, EventArgs e)
        {
            if (tvInformation.SelectedNode != null &&
                !String.IsNullOrEmpty(tvInformation.SelectedNode.ID))
            {
                try
                {
                    tvInformationNodeMove(Int32.Parse(tvInformation.SelectedNode.ID), "UP");
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), String.Empty, "location.href=location.href", true);
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), String.Empty, "alert('" + ex.Message + "')", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), String.Empty, "alert('请选择节点')", true);
            }
        }

        protected void lnkbtnDown_Click(object sender, EventArgs e)
        {
            if (tvInformation.SelectedNode != null &&
                !String.IsNullOrEmpty(tvInformation.SelectedNode.ID))
            {
                try
                {
                    tvInformationNodeMove(Int32.Parse(tvInformation.SelectedNode.ID), "DOWN");
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), String.Empty, "location.href=location.href", true);
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), String.Empty, "alert('" + ex.Message + "')", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(Page.GetType(), String.Empty, "alert('请选择节点')", true);
            }
        }
    }
}
