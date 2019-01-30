using System;
using System.Collections.Generic;
using System.Text;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
	public class RandomExamApplyBLL
	{
		private static readonly RandomExamApplyDAL dal = new RandomExamApplyDAL();

		public int AddRandomExamApply(RandomExamApply exam)
		{
			int id = dal.AddRandomExamApply(exam);
			return id;
		}

		public void UpdateRandomExamApply(RandomExamApply exam)
		{
			dal.UpdateRandomExamApply(exam);
		}

		public RandomExamApply GetRandomExamApply(int applyID)
		{
			return dal.GetRandomExamApply(applyID);
		}

		public void DelRandomExamApply(int applyID)
		{
			dal.DelRandomExamApply(applyID);
		}

		public void DelRandomExamApplyByExamID(int examID)
		{
			dal.DelRandomExamApplyByExamID(examID);
		}

		public IList<RandomExamApply> GetRandomExamApplyByExamID(int examID)
		{
			return dal.GetRandomExamApplyByExamID(examID);
		}

		public IList<RandomExamApply> GetRandomExamApplyByOrgID(int orgID,string serverNo)
		{
			return dal.GetRandomExamApplyByOrgID(orgID, serverNo);
		}

		public void UpdateRandomExamApplyStatus(int applyID,int applyStatus)
		{
			dal.UpdateRandomExamApplyStatus(applyID,applyStatus);
		}

		public RandomExamApply GetRandomExamApplyByExamResultCurID(int examresultID)
		{
			return dal.GetRandomExamApplyByExamResultCurID(examresultID);
		}

		public IList<RandomExamApply> GetRandomExamApplyByIPAddress(string strIP)
		{
			return dal.GetRandomExamApplyByIPAddress(strIP);
		}

		public void DelRandomExamApplyByIPAddress(string strIP)
		{
			dal.DelRandomExamApplyByIPAddress(strIP);
		}
	}
}
