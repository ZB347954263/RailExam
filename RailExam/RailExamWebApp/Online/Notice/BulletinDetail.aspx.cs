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
    public partial class BulletinDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strBulletinID = Request.QueryString["id"];

                BulletinBLL bulletinBLL = new BulletinBLL();
                Bulletin bulletin = bulletinBLL.GetBulletin(int.Parse(strBulletinID));

                ViewState["Title"] = bulletin.Title;
                ViewState["Author"] = bulletin.EmployeeName;
                ViewState["Content"] = bulletin.Content;
                ViewState["CreateTime"] = bulletin.CreateTime;
            }
        }
    }
}
