using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
	public class WorkGroupLeaderLevelBLL
	{
		private static readonly WorkGroupLeaderLevelDAL dal = new WorkGroupLeaderLevelDAL();

		public IList<WorkGroupLeaderLevel> GetAllWorkGroupLeaderLevel()
		{
			return dal.GetAllWorkGroupLeaderLevel();
		}

		public IList<WorkGroupLeaderLevel> GetWorkGroupLeaderLevel()
		{
			IList<WorkGroupLeaderLevel> objList = dal.GetAllWorkGroupLeaderLevel();

			WorkGroupLeaderLevel obj = new WorkGroupLeaderLevel();
			obj.WorkGroupLeaderLevelID = 0;
			obj.LevelName = "--«Î—°‘Ò--";

			objList.Insert(0, obj);

			return objList;
		}

		public void DeleteWorkGroupLeaderLevel(int ID)
		{
			dal.DeleteWorkGroupLeaderLevel(ID);
		}

		public WorkGroupLeaderLevel GetWorkGroupLeaderLevelByWorkGroupLeaderLevelID(int workGroupLeaderLevelID)
		{
			return dal.GetWorkGroupLeaderLevelByWorkGroupLeaderLevelID(workGroupLeaderLevelID);
		}

		public void UpdateWorkGroupLeaderLevel(WorkGroupLeaderLevel obj)
		{
			dal.UpdateWorkGroupLeaderLevel(obj);
		}

		public void InsertWorkGroupLeaderLevel(string levelName, string memo)
		{
			dal.InsertWorkGroupLeaderLevel(levelName, memo);
		}

		public IList<WorkGroupLeaderLevel> GetWorkGroupLeaderLevelByWhereClause(string sql)
		{
			return dal.GetWorkGroupLeaderLevelByWhereClause(sql);
		}
	}
}
