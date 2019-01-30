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
using DSunSoft.Web.UI;
using System.Text;

namespace RailExamWebApp.RandomExamTai
{
    public partial class EmployeeEducationEdit :PageBase
    {
    	//private static string empID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindLevel();
				if (Request.QueryString.Get("mode").Equals("edit"))
					bindInfo();
				else
					GetOldInfo();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Request.QueryString.Get("mode").Equals("edit"))
                editInfo();
            else
                addInfo();
        }
        private void addInfo()
        {
            try
            {
                OracleAccess access = new OracleAccess();
                string sql =
                    string.Format(
                        " insert into zj_employee_education values (employee_education_seq.nextval,{0},to_date('{1}','yyyy-mm-dd hh24:mi:ss'),{2},{3},'{4}','{5}','{6}','{7}',{8},to_date('{9}','yyyy-mm-dd hh24:mi:ss'),'{10}')",
                        Convert.ToInt32(Request.QueryString.Get("id")), graduate_date.DateValue.ToString(),
                        Convert.ToInt32(droponame.SelectedValue), Convert.ToInt32(dropnname.SelectedValue),
                        txtsubject.Text, txtstyle.Text,
                        txtdiplomano.Text, txtgraducateschool.Text,Convert.ToInt32(dropSchoolCategory.SelectedValue),
                        Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("yyyy-MM-dd"),
                        PrjPub.CurrentLoginUser.EmployeeName);
                access.ExecuteNonQuery(sql);         //新增信息
                UpdateEmployeeInfo(Request.QueryString.Get("ID"), txtsubject.Text, txtgraducateschool.Text, graduate_date.DateValue.ToString());               //修改基本信息
                ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue=true;window.close();", true);
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据保存失败！');", true);
            }

        }
        private void editInfo()
        {
            try
            {
                OracleAccess access = new OracleAccess();
                string sql =
                    string.Format(
                        "update zj_employee_education set graduate_date=to_date('{0}','yyyy-mm-dd hh24:mi:ss'),old_education_id={1},now_education_id={2},school_subject='{3}',study_style='{4}',diploma_no='{5}',graducate_school='{6}',school_type={7},create_date=to_date('{8}','yyyy-mm-dd hh24:mi:ss'),create_person='{9}' where  employee_education_id={10}",
                        graduate_date.DateValue.ToString(), Convert.ToInt32(droponame.SelectedValue),
                        Convert.ToInt32(dropnname.SelectedValue), txtsubject.Text,
                        txtstyle.Text, txtdiplomano.Text,
                        txtgraducateschool.Text, Convert.ToInt32(dropSchoolCategory.SelectedValue),
                        Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("yyyy-MM-dd"),
                        PrjPub.CurrentLoginUser.EmployeeName, Convert.ToInt32(Request.QueryString.Get("id")));
                access.ExecuteNonQuery(sql);                           //修改信息
				if (GetNewEduID() == Request.QueryString.Get("id"))    //修改的是最新的教育动态
                    UpdateEmployeeInfo(empID.Value, txtsubject.Text, txtgraducateschool.Text, graduate_date.DateValue.ToString());             //修改基本信息
                ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue=true;window.close();", true);
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据更新失败！');", true);
            }
        }
        private void bindInfo()
        {
             OracleAccess access = new OracleAccess();
             StringBuilder sql = new StringBuilder();
             sql.Append("select e.*,ol.education_level_name oname,wl.education_level_name nname from zj_employee_education e left join education_level ol ");
             sql.Append(" on e.old_education_id=ol.education_level_id left join education_level wl ");
             sql.Append(" on e.now_education_id=wl.education_level_id ");
			 sql.AppendFormat(" where employee_education_id={0} order by EMPLOYEE_EDUCATION_ID desc", Convert.ToInt32(Request.QueryString.Get("ID")));
             DataTable dt = access.RunSqlDataSet(sql.ToString()).Tables[0];
             if (dt != null && dt.Rows.Count > 0)
             {
                 droponame.SelectedValue = dt.Rows[0]["old_education_id"].ToString();
                 dropnname.SelectedValue = dt.Rows[0]["now_education_id"].ToString();
                 txtsubject.Text = dt.Rows[0]["school_subject"].ToString();
                 txtstyle.Text = dt.Rows[0]["study_style"].ToString();
                 txtdiplomano.Text = dt.Rows[0]["diploma_no"].ToString();
                 txtgraducateschool.Text = dt.Rows[0]["graducate_school"].ToString();
                 dropSchoolCategory.SelectedValue = dt.Rows[0]["school_type"].ToString();
                 graduate_date.DateValue = Convert.ToDateTime(dt.Rows[0]["graduate_date"]).ToString("yyyy-MM-dd");
				 empID.Value = dt.Rows[0]["employee_id"].ToString();
			 }
             this.Title = "修改学习动态";
             current.InnerText = "修改学习动态";
        }
        private void bindLevel()
        {
            string sql = "select education_level_id,education_level_name from education_level order by order_index";
            OracleAccess access=new OracleAccess();
            DataTable dt = access.RunSqlDataSet(sql).Tables[0];

			droponame.Items.Add(new ListItem("--请选择--", "0"));
			droponame.SelectedValue = "0";
			dropnname.Items.Add(new ListItem("--请选择--", "0"));
			dropnname.SelectedValue = "0";
        	foreach (DataRow r in dt.Rows)
        	{
        		droponame.Items.Add(new ListItem(r["education_level_name"].ToString(), r["education_level_id"].ToString()));
				dropnname.Items.Add(new ListItem(r["education_level_name"].ToString(), r["education_level_id"].ToString()));
        	}
        }

		/// <summary>
		/// 获取原来的学历
		/// </summary>
		private void GetOldInfo()
		{
			string sql = "select EDUCATION_LEVEL_ID from employee where employee_id="+Request.QueryString.Get("ID");
			OracleAccess access=new OracleAccess();
			DataTable dt= access.RunSqlDataSet(sql).Tables[0];
			if(dt.Rows.Count>0)
			{
				droponame.SelectedValue = dt.Rows[0][0].ToString();
			}
		}

		/// <summary>
		/// 获取该员工最新的学历ID
		/// </summary>
		/// <returns></returns>
		private string GetNewEduID()
		{
			string sql = @"
               select max(employee_education_id) from zj_employee_education where employee_id=
         (select distinct employee_id from zj_employee_education where employee_education_id=" + Request.QueryString.Get("id") + ")";
			OracleAccess access = new OracleAccess();
			DataTable dt = access.RunSqlDataSet(sql).Tables[0];
			if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString()!="")
			{
				return dt.Rows[0][0].ToString();
			}
			else
			{
				return "";
			}
		}

		//private string GetEmpIDByEduID()
		//{
		//    string sql = " select employee_id from zj_employee_education where employee_education_id=" +
		//                 Request.QueryString.Get("id");
		//    DataTable dt = new OracleAccess().RunSqlDataSet(sql).Tables[0];
		//    if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "")
		//        return dt.Rows[0][0].ToString();
		//    else
		//        return "";
		//}


    	/// <summary>
		/// 修改基本信息
		/// </summary>
		private void UpdateEmployeeInfo(string empID,string subject,string school,string graduatedate)
		{
			try
			{
				string sql = "update employee set EDUCATION_LEVEL_ID=" + dropnname.SelectedValue +","
                    +" Study_Major='"+ subject +"',"
                    + " Graduate_University='" + school + "',"
                    + " Graduate_Date=to_date('" + graduatedate + "','yyyy-mm-dd hh24:mi:ss') "
                    + " where employee_id=" +
				 empID;
				OracleAccess access = new OracleAccess();
				access.ExecuteNonQuery(sql);
			}
			catch 
			{
			}
		}
    }
}
