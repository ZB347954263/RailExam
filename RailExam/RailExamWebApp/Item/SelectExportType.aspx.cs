using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;

namespace RailExamWebApp.Item
{
	public partial class SelectExportType : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		    string refresh = Request.Form.Get("refresh");
			if (!string.IsNullOrEmpty(refresh))
			{
				if (rbnType1.Checked)
				{
                    DownloadWord(refresh);
				}
				else
				{
                    DownloadExcel(refresh);
				}
			}
		}

		private void DownloadWord(string strName)
		{
			string filename = Server.MapPath("/RailExamBao/Excel/" + strName + "试题.doc");
			if (File.Exists(filename))
			{
				FileInfo file = new FileInfo(filename.ToString());
				this.Response.Clear();
				this.Response.Buffer = true;
				this.Response.Charset = "utf-7";
				this.Response.ContentEncoding = Encoding.UTF7;
				// 添加头信息，为"文件下载/另存为"对话框指定默认文件名
				this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode("《" + strName + "》试题") + ".doc");
				// 添加头信息，指定文件大小，让浏览器能够显示下载进度
				this.Response.AddHeader("Content-Length", file.Length.ToString());
				// 指定返回的是一个不能被客户端读取的流，必须被下载
				this.Response.ContentType = "application/ms-word";
				// 把文件流发送到客户端
				this.Response.WriteFile(file.FullName);
			}
		}

		private void DownloadExcel(string strName)
		{
            string filename = Server.MapPath("/RailExamBao/Excel/" + strName + "试题.xls");

			if (File.Exists(filename))
			{
				FileInfo file = new FileInfo(filename);
				this.Response.Clear();
				this.Response.Buffer = true;
				this.Response.Charset = "utf-7";
				this.Response.ContentEncoding = Encoding.UTF7;
				// 添加头信息，为"文件下载/另存为"对话框指定默认文件名
				this.Response.AddHeader("Content-Disposition",
										"attachment; filename=" + HttpUtility.UrlEncode("《" + strName + "》试题") + ".xls");
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
