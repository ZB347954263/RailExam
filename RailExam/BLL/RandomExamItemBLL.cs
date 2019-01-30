using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
   public class RandomExamItemBLL
    {
       private static readonly RandomExamItemDAL dal = new RandomExamItemDAL();

       public void AddItem(IList<RandomExamItem> Items, int year)
       {
            dal.AddItem(Items,year);
       }

       public RandomExamItem GetRandomExamItem(int randomExaID,int year)
       {
           return dal.GetRandomExamItem(randomExaID,year);
       }

       public IList<RandomExamItem> GetItemsBySubjectId(int SubjectId,int year)
       {
           return dal.GetItemsBySubjectId(SubjectId,year);
       }

       public IList<RandomExamItem> GetItemsByStrategyId(int StrategyId,int year)
       {
           return dal.GetItemsByStrategyId(StrategyId,year);
       }

       public IList<RandomExamItem> GetItems(int SubjectId, int randomExamResultId,int year)
       {
           return dal.GetItems(SubjectId, randomExamResultId,year);
       }

       public IList<RandomExamItem> GetItemsCurrent(int SubjectId, int randomExamResultId,int year)
       {
           return dal.GetItemsCurrent(SubjectId, randomExamResultId,year);
       }

       public IList<RandomExamItem> GetItemsCurrentCheck()
       {
           return dal.GetItemsCurrentCheck();
       }

       public IList<RandomExamItem> GetItemsByOrgID(int SubjectId, int randomExamResultId, int orgID,int year)
       {
           return dal.GetItemsByOrgID(SubjectId, randomExamResultId,orgID,year);
       }

	   public IList<RandomExamItem> GetItemsStation(int SubjectId, int randomExamResultId,  int year)
	   {
		   return dal.GetItemsStation(SubjectId, randomExamResultId, year);
	   }

       public void DeleteItems(int ExamId, int year)
       {
           dal.DeleteItems(ExamId,year);
       }

       public IList<RandomExamItem> GetItemsByParentItemID(int parentItemID, int randomExamID, int year)
       {
           return dal.GetItemsByParentItemID(parentItemID,randomExamID, year);
       }
    }
}
