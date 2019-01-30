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
using RailExam.BLL;

namespace RailExamWebApp.Main
{
    public partial class EmployeeDesktop : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.lblIP.Text = Pub.GetRealIP();

                SystemVersionBLL objVersionBll = new SystemVersionBLL();
                this.lblVersion.Text = objVersionBll.GetVersion().ToString("0.0");

                int employeeID = PrjPub.CurrentStudent.EmployeeID;
                if (employeeID > 0)
                {
                    Grid1DataBind(employeeID);
                    Grid2DataBind(employeeID);
                    Grid3DataBind(employeeID);
                    Grid4DataBind(employeeID);
                }
            }
        }

        //正在参加的培训
        private void Grid1DataBind(int employeeID)
        {
            OracleAccess oracle = new OracleAccess();
            string sql = String.Format(
				@"select tt.trainplan_type_name,
                         tprj.trainplan_project_name,
                         tp.train_plan_name,
                         o1.full_name as sponsor_unit,
                         o2.full_name as undertake_unit,
                         tc1.train_class_id,
                         tc1.train_class_name,
                         tc1.begin_date,
                         tc1.end_date
                  from zj_train_class tc1
                  inner join zj_train_plan tp 
                        on tc1.train_plan_id = tp.train_plan_id
                  inner join zj_trainplan_type tt 
                        on tp.train_plan_type_id = tt.trainplan_type_id
                  inner join zj_trainplan_project tprj 
                        on tp.train_plan_project_id = tprj.trainplan_project_id
                  inner join org o1 
                        on tp.sponsor_unit_id = o1.org_id
                  inner join org o2 
                        on tp.undertake_unit_id = o2.org_id
                  where tc1.train_class_id in
                  (
                    select tc2.train_class_id 
                    from zj_train_class tc2
                    inner join zj_train_plan_employee tpe 
                          on tc2.train_class_id = tpe.train_class_id
                    where tpe.employee_id = {0}
                  )",
                employeeID
            );
            try
            {
                DataSet ds = oracle.RunSqlDataSet(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
					ds.Tables[0].Columns.Add("train_class_name1", typeof(string));
					foreach (DataRow r in ds.Tables[0].Rows)
					{
						r["train_class_name1"] = "<a href='#' onclick='showInfo(" + r["train_class_id"] + ")'>" + r["train_class_name"] + "</a>";
					}
                    this.grid1.DataSource = ds;
                    this.grid1.DataBind();
                }
            }
            catch { }
        }

        //计划参加的培训
        private void Grid2DataBind(int employeeID)
        {
            OracleAccess oracle = new OracleAccess();
            string sql = String.Format(
                @"select tt.trainplan_type_name,
                         tprj.trainplan_project_name,
                         tp.train_plan_name,
                         o1.full_name as sponsor_unit,
                         o2.full_name as undertake_unit,
                         tppc.class_name,
                         tppc.begin_date,
                         tppc.end_date         	   
                  from zj_train_plan_post_class tppc
                  inner join zj_train_plan_employee tpe
                        on tppc.train_plan_post_class_id = tpe.train_plan_post_class_id
                  inner join zj_train_plan tp 
	                    on tppc.train_plan_id = tp.train_plan_id
                  inner join zj_trainplan_type tt 
                        on tp.train_plan_type_id = tt.trainplan_type_id
                  inner join zj_trainplan_project tprj 
                        on tp.train_plan_project_id = tprj.trainplan_project_id
                  inner join org o1 
                        on tp.sponsor_unit_id = o1.org_id
                  inner join org o2 
                        on tp.undertake_unit_id = o2.org_id  
                  where tpe.employee_id = {0} and train_class_id is null",
                employeeID
            );
            try
            {
                DataSet ds = oracle.RunSqlDataSet(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    this.grid2.DataSource = ds;
                    this.grid2.DataBind();
                }
            }
            catch { }
        }

        private void Grid3DataBind(int employeeID)
        {
            string strSql = @"select b.Exam_Name,b.begin_time,b.end_time,
                            case when b.Start_Mode=1 then '随到随考' else '统一时间考试' end Start_Mode_Name,
                            case when b.Exam_Style=1 then '不存档考试' else '存档考试' end Exam_Style_Name,
                            d.Short_Name||'-'||c.Computer_Room_Name Exam_Address,
                            e.Short_Name Exam_Org_Name
                            from Random_Exam_Arrange_Detail a 
                            inner join Random_Exam b on a.Random_Exam_ID=b.Random_Exam_ID 
                            inner join Computer_Room c on a.Computer_Room_ID=c.Computer_Room_ID
                            inner join Org d on c.Org_ID=d.Org_ID
                            inner join Org e on b.Org_ID=e.Org_ID
                            where ','||a.User_Ids||',' like '%," + employeeID + ",%' and sysdate>=begin_time and sysdate<=end_time order by a.Random_Exam_ID desc";

            try
            {
                OracleAccess db = new OracleAccess();
                DataSet ds = db.RunSqlDataSet(strSql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    this.grid3.DataSource = ds;
                    this.grid3.DataBind();
                }
            }
            catch { }
        }

        private void Grid4DataBind(int employeeID)
        {
            string strSql = @"select b.Exam_Name,b.begin_time,b.end_time,
                            case when b.Start_Mode=1 then '随到随考' else '统一时间考试' end Start_Mode_Name,
                            case when b.Exam_Style=1 then '不存档考试' else '存档考试' end Exam_Style_Name,
                            d.Short_Name||'-'||c.Computer_Room_Name Exam_Address,
                            e.Short_Name Exam_Org_Name
                            from Random_Exam_Arrange_Detail a 
                            inner join Random_Exam b on a.Random_Exam_ID=b.Random_Exam_ID 
                            inner join Computer_Room c on a.Computer_Room_ID=c.Computer_Room_ID
                            inner join Org d on c.Org_ID=d.Org_ID
                            inner join Org e on b.Org_ID=e.Org_ID
                            where ','||a.User_Ids||',' like '%," + employeeID + ",%' and sysdate<begin_time order by a.Random_Exam_ID";

            try
            {
                OracleAccess db = new OracleAccess();
                DataSet ds = db.RunSqlDataSet(strSql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    this.grid4.DataSource = ds;
                    this.grid4.DataBind();
                }
            }
            catch { }
        }
    }
}
