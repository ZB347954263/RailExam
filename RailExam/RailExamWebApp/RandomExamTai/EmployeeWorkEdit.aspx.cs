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
    public partial class EmployeeWorkEdit : PageBase
    {
    	//private static int oldOrgID = 0;
    	//private static int oldPostID = 0;
    	//private static int empID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString.Get("mode").Equals("edit"))
                    bindInfo();
				if (Request.QueryString.Get("mode").EndsWith("add"))
					GetOldInfoByEmployeeID();

                hfLoginOrgID.Value = PrjPub.CurrentLoginUser.StationOrgID.ToString();
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
				string oldPostDate = access.RunSqlDataSet("select POST_DATE from employee where employee_id="+Request.QueryString.Get("ID")).Tables[0].Rows[0][0].ToString();
				string sql =
					string.Format(
						@" insert into zj_employee_work(EMPLOYEE_WORK_ID,EMPLOYEE_ID,TRANSFER_DATE,OLD_ORG_ID,NEW_ORG_ID,OLD_POST_ID,
                          NEW_POST_ID,MOBILIZATIONORDERNO,CREATE_DATE,CREATE_PERSON,old_post_date 
                          ) values (employee_work_seq.nextval,{0},'{1}',{2},{3},{4},{5},'{6}',to_date('{7}','yyyy-mm-dd hh24:mi:ss'),'{8}',to_date('{9}','yyyy-mm-dd hh24:mi:ss'))",
						Convert.ToInt32(Request.QueryString.Get("id")), txtTransfer.DateValue.ToString(), oldOrgID.Value==""?0:Convert.ToInt32(oldOrgID.Value),
						Convert.ToInt32(hfOrgID.Value), oldPostID.Value==""?0:Convert.ToInt32(oldPostID.Value), Convert.ToInt32(hfPostID.Value),
						txtmobilizationorderno.Text, Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("yyyy-MM-dd"),
						PrjPub.CurrentLoginUser.EmployeeName, oldPostDate);
				access.ExecuteNonQuery(sql);       //基本信息
			    UpdateEmployeeInfo(Request.QueryString.Get("id"));   //更新主表信息
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
				//string oldPostDate = new OracleAccess().RunSqlDataSet("select POST_DATE from employee where employee_id=" + empID).Tables[0].Rows[0][0].ToString();
            	string sql =
            		string.Format(
            			@"update zj_employee_work set transfer_date='{0}',old_org_id={1},new_org_id={2},old_post_id={3},
                       new_post_id={4},mobilizationorderno='{5}',create_date=to_date('{6}','yyyy-mm-dd hh24:mi:ss'),create_person='{7}' where employee_work_id={8}",
            			txtTransfer.DateValue, oldOrgID.Value==""?0:Convert.ToInt32(oldOrgID.Value), hfOrgID.Value, oldPostID.Value==""?0:Convert.ToInt32(oldPostID.Value), hfPostID.Value, txtmobilizationorderno.Text,
            			Convert.ToDateTime(DateTime.Now.ToShortDateString()).ToString("yyyy-MM-dd"),
						PrjPub.CurrentLoginUser.EmployeeName, Convert.ToInt32(Request.QueryString.Get("id")));
				OracleAccess access = new OracleAccess();
				access.ExecuteNonQuery(sql);                         //基本信息
				if (GetNewWorkID() == Request.QueryString.Get("id"))     //如果修改的是最新的动态
					UpdateEmployeeInfo(empID.Value);               //更新主表信息
				ClientScript.RegisterClientScriptBlock(GetType(), "", "window.returnValue=true;window.close();", true);
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据更新失败！');", true);
            }
        }
        private void bindInfo()
        {
            OracleAccess access=new OracleAccess();
			string sql = string.Format(@"select w.*,op.post_name opost_name,np.post_name npost_name,getorgname(w.old_org_id) oorg,getorgname(w.new_org_id) norg
					   from zj_employee_work w left join post op on op.post_id=w.old_post_id 
					   left join post np on np.post_id=w.new_post_id where employee_work_id={0}",
                Convert.ToInt32(Request.QueryString.Get("id")));
            DataTable dt = access.RunSqlDataSet(sql).Tables[0];
            if(dt!=null)
            {
				if (dt.Rows.Count > 0)
				{
					oldOrgID.Value =dt.Rows[0]["old_org_id"].ToString();
					txtOldOrg.Text = dt.Rows[0]["oorg"].ToString();
					oldPostID.Value = dt.Rows[0]["old_post_id"].ToString();
					txtOldPost.Text = dt.Rows[0]["opost_name"].ToString();
					hfOrgID.Value = dt.Rows[0]["new_org_id"].ToString();
					txtNewOrg.Text = dt.Rows[0]["norg"].ToString();
					hfPostID.Value = dt.Rows[0]["new_post_id"].ToString();
					txtNewPost.Text = dt.Rows[0]["npost_name"].ToString();
					txtmobilizationorderno.Text = dt.Rows[0]["mobilizationorderno"].ToString();
					if (dt.Rows[0]["transfer_date"].ToString().Trim() != "")
						txtTransfer.DateValue = Convert.ToDateTime(dt.Rows[0]["transfer_date"]).ToString("yyyy-MM-dd");
					empID.Value =dt.Rows[0]["employee_id"].ToString();
				}
            }
            this.Title = "修改工作动态";
            current.InnerText = "修改工作动态";
        }

		/// <summary>
		/// 获取之前的信息
		/// </summary>
		private void GetOldInfoByEmployeeID()
		{
			string sql = @"  select e.employee_id,e.org_id,e.post_id,getorgname(e.org_id) oorg,getpostname(e.post_id),p.post_name opost_name from employee e
                      left join post p on p.post_id=e.post_id
                      where e.employee_id=" + Request.QueryString.Get("id");
			OracleAccess access = new OracleAccess();
			DataTable dt = access.RunSqlDataSet(sql).Tables[0];
			if (dt != null)
			{
				if (dt.Rows.Count > 0)
				{
					txtOldOrg.Text= dt.Rows[0]["oorg"].ToString();
					txtOldPost.Text = dt.Rows[0]["opost_name"].ToString();
					oldOrgID.Value = dt.Rows[0]["org_id"].ToString();
					oldPostID.Value = dt.Rows[0]["post_id"].ToString();
				}
			}
		}

		/// <summary>
		/// 获取该员工最新的工作动态ID
		/// </summary>
		/// <returns></returns>
		private string GetNewWorkID()
		{
			string sql = @"
                select max(employee_work_id) from zj_employee_work where employee_id=
              (select distinct employee_id from zj_employee_work where employee_work_id=" + Request.QueryString.Get("id") + ")";
			OracleAccess access = new OracleAccess();
			DataTable dt = access.RunSqlDataSet(sql).Tables[0];
			if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "")
			{
				return dt.Rows[0][0].ToString();
			}
			else
			{
				return "";
			}
		}


		/// <summary>
		/// 更新主表信息
		/// </summary>
		private void UpdateEmployeeInfo(string strempID)
		{
			try
			{
				string sql = string.Format(@"
                   update employee set ORG_ID={0} ,POST_ID={1},POST_DATE=to_date('{3}','yyyy-mm-dd hh24:mi:ss') where  EMPLOYEE_ID={2} 
                  ", hfOrgID.Value, hfPostID.Value, strempID, txtTransfer.DateValue);
				OracleAccess access = new OracleAccess();
				access.ExecuteNonQuery(sql);
			}
			catch  
			{
			}
		}
    }
}
