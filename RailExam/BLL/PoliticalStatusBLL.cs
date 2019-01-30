using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
	public class PoliticalStatusBLL
	{
		private static readonly PoliticalStatusDAL dal = new PoliticalStatusDAL();

		public IList<PoliticalStatus> GetAllPoliticalStatus()
		{
			return dal.GetAllPoliticalStatus();
		}

		public IList<PoliticalStatus> GetPoliticalStatus()
		{
			IList<PoliticalStatus> objList = dal.GetAllPoliticalStatus();

			PoliticalStatus obj = new PoliticalStatus();
			obj.PoliticalStatusID = 0;
			obj.PoliticalStatusName = "--«Î—°‘Ò--";

			objList.Insert(0, obj);

			return objList;
		}

		public void DeletePoliticalStatus(int ID)
		{
			dal.DeletePoliticalStatus(ID);
		}

		public PoliticalStatus GetPoliticalStatusByPoliticalStatusID(int politicalStatusID)
		{
			return dal.GetPoliticalStatusByPoliticalStatusID(politicalStatusID);
		}

		public void UpdatePoliticalStatus(PoliticalStatus obj)
		{
			dal.UpdatePoliticalStatus(obj);
		}

		public void InsertPoliticalStatus(string levelName, string memo)
		{
			dal.InsertPoliticalStatus(levelName, memo);
		}

		public IList<PoliticalStatus> GetPoliticalStatusByWhereClause(string sql)
		{
			return dal.GetPoliticalStatusByWhereClause(sql);
		}
	}
}
