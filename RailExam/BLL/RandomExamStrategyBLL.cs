using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
   public class RandomExamStrategyBLL
    {
       private static readonly RandomExamStrategyDAL dal = new RandomExamStrategyDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

       public void AddRandomExamStrategy(RandomExamStrategy randomExamStrategy)
        {
             dal.AddRandomExamStrategy(randomExamStrategy);
        }

       public int UpdateRandomExamStrategy(RandomExamStrategy randomExamStrategy)
        {
            return dal.UpdateRandomExamStrategy(randomExamStrategy);
        }

       public int DeleteRandomExamStrategy(int RandomExamStrategyID)
        {
            return dal.DeleteRandomExamStrategy(RandomExamStrategyID);
        }

       public RandomExamStrategy GetRandomExamStrategy(int ExamStrategyId)
        {
            return dal.GetRandomExamStrategy(ExamStrategyId);
        }

	   public IList<RandomExamStrategy> GetRandomExamStrategy(int randomExamID, int rangeType, int rangeID)
	   {
		   return dal.GetRandomExamStrategy(randomExamID, rangeType, rangeID);
	   }

       public IList<RandomExamStrategy> GetRandomExamStrategys(int SubjectID)
        {
            return dal.GetRandomExamStrategys(SubjectID);
        }

       public IList<RandomExamStrategy> GetTotalRandomExamStrategys(int SubjectID)
       {
           return dal.GetTotalRandomExamStrategys(SubjectID);
       }


	   public IList<RandomExamStrategy> GetRandomExamStrategyBySubjectIDAndRangeID(int SubjectID, int rangeID,int rangeType)
	   {
		   return dal.GetRandomExamStrategyBySubjectIDAndRangeID(SubjectID,rangeID,rangeType);
	   }

       public IList<RandomExamStrategy> GetRandomExamStrategysByExamID(int RandomExamID)
       {
           return dal.GetRandomExamStrategysByExamID(RandomExamID);
       }


    }
}
