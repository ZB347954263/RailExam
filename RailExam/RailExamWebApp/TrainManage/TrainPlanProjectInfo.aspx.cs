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

namespace RailExamWebApp.TrainManage
{
    public partial class TrainPlanProjectInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString.Get("id");
                if (!String.IsNullOrEmpty(id))
                {
                    hfTypeID.Value = id;
                    BindGrid(id);
                }

                hfDelete.Value = PrjPub.HasDeleteRight("培训项目").ToString();
                hfUpdate.Value = PrjPub.HasEditRight("培训项目").ToString();

            }
        }

        //绑定培训项目数据表信息
        private void BindGrid(string id)
        {
            this.hfSelect.Value = GetSql(id);
            this.grdEntity.DataBind();
        }

        //得到查询语句
        private string GetSql(string id)
        {
            return "select * from ZJ_TRAINPLAN_PROJECT t where trainplan_type_id = " + id;
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

        protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            DataTable db = e.ReturnValue as DataTable;
            if (db != null && db.Rows.Count == 0)
            {
                DataRow row = db.NewRow();
                row["trainplan_project_id"] = -1;
                db.Rows.Add(row);
            }
        }

        //删除培训项目
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            LinkButton link = sender as LinkButton;
            Label lblID = link.Parent.FindControl("lblID") as Label;
            Del(lblID.Text);
            BindGrid(Request["id"].ToString());
        }

        private void Del(string id)
        {
            OracleAccess oa = new OracleAccess();
            string sql = String.Format("delete from ZJ_TRAINPLAN_PROJECT where trainplan_project_id = {0}", id);
            try
            {
                oa.ExecuteNonQuery(sql);
            }
            catch (Exception)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "OK", "alert('该项目已被引用,不应该被删除!');", true);
            }
        }
    }
}
