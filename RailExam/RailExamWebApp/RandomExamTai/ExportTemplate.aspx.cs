using System;
using System.Collections.Generic;
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
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamTai
{
    public partial class ExportTemplate : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Get("Type") == "template")
            {
                ExportEmployeeTemplate();
            }
            else 
            {
                ExportEmployeeInfo();
            }
        }



        private void ExportEmployeeTemplate()
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
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "��λ";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "��λ����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "Ա����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "Ա������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();


                colIndex++;
                objSheet.Cells[1, colIndex] = "�Ա�";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "��������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "���֤����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();


                colIndex++;
                objSheet.Cells[1, colIndex] = "�μӹ���ʱ��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                
                colIndex++;
                objSheet.Cells[1, colIndex] = "���뱾��˾ʱ��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "������ò";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "���ѧ��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "�ڸ�";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "�ڲ�";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա��Excelģ��','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
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


        private  void ExportEmployeeInfo()
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


            string filename = "";
            string strName = "";

            OrganizationBLL orgBll = new OrganizationBLL();
            int stationID;

            Organization org = new Organization();
            Organization orgStation = new Organization();
            if (string.IsNullOrEmpty(Request.QueryString.Get("Source")))
            {
                stationID = orgBll.GetStationOrgID(Convert.ToInt32(strOrgID));
                org = orgBll.GetOrganization(Convert.ToInt32(strOrgID));
                orgStation = orgBll.GetOrganization(stationID);

                strName = orgStation.ShortName + "Ա����Ϣ";
                filename = Server.MapPath("/RailExamBao/Excel/" + strName + ".xls");
            }
            else
            {
                strName = "���ӵ���";
                filename = Server.MapPath("/RailExamBao/Excel/" + strName + ".xls");
            }


            if (File.Exists(filename.ToString()))
            {
                File.Delete(filename.ToString());
            }

            #region ԭ΢��������
            /*
            try
            {


                objSheet.Cells.Font.Size = 10;
                objSheet.Cells.Font.Name = "����";

                int colIndex = 1;
                objSheet.Cells[1, colIndex] = "���";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "Ա��ID";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��λ";
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
                objSheet.Cells[1, colIndex] = "���֤��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                //colIndex++;
                //objSheet.Cells[1, colIndex] = "����֤��";
                //((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "�Ա�";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                //colIndex++;
                //objSheet.Cells[1, colIndex] = "����";
                //((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                //colIndex++;
                //objSheet.Cells[1, colIndex] = "����";
                //((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                //colIndex++;
                //objSheet.Cells[1, colIndex] = "����״��";
                //((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "�Ļ��̶�";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "������ò";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


                //colIndex++;
                //objSheet.Cells[1, colIndex] = "������ַ";
                //((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                //colIndex++;
                //objSheet.Cells[1, colIndex] = "ְ�񼶱�";
                //((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��·ʱ��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "�μӹ���ʱ��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "ְ������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "ְ��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "����ְ��ʱ��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "�ִ��¸�λ";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "�ڶ�ְ��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "����ְ��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "���鳤����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "���鳤��������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "�ڲ�";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "�ڸ�";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "����ҵְ������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "�ɲ�����ְ��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "����ְ��Ƹ��ʱ��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "���˼��ܵȼ�";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "���ܵȼ�ȡ��ʱ��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��λ��ѵ�ϸ�֤���";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��λ��ѵ�ϸ�֤�䷢����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "ְ����Ա����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "ְ��ίԱ��ְ��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "�����������";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��ҵѧУ";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��ѧרҵ";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��ҵʱ��";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "ѧУ���";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "��ע";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


                colIndex++;
                objSheet.Cells[1, colIndex] = "�ɼ�ָ����";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "�Ƿ�����Ƭ";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                DataSet ds = new DataSet();

                if(string.IsNullOrEmpty(Request.QueryString.Get("Source")))
                {
                    string strSql = "select a.*,nvl(fingerCount,0) fingerCount,"
                        + " case when c.photo is null then 'û��' else '��' end HasPhoto "
                        + " from Employee a "
                        + "left join (select employee_id,count(*) fingerCount from employee_fingerPrint group by employee_id) b"
                        + " on a.employee_id=b.employee_id "
                        + "left join employee_photo c on a.employee_id=c.employee_id"
                        + " where Org_ID in (select Org_ID from Org where ID_Path||'/' like '" + org.IdPath + "/%')";

                    OracleAccess db = new OracleAccess();
                    ds = db.RunSqlDataSet(strSql);
                }
                else
                {
                    System.Data.DataTable dt = ((System.Data.DataTable)Session["EmployeeDangan"]);
                    ds.Tables.Add(dt);
                    Session.Remove("EmployeeDangan");
                }

                int rowIndex = 2;
                Hashtable htEmployeeType = GetEmployeeType();
                Hashtable htEmployeeTransportType = GetEmployeeTransportType();
                Hashtable htEducationEmployeeType = GetEducationEmployeeType();
                Hashtable htCommitteeHeadship = GetCommitteeHeadship();
                Hashtable htUniversity = GetUniversityType();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    colIndex = 1;
                    objSheet.Cells[rowIndex, colIndex] = rowIndex - 1;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["Employee_ID"].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = orgBll.GetOrganization(orgBll.GetStationOrgID(Convert.ToInt32(dr["Org_ID"].ToString()))).ShortName;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    Organization nowOrg = orgBll.GetOrganization(Convert.ToInt32(dr["Org_ID"].ToString()));
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
                    objSheet.Cells[rowIndex, colIndex] = dr["Employee_Name"].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = "'" + dr["Identity_CardNo"].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //colIndex++;
                    //objSheet.Cells[rowIndex, colIndex] = "'" + employeeDetail.PostNo;
                    //((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["Sex"].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //colIndex++;
                    //objSheet.Cells[rowIndex, colIndex] = employeeDetail.NativePlace;
                    //((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //colIndex++;
                    //objSheet.Cells[rowIndex, colIndex] = employeeDetail.Folk;
                    //((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    //colIndex++;
                    //objSheet.Cells[rowIndex, colIndex] = employeeDetail.Wedding == 1 ? "�ѻ�" : "δ��";
                    //((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    EducationLevelBLL educationLevelBll = new EducationLevelBLL();
                    EducationLevel educationLevel =
                        educationLevelBll.GetEducationLevelByEducationLevelID(Convert.ToInt32(dr["Education_Level_ID"].ToString()));
                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = educationLevel.EducationLevelName;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    PoliticalStatusBLL politicalStatusBll = new PoliticalStatusBLL();
                    PoliticalStatus politicalStatus =
                        politicalStatusBll.GetPoliticalStatusByPoliticalStatusID(Convert.ToInt32(dr["Political_Status_ID"].ToString()));
                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = politicalStatus.PoliticalStatusName;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


                    //colIndex++;
                    //objSheet.Cells[rowIndex, colIndex] = employeeDetail.Address;
                    //((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


                    //colIndex++;
                    //objSheet.Cells[rowIndex, colIndex] = htEmployeeLevel[employeeDetail.EmployeeLevelID].ToString();
                    //((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = Convert.ToDateTime(dr["Birthday"].ToString()).ToString("yyyyMMdd").Replace("00010101", ""); ;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = Convert.ToDateTime(dr["Join_Rail_Date"].ToString()).ToString("yyyyMMdd").Replace("00010101", ""); ;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = Convert.ToDateTime(dr["Begin_Date"].ToString()).ToString("yyyyMMdd").Replace("00010101", ""); ;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] =  htEmployeeType[Convert.ToInt32(dr["Employee_Type_ID"])].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    PostBLL post = new PostBLL();
                    string first = string.Empty;
                    string second = string.Empty;
                    string third = string.Empty;
                    string now = string.Empty;

                    first = post.GetPost(Convert.ToInt32(dr["Post_ID"])).PostName;

                    if (dr["Now_Post_ID"] != DBNull.Value)
                    {
                        now = post.GetPost(Convert.ToInt32(dr["Now_Post_ID"])).PostName;
                    }

                    if (dr["Second_Post_ID"] != DBNull.Value)
                    {
                        second = post.GetPost(Convert.ToInt32(dr["Second_Post_ID"])).PostName;
                    }

                    if(dr["Third_Post_ID"]!=DBNull.Value)
                    {
                       third = post.GetPost(Convert.ToInt32(dr["Third_Post_ID"])).PostName;

                    }

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = first;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = Convert.ToDateTime(dr["Post_Date"].ToString()).ToString("yyyyMMdd").Replace("00010101", ""); ;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = now;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = second;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = third;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    WorkGroupLeaderLevelBLL workGroupLeaderLevelBll = new WorkGroupLeaderLevelBLL();
                    WorkGroupLeaderLevel workGroupLeaderLevel =
                        workGroupLeaderLevelBll.GetWorkGroupLeaderLevelByWorkGroupLeaderLevelID(
                            Convert.ToInt32(dr["WorkGroupLeader_Type_ID"]));
                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = workGroupLeaderLevel.LevelName;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = Convert.ToDateTime(dr["WORKGROUPLEADER_ORDER_DATE"]).ToString("yyyyMMdd").Replace("00010101", "");
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["IsRegistered"].ToString()=="1"? "�ڲ�" : "���ڲ�";
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["IsOnPost"].ToString() == "1" ? "�ڸ�" : "���ڸ�";
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["Employee_Transport_Type_ID"] == DBNull.Value
                                                             ? string.Empty
                                                             : htEmployeeTransportType[Convert.ToInt32(dr["Employee_Transport_Type_ID"].ToString())].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    TechnicianTitleTypeBLL technicianTitleTypeBll = new TechnicianTitleTypeBLL();
                    TechnicianTitleType technicianTitleType =
                        technicianTitleTypeBll.GetTechnicianTitleTypeByTechnicianTitleTypeID(Convert.ToInt32(dr["TECHNICAL_TITLE_ID"].ToString()));
                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = technicianTitleType.TypeName;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["Technical_Title_Date"] == DBNull.Value
                                                             ? string.Empty
                                                             : Convert.ToDateTime(dr["Technical_Title_Date"].ToString()).
                                                                   ToString("yyyyMMdd").Replace("00010101", ""); ;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


                    TechnicianTypeBLL technicianTypeBll = new TechnicianTypeBLL();
                    TechnicianType technicianType =
                        technicianTypeBll.GetTechnicianTypeByTechnicianTypeID(Convert.ToInt32(dr["TECHNICIAN_TYPE_ID"].ToString()));
                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = technicianType.TypeName;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["Technical_Date"] == DBNull.Value
                                                             ? string.Empty
                                                             : Convert.ToDateTime(dr["Technical_Date"].ToString()).
                                                                   ToString("yyyyMMdd").Replace("00010101", ""); ;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["Work_No"] == DBNull.Value ? string.Empty : "'" + dr["Work_No"];
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = Convert.ToDateTime(dr["AWARD_DATE"]).ToString("yyyyMMdd").Replace("00010101", "");

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["EDUCATION_EMPLOYEE_TYPE_ID"] == DBNull.Value
                                                             ? string.Empty
                                                             : htEducationEmployeeType[dr["EDUCATION_EMPLOYEE_TYPE_ID"].ToString()].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["COMMITTEE_HEAD_SHIP_ID"] == DBNull.Value
                                                             ? string.Empty
                                                             : htCommitteeHeadship[dr["COMMITTEE_HEAD_SHIP_ID"].ToString()].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = "'"+dr["TECHNICAL_CODE"];
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["GRADUATE_UNIVERSITY"]==DBNull.Value ? string.Empty:dr["GRADUATE_UNIVERSITY"].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["STUDY_MAJOR"] == DBNull.Value ? string.Empty : dr["STUDY_MAJOR"].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["GRADUATE_DATE"] == DBNull.Value
                                                             ? string.Empty
                                                             : Convert.ToDateTime(dr["GRADUATE_DATE"]).ToString("yyyyMMdd").Replace("00010101", "");

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["UNIVERSITY_TYPE"] == DBNull.Value
                                                             ? string.Empty
                                                             : htUniversity[Convert.ToInt32(dr["UNIVERSITY_TYPE"])].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["Memo"].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["fingerCount"].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["HasPhoto"].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('����Ա����Ϣ','" + ((double)((rowIndex - 1) * 100) / (double)ds.Tables[0].Rows.Count).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();

                    rowIndex++;
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
             */
            #endregion


            if (File.Exists(filename.ToString()))
            {
                File.Delete(filename.ToString());
            }

            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();

            #region �һ��ļ� ������Ϣ
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "NPOI";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "�ļ�������Ϣ"; //���xls�ļ�������Ϣ
                si.ApplicationName = "����������Ϣ"; //���xls�ļ�����������Ϣ
                si.LastAuthor = "��󱣴�����Ϣ"; //���xls�ļ���󱣴�����Ϣ
                si.Comments = "������Ϣ"; //���xls�ļ�������Ϣ
                si.Title = "������Ϣ"; //���xls�ļ�������Ϣ
                si.Subject = "������Ϣ";//����ļ�������Ϣ
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //ȡ���п�
            string[] arrColWidth = { "���", "��λ", "����", "��λ����", "Ա����", "Ա������", "����", "�Ա�", "����", "��������", 
            "���֤����", "�μӹ���ʱ��", "���뱾��˾ʱ��", "������ò", "���ѧ��" ,"�ڸ�","�ڲ�" };

            System.Data.DataTable dt = new System.Data.DataTable();

            if (string.IsNullOrEmpty(Request.QueryString.Get("Source")))
            {
                string strSql = "select a.*,nvl(fingerCount,0) fingerCount,"
                    + " case when c.photo is null then 'û��' else '��' end HasPhoto "
                    + " from Employee a "
                    + "left join (select employee_id,count(*) fingerCount from employee_fingerPrint group by employee_id) b"
                    + " on a.employee_id=b.employee_id "
                    + "left join employee_photo c on a.employee_id=c.employee_id"
                    + " where Org_ID in (select Org_ID from Org where ID_Path||'/' like '" + org.IdPath + "/%')";

                OracleAccess db = new OracleAccess();
                dt = db.RunSqlDataSet(strSql).Tables[0];
            }
            else
            {
                dt = ((System.Data.DataTable)Session["EmployeeDangan"]);
                Session.Remove("EmployeeDangan");
            }

            //Hashtable htEmployeeType = GetEmployeeType();
            //Hashtable htEmployeeTransportType = GetEmployeeTransportType();
            //Hashtable htEducationEmployeeType = GetEducationEmployeeType();
            //Hashtable htCommitteeHeadship = GetCommitteeHeadship();
            //Hashtable htUniversity = GetUniversityType();

            int rowIndex = 0;
            decimal decScore = 0;
            int colIndex;
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                #region �½�������ͷ�������ͷ����ʽ
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = workbook.CreateSheet();
                        decScore = 0;
                    }

                    #region ��ͷ����ʽ
                    {
                        IRow headerRow = sheet.CreateRow(0);
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

                    rowIndex = 1;
                }
                #endregion

                #region �������
                IRow dataRow = sheet.CreateRow(rowIndex);

                for (int x = 0; x < arrColWidth.Length; x++)
                {
                    dataRow.CreateCell(x);
                }

                DataRow dr = dt.Rows[j];
                int k = j + 1;

                colIndex = 0;
                dataRow.Cells[colIndex].SetCellValue(k.ToString());
                
                //colIndex++;
                //dataRow.Cells[colIndex].SetCellValue(dr["Employee_ID"].ToString());
                
                colIndex++;
                dataRow.Cells[colIndex].SetCellValue(orgBll.GetOrganization(orgBll.GetStationOrgID(Convert.ToInt32(dr["Org_ID"].ToString()))).ShortName);

                Organization nowOrg = orgBll.GetOrganization(Convert.ToInt32(dr["Org_ID"].ToString()));
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
                dataRow.Cells[colIndex].SetCellValue(strShop);
                colIndex++;
                PostBLL post = new PostBLL();
                dataRow.Cells[colIndex].SetCellValue(post.GetPost(Convert.ToInt32(dr["Post_ID"])).PostName);
                colIndex++;
                dataRow.Cells[colIndex].SetCellValue(strGroup);
                colIndex++;
                dataRow.Cells[colIndex].SetCellValue(dr["Work_No"].ToString());
                colIndex++;
                dataRow.Cells[colIndex].SetCellValue(dr["Employee_Name"].ToString());
                colIndex++;
                dataRow.Cells[colIndex].SetCellValue(dr["Sex"].ToString());
                colIndex++;
                dataRow.Cells[colIndex].SetCellValue(dr["Native_Place"].ToString());
                colIndex++;
                dataRow.Cells[colIndex].SetCellValue(Convert.ToDateTime(dr["Birthday"].ToString()).ToString("yyyyMMdd").Replace("00010101", ""));
                colIndex++;
                dataRow.Cells[colIndex].SetCellValue(dr["Identity_CardNo"].ToString());
                colIndex++;
                dataRow.Cells[colIndex].SetCellValue(Convert.ToDateTime(dr["Begin_Date"].ToString()).ToString("yyyyMMdd").Replace("00010101", ""));
                colIndex++;
                dataRow.Cells[colIndex].SetCellValue(Convert.ToDateTime(dr["Join_Rail_Date"].ToString()).ToString("yyyyMMdd").Replace("00010101", ""));
                PoliticalStatusBLL politicalStatusBll = new PoliticalStatusBLL();
                PoliticalStatus politicalStatus =
                    politicalStatusBll.GetPoliticalStatusByPoliticalStatusID(Convert.ToInt32(dr["Political_Status_ID"].ToString()));
                colIndex++;
                dataRow.Cells[colIndex].SetCellValue(politicalStatus.PoliticalStatusName);
                EducationLevelBLL educationLevelBll = new EducationLevelBLL();
                EducationLevel educationLevel =
                    educationLevelBll.GetEducationLevelByEducationLevelID(Convert.ToInt32(dr["Education_Level_ID"].ToString()));
                colIndex++;
                dataRow.Cells[colIndex].SetCellValue(educationLevel.EducationLevelName);

                colIndex++;
                dataRow.Cells[colIndex].SetCellValue(dr["IsRegistered"].ToString() == "1" ? "1" : "0");
                colIndex++;
                dataRow.Cells[colIndex].SetCellValue(dr["IsOnPost"].ToString() == "1" ? "1" : "0");

                #endregion

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('����Ա����Ϣ','" + ((double)((j + 1) * 100) / ((double)dt.Rows.Count)).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

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

            Response.Write("<script>top.returnValue='" + strName + "';window.close();</script>");
        }

        /// <summary>
        /// Ա������
        /// </summary>
        /// <returns></returns>
        private Hashtable GetEmployeeType()
        {
            Hashtable htEmployeeType = new Hashtable();

            htEmployeeType.Add(-1, "");
            htEmployeeType.Add(0,"����");
            htEmployeeType.Add(1,"�����ɲ�");
            htEmployeeType.Add(2,"����ɲ�");
            htEmployeeType.Add(3,"�����ɲ�");

            return htEmployeeType;
        }


        /// <summary>
        /// ����ҵ�ĸɲ����˱�ʶ
        /// </summary>
        /// <returns></returns>
        private Hashtable GetEmployeeTransportType()
        {
            Hashtable htEmployeeTransportType = new Hashtable();
            htEmployeeTransportType.Add(0,"������Ա");
            htEmployeeTransportType.Add(1,"������Ա");
            htEmployeeTransportType.Add(2,"������Ա");
            htEmployeeTransportType.Add(3,"������Ա");
            htEmployeeTransportType.Add(-1, "");

            return htEmployeeTransportType;
        }

        private Hashtable GetUniversityType()
        {
            Hashtable hfUniversityType = new Hashtable();
            hfUniversityType.Add(1,"ȫ����");
            hfUniversityType.Add(2, "�������");
            hfUniversityType.Add(3, "��ѧ����");
            hfUniversityType.Add(4, "��У����");
            hfUniversityType.Add(5, "����ѧϰ");
            hfUniversityType.Add(6, "���ѧϰ");
            hfUniversityType.Add(7, "ְУѧϰ");
            hfUniversityType.Add(8, "ҵУѧϰ");
            hfUniversityType.Add(9, "ҹУѧϰ");
            hfUniversityType.Add(10, "���˽���");
            hfUniversityType.Add( 0,"");
            return hfUniversityType;
        }


        /// <summary>
        /// ְ�̸ɲ�����
        /// </summary>
        /// <returns></returns>
        private Hashtable GetEducationEmployeeType()
        {
            Hashtable htEducationEmployeeType = new Hashtable();

            OracleAccess db = new OracleAccess();
            string strSql = "select * from ZJ_EDUCATION_EMPLOYEE_TYPE";
            DataSet ds = db.RunSqlDataSet(strSql);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                htEducationEmployeeType.Add(dr["EDUCATION_EMPLOYEE_TYPE_ID"].ToString(),dr["EDUCATION_EMPLOYEE_TYPE_NAME"].ToString());
            }

            htEducationEmployeeType.Add("-1", "");

            return htEducationEmployeeType;
        }

        /// <summary>
        /// ְ��ίԱ��ְ��

        /// </summary>
        /// <returns></returns>
        private Hashtable GetCommitteeHeadship()
        {
            Hashtable htCommitteeHeadship = new Hashtable();
            OracleAccess db = new OracleAccess();
            string strSql = "select * from ZJ_COMMITTEE_HEAD_SHIP";
            DataSet ds = db.RunSqlDataSet(strSql);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                htCommitteeHeadship.Add(dr["COMMITTEE_HEAD_SHIP_ID"].ToString(),dr["COMMITTEE_HEAD_SHIP_NAME"].ToString());
            }

            htCommitteeHeadship.Add("-1", "");
            return htCommitteeHeadship;
        }
    }
}
