using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using Microsoft.Office.Interop.Owc11;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class ExportArrangeExel : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ExportArrange();
            }
        }

        private void ExportArrange()
        {
            // 根据 ProgressBar.htm 显示进度条界面
            string templateFileName = Path.Combine(Server.MapPath("."), "ProgressBar.htm");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
            System.Threading.Thread.Sleep(200);

            string strExamId = Request.QueryString.Get("id");

            RandomExamArrangeBLL eaBll = new RandomExamArrangeBLL();
            IList<RailExam.Model.RandomExamArrange> ExamArranges = eaBll.GetRandomExamArranges(int.Parse(strExamId));

            RandomExamBLL objBll = new RandomExamBLL();
            RailExam.Model.RandomExam objRandomExam = objBll.GetExam(Convert.ToInt32(Request.QueryString.Get("id")));

            EmployeeBLL psBLL = new EmployeeBLL();
            DataSet ds = new DataSet();

            string[] str = ExamArranges[0].UserIds.Split(',');
            IList<Employee> objList = new List<Employee>();

            if (str[0] != "")
            {
                OracleAccess db = new OracleAccess();
                string strSql;

                OrganizationBLL orgBll = new OrganizationBLL();
                for (int i = 0; i < str.Length; i++)
                {
                    Employee obj = psBLL.GetChooseEmployeeInfo(str[i]);
                    obj.RowNum = i + 1;

                    if (PrjPub.CurrentLoginUser.RoleID != 1)
                    {
                        if (orgBll.GetStationOrgID(obj.OrgID) == PrjPub.CurrentLoginUser.StationOrgID)
                        {
                                objList.Add(obj);
                        }
                    }
                    else
                    {
                        objList.Add(obj);
                    }
                }

                if (objList.Count > 0)
                {
                    ds.Tables.Add(ConvertToDataTable((IList) objList));

                    if (ViewState["Sort"] != null)
                    {
                        ds.Tables[0].DefaultView.Sort = ViewState["Sort"].ToString();
                    }

                    DataColumn dc = ds.Tables[0].Columns.Add("ComputeRoom");

                    strSql = "select a.*,c.Short_Name||'-'||b.Computer_Room_Name as ComputeRoom "
                             + " from Random_Exam_Arrange_Detail a "
                             + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
                             + " inner join Org c on b.Org_ID=c.Org_ID"
                             + " where Random_Exam_ID='" + strExamId + "'";
                    DataSet dsDetail = db.RunSqlDataSet(strSql);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string strUser = "," + dr["EmployeeID"] + ",";
                        DataRow[] drs = dsDetail.Tables[0].Select("','+User_Ids+',' like '%" + strUser + "%'");

                        if (drs.Length > 0)
                        {
                            dr["ComputeRoom"] = drs[0]["ComputeRoom"].ToString();
                        }
                        else
                        {
                            dr["ComputeRoom"] = string.Empty;
                        }
                    }
                }

                System.Threading.Thread.Sleep(10);
                string jsBlock = "<script>SetPorgressBar('导出考生信息','" + ((1 * 100) / ((double)(ds.Tables[0].Rows.Count + 1))).ToString("0.00") + "'); </script>";
                Response.Write(jsBlock);
                Response.Flush();

                #region OWC11
                /*
                SpreadsheetClass xlsheet = new SpreadsheetClass();
                Worksheet ws = (Worksheet)xlsheet.Worksheets[1];
                ws.Cells.Font.set_Size(10);
                ws.Cells.Font.set_Name("宋体");

                ws.Cells[1, 1] = objRandomExam.ExamName + " 参加考试学员名单";
                Range range = ws.get_Range(ws.Cells[1, 1], ws.Cells[1, 7]);
                range.set_MergeCells(true);
                range.set_HorizontalAlignment(XlHAlign.xlHAlignCenter);
                range.Font.set_Name("宋体");


                //write headertext
                ws.Cells[2, 1] = "序号";
                ((Range)ws.Cells[2, 1]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);


                ws.Cells[2, 2] = "姓名";
                ws.get_Range(ws.Cells[2, 2], ws.Cells[2, 2]).set_MergeCells(true);
                ws.get_Range(ws.Cells[2, 2], ws.Cells[2, 2]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

                if (PrjPub.IsWuhan())
                {
                    ws.Cells[2, 3] = "员工编码";
                }
                else
                {
                    ws.Cells[2, 3] = "工资编号";
                }
                ws.get_Range(ws.Cells[2, 3], ws.Cells[2, 3]).set_MergeCells(true);
                ws.get_Range(ws.Cells[2, 3], ws.Cells[2, 3]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

                ws.Cells[2, 4] = "职名";
                ws.get_Range(ws.Cells[2, 4], ws.Cells[2, 4]).set_MergeCells(true);
                ws.get_Range(ws.Cells[2, 4], ws.Cells[2, 4]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

                ws.Cells[2, 5] = "组织机构";
                ws.get_Range(ws.Cells[2, 5], ws.Cells[2, 7]).set_MergeCells(true);
                ws.get_Range(ws.Cells[2, 5], ws.Cells[2, 7]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

                ws.Cells[2, 8] = "考试地点";
                ws.get_Range(ws.Cells[2, 8], ws.Cells[2, 10]).set_MergeCells(true);
                ws.get_Range(ws.Cells[2, 8], ws.Cells[2, 10]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

                int j = 0;
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    ws.Cells[3 + j, 1] = j + 1;

                    ws.Cells[3 + j, 2] = dr["EmployeeName"].ToString();
                    ws.get_Range(ws.Cells[3 + j, 2], ws.Cells[3 + j, 2]).set_MergeCells(true);
                    ws.get_Range(ws.Cells[3 + j, 2], ws.Cells[3 + j, 2]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);

                    ws.Cells[3 + j, 3] = "'" + dr["StrWorkNo"].ToString(); 
                    ws.get_Range(ws.Cells[3 + j, 3], ws.Cells[3 + j, 3]).set_MergeCells(true);
                    ws.get_Range(ws.Cells[3 + j, 3], ws.Cells[3 + j, 3]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);


                    ws.Cells[3 + j, 4] = dr["PostName"].ToString();
                    ws.get_Range(ws.Cells[3 + j, 4], ws.Cells[3 + j, 4]).set_MergeCells(true);
                    ws.get_Range(ws.Cells[3 + j, 4], ws.Cells[3 + j, 4]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);

                    ws.Cells[3 + j, 5] = dr["OrgName"].ToString();
                    ws.get_Range(ws.Cells[3 + j, 5], ws.Cells[3 + j, 7]).set_MergeCells(true);
                    ws.get_Range(ws.Cells[3 + j, 5], ws.Cells[3 + j, 7]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

                    ws.Cells[3 + j, 8] = dr["ComputeRoom"].ToString();
                    ws.get_Range(ws.Cells[3 + j, 8], ws.Cells[3 + j, 10]).set_MergeCells(true);
                    ws.get_Range(ws.Cells[3 + j, 8], ws.Cells[3 + j, 10]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

                    j++;

                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('导出考生信息','" + (((j + 1) * 100) / ((double)(ds.Tables[0].Rows.Count + 1))).ToString("0.00") + "'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();
                }

                ws.Name = "1-1";
                ws.Cells.Columns.AutoFit();


                ((Worksheet)xlsheet.Worksheets[1]).Activate();

                string path = Server.MapPath("../Excel/Excel.xls");
                if (File.Exists(path))
                    File.Delete(path);
                xlsheet.Export(path, SheetExportActionEnum.ssExportActionNone, SheetExportFormat.ssExportAsAppropriate);
                 */
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
					filename = Server.MapPath("/RailExamBao/Excel/Excel.xls");

					if (File.Exists(filename.ToString()))
					{
						File.Delete(filename.ToString());
					}
					objSheet.Cells.Font.Size = 10;
					objSheet.Cells.Font.Name = "宋体";

					objSheet.Cells[1, 1] = objRandomExam.ExamName + " 参加考试学员名单";
					rang1 = objSheet.get_Range(objSheet.Cells[1, 1], objSheet.Cells[1, 6]);
					rang1.Merge(0);
					rang1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
					rang1.Font.Bold = true;
					objSheet.Cells.Font.Size = 17;
					objSheet.Cells.Font.Name = "宋体";


					objSheet.Cells.Font.Size = 12;
					objSheet.Cells.Font.Name = "宋体";

					//write headertext
					objSheet.Cells[2, 1] = "序号";
					((Excel.Range)objSheet.Cells[2, 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;


					objSheet.Cells[2, 2] = "姓名";
					objSheet.get_Range(objSheet.Cells[2, 2], objSheet.Cells[2, 2]).Merge(0);
					objSheet.get_Range(objSheet.Cells[2, 2], objSheet.Cells[2, 2]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    objSheet.Cells[2, 3] = "员工编码";
                    objSheet.get_Range(objSheet.Cells[2, 3], objSheet.Cells[2, 3]).Merge(0);
                    objSheet.get_Range(objSheet.Cells[2, 3], objSheet.Cells[2, 3]).HorizontalAlignment =XlHAlign.xlHAlignCenter;

				    objSheet.Cells[2, 4] = "职名";
					objSheet.get_Range(objSheet.Cells[2, 4], objSheet.Cells[2, 4]).Merge(0);
					objSheet.get_Range(objSheet.Cells[2, 4], objSheet.Cells[2, 4]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

					objSheet.Cells[2, 5] = "组织机构（车间）";
					objSheet.get_Range(objSheet.Cells[2, 5], objSheet.Cells[2, 5]).Merge(0);
					objSheet.get_Range(objSheet.Cells[2, 5], objSheet.Cells[2, 5]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    objSheet.Cells[2, 6] = "考试地点";
                    objSheet.get_Range(objSheet.Cells[2, 6], objSheet.Cells[2, 6]).Merge(0);
                    objSheet.get_Range(objSheet.Cells[2, 6], objSheet.Cells[2, 6]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

					 int j = 0;
					foreach(DataRow dr in ds.Tables[0].Rows)
                    {
						objSheet.Cells[3 + j, 1] = j + 1;

						objSheet.Cells[3 + j, 2] =  dr["EmployeeName"].ToString();
						objSheet.get_Range(objSheet.Cells[6 + j, 2], objSheet.Cells[6 + j, 2]).Merge(0);
						objSheet.get_Range(objSheet.Cells[6 + j, 2], objSheet.Cells[6 + j, 2]).HorizontalAlignment =
							XlHAlign.xlHAlignLeft;

                        objSheet.Cells[3 + j, 3] =  "'" + dr["StrWorkNo"].ToString(); 
                        objSheet.get_Range(objSheet.Cells[3 + j, 3], objSheet.Cells[3 + j, 3]).Merge(0);
                        objSheet.get_Range(objSheet.Cells[3 + j, 3], objSheet.Cells[3 + j, 3]).HorizontalAlignment =
                            XlHAlign.xlHAlignLeft;

						objSheet.Cells[3 + j, 4] = dr["PostName"].ToString();
						objSheet.get_Range(objSheet.Cells[3 + j, 4], objSheet.Cells[3 + j, 4]).Merge(0);
						objSheet.get_Range(objSheet.Cells[3 + j, 4], objSheet.Cells[3 + j, 4]).HorizontalAlignment = XlHAlign.xlHAlignLeft;


						objSheet.Cells[3 + j, 5] = dr["OrgName"].ToString();
						objSheet.get_Range(objSheet.Cells[3 + j, 5], objSheet.Cells[3 + j, 5]).Merge(0);
						objSheet.get_Range(objSheet.Cells[3 + j, 5], objSheet.Cells[3 + j, 5]).HorizontalAlignment = XlHAlign.xlHAlignLeft;

                        objSheet.Cells[3 + j, 6] =  dr["ComputeRoom"].ToString();
                        objSheet.get_Range(objSheet.Cells[3 + j, 6], objSheet.Cells[3 + j, 6]).Merge(0);
                        objSheet.get_Range(objSheet.Cells[3 + j, 6], objSheet.Cells[3 + j, 6]).HorizontalAlignment =
                            XlHAlign.xlHAlignLeft;

                        j++;

						System.Threading.Thread.Sleep(10);
                        jsBlock = "<script>SetPorgressBar('导出考生信息','" + (((j + 1) * 100) / ((double)(ds.Tables[0].Rows.Count + 1))).ToString("0.00") + "'); </script>";
						Response.Write(jsBlock);
						Response.Flush();
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
            }

            Response.Write("<script>top.returnValue='true';window.close();</script>");
        }

        public static DataTable ConvertToDataTable(IList list)
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
    }
}
