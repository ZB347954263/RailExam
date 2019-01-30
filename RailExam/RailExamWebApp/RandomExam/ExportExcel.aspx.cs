using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
	public partial class ExportExcel : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				if (Request.QueryString.Get("Type") == "StudentInfo")
				{
					OutputStudentInfoExcel();
				}
                else if (Request.QueryString.Get("Type") == "StudentExamInfo")
                {
                    OutputStudentExamInfoExcel();
                }
				else if (Request.QueryString.Get("Type") == "ExamInfo")
				{
					OutputExamInfoExcel();
				}
				else if (Request.QueryString.Get("Type") == "examStatistic")
				{
					OutputExamStatistic();
				}
                else if (Request.QueryString.Get("Type") == "noExam")
                {
                    OutputNoExam();
                }
                else if (Request.QueryString.Get("Type") == "newExam")
                {
                    OutputExcelNew();
                }
				else
				{
					OutputExcel();
				}
			}
		}

		#region 导出成绩登记表
		private void OutputExcel()
		{
			// 根据 ProgressBar.htm 显示进度条界面
			string templateFileName = Path.Combine(Server.MapPath("."), "ProgressBar.htm");
			StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
			string html = reader.ReadToEnd();
			reader.Close();
			Response.Write(html);
			Response.Flush();
			System.Threading.Thread.Sleep(200);

			string jsBlock;

			string qsExamId = Request.QueryString.Get("eid");
			int orgID = Convert.ToInt32(Request.QueryString.Get("OrgID"));
			DataSet ds = new DataSet();

			if (!string.IsNullOrEmpty(qsExamId))
			{
				string OrganizationName = Request.QueryString.Get("orgName");
				string strExamineeName = Request.QueryString.Get("employeeName");
				decimal dScoreLower = string.IsNullOrEmpty(Request.QueryString.Get("lowerScore")) ? 0 : decimal.Parse(Request.QueryString.Get("lowerScore"));
				decimal dScoreUpper = string.IsNullOrEmpty(Request.QueryString.Get("upperScore")) ? 1000 : decimal.Parse(Request.QueryString.Get("upperScore"));

				IList<RandomExamResult> examResults = null;
				RandomExamResultBLL bllExamResult = new RandomExamResultBLL();
                examResults = bllExamResult.GetRandomExamResults(int.Parse(qsExamId), OrganizationName, "", strExamineeName, string.Empty, dScoreLower,
							dScoreUpper, orgID);

				ExamResultStatusBLL bllExamResultStatus = new ExamResultStatusBLL();
				IList<ExamResultStatus> examResultStatuses = bllExamResultStatus.GetExamResultStatuses();

				DataTable dtExamResults = ConvertToDataTable((IList)examResults);
				DataTable dtExamResultStatuses = ConvertToDataTable((IList)examResultStatuses);

				if (dtExamResults != null)
				{
					ds.Tables.Add(dtExamResults);
				}
				else
				{
					ds.Tables.Add(ConvertToDataTable(typeof(RandomExamResult)));
				}
                //ds.Tables.Add(dtExamResultStatuses);
                //ds.Relations.Add(ds.Tables["ExamResultStatus"].Columns["ExamResultStatusId"],
                //    ds.Tables["RandomExamResult"].Columns["StatusId"]);
			}

			DataTable dt = ds.Tables[0];
			string OrgName = "";
			OrganizationBLL orgBll = new OrganizationBLL();
			if (orgID != 1)
			{
				Organization org = orgBll.GetOrganization(orgID);
				OrgName = org.ShortName;
			}

			string strExamName = "";
			string strExamTime = "";
			RandomExamBLL examBll = new RandomExamBLL();
			RailExam.Model.RandomExam exam = examBll.GetExam(int.Parse(qsExamId));
			DateTime date;
			strExamName = exam.ExamName;
			strExamTime = exam.BeginTime.ToString() + "--" + exam.EndTime.ToString();
			date = examBll.GetRandomExamTimeByExamID(exam.RandomExamId);

			System.Threading.Thread.Sleep(10);
			jsBlock = "<script>SetPorgressBar('导出成绩登记表','" + ((1 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") + "'); </script>";
			Response.Write(jsBlock);
			Response.Flush();

            OracleAccess db = new OracleAccess();
		    string strSql;

            strSql =
                @"select y.Examinee_ID as Employee_ID, x.education_level_name, GetEmployeeAge(d.birthday) YearsOld,
                   c.Short_Name||'-'||b.Computer_Room_Name Address
                   from Random_Exam_Result y
                   left join (select * from Random_Exam_Result_Detail where Random_Exam_ID=" + qsExamId+@" and Is_Remove=1) a on y.Random_Exam_Result_ID=a.Random_Exam_Result_ID
                   left join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID
                   left join Org c on b.Org_ID=c.Org_ID
                   left join Employee d on y.Examinee_ID=d.Employee_ID
                   left join Education_Level x on d.Education_level_id=x.Education_level_id
                   where y.Random_Exam_ID=" + qsExamId+@" union all 
                    select y.Examinee_ID as Employee_ID, x.education_level_name, GetEmployeeAge(d.birthday) YearsOld,
                   c.Short_Name||'-'||b.Computer_Room_Name Address
                   from Random_Exam_Result_Temp y
                   left join (select * from Random_Exam_Result_Detail_Temp where Random_Exam_ID=" + qsExamId+@" and Is_Remove=1) a on y.Random_Exam_Result_ID=a.Random_Exam_Result_ID
                   left join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID
                   left join Org c on b.Org_ID=c.Org_ID
                   left join Employee d on y.Examinee_ID=d.Employee_ID
                   left join Education_Level x on d.Education_level_id=x.Education_level_id
                   where y.Random_Exam_ID=" + qsExamId;
            DataSet dsEmployee = db.RunSqlDataSet(strSql);

			string strFirstOrg = orgBll.GetOrganization(1).ShortName;

            #region 原站段导出
            /*
			if (!PrjPub.IsServerCenter)
			{
				SpreadsheetClass xlsheet = new SpreadsheetClass();
				Worksheet ws = (Worksheet)xlsheet.Worksheets[1];
				ws.Cells.Font.set_Size(10);
				ws.Cells.Font.set_Name("宋体");

                ws.Cells[1, 1] = strFirstOrg + OrgName;
				Range rang1 = ws.get_Range(ws.Cells[1, 1], ws.Cells[2, 10]);
				rang1.set_MergeCells(true);
				rang1.set_HorizontalAlignment(XlHAlign.xlHAlignCenter);
				rang1.Font.set_Bold(true);
				rang1.Font.set_Size(17);
				rang1.Font.set_Name("宋体");


				ws.Cells[3, 1] = strExamName + "学员成绩登记表";

				rang1 = ws.get_Range(ws.Cells[3, 1], ws.Cells[3, 10]);
				rang1.set_MergeCells(true);
				rang1.set_HorizontalAlignment(XlHAlign.xlHAlignCenter);
				rang1.Font.set_Bold(true);
				rang1.Font.set_Size(12);
				rang1.Font.set_Name("宋体");

				ws.Cells[4, 1] = "考试日期： " + date.ToString("yyyy-MM-dd");
                rang1 = ws.get_Range(ws.Cells[4, 1], ws.Cells[4, 10]);
				rang1.set_MergeCells(true);
				rang1.set_HorizontalAlignment(XlHAlign.xlHAlignRight);
				rang1.Font.set_Name("宋体");


				//write headertext
				ws.Cells[5, 1] = "序号";
				((Range)ws.Cells[5, 1]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);


				ws.Cells[5, 2] = "姓名";
				ws.get_Range(ws.Cells[5, 2], ws.Cells[5, 2]).set_MergeCells(true);
				ws.get_Range(ws.Cells[5, 2], ws.Cells[5, 2]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

                if (PrjPub.IsWuhan())
                {
                    ws.Cells[5, 3] = "员工编码(身份证号)";
                    ws.get_Range(ws.Cells[5, 3], ws.Cells[5, 3]).set_MergeCells(true);
                    ws.get_Range(ws.Cells[5, 3], ws.Cells[5, 3]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);
                }

			    ws.Cells[5, 4] = "组织机构（车间）";
				ws.get_Range(ws.Cells[5, 4], ws.Cells[5, 4]).set_MergeCells(true);
				ws.get_Range(ws.Cells[5, 4], ws.Cells[5, 4]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

				ws.Cells[5, 5] = "职名";
				ws.get_Range(ws.Cells[5, 5], ws.Cells[5, 5]).set_MergeCells(true);
				ws.get_Range(ws.Cells[5, 5], ws.Cells[5, 5]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

                ws.Cells[5, 6] = "考试时间";
                ws.get_Range(ws.Cells[5, 6], ws.Cells[5, 6]).set_MergeCells(true);
                ws.get_Range(ws.Cells[5, 6], ws.Cells[5, 6]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);
                
                ws.Cells[5, 7] = "文化程度";
				ws.get_Range(ws.Cells[5, 7], ws.Cells[5, 7]).set_MergeCells(true);
				ws.get_Range(ws.Cells[5, 7], ws.Cells[5, 7]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

                ws.Cells[5, 8] = "年龄";
				ws.get_Range(ws.Cells[5, 8], ws.Cells[5, 8]).set_MergeCells(true);
				ws.get_Range(ws.Cells[5, 8], ws.Cells[5, 8]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

                ws.Cells[5, 9] = "考试地点";
				ws.get_Range(ws.Cells[5, 9], ws.Cells[5, 9]).set_MergeCells(true);
				ws.get_Range(ws.Cells[5, 9], ws.Cells[5, 9]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

                ws.Cells[5, 10] = "分数";
				ws.get_Range(ws.Cells[5, 10], ws.Cells[5, 10]).set_MergeCells(true);
				ws.get_Range(ws.Cells[5, 10], ws.Cells[5, 10]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

				System.Threading.Thread.Sleep(10);
				jsBlock = "<script>SetPorgressBar('导出成绩登记表','" + ((2 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") + "'); </script>";
				Response.Write(jsBlock);
				Response.Flush();

				decimal decScore = 0;
				for (int j = 0; j < dt.Rows.Count; j++)
				{
					ws.Cells[6 + j, 1] = j + 1;

					if(!PrjPub.IsWuhan())
					{
						ws.Cells[6 + j, 2] = dt.Rows[j]["ExamineeName"] + "(" + dt.Rows[j]["WorkNo"] + ")";
						ws.get_Range(ws.Cells[6 + j, 2], ws.Cells[6 + j, 3]).set_MergeCells(true);
						ws.get_Range(ws.Cells[6 + j, 2], ws.Cells[6 + j, 3]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);
					}
					else
					{
						ws.Cells[6 + j, 2] = dt.Rows[j]["ExamineeName"].ToString();
						ws.get_Range(ws.Cells[6 + j, 2], ws.Cells[6 + j, 2]).set_MergeCells(true);
						ws.get_Range(ws.Cells[6 + j, 2], ws.Cells[6 + j, 2]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);

                        ws.Cells[6 + j, 3] = "'"+dt.Rows[j]["WorkNo"].ToString();
                        ws.get_Range(ws.Cells[6 + j, 3], ws.Cells[6 + j, 3]).set_MergeCells(true);
                        ws.get_Range(ws.Cells[6 + j, 3], ws.Cells[6 + j, 3]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);
					}

					ws.Cells[6 + j, 4] = dt.Rows[j]["OrganizationName"].ToString();
					ws.get_Range(ws.Cells[6 + j, 4], ws.Cells[6 + j, 4]).set_MergeCells(true);
					ws.get_Range(ws.Cells[6 + j, 4], ws.Cells[6 + j, 4]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);


					ws.Cells[6 + j, 5] = dt.Rows[j]["PostName"].ToString();
					ws.get_Range(ws.Cells[6 + j, 5], ws.Cells[6 + j, 5]).set_MergeCells(true);
					ws.get_Range(ws.Cells[6 + j, 5], ws.Cells[6 + j, 5]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);

                    ws.Cells[6 + j, 6] = Convert.ToDateTime(dt.Rows[j]["BeginDateTime"].ToString()).ToString("f");
                    ws.get_Range(ws.Cells[6 + j, 6], ws.Cells[6 + j, 6]).set_MergeCells(true);
                    ws.get_Range(ws.Cells[6 + j, 6], ws.Cells[6 + j, 6]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);

//                    strSql = @"select a.Employee_ID, x.education_level_name, GetEmployeeAge(d.birthday) YearsOld,
//                                   c.Short_Name||'-'||b.Computer_Room_Name Address
//                                   from Random_Exam_Result_Detail a
//                                   inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID
//                                   inner join Org c on b.Org_ID=c.Org_ID
//                                   inner join Employee d on a.Employee_ID=d.Employee_ID
//                                   inner join Education_Level x on d.Education_level_id=x.Education_level_id
//                                   where Random_Exam_ID=" + qsExamId + " and a.Employee_ID=" + dt.Rows[j]["ExamineeID"];
                    DataRow[] drEmployees = dsEmployee.Tables[0].Select("Employee_ID=" + dt.Rows[j]["ExamineeID"]);
                    if (drEmployees.Length > 0)
                    {
                        DataRow drs = drEmployees[0];
					    ws.Cells[6 + j, 7] = drs["education_level_name"].ToString();
					    ws.get_Range(ws.Cells[6 + j, 7], ws.Cells[6 + j, 7]).set_MergeCells(true);
					    ws.get_Range(ws.Cells[6 + j, 7], ws.Cells[6 + j, 7]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

                        ws.Cells[6 + j, 8] = drs["YearsOld"].ToString();
                        ws.get_Range(ws.Cells[6 + j, 8], ws.Cells[6 + j, 8]).set_MergeCells(true);
                        ws.get_Range(ws.Cells[6 + j, 8], ws.Cells[6 + j, 8]).set_HorizontalAlignment(
                            XlHAlign.xlHAlignCenter);

                        ws.Cells[6 + j, 9] =  drs["Address"].ToString();
                        ws.get_Range(ws.Cells[6 + j, 9], ws.Cells[6 + j, 9]).set_MergeCells(true);
                        ws.get_Range(ws.Cells[6 + j, 9], ws.Cells[6 + j, 9]).set_HorizontalAlignment(
                            XlHAlign.xlHAlignCenter);
                    }

				    decScore += decimal.Parse(dt.Rows[j]["Score"].ToString());
					ws.Cells[6 + j, 10] = dt.Rows[j]["Score"].ToString();
					ws.get_Range(ws.Cells[6 + j, 10], ws.Cells[6 + j, 10]).set_MergeCells(true);
					ws.get_Range(ws.Cells[6 + j, 10], ws.Cells[6 + j, 10]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);


					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('导出成绩登记表','" + (((2+j+1) * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") + "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();
				}

				decimal dec1 = 0;
				if (dt.Rows.Count > 0)
				{
					dec1 = decScore / dt.Rows.Count;
				}

				ws.Cells[6 + dt.Rows.Count, 1] = "平均分：";
				((Range)ws.Cells[5, 1]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

				ws.Cells[6 + dt.Rows.Count, 2] = dec1.ToString("0.00");
				ws.get_Range(ws.Cells[6 + dt.Rows.Count, 2], ws.Cells[6 + dt.Rows.Count, 10]).set_MergeCells(true);
				ws.get_Range(ws.Cells[6 + dt.Rows.Count, 2], ws.Cells[6 + dt.Rows.Count, 10]).set_HorizontalAlignment(
					XlHAlign.xlHAlignRight);

				for (int k = 1; k <= 10; k++)
				{
					for (int j = 5; j <= 6 + dt.Rows.Count; j++)
					{
						((Range)ws.Cells[j, k]).BorderAround(1, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, Missing.Value);
					}
				}

				ws.Name = "1-1";
				ws.Cells.Columns.AutoFit();

				((Worksheet)xlsheet.Worksheets[1]).Activate();

				string path = Server.MapPath("/RailExamBao/Excel/ExamResult.xls");
				if (File.Exists(path))
					File.Delete(path);
				xlsheet.Export(path, SheetExportActionEnum.ssExportActionNone, SheetExportFormat.ssExportAsAppropriate);

				jsBlock = "<script>SetCompleted('处理完成。'); </script>";
				Response.Write(jsBlock);
				Response.Flush();
			}
			else
			{
            } */
            #endregion

            Excel.Application objApp = new Excel.ApplicationClass();
			Excel.Workbooks objbooks = objApp.Workbooks;
			Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
			Excel.Worksheet objSheet = (Excel.Worksheet)objbook.Worksheets[1];//取得sheet1 
			Excel.Range rang1;
			string filename = "";

			try
			{
				//生成.xls文件完整路径名 
				filename = Server.MapPath("/RailExamBao/Excel/ExamResult.xls");

				if (File.Exists(filename.ToString()))
				{
					File.Delete(filename.ToString());
				}
				objSheet.Cells.Font.Size = 10;
				objSheet.Cells.Font.Name = "宋体";

				objSheet.Cells[1, 1] = strFirstOrg + OrgName;
				rang1 = objSheet.get_Range(objSheet.Cells[1, 1], objSheet.Cells[2, 10]);
				rang1.Merge(0);
				rang1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
				rang1.Font.Bold = true;
				objSheet.Cells.Font.Size = 17;
				objSheet.Cells.Font.Name = "宋体";


				objSheet.Cells[3, 1] = strExamName + "学员成绩登记表";

				rang1 = objSheet.get_Range(objSheet.Cells[3, 1], objSheet.Cells[3, 10]);
				rang1.Merge(0);
				rang1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
				rang1.Font.Bold = true;
				objSheet.Cells.Font.Size = 12;
				objSheet.Cells.Font.Name = "宋体";

				objSheet.Cells[4, 1] = "考试日期： " + date.ToString("yyyy-MM-dd");
				rang1 = objSheet.get_Range(objSheet.Cells[4, 1], objSheet.Cells[4, 10]);
				rang1.Merge(0);
				rang1.HorizontalAlignment = XlHAlign.xlHAlignRight;
				objSheet.Cells.Font.Name = "宋体";


				//write headertext
				objSheet.Cells[5, 1] = "序号";
				((Excel.Range)objSheet.Cells[5, 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


				objSheet.Cells[5, 2] = "姓名";
				objSheet.get_Range(objSheet.Cells[5, 2], objSheet.Cells[5, 2]).Merge(0);
				objSheet.get_Range(objSheet.Cells[5, 2], objSheet.Cells[5, 2]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                if (PrjPub.IsWuhan())
                {
                    objSheet.Cells[5, 3] = "员工编码(身份证号)";
                    objSheet.get_Range(objSheet.Cells[5, 3], objSheet.Cells[5, 3]).Merge(0);
                    objSheet.get_Range(objSheet.Cells[5, 3], objSheet.Cells[5, 3]).HorizontalAlignment =
                        XlHAlign.xlHAlignCenter;
                }


			    objSheet.Cells[5, 4] = "组织机构（车间）";
				objSheet.get_Range(objSheet.Cells[5, 4], objSheet.Cells[5, 4]).Merge(0);
				objSheet.get_Range(objSheet.Cells[5, 4], objSheet.Cells[5, 4]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

				objSheet.Cells[5, 5] = "职名";
				objSheet.get_Range(objSheet.Cells[5, 5], objSheet.Cells[5, 5]).Merge(0);
				objSheet.get_Range(objSheet.Cells[5, 5], objSheet.Cells[5, 5]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                objSheet.Cells[5, 6] = "考试时间";
                objSheet.get_Range(objSheet.Cells[5, 6], objSheet.Cells[5, 6]).Merge(0);
                objSheet.get_Range(objSheet.Cells[5, 6], objSheet.Cells[5, 6]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

				objSheet.Cells[5, 7] = "文化程度";
				objSheet.get_Range(objSheet.Cells[5, 7], objSheet.Cells[5, 7]).Merge(0);
				objSheet.get_Range(objSheet.Cells[5, 7], objSheet.Cells[5, 7]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                objSheet.Cells[5, 8] = "年龄";
				objSheet.get_Range(objSheet.Cells[5, 8], objSheet.Cells[5, 8]).Merge(0);
				objSheet.get_Range(objSheet.Cells[5, 8], objSheet.Cells[5, 8]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                objSheet.Cells[5, 9] = "考试地点";
				objSheet.get_Range(objSheet.Cells[5, 9], objSheet.Cells[5, 9]).Merge(0);
				objSheet.get_Range(objSheet.Cells[5, 9], objSheet.Cells[5, 9]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                objSheet.Cells[5, 10] = "分数";
				objSheet.get_Range(objSheet.Cells[5, 10], objSheet.Cells[5, 10]).Merge(0);
				objSheet.get_Range(objSheet.Cells[5, 10], objSheet.Cells[5, 10]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

				System.Threading.Thread.Sleep(10);
				jsBlock = "<script>SetPorgressBar('导出成绩登记表','" + ((2 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") + "'); </script>";
				Response.Write(jsBlock);
				Response.Flush();

				decimal decScore = 0;
				for (int j = 0; j < dt.Rows.Count; j++)
				{
					objSheet.Cells[6 + j, 1] = j + 1;


					if (!PrjPub.IsWuhan())
					{
						objSheet.Cells[6 + j, 2] = dt.Rows[j]["ExamineeName"] + "(" + dt.Rows[j]["WorkNo"] + ")";
						objSheet.get_Range(objSheet.Cells[6 + j, 2], objSheet.Cells[6 + j, 3]).Merge(0);
						objSheet.get_Range(objSheet.Cells[6 + j, 2], objSheet.Cells[6 + j, 3]).HorizontalAlignment =
							XlHAlign.xlHAlignLeft;
					}
					else
					{
						objSheet.Cells[6 + j, 2] = dt.Rows[j]["ExamineeName"].ToString();
						objSheet.get_Range(objSheet.Cells[6 + j, 2], objSheet.Cells[6 + j, 2]).Merge(0);
						objSheet.get_Range(objSheet.Cells[6 + j, 2], objSheet.Cells[6 + j, 2]).HorizontalAlignment =
							XlHAlign.xlHAlignLeft;

                        objSheet.Cells[6 + j, 3] = "'"+dt.Rows[j]["WorkNo"].ToString();
                        objSheet.get_Range(objSheet.Cells[6 + j, 3], objSheet.Cells[6 + j, 3]).Merge(0);
                        objSheet.get_Range(objSheet.Cells[6 + j, 3], objSheet.Cells[6 + j, 3]).HorizontalAlignment =
                            XlHAlign.xlHAlignLeft;
					}

					objSheet.Cells[6 + j, 4] = dt.Rows[j]["OrganizationName"].ToString();
					objSheet.get_Range(objSheet.Cells[6 + j, 4], objSheet.Cells[6 + j, 4]).Merge(0);
					objSheet.get_Range(objSheet.Cells[6 + j, 4], objSheet.Cells[6 + j, 4]).HorizontalAlignment = XlHAlign.xlHAlignLeft;


					objSheet.Cells[6 + j, 5] = dt.Rows[j]["PostName"].ToString();
					objSheet.get_Range(objSheet.Cells[6 + j, 5], objSheet.Cells[6 + j, 5]).Merge(0);
					objSheet.get_Range(objSheet.Cells[6 + j, 5], objSheet.Cells[6 + j, 5]).HorizontalAlignment = XlHAlign.xlHAlignLeft;

                    objSheet.Cells[6 + j, 6] = Convert.ToDateTime(dt.Rows[j]["BeginDateTime"].ToString()).ToString("f");
                    objSheet.get_Range(objSheet.Cells[6 + j, 6], objSheet.Cells[6 + j, 6]).Merge(0);
                    objSheet.get_Range(objSheet.Cells[6 + j, 6], objSheet.Cells[6 + j, 6]).HorizontalAlignment =
                        XlHAlign.xlHAlignLeft;

//                        strSql = @"select a.Employee_ID, x.education_level_name, GetEmployeeAge(d.birthday) YearsOld,
//                                   c.Short_Name||'-'||b.Computer_Room_Name Address
//                                   from Random_Exam_Result_Detail a
//                                   inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID
//                                   inner join Org c on b.Org_ID=c.Org_ID
//                                   inner join Employee d on a.Employee_ID=d.Employee_ID
//                                   inner join Education_Level x on d.Education_level_id=x.Education_level_id
//                                   where Random_Exam_ID=" + qsExamId + " and a.Employee_ID=" + dt.Rows[j]["ExamineeID"];
//                        DataSet dsDetail = db.RunSqlDataSet(strSql);
                    DataRow[] drEmployees = dsEmployee.Tables[0].Select("Employee_ID=" + dt.Rows[j]["ExamineeID"]);
                    if (drEmployees.Length > 0)
                    {
                        DataRow drs = drEmployees[0];
					    objSheet.Cells[6 + j, 7] = drs["education_level_name"].ToString();
					    objSheet.get_Range(objSheet.Cells[6 + j, 7], objSheet.Cells[6 + j, 7]).Merge(0);
					    objSheet.get_Range(objSheet.Cells[6 + j, 7], objSheet.Cells[6 + j, 7]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

					    objSheet.Cells[6 + j, 8] = drs["YearsOld"].ToString();
					    objSheet.get_Range(objSheet.Cells[6 + j, 8], objSheet.Cells[6 + j, 8]).Merge(0);
					    objSheet.get_Range(objSheet.Cells[6 + j, 8], objSheet.Cells[6 + j, 8]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

					    objSheet.Cells[6 + j, 9] = drs["Address"].ToString();
					    objSheet.get_Range(objSheet.Cells[6 + j, 9], objSheet.Cells[6 + j, 9]).Merge(0);
					    objSheet.get_Range(objSheet.Cells[6 + j, 9], objSheet.Cells[6 + j, 9]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    }

					decScore += decimal.Parse(dt.Rows[j]["Score"].ToString());
					objSheet.Cells[6 + j, 10] = dt.Rows[j]["Score"].ToString();
					objSheet.get_Range(objSheet.Cells[6 + j, 10], objSheet.Cells[6 + j, 10]).Merge(0);
					objSheet.get_Range(objSheet.Cells[6 + j, 10], objSheet.Cells[6 + j, 10]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('导出成绩登记表','" + (((2 + j + 1) * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") + "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();
				}

				decimal dec1 = 0;
				if (dt.Rows.Count > 0)
				{
					dec1 = decScore / dt.Rows.Count;
				}

				objSheet.Cells[6 + dt.Rows.Count, 1] = "平均分：";
				((Excel.Range)objSheet.Cells[5, 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

				objSheet.Cells[6 + dt.Rows.Count, 2] = dec1.ToString("0.00");
				objSheet.get_Range(objSheet.Cells[6 + dt.Rows.Count, 2], objSheet.Cells[6 + dt.Rows.Count, 10]).Merge(0);
				objSheet.get_Range(objSheet.Cells[6 + dt.Rows.Count, 2], objSheet.Cells[6 + dt.Rows.Count, 10]).HorizontalAlignment = XlHAlign.xlHAlignRight;



				for (int k = 1; k <= 10; k++)
				{
					for (int j = 5; j <= 6 + dt.Rows.Count; j++)
					{
						((Excel.Range)objSheet.Cells[j, k]).BorderAround(1, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Missing.Value);
					}
				}

                objSheet.Cells.Columns.AutoFit();

				objApp.Visible = false;

				objbook.Saved = true;
				objbook.SaveCopyAs(filename);
			}
			catch(Exception ex)
			{
				throw ex;
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
			Response.Write("<script>top.returnValue='true';window.close();</script>");
		}

		protected static DataTable ConvertToDataTable(Type type)
		{
			DataTable dt = new DataTable(type.Name);

			foreach (PropertyInfo pi in type.GetProperties())
			{
				dt.Columns.Add(pi.Name, pi.PropertyType);
			}
			dt.AcceptChanges();

			return dt;
		}

		protected static DataTable ConvertToDataTable(IList list)
		{
			DataTable dt = null;

			if (list.Count > 0)
			{
				dt = new DataTable(list[0].GetType().Name);
				Type type = list[0].GetType();

				foreach (PropertyInfo pi in type.GetProperties())
				{
					dt.Columns.Add(pi.Name, pi.PropertyType);
				}

				DataRow dr = null;
				foreach (Object o in list)
				{
					dr = dt.NewRow();

					foreach (DataColumn dc in dt.Columns)
					{
						dr[dc] = type.GetProperty(dc.ColumnName).GetValue(o, null);
					}
					dt.Rows.Add(dr);
				}
				dt.AcceptChanges();
			}

			return dt;
		}
		#endregion

		#region 导出考生信息
		private  void OutputStudentInfoExcel()
		{
			// 根据 ProgressBar.htm 显示进度条界面
			string templateFileName = Path.Combine(Server.MapPath("."), "ProgressBar.htm");
			StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
			string html = reader.ReadToEnd();
			reader.Close();
			Response.Write(html);
			Response.Flush();
			System.Threading.Thread.Sleep(200);

			string jsBlock;

			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam objRandomExam = objBll.GetExam(Convert.ToInt32(Request.QueryString.Get("RandomExamID")));

            //RandomExamArrangeBLL objArrangeBll = new RandomExamArrangeBLL();
            //IList<RandomExamArrange> objArrangeList = objArrangeBll.GetRandomExamArranges(objRandomExam.RandomExamId);

            string strSql = "select * from Random_Exam_Arrange_Detail a "
                               + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
                               + " where b.Org_ID='" + PrjPub.CurrentLoginUser.StationOrgID + "'"
                               + " and a.Random_Exam_ID="+objRandomExam.RandomExamId;
            OracleAccess db = new OracleAccess();
            DataSet dsAll = db.RunSqlDataSet(strSql);

            string strUserId = "";
            foreach (DataRow dr in dsAll.Tables[0].Rows)
            {
                if (string.IsNullOrEmpty(strUserId))
                {
                    strUserId += dr["User_Ids"].ToString();
                }
                else
                {
                    strUserId += "," + dr["User_Ids"];
                }
            }

			EmployeeBLL objEmployeeBll = new EmployeeBLL();
            DataSet ds = objEmployeeBll.GetEmployeesByEmployeeIdS(strUserId);
			DataTable dt = ds.Tables[0];

			System.Threading.Thread.Sleep(10);
			jsBlock = "<script>SetPorgressBar('导出考生信息','" + ((1 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") + "'); </script>";
			Response.Write(jsBlock);
			Response.Flush();

            
			Excel.Application objApp = new Excel.ApplicationClass();
			Excel.Workbooks objbooks = objApp.Workbooks;
			Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
			Excel.Worksheet objSheet = (Excel.Worksheet)objbook.Worksheets[1];//取得sheet1 
			Excel.Range range;
			string filename = "";

			try
			{
			    //生成.xls文件完整路径名 
			    filename = Server.MapPath("/RailExamBao/Excel/Excel.xls");

			    if (File.Exists(filename.ToString()))
			    {
			        File.Delete(filename.ToString());
			    }

			    //将所得到的表的列名,赋值给单元格   
			    objSheet.Cells[1, 1] = objRandomExam.ExamName + " 参加考试学员名单";
			    range = objSheet.get_Range(objSheet.Cells[1, 1], objSheet.Cells[1, 9]);
			    range.HorizontalAlignment = HorizontalAlign.Center;
			    range.Merge(0);

			    objSheet.Cells[2, 1] = "序号";

			    objSheet.Cells[2, 2] = "姓名";
			    range = objSheet.get_Range(objSheet.Cells[2, 2], objSheet.Cells[2, 2]);
			    range.Merge(0);

			    objSheet.Cells[2, 3] = "员工编码";
			    range = objSheet.get_Range(objSheet.Cells[2, 3], objSheet.Cells[2, 3]);
			    range.Merge(0);

			    objSheet.Cells[2, 4] = "身份证号";
			    range = objSheet.get_Range(objSheet.Cells[2, 4], objSheet.Cells[2, 4]);
			    range.Merge(0);

			    objSheet.Cells[2, 5] = "职名";
			    range = objSheet.get_Range(objSheet.Cells[2, 5], objSheet.Cells[2, 5]);
			    range.Merge(0);

			    objSheet.Cells[2, 6] = "组织机构";
			    range = objSheet.get_Range(objSheet.Cells[2, 6], objSheet.Cells[2, 8]);
			    range.Merge(0);

			    objSheet.Cells[2, 9] = "技术档案编号（工号）";
			    range = objSheet.get_Range(objSheet.Cells[2, 6], objSheet.Cells[2, 8]);
			    range.Merge(0);

			    System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出考生信息','" + ((2 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
			              "'); </script>";
			    Response.Write(jsBlock);
			    Response.Flush();

			    //同样方法处理数据  
			    for (int i = 0; i < dt.Rows.Count; i++)
			    {
			        objSheet.Cells[3 + i, 1] = i + 1;

			        objSheet.Cells[3 + i, 2] = dt.Rows[i]["EmployeeName"].ToString();
			        range = objSheet.get_Range(objSheet.Cells[3 + i, 2], objSheet.Cells[3 + i, 2]);
			        range.Merge(0);

			        objSheet.Cells[3 + i, 3] = "'" + dt.Rows[i]["WorkNo"];
			        range = objSheet.get_Range(objSheet.Cells[3 + i, 3], objSheet.Cells[3 + i, 3]);
			        range.Merge(0);

			        objSheet.Cells[3 + i, 4] = "'" + dt.Rows[i]["IdentityCardno"].ToString();
			        range = objSheet.get_Range(objSheet.Cells[3 + i, 4], objSheet.Cells[3 + i, 4]);
			        range.Merge(0);

			        objSheet.Cells[3 + i, 5] = dt.Rows[i]["PostName"].ToString();
			        range = objSheet.get_Range(objSheet.Cells[3 + i, 5], objSheet.Cells[3 + i, 5]);
			        range.Merge(0);

			        objSheet.Cells[3 + i, 6] = dt.Rows[i]["OrgName"].ToString();
			        range = objSheet.get_Range(objSheet.Cells[3 + i, 6], objSheet.Cells[3 + i, 8]);
			        range.Merge(0);

			        objSheet.Cells[3 + i, 9] = dt.Rows[i]["TECHNICAL_CODE"].ToString();
			        range = objSheet.get_Range(objSheet.Cells[3 + i, 9], objSheet.Cells[3 + i, 9]);
			        range.Merge(0);

			        System.Threading.Thread.Sleep(10);
			        jsBlock = "<script>SetPorgressBar('导出考生信息','" +
			                  (((2 + i + 1)*100)/((double) dt.Rows.Count + 2)).ToString("0.00") + "'); </script>";
			        Response.Write(jsBlock);
			        Response.Flush();
			    }

                objSheet.Cells.Columns.AutoFit();

			    //不可见,即后台处理   
			    objApp.Visible = false;

			    objbook.Saved = true;
			    objbook.SaveCopyAs(filename);

			    jsBlock = "<script>SetCompleted('处理完成。'); </script>";
			    Response.Write(jsBlock);
			    Response.Flush();
			}
            catch (Exception ex)
            {
                throw ex;
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

		    Response.Write("<script>top.returnValue='true';window.close();</script>");
		}
		#endregion

		#region 导出考试信息
		private void OutputExamInfoExcel()
		{
			// 根据 ProgressBar.htm 显示进度条界面
			string templateFileName = Path.Combine(Server.MapPath("."), "ProgressBar.htm");
			StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
			string html = reader.ReadToEnd();
			reader.Close();
			Response.Write(html);
			Response.Flush();
			System.Threading.Thread.Sleep(200);

			string jsBlock;

			int _OrgId = Convert.ToInt32(Request.QueryString["OrgID"]);
			DateTime _DateFrom = Convert.ToDateTime(Request.QueryString["beginTime"]);
			if (string.IsNullOrEmpty(_DateFrom.ToString()))
			{
				_DateFrom = Convert.ToDateTime(System.DateTime.Now.Year.ToString() + "-" + System.DateTime.Now.Month.ToString("00") + "-01");
			}
			DateTime _DateTo = Convert.ToDateTime(Request.QueryString["endTime"]);
			if (string.IsNullOrEmpty(_DateTo.ToString()))
			{
				_DateTo = Convert.ToDateTime(DateTime.Today.ToShortDateString());
			}
		    int style = Convert.ToInt32(Request.QueryString.Get("style"));
			ExamBLL objBll = new ExamBLL();
			IList<RailExam.Model.Exam> objList = objBll.GetListtWithOrg(_OrgId, _DateFrom, _DateTo, style);

			System.Threading.Thread.Sleep(10);
			jsBlock = "<script>SetPorgressBar('导出考试信息','" + ((1 * 100) / ((double)objList.Count + 2)).ToString("0.00") + "'); </script>";
			Response.Write(jsBlock);
			Response.Flush();

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

				objSheet.Cells[1, 2] = "考试名称";
				range = objSheet.get_Range(objSheet.Cells[1, 2], objSheet.Cells[1, 4]);
				range.Merge(0);

				objSheet.Cells[1, 5] = "有效时间";
				range = objSheet.get_Range(objSheet.Cells[1, 5], objSheet.Cells[1, 6]);
				range.Merge(0);

                objSheet.Cells[1, 7] = "考试类型";
                
                objSheet.Cells[1, 8] = "参考人次";

				objSheet.Cells[1, 9] = "平均成绩";

				objSheet.Cells[1, 10] = "制卷人";

				System.Threading.Thread.Sleep(10);
				jsBlock = "<script>SetPorgressBar('导出考试信息','" + ((2 * 100) / ((double)objList.Count + 2)).ToString("0.00") + "'); </script>";
				Response.Write(jsBlock);
				Response.Flush();

				//同样方法处理数据  
				for (int i = 0; i < objList.Count; i++)
				{
					objSheet.Cells[2 + i, 1] = i + 1;

					objSheet.Cells[2 + i, 2] = objList[i].ExamName;
					range = objSheet.get_Range(objSheet.Cells[2 + i, 2], objSheet.Cells[2 + i, 4]);
					range.Merge(0);

					objSheet.Cells[2 + i, 5] = objList[i].ValidExamTimeDurationString;

				    objSheet.Cells[2 + i, 7] = objList[i].ExamStyleName;
                    
                    objSheet.Cells[2 + i, 8] = objList[i].ExamineeCount;

					objSheet.Cells[2 + i, 9] = objList[i].ExamAverageScore;

					objSheet.Cells[2 + i, 10] = objList[i].CreatePerson;

					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('导出考试信息','" + (((2 + i + 1) * 100) / ((double)objList.Count + 2)).ToString("0.00") + "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();
				}

				//不可见,即后台处理   
				objApp.Visible = false;

				objbook.Saved = true;
				objbook.SaveCopyAs(filename);

				jsBlock = "<script>SetCompleted('处理完成。'); </script>";
				Response.Write(jsBlock);
				Response.Flush();
			}
			catch(Exception ex)
			{
				throw ex;
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

			Response.Write("<script>top.returnValue='true';window.close();</script>");
		}
		#endregion

		#region 导出试题出错信息
		private void OutputExamStatistic()
		{
			

			if (Session["dtExamStatistic"] != null)
			{
				string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
				StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
				string html = reader.ReadToEnd();
				reader.Close();
				Response.Write(html);
				Response.Flush();
				System.Threading.Thread.Sleep(200);

				string jsBlock;

				IList<RailExam.Model.RandomExamStatistic> objList =
					Session["dtExamStatistic"] as IList<RailExam.Model.RandomExamStatistic>;
				Session.Remove("dtExamStatistic");

				System.Threading.Thread.Sleep(10);
				jsBlock = "<script>SetPorgressBar('导出试题出错信息','" + ((1*100)/((double) objList.Count + 2)).ToString("0.00") +
				          "'); </script>";
				Response.Write(jsBlock);
				Response.Flush();

				Excel.Application objApp = new Excel.ApplicationClass();
				Excel.Workbooks objbooks = objApp.Workbooks;
				Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
				Excel.Worksheet objSheet = (Excel.Worksheet) objbook.Worksheets[1]; //取得sheet1 
				Excel.Range range;
				string filename = "";

				try
				{
					//生成.xls文件完整路径名 
					filename = Server.MapPath("/RailExamBao/Excel/试题出错信息.xls");

					if (File.Exists(filename.ToString()))
					{
						File.Delete(filename.ToString());
					}
					//将所得到的表的列名,赋值给单元格   

					int index = 1;
					objSheet.Cells[index, 1] = "序号";

					objSheet.Cells[index, 2] = "试题内容";
					range = objSheet.get_Range(objSheet.Cells[1, 2], objSheet.Cells[1,2]);
					range.NumberFormatLocal = "@";
					range.WrapText = true;

					objSheet.Cells[index, 3] = "所属教材";
					range = objSheet.get_Range(objSheet.Cells[1, 3], objSheet.Cells[1, 3]);
					range.NumberFormatLocal = "@";
					range.WrapText = true;

					objSheet.Cells[index, 4] = "所属章节";

					objSheet.Cells[index,5] = "错误次数";

					objSheet.Cells[index, 6] = "出题次数";

					objSheet.Cells[index, 7] = "错误率";

					index++;
					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('导出试题出错信息','" + ((2*100)/((double) objList.Count + 2)).ToString("0.00") +
					          "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();

					//同样方法处理数据  
					for (int i = 0; i < objList.Count; i++)
					{
						objSheet.Cells[index + i, 1] = i + 1;

						
						range = objSheet.get_Range(objSheet.Cells[index + i, 2], objSheet.Cells[index + i, 2]);
						range.NumberFormatLocal = "@";
						range.WrapText = true;
					 objSheet.Cells[index + i, 2] = objList[i].Content;

						objSheet.Cells[index + i, 3] = objList[i].BookName;

						objSheet.Cells[index + i, 4] = objList[i].ChapterName;

						objSheet.Cells[index + i, 5] = objList[i].ErrorNum;

						objSheet.Cells[index + i, 6] = objList[i].ExamCount;
						
						objSheet.Cells[index + i, 7] = objList[i].ErrorRate;

						System.Threading.Thread.Sleep(10);
						jsBlock = "<script>SetPorgressBar('导出试题出错信息','" +
						          (((2 + i + 1)*100)/((double) objList.Count + 2)).ToString("0.00") +
						          "'); </script>";
						Response.Write(jsBlock);
						Response.Flush();
					}
					objSheet.Cells.Columns.WrapText = true;
					//不可见,即后台处理   
					objApp.Visible = false;

					objbook.Saved = true;
					objbook.SaveCopyAs(filename);

					jsBlock = "<script>SetCompleted('处理完成。'); </script>";
					Response.Write(jsBlock);
					Response.Flush();
				}
				catch (Exception ex)
				{
					throw ex;
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

			Response.Write("<script>top.returnValue='true';window.close();</script>");
		}
        #endregion

        private void OutputStudentExamInfoExcel()
        {
            if (Session["StudentExamInfo"] != null)
            {
                string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
                StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
                string html = reader.ReadToEnd();
                reader.Close();
                Response.Write(html);
                Response.Flush();
                System.Threading.Thread.Sleep(200);

                string jsBlock;

                IList<RailExam.Model.RandomExamResult> objList =
                    Session["StudentExamInfo"] as IList<RailExam.Model.RandomExamResult>;
                Session.Remove("StudentExamInfo");

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出学员考试成绩','" + ((1 * 100) / ((double)objList.Count + 2)).ToString("0.00") +
                          "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                Excel.Application objApp = new Excel.ApplicationClass();
                Excel.Workbooks objbooks = objApp.Workbooks;
                Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet objSheet = (Excel.Worksheet)objbook.Worksheets[1]; //取得sheet1 
                Excel.Range range;
                string filename = "";

                try
                {
                    //生成.xls文件完整路径名 
                    filename = Server.MapPath("/RailExamBao/Excel/学员考试成绩.xls");

                    if (File.Exists(filename.ToString()))
                    {
                        File.Delete(filename.ToString());
                    }
                    //将所得到的表的列名,赋值给单元格   

                    int index = 1;
                    objSheet.Cells[index, 1] = "序号";

                    objSheet.Cells[index, 2] = "考生姓名";
                    range = objSheet.get_Range(objSheet.Cells[1, 2], objSheet.Cells[1, 2]);
                    range.NumberFormatLocal = "@";
                    range.WrapText = true;

                    objSheet.Cells[index, 3] = "员工编码";
                    range = objSheet.get_Range(objSheet.Cells[1, 3], objSheet.Cells[1, 3]);
                    range.NumberFormatLocal = "@";
                    range.WrapText = true;

                    objSheet.Cells[index, 4] = "职名";

                    objSheet.Cells[index, 5] = "考试名称";

                    objSheet.Cells[index, 6] = "考试类型";

                    objSheet.Cells[index, 7] = "考生单位";

                    objSheet.Cells[index, 8] = "考试地点";

                    objSheet.Cells[index, 9] = "开始时间";

                    objSheet.Cells[index, 10] = "结束时间";

                    objSheet.Cells[index, 11] = "考试时间";

                    objSheet.Cells[index, 12] = "成绩";

                    index++;
                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('导出学员考试成绩','" + ((2 * 100) / ((double)objList.Count + 2)).ToString("0.00") +
                              "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();

                    //同样方法处理数据  
                    for (int i = 0; i < objList.Count; i++)
                    {
                        objSheet.Cells[index + i, 1] = i + 1;

                        range = objSheet.get_Range(objSheet.Cells[index + i, 2], objSheet.Cells[index + i, 2]);
                        range.NumberFormatLocal = "@";
                        range.WrapText = true;
                        objSheet.Cells[index + i, 2] = objList[i].ExamineeName;

                        objSheet.Cells[index + i, 3] = objList[i].WorkNo;

                        objSheet.Cells[index + i, 4] = objList[i].PostName;

                        objSheet.Cells[index + i, 5] = objList[i].ExamName;

                        objSheet.Cells[index + i, 6] = objList[i].ExamStyleName;

                        objSheet.Cells[index + i, 7] = objList[i].OrganizationName;

                        objSheet.Cells[index + i, 8] = objList[i].ExamOrgName;

                        objSheet.Cells[index + i, 9] = objList[i].BeginDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                        objSheet.Cells[index + i, 10] = objList[i].EndDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                        objSheet.Cells[index + i, 11] = objList[i].ExamTime / 60 + "分" + objList[i].ExamTime % 60+"秒";

                        objSheet.Cells[index + i, 12] = objList[i].Score;

                        System.Threading.Thread.Sleep(10);
                        jsBlock = "<script>SetPorgressBar('导出学员考试成绩','" +
                                  (((2 + i + 1) * 100) / ((double)objList.Count + 2)).ToString("0.00") +
                                  "'); </script>";
                        Response.Write(jsBlock);
                        Response.Flush();
                    }
                    objSheet.Cells.Columns.WrapText = true;
                    objSheet.Cells.Columns.AutoFit();
                    //不可见,即后台处理   
                    objApp.Visible = false;

                    objbook.Saved = true;
                    objbook.SaveCopyAs(filename);

                    jsBlock = "<script>SetCompleted('处理完成。'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                }
                catch (Exception ex)
                {
                    throw ex;
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

            Response.Write("<script>top.returnValue='true';window.close();</script>");
        }

        private void OutputNoExam()
        {
            if(Session["NoExamInfo"] !=null)
            {
                string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
                StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
                string html = reader.ReadToEnd();
                reader.Close();
                Response.Write(html);
                Response.Flush();
                System.Threading.Thread.Sleep(200);

                RandomExamBLL objBll = new RandomExamBLL();
                RailExam.Model.RandomExam objRandomExam = objBll.GetExam(Convert.ToInt32(Request.QueryString.Get("eid")));

                IList<Employee> objList = Session["NoExamInfo"] as IList<RailExam.Model.Employee>;
                Session.Remove("NoExamInfo");

                Excel.Application objApp = new Excel.ApplicationClass();
                Excel.Workbooks objbooks = objApp.Workbooks;
                Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet objSheet = (Excel.Worksheet) objbook.Worksheets[1]; //取得sheet1 
                Excel.Range rang1;
                string filename = "";

                string strName = "Excel" + DateTime.Now.ToString("yyyyMMddHHmmss");
                //生成.xls文件完整路径名 
                filename = Server.MapPath("/RailExamBao/Excel/" + strName + ".xls");

                if (File.Exists(filename.ToString()))
                {
                    File.Delete(filename.ToString());
                }
                objSheet.Cells.Font.Size = 10;
                objSheet.Cells.Font.Name = "宋体";

                objSheet.Cells[1, 1] = objRandomExam.ExamName + " 未参加考试学员名单";
                rang1 = objSheet.get_Range(objSheet.Cells[1, 1], objSheet.Cells[1, 7]);
                rang1.Merge(0);
                rang1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                rang1.Font.Bold = true;
                objSheet.Cells.Font.Size = 17;
                objSheet.Cells.Font.Name = "宋体";

                //write headertext
                objSheet.Cells[2, 1] = "序号";
                ((Excel.Range) objSheet.Cells[2, 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                objSheet.Cells[2, 2] = "姓名";
                objSheet.get_Range(objSheet.Cells[2, 2], objSheet.Cells[2, 2]).Merge(0);
                objSheet.get_Range(objSheet.Cells[2, 2], objSheet.Cells[2, 2]).HorizontalAlignment =
                    XlHAlign.xlHAlignCenter;

                objSheet.Cells[2, 3] = "员工编码(身份证号码)";
                objSheet.get_Range(objSheet.Cells[2, 3], objSheet.Cells[2, 3]).Merge(0);
                objSheet.get_Range(objSheet.Cells[2, 3], objSheet.Cells[2, 3]).HorizontalAlignment =
                    XlHAlign.xlHAlignCenter;

                objSheet.Cells[2, 4] = "职名";
                objSheet.get_Range(objSheet.Cells[2, 4], objSheet.Cells[2, 4]).Merge(0);
                objSheet.get_Range(objSheet.Cells[2, 4], objSheet.Cells[2, 4]).HorizontalAlignment =
                    XlHAlign.xlHAlignCenter;

                objSheet.Cells[2, 5] = "组织机构";
                objSheet.get_Range(objSheet.Cells[2, 5], objSheet.Cells[2, 5]).Merge(0);
                objSheet.get_Range(objSheet.Cells[2, 5], objSheet.Cells[2, 5]).HorizontalAlignment =
                    XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                string jsBlock = "<script>SetPorgressBar('导出未参加考试人员','" + ((1 * 100) / ((double)objList.Count + 1)).ToString("0.00") +
                          "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                for (int j = 0; j < objList.Count; j++)
                {
                    objSheet.Cells[3 + j, 1] = j + 1;

                    objSheet.Cells[3 + j, 2] = objList[j].EmployeeName;
                    objSheet.get_Range(objSheet.Cells[3 + j, 2], objSheet.Cells[3 + j, 2]).Merge(0);
                    objSheet.get_Range(objSheet.Cells[3 + j, 2], objSheet.Cells[3 + j, 2]).HorizontalAlignment =
                        XlHAlign.xlHAlignCenter;

                    objSheet.Cells[3 + j, 3] = "'" + objList[j].StrWorkNo;
                    objSheet.get_Range(objSheet.Cells[3 + j, 3], objSheet.Cells[3 + j, 3]).Merge(0);
                    objSheet.get_Range(objSheet.Cells[3 + j, 3], objSheet.Cells[3 + j, 3]).HorizontalAlignment =
                        XlHAlign.xlHAlignCenter;

                    objSheet.Cells[3 + j, 4] = objList[j].PostName;
                    objSheet.get_Range(objSheet.Cells[3 + j, 4], objSheet.Cells[3 + j, 4]).Merge(0);
                    objSheet.get_Range(objSheet.Cells[3 + j, 4], objSheet.Cells[3 + j, 4]).HorizontalAlignment =
                        XlHAlign.xlHAlignCenter;

                    objSheet.Cells[3 + j, 5] = objList[j].OrgName;
                    objSheet.get_Range(objSheet.Cells[3 + j, 5], objSheet.Cells[3 + j, 5]).Merge(0);
                    objSheet.get_Range(objSheet.Cells[3 + j, 5], objSheet.Cells[3 + j, 5]).HorizontalAlignment =
                        XlHAlign.xlHAlignCenter;

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('导出未参加考试人员','" +
                              (((1 +j + 1) * 100) / ((double)objList.Count + 1)).ToString("0.00") +
                              "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                }

                objSheet.Columns.AutoFit();

                objApp.Visible = false;

                objbook.Saved = true;
                objbook.SaveCopyAs(filename);

                Response.Write("<script>top.returnValue='" + strName + "';window.close();</script>");
            }
        }



        #region 导出成绩登记表
        private void OutputExcelNew()
        {
            // 根据 ProgressBar.htm 显示进度条界面
            string templateFileName = Path.Combine(Server.MapPath("."), "ProgressBar.htm");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
            System.Threading.Thread.Sleep(200);

            string jsBlock;


            OracleAccess db = new OracleAccess();

            string qsExamId = Request.QueryString.Get("eid").Replace("|", ",");
            DataSet ds = ((DataSet) Session["ExamResultExport"]);
            DataTable dt = ds.Tables[0];
            Hashtable ht=new Hashtable();
            foreach(DataRow dr in dt.Rows)
            {
                if(!ht.ContainsKey(dr["Station_Org_ID"]))
                {
                    ht.Add(dr["Station_Org_ID"].ToString(), dr["Station_Org_Name"].ToString());
                }
            }


            System.Threading.Thread.Sleep(10);
            jsBlock = "<script>SetPorgressBar('导出成绩登记表','" + ((1*100)/((double) dt.Rows.Count + 3)).ToString("0.00") +
                      "'); </script>";
            Response.Write(jsBlock);
            Response.Flush();
            string strSql;


            strSql =
                @"select y.Examinee_ID as Employee_ID,  GetEmployeeAge(d.birthday) YearsOld,d.birthday,
               c.Short_Name||'-'||b.Computer_Room_Name Address,a.Random_Exam_ID
               from Random_Exam_Result y
               left join (select * from Random_Exam_Result_Detail where Random_Exam_ID in (" +
                qsExamId +
                @") and Is_Remove=1) a on y.Random_Exam_Result_ID=a.Random_Exam_Result_ID
               left join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID
               left join Org c on b.Org_ID=c.Org_ID
               left join Employee d on y.Examinee_ID=d.Employee_ID
               where y.Random_Exam_ID in (" +
                qsExamId +
                @") union all 
                select y.Examinee_ID as Employee_ID, GetEmployeeAge(d.birthday) YearsOld,d.birthday,
               c.Short_Name||'-'||b.Computer_Room_Name Address,a.Random_Exam_ID
               from Random_Exam_Result_Temp y
               left join (select * from Random_Exam_Result_Detail_Temp where Random_Exam_ID in (" +
                qsExamId +
                @") and Is_Remove=1) a on y.Random_Exam_Result_ID=a.Random_Exam_Result_ID
               left join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID
               left join Org c on b.Org_ID=c.Org_ID
               left join Employee d on y.Examinee_ID=d.Employee_ID
               where y.Random_Exam_ID in (" +
                qsExamId + ")";
            DataSet dsEmployee = db.RunSqlDataSet(strSql);

            System.Threading.Thread.Sleep(10);
            jsBlock = "<script>SetPorgressBar('导出成绩登记表','" + ((2*100)/((double) dt.Rows.Count + 3)).ToString("0.00") +
                      "'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            string flodername = DateTime.Today.ToString("yyyyMMddHHmmss");

            string strFloderPath = Server.MapPath("/RailExamBao/Excel/" +flodername );
            if (!Directory.Exists(strFloderPath))
            {
                Directory.CreateDirectory(strFloderPath);
            }

            int proindex = 1;
            foreach (DictionaryEntry item in ht)
            {
                string strExamName = "";
                string strExamTime = "";
                DateTime date;
                strExamName = DateTime.Today.ToString("yyyy-MM-dd") + "【" + item.Value + "】成绩登记表";

                string filename = strFloderPath + "\\" + strExamName + ".xls";

                if (File.Exists(filename.ToString()))
                {
                    File.Delete(filename.ToString());
                }

                HSSFWorkbook workbook = new HSSFWorkbook();
                ISheet sheet = workbook.CreateSheet();

                #region 右击文件 属性信息

                {
                    DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                    dsi.Company = "NPOI";
                    workbook.DocumentSummaryInformation = dsi;

                    SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                    si.Author = "文件作者信息"; //填加xls文件作者信息
                    si.ApplicationName = "创建程序信息"; //填加xls文件创建程序信息
                    si.LastAuthor = "最后保存者信息"; //填加xls文件最后保存者信息
                    si.Comments = "作者信息"; //填加xls文件作者信息
                    si.Title = "标题信息"; //填加xls文件标题信息
                    si.Subject = "主题信息"; //填加文件主题信息
                    si.CreateDateTime = DateTime.Now;
                    workbook.SummaryInformation = si;
                }

                #endregion

                ICellStyle dateStyle = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

                //取得列宽
                string[] arrColWidth = {
                                           "序号", "姓名", "岗位证书编号", "组织机构（车间）", "职名","考试名称", "考试时间", "考试用时", "出生日期", "年龄", "考试地点",
                                           "分数"
                                       };

                int rowIndex = 0;
                decimal decScore = 0;

                DataRow[] dts = dt.Select("Station_Org_ID='" + item.Key+"'");

                for (int j = 0; j < dts.Length; j++)
                {
                    #region 新建表，填充表头，填充列头，样式

                    if (rowIndex == 65535 || rowIndex == 0)
                    {
                        if (rowIndex != 0)
                        {
                            sheet = workbook.CreateSheet();
                            decScore = 0;
                        }

                        #region 表头及样式

                        {
                            IRow headerRow = sheet.CreateRow(0);
                            headerRow.HeightInPoints = 25;
                            headerRow.CreateCell(0).SetCellValue(strExamName + "学员成绩登记表");

                            ICellStyle headStyle = workbook.CreateCellStyle();
                            headStyle.Alignment = HorizontalAlignment.Center;
                            IFont font = workbook.CreateFont();
                            font.FontHeightInPoints = 20;
                            font.Boldweight = 700;
                            headStyle.SetFont(font);
                            headerRow.GetCell(0).CellStyle = headStyle;
                            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, arrColWidth.Length - 1));

                            headerRow = sheet.CreateRow(1);
                            headerRow.CreateCell(0).SetCellValue("导出日期： " + DateTime.Today.ToString("yyyy-MM-dd"));
                            headStyle = workbook.CreateCellStyle();
                            headStyle.Alignment = HorizontalAlignment.Right;
                            font = workbook.CreateFont();
                            font.FontHeightInPoints = 10;
                            headStyle.SetFont(font);
                            headerRow.GetCell(0).CellStyle = headStyle;
                            sheet.AddMergedRegion(new CellRangeAddress(1, 1, 0, arrColWidth.Length - 1));
                        }

                        #endregion

                        #region 列头及样式

                        {
                            IRow headerRow = sheet.CreateRow(2);
                            ICellStyle headStyle = workbook.CreateCellStyle();
                            headStyle.Alignment = HorizontalAlignment.Center;
                            NPOI.SS.UserModel.IFont font = workbook.CreateFont();
                            font.FontHeightInPoints = 10;
                            font.Boldweight = 700;
                            headStyle.SetFont(font);
                            for (int x = 0; x < arrColWidth.Length; x++)
                            {
                                headerRow.CreateCell(x).SetCellValue(arrColWidth[x]);
                                headerRow.GetCell(x).CellStyle = headStyle;
                            }
                        }

                        #endregion

                        rowIndex = 3;
                    }

                    #endregion

                    #region 填充内容

                    IRow dataRow = sheet.CreateRow(rowIndex);

                    for (int x = 0; x < arrColWidth.Length; x++)
                    {
                        dataRow.CreateCell(x);
                    }

                    DataRow dr = dts[j];
                    int k = j + 1;

                    dataRow.Cells[0].SetCellValue(k.ToString());

                    dataRow.Cells[1].SetCellValue(dr["Examinee_Name"].ToString());
                    dataRow.Cells[2].SetCellValue(dr["Work_No"].ToString());
                    dataRow.Cells[3].SetCellValue(dr["Org_Name"].ToString());
                    dataRow.Cells[4].SetCellValue(dr["Post_Name"].ToString());
                    dataRow.Cells[5].SetCellValue(dr["Exam_Name"].ToString());
                    dataRow.Cells[6].SetCellValue(dr["Exam_Time_Str"].ToString());

                    int examTime = Math.Abs(Convert.ToInt32(dr["Exam_Time"].ToString()));
                    string strTime = examTime/60 + "分" + examTime%60 + "秒";
                    dataRow.Cells[7].SetCellValue(strTime);
                    DataRow[] drEmployees =
                        dsEmployee.Tables[0].Select("Employee_ID=" + dt.Rows[j]["Examinee_ID"] + " and Random_Exam_ID=" +
                                                    dt.Rows[j]["Random_Exam_ID"]);
                    if (drEmployees.Length > 0)
                    {
                        DataRow drs = drEmployees[0];
                        dataRow.Cells[8].SetCellValue(drs["birthday"] != DBNull.Value
                                                          ? Convert.ToDateTime(drs["birthday"]).ToString("yyyy-MM-dd")
                                                          : "");
                        dataRow.Cells[9].SetCellValue(drs["YearsOld"].ToString());
                        dataRow.Cells[10].SetCellValue(drs["Address"].ToString());
                    }
                    decScore += decimal.Parse(dr["Score"].ToString());
                    dataRow.Cells[11].SetCellValue(dr["Score"].ToString());

                    #endregion

                    #region 平均成绩

                    if (j == dts.Length- 1)
                    {
                        int index = 3 + dts.Length;
                        IRow headerRow = sheet.CreateRow(index);
                        ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.Right;
                        NPOI.SS.UserModel.IFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        headStyle.SetFont(font);
                        headerRow.CreateCell(0).SetCellValue("平均分：");
                        headerRow.GetCell(0).CellStyle = headStyle;

                        decimal dec1 = 0;
                        if (dts.Length > 0)
                        {
                            dec1 = decScore / dts.Length;
                        }
                        headerRow.CreateCell(1).SetCellValue(dec1.ToString("0.00"));
                        headerRow.GetCell(1).CellStyle = headStyle;
                        sheet.AddMergedRegion(new CellRangeAddress(index, index, 1, arrColWidth.Length - 1));

                        //列宽自适应，只对英文和数字有效
                        for (int i = 0; i < arrColWidth.Length; i++)
                        {
                            sheet.AutoSizeColumn(i);
                        }
                        //获取当前列的宽度，然后对比本列的长度，取最大值
                        for (int columnNum = 1; columnNum < arrColWidth.Length; columnNum++)
                        {
                            int columnWidth = sheet.GetColumnWidth(columnNum)/256;
                            for (int rowNum = 1; rowNum <= sheet.LastRowNum; rowNum++)
                            {
                                IRow currentRow;
                                //当前行未被使用过
                                if (sheet.GetRow(rowNum) == null)
                                {
                                    currentRow = sheet.CreateRow(rowNum);
                                }
                                else
                                {
                                    currentRow = sheet.GetRow(rowNum);
                                }

                                if (currentRow.GetCell(columnNum) != null)
                                {
                                    ICell currentCell = currentRow.GetCell(columnNum);
                                    int length = Encoding.Default.GetBytes(currentCell.ToString()).Length;
                                    if (columnWidth < length)
                                    {
                                        columnWidth = length;
                                    }
                                }
                            }
                            sheet.SetColumnWidth(columnNum, columnWidth*256);
                        }
                    }

                    #endregion

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('导出成绩登记表','" +
                              ((double)((2 + proindex) * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
                              "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    proindex++;

                    rowIndex++;

                }

                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.Write(ms);
                    ms.Flush();
                    ms.Position = 0;

                    using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                    {
                        byte[] data = ms.ToArray();
                        fs.Write(data, 0, data.Length);
                        fs.Flush();
                    }
                }
            }

            Response.Write("<script>top.returnValue='"+flodername+"';window.close();</script>");

        }
        #endregion
    }
}
