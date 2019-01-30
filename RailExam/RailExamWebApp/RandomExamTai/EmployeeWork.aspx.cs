using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;
using DSunSoft.Web.Global;

namespace RailExamWebApp.RandomExamTai
{
    public partial class EmployeeWork :PageBase
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
                    int columnsCount=this.grdEntity.Levels[0].Columns.Count;
                    this.grdEntity.Levels[0].Columns[columnsCount - 1].Visible = false;
                }
            }
            if(PrjPub.CurrentLoginUser != null)
            {
                hfStationID.Value = PrjPub.CurrentLoginUser.StationOrgID.ToString(); ;
            }
            else
            {
                hfStationID.Value = PrjPub.CurrentStudent.StationOrgID.ToString(); ;
            }
            hfIsServerCenter.Value = PrjPub.IsServerCenter.ToString();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string mode = Request.Form["__EVENTARGUMENT"];
            if (mode != "ref")
                DeleteInfo();
            BindGrid();
			ClientScript.RegisterClientScriptBlock(GetType(),"","window.parent.frames[0].location=window.parent.frames[0].location",true);
        }
        private void BindGrid()
        {
            grdEntity.DataSource = GetInfo();
            grdEntity.DataBind();
        }
        private DataTable GetInfo()
       {
           DataTable dt=new DataTable();
           OracleAccess access=new OracleAccess();
           string sql =
               string.Format(
                   @" select w.*,getorgname(w.old_org_id) oorg,getorgname(w.new_org_id) norg,op.post_name opost_name,np.post_name npost_name, getstationorgid(w.old_org_id) oldStationID
                      from zj_employee_work w 
                      left join post op on op.post_id=w.old_post_id
                      left join post np on np.post_id=w.new_post_id where w.employee_id={0} order by w.employee_work_id desc",
            Convert.ToInt32(Request.QueryString.Get("ID")));
           dt = access.RunSqlDataSet(sql).Tables[0];
           return dt;
       }
        private void DeleteInfo()
        {
            try
            {
                string id = Request.Form["__EVENTARGUMENT"];
                OracleAccess access = new OracleAccess();
                string sql =
                    string.Format("select * from zj_employee_work where employee_id={0} and employee_work_id>{1}",
                                  Request.QueryString.Get("ID"), id);
                DataSet ds = access.RunSqlDataSet(sql);
                if(ds.Tables[0].Rows.Count>0)
                {
                    SessionSet.PageMessage = "请按照工作动态先后顺序，从后向前删除数据！";
                    return;
                }
                    
                sql= string.Format("delete from zj_employee_work where employee_work_id={0}", Convert.ToInt32(id));
                
            	UpdateEmployeeInfo(Request.QueryString.Get("ID"));     //更新主表信息
				access.ExecuteNonQuery(sql);
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
			OracleAccess access=new OracleAccess();
			try
			{
				string workID = Request.Form["__EVENTARGUMENT"];    //要删除的工作动态ID
				string maxWorkID = "";
				string sql = "select max(employee_work_id) from zj_employee_work  where employee_id=" + empID;
				DataTable dtMax = access.RunSqlDataSet(sql).Tables[0];
				if (dtMax.Rows.Count > 0)
				{
					maxWorkID = dtMax.Rows[0][0].ToString();    //最新的工作动态ID
				}
				if (workID.Equals(maxWorkID))
				{
					//删除的是最新的动态
					sql = "select * from   zj_employee_work where employee_work_id=" + maxWorkID;
					DataTable dtNewInfo = access.RunSqlDataSet(sql).Tables[0];
					if (dtNewInfo.Rows.Count > 0)
					{
						//还原之前的动态
						string orgID = dtNewInfo.Rows[0]["OLD_ORG_ID"].ToString();
						string postID = dtNewInfo.Rows[0]["OLD_POST_ID"].ToString();
						string date = dtNewInfo.Rows[0]["old_post_date"].ToString();
						sql = string.Format(@"
                   update employee set ORG_ID={0} ,POST_ID={1},POST_DATE=to_date('{3}','yyyy-mm-dd hh24:mi:ss') where  EMPLOYEE_ID={2} 
                  ", orgID, postID, empID, date);
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
