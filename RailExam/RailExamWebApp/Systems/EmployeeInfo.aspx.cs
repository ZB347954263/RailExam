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
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
    public partial class EmployeeInfo : PageBase
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

				if (PrjPub.HasEditRight("角色权限") && PrjPub.IsServerCenter)
				{
					HfUpdateRight.Value = "True";
				}
				else
				{
					HfUpdateRight.Value = "False";
				}
				if (PrjPub.HasDeleteRight("角色权限") && PrjPub.IsServerCenter)
				{
					HfDeleteRight.Value = "True";
				}
				else
				{
					HfDeleteRight.Value = "False";
				} 

                if (PrjPub.HasEditRight("职员管理") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }
                if (PrjPub.HasDeleteRight("职员管理") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }

				if (PrjPub.IsWuhan())
				{
					Grid1.Levels[0].Columns[1].HeadingText = "员工编码";
					lblTitle.Text = "员工编码";
				}
				else
				{
					Grid1.Levels[0].Columns[1].HeadingText = "工资编号";
					lblTitle.Text = "工资编号";
				}

                BindGrid();

            	IsWuhan.Value = PrjPub.IsWuhan().ToString();
            	IsWuhanOnly.Value = PrjPub.IsWuhanOnly().ToString();
            	hfAdmin.Value = PrjPub.CurrentLoginUser.IsAdmin.ToString();
            	NowEmployeeID.Value = PrjPub.CurrentLoginUser.EmployeeID.ToString();
            }
            else
            {
                string strDeleteID = Request.Form.Get("DeleteID");
                if (!string.IsNullOrEmpty(strDeleteID))
                {
					if(strDeleteID == "1" || strDeleteID == "2")
					{
						SessionSet.PageMessage = "该员工为最高权限用户，不能被删除！";
						BindGrid();
						return;
					}
                    DeleteData(int.Parse(strDeleteID));
                    BindGrid();
                }

                string strRefresh = Request.Form.Get("Refresh");
                if (strRefresh == "true")
                {
                    BindGrid();
                }
                string strUpdate = Request.Form.Get("UpdatePsw");
                if (!string.IsNullOrEmpty(strUpdate))
                {
                    SystemUserBLL objBll = new SystemUserBLL();
                    SystemUser obj = objBll.GetUserByEmployeeID(Convert.ToInt32(strUpdate));
                    if(obj != null)
                    {
                        obj.Password = "111111";
						if(PrjPub.IsServerCenter)
						{
							objBll.UpdateUser(obj);
						}
						else
						{
							objBll.UpdateUserPsw(obj.UserID,"111111");
						}
                        SessionSet.PageMessage = "初始化密码成功！";
                    }
                    else
                    {
                        SessionSet.PageMessage = "该员工登录帐户不存在，初始化密码失败！";
                    }
                    BindGrid();
                }
            }
        }

        private void BindGrid()
        {
            string strIDPath = Request.QueryString["idpath"];

            EmployeeBLL employeeBLL = new EmployeeBLL();
            IList<RailExam.Model.Employee> employees = new List<RailExam.Model.Employee>();

            if (Request.QueryString.Get("type") == "Org")
            {
                if (strIDPath == "/1")
                {
                    employees = employeeBLL.GetEmployees(1, "");
                }
                else
                {
                    employees = employeeBLL.GetEmployeesByOrgIDPath(strIDPath);
                }
            }
            else
            {
                if(PrjPub.CurrentLoginUser.SuitRange == 1)
                {
                    employees = employeeBLL.GetEmployees(-1, strIDPath);   
                }
                else
                {
                    employees = employeeBLL.GetEmployees(PrjPub.CurrentLoginUser.StationOrgID, strIDPath);
                }   
            }

            if (employees != null)
            {
                Grid1.DataSource = employees;
                Grid1.DataBind();
            }
        }

        private void DeleteData(int nEmployeID)
        {
            EmployeeBLL employeeBLL = new EmployeeBLL();
			if(employeeBLL.CanDeleteEmployee(nEmployeID))
			{
				if(!PrjPub.IsWuhan())
				{
					EmployeeDetailBLL objBll = new EmployeeDetailBLL();
					objBll.DeleteEmployeeDetail(nEmployeID);
				}
				else
				{
					employeeBLL.DeleteEmployee(nEmployeID);
				}
			}
			else
			{
				SessionSet.PageMessage = "该员工已参加考试，不能删除！";
				return;
			}
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            string strID = Request.QueryString["id"];
            string strIDPath = Request.QueryString["idpath"];

            EmployeeBLL employeeBLL = new EmployeeBLL();
            IList<RailExam.Model.Employee> employees = new List<RailExam.Model.Employee>();

            if (Request.QueryString.Get("type") == "Org")
            {
                employees = employeeBLL.GetEmployees(int.Parse(strID), "", txtWorkNo.Text, txtName.Text, ddlSex.SelectedValue,"",Convert.ToInt32(ddlStatus.SelectedValue));
            }
            else
            {
				employees = employeeBLL.GetEmployees(-1, strIDPath, txtWorkNo.Text, txtName.Text, ddlSex.SelectedValue, "", Convert.ToInt32(ddlStatus.SelectedValue));
            }

            if (employees != null)
            {
                Grid1.DataSource = employees;
                Grid1.DataBind();
            }
        }
    }
}