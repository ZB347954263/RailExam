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
using RailExam.Model;
using RailExam.BLL;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp
{
    public partial class Default3 : PageBase
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

            if (!IsPostBack)
            {
                txtUserName.Focus();
            }
        }

        protected void ImageButtonLogin_Click(object sender, ImageClickEventArgs e)
        {
            LoginUserBLL loginUserBLL = new LoginUserBLL();
            LoginUser loginUser = loginUserBLL.GetLoginUser(txtUserName.Text, txtPassword.Text,0);

            if (loginUser == null)
            {
                SessionSet.PageMessage = "您输入的用户名或密码不正确！";
                return;
            }

            PrjPub.CurrentLoginUser = loginUser;
            PrjPub.WelcomeInfo = loginUser.OrgName + "：" + loginUser.EmployeeName + "，您好！";
            SessionSet.UserID = loginUser.UserID;
            SessionSet.EmployeeID = loginUser.EmployeeID;
            SessionSet.EmployeeName = loginUser.EmployeeName;
            SessionSet.OrganizationID = loginUser.OrgID;
            SessionSet.OrganizationName = loginUser.OrgName;

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

            Response.Redirect("Main/Main.aspx");
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