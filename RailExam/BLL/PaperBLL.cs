using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class PaperBLL
    {
        private SystemLogBLL objLogBll = new SystemLogBLL();
        public int GetRecordCount()
        {
            return  dal.RecordCount;
        }

        private static readonly PaperDAL dal = new PaperDAL();

        public IList<Paper> GetPaperByCategoryID(int CategoryID)
        {
            IList<Paper> paperList = dal.GetPaperByCategoryID(CategoryID);
            return paperList;
        }

        public IList<Paper> GetPaperByCategoryID(int CategoryID, int CreateMode, string PaperName, string CreatePerson)
        {
            IList<Paper> paperList = dal.GetPaperByCategoryID(CategoryID, CreateMode, PaperName, CreatePerson);
            return paperList;
        }

        public IList<Paper> GetPaperByCategoryIDPath(string idPath, int createMode, string paperName, string createPerson,int OrgId)
        {
            OrganizationBLL objOrgBll = new OrganizationBLL();
            IList<Paper> paperList = dal.GetPaperByCategoryIDPath(idPath, createMode, paperName, createPerson, OrgId);
            foreach (Paper paper in paperList)
            {
                paper.StationName = objOrgBll.GetOrganization(paper.OrgId).ShortName;
            }
            return paperList;
        }



        public IList<Paper> GetPaperByPaperId(int PaperId)
        {
            IList<Paper> paperList = dal.GetPaperByPaperId(PaperId);
            return paperList;
        }

        public IList<Paper> GetTopPapers()
        {
            IList<Paper> paperList = dal.GetTopPapers();
            return paperList;
        }
        

        public IList<Paper> GetTaskPapers(string paperName, int organizationId, int categoryId, string createPerson, 
            int statusId, int currentPageIndex, int pageSize, string orderBy)
        {
            return dal.GetTaskPapers(paperName, organizationId, categoryId, createPerson,
                statusId, currentPageIndex, pageSize, orderBy);
        }

        public Paper GetPaper(int PaperId)
        {
            return dal.GetPaper(PaperId);
        }

        public int AddPaper(Paper paper)
        {
            objLogBll.WriteLog("ÐÂÔöÊÔ¾í£º"¡¡+ paper.PaperName);
            return dal.AddPaper(paper);
        }

        public void UpdatePaper(Paper paper)
        {
            objLogBll.WriteLog("ÐÞ¸ÄÊÔ¾í£º" + paper.PaperName);
            dal.UpdatePaper(paper);
        }

        public void DeletePaper(int PaperId)
        {
            string strName = GetPaper(PaperId).PaperName;
            objLogBll.WriteLog("É¾³ýÊÔ¾í£º" + strName);
            dal.DeletePaper(PaperId);
        }
    }
}
