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
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamTai
{
    public partial class ComputerRoomExportExcel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ExportComputerRoom();
            }
        }

        private void ExportComputerRoom()
        {
            //不符合数据的数据源
            DataTable dt = new DataTable();
            DataColumn dcnew1 = dt.Columns.Add("ComputerRoomName");
            DataColumn dcnew2 = dt.Columns.Add("SeatCount");
            DataColumn dcnew5 = dt.Columns.Add("Seat");
            DataColumn dcnew6 = dt.Columns.Add("MACAddress");
            DataColumn dcnew7 = dt.Columns.Add("ErrorReason");

            string strFileName = Server.UrlDecode(Request.QueryString.Get("FileName"));
            string jsBlock;
            string strPath = Server.MapPath("/RailExamBao/Excel/" + strFileName);
            bool isClose = false;
            string strMessage;


            Excel.Application objApp = null;
            Excel._Workbook objBook = null;
            Excel.Workbooks objBooks = null;
            Excel.Sheets objSheets = null;
            Excel._Worksheet objSheet = null;
            Excel.Range range = null;
            DataSet ds = new DataSet();

            #region 读取Excel文件

            try
            {
                //生成ExcelApp   
                objApp = new Excel.Application();
                //Excel不显示   
                objApp.Visible = false;
                //生成Books   
                objBooks = objApp.Workbooks;
                //打开Excel文件   
                objBooks.Open(strPath,
                              Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                              Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                              Type.Missing, Type.Missing, Type.Missing,
                              Type.Missing, Type.Missing, Type.Missing);
                //取得Book   
                objBook = objBooks.get_Item(1);
                //取得Sheets   
                objSheets = objBook.Worksheets;
                //取得Sheet   
                objSheet = (Excel._Worksheet)objSheets.get_Item(1);
                //取得Range   

                int rowNum = objSheet.UsedRange.Rows.Count;
                int colNum = objSheet.UsedRange.Columns.Count;

                // 根据 ProgressBar.htm 显示进度条界面
                string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
                StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
                string html = reader.ReadToEnd();
                reader.Close();
                Response.Write(html);
                Response.Flush();
                System.Threading.Thread.Sleep(200);

                DataTable dtItem = new DataTable();
                Hashtable htCol = new Hashtable();
                for (int i = 1; i <= colNum; i++)
                {
                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[1, i], objSheet.Cells[1, i]));
                    DataColumn dc = dtItem.Columns.Add(range.Value2.ToString());
                    htCol[range.Value2.ToString()] = i;
                }

                DataRow newRow = null;
                for (int i = 2; i <= rowNum; i++)
                {
                    newRow = dtItem.NewRow();

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["序号"]], objSheet.Cells[i, htCol["序号"]]));
                    newRow["序号"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["单位"]], objSheet.Cells[i, htCol["单位"]]));
                    newRow["单位"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["微机教室名称"]], objSheet.Cells[i, htCol["微机教室名称"]]));
                    newRow["微机教室名称"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["微机教室地址"]], objSheet.Cells[i, htCol["微机教室地址"]]));
                    newRow["微机教室地址"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["联系人"]], objSheet.Cells[i, htCol["联系人"]]));
                    newRow["联系人"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["联系电话"]], objSheet.Cells[i, htCol["联系电话"]]));
                    newRow["联系电话"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["服务器名称及型号"]], objSheet.Cells[i, htCol["服务器名称及型号"]]));
                    newRow["服务器名称及型号"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["服务器存放地点"]], objSheet.Cells[i, htCol["服务器存放地点"]]));
                    newRow["服务器存放地点"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["服务器IP地址"]], objSheet.Cells[i, htCol["服务器IP地址"]]));
                    newRow["服务器IP地址"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["机位数"]], objSheet.Cells[i, htCol["机位数"]]));
                    newRow["机位数"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["机位"]], objSheet.Cells[i, htCol["机位"]]));
                    newRow["机位"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["MAC地址"]], objSheet.Cells[i, htCol["MAC地址"]]));
                    newRow["MAC地址"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["良好"]], objSheet.Cells[i, htCol["良好"]]));
                    newRow["良好"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["损坏"]], objSheet.Cells[i, htCol["损坏"]]));
                    newRow["损坏"] = range.Value2;

                    dtItem.Rows.Add(newRow);

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('正在读取Excel文件','" + ((double)((i - 1) * 100) / (double)rowNum).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                }

                ds.Tables.Add(dtItem);

                // 处理完成
                jsBlock = "<script>SetCompleted('Excel数据读取完毕'); </script>";
                Response.Write(jsBlock);
                Response.Flush();
            }
            catch
            {
                isClose = true;
            }
            finally
            {
                objBook.Close(Type.Missing, strPath, Type.Missing);
                objBooks.Close();
                objApp.Application.Workbooks.Close();
                objApp.Application.Quit();
                objApp.Quit();
                GC.Collect();
            }

            if (isClose)
            {
                if (File.Exists(strPath))
                    File.Delete(strPath);
                Response.Write("<script>window.returnValue='请检查Excel文件格式',window.close();</script>");
                return;
            }

            #endregion

            #region 检验数据

            System.Threading.Thread.Sleep(10);
            jsBlock = "<script>SetPorgressBar('正准备检测Excel数据','0.00'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            if (ds.Tables[0].Rows.Count == 0)
            {
                Response.Write("<script>window.returnValue='Excel中没有任何记录，请核对',window.close();</script>");
                return;
            }

            DataColumn dc1 = ds.Tables[0].Columns.Add("ItemType");
            DataColumn dc2 = ds.Tables[0].Columns.Add("BookID");
            int index = 1;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('正在检测Excel数据','" + ((double)(index * 100) / (double)ds.Tables[0].Rows.Count).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();
                index = index + 1;
            }

            // 处理完成
            jsBlock = "<script>SetCompleted('Excel数据检测完毕'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            #endregion

            if (dt.Rows.Count > 0)
            {
                Session["table"] = dt;
                if (File.Exists(strPath))
                    File.Delete(strPath);
                Response.Write("<script>window.returnValue='refresh|请检查Excel文件数据',window.close();</script>");
                return;
            }
            else
            {
                dt.Clear();
                Session["table"] = dt;
            }

            #region 导入数据

            System.Threading.Thread.Sleep(10);
            jsBlock = "<script>SetPorgressBar('正准备导入试题','0.00'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            try
            {
                int count = 0;
                int m = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {


                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('正在导入试题','" + ((double)(m * 100) / (double)ds.Tables[0].Rows.Count).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    m = m + 1;
                }

                jsBlock = "<script>SetCompleted('试题导入完毕'); </script>";
                Response.Write(jsBlock);
                Response.Flush();
                strMessage = "导入成功！";
            }
            catch
            {
                strMessage = "导入失败!";
            }

            if (File.Exists(strPath))
                File.Delete(strPath);
            Response.Write("<script>window.returnValue='refresh|" + strMessage + "';window.close();</script>");
            #endregion

        }

        /// <summary>
        /// 添加错误信息
        /// </summary>
        /// <param name="dt">错误信息DataTable</param>
        /// <param name="dr">数据来源DataRow</param>
        /// <param name="strError">错误原因</param>
        private void AddError(DataTable dt, DataRow dr, string strError)
        {
            DataRow drnew = dt.NewRow();
            drnew["ComputerRoomName"] = dr["微机教室名称"].ToString();
            drnew["SeatCount"] = dr["机位数"].ToString();
            drnew["Seat"] = dr["机位"].ToString();
            drnew["MACAddress"] = dr["MAC地址"].ToString();
            drnew["ErrorReason"] = strError;
            dt.Rows.Add(drnew);
        }
    }
}
