using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using RailExam.Model;
using System.Web;
using DSunSoft.Data;

namespace RailExam.DAL
{
    public class SystemLogDAL
    {
        private static Hashtable _ormTable;
		private int _recordCount = 0;

        static SystemLogDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("logid", "LOG_ID");
            _ormTable.Add("actiontime", "ACTION_TIME");
            _ormTable.Add("actionorgname", "ACTION_ORG_NAME");
            _ormTable.Add("actionuserid", "ACTION_USER_ID");
            _ormTable.Add("actionemployeename", "ACTION_EMPLOYEE_NAME");
            _ormTable.Add("actioncontent", "ACTION_CONTENT");
            _ormTable.Add("memo", "MEMO");
        }

        /// <summary>
        /// 查询日志
        /// </summary>
        /// <param name="actionOrgName"></param>
        /// <param name="actionUserID"></param>
        /// <param name="actionEmployeeName"></param>
        /// <param name="dateBeginTime"></param>
        /// <param name="dateEndTime"></param>
        /// <param name="actionContent"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public IList<SystemLog> GetLogs(string actionOrgName, string actionUserID, string actionEmployeeName,
            DateTime dateBeginTime, DateTime dateEndTime, string actionContent, string memo, string flag)
        {
            IList<SystemLog> systemLogs = new List<SystemLog>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_LOG_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_action_org_name", DbType.String, actionOrgName);
            db.AddInParameter(dbCommand, "p_action_user_id", DbType.String, actionUserID);
            db.AddInParameter(dbCommand, "p_action_employee_name", DbType.String, actionEmployeeName);
            db.AddInParameter(dbCommand, "p_action_begin_time", DbType.Date, dateBeginTime);
            db.AddInParameter(dbCommand, "p_action_end_time", DbType.Date, dateEndTime);
            db.AddInParameter(dbCommand, "p_action_content", DbType.String, actionContent);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, memo);
            db.AddInParameter(dbCommand, "p_flag", DbType.String, flag);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    SystemLog systemLog = CreateModelObject(dataReader);

                    systemLogs.Add(systemLog);
                }
            }

            return systemLogs;
        }

        /// <summary>
        /// 查询日志
        /// </summary>
        /// <returns></returns>
        public IList<SystemLog> GetLogs()
        {
            IList<SystemLog> systemLogs = new List<SystemLog>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_LOG_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy("logid"));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    SystemLog systemLog = CreateModelObject(dataReader);

                    systemLogs.Add(systemLog);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return systemLogs;
        }
        
        /// <summary>
        /// 通过ID获取日志
        /// </summary>
        /// <param name="logID">日志ID</param>
        /// <returns>日志</returns>
        public SystemLog GetLog(int logID)
        {
            SystemLog systemlog = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_LOG_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_log_id", DbType.Int32, logID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    systemlog = CreateModelObject(dataReader);
                }
            }

            return systemlog;
        }

        /// <summary>
        /// 更新日志
        /// </summary>
        /// <param name="log">更新后的日志信息</param>
        public bool UpdateLog(SystemLog systemLog)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_LOG_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_log_id", DbType.Int32, systemLog.LogID);
            db.AddInParameter(dbCommand, "p_action_time", DbType.Date, systemLog.ActionTime);
            db.AddInParameter(dbCommand, "p_action_org_name", DbType.String, systemLog.ActionOrgName);
            db.AddInParameter(dbCommand, "p_action_user_id", DbType.String, systemLog.ActionUserID);
            db.AddInParameter(dbCommand, "p_action_employee_name", DbType.String, systemLog.ActionEmployeeName);
            db.AddInParameter(dbCommand, "p_action_content", DbType.String, systemLog.ActionContent);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, systemLog.Memo);

            if (db.ExecuteNonQuery(dbCommand) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="logID">要删除的日志ID</param>
        public bool DeleteLog(int logID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_LOG_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_log_id", DbType.Int32, logID);

            if (db.ExecuteNonQuery(dbCommand) > 0)
                return true;
            else
                return false;
        }

        public void DeleteLogs(IList<SystemLog> systemLogs)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "USP_SYSTEM_LOG_D";
          
            foreach (SystemLog systemLog in systemLogs) 
            {
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "p_log_id", DbType.Int32, systemLog.LogID);
            db.ExecuteNonQuery(dbCommand);
            }

        }



        public void WriteLog(string OrgName, string UserID, string EmployeeName, string actionContent, string memo)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYSTEM_LOG_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_log_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_action_org_name", DbType.String, OrgName);
            db.AddInParameter(dbCommand, "p_action_user_id", DbType.String, UserID);
            db.AddInParameter(dbCommand, "p_action_employee_name", DbType.String, EmployeeName);
            db.AddInParameter(dbCommand, "p_action_content", DbType.String, actionContent);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, memo);

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

        public static SystemLog CreateModelObject(IDataReader dataReader)
        {
            return new SystemLog(
                DataConvert.ToInt(dataReader[GetMappingFieldName("LogID")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("ActionTime")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ActionOrgName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ActionUserID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ActionEmployeeName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ActionContent")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }
    }
}
