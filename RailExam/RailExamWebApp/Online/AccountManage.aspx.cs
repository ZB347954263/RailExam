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
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;
using RailExamWebApp.Common.Control.Date;

namespace RailExamWebApp.Online
{
    public partial class AccountManage : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PrjPub.StudentID))
            {
                Response.Write("<script>alert('您还没有登录，不能查看帐户！');window.close();</script>");
            }

            ((Date)fvEmployee.FindControl("dateBirthdayeEdit")).Enabled = false;
            ((Date)fvEmployee.FindControl("dateBeginDateEdit")).Enabled = false;
        }

        protected void fvEmployee_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            Response.Write("<script>window.close();</script>");
        }

        protected void fvEmployee_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            if(e.CommandName == "Cancel")
            {
                Response.Write("<script>window.close();</script>");
            }
        }

		protected void fvEmployee_DataBound(object sender, EventArgs e)
		{
			if(fvEmployee.CurrentMode == FormViewMode.Edit)
			{
				EmployeeBLL objEmployeeBll = new EmployeeBLL();
				Employee objEmployee = objEmployeeBll.GetEmployee(Convert.ToInt32(PrjPub.StudentID));
				((Label)fvEmployee.FindControl("lblCount")).Text = objEmployee.LoginCount + "次";
				((Label)fvEmployee.FindControl("lblTime")).Text = objEmployee.LoginTime / 3600 + "小时" + (objEmployee.LoginTime % 3600) / 60 + "分" + (objEmployee.LoginTime % 3600) % 60 + "秒";
			}
		}
    }
}
