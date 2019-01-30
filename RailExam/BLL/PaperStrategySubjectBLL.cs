using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class PaperStrategySubjectBLL
    {
        private static readonly PaperStrategySubjectDAL dal = new PaperStrategySubjectDAL();




        public IList<PaperStrategySubject> GetPaperStrategySubjectByPaperStrategyId(int PaperId)
        {

            IList<PaperStrategySubject> PaperList = dal.GetPaperStrategySubjectByPaperStrategyId(PaperId);
            return PaperList;
        }

        public PaperStrategySubject GetPaperStrategySubject(int PaperStrategySubjectId)
        {
            return dal.GetPaperStrategySubject(PaperStrategySubjectId);
        }

        public void AddPaperStrategySubject(PaperStrategySubject PaperStrategySubject)
        {
            dal.AddPaperStrategySubject(PaperStrategySubject);
        }

       

        public void UpdatePaperStrategySubject(int PaperId, int TotalScore, IList<PaperStrategySubject> PaperStrategySubjects)
        {
            dal.UpdatePaperStrategySubject(PaperId, TotalScore, PaperStrategySubjects);
        }


        public void DeletePaperStrategySubject(int PaperId)
        {
            dal.DeletePaperStrategySubject(PaperId);
        }
    }
}
