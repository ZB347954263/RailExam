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
					lblInfo.Text = "���������Ծ���ȴ�......";
				}

				if (Request.QueryString.Get("type") == "GetAfter")
				{
					lblInfo.Text = "���������Ծ���ȴ�......";
				}

				if (Request.QueryString.Get("type") == "End")
				{
                    //OrganizationBLL objBll = new OrganizationBLL();
                    //if (objBll.IsAutoUpload(Convert.ToInt32(ConfigurationManager.AppSettings["StationID"])))
                    //{
                    //    lblInfo.Text = "���ڽ������Բ��ϴ������ɼ��ʹ����ȴ�......";
                    //}
                    //else
                    //{
                    //    lblInfo.Text = "���ڽ������Բ��ϴ������ɼ�����ȴ�......";
                    //}

                    lblInfo.Text = "���ڽ������ԣ���ȴ�......";
				}

				if (Request.QueryString.Get("type") == "Upload")
				{
					lblInfo.Text = "�����ϴ������ɼ��ʹ����ȴ�......";
				}

                if (Request.QueryString.Get("type") == "CheckExam")
                {
                    lblInfo.Text = "���ڼ�鿼����������ԣ���ȴ�......";
                }
			}
		}

	}
}
