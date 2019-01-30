using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using Excel;
using RailExamWebApp.Common.Class;
using DataTable = System.Data.DataTable;

namespace RailExamWebApp.RandomExamTai
{
	public partial class TJ_ExportTemplate : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString.Get("type").Equals("EmployeeWorkerByOrg"))
				OutPutEmployeeWorkerByOrg();
			if (Request.QueryString.Get("type").Equals("EmployeeWorkerPostByOrg"))
				OutPutEmployeeWorkerPostByOrg();
			if(Request.QueryString.Get("type").Equals("EmployeeWorkerGroupHeaderByOrg"))
				OutPutEmployeeWorkerGroupHeaderByOrg();
			if (Request.QueryString.Get("type").Equals("EmployeeWorkerStructureByOrg"))
				OutPutEmployeeWorkerStructureByOrg();
			if (Request.QueryString.Get("type").Equals("EmployeeWorkerStructureByPost"))
				OutPutEmployeeWorkerStructureByPost();
			if (Request.QueryString.Get("type").Equals("EmployeeWorkerByEducation"))
				OutPutEmployeeWorkerByEducation();
            if (Request.QueryString.Get("type").Equals("EmployeePrize"))
                OutPutEmployeeMatch();
            if (Request.QueryString.Get("type").Equals("EmployeeOther"))
                OutPutEmployeeOther();
		}

		/// <summary>
		/// 导出各单位工人总数统计信息
		/// </summary>
		private void OutPutEmployeeWorkerByOrg()
		{
			if (Session["EmployeeWorkerByOrg"] != null)
			{
				DataTable dt = ((DataSet) Session["EmployeeWorkerByOrg"]).Tables[0];
				Session.Remove("EmployeeWorkerByOrg");

				string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
				StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
				string html = reader.ReadToEnd();
				reader.Close();
				Response.Write(html);
				Response.Flush();
				System.Threading.Thread.Sleep(200);

				string jsBlock;
				System.Threading.Thread.Sleep(10);
				jsBlock = "<script>SetPorgressBar('各单位工人总数统计信息','" + ((1 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
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
					filename = Server.MapPath("/RailExamBao/Excel/各单位工人总数统计信息.xls");

					if (File.Exists(filename.ToString()))
					{
						File.Delete(filename.ToString());
					}
					//将所得到的表的列名,赋值给单元格   

					int index = 1;
					objSheet.Cells[index, 1] = "序号";
					range = objSheet.get_Range(objSheet.Cells[index, 1], objSheet.Cells[index, 1]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 2] = "单位";
					range = objSheet.get_Range(objSheet.Cells[index, 2], objSheet.Cells[index, 2]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 3] = "总数";
					range = objSheet.get_Range(objSheet.Cells[index, 3], objSheet.Cells[index, 3]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 4] = "在岗人数";
					range = objSheet.get_Range(objSheet.Cells[index, 4], objSheet.Cells[index, 4]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 5] = "不在岗人数";
					range = objSheet.get_Range(objSheet.Cells[index, 5], objSheet.Cells[index, 5]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 6] = "在册人数";
					range = objSheet.get_Range(objSheet.Cells[index, 6], objSheet.Cells[index, 6]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 7] = "不在册人数";
					range = objSheet.get_Range(objSheet.Cells[index, 7], objSheet.Cells[index, 7]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

                    objSheet.Cells[index, 8] = "在岗工人人数";
                    range = objSheet.get_Range(objSheet.Cells[index, 8], objSheet.Cells[index, 8]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;
                    range.Merge(0);

                    objSheet.Cells[index, 9] = "在岗工人无照片人数";
                    range = objSheet.get_Range(objSheet.Cells[index, 9], objSheet.Cells[index, 9]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;
                    range.Merge(0);

                    objSheet.Cells[index, 10] = "在岗工人无指纹人数";
                    range = objSheet.get_Range(objSheet.Cells[index, 10], objSheet.Cells[index, 10]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;
                    range.Merge(0);

					index++;

					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('各单位工人总数统计信息','" + ((2 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
							  "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();

					//同样方法处理数据  
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						objSheet.Cells[index + i, 1] = i + 1;
						 

						objSheet.Cells[index + i, 2] = dt.Rows[i][3];
					 

						objSheet.Cells[index + i, 3] = dt.Rows[i][4];
						 

						objSheet.Cells[index + i, 4] = dt.Rows[i][5];
					 
						objSheet.Cells[index + i, 5] = dt.Rows[i][6];

						objSheet.Cells[index + i, 6] = dt.Rows[i][7];

						objSheet.Cells[index + i, 7] = dt.Rows[i][8];

                        objSheet.Cells[index + i, 8] = dt.Rows[i][9];

                        objSheet.Cells[index + i, 9] = dt.Rows[i][10];

                        objSheet.Cells[index + i, 10] = dt.Rows[i][11];
						

						System.Threading.Thread.Sleep(10);
						jsBlock = "<script>SetPorgressBar('各单位工人总数统计信息','" +
								  (((2 + i + 1) * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
								  "'); </script>";
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
			}

			Response.Write("<script>top.returnValue='true';window.close();</script>");
		}

		/// <summary>
		/// 导出各单位工种人数统计信息
		/// </summary>
		private void OutPutEmployeeWorkerPostByOrg()
		{
			if (Session["EmployeeWorkerPostByOrg"] != null)
			{
				DataTable dt = (DataTable) Session["EmployeeWorkerPostByOrg"];
				Session.Remove("EmployeeWorkerPostByOrg");

				string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
				StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
				string html = reader.ReadToEnd();
				reader.Close();
				Response.Write(html);
				Response.Flush();
				System.Threading.Thread.Sleep(200);

				string jsBlock;
				System.Threading.Thread.Sleep(10);
				jsBlock = "<script>SetPorgressBar('各单位工种人数统计信息','" + ((1 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
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
					filename = Server.MapPath("/RailExamBao/Excel/各单位工种人数统计信息.xls");

					if (File.Exists(filename.ToString()))
					{
						File.Delete(filename.ToString());
					}
					//将所得到的表的列名,赋值给单元格   

					int index = 1;

					objSheet.Cells[index, 1] = "序号";
					range = objSheet.get_Range(objSheet.Cells[index, 1], objSheet.Cells[index, 1]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					for (int i = 0; i < dt.Columns.Count; i++)
					{
						if (i == 0)
							objSheet.Cells[index, i + 2] = "单位";
						else
							objSheet.Cells[index, i + 2] = dt.Columns[i].ColumnName.Replace("#", "、").Replace("AA", "(").Replace("BB", ")");
						range = objSheet.get_Range(objSheet.Cells[index, i + 2], objSheet.Cells[index, i + 2]);
						range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
						range.Font.Bold = true;
						range.Merge(0);
					}
					
					index++;

					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('各单位工种人数统计信息','" + ((2 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
							  "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();

					//同样方法处理数据  
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						objSheet.Cells[index + i, 1] = i + 1;

						for (int j = 0; j < dt.Columns.Count; j++)
						{
							objSheet.Cells[index + i, j+2] = dt.Rows[i][j];

						}
						
						 
						System.Threading.Thread.Sleep(10);
						jsBlock = "<script>SetPorgressBar('各单位工种人数统计信息','" +
								  (((2 + i + 1) * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
								  "'); </script>";
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
			}

			Response.Write("<script>top.returnValue='true';window.close();</script>");
		}

		/// <summary>
		/// 导出各单位班组长人数统计信息
		/// </summary>
		private void OutPutEmployeeWorkerGroupHeaderByOrg()
		{
			if (Session["EmployeeWorkerGroupHeaderByOrg"] != null)
			{
				DataTable dt = (DataTable)Session["EmployeeWorkerGroupHeaderByOrg"];
				Session.Remove("EmployeeWorkerGroupHeaderByOrg");

				string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
				StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
				string html = reader.ReadToEnd();
				reader.Close();
				Response.Write(html);
				Response.Flush();
				System.Threading.Thread.Sleep(200);

				string jsBlock;
				System.Threading.Thread.Sleep(10);
				jsBlock = "<script>SetPorgressBar('各单位班组长人数统计信息','" + ((1 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
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
					filename = Server.MapPath("/RailExamBao/Excel/各单位班组长人数统计信息.xls");

					if (File.Exists(filename.ToString()))
					{
						File.Delete(filename.ToString());
					}
					//将所得到的表的列名,赋值给单元格   

					int index = 1;

					objSheet.Cells[index, 1] = "序号";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 1], objSheet.Cells[index + 1, 1]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);
			 
					objSheet.Cells[index, 2] = "单位";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 2], objSheet.Cells[index + 1, 2]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 3] = "班组长总数";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 3], objSheet.Cells[index + 1, 3]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);
						 
					objSheet.Cells[index,  4] = "其中";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 4], objSheet.Cells[index, 4+(dt.Columns.Count-3)]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);
							 

					index++;

					for (int i = 2; i < dt.Columns.Count; i++)
					{
						objSheet.Cells[index, i + 2] = dt.Columns[i].ColumnName;
						range = objSheet.get_Range(objSheet.Cells[index, i + 2], objSheet.Cells[index, i + 2]);
						range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
						range.Font.Bold = true;
						range.Merge(0);

					}

					index++;

					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('各单位班组长人数统计信息','" + ((2 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
							  "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();

					//同样方法处理数据  
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						objSheet.Cells[index + i, 1] = i + 1;

						for (int j = 0; j < dt.Columns.Count; j++)
						{
							objSheet.Cells[index + i, j + 2] = dt.Rows[i][j];
		 
						}


						System.Threading.Thread.Sleep(10);
						jsBlock = "<script>SetPorgressBar('各单位班组长人数统计信息','" +
								  (((2 + i + 1) * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
								  "'); </script>";
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
			}

			Response.Write("<script>top.returnValue='true';window.close();</script>");
		}

		/// <summary>
		/// 导出各单位工人结构统计信息
		/// </summary>
		private void OutPutEmployeeWorkerStructureByOrg()
		{
			if (Session["EmployeeWorkerStructureByOrg"] != null)
			{
				DataTable dt = (DataTable)Session["EmployeeWorkerStructureByOrg"];
				Session.Remove("EmployeeWorkerStructureByOrg");

				string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
				StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
				string html = reader.ReadToEnd();
				reader.Close();
				Response.Write(html);
				Response.Flush();
				System.Threading.Thread.Sleep(200);

				string jsBlock;
				System.Threading.Thread.Sleep(10);
				jsBlock = "<script>SetPorgressBar('各单位工人结构统计信息','" + ((1 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
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
					filename = Server.MapPath("/RailExamBao/Excel/各单位工人结构统计信息.xls");

					if (File.Exists(filename.ToString()))
					{
						File.Delete(filename.ToString());
					}
					//将所得到的表的列名,赋值给单元格   

					int eduCount = Request.QueryString.Get("eduCount") == "" ? 0 : Convert.ToInt32(Request.QueryString.Get("eduCount"));
					int techCount=Request.QueryString.Get("techCount") == "" ? 0 : Convert.ToInt32(Request.QueryString.Get("techCount"));

					int index = 1;

					objSheet.Cells[index, 1] = "序号";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 1], objSheet.Cells[index + 1, 1]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 2] = "单位";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 2], objSheet.Cells[index + 1, 2]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 3] = "年龄结构";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 3], objSheet.Cells[index,11]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 11] = "文化程度";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 12], objSheet.Cells[index, 11 + eduCount]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 11 + eduCount] = "职业技能等级";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 12 + eduCount], objSheet.Cells[index, 11 + eduCount+techCount]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					index++;

					for (int i = 1; i < dt.Columns.Count; i++)
					{
						string colName = "";
						if (dt.Columns[i].ColumnName.Equals("A"))
							colName = "25岁及以下";
						else if (dt.Columns[i].ColumnName.Equals("B"))
							colName = "26岁至30岁";
						else if (dt.Columns[i].ColumnName.Equals("C"))
							colName = "31岁至35岁";
						else if (dt.Columns[i].ColumnName.Equals("D"))
							colName = "36岁至40岁";
						else if (dt.Columns[i].ColumnName.Equals("E"))
							colName = "41岁至45岁";
						else if (dt.Columns[i].ColumnName.Equals("F"))
							colName = "46岁至50岁";
						else if (dt.Columns[i].ColumnName.Equals("G"))
							colName = "51岁至55岁";
						else if (dt.Columns[i].ColumnName.Equals("H"))
							colName = "56岁至60岁";
                        else if (dt.Columns[i].ColumnName.Equals("I"))
                            colName = "60岁以上";
						else
							colName = dt.Columns[i].ColumnName;
						objSheet.Cells[index, i + 2] = colName;
						range = objSheet.get_Range(objSheet.Cells[index, i + 2], objSheet.Cells[index, i + 2]);
						range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
						range.Font.Bold = true;
						range.Merge(0);
					}

					index++;

					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('各单位工人结构统计信息','" + ((2 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
							  "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();

					//同样方法处理数据  
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						objSheet.Cells[index + i, 1] = i + 1;

						for (int j = 0; j < dt.Columns.Count; j++)
						{
							objSheet.Cells[index + i, j + 2] = dt.Rows[i][j];

						}


						System.Threading.Thread.Sleep(10);
						jsBlock = "<script>SetPorgressBar('各单位工人结构统计信息','" +
								  (((2 + i + 1) * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
								  "'); </script>";
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
			}

			Response.Write("<script>top.returnValue='true';window.close();</script>");
		}

		/// <summary>
		/// 导出各工种工人结构统计信息
		/// </summary>
		private void OutPutEmployeeWorkerStructureByPost()
		{
			if (Session["EmployeeWorkerStructureByPost"] != null)
			{
				DataTable dt = (DataTable)Session["EmployeeWorkerStructureByPost"];
				Session.Remove("EmployeeWorkerStructureByPost");

				string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
				StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
				string html = reader.ReadToEnd();
				reader.Close();
				Response.Write(html);
				Response.Flush();
				System.Threading.Thread.Sleep(200);

				string jsBlock;
				System.Threading.Thread.Sleep(10);
				jsBlock = "<script>SetPorgressBar('各工种工人结构统计信息','" + ((1 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
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
					filename = Server.MapPath("/RailExamBao/Excel/各工种工人结构统计信息.xls");

					if (File.Exists(filename.ToString()))
					{
						File.Delete(filename.ToString());
					}
					//将所得到的表的列名,赋值给单元格   

					int eduCount = Request.QueryString.Get("eduCount") == "" ? 0 : Convert.ToInt32(Request.QueryString.Get("eduCount"));
					int techCount = Request.QueryString.Get("techCount") == "" ? 0 : Convert.ToInt32(Request.QueryString.Get("techCount"));

					int index = 1;

					objSheet.Cells[index, 1] = "序号";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 1], objSheet.Cells[index + 1, 1]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

                    objSheet.Cells[index, 2] = "工种";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 2], objSheet.Cells[index + 1, 2]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 3] = "年龄结构";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 3], objSheet.Cells[index, 11]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 11] = "文化程度";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 12], objSheet.Cells[index, 11 + eduCount]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 11 + eduCount] = "职业技能等级";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 12 + eduCount], objSheet.Cells[index, 11 + eduCount + techCount]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					index++;

					for (int i = 1; i < dt.Columns.Count; i++)
					{
						string colName = "";
						if (dt.Columns[i].ColumnName.Equals("A"))
							colName = "25岁及以下";
						else if (dt.Columns[i].ColumnName.Equals("B"))
							colName = "26岁至30岁";
						else if (dt.Columns[i].ColumnName.Equals("C"))
							colName = "31岁至35岁";
						else if (dt.Columns[i].ColumnName.Equals("D"))
							colName = "36岁至40岁";
						else if (dt.Columns[i].ColumnName.Equals("E"))
							colName = "41岁至45岁";
						else if (dt.Columns[i].ColumnName.Equals("F"))
							colName = "46岁至50岁";
						else if (dt.Columns[i].ColumnName.Equals("G"))
							colName = "51岁至55岁";
						else if (dt.Columns[i].ColumnName.Equals("H"))
							colName = "56岁至60岁";
                        else if (dt.Columns[i].ColumnName.Equals("J"))
                            colName = "60岁以上";
						else
							colName = dt.Columns[i].ColumnName;
						objSheet.Cells[index, i + 2] = colName;
						range = objSheet.get_Range(objSheet.Cells[index, i + 2], objSheet.Cells[index, i + 2]);
						range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
						range.Font.Bold = true;
						range.Merge(0);

					}

					index++;

					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('各工种工人结构统计信息','" + ((2 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
							  "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();

					//同样方法处理数据  
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						objSheet.Cells[index + i, 1] = i + 1;

						for (int j = 0; j < dt.Columns.Count; j++)
						{
							objSheet.Cells[index + i, j + 2] = dt.Rows[i][j];

						}


						System.Threading.Thread.Sleep(10);
						jsBlock = "<script>SetPorgressBar('各工种工人结构统计信息','" +
								  (((2 + i + 1) * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
								  "'); </script>";
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
			}

			Response.Write("<script>top.returnValue='true';window.close();</script>");
		}

		/// <summary>
		/// 导出各单位职教工作人员统计信息
		/// </summary>
		private void OutPutEmployeeWorkerByEducation()
		{
			if (Session["EmployeeWorkerByEducation"] != null)
			{
				DataTable dt = (DataTable)Session["EmployeeWorkerByEducation"];
				Session.Remove("EmployeeWorkerByEducation");

				string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
				StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
				string html = reader.ReadToEnd();
				reader.Close();
				Response.Write(html);
				Response.Flush();
				System.Threading.Thread.Sleep(200);

				string jsBlock;
				System.Threading.Thread.Sleep(10);
				jsBlock = "<script>SetPorgressBar('各单位职教工作人员统计信息','" + ((1 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
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
					filename = Server.MapPath("/RailExamBao/Excel/各单位职教工作人员统计信息.xls");

					if (File.Exists(filename.ToString()))
					{
						File.Delete(filename.ToString());
					}
					//将所得到的表的列名,赋值给单元格   

					int eduCount = Request.QueryString.Get("eduCount") == "" ? 0 : Convert.ToInt32(Request.QueryString.Get("eduCount"));
					int techCount = Request.QueryString.Get("techCount") == ""
					                	? 0
					                	: Convert.ToInt32(Request.QueryString.Get("techCount"));
					int techTitleCount = Request.QueryString.Get("techTitleCount") == ""
					                     	? 0
					                     	: Convert.ToInt32(Request.QueryString.Get("techTitleCount"));
					int eduTypeCount = Request.QueryString.Get("eduType") == ""
					                   	? 0
					                   	: Convert.ToInt32(Request.QueryString.Get("eduType"));

					int index = 1;

					objSheet.Cells[index, 1] = "序号";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 1], objSheet.Cells[index + 1, 1]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 2] = "单位";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 2], objSheet.Cells[index + 1, 3]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 4] = "总计(人)";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 4], objSheet.Cells[index + 1, 4]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 5] = "年龄结构";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 5], objSheet.Cells[index, 8]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 9] = "学历结构";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 9], objSheet.Cells[index, 8 + eduCount]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);


					objSheet.Cells[index, 9 + eduCount] = "技术职称";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 9 + eduCount],
					                                 objSheet.Cells[index, 8 + eduCount + techTitleCount]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 9 + eduCount + techTitleCount] = "技能等级";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 9 + eduCount + techTitleCount],
					                                 objSheet.Cells[index, 8 + eduCount + techTitleCount + techCount]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					index++;

					for (int i = 1; i < dt.Columns.Count; i++)
					{
						string colName = "";
						if (dt.Columns[i].ColumnName.Equals("A"))
							colName = "30岁及以下";
						else if (dt.Columns[i].ColumnName.Equals("B"))
							colName = "31-40岁";
						else if (dt.Columns[i].ColumnName.Equals("C"))
							colName = "41-50岁";
						else if (dt.Columns[i].ColumnName.Equals("D"))
							colName = "51岁及以上";
						 
						else
							colName = dt.Columns[i].ColumnName.Replace("1","");
						objSheet.Cells[index, i + 2] = colName;
						range = objSheet.get_Range(objSheet.Cells[index, i + 2], objSheet.Cells[index, i + 2]);
						range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
						range.Font.Bold = true;
						range.Merge(0);

					}

					index++;

					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('各单位职教工作人员统计信息','" + ((2 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
							  "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();

					//同样方法处理数据  
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						objSheet.Cells[index + i, 1] = i + 1;

						for (int j = 0; j < dt.Columns.Count; j++)
						{

							if ((i == 0 && j == 0) || (i % eduTypeCount == 0 && j == 0))
							{
								objSheet.Cells[index + i, j + 2] = dt.Rows[i][j];
								range = objSheet.get_Range(objSheet.Cells[index + i, j + 2], objSheet.Cells[index + (eduTypeCount-1) + i, j + 2]);
								range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
								range.Merge(0);
							}
							else 
							{
								objSheet.Cells[index + i, j + 2] = dt.Rows[i][j];
							}
						}


						System.Threading.Thread.Sleep(10);
						jsBlock = "<script>SetPorgressBar('各单位职教工作人员统计信息','" +
								  (((2 + i + 1) * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
								  "'); </script>";
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
			}

			Response.Write("<script>top.returnValue='true';window.close();</script>");
		}

        private  void OutPutEmployeeMatch()
        {
            if (Session["EmployeePrize"] != null)
            {
                DataTable dt = (DataTable)Session["EmployeePrize"];
                Session.Remove("EmployeePrize");

                string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
                StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
                string html = reader.ReadToEnd();
                reader.Close();
                Response.Write(html);
                Response.Flush();
                System.Threading.Thread.Sleep(200);

                string jsBlock;
                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('各单位技能竞赛统计','" + ((1 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
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
                    filename = Server.MapPath("/RailExamBao/Excel/各单位技能竞赛统计.xls");

                    if (File.Exists(filename.ToString()))
                    {
                        File.Delete(filename.ToString());
                    }
                    //将所得到的表的列名,赋值给单元格   
                    int index = 1;
                    objSheet.Cells[index, 1] = "技能竞赛总体情况统计表"; 
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, 1], objSheet.Cells[index + 1, dt.Columns.Count]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;
                    range.Font.Size = 18;
                    range.Merge(0);

                    index = index + 2;
                    objSheet.Cells[index, 1] =DateTime.Today.ToString("yyyy-MM-dd");
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, 1], objSheet.Cells[index, dt.Columns.Count]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignRight;
                    range.Font.Size = 10;
                    range.Merge(0);

                    index++;
                    objSheet.Cells[index, 1] = "序号";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, 1], objSheet.Cells[index + 1, 1]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    objSheet.Cells[index, 2] = "单位";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, 2], objSheet.Cells[index + 1, 2]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    objSheet.Cells[index, 3] = "举办单位";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, 3], objSheet.Cells[index, 7]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    objSheet.Cells[index, 8] = "举办单位";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, 8], objSheet.Cells[index, dt.Columns.Count]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    int row = 3;
                    index++;
                    objSheet.Cells[index, row] = "合计";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    row++;
                    objSheet.Cells[index, row] = "全国";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    row++;
                    objSheet.Cells[index, row] = "铁道部";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    row++;
                    objSheet.Cells[index, row] = "铁路局";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    row++;
                    objSheet.Cells[index, row] = "站段";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    row++;
                    objSheet.Cells[index, row] = "合计";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    row++;
                    objSheet.Cells[index, row] = "全能";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    row++;
                    objSheet.Cells[index, row] = "团体";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    row++;
                    objSheet.Cells[index, row] = "合计";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    row++;
                    objSheet.Cells[index, row] = "其他";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);


                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('技能竞赛总体情况统计表','" + ((2 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
                              "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();

                    index++;
                    //同样方法处理数据  
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            objSheet.Cells[index + i, j+1 ] = dt.Rows[i][j];
                            range = objSheet.get_Range(objSheet.Cells[index + i, j + 1], objSheet.Cells[index + i, j + 1]);
                            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                            range.Merge(0);
                        }


                        System.Threading.Thread.Sleep(10);
                        jsBlock = "<script>SetPorgressBar('技能竞赛总体情况统计表','" +
                                  (((2 + i + 1) * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
                                  "'); </script>";
                        Response.Write(jsBlock);
                        Response.Flush();
                    }
                    objSheet.Cells.Columns.AutoFit();
                    range = objSheet.get_Range(objSheet.Cells[1, 1], objSheet.Cells[index + dt.Rows.Count-1, dt.Columns.Count]);
                    range.Borders.LineStyle = 1;

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

        private void OutPutEmployeeOther()
        {
            if (Session["EmployeeOther"] != null)
            {
                DataTable dt = (DataTable)Session["EmployeeOther"];
                Session.Remove("EmployeeOther");

                string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
                StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
                string html = reader.ReadToEnd();
                reader.Close();
                Response.Write(html);
                Response.Flush();
                System.Threading.Thread.Sleep(200);

                string jsBlock;
                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('各单位技其他持证情况统计','" + ((1 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
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
                    filename = Server.MapPath("/RailExamBao/Excel/各单位技其他持证情况统计.xls");

                    if (File.Exists(filename.ToString()))
                    {
                        File.Delete(filename.ToString());
                    }
                    //将所得到的表的列名,赋值给单元格   
                    int index = 1;
                    objSheet.Cells[index, 1] = "各单位技其他持证情况统计";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, 1], objSheet.Cells[index + 1, dt.Columns.Count]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;
                    range.Font.Size = 18;
                    range.Merge(0);

                    index = index + 2;
                    objSheet.Cells[index, 1] = DateTime.Today.ToString("yyyy-MM-dd");
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, 1], objSheet.Cells[index, dt.Columns.Count]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignRight;
                    range.Font.Size = 10;
                    range.Merge(0);

                    index++;
                    objSheet.Cells[index, 1] = "序号";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, 1], objSheet.Cells[index + 2, 1]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    objSheet.Cells[index, 2] = "单位";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, 2], objSheet.Cells[index + 2, 2]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    objSheet.Cells[index, 3] = "举办单位";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, 3], objSheet.Cells[index, dt.Columns.Count]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    index++;
                    objSheet.Cells[index, 3] = "合计";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, 3], objSheet.Cells[index+1, 3]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    OracleAccess access = new OracleAccess();
                    string strSql = "select a.* from ZJ_CERTIFICATE_LEVEL a "
                                  + "inner join ZJ_CERTIFICATE c on a.CERTIFICATE_ID=c.CERTIFICATE_ID "
                                  + " order by c.CERTIFICATE_ID,a.ORDER_INDEX";
                    DataTable dtLevel = access.RunSqlDataSet(strSql).Tables[0];

                    strSql = "select * from  ZJ_CERTIFICATE order by CERTIFICATE_ID";
                    DataTable dtCer = access.RunSqlDataSet(strSql).Tables[0];

                    int row = 4;
                    for (int i = 0; i < dtCer.Rows.Count; i++)
                    {
                        DataRow[] drs = dtLevel.Select("CERTIFICATE_ID=" + dtCer.Rows[i]["CERTIFICATE_ID"]);

                        objSheet.Cells[index, row] = dtCer.Rows[i]["CERTIFICATE_Name"].ToString();
                        range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row+drs.Length-1]);
                        range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        range.Merge(0);

                        row = row + drs.Length;                               
                    }

                    index++;
                    row = 4;
                    for (int i = 0; i < dtLevel.Rows.Count; i++)
                    {
                        objSheet.Cells[index, row] = dtLevel.Rows[i]["CERTIFICATE_LEVEL_Name"].ToString();
                        range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row]);
                        range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        range.Merge(0);

                        row++;
                    }

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('各单位技其他持证情况统计','" + ((2 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
                              "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();

                    index++;
                    //同样方法处理数据  
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            objSheet.Cells[index + i, j + 1] = dt.Rows[i][j];
                            range = objSheet.get_Range(objSheet.Cells[index + i, j + 1], objSheet.Cells[index + i, j + 1]);
                            range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                            range.Merge(0);
                        }


                        System.Threading.Thread.Sleep(10);
                        jsBlock = "<script>SetPorgressBar('各单位技其他持证情况统计','" +
                                  (((2 + i + 1) * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
                                  "'); </script>";
                        Response.Write(jsBlock);
                        Response.Flush();
                    }
                    objSheet.Cells.Columns.AutoFit();

                    range = objSheet.get_Range(objSheet.Cells[1, 1], objSheet.Cells[index + dt.Rows.Count - 1, dt.Columns.Count]);
                    range.Borders.LineStyle = 1;

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
	}
}
