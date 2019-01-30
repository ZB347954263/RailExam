using System;
using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class ExamResultAnswerBLL
    {
        private static readonly ExamResultAnswerDAL dal = new ExamResultAnswerDAL();

        public int AddExamResultAnswer(ExamResultAnswer examResultAnswer)
        {
            return dal.AddExamResultAnswer(examResultAnswer);
        }

        public int DeleteExamResultAnswers(int examResultAnswerId)
        {
            return dal.DeleteExamResultAnswers(examResultAnswerId);
        }

        public int DeleteExamResultAnswer(int examResultAnswerId, int paperItemId)
        {
            return dal.DeleteExamResultAnswer(examResultAnswerId, paperItemId);
        }

        public int UpdateExamResultAnswer(ExamResultAnswer examResultAnswer)
        {
            return dal.UpdateExamResultAnswer(examResultAnswer);
        }

        public ExamResultAnswer GetExamResultAnswer(int examResultId, int paperItemId)
        {
            return dal.GetExamResultAnswer(examResultId, paperItemId);
        }

        public IList<ExamResultAnswer> GetExamResultAnswers(int examResultId)
        {
            return dal.GetExamResultAnswers(examResultId);
        }
        /// <summary>
        /// 查询给定考卷的所有考生结果答案(路局查询站段)
        /// </summary>
        /// <param name="examResultId"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>        
        public IList<ExamResultAnswer> GetExamResultAnswersByOrgID(int examResultId,int orgID)
        {
            return dal.GetExamResultAnswersByOrgID(examResultId, orgID);
        }

        public int GetCount()
        {
            return dal.RecordCount;
        }
    }
}
