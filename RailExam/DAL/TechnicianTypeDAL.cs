using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
    public class TechnicianTypeDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static TechnicianTypeDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("techniciantypeid", "TECHNICIAN_TYPE_ID");
            _ormTable.Add("typename", "TYPE_NAME");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("isdefault", "IS_DEFAULT");
            _ormTable.Add("memo", "MEMO");
        }


        public IList<TechnicianType> GetAllTechnicianType()
        {
            IList<TechnicianType> objList = new List<TechnicianType>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TECHNICIAN_TYPE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TechnicianType obj = CreateModelObject(dataReader);
                    objList.Add(obj);
                }
            }

            return objList;
        }

		public TechnicianType GetTechnicianTypeByTechnicianTypeID(int technicianTypeID)
		{
			TechnicianType objList = new TechnicianType();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_TECHNICIAN_TYPE_S";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_technician_type_id", DbType.Int32, technicianTypeID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					objList = CreateModelObject(dataReader);
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

        public static TechnicianType CreateModelObject(IDataReader dataReader)
        {
            return new TechnicianType(
                DataConvert.ToInt(dataReader[GetMappingFieldName("TechnicianTypeID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("TypeName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("IsDefault")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }

		public void DeleteTechnicianType(int ID)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_technician_type_D";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_technician_type_id", DbType.Int32, ID);

			db.ExecuteNonQuery(dbCommand);
		}
		public void DeleteTechnicianType(int ID,string str)
		{
			Database db = DatabaseFactory.CreateDatabase();
			string sql = string.Format("delete from technician_type where technician_type_id={0}",ID);
			db.ExecuteNonQuery(CommandType.Text,sql);
		}

		public void UpdateTechnicianType(TechnicianType obj)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_technician_type_U";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_technician_type_id", DbType.Int32,obj.TechnicianTypeID);
			db.AddInParameter(dbCommand, "p_type_name", DbType.String,obj.TypeName);
			db.AddInParameter(dbCommand, "p_memo", DbType.String, obj.Memo);

			db.ExecuteNonQuery(dbCommand);
		}
		public void UpdateTechnicianType(TechnicianType obj,string str)
		{
			Database db = DatabaseFactory.CreateDatabase();
			string sql =
				string.Format("update technician_type set type_name='{0}',memo='{1}' where technician_type_id={2}",
				              obj.TypeName, obj.Memo, obj.TechnicianTypeID);
			db.ExecuteNonQuery(CommandType.Text,sql);
		}
		public void InsertTechnicianType(string typeName, string memo)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_technician_type_I";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_type_name", DbType.String, typeName);
			db.AddInParameter(dbCommand, "p_memo", DbType.String, memo);

			db.ExecuteNonQuery(dbCommand);
		}
		public void InsertTechnicianType(string typeName, string memo,string str)
		{
			Database db = DatabaseFactory.CreateDatabase();
			string sql = string.Format("insert into technician_type values (technician_type_seq.nextval,'{0}','',0,'{1}')",typeName,memo);
			db.ExecuteNonQuery(CommandType.Text,sql);
		}

		public IList<TechnicianType> GetTechnicianTypeByWhereClause(string sql)
		{
			IList<TechnicianType> objList = new List<TechnicianType>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_technician_type_G_Where";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_sql", DbType.String, sql);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					TechnicianType obj = CreateModelObject(dataReader);
					objList.Add(obj);
				}
			}

			return objList;
		}
		public IList<TechnicianType> GetTechnicianTypeByWhereClause(string sql,string str)
		{
			IList<TechnicianType> objList = new List<TechnicianType>();
			Database db = DatabaseFactory.CreateDatabase();
			string strSql = string.Format("select * from Technician_Type where {0}",sql);
			using (IDataReader dataReader = db.ExecuteReader(CommandType.Text, strSql))
			{
				while (dataReader.Read())
				{
					TechnicianType obj = CreateModelObject(dataReader);
					objList.Add(obj);
				}
			}
			return objList;
		}
    }
}
