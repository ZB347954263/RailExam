using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using mshtml;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;


namespace RailExamWebApp.RandomExam
{
    public partial class RandomExamControlDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			if(!IsPostBack && !searchExamCallBack.IsCallback)
			{
				//如果是系统管理员访问站段服务器则没有权限监控站段考试
				if (!PrjPub.IsServerCenter && PrjPub.CurrentLoginUser.IsAdmin && PrjPub.CurrentLoginUser.RoleID == 1)
				{
					btnGetPaper.Enabled = false;
					btnDelPaper.Enabled = false;
					btnStart.Enabled = false;
					btnStop.Enabled = false;
					btnEnd.Enabled = false;
					btnUpload.Enabled = false;
				    btnUploadScore.Enabled = false;
					HfUpdateRight.Value = "False";
				}

                //if (PrjPub.IsWuhan())
                //{
                //    examsGrid.Levels[0].Columns[3].HeadingText = "员工编码";
                //}
                //else
                //{
                //    examsGrid.Levels[0].Columns[3].HeadingText = "工资编号";
                //}

				if(PrjPub.IsServerCenter)
				{
					btnUpload.Enabled = false;
				    btnUploadScore.Enabled = false;
				}

				//当前站段有两台服务器并且当前服务器为次服务器时屏蔽“删除所有考生试卷”和“上传考试成绩”按钮
				//if(!PrjPub.IsServerCenter && PrjPub.HasTwoServer() && !PrjPub.IsMainServer())
				//{
				//    btnGetPaper.Visible = false;
				//    btnDelPaper.Visible = false;
				//    btnUpload.Visible = false;
				//}

				hfOrgID.Value = ConfigurationManager.AppSettings["StationID"].ToString();

				string strId = Request.QueryString.Get("RandomExamID");

				RandomExamArrangeBLL objArrangeBll = new RandomExamArrangeBLL();
				IList<RandomExamArrange> objArrangeList = objArrangeBll.GetRandomExamArranges(Convert.ToInt32(strId));
				string[] str = {"0"};
				hfNowCount.Value = "0";
				if(objArrangeList.Count == 1)
				{
                    string strSql = "select * from Random_Exam_Arrange_Detail a "
                                + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
                                + " inner join Computer_Server c on b.Computer_Server_ID=c.Computer_Server_ID"
                                + " where b.Org_ID='" + ConfigurationManager.AppSettings["StationID"] + "'"
                                + " and c.Computer_Server_No='" + PrjPub.ServerNo + "'"
                                +" and Random_Exam_ID=" + strId;
                    OracleAccess db = new OracleAccess();
                    DataSet ds = db.RunSqlDataSet(strSql);

				    string strUserId = "";
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (string.IsNullOrEmpty(strUserId))
                        {
                            strUserId += dr["User_Ids"].ToString();
                        }
                        else
                        {
                            strUserId += "," + dr["User_Ids"];
                        }
                    }

                    strUserId = ("," + strUserId + ",").Replace(",0,", ",");
					str = ((strUserId.TrimStart(',')).TrimEnd(',')).Split(',');

					ExamBLL objexambll = new ExamBLL();
					IList<RailExam.Model.Exam> objExamList=objexambll.GetExamsInfoByOrgID(null,-1,Convert.ToDateTime("0001-01-01"), Convert.ToDateTime("0001-01-01"),
					                               Convert.ToInt32(hfOrgID.Value), PrjPub.IsServerCenter.ToString());
					foreach (RailExam.Model.Exam objExam in objExamList)
					{
						if(objExam.ExamId == Convert.ToInt32(strId))
						{
							hfNowCount.Value = (str.Length - objExam.ExamineeCount).ToString();
							break;
						}
					}
				}

				//获取当前考试的生成试卷的状态和次数
				RandomExamBLL objBll = new RandomExamBLL();
				RailExam.Model.RandomExam objRandomExam = objBll.GetExam(Convert.ToInt32(strId));

                RandomExamComputerServerBLL serverBll = new RandomExamComputerServerBLL();
                RandomExamComputerServer server = serverBll.GetRandomExamComputerServer(objRandomExam.RandomExamId,
                                                                                        PrjPub.ServerNo);

                if (server.HasPaper && objRandomExam.StartMode == 2 && server.IsStart != 0)
				{
					lblTitle.Text = "考试验证码：";
                    lblCode.Text = server.RandomExamCode;
					btnApply.Visible = true;
				}

                if (server.HasPaper && server.IsStart != 2)// && !objRandomExam.HasTrainClass
				{
					btnAddEmployee.Visible = true;
				}


                if(PrjPub.CurrentLoginUser.EmployeeID != 0)
                {
                    examsGrid.Levels[0].Columns[12].Visible = false;
                }
			}

            if (!searchExamCallBack.IsCallback)
            {
                hfSql.Value = GetSql();
                examsGrid.DataBind();
            }

            if (Request.Form.Get("OutPutRandom") != null && Request.Form.Get("OutPutRandom") != "")
            {
				OutputWord(Request.Form.Get("OutPutRandom"));
            }

            if (Request.Form.Get("StopExam") != null && Request.Form.Get("StopExam") != "")
            {
                StopExam(Request.Form.Get("StopExam"));
            }

            if (Request.Form.Get("DeleteExam") != null && Request.Form.Get("DeleteExam") != "")
            {
                DeleteExam(Request.Form.Get("DeleteExam"));
            }

            if (Request.Form.Get("ClearExam") != null && Request.Form.Get("ClearExam") != "")
            {
                ClearExam(Request.Form.Get("ClearExam"));
            }

            if (Request.Form.Get("ReplyExam") != null && Request.Form.Get("ReplyExam") != "")
            {
                ReplyExam(Request.Form.Get("ReplyExam"));
            }

			if (Request.Form.Get("IsGet") != null && Request.Form.Get("IsGet") != "")
			{
                hfSql.Value = GetSql();
				examsGrid.DataBind();
				string strId = Request.QueryString.Get("RandomExamID");
				RandomExamBLL objBll = new RandomExamBLL();
				RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(strId));
                //if(!obj.HasTrainClass)
                //{
					btnAddEmployee.Visible = true;
                //}
				SessionSet.PageMessage = "生成成功！";
			}

			if (Request.Form.Get("IsEnd") != null && Request.Form.Get("IsEnd") != "")
			{
                hfSql.Value = GetSql();
				examsGrid.DataBind();
				SessionSet.PageMessage = "结束考试成功！";
			}

			if(Request.Form.Get("IsUpload") != null && Request.Form.Get("IsUpload")!="")
			{
                hfSql.Value = GetSql();
				examsGrid.DataBind();
				string strId = Request.QueryString.Get("RandomExamID");
				RandomExamBLL objBll = new RandomExamBLL();
				RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(strId));
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("“" + obj.ExamName + "”上传考试成绩和答卷");
				SessionSet.PageMessage = "上传成功！";
			}

			if (Request.Form.Get("IsStart") != null && Request.Form.Get("IsStart") != "")
			{
                hfSql.Value = GetSql();
				examsGrid.DataBind();
				SessionSet.PageMessage = "开始考试成功！";
				string strId = Request.QueryString.Get("RandomExamID");
				//获取当前考试的生成试卷的状态和次数
                RandomExamBLL objBll = new RandomExamBLL();
                RailExam.Model.RandomExam objExam = objBll.GetExam(Convert.ToInt32(strId));

                RandomExamComputerServerBLL serverBll = new RandomExamComputerServerBLL();
                RandomExamComputerServer server = serverBll.GetRandomExamComputerServer(objExam.RandomExamId,
                                                                                        PrjPub.ServerNo);


                if (server.HasPaper && objExam.StartMode == 2)
				{
                    lblCode.Text = "考试验证码：" + server.RandomExamCode;
					btnApply.Visible = true;
				}
				ClientScript.RegisterStartupScript(GetType(),
				"jsSelectFirstNode",
				@"ApplyExam();",
				true);
			}

			if (Request.Form.Get("StudentInfo") != null && Request.Form.Get("StudentInfo") != "" && !searchExamCallBack.IsCallback)
			{
				DownloadStudentInfoExcel();
			}

            refreshGridCallback.RefreshInterval = Convert.ToInt32(ConfigurationManager.AppSettings["RefreshInterval"]);

            btnUpload.Visible = btnUploadScore.Visible = false; // 包神这里用不著上传 2014-03-18
        }

		private void OutputWord(string strName)
		{
            hfSql.Value = GetSql();
			examsGrid.DataBind();

            string filename = Server.MapPath("/RailExamBao/Excel/" + strName + ".doc");
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

        protected void searchExamCallBack_Callback(object sender, CallBackEventArgs e)
        {
            string sql = "";
            string name = Server.UrlDecode(e.Parameters[0].Trim());
            if (!string.IsNullOrEmpty(name))
            {
                sql += " and e.Employee_Name like '%" + name + "%'";
            }

            string workNo = Server.UrlDecode(e.Parameters[1].Trim());
            if (!string.IsNullOrEmpty(workNo))
            {
                sql += " and e.Work_No like '%" + workNo + "%'";
            }

            string cardNo = Server.UrlDecode(e.Parameters[2].Trim());
            if (!string.IsNullOrEmpty(cardNo))
            {
                sql += " and e.Identity_CardNo like '%" + cardNo + "%'";
            }
            hfSql.Value = sql;
            hfSql.RenderControl(e.Output);
            examsGrid.DataBind();
            examsGrid.RenderControl(e.Output);
        }

        protected void refreshGridCallback_Callback(object sender, CallBackEventArgs e)
        {
        }

        protected void btnGetPaper_Click(object sender, EventArgs e)
        {
			string strId = Request.QueryString.Get("RandomExamID");
			if (string.IsNullOrEmpty(strId))
			{
				SessionSet.PageMessage = "缺少参数！";
				return;
			}

			//获取当前考试的生成试卷的状态和次数
			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam objExam = objBll.GetExam(Convert.ToInt32(strId));

			if(!PrjPub.IsServerCenter)
			{
				int nowVersion = objBll.GetExamServer(Convert.ToInt32(strId)).Version;
                if (nowVersion != objExam.Version)
				{
					SessionSet.PageMessage = "当前考试版本与路局不匹配，请重新同步基础数据！";
					return;
				}
			}

            if (objExam.EndTime < DateTime.Today)
			{
				SessionSet.PageMessage = "当前考试已过期，不能生成考试试卷！";
				return;
			}

            RandomExamComputerServerBLL serverBll = new RandomExamComputerServerBLL();
            RandomExamComputerServer server = serverBll.GetRandomExamComputerServer(objExam.RandomExamId,
                                                                                    PrjPub.ServerNo);
            if (server.HasPaper)
			{
				SessionSet.PageMessage = "当前考试已生成试卷！";
				return;
			}

			//获取当前考试的考生信息
			RandomExamArrangeBLL eaBll = new RandomExamArrangeBLL();
			IList<RailExam.Model.RandomExamArrange> ExamArranges = eaBll.GetRandomExamArranges(int.Parse(strId));
			string strChooseID = "";
			if (ExamArranges.Count > 0)
			{
				strChooseID = ExamArranges[0].UserIds;
			}
			else
			{
				strChooseID = "";
			}
			string[] str = strChooseID.Split(',');
			if (str[0] == "")
			{
				SessionSet.PageMessage = "请为该考试添加考生！";
				return;
			}

			ClientScript.RegisterStartupScript(GetType(),
						"jsSelectFirstNode",
						@"GetPaper();",
						true);
        }

		protected void btnDelPaper_Click(object sender, EventArgs e)
        {
            string strId = Request.QueryString.Get("RandomExamID");
            if (string.IsNullOrEmpty(strId))
            {
                SessionSet.PageMessage = "缺少参数！";
                return;
            }

            //获取当前考试的生成试卷的状态和次数
            RandomExamBLL objBll = new RandomExamBLL();
            RailExam.Model.RandomExam objExam = objBll.GetExam(Convert.ToInt32(strId));

			RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
			IList<RandomExamResultCurrent> objList = objResultCurrentBll.GetRandomExamResultInfo(Convert.ToInt32(strId));
			int n = 0;
			foreach (RandomExamResultCurrent current in objList)
			{
				if(current.StatusId > 0)
				{
					n = 1;
					break;
				}
			}
			if (n > 0)
			{
				SessionSet.PageMessage = "当前考试有考生参加考试，不能再删除试卷！";
				return;
			}

            RandomExamComputerServerBLL serverBll = new RandomExamComputerServerBLL();
            RandomExamComputerServer server = serverBll.GetRandomExamComputerServer(objExam.RandomExamId,
                                                                                    PrjPub.ServerNo);
            //if (!server.HasPaper)
            //{
            //    SessionSet.PageMessage = "当前考试还未生成试卷，不能删除试卷！";
            //    return;
            //}

            if (server.IsStart == 2)
            {
                SessionSet.PageMessage = "当前考试已经结束，不能再删除试卷！";
                return;
            }

            try
            {
                objResultCurrentBll.DelRandomExamResultCurrent(Convert.ToInt32(strId));
                objBll.UpdateHasPaper(Convert.ToInt32(strId),PrjPub.ServerNo,false);
                objBll.UpdateIsStart(Convert.ToInt32(strId), PrjPub.ServerNo, 0);
                if (objExam.StartMode == 2)
                {
                    objBll.UpdateStartCode(Convert.ToInt32(strId),PrjPub.ServerNo,string.Empty);
                    lblTitle.Visible = false;
                    lblCode.Visible = false;
                    btnApply.Visible = false;
                }
				SystemLogBLL objLogBll = new SystemLogBLL();
                objLogBll.WriteLog("“" + objExam.ExamName + "”删除所有考试试卷");
                SessionSet.PageMessage = "删除试卷成功！";

                ClientScript.RegisterStartupScript(GetType(),
                        "jsSelectFirstNode",
                        @"refreshGrid();",
                        true);
            }
            catch(Exception ex)
            {
                SessionSet.PageMessage = "删除试卷失败！";
                Pub.ShowErrorPage(ex.Message);
            }
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            string strId = Request.QueryString.Get("RandomExamID");
            if (string.IsNullOrEmpty(strId))
            {
                SessionSet.PageMessage = "缺少参数！";
                return;
            }
            //获取当前考试的生成试卷的状态和次数
            RandomExamBLL objBll = new RandomExamBLL();
        	RailExam.Model.RandomExam objExam;
            objExam = objBll.GetExam(Convert.ToInt32(strId));

            RandomExamComputerServerBLL serverBll = new RandomExamComputerServerBLL();
            RandomExamComputerServer server = serverBll.GetRandomExamComputerServer(objExam.RandomExamId,
                                                                                    PrjPub.ServerNo);

            if (!server.HasPaper)
			{
				SessionSet.PageMessage = "当前考试还未生成试卷，不能开始考试！";
				return;
			}

            if (objExam.StartMode == 1)
			{
				SessionSet.PageMessage = "当前考试的开考模式为随到随考，不需要开始考试！";
				return;
			}

            OracleAccess db = new OracleAccess();
            string strSql =
            @"select * from Random_Exam_Result_Answer_Cur where Random_Exam_Result_ID in (
                        select Random_Exam_Result_ID from Random_Exam_Result_Current 
                        where Random_Exam_ID=" + objExam.RandomExamId + ")";
            DataSet dsAnswer = db.RunSqlDataSet(strSql);
            if (dsAnswer.Tables[0].Rows.Count == 0)
            {
                SessionSet.PageMessage = "该考试试卷生成有误，请删除所有考生试卷后重新生成！";
                return;
            }

            if (server.IsStart == 1)
			{
				SessionSet.PageMessage = "当前考试已经开考！";
				return;
			}
            else if (server.IsStart == 2)
			{
				SessionSet.PageMessage = "当前考试已经结束！";
				return;
			}

			ClientScript.RegisterStartupScript(GetType(),
						"jsSelectFirstNode",
						@"StartPaper();",
						true);

			//}
        }

        protected void btnEnd_Click(object sender, EventArgs e)
        {
            string strId = Request.QueryString.Get("RandomExamID");
            if (string.IsNullOrEmpty(strId))
            {
                SessionSet.PageMessage = "缺少参数！";
                return;
            }
            //获取当前考试的生成试卷的状态和次数
            RandomExamBLL objBll = new RandomExamBLL();
            RailExam.Model.RandomExam objExam = objBll.GetExam(Convert.ToInt32(strId));


            RandomExamComputerServerBLL serverBll = new RandomExamComputerServerBLL();
            RandomExamComputerServer server = serverBll.GetRandomExamComputerServer(objExam.RandomExamId,
                                                                                    PrjPub.ServerNo);

            if (server.IsStart == 0)
            {
                if (objExam.EndTime >= DateTime.Today)
				{
					SessionSet.PageMessage = "当前考试还未开始考试！";
					return;
				}
			}
            else if (server.IsStart == 2)
			{
				SessionSet.PageMessage = "当前考试已经结束！";
				return;
			}

			ClientScript.RegisterStartupScript(GetType(),
						"jsSelectFirstNode",
						@"EndPaper();",
						true);
        }

        private void StopExam(string strResultID)
        {
            ViewState["EndTime"] = DateTime.Now.ToString();
            //记录当前考试所在地的OrgID
            ViewState["OrgID"] = ConfigurationManager.AppSettings["StationID"];

            RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
            RandomExamResultCurrent objResultCurrent = objResultCurrentBll.GetRandomExamResult(Convert.ToInt32(strResultID));
            objResultCurrent.CurrentDateTime = DateTime.Parse(ViewState["EndTime"].ToString());
            objResultCurrent.ExamTime = GetSecondBetweenTwoDate(DateTime.Parse(ViewState["EndTime"].ToString()),
                                    objResultCurrent.BeginDateTime);

            objResultCurrent.EndDateTime = DateTime.Parse(ViewState["EndTime"].ToString());
            objResultCurrent.Score = 0;
            objResultCurrent.OrganizationId = int.Parse(ViewState["OrgID"].ToString());
            objResultCurrent.Memo = "";
            //参加考试将当前考试的标志置为2－已经结束
            objResultCurrent.StatusId = 2;
            objResultCurrent.AutoScore = 0;
            objResultCurrent.CorrectRate = 0;

            RandomExamResultBLL objResultBll = new RandomExamResultBLL();
            try
            {
                objResultCurrentBll.UpdateRandomExamResultCurrent(objResultCurrent);
                int randomExamResultID = objResultBll.RemoveResultAnswer(Convert.ToInt32(strResultID));

                //将实时考试记录（临时表）转存到中间考试成绩表和答卷表
                //int randomExamResultID = objResultBll.RemoveResultAnswerCurrent(Convert.ToInt32(strResultID));

                try
                {
                    OracleAccess dbPhoto = new OracleAccess();
                    string strSql = "select * from Random_Exam_Result_Detail where Random_Exam_Result_ID=" +
                             randomExamResultID + " and Random_Exam_ID=" + objResultCurrent.RandomExamId;
                    DataSet dsPhoto = dbPhoto.RunSqlDataSet(strSql);

                    if (dsPhoto.Tables[0].Rows.Count > 0)
                    {
                        DataRow drPhoto = dsPhoto.Tables[0].Rows[0];
                        int employeeId = Convert.ToInt32(drPhoto["Employee_ID"]);
                        if (drPhoto["FingerPrint"] != DBNull.Value)
                        {
                            Pub.SavePhotoToLocal(objResultCurrent.RandomExamId, employeeId, (byte[])drPhoto["FingerPrint"], 0, randomExamResultID);
                        }
                        if (drPhoto["Photo1"] != DBNull.Value)
                        {
                            Pub.SavePhotoToLocal(objResultCurrent.RandomExamId, employeeId, (byte[])drPhoto["Photo1"], 1, randomExamResultID);
                        }
                        if (drPhoto["Photo2"] != DBNull.Value)
                        {
                            Pub.SavePhotoToLocal(objResultCurrent.RandomExamId, employeeId, (byte[])drPhoto["Photo2"], 2, randomExamResultID);
                        }
                        if (drPhoto["Photo3"] != DBNull.Value)
                        {
                            Pub.SavePhotoToLocal(objResultCurrent.RandomExamId, employeeId, (byte[])drPhoto["Photo3"], 3, randomExamResultID);
                        }
                    }
                }
                catch
                {
                    hfSql.Value = GetSql();
                }

				#region 上传路局
				//try
				//{
				//    if (!PrjPub.IsServerCenter)
				//    {
				//        RandomExamResult randomExamResult =
				//            objResultBll.GetRandomExamResult(randomExamResultID);
				//        objResultBll.AddRandomExamResultToServer(randomExamResult);
				//    }
				//}
				//catch
				//{
				//    Pub.ShowErrorPage("无法连接路局服务器，请检查站段服务器网络连接是否正常！");
				//}
				#endregion

                hfSql.Value = GetSql();
				examsGrid.DataBind();
				RandomExamBLL objBll = new RandomExamBLL();
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("“" + objBll.GetExam(objResultCurrent.RandomExamId).ExamName + "”中的“" + objResultCurrent.ExamineeName + "”终止考试");
                SessionSet.PageMessage = "终止考试成功！";
            }
            catch
            {
                SessionSet.PageMessage = "终止考试失败！";
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if(PrjPub.IsServerCenter)
            {
                SessionSet.PageMessage = "请确认当前数据库连接为站段数据库！";
                return;
            }

            string strSql = "select GetOrgName(org_ID) from SYNCHRONIZE_LOG@link_sf where SYNCHRONIZE_STATUS_ID=1 and SYNCHRONIZE_TYPE_ID=6";
            OracleAccess db = new OracleAccess();
            DataSet ds = db.RunSqlDataSet(strSql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                SessionSet.PageMessage = "站段【" + ds.Tables[0].Rows[0][0]+ "】正在上传考试成绩或答卷，请稍后再上传考试答卷！";
                return;
            }

            string strId = Request.QueryString.Get("RandomExamID");
            if (string.IsNullOrEmpty(strId))
            {
                SessionSet.PageMessage = "缺少参数！";
                return;
            }

			ClientScript.RegisterStartupScript(GetType(),
						"jsSelectFirstNode",
						@"UploadExam(2);",
						true);
        }

        protected void btnUploadScore_Click(object sender, EventArgs e)
        {
            if (PrjPub.IsServerCenter)
            {
                SessionSet.PageMessage = "请确认当前数据库连接为站段数据库！";
                return;
            }

            string strSql = "select * from SYNCHRONIZE_LOG@link_sf where SYNCHRONIZE_STATUS_ID=1 and SYNCHRONIZE_TYPE_ID=6";
            OracleAccess db = new OracleAccess();
            DataSet ds = db.RunSqlDataSet(strSql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                SessionSet.PageMessage = "有站段正在上传考试成绩或答卷，请稍后再上传考试成绩！";
                return;
            }

            string strId = Request.QueryString.Get("RandomExamID");
            if (string.IsNullOrEmpty(strId))
            {
                SessionSet.PageMessage = "缺少参数！";
                return;
            }

            ClientScript.RegisterStartupScript(GetType(),
                        "jsSelectFirstNode",
                        @"UploadExam(1);",
                        true);
        }

        private int GetSecondBetweenTwoDate(DateTime dt1, DateTime dt2)
        {
            int i1 = dt1.Hour * 3600 + dt1.Minute * 60 + dt1.Second;
            int i2 = dt2.Hour * 3600 + dt2.Minute * 60 + dt2.Second;

            return i1 - i2;
        }

		protected void btnStop_Click(object sender, EventArgs e)
		{
			ViewState["EndTime"] = DateTime.Now.ToString();
			//记录当前考试所在地的OrgID
			ViewState["OrgID"] = ConfigurationManager.AppSettings["StationID"];

			string strId = Request.QueryString.Get("RandomExamID");
			if (string.IsNullOrEmpty(strId))
			{
				SessionSet.PageMessage = "缺少参数！";
				return;
			}

			//获取当前考试的生成试卷的状态和次数
			RandomExamBLL objBll = new RandomExamBLL();
            RailExam.Model.RandomExam objExam = objBll.GetExam(Convert.ToInt32(strId));

            RandomExamComputerServerBLL serverBll = new RandomExamComputerServerBLL();
            RandomExamComputerServer server = serverBll.GetRandomExamComputerServer(objExam.RandomExamId,
                                                                                    PrjPub.ServerNo);


            if (server.IsStart == 0)
			{
                if (objExam.EndTime >= DateTime.Today)
				{
					SessionSet.PageMessage = "当前考试还未开始考试！";
					return;
				}
			}
            else if (server.IsStart == 2)
			{
				SessionSet.PageMessage = "当前考试已经结束！";
				return;
			}

			RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
			IList<RandomExamResultCurrent> objResultCurrent =
				objResultCurrentBll.GetStartRandomExamResultInfo(Convert.ToInt32(strId));

			IList<RandomExamResultCurrent> objResultCurrentNew = new List<RandomExamResultCurrent>();

			try
			{
                RandomExamResultBLL objResultBll = new RandomExamResultBLL();

                foreach (RandomExamResultCurrent current in objResultCurrent)
                {
                    current.CurrentDateTime = DateTime.Parse(ViewState["EndTime"].ToString());
                    current.ExamTime = GetSecondBetweenTwoDate(DateTime.Parse(ViewState["EndTime"].ToString()),
                                            current.BeginDateTime);

                    current.EndDateTime = DateTime.Parse(ViewState["EndTime"].ToString());
                    current.Score = 0;
                    current.OrganizationId = int.Parse(ViewState["OrgID"].ToString());
                    current.Memo = "";
                    //参加考试将当前考试的标志置为2－已经结束
                    current.StatusId = 2;
                    current.AutoScore = 0;
                    current.CorrectRate = 0;
                    //objResultCurrentNew.Add(current);
                    objResultCurrentBll.UpdateRandomExamResultCurrent(current);

                    int randomExamResultID = objResultBll.RemoveResultAnswer(Convert.ToInt32(current.RandomExamResultId));
                    //int randomExamResultID = objResultBll.RemoveResultAnswerCurrent(Convert.ToInt32(current.RandomExamResultId));

                    try
                    {
                        OracleAccess dbPhoto = new OracleAccess();
                        string strSql = "select * from Random_Exam_Result_Detail where Random_Exam_Result_ID=" +
                                 randomExamResultID + " and Random_Exam_ID=" + current.RandomExamId;
                        DataSet dsPhoto = dbPhoto.RunSqlDataSet(strSql);

                        if (dsPhoto.Tables[0].Rows.Count > 0)
                        {
                            DataRow drPhoto = dsPhoto.Tables[0].Rows[0];
                            int employeeId = Convert.ToInt32(drPhoto["Employee_ID"]);
                            if (drPhoto["FingerPrint"] != DBNull.Value)
                            {
                                Pub.SavePhotoToLocal(current.RandomExamId, employeeId, (byte[])drPhoto["FingerPrint"], 0, randomExamResultID);
                            }
                            if (drPhoto["Photo1"] != DBNull.Value)
                            {
                                Pub.SavePhotoToLocal(current.RandomExamId, employeeId, (byte[])drPhoto["Photo1"], 1, randomExamResultID);
                            }
                            if (drPhoto["Photo2"] != DBNull.Value)
                            {
                                Pub.SavePhotoToLocal(current.RandomExamId, employeeId, (byte[])drPhoto["Photo2"], 2, randomExamResultID);
                            }
                            if (drPhoto["Photo3"] != DBNull.Value)
                            {
                                Pub.SavePhotoToLocal(current.RandomExamId, employeeId, (byte[])drPhoto["Photo3"], 3, randomExamResultID);
                            }
                        }
                    }
                    catch
                    {
                        hfSql.Value = GetSql();
                    }
                }

                hfSql.Value = GetSql();
				examsGrid.DataBind();
				SystemLogBLL objLogBll = new SystemLogBLL();
                objLogBll.WriteLog("“" + objExam.ExamName + "”终止正在进行的所有考试");
				SessionSet.PageMessage = "终止当前考试成功！";
			}
			catch
			{
				SessionSet.PageMessage = "终止当前考试失败！";
			}
		}

		private void DownloadStudentInfoExcel()
		{
			string path = Server.MapPath("/RailExamBao/Excel/Excel.xls");

			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam objRandomExam = objBll.GetExam(Convert.ToInt32(Request.QueryString.Get("RandomExamID")));

			if (File.Exists(path))
			{
				FileInfo file = new FileInfo(path);
				this.Response.Clear();
				this.Response.Buffer = true;
				this.Response.Charset = "utf-7";
				this.Response.ContentEncoding = Encoding.UTF7;
				// 添加头信息，为"文件下载/另存为"对话框指定默认文件名
				this.Response.AddHeader("Content-Disposition",
				                        "attachment; filename=" + HttpUtility.UrlEncode(objRandomExam.ExamName + "参加考试学员名单") +
				                        ".xls");
				// 添加头信息，指定文件大小，让浏览器能够显示下载进度
				this.Response.AddHeader("Content-Length", file.Length.ToString());

				// 指定返回的是一个不能被客户端读取的流，必须被下载
				this.Response.ContentType = "application/ms-excel";

				// 把文件流发送到客户端
				this.Response.WriteFile(file.FullName);
			}
		}

        protected  void btnAddEmployee_Click(object sender, EventArgs e)
        {
            string strId = Request.QueryString.Get("RandomExamID");

            RandomExamBLL objBll = new RandomExamBLL();
            RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(strId));

            if (obj.CreatePerson != PrjPub.CurrentLoginUser.EmployeeName && PrjPub.CurrentLoginUser.RoleID != 362 && PrjPub.CurrentLoginUser.RoleID != 1)
            {
                SessionSet.PageMessage = "您没有添加考生的权限！添加考生只能由出卷人操作。";
                return;
            }

            ClientScript.RegisterStartupScript(GetType(), "jsSelectFirstNode", @"showEmployee("+ (obj.HasTrainClass ?1:0) +")", true);
        }

        private string GetSql()
        {
            string sql = "";
            if(!string.IsNullOrEmpty(txtEmployeeName.Text.Trim()))
            {
                sql += " and e.Employee_Name like '%" + txtEmployeeName.Text.Trim() + "%'";
            }

            if (!string.IsNullOrEmpty(txtWorkNo.Text.Trim()))
            {
                sql += " and e.Work_No like '%" + txtWorkNo.Text.Trim() + "%'";
            }

            if (!string.IsNullOrEmpty(txtIdentityCardNo.Text.Trim()))
            {
                sql += " and e.Identity_CardNo like '%" + txtIdentityCardNo.Text.Trim() + "%'";
            }
            return sql;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            hfSql.Value = GetSql();
            examsGrid.DataBind();
        }

        private void DeleteExam(string strId)
        {
            RandomExamResultCurrentBLL objBll = new RandomExamResultCurrentBLL();
            RandomExamResultCurrent current = objBll.GetRandomExamResult(Convert.ToInt32(strId));

            #region 屏蔽
            /*
            try
            {
                string strSql;
                OracleAccess db =
                    new OracleAccess(ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString);

                strSql = "select * from Random_Exam_Arrange_Detail where Random_Exam_ID=" + current.RandomExamId +
                         " and Employee_ID=" + current.ExamineeId +
                         " and Random_Exam_Result_ID=" + strId +
                         " and Is_Remove=0";
                DataSet dsOther = db.RunSqlDataSet(strSql);

                //查询微机教室
                string computerId = dsOther.Tables[0].Rows[0]["Computer_Room_ID"].ToString();

                //将存在该考生的安排表和安排明细进行修改去除该考生
                string strReplace = "," + current.ExamineeId + ",";

                strSql = "update Random_Exam_Arrange_Detail "
                         + "set User_ids = substr(Replace(','||User_ids||',','" + strReplace +
                         "',','),2,length(Replace(','||User_ids||',','" + strReplace + "',','))-2) "
                         + "where  ','|| User_ids || ',' like '%" + strReplace + "%' and Random_Exam_ID=" +
                         current.RandomExamId +
                         " and Computer_Room_ID=" + computerId;
                db.ExecuteNonQuery(strSql);

                //最后删除User_ids为空的考生安排明细信息
                strSql = "delete from Random_Exam_Arrange_Detail where User_ids is null  and Random_Exam_ID=" +
                         current.RandomExamId;
                db.ExecuteNonQuery(strSql);

                strSql = "update Random_Exam_Arrange "
                         + "set User_ids = substr(Replace(','||User_ids||',','" + strReplace +
                         "',','),2,length(Replace(','||User_ids||',','" + strReplace + "',','))-2) "
                         + "where  ','|| User_ids || ',' like '%" + strReplace + "%' and Random_Exam_ID=" +
                         current.RandomExamId;
                db.ExecuteNonQuery(strSql);
            }
            catch
            {
                return;
            }*/
            #endregion

            objBll.DelRandomExamResultCurrentByResultID(current.ExamineeId,Convert.ToInt32(strId));
            examsGrid.DataSource=objBll.GetRandomExamResultInfoByExamID(current.RandomExamId, GetSql());
            examsGrid.DataBind();
        }

        private void ClearExam(string strId)
        {
            RandomExamResultBLL objResultBll = new RandomExamResultBLL();
            RandomExamResult result = objResultBll.GetRandomExamResult(Convert.ToInt32(strId));

            RandomExamResultCurrentBLL objBll = new RandomExamResultCurrentBLL();
            objBll.ClearRandomExamResultCurrentByResultID(result.ExamineeId, Convert.ToInt32(strId));
            examsGrid.DataSource = objBll.GetRandomExamResultInfoByExamID(result.RandomExamId, GetSql());
            examsGrid.DataBind();
        }

        private void ReplyExam(string strId)
        {
            RandomExamResultBLL objResultBll = new RandomExamResultBLL();
            RandomExamResult result = objResultBll.GetRandomExamResult(Convert.ToInt32(strId));

            RandomExamResultCurrentBLL objBll = new RandomExamResultCurrentBLL();
            objBll.ReplyRandomExamResultCurrentByResultID(result.ExamineeId, Convert.ToInt32(strId));
            examsGrid.DataSource = objBll.GetRandomExamResultInfoByExamID(result.RandomExamId, GetSql());
            examsGrid.DataBind();
        }
    }
}
