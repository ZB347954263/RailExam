using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
	public class EducationLevelDAL
	{
		private static Hashtable _ormTable;
		private int _recordCount = 0;

		static EducationLevelDAL()
        {
            _ormTable = new Hashtable();

			_ormTable.Add("educationlevelid", "EDUCATION_LEVEL_ID");
            _ormTable.Add("educationlevelname", "EDUCATION_LEVEL_NAME");
			_ormTable.Add("memo","Memo");
			_ormTable.Add("orderindex","ORDER_INDEX");
        }


		public IList<EducationLevel> GetAllEducationLevel()
        {
			IList<EducationLevel> objList = new List<EducationLevel>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Education_Level_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
					EducationLevel obj = CreateModelObject(dataReader);
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

		public static EducationLevel CreateModelObject(IDataReader dataReader)
        {
			return new EducationLevel(
				DataConvert.ToInt(dataReader[GetMappingFieldName("EducationLevelID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("EducationLevelName")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("OrderIndex")]));
        }

		public EducationLevel GetEducationLevelByEducationLevelID(int educationLevelID)
		{
			EducationLevel objList = new EducationLevel();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_education_level_S";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_education_level_id", DbType.Int32, educationLevelID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					objList = CreateModelObject(dataReader);
				}
			}

			return objList;
		}

		public void DeleteEducationLevel(int ID)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_education_level_D";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_education_level_id", DbType.Int32, ID);

			db.ExecuteNonQuery(dbCommand);
		}

		public void DeleteEducationLevel(int LevelID,string str)
		{
			Database db = DatabaseFactory.CreateDatabase();
			string sql = string.Format("delete from education_level where education_level_id={0}",LevelID);
			db.ExecuteNonQuery(CommandType.Text, sql);
		}
		public void UpdateEducationLevel(EducationLevel obj)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_education_level_U";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_education_level_id", DbType.Int32, obj.EducationLevelID);
			db.AddInParameter(dbCommand, "p_education_level_name", DbType.String,obj.EducationLevelName);
			db.AddInParameter(dbCommand, "p_memo", DbType.String,obj.Memo);
			db.AddInParameter(dbCommand, "p_orderindex", DbType.String,obj.OrderIndex);

			db.ExecuteNonQuery(dbCommand);
		}

		public void InsertEducationLevel(string educationLevelName, string memo)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_education_level_I";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_education_level_name", DbType.String, educationLevelName);
			db.AddInParameter(dbCommand, "p_memo", DbType.String, memo);

			db.ExecuteNonQuery(dbCommand);
		}


		public IList<EducationLevel> GetEducationLevelByWhereClause(string sql)
		{
			IList<EducationLevel> objList = new List<EducationLevel>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Education_Level_G_Where";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_sql", DbType.String, sql);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					EducationLevel obj = CreateModelObject(dataReader);
					objList.Add(obj);
				}
			}

			return objList;
		}
	}
}
