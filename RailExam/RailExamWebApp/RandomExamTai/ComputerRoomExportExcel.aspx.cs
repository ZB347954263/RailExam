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
            //���������ݵ�����Դ
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

            #region ��ȡExcel�ļ�

            try
            {
                //����ExcelApp   
                objApp = new Excel.Application();
                //Excel����ʾ   
                objApp.Visible = false;
                //����Books   
                objBooks = objApp.Workbooks;
                //��Excel�ļ�   
                objBooks.Open(strPath,
                              Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                              Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                              Type.Missing, Type.Missing, Type.Missing,
                              Type.Missing, Type.Missing, Type.Missing);
                //ȡ��Book   
                objBook = objBooks.get_Item(1);
                //ȡ��Sheets   
                objSheets = objBook.Worksheets;
                //ȡ��Sheet   
                objSheet = (Excel._Worksheet)objSheets.get_Item(1);
                //ȡ��Range   

                int rowNum = objSheet.UsedRange.Rows.Count;
                int colNum = objSheet.UsedRange.Columns.Count;

                // ���� ProgressBar.htm ��ʾ����������
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

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["���"]], objSheet.Cells[i, htCol["���"]]));
                    newRow["���"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��λ"]], objSheet.Cells[i, htCol["��λ"]]));
                    newRow["��λ"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["΢����������"]], objSheet.Cells[i, htCol["΢����������"]]));
                    newRow["΢����������"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["΢�����ҵ�ַ"]], objSheet.Cells[i, htCol["΢�����ҵ�ַ"]]));
                    newRow["΢�����ҵ�ַ"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��ϵ��"]], objSheet.Cells[i, htCol["��ϵ��"]]));
                    newRow["��ϵ��"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��ϵ�绰"]], objSheet.Cells[i, htCol["��ϵ�绰"]]));
                    newRow["��ϵ�绰"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["���������Ƽ��ͺ�"]], objSheet.Cells[i, htCol["���������Ƽ��ͺ�"]]));
                    newRow["���������Ƽ��ͺ�"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��������ŵص�"]], objSheet.Cells[i, htCol["��������ŵص�"]]));
                    newRow["��������ŵص�"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["������IP��ַ"]], objSheet.Cells[i, htCol["������IP��ַ"]]));
                    newRow["������IP��ַ"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��λ��"]], objSheet.Cells[i, htCol["��λ��"]]));
                    newRow["��λ��"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��λ"]], objSheet.Cells[i, htCol["��λ"]]));
                    newRow["��λ"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["MAC��ַ"]], objSheet.Cells[i, htCol["MAC��ַ"]]));
                    newRow["MAC��ַ"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["����"]], objSheet.Cells[i, htCol["����"]]));
                    newRow["����"] = range.Value2;

                    range = ((Excel.Range)objSheet.get_Range(objSheet.Cells[i, htCol["��"]], objSheet.Cells[i, htCol["��"]]));
                    newRow["��"] = range.Value2;

                    dtItem.Rows.Add(newRow);

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('���ڶ�ȡExcel�ļ�','" + ((double)((i - 1) * 100) / (double)rowNum).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                }

                ds.Tables.Add(dtItem);

                // �������
                jsBlock = "<script>SetCompleted('Excel���ݶ�ȡ���'); </script>";
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
                Response.Write("<script>window.returnValue='����Excel�ļ���ʽ',window.close();</script>");
                return;
            }

            #endregion

            #region ��������

            System.Threading.Thread.Sleep(10);
            jsBlock = "<script>SetPorgressBar('��׼�����Excel����','0.00'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            if (ds.Tables[0].Rows.Count == 0)
            {
                Response.Write("<script>window.returnValue='Excel��û���κμ�¼����˶�',window.close();</script>");
                return;
            }

            DataColumn dc1 = ds.Tables[0].Columns.Add("ItemType");
            DataColumn dc2 = ds.Tables[0].Columns.Add("BookID");
            int index = 1;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                System.Threading.Thread.Sleep(10);
                jsBlock = "<script>SetPorgressBar('���ڼ��Excel����','" + ((double)(index * 100) / (double)ds.Tables[0].Rows.Count).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();
                index = index + 1;
            }

            // �������
            jsBlock = "<script>SetCompleted('Excel���ݼ�����'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            #endregion

            if (dt.Rows.Count > 0)
            {
                Session["table"] = dt;
                if (File.Exists(strPath))
                    File.Delete(strPath);
                Response.Write("<script>window.returnValue='refresh|����Excel�ļ�����',window.close();</script>");
                return;
            }
            else
            {
                dt.Clear();
                Session["table"] = dt;
            }

            #region ��������

            System.Threading.Thread.Sleep(10);
            jsBlock = "<script>SetPorgressBar('��׼����������','0.00'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

            try
            {
                int count = 0;
                int m = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {


                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('���ڵ�������','" + ((double)(m * 100) / (double)ds.Tables[0].Rows.Count).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                    m = m + 1;
                }

                jsBlock = "<script>SetCompleted('���⵼�����'); </script>";
                Response.Write(jsBlock);
                Response.Flush();
                strMessage = "����ɹ���";
            }
            catch
            {
                strMessage = "����ʧ��!";
            }

            if (File.Exists(strPath))
                File.Delete(strPath);
            Response.Write("<script>window.returnValue='refresh|" + strMessage + "';window.close();</script>");
            #endregion

        }

        /// <summary>
        /// ��Ӵ�����Ϣ
        /// </summary>
        /// <param name="dt">������ϢDataTable</param>
        /// <param name="dr">������ԴDataRow</param>
        /// <param name="strError">����ԭ��</param>
        private void AddError(DataTable dt, DataRow dr, string strError)
        {
            DataRow drnew = dt.NewRow();
            drnew["ComputerRoomName"] = dr["΢����������"].ToString();
            drnew["SeatCount"] = dr["��λ��"].ToString();
            drnew["Seat"] = dr["��λ"].ToString();
            drnew["MACAddress"] = dr["MAC��ַ"].ToString();
            drnew["ErrorReason"] = strError;
            dt.Rows.Add(drnew);
        }
    }
}
