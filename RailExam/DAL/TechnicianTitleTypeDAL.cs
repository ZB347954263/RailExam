using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
	public class TechnicianTitleTypeDAL
	{
		private static Hashtable _ormTable;
		private int _recordCount = 0;

		static TechnicianTitleTypeDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("techniciantitletypeid", "TECHNICIAN_TITLE_TYPE_ID");
            _ormTable.Add("typename", "TYPE_NAME");
			_ormTable.Add("typelevel","TYPE_LEVEL");
			_ormTable.Add("typelevelname","TYPE_LEVEL_NAME");
			_ormTable.Add("orderindex", "ORDER_INDEX");
        }


		public IList<TechnicianTitleType> GetAllTechnicianTitleType()
        {
			IList<TechnicianTitleType> objList = new List<TechnicianTitleType>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TECHNICIAN_TITLE_TYPE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
					TechnicianTitleType obj = CreateModelObject(dataReader);
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

		public static TechnicianTitleType CreateModelObject(IDataReader dataReader)
        {
			return new TechnicianTitleType(
                DataConvert.ToInt(dataReader[GetMappingFieldName("TechnicianTitleTypeID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("TypeName")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("TypeLevel")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("TypeLevelName")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("OrderIndex")]));
        }

		public void DeleteTechnicianTitleType(int ID)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_technician_title_type_D";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_technician_title_type_id", DbType.Int32, ID);

			db.ExecuteNonQuery(dbCommand);
		}

		public TechnicianTitleType GetTechnicianTitleTypeByTechnicianTitleTypeID(int technicianTitleTypeID)
		{
			TechnicianTitleType objList = new TechnicianTitleType();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "usp_technician_title_type_s";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_technician_title_type_id", DbType.Int32, technicianTitleTypeID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					objList = CreateModelObject(dataReader);
				}
			}

			return objList;
		}

		public void UpdateTechnicianTitleType(TechnicianTitleType obj)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_technician_title_type_U";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_technician_title_type_id", DbType.Int32, obj.TechnicianTitleTypeID);
			db.AddInParameter(dbCommand, "p_type_name", DbType.String, obj.TypeName);
			db.AddInParameter(dbCommand, "p_type_level", DbType.Int32, obj.TypeLevel);
			db.AddInParameter(dbCommand, "p_orderindex", DbType.Int32,obj.OrderIndex);

			db.ExecuteNonQuery(dbCommand);
		}

		public void InsertTechnicianTitleType(TechnicianTitleType obj)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_technician_title_type_I";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_type_name", DbType.String, obj.TypeName);
			db.AddInParameter(dbCommand, "p_type_level", DbType.Int32, obj.TypeLevel);

			db.ExecuteNonQuery(dbCommand);
		}

		public IList<TechnicianTitleType> GetTechnicianTitleTypeByWhereClause(string sql)
		{
			IList<TechnicianTitleType> objList = new List<TechnicianTitleType>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_technician_title_G_Where";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_sql", DbType.String, sql);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					TechnicianTitleType obj = CreateModelObject(dataReader);
					objList.Add(obj);
				}
			}

			return objList;
		}
	}
}
