using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using ET99_FULLLib;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamOther
{
	public partial class StudentLogin : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				OrganizationBLL objOrgBll = new OrganizationBLL();
				IList<RailExam.Model.Organization> objOrgList = objOrgBll.GetOrganizationsByLevel(2);

				ListItem item = new ListItem();
				item.Text = "--��ѡ��--";
				item.Value = "0";
				ddlOrg.Items.Add(item);

				foreach (Organization organization in objOrgList)
				{
					if (organization.OrganizationId != 1)
					{
						item = new ListItem();
						item.Text = organization.ShortName;
						item.Value = organization.OrganizationId.ToString();
						ddlOrg.Items.Add(item);
					}
				}

				if(!PrjPub.IsServerCenter)
				{
					OrgConfigBLL orgConfigBLL = new OrgConfigBLL();
					ddlOrg.SelectedValue = orgConfigBLL.GetOrgConfig().OrgID.ToString();
					txtUserName.Focus();
				}
			}
		}


		protected void ImageButtonLogin_Click(object sender, EventArgs e)
		{
			#region ��֤�������˹���Ϣ
			ET99FullClass et99 = new ET99FullClass();
			Random random = new Random((int)(DateTime.Now.Ticks >> 32));
			string strServerRandomData = string.Empty;
			string strServerRandomResult = string.Empty;
			string strServerPid = string.Empty;
			string strServerUserpin = string.Empty;
			//string strServerSn = string.Empty;
			string strServerKey = string.Empty;

			for (int i = 0; i < 20; i++)
			{
				strServerRandomData += random.Next(9).ToString();
			}

			try
			{
				strServerPid = ConfigurationManager.AppSettings["PID"];
				et99.FindToken(strServerPid);
			}
			catch
			{
				SessionSet.PageMessage = "��������δ��⵽��������";
				return;
			}
			try
			{
				et99.OpenToken(strServerPid, 1);
			}
			catch
			{
				SessionSet.PageMessage = "�������˴򿪼�����ʧ�ܣ�";
				et99.CloseToken();
				return;
			}
			try
			{
				strServerUserpin = ConfigurationManager.AppSettings["USERPIN"];
				et99.VerifyPIN(0, strServerUserpin);
			}
			catch
			{
				SessionSet.PageMessage = "�������˼�����USERPIN����ȷ��";
				et99.CloseToken();
				return;
			}

			try
			{
				strServerKey = ConfigurationManager.AppSettings["KEY1"];
				strServerRandomResult = et99.MD5HMAC(1, strServerRandomData, 20).ToString();
				if (strServerRandomResult != et99.Soft_MD5HMAC(1, strServerRandomData, strServerKey).ToString())
				{
					SessionSet.PageMessage = "�������˼�������Կ����ȷ��";
					return;
				}
			}
			catch
			{
				SessionSet.PageMessage = "����������֤��������Կ����";
				return;
			}
			#endregion

			if(ddlOrg.SelectedValue == "0")
			{
				SessionSet.PageMessage = "��ѡ��λ��";
				return;
			}

			LoginUserBLL loginUserBLL = new LoginUserBLL();
			LoginUser loginUser;

			if (PrjPub.IsServerCenter)
			{
				loginUser = loginUserBLL.GetLoginUserByOrgID(Convert.ToInt32(ddlOrg.SelectedValue), txtUserName.Text, txtPassword.Text, 0);
			}
			else
			{
				loginUser = loginUserBLL.GetLoginUserByOrgID(Convert.ToInt32(ddlOrg.SelectedValue), txtUserName.Text, txtPassword.Text, 1);
			}

			if (loginUser == null)
			{
				SessionSet.PageMessage = "��������û��������벻��ȷ��";
				return;
			}

			string strUser = string.Empty;
			string strCacheKey = loginUser.EmployeeID.ToString();

			strUser = Convert.ToString(Cache[strCacheKey]);

			SystemUserLoginBLL objloginBll = new SystemUserLoginBLL();
			IList<SystemUserLogin> objList = objloginBll.GetSystemUserLogin(loginUser.EmployeeID);

			if (strUser == string.Empty || objList.Count == 0)
			{
				TimeSpan SessTimeOut = new TimeSpan(0, 0, System.Web.HttpContext.Current.Session.Timeout, 0, 0);

				Cache.Insert(strCacheKey, strCacheKey, null, DateTime.MaxValue, SessTimeOut, CacheItemPriority.NotRemovable, null);
				Session["User"] = strCacheKey;

				if (objList.Count == 0)
				{
					SystemUserLogin objLogin = new SystemUserLogin();
					objLogin.EmployeeID = loginUser.EmployeeID;
					objLogin.IPAddress = Pub.GetRealIP();
					objloginBll.DeleteSystemUserLogin(loginUser.EmployeeID);
					objloginBll.AddSystemUserLogin(objLogin);
				}
			}
			else
			{
				SessionSet.PageMessage = "���û��Ѿ���¼�������ظ���¼��";
				return;
			}

			PrjPub.CurrentLoginUser = loginUser;
			PrjPub.CurrentStudent = loginUser;
			PrjPub.WelcomeInfo = loginUser.OrgName + "��" + loginUser.EmployeeName + "�����ã�";
			PrjPub.StudentID = loginUser.EmployeeID.ToString();
			hfEmployeeID.Value = loginUser.EmployeeID.ToString();
			Session["StudentOrdID"] = loginUser.OrgID;
			SessionSet.UserID = loginUser.UserID;
			SessionSet.EmployeeID = loginUser.EmployeeID;
			SessionSet.EmployeeName = loginUser.EmployeeName;
			SessionSet.OrganizationID = loginUser.OrgID;
			SessionSet.OrganizationName = loginUser.OrgName;
			SessionSet.StationOrgID = loginUser.StationOrgID;

			EmployeeBLL objEmployeeBll = new EmployeeBLL();
			OrganizationBLL objOrgBll = new OrganizationBLL();
			//�ؼ���ʾ
			lblUserName.Text = "��&nbsp;&nbsp;&nbsp;&nbsp;����";
			lblOrgName.Text = objOrgBll.GetOrganization(loginUser.StationOrgID).ShortName;

			lblPassword.Text = "���ʱ�ţ�";
			lblOrg.Text = objEmployeeBll.GetEmployee(loginUser.EmployeeID).WorkNo;
			lblEmployeeName.Text = loginUser.EmployeeName;

			lbl.Visible = true;
			lblPost.Visible = true;
			lblPost.Text = loginUser.PostName;

			if (Request.QueryString.Get("Type") == "middle")
			{
				ddlOrg.Visible = false;
				lblOrgName.Visible = true;
				txtUserName.Visible = false;
				txtPassword.Visible = false;
				ImageButtonLogin.Visible = false;
				btnExit.Visible = true;
				lblOrg.Visible = true;
				lblEmployeeName.Visible = true;
				btnModifyPsw.Visible = true;
				btnExam.Visible = true;

				//ClientScript.RegisterStartupScript(GetType(),
				//        "jsSelectFirstNode",
				//        @"ShowExamList();",
				//        true);

				//ClientScript.RegisterStartupScript(GetType(), "import", "inputCallback.callback('middle');", true);
			}
			else if (Request.QueryString.Get("Type") == "right")
			{
				ddlOrg.Visible = false;
				lblOrgName.Visible = true;
				txtUserName.Visible = false;
				txtPassword.Visible = false;
				ImageButtonLogin.Visible = false;
				btnExit.Visible = true;
				lblOrg.Visible = true;
				lblEmployeeName.Visible = true;
				btnModifyPsw.Visible = true;
				btnResult.Visible = true;

				//ClientScript.RegisterStartupScript(GetType(),
				//    "jsSelectFirstNode",
				//    @"ShowResultList();",
				//    true);

				//ClientScript.RegisterStartupScript(GetType(), "import", "inputCallback.callback('right');", true);
			}
			else if (Request.QueryString.Get("Type") == "left")
			{
				//�����˵ĵ�¼����+1
				EmployeeBLL objEmpBll = new EmployeeBLL();
				Employee employee = objEmpBll.GetEmployee(loginUser.EmployeeID);

				employee.LoginCount = employee.LoginCount + 1;
				if (PrjPub.IsServerCenter)
				{
					objEmpBll.UpdateEmployee(employee);
				}
				else
				{
					objEmpBll.UpdateEmployeeInStation(employee);
				}

				ddlOrg.Visible = false;
				lblOrgName.Visible = true;
				txtUserName.Visible = false;
				txtPassword.Visible = false;
				ImageButtonLogin.Visible = false;
				btnExit.Visible = true;
				lblOrg.Visible = true;
				lblEmployeeName.Visible = true;
				btnModifyPsw.Visible = true;
				btnExam.Visible = false;
				btnStudy.Visible = true;
			}
		}

		protected void btnExit_Click(object sender, EventArgs e)
		{
			if (PrjPub.CurrentStudent != null)
			{
				string strCacheKey = PrjPub.CurrentStudent.EmployeeID.ToString();
				string strUser = Convert.ToString(Cache[strCacheKey]);
				if (strUser != string.Empty)
				{
					Cache.Remove(strCacheKey);
					SystemUserLoginBLL objloginBll = new SystemUserLoginBLL();
					objloginBll.DeleteSystemUserLogin(Convert.ToInt32(strCacheKey));
				}
			}

			PrjPub.CurrentStudent = null;
			PrjPub.WelcomeInfo = string.Empty;
			PrjPub.StudentID = string.Empty;
			Session.Remove("StudentOrdID");

			ddlOrg.Visible = true;
			lblOrgName.Visible = false;

			//�ؼ���ʾ
			lblUserName.Text = "�û���";
			txtUserName.Text = string.Empty;

			lblPassword.Text = "�� ��";

			txtUserName.Visible = true;
			lblOrg.Visible = false;
			txtPassword.Visible = true;
			lblEmployeeName.Visible = false;
			ImageButtonLogin.Visible = true;
			btnExit.Visible = false;
			btnModifyPsw.Visible = false;
			btnResult.Visible = false;

			Response.Write("<script> top.returnValue = 'true';window.close();</script>");
		}

		protected void inputCallback_Callback(object sender, CallBackEventArgs e)
		{
			hfType.Value = e.Parameters[0];
			hfType.RenderControl(e.Output);
		}
	}
}
