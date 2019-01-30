using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using RailExam.Model;
using DSunSoft.Data;

namespace RailExam.DAL
{
	public class SystemFunctionDAL
	{
		private static Hashtable _ormTable;
		private int _recordCount = 0;

		static SystemFunctionDAL()
		{
			_ormTable = new Hashtable();

            _ormTable.Add("functionid", "FUNCTION_ID");
            _ormTable.Add("functionname", "FUNCTION_NAME");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("pageurl", "PAGE_URL");
            _ormTable.Add("menuname", "MENU_NAME");
            _ormTable.Add("toolbarno", "TOOLBAR_NO");
            _ormTable.Add("toolbarname", "TOOLBAR_NAME");
			_ormTable.Add("isdefault", "IS_DEFAULT");
			_ormTable.Add("memo", "MEMO");
		}

        public IList<SystemFunction> GetSystemFunctions(string functionID, string functionName,
            string description, string pageUrl, string menuName, int toolbarNo, string toolbarName, 
            bool isDefault, string memo, int startRowIndex, int maximumRows, string orderBy)
		{
            IList<SystemFunction> systemFunctions = new List<SystemFunction>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_SYSTEM_FUNCTION_S";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
			db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
			db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
			db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
                    SystemFunction systemFunction = CreateModelObject(dataReader);

                    systemFunctions.Add(systemFunction);
				}
			}

			_recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return systemFunctions;
		}

        public IList<SystemFunction> GetSystemFunctions()
		{
            IList<SystemFunction> systemFunctions = new List<SystemFunction>();

			Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_FUNCTION_S";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, 0);
			db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, 25);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy("functionid"));
			db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
                    SystemFunction systemFunction = CreateModelObject(dataReader);

                    systemFunctions.Add(systemFunction);
				}
			}

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return systemFunctions;
		}

        public SystemFunction GetSystemFunction(string functionID)
		{
            SystemFunction systemFunction = null;

			Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_FUNCTION_G";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_function_id", DbType.String, functionID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				if (dataReader.Read())
				{
                    systemFunction = CreateModelObject(dataReader);
				}
			}

            return systemFunction;
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

        public static SystemFunction CreateModelObject(IDataReader dataReader)
		{
            return new SystemFunction(
                DataConvert.ToString(dataReader[GetMappingFieldName("FunctionID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("FunctionName")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("PageUrl")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("MenuName")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("ToolbarNo")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("ToolbarName")]),
				DataConvert.ToBool(dataReader[GetMappingFieldName("IsDefault")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
		}
	}
}