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

namespace RailExamWebApp.Online.Notice
{
    public partial class NoticeDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strNoticeID = Request.QueryString["id"];

                NoticeBLL noticeBLL = new NoticeBLL();
                RailExam.Model.Notice notice = noticeBLL.GetNotice(int.Parse(strNoticeID));

                ViewState["Title"] = notice.Title;
                ViewState["Author"] = notice.EmployeeName;
                ViewState["Content"] = notice.Content;
                ViewState["CreateTime"] = notice.CreateTime;
            }
        }
    }
}
