using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
	public partial class EmployeeTransferOutDetail : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (PrjPub.CurrentLoginUser == null)
            {
                Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                return;
            }

		    hfNowEmployeeID.Value = PrjPub.CurrentLoginUser.EmployeeID.ToString();

			if(!string.IsNullOrEmpty(hfEmployeeID.Value))
			{
				EmployeeBLL objEmployeeBll = new EmployeeBLL();
				string[] strID = hfEmployeeID.Value.Split(',');

				string strName = string.Empty;
				for (int i = 0; i < strID.Length; i++ )
				{
					RailExam.Model.Employee objEmployee = objEmployeeBll.GetEmployee(Convert.ToInt32(strID[i]));
					if (strName == string.Empty)
					{
						strName = objEmployee.EmployeeName;
					}
					else
					{
						strName = strName + "," + objEmployee.EmployeeName;
					}
				}

				txtEmployee.Text = strName;
			}

			if(!IsPostBack)
			{
				ddlOrg.Items.Clear();
				OrganizationBLL organizationBLL = new OrganizationBLL();

				IList<RailExam.Model.Organization> organizationList = organizationBLL.GetOrganizationsByLevel(2);
				foreach (RailExam.Model.Organization organization in organizationList)
				{
					if (organization.OrganizationId != 1  && organization.OrganizationId != PrjPub.CurrentLoginUser.StationOrgID)
					{
						ListItem item = new ListItem();
						item.Value = organization.OrganizationId.ToString();
						item.Text = organization.ShortName;
						ddlOrg.Items.Add(item);
					}
				}
			}
		}

		protected  void btnOk_Click(object sender, EventArgs e)
		{
			string[] strID = hfEmployeeID.Value.Split(',');
			
			EmployeeTransferBLL objBll = new EmployeeTransferBLL();
			IList<EmployeeTransfer> objList = new List<EmployeeTransfer>();

            OracleAccess db = new OracleAccess();
		    string strSql;

            if (PrjPub.CurrentLoginUser.SuitRange == 1)
            {
                strSql=@"select getstationorgid(org_id) stationorgid,count(*) AdminCount, 
                            GetOrgName(getstationorgid(org_id)) stationName
                            from system_user s 
                            left join employee e on s.employee_id=e.employee_id
                            where  role_id=123 
                            group by getstationorgid(org_id)";
            }
            else
            {
                strSql = @"select getstationorgid(org_id) stationorgid,count(*) AdminCount, 
                            GetOrgName(getstationorgid(org_id)) stationName
                            from system_user s 
                            left join employee e on s.employee_id=e.employee_id
                            where  role_id=123  and getstationorgid(org_id)=" + PrjPub.CurrentLoginUser.StationOrgID 
                            + @" group by getstationorgid(org_id)";
            }
		    DataTable dtCount = db.RunSqlDataSet(strSql).Tables[0];

            strSql = @"select GetStationOrgID(org_id) stationorgid,s.* from Employee a 
                            inner join system_user s on s.employee_id=a.employee_id 
                            where a.Employee_ID in (" + hfEmployeeID.Value + ")";
		    DataTable dtEmployee = db.RunSqlDataSet(strSql).Tables[0];

            strSql = @"select a.*,b.Employee_Name from EMPLOYEE_TRANSFER a 
                         inner join Employee b on a.Employee_ID=b.Employee_ID 
                        where getstationorgid(org_id)=" + PrjPub.CurrentLoginUser.StationOrgID;
            DataTable dtHasTrans = db.RunSqlDataSet(strSql).Tables[0];

            Hashtable htCount = new Hashtable();
		    int count = 0;
		    string error = string.Empty;
			for (int i = 0; i < strID.Length; i++)
			{
				EmployeeTransfer objTransfer = new EmployeeTransfer();
				objTransfer.EmployeeID = Convert.ToInt32(strID[i]);
				objTransfer.TransferToOrgID = Convert.ToInt32(ddlOrg.SelectedValue);
				objList.Add(objTransfer);

                if(strID[i] ==PrjPub.CurrentLoginUser.EmployeeID.ToString())
                {
                    count++;
                }

			    DataRow[] drAdmin = dtEmployee.Select("Employee_ID=" + strID[i] + " and Role_ID=123");
                if(drAdmin.Length>0)
                {
                    string stationid = drAdmin[0]["StationOrgID"].ToString();
                    if(htCount.ContainsKey(stationid))
                    {
                        int num = Convert.ToInt32(htCount[stationid]);
                        htCount[stationid] = num + 1;
                    }
                    else
                    {
                        htCount.Add(stationid,1);
                    }
                }

			    DataRow[] drHasTrans = dtHasTrans.Select("Employee_ID=" + strID[i]);
                if(drHasTrans.Length>0)
                {
                    error = "学员【" + drHasTrans[0]["Employee_Name"] + "】已经处于调出状态，不能重复调出！";
                    break;
                }
			}

            if (error !="")
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('" + error + "')", true);
                return;
            }

            string strMessage = "";
            foreach (DataRow dr in dtCount.Rows)
		    {
		        string stationid = dr["stationorgid"].ToString();
                if(htCount.ContainsKey(stationid))
                {
                    int nownum = Convert.ToInt32(htCount[stationid]);

                    if(nownum == Convert.ToInt32(dr["AdminCount"].ToString()))
                    {
                        strMessage = "必须为" + dr["stationName"] + "至少保留一位站段管理员！";
                        break;
                    }
                }
		    }


            if (count > 0)
            {
                ClientScript.RegisterClientScriptBlock(GetType(),"","alert('"+PrjPub.CurrentLoginUser.EmployeeName+"是当前操作人员，不能被调出！')",true);
                return;
            }

            if (strMessage !="")
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('" + strMessage + "')", true);
                return;
            }

		    objBll.AddEmployeeTransfer(objList);

			SystemLogBLL objLogBll = new SystemLogBLL();
			objLogBll.WriteLog("将部分员工调至"+ddlOrg.SelectedItem.Text);

			Response.Write("<script>window.returnValue='true';window.close();</script>");
		}
	}
}
