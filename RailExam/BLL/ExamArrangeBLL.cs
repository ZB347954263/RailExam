using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
   public class ExamArrangeBLL
    {
       private static readonly ExamArrangeDAL dal = new ExamArrangeDAL();

       public int AddExamArrange(ExamArrange examArrange)
       {
           return dal.AddExamArrange(examArrange);
       }

       public int DeleteExamArrange(int ExamArrangeId)
       {
           return dal.DeleteExamArrange(ExamArrangeId);
       }

       public void UpdateExamArrange(ExamArrange examArrange)
       {
            dal.UpdateExamArrange(examArrange);
       }

       public void UpdateExamUser(int examID, string strUserIds)
       {
            dal.UpdateExamUser(examID, strUserIds);
       }

       public void UpdateExamArrangeUser(int ExamArrangeId, string strUserIds)
       {
           dal.UpdateExamArrangeUser(ExamArrangeId, strUserIds);
       }

       public void UpdateExamArrangeJudge(int ExamArrangeId, string strJudgeIds)
       {
           dal.UpdateExamArrangeJudge(ExamArrangeId, strJudgeIds);
       }

       public void UpdateExamArrange(IList<ExamArrange> examArranges)
       {
           dal.UpdateExamArrange(examArranges);
       }

       public ExamArrange GetExamArrange(int ExamArrangeId)
       {
           return dal.GetExamArrange(ExamArrangeId);
       }

       public IList<ExamArrange> GetExamArrangesByExamId(int ExamId)
       {
           return dal.GetExamArrangesByExamId(ExamId);
       }
    }
}
