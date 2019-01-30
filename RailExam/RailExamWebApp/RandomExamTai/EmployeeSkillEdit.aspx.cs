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
using RailExamWebApp.Common.Class;
using System.Text;

namespace RailExamWebApp.RandomExamTai
{
    public partial class EmployeeSkillEdit : System.Web.UI.Page
    {
    	//private static string empID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLevel();
            	BindSafe();
            	GetOldSkill();
            	GetOldInfo();
                if (Request.QueryString.Get("mode").Equals("edit"))
                    BindInfo();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Request.QueryString.Get("mode").Equals("edit"))
                EditInfo();
            else
                AddInfo();
        }
        private void AddInfo()
        {
            try
            {
                OracleAccess access = new OracleAccess();
            	string sql =
            		string.Format(
            			@" insert into zj_employee_skill(employee_skill_id,employee_id,appoint_time,qualification_time,old_level_id,now_level_id,
                          certificate_no,appoint_order_no,create_date,create_person,old_safe_level_id,new_safe_level_id) 
                           values(employee_skill_seq.nextval,{0},to_date('{1}','yyyy-mm-dd hh24:mi:ss'),
                         to_date('{2}','yyyy-mm-dd hh24:mi:ss'),{3},{4},'{5}','{6}',to_date('{7}','yyyy-mm-dd hh24:mi:ss'),'{8}',{9},{10})",
            			Convert.ToInt32(Request.QueryString.Get("id")), txtappoint_time.DateValue.ToString(),
            			qualification_time.DateValue.ToString(),
            			Convert.ToInt32(droponame.SelectedValue), Convert.ToInt32(dropnname.SelectedValue),
            			txtcertificate_no.Text, txtappoint_order_no.Text,
            			Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("yyyy-MM-dd"),
            			PrjPub.CurrentLoginUser.EmployeeName, dropOldSafe.SelectedValue == "0"
            			                                      	? "null"
            			                                      	: dropOldSafe.SelectedValue,
            			dropNewSafe.SelectedValue == "0" ? "null" : dropNewSafe.SelectedValue);
                access.ExecuteNonQuery(sql);        //新增信息
            	UpdateEmployeeInfo(Request.QueryString.Get("ID"));              //修改主表信息
                ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue=true;window.close();", true);
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据保存失败！');", true);
            }

        }
        private void EditInfo()
        {
            try
            {
                OracleAccess access = new OracleAccess();
                StringBuilder sql=new StringBuilder();
                sql.Append(" update zj_employee_skill set appoint_time=to_date('{0}','yyyy-mm-dd hh24:mi:ss'),");
                sql.Append(" qualification_time=to_date('{1}','yyyy-mm-dd hh24:mi:ss'),");
                sql.Append(" old_level_id={2},now_level_id={3},certificate_no='{4}',appoint_order_no='{5}',");
                sql.Append(" create_date=to_date('{6}','yyyy-mm-dd hh24:mi:ss'),create_person='{7}',");
				sql.Append(" old_safe_level_id={9},new_safe_level_id={10} where employee_skill_id={8}");
            	string sqlEdit = string.Format(sql.ToString(), txtappoint_time.DateValue.ToString().Trim(),
            	                               qualification_time.DateValue.ToString().Trim(),
            	                               Convert.ToInt32(droponame.SelectedValue),
            	                               Convert.ToInt32(dropnname.SelectedValue),
            	                               txtcertificate_no.Text, txtappoint_order_no.Text,
            	                               Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString(
            	                               	"yyyy-MM-dd"),
            	                               PrjPub.CurrentLoginUser.EmployeeName,
            	                               Convert.ToInt32(Request.QueryString.Get("id")),
            	                               dropOldSafe.SelectedValue == "0"
            	                               	? "null"
            	                               	: dropOldSafe.SelectedValue,
            	                               dropNewSafe.SelectedValue == "0" ? "null" : dropNewSafe.SelectedValue);
              access.ExecuteNonQuery(sqlEdit);
			  if (GetNewSkillID() == Request.QueryString.Get("id"))      //如果修改的是最新的技能
				  UpdateEmployeeInfo(empID.Value);                                 //修改主表信息
              ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue=true;window.close();", true);
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据更新失败！');", true);
            }
        }
        private void BindInfo()
        {
            OracleAccess access = new OracleAccess();
            StringBuilder sql = new StringBuilder();
			sql.Append(" select s.*,t.type_name oname,tt.type_name nname,osf.safe_level_name oldSafe,nsf.safe_level_name newSafe from zj_employee_skill s  ");
            sql.Append("  left join technician_type t on s.old_level_id=t.technician_type_id ");
            sql.Append(" left join technician_type tt on tt.technician_type_id=s.employee_skill_id ");
			sql.Append(" left join zj_safe_level osf on osf.safe_level_id=s.old_safe_level_id ");
			sql.Append(" left join zj_safe_level nsf on nsf.safe_level_id=s.new_safe_level_id ");
			sql.AppendFormat(" where s.employee_skill_id={0} order by employee_skill_id desc", Convert.ToInt32(Request.QueryString.Get("ID")));
            DataTable dt = access.RunSqlDataSet(sql.ToString()).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
				droponame.SelectedValue = dt.Rows[0]["old_level_id"].ToString() == "" ? "1" : dt.Rows[0]["old_level_id"].ToString();
                dropnname.SelectedValue = dt.Rows[0]["now_level_id"].ToString();
                txtcertificate_no.Text = dt.Rows[0]["certificate_no"].ToString();
                txtappoint_order_no.Text = dt.Rows[0]["appoint_order_no"].ToString();
                if (dt.Rows[0]["appoint_time"].ToString() != "")
                    txtappoint_time.DateValue = Convert.ToDateTime(dt.Rows[0]["appoint_time"]).ToString("yyyy-MM-dd");
                else
                    txtappoint_time.DateValue = "";
                if (dt.Rows[0]["qualification_time"].ToString() != "")
                    qualification_time.DateValue = Convert.ToDateTime(dt.Rows[0]["qualification_time"]).ToString("yyyy-MM-dd");
                else
                    qualification_time.DateValue = "";
				if (dt.Rows[0]["old_safe_level_id"].ToString() == "")
					dropOldSafe.SelectedValue = "0";
				dropOldSafe.SelectedValue = dt.Rows[0]["old_safe_level_id"].ToString();
				if (dt.Rows[0]["new_safe_level_id"].ToString() == "")
					dropNewSafe.SelectedValue = "0";
				dropNewSafe.SelectedValue = dt.Rows[0]["new_safe_level_id"].ToString();
				empID.Value = dt.Rows[0]["employee_id"].ToString();
            }
            this.Title = "修改技能动态";
            current.InnerText = "修改技能动态";
        }
        private void BindLevel()
        {
            string sql = "select * from technician_type order by technician_type_id";
            OracleAccess access = new OracleAccess();
            DataTable dt = access.RunSqlDataSet(sql).Tables[0];
			droponame.Items.Add(new ListItem("--请选择--", "0"));
			droponame.SelectedValue = "0";
			dropnname.Items.Add(new ListItem("--请选择--", "0"));
			dropnname.SelectedValue = "0";

        	foreach (DataRow r in dt.Rows)
        	{
				droponame.Items.Add(new ListItem(r["type_name"].ToString(), r["technician_type_id"].ToString()));
				dropnname.Items.Add(new ListItem(r["type_name"].ToString(), r["technician_type_id"].ToString()));
        	}
        }

		private void BindSafe()
		{
			string sql = "select safe_level_id,safe_level_name from zj_safe_level order by order_index";
			OracleAccess access = new OracleAccess();
			DataTable dt = access.RunSqlDataSet(sql).Tables[0];
			dropOldSafe.Items.Add(new ListItem("--请选择--", "0"));
			dropNewSafe.Items.Add(new ListItem("--请选择--", "0"));
			foreach (DataRow r in dt.Rows)
			{
				dropOldSafe.Items.Add(new ListItem(r["safe_level_name"].ToString(), r["safe_level_id"].ToString()));
				dropNewSafe.Items.Add(new ListItem(r["safe_level_name"].ToString(), r["safe_level_id"].ToString()));
			}
		}

		/// <summary>
		/// 获取原来的安全等级
		/// </summary>
		private void GetOldInfo()
		{
			string sql = "select SAFE_LEVEL_ID from employee where employee_id=" + Request.QueryString.Get("ID");
			OracleAccess access = new OracleAccess();
			DataTable dt = access.RunSqlDataSet(sql).Tables[0];
			if (dt.Rows.Count > 0)
			{
				dropOldSafe.SelectedValue = dt.Rows[0][0].ToString();
			}
		}

		/// <summary>
		/// 获取原来的技能等级
		/// </summary>
		private void GetOldSkill()
		{
			string sql = "select TECHNICIAN_TYPE_ID from employee where employee_id="+Request.QueryString.Get("ID");
			DataTable dt = new OracleAccess().RunSqlDataSet(sql).Tables[0];
			if (dt.Rows.Count > 0)
			{
				droponame.SelectedValue = dt.Rows[0][0].ToString() == "" ? "1" : dt.Rows[0][0].ToString();
				droponame.SelectedValue = dt.Rows[0][0].ToString() == "-1" ? "1" : dt.Rows[0][0].ToString();
			}
		}

    	/// <summary>
		/// 获取该员工最新的技能ID
		/// </summary>
		/// <returns></returns>
		private  string GetNewSkillID()
		{
			string sql = @"
                select max(employee_skill_id) from zj_employee_skill where employee_id=
              (select distinct employee_id from zj_employee_skill where employee_skill_id="+Request.QueryString.Get("id")+")";
			OracleAccess access=new OracleAccess();
			DataTable dt = access.RunSqlDataSet(sql).Tables[0];
			if(dt.Rows.Count>0 && dt.Rows[0][0].ToString()!="")
			{
				return dt.Rows[0][0].ToString();
			}
			else
			{
				return "";
			}
		}

		//private string GetEmpIDBySkillID()
		//{
		//    string sql = " select   employee_id from  zj_employee_skill where employee_skill_id=" + Request.QueryString.Get("id");
		//    DataTable dt = new OracleAccess().RunSqlDataSet(sql).Tables[0];
		//    if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "")
		//        return dt.Rows[0][0].ToString();
		//    else
		//        return "";
		//}

    	/// <summary>
		/// 修改基本信息
		/// </summary>
		private void UpdateEmployeeInfo(string empID)
		{
			try
			{
				string sql = "update employee set SAFE_LEVEL_ID=" 
                    + dropNewSafe.SelectedValue + ",TECHNICIAN_TYPE_ID="
                    + dropnname.SelectedValue + ",TECHNICAL_DATE=to_date('"
                    + qualification_time.DateValue.ToString().Trim()+"','yyyy-MM-dd') where employee_id=" 
                    + empID;
				OracleAccess access = new OracleAccess();
				access.ExecuteNonQuery(sql);
			}
			catch
			{
			}
		}
    }
}
