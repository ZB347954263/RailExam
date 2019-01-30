using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using Microsoft.Office.Interop.Owc11;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
	public partial class RandomExamNoExam : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
                //if (PrjPub.IsWuhan())
                //{
                //    gradesGrid.Levels[0].Columns[2].HeadingText = "员工编码";
                //}
                //else
                //{
                //    gradesGrid.Levels[0].Columns[2].HeadingText = "工资编号";
                //}

				gradesGrid.DataSource = BindGrid(); ;
				gradesGrid.DataBind();
			}

            if (Request.Form.Get("StudentInfo") != null && Request.Form.Get("StudentInfo") != "" )
            {
                DownloadStudentInfoExcel(Request.Form.Get("StudentInfo"));
            }
		}

		private IList<Employee>  BindGrid()
		{
			string RandomExamID = Request.QueryString.Get("eid");
			string OrgID = Request.QueryString.Get("OrgID");

			string OrganizationName = "";
			string strExamineeName = "";
			decimal dScoreLower = -1000;
			decimal dScoreUpper = 1000;

			IList<RandomExamResult> examResults = null;
			RandomExamResultBLL bllExamResult = new RandomExamResultBLL();

			examResults = bllExamResult.GetRandomExamResults(Convert.ToInt32(RandomExamID), OrganizationName, string.Empty,strExamineeName, string.Empty, dScoreLower,
						dScoreUpper, Convert.ToInt32(OrgID));

			string strID = string.Empty;
			foreach (RandomExamResult result in examResults)
			{
				if (strID == string.Empty)
				{
					strID = result.ExamineeId.ToString();
				}
				else
				{
					strID = strID + "," + result.ExamineeId;
				}
			}

            RandomExamBLL objBll = new RandomExamBLL();
            RailExam.Model.RandomExam objExam = objBll.GetExam(Convert.ToInt32(RandomExamID));

			RandomExamArrangeBLL objArrangebll = new RandomExamArrangeBLL();
			IList<RandomExamArrange> objArrangeList = objArrangebll.GetRandomExamArranges(Convert.ToInt32(RandomExamID));
			string strChooseID = string.Empty;
			if (objArrangeList.Count > 0)
			{
                if (PrjPub.IsServerCenter && Convert.ToInt32(OrgID) == objExam.OrgId)
                    {
                        strChooseID = objArrangeList[0].UserIds;
                    }
                    else if (PrjPub.IsServerCenter && Convert.ToInt32(OrgID) != objExam.OrgId)
                    {
                        string[] strEmployee = objArrangeList[0].UserIds.Split(',');

                        OrganizationBLL orgBll = new OrganizationBLL();
                        EmployeeBLL employeeBll = new EmployeeBLL();
                        int count = 0;
                        for (int i = 0; i < strEmployee.Length; i++)
                        {
                            Employee employee = employeeBll.GetChooseEmployeeInfo(strEmployee[i]);
                            int stationOrgID = orgBll.GetStationOrgID(employee.OrgID);

                            if (stationOrgID == Convert.ToInt32(OrgID))
                            {
                                if (strChooseID == string.Empty)
                                {
                                    strChooseID = strEmployee[i];
                                }
                                else
                                {
                                    strChooseID += "," + strEmployee[i];
                                }
                            }
                        }
                    }
                    else
                    {
                        string strSql = "select a.* from Random_Exam_Arrange_Detail a "
                                 + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID "
                                 + " inner join Computer_Server c on b.Computer_Server_ID=c.Computer_Server_ID "
                                 + " where c.Computer_Server_No='" + PrjPub.ServerNo + "'"
                                 + " and a.Random_Exam_ID=" + RandomExamID;
                        OracleAccess db =new OracleAccess();
                        DataSet dsArrange = db.RunSqlDataSet(strSql);

                        foreach (DataRow dr in dsArrange.Tables[0].Rows)
                        {
                            if (strChooseID == string.Empty)
                            {
                                strChooseID = dr["User_Ids"].ToString();
                            }
                            else
                            {
                                strChooseID += "," + dr["User_Ids"].ToString();
                            }
                        }
                    }
                }
			
			string[] str = strChooseID.Split(',');
			string strNoResult = "";
			for (int i = 0; i < str.Length; i++)
			{
                if(str[i] == string.Empty)
                {
                    continue;
                }

				if (("," + strID + ",").IndexOf(("," + str[i] + ",")) < 0)
				{
					if (strNoResult == string.Empty)
					{
						strNoResult = str[i];
					}
					else
					{
						strNoResult = strNoResult + "," + str[i];
					}
				}
			}

			EmployeeBLL objbll = new EmployeeBLL();
		    IList<Employee> objList = new List<Employee>();
            if (strNoResult != string.Empty)
            {
                string[] strNo = strNoResult.Split(',');
                for (int i = 0; i < strNo.Length; i++)
                {
                    Employee employee = objbll.GetChooseEmployeeInfo(strNo[i]);
                    if(employee.EmployeeID != 0)
                    {
                        objList.Add(employee);
                    }
                }
            }

		    Session["NoExamInfo"] = objList;

           return objList;
		}

		protected void btnOutPut_Click(object sender, EventArgs e)
		{
			try
			{
                RandomExamBLL objBll = new RandomExamBLL();
                RailExam.Model.RandomExam objRandomExam = objBll.GetExam(Convert.ToInt32(Request.QueryString.Get("eid")));

                IList<Employee> objList = BindGrid();

                #region 原导出Excel方法
                //SpreadsheetClass xlsheet = new SpreadsheetClass();
                //Worksheet ws = (Worksheet)xlsheet.Worksheets[1];
                //ws.Cells.Font.set_Size(10);
                //ws.Cells.Font.set_Name("宋体");

                //ws.Cells[1, 1] = objRandomExam.ExamName + " 未参加考试学员名单";
                //Range rang1 = ws.get_Range(ws.Cells[1, 1], ws.Cells[1, 7]);
                //rang1.set_MergeCells(true);
                //rang1.set_HorizontalAlignment(XlHAlign.xlHAlignCenter);
                //rang1.Font.set_Name("宋体");

                ////write headertext
                //ws.Cells[2, 1] = "序号";
                //((Range)ws.Cells[2, 1]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

                //ws.Cells[2, 2] = "姓名";
                //ws.get_Range(ws.Cells[2, 2], ws.Cells[2, 2]).set_MergeCells(true);
                //ws.get_Range(ws.Cells[2, 2], ws.Cells[2, 2]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

                //if (PrjPub.IsWuhan())
                //{
                //    ws.Cells[2, 3] = "员工编码(身份证号码)";
                //}
                //else
                //{
                //    ws.Cells[2, 3] = "工资编号";
                //}

                //ws.get_Range(ws.Cells[2, 3], ws.Cells[2, 3]).set_MergeCells(true);
                //ws.get_Range(ws.Cells[2, 3], ws.Cells[2, 3]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

                //ws.Cells[2, 4] = "职名";
                //ws.get_Range(ws.Cells[2, 4], ws.Cells[2, 4]).set_MergeCells(true);
                //ws.get_Range(ws.Cells[2, 4], ws.Cells[2, 4]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

                //ws.Cells[2, 5] = "组织机构";
                //ws.get_Range(ws.Cells[2, 5], ws.Cells[2, 7]).set_MergeCells(true);
                //ws.get_Range(ws.Cells[2, 5], ws.Cells[2, 7]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

                //for (int j = 0; j < objList.Count; j++)
                //{
                //    ws.Cells[3 + j, 1] = j + 1;

                //    ws.Cells[3 + j, 2] = objList[j].EmployeeName;
                //    ws.get_Range(ws.Cells[3 + j, 2], ws.Cells[3 + j, 2]).set_MergeCells(true);
                //    ws.get_Range(ws.Cells[3 + j, 2], ws.Cells[3 + j, 2]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);

                //    ws.Cells[3 + j, 3] = "'" + objList[j].StrWorkNo;
                //    ws.get_Range(ws.Cells[3 + j, 3], ws.Cells[3 + j, 3]).set_MergeCells(true);
                //    ws.get_Range(ws.Cells[3 + j, 3], ws.Cells[3 + j, 3]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);


                //    ws.Cells[3 + j, 4] = objList[j].PostName;
                //    ws.get_Range(ws.Cells[3 + j, 4], ws.Cells[3 + j, 4]).set_MergeCells(true);
                //    ws.get_Range(ws.Cells[3 + j, 4], ws.Cells[3 + j, 4]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);

                //    ws.Cells[3 + j, 5] = objList[j].OrgName;
                //    ws.get_Range(ws.Cells[3 + j, 5], ws.Cells[3 + j, 7]).set_MergeCells(true);
                //    ws.get_Range(ws.Cells[3 + j, 5], ws.Cells[3 + j, 7]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);
                //}

                //ws.Name = "1-1";
                //ws.Cells.Columns.AutoFit();

                //((Worksheet)xlsheet.Worksheets[1]).Activate();

                //string path = Server.MapPath("../Excel/Excel.xls");
                //if (File.Exists(path))
                //    File.Delete(path);
                //xlsheet.Export(path, SheetExportActionEnum.ssExportActionNone, SheetExportFormat.ssExportAsAppropriate);
                #endregion

                Excel.Application objApp = new Excel.ApplicationClass();
			    Excel.Workbooks objbooks = objApp.Workbooks;
			    Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
			    Excel.Worksheet objSheet = (Excel.Worksheet)objbook.Worksheets[1];//取得sheet1 
			    Excel.Range rang1;
			    string filename = "";

				//生成.xls文件完整路径名 
				filename = Server.MapPath("/RailExamBao/Excel/Excel"+DateTime.Now.ToString("yyyyMMddHHmmss")+".xls");

				if (File.Exists(filename.ToString()))
				{
					File.Delete(filename.ToString());
				}
				objSheet.Cells.Font.Size = 10;
				objSheet.Cells.Font.Name = "宋体";

                objSheet.Cells[1, 1] = objRandomExam.ExamName + " 未参加考试学员名单";
				rang1 = objSheet.get_Range(objSheet.Cells[1, 1], objSheet.Cells[1,7]);
				rang1.Merge(0);
				rang1.HorizontalAlignment = XlHAlign.xlHAlignCenter;
				rang1.Font.Bold = true;
				objSheet.Cells.Font.Size = 17;
				objSheet.Cells.Font.Name = "宋体";

				//write headertext
				objSheet.Cells[2, 1] = "序号";
				((Excel.Range)objSheet.Cells[2, 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

				objSheet.Cells[2, 2] = "姓名";
				objSheet.get_Range(objSheet.Cells[2, 2], objSheet.Cells[2, 2]).Merge(0);
				objSheet.get_Range(objSheet.Cells[2, 2], objSheet.Cells[2, 2]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                objSheet.Cells[2, 3] = "员工编码(身份证号码)";
                objSheet.get_Range(objSheet.Cells[2, 3], objSheet.Cells[2, 3]).Merge(0);
                objSheet.get_Range(objSheet.Cells[2, 3], objSheet.Cells[2, 3]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                objSheet.Cells[2, 4] = "职名";
                objSheet.get_Range(objSheet.Cells[2, 4], objSheet.Cells[2, 4]).Merge(0);
                objSheet.get_Range(objSheet.Cells[2, 4], objSheet.Cells[2, 4]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                objSheet.Cells[2, 5] = "组织机构";
                objSheet.get_Range(objSheet.Cells[2, 5], objSheet.Cells[2, 5]).Merge(0);
                objSheet.get_Range(objSheet.Cells[2, 5], objSheet.Cells[2, 5]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                for (int j = 0; j < objList.Count; j++)
                {
                    objSheet.Cells[3 + j, 1] = j + 1;

                    objSheet.Cells[3 + j, 2] = objList[j].EmployeeName;
                    objSheet.get_Range(objSheet.Cells[3 + j, 2], objSheet.Cells[3 + j, 2]).Merge(0);
                    objSheet.get_Range(objSheet.Cells[3 + j, 2], objSheet.Cells[3 + j, 2]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    objSheet.Cells[3 + j, 3] = "'" + objList[j].StrWorkNo;
                    objSheet.get_Range(objSheet.Cells[3 + j, 3], objSheet.Cells[3 + j, 3]).Merge(0);
                    objSheet.get_Range(objSheet.Cells[3 + j, 3], objSheet.Cells[3 + j, 3]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    objSheet.Cells[3 + j, 4] = objList[j].PostName;
                    objSheet.get_Range(objSheet.Cells[3 + j, 4], objSheet.Cells[3 + j, 4]).Merge(0);
                    objSheet.get_Range(objSheet.Cells[3 + j, 4], objSheet.Cells[3 + j, 4]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                    objSheet.Cells[3 + j, 5] = objList[j].OrgName;
                    objSheet.get_Range(objSheet.Cells[3 + j, 5], objSheet.Cells[3 + j, 5]).Merge(0);
                    objSheet.get_Range(objSheet.Cells[3 + j, 5], objSheet.Cells[3 + j, 5]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                }

                objSheet.Columns.AutoFit();

                objApp.Visible = false;

                objbook.Saved = true;
                objbook.SaveCopyAs(filename);

                FileInfo file = new FileInfo(filename);
                this.Response.Clear();
                this.Response.Buffer = true;
                this.Response.Charset = "utf-7";
                this.Response.ContentEncoding = Encoding.UTF7;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(objRandomExam.ExamName + "未参加考试学员名单") + ".xls");
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度
                this.Response.AddHeader("Content-Length", file.Length.ToString());
                // 指定返回的是一个不能被客户端读取的流，必须被下载
                this.Response.ContentType = "application/ms-excel";
                // 把文件流发送到客户端
                this.Response.WriteFile(file.FullName);
			}
			catch
			{
				SessionSet.PageMessage = "系统错误，导出Excel文件失败！";
			}
		}

        private void DownloadStudentInfoExcel(string strName)
        {
            string path = Server.MapPath("/RailExamBao/Excel/"+strName+".xls");

            RandomExamBLL objBll = new RandomExamBLL();
            RailExam.Model.RandomExam objRandomExam = objBll.GetExam(Convert.ToInt32(Request.QueryString.Get("eid")));

            if (File.Exists(path))
            {
                FileInfo file = new FileInfo(path);
                this.Response.Clear();
                this.Response.Buffer = true;
                this.Response.Charset = "utf-7";
                this.Response.ContentEncoding = Encoding.UTF7;
                // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                this.Response.AddHeader("Content-Disposition",
                                        "attachment; filename=" + HttpUtility.UrlEncode(objRandomExam.ExamName + "未参加考试学员名单") +
                                        ".xls");
                // 添加头信息，指定文件大小，让浏览器能够显示下载进度
                this.Response.AddHeader("Content-Length", file.Length.ToString());

                // 指定返回的是一个不能被客户端读取的流，必须被下载
                this.Response.ContentType = "application/ms-excel";

                // 把文件流发送到客户端
                this.Response.WriteFile(file.FullName);
            }
        }
	}
}
