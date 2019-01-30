using System;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;
using YYControls;

namespace RailExamWebApp.Common
{
	public enum ExcelContentType
	{
		HtmlString,
		FilePath
	}

	public class ExcelContent
	{
		private ExcelContentType _contentType = ExcelContentType.HtmlString;
		private string _content;

		public ExcelContentType ContentType
		{
			get
			{
				return _contentType;
			}
			set
			{
				_contentType = value;
			}
		}

		public string Content
		{
			get
			{
				return _content;
			}
			set
			{
				_content = value;
			}
		}

		public ExcelContent(ExcelContentType contentType, string content)
		{
			_contentType = contentType;
			_content = content;
		}

		public ExcelContent(string content)
		{
			_content = content;
		}

		public ExcelContent()
		{

		}
	}

	public partial class ExportExcel : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            //if (PreviousPage is PageBase)
            //{
            //    PageBase previousPage = (PageBase)PreviousPage;

            //    if (previousPage.ExcelContent.ContentType == ExcelContentType.FilePath)
            //    {
            //        ExportToExcel(this, previousPage.ExcelContent, previousPage.Title.TrimEnd('��'));
            //    }
            //    else
            //    {
            //        SmartGridView gridView = previousPage.Grid;
            //        //gridView.Export("����Excel");
            //        gridView.ExportExcel();
            //    }
            //}
		}

		/// <summary>
		/// ���Ѿ�׼���õ�Excel����������ָ��ҳ�浼��
		/// </summary>
		/// <param name="page">����ҳ��</param>
		/// <param name="excelContent">Excel��������</param>
		/// <param name="fileName">�ļ���</param>
		public static void ExportToExcel(Page page, ExcelContent excelContent, string fileName)
		{
			if (!string.IsNullOrEmpty(excelContent.Content))
			{
				page.Response.Clear();
				page.Response.Buffer = true;
				page.Response.Charset = string.Empty;
				page.EnableViewState = false;

				//attachment����Ϊ�������أ�online�����ߴ�
				//filename������ļ������ƣ�����չ����ָ���ļ��������������Ϊ��.doc || .xls || .txt || .htm
				page.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName) + ".xls");

				//ָ���ļ����� ����Ϊapplication/ms-excel || application/ms-word || application/ms-txt || application/ms-html || application/octet-stream || �������������ֱ��֧���ĵ�
				page.Response.ContentType = "application/vnd.ms-excel";

				//������������
				page.Response.ContentEncoding = Encoding.UTF7;

				//���HTML�������
				if (excelContent.ContentType == ExcelContentType.HtmlString)
				{
					page.Response.Write(excelContent.Content);
				}
				else
				{
					page.Response.WriteFile(excelContent.Content);
				}

				//����
				page.Response.End();
			}
		}
	}

}
