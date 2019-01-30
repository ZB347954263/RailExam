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
using RailExam.Model;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Main
{
    public partial class Admin_Index :PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !CallBack.IsCallback)
            {

                if (PrjPub.CurrentLoginUser == null)
                {
                    CallBack.RefreshInterval = 0;
                    return;
                }

                hfEmployeeID.Value = PrjPub.CurrentLoginUser.EmployeeID.ToString();
            }
        }

        protected void CallBack_OnCallback(object sender, CallBackEventArgs e)
        {
            if (PrjPub.CurrentLoginUser != null)
            {
                LoginUser loginUser = PrjPub.CurrentLoginUser;
            }
        }
    }
}
