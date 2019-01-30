using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Office.Interop.Owc11;
using mshtml;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;
using Range=Microsoft.Office.Interop.Owc11.Range;

namespace RailExamWebApp.RandomExam
{
    public partial class RandomExamResultList : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (PrjPub.IsWuhan())
                //{
                //    gradesGrid.Levels[0].Columns[3].HeadingText = "员工编码";
                //    lblWorkNo.Text = "上岗证号";
                //}
                //else
                //{
                //    gradesGrid.Levels[0].Columns[3].HeadingText = "工资编号";
                //    lblWorkNo.Text = "工资编号";
                //}

                hfRoleID.Value = PrjPub.CurrentLoginUser.RoleID.ToString();

                if (PrjPub.HasEditRight("成绩查询"))
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }

                if(!PrjPub.IsServerCenter)
                {
                    btnExam.Visible = false;
                }

                lblWorkNo.Text = "上岗证号";

                hfOrganizationId.Value = ConfigurationManager.AppSettings["StationID"].ToString();

                string qsExamId = Request.QueryString.Get("eid");
                int orgID = Convert.ToInt32(Request.QueryString.Get("OrgID"));

                string OrganizationName = "";
                string strExamineeName = "";
                decimal dScoreLower = 0;
                decimal dScoreUpper = 1000;

                IList<RandomExamResult> examResults = null;
                RandomExamResultBLL bllExamResult = new RandomExamResultBLL();
                string strSql = "";
                OracleAccess db = new OracleAccess();

                //examResults = bllExamResult.GetRandomExamResults(Convert.ToInt32(qsExamId), OrganizationName,string.Empty,
                //                                                 strExamineeName, string.Empty, dScoreLower,
                //                                                 dScoreUpper, Convert.ToInt32(orgID));


                RandomExamBLL objBll = new RandomExamBLL();
                RailExam.Model.RandomExam objExam = objBll.GetExam(Convert.ToInt32(qsExamId));

                RandomExamArrangeBLL objArrangebll = new RandomExamArrangeBLL();
                IList<RandomExamArrange> objArrangeList = objArrangebll.GetRandomExamArranges(Convert.ToInt32(qsExamId));
                string strChooseID = string.Empty;
                if (objArrangeList.Count > 0)
                {
                    if (PrjPub.IsServerCenter && orgID == objExam.OrgId)
                    {
                        strChooseID = objArrangeList[0].UserIds;
                        int count = 0;
                        EmployeeBLL objEmployeeBll = new EmployeeBLL();
                        string[] str = strChooseID.Split(',');
                        for (int i = 0; i < str.Length; i++)
                        {
                            if (str[i] == string.Empty)
                            {
                                continue;
                            }

                            Employee employee = objEmployeeBll.GetChooseEmployeeInfo(str[i]);
                            if (employee.EmployeeID == 0)
                            {
                                continue;
                            }

                            count++;
                        }
                        lblMaxCount.Text = count.ToString();
                    }
                    else if (PrjPub.IsServerCenter && orgID != objExam.OrgId)
                    {
                        string[] strEmployee = objArrangeList[0].UserIds.Split(',');

                        OrganizationBLL orgBll = new OrganizationBLL();
                        EmployeeBLL employeeBll = new EmployeeBLL();
                        int count = 0;
                        for (int i = 0; i < strEmployee.Length; i++)
                        {
                            Employee employee = employeeBll.GetChooseEmployeeInfo(strEmployee[i]);
                            int stationOrgID = orgBll.GetStationOrgID(employee.OrgID);

                            if (stationOrgID == orgID)
                            {
                                count++;
                            }
                        }
                        lblMaxCount.Text = count.ToString();
                    }
                    else
                    {
                        strSql = "select a.* from Random_Exam_Arrange_Detail a "
                                 + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID "
                                 + " inner join Computer_Server c on b.Computer_Server_ID=c.Computer_Server_ID "
                                 + " where c.Computer_Server_No='" + PrjPub.ServerNo + "'"
                                 + " and a.Random_Exam_ID=" + qsExamId;

                        DataSet dsArrange = db.RunSqlDataSet(strSql);
                        foreach (DataRow dr in dsArrange.Tables[0].Rows)
                        {
                            if (strChooseID == string.Empty)
                            {
                                strChooseID = dr["User_Ids"].ToString();
                            }
                            else
                            {
                                strChooseID += ","+dr["User_Ids"].ToString();
                            }
                        }

                        int count = 0;
                        EmployeeBLL objEmployeeBll = new EmployeeBLL();
                        string[] str = strChooseID.Split(',');
                        for (int i = 0; i < str.Length; i++ )
                        {
                            if(str[i]==string.Empty)
                            {
                                continue;
                            }

                            Employee employee = objEmployeeBll.GetChooseEmployeeInfo(str[i]);
                            if (employee.EmployeeID == 0)
                            {
                               continue;
                            }

                            count++;
                        }
                        lblMaxCount.Text = count.ToString();
                    }
                }


                DataSet ds = GetDataSet(true);
                if (ds != null && ds.Tables.Count == 2)
                {
					//foreach (DataRow dr in ds.Tables[0].Rows)
					//{
					//    if(dr["OrganizationId"].ToString() != hfOrganizationId.Value)
					//    {
					//        btnOutPutWord.Enabled = false;
					//        break;
					//    }
					//}

                    gradesGrid.DataSource = ds;
                    gradesGrid.DataBind();
                	BindGrid();
                }
                else
                {
                    SessionSet.PageMessage = "数据错误！";
                    return;
                }
            	hfIsServer.Value = PrjPub.IsServerCenter.ToString();

                //当是路局服务器时，屏蔽上传按钮
				if (PrjPub.IsServerCenter)
				{
					btnUpload.Visible = false;
				}
            }
            else
            {
                if (Request.Form.Get("OutPut") != null && Request.Form.Get("OutPut") != "")
                {
					OutputWord(Request.Form.Get("OutPut"));
                }
				if (Request.Form.Get("Refresh") != null && Request.Form.Get("Refresh") != "")
				{
					DownloadAll(Request.Form.Get("Refresh"));
				}

				if (Request.Form.Get("RefreshExcel") != null && Request.Form.Get("RefreshExcel") != "")
				{
					DownloadExcel();
				}

				if (Request.Form.Get("IsUpload") != null && Request.Form.Get("IsUpload") != "")
				{
					DataSet ds = GetDataSet(false);
					gradesGrid.DataSource = ds;
					gradesGrid.DataBind();
					BindGrid();
                    //string strId = Request.QueryString.Get("eid");
                    //RandomExamBLL objBll = new RandomExamBLL();
                    //RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(strId));
                    //SystemLogBLL objLogBll = new SystemLogBLL();
                    //objLogBll.WriteLog("“" + obj.ExamName + "”上传考试成绩和答卷");
					SessionSet.PageMessage = "上传成功！";
				}

                if (Request.Form.Get("RefreshList") != null && Request.Form.Get("RefreshList") != "")
                {
                    DataSet ds = GetDataSet(false);
                    gradesGrid.DataSource = ds;
                    gradesGrid.DataBind();
                    BindGrid();
                }
            }
            btnUpload.Visible = false;     // 包神这里用不著上传 2014-03-18
        }

		private void BindGrid()
		{
			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam objExam = objBll.GetExam(Convert.ToInt32(Request.QueryString.Get("eid")));
			if (!objExam.IsComputerExam)
			{
				gradesGrid.Levels[0].Columns[8].Visible = false;
				gradesGrid.Levels[0].Columns[9].Visible = false;
				gradesGrid.Levels[0].Columns[10].Visible = true;
				gradesGrid.Levels[0].Columns[12].Visible = false;
			}
			else
			{
				gradesGrid.Levels[0].Columns[8].Visible = true;
				gradesGrid.Levels[0].Columns[9].Visible = true;
				gradesGrid.Levels[0].Columns[10].Visible = false;
			}
		}

        protected DataSet GetDataSet(bool isFirst)
        {
            string qsExamId = Request.QueryString.Get("eid");
            int orgID = Convert.ToInt32(Request.QueryString.Get("OrgID"));
            DataSet ds = new DataSet();

            if (!string.IsNullOrEmpty(qsExamId))
            {
                string OrganizationName = txtOrganizationName.Text;
                string workShopName = txtWorkShop.Text;
                string strExamineeName = txtExamineeName.Text;
            	string strWorkNo = txtWorkNo.Text;
                decimal dScoreLower = string.IsNullOrEmpty(txtScoreLower.Text) ? -1000 : decimal.Parse(txtScoreLower.Text);
                decimal dScoreUpper = string.IsNullOrEmpty(txtScoreUpper.Text) ? 1000 : decimal.Parse(txtScoreUpper.Text);

                IList<RandomExamResult> examResults = null;
                RandomExamResultBLL bllExamResult = new RandomExamResultBLL();

                try
                {
					//RandomExamBLL objBll = new RandomExamBLL();
					//RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(qsExamId));

					//如果在站段是随到随考，需要综合路局和本地考试情况。
					//if(obj.StartMode == PrjPub.START_MODE_NO_CONTROL && !PrjPub.IsServerCenter)
					//{
					//    examResults = bllExamResult.GetRandomExamResultsFromServer(int.Parse(qsExamId), OrganizationName, strExamineeName, dScoreLower,
					//         dScoreUpper, orgID);
					//}
					//else
					//{
					//    examResults = bllExamResult.GetRandomExamResults(int.Parse(qsExamId), OrganizationName, strExamineeName, dScoreLower,
					//             dScoreUpper, orgID);
					//}

                    examResults = bllExamResult.GetRandomExamResults(int.Parse(qsExamId), OrganizationName, workShopName,strExamineeName, strWorkNo, dScoreLower,
							dScoreUpper, orgID);

                    if(isFirst)
                    {
                        lblNowCount.Text = examResults.Count.ToString();
                    }
                }
                catch
                {
                    Pub.ShowErrorPage("无法连接站段服务器，请检查站段服务器是否打开以及网络连接是否正常！");
                }

                ExamResultStatusBLL bllExamResultStatus = new ExamResultStatusBLL();
                IList<ExamResultStatus> examResultStatuses = bllExamResultStatus.GetExamResultStatuses();

                DataTable dtExamResults = ConvertToDataTable((IList)examResults);
                DataTable dtExamResultStatuses = ConvertToDataTable((IList)examResultStatuses);

                if (dtExamResults != null)
                {
                    ds.Tables.Add(dtExamResults);
                }
                else
                {
                    ds.Tables.Add(ConvertToDataTable(typeof(RandomExamResult)));
                }
                ds.Tables.Add(dtExamResultStatuses);
                ds.Relations.Add(ds.Tables["ExamResultStatus"].Columns["ExamResultStatusId"],
                    ds.Tables["RandomExamResult"].Columns["StatusId"]);
            }

            return ds;
        }

        protected static DataTable ConvertToDataTable(IList list)
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

        protected static DataTable ConvertToDataTable(Type type)
        {
            DataTable dt = new DataTable(type.Name);

            foreach (PropertyInfo pi in type.GetProperties())
            {
                dt.Columns.Add(pi.Name, pi.PropertyType);
            }
            dt.AcceptChanges();

            return dt;
        }

		private void DownloadExcel()
		{
			string path = Server.MapPath("/RailExamBao/Excel/ExamResult.xls");

			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(Request.QueryString.Get("eid")));

			if (File.Exists(path))
			{
				FileInfo file = new FileInfo(path);
				this.Response.Clear();
				this.Response.Buffer = true;
				this.Response.Charset = "utf-7";
				this.Response.ContentEncoding = Encoding.UTF7;
				// 添加头信息，为"文件下载/另存为"对话框指定默认文件名
				this.Response.AddHeader("Content-Disposition",
				                        "attachment; filename=" + HttpUtility.UrlEncode(obj.ExamName + "成绩登记表") + ".xls");
				// 添加头信息，指定文件大小，让浏览器能够显示下载进度
				this.Response.AddHeader("Content-Length", file.Length.ToString());

				// 指定返回的是一个不能被客户端读取的流，必须被下载
				this.Response.ContentType = "application/ms-excel";

				// 把文件流发送到客户端
				this.Response.WriteFile(file.FullName);
			}
		}

		private void SetHeaderText(Range rang, string headertxt)
        {
            rang.set_MergeCells(true);
            rang.Value2 = headertxt;
            rang.set_HorizontalAlignment(XlHAlign.xlHAlignCenter);
            rang.set_VerticalAlignment(XlVAlign.xlVAlignCenter);
            rang.BorderAround(1, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, Missing.Value);
        }

		private void OutputWord(string strName)
		{
            DataSet ds = GetDataSet(false);
			if (ds != null && ds.Tables.Count == 2)
			{
				gradesGrid.DataSource = ds;
				gradesGrid.DataBind();
				BindGrid();
			}

			string filename = Server.MapPath("/RailExamBao/Excel/"+ strName +".doc");
			if (File.Exists(filename))
			{
				FileInfo file = new FileInfo(filename.ToString());
				this.Response.Clear();
				this.Response.Buffer = true;
				this.Response.Charset = "utf-7";
				this.Response.ContentEncoding = Encoding.UTF7;
				// 添加头信息，为"文件下载/另存为"对话框指定默认文件名
				this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(strName) + ".doc");
				// 添加头信息，指定文件大小，让浏览器能够显示下载进度
				this.Response.AddHeader("Content-Length", file.Length.ToString());
				// 指定返回的是一个不能被客户端读取的流，必须被下载
				this.Response.ContentType = "application/ms-word";
				// 把文件流发送到客户端
				this.Response.WriteFile(file.FullName);
			}
		}

		private string GetNo(int i)
        {
            string strReturn = string.Empty;

            switch (i.ToString())
            {
                case "0": strReturn = "一";
                    break;
                case "1": strReturn = "二";
                    break;
                case "2": strReturn = "三";
                    break;
                case "3": strReturn = "四";
                    break;
                case "4": strReturn = "五";
                    break;
                case "5": strReturn = "六";
                    break;
                case "6": strReturn = "七";
                    break;
                case "7": strReturn = "八";
                    break;
                case "8": strReturn = "九";
                    break;
                case "9": strReturn = "十";
                    break;
                case "10": strReturn = "十一";
                    break;
                case "11": strReturn = "十二";
                    break;
                case "12": strReturn = "十三";
                    break;
                case "13": strReturn = "十四";
                    break;
                case "14": strReturn = "十五";
                    break;
                case "15": strReturn = "十六";
                    break;
                case "16": strReturn = "十七";
                    break;
                case "17": strReturn = "十八";
                    break;
                case "18": strReturn = "十九";
                    break;
                case "19": strReturn = "二十";
                    break;
            }
            return strReturn;
        }

        private char intToChar(int intCol)
        {
			return Convert.ToChar('A' + intCol);
        }

        protected void btnOutPutWord_Click(object sender, ImageClickEventArgs e)
		{
		}

		private void DownloadAll(string strType)
		{
				RandomExamBLL objBll = new RandomExamBLL();
				string strId = Request.QueryString.Get("eid");
				RailExam.Model.RandomExam obj= objBll.GetExam(int.Parse(strId));
				string filename = "";

				filename = Server.MapPath("/RailExamBao/Excel/" + strType+ "/");

				string ZipName = Server.MapPath("/RailExamBao/Excel/Word.zip");

				GzipCompress(filename,ZipName);

				FileInfo file = new FileInfo(ZipName.ToString());
				this.Response.Clear();
				this.Response.Buffer = true;
				this.Response.Charset = "utf-7";
				this.Response.ContentEncoding = Encoding.UTF7;
				// 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                if (strType.IndexOf("合格")>=0)
				{
					this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(obj.ExamName+"合格试卷") + ".zip");
				}
				else
				{
					this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(obj.ExamName) + ".zip");
				}
				// 添加头信息，指定文件大小，让浏览器能够显示下载进度
				this.Response.AddHeader("Content-Length", file.Length.ToString());
				// 指定返回的是一个不能被客户端读取的流，必须被下载
				this.Response.ContentType = "application/ms-word";
				// 把文件流发送到客户端
				this.Response.WriteFile(file.FullName);
		}

		public void GzipCompress(string sourceFile, string disPath)
		{
            Crc32 crc = new Crc32();
			ZipOutputStream s = new ZipOutputStream(File.Create(disPath)); // 指定zip文件的绝对路径，包括文件名            
            s.SetLevel(6); // 0 - store only to 9 - means best compression 

			#region 压缩一个文件
			//FileStream fs = File.OpenRead(sourceFile); // 文件的绝对路径，包括文件名
			//byte[] buffer = new byte[fs.Length];
			//fs.Read(buffer, 0, buffer.Length);
			//ZipEntry entry = new ZipEntry(extractFileName(sourceFile)); //这里ZipEntry的参数必须是相对路径名，表示文件在zip文档里的相对路径
			//entry.DateTime = DateTime.Now;
			//entry.Size = fs.Length;
			//fs.Close();
			//crc.Reset();
			//crc.Update(buffer); 
			//entry.Crc = crc.Value; 
			//s.PutNextEntry(entry);
			//s.Write(buffer, 0, buffer.Length);
			#endregion

			//压缩多个文件
			DirectoryInfo di = new DirectoryInfo(sourceFile);
			#region 递归
			//foreach (DirectoryInfo item in di.GetDirectories())
			//{
			//    addZipEntry(item.FullName);
			//}
			#endregion
			foreach (FileInfo item in di.GetFiles())
			{
				FileStream fs = File.OpenRead(item.FullName);
				byte[] buffer = new byte[fs.Length];
				fs.Read(buffer, 0, buffer.Length);
				string strEntryName = item.FullName.Replace(sourceFile, "");
				ZipEntry entry = new ZipEntry(strEntryName);
				s.PutNextEntry(entry);
				s.Write(buffer, 0, buffer.Length);
				fs.Close();
			}

            s.Finish();
            s.Close();
		}

		private string extractFileName(string filePath)
		{
			int index1 = filePath.LastIndexOf("\\");
			string fileName = filePath.Substring(index1 + 1);
			return fileName;
		} 

        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            DataSet ds = GetDataSet(false);
            if (ds != null && ds.Tables.Count == 2)
            {
                gradesGrid.DataSource = ds;
                gradesGrid.DataBind();
				BindGrid();
            }
        }

		protected void btnUpload_Click(object sender, EventArgs e)
		{
			if (PrjPub.IsServerCenter)
			{
				SessionSet.PageMessage = "请确认当前数据库连接为站段数据库！";
				return;
			}

            string strSql = "select GetOrgName(org_ID) from SYNCHRONIZE_LOG@link_sf where SYNCHRONIZE_STATUS_ID=1 and SYNCHRONIZE_TYPE_ID=6";
            OracleAccess db = new OracleAccess();
            DataSet ds = db.RunSqlDataSet(strSql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                SessionSet.PageMessage = "站段【" + ds.Tables[0].Rows[0][0] + "】正在上传考试成绩或答卷，请稍后再上传考试答卷！";
                return;
            }

			string strId = Request.QueryString.Get("eid");
			if (string.IsNullOrEmpty(strId))
			{
				SessionSet.PageMessage = "缺少参数！";
				return;
			}

			ClientScript.RegisterStartupScript(GetType(),
						"jsSelectFirstNode",
						@"UploadExam();",
						true);
		}

        protected  void btnExam_Click(object sender, EventArgs e)
        {
            string strId = Request.QueryString.Get("eid");
            RandomExamBLL objBll = new RandomExamBLL();
            RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(strId));

            OracleAccess db = new OracleAccess();

            IList<RandomExamResult> examResults = null;
            RandomExamResultBLL bllExamResult = new RandomExamResultBLL();
            examResults = bllExamResult.GetRandomExamResults(int.Parse(strId), "", "", "", "", 0, 1000, Convert.ToInt32(Request.QueryString.Get("OrgID")));
            string strChooseID = string.Empty;
            foreach (RandomExamResult randomExamResult in examResults)
            {
                if (string.IsNullOrEmpty(strChooseID))
                {
                    strChooseID += randomExamResult.ExamineeId;
                }
                else
                {
                    strChooseID += "," + randomExamResult.ExamineeId;
                }
            }

            int beginYear = obj.BeginTime.Year;
            string strSql =
            @"select a.*,b.Employee_Name,b.Employee_ID from Random_Exam_Result a
                inner join Employee b on a.Examinee_ID=b.Employee_ID
                where Random_Exam_Result_ID not in (
                select distinct  a.Random_Exam_Result_ID from Random_Exam_Result_Answer a
                        inner join  Random_Exam_Result b on a.Random_Exam_Result_ID=b.Random_Exam_Result_ID
                        where Random_Exam_ID=" + strId + @"
                union all 
                select distinct  a.Random_Exam_Result_ID from Random_Exam_Result_Answer_" + beginYear + @" a
                         inner join  Random_Exam_Result b on a.Random_Exam_Result_ID=b.Random_Exam_Result_ID
                        where Random_Exam_ID=" + strId + @")
                and Random_Exam_ID=" + strId;
            DataSet dsAnswer = db.RunSqlDataSet(strSql);

            if (dsAnswer.Tables[0].Rows.Count >0)
            {
                string strIDs = string.Empty;
                foreach (DataRow dr in dsAnswer.Tables[0].Rows)
                {
                    if (("," + strChooseID + ",").IndexOf("," + dr["Employee_ID"] + ",") >= 0)
                    {
                        if (string.IsNullOrEmpty(strIDs))
                        {
                            strIDs += dr["Employee_Name"].ToString();
                        }
                        else
                        {
                            strIDs += "," + dr["Employee_Name"];
                        }
                    }
                }

                if(string.IsNullOrEmpty(strIDs))
                {
                    SessionSet.PageMessage = "所有考生试卷完整！";
                }
                else
                {
                    SessionSet.PageMessage = "下列考生在路局服务器无考试试卷：" + strIDs;
                }
            }
            else
            {
                SessionSet.PageMessage = "所有考生试卷完整！";
            }
        }

    }
}
