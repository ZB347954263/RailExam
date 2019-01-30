using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class ExamStatusBLL
    {
        private static ExamStatusDAL dal = new ExamStatusDAL();

        public IList<ExamStatus> GetExamStatuses()
        {
            return dal.GetExamStatuses();   // ≤‚ ‘
        }
    }
}
