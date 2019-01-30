using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class PaperStrategyItemCategoryBLL
    {

        private static readonly PaperStrategyItemCategoryDAL dal = new PaperStrategyItemCategoryDAL();


        public int AddPaperStrategyItemCategory(PaperStrategyItemCategory item)
        {
            return dal.AddPaperStrategyItemCategory(item);
        }

        public int UpdatePaperStrategyItemCategory(PaperStrategyItemCategory item)
        {
            return dal.UpdatePaperStrategyItemCategory(item);
        }

        public int DeletePaperStrategyItemCategory(int PaperStrategyItemCategoryId)
        {
            return dal.DeletePaperStrategyItemCategory(PaperStrategyItemCategoryId);
        }

        public PaperStrategyItemCategory GetPaperStrategyItemCategory(int paperItemId)
        {
            return dal.GetPaperStrategyItemCategory(paperItemId);
        }

        public IList<PaperStrategyItemCategory> GetItemsByPaperSubjectId(int paperSubjectId)
        {
            return dal.GetItemsByPaperSubjectId(paperSubjectId);
        }

        public int GetItemCount(int PaperStrategyID)
        {
            return dal.GetItemCount(PaperStrategyID);
        }


    }
}
