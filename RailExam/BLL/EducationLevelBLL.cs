using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
	public class EducationLevelBLL
	{
		private static readonly EducationLevelDAL dal = new EducationLevelDAL();

		public IList<EducationLevel> GetAllEducationLevel()
		{
			return dal.GetAllEducationLevel();
		}

		public IList<EducationLevel> GetEducationLevel()
		{
			IList<EducationLevel> objList = dal.GetAllEducationLevel();

			EducationLevel obj = new EducationLevel();
			obj.EducationLevelID = 0;
			obj.EducationLevelName = "--«Î—°‘Ò--";

			objList.Insert(0, obj);

			return objList;
		}

		public EducationLevel GetEducationLevelByEducationLevelID(int educationLevelID)
		{
			return dal.GetEducationLevelByEducationLevelID(educationLevelID);
		}

		public void DeleteEducationLevel(int ID)
		{
			dal.DeleteEducationLevel(ID);
		}

		public void DeleteEducationLevel(int ID,string str)
		{
			dal.DeleteEducationLevel(ID,"");
		}

		public void UpdateEducationLevel(EducationLevel obj)
		{
			dal.UpdateEducationLevel(obj);
		}

		public void InsertEducationLevel(string educationLevelName, string memo)
		{
			dal.InsertEducationLevel(educationLevelName, memo);
		}

		public IList<EducationLevel> GetEducationLevelByWhereClause(string sql)
		{
			return dal.GetEducationLevelByWhereClause(sql);
		}
	}
}
