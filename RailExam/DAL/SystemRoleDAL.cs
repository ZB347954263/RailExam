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
    public class SystemRoleDAL
    {
        private static Hashtable _ormTable;
		private int _recordCount = 0;

        static SystemRoleDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("roleid", "ROLE_ID");
            _ormTable.Add("rolename", "ROLE_NAME");
            _ormTable.Add("isadmin", "IS_ADMIN");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("memo", "MEMO");
            _ormTable.Add("usetype","USE_TYPE");
            _ormTable.Add("railsystemid","RAIL_SYSTEM_ID");
            _ormTable.Add("railsystemname","RAIL_SYSTEM_NAME");
        }

        /// <summary>
        /// 查询角色
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="roleName"></param>
        /// <param name="isAdmin"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">起始记录行</param>
        /// <param name="maximumRows">每页记录条数</param>
        /// <param name="orderBy">排序字符串，如"FieldName ASC"</param>
        /// <returns></returns>
        public IList<SystemRole> GetRoles(int roleID, string roleName, bool isAdmin, string description, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<SystemRole> systemRoles = new List<SystemRole>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_ROLE_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    SystemRole systemRole = CreateModelObject(dataReader);

                    systemRoles.Add(systemRole);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return systemRoles;
        }

        /// <summary>
        /// 查询角色
        /// </summary>
        /// <returns></returns>
        public IList<SystemRole> GetRoles(int suitRange)
        {
            IList<SystemRole> systemRoles = new List<SystemRole>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_ROLE_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_suit_range", DbType.String, suitRange);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    SystemRole systemRole = CreateModelObject(dataReader);

                    systemRoles.Add(systemRole);
                }
            }

            return systemRoles;
        }


        /// <summary>
        /// 通过ID获取角色
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns>角色</returns>
        public SystemRole GetRole(int roleID)
        {
            SystemRole systemRole = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_ROLE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_role_id", DbType.Int32, roleID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    systemRole = CreateModelObject(dataReader);
                    systemRole.RailSystemID =Convert.ToInt32(dataReader[GetMappingFieldName("RailSystemID")].ToString());
                    systemRole.RailSystemName = dataReader[GetMappingFieldName("RailSystemName")].ToString();

                }
            }

            return systemRole;
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="systemRole">新增的角色信息</param>
        /// <returns></returns>
        public int AddRole(SystemRole systemRole)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_ROLE_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_role_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_role_name", DbType.String, systemRole.RoleName);
            db.AddInParameter(dbCommand, "p_is_admin", DbType.Int32, systemRole.IsAdmin ? 1 : 0);
            db.AddInParameter(dbCommand, "p_description", DbType.String, systemRole.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, systemRole.Memo);
            db.AddInParameter(dbCommand, "p_rail_system_id", DbType.Int32, systemRole.RailSystemID);

            db.ExecuteNonQuery(dbCommand);

            return Convert.ToInt32(db.GetParameterValue(dbCommand, "p_role_id"));
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="systemRole">更新后的角色信息</param>
        public bool UpdateRole(SystemRole systemRole)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_ROLE_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_role_id", DbType.Int32, systemRole.RoleID);
            db.AddInParameter(dbCommand, "p_role_name", DbType.String, systemRole.RoleName);
            db.AddInParameter(dbCommand, "p_is_admin", DbType.Int32, systemRole.IsAdmin ? 1 : 0);
            db.AddInParameter(dbCommand, "p_description", DbType.String, systemRole.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, systemRole.Memo);
            db.AddInParameter(dbCommand, "p_rail_system_id", DbType.Int32, systemRole.RailSystemID);

            if (db.ExecuteNonQuery(dbCommand) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleID">要删除的角色ID</param>
        public bool DeleteRole(int roleID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_ROLE_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_role_id", DbType.Int32, roleID);

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

        public static SystemRole CreateModelObject(IDataReader dataReader)
        {
            return new SystemRole(
                DataConvert.ToInt(dataReader[GetMappingFieldName("RoleID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("RoleName")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("IsAdmin")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("UseType")]));
        }
    }
}
