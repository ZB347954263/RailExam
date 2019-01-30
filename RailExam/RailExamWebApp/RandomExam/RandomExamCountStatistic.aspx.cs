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
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using Microsoft.Office.Interop.Owc11;
using RailExam.BLL;
using RailExam.Model;
using System.Collections.Generic;
using RailExamWebApp.Common.Class;
using RailExam.BLL;

namespace RailExamWebApp.RandomExam
{
	public partial class RandomExamCountStatistic : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				dateStartDateTime.DateValue = System.DateTime.Now.Year.ToString() + "-" + System.DateTime.Now.Month.ToString("00") + "-01";
				dateEndDateTime.DateValue = DateTime.Today.ToShortDateString();
			    hfStyle.Value = ddlStyle.SelectedValue;
				BindData();
			}
		}


		protected void btnOutPut_Click(object sender, EventArgs e)
		{
			OutPut();

			#region owc11导出Excel
			//RandomExamCountStatisticBLL objBll = new RandomExamCountStatisticBLL();
			//IList<RailExam.Model.RandomExamCountStatistic> objList = objBll.GetCountWithOrg();

			//SpreadsheetClass xlsheet = new SpreadsheetClass();
			//Worksheet ws = (Worksheet)xlsheet.Worksheets[1];

			//ws.Cells[1, 1] = "序号";
			//((Range)ws.Cells[1, 1]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);


			//ws.Cells[1, 2] = "站段单位";
			//ws.get_Range(ws.Cells[1, 2], ws.Cells[1, 4]).set_MergeCells(true);
			//ws.get_Range(ws.Cells[1, 2], ws.Cells[1, 4]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

			//ws.Cells[1, 5] = "考试次数";
			//((Range)ws.Cells[1, 5]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

			//ws.Cells[1, 6] = "参考人次";
			//((Range)ws.Cells[1, 16]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

			//for(int i = 0; i <objList.Count; i++)
			//{
			//    ws.Cells[2 + i, 1] = i + 1;

			//    ws.Cells[2 + i, 2] = objList[i].OrgName;
			//    ws.get_Range(ws.Cells[2 + i, 2], ws.Cells[2 + i, 4]).set_MergeCells(true);
			//    ws.get_Range(ws.Cells[2 + i, 2], ws.Cells[2 + i, 4]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);

			//    ws.Cells[2 + i, 5] = objList[i].ExamCount;
			//    ((Range)ws.Cells[2 + i, 5]).set_HorizontalAlignment(XlHAlign.xlHAlignRight);

			//    ws.Cells[2 + i, 6] = objList[i].EmployeeCount;
			//    ((Range)ws.Cells[12 + i, 6]).set_HorizontalAlignment(XlHAlign.xlHAlignRight);
			//}

			//ws.Name = "Sheet1";
			//ws.Cells.Columns.AutoFit();

			//try
			//{
			//    ((Worksheet)xlsheet.Worksheets[1]).Activate();

			//    string path = Server.MapPath("../Excel/Excel.xls");
			//    if (File.Exists(path))
			//        File.Delete(path);
			//    xlsheet.Export(path, SheetExportActionEnum.ssExportActionNone, SheetExportFormat.ssExportAsAppropriate);

			//    FileInfo file = new FileInfo(path);
			//    this.Response.Clear();
			//    this.Response.Buffer = true;
			//    this.Response.Charset = "utf-7";
			//    this.Response.ContentEncoding = Encoding.UTF7;
			//    // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
			//    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode("站段汇总") + ".xls");
			//    // 添加头信息，指定文件大小，让浏览器能够显示下载进度
			//    this.Response.AddHeader("Content-Length", file.Length.ToString());

			//    // 指定返回的是一个不能被客户端读取的流，必须被下载
			//    this.Response.ContentType = "application/ms-excel";

			//    // 把文件流发送到客户端
			//    this.Response.WriteFile(file.FullName);
			//}
			//catch
			//{
			//    SessionSet.PageMessage = "系统错误，导出Excel文件失败！";
			//}
			#endregion

			string filename = Server.MapPath("/RailExamBao/Excel/Count.xls");

			if (File.Exists(filename))
			{
				FileInfo file = new FileInfo(filename.ToString());
				this.Response.Clear();
				this.Response.Buffer = true;
				this.Response.Charset = "utf-7";
				this.Response.ContentEncoding = Encoding.UTF7;
				// 添加头信息，为"文件下载/另存为"对话框指定默认文件名
				this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode("站段汇总") + ".xls");
				// 添加头信息，指定文件大小，让浏览器能够显示下载进度
				this.Response.AddHeader("Content-Length", file.Length.ToString());
				// 指定返回的是一个不能被客户端读取的流，必须被下载
				this.Response.ContentType = "application/ms-excel";
				// 把文件流发送到客户端
				this.Response.WriteFile(file.FullName);
			}
		}

		private void OutPut()
		{
			RandomExamCountStatisticBLL objBll = new RandomExamCountStatisticBLL();
			//I当前登录人ID
			int _OrgId = PrjPub.CurrentLoginUser.StationOrgID;
			//等1:路局 等于0:站段
			int _SuitRangeId = PrjPub.CurrentLoginUser.SuitRange;
			//考试开始时间
			DateTime _DateFrom = Convert.ToDateTime(dateStartDateTime.DateValue);
			//考试结束时间
			DateTime _DateTo = Convert.ToDateTime(dateEndDateTime.DateValue);
		    int style = Convert.ToInt32(ddlStyle.SelectedValue);
			IList<RailExam.Model.RandomExamCountStatistic> objList = objBll.GetCountWithOrg(_SuitRangeId, _OrgId, _DateFrom, _DateTo,PrjPub.GetRailSystemId(),style);


			Excel.Application objApp = new Excel.ApplicationClass();
			Excel.Workbooks objbooks = objApp.Workbooks;
			Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
			Excel.Worksheet objSheet = (Excel.Worksheet)objbook.Worksheets[1];//取得sheet1 
			Excel.Range range;
			string filename = "";

			try
			{
				//生成.xls文件完整路径名 
				filename = Server.MapPath("/RailExamBao/Excel/Count.xls");

				if (File.Exists(filename.ToString()))
				{
					File.Delete(filename.ToString());
				}

				//将所得到的表的列名,赋值给单元格   

				objSheet.Cells[1, 1] = "序号";

				objSheet.Cells[1, 2] = "站段单位";
				range = objSheet.get_Range(objSheet.Cells[1, 2], objSheet.Cells[1, 4]);
				range.Merge(0); 

				objSheet.Cells[1, 5] = "考试次数";

				objSheet.Cells[1, 6] = "参考人次";

				//同样方法处理数据  
				for (int i = 0; i < objList.Count; i++)
				{
					objSheet.Cells[2 + i, 1] = i + 1;

					objSheet.Cells[2 + i, 2] = objList[i].OrgName;
					range = objSheet.get_Range(objSheet.Cells[2 + i, 2], objSheet.Cells[2 + i, 4]);
					range.Merge(0); 

					objSheet.Cells[2 + i, 5] = objList[i].ExamCount;

					objSheet.Cells[2 + i, 6] = objList[i].EmployeeCount;
				}

				//不可见,即后台处理   
				objApp.Visible = false;

				objbook.Saved = true;
				objbook.SaveCopyAs(filename);
			}
			catch
			{
				SessionSet.PageMessage = "系统错误，导出Excel文件失败！";
			}   
			finally
			{
				objbook.Close(Type.Missing, filename, Type.Missing);
				objbooks.Close();
				objApp.Application.Workbooks.Close();
				objApp.Application.Quit();
				objApp.Quit();
				GC.Collect();
			}

		}
		/// <summary>
		/// 绑定数据
		/// </summary>
		protected void BindData()
		{
			//当前登录人站段ID
			int _OrgId = PrjPub.CurrentLoginUser.StationOrgID;
			//等1:路局 等于0:站段
			int _SuitRangeId = PrjPub.CurrentLoginUser.SuitRange;
			//考试开始时间
			if (string.IsNullOrEmpty(dateStartDateTime.DateValue.ToString()))
			{
				SessionSet.PageMessage = "请选择有效开始日期！";
			}
			DateTime _DateFrom = Convert.ToDateTime(dateStartDateTime.DateValue);
			//考试结束时间
			if (string.IsNullOrEmpty(dateEndDateTime.DateValue.ToString()))
			{
				SessionSet.PageMessage = "请选择有效结束日期！";
			}
			DateTime _DateTo = Convert.ToDateTime(dateEndDateTime.DateValue);

			beginTime.Value= _DateFrom.ToString();
			endTime.Value = _DateTo.ToString();

		    int style = Convert.ToInt32(ddlStyle.SelectedValue);

			RandomExamCountStatisticBLL objBll = new RandomExamCountStatisticBLL();
			IList<RailExam.Model.RandomExamCountStatistic> objList = objBll.GetCountWithOrg(_SuitRangeId, _OrgId, _DateFrom, _DateTo,PrjPub.GetRailSystemId(),style);

			examsGrid.DataSource = objList;
			examsGrid.DataBind();
		}
		/// <summary>
		/// 查询
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSelect_Click(object sender, EventArgs e)
		{
			BindData();
		}
	}
}
