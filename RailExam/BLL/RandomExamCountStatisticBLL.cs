using System;
using System.Collections.Generic;
using System.Text;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
	public class RandomExamCountStatisticBLL
	{
		private static readonly RandomExamCountStatisticDAL dal = new RandomExamCountStatisticDAL();

		public IList<RandomExamCountStatistic> GetCountWithOrg(int SuitRange, int OrgID, DateTime DateFrom, DateTime DateTo,int railSystemId,int style)
		{
			IList<RandomExamCountStatistic> objList = dal.GetCountWithOrg(SuitRange,OrgID,DateFrom,DateTo,railSystemId, style);

			if(SuitRange == 1)
			{
				int sumExamCount = 0;
				int sumEmployeeCount = 0;

				foreach (RandomExamCountStatistic statistic in objList)
				{
					sumExamCount += statistic.ExamCount;
					sumEmployeeCount += statistic.EmployeeCount;
				}
				RandomExamCountStatistic obj = new RandomExamCountStatistic();
				obj.OrgID = 0;
				obj.OrgName = "ºÏ¼Æ";
				obj.ExamCount = sumExamCount;
				obj.EmployeeCount = sumEmployeeCount;
				objList.Add(obj);
			}

			return objList;
		}
	}
}
