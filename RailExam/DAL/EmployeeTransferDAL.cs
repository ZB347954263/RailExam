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
	public class EmployeeTransferDAL
	{
		private static Hashtable _ormTable;
		private int _recordCount = 0;

		static EmployeeTransferDAL()
        {
            _ormTable = new Hashtable();

			_ormTable.Add("employeetransferid", "EMPLOYEE_TRANSFER_ID");
			_ormTable.Add("transfertoorgid", "TRANSFER_TO_ORG_ID");
			_ormTable.Add("employeeid", "EMPLOYEE_ID");
            _ormTable.Add("transferoutdate", "TRANSFER_OUT_DATE");
            _ormTable.Add("employeename", "EMPLOYEE_NAME");
            _ormTable.Add("sex", "SEX");
            _ormTable.Add("transferoutorgname", "TRANSFER_OUT_ORG_NAME");
			_ormTable.Add("transfertoorgname", "TRANSFER_TO_ORG_NAME");
            _ormTable.Add("workno", "WORK_NO");
            _ormTable.Add("postno", "POST_NO");
            _ormTable.Add("transferoutorgpath", "TRANSFER_OUT_ORG_PATH");
            _ormTable.Add("postname", "POST_NAME");
        }

		public void AddEmployeeTransfer(IList<EmployeeTransfer> objList)
		{
			Database db = DatabaseFactory.CreateDatabase();

			DbConnection connection = db.CreateConnection();
			connection.Open();
			DbTransaction transaction = connection.BeginTransaction();

			try
			{
				foreach (EmployeeTransfer employee in objList)
				{
					string sqlCommand = "USP_EMPLOYEE_TRANSFER_I";
					DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

					db.AddOutParameter(dbCommand, "p_employee_transfer_id", DbType.Int32, 4);
					db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employee.EmployeeID);
					db.AddInParameter(dbCommand, "p_transfer_to_org_id", DbType.Int32, employee.TransferToOrgID);

					db.ExecuteNonQuery(dbCommand, transaction);

					int id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_employee_transfer_id"));
				}

				transaction.Commit();
			}
			catch (SystemException ex)
			{
				transaction.Rollback();
				throw ex;
			}
			connection.Close();
		}

		public bool DeleteEmployeeTransfer(int employeeTransferID)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_EMPLOYEE_TRANSFER_D";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_employee_transfer_id", DbType.Int32, employeeTransferID);

			if (db.ExecuteNonQuery(dbCommand) > 0)
				return true;
			else
				return false;
		}

		public IList<EmployeeTransfer> GetEmployeeTransferOutByOrgID(int orgID)
		{
			IList<EmployeeTransfer> employees = new List<EmployeeTransfer>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_EMPLOYEE_TRANSFER_OUT_G";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_org_id", DbType.String, orgID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					EmployeeTransfer employee = CreateModelObject(dataReader);

					employees.Add(employee);
				}
			}

			return employees;
		}

		public IList<EmployeeTransfer> GetEmployeeTransferToByOrgID(int orgID)
		{
			IList<EmployeeTransfer> employees = new List<EmployeeTransfer>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_EMPLOYEE_TRANSFER_To_G";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_org_id", DbType.String, orgID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					EmployeeTransfer employee = CreateModelObject(dataReader);

					employees.Add(employee);
				}
			}

			return employees;
		}

		public EmployeeTransfer GetEmployeeTransfer(int transferID)
		{
			EmployeeTransfer employee = null;

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_EMPLOYEE_Transfer_G";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_employee_transfer_id", DbType.Int32, transferID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				if (dataReader.Read())
				{
					employee = CreateModelObject(dataReader);
				}
			}

			return employee;
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

        public static EmployeeTransfer CreateModelObject(IDataReader dataReader)
        {
			return new EmployeeTransfer(
				DataConvert.ToInt(dataReader[GetMappingFieldName("EmployeeTransferID")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("TransferToOrgID")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("EmployeeID")]),
				DataConvert.ToDateTime(dataReader[GetMappingFieldName("TransferOutDate")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("EmployeeName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Sex")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("TransferOutOrgName")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("TransferToOrgName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("WorkNo")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("PostNo")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("TransferOutOrgPath")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("PostName")])
				);
        }
	}
}
