using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamOther
{
	public partial class EmployeeDetail : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }
				hfType.Value = Request.QueryString.Get("mode").ToString();
				if (PrjPub.CurrentLoginUser.SuitRange == 1 && PrjPub.CurrentLoginUser.IsAdmin && PrjPub.CurrentLoginUser.UseType == 0)
				{
					hfSuitRange.Value = "1";
				}
				else
				{
					hfSuitRange.Value = "0";
				}
				EmployeeDetailBLL objEmployeeBll = new EmployeeDetailBLL();
				switch (hfType.Value)
				{
					case "Edit":
						btnSave.Visible = true;
						btnSaveNew.Visible = true;
						btnClose.Visible = true;
						string strEmployeeID = Request.QueryString.Get("id");
						RailExam.Model.EmployeeDetail obj = objEmployeeBll.GetEmployee(Convert.ToInt32(strEmployeeID));
						GetEmployeeInfo(obj);
						GetRoleInfo(obj.EmployeeID);
						if (Request.QueryString.Get("type") == "Org" && SessionSet.OrganizationID != 1)
						{
							img1.Visible = false;
						}

						//img2.Visible = false;
						//ddlEducationLevel.Enabled = false;
						//txtStudy.ReadOnly = true;
						//txtUniversity.ReadOnly = true;
						//ddlTech.Enabled = false;
						chkApprove.Enabled = false;

						if (!PrjPub.CurrentLoginUser.IsAdmin)
						{
							ddlRoleNameEdit.Enabled = false;
						}
						break;

					case "Insert":
						btnSave.Visible = true;
						btnSaveNew.Visible = true;
						btnClose.Visible = true;
						string strOrgID = Request.QueryString.Get("OrgID");
						if (strOrgID != null && strOrgID != string.Empty)
						{
							OrganizationBLL objBll = new OrganizationBLL();
							if (SessionSet.OrganizationID != 1)
							{
								img1.Visible = false;
							}
							txtOrgNameEdit.Text = objBll.GetOrganization(Convert.ToInt32(strOrgID)).ShortName;
							hfOrgID.Value = strOrgID;
						}

						string strPostID = Request.QueryString.Get("Post");
						if (strPostID != null && strPostID != string.Empty)
						{
							if (SessionSet.OrganizationID != 1)
							{
								img1.Visible = false;
								hfOrgID.Value = SessionSet.OrganizationID.ToString();
								txtOrgNameEdit.Text = SessionSet.OrganizationName;
							}
							PostBLL objBll = new PostBLL();
							//img2.Visible = false;
							txtPostNameEdit.Text = objBll.GetPost(Convert.ToInt32(strPostID)).PostName;
							hfPostID.Value = strPostID;
						}

						dateBirthday.DateValue = DateTime.Today.ToString("yyyy-MM-dd");
						dateBeginDate.DateValue = DateTime.Today.ToString("yyyy-MM-dd");
						workDate.DateValue = DateTime.Today.ToString("yyyy-MM-dd");

						if (!PrjPub.CurrentLoginUser.IsAdmin)
						{
							ddlRoleNameEdit.Enabled = false;
						}
						break;

					case "ReadOnly":
						btnSave.Visible = false;
						btnSaveNew.Visible = false;
						btnClose.Visible = true;
						RailExam.Model.EmployeeDetail objEmployee = objEmployeeBll.GetEmployee(Convert.ToInt32(Request.QueryString.Get("id")));
						GetEmployeeInfo(objEmployee);
						GetRoleInfo(objEmployee.EmployeeID);
						break;
				}
			}

			if(hfPostID.Value != "")
			{
				PostBLL objPost = new PostBLL();
				txtPostNameEdit.Text = objPost.GetPost(Convert.ToInt32(hfPostID.Value)).PostName;
			}
		}

		private void GetEmployeeInfo(RailExam.Model.EmployeeDetail obj)
		{
			txtEmployeeNameEdit.Text = obj.EmployeeName;
			txtWorkNoEdit.Text = obj.WorkNo;
			if (obj.WorkNo == null)
			{
				ViewState["OldWorkNo"] = "";
			}
			else
			{
				ViewState["OldWorkNo"] = obj.WorkNo;
			}
			txtAddress.Text = obj.Address;
			dateBeginDate.DateValue = obj.BeginDate.ToString("yyyy-MM-dd");
			dateBirthday.DateValue = obj.Birthday.ToString("yyyy-MM-dd");
			ddlSex.SelectedValue = obj.Sex;
			txtFolk.Text = obj.Folk;
			txtNativePlace.Text = obj.NativePlace;
			rblWedding.SelectedValue = obj.Wedding.ToString();
			txtWorkPhoneEdit.Text = obj.WorkPhone;
			txtPostCode.Text = obj.PostCode;
			txtMobilePhone.Text = obj.MobilePhone;

			workDate.DateValue = obj.WorkDate.ToString("yyyy-MM-dd");
			ddlEducationLevel.SelectedValue = obj.EducationLevelID.ToString();
			ddlPolictical.SelectedValue = obj.PoliticalStatusID.ToString();
			txtUniversity.Text = obj.GraduateUniversity;
			txtStudy.Text = obj.StudyMajor;
			txtIdentifyCode.Text = obj.IdentifyCode;
			ddlEmployeeTypeID.SelectedValue = obj.EmployeeTypeID.ToString();
			if (ddlEmployeeTypeID.SelectedValue == "1")
			{
				ddlTech.Enabled = true;
				ddlTechTitle.Enabled = false;
			}
			else
			{
				ddlTech.Enabled = false;
				ddlTechTitle.Enabled = true;
			}
			ddlTechTitle.SelectedValue = obj.TechnicalTitleID.ToString();
			ddlEmployeeLevel.SelectedValue = obj.EmployeeLevelID.ToString();
			ddlWorkGroup.SelectedValue = obj.WorkGroupLeaderTypeID.ToString();
			ddlEducationEmployeeType.SelectedValue = obj.EducationEmployeeTypeID.ToString();
			ddlHeadship.SelectedValue = obj.CommitteeHeadShipID.ToString();
			ddlEmployeeTransportType.SelectedValue = obj.EmployeeTransportTypeID.ToString();
			ddlTeacherType.SelectedValue = obj.TeacherTypeID.ToString();
			chkApprove.Checked = (obj.ApprovePost == 1);
			txtHomePhone.Text = obj.HomePhone;

			chDimission.Checked = obj.Dimission;
			ddlTech.SelectedValue = obj.TechnicianTypeID.ToString();
			ddlIsGroup.SelectedValue = obj.IsGroupLeader.ToString();
			txtPostNo.Text = obj.PostNo;
			lblCount.Text = obj.LoginCount + "次";
			lblTime.Text = obj.LoginTime / 3600 + "小时" + (obj.LoginTime % 3600) / 60 + "分" + (obj.LoginTime % 3600) % 60 + "秒";

			if (obj.EmployeeID != 0)
			{
				txtPostNameEdit.Text = obj.PostName;
				hfPostID.Value = obj.PostID.ToString();

				OrganizationBLL orgBll = new OrganizationBLL();
				txtOrgNameEdit.Text = orgBll.GetOrganization(obj.OrgID).ShortName;
				hfOrgID.Value = obj.OrgID.ToString();
			}
		}

		private void GetRoleInfo(int employeeID)
		{
			SystemUserBLL objSystemBll = new SystemUserBLL();
			RailExam.Model.SystemUser objSystem = objSystemBll.GetUserByEmployeeID(employeeID);
			ddlRoleNameEdit.SelectedValue = objSystem.RoleID.ToString();
		}

		private void SaveEmployeeInfo()
		{
			EmployeeDetailBLL objBll = new EmployeeDetailBLL();
			RailExam.Model.EmployeeDetail obj = new RailExam.Model.EmployeeDetail();
			obj.EmployeeName = txtEmployeeNameEdit.Text;
			obj.WorkNo = txtWorkNoEdit.Text;
			obj.Address = txtAddress.Text;
			obj.BeginDate = DateTime.Parse(dateBeginDate.DateValue.ToString());
			obj.Birthday = DateTime.Parse(dateBirthday.DateValue.ToString());
			obj.PostID = Convert.ToInt32(hfPostID.Value);
			obj.OrgID = Convert.ToInt32(hfOrgID.Value);
			obj.Sex = ddlSex.SelectedValue;
			obj.Dimission = chDimission.Checked;
			obj.HomePhone = txtHomePhone.Text;
			obj.Memo = txtMemoEdit.Text;
			obj.IsGroupLeader = Convert.ToInt32(ddlIsGroup.SelectedValue);
			obj.TechnicianTypeID = Convert.ToInt32(ddlTech.SelectedValue);
			obj.PostNo = txtPostNo.Text;
			obj.PinYinCode = Pub.GetChineseSpell(obj.EmployeeName);

			obj.Folk = txtFolk.Text;
			obj.NativePlace = txtNativePlace.Text;
			obj.Wedding = Convert.ToInt32(rblWedding.SelectedValue);
			obj.WorkPhone = txtWorkPhoneEdit.Text;
			obj.PostCode = txtPostCode.Text;
			obj.MobilePhone = txtMobilePhone.Text;

			obj.WorkDate = DateTime.Parse(workDate.DateValue.ToString());
			obj.EducationLevelID = Convert.ToInt32(ddlEducationLevel.SelectedValue);
			obj.PoliticalStatusID = Convert.ToInt32(ddlPolictical.SelectedValue);
			obj.GraduateUniversity = txtUniversity.Text;
			obj.StudyMajor = txtStudy.Text;
			obj.IdentifyCode = txtIdentifyCode.Text;
			obj.EmployeeTypeID = Convert.ToInt32(ddlEmployeeTypeID.SelectedValue);
			obj.TechnicalTitleID = Convert.ToInt32(ddlTechTitle.SelectedValue);
			obj.EmployeeLevelID = Convert.ToInt32(ddlEmployeeLevel.SelectedValue);
			obj.WorkGroupLeaderTypeID = Convert.ToInt32(ddlWorkGroup.SelectedValue);
			obj.EducationEmployeeTypeID = Convert.ToInt32(ddlEducationEmployeeType.SelectedValue);
			obj.CommitteeHeadShipID = Convert.ToInt32(ddlHeadship.SelectedValue);
			obj.EmployeeTransportTypeID = Convert.ToInt32(ddlEmployeeTransportType.SelectedValue);
			obj.TeacherTypeID = Convert.ToInt32(ddlTeacherType.SelectedValue);

			chkApprove.Checked = (obj.ApprovePost == 1);

			SystemUserBLL objSystemBll = new SystemUserBLL();
			if (hfType.Value == "Edit")
			{
				obj.EmployeeID = Convert.ToInt32(Request.QueryString.Get("id"));
				objBll.UpdateEmployee(obj);

				RailExam.Model.SystemUser objSystem = objSystemBll.GetUserByEmployeeID(obj.EmployeeID);
				objSystem.RoleID = Convert.ToInt32(ddlRoleNameEdit.SelectedValue);
				objSystem.UserID = obj.WorkNo;
				objSystemBll.UpdateUser(objSystem);
			}
			else
			{
				obj.LoginTime = 0;
				obj.LoginCount = 0;
				int id = objBll.AddEmployee(obj);
				RailExam.Model.SystemUser objSystem = objSystemBll.GetUserByEmployeeID(id);
				objSystem.RoleID = Convert.ToInt32(ddlRoleNameEdit.SelectedValue);
				objSystemBll.UpdateUser(objSystem);
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			EmployeeBLL objBll = new EmployeeBLL();
			OrganizationBLL organizationBLL = new OrganizationBLL();

			int orgID = organizationBLL.GetStationOrgID(Convert.ToInt32(hfOrgID.Value));
			IList<RailExam.Model.Employee> objView = new List<Employee>();
			if (hfType.Value != "Edit")
			{
				objView =
					objBll.GetEmployeeByWhereClause("GetStationOrgID(a.Org_ID)=" + orgID + " and Work_No='" + txtWorkNoEdit.Text.Trim() + "'");
				if (objView.Count > 0)
				{
					SessionSet.PageMessage = "该工资编号在当前单位已经存在！";
					return;
				}

				if (txtHomePhone.Text.Trim() != string.Empty)
				{
					objView =
						objBll.GetEmployeeByWhereClause("Home_Phone='" + txtHomePhone.Text.Trim() + "'");
					if (objView.Count > 0)
					{
						SessionSet.PageMessage = "该员工编码在系统中已经存在！";
						return;
					}
				}
			}
			else
			{
				if (ViewState["OldWorkNo"].ToString() != txtWorkNoEdit.Text.Trim())
				{
					objView =
					objBll.GetEmployeeByWhereClause("GetStationOrgID(a.Org_ID)=" + orgID + " and Work_No='" + txtWorkNoEdit.Text.Trim() + "'");
					if (objView.Count > 0)
					{
						SessionSet.PageMessage = "该工资编号在当前单位已经存在！";
						return;
					}
				}

				if(txtHomePhone.Text.Trim() != string.Empty)
				{
					objView =
						objBll.GetEmployeeByWhereClause("Employee_ID!=" + Request.QueryString.Get("id") + " and Home_Phone='" + txtHomePhone.Text.Trim() + "'");
					if (objView.Count > 0)
					{
						SessionSet.PageMessage = "该员工编码在系统中已经存在！";
						return;
					}
				}
			}
			SaveEmployeeInfo();
			if (hfType.Value != "Edit")
			{
				Response.Write("<script>window.opener.frames['ifEmployeeInfo'].form1.Refresh.value='true';window.opener.frames['ifEmployeeInfo'].form1.submit();window.close();</script>");
			}
			else
			{
				Response.Write("<script>window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");
			}
		}

		protected void btnSaveAdd_Click(object sender, EventArgs e)
		{
			EmployeeBLL objBll = new EmployeeBLL();
			OrganizationBLL organizationBLL = new OrganizationBLL();

			int orgID = organizationBLL.GetStationOrgID(Convert.ToInt32(hfOrgID.Value));
			IList<RailExam.Model.Employee> objView = new List<Employee>();
			if (hfType.Value != "Edit")
			{
				objView =
					objBll.GetEmployeeByWhereClause("GetStationOrgID(a.Org_ID)=" + orgID + " and Work_No='" + txtWorkNoEdit.Text.Trim() + "'");
				if (objView.Count > 0)
				{
					SessionSet.PageMessage = "该工资编号在当前单位已经存在！";
					return;
				}

				if (txtHomePhone.Text.Trim() != string.Empty)
				{
					objView =
						objBll.GetEmployeeByWhereClause("Home_Phone='" + txtHomePhone.Text.Trim() + "'");
					if (objView.Count > 0)
					{
						SessionSet.PageMessage = "该员工编码在系统中已经存在！";
						return;
					}
				}
			}
			else
			{
				if (ViewState["OldWorkNo"].ToString() != txtWorkNoEdit.Text.Trim())
				{
					objView =
					objBll.GetEmployeeByWhereClause("GetStationOrgID(a.Org_ID)=" + orgID + " and Work_No='" + txtWorkNoEdit.Text.Trim() + "'");
					if (objView.Count > 0)
					{
						SessionSet.PageMessage = "该工资编号在当前单位已经存在！";
						return;
					}
				}

				if (txtHomePhone.Text.Trim() != string.Empty)
				{
					objView =
						objBll.GetEmployeeByWhereClause("a.Employee_ID!=" + Request.QueryString.Get("id") + " and Home_Phone='" + txtHomePhone.Text.Trim() + "'");
					if (objView.Count > 0)
					{
						SessionSet.PageMessage = "该员工编码在系统中已经存在！";
						return;
					}
				}
			}

			SaveEmployeeInfo();
			if (hfType.Value != "Edit")
			{
				Response.Write("<script>window.opener.frames['ifEmployeeInfo'].form1.Refresh.value='true';window.opener.frames['ifEmployeeInfo'].form1.submit();</script>");
			}
			else
			{
				Response.Write("<script>window.opener.form1.Refresh.value='true';window.opener.form1.submit();</script>");
			}
			RailExam.Model.EmployeeDetail obj = new RailExam.Model.EmployeeDetail();
			GetEmployeeInfo(obj);
			ddlRoleNameEdit.SelectedValue = "0";
			if (hfType.Value == "Insert")
			{
				if (txtPostNameEdit.Text == string.Empty)
				{
					PostBLL objPostBll = new PostBLL();
					txtPostNameEdit.Text = objPostBll.GetPost(Convert.ToInt32(hfPostID.Value)).PostName;
				}

				if (txtOrgNameEdit.Text == string.Empty)
				{
					OrganizationBLL objOrgBll = new OrganizationBLL();
					txtOrgNameEdit.Text = objOrgBll.GetOrganization(Convert.ToInt32(hfOrgID.Value)).ShortName;
				}
			}
			dateBirthday.DateValue = DateTime.Today.ToString("yyyy-MM-dd");
			dateBeginDate.DateValue = DateTime.Today.ToString("yyyy-MM-dd");
			hfType.Value = "Insert";
		}

		protected void ddlEmployeeTypeID_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(ddlEmployeeTypeID.SelectedValue == "")
			{
				ddlTech.SelectedValue = "1";
				ddlTech.Enabled = true;
				ddlTechTitle.Enabled = true;
			}
			else if(ddlEmployeeTypeID.SelectedValue == "1")
			{
				ddlTech.Enabled = true;
				ddlTechTitle.Enabled = false;
			}
			else 
			{
				ddlTech.SelectedValue = "1";
				ddlTech.Enabled = false;
				ddlTechTitle.Enabled = true;
			}

			//if(hfType.Value =="Edit")
			//{
			//    ddlTech.Enabled = false;
			//}
		}

		protected void ddlWorkGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(ddlWorkGroup.SelectedValue == "1")
			{
				ddlIsGroup.SelectedValue = "1";
			}
			else
			{
				ddlIsGroup.SelectedValue = "0";
			}
		}
	}
}
