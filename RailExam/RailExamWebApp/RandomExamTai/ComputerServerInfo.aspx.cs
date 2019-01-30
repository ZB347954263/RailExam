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
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamTai
{
    public partial class ComputerServerInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

                string orgID = Request.QueryString.Get("OrgID");
                if (!String.IsNullOrEmpty(orgID))
                {
                    this.hfSelectOrg.Value = orgID;
                    BindGrid();
                }
            }
        }

        //绑定服务器数据表信息
        private void BindGrid()
        {
            this.hfSelect.Value = GetSql();
            this.grdEntity.DataBind();
        }

        //得到查询语句
        private string GetSql()
        {
            string orgID = Request.QueryString.Get("OrgID");
            return String.IsNullOrEmpty(orgID) ?
                String.Empty :
                String.Format("select * from COMPUTER_SERVER where org_id = {0}", orgID);
        }

        /// <summary>
        /// 选中行是变色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdEntity_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
            {
                if (grdEntity.DataKeys[e.Row.RowIndex][0].ToString() == "-1")
                {
                    e.Row.Visible = false;
                }
                else
                {
                    e.Row.Attributes.Add("onclick", "selectArow('" + e.Row.RowIndex + "');");
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            if (PrjPub.CurrentLoginUser == null)
            {
                Response.Redirect("../Common/Error.aspx?error=Session过期请重新登录本系统！");
                return;
            }

            BindGrid();
        }

        protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            DataTable db = e.ReturnValue as DataTable;
            if (db != null && db.Rows.Count == 0)
            {
                DataRow row = db.NewRow();
                row["COMPUTER_SERVER_ID"] = -1;
                db.Rows.Add(row);
            }
        }

        //删除服务器
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            LinkButton link=sender as LinkButton;
            Label lblID = link.Parent.FindControl("lblID") as Label;
            Del(lblID.Text);
            BindGrid();
        }

        private void Del(string csID)
        {
            OracleAccess oa = new OracleAccess();
            string sql = String.Format("delete from COMPUTER_SERVER where COMPUTER_SERVER_ID = {0}", csID);
            try
            {
                oa.ExecuteNonQuery(sql);
            }
            catch (Exception)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "OK", "alert('删除失败');", true);
            }
        }
    }
}
