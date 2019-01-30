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
		/// 绑定数据
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
				// 添加头信息，为"文件下载/另存为"对话框指定默认文件名
				this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(org.ShortName+"考试列表") + ".xls");
				// 添加头信息，指定文件大小，让浏览器能够显示下载进度
				this.Response.AddHeader("Content-Length", file.Length.ToString());
				// 指定返回的是一个不能被客户端读取的流，必须被下载
				this.Response.ContentType = "application/ms-excel";
				// 把文件流发送到客户端
				this.Response.WriteFile(file.FullName);
			}
		}
	}
}
