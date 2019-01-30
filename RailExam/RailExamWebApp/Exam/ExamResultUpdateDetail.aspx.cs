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
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Exam
{
    public partial class ExamResultUpdateDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }

                this.lblExamineeName.Text = Server.UrlDecode(Request.QueryString.Get("ExamineeName"));
                this.LabelOldScore.Text = Server.UrlDecode(Request.QueryString.Get("OldScore"));
                this.LabelWorkMan.Text = PrjPub.CurrentLoginUser.EmployeeName;
                this.LabelTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }
    }
}
