using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
	public class WorkGroupLeaderLevelDAL
	{
		private static Hashtable _ormTable;
		private int _recordCount = 0;

		static WorkGroupLeaderLevelDAL()
        {
            _ormTable = new Hashtable();

			_ormTable.Add("workgroupleaderlevelid", "WORKGROUPLEADER_LEVEL_ID");
            _ormTable.Add("levelname", "LEVEL_NAME");
			_ormTable.Add("memo","Memo");
			_ormTable.Add("orderindex", "ORDER_INDEX");
        }


		public IList<WorkGroupLeaderLevel> GetAllWorkGroupLeaderLevel()
        {
			IList<WorkGroupLeaderLevel> objList = new List<WorkGroupLeaderLevel>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_WorkGroupLeader_Level_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
					WorkGroupLeaderLevel obj = CreateModelObject(dataReader);
                    objList.Add(obj);
                }
            }

            return objList;
        }

        public int RecordCount
        {
            get
            {
                return _recordCount;
            }
        }

        public static string GetMappingFieldName(string propertyName)
        {
            return (string)_ormTable[propertyName.ToLower()];
        }

        public static string GetMappingOrderBy(string orderBy)
        {
            orderBy = orderBy.Trim();

            if (string.IsNullOrEmpty(orderBy))
            {
                return string.Empty;
            }

            string mappingOrderBy = string.Empty;
            string[] orderByConditions = orderBy.Split(new char[] { ',' });

            foreach (string s in orderByConditions)
            {
                string orderByCondition = s.Trim();

                string[] orderBysOfOneCondition = orderByCondition.Split(new char[] { ' ' });

                if (orderBysOfOneCondition.Length == 0)
                {
                    continue;
                }
                else
                {
                    if (mappingOrderBy != string.Empty)
                    {
                        mappingOrderBy += ',';
                    }

                    if (orderBysOfOneCondition.Length == 1)
                    {
                        mappingOrderBy += GetMappingFieldName(orderBysOfOneCondition[0]);
                    }
                    else
                    {
                        mappingOrderBy += GetMappingFieldName(orderBysOfOneCondition[0]) + ' ' + orderBysOfOneCondition[1];
                    }
                }
            }

            return mappingOrderBy;
        }

		public static WorkGroupLeaderLevel CreateModelObject(IDataReader dataReader)
        {
			return new WorkGroupLeaderLevel(
				DataConvert.ToInt(dataReader[GetMappingFieldName("WorkGroupLeaderLevelID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("LevelName")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("OrderIndex")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }

		public void DeleteWorkGroupLeaderLevel(int ID)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_workgroupleader_level_D";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_workgroupleader_level_id", DbType.Int32, ID);

			db.ExecuteNonQuery(dbCommand);
		}

		public WorkGroupLeaderLevel GetWorkGroupLeaderLevelByWorkGroupLeaderLevelID(int workGroupLeaderLevelID)
		{
			WorkGroupLeaderLevel objList = new WorkGroupLeaderLevel();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_workgroupleader_level_S";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_workgroupleader_level_id", DbType.Int32, workGroupLeaderLevelID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					objList = CreateModelObject(dataReader);
				}
			}

			return objList;
		}

		public void UpdateWorkGroupLeaderLevel(WorkGroupLeaderLevel obj)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_workgroupleader_level_U";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_workgroupleader_level_id", DbType.Int32,obj.WorkGroupLeaderLevelID);
			db.AddInParameter(dbCommand, "p_level_name", DbType.String,obj.LevelName);
			db.AddInParameter(dbCommand, "p_memo", DbType.String,obj.Memo);
			db.AddInParameter(dbCommand,"p_orderindex",DbType.Int32,obj.OrderIndex);

			db.ExecuteNonQuery(dbCommand);
		}

		public void InsertWorkGroupLeaderLevel(string levelName, string memo)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_workgroupleader_level_I";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_level_name", DbType.String, levelName);
			db.AddInParameter(dbCommand, "p_memo", DbType.String, memo);

			db.ExecuteNonQuery(dbCommand);
		}

		public IList<WorkGroupLeaderLevel> GetWorkGroupLeaderLevelByWhereClause(string sql)
		{
			IList<WorkGroupLeaderLevel> objList = new List<WorkGroupLeaderLevel>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_workgroupleader_G_Where";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_sql", DbType.String, sql);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					WorkGroupLeaderLevel obj = CreateModelObject(dataReader);
					objList.Add(obj);
				}
			}

			return objList;
		}
	}
}
