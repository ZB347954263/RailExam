using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;
using RailExam.DAL;


namespace RailExam.BLL
{
    public class OrgImportBLL
    {
        private static readonly OrgImportDAL dal = new OrgImportDAL();

        public IList<OrgImport> GetOrgImport(int orgID)
        {
            return dal.GetOrgImport(orgID);
        }

		public IList<OrgImport> GetOrgImport(Database db,DbTransaction trans,int orgID)
		{
			return dal.GetOrgImport(db,trans,orgID);
		}
    }
}
