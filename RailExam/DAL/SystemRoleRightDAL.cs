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
    public class SystemRoleRightDAL
    {
        private static Hashtable _ormTable;

        static SystemRoleRightDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("roleid", "ROLE_ID");
            _ormTable.Add("functionid", "FUNCTION_ID");
            _ormTable.Add("functionname", "FUNCTION_NAME");
            _ormTable.Add("functionright", "FUNCTION_RIGHT");
			_ormTable.Add("rangeright", "RANGE_RIGHT");
        }

        /// <summary>
        /// 通过ID获取角色权限
        /// </summary>
        /// <param name="roleID">角色权限ID</param>
        /// <returns>角色权限</returns>
        public IList<SystemRoleRight> GetRoleRights(int roleID)
        {
            IList<SystemRoleRight> systemRoleRights = new List<SystemRoleRight>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_ROLE_RIGHT_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_role_id", DbType.Int32, roleID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    SystemRoleRight systemRoleRight = CreateModelObject(dataReader);

                    systemRoleRights.Add(systemRoleRight);
                }
           }

           return systemRoleRights;
        }

        /// <summary>
        /// 更新角色权限
        /// </summary>
        /// <param name="roleID">更新的角色ID</param>
        /// <param name="systemRoleRights">更新后的角色权限信息</param>       
        public bool UpdateRoleRight(int roleID, IList<SystemRoleRight> systemRoleRights)
        {
            bool bSuccess = true;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand1 = "USP_SYSTEM_ROLE_RIGHT_D";
            string sqlCommand2 = "USP_SYSTEM_ROLE_RIGHT_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand1);

            db.AddInParameter(dbCommand, "p_role_id", DbType.Int32, roleID);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);

                foreach(SystemRoleRight systemRoleRight in systemRoleRights)
                {
                    dbCommand = db.GetStoredProcCommand(sqlCommand2);
                    db.AddInParameter(dbCommand, "p_role_id", DbType.Int32, roleID);
                    db.AddInParameter(dbCommand, "p_function_id", DbType.String, systemRoleRight.FunctionID);
                    db.AddInParameter(dbCommand, "p_function_right", DbType.Int32, systemRoleRight.FunctionRight);
					db.AddInParameter(dbCommand, "p_range_right", DbType.Int32, systemRoleRight.RangeRight);
                    db.ExecuteNonQuery(dbCommand, transaction);
                }

                transaction.Commit();
            }
            catch(Exception ex)
            {
                transaction.Rollback();

                bSuccess = false;

                throw ex;
            }
            connection.Close();

            return bSuccess;
        }
		
        public static string GetMappingFieldName(string propertyName)
        {
            return (string)_ormTable[propertyName.ToLower()];
        }

        public static SystemRoleRight CreateModelObject(IDataReader dataReader)
        {
            string strFunctionName = string.Empty;

            if (dataReader[GetMappingFieldName("FunctionID")].ToString().Length > 2)
            {
                strFunctionName = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + DataConvert.ToString(dataReader[GetMappingFieldName("FunctionName")]);
            }
            else
            {
                strFunctionName = DataConvert.ToString(dataReader[GetMappingFieldName("FunctionName")]);
            }

            return new SystemRoleRight(
                DataConvert.ToInt(dataReader[GetMappingFieldName("RoleID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("FunctionID")]),
                strFunctionName,
                DataConvert.ToInt(dataReader[GetMappingFieldName("FunctionRight")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("RangeRight")]));
        }


		/// <summary>
		/// 通过ID获取范围权限
		/// </summary>
		/// <param name="roleID">角色权限ID</param>
		/// <returns>权限</returns>
		public IList<SystemRoleRight> GetRoleRightsClass(int roleID)
		{
			IList<SystemRoleRight> systemRoleRights = new List<SystemRoleRight>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_SYSTEM_ROLE_RIGHT_Q";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_role_id", DbType.Int32, roleID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					SystemRoleRight systemRoleRight = CreateModelObject(dataReader);

					systemRoleRights.Add(systemRoleRight);
				}
			}

			return systemRoleRights;
		}

		/// <summary>
		/// 更新范围权限
		/// </summary>
		/// <param name="roleID"></param>
		/// <param name="systemRoleRights"></param>
		/// <returns></returns>
		public bool UpdateRoleRightClass(int roleID, IList<SystemRoleRight> systemRoleRights)
		{
			bool bSuccess = true;

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand1 = "USP_SYSTEM_ROLE_RIGHT_D";
			string sqlCommand2 = "USP_SYSTEM_ROLE_RIGHT_I";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand1);

			db.AddInParameter(dbCommand, "p_role_id", DbType.Int32, roleID);

			DbConnection connection = db.CreateConnection();
			connection.Open();
			DbTransaction transaction = connection.BeginTransaction();

			try
			{
				db.ExecuteNonQuery(dbCommand, transaction);

				foreach (SystemRoleRight systemRoleRight in systemRoleRights)
				{
					dbCommand = db.GetStoredProcCommand(sqlCommand2);
					db.AddInParameter(dbCommand, "p_role_id", DbType.Int32, roleID);
					db.AddInParameter(dbCommand, "p_function_id", DbType.String, systemRoleRight.FunctionID);
					db.AddInParameter(dbCommand, "p_function_right", DbType.Int32, systemRoleRight.FunctionRight);
					db.AddInParameter(dbCommand, "p_range_right", DbType.Int32, systemRoleRight.RangeRight);

					db.ExecuteNonQuery(dbCommand, transaction);
				}

				transaction.Commit();
			}
			catch (Exception ex)
			{
				transaction.Rollback();

				bSuccess = false;

				throw ex;
			}
			connection.Close();

			return bSuccess;
		}

		public static SystemRoleRight CreateModelObjectClass(IDataReader dataReader)
		{
			string strFunctionName = string.Empty;

			if (dataReader[GetMappingFieldName("FunctionID")].ToString().Length > 2)
			{
				strFunctionName = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + DataConvert.ToString(dataReader[GetMappingFieldName("FunctionName")]);
			}
			else
			{
				strFunctionName = DataConvert.ToString(dataReader[GetMappingFieldName("FunctionName")]);
			}

			return new SystemRoleRight(
				DataConvert.ToInt(dataReader[GetMappingFieldName("RoleID")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("FunctionID")]),
				strFunctionName,
				DataConvert.ToInt(dataReader[GetMappingFieldName("RangeRight")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("FunctionRight")]));
		}
    }
}
