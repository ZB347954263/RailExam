using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using DSunSoft.Web.Global;

namespace RailExam.BLL
{
    /// <summary>
    /// 业务逻辑：日志
    /// </summary>
    public class SystemLogBLL
    {
        private static readonly SystemLogDAL dal = new SystemLogDAL();

        /// <summary>
        /// 获取日志数据
        /// </summary>
        /// <param name="actionBeginTime"></param>
        /// <param name="actionEndTime"></param>
        /// <param name="actionOrgName"></param>
        /// <param name="actionUserID"></param>
        /// <param name="actionEmployeeName"></param>
        /// <param name="actionContent"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        //DateTime actionBeginTime, DateTime actionEndTime, 
        public IList<SystemLog> GetLogs(string actionOrgName, string actionUserID, string actionEmployeeName,
            DateTime dateBeginTime, DateTime dateEndTime, string actionContent, string memo, string strFlag)
        {
            IList<SystemLog> systemLogList = dal.GetLogs(actionOrgName, actionUserID, actionEmployeeName,
                dateBeginTime, dateEndTime, actionContent, memo, strFlag);

            return systemLogList;
        }

        public IList<SystemLog> GetLogs()
        {
            IList<SystemLog> systemLogList = dal.GetLogs();

            return systemLogList;
        }

        /// <summary>
        /// 按ID取Log
        /// </summary>
        /// <param name="logID">日志ID</param>
        /// <returns>日志</returns>
        public SystemLog GetLog(int logID)
        {
            if (logID < 1)
            {
                return null;
            }

            return dal.GetLog(logID);
        }

        /// <summary>
        /// 更新日志
        /// </summary>
        /// <param name="log">更新后的日志信息</param>
        public void UpdateLog(SystemLog systemLog)
        {
            if(dal.UpdateLog(systemLog))
            {
                WriteLog("更新的操作日志ID为：" + systemLog.LogID.ToString());
            }
        }

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="log">要删除的日志</param>
        public void DeleteLog(SystemLog systemLog)
        {
            DeleteLog(systemLog.LogID);
        }

        /// <summary>
        /// 删除日志
        /// </summary>
        /// <param name="logID">要删除的日志ID</param>
        public void DeleteLog(int logID)
        {
            dal.DeleteLog(logID);
        }

        public void DeleteLogs(IList<SystemLog> systemLogs)
        {
            if(systemLogs != null)
            {
                dal.DeleteLogs(systemLogs);

                WriteLog( SessionSet.EmployeeName+"删除操作日志");

            }
        }

        public void WriteLog(string actionContent, string memo)
        {
            dal.WriteLog(SessionSet.OrganizationName, SessionSet.UserID, SessionSet.EmployeeName, actionContent, memo);
        }

        public void WriteLog(string actionContent)
        {
            WriteLog(actionContent, "");
        }

        /// <summary>
        /// 获取查询结果记录数
        /// </summary>
        /// <param name="logID"></param>
        /// <param name="actionTime"></param>
        /// <param name="actionOrgName"></param>
        /// <param name="actionUserID"></param>
        /// <param name="actionEmployeeName"></param>
        /// <param name="actionContent"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public int GetCount(int logID, DateTime actionTime, string actionOrgName, string actionUserID,
            string actionEmployeeName, string actionContent, string memo)
        {
            return dal.RecordCount;
        }
    }
}
