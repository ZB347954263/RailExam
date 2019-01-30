using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class AssistBookUpdateBLL
    {
        private static readonly AssistBookUpdateDAL dal = new AssistBookUpdateDAL();

        public IList<AssistBookUpdate> GetAssistBookUpdateByChapterID(int chapterID, int assistBookID)
        {
            return dal.GetAssistBookUpdateByChapterID(chapterID, assistBookID); ;
        }

        public IList<AssistBookUpdate> GetAssistBookUpdateBySelect(int assistbookID, string bookname, string person, DateTime begin, DateTime end, string updateobject)
        {
            return dal.GetAssistBookUpdateBySelect(assistbookID, bookname, person, begin, end, updateobject);
        }

        public AssistBookUpdate GetAssistBookUpdate(int assistBookUpdateID)
        {
            return dal.GetAssistBookUpdate(assistBookUpdateID);
        }

        public void AddAssistBookUpdate(AssistBookUpdate BookUpdate)
        {
            dal.AddAssistBookUpdate(BookUpdate);
        }

        public void UpdateAssistBookUpdate(AssistBookUpdate BookUpdate)
        {
            dal.UpdateAssistBookUpdate(BookUpdate);
        }

        public void DeleteAssistBookUpdate(int BookUpdateID)
        {
            dal.DeleteAssistBookUpdate(BookUpdateID);
        }
    }
}
