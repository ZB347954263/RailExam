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
			string filename = Server.MapPath("/RailExamBao/Excel/" + strName + "����.doc");
			if (File.Exists(filename))
			{
				FileInfo file = new FileInfo(filename.ToString());
				this.Response.Clear();
				this.Response.Buffer = true;
				this.Response.Charset = "utf-7";
				this.Response.ContentEncoding = Encoding.UTF7;
				// ���ͷ��Ϣ��Ϊ"�ļ�����/���Ϊ"�Ի���ָ��Ĭ���ļ���
				this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode("��" + strName + "������") + ".doc");
				// ���ͷ��Ϣ��ָ���ļ���С����������ܹ���ʾ���ؽ���
				this.Response.AddHeader("Content-Length", file.Length.ToString());
				// ָ�����ص���һ�����ܱ��ͻ��˶�ȡ���������뱻����
				this.Response.ContentType = "application/ms-word";
				// ���ļ������͵��ͻ���
				this.Response.WriteFile(file.FullName);
			}
		}

		private void DownloadExcel(string strName)
		{
            string filename = Server.MapPath("/RailExamBao/Excel/" + strName + "����.xls");

			if (File.Exists(filename))
			{
				FileInfo file = new FileInfo(filename);
				this.Response.Clear();
				this.Response.Buffer = true;
				this.Response.Charset = "utf-7";
				this.Response.ContentEncoding = Encoding.UTF7;
				// ���ͷ��Ϣ��Ϊ"�ļ�����/���Ϊ"�Ի���ָ��Ĭ���ļ���
				this.Response.AddHeader("Content-Disposition",
										"attachment; filename=" + HttpUtility.UrlEncode("��" + strName + "������") + ".xls");
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
