using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class RandomExamResultCurrentBLL
    {
        private static readonly RandomExamResultCurrentDAL dal = new RandomExamResultCurrentDAL();

        public IList<RandomExamResultCurrent> GetRandomExamResultInfo(int examID)
        {
			IList<RandomExamResultCurrent> objList = dal.GetRandomExamResultInfo(examID);
        	IList<RandomExamResultCurrent> objNewList = new List<RandomExamResultCurrent>();
        	foreach (RandomExamResultCurrent current in objList)
        	{
        		if(current.OrganizationId.ToString() == ConfigurationManager.AppSettings["StationID"].ToString())
        		{
        			objNewList.Add(current);
        		}
        	}

        	return objNewList;
        }

        public IList<RandomExamResultCurrent> GetRandomExamResultInfoByExamID(int examID, string sql)
        {
            IList<RandomExamResultCurrent> objList = dal.GetRandomExamResultInfoByExamID(examID, sql);
            IList<RandomExamResultCurrent> objNewList = new List<RandomExamResultCurrent>();
            foreach (RandomExamResultCurrent current in objList)
            {
                if (current.OrganizationId.ToString() == ConfigurationManager.AppSettings["StationID"].ToString())
                {
                    objNewList.Add(current);
                }
            }

            return objNewList;
        }

        public int AddRandomExamResultCurrent(RandomExamResultCurrent examResult)
        {
            return dal.AddRandomExamResultCurrent(examResult);
        }

        public int UpdateRandomExamResultCurrent(RandomExamResultCurrent examResult)
        {
            return dal.UpdateRandomExamResultCurrent(examResult);
        }

		public void UpdateRandomExamResultCurrent(IList<RandomExamResultCurrent> objList)
		{
			dal.UpdateRandomExamResultCurrent(objList);
		}

		public void UpdateRandomExamResultCurrentCenter(IList<RandomExamResultCurrent> objList)
		{
			dal.UpdateRandomExamResultCurrentCenter(objList);
		}

        /// <summary>
        /// 获取当前考生最新需要做的试卷
        /// </summary>
        /// <param name="employeeid"></param>
        /// <param name="examid"></param>
        /// <returns></returns>
        public RandomExamResultCurrent GetNowRandomExamResultInfo(int employeeid, int examid)
        {
            return dal.GetNowRandomExamResultInfo(employeeid, examid);
        }

        /// <summary>
		/// 查找本地正在进行的考试
        /// </summary>
        /// <param name="examID"></param>
        /// <returns></returns>
        public IList<RandomExamResultCurrent> GetStartRandomExamResultInfo(int examID)
        {
            return dal.GetStartRandomExamResultInfo(examID);
        }

		/// <summary>
		/// 查找路局正在进行的考试
		/// </summary>
		/// <param name="examID"></param>
		/// <returns></returns>
		public IList<RandomExamResultCurrent> GetCenterStartRandomExamResultInfo(int examID)
		{
			return dal.GetCenterStartRandomExamResultInfo(examID);
		}

        public RandomExamResultCurrent GetRandomExamResult(int examResultID)
        {
            return dal.GetRandomExamResult(examResultID);
        }

        public void DelRandomExamResultCurrent(int randomExamID)
        {
            dal.DelRandomExamResultCurrent(randomExamID);
       }

       /// <summary>
       /// 先将中间表已完成考试移到正式表中
       /// </summary>
       /// <param name="randomExamID"></param>
       public void RemoveRandomExamResultTemp(int randomExamID)
       {
           dal.RemoveRandomExamResultTemp(randomExamID);
       }

        public void DelRandomExamResultCurrentByResultID(int employeeID,int randomExamID)
        {
            dal.DelRandomExamResultCurrentByResultID(employeeID,randomExamID);
        }

        public void ClearRandomExamResultCurrentByResultID(int employeeID, int randomExamID)
        {
            dal.ClearRandomExamResultCurrentByResultID(employeeID, randomExamID);
        }

        public void ReplyRandomExamResultCurrentByResultID(int employeeID, int randomExamID)
        {
            dal.ReplyRandomExamResultCurrentByResultID(employeeID, randomExamID);
        }
    }
}
