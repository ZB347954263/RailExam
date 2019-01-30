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
    public class SystemUserDAL
    {
        private static Hashtable _ormTable;
		private int _recordCount = 0;

        static SystemUserDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("userid", "USER_ID");
            _ormTable.Add("password", "PASSWORD");
            _ormTable.Add("employeeid", "EMPLOYEE_ID");
            _ormTable.Add("roleid", "ROLE_ID");
            _ormTable.Add("rolename", "ROLE_NAME");
            _ormTable.Add("memo", "MEMO");
        }
        
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="employeeID"></param>
        /// <param name="roleID"></param>
        /// <param name="roleName"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">起始记录行</param>
        /// <param name="maximumRows">每页记录条数</param>
        /// <param name="orderBy">排序字符串，如"FieldName ASC"</param>
        /// <returns></returns>
        public IList<SystemUser> GetUsers(string userID, string password, int employeeID, 
                int roleID, string roleName, string memo, int startRowIndex, int maximumRows, string orderBy)
	    {
            IList<SystemUser> systemUsers = new List<SystemUser>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_USER_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    SystemUser systemUser = CreateModelObject(dataReader);

                    systemUsers.Add(systemUser);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return systemUsers;
	    }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <returns></returns>
        public IList<SystemUser> GetUsers()
        {
            IList<SystemUser> systemUsers = new List<SystemUser>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_USER_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy("userid"));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while(dataReader.Read())
                {
                    SystemUser systemUser = CreateModelObject(dataReader);

                    systemUsers.Add(systemUser);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return systemUsers;
        }

        public IList<SystemUser> GetUsersByEmployeeID(int employeeID)
        {
            IList<SystemUser> systemUsers = new List<SystemUser>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_USER_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employeeID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    SystemUser systemUser = CreateModelObject(dataReader);

                    systemUsers.Add(systemUser);
                }
            }

            return systemUsers;
        }

        public IList<SystemUser> GetUsersByRoleID(int roleid)
        {
            IList<SystemUser> systemUsers = new List<SystemUser>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_USER_Q_Role";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_role_id", DbType.Int32, roleid);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    SystemUser systemUser = CreateModelObject(dataReader);

                    systemUsers.Add(systemUser);
                }
            }

            return systemUsers;
        }

        public SystemUser GetUserByEmployeeID(int employeeID)
        {
            SystemUser systemUser = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_USER_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employeeID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    systemUser = CreateModelObject(dataReader);
                }
            }

            return systemUser;
        }

        /// <summary>
        /// 通过ID获取用户
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <returns>用户</returns>
        public SystemUser GetUser(string userID)
        {
            SystemUser systemUser = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_USER_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_user_id", DbType.String, userID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    systemUser = CreateModelObject(dataReader);
                }
            }

            return systemUser;
        }

		/// <summary>
		/// 通过ID获取用户
		/// </summary>
		/// <param name="userID">用户ID</param>
		/// <returns>用户</returns>
		public SystemUser GetUser(string userID,int orgID)
		{
			SystemUser systemUser = null;

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_SYSTEM_USER_G_ORG";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_user_id", DbType.String, userID);
			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				if (dataReader.Read())
				{
					systemUser = CreateModelObject(dataReader);
				}
			}

			return systemUser;
		}

        /// <summary>
        /// 通过ID获取用户
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="password">密码</param>
        /// <returns>用户</returns>
        public SystemUser GetUser(string userID, string password)
        {
            SystemUser systemUser = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_USER_G1";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_user_id", DbType.String, userID);
            db.AddInParameter(dbCommand, "p_password", DbType.String, password);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    systemUser = CreateModelObject(dataReader);
                }
            }

            return systemUser;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user">新增的用户信息</param>
        /// <returns></returns>
        public bool AddUser(SystemUser systemUser)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_USER_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_user_id", DbType.String, systemUser.UserID);
            db.AddInParameter(dbCommand, "p_password", DbType.String, systemUser.Password);
            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, systemUser.EmployeeID);
            db.AddInParameter(dbCommand, "p_role_id", DbType.Int32, systemUser.RoleID);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, systemUser.Memo);

            if (db.ExecuteNonQuery(dbCommand) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="user">更新后的用户信息</param>
        public bool UpdateUser(SystemUser systemUser)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_USER_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_user_id", DbType.String, systemUser.UserID);
            db.AddInParameter(dbCommand, "p_password", DbType.String, systemUser.Password);
            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, systemUser.EmployeeID);
            db.AddInParameter(dbCommand, "p_role_id", DbType.Int32, systemUser.RoleID);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, systemUser.Memo);

            if (db.ExecuteNonQuery(dbCommand) > 0)
                return true;
            else
                return false;
        }

        public void UpdateUserPsw(string userID,string password)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_USER_U_Station";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_user_id", DbType.String, userID);
            db.AddInParameter(dbCommand, "p_password", DbType.String, password);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userID">要删除的用户ID</param>
        public bool DeleteUser(string userID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_USER_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_user_id", DbType.String, userID);

            if (db.ExecuteNonQuery(dbCommand) > 0)
                return true;
            else
                return false;
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

        public static SystemUser CreateModelObject(IDataReader dataReader)
        {
            return new SystemUser(
                DataConvert.ToString(dataReader[GetMappingFieldName("UserID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Password")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("EmployeeID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("RoleID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("RoleName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }
    }
}
