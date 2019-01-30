using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamTai
{
    public partial class EmployeeTrain : System.Web.UI.Page
    {
        private OracleAccess access;
        private static int employeeID = 0;
        int trainClassID = 0;
        string beginDate = "";
        string endDate = "";
        int trainPlanTypeID = 0;
        string location = "";
        int hour = 0;
        string classSubject = " ";
        string createDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("yyyy-MM-dd");
        string createPerson = PrjPub.CurrentLoginUser != null ? PrjPub.CurrentLoginUser.EmployeeName : PrjPub.CurrentStudent.EmployeeName;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hfID.Value = Request.QueryString.Get("ID");
                employeeID = Convert.ToInt32(Request.QueryString.Get("ID"));
                BindGrid();

                string type = Request.QueryString.Get("Type");
                if (type == "0" || !PrjPub.IsServerCenter)
                {
                    int columnsCount = this.grdEntity.Levels[0].Columns.Count;
                    this.grdEntity.Levels[0].Columns[columnsCount - 1].Visible = false;
                }

                hfIsServerCenter.Value = PrjPub.IsServerCenter.ToString();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string mode = Request.Form["__EVENTARGUMENT"];
            if (mode != "ref")
                DeleteInfo();
            BindGrid();
        }
        private void BindGrid()
        {
            grdEntity.DataSource = GetInfo();
            grdEntity.DataBind();
        }
        private DataTable GetInfo()
        {

            DataTable dt = new DataTable();
            access = new OracleAccess();
            StringBuilder sql = new StringBuilder();
            sql.Append(" select et.employee_train_id,et.employee_id,tc.train_class_name,tt.trainplan_type_name,  ");
            sql.Append("  et.begin_date,et.end_date,et.location, et.class_hour_count,et.train_subject, ");
            sql.Append(" et.create_date,et.create_person from zj_employee_train et ");
            sql.Append(" left join zj_train_class tc on tc.train_class_id=et.train_class_id ");
            sql.Append(" left join zj_trainplan_type tt on tt.trainplan_type_id=et.train_type_id ");
            sql.AppendFormat(" where et.employee_id={0} order by et.begin_date desc", Convert.ToInt32(Request.QueryString.Get("ID")));
            dt = access.RunSqlDataSet(sql.ToString()).Tables[0];
            dt.Columns.Add("begin_date1", typeof(string));
            dt.Columns.Add("end_date1", typeof(string));
            dt.Columns.Add("create_date1", typeof(string));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    if (r["begin_date"].ToString() != "")
                        r["begin_date1"] = Convert.ToDateTime(r["begin_date"].ToString()).ToString("yyyy-MM-dd");
                    if (r["end_date"].ToString()!="")
                        r["end_date1"] = Convert.ToDateTime(r["end_date"].ToString()).ToString("yyyy-MM-dd");
                    r["create_date1"] = Convert.ToDateTime(r["create_date"].ToString()).ToString("yyyy-MM-dd");
                }
            }
            return dt;
        }
        private void DeleteInfo()
        {
            try
            {
                string id = Request.Form["__EVENTARGUMENT"];
                string sql = string.Format("delete from zj_employee_train where employee_train_id={0}", Convert.ToInt32(id));
                 access = new OracleAccess();
                access.ExecuteNonQuery(sql);
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据删除成功！')", true);
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据删除失败！')", true);
            }
        }

        protected void btnRef_Click(object sender, EventArgs e)
        {
            RefInfo();

        }
       
        /// <summary>
        /// 更新信息
        /// </summary>
        private void RefInfo()
        {

            //获取基本信息
            StringBuilder sqlClass=new StringBuilder();
            sqlClass.Append(" select C.TRAIN_CLASS_ID,begin_date,end_date,TP.TRAIN_PLAN_TYPE_ID,TP.Location");
            sqlClass.Append(" from zj_train_class TC right join (select  train_plan_id, train_class_id from zj_train_plan_employee");
            sqlClass.AppendFormat(" where employee_id={0} and train_class_id is not null ", employeeID);
            sqlClass.Append("  ) C on C.TRAIN_CLASS_ID=TC.TRAIN_CLASS_ID ");
            sqlClass.Append(" left join zj_train_plan TP on TP.TRAIN_PLAN_ID=C.TRAIN_PLAN_ID");
             access=new OracleAccess();
            DataTable dtClass = access.RunSqlDataSet(sqlClass.ToString()).Tables[0];
            access.ExecuteNonQuery(" delete from zj_employee_train where employee_id=" + employeeID);
            if(dtClass!=null && dtClass.Rows.Count>0)
            {
                foreach (DataRow r in dtClass.Rows)
                {
                    trainClassID = Convert.ToInt32(r["TRAIN_CLASS_ID"]);
                    beginDate = r["begin_date"].ToString();
                    endDate = r["end_date"].ToString();
                    trainPlanTypeID = Convert.ToInt32(r["TRAIN_PLAN_TYPE_ID"]);
                    location = r["Location"].ToString();

                    GetSubAndHour(trainClassID);
                    InsertInfo();
                }

            }  
            BindGrid();
        }

        /// <summary>
        /// /获取科目名称和培训时间
        /// </summary>
        /// <param name="classID"></param>
        private void GetSubAndHour(int classID)
        {
            DataTable dtSubject =
                access.RunSqlDataSet(
                    "select subject_name,class_hour from zj_train_class_subject where train_class_id=" +
                    classID).Tables[0];
            if (dtSubject != null && dtSubject.Rows.Count > 0)
            {
                List<string> lst = new List<string>();
                foreach (DataRow r in dtSubject.Rows)
                {
                    lst.Add(r["subject_name"].ToString());
                    if (r["class_hour"].ToString() != "")
                        hour += Convert.ToInt32(r["class_hour"]);
                }
                classSubject = string.Join(",", lst.ToArray());
            }
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        private void InsertInfo()
        {
            //新增培训数据
             access=new OracleAccess();
            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.Append("insert into zj_employee_train values(EMPLOYEE_TRAIN_SEQ.NEXTVAL,");
            sqlInsert.Append("{0},{1},to_date('{2}','yyyy-mm-dd hh24:mi:ss'),to_date('{3}','yyyy-mm-dd hh24:mi:ss'),");
            sqlInsert.Append("{4},'{5}',{6},'{7}',to_date('{8}','yyyy-mm-dd hh24:mi:ss'),'{9}')");
            string sqlIns = string.Format(sqlInsert.ToString(), employeeID, trainClassID, beginDate, endDate,
                                          trainPlanTypeID, location, hour, classSubject, createDate, createPerson);
            try
            {
             
                if (trainClassID > 0)
                    access.ExecuteNonQuery(sqlIns);
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据更新成功！');", true);
            }
            catch (Exception)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据更新失败！');", true);
            }
        }
    }
}
