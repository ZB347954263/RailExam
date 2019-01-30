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
using System.Data.OracleClient;

namespace RailExam.DAL
 {
	public class SystemUserLoginDAL
	{
		private static Hashtable _ormTable;
		private int _recordCount = 0;

        static SystemUserLoginDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("employeeid", "EMPLOYEE_ID");
            _ormTable.Add("employeename", "EMPLOYEE_NAME");
            _ormTable.Add("orgname", "ORG_NAME");
            _ormTable.Add("postname", "POST_NAME");
            _ormTable.Add("ipaddress", "IP_ADDRESS");
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <returns></returns>
        public IList<SystemUserLogin> GetSystemUserLogin(int employeeID)
        {
            IList<SystemUserLogin> systemUsers = new List<SystemUserLogin>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_USER_Login_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32 ,employeeID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while(dataReader.Read())
                {
                    SystemUserLogin systemUser = CreateModelObject(dataReader);

                    systemUsers.Add(systemUser);
                }
            }

            return systemUsers;
        }

		public IList<SystemUserLogin> GetSystemUserLoginByOrgID(int orgID)
		{
			IList<SystemUserLogin> systemUsers = new List<SystemUserLogin>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_SYSTEM_USER_Login_G_ORG";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					SystemUserLogin systemUser = CreateModelObject(dataReader);

					systemUsers.Add(systemUser);
				}
			}

			return systemUsers;
		}

        /// <summary>
        /// 新增用户
        /// </summary>
		/// <param name="systemUser">新增的用户信息</param>
        /// <returns></returns>
        public void AddSystemUserLogin(SystemUserLogin systemUser)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_USER_Login_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_employee_id", DbType.String, systemUser.EmployeeID);
            db.AddInParameter(dbCommand, "p_ip_address", DbType.String, systemUser.IPAddress);

           db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
		/// <param name="employeeID">要删除的用户ID</param>
        public void DeleteSystemUserLogin(int employeeID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_USER_LOGIN_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_employee_id", DbType.String, employeeID);

           db.ExecuteNonQuery(dbCommand);
        }

		public void DeleteSystemUserLogin()
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_SYSTEM_USER_LOGIN_D_ALL";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.ExecuteNonQuery(dbCommand);
		}

		public void ClearSystemUserLogin()
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_SYSTEM_USER_LOGIN_C";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

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

        public static SystemUserLogin CreateModelObject(IDataReader dataReader)
        {
            return new SystemUserLogin(
				DataConvert.ToInt(dataReader[GetMappingFieldName("EmployeeID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("EmployeeName")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("OrgName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("PostName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("IPAddress")]));
        }
	}
}
