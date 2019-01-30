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
				//�����ϵͳ����Ա����վ�η�������û��Ȩ�޼��վ�ο���
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
                //    examsGrid.Levels[0].Columns[3].HeadingText = "Ա������";
                //}
                //else
                //{
                //    examsGrid.Levels[0].Columns[3].HeadingText = "���ʱ��";
                //}

				if(PrjPub.IsServerCenter)
				{
					btnUpload.Enabled = false;
				    btnUploadScore.Enabled = false;
				}

				//��ǰվ������̨���������ҵ�ǰ������Ϊ�η�����ʱ���Ρ�ɾ�����п����Ծ��͡��ϴ����Գɼ�����ť
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

				//��ȡ��ǰ���Ե������Ծ��״̬�ʹ���
				RandomExamBLL objBll = new RandomExamBLL();
				RailExam.Model.RandomExam objRandomExam = objBll.GetExam(Convert.ToInt32(strId));

                RandomExamComputerServerBLL serverBll = new RandomExamComputerServerBLL();
                RandomExamComputerServer server = serverBll.GetRandomExamComputerServer(objRandomExam.RandomExamId,
                                                                                        PrjPub.ServerNo);

                if (server.HasPaper && objRandomExam.StartMode == 2 && server.IsStart != 0)
				{
					lblTitle.Text = "������֤�룺";
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
				SessionSet.PageMessage = "���ɳɹ���";
			}

			if (Request.Form.Get("IsEnd") != null && Request.Form.Get("IsEnd") != "")
			{
                hfSql.Value = GetSql();
				examsGrid.DataBind();
				SessionSet.PageMessage = "�������Գɹ���";
			}

			if(Request.Form.Get("IsUpload") != null && Request.Form.Get("IsUpload")!="")
			{
                hfSql.Value = GetSql();
				examsGrid.DataBind();
				string strId = Request.QueryString.Get("RandomExamID");
				RandomExamBLL objBll = new RandomExamBLL();
				RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(strId));
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("��" + obj.ExamName + "���ϴ����Գɼ��ʹ��");
				SessionSet.PageMessage = "�ϴ��ɹ���";
			}

			if (Request.Form.Get("IsStart") != null && Request.Form.Get("IsStart") != "")
			{
                hfSql.Value = GetSql();
				examsGrid.DataBind();
				SessionSet.PageMessage = "��ʼ���Գɹ���";
				string strId = Request.QueryString.Get("RandomExamID");
				//��ȡ��ǰ���Ե������Ծ��״̬�ʹ���
                RandomExamBLL objBll = new RandomExamBLL();
                RailExam.Model.RandomExam objExam = objBll.GetExam(Convert.ToInt32(strId));

                RandomExamComputerServerBLL serverBll = new RandomExamComputerServerBLL();
                RandomExamComputerServer server = serverBll.GetRandomExamComputerServer(objExam.RandomExamId,
                                                                                        PrjPub.ServerNo);


                if (server.HasPaper && objExam.StartMode == 2)
				{
                    lblCode.Text = "������֤�룺" + server.RandomExamCode;
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

            btnUpload.Visible = btnUploadScore.Visible = false; // ���������ò����ϴ� 2014-03-18
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
				// ���ͷ��Ϣ��Ϊ"�ļ�����/���Ϊ"�Ի���ָ��Ĭ���ļ���
				this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(strName) + ".doc");
				// ���ͷ��Ϣ��ָ���ļ���С����������ܹ���ʾ���ؽ���
				this.Response.AddHeader("Content-Length", file.Length.ToString());
				// ָ�����ص���һ�����ܱ��ͻ��˶�ȡ���������뱻����
				this.Response.ContentType = "application/ms-word";
				// ���ļ������͵��ͻ���
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
				SessionSet.PageMessage = "ȱ�ٲ�����";
				return;
			}

			//��ȡ��ǰ���Ե������Ծ��״̬�ʹ���
			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam objExam = objBll.GetExam(Convert.ToInt32(strId));

			if(!PrjPub.IsServerCenter)
			{
				int nowVersion = objBll.GetExamServer(Convert.ToInt32(strId)).Version;
                if (nowVersion != objExam.Version)
				{
					SessionSet.PageMessage = "��ǰ���԰汾��·�ֲ�ƥ�䣬������ͬ���������ݣ�";
					return;
				}
			}

            if (objExam.EndTime < DateTime.Today)
			{
				SessionSet.PageMessage = "��ǰ�����ѹ��ڣ��������ɿ����Ծ�";
				return;
			}

            RandomExamComputerServerBLL serverBll = new RandomExamComputerServerBLL();
            RandomExamComputerServer server = serverBll.GetRandomExamComputerServer(objExam.RandomExamId,
                                                                                    PrjPub.ServerNo);
            if (server.HasPaper)
			{
				SessionSet.PageMessage = "��ǰ�����������Ծ�";
				return;
			}

			//��ȡ��ǰ���ԵĿ�����Ϣ
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
				SessionSet.PageMessage = "��Ϊ�ÿ�����ӿ�����";
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
                SessionSet.PageMessage = "ȱ�ٲ�����";
                return;
            }

            //��ȡ��ǰ���Ե������Ծ��״̬�ʹ���
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
				SessionSet.PageMessage = "��ǰ�����п����μӿ��ԣ�������ɾ���Ծ�";
				return;
			}

            RandomExamComputerServerBLL serverBll = new RandomExamComputerServerBLL();
            RandomExamComputerServer server = serverBll.GetRandomExamComputerServer(objExam.RandomExamId,
                                                                                    PrjPub.ServerNo);
            //if (!server.HasPaper)
            //{
            //    SessionSet.PageMessage = "��ǰ���Ի�δ�����Ծ�����ɾ���Ծ�";
            //    return;
            //}

            if (server.IsStart == 2)
            {
                SessionSet.PageMessage = "��ǰ�����Ѿ�������������ɾ���Ծ�";
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
                objLogBll.WriteLog("��" + objExam.ExamName + "��ɾ�����п����Ծ�");
                SessionSet.PageMessage = "ɾ���Ծ�ɹ���";

                ClientScript.RegisterStartupScript(GetType(),
                        "jsSelectFirstNode",
                        @"refreshGrid();",
                        true);
            }
            catch(Exception ex)
            {
                SessionSet.PageMessage = "ɾ���Ծ�ʧ�ܣ�";
                Pub.ShowErrorPage(ex.Message);
            }
        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            string strId = Request.QueryString.Get("RandomExamID");
            if (string.IsNullOrEmpty(strId))
            {
                SessionSet.PageMessage = "ȱ�ٲ�����";
                return;
            }
            //��ȡ��ǰ���Ե������Ծ��״̬�ʹ���
            RandomExamBLL objBll = new RandomExamBLL();
        	RailExam.Model.RandomExam objExam;
            objExam = objBll.GetExam(Convert.ToInt32(strId));

            RandomExamComputerServerBLL serverBll = new RandomExamComputerServerBLL();
            RandomExamComputerServer server = serverBll.GetRandomExamComputerServer(objExam.RandomExamId,
                                                                                    PrjPub.ServerNo);

            if (!server.HasPaper)
			{
				SessionSet.PageMessage = "��ǰ���Ի�δ�����Ծ����ܿ�ʼ���ԣ�";
				return;
			}

            if (objExam.StartMode == 1)
			{
				SessionSet.PageMessage = "��ǰ���ԵĿ���ģʽΪ�浽�濼������Ҫ��ʼ���ԣ�";
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
                SessionSet.PageMessage = "�ÿ����Ծ�����������ɾ�����п����Ծ���������ɣ�";
                return;
            }

            if (server.IsStart == 1)
			{
				SessionSet.PageMessage = "��ǰ�����Ѿ�������";
				return;
			}
            else if (server.IsStart == 2)
			{
				SessionSet.PageMessage = "��ǰ�����Ѿ�������";
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
                SessionSet.PageMessage = "ȱ�ٲ�����";
                return;
            }
            //��ȡ��ǰ���Ե������Ծ��״̬�ʹ���
            RandomExamBLL objBll = new RandomExamBLL();
            RailExam.Model.RandomExam objExam = objBll.GetExam(Convert.ToInt32(strId));


            RandomExamComputerServerBLL serverBll = new RandomExamComputerServerBLL();
            RandomExamComputerServer server = serverBll.GetRandomExamComputerServer(objExam.RandomExamId,
                                                                                    PrjPub.ServerNo);

            if (server.IsStart == 0)
            {
                if (objExam.EndTime >= DateTime.Today)
				{
					SessionSet.PageMessage = "��ǰ���Ի�δ��ʼ���ԣ�";
					return;
				}
			}
            else if (server.IsStart == 2)
			{
				SessionSet.PageMessage = "��ǰ�����Ѿ�������";
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
            //��¼��ǰ�������ڵص�OrgID
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
            //�μӿ��Խ���ǰ���Եı�־��Ϊ2���Ѿ�����
            objResultCurrent.StatusId = 2;
            objResultCurrent.AutoScore = 0;
            objResultCurrent.CorrectRate = 0;

            RandomExamResultBLL objResultBll = new RandomExamResultBLL();
            try
            {
                objResultCurrentBll.UpdateRandomExamResultCurrent(objResultCurrent);
                int randomExamResultID = objResultBll.RemoveResultAnswer(Convert.ToInt32(strResultID));

                //��ʵʱ���Լ�¼����ʱ��ת�浽�м俼�Գɼ���ʹ���
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

				#region �ϴ�·��
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
				//    Pub.ShowErrorPage("�޷�����·�ַ�����������վ�η��������������Ƿ�������");
				//}
				#endregion

                hfSql.Value = GetSql();
				examsGrid.DataBind();
				RandomExamBLL objBll = new RandomExamBLL();
				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("��" + objBll.GetExam(objResultCurrent.RandomExamId).ExamName + "���еġ�" + objResultCurrent.ExamineeName + "����ֹ����");
                SessionSet.PageMessage = "��ֹ���Գɹ���";
            }
            catch
            {
                SessionSet.PageMessage = "��ֹ����ʧ�ܣ�";
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if(PrjPub.IsServerCenter)
            {
                SessionSet.PageMessage = "��ȷ�ϵ�ǰ���ݿ�����Ϊվ�����ݿ⣡";
                return;
            }

            string strSql = "select GetOrgName(org_ID) from SYNCHRONIZE_LOG@link_sf where SYNCHRONIZE_STATUS_ID=1 and SYNCHRONIZE_TYPE_ID=6";
            OracleAccess db = new OracleAccess();
            DataSet ds = db.RunSqlDataSet(strSql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                SessionSet.PageMessage = "վ�Ρ�" + ds.Tables[0].Rows[0][0]+ "�������ϴ����Գɼ��������Ժ����ϴ����Դ��";
                return;
            }

            string strId = Request.QueryString.Get("RandomExamID");
            if (string.IsNullOrEmpty(strId))
            {
                SessionSet.PageMessage = "ȱ�ٲ�����";
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
                SessionSet.PageMessage = "��ȷ�ϵ�ǰ���ݿ�����Ϊվ�����ݿ⣡";
                return;
            }

            string strSql = "select * from SYNCHRONIZE_LOG@link_sf where SYNCHRONIZE_STATUS_ID=1 and SYNCHRONIZE_TYPE_ID=6";
            OracleAccess db = new OracleAccess();
            DataSet ds = db.RunSqlDataSet(strSql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                SessionSet.PageMessage = "��վ�������ϴ����Գɼ��������Ժ����ϴ����Գɼ���";
                return;
            }

            string strId = Request.QueryString.Get("RandomExamID");
            if (string.IsNullOrEmpty(strId))
            {
                SessionSet.PageMessage = "ȱ�ٲ�����";
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
			//��¼��ǰ�������ڵص�OrgID
			ViewState["OrgID"] = ConfigurationManager.AppSettings["StationID"];

			string strId = Request.QueryString.Get("RandomExamID");
			if (string.IsNullOrEmpty(strId))
			{
				SessionSet.PageMessage = "ȱ�ٲ�����";
				return;
			}

			//��ȡ��ǰ���Ե������Ծ��״̬�ʹ���
			RandomExamBLL objBll = new RandomExamBLL();
            RailExam.Model.RandomExam objExam = objBll.GetExam(Convert.ToInt32(strId));

            RandomExamComputerServerBLL serverBll = new RandomExamComputerServerBLL();
            RandomExamComputerServer server = serverBll.GetRandomExamComputerServer(objExam.RandomExamId,
                                                                                    PrjPub.ServerNo);


            if (server.IsStart == 0)
			{
                if (objExam.EndTime >= DateTime.Today)
				{
					SessionSet.PageMessage = "��ǰ���Ի�δ��ʼ���ԣ�";
					return;
				}
			}
            else if (server.IsStart == 2)
			{
				SessionSet.PageMessage = "��ǰ�����Ѿ�������";
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
                    //�μӿ��Խ���ǰ���Եı�־��Ϊ2���Ѿ�����
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
                objLogBll.WriteLog("��" + objExam.ExamName + "����ֹ���ڽ��е����п���");
				SessionSet.PageMessage = "��ֹ��ǰ���Գɹ���";
			}
			catch
			{
				SessionSet.PageMessage = "��ֹ��ǰ����ʧ�ܣ�";
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
				// ���ͷ��Ϣ��Ϊ"�ļ�����/���Ϊ"�Ի���ָ��Ĭ���ļ���
				this.Response.AddHeader("Content-Disposition",
				                        "attachment; filename=" + HttpUtility.UrlEncode(objRandomExam.ExamName + "�μӿ���ѧԱ����") +
				                        ".xls");
				// ���ͷ��Ϣ��ָ���ļ���С����������ܹ���ʾ���ؽ���
				this.Response.AddHeader("Content-Length", file.Length.ToString());

				// ָ�����ص���һ�����ܱ��ͻ��˶�ȡ���������뱻����
				this.Response.ContentType = "application/ms-excel";

				// ���ļ������͵��ͻ���
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
                SessionSet.PageMessage = "��û����ӿ�����Ȩ�ޣ���ӿ���ֻ���ɳ����˲�����";
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

            #region ����
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

                //��ѯ΢������
                string computerId = dsOther.Tables[0].Rows[0]["Computer_Room_ID"].ToString();

                //�����ڸÿ����İ��ű�Ͱ�����ϸ�����޸�ȥ���ÿ���
                string strReplace = "," + current.ExamineeId + ",";

                strSql = "update Random_Exam_Arrange_Detail "
                         + "set User_ids = substr(Replace(','||User_ids||',','" + strReplace +
                         "',','),2,length(Replace(','||User_ids||',','" + strReplace + "',','))-2) "
                         + "where  ','|| User_ids || ',' like '%" + strReplace + "%' and Random_Exam_ID=" +
                         current.RandomExamId +
                         " and Computer_Room_ID=" + computerId;
                db.ExecuteNonQuery(strSql);

                //���ɾ��User_idsΪ�յĿ���������ϸ��Ϣ
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
