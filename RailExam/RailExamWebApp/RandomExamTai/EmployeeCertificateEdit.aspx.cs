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
	public partial class EmployeeCertificateEdit : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				LoadCer();
				if (Request.QueryString.Get("mode") == "edit")
					LoadInfo();
			}
		}

		private void LoadInfo()
		{
			OracleAccess access=new OracleAccess();
			int id = Request.QueryString.Get("id") == "" ? 0 : Convert.ToInt32(Request.QueryString.Get("id"));
			string sql = string.Format(@" select * 
                                       from zj_employee_certificate   where employee_certificate_id={0}", id);
			DataTable dt = access.RunSqlDataSet(sql).Tables[0];
			if(dt!=null && dt.Rows.Count>0)
			{
				drop_certificate.SelectedValue = dt.Rows[0]["certificate_id"].ToString();
				drop_certificate_Level.SelectedValue = dt.Rows[0]["certificate_level_id"].ToString();
				drop_train_unit.SelectedValue = dt.Rows[0]["train_unit_id"].ToString();
				drop_certificate_unit.SelectedValue = dt.Rows[0]["certificate_unit_id"].ToString();
				txtcertificate_num.Text = dt.Rows[0]["certificate_no"].ToString();
				check_date.DateValue = dt.Rows[0]["check_date"].ToString() == ""
				                       	? ""
				                       	: Convert.ToDateTime(dt.Rows[0]["check_date"]).ToString("yyyy-MM-dd");
				txtcheck_unit.Text = dt.Rows[0]["check_unit"].ToString();
				check_result.Text = dt.Rows[0]["check_result"].ToString();
				begin_date.DateValue =Convert.ToDateTime(dt.Rows[0]["begin_date"]).ToString("yyyy-MM-dd");
				end_date.DateValue = Convert.ToDateTime(dt.Rows[0]["end_date"]).ToString("yyyy-MM-dd");
				txtmemo.Text = dt.Rows[0]["memo"].ToString();
				certificate_date.DateValue = Convert.ToDateTime(dt.Rows[0]["certificate_date"]).ToString("yyyy-MM-dd");
				txtcheck_cycle.Text = dt.Rows[0]["check_cycle"].ToString();
			}

		}

		private void LoadCer()
		{
			OracleAccess access=new OracleAccess();

			drop_certificate.DataSource = access.RunSqlDataSet(" select * from zj_certificate order by order_index");
			drop_certificate.DataValueField = "certificate_id";
			drop_certificate.DataTextField = "certificate_name";
			drop_certificate.DataBind();

			string sql = string.Format(" select * from zj_certificate_level  where certificate_id={0}  order by order_index",
			                           drop_certificate.SelectedValue == "" ? 0 : Convert.ToInt32(drop_certificate.SelectedValue));
			drop_certificate_Level.DataSource = access.RunSqlDataSet(sql);
			drop_certificate_Level.DataValueField = "certificate_level_id";
			drop_certificate_Level.DataTextField = "certificate_level_name";
			drop_certificate_Level.DataBind();

			drop_certificate_unit.DataSource = access.RunSqlDataSet(" select * from zj_certificate_unit order by order_index");
			drop_certificate_unit.DataValueField = "certificate_unit_id";
			drop_certificate_unit.DataTextField = "certificate_unit_name";
			drop_certificate_unit.DataBind();

			drop_train_unit.DataSource = access.RunSqlDataSet(" select * from zj_train_unit order by order_index");
			drop_train_unit.DataValueField = "train_unit_id";
			drop_train_unit.DataTextField = "train_unit_name";
			drop_train_unit.DataBind();

		}

		private void AddInfo()
		{
			OracleAccess access=new OracleAccess(); 
			int empID = Request.QueryString.Get("id") == "" ? 0 : Convert.ToInt32(Request.QueryString.Get("id"));
			int cerID = Convert.ToInt32(drop_certificate.SelectedValue);
			int levelID = Convert.ToInt32(drop_certificate_Level.SelectedValue);
			int trainUnitID = Convert.ToInt32(drop_train_unit.SelectedValue);
			int cerUintID = Convert.ToInt32(drop_certificate_unit.SelectedValue);
			string cerNum = txtcertificate_num.Text;
			string checkDate = check_date.DateValue.ToString();
			string checkUnit = txtcheck_unit.Text;
			string checkReset = check_result.Text;
			string beginDate = begin_date.DateValue.ToString();
			string endDate = end_date.DateValue.ToString();
			string memo = txtmemo.Text;
			string cerDate = certificate_date.DateValue.ToString();
			string checkCycle = txtcheck_cycle.Text;

			string sql =string.Format(@"
                 insert into zj_employee_certificate(employee_certificate_id,employee_id,certificate_id,certificate_level_id,
				 train_unit_id,certificate_unit_id,certificate_no,check_date,check_unit,check_result,begin_date,end_date,memo,certificate_date,check_cycle
				 ) values
				 (zj_employee_certificat_seq.nextval,{0},{1},{2},{3},{4},'{5}',to_date('{6}','yyyy-mm-dd hh24:mi:ss'),
				  '{7}','{8}',to_date('{9}','yyyy-mm-dd hh24:mi:ss'),to_date('{10}','yyyy-mm-dd hh24:mi:ss'),
				  '{11}',to_date('{12}','yyyy-mm-dd hh24:mi:ss'),'{13}'
				 )
                  ", empID, cerID, levelID, trainUnitID, cerUintID, cerNum, checkDate, checkUnit, checkReset, beginDate, endDate, memo, cerDate, checkCycle);
			try
			{
				access.ExecuteNonQuery(sql);
				ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue=true;window.close();", true);
			}
			catch (Exception)
			{
				ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据保存失败！');", true);
			}
		}

		private void EditInfo()
		{
			OracleAccess access = new OracleAccess();
			int ID = Request.QueryString.Get("id") == "" ? 0 : Convert.ToInt32(Request.QueryString.Get("id"));
			int cerID = Convert.ToInt32(drop_certificate.SelectedValue);
			int levelID = Convert.ToInt32(drop_certificate_Level.SelectedValue);
			int trainUnitID = Convert.ToInt32(drop_train_unit.SelectedValue);
			int cerUintID = Convert.ToInt32(drop_certificate_unit.SelectedValue);
			string cerNum = txtcertificate_num.Text;
			string checkDate = check_date.DateValue.ToString();
			string checkUnit = txtcheck_unit.Text;
			string checkReset = check_result.Text;
			string beginDate = begin_date.DateValue.ToString();
			string endDate = end_date.DateValue.ToString();
			string memo = txtmemo.Text;
			string cerDate = certificate_date.DateValue.ToString();
			string checkCycle = txtcheck_cycle.Text;

			string sql = string.Format(@"
				    update zj_employee_certificate set 
						certificate_id={0},certificate_level_id={1},train_unit_id={2},
						certificate_unit_id={3},certificate_no='{4}',
						check_date=to_date('{5}','yyyy-mm-dd hh24:mi:ss'),
						check_unit='{6}',check_result='{7}',
						begin_date=to_date('{8}','yyyy-mm-dd hh24:mi:ss'),
						end_date=to_date('{9}','yyyy-mm-dd hh24:mi:ss'),
						memo='{10}',certificate_date=to_date('{11}','yyyy-mm-dd hh24:mi:ss'),check_cycle='{13}'
					where employee_certificate_id={12}
                  ", cerID, levelID, trainUnitID, cerUintID, cerNum, checkDate, checkUnit, checkReset, beginDate, endDate, memo, cerDate, ID, checkCycle);
			try
			{
				access.ExecuteNonQuery(sql);
				ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue=true;window.close();", true);
			}
			catch (Exception)
			{
				ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据保存失败！');", true);
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			string action = Request.QueryString.Get("mode");
			if (action == "add")
				AddInfo();
			if(action=="edit")
				EditInfo();
		}

		protected void drop_certificate_SelectedIndexChanged(object sender, EventArgs e)
		{
			OracleAccess access=new OracleAccess();
			string sql = string.Format(" select * from zj_certificate_level  where certificate_id={0}  order by order_index",
							   drop_certificate.SelectedValue == "" ? 0 : Convert.ToInt32(drop_certificate.SelectedValue));
			drop_certificate_Level.DataSource = access.RunSqlDataSet(sql);
			drop_certificate_Level.DataValueField = "certificate_level_id";
			drop_certificate_Level.DataTextField = "certificate_level_name";
			drop_certificate_Level.DataBind();
		}
	}
}
