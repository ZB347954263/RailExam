using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
	public class PoliticalStatusDAL
	{
		private static Hashtable _ormTable;
		private int _recordCount = 0;

		static PoliticalStatusDAL()
        {
            _ormTable = new Hashtable();

			_ormTable.Add("politicalstatusid", "POLITICAL_STATUS_ID");
			_ormTable.Add("politicalstatusname", "POLITICAL_STATUS_NAME");
			_ormTable.Add("memo","Memo");
			_ormTable.Add("orderindex", "ORDER_INDEX");
        }


		public IList<PoliticalStatus> GetAllPoliticalStatus()
        {
			IList<PoliticalStatus> objList = new List<PoliticalStatus>();

            Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_POLITICAL_STATUS_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
					PoliticalStatus obj = CreateModelObject(dataReader);
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

		public static PoliticalStatus CreateModelObject(IDataReader dataReader)
        {
			return new PoliticalStatus(
				DataConvert.ToInt(dataReader[GetMappingFieldName("PoliticalStatusID")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("PoliticalStatusName")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("OrderIndex")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }

		public void DeletePoliticalStatus(int ID)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_political_status_D";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_political_status_id", DbType.Int32, ID);

			db.ExecuteNonQuery(dbCommand);
		}

		public PoliticalStatus GetPoliticalStatusByPoliticalStatusID(int politicalStatusID)
		{
			PoliticalStatus objList = new PoliticalStatus();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_political_status_S";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_political_status_id", DbType.Int32, politicalStatusID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					objList = CreateModelObject(dataReader);
				}
			}

			return objList;
		}

		public void UpdatePoliticalStatus(PoliticalStatus obj)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_political_status_U";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_political_status_id", DbType.Int32, obj.PoliticalStatusID);
			db.AddInParameter(dbCommand, "p_political_status_name", DbType.String, obj.PoliticalStatusName);
			db.AddInParameter(dbCommand, "p_memo", DbType.String, obj.Memo);
			db.AddInParameter(dbCommand, "p_orderindex", DbType.Int32, obj.OrderIndex);

			db.ExecuteNonQuery(dbCommand);
		}

		public void InsertPoliticalStatus(string levelName, string memo)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_political_status_I";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_political_status_name", DbType.String, levelName);
			db.AddInParameter(dbCommand, "p_memo", DbType.String, memo);

			db.ExecuteNonQuery(dbCommand);
		}

		public IList<PoliticalStatus> GetPoliticalStatusByWhereClause(string sql)
		{
			IList<PoliticalStatus> objList = new List<PoliticalStatus>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_political_status_G_Where";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_sql", DbType.String, sql);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					PoliticalStatus obj = CreateModelObject(dataReader);
					objList.Add(obj);
				}
			}

			return objList;
		}
	}
}
