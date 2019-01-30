using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class ExamResultUpdateBLL
    {
        private static ExamResultUpdateDAL dal = new ExamResultUpdateDAL();

        public IList<ExamResultUpdate> GetExamResultUpdates()
        {
            return dal.GetExamResultUpdates();
        }

        public ExamResultUpdate GetExamResultUpdate(int ExamResultUpdateId)
        {
            return dal.GetExamResultUpdate(ExamResultUpdateId);
        }

        public void UpdateExamResultUpdate(int ExamResultUpdateId, string updatecause, string memo)
        {
            dal.UpdateExamResultUpdate(ExamResultUpdateId, updatecause, memo);
        }
    }
}
