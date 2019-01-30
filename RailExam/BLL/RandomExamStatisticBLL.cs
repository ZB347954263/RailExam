using System;
using System.Collections.Generic;
using System.Text;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
	public class RandomExamStatisticBLL
	{
		private static readonly RandomExamStatisticDAL dal = new RandomExamStatisticDAL();

		public IList<RandomExamStatistic> GetErrorItemInfo(int bookID, int chapterID, int type, int randomExamID, DateTime beginTime, DateTime endTime,int orgID)
		{
			IList<RandomExamStatistic> objList = new List<RandomExamStatistic>();
			objList = dal.GetErrorItemInfo(bookID, chapterID, type, randomExamID, beginTime, endTime,orgID);

			foreach (RandomExamStatistic statistic in objList)
			{
				statistic.Content = ItemBLL.NoHTML(statistic.Content);
			}

			return objList;
		}

		public IList<RandomExamStatistic> GetErrorItemInfoByEmployeeID(int EmployeeID, DateTime beginTime, DateTime endTime)
		{
			IList<RandomExamStatistic> objList = new List<RandomExamStatistic>();
			objList = dal.GetErrorItemInfoByEmployeeID(EmployeeID,beginTime,endTime);

			foreach (RandomExamStatistic statistic in objList)
			{
				statistic.Content = ItemBLL.NoHTML(statistic.Content);
			}

			return objList;
		}

		public IList<RandomExamStatistic> GetErrorItemInfoByItemID(int bookID, int chapterID, int type, int randomExamID, DateTime beginTime, DateTime endTime, int orgID, int employeeID,int ItemID)
		{
			return dal.GetErrorItemInfoByItemID(bookID, chapterID, type, randomExamID, beginTime, endTime, orgID, employeeID, ItemID);
		}
	}
}
