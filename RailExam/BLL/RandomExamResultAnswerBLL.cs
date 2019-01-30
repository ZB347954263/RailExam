using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class RandomExamResultAnswerBLL
    {
        private static readonly RandomExamResultAnswerDAL dal = new RandomExamResultAnswerDAL();

        public int AddExamResultAnswer(RandomExamResultAnswer examResultAnswer)
        {
            return dal.AddExamResultAnswer(examResultAnswer);
        }

        public int AddExamResultAnswer(IList<RandomExamResultAnswer> examResultAnswers)
        {
            return dal.AddExamResultAnswer(examResultAnswers);
        }


        public int UpdateExamResultAnswer(RandomExamResultAnswer examResultAnswer)
        {
            return dal.UpdateExamResultAnswer(examResultAnswer);
        }

        public IList<RandomExamResultAnswer> GetExamResultAnswers(int randomExamResultId)
        {
            return dal.GetExamResultAnswers(randomExamResultId);
        }

        public IList<RandomExamResultAnswer> GetExamResultAnswersByOrgID(int randomExamResultId, int orgID)
        {
            return dal.GetExamResultAnswersByOrgID(randomExamResultId, orgID);
        }

		public IList<RandomExamResultAnswer> GetExamResultAnswersStation(int randomExamResultId)
		{
			return dal.GetExamResultAnswersStation(randomExamResultId);
		}

        public int UpdateExamResultAnswer(IList<RandomExamResultAnswer> examResultAnswers)
        {
            return dal.UpdateExamResultAnswer(examResultAnswers);
        }
        public int UpdateExamResultAnswer(int resultId, int itemId, string answer, int hasYear)
        {
            return dal.UpdateExamResultAnswer(resultId, itemId, answer, hasYear);
        }
    }
}
