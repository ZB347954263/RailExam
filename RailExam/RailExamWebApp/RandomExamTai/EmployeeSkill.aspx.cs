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
using System.Text;

namespace RailExamWebApp.RandomExamTai
{
    public partial class EmployeeSkill : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hfID.Value = Request.QueryString.Get("ID");
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
			ClientScript.RegisterClientScriptBlock(GetType(), "", "window.parent.frames[0].location=window.parent.frames[0].location;", true);
        }
        private void BindGrid()
        {
            grdEntity.DataSource = GetInfo();
            grdEntity.DataBind();
        }
        private DataTable GetInfo()
        {
           
            DataTable dt = new DataTable();
            OracleAccess access = new OracleAccess();
            StringBuilder sql = new StringBuilder();
			sql.Append(" select s.*,t.type_name oname,tt.type_name nname,osf.safe_level_name oldSafe,nsf.safe_level_name newSafe from zj_employee_skill s  ");
            sql.Append("  left join technician_type t on s.old_level_id=t.technician_type_id ");
            sql.Append(" left join technician_type tt on tt.technician_type_id=s.now_level_id ");
        	sql.Append(" left join zj_safe_level osf on osf.safe_level_id=s.old_safe_level_id ");
			sql.Append(" left join zj_safe_level nsf on nsf.safe_level_id=s.new_safe_level_id ");
			sql.AppendFormat(" where s.employee_id={0} order by employee_skill_id desc", Convert.ToInt32(Request.QueryString.Get("ID")));
            dt = access.RunSqlDataSet(sql.ToString()).Tables[0];
            dt.Columns.Add("appoint_time1", typeof(string));
            dt.Columns.Add("qualification_time1", typeof(string));
            dt.Columns.Add("create_date1", typeof(string));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    if (r["appoint_time"].ToString() != "")
                        r["appoint_time1"] = Convert.ToDateTime(r["appoint_time"].ToString()).ToString("yyyy-MM-dd");
                    if (r["qualification_time"].ToString() != "")
                        r["qualification_time1"] =
                            Convert.ToDateTime(r["qualification_time"].ToString()).ToString("yyyy-MM-dd");
                    r["create_date1"] =   Convert.ToDateTime(r["create_date"].ToString()).ToString("yyyy-MM-dd");
                }
            }
            return dt;
        }
        private void DeleteInfo()
        {
            try
            {
                string id = Request.Form["__EVENTARGUMENT"];
                string sql = string.Format("delete from zj_employee_skill where employee_skill_id={0}", Convert.ToInt32(id));
                OracleAccess access = new OracleAccess();
            	UpdateEmployeeInfo(Request.QueryString.Get("ID"));     //更新主表信息
				access.ExecuteNonQuery(sql);                            //删除基本信息
				ClientScript.RegisterClientScriptBlock(GetType(), "", "window.parent.frames[0].location=window.parent.frames[0].location;", true);
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据删除成功！')", true);
            }
            catch
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('数据删除失败！')", true);
            }
        }
		/// <summary>
		/// 更新主表信息
		/// </summary>
		/// <param name="empID"></param>
		private void UpdateEmployeeInfo(string empID)
		{
			OracleAccess access = new OracleAccess();
			try
			{
				string skillID = Request.Form["__EVENTARGUMENT"];    //要删除的技能动态ID
				string maxskillID = "";
				string sql = "select max(employee_skill_id) from zj_employee_skill  where employee_id=" + empID;
				DataTable dtMax = access.RunSqlDataSet(sql).Tables[0];
				if (dtMax.Rows.Count > 0)
				{
					maxskillID = dtMax.Rows[0][0].ToString();    //最新的学习动态ID
				}
				if (skillID.Equals(maxskillID))
				{
					//删除的是最新的动态
					sql = "select * from   zj_employee_skill where employee_skill_id=" + maxskillID;
					DataTable dtNewInfo = access.RunSqlDataSet(sql).Tables[0];
					if (dtNewInfo.Rows.Count > 0)
					{
						//还原之前的动态
						string oldSkillID = dtNewInfo.Rows[0]["old_level_id"].ToString();
						string oldSafeID = dtNewInfo.Rows[0]["old_safe_level_id"].ToString();
						sql = string.Format("update employee set TECHNICIAN_TYPE_ID={0},safe_level_id={1} where employee_id={2}",
							oldSkillID == "" ? "0" : oldSkillID, oldSafeID == "" ? "null" : oldSafeID, empID);
						access.ExecuteNonQuery(sql);
					}
				}
			}
			catch
			{
			}
		}
    }
}
