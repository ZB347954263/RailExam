using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.TrainManage
{
    public partial class TrainClassList : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("../Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }
                //绑定培训计划类别
                OracleAccess oracleAccess = new OracleAccess();
                DataSet ds1 = oracleAccess.RunSqlDataSet("select * from ZJ_TRAIN_PLAN where SPONSOR_UNIT_ID="+ PrjPub.CurrentLoginUser.StationOrgID+
					"or undertake_unit_id=" +PrjPub.CurrentLoginUser.StationOrgID+" order by train_plan_id desc ");
                ddlTrainPlan.DataSource = ds1.Tables[0].DefaultView;
                ddlTrainPlan.DataTextField = "TRAIN_PLAN_NAME";
                ddlTrainPlan.DataValueField = "TRAIN_PLAN_ID";
                ddlTrainPlan.DataBind();
                hfSelect.Value = GetSql();

                if (PrjPub.HasEditRight("培训班管理") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }

                if (PrjPub.HasDeleteRight("培训班管理") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }
            }
        }

        /// <summary>
        /// 项目类别绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlTrainPlan_DataBound(object sender, EventArgs e)
        {
            ddlTrainPlan.Items.Insert(0, "--请选择--");
        }
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            hfSelect.Value = GetSql();
            grdEntity.DataBind();
        }

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
                    e.Row.Attributes.Add("onclick", "selectArow(this);");
                }
            }
        }
        protected void grdEntity_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string Id = e.CommandArgument.ToString();
            if (string.IsNullOrEmpty(Id))
            {
                return;
            }

            if (e.CommandName == "del")
            {
                try
                {
                    OracleAccess oracleAccess = new OracleAccess();
                    oracleAccess.ExecuteNonQuery("update zj_train_plan_employee set train_class_id=null where train_class_id="+Id);
                    string sql = "delete from ZJ_TRAIN_CLASS where TRAIN_CLASS_ID=" + Id;
                    oracleAccess.ExecuteNonQuery(sql);
                }
                catch
                {
                    ClientScript.RegisterStartupScript(GetType(), "Error", "alert('该培训班已被引用，不能删除！');", true);
                    return;
                }

                //ClientScript.RegisterStartupScript(GetType(), "OK", "alert('删除成功！');", true);
                hfSelect.Value = GetSql();
                grdEntity.DataBind();
            }
        }
        protected void grdEntity_RowDataBoundDataRow(object sender, GridViewRowEventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl addPlan = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Row.FindControl("add");      //修改
            System.Web.UI.HtmlControls.HtmlGenericControl spanView = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Row.FindControl("spanView"); //查看
            LinkButton delete = (LinkButton)e.Row.FindControl("btnDelete");
            if (!(PrjPub.HasEditRight("培训班管理") && PrjPub.IsServerCenter))
            {
                addPlan.Visible = false;
            }
            if (!(PrjPub.HasDeleteRight("培训班管理") && PrjPub.IsServerCenter))
            {
                delete.Visible = false;
            }
        }

        protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            DataTable db = e.ReturnValue as DataTable;
			db.Columns.Add("BEGIN_DATE_1", typeof(string));
			db.Columns.Add("END_DATE_1", typeof(string));
			db.Columns.Add("MAKEDATE_1", typeof(string));
			if(db!=null && db.Rows.Count>0)
			{
				foreach (DataRow r in db.Rows)
				{
					r["BEGIN_DATE_1"] = Convert.ToDateTime(r["BEGIN_DATE"]).ToString("yyyy-MM-dd");
					r["END_DATE_1"] = Convert.ToDateTime(r["END_DATE"]).ToString("yyyy-MM-dd");
					r["MAKEDATE_1"] = Convert.ToDateTime(r["MAKEDATE"]).ToString("yyyy-MM-dd");
				}
			}
        	if (db.Rows.Count == 0)
            {
                DataRow row = db.NewRow();
                row["TRAIN_Class_ID"] = -1;
                db.Rows.Add(row);
            }
        }

        private string GetSql()
        {
            string str = "";
            int railSystemId = PrjPub.GetRailSystemId();

            if (railSystemId != 0)
            {
                str = " or SPONSOR_UNIT_ID in (select Org_ID from Org where Rail_System_ID=" + railSystemId +
                      " and level_num=2)  or undertake_unit_id in (select Org_ID from Org where Rail_System_ID=" + railSystemId +
                      " and level_num=2) ";

                string strSql = "select Org_ID from Org where Rail_System_ID=" + railSystemId +
                " and level_num=2";
                OracleAccess db = new OracleAccess();
                DataSet ds = db.RunSqlDataSet(strSql);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    str += "or  ','|| orgids||',' like '%," + dr["Org_ID"] + ",%'";
                }
            }

            StringBuilder sql = new StringBuilder("select a.* from zj_train_class_view a "
                + " inner join ZJ_Train_Plan b on a.Train_Plan_ID=b.Train_Plan_ID " 
                +" where (b.SPONSOR_UNIT_ID="+ PrjPub.CurrentLoginUser.StationOrgID
                + " or UNDERTAKE_UNIT_ID=" + PrjPub.CurrentLoginUser.StationOrgID + str + " or  ','|| orgids||',' like '%," + PrjPub.CurrentLoginUser.StationOrgID + ",%')");

            if (txtCode.Text.Length != 0)
            {
                sql.AppendFormat(" and TRAIN_CLASS_CODE like '%{0}%'", txtCode.Text);
            }
            if (txtName.Text.Length != 0)
            {
                sql.AppendFormat(" and TRAIN_CLASS_NAME like '%{0}%'", txtName.Text);
            }
            if (ddlTrainPlan.SelectedValue != "--请选择--")
            {
                sql.AppendFormat(" and b.TRAIN_PLAN_ID={0}", ddlTrainPlan.SelectedValue);

            }
            if (ddlTrainPlanPhase.SelectedValue != "0")
            {
                sql.AppendFormat(" and TRAIN_CLASS_STATUS_ID={0}", ddlTrainPlanPhase.SelectedValue);

            }
        	sql.Append("  order by train_class_id desc");
            return sql.ToString();
        }
    }
}
