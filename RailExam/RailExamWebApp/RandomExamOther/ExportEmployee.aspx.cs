using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using Excel;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamOther
{
    public partial class ExportEmployee : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                SystemVersionBLL systemVersionBll = new SystemVersionBLL();
                int usePlace = systemVersionBll.GetUsePlace();

                if (Request.QueryString.Get("Type") == "template")
                {
                    if(usePlace == 2)
                    {
                        ExportTemplateOther();
                    }
                    else if(usePlace == 3)
                    {
                        ExportTemplate();
                    }
                }
                else
                {
                    if (usePlace == 2)
                    {
                        ExportEmployeeInfoOther();
                    }
                    else if(usePlace == 3)
                    {
                        ExportEmployeeInfo();
                    }
                }
            }
        }

        private void ExportTemplate()
        {
            string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
			StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
			string html = reader.ReadToEnd();
			reader.Close();
			Response.Write(html);
			Response.Flush();
			System.Threading.Thread.Sleep(200);

			string jsBlock;

			Excel.Application objApp = new Excel.ApplicationClass();
			Excel.Workbooks objbooks = objApp.Workbooks;
			Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
			Excel.Worksheet objSheet = (Excel.Worksheet) objbook.Worksheets[1]; //取得sheet1 
			string filename = "";
			string strName = "";
			try
			{
                strName = "员工Excel模板";
                filename = Server.MapPath("/RailExamBao/Excel/" + strName + ".xls");

                if (File.Exists(filename.ToString()))
                {
                    File.Delete(filename.ToString());
                }
                objSheet.Cells.Font.Size = 10;
                objSheet.Cells.Font.Name = "宋体";

			    int colIndex = 1;
                objSheet.Cells[1, colIndex] = "序号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

			    colIndex++;
                objSheet.Cells[1, colIndex] = "员工ID";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "单位名称";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "部门名称";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "班组";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "姓名";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "身份证号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "工作证号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "性别";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "籍贯";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "民族";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "婚姻状况";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "现文化程度";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "政治面貌";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "毕(肄)业学校(单位)";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "所学专业";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "工作地址";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "职务级别";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "出生日期";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "入路工作日期";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "参加工作日期";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "干部工人标识";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "职务名称";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "岗位";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "班组长类型";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "教师类别";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "人员岗位状态";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "在岗职工按岗位分组";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "现技术职务名称";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "技术等级";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "岗位培训合格证编号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "职教干部类型";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "职教委员会职务";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "邮政编码";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "工资编号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                objApp.Visible = false;

                objbook.Saved = true;
                objbook.SaveCopyAs(filename);

                // 处理完成
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

            Response.Write("<script>top.returnValue='" + strName + "';window.close();</script>");
        }

        private void ExportTemplateOther()
        {
            string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
            System.Threading.Thread.Sleep(200);

            string jsBlock;

            Excel.Application objApp = new Excel.ApplicationClass();
            Excel.Workbooks objbooks = objApp.Workbooks;
            Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet objSheet = (Excel.Worksheet)objbook.Worksheets[1]; //取得sheet1 
            string filename = "";
            string strName = "";
            try
            {
                strName = "员工Excel模板";
                filename = Server.MapPath("/RailExamBao/Excel/" + strName + ".xls");

                if (File.Exists(filename.ToString()))
                {
                    File.Delete(filename.ToString());
                }
                objSheet.Cells.Font.Size = 10;
                objSheet.Cells.Font.Name = "宋体";

                int colIndex = 1;
                objSheet.Cells[1, colIndex] = "序号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "员工ID";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "站段名称";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "车间";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "班组";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "姓名";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "工作证号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "性别";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();


                colIndex++;
                objSheet.Cells[1, colIndex] = "职名";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "是否班组长";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

               colIndex++;
               objSheet.Cells[1, colIndex] = "人员岗位状态";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "技能等级";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "岗位培训合格证编号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                objApp.Visible = false;

                objbook.Saved = true;
                objbook.SaveCopyAs(filename);

                // 处理完成
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

            Response.Write("<script>top.returnValue='" + strName + "';window.close();</script>");
        }


        private void ExportEmployeeInfoOther()
        {
            string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
			StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
			string html = reader.ReadToEnd();
			reader.Close();
			Response.Write(html);
			Response.Flush();
			System.Threading.Thread.Sleep(200);

			string strOrgID = Request.QueryString.Get("OrgID");

			string jsBlock;

			Excel.Application objApp = new Excel.ApplicationClass();
			Excel.Workbooks objbooks = objApp.Workbooks;
			Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
			Excel.Worksheet objSheet = (Excel.Worksheet) objbook.Worksheets[1]; //取得sheet1 
			string filename = "";
			string strName = "";
			try
			{
                OrganizationBLL orgBll = new OrganizationBLL();
			    int stationID;
			    stationID = orgBll.GetStationOrgID(Convert.ToInt32(strOrgID));
                Organization org = orgBll.GetOrganization(Convert.ToInt32(strOrgID));
			    Organization orgStation = orgBll.GetOrganization(stationID);

			    strName = orgStation.ShortName+ "员工信息";
                filename = Server.MapPath("/RailExamBao/Excel/" + strName + ".xls");

                if (File.Exists(filename.ToString()))
                {
                    File.Delete(filename.ToString());
                }
                objSheet.Cells.Font.Size = 10;
                objSheet.Cells.Font.Name = "宋体";

			    int colIndex = 1;
                objSheet.Cells[1, colIndex] = "序号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

			    colIndex++;
                objSheet.Cells[1, colIndex] = "员工ID";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "站段名称";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "车间";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "班组";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "姓名";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "工作证号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "性别";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


                colIndex++;
                objSheet.Cells[1, colIndex] = "职名";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "是否班组长";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "人员岗位状态";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


                colIndex++;
                objSheet.Cells[1, colIndex] = "技能等级";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "岗位培训合格证编号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


                EmployeeBLL objBll = new EmployeeBLL();

			    string strSql = "Org_ID in (select Org_ID from Org where ID_Path||'/' like '" + org.IdPath + "/%')";

			    IList<RailExam.Model.Employee> objList = objBll.GetEmployeeByWhereClause(strSql);

			    int rowIndex = 2;
			    foreach (RailExam.Model.Employee employeeDetail in objList)
			    {
                    colIndex = 1;
			        objSheet.Cells[rowIndex, colIndex] = rowIndex - 1;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
			        objSheet.Cells[rowIndex, colIndex] = employeeDetail.EmployeeID;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
			        objSheet.Cells[rowIndex, colIndex] = orgStation.ShortName;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

			        Organization nowOrg = orgBll.GetOrganization(employeeDetail.OrgID);
			        string strShop = string.Empty;
			        string strGroup = string.Empty;
                    if(nowOrg.LevelNum == 3)
                    {
                        strShop = nowOrg.ShortName;
                    }
                    else
                    {
                        Organization orgParent = orgBll.GetOrganization(nowOrg.ParentId);
                        strShop = orgParent.ShortName;
                        strGroup = nowOrg.ShortName;
                    }

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = strShop;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = strGroup;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
			        objSheet.Cells[rowIndex, colIndex] = employeeDetail.EmployeeName;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = "'" + employeeDetail.PostNo;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
			        objSheet.Cells[rowIndex, colIndex] = employeeDetail.Sex;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


                    colIndex++;
			        objSheet.Cells[rowIndex, colIndex] = employeeDetail.IsGroupLeader==1 ? "是" : "";
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.IsOnPost ? "在岗" : "";
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    
                    TechnicianTypeBLL technicianTypeBll = new TechnicianTypeBLL();
			        TechnicianType technicianType =
			            technicianTypeBll.GetTechnicianTypeByTechnicianTypeID(employeeDetail.TechnicianTypeID);                    
                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = technicianType.TypeName;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
			        objSheet.Cells[rowIndex, colIndex] = "'" + employeeDetail.WorkNo;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


			        rowIndex++;

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('导出员工信息','" + ((double)((rowIndex - 1) * 100) / (double)objList.Count).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
			    }

                objApp.Visible = false;

                objbook.Saved = true;
                objbook.SaveCopyAs(filename);

                // 处理完成
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

            Response.Write("<script>top.returnValue='" + strName + "';window.close();</script>");
        }

        private void ExportEmployeeInfo()
        {
            string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
            System.Threading.Thread.Sleep(200);

            string strOrgID = Request.QueryString.Get("OrgID");

            string jsBlock;

            Excel.Application objApp = new Excel.ApplicationClass();
            Excel.Workbooks objbooks = objApp.Workbooks;
            Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet objSheet = (Excel.Worksheet)objbook.Worksheets[1]; //取得sheet1 
            string filename = "";
            string strName = "";
            try
            {
                OrganizationBLL orgBll = new OrganizationBLL();
                int stationID;
                stationID = orgBll.GetStationOrgID(Convert.ToInt32(strOrgID));
                Organization org = orgBll.GetOrganization(Convert.ToInt32(strOrgID));
                Organization orgStation = orgBll.GetOrganization(stationID);

                strName = orgStation.ShortName + "员工信息";
                filename = Server.MapPath("/RailExamBao/Excel/" + strName + ".xls");

                if (File.Exists(filename.ToString()))
                {
                    File.Delete(filename.ToString());
                }
                objSheet.Cells.Font.Size = 10;
                objSheet.Cells.Font.Name = "宋体";

                int colIndex = 1;
                objSheet.Cells[1, colIndex] = "序号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "员工ID";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "单位名称";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "部门名称";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "班组";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "姓名";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "身份证号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "工作证号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "性别";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "籍贯";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "民族";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "婚姻状况";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "现文化程度";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "政治面貌";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "毕(肄)业学校(单位)";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "所学专业";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "工作地址";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "职务级别";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "出生日期";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "入路工作日期";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "参加工作日期";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "干部工人标识";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "职务名称";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "岗位";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "班组长类型";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "教师类别";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "人员岗位状态";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "在岗职工按岗位分组";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "现技术职务名称";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "技术等级";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "岗位培训合格证编号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "职教干部类型";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "职教委员会职务";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "邮政编码";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "工资编号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                EmployeeDetailBLL objBll = new EmployeeDetailBLL();

                string strSql = "Org_ID in (select Org_ID from Org where ID_Path||'/' like '" + org.IdPath + "/%')";

                IList<RailExam.Model.EmployeeDetail> objList = objBll.GetEmployeeByWhereClause(strSql);

                int rowIndex = 2;
                Hashtable htEmployeeLevel = GetEmployeeLevel();
                Hashtable hfTeacherType = GetTeacherType();
                Hashtable htEmployeeTransportType = GetEmployeeTransportType();
                Hashtable htEducationEmployeeType = GetEducationEmployeeType();
                Hashtable htCommitteeHeadship = GetCommitteeHeadship();
                foreach (RailExam.Model.EmployeeDetail employeeDetail in objList)
                {
                    colIndex = 1;
                    objSheet.Cells[rowIndex, colIndex] = rowIndex - 1;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.EmployeeID;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = orgStation.ShortName;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    Organization nowOrg = orgBll.GetOrganization(employeeDetail.OrgID);
                    string strShop = string.Empty;
                    string strGroup = string.Empty;
                    if (nowOrg.LevelNum == 3)
                    {
                        strShop = nowOrg.ShortName;
                    }
                    else
                    {
                        Organization orgParent = orgBll.GetOrganization(nowOrg.ParentId);
                        strShop = orgParent.ShortName;
                        strGroup = nowOrg.ShortName;
                    }

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = strShop;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = strGroup;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.EmployeeName;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = "'" + employeeDetail.IdentifyCode;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = "'" + employeeDetail.PostNo;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.Sex;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.NativePlace;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.Folk;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.Wedding == 1 ? "已婚" : "未婚";
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    EducationLevelBLL educationLevelBll = new EducationLevelBLL();
                    EducationLevel educationLevel =
                        educationLevelBll.GetEducationLevelByEducationLevelID(employeeDetail.EducationLevelID);
                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = educationLevel.EducationLevelName;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    PoliticalStatusBLL politicalStatusBll = new PoliticalStatusBLL();
                    PoliticalStatus politicalStatus =
                        politicalStatusBll.GetPoliticalStatusByPoliticalStatusID(employeeDetail.PoliticalStatusID);
                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = politicalStatus.PoliticalStatusName;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.GraduateUniversity;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.StudyMajor;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.Address;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = htEmployeeLevel[employeeDetail.EmployeeLevelID].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.Birthday.ToString("yyyyMMdd");
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.BeginDate.ToString("yyyyMMdd");
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.WorkDate.ToString("yyyyMMdd");
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.EmployeeTypeID == 1 ? "工人" : "干部";
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.EmployeeTypeID == 1 ? "" : employeeDetail.PostName;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.EmployeeTypeID == 1 ? employeeDetail.PostName : "";
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    WorkGroupLeaderLevelBLL workGroupLeaderLevelBll = new WorkGroupLeaderLevelBLL();
                    WorkGroupLeaderLevel workGroupLeaderLevel =
                        workGroupLeaderLevelBll.GetWorkGroupLeaderLevelByWorkGroupLeaderLevelID(
                            employeeDetail.WorkGroupLeaderTypeID);
                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = workGroupLeaderLevel.LevelName;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = hfTeacherType[employeeDetail.TeacherTypeID].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.Dimission ? "" : "在岗工作";
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = htEmployeeTransportType[employeeDetail.EmployeeTransportTypeID].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    TechnicianTitleTypeBLL technicianTitleTypeBll = new TechnicianTitleTypeBLL();
                    TechnicianTitleType technicianTitleType =
                        technicianTitleTypeBll.GetTechnicianTitleTypeByTechnicianTitleTypeID(employeeDetail.TechnicalTitleID);
                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = technicianTitleType.TypeName;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


                    TechnicianTypeBLL technicianTypeBll = new TechnicianTypeBLL();
                    TechnicianType technicianType =
                        technicianTypeBll.GetTechnicianTypeByTechnicianTypeID(employeeDetail.TechnicianTypeID);
                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = technicianType.TypeName;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = "'" + employeeDetail.HomePhone;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = htEducationEmployeeType[employeeDetail.EducationEmployeeTypeID].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = htCommitteeHeadship[employeeDetail.CommitteeHeadShipID].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.PostCode;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.WorkNo;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    rowIndex++;

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('导出员工信息','" + ((double)((rowIndex - 1) * 100) / (double)objList.Count).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                }

                objApp.Visible = false;

                objbook.Saved = true;
                objbook.SaveCopyAs(filename);

                // 处理完成
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

            Response.Write("<script>top.returnValue='" + strName + "';window.close();</script>");
        }

        /// <summary>
        /// 职务级别
        /// </summary>
        /// <returns></returns>
        private Hashtable GetEmployeeLevel()
        {
            Hashtable htEmployeeLevel = new Hashtable();

            htEmployeeLevel.Add(0, "");
            htEmployeeLevel.Add(1, "正局");
            htEmployeeLevel.Add(2, "副局");
            htEmployeeLevel.Add(3, "正部");
            htEmployeeLevel.Add(4, "副部");
            htEmployeeLevel.Add(5, "正处");
            htEmployeeLevel.Add(6, "副处");
            htEmployeeLevel.Add(7, "正科");
            htEmployeeLevel.Add(8, "副科");
            htEmployeeLevel.Add(9, "科员");
            htEmployeeLevel.Add(10, "股级");
            htEmployeeLevel.Add(11, "干事");
            htEmployeeLevel.Add(12, "办事员");
            htEmployeeLevel.Add(13, "其他");

            return htEmployeeLevel;
        }

        /// <summary>
        /// 职务级别
        /// </summary>
        /// <returns></returns>
        private Hashtable GetTeacherType()
        {
            Hashtable hfTeacherType = new Hashtable();

            hfTeacherType.Add(0, "");
            hfTeacherType.Add(1, "兼职教师");
            hfTeacherType.Add(2, "专职教师");
            hfTeacherType.Add(3, "管理干部");

            return hfTeacherType;
        }

        private Hashtable GetEmployeeTransportType()
        {
            Hashtable htEmployeeTransportType = new Hashtable();
            htEmployeeTransportType.Add(0, "");
            htEmployeeTransportType.Add(1, "生产人员");
            htEmployeeTransportType.Add(2, "服务人员");
            htEmployeeTransportType.Add(3, "其他人员");
            htEmployeeTransportType.Add(4, "工程技术人员");
            htEmployeeTransportType.Add(5, "行政管理人员");
            htEmployeeTransportType.Add(6, "政工人员");
            return htEmployeeTransportType;
        }

        /// <summary>
        /// 职教干部类型
        /// </summary>
        /// <returns></returns>
        private Hashtable GetEducationEmployeeType()
        {
            Hashtable htEducationEmployeeType = new Hashtable();
            htEducationEmployeeType.Add(0, "");
            htEducationEmployeeType.Add(1, "管理干部");
            htEducationEmployeeType.Add(2, "专职教员");
            htEducationEmployeeType.Add(3, "其他");
            return htEducationEmployeeType;
        }

        /// <summary>
        /// 职教委员会职务

        /// </summary>
        /// <returns></returns>
        private Hashtable GetCommitteeHeadship()
        {
            Hashtable htCommitteeHeadship = new Hashtable();
            htCommitteeHeadship.Add(0, "");
            htCommitteeHeadship.Add(1, "主任");
            htCommitteeHeadship.Add(2, "副主任");
            htCommitteeHeadship.Add(3, "委员");
            return htCommitteeHeadship;
        }
    }
}
