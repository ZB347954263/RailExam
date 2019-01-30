using System;
using System.Collections.Generic;
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

namespace RailExamWebApp.Systems
{
	public partial class EmployeeTransferManage : System.Web.UI.Page
	{
		private List<string> lst = new List<string>();

		protected void Page_Load(object sender, EventArgs e)
		{
            if(!IsPostBack)
            {
                if (PrjPub.HasEditRight("职员批量调动") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }

                if (PrjPub.HasDeleteRight("职员批量调动") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }
            }

			if (hfDeleteID.Value == "")
			{
				hfSql.Value =
					String.Format(
						@"
                                    select e.employee_id,e.employee_name,e.sex,e.work_no,p.post_name,getorgname(e.org_id) getorgname from employee e
									left join post p on p.post_id=e.post_id where e.employee_id in ({0})
                                    ",
						hfEmployeeID.Value == "" ? "-1" : hfEmployeeID.Value);
			}
			string[] arr = hfEmployeeID.Value.Split(',');
			foreach (string s in arr)
			{
				lst.Add(s);
			}

			if (hfDeleteID.Value != "")
			{
				if (hfDeleteID.Value.IndexOf(",") > 0)
				{
					string[] arrID = hfDeleteID.Value.Split(',');
					foreach (string s in arrID)
					{
						lst.Remove(s);
					}
				}
				else
				{
					lst.Remove(hfDeleteID.Value);
				}
				hfEmployeeID.Value = string.Join(",", lst.ToArray());
				//grdEntity.EnableViewState = false;
				hfSql.Value =
					String.Format(
						@"
                                    select e.employee_id,e.employee_name,e.sex,e.work_no,p.post_name,getorgname(e.org_id) getorgname from employee e
									left join post p on p.post_id=e.post_id where e.employee_id in ({0})
                                    ",
						hfEmployeeID.Value == "" ? "-1" : hfEmployeeID.Value);
				grdEntity.DataBind();
				hfDeleteID.Value = "";
			}
		}

		protected void ObjectDataSource1_Selected(object sender, ObjectDataSourceStatusEventArgs e)
		{
			DataTable db = e.ReturnValue as DataTable;
			if (db.Rows.Count == 0)
			{
				DataRow row = db.NewRow();
				row["Employee_ID"] = -1;
				db.Rows.Add(row);
			}
		}

		protected void grdEntity_RowCreated(object sender, GridViewRowEventArgs e)
		{
			if (e.Row != null && e.Row.RowType == DataControlRowType.DataRow)
			{
				if (grdEntity.DataKeys[e.Row.RowIndex][0].ToString() == "-1")
					e.Row.Visible = false;
				else
					e.Row.Attributes.Add("onclick", "selectArow(this);");
			}
			if (e.Row != null && e.Row.RowType == DataControlRowType.Header)
				((CheckBox) e.Row.FindControl("chkAll")).Attributes.Add("onclick", "chkAll(this)");
		}

		protected void btnOK_Click(object sender, EventArgs e)
		{
		    string strEmployeeID = "";
            foreach (GridViewRow r in grdEntity.Rows)
            {
                CheckBox ch = r.FindControl("item") as CheckBox;
                Label lblID = r.FindControl("lblID") as Label;
                if (ch.Checked)
                {
                    strEmployeeID += strEmployeeID == string.Empty ? lblID.Text : "," + lblID.Text;
                }
            }

			if (hfOrgIDAndPostID.Value != "")
			{
				string sql = string.Format("update employee set {0} where employee_id in ({1})", hfOrgIDAndPostID.Value,
                                           strEmployeeID);
				try
				{
					OracleAccess access = new OracleAccess();
                    string[] arrID = strEmployeeID.Split(',');
					foreach (string s in arrID)
					{
						List<string> arr =GetOrgIDAndPostID(s);
						string newOrgID = arr[0];
						string newPostID = arr[1];
						if (hfOrgID.Value != "")
							newOrgID = hfOrgID.Value;
						if (hfPostID.Value != "")
							newPostID = hfPostID.Value;
					    	string strSql = "insert into ZJ_Employee_Work "
							+ " values(Employee_Work_Seq.nextval,  "
							+ s
							+ ", to_char(sysdate,'yyyy-mm-dd')," 
							+ arr[0] + "," + newOrgID + ","
							+ arr[1] + "," + newPostID+ ","
							+ "'',sysdate,'" + PrjPub.CurrentLoginUser.EmployeeName + "',"
							+ "to_date('" + arr[2] + "','YYYY-MM-DD HH24:MI:SS'))";
						access.ExecuteNonQuery(strSql);  //新增工作动态

						access.ExecuteNonQuery(
							"update employee set post_date=to_date(to_char(sysdate,'yyyy-mm-dd'),'YYYY-MM-DD HH24:MI:SS') where employee_id=" +
							s);    //更新任现时间
					}
					access.ExecuteNonQuery(sql);   //更新单位，职名

					//刷新列表
                    if (strEmployeeID.IndexOf(",") > 0)
					{
                        string[] arr = strEmployeeID.Split(',');
						foreach (string s in arr)
						{
							lst.Remove(s);
						}
					}
					else
					{
                        lst.Remove(strEmployeeID);
					}
					hfEmployeeID.Value = string.Join(",", lst.ToArray());
					//grdEntity.EnableViewState = false;
					hfSql.Value =
				String.Format(
					               @"
                                    select e.employee_id,e.employee_name,e.sex,e.work_no,p.post_name,getorgname(e.org_id) getorgname from employee e
									left join post p on p.post_id=e.post_id where e.employee_id in ({0})
                                    ",
					hfEmployeeID.Value == "" ? "-1" : hfEmployeeID.Value);
				
				}
				catch (Exception)
				{
					ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('职员调动失败！')", true);
				}
				hfOrgIDAndPostID.Value = "";
			}
		}

		/// <summary>
		/// 获取OrgID和PostID
		/// </summary>
		/// <param name="employeeID"></param>
		/// <returns></returns>
		private List<string> GetOrgIDAndPostID(string employeeID)
		{
			List<string> arr =new List<string>();
			OracleAccess access=new OracleAccess();
			string sql = "select org_id,Post_id,post_date from employee where employee_id=" + employeeID;
			DataSet ds = access.RunSqlDataSet(sql);
			if(ds!=null)
			{
				arr.Add(ds.Tables[0].Rows[0][0].ToString());
				arr.Add(ds.Tables[0].Rows[0][1].ToString());
				arr.Add(ds.Tables[0].Rows[0][2].ToString());
			}
			return arr;
		}

		protected void btnDeleteChecked_Click(object sender, EventArgs e)
		{
			foreach (GridViewRow r in grdEntity.Rows)
			{
				CheckBox ch = r.FindControl("item") as CheckBox;
				Label lblID = r.FindControl("lblID") as Label;
				if(ch.Checked)
				{
					lst.Remove(lblID.Text);
				}
			}
			hfEmployeeID.Value = string.Join(",", lst.ToArray());
 			hfSql.Value =
		String.Format(
						   @"
                                    select e.employee_id,e.employee_name,e.sex,e.work_no,p.post_name,getorgname(e.org_id) getorgname from employee e
									left join post p on p.post_id=e.post_id where e.employee_id in ({0})
                                    ",
			hfEmployeeID.Value == "" ? "-1" : hfEmployeeID.Value);
		}
	}


}
