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
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
	public partial class EmployeeTransferInDetail : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				string transferID = Request.QueryString.Get("transferID");

				EmployeeTransferBLL objTransferBll = new EmployeeTransferBLL();
				EmployeeTransfer obj = objTransferBll.GetEmployeeTransfer(Convert.ToInt32(transferID));
				txtName.Text = obj.EmployeeName;
				txtWorkNo.Text = obj.WorkNo;
				lblOrg.Text = obj.TransferToOrgName;
				ViewState["EmployeeID"] = obj.EmployeeID.ToString();

                OracleAccess db = new OracleAccess();
                string strSql = "select * from Employee where Employee_ID=" + ViewState["EmployeeID"];
                DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];
			    txtPostNo.Text = dr["Identity_CardNo"].ToString();

				BindWorkShopStart();
				BindWorkgroupStart();

				OrganizationBLL objBll = new OrganizationBLL();
				IList<RailExam.Model.Organization> objList =
					objBll.GetOrganizationsByParentID(obj.TransferToOrgID);
				foreach (RailExam.Model.Organization organization in objList)
				{
					ListItem item = new ListItem();
					item.Value = organization.OrganizationId.ToString();
					item.Text = organization.ShortName;
					ddlWorkshop.Items.Add(item);
				}
			}
		}

		private void BindWorkShopStart()
		{
			ddlWorkshop.Items.Clear();
			ListItem i = new ListItem();
			i.Text = "--请选择--";
			i.Value = "0";
			ddlWorkshop.Items.Add(i);
		}


		private void BindWorkgroupStart()
		{
			ddlWorkgroup.Items.Clear();
			ListItem i = new ListItem();
			i.Text = "--请选择--";
			i.Value = "0";
			ddlWorkgroup.Items.Add(i);
		}

		protected void ddlWorkshop_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ddlWorkshop.SelectedValue == "0")
			{
				BindWorkgroupStart();
			}
			else
			{
				BindWorkgroupStart();
				OrganizationBLL objBll = new OrganizationBLL();
				IList<RailExam.Model.Organization> objList =
					objBll.GetOrganizationsByParentID(Convert.ToInt32(ddlWorkshop.SelectedValue));
				foreach (RailExam.Model.Organization organization in objList)
				{
					ListItem item = new ListItem();
					item.Value = organization.OrganizationId.ToString();
					item.Text = organization.ShortName;
					ddlWorkgroup.Items.Add(item);
				}
			}
		}

		protected void btnOk_Click(object sender, EventArgs e)
		{
			OracleAccess db = new OracleAccess();
            string strSql = "select * from Employee where Employee_ID="+ViewState["EmployeeID"];
		    DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];


		    int orgID;
			if(ddlWorkgroup.SelectedValue != "0")
			{
				orgID = Convert.ToInt32(ddlWorkgroup.SelectedValue);
			}
			else
			{
				orgID = Convert.ToInt32(ddlWorkshop.SelectedValue);
			}

			OrganizationBLL organizationBLL = new OrganizationBLL();
            EmployeeBLL objEmployeeBll = new EmployeeBLL();
			IList<RailExam.Model.Employee> objView =
				objEmployeeBll.GetEmployeeByWhereClause("GetStationOrgID(a.Org_ID)=" + organizationBLL.GetStationOrgID(orgID) +
				                                              " and Work_No='" + txtWorkNo.Text.Trim() + "'");
			if (objView.Count > 0)
			{
				SessionSet.PageMessage = "该员工编码在本单位已经存在！";
				return;
			}
		    strSql = "update Employee "
		             + " set Employee_Name='" + txtName.Text + "',"
		             + "Org_ID=" + orgID + ","
		             + "Work_No='" + txtWorkNo.Text + "',"
		             + "identity_CardNo='" + txtPostNo.Text + "',"
		             + "PinYin_Code='" + Pub.GetChineseSpell(txtName.Text) + "' "
		             + " where Employee_ID=" + ViewState["EmployeeID"];
            db.ExecuteNonQuery(strSql);

		    strSql = "insert into ZJ_Employee_Work "
		             + " values(Employee_Work_Seq.nextval,  "
                     + dr["EMPLOYEE_ID"]
                     + ", to_char(sysdate,'yyyy-mm-dd')," + dr["org_id"] + "," + orgID + ","
		             + dr["Post_ID"] + "," + dr["Post_ID"] + ","
		             + "'',sysdate,'" + PrjPub.CurrentLoginUser.EmployeeName + "',"
                     + "to_date('" + dr["Post_Date"] + "','YYYY-MM-DD HH24:MI:SS'))";
            db.ExecuteNonQuery(strSql);

			string transferID = Request.QueryString.Get("transferID");
			EmployeeTransferBLL objTransferBll = new EmployeeTransferBLL();
			objTransferBll.DeleteEmployeeTransfer(Convert.ToInt32(transferID));

			SystemLogBLL objLogBll = new SystemLogBLL();
			objLogBll.WriteLog("将"+ dr["Employee_Name"] +"调至" + lblOrg.Text);

			Response.Write("<script>window.returnValue='true';window.close();</script>");
		}
	}
}
