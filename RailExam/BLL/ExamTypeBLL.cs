using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
  public  class ExamTypeBLL
    {
      private static readonly ExamTypeDAL dal = new ExamTypeDAL();

      public IList<ExamType> GetExamTypes()
      {
          return dal.GetExamTypes();
      }

    }
}
