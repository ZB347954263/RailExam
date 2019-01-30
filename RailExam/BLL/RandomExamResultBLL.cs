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
		/// ���ݿ���ID�������ڽ��л�����Ŀ��Գɼ���Ϣ
		/// </summary>
		/// <param name="examID"></param>
		/// <returns></returns>
        public IList<RandomExamResult> GetRandomExamResultByExamID(int examID)
        {
            return dal.GetRandomExamResultByExamID(examID);
        }

		/// <summary>
		/// ���ݿ���ID��ѯ���п��Գɼ���Ϣ
		/// </summary>
		/// <param name="examID"></param>
		/// <returns></returns>
        public IList<RandomExamResult> GetRandomExamResultInfo(int examID)
        {
            return dal.GetRandomExamResultInfo(examID);
        }

		/// <summary>
		/// ���ݿ���ID��վ��ID��ѯ·���и�վ�α��ο������п��Գɼ���Ϣ
		/// </summary>
		/// <param name="examID"></param>
		/// <returns></returns>
		public IList<RandomExamResult> GetRandomExamResultInfoStation(int examID,int orgID)
		{
			return dal.GetRandomExamResultInfoStation(examID,orgID);
		}

		/// <summary>
		/// �ÿ������ο����ѲμӵĴ��������أ�
		/// </summary>
		/// <param name="ExamineeID"></param>
		/// <param name="examID"></param>
		/// <returns></returns>
        public IList<RandomExamResult> GetRandomExamResultByExamineeID(int ExamineeID, int examID)
        {
            return dal.GetRandomExamResultByExamineeID(ExamineeID, examID);
        }

		/// <summary>
		///  �ÿ������ο����ѲμӵĴ�����·�֣�
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
		/// �ɼ���ѯ�����أ�
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
		/// ��վ����·�ֳɼ���ѯ��¼
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
		/// �����Գɼ��ʹ�����ʱ����ת�浽��ʽ�ɼ���ʹ���
		/// </summary>
		/// <param name="randomResultExamID"></param>
		/// <returns></returns>
        public int RemoveResultAnswer(int randomResultExamID)
        {
            return dal.RemoveResultAnswer(randomResultExamID);
        }

        /// <summary>
        /// �����Գɼ��ʹ�����ʱ����ת�浽�м�ɼ���ʹ���
        /// </summary>
        /// <param name="randomResultExamID"></param>
        /// <returns></returns>
        public int RemoveResultAnswerCurrent(int randomResultExamID)
        {
            return dal.RemoveResultAnswerCurrent(randomResultExamID);
        }


        /// <summary>
        /// ���м俼�Գɼ��ʹ�����ʱ����ת�浽��ʽ�ɼ���ʹ���
        /// </summary>
        /// <param name="randomResultExamID"></param>
        /// <returns></returns>
        public void RemoveResultAnswerTemp(int randomResultExamID)
        {
             dal.RemoveResultAnswerTemp(randomResultExamID);
        }

		/// <summary>
		/// �����������ڽ��еĿ���
		/// </summary>
		/// <param name="objResultCurrent"></param>
		/// <param name="randomExamID"></param>
		/// <param name="isServerCenter"></param>
        public void RemoveResultAnswerAfterEnd(IList<RandomExamResultCurrent> objResultCurrent, int randomExamID,bool isServerCenter)
        {
            dal.RemoveResultAnswerAfterEnd(objResultCurrent,randomExamID,isServerCenter);
        }

		/// <summary>
		/// ����·�����ڽ��еĿ���
		/// </summary>
		/// <param name="objResultCurrent"></param>
		/// <param name="randomExamID"></param>
		public void RemoveResultAnswerAfterEndCenter(IList<RandomExamResultCurrent> objResultCurrent, int randomExamID)
		{
			dal.RemoveResultAnswerAfterEndCenter(objResultCurrent, randomExamID);
		}

		/// <summary>
		/// �ϴ����Գɼ�
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
		/// ���ݿ���IDȡ�øÿ����ոս����Ŀ���
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
