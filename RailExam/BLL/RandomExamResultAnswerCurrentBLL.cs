using System;
using System.Collections.Generic;
using System.Text;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class RandomExamResultAnswerCurrentBLL
    {
        private static readonly RandomExamResultAnswerCurrentDAL dal = new RandomExamResultAnswerCurrentDAL();

        public int AddExamResultAnswerCurrent(RandomExamResultAnswerCurrent examResultAnswer)
        {
            return dal.AddExamResultAnswerCurrent(examResultAnswer);
        }

        public int AddExamResultAnswerCurrent(IList<RandomExamResultAnswerCurrent> examResultAnswers)
        {
            return dal.AddExamResultAnswerCurrent(examResultAnswers);
        }


        public void UpdateExamResultAnswerCurrent(RandomExamResultAnswerCurrent examResultAnswer)
        {
            dal.UpdateExamResultAnswerCurrent(examResultAnswer);
        }

        public IList<RandomExamResultAnswerCurrent> GetExamResultAnswersCurrent(int randomExamResultId)
        {
            return dal.GetExamResultAnswersCurrent(randomExamResultId);
        }

        public IList<RandomExamResultAnswerCurrent> GetExamResultAnswersCurrentByOrgID(int randomExamResultId, int orgID)
        {
            return dal.GetExamResultAnswersCurrentByOrgID(randomExamResultId, orgID);
        }

        public int UpdateExamResultAnswerCurrent(IList<RandomExamResultAnswerCurrent> examResultAnswers)
        {
            return dal.UpdateExamResultAnswerCurrent(examResultAnswers);
        }

        public void AddExamResultAnswerCurrent(int randomExamResultID,string randomItemIDs)
        {
            dal.AddExamResultAnswerCurrent(randomExamResultID, randomItemIDs);
        }

        public void DeleteExamResultAnswerCurrent(int randomExamResultID)
        {
            dal.DeleteExamResultAnswerCurrent(randomExamResultID);
        }

        public RandomExamResultAnswerCurrent GetExamResultAnswerCurrent(int randomExamResultID, int examItemID)
        {
            return dal.GetExamResultAnswerCurrent(randomExamResultID, examItemID);
        }

        public int AddExamResultAnswerCurrentSave(int randomExamResultID, IList<RandomExamResultAnswerCurrent> examResultAnswers)
        {
            return dal.AddExamResultAnswerCurrentSave(randomExamResultID, examResultAnswers);
        }
    }
}
