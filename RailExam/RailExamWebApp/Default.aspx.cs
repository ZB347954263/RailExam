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
using ET99_FULLLib;
using RailExam.Model;
using RailExam.BLL;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp
{
    public partial class Default : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            string strLogout;

            if(! IsPostBack)
            {
                strLogout = Request.QueryString["logout"];
                if(! string.IsNullOrEmpty(strLogout))
                {
                    Logout();
                }
            }

            strLogout = Request.Form.Get("logout");
            if(! string.IsNullOrEmpty(strLogout))
            {
                Logout();

                lblUserName.Text = "用户名：";
                lblPassword.Text = "密&nbsp;&nbsp;码：";

                txtUserName.Visible = true;
                txtPassword.Visible = true;
                ImageButtonLogin.Visible = true;
            }
            */

			//if (PrjPub.IsEvaluation)
			//{
			//    DateTime strEnd = Convert.ToDateTime(PrjPub.EvaluationDate);

			//    if (DateTime.Today > strEnd)
			//    {
			//        Response.Write(ViewState["OverTime"].ToString());
			//    }
			//}

            if (!IsPostBack)
            {

				txtUserName.Focus();
            }
        }

        protected void ImageButtonLogin_Click(object sender, ImageClickEventArgs e)
        {
            #region 验证服务器端狗信息

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["PID"]))
            {
                ET99FullClass et99 = new ET99FullClass();

                Random random = new Random((int) (DateTime.Now.Ticks >> 32));
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
                    SessionSet.PageMessage = "服务器端未检测到加密锁！";
                    return;
                }
                try
                {
                    et99.OpenToken(strServerPid, 1);
                }
                catch
                {
                    SessionSet.PageMessage = "服务器端打开加密锁失败！";
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
                    SessionSet.PageMessage = "服务器端加密锁USERPIN不正确！";
                    et99.CloseToken();
                    return;
                }
                //try
                //{
                //    strServerSn = ConfigurationManager.AppSettings["SN"];
                //    if (et99.GetSN().ToString() != strServerSn)
                //    {
                //        SessionSet.PageMessage = "服务器端加密锁SN不正确！";
                //        return;
                //    }
                //}
                //catch
                //{
                //    SessionSet.PageMessage = "服务器端获取加密锁SN出错！";
                //}
                try
                {
                    strServerKey = ConfigurationManager.AppSettings["KEY1"];
                    strServerRandomResult = et99.MD5HMAC(1, strServerRandomData, 20).ToString();
                    if (strServerRandomResult != et99.Soft_MD5HMAC(1, strServerRandomData, strServerKey).ToString())
                    {
                        SessionSet.PageMessage = "服务器端加密锁密钥不正确！";
                        return;
                    }
                }
                catch
                {
                    SessionSet.PageMessage = "服务器端验证加密锁密钥出错！";
                    return;
                }
            }

            #endregion

			LoginUserBLL loginUserBLL = new LoginUserBLL();
        	LoginUser loginUser;


			if (PrjPub.IsServerCenter)
			{
				loginUser = loginUserBLL.GetLoginUser(txtUserName.Text, txtPassword.Text, 0);
			}
			else
			{
				loginUser = loginUserBLL.GetLoginUser(txtUserName.Text, txtPassword.Text, 1);
			}
		

        	if (loginUser == null)
        	{
        		SessionSet.PageMessage = "您输入的用户名或密码不正确！";
        		return;
        	}

        	if (loginUser.RoleID == 0)
        	{
        		PrjPub.CurrentStudent = loginUser;
        		PrjPub.WelcomeInfo = loginUser.OrgName + "：" + loginUser.EmployeeName + "，您好！";
        		PrjPub.StudentID = loginUser.EmployeeID.ToString();
        		Session["StudentOrdID"] = loginUser.OrgID;
        		Response.Redirect("Online/AccountManage.aspx");
			}

        	PrjPub.CurrentLoginUser = loginUser;
        	PrjPub.WelcomeInfo = loginUser.OrgName + "：" + loginUser.EmployeeName + "，您好！";
        	SessionSet.UserID = loginUser.UserID;
        	SessionSet.EmployeeID = loginUser.EmployeeID;
        	SessionSet.EmployeeName = loginUser.EmployeeName;
        	SessionSet.OrganizationID = loginUser.OrgID;
        	SessionSet.OrganizationName = loginUser.OrgName;
        	SessionSet.StationOrgID = loginUser.StationOrgID;


			//if (!PrjPub.IsWuhan() && loginUser.EmployeeID != 1 && loginUser.EmployeeID != 2 && !(loginUser.RoleID==2 && !PrjPub.IsServerCenter))
			//{
			//    string strUser = string.Empty;
			//    string strCacheKey = loginUser.EmployeeID.ToString();

			//    strUser = Convert.ToString(Cache[strCacheKey]);

			//    SystemUserLoginBLL objloginBll = new SystemUserLoginBLL();
			//    IList<SystemUserLogin> objList = objloginBll.GetSystemUserLogin(loginUser.EmployeeID);

			//    if (strUser == string.Empty || objList.Count == 0)
			//    {
			//        TimeSpan SessTimeOut = new TimeSpan(0, 0, System.Web.HttpContext.Current.Session.Timeout, 0, 0);

			//        Cache.Insert(strCacheKey, strCacheKey, null, DateTime.MaxValue, SessTimeOut, CacheItemPriority.NotRemovable, null);
			//        Session["User"] = strCacheKey;

			//        if(objList.Count == 0)
			//        {
			//            SystemUserLogin objLogin = new SystemUserLogin();
			//            objLogin.EmployeeID = loginUser.EmployeeID;
			//            objLogin.IPAddress = Pub.GetRealIP();
			//            objloginBll.DeleteSystemUserLogin(loginUser.EmployeeID);
			//            objloginBll.AddSystemUserLogin(objLogin);
			//        }
			//    }
			//    else
			//    {
			//        SessionSet.PageMessage = "该用户已经登录，不能重复登录！";
			//        return;
			//    }
			//}

        	//判断用户数

        	//ApplicationSet.UserCount ++;
        	//if (ApplicationSet.UserCount > ApplicationSet.AllowUserCount)
        	//{
        	//    SessionSet.PageMessage = "系统已达最大用户数，请稍候登录！";
        	//    ApplicationSet.UserCount --;
        	//    return;
        	//}

        	////控件显示
        	//lblUserName.Text = "使用部门：";
        	//lblPassword.Text = "登录用户：";
        	//lblDepartment.Text = loginUser.OrgName;
        	//lblEmployee.Text = loginUser.EmployeeName;
        	//lblUserCount.Text = "用户许可数：&nbsp;&nbsp;" + ApplicationSet.AllowUserCount.ToString();

        	//txtUserName.Visible = false;
        	//txtPassword.Visible = false;
        	//ImageButtonLogin.Visible = false;
        	//lblDepartment.Visible = true;
        	//lblEmployee.Visible = true;
        	//lblUserCount.Visible = true;

        	////将登录用户添加到在线用户数组
        	//ApplicationSet.UserOnline.Add(loginUser.UserID);

        	////登录成功标志
        	//SessionSet.Login = true;

        	//设置Form许可
        	FormsAuthentication.SetAuthCookie(loginUser.UserID, false);

			//HttpBrowserCapabilities brObject = Request.Browser;
			//SessionSet.PageMessage = brObject.Type;

            if(string.IsNullOrEmpty(Request.QueryString.Get("type")))
            {
                loginUser.IsDangan = false;
                Response.Redirect("Main/Admin_Index.aspx");
            }
            else
            {
                loginUser.IsDangan = true;
                Response.Redirect("Main/Admin_Index_Dangan.aspx");
            }
        }

        /*
        private void Logout()
        {
            if(SessionSet.Login)
            {
                if(ApplicationSet.UserCount == 0)
                {
                    return;
                }

                ApplicationSet.UserCount --;
                ApplicationSet.UserOnline.Remove(txtUserName.Text);

                SessionSet.Login = false;

                FormsAuthentication.SignOut();
            }
        }
        */
    }
}
