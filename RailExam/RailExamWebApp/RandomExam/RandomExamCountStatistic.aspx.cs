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

			#region owc11����Excel
			//RandomExamCountStatisticBLL objBll = new RandomExamCountStatisticBLL();
			//IList<RailExam.Model.RandomExamCountStatistic> objList = objBll.GetCountWithOrg();

			//SpreadsheetClass xlsheet = new SpreadsheetClass();
			//Worksheet ws = (Worksheet)xlsheet.Worksheets[1];

			//ws.Cells[1, 1] = "���";
			//((Range)ws.Cells[1, 1]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);


			//ws.Cells[1, 2] = "վ�ε�λ";
			//ws.get_Range(ws.Cells[1, 2], ws.Cells[1, 4]).set_MergeCells(true);
			//ws.get_Range(ws.Cells[1, 2], ws.Cells[1, 4]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

			//ws.Cells[1, 5] = "���Դ���";
			//((Range)ws.Cells[1, 5]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

			//ws.Cells[1, 6] = "�ο��˴�";
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
			//    // ���ͷ��Ϣ��Ϊ"�ļ�����/���Ϊ"�Ի���ָ��Ĭ���ļ���
			//    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode("վ�λ���") + ".xls");
			//    // ���ͷ��Ϣ��ָ���ļ���С����������ܹ���ʾ���ؽ���
			//    this.Response.AddHeader("Content-Length", file.Length.ToString());

			//    // ָ�����ص���һ�����ܱ��ͻ��˶�ȡ���������뱻����
			//    this.Response.ContentType = "application/ms-excel";

			//    // ���ļ������͵��ͻ���
			//    this.Response.WriteFile(file.FullName);
			//}
			//catch
			//{
			//    SessionSet.PageMessage = "ϵͳ���󣬵���Excel�ļ�ʧ�ܣ�";
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
				// ���ͷ��Ϣ��Ϊ"�ļ�����/���Ϊ"�Ի���ָ��Ĭ���ļ���
				this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode("վ�λ���") + ".xls");
				// ���ͷ��Ϣ��ָ���ļ���С����������ܹ���ʾ���ؽ���
				this.Response.AddHeader("Content-Length", file.Length.ToString());
				// ָ�����ص���һ�����ܱ��ͻ��˶�ȡ���������뱻����
				this.Response.ContentType = "application/ms-excel";
				// ���ļ������͵��ͻ���
				this.Response.WriteFile(file.FullName);
			}
		}

		private void OutPut()
		{
			RandomExamCountStatisticBLL objBll = new RandomExamCountStatisticBLL();
			//I��ǰ��¼��ID
			int _OrgId = PrjPub.CurrentLoginUser.StationOrgID;
			//��1:·�� ����0:վ��
			int _SuitRangeId = PrjPub.CurrentLoginUser.SuitRange;
			//���Կ�ʼʱ��
			DateTime _DateFrom = Convert.ToDateTime(dateStartDateTime.DateValue);
			//���Խ���ʱ��
			DateTime _DateTo = Convert.ToDateTime(dateEndDateTime.DateValue);
		    int style = Convert.ToInt32(ddlStyle.SelectedValue);
			IList<RailExam.Model.RandomExamCountStatistic> objList = objBll.GetCountWithOrg(_SuitRangeId, _OrgId, _DateFrom, _DateTo,PrjPub.GetRailSystemId(),style);


			Excel.Application objApp = new Excel.ApplicationClass();
			Excel.Workbooks objbooks = objApp.Workbooks;
			Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
			Excel.Worksheet objSheet = (Excel.Worksheet)objbook.Worksheets[1];//ȡ��sheet1 
			Excel.Range range;
			string filename = "";

			try
			{
				//����.xls�ļ�����·���� 
				filename = Server.MapPath("/RailExamBao/Excel/Count.xls");

				if (File.Exists(filename.ToString()))
				{
					File.Delete(filename.ToString());
				}

				//�����õ��ı������,��ֵ����Ԫ��   

				objSheet.Cells[1, 1] = "���";

				objSheet.Cells[1, 2] = "վ�ε�λ";
				range = objSheet.get_Range(objSheet.Cells[1, 2], objSheet.Cells[1, 4]);
				range.Merge(0); 

				objSheet.Cells[1, 5] = "���Դ���";

				objSheet.Cells[1, 6] = "�ο��˴�";

				//ͬ��������������  
				for (int i = 0; i < objList.Count; i++)
				{
					objSheet.Cells[2 + i, 1] = i + 1;

					objSheet.Cells[2 + i, 2] = objList[i].OrgName;
					range = objSheet.get_Range(objSheet.Cells[2 + i, 2], objSheet.Cells[2 + i, 4]);
					range.Merge(0); 

					objSheet.Cells[2 + i, 5] = objList[i].ExamCount;

					objSheet.Cells[2 + i, 6] = objList[i].EmployeeCount;
				}

				//���ɼ�,����̨����   
				objApp.Visible = false;

				objbook.Saved = true;
				objbook.SaveCopyAs(filename);
			}
			catch
			{
				SessionSet.PageMessage = "ϵͳ���󣬵���Excel�ļ�ʧ�ܣ�";
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
		/// ������
		/// </summary>
		protected void BindData()
		{
			//��ǰ��¼��վ��ID
			int _OrgId = PrjPub.CurrentLoginUser.StationOrgID;
			//��1:·�� ����0:վ��
			int _SuitRangeId = PrjPub.CurrentLoginUser.SuitRange;
			//���Կ�ʼʱ��
			if (string.IsNullOrEmpty(dateStartDateTime.DateValue.ToString()))
			{
				SessionSet.PageMessage = "��ѡ����Ч��ʼ���ڣ�";
			}
			DateTime _DateFrom = Convert.ToDateTime(dateStartDateTime.DateValue);
			//���Խ���ʱ��
			if (string.IsNullOrEmpty(dateEndDateTime.DateValue.ToString()))
			{
				SessionSet.PageMessage = "��ѡ����Ч�������ڣ�";
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
		/// ��ѯ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnSelect_Click(object sender, EventArgs e)
		{
			BindData();
		}
	}
}
