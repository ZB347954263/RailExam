using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class ExamResultStatusBLL
    {
        private static ExamResultStatusDAL dal = new ExamResultStatusDAL();

        public IList<ExamResultStatus> GetExamResultStatuses()
        {
            return dal.GetExamResultStatuses();
        }

      
        public IList<ExamResultStatus> GetExamResultStatuses(bool bForSearchUse)
        {
            IList<ExamResultStatus> statuses = GetExamResultStatuses();

            if (bForSearchUse)
            {
                ExamResultStatus status = new ExamResultStatus();

                status.StatusName = "-«Î—°‘Ò-";
                status.ExamResultStatusId = -1;
                statuses.Insert(0, status);
            }

            return statuses;
        }
    }
}
