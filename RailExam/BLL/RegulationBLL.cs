using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class RegulationBLL
    {
        private static readonly RegulationDAL dal = new RegulationDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

        public IList<Regulation> GetRegulations(int regulationID, int categoryID, string regulationNo,
                string regulationName, string version, string fileNo, string titleRemark,
                DateTime issueDate, DateTime executeDate, int status, string url,
                string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<Regulation> regulationList = dal.GetRegulations(regulationID, categoryID, regulationNo,
                regulationName, version, fileNo, titleRemark, issueDate, executeDate, status, url,
                memo, startRowIndex, maximumRows, orderBy);

            return regulationList;
        }

        public IList<Regulation> GetRegulations()
        {
            IList<Regulation> regulationList = dal.GetRegulations();

            return regulationList;
        }

        public IList<Regulation> GetRegulations(string regulationName, string regulationNo, int status)
        {
            IList<Regulation> regulationList = dal.GetRegulations(regulationName, regulationNo, status);

            return regulationList;
        }

        public IList<Regulation> GetRegulationsByCategoryIDPath(string strCategoryIDPath)
        {
            IList<Regulation> regulationList = dal.GetRegulationsByCategoryIDPath(strCategoryIDPath);

            return regulationList;
        }

        public Regulation GetRegulationByRegulationID(int regulationID)
        {
            if(regulationID < 1)
            {
                return null;
            }

            return dal.GetRegulationByRegulationID(regulationID);
        }

        public int AddRegulation(Regulation regulation)
        {
            objLogBll.WriteLog("新增政策法规“"+ regulation.RegulationName +"”基本信息");
            return dal.AddRegulation(regulation);
        }

        public void UpdateRegulation(Regulation regulation)
        {
            objLogBll.WriteLog("修改政策法规“" + regulation.RegulationName + "”基本信息");
            dal.UpdateRegulation(regulation);
        }

        public void DeleteRegulation(int regulationID)
        {
            string strName = GetRegulationByRegulationID(regulationID).RegulationName;
            objLogBll.WriteLog("删除政策法规“" + strName + "”基本信息");
            dal.DeleteRegulation(regulationID);
        }

        public int GetCount(int regulationID, int categoryID, string regulationNo,
                string regulationName, string version, string fileNo, string titleRemark,
                DateTime issueDate, DateTime executeDate, int status, string url, string memo)
        {
            return dal.RecordCount;
        }
    }
}
