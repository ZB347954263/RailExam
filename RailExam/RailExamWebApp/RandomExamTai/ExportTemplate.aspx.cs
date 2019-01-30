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
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "单位";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "部门";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "岗位名称";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "员工组";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "员工编码";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "姓名";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();


                colIndex++;
                objSheet.Cells[1, colIndex] = "性别";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "籍贯";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "出生日期";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "身份证件号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();


                colIndex++;
                objSheet.Cells[1, colIndex] = "参加工作时间";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                
                colIndex++;
                objSheet.Cells[1, colIndex] = "进入本公司时间";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "政治面貌";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "最高学历";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "在岗";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                colIndex++;
                objSheet.Cells[1, colIndex] = "在册";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出员工Excel模板','" + ((double)((colIndex) * 100) / (double)15).ToString("0.00") + "'); </script>";
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

                strName = orgStation.ShortName + "员工信息";
                filename = Server.MapPath("/RailExamBao/Excel/" + strName + ".xls");
            }
            else
            {
                strName = "电子档案";
                filename = Server.MapPath("/RailExamBao/Excel/" + strName + ".xls");
            }


            if (File.Exists(filename.ToString()))
            {
                File.Delete(filename.ToString());
            }

            #region 原微软导出方法
            /*
            try
            {


                objSheet.Cells.Font.Size = 10;
                objSheet.Cells.Font.Name = "宋体";

                int colIndex = 1;
                objSheet.Cells[1, colIndex] = "序号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "员工ID";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "单位";
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
                objSheet.Cells[1, colIndex] = "身份证号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                //colIndex++;
                //objSheet.Cells[1, colIndex] = "工作证号";
                //((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "性别";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                //colIndex++;
                //objSheet.Cells[1, colIndex] = "籍贯";
                //((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                //colIndex++;
                //objSheet.Cells[1, colIndex] = "民族";
                //((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                //colIndex++;
                //objSheet.Cells[1, colIndex] = "婚姻状况";
                //((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "文化程度";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "政治面貌";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


                //colIndex++;
                //objSheet.Cells[1, colIndex] = "工作地址";
                //((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                //colIndex++;
                //objSheet.Cells[1, colIndex] = "职务级别";
                //((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "出生日期";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "入路时间";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "参加工作时间";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "职工类型";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "职名";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "任现职名时间";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "现从事岗位";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "第二职名";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "第三职名";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "班组长类型";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "班组长下令日期";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "在册";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "在岗";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "运输业职工类型";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "干部技术职称";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "技术职称聘任时间";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "工人技能等级";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "技能等级取得时间";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "岗位培训合格证编号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "岗位培训合格证颁发日期";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "职教人员类型";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "职教委员会职务";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "技术档案编号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "毕业学校";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "所学专业";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "毕业时间";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "学校类别";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "备注";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


                colIndex++;
                objSheet.Cells[1, colIndex] = "采集指纹数";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "是否有照片";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                DataSet ds = new DataSet();

                if(string.IsNullOrEmpty(Request.QueryString.Get("Source")))
                {
                    string strSql = "select a.*,nvl(fingerCount,0) fingerCount,"
                        + " case when c.photo is null then '没有' else '有' end HasPhoto "
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
                    //objSheet.Cells[rowIndex, colIndex] = employeeDetail.Wedding == 1 ? "已婚" : "未婚";
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
                    objSheet.Cells[rowIndex, colIndex] = dr["IsRegistered"].ToString()=="1"? "在册" : "不在册";
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["IsOnPost"].ToString() == "1" ? "在岗" : "不在岗";
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
                    jsBlock = "<script>SetPorgressBar('导出员工信息','" + ((double)((rowIndex - 1) * 100) / (double)ds.Tables[0].Rows.Count).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();

                    rowIndex++;
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
             */
            #endregion


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
                si.Subject = "主题信息";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            string[] arrColWidth = { "序号", "单位", "部门", "岗位名称", "员工组", "员工编码", "姓名", "性别", "籍贯", "出生日期", 
            "身份证件号", "参加工作时间", "进入本公司时间", "政治面貌", "最高学历" ,"在岗","在册" };

            System.Data.DataTable dt = new System.Data.DataTable();

            if (string.IsNullOrEmpty(Request.QueryString.Get("Source")))
            {
                string strSql = "select a.*,nvl(fingerCount,0) fingerCount,"
                    + " case when c.photo is null then '没有' else '有' end HasPhoto "
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
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = workbook.CreateSheet();
                        decScore = 0;
                    }

                    #region 列头及样式
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

                #region 填充内容
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
                jsBlock = "<script>SetPorgressBar('导出员工信息','" + ((double)((j + 1) * 100) / ((double)dt.Rows.Count)).ToString("0.00") + "'); </script>";
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
        /// 员工类型
        /// </summary>
        /// <returns></returns>
        private Hashtable GetEmployeeType()
        {
            Hashtable htEmployeeType = new Hashtable();

            htEmployeeType.Add(-1, "");
            htEmployeeType.Add(0,"工人");
            htEmployeeType.Add(1,"技术干部");
            htEmployeeType.Add(2,"管理干部");
            htEmployeeType.Add(3,"其他干部");

            return htEmployeeType;
        }


        /// <summary>
        /// 运输业的干部工人标识
        /// </summary>
        /// <returns></returns>
        private Hashtable GetEmployeeTransportType()
        {
            Hashtable htEmployeeTransportType = new Hashtable();
            htEmployeeTransportType.Add(0,"生产人员");
            htEmployeeTransportType.Add(1,"管理人员");
            htEmployeeTransportType.Add(2,"服务人员");
            htEmployeeTransportType.Add(3,"其他人员");
            htEmployeeTransportType.Add(-1, "");

            return htEmployeeTransportType;
        }

        private Hashtable GetUniversityType()
        {
            Hashtable hfUniversityType = new Hashtable();
            hfUniversityType.Add(1,"全日制");
            hfUniversityType.Add(2, "网络教育");
            hfUniversityType.Add(3, "自学考试");
            hfUniversityType.Add(4, "党校函授");
            hfUniversityType.Add(5, "函授学习");
            hfUniversityType.Add(6, "电大学习");
            hfUniversityType.Add(7, "职校学习");
            hfUniversityType.Add(8, "业校学习");
            hfUniversityType.Add(9, "夜校学习");
            hfUniversityType.Add(10, "成人教育");
            hfUniversityType.Add( 0,"");
            return hfUniversityType;
        }


        /// <summary>
        /// 职教干部类型
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
        /// 职教委员会职务

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
