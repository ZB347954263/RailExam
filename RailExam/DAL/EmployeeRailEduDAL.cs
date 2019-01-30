using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.IO;
using System.Web;
using System.Xml;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
	public class EmployeeRailEduDAL
	{
		private static Hashtable _ormTable;
		private int _recordCount = 0;

		static EmployeeRailEduDAL()
		{
			_ormTable = new Hashtable();
			_ormTable.Add("employeeid", "Employee_ID");
			_ormTable.Add("raileduemployeeid", "RailEdu_Employee_ID");
		}

		public void AddEmployeeRailEdu(EmployeeRailEdu employee)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_EMPLOYEE_RAILEDU_I";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddOutParameter(dbCommand, "p_employee_id", DbType.Int32, 4);
			db.AddInParameter(dbCommand, "p_railedu_employee_id", DbType.Int32, employee.RailEduEmployeeID);

			db.ExecuteNonQuery(dbCommand);
		}

		public void UpdateEmployeeRailEdu(EmployeeRailEdu employee)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_EMPLOYEE_RAILEDU_U";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddOutParameter(dbCommand, "p_employee_id", DbType.Int32, employee.EmployeeID);
			db.AddInParameter(dbCommand, "p_railedu_employee_id", DbType.Int32, employee.RailEduEmployeeID);

			db.ExecuteNonQuery(dbCommand);
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

		public static EmployeeRailEdu CreateModelObject(IDataReader dataReader)
		{
			return new EmployeeRailEdu(
				DataConvert.ToInt(dataReader[GetMappingFieldName("EmployeeID")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("RailEduEmployeeID")])
				);
		}
	}
}
