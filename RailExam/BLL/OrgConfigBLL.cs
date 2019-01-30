using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
    public class OrgConfigBLL
    {
        private static readonly OrgConfigDAL¡¡dal = new OrgConfigDAL();

        public void  AddOrgConfig(OrgConfig orgconfig)
        {
            dal.AddOrgConfig(orgconfig);
        }

        public void UpdateOrgConfig(OrgConfig orgconfig)
        {
            dal.UpdateOrgConfig(orgconfig);
        }

        public OrgConfig GetOrgConfig()
        {
            return dal.GetOrgConfig();
        }
    }
}
