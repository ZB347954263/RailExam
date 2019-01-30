using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
   public class TaskResultAnswerBLL
   {
       public static TaskResultAnswerDAL dal = new TaskResultAnswerDAL();

       /// <summary>
       /// 按考试结果获取答案
       /// </summary>
       /// <param name="examResultId"></param>
       /// <returns>答案集合</returns>
       public IList<TaskResultAnswer> GetTaskResultAnswers(int examResultId)
       {
           return dal.GetTaskResultAnswers(examResultId);
       }
    }
}
