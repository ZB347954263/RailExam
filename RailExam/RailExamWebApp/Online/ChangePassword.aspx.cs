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
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.UI;
using DSunSoft.Web.Global;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Online
{
    public partial class ChangePassword : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblUserID.Text = PrjPub.CurrentStudent.UserID;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SystemUserBLL systemUserBll = new SystemUserBLL();

			SystemUser systemUser = systemUserBll.GetUserByEmployeeID(PrjPub.CurrentStudent.EmployeeID);

            string strOldPassword = systemUser.Password;
            if (strOldPassword != txtOldPassword.Text)
            {
                SessionSet.PageMessage = "原始密码错误！";
                return;
            }
            
            if(PrjPub.IsServerCenter)
            {
                systemUser.Password = txtNewPassword.Text;
                systemUser.Flag = false;
                systemUserBll.UpdateUser(systemUser);
            }
            else
            {
                systemUserBll.UpdateUserPsw(lblUserID.Text, txtNewPassword.Text);
            }
 
            Response.Write("<script>top.returnValue='true';window.close();</script>");
        }
    }
}
