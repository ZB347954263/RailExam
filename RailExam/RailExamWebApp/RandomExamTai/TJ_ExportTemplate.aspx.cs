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
		/// ��������λ��������ͳ����Ϣ
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
				jsBlock = "<script>SetPorgressBar('����λ��������ͳ����Ϣ','" + ((1 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
						  "'); </script>";
				Response.Write(jsBlock);
				Response.Flush();

				Excel.Application objApp = new Excel.ApplicationClass();
				Excel.Workbooks objbooks = objApp.Workbooks;
				Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
				Excel.Worksheet objSheet = (Excel.Worksheet)objbook.Worksheets[1]; //ȡ��sheet1 
				Excel.Range range;
				string filename = "";

				try
				{
					//����.xls�ļ�����·���� 
					filename = Server.MapPath("/RailExamBao/Excel/����λ��������ͳ����Ϣ.xls");

					if (File.Exists(filename.ToString()))
					{
						File.Delete(filename.ToString());
					}
					//�����õ��ı������,��ֵ����Ԫ��   

					int index = 1;
					objSheet.Cells[index, 1] = "���";
					range = objSheet.get_Range(objSheet.Cells[index, 1], objSheet.Cells[index, 1]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 2] = "��λ";
					range = objSheet.get_Range(objSheet.Cells[index, 2], objSheet.Cells[index, 2]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 3] = "����";
					range = objSheet.get_Range(objSheet.Cells[index, 3], objSheet.Cells[index, 3]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 4] = "�ڸ�����";
					range = objSheet.get_Range(objSheet.Cells[index, 4], objSheet.Cells[index, 4]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 5] = "���ڸ�����";
					range = objSheet.get_Range(objSheet.Cells[index, 5], objSheet.Cells[index, 5]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 6] = "�ڲ�����";
					range = objSheet.get_Range(objSheet.Cells[index, 6], objSheet.Cells[index, 6]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 7] = "���ڲ�����";
					range = objSheet.get_Range(objSheet.Cells[index, 7], objSheet.Cells[index, 7]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

                    objSheet.Cells[index, 8] = "�ڸڹ�������";
                    range = objSheet.get_Range(objSheet.Cells[index, 8], objSheet.Cells[index, 8]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;
                    range.Merge(0);

                    objSheet.Cells[index, 9] = "�ڸڹ�������Ƭ����";
                    range = objSheet.get_Range(objSheet.Cells[index, 9], objSheet.Cells[index, 9]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;
                    range.Merge(0);

                    objSheet.Cells[index, 10] = "�ڸڹ�����ָ������";
                    range = objSheet.get_Range(objSheet.Cells[index, 10], objSheet.Cells[index, 10]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Font.Bold = true;
                    range.Merge(0);

					index++;

					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('����λ��������ͳ����Ϣ','" + ((2 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
							  "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();

					//ͬ��������������  
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
						jsBlock = "<script>SetPorgressBar('����λ��������ͳ����Ϣ','" +
								  (((2 + i + 1) * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
								  "'); </script>";
						Response.Write(jsBlock);
						Response.Flush();
					}
					objSheet.Cells.Columns.AutoFit();

					//���ɼ�,����̨����   
					objApp.Visible = false;
					objbook.Saved = true;
					objbook.SaveCopyAs(filename);

					jsBlock = "<script>SetCompleted('������ɡ�'); </script>";
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
		/// ��������λ��������ͳ����Ϣ
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
				jsBlock = "<script>SetPorgressBar('����λ��������ͳ����Ϣ','" + ((1 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
						  "'); </script>";
				Response.Write(jsBlock);
				Response.Flush();

				Excel.Application objApp = new Excel.ApplicationClass();
				Excel.Workbooks objbooks = objApp.Workbooks;
				Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
				Excel.Worksheet objSheet = (Excel.Worksheet)objbook.Worksheets[1]; //ȡ��sheet1 
				Excel.Range range;
				string filename = "";

				try
				{
					//����.xls�ļ�����·���� 
					filename = Server.MapPath("/RailExamBao/Excel/����λ��������ͳ����Ϣ.xls");

					if (File.Exists(filename.ToString()))
					{
						File.Delete(filename.ToString());
					}
					//�����õ��ı������,��ֵ����Ԫ��   

					int index = 1;

					objSheet.Cells[index, 1] = "���";
					range = objSheet.get_Range(objSheet.Cells[index, 1], objSheet.Cells[index, 1]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					for (int i = 0; i < dt.Columns.Count; i++)
					{
						if (i == 0)
							objSheet.Cells[index, i + 2] = "��λ";
						else
							objSheet.Cells[index, i + 2] = dt.Columns[i].ColumnName.Replace("#", "��").Replace("AA", "(").Replace("BB", ")");
						range = objSheet.get_Range(objSheet.Cells[index, i + 2], objSheet.Cells[index, i + 2]);
						range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
						range.Font.Bold = true;
						range.Merge(0);
					}
					
					index++;

					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('����λ��������ͳ����Ϣ','" + ((2 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
							  "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();

					//ͬ��������������  
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						objSheet.Cells[index + i, 1] = i + 1;

						for (int j = 0; j < dt.Columns.Count; j++)
						{
							objSheet.Cells[index + i, j+2] = dt.Rows[i][j];

						}
						
						 
						System.Threading.Thread.Sleep(10);
						jsBlock = "<script>SetPorgressBar('����λ��������ͳ����Ϣ','" +
								  (((2 + i + 1) * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
								  "'); </script>";
						Response.Write(jsBlock);
						Response.Flush();
					}
					objSheet.Cells.Columns.AutoFit();
					//���ɼ�,����̨����   
					objApp.Visible = false;
					objbook.Saved = true;
					objbook.SaveCopyAs(filename);

					jsBlock = "<script>SetCompleted('������ɡ�'); </script>";
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
		/// ��������λ���鳤����ͳ����Ϣ
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
				jsBlock = "<script>SetPorgressBar('����λ���鳤����ͳ����Ϣ','" + ((1 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
						  "'); </script>";
				Response.Write(jsBlock);
				Response.Flush();

				Excel.Application objApp = new Excel.ApplicationClass();
				Excel.Workbooks objbooks = objApp.Workbooks;
				Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
				Excel.Worksheet objSheet = (Excel.Worksheet)objbook.Worksheets[1]; //ȡ��sheet1 
				Excel.Range range;
				string filename = "";

				try
				{
					//����.xls�ļ�����·���� 
					filename = Server.MapPath("/RailExamBao/Excel/����λ���鳤����ͳ����Ϣ.xls");

					if (File.Exists(filename.ToString()))
					{
						File.Delete(filename.ToString());
					}
					//�����õ��ı������,��ֵ����Ԫ��   

					int index = 1;

					objSheet.Cells[index, 1] = "���";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 1], objSheet.Cells[index + 1, 1]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);
			 
					objSheet.Cells[index, 2] = "��λ";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 2], objSheet.Cells[index + 1, 2]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 3] = "���鳤����";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 3], objSheet.Cells[index + 1, 3]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);
						 
					objSheet.Cells[index,  4] = "����";
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
					jsBlock = "<script>SetPorgressBar('����λ���鳤����ͳ����Ϣ','" + ((2 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
							  "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();

					//ͬ��������������  
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						objSheet.Cells[index + i, 1] = i + 1;

						for (int j = 0; j < dt.Columns.Count; j++)
						{
							objSheet.Cells[index + i, j + 2] = dt.Rows[i][j];
		 
						}


						System.Threading.Thread.Sleep(10);
						jsBlock = "<script>SetPorgressBar('����λ���鳤����ͳ����Ϣ','" +
								  (((2 + i + 1) * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
								  "'); </script>";
						Response.Write(jsBlock);
						Response.Flush();
					}
				   objSheet.Cells.Columns.AutoFit();

					//���ɼ�,����̨����   
					objApp.Visible = false;
					objbook.Saved = true;
					objbook.SaveCopyAs(filename);

					jsBlock = "<script>SetCompleted('������ɡ�'); </script>";
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
		/// ��������λ���˽ṹͳ����Ϣ
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
				jsBlock = "<script>SetPorgressBar('����λ���˽ṹͳ����Ϣ','" + ((1 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
						  "'); </script>";
				Response.Write(jsBlock);
				Response.Flush();

				Excel.Application objApp = new Excel.ApplicationClass();
				Excel.Workbooks objbooks = objApp.Workbooks;
				Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
				Excel.Worksheet objSheet = (Excel.Worksheet)objbook.Worksheets[1]; //ȡ��sheet1 
				Excel.Range range;
				string filename = "";

				try
				{
					//����.xls�ļ�����·���� 
					filename = Server.MapPath("/RailExamBao/Excel/����λ���˽ṹͳ����Ϣ.xls");

					if (File.Exists(filename.ToString()))
					{
						File.Delete(filename.ToString());
					}
					//�����õ��ı������,��ֵ����Ԫ��   

					int eduCount = Request.QueryString.Get("eduCount") == "" ? 0 : Convert.ToInt32(Request.QueryString.Get("eduCount"));
					int techCount=Request.QueryString.Get("techCount") == "" ? 0 : Convert.ToInt32(Request.QueryString.Get("techCount"));

					int index = 1;

					objSheet.Cells[index, 1] = "���";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 1], objSheet.Cells[index + 1, 1]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 2] = "��λ";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 2], objSheet.Cells[index + 1, 2]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 3] = "����ṹ";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 3], objSheet.Cells[index,11]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 11] = "�Ļ��̶�";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 12], objSheet.Cells[index, 11 + eduCount]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 11 + eduCount] = "ְҵ���ܵȼ�";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 12 + eduCount], objSheet.Cells[index, 11 + eduCount+techCount]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					index++;

					for (int i = 1; i < dt.Columns.Count; i++)
					{
						string colName = "";
						if (dt.Columns[i].ColumnName.Equals("A"))
							colName = "25�꼰����";
						else if (dt.Columns[i].ColumnName.Equals("B"))
							colName = "26����30��";
						else if (dt.Columns[i].ColumnName.Equals("C"))
							colName = "31����35��";
						else if (dt.Columns[i].ColumnName.Equals("D"))
							colName = "36����40��";
						else if (dt.Columns[i].ColumnName.Equals("E"))
							colName = "41����45��";
						else if (dt.Columns[i].ColumnName.Equals("F"))
							colName = "46����50��";
						else if (dt.Columns[i].ColumnName.Equals("G"))
							colName = "51����55��";
						else if (dt.Columns[i].ColumnName.Equals("H"))
							colName = "56����60��";
                        else if (dt.Columns[i].ColumnName.Equals("I"))
                            colName = "60������";
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
					jsBlock = "<script>SetPorgressBar('����λ���˽ṹͳ����Ϣ','" + ((2 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
							  "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();

					//ͬ��������������  
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						objSheet.Cells[index + i, 1] = i + 1;

						for (int j = 0; j < dt.Columns.Count; j++)
						{
							objSheet.Cells[index + i, j + 2] = dt.Rows[i][j];

						}


						System.Threading.Thread.Sleep(10);
						jsBlock = "<script>SetPorgressBar('����λ���˽ṹͳ����Ϣ','" +
								  (((2 + i + 1) * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
								  "'); </script>";
						Response.Write(jsBlock);
						Response.Flush();
					}
					objSheet.Cells.Columns.AutoFit();

					//���ɼ�,����̨����   
					objApp.Visible = false;
					objbook.Saved = true;
					objbook.SaveCopyAs(filename);

					jsBlock = "<script>SetCompleted('������ɡ�'); </script>";
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
		/// ���������ֹ��˽ṹͳ����Ϣ
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
				jsBlock = "<script>SetPorgressBar('�����ֹ��˽ṹͳ����Ϣ','" + ((1 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
						  "'); </script>";
				Response.Write(jsBlock);
				Response.Flush();

				Excel.Application objApp = new Excel.ApplicationClass();
				Excel.Workbooks objbooks = objApp.Workbooks;
				Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
				Excel.Worksheet objSheet = (Excel.Worksheet)objbook.Worksheets[1]; //ȡ��sheet1 
				Excel.Range range;
				string filename = "";

				try
				{
					//����.xls�ļ�����·���� 
					filename = Server.MapPath("/RailExamBao/Excel/�����ֹ��˽ṹͳ����Ϣ.xls");

					if (File.Exists(filename.ToString()))
					{
						File.Delete(filename.ToString());
					}
					//�����õ��ı������,��ֵ����Ԫ��   

					int eduCount = Request.QueryString.Get("eduCount") == "" ? 0 : Convert.ToInt32(Request.QueryString.Get("eduCount"));
					int techCount = Request.QueryString.Get("techCount") == "" ? 0 : Convert.ToInt32(Request.QueryString.Get("techCount"));

					int index = 1;

					objSheet.Cells[index, 1] = "���";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 1], objSheet.Cells[index + 1, 1]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

                    objSheet.Cells[index, 2] = "����";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 2], objSheet.Cells[index + 1, 2]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 3] = "����ṹ";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 3], objSheet.Cells[index, 11]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 11] = "�Ļ��̶�";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 12], objSheet.Cells[index, 11 + eduCount]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 11 + eduCount] = "ְҵ���ܵȼ�";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 12 + eduCount], objSheet.Cells[index, 11 + eduCount + techCount]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					index++;

					for (int i = 1; i < dt.Columns.Count; i++)
					{
						string colName = "";
						if (dt.Columns[i].ColumnName.Equals("A"))
							colName = "25�꼰����";
						else if (dt.Columns[i].ColumnName.Equals("B"))
							colName = "26����30��";
						else if (dt.Columns[i].ColumnName.Equals("C"))
							colName = "31����35��";
						else if (dt.Columns[i].ColumnName.Equals("D"))
							colName = "36����40��";
						else if (dt.Columns[i].ColumnName.Equals("E"))
							colName = "41����45��";
						else if (dt.Columns[i].ColumnName.Equals("F"))
							colName = "46����50��";
						else if (dt.Columns[i].ColumnName.Equals("G"))
							colName = "51����55��";
						else if (dt.Columns[i].ColumnName.Equals("H"))
							colName = "56����60��";
                        else if (dt.Columns[i].ColumnName.Equals("J"))
                            colName = "60������";
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
					jsBlock = "<script>SetPorgressBar('�����ֹ��˽ṹͳ����Ϣ','" + ((2 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
							  "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();

					//ͬ��������������  
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						objSheet.Cells[index + i, 1] = i + 1;

						for (int j = 0; j < dt.Columns.Count; j++)
						{
							objSheet.Cells[index + i, j + 2] = dt.Rows[i][j];

						}


						System.Threading.Thread.Sleep(10);
						jsBlock = "<script>SetPorgressBar('�����ֹ��˽ṹͳ����Ϣ','" +
								  (((2 + i + 1) * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
								  "'); </script>";
						Response.Write(jsBlock);
						Response.Flush();
					}
					objSheet.Cells.Columns.AutoFit();

					//���ɼ�,����̨����   
					objApp.Visible = false;
					objbook.Saved = true;
					objbook.SaveCopyAs(filename);

					jsBlock = "<script>SetCompleted('������ɡ�'); </script>";
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
		/// ��������λְ�̹�����Աͳ����Ϣ
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
				jsBlock = "<script>SetPorgressBar('����λְ�̹�����Աͳ����Ϣ','" + ((1 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
						  "'); </script>";
				Response.Write(jsBlock);
				Response.Flush();

				Excel.Application objApp = new Excel.ApplicationClass();
				Excel.Workbooks objbooks = objApp.Workbooks;
				Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
				Excel.Worksheet objSheet = (Excel.Worksheet)objbook.Worksheets[1]; //ȡ��sheet1 
				Excel.Range range;
				string filename = "";

				try
				{
					//����.xls�ļ�����·���� 
					filename = Server.MapPath("/RailExamBao/Excel/����λְ�̹�����Աͳ����Ϣ.xls");

					if (File.Exists(filename.ToString()))
					{
						File.Delete(filename.ToString());
					}
					//�����õ��ı������,��ֵ����Ԫ��   

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

					objSheet.Cells[index, 1] = "���";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 1], objSheet.Cells[index + 1, 1]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 2] = "��λ";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 2], objSheet.Cells[index + 1, 3]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 4] = "�ܼ�(��)";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 4], objSheet.Cells[index + 1, 4]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 5] = "����ṹ";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 5], objSheet.Cells[index, 8]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 9] = "ѧ���ṹ";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 9], objSheet.Cells[index, 8 + eduCount]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);


					objSheet.Cells[index, 9 + eduCount] = "����ְ��";
					range = objSheet.Cells.get_Range(objSheet.Cells[index, 9 + eduCount],
					                                 objSheet.Cells[index, 8 + eduCount + techTitleCount]);
					range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					range.Font.Bold = true;
					range.Merge(0);

					objSheet.Cells[index, 9 + eduCount + techTitleCount] = "���ܵȼ�";
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
							colName = "30�꼰����";
						else if (dt.Columns[i].ColumnName.Equals("B"))
							colName = "31-40��";
						else if (dt.Columns[i].ColumnName.Equals("C"))
							colName = "41-50��";
						else if (dt.Columns[i].ColumnName.Equals("D"))
							colName = "51�꼰����";
						 
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
					jsBlock = "<script>SetPorgressBar('����λְ�̹�����Աͳ����Ϣ','" + ((2 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
							  "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();

					//ͬ��������������  
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
						jsBlock = "<script>SetPorgressBar('����λְ�̹�����Աͳ����Ϣ','" +
								  (((2 + i + 1) * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
								  "'); </script>";
						Response.Write(jsBlock);
						Response.Flush();
					}
					objSheet.Cells.Columns.AutoFit();

					//���ɼ�,����̨����   
					objApp.Visible = false;
					objbook.Saved = true;
					objbook.SaveCopyAs(filename);

					jsBlock = "<script>SetCompleted('������ɡ�'); </script>";
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
                jsBlock = "<script>SetPorgressBar('����λ���ܾ���ͳ��','" + ((1 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
                          "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                Excel.Application objApp = new Excel.ApplicationClass();
                Excel.Workbooks objbooks = objApp.Workbooks;
                Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet objSheet = (Excel.Worksheet)objbook.Worksheets[1]; //ȡ��sheet1 
                Excel.Range range;
                string filename = "";

                try
                {
                    //����.xls�ļ�����·���� 
                    filename = Server.MapPath("/RailExamBao/Excel/����λ���ܾ���ͳ��.xls");

                    if (File.Exists(filename.ToString()))
                    {
                        File.Delete(filename.ToString());
                    }
                    //�����õ��ı������,��ֵ����Ԫ��   
                    int index = 1;
                    objSheet.Cells[index, 1] = "���ܾ����������ͳ�Ʊ�"; 
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
                    objSheet.Cells[index, 1] = "���";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, 1], objSheet.Cells[index + 1, 1]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    objSheet.Cells[index, 2] = "��λ";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, 2], objSheet.Cells[index + 1, 2]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    objSheet.Cells[index, 3] = "�ٰ쵥λ";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, 3], objSheet.Cells[index, 7]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    objSheet.Cells[index, 8] = "�ٰ쵥λ";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, 8], objSheet.Cells[index, dt.Columns.Count]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    int row = 3;
                    index++;
                    objSheet.Cells[index, row] = "�ϼ�";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    row++;
                    objSheet.Cells[index, row] = "ȫ��";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    row++;
                    objSheet.Cells[index, row] = "������";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    row++;
                    objSheet.Cells[index, row] = "��·��";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    row++;
                    objSheet.Cells[index, row] = "վ��";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    row++;
                    objSheet.Cells[index, row] = "�ϼ�";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    row++;
                    objSheet.Cells[index, row] = "ȫ��";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    row++;
                    objSheet.Cells[index, row] = "����";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    row++;
                    objSheet.Cells[index, row] = "�ϼ�";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    row++;
                    objSheet.Cells[index, row] = "����";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, row], objSheet.Cells[index, row]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);


                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('���ܾ����������ͳ�Ʊ�','" + ((2 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
                              "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();

                    index++;
                    //ͬ��������������  
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
                        jsBlock = "<script>SetPorgressBar('���ܾ����������ͳ�Ʊ�','" +
                                  (((2 + i + 1) * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
                                  "'); </script>";
                        Response.Write(jsBlock);
                        Response.Flush();
                    }
                    objSheet.Cells.Columns.AutoFit();
                    range = objSheet.get_Range(objSheet.Cells[1, 1], objSheet.Cells[index + dt.Rows.Count-1, dt.Columns.Count]);
                    range.Borders.LineStyle = 1;

                    //���ɼ�,����̨����   
                    objApp.Visible = false;
                    objbook.Saved = true;
                    objbook.SaveCopyAs(filename);

                    jsBlock = "<script>SetCompleted('������ɡ�'); </script>";
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
                jsBlock = "<script>SetPorgressBar('����λ��������֤���ͳ��','" + ((1 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
                          "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                Excel.Application objApp = new Excel.ApplicationClass();
                Excel.Workbooks objbooks = objApp.Workbooks;
                Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet objSheet = (Excel.Worksheet)objbook.Worksheets[1]; //ȡ��sheet1 
                Excel.Range range;
                string filename = "";

                try
                {
                    //����.xls�ļ�����·���� 
                    filename = Server.MapPath("/RailExamBao/Excel/����λ��������֤���ͳ��.xls");

                    if (File.Exists(filename.ToString()))
                    {
                        File.Delete(filename.ToString());
                    }
                    //�����õ��ı������,��ֵ����Ԫ��   
                    int index = 1;
                    objSheet.Cells[index, 1] = "����λ��������֤���ͳ��";
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
                    objSheet.Cells[index, 1] = "���";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, 1], objSheet.Cells[index + 2, 1]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    objSheet.Cells[index, 2] = "��λ";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, 2], objSheet.Cells[index + 2, 2]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    objSheet.Cells[index, 3] = "�ٰ쵥λ";
                    range = objSheet.Cells.get_Range(objSheet.Cells[index, 3], objSheet.Cells[index, dt.Columns.Count]);
                    range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Merge(0);

                    index++;
                    objSheet.Cells[index, 3] = "�ϼ�";
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
                    jsBlock = "<script>SetPorgressBar('����λ��������֤���ͳ��','" + ((2 * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
                              "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();

                    index++;
                    //ͬ��������������  
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
                        jsBlock = "<script>SetPorgressBar('����λ��������֤���ͳ��','" +
                                  (((2 + i + 1) * 100) / ((double)dt.Rows.Count + 2)).ToString("0.00") +
                                  "'); </script>";
                        Response.Write(jsBlock);
                        Response.Flush();
                    }
                    objSheet.Cells.Columns.AutoFit();

                    range = objSheet.get_Range(objSheet.Cells[1, 1], objSheet.Cells[index + dt.Rows.Count - 1, dt.Columns.Count]);
                    range.Borders.LineStyle = 1;

                    //���ɼ�,����̨����   
                    objApp.Visible = false;
                    objbook.Saved = true;
                    objbook.SaveCopyAs(filename);

                    jsBlock = "<script>SetCompleted('������ɡ�'); </script>";
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
