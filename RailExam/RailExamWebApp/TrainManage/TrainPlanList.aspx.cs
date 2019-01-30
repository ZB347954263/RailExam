using System;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.TrainManage
{
    public partial class TrainPlanList : PageBase
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

                int railSystemId = PrjPub.RailSystemId();

                string str = "";
                if (railSystemId != 0)
                {
                    str = " and (Rail_System_ID= " + railSystemId + " or org_id=" + PrjPub.CurrentLoginUser.StationOrgID + ")";
                }
                string strSql = "select * from org where level_num=2 " + str + "  and Is_Effect=1 order by parent_id,order_index";

                OracleAccess db = new OracleAccess();
                DataSet ds = db.RunSqlDataSet(strSql);

                ListItem item1 = new ListItem();
                item1.Text = "--请选择--";
                item1.Value = "0";
                ddlOrg.Items.Add(item1);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListItem item = new ListItem();
                    item.Text = dr["Short_Name"].ToString();
                    item.Value = dr["Org_ID"].ToString();
                    ddlOrg.Items.Add(item);
                }

                //ddlOrg.SelectedValue = PrjPub.CurrentLoginUser.StationOrgID.ToString();
                
                if (PrjPub.CurrentLoginUser.SuitRange == 0)
                {
                    ddlOrg.Visible = false;
                    lblOrg.Visible = false;
                }

                for (int i = 2010; i < 2027; i++)
                {
                    ListItem item = new ListItem();
                    item.Text = i.ToString();
                    item.Value = i.ToString();
                    ddlYear.Items.Add(item);
                }
                ddlYear.Items.Insert(0, "--请选择--");
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                //绑定培训计划类别
                OracleAccess oracleAccess = new OracleAccess();
                DataSet ds1 = oracleAccess.RunSqlDataSet("select * from ZJ_TRAINPLAN_TYPE");
                ddlTrainPlanType.DataSource = ds1.Tables[0].DefaultView;
                ddlTrainPlanType.DataTextField = "TRAINPLAN_TYPE_NAME";
                ddlTrainPlanType.DataValueField = "TRAINPLAN_TYPE_ID";
                ddlTrainPlanType.DataBind();
                hfSelect.Value = GetSql();


                if (PrjPub.HasEditRight("培训计划") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }

                if (PrjPub.HasDeleteRight("培训计划") && PrjPub.IsServerCenter)
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
        protected void ddlSPONSORUNIT_DataBound(object sender, EventArgs e)
        {
            //ddlSPONSORUNIT.Items.Insert(0, "--请选择--");
        }
        /// <summary>
        /// 项目类别绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlTrainPlanType_DataBound(object sender, EventArgs e)
        {
            ddlTrainPlanType.Items.Insert(0, "--请选择--");
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
                    string sql = "select * from zj_train_class where train_plan_id=" + Id;
                    DataSet dsClass = oracleAccess.RunSqlDataSet(sql);
                    string strInfo = string.Empty;
                    foreach (DataRow dr in dsClass.Tables[0].Rows)
                    {
                        strInfo += strInfo==string.Empty ? "【"+dr["Train_Class_Name"]+"】":
                        "、【" + dr["Train_Class_Name"] + "】";
                    }

                    if(strInfo != string.Empty)
                    {
                        strInfo = "该培训计划下存在以下培训培训班：" + strInfo;
                    }

                    sql = "select b.Exam_Name from Random_Exam_Train_Class a "
                          + " inner join Random_Exam b on a.Random_Exam_ID=b.Random_Exam_ID "
                          + " inner join ZJ_Train_Class c on a.Train_Class_ID=c.Train_Class_ID "
                          + " where c.Train_Plan_ID=" + Id;
                    DataSet dsExam = oracleAccess.RunSqlDataSet(sql);
                    string strExam = string.Empty;
                    foreach (DataRow dr in dsExam.Tables[0].Rows)
                    {
                        strExam += strExam == string.Empty ? "【" + dr["Exam_Name"] + "】" :
                        "、【" + dr["Exam_Name"] + "】";
                    }

                    if (strExam != string.Empty)
                    {
                        strInfo = strInfo + "该培训计划下存在以下考试：" + strExam;
                    }

                    if(strInfo != string.Empty)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Error", "alert('" + strInfo + "');", true);
                        return;
                    }

                    sql = "delete from zj_train_plan_employee where train_plan_id=" + Id;
                    oracleAccess.ExecuteNonQuery(sql);

                    sql = "delete from ZJ_TRAIN_PLAN_POST_CLASS_ORG where TRAIN_PLAN_POST_CLASS_ID in (select TRAIN_PLAN_POST_CLASS_ID from ZJ_TRAIN_PLAN_POST_CLASS where Train_Plan_ID=" + Id+")";
                    oracleAccess.ExecuteNonQuery(sql);

                    sql="delete from ZJ_TRAIN_PLAN_POST_CLASS where Train_Plan_ID=" + Id;
                    oracleAccess.ExecuteNonQuery(sql);

                    sql = "delete from ZJ_TRAIN_PLAN_POST where Train_Plan_ID=" + Id;
                    oracleAccess.ExecuteNonQuery(sql);

                    sql = "delete from zj_train_plan where train_plan_id=" + Id;
                    oracleAccess.ExecuteNonQuery(sql);
                }
                catch(Exception ex)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Error", "alert('该培训计划已被引用，不能删除！');", true);
                    return;
                }

                //ClientScript.RegisterStartupScript(GetType(), "OK", "alert('删除成功！');", true);
                hfSelect.Value = GetSql();
                grdEntity.DataBind();
            }
        }
        protected void grdEntity_RowDataBoundDataRow(object sender, GridViewRowEventArgs e)
        {
            HiddenField  OrgIDs = (HiddenField)e.Row.FindControl("hidOrgID");
            HiddenField  UnitID = (HiddenField)e.Row.FindControl("hidUnitID");
            HiddenField hidMaiUnitID = (HiddenField)e.Row.FindControl("hidMaiUnitID");

            System.Web.UI.HtmlControls.HtmlGenericControl spanUp = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Row.FindControl("spanUp");   //上报
            System.Web.UI.HtmlControls.HtmlGenericControl addPlan = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Row.FindControl("add");      //修改
            System.Web.UI.HtmlControls.HtmlGenericControl spanView = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Row.FindControl("spanView"); //查看

            LinkButton delete = (LinkButton)e.Row.FindControl("btnDelete");
            if (UnitID.Value.Equals(PrjPub.CurrentLoginUser.StationOrgID.ToString()))
            {
                spanView.Visible = true;
                addPlan.Visible = false;
                delete.Visible = false;
            }

            if (hidMaiUnitID.Value.Equals(PrjPub.CurrentLoginUser.StationOrgID.ToString()))
            {
				spanView.Visible = true;
                addPlan.Visible = true;
                delete.Visible = true;
            }
            else
            {
                addPlan.Visible = false;
                delete.Visible = false;
            }

            string[] arrOrgs = OrgIDs.Value.Split(',');
            foreach (string str in arrOrgs)
            {
                if (str.Equals(PrjPub.CurrentLoginUser.StationOrgID.ToString()))
                {
                    spanUp.Visible = true;
                    break;
                }
                else
                    spanUp.Visible = false;
            }

            if (!(PrjPub.HasEditRight("培训计划") && PrjPub.IsServerCenter))
            {
                addPlan.Visible = false;
            }
            if (!(PrjPub.HasDeleteRight("培训计划") && PrjPub.IsServerCenter))
            {
                delete.Visible = false;
            }
        }
        protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
        {
            DataTable db = e.ReturnValue as DataTable;
			db.Columns.Add("BEGINDATE_1",typeof(string));
			db.Columns.Add("ENDDATE_1", typeof(string));
			db.Columns.Add("MAKEDATE_1", typeof(string));
        	foreach (DataRow r in db.Rows)
        	{
				r["BEGINDATE_1"] = Convert.ToDateTime(r["BEGINDATE"]).ToString("yyyy-MM-dd");
				r["ENDDATE_1"] = Convert.ToDateTime(r["ENDDATE"]).ToString("yyyy-MM-dd");
				r["MAKEDATE_1"] = Convert.ToDateTime(r["MAKEDATE"]).ToString("yyyy-MM-dd");
        	}
            if (db.Rows.Count == 0)
            {
                DataRow row = db.NewRow();
                row["TRAIN_PLAN_ID"] = -1;
                db.Rows.Add(row);
            }
           
        }

        private string GetSql()
        {
            StringBuilder sql =new StringBuilder("");
            if (PrjPub.CurrentLoginUser.EmployeeID != 0)
            {

                string str = "";
                int railSystemId = PrjPub.GetRailSystemId();
                if (railSystemId != 0)
                {
                    str = " or SPONSOR_UNIT_ID in (select Org_ID from Org where Rail_System_ID=" + railSystemId +
                          " and level_num=2)  or undertake_unit_id in (select Org_ID from Org where Rail_System_ID=" +
                          railSystemId +
                          " and level_num=2) ";

                    string strSql = "select Org_ID from Org where Rail_System_ID=" + railSystemId +
                                    " and level_num=2";
                    OracleAccess db = new OracleAccess();
                    DataSet ds = db.RunSqlDataSet(strSql);

                    string strOrgIDs = string.Empty;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        //str += "or  ','|| orgids||',' like '%," + dr["Org_ID"] + ",%'";

                        strOrgIDs += strOrgIDs == string.Empty ? dr["Org_ID"].ToString() : "," + dr["Org_ID"];
                    }

                    str +=
                        @" or Train_Plan_ID in (select distinct b.Train_Plan_ID from ZJ_TRAIN_PLAN_POST_CLASS_ORG a
                        inner join ZJ_TRAIN_PLAN_POST_CLASS b on a.TRAIN_PLAN_POST_CLASS_ID=b.TRAIN_PLAN_POST_CLASS_ID 
                        where a.Org_ID in (" +
                        strOrgIDs + "))";
                }

                // ','|| orgids||',' like '%," + PrjPub.CurrentLoginUser.StationOrgID + ",%'
                sql =
                    new StringBuilder("select * from zj_train_plan_view where (SPONSOR_UNIT_ID=" +
                                      PrjPub.CurrentLoginUser.StationOrgID
                                      + " or undertake_unit_id=" + PrjPub.CurrentLoginUser.StationOrgID + str +
                                      @" or Train_Plan_ID in (select distinct b.Train_Plan_ID from                         ZJ_TRAIN_PLAN_POST_CLASS_ORG a
                        inner join ZJ_TRAIN_PLAN_POST_CLASS b on a.TRAIN_PLAN_POST_CLASS_ID=b.TRAIN_PLAN_POST_CLASS_ID 
                        where a.Org_ID=" +
                                      PrjPub.CurrentLoginUser.StationOrgID + "))");
            }
            else
            {
                sql =
                    new StringBuilder("select * from zj_train_plan_view where 1=1");
            }

            if (ddlOrg.SelectedValue != "0")
            {
                sql.AppendFormat(" and SPONSOR_UNIT_ID={0}", ddlOrg.SelectedValue);
            }

            if (ddlYear.SelectedValue != "--请选择--")
            {
                sql.AppendFormat(" and Year={0}", ddlYear.SelectedValue);
            }
            if (txtTrainPlanName.Text.Length != 0)
            {
                sql.AppendFormat(" and TRAIN_PLAN_NAME like '%{0}%'", txtTrainPlanName.Text);
            }
            if (ddlTrainPlanType.SelectedValue != "--请选择--")
            {
                sql.AppendFormat(" and TRAIN_PLAN_TYPE_ID={0}", ddlTrainPlanType.SelectedValue);

            }
            if (ddlTrainPlanPhase.SelectedValue != "0")
            {
                sql.AppendFormat(" and TRAIN_PLAN_PHASE_ID={0}", ddlTrainPlanPhase.SelectedValue);

            }
            sql.Append("  order by MAKEDATE desc");
            return sql.ToString();
        }
    }
}
