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

namespace RailExamWebApp.TrainManage
{
    public partial class ExportExcel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Get("Type") == "upEmployee")
            {
                ExportUpEmployee();
            }
        }

        private void ExportUpEmployee()
        {
            string strSql = @"select  GetOrgName(GetStationOrgID(b.org_Id))   UnitName,
                            getworkshopname(b.org_id) WorkShopName,
                            case when c.level_num=4  then c.Short_Name else null end WorkGroupName,
                            Employee_Name, Identity_CardNo,
                            e.Train_Plan_Name,
                            d.Class_Name,f.Post_Name
                             from zj_train_plan_employee t 
                            inner join Employee b on t.Employee_ID=b.Employee_ID
                            inner join Org c on b.Org_ID=c.Org_ID
                            inner join ZJ_Train_Plan_Post_Class d on t.Train_Plan_Post_Class_ID=d.Train_Plan_Post_Class_ID
                            inner join ZJ_Train_Plan e on d.Train_Plan_ID=e.Train_Plan_ID
                            inner join Post f on b.Post_ID = f.Post_ID
                            where t.Train_Plan_Post_Class_Org_ID=" + Request.QueryString.Get("classOrgID");
            OracleAccess db = new OracleAccess();
            DataSet ds = db.RunSqlDataSet(strSql);
            if (ds.Tables[0].Rows.Count == 0)
            {
                Response.Write("<script>alert('没有已上报的员工信息！');window.close();</script>");
                return;
            }

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
            string strFileName = "";
            try
            {
                int count = ds.Tables[0].Rows.Count;
                strName = "Excel";
                strFileName = ds.Tables[0].Rows[0]["UnitName"].ToString() + ds.Tables[0].Rows[0]["Class_Name"].ToString() + "上报人员";
                filename = Server.MapPath("/RailExamBao/Excel/" + strName + ".xls");

                objSheet.Cells.Font.Size = 10;
                objSheet.Cells.Font.Name = "宋体";

                int colIndex = 1;
                objSheet.Cells[1, colIndex] = "序号";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "站段";
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

                colIndex++;
                objSheet.Cells[1, colIndex] = "职名";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "上报培训计划名";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                colIndex++;
                objSheet.Cells[1, colIndex] = "上报计划培训班名";
                ((Excel.Range)objSheet.Cells[1, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('导出培训计划上报人员','" + ((double)(1 * 100) / (double)count + 1).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                int rowIndex = 2;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    colIndex = 1;
                    objSheet.Cells[rowIndex, colIndex] = rowIndex - 1;
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["UnitName"].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["WorkShopName"].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["WorkGroupName"].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["Employee_Name"].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = "'" + dr["Identity_CardNo"];
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["Post_Name"].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["Train_Plan_Name"].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    colIndex++;
                    objSheet.Cells[rowIndex, colIndex] = dr["Class_Name"].ToString();
                    ((Excel.Range)objSheet.Cells[rowIndex, colIndex]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('导出培训计划上报人员','" + ((double)(rowIndex * 100) / (double)count + 1).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();

                    rowIndex++;
                }

                objSheet.Cells.Columns.AutoFit();

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

            Response.Write("<script>top.returnValue='" + strName + "|" + strFileName + "';window.close();</script>");
        }
    }
}
