using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
namespace RailExam.BLL
{
	public class RandomExamModularTypeBLL
	{
		private static readonly RandomExamModularTypeDAL dal = new RandomExamModularTypeDAL();
		public IList<RandomExamModularType> GetAllRandomExamModularType()
		{
			return dal.GetAllRandomExamModularType();
		}
		public IList<RandomExamModularType> GetAllRandomExamModularTypeByWhereClause(string sql)
		{
			return dal.GetAllRandomExamModularTypeByWhereClause(sql);
		}
		public void InsertRandomExamModularType(RandomExamModularType obj)
		{
			dal.InsertRandomExamModularType(obj);
		}
		public RandomExamModularType GetRandomExamModularTypeByTypeID(int modularTypeID)
		{
			return dal.GetRandomExamModularTypeByTypeID(modularTypeID);
		}
		public int GetRandomExam(int modularTypeID)
		{
			return Convert.ToInt32(dal.GetRandomExam(modularTypeID));
		}

		public void UpdateRandomExamModularType(RandomExamModularType obj)
		{
			dal.UpdateRandomExamModularType(obj);
		}
		public void DeleteCommitteeHeadShip(int id)
		{
			dal.DeleteRandomExamModularType(id);
		}
	}
}
