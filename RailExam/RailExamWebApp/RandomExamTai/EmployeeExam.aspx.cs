using System;
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
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamTai
{
    public partial class EmployeeExam : PageBase
    {
        private OracleAccess access;
        private static int employeeID = 0;
        private string createDate = Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("yyyy-MM-dd");
        private string createPerson;

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.QueryString.Get("Type");
            if (string.IsNullOrEmpty(type))
            {
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }
                else
                {
                    createPerson = PrjPub.CurrentLoginUser.EmployeeName;
                }
            }
            if(!IsPostBack)
            {
                employeeID = Convert.ToInt32(Request.QueryString.Get("ID"));
                BindGrid();

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

            DeleteInfo();
            BindGrid();
        }

        private void DeleteInfo()
        {
            try
            {
                string id = Request.Form["__EVENTARGUMENT"];
                string sql = string.Format("delete from ZJ_EMPLOYEE_EXAM where employee_exam_id={0}",
                                           Convert.ToInt32(id));
                access = new OracleAccess();
                access.ExecuteNonQuery(sql);
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据删除成功！')", true);
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据删除失败！')", true);
            }
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
            sql.Append(" select E.*,(case E.Exam_Style when 1 then '机考' else  '试卷考试' end) Style,");
            sql.Append("(case E.EXAM_RESULT when 1 then '合格' else '不合格' end) Result,");
            sql.Append( "RE.exam_name from ZJ_EMPLOYEE_EXAM E left join RANDOM_EXAM RE on RE.random_exam_id=E.random_exam_id ");
            sql.AppendFormat(" where E. employee_id={0}", Convert.ToInt32(Request.QueryString.Get("ID")));
            dt = access.RunSqlDataSet(sql.ToString()).Tables[0];
            dt.Columns.Add("exam_date1", typeof (string));
            dt.Columns.Add("create_date1", typeof (string));
            dt.Columns.Add("RandomExamResultID", typeof(string));
            dt.Columns.Add("OrgID", typeof(string));
            if (dt.Rows.Count > 0)
            {
                string strSql;
                foreach (DataRow r in dt.Rows)
                {
                    strSql = "select * from Random_Exam_Result where Random_Exam_ID=" + r["Random_Exam_ID"] +
                          " and Examinee_ID=" + r["Employee_ID"] + " and score=" + r["Exam_Score"];
                    DataSet ds = access.RunSqlDataSet(strSql);
                    if(ds.Tables[0].Rows.Count>0)
                    {
                        r["RandomExamResultID"] = ds.Tables[0].Rows[0]["Random_Exam_Result_ID"].ToString();
                        r["OrgID"] = ds.Tables[0].Rows[0]["Org_ID"].ToString();
                    }
                    else
                    {
                        r["RandomExamResultID"] = "0";
                        r["OrgID"] = "0";
                    }

                    if (r["exam_date"].ToString() != "")
                        r["exam_date1"] = Convert.ToDateTime(r["exam_date"].ToString()).ToString("yyyy-MM-dd");
                    r["create_date1"] = Convert.ToDateTime(r["create_date"].ToString()).ToString("yyyy-MM-dd");
                }
            }
            return dt;
        }

        protected void btnRef_Click(object sender, EventArgs e)
        {
            access=new OracleAccess();
            string strSql =
                @"select ER.Begin_Time,E.Is_Computerexam,
                    case when z.computer_room_seat=0 then GetOrgName(ER.org_ID)||CR.Computer_Room_Name||'微机教室'
                    else GetOrgName(ER.org_ID)||CR.Computer_Room_Name||'微机教室-'||z.computer_room_seat||'机位' end Computer_Room_Name, 
                    case when y.subject_name is not null then y.subject_name else
                    case when  MT.RANDOM_EXAM_MODULAR_TYPE_NAME is not null then (MT.RANDOM_EXAM_MODULAR_TYPE_NAME||'-'|| E.EXAM_NAME) 
                    else E.EXAM_NAME end  end as subject, 
                    ER.Score,case when ER.Score>=E.Pass_Score then 1 else 0 end as Is_Pass,E.RANDOM_EXAM_ID 
                    from random_exam_result ER 
                    inner join random_exam_result_detail z on ER.Random_Exam_Result_Id=z.Random_Exam_Result_Id and z.is_remove=1
                    left join random_exam E on E.RANDOM_EXAM_ID=ER.RANDOM_EXAM_ID 
                    left join random_exam_train_class x on E.Random_Exam_ID=x.Random_Exam_ID
                    left join Zj_Train_Class_Subject y on x.train_class_subject_id=y.train_class_subject_id
                    left join computer_room CR on CR.COMPUTER_ROOM_ID=z.Computer_Room_Id 
                    left join random_exam_modular_type MT on MT.RANDOM_EXAM_MODULAR_TYPE_ID=E.Random_Exam_Modular_Type_Id 
                    Inner join (
                    select b.*,min(a.end_time) end_time from Random_Exam_Result a
                    inner join
                   (select distinct  max(a.score) score, a.examinee_id, a.Random_Exam_Id
                    from Random_EXAM_RESULT a where  a.status_id > 0
                    group by a.examinee_id, a.Random_Exam_Id) b on a.examinee_id =b.examinee_id
                    and a.score =b.score and a.Random_Exam_Id = b.Random_Exam_Id
                    group by  b.examinee_id, b.Random_Exam_Id,b.score ) F on ER.examinee_id = f.examinee_id
                    and ER.score = f.score  and ER.Random_Exam_Id = f.Random_Exam_Id
                    and ER.end_time=f.end_time 
                    where ER.EXAMINEE_ID=" + employeeID;
            DataTable dtSel = access.RunSqlDataSet(strSql).Tables[0];
            access.ExecuteNonQuery(" delete from zj_employee_exam where  employee_id="+employeeID);
            if (dtSel != null && dtSel.Rows.Count > 0)
            {
                foreach (DataRow r in dtSel.Rows)
                {
                    string exam_date = r["Begin_Time"].ToString();
                    int exam_style = Convert.ToInt32(r["Is_Computerexam"]);
                    string exam_address = r["Computer_Room_Name"].ToString();
                    string exam_subject = r["subject"].ToString();
                    double exam_score = Convert.ToDouble(r["Score"]);
                    int exam_result = Convert.ToInt32(r["Is_Pass"]);
                    int random_exam_id = Convert.ToInt32(r["RANDOM_EXAM_ID"]);
                    InsertInfo(exam_date, exam_style, exam_address, exam_subject, exam_score, exam_result,
                               random_exam_id);
                }
                BindGrid();
            }
        }

        /// <summary>
        /// 新增信息
        /// </summary>
        private void InsertInfo(string exam_date, int exam_style, string exam_address, string exam_subject, double exam_score, int exam_result, int random_exam_id)
        {

            access = new OracleAccess();
            StringBuilder sqlInsert = new StringBuilder();
            sqlInsert.Append(
                "insert into zj_employee_exam(employee_exam_id,employee_id,exam_date,exam_style,exam_address,");
            sqlInsert.Append("exam_subject,exam_score ,exam_result,create_date,create_person,random_exam_id");
            sqlInsert.Append(")  values(employee_exam_seq.nextval,{0},to_date('{1}','yyyy-mm-dd hh24:mi:ss'),{2},");
            sqlInsert.Append(" '{3}','{4}','{5}',{6},to_date('{7}','yyyy-mm-dd hh24:mi:ss'),'{8}',{9})");
            string sqlIns = string.Format(sqlInsert.ToString(), employeeID, exam_date, exam_style, exam_address,
                                          exam_subject, exam_score, exam_result, createDate, createPerson,
                                          random_exam_id);
            try
            {

                if (random_exam_id > 0)
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
