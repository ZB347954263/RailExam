using System;
using System.Collections.Generic;
using System.Text;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class SynchronizeLogBLL
    {
        private static readonly SynchronizeLogDAL dal = new SynchronizeLogDAL();

        public void AddSynchronizeLog(SynchronizeLog obj)
        {
            dal.WriteLog(obj);
        }

        public void UpdateSynchronizeLog(SynchronizeLog obj)
        {
            dal.UpdateSynchronizeLog(obj);
        }

        public IList<SynchronizeLog> GetSynchronizeLogByOrgID(int orgID)
        {
            return dal.GetSynchronizeLogByOrgID(orgID);
        }

        public void DeleteSynchronizeLog(int orgID)
        {
             dal.DeleteSynchronizeLog(orgID);
        }

		public IList<SynchronizeLog> GetSynchronizeLogByOrgIDAndTypeID(int orgID, int typeID)
		{
			return dal.GetSynchronizeLogByOrgIDAndTypeID(orgID, typeID);
		}
    }
}
