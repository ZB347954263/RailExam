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
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Online.Study
{
    public partial class FreedomStudyLogin : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                lblPassword.Text = "��&nbsp;&nbsp;�룺";
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Write("<script> top.returnValue = 'true';window.close();</script>");
        }

        protected void ImageButtonLogin_Click(object sender, EventArgs e)
        {

            if (txtPassword.Text.Trim() == string.Empty || txtUserName.Text.Trim() == string.Empty)
            {
                SessionSet.PageMessage = "�������û��������룡";
                return;
            }

            if (txtUserName.Text.Trim() != "test" || txtPassword.Text.Trim() != "111111")
            {
                SessionSet.PageMessage = "��������û��������벻��ȷ��";
                return;
            }

            lblPassword.Visible = false;
            txtPassword.Visible = false;
            txtUserName.Visible = false;
            lblEmployeeName.Text = "test";
            lblEmployeeName.Visible = true;
            btnStudy.Visible = true;
            btnExit.Visible = true;
            ImageButtonLogin.Visible = false;

            ClientScript.RegisterStartupScript(GetType(),
                        "jsSelectFirstNode",
                        @"ShowStudy()",
                        true);
        }
    }
}
