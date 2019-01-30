using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class RandomExamResultBLL
    {
        private static readonly RandomExamResultDAL dal = new RandomExamResultDAL();

		/// <summary>
		/// 根据考试ID查找正在进行或结束的考试成绩信息
		/// </summary>
		/// <param name="examID"></param>
		/// <returns></returns>
        public IList<RandomExamResult> GetRandomExamResultByExamID(int examID)
        {
            return dal.GetRandomExamResultByExamID(examID);
        }

		/// <summary>
		/// 根据考试ID查询所有考试成绩信息
		/// </summary>
		/// <param name="examID"></param>
		/// <returns></returns>
        public IList<RandomExamResult> GetRandomExamResultInfo(int examID)
        {
            return dal.GetRandomExamResultInfo(examID);
        }

		/// <summary>
		/// 根据考试ID和站段ID查询路局中该站段本次考试所有考试成绩信息
		/// </summary>
		/// <param name="examID"></param>
		/// <returns></returns>
		public IList<RandomExamResult> GetRandomExamResultInfoStation(int examID,int orgID)
		{
			return dal.GetRandomExamResultInfoStation(examID,orgID);
		}

		/// <summary>
		/// 该考生本次考试已参加的次数（本地）
		/// </summary>
		/// <param name="ExamineeID"></param>
		/// <param name="examID"></param>
		/// <returns></returns>
        public IList<RandomExamResult> GetRandomExamResultByExamineeID(int ExamineeID, int examID)
        {
            return dal.GetRandomExamResultByExamineeID(ExamineeID, examID);
        }

		/// <summary>
		///  该考生本次考试已参加的次数（路局）
		/// </summary>
		/// <param name="ExamineeID"></param>
		/// <param name="examID"></param>
		/// <returns></returns>
		public IList<RandomExamResult> GetRandomExamResultByExamineeIDFromServer(int ExamineeID, int examID)
		{
			return dal.GetRandomExamResultByExamineeIDFromServer(ExamineeID, examID);
		}


    	public RandomExamResult GetRandomExamResultByOrgID(int examResultID, int orgID)
        {
            return dal.GetRandomExamResultByOrgID(examResultID, orgID);
        }

        public RandomExamResult GetRandomExamResult(int examResultID)
        {
            return dal.GetRandomExamResult(examResultID);
        }

        public RandomExamResult GetRandomExamResultTemp(int examResultID)
        {
            return dal.GetRandomExamResultTemp(examResultID);
        }

		public RandomExamResult GetRandomExamResultStation(int examResultID)
		{
			return dal.GetRandomExamResultStation(examResultID);
		}

		/// <summary>
		/// 成绩查询（本地）
		/// </summary>
		/// <param name="ExamId"></param>
		/// <param name="OrganizationName"></param>
		/// <param name="strExamineeName"></param>
		/// <param name="dScoreLower"></param>
		/// <param name="dScoreUpper"></param>
		/// <param name="orgID"></param>
		/// <returns></returns>
        public IList<RandomExamResult> GetRandomExamResults(int ExamId, string OrganizationName,string workshopName, string strExamineeName, string strWorkNo,decimal dScoreLower,
                         decimal dScoreUpper, int orgID)
        {
			OrganizationBLL objBll = new OrganizationBLL();
             IList<RandomExamResult> objList = dal.GetRandomExamResults(ExamId, OrganizationName,workshopName, strExamineeName, strWorkNo,dScoreLower, dScoreUpper, orgID);
            //foreach (RandomExamResult result in objList)
            //{
            //    result.ExamOrgName = objBll.GetOrganization(result.OrganizationId).ShortName+"-"+result.ExamOrgName;
            //}
        	return objList;
        }

		/// <summary>
		/// 在站段连路局成绩查询记录
		/// </summary>
		/// <param name="ExamId"></param>
		/// <param name="OrganizationName"></param>
		/// <param name="strExamineeName"></param>
		/// <param name="dScoreLower"></param>
		/// <param name="dScoreUpper"></param>
		/// <param name="orgID"></param>
		/// <returns></returns>
		public IList<RandomExamResult> GetRandomExamResultsFromServer(int ExamId, string OrganizationName, string strExamineeName, decimal dScoreLower,
						decimal dScoreUpper, int orgID)
		{
			OrganizationBLL objBll = new OrganizationBLL();
			IList<RandomExamResult> objList = dal.GetRandomExamResultsFromServer(ExamId, OrganizationName, strExamineeName, dScoreLower, dScoreUpper, orgID);
			foreach (RandomExamResult result in objList)
			{
				result.ExamOrgName = objBll.GetOrganization(result.OrganizationId).ShortName;
			}
			return objList;
		}

        public int AddRandomExamResult(RandomExamResult examResult)
        {
            return dal.AddRandomExamResult(examResult);
        }

        public int AddRandomExamResultToServer(RandomExamResult examResult)
        {
            return dal.AddRandomExamResultToServer(examResult);
        }


        public int UpdateRandomExamResult(RandomExamResult examResult)
        {
            return dal.UpdateRandomExamResult(examResult);
        }

		public int UpdateRandomExamResultOther(RandomExamResult examResult)
		{
			return dal.UpdateRandomExamResultOther(examResult);
		}

    	public int UpdateRandomExamResultToServer(RandomExamResult examResult)
        {
            return dal.UpdateRandomExamResultToServer(examResult);
        }

		/// <summary>
		/// 将考试成绩和答卷从临时表中转存到正式成绩表和答卷表
		/// </summary>
		/// <param name="randomResultExamID"></param>
		/// <returns></returns>
        public int RemoveResultAnswer(int randomResultExamID)
        {
            return dal.RemoveResultAnswer(randomResultExamID);
        }

        /// <summary>
        /// 将考试成绩和答卷从临时表中转存到中间成绩表和答卷表
        /// </summary>
        /// <param name="randomResultExamID"></param>
        /// <returns></returns>
        public int RemoveResultAnswerCurrent(int randomResultExamID)
        {
            return dal.RemoveResultAnswerCurrent(randomResultExamID);
        }


        /// <summary>
        /// 将中间考试成绩和答卷从临时表中转存到正式成绩表和答卷表
        /// </summary>
        /// <param name="randomResultExamID"></param>
        /// <returns></returns>
        public void RemoveResultAnswerTemp(int randomResultExamID)
        {
             dal.RemoveResultAnswerTemp(randomResultExamID);
        }

		/// <summary>
		/// 结束本地正在进行的考试
		/// </summary>
		/// <param name="objResultCurrent"></param>
		/// <param name="randomExamID"></param>
		/// <param name="isServerCenter"></param>
        public void RemoveResultAnswerAfterEnd(IList<RandomExamResultCurrent> objResultCurrent, int randomExamID,bool isServerCenter)
        {
            dal.RemoveResultAnswerAfterEnd(objResultCurrent,randomExamID,isServerCenter);
        }

		/// <summary>
		/// 结束路局正在进行的考试
		/// </summary>
		/// <param name="objResultCurrent"></param>
		/// <param name="randomExamID"></param>
		public void RemoveResultAnswerAfterEndCenter(IList<RandomExamResultCurrent> objResultCurrent, int randomExamID)
		{
			dal.RemoveResultAnswerAfterEndCenter(objResultCurrent, randomExamID);
		}

		/// <summary>
		/// 上传考试成绩
		/// </summary>
		/// <param name="randomExamID"></param>
		/// <param name="orgID"></param>
        public void RemoveRandomExamResultToServer(int randomExamID, int orgID)
        {
            dal.RemoveRandomExamResultToServer(randomExamID,orgID);
        }

        public void RemoveRandomExamResultToServerAnswer(int randomExamID, int orgID)
        {
            dal.RemoveRandomExamResultToServerAnswer(randomExamID, orgID);
        }

		/// <summary>
		/// 根据考生ID取得该考生刚刚结束的考试
		/// </summary>
		/// <param name="ExamineeID"></param>
		/// <param name="examID"></param>
		/// <returns></returns>
        public RandomExamResult GetNewRandomExamResultByExamineeID(int ExamineeID, int examID)
        {
            return dal.GetNewRandomExamResultByExamineeID(ExamineeID, examID);
        }

		public void DeleteRandomExamResult(int resultID)
		{
			dal.DeleteRandomExamResult(resultID);	
		}

		public IList<RandomExamResult> GetRandomExamResults(string strWhere)
		{
			return dal.GetRandomExamResults(strWhere);
		}

        public void DeleteRandomExamResultServer(int randomExamID)
        {
            dal.DeleteRandomExamResultServer(randomExamID);
        }

        public void InsertRandomExamResultServer(int randomExamResultId, int serverId,int examid)
        {
            dal.InsertRandomExamResultServer(randomExamResultId,serverId,examid);
        }

        public void InsertRandomExamResultAnswerServer(int examid,int randomExamResultId, int serverId)
        {
            dal.InsertRandomExamResultAnswerServer(examid,randomExamResultId, serverId);
        }
    }
}
