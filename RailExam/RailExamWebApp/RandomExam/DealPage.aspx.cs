using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
	public partial class DealPage : PageBase
	{
		public string load = "false";
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Request.QueryString.Get("type") == "Get")
				{
					GetPaper();
					load = "true";
				}

				if (Request.QueryString.Get("type") == "GetAfter")
				{
					GetPaperAfter();
					load = "true";
				}
				
				if(Request.QueryString.Get("type") == "End")
				{
					EndExam();
				}

				if (Request.QueryString.Get("type") == "Upload")
				{
					UploadExam();
				}

                if (Request.QueryString.Get("type") == "CheckExam")
                {
                    CheckExam();
                }
			}
		}


		private void UploadExam()
		{
			string strId = Request.QueryString.Get("RandomExamID");
			//获取当前考试的生成试卷的状态和次数
			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(strId));

		    string typeid = Request.QueryString.Get("typeid");
            string strSql,strKey="0";
            OracleAccess db = new OracleAccess();

			try
			{
				RandomExamResultBLL objResultBll = new RandomExamResultBLL();

                strSql = "select SYNCHRONIZE_LOG_SEQ.NextVal@link_sf from dual";
                DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];
                strKey = dr[0].ToString();

                strSql = "insert into  SYNCHRONIZE_LOG@link_sf values(" + strKey + ","
                    + ConfigurationManager.AppSettings["StationID"] + ",6,sysdate,null,1," + PrjPub.ServerNo + ")";
                db.ExecuteNonQuery(strSql);

                if(typeid == "1")
                {
                     objResultBll.RemoveRandomExamResultToServer(Convert.ToInt32(strId), Convert.ToInt32(ConfigurationManager.AppSettings["StationID"]));
                     //只上传成绩须更新时间
                     strSql = "update Random_Exam_Computer_Server@link_sf set  "
                                     + "Last_Upload_Date=sysdate   where random_exam_id=" + obj.RandomExamId
                                     + " and Computer_server_no=" + PrjPub.ServerNo;
                     db.ExecuteNonQuery(strSql);
                }
                else
                {
                     objResultBll.RemoveRandomExamResultToServerAnswer(Convert.ToInt32(strId), Convert.ToInt32(ConfigurationManager.AppSettings["StationID"]));

                     //如果考试已经结束上传，将上传标志置为已经上传
                     if (obj.IsStart == 2)
                     {
                         objBll.UpdateIsUpload(obj.RandomExamId, PrjPub.ServerNo, 1);
                     }
                     else
                     {
                         objBll.UpdateIsUpload(obj.RandomExamId, PrjPub.ServerNo, 0);
                     }
                 }

                 strSql = "update SYNCHRONIZE_LOG@link_sf set SYNCHRONIZE_STATUS_ID=2,End_Time=sysdate where SYNCHRONIZE_LOG_ID=" + strKey;
                 db.ExecuteNonQuery(strSql);

                 #region 原方法
                 /*
                //上传答卷
                if (typeid == "2")
                {

                    IList<RandomExamResult> objList = objResultBll.GetRandomExamResultInfoStation(
                        Convert.ToInt32(strId), Convert.ToInt32(ConfigurationManager.AppSettings["StationID"]));
                    RandomExamResultAnswerBLL objResultAnswerBll = new RandomExamResultAnswerBLL();
                    IList<RandomExamResultAnswerStation> objAnswerStationList =
                        new List<RandomExamResultAnswerStation>();
                    foreach (RandomExamResult result in objList)
                    {
                        IList<RandomExamResultAnswer> objAnswerList =
                            objResultAnswerBll.GetExamResultAnswers(result.RandomExamResultIDStation);

                        foreach (RandomExamResultAnswer answer in objAnswerList)
                        {
                            RandomExamResultAnswerStation objStation = new RandomExamResultAnswerStation();
                            objStation.RandomExamResultID = result.RandomExamResultId;
                            objStation.RandomExamItemID = answer.RandomExamItemId;
                            objStation.Answer = answer.Answer;
                            objStation.ExamTime = answer.ExamTime;
                            objStation.JudgeScore = answer.JudgeScore;
                            objStation.JudgeStatusID = answer.JudgeStatusId;
                            objStation.JudgeRemark = answer.JudgeRemark;
                            objStation.RandomExamResultIDStation = answer.RandomExamResultId;

                            objAnswerStationList.Add(objStation);
                        }
                    }
                    RandomExamResultAnswerStationBLL objStationBll = new RandomExamResultAnswerStationBLL();
                    objStationBll.AddExamResultAnswerStation(objAnswerStationList);

                    //如果考试已经结束上传，将上传标志置为已经上传
                    if (obj.IsStart == 2)
                    {
                        objBll.UpdateIsUpload(obj.RandomExamId, PrjPub.ServerNo, 1);
                    }
                    else
                    {
                        objBll.UpdateIsUpload(obj.RandomExamId, PrjPub.ServerNo, 0);
                    }
                }
                else
                {
                    //只上传成绩须更新时间
                    string strSql = "update Random_Exam_Computer_Server@link_sf set  "
                                    + "Last_Upload_Date=sysdate   where random_exam_id=" + obj.RandomExamId
                                    + " and Computer_server_no="+PrjPub.ServerNo;
                    OracleAccess db = new OracleAccess();
                    db.ExecuteNonQuery(strSql);
                }
                */
                 #endregion

                 Response.Write("<script>top.returnValue='true';window.close();</script>");
			}
			catch
			{
                if (typeid == "2")
                {
                    strSql = "update SYNCHRONIZE_LOG@link_sf set SYNCHRONIZE_STATUS_ID=3,End_Time=sysdate where SYNCHRONIZE_LOG_ID=" + strKey;
                    db.ExecuteNonQuery(strSql);
                }
				Response.Write("<script>top.returnValue='false';window.close();</script>");
			}
		}

		private void GetPaper()
		{
			ViewState["BeginTime"] = DateTime.Now.ToString();
			string strId = Request.QueryString.Get("RandomExamID");

			//获取当前考试的生成试卷的状态和次数
			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(strId));

			int year = obj.BeginTime.Year;
			int ExamCount = obj.MaxExamTimes;

            RandomExamArrangeBLL eaBll = new RandomExamArrangeBLL();
            IList<RailExam.Model.RandomExamArrange> ExamArranges = eaBll.GetRandomExamArranges(int.Parse(strId));
            string strChooseID = "";
            OracleAccess db = new OracleAccess();
		    string strSql;
            if (ExamArranges.Count > 0)
            {
                strSql = "select a.* from Random_Exam_Arrange_Detail a "
                          + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
                          + " inner join Computer_Server c on c.Computer_server_ID=b.Computer_Server_ID"
                          + " where c.Computer_Server_No='" + PrjPub.ServerNo + "' "
                          + " and Random_Exam_ID=" + strId;

                DataSet ds = db.RunSqlDataSet(strSql);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if(string.IsNullOrEmpty(strChooseID))
                    {
                        strChooseID += dr["User_Ids"].ToString();
                    }
                    else
                    {
                        strChooseID += "," + dr["User_Ids"];
                    }
                }
            }
            else
            {
                strChooseID = "";
            }

            if(strChooseID == "")
            {
                Response.Write("<script>top.returnValue='本场考试未在本单位安排考生！';window.close();</script>");
                return;
            }


            //每次生成试卷之前删除已生成的考试试卷
            RandomExamResultCurrentBLL randomExamResultBLL = new RandomExamResultCurrentBLL();
            randomExamResultBLL.DelRandomExamResultCurrent(Convert.ToInt32(strId));

			string[] str = strChooseID.Split(',');

			RandomExamResultAnswerCurrentBLL randomExamResultAnswerBLL = new RandomExamResultAnswerCurrentBLL();
			//定义全局答卷对象List
			IList<RandomExamResultAnswerCurrent> randomExamResultAnswersCurrentAll = new List<RandomExamResultAnswerCurrent>();
			//定义一个考生一次答卷对象List
			IList<RandomExamResultAnswerCurrent> randomExamResultAnswers = null;
			for (int n = 1; n <= ExamCount; n++)
			{
				for (int m = 0; m < str.Length; m++)
				{
					RandomExamResultCurrent randomExamResult = new RandomExamResultCurrent();

					randomExamResult.RandomExamId = int.Parse(strId);
					randomExamResult.AutoScore = 0;
					randomExamResult.BeginDateTime = DateTime.Parse(ViewState["BeginTime"].ToString());
					randomExamResult.CurrentDateTime = DateTime.Parse(ViewState["BeginTime"].ToString());
					randomExamResult.ExamTime = 0;
					randomExamResult.EndDateTime = DateTime.Parse(ViewState["BeginTime"].ToString());
					randomExamResult.Score = 0;
					randomExamResult.OrganizationId = int.Parse(ConfigurationManager.AppSettings["StationID"]);
					randomExamResult.Memo = "";
					randomExamResult.StatusId = 0;
					randomExamResult.AutoScore = 0;
					randomExamResult.CorrectRate = 0;
					randomExamResult.ExamineeId = int.Parse(str[m]);
					randomExamResult.ExamSeqNo = n;

					int nRandomExamResultPK = randomExamResultBLL.AddRandomExamResultCurrent(randomExamResult);
					ViewState["RandomExamResultPK"] = nRandomExamResultPK;

                    strSql = "select a.* from Random_Exam_Arrange_Detail a "
                          + " where ','||User_Ids||',' like '%,"+str[m]+",%' "
                          + " and Random_Exam_ID=" + strId;
				    DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

				    strSql = "insert into Random_Exam_Result_Detail(Random_Exam_Result_Detail_ID,"
				             + "Random_Exam_Result_ID,Random_Exam_ID,Employee_ID,Computer_Room_SEAT,Computer_Room_ID) "
				             + "values(Random_Exam_Result_Detail_SEQ.NextVal,"
				             + nRandomExamResultPK + ","
				             + randomExamResult.RandomExamId + ","
				             + randomExamResult.ExamineeId + ","
				             + "0,"+ dr["Computer_Room_ID"] +") ";
                    db.ExecuteNonQuery(strSql);

					RandomExamItemBLL randomItemBLL = new RandomExamItemBLL();
					RandomExamSubjectBLL subjectBLL = new RandomExamSubjectBLL();
					RandomExamStrategyBLL strategyBLL = new RandomExamStrategyBLL();

					IList<RandomExamSubject> randomExamSubjects =
						subjectBLL.GetRandomExamSubjectByRandomExamId(int.Parse(strId));

					if (randomExamSubjects != null)
					{
						Hashtable hashTableItemIds = new Hashtable();
						Hashtable htSubjectItemIds = new Hashtable();
						for (int i = 0; i < randomExamSubjects.Count; i++)
						{
							RandomExamSubject paperSubject = randomExamSubjects[i];
							int nSubjectId = paperSubject.RandomExamSubjectId;
							//  int nItemCount = paperSubject.ItemCount;

							IList<RandomExamStrategy> strategys = strategyBLL.GetRandomExamStrategys(nSubjectId);
							for (int j = 0; j < strategys.Count; j++)
							{
								int nStrategyId = strategys[j].RandomExamStrategyId;
								int nItemCount = strategys[j].ItemCount;
								IList<RandomExamItem> itemList = randomItemBLL.GetItemsByStrategyId(nStrategyId, year);

								// IList<RandomExamItem> itemList = randomItemBLL.GetItemsBySubjectId(nSubjectId);

								Random ObjRandom = new Random();
								Hashtable hashTable = new Hashtable();
								Hashtable hashTableCount = new Hashtable();
								int index = 0;
								while (hashTable.Count < nItemCount)
								{
									int k = ObjRandom.Next(itemList.Count);
									hashTableCount[index] = k;
									index = index + 1;
									int itemID = itemList[k].ItemId;
									int examItemID = itemList[k].RandomExamItemId;
									if (!hashTableItemIds.ContainsKey(itemID))
									{
										hashTable[examItemID] = examItemID;
										hashTableItemIds[itemID] = itemID;
										htSubjectItemIds[examItemID] = examItemID;
									}
									//if (hashTableCount.Count == itemList.Count && hashTable.Count < nItemCount)
									//{
									//    SessionSet.PageMessage = "随机考试在设定的取题范围内的试题量不够，请重新设置取题范围！";
									//    return;
									//}
								}
							}
						}
						randomExamResultAnswers = new List<RandomExamResultAnswerCurrent>();
						foreach (int key in htSubjectItemIds.Keys)
						{
							string strItemId = htSubjectItemIds[key].ToString();
							RandomExamResultAnswerCurrent randomExamResultAnswer = new RandomExamResultAnswerCurrent();
							randomExamResultAnswer.RandomExamResultId = nRandomExamResultPK;
							randomExamResultAnswer.RandomExamItemId = int.Parse(strItemId);
							randomExamResultAnswer.JudgeStatusId = 0;
							randomExamResultAnswer.JudgeRemark = string.Empty;
							randomExamResultAnswer.ExamTime = 0;
							randomExamResultAnswer.Answer = string.Empty;
							randomExamResultAnswers.Add(randomExamResultAnswer);

                            //完型填空子题
						    RandomExamItem examItem = randomItemBLL.GetRandomExamItem(int.Parse(strItemId), year);
                            IList<RandomExamItem> randomExamItems =randomItemBLL.GetItemsByParentItemID(examItem.ItemId,obj.RandomExamId,year);
						    foreach (RandomExamItem randomExamItem in randomExamItems)
						    {
                                randomExamResultAnswer = new RandomExamResultAnswerCurrent();
                                randomExamResultAnswer.RandomExamResultId = nRandomExamResultPK;
                                randomExamResultAnswer.RandomExamItemId = randomExamItem.RandomExamItemId;
                                randomExamResultAnswer.JudgeStatusId = 0;
                                randomExamResultAnswer.JudgeRemark = string.Empty;
                                randomExamResultAnswer.ExamTime = 0;
                                randomExamResultAnswer.Answer = string.Empty;
                                randomExamResultAnswers.Add(randomExamResultAnswer);
						    }
						}
					}
					else
					{
						SessionSet.PageMessage = "未找到记录！";
					}

					foreach (RandomExamResultAnswerCurrent answer in randomExamResultAnswers)
					{
						randomExamResultAnswersCurrentAll.Add(answer);
					}
				}
			}

			randomExamResultAnswerBLL.AddExamResultAnswerCurrent(randomExamResultAnswersCurrentAll);
            objBll.UpdateHasPaper(Convert.ToInt32(strId), PrjPub.ServerNo, true);
			//如果考试是随到随考，考试状态自动变为正在进行
			if (obj.StartMode == 1)
			{
                objBll.UpdateIsStart(Convert.ToInt32(strId), PrjPub.ServerNo, 1);
			}
            SystemLogBLL objLogBll = new SystemLogBLL();
            objLogBll.WriteLog("“" + obj.ExamName + "”生成所有考试试卷");
		}

		public void EndExam()
		{
            string strSql = string.Empty;

            ViewState["EndTime"] = DateTime.Now.ToString("yyyy-MM-dd");
			//记录当前考试所在地的OrgID
			ViewState["OrgID"] = ConfigurationManager.AppSettings["StationID"];
			string strId = Request.QueryString.Get("RandomExamID");
			//获取当前考试的生成试卷的状态和次数
			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(strId));

			RandomExamResultBLL objResultBll = new RandomExamResultBLL();
			RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
			IList<RandomExamResultCurrent> objResultCurrent =
				objResultCurrentBll.GetStartRandomExamResultInfo(Convert.ToInt32(strId));

			IList<RandomExamResultCurrent> objResultCurrentNew = new List<RandomExamResultCurrent>();

            try
            {
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
                    //将实时考试记录（临时表）转存到中间考试成绩表和答卷表
                    int randomExamResultID = objResultBll.RemoveResultAnswer(Convert.ToInt32(current.RandomExamResultId));

                    try
                    {
                        OracleAccess dbPhoto = new OracleAccess();
                        strSql = "select * from Random_Exam_Result_Detail where Random_Exam_Result_ID=" +
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
                        continue;
                    }
                }

                //结束正在进行的当前考试,并将考试成绩传至路局
                //objResultBll.RemoveResultAnswerAfterEnd(objResultCurrentNew, Convert.ToInt32(strId), PrjPub.IsServerCenter);

                objResultCurrentBll.RemoveRandomExamResultTemp(Convert.ToInt32(strId));
                objResultCurrentBll.DelRandomExamResultCurrent(Convert.ToInt32(strId));


                #region 结束考试时上传成绩 屏蔽
                /*
				if (!PrjPub.IsServerCenter)
				{
					#region 在站段，当考试为随到随考时需检测异地考试的情况
					//if (obj.StartMode == PrjPub.START_MODE_NO_CONTROL)
					//{
					//    IList<RandomExamResultCurrent> objResultCurrentNewCenter = new List<RandomExamResultCurrent>();
					//    if (!PrjPub.IsServerCenter)
					//    {
					//        IList<RandomExamResultCurrent> objResultCurrentCenter =
					//            objResultCurrentBll.GetCenterStartRandomExamResultInfo(Convert.ToInt32(strId));


					//        foreach (RandomExamResultCurrent current in objResultCurrentCenter)
					//        {
					//            current.CurrentDateTime = DateTime.Parse(ViewState["EndTime"].ToString());
					//            current.ExamTime = GetSecondBetweenTwoDate(DateTime.Parse(ViewState["EndTime"].ToString()),
					//                                    current.BeginDateTime);

					//            current.EndDateTime = DateTime.Parse(ViewState["EndTime"].ToString());
					//            current.Score = 0;
					//            current.Memo = "";
					//            //参加考试将当前考试的标志置为2－已经结束
					//            current.StatusId = 2;
					//            current.AutoScore = 0;
					//            current.CorrectRate = 0;
					//            objResultCurrentNewCenter.Add(current);
					//        }
					//    }
					//    objResultBll.RemoveResultAnswerAfterEndCenter(objResultCurrentNewCenter, Convert.ToInt32(strId));
					//}
					#endregion
					OrganizationBLL objOrgBll = new OrganizationBLL();
					if (objOrgBll.IsAutoUpload(Convert.ToInt32(ConfigurationManager.AppSettings["StationID"])))
					{
						#region  上传答卷
						//从路局获取本次考试在本站段参考所有考试成绩信息
						IList<RandomExamResult> objList =
							objResultBll.GetRandomExamResultInfoStation(Convert.ToInt32(strId),
							                                            Convert.ToInt32(ConfigurationManager.AppSettings["StationID"]));
						RandomExamResultAnswerBLL objResultAnswerBll = new RandomExamResultAnswerBLL();
						IList<RandomExamResultAnswerStation> objAnswerStationList = new List<RandomExamResultAnswerStation>();
						foreach (RandomExamResult result in objList)
						{
							//根据考试成绩信息获取答卷
							IList<RandomExamResultAnswer> objAnswerList =
								objResultAnswerBll.GetExamResultAnswers(result.RandomExamResultIDStation);

							foreach (RandomExamResultAnswer answer in objAnswerList)
							{
								RandomExamResultAnswerStation objStation = new RandomExamResultAnswerStation();
								objStation.RandomExamResultID = result.RandomExamResultId;
								objStation.RandomExamItemID = answer.RandomExamItemId;
								objStation.Answer = answer.Answer;
								objStation.ExamTime = answer.ExamTime;
								objStation.JudgeScore = answer.JudgeScore;
								objStation.JudgeStatusID = answer.JudgeStatusId;
								objStation.JudgeRemark = answer.JudgeRemark;
								objStation.RandomExamResultIDStation = answer.RandomExamResultId;

								objAnswerStationList.Add(objStation);
							}
						}
						//将答卷上传至路局
						RandomExamResultAnswerStationBLL objStationBll = new RandomExamResultAnswerStationBLL();
						objStationBll.AddExamResultAnswerStation(objAnswerStationList);
						#endregion
					}
                }
                 */
                #endregion

                if (obj.HasTrainClass)
				{
						try
						{
                            OracleAccess db = new OracleAccess(ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString);
							string strTrainClassID = "";
							string strTrainSubjectID = "";
							RandomExamTrainClassBLL trainClassBLL = new RandomExamTrainClassBLL();
							IList<RandomExamTrainClass> trainClasses =
								trainClassBLL.GetRandomExamTrainClassByRandomExamID(Convert.ToInt32(strId));
							foreach (RandomExamTrainClass trainClass in trainClasses)
							{
								if (strTrainClassID == "")
								{
									strTrainClassID = trainClass.TrainClassID.ToString();
								}
								else
								{
									strTrainClassID = strTrainClassID + "," + trainClass.TrainClassID;
								}

								if (strTrainSubjectID == "")
								{
									strTrainSubjectID = trainClass.TrainClassSubjectID.ToString();
								}
								else
								{
									strTrainSubjectID = strTrainSubjectID + "," + trainClass.TrainClassSubjectID;
								}
							}

                            //RandomExamArrangeBLL objArrangeBLL = new RandomExamArrangeBLL();
                            //RandomExamArrange objArrange = objArrangeBLL.GetRandomExamArranges(Convert.ToInt32(strId))[0];
                            //string strEmptyStudent = objArrange.UserIds;

                           strSql = "select * from Random_Exam_Arrange_Detail a "
                              + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
                              + " inner join Computer_Server c on c.Computer_server_ID=b.Computer_Server_ID"
                              + " where c.Computer_Server_No='"+PrjPub.ServerNo+"' "
                              + " and Random_Exam_ID=" + strId;
                            DataSet dsAll = db.RunSqlDataSet(strSql);

                            string strUserId = "";
                            foreach (DataRow dr in dsAll.Tables[0].Rows)
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
						    string strEmptyStudent = strUserId;

                            IList<RandomExamResult> examResults = objResultBll.GetRandomExamResults(Convert.ToInt32(strId), "", "", "", string.Empty, 0, 100, Convert.ToInt32(ConfigurationManager.AppSettings["StationID"]));
							foreach (RandomExamResult result in examResults)
							{
								strEmptyStudent = ("," + strEmptyStudent + ",").Replace(result.ExamineeId + ",", "");

							    string strEmployeeID = result.ExamineeId.ToString();

                                strSql = "select * "
                                         + " from ZJ_Train_Class_Subject_Result "
                                         + " where Train_Class_ID in (" + strTrainClassID + ") and Train_Class_Subject_ID in (" + strTrainSubjectID + ") "
                                         + " and Employee_ID=" + strEmployeeID + " order by Train_Class_Subject_Result_ID desc";

								DataSet ds = db.RunSqlDataSet(strSql);

								if(ds.Tables[0].Rows.Count > 0)
								{
									DataRow dr = ds.Tables[0].Rows[0];

									if (dr["Result"] == DBNull.Value)
									{
                                        strSql = "update ZJ_Train_Class_Subject_Result set  Result=" + result.Score + ", IsPass=" + (result.Score >= obj.PassScore ? 1 : 0) + ","
                                             + " Exam_Date=to_date('" + result.BeginDateTime.ToString("yyyy-MM-dd") + "','YYYY-MM-DD')"
                                             + " where Train_Class_ID=" + dr["Train_Class_ID"] + " and Train_Class_Subject_ID=" + dr["Train_Class_Subject_ID"]
                                             + " and Employee_ID=" + strEmployeeID;
										db.ExecuteNonQuery(strSql);
									}
									else
									{
                                        strSql = "insert into ZJ_Train_Class_Subject_Result values(Train_Class_Subject_Result_SEQ.nextval,"
                                                 + dr["Train_Class_ID"] + "," + dr["Train_Class_Subject_ID"] + "," + strEmployeeID + ",to_date('"
                                                 + result.BeginDateTime.ToString("yyyy-MM-dd") + "','YYYY-MM-DD')," + result.Score + ","
												 + (result.Score >= obj.PassScore ? 1 : 0) + ")";
										db.ExecuteNonQuery(strSql);
									}
								}
							}


							if (strEmptyStudent.TrimStart(',').TrimEnd(',') != string.Empty)
							{
								string[] strEmpty = strEmptyStudent.TrimStart(',').TrimEnd(',').Split(',');
								for (int i = 0; i < strEmpty.Length; i++)
								{
									//获取职教系统员工ID
                                    string strEmployeeID = strEmpty[i];

									strSql = "select * "
                                             + " from ZJ_Train_Class_Subject_Result "
                                             + " where Train_Class_ID in (" + strTrainClassID + ") and Train_Class_Subject_ID in (" + strTrainSubjectID +
									         ") "
                                             + " and Employee_ID=" + strEmployeeID + " order by Train_Class_Subject_Result_ID desc";

									DataSet ds = db.RunSqlDataSet(strSql);

									if (ds.Tables[0].Rows.Count > 0)
									{
										DataRow dr = ds.Tables[0].Rows[0];

										if (dr["Result"] == DBNull.Value)
										{
                                            strSql = "update ZJ_Train_Class_Subject_Result set  Result=0, IsPass=0,"
                                                     + " Exam_Date=sysdate"
                                                     + " where Train_Class_ID=" + dr["Train_Class_ID"] + " and Train_Class_Subject_ID=" +
											         dr["Train_Class_Subject_ID"]
                                                     + " and Employee_ID=" + strEmployeeID;
											db.ExecuteNonQuery(strSql);
										}
										else
										{
                                            strSql = "insert into ZJ_Train_Class_Subject_Result values(Train_Class_Subject_Result_SEQ.nextval,"
                                                     + dr["Train_Class_ID"] + "," + dr["Train_Class_Subject_ID"] + "," + strEmployeeID + ","
                                                     + "sysdate,0,0)";
											db.ExecuteNonQuery(strSql);
										}
									}
								}
							}

							//将其他没有成绩的学员登录为0分
							//strSql = "update TrainClassSubjectResult set Result=0 "
							//                 + " where Result is null and  TrainClassID=" + obj.TrainClassID + " and TrainClassSubjectID=" +
							//                 obj.TrainClassSubjectID;
                            //db.ExecuteNonQuery(strSql);
						}
						catch (Exception ex)
						{
							throw ex;
						}
					}

				

				RandomExamApplyBLL objApplyBll = new RandomExamApplyBLL();
				objApplyBll.DelRandomExamApplyByExamID(Convert.ToInt32(strId));
                objBll.UpdateIsStart(Convert.ToInt32(strId), PrjPub.ServerNo, 2);
                objBll.UpdateStatusID(Convert.ToInt32(strId), PrjPub.ServerNo, 2);

			    OracleAccess db1 = new OracleAccess();
                //考试结束将该服务器下当前考试的所有机房
                if(PrjPub.IsServerCenter)
                {
                    strSql = "update Computer_Room set Is_Use=0 "
                         + "where Computer_Room_ID in "
                         + "(select  a.Computer_Room_ID from Random_Exam_Arrange_Detail a "
                         + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
                         + " inner join Computer_Server c on c.Computer_server_ID=b.Computer_Server_ID"
                         + " where c.Computer_Server_No='" + PrjPub.ServerNo + "' "
                         + " and Random_Exam_ID=" + strId + ")";
                }
                else
                {
                    strSql = "update Computer_Room@link_sf set Is_Use=0 "
                         + "where Computer_Room_ID in "
                         + "(select a.Computer_Room_ID from Random_Exam_Arrange_Detail a "
                         + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
                         + " inner join Computer_Server c on c.Computer_server_ID=b.Computer_Server_ID"
                         + " where c.Computer_Server_No='" + PrjPub.ServerNo + "' "
                         + " and Random_Exam_ID=" + strId + ")";
                }

                db1.ExecuteNonQuery(strSql);

				SystemLogBLL objLogBll = new SystemLogBLL();
				objLogBll.WriteLog("“" + obj.ExamName + "”结束考试");
				load = "true";
			}
			catch
			{
				SessionSet.PageMessage = "结束考试失败！";
				load = "false";
			}
		}

        private void SavePhotoToServer(int examid, int employeeid, byte[] ph, int index, int serverId)
        {
            string file = Server.MapPath("/RailExamBao/Online/Photo/" + examid + "/");
            if (!Directory.Exists(file))
            {
                Directory.CreateDirectory(file);
            }

            string uploadPath = Server.MapPath("/RailExamBao/Online/Photo/" + examid + "/") + employeeid + "_" + serverId + "_";

            string fileName = string.Empty;
            fileName = uploadPath + "0" + index + ".jpg";
            System.Drawing.Image image = FromBytes(ph);

            if (image != null)
            {
                System.Drawing.Image thumbnail = image.GetThumbnailImage(170, 130, null, IntPtr.Zero);
                //保存本地
                thumbnail.Save(fileName, ImageFormat.Jpeg);
            }
        }

        public System.Drawing.Image FromBytes(byte[] bs)
        {
            if (bs == null) return null;
            try
            {
                MemoryStream ms = new MemoryStream(bs);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                ms.Close();
                return returnImage;

            }
            catch { return null; }
        }

		private void GetPaperAfter()
		{
			ViewState["BeginTime"] = DateTime.Now.ToString();
			string strId = Request.QueryString.Get("RandomExamID");

			//获取当前考试的生成试卷的状态和次数
			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(strId));

			int year = obj.BeginTime.Year;
			int ExamCount = obj.MaxExamTimes;

            if (!PrjPub.IsServerCenter)
            {
                RandomExamArrangeBLL objArrangeBll = new RandomExamArrangeBLL();
                objArrangeBll.RefreshRandomExamArrange();
            }

            //RandomExamArrangeBLL eaBll = new RandomExamArrangeBLL();
            //IList<RailExam.Model.RandomExamArrange> ExamArranges = eaBll.GetRandomExamArranges(int.Parse(strId));
            //string strChooseID = "";
            //if (ExamArranges.Count > 0)
            //{
            //    strChooseID = ExamArranges[0].UserIds;
            //}
            //else
            //{
            //    strChooseID = "";
            //}

            //RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
            //IList<RandomExamResultCurrent> examResults = objResultCurrentBll.GetRandomExamResultInfo(Convert.ToInt32(strId));
            //for (int i = 0; i < examResults.Count; i++)
            //{
            //    strChooseID = ("," + strChooseID + ",").Replace("," + examResults[i].ExamineeId + ",", ",");
            //}

            //strChooseID = strChooseID.TrimStart(',').TrimEnd(',');

		    string strChooseID = Request.QueryString.Get("addIds");

			if(strChooseID == string.Empty)
			{
				return;
			}

			string[] str = strChooseID.Split('|');

            OracleAccess db = new OracleAccess();
            string strSql;

			RandomExamResultAnswerCurrentBLL randomExamResultAnswerBLL = new RandomExamResultAnswerCurrentBLL();
			//定义全局答卷对象List
			IList<RandomExamResultAnswerCurrent> randomExamResultAnswersCurrentAll = new List<RandomExamResultAnswerCurrent>();
			//定义一个考生一次答卷对象List
			IList<RandomExamResultAnswerCurrent> randomExamResultAnswers = null;
			for (int n = 1; n <= ExamCount; n++)
			{
				for (int m = 0; m < str.Length; m++)
				{
					RandomExamResultCurrentBLL randomExamResultBLL = new RandomExamResultCurrentBLL();
					RandomExamResultCurrent randomExamResult = new RandomExamResultCurrent();

					randomExamResult.RandomExamId = int.Parse(strId);
					randomExamResult.AutoScore = 0;
					randomExamResult.BeginDateTime = DateTime.Parse(ViewState["BeginTime"].ToString());
					randomExamResult.CurrentDateTime = DateTime.Parse(ViewState["BeginTime"].ToString());
					randomExamResult.ExamTime = 0;
					randomExamResult.EndDateTime = DateTime.Parse(ViewState["BeginTime"].ToString());
					randomExamResult.Score = 0;
					randomExamResult.OrganizationId = int.Parse(ConfigurationManager.AppSettings["StationID"]);
					randomExamResult.Memo = "";
					randomExamResult.StatusId = 0;
					randomExamResult.AutoScore = 0;
					randomExamResult.CorrectRate = 0;
					randomExamResult.ExamineeId = int.Parse(str[m]);
					randomExamResult.ExamSeqNo = n;

					int nRandomExamResultPK = randomExamResultBLL.AddRandomExamResultCurrent(randomExamResult);
					ViewState["RandomExamResultPK"] = nRandomExamResultPK;

                    strSql = "select a.* from Random_Exam_Arrange_Detail a "
                          + " where ','||User_Ids||',' like '%," + str[m] + ",%' "
                          + " and Random_Exam_ID=" + strId;
                    DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

                    strSql = "insert into Random_Exam_Result_Detail(Random_Exam_Result_Detail_ID,"
                             + "Random_Exam_Result_ID,Random_Exam_ID,Employee_ID,Computer_Room_SEAT,Computer_Room_ID) "
                             + "values(Random_Exam_Result_Detail_SEQ.NextVal,"
                             + nRandomExamResultPK + ","
                             + randomExamResult.RandomExamId + ","
                             + randomExamResult.ExamineeId + ","
                             + "0,"+ dr["Computer_Room_ID"] +") ";
                    db.ExecuteNonQuery(strSql);

					RandomExamItemBLL randomItemBLL = new RandomExamItemBLL();
					RandomExamSubjectBLL subjectBLL = new RandomExamSubjectBLL();
					RandomExamStrategyBLL strategyBLL = new RandomExamStrategyBLL();

					IList<RandomExamSubject> randomExamSubjects =
						subjectBLL.GetRandomExamSubjectByRandomExamId(int.Parse(strId));

					if (randomExamSubjects != null)
					{
						Hashtable hashTableItemIds = new Hashtable();
						Hashtable htSubjectItemIds = new Hashtable();
						for (int i = 0; i < randomExamSubjects.Count; i++)
						{
							RandomExamSubject paperSubject = randomExamSubjects[i];
							int nSubjectId = paperSubject.RandomExamSubjectId;
							//  int nItemCount = paperSubject.ItemCount;

							IList<RandomExamStrategy> strategys = strategyBLL.GetRandomExamStrategys(nSubjectId);
							for (int j = 0; j < strategys.Count; j++)
							{
								int nStrategyId = strategys[j].RandomExamStrategyId;
								int nItemCount = strategys[j].ItemCount;
								IList<RandomExamItem> itemList = randomItemBLL.GetItemsByStrategyId(nStrategyId, year);

								// IList<RandomExamItem> itemList = randomItemBLL.GetItemsBySubjectId(nSubjectId);

								Random ObjRandom = new Random();
								Hashtable hashTable = new Hashtable();
								Hashtable hashTableCount = new Hashtable();
								int index = 0;
								while (hashTable.Count < nItemCount)
								{
									int k = ObjRandom.Next(itemList.Count);
									hashTableCount[index] = k;
									index = index + 1;
									int itemID = itemList[k].ItemId;
									int examItemID = itemList[k].RandomExamItemId;
									if (!hashTableItemIds.ContainsKey(itemID))
									{
										hashTable[examItemID] = examItemID;
										hashTableItemIds[itemID] = itemID;
										htSubjectItemIds[examItemID] = examItemID;
									}
									//if (hashTableCount.Count == itemList.Count && hashTable.Count < nItemCount)
									//{
									//    SessionSet.PageMessage = "随机考试在设定的取题范围内的试题量不够，请重新设置取题范围！";
									//    return;
									//}
								}
							}
						}
						randomExamResultAnswers = new List<RandomExamResultAnswerCurrent>();
						foreach (int key in htSubjectItemIds.Keys)
						{
							string strItemId = htSubjectItemIds[key].ToString();
							RandomExamResultAnswerCurrent randomExamResultAnswer = new RandomExamResultAnswerCurrent();
							randomExamResultAnswer.RandomExamResultId = nRandomExamResultPK;
							randomExamResultAnswer.RandomExamItemId = int.Parse(strItemId);
							randomExamResultAnswer.JudgeStatusId = 0;
							randomExamResultAnswer.JudgeRemark = string.Empty;
							randomExamResultAnswer.ExamTime = 0;
							randomExamResultAnswer.Answer = string.Empty;
							randomExamResultAnswers.Add(randomExamResultAnswer);

                            //完型填空子题
                            RandomExamItem examItem = randomItemBLL.GetRandomExamItem(int.Parse(strItemId), year);
                            IList<RandomExamItem> randomExamItems = randomItemBLL.GetItemsByParentItemID(examItem.ItemId, obj.RandomExamId, year); 
                            foreach (RandomExamItem randomExamItem in randomExamItems)
                            {
                                randomExamResultAnswer = new RandomExamResultAnswerCurrent();
                                randomExamResultAnswer.RandomExamResultId = nRandomExamResultPK;
                                randomExamResultAnswer.RandomExamItemId = randomExamItem.RandomExamItemId;
                                randomExamResultAnswer.JudgeStatusId = 0;
                                randomExamResultAnswer.JudgeRemark = string.Empty;
                                randomExamResultAnswer.ExamTime = 0;
                                randomExamResultAnswer.Answer = string.Empty;
                                randomExamResultAnswers.Add(randomExamResultAnswer);
                            }
						}
					}
					else
					{
						SessionSet.PageMessage = "未找到记录！";
					}

					foreach (RandomExamResultAnswerCurrent answer in randomExamResultAnswers)
					{
						randomExamResultAnswersCurrentAll.Add(answer);
					}
				}
			}

			randomExamResultAnswerBLL.AddExamResultAnswerCurrent(randomExamResultAnswersCurrentAll);
            objBll.UpdateHasPaper(Convert.ToInt32(strId), PrjPub.ServerNo, true);
			//如果考试是随到随考，考试状态自动变为正在进行
			if (obj.StartMode == 1)
			{
                objBll.UpdateIsStart(Convert.ToInt32(strId), PrjPub.ServerNo, 1);
			}
            SystemLogBLL objLogBll = new SystemLogBLL();
            objLogBll.WriteLog("“" + obj.ExamName + "”生成新增考生试卷");
		}

		private int GetSecondBetweenTwoDate(DateTime dt1, DateTime dt2)
		{
			int i1 = dt1.Hour * 3600 + dt1.Minute * 60 + dt1.Second;
			int i2 = dt2.Hour * 3600 + dt2.Minute * 60 + dt2.Second;

			return i1 - i2;
		}


        private void CheckExam()
        {
            try
            {


                ExamBLL examBll = new ExamBLL();

                int categoryId = Request.QueryString.Get("typeId") == ""
                                     ? -1
                                     : Convert.ToInt32(Request.QueryString.Get("typeId"));
                DateTime beginDate = string.IsNullOrEmpty(Request.QueryString.Get("beginDate"))
                                         ? Convert.ToDateTime("0001-01-01")
                                         : Convert.ToDateTime(Request.QueryString.Get("beginDate"));
                DateTime endDate = string.IsNullOrEmpty(Request.QueryString.Get("endDate"))
                                       ? Convert.ToDateTime("0001-01-01")
                                       : Convert.ToDateTime(Request.QueryString.Get("endDate"));
                int orgId = Convert.ToInt32(Request.QueryString.Get("Orgid"));
                IList<RailExam.Model.Exam> objList = examBll.GetExamsInfoByOrgID(Request.QueryString.Get("name"),
                                                                                 categoryId, beginDate, endDate, orgId,
                                                                                 "true");

                OracleAccess db = new OracleAccess();

                string strdel = "delete from Random_Exam_Check where Org_ID=" + Request.QueryString.Get("OrgID");
                db.ExecuteNonQuery(strdel);

                foreach (RailExam.Model.Exam exam in objList)
                {

                    IList<RandomExamResult> examResults = null;
                    RandomExamResultBLL bllExamResult = new RandomExamResultBLL();
                    examResults = bllExamResult.GetRandomExamResults(exam.ExamId, "", "", "", "", 0, 1000,
                                                                     Convert.ToInt32(Request.QueryString.Get("OrgID")));
                    string strChooseID = string.Empty;
                    foreach (RandomExamResult randomExamResult in examResults)
                    {
                        if (string.IsNullOrEmpty(strChooseID))
                        {
                            strChooseID += randomExamResult.RandomExamResultId;
                        }
                        else
                        {
                            strChooseID += "," + randomExamResult.RandomExamResultId;
                        }
                    }

                    int beginYear = exam.BeginTime.Year;
                    string strSql =
                        @"select distinct a.Random_Exam_Result_ID,d.Employee_Name,d.Employee_ID from Random_Exam_Result_Answer a
                     inner join Random_Exam_Item_" +
                        beginYear +
                        @" b on a.Random_Exam_Item_ID=b.Random_Exam_Item_ID
                    inner join Random_Exam_Result c on a.Random_Exam_Result_ID=c.Random_Exam_Result_ID
                     inner join Employee d on d.Employee_ID=c.Examinee_ID
                        where c.Random_Exam_ID=" +
                        exam.ExamId;

                    if(PrjPub.IsServerCenter)
                    {
                        strSql += @"
                            union all 
                            select  distinct a.Random_Exam_Result_ID,d.Employee_Name,d.Employee_ID from Random_Exam_Result_Answer_" + beginYear + @" a
                         inner join Random_Exam_Item_" + beginYear + @" b on a.Random_Exam_Item_ID=b.Random_Exam_Item_ID
                        inner join Random_Exam_Result c on a.Random_Exam_Result_ID=c.Random_Exam_Result_ID
                         inner join Employee d on d.Employee_ID=c.Examinee_ID
                            where c.Random_Exam_ID=" + exam.ExamId;
                    }
                    else
                    {
                        strSql += @"
                            union all 
                            select  distinct a.Random_Exam_Result_ID,d.Employee_Name,d.Employee_ID from Random_Exam_Result_Answer_Temp a
                         inner join Random_Exam_Item_" + beginYear + @" b on a.Random_Exam_Item_ID=b.Random_Exam_Item_ID
                        inner join Random_Exam_Result_Temp c on a.Random_Exam_Result_ID=c.Random_Exam_Result_ID
                         inner join Employee d on d.Employee_ID=c.Examinee_ID
                            where c.Random_Exam_ID=" + exam.ExamId;
                    }

                    DataSet dsAnswer = db.RunSqlDataSet(strSql);


                    if(dsAnswer.Tables[0].Rows.Count== 0)
                    {
                        if (!string.IsNullOrEmpty(strChooseID))
                        {
                            string[] str = strChooseID.Split(',');

                            for (int i = 0; i < str.Length; i++)
                            {
                                strSql = "insert into Random_Exam_Check values(Random_Exam_Check_Seq.nextval," +
                                         Request.QueryString.Get("OrgID") + "," + str[i] + ")";
                                db.ExecuteNonQuery(strSql);
                            }
                        }
                    }


                }

                load = "true";
            }
            catch
            {
                SessionSet.PageMessage = "检查考试完整性失败！";
                load = "false";
            }
        }
	}
}
