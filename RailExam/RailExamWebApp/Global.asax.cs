using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using System.Web.SessionState;
using DSunSoft.Web.Global;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp
{
	public class Global : System.Web.HttpApplication
	{

		protected void Application_Start(object sender, EventArgs e)
		{
			Application["IsServerCenter"] = PrjPub.CheckServerCenter();

			/*
			string strNowOrg = ConfigurationManager.AppSettings["StationID"];
			OrganizationBLL objOrgBll = new OrganizationBLL();
			if (objOrgBll.IsAutoUpload(Convert.ToInt32(strNowOrg)))
			{
				DateTime dtNow = DateTime.Now;
				//记录当前考试所在地的OrgID
				//获取当前考试的生成试卷的状态和次数

				RandomExamBLL objBll = new RandomExamBLL();
				IList<RailExam.Model.RandomExam> objExamList = objBll.GetOverdueNotEndRandomExam(Convert.ToInt32(strNowOrg));
				foreach (RailExam.Model.RandomExam exam in objExamList)
				{
					RandomExamResultBLL objResultBll = new RandomExamResultBLL();
					RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
					IList<RandomExamResultCurrent> objResultCurrent =
						objResultCurrentBll.GetStartRandomExamResultInfo(exam.RandomExamId);
					IList<RandomExamResultCurrent> objResultCurrentNew = new List<RandomExamResultCurrent>();
					foreach (RandomExamResultCurrent current in objResultCurrent)
					{
						current.CurrentDateTime = dtNow;
						current.ExamTime = GetSecondBetweenTwoDate(dtNow, current.BeginDateTime);
						current.EndDateTime = dtNow;
						current.Score = 0;
						current.OrganizationId = int.Parse(strNowOrg);
						current.Memo = "";
						//参加考试将当前考试的标志置为2－已经结束
						current.StatusId = 2;
						current.AutoScore = 0;
						current.CorrectRate = 0;
						objResultCurrentNew.Add(current);
					}
					objResultBll.RemoveResultAnswerAfterEnd(objResultCurrentNew, exam.RandomExamId, PrjPub.IsServerCenter);

					if (!PrjPub.IsServerCenter)
					{
						//在站段，当考试为随到随考时需检测异地考试的情况
						//if (exam.StartMode == PrjPub.START_MODE_NO_CONTROL)
						//{
						//    IList<RandomExamResultCurrent> objResultCurrentNewCenter = new List<RandomExamResultCurrent>();
						//    IList<RandomExamResultCurrent> objResultCurrentCenter =
						//        objResultCurrentBll.GetCenterStartRandomExamResultInfo(exam.RandomExamId);

						//    foreach (RandomExamResultCurrent current in objResultCurrentCenter)
						//    {
						//        current.CurrentDateTime = dtNow;
						//        current.ExamTime = GetSecondBetweenTwoDate(dtNow, current.BeginDateTime);
						//        current.EndDateTime = dtNow;
						//        current.Score = 0;
						//        current.Memo = "";
						//        //参加考试将当前考试的标志置为2－已经结束
						//        current.StatusId = 2;
						//        current.AutoScore = 0;
						//        current.CorrectRate = 0;
						//        objResultCurrentNewCenter.Add(current);
						//    }
						//    objResultBll.RemoveResultAnswerAfterEndCenter(objResultCurrentNewCenter, exam.RandomExamId);
						//}

						IList<RandomExamResult> objList =
							objResultBll.GetRandomExamResultInfoStation(exam.RandomExamId,
																		Convert.ToInt32(ConfigurationManager.AppSettings["StationID"]));
						RandomExamResultAnswerBLL objResultAnswerBll = new RandomExamResultAnswerBLL();
						IList<RandomExamResultAnswerStation> objAnswerStationList = new List<RandomExamResultAnswerStation>();
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
					}
					objBll.UpdateIsStart(exam.RandomExamId, 2);
				}
			}*/
		}

		protected void Application_End(object sender, EventArgs e)
		{

		}

		protected  void Session_End(object sender,EventArgs e)
		{
			if (PrjPub.CurrentStudent != null)
			{
				string strCacheKey = PrjPub.CurrentStudent.EmployeeID.ToString();
				string strUser = Convert.ToString(HttpContext.Current.Cache[strCacheKey]);
				if (strUser != string.Empty)
				{
					HttpContext.Current.Cache.Remove(strCacheKey);
					SystemUserLoginBLL objloginBll = new SystemUserLoginBLL();
					objloginBll.DeleteSystemUserLogin(Convert.ToInt32(strCacheKey));
				}
			}

			if(Session["IPAddress"] != null)
			{
				string strIP = Session["IPAddress"].ToString();
				RandomExamApplyBLL objBll = new RandomExamApplyBLL();
				objBll.DelRandomExamApplyByIPAddress(strIP);
			}
		}
		
		private int GetSecondBetweenTwoDate(DateTime dt1, DateTime dt2)
		{
			int i1 = dt1.Hour * 3600 + dt1.Minute * 60 + dt1.Second;
			int i2 = dt2.Hour * 3600 + dt2.Minute * 60 + dt2.Second;

			return i1 - i2;
		}
	}
}