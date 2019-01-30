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
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using System.Collections.Generic;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Online.Study
{
	public partial class SelectStudyHaErBin : System.Web.UI.Page
	{
		/// <summary>
		/// 页面加载
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack && !Callback1.IsCallback)
			{
				try
				{
					EmployeeBLL employeeBLL = new EmployeeBLL();
					Employee employee = employeeBLL.GetEmployee(Convert.ToInt32(PrjPub.StudentID));

					hfEmployeeName.Value= employee.EmployeeName;
					hfEmployeID.Value = PrjPub.StudentID;

					hfOrgID.Value = employee.OrgID.ToString();
					txtOrg.Text = employee.OrgName;

					hfPostID.Value = employee.PostID.ToString();
					txtPost.Text = employee.PostName;

					ddlIsGroup.SelectedValue = employee.IsGroupLeader.ToString();
					ddlTech.SelectedValue = employee.TechnicianTypeID.ToString();
				}
				catch
				{
					Response.Redirect("/RailExamBao/Common/Error.aspx?error=超时时间已到，请重新登录");
					return;
				}

				if (!PrjPub.IsShowOnline())
				{
					Response.Redirect("/RailExamBao/Common/Error.aspx?error=您当前时间无权使用此功能");
				}
			}


		}

		/// <summary>
		/// 记时
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Callback1_Callback(object sender, CallBackEventArgs e)
		{
			if (PrjPub.IsWuhan())
			{
				return;
			}

			if (string.IsNullOrEmpty(e.Parameters[0].ToString()) || string.IsNullOrEmpty(e.Parameters[1].ToString()))
			{
				return;
			}

			DateTime starttime;
			DateTime endtime;
			try
			{
				starttime = Convert.ToDateTime(e.Parameters[0]);
				endtime = Convert.ToDateTime(e.Parameters[1]);
			}
			catch
			{
				return;
			}

			TimeSpan ts = endtime.Subtract(starttime);
			int spanLoginTime = ts.Hours*3600 + ts.Minutes*60 + ts.Seconds;

			EmployeeBLL employeeBLL = new EmployeeBLL();
			Employee employyee = employeeBLL.GetEmployee(Convert.ToInt32(e.Parameters[2]));
			int oldLogintime = employyee.LoginTime;
			employyee.LoginTime = oldLogintime + spanLoginTime;

			SystemUserBLL systemUserBLL = new SystemUserBLL();
			SystemUser loginUser= systemUserBLL.GetUserByEmployeeID(Convert.ToInt32(e.Parameters[2]));

			SessionSet.UserID = loginUser.UserID;
			SessionSet.EmployeeID = loginUser.EmployeeID;
			SessionSet.EmployeeName = employyee.EmployeeName;
			SessionSet.OrganizationID = employyee.OrgID;
			SessionSet.OrganizationName = employyee.OrgName;

			if(PrjPub.IsServerCenter)
			{
				employeeBLL.UpdateEmployee(employyee);
			}
			else
			{
				employeeBLL.UpdateEmployeeInStation(employyee);
			}
		}
	}
}
