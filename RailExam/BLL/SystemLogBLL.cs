using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using DSunSoft.Web.Global;

namespace RailExam.BLL
{
    /// <summary>
    /// ҵ���߼�����־
    /// </summary>
    public class SystemLogBLL
    {
        private static readonly SystemLogDAL dal = new SystemLogDAL();

        /// <summary>
        /// ��ȡ��־����
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
        /// ��IDȡLog
        /// </summary>
        /// <param name="logID">��־ID</param>
        /// <returns>��־</returns>
        public SystemLog GetLog(int logID)
        {
            if (logID < 1)
            {
                return null;
            }

            return dal.GetLog(logID);
        }

        /// <summary>
        /// ������־
        /// </summary>
        /// <param name="log">���º����־��Ϣ</param>
        public void UpdateLog(SystemLog systemLog)
        {
            if(dal.UpdateLog(systemLog))
            {
                WriteLog("���µĲ�����־IDΪ��" + systemLog.LogID.ToString());
            }
        }

        /// <summary>
        /// ɾ����־
        /// </summary>
        /// <param name="log">Ҫɾ������־</param>
        public void DeleteLog(SystemLog systemLog)
        {
            DeleteLog(systemLog.LogID);
        }

        /// <summary>
        /// ɾ����־
        /// </summary>
        /// <param name="logID">Ҫɾ������־ID</param>
        public void DeleteLog(int logID)
        {
            dal.DeleteLog(logID);
        }

        public void DeleteLogs(IList<SystemLog> systemLogs)
        {
            if(systemLogs != null)
            {
                dal.DeleteLogs(systemLogs);

                WriteLog( SessionSet.EmployeeName+"ɾ��������־");

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
        /// ��ȡ��ѯ�����¼��
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
