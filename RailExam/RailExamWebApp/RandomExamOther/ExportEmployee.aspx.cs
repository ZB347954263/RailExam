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
			Excel.Worksheet objSheet = (Excel.Worksheet) objbook.Worksheets[1]; //ȡ��sheet1 
			string filename = "";
			string strName = "";
			try
			{
                strName = "Ա��Excelģ��";
                filename = Server.MapPath("/RailExamBao/Excel/" + strName + ".xls");

                if (File.Exists(filename.ToString()))
                {
                    File.Delete(filename.ToString());
                }
                objSheet.Cells.Font.Size = 10;
                objSheet.Cells.Font.Name = "����";

			    int colIndex = 1;
                objSheet.Cells[1, colIndex] = "���";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

			    colIndex++;
                objSheet.Cells[1, colIndex] = "Ա��ID";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "��λ����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "��������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "���֤��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "����֤��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "�Ա�";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "����״��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "���Ļ��̶�";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "������ò";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "��(��)ҵѧУ(��λ)";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "��ѧרҵ";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "������ַ";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "ְ�񼶱�";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "��������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "��·��������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "�μӹ�������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "�ɲ����˱�ʶ";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "ְ������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "��λ";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "���鳤����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "��ʦ���";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "��Ա��λ״̬";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "�ڸ�ְ������λ����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "�ּ���ְ������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "�����ȼ�";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "��λ��ѵ�ϸ�֤���";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "ְ�̸ɲ�����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "ְ��ίԱ��ְ��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "��������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "���ʱ��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)35).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                objApp.Visible = false;

                objbook.Saved = true;
                objbook.SaveCopyAs(filename);

                // �������
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
            Excel.Worksheet objSheet = (Excel.Worksheet)objbook.Worksheets[1]; //ȡ��sheet1 
            string filename = "";
            string strName = "";
            try
            {
                strName = "Ա��Excelģ��";
                filename = Server.MapPath("/RailExamBao/Excel/" + strName + ".xls");

                if (File.Exists(filename.ToString()))
                {
                    File.Delete(filename.ToString());
                }
                objSheet.Cells.Font.Size = 10;
                objSheet.Cells.Font.Name = "����";

                int colIndex = 1;
                objSheet.Cells[1, colIndex] = "���";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "Ա��ID";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "վ������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "����֤��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "�Ա�";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();


                colIndex++;
                objSheet.Cells[1, colIndex] = "ְ��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "�Ƿ���鳤";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

               colIndex++;
               objSheet.Cells[1, colIndex] = "��Ա��λ״̬";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "���ܵȼ�";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "��λ��ѵ�ϸ�֤���";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)13).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                objApp.Visible = false;

                objbook.Saved = true;
                objbook.SaveCopyAs(filename);

                // �������
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
			Excel.Worksheet objSheet = (Excel.Worksheet) objbook.Worksheets[1]; //ȡ��sheet1 
			string filename = "";
			string strName = "";
			try
			{
                OrganizationBLL orgBll = new OrganizationBLL();
			    int stationID;
			    stationID = orgBll.GetStationOrgID(Convert.ToInt32(strOrgID));
                Organization org = orgBll.GetOrganization(Convert.ToInt32(strOrgID));
			    Organization orgStation = orgBll.GetOrganization(stationID);

			    strName = orgStation.ShortName+ "Ա����Ϣ";
                filename = Server.MapPath("/RailExamBao/Excel/" + strName + ".xls");

                if (File.Exists(filename.ToString()))
                {
                    File.Delete(filename.ToString());
                }
                objSheet.Cells.Font.Size = 10;
                objSheet.Cells.Font.Name = "����";

			    int colIndex = 1;
                objSheet.Cells[1, colIndex] = "���";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

			    colIndex++;
                objSheet.Cells[1, colIndex] = "Ա��ID";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "վ������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "����֤��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "�Ա�";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


                colIndex++;
                objSheet.Cells[1, colIndex] = "ְ��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "�Ƿ���鳤";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��Ա��λ״̬";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


                colIndex++;
                objSheet.Cells[1, colIndex] = "���ܵȼ�";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��λ��ѵ�ϸ�֤���";
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
			        objSheet.Cells[rowIndex, colIndex] = employeeDetail.IsGroupLeader==1 ? "��" : "";
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.IsOnPost ? "�ڸ�" : "";
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
                    jsBlock = "<script>SetPorgressBar('����Ա����Ϣ','" + ((double)((rowIndex - 1) * 100) / (double)objList.Count).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
			    }

                objApp.Visible = false;

                objbook.Saved = true;
                objbook.SaveCopyAs(filename);

                // �������
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
            Excel.Worksheet objSheet = (Excel.Worksheet)objbook.Worksheets[1]; //ȡ��sheet1 
            string filename = "";
            string strName = "";
            try
            {
                OrganizationBLL orgBll = new OrganizationBLL();
                int stationID;
                stationID = orgBll.GetStationOrgID(Convert.ToInt32(strOrgID));
                Organization org = orgBll.GetOrganization(Convert.ToInt32(strOrgID));
                Organization orgStation = orgBll.GetOrganization(stationID);

                strName = orgStation.ShortName + "Ա����Ϣ";
                filename = Server.MapPath("/RailExamBao/Excel/" + strName + ".xls");

                if (File.Exists(filename.ToString()))
                {
                    File.Delete(filename.ToString());
                }
                objSheet.Cells.Font.Size = 10;
                objSheet.Cells.Font.Name = "����";

                int colIndex = 1;
                objSheet.Cells[1, colIndex] = "���";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "Ա��ID";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��λ����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "���֤��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "����֤��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "�Ա�";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "����״��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "���Ļ��̶�";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "������ò";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��(��)ҵѧУ(��λ)";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��ѧרҵ";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "������ַ";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "ְ�񼶱�";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��·��������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "�μӹ�������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "�ɲ����˱�ʶ";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "ְ������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��λ";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "���鳤����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��ʦ���";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��Ա��λ״̬";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "�ڸ�ְ������λ����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "�ּ���ְ������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "�����ȼ�";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��λ��ѵ�ϸ�֤���";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "ְ�̸ɲ�����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "ְ��ίԱ��ְ��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "���ʱ��";
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
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.Wedding == 1 ? "�ѻ�" : "δ��";
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
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.EmployeeTypeID == 1 ? "����" : "�ɲ�";
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
                    objSheet.Cells[rowIndex, colIndex] = employeeDetail.Dimission ? "" : "�ڸڹ���";
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
                    jsBlock = "<script>SetPorgressBar('����Ա����Ϣ','" + ((double)((rowIndex - 1) * 100) / (double)objList.Count).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                }

                objApp.Visible = false;

                objbook.Saved = true;
                objbook.SaveCopyAs(filename);

                // �������
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

            Response.Write("<script>top.returnValue='" + strName + "';window.close();</script>");
        }

        /// <summary>
        /// ְ�񼶱�
        /// </summary>
        /// <returns></returns>
        private Hashtable GetEmployeeLevel()
        {
            Hashtable htEmployeeLevel = new Hashtable();

            htEmployeeLevel.Add(0, "");
            htEmployeeLevel.Add(1, "����");
            htEmployeeLevel.Add(2, "����");
            htEmployeeLevel.Add(3, "����");
            htEmployeeLevel.Add(4, "����");
            htEmployeeLevel.Add(5, "����");
            htEmployeeLevel.Add(6, "����");
            htEmployeeLevel.Add(7, "����");
            htEmployeeLevel.Add(8, "����");
            htEmployeeLevel.Add(9, "��Ա");
            htEmployeeLevel.Add(10, "�ɼ�");
            htEmployeeLevel.Add(11, "����");
            htEmployeeLevel.Add(12, "����Ա");
            htEmployeeLevel.Add(13, "����");

            return htEmployeeLevel;
        }

        /// <summary>
        /// ְ�񼶱�
        /// </summary>
        /// <returns></returns>
        private Hashtable GetTeacherType()
        {
            Hashtable hfTeacherType = new Hashtable();

            hfTeacherType.Add(0, "");
            hfTeacherType.Add(1, "��ְ��ʦ");
            hfTeacherType.Add(2, "רְ��ʦ");
            hfTeacherType.Add(3, "����ɲ�");

            return hfTeacherType;
        }

        private Hashtable GetEmployeeTransportType()
        {
            Hashtable htEmployeeTransportType = new Hashtable();
            htEmployeeTransportType.Add(0, "");
            htEmployeeTransportType.Add(1, "������Ա");
            htEmployeeTransportType.Add(2, "������Ա");
            htEmployeeTransportType.Add(3, "������Ա");
            htEmployeeTransportType.Add(4, "���̼�����Ա");
            htEmployeeTransportType.Add(5, "����������Ա");
            htEmployeeTransportType.Add(6, "������Ա");
            return htEmployeeTransportType;
        }

        /// <summary>
        /// ְ�̸ɲ�����
        /// </summary>
        /// <returns></returns>
        private Hashtable GetEducationEmployeeType()
        {
            Hashtable htEducationEmployeeType = new Hashtable();
            htEducationEmployeeType.Add(0, "");
            htEducationEmployeeType.Add(1, "����ɲ�");
            htEducationEmployeeType.Add(2, "רְ��Ա");
            htEducationEmployeeType.Add(3, "����");
            return htEducationEmployeeType;
        }

        /// <summary>
        /// ְ��ίԱ��ְ��

        /// </summary>
        /// <returns></returns>
        private Hashtable GetCommitteeHeadship()
        {
            Hashtable htCommitteeHeadship = new Hashtable();
            htCommitteeHeadship.Add(0, "");
            htCommitteeHeadship.Add(1, "����");
            htCommitteeHeadship.Add(2, "������");
            htCommitteeHeadship.Add(3, "ίԱ");
            return htCommitteeHeadship;
        }
    }
}
