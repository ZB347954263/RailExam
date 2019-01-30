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
using RailExam.BLL;
using System.Collections.Generic;
using RailExam.Model;

namespace RailExamWebApp.RandomExam
{
	public partial class FalseProgressBar : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				if(Request.QueryString.Get("type") == "Get")
				{
					lblInfo.Text = "正在生成试卷，请等待......";
				}

				if (Request.QueryString.Get("type") == "GetAfter")
				{
					lblInfo.Text = "正在生成试卷，请等待......";
				}

				if (Request.QueryString.Get("type") == "End")
				{
                    //OrganizationBLL objBll = new OrganizationBLL();
                    //if (objBll.IsAutoUpload(Convert.ToInt32(ConfigurationManager.AppSettings["StationID"])))
                    //{
                    //    lblInfo.Text = "正在结束考试并上传考生成绩和答卷，请等待......";
                    //}
                    //else
                    //{
                    //    lblInfo.Text = "正在结束考试并上传考生成绩，请等待......";
                    //}

                    lblInfo.Text = "正在结束考试，请等待......";
				}

				if (Request.QueryString.Get("type") == "Upload")
				{
					lblInfo.Text = "正在上传考生成绩和答卷，请等待......";
				}

                if (Request.QueryString.Get("type") == "CheckExam")
                {
                    lblInfo.Text = "正在检查考生答卷完整性，请等待......";
                }
			}
		}

	}
}
