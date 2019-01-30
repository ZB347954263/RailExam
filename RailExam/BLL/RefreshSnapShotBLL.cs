using System;
using System.Collections.Generic;
using System.Text;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class RefreshSnapShotBLL
    {
        private RefreshSnapShotDAL dal = new RefreshSnapShotDAL();

        public void RefreshSnapshot()
        {
            dal.RefreshSnapShot();
        }

        public void RefreshSnapShot(string strPro, int typeID)
        {
            dal.RefreshSnapShot(strPro,typeID);
        }

        public bool IsExistsRefreshSnapShot(string proName,string objectType)
        {
            return dal.IsExistsRefreshSnapShot(proName,objectType);
        }

        public void CreateSnapShot(int orgID, int typeId)
        {
            dal.CreateSnapShot(orgID, typeId);
        }

        public void RefreshOrg(int hours)
        {
            dal.RefreshOrg(hours);
        }

        public void DropSnapShot()
        {
            dal.DropSnapshot();
        }

        public IList<Book> GetStationBook()
        {
            return dal.GetStationBook();
        }

        public IList<Courseware> GetStationCourseware()
        {
            return dal.GetStationCourseware();
        }
    }
}
