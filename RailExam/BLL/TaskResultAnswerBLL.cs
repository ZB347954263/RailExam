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
       /// �����Խ����ȡ��
       /// </summary>
       /// <param name="examResultId"></param>
       /// <returns>�𰸼���</returns>
       public IList<TaskResultAnswer> GetTaskResultAnswers(int examResultId)
       {
           return dal.GetTaskResultAnswers(examResultId);
       }
    }
}
