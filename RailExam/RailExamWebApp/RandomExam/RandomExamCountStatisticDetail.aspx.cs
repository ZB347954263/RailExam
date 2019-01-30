using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using RailExam.BLL;
using RailExamWebApp.Common.Class;
using RailExam.Model;

namespace RailExamWebApp.RandomExam
{
	public partial class RandomExamCountStatisticDetail : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				BindData();
			}

			if (Request.Form.Get("ExamInfo") != null && Request.Form.Get("ExamInfo") != "")
			{
				DownloadExamInfoExcel();
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		protected void BindData()
		{
			int _OrgId = Convert.ToInt32(Request.QueryString["OrgID"]);
			DateTime _DateFrom = Convert.ToDateTime(Request.QueryString["beginTime"]);
			if (string.IsNullOrEmpty(_DateFrom.ToString()))
			{
				_DateFrom = Convert.ToDateTime(System.DateTime.Now.Year.ToString() + "-" + System.DateTime.Now.Month.ToString("00") + "-01");
			}
			DateTime _DateTo =Convert.ToDateTime(Request.QueryString["endTime"]);
			if (string.IsNullOrEmpty(_DateTo.ToString()))
			{
				_DateTo = Convert.ToDateTime(DateTime.Today.ToShortDateString());
			}

		    int style = Convert.ToInt32(Request.QueryString.Get("style"));

			ExamBLL objBll = new ExamBLL();
			IList<RailExam.Model.Exam> objList = objBll.GetListtWithOrg(_OrgId, _DateFrom, _DateTo,style);

			examsGrid.DataSource = objList;
			examsGrid.DataBind();
		}

		private void DownloadExamInfoExcel()
		{
			string filename = Server.MapPath("/RailExamBao/Excel/Count.xls");

			int _OrgId = Convert.ToInt32(Request.QueryString["OrgID"]);
			OrganizationBLL orgBll = new OrganizationBLL();
			Organization org = orgBll.GetOrganization(_OrgId);

			if (File.Exists(filename))
			{
				FileInfo file = new FileInfo(filename);
				this.Response.Clear();
				this.Response.Buffer = true;
				this.Response.Charset = "utf-7";
				this.Response.ContentEncoding = Encoding.UTF7;
				// ���ͷ��Ϣ��Ϊ"�ļ�����/���Ϊ"�Ի���ָ��Ĭ���ļ���
				this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(org.ShortName+"�����б�") + ".xls");
				// ���ͷ��Ϣ��ָ���ļ���С����������ܹ���ʾ���ؽ���
				this.Response.AddHeader("Content-Length", file.Length.ToString());
				// ָ�����ص���һ�����ܱ��ͻ��˶�ȡ���������뱻����
				this.Response.ContentType = "application/ms-excel";
				// ���ļ������͵��ͻ���
				this.Response.WriteFile(file.FullName);
			}
		}
	}
}
