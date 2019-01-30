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
                if(PrjPub.CurrentLoginUser.SuitRange == 1 && PrjPub.CurrentLoginUser.IsAdmin && PrjPub.CurrentLoginUser.UseType == 0)
                {
                    hfSuitRange.Value = "1";
                }
                else
                {
                    hfSuitRange.Value = "0";
                }
                EmployeeBLL objEmployeeBll = new EmployeeBLL();
                switch (hfType.Value)
                {
                    case "Edit":
                        btnSave.Visible = true;
                        btnSaveNew.Visible = true;
                        btnClose.Visible = true;
                        string strEmployeeID = Request.QueryString.Get("id");
                        RailExam.Model.Employee obj = objEmployeeBll.GetEmployee(Convert.ToInt32(strEmployeeID));
                        GetEmployeeInfo(obj);
                        GetRoleInfo(obj.EmployeeID);
                        if (Request.QueryString.Get("type") == "Org" && SessionSet.OrganizationID !=1)
                        {
                            img1.Visible = false;
                        }

                        if(!PrjPub.CurrentLoginUser.IsAdmin)
                        {
                            ddlRoleNameEdit.Enabled = false;
                        }
                        break;

                    case "Insert":
                        btnSave.Visible = true;
                        btnSaveNew.Visible = true;
                        btnClose.Visible = true;
                        string strOrgID = Request.QueryString.Get("OrgID");
                        if(strOrgID != null && strOrgID != string.Empty)
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
                        if(strPostID != null && strPostID != string.Empty)
                        {
                            if(SessionSet.OrganizationID !=1)
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

                        if (!PrjPub.CurrentLoginUser.IsAdmin)
                        {
                            ddlRoleNameEdit.Enabled = false;
                        }
                        break;

                    case "ReadOnly":
                        btnSave.Visible = false;
                        btnSaveNew.Visible = false;
                        btnClose.Visible = true;
                        RailExam.Model.Employee objEmployee = objEmployeeBll.GetEmployee(Convert.ToInt32(Request.QueryString.Get("id")));
                        GetEmployeeInfo(objEmployee);
                        GetRoleInfo(objEmployee.EmployeeID);
                        break;
                }
            }
        }

        private void GetEmployeeInfo(RailExam.Model.Employee obj)
        {
            txtEmployeeNameEdit.Text = obj.EmployeeName;
            txtWorkNoEdit.Text = obj.WorkNo;
			if(obj.WorkNo == null)
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
            txtFolk.Text = obj.Folk;
            ddlSex.SelectedValue = obj.Sex;
            txtNativePlace.Text = obj.NativePlace;
            rblWedding.SelectedValue = obj.Wedding.ToString();
            txtWorkPhoneEdit.Text = obj.WorkPhone;
            txtHomePhone.Text = obj.HomePhone;
            txtPostCode.Text = obj.PostCode;
            chDimission.Checked = !obj.IsOnPost;
            ddlTech.SelectedValue = obj.TechnicianTypeID.ToString();
            ddlIsGroup.SelectedValue = obj.IsGroupLeader.ToString();
            txtPostNo.Text = obj.PostNo;
            
            if(obj.EmployeeID != 0)
            {
                txtPostNameEdit.Text = obj.PostName;
                hfPostID.Value = obj.PostID.ToString();
                txtOrgNameEdit.Text = obj.OrgName;
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
            EmployeeBLL objBll =new EmployeeBLL();
            RailExam.Model.Employee obj = new RailExam.Model.Employee();
            obj.EmployeeName = txtEmployeeNameEdit.Text;
            obj.WorkNo = txtWorkNoEdit.Text;
            obj.Address = txtAddress.Text;
            obj.BeginDate = DateTime.Parse(dateBeginDate.DateValue.ToString());
            obj.Birthday = DateTime.Parse(dateBirthday.DateValue.ToString());
            obj.Folk = txtFolk.Text;
            obj.PostID = Convert.ToInt32(hfPostID.Value);
            obj.OrgID = Convert.ToInt32(hfOrgID.Value);
            obj.Sex = ddlSex.SelectedValue;
            obj.NativePlace = txtNativePlace.Text;
            obj.Wedding = Convert.ToInt32(rblWedding.SelectedValue);
            obj.WorkPhone = txtWorkPhoneEdit.Text;
            obj.HomePhone = txtHomePhone.Text;
            obj.MobilePhone = txtMobilePhone.Text;
            obj.PostCode = txtPostCode.Text;
            obj.IsOnPost = !chDimission.Checked;
            obj.Memo = txtMemoEdit.Text;
            obj.IsGroupLeader = Convert.ToInt32(ddlIsGroup.SelectedValue);
            obj.TechnicianTypeID = Convert.ToInt32(ddlTech.SelectedValue);
            obj.PostNo = txtPostNo.Text;
            obj.PinYinCode = Pub.GetChineseSpell(obj.EmployeeName);

           SystemUserBLL objSystemBll = new SystemUserBLL();
            if (hfType.Value == "Edit")
            {
                obj.EmployeeID = Convert.ToInt32(Request.QueryString.Get("id"));
                objBll.UpdateEmployee(obj);

                RailExam.Model.SystemUser objSystem = objSystemBll.GetUserByEmployeeID(obj.EmployeeID);
                if(objSystem != null)
                {
                    objSystem.RoleID = Convert.ToInt32(ddlRoleNameEdit.SelectedValue);
                    objSystem.UserID = obj.WorkNo;
                    objSystemBll.UpdateUser(objSystem);
                }
                else
                {
                    objSystem = new SystemUser();
                    objSystem.EmployeeID = obj.EmployeeID;
                    objSystem.Memo = "";
                    objSystem.Password = "111111";
                    objSystem.RoleID = Convert.ToInt32(ddlRoleNameEdit.SelectedValue);
                    objSystem.UserID = obj.WorkNo;
                    objSystemBll.AddUser(objSystem);
                }
            }
            else
            {
            	obj.LoginTime = 0;
            	obj.LoginCount = 0;
                int id = objBll.AddEmployee(obj);
                RailExam.Model.SystemUser objSystem = new SystemUser();
                objSystem.EmployeeID = id;
                objSystem.Memo = "";
                objSystem.Password = "111111";
                objSystem.RoleID = Convert.ToInt32(ddlRoleNameEdit.SelectedValue);
                objSystem.UserID = obj.WorkNo;
                objSystemBll.AddUser(objSystem);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
			EmployeeBLL objBll = new EmployeeBLL();
			if(hfType.Value != "Edit")
			{
				if (objBll.GetEmployeeByWorkNo(txtWorkNoEdit.Text.Trim()) > 0)
				{
					SessionSet.PageMessage = "该员工编码已经存在！";
					return;
				}
			}
			else
			{
				if (ViewState["OldWorkNo"].ToString() != txtWorkNoEdit.Text.Trim())
				{
					if (objBll.GetEmployeeByWorkNo(txtWorkNoEdit.Text.Trim()) > 0)
					{
						SessionSet.PageMessage = "该员工编码已经存在！";
						return;
					}
				}
			}
            SaveEmployeeInfo();
            if(hfType.Value != "Edit")
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
			if (hfType.Value != "Edit")
			{
				if (objBll.GetEmployeeByWorkNo(txtWorkNoEdit.Text.Trim()) > 0)
				{
					SessionSet.PageMessage = "该员工编码已经存在！";
					return;
				}
			}
			else
			{
				if (ViewState["OldWorkNo"].ToString() != txtWorkNoEdit.Text.Trim())
				{
					if (objBll.GetEmployeeByWorkNo(txtWorkNoEdit.Text.Trim()) > 0)
					{
						SessionSet.PageMessage = "该员工编码已经存在！";
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
            RailExam.Model.Employee obj = new RailExam.Model.Employee();
            GetEmployeeInfo(obj);
            ddlRoleNameEdit.SelectedValue = "0";
            if(hfType.Value =="Insert")
            {
               if(txtPostNameEdit.Text == string.Empty)
               {
                   PostBLL objPostBll = new PostBLL();
                   txtPostNameEdit.Text = objPostBll.GetPost(Convert.ToInt32(hfPostID.Value)).PostName;
               }
               
                if(txtOrgNameEdit.Text == string.Empty)
               {
                   OrganizationBLL objOrgBll = new OrganizationBLL();
                   txtOrgNameEdit.Text = objOrgBll.GetOrganization(Convert.ToInt32(hfOrgID.Value)).ShortName;
               }
            }
            dateBirthday.DateValue = DateTime.Today.ToString("yyyy-MM-dd");
            dateBeginDate.DateValue = DateTime.Today.ToString("yyyy-MM-dd");
            hfType.Value = "Insert";
        }
    }
}