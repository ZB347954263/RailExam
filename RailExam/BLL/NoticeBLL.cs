using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using DSunSoft.Web.Global;

namespace RailExam.BLL
{
    /// <summary>
    /// 业务逻辑：通知发布
    /// </summary>
    public class NoticeBLL
    {
        private static readonly NoticeDAL dal = new NoticeDAL();

        public IList<Notice> GetNotices(int noticeID, string title, string content, int importanceID, string importanceName,
            int dayCount, int createPersonID, string employeeName, string orgName, DateTime createTime, string receiveOrgIDS,
            string receiveEmployeeIDS, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<Notice> noticeList = dal.GetNotices(noticeID, title, content, importanceID, importanceName, dayCount,
                createPersonID, employeeName, orgName, createTime, receiveOrgIDS, receiveEmployeeIDS,
                memo, startRowIndex, maximumRows, orderBy);

            return noticeList;
        }

        public IList<Notice> GetNotices(string title, int importanceID, string OrgName, string EmployeeName,
            DateTime beginTime, DateTime endTime,bool isAdmin, Int32 empolyeeID) 
        {
            IList<Notice> noticeList = dal.GetNotices(title, importanceID, OrgName, EmployeeName, beginTime, endTime, isAdmin, empolyeeID);

            return noticeList;
        }

        public IList<Notice> GetNotices(int nNum)
        {
            IList<Notice> noticeList = dal.GetNotices(nNum);

            return noticeList;
        }

        public IList<Notice> GetNotices(int nNum, string examineeId, string OrgId)
        {
            IList<Notice> noticeList = dal.GetNotices(nNum, examineeId, OrgId);

            return noticeList;
        }



        public IList<Notice> GetNotices1()
        {
            IList<Notice> noticeList = dal.GetNotices1();

            return noticeList;
        }

        public Notice GetNotice(int noticeID)
        {
            if (noticeID < 1)
            {
                return null;
            }

            return dal.GetNotice(noticeID);
        }

        public int AddNotice(Notice notice)
        {
            int nNoticeID = dal.AddNotice(notice, SessionSet.EmployeeID);
            if(nNoticeID > 0)
            {
                SystemLogBLL systemLogBLL = new SystemLogBLL();

                systemLogBLL.WriteLog("新增通知");
            }

            return nNoticeID;
        }

        public void UpdateNotice(Notice notice)
        {
            if(dal.UpdateNotice(notice))
            {
                SystemLogBLL systemLogBLL = new SystemLogBLL();

                systemLogBLL.WriteLog("修改通知");
            }
        }

        public void DeleteNotice(Notice notice)
        {
            DeleteNotice(notice.NoticeID);
        }

        public void DeleteNotice(int noticeID)
        {
            if(dal.DeleteNotice(noticeID))
            {
                SystemLogBLL systemLogBLL = new SystemLogBLL();

                systemLogBLL.WriteLog("删除通知");
            }
        }

        public int GetCount(int noticeID, string title, string content, int importanceID, string importanceName,
            int dayCount, int createPersonID, string employeeName, string orgName, DateTime createTime, string receiveOrgIDS, 
            string receiveEmployeeIDS, string memo)
        {
            return dal.RecordCount;
        }
    }
}
