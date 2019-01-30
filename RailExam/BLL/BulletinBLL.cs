using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using DSunSoft.Web.Global;

namespace RailExam.BLL
{
    /// <summary>
    /// 业务逻辑：信息公告
    /// </summary>
    public class BulletinBLL
    {
        private static readonly BulletinDAL dal = new BulletinDAL();

        public IList<Bulletin> GetBulletins(int bulletinID, string title, string content, int importanceID,
            string importanceName, int dayCount, int createPersonID, string employeeName, string orgName,
            DateTime createTime, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<Bulletin> bulletinList = dal.GetBulletins(bulletinID, title, content, importanceID,
                importanceName, dayCount, createPersonID, employeeName, orgName, createTime, memo,
                startRowIndex, maximumRows, orderBy);

            return bulletinList;
        }

        public IList<Bulletin> GetBulletins(string title, int importanceID, string OrgName, string EmployeeName,
            DateTime beginTime, DateTime endTime, bool IsAdmin, int EmployeeID)
        {
            IList<Bulletin> bulletinList = dal.GetBulletins(title, importanceID, OrgName, EmployeeName, beginTime, endTime, IsAdmin, EmployeeID);

            return bulletinList;
        }

        public IList<Bulletin> GetBulletins(int nNum)
        {
            IList<Bulletin> bulletinList = dal.GetBulletins(nNum);

            return bulletinList;
        }

        public IList<Bulletin> GetBulletins1()
        {
            IList<Bulletin> bulletinList = dal.GetBulletins1();

            return bulletinList;
        }

        public Bulletin GetBulletin(int bulletinID)
        {
            if (bulletinID < 1)
            {
                return null;
            }

            return dal.GetBulletin(bulletinID);
        }

        public int AddBulletin(Bulletin bulletin)
        {
            int nBulletinID = dal.AddBulletin(bulletin, SessionSet.EmployeeID);
            if(nBulletinID > 0)
            {
                SystemLogBLL systemLogBLL = new SystemLogBLL();

                systemLogBLL.WriteLog("添加的信息公告ID为：" + nBulletinID.ToString());
            }

            return nBulletinID;
        }

        public void UpdateBulletin(Bulletin bulletin)
        {
            if(dal.UpdateBulletin(bulletin))
            {
                SystemLogBLL systemLogBLL = new SystemLogBLL();

                systemLogBLL.WriteLog("更新的信息公告ID为：" + bulletin.BulletinID.ToString());
            }
        }

        public void DeleteBulletin(Bulletin bulletin)
        {
            DeleteBulletin(bulletin.BulletinID);
        }

        public void DeleteBulletin(int bulletinID)
        {
            if(dal.DeleteBulletin(bulletinID))
            {
                SystemLogBLL systemLogBLL = new SystemLogBLL();

                systemLogBLL.WriteLog("删除的信息公告ID为：" + bulletinID.ToString());
            }
        }

        public int GetCount(int bulletinID, string title, string content, int importanceID, string importanceName,
            int dayCount, int createPersonID, string employeeName, string orgName, DateTime createTime, string memo)
        {
            return dal.RecordCount;
        }
    }
}
