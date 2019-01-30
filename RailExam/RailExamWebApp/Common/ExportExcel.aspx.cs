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
            //        ExportToExcel(this, previousPage.ExcelContent, previousPage.Title.TrimEnd('　'));
            //    }
            //    else
            //    {
            //        SmartGridView gridView = previousPage.Grid;
            //        //gridView.Export("导出Excel");
            //        gridView.ExportExcel();
            //    }
            //}
		}

		/// <summary>
		/// 将已经准备好的Excel导出数据在指定页面导出
		/// </summary>
		/// <param name="page">导出页面</param>
		/// <param name="excelContent">Excel导出数据</param>
		/// <param name="fileName">文件名</param>
		public static void ExportToExcel(Page page, ExcelContent excelContent, string fileName)
		{
			if (!string.IsNullOrEmpty(excelContent.Content))
			{
				page.Response.Clear();
				page.Response.Buffer = true;
				page.Response.Charset = string.Empty;
				page.EnableViewState = false;

				//attachment：作为附件下载；online：在线打开
				//filename：输出文件的名称，其扩展名和指定文件类型相符，可以为：.doc || .xls || .txt || .htm
				page.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName) + ".xls");

				//指定文件类型 可以为application/ms-excel || application/ms-word || application/ms-txt || application/ms-html || application/octet-stream || 或其他浏览器可直接支持文档
				page.Response.ContentType = "application/vnd.ms-excel";

				//避免乱码问题
				page.Response.ContentEncoding = Encoding.UTF7;

				//输出HTML到浏览器
				if (excelContent.ContentType == ExcelContentType.HtmlString)
				{
					page.Response.Write(excelContent.Content);
				}
				else
				{
					page.Response.WriteFile(excelContent.Content);
				}

				//结束
				page.Response.End();
			}
		}
	}

}
