using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using RailExam.Model;
using DSunSoft.Data;
namespace RailExam.DAL
{
	public class SystemVersionDAL
	{
		private static Hashtable _ormTable;
		private int _recordCount = 0;

		static SystemVersionDAL()
		{
			_ormTable = new Hashtable();
			_ormTable.Add("useplace", "USEPLACE");
		}

		/// <summary>
		/// 删除用户
		/// </summary>
		public int  GetUsePlace()
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_SYSTEM_VERSION_G";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddOutParameter(dbCommand, "p_use_place", DbType.Int32, 4);

			db.ExecuteNonQuery(dbCommand);
			int userPlace = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_use_place"));
			return userPlace;
		}

		public Decimal GetVersion()
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_SYSTEM_VERSION_G_Ver";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddOutParameter(dbCommand, "p_version", DbType.Decimal, 5);

			db.ExecuteNonQuery(dbCommand);
            Decimal userPlace = Convert.ToDecimal(db.GetParameterValue(dbCommand, "p_version"));
			return userPlace;
		}

        public Decimal GetVersionToServer()
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_System_Version_G_Ser";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_version", DbType.Decimal, 5);

			db.ExecuteNonQuery(dbCommand);
            Decimal userPlace = Convert.ToDecimal(db.GetParameterValue(dbCommand, "p_version"));
			return userPlace;
		}

		/// <summary>
		/// 查询结果记录数
		/// </summary>
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

		public static SystemVersion CreateModelObject(IDataReader dataReader)
		{
			return new SystemVersion(
				DataConvert.ToInt(dataReader[GetMappingFieldName("UserPlace")]));
		}
	}
}
