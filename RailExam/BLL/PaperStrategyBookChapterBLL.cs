using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class PaperStrategyBookChapterBLL
    {
        private static readonly PaperStrategyBookChapterDAL dal = new PaperStrategyBookChapterDAL();

        public int AddPaperStrategyBookChapter(PaperStrategyBookChapter item)
        {
            return dal.AddPaperStrategyBookChapter(item);
        }

        public int UpdatePaperStrategyBookChapter(PaperStrategyBookChapter item)
        {
            return dal.UpdatePaperStrategyBookChapter(item);
        }

        public int DeletePaperStrategyBookChapter(int PaperStrategyBookChapterId)
        {
            return dal.DeletePaperStrategyBookChapter(PaperStrategyBookChapterId);
        }

        public PaperStrategyBookChapter GetPaperStrategyBookChapter(int paperItemId)
        {
            return dal.GetPaperStrategyBookChapter(paperItemId);
        }

        public IList<PaperStrategyBookChapter> GetItemsByPaperStrategySubjectID(int paperStrategySubjectID)
        {
            return dal.GetItemsByPaperStrategySubjectID(paperStrategySubjectID);
        }

        public int GetBookChapterCount(int PaperStrategyID)
        {
            return dal.GetBookChapterCount(PaperStrategyID);
        }

    }
}
