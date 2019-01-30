using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class PaperStrategyBLL
    {
        private static readonly PaperStrategyDAL dal = new PaperStrategyDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

        public IList<PaperStrategy> GetPaperStrategy(int PaperStrategyId, int PaperCategoryId, bool IsRandomOrder, bool SingleAsMultiple,
              string PaperStrategyName,int StrategyMode, string description, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<PaperStrategy> paperStrategyList = dal.GetPaperStrategy(PaperStrategyId, PaperCategoryId, IsRandomOrder, SingleAsMultiple,
                PaperStrategyName, StrategyMode, description, startRowIndex, maximumRows, orderBy);

            return paperStrategyList;
        }

        public IList<PaperStrategy> GetPaperStrategyByPaperCategoryIDPath(string PaperCategoryIDPath)
        {
            IList<PaperStrategy> paperStrategyList = dal.GetPaperStrategyByPaperCategoryIDPath(PaperCategoryIDPath);
            return paperStrategyList;
        }

        public IList<PaperStrategy> GetPaperStrategyByPaperCategoryId(int PaperCategoryId)
        {
            IList<PaperStrategy> paperStrategyList = dal.GetPaperStrategyByPaperCategoryId(PaperCategoryId);
            return paperStrategyList;
        }

        public IList<PaperStrategy> GetPaperStrategyByPaperCategoryId(int PaperCategoryId, int StrategyMode)
        {
            IList<PaperStrategy> paperStrategyList = dal.GetPaperStrategyByPaperCategoryId(PaperCategoryId, StrategyMode);
            return paperStrategyList;
        }

        public PaperStrategy GetPaperStrategy(int PaperStrategyId)
        {
            return dal.GetPaperStrategy(PaperStrategyId);
        }

        public int AddPaperStrategy(PaperStrategy paperStrategy)
        {
            objLogBll.WriteLog("新增组卷策略："　+ paperStrategy.PaperStrategyName);
            return dal.AddPaperStrategy(paperStrategy);
        }

        public void UpdatePaperStrategy(PaperStrategy paperStrategy)
        {
            objLogBll.WriteLog("修改组卷策略：" + paperStrategy.PaperStrategyName);
            dal.UpdatePaperStrategy(paperStrategy);
        }

        public void DeletePaperStrategy(PaperStrategy paperStrategy)
        {
            DeletePaperStrategy(paperStrategy.PaperCategoryId);
        }

        public void DeletePaperStrategy(int PaperStrategyId)
        {
            string strName = GetPaperStrategy(PaperStrategyId).PaperStrategyName;
            objLogBll.WriteLog("删除组卷策略：" + strName);
            dal.DeletePaperStrategy(PaperStrategyId);
        }
    }
}
