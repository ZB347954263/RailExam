using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
   public  class PaperSubjectBLL
    {
       private static readonly PaperSubjectDAL dal = new PaperSubjectDAL();




       public IList<PaperSubject> GetPaperSubjectByPaperId(int PaperId)
        {
            IList<PaperSubject> PaperList = dal.GetPaperSubjectByPaperId(PaperId);
            return PaperList;
        }

       /// <summary>
       /// 根据试卷ID查询试卷大题（路局查询站段）
       /// </summary>
       /// <param name="PaperId"></param>
       /// <param name="orgID"></param>
       /// <returns></returns>
       public IList<PaperSubject> GetPaperSubjectByPaperIdByOrgID(int PaperId,int orgID)
       {
           IList<PaperSubject> PaperList = dal.GetPaperSubjectByPaperIdByOrgID(PaperId,orgID);
           return PaperList;
       }

       public PaperSubject GetPaperSubject(int PaperSubjectId)
       {
           return dal.GetPaperSubject(PaperSubjectId);
       }

       public int AddPaperSubject(PaperSubject PaperSubject)
        {
          return  dal.AddPaperSubject(PaperSubject);
        }

       public void UpdatePaperSubject(PaperSubject PaperSubject)
       {
           dal.UpdatePaperSubject(PaperSubject);
       }

       public void UpdatePaperSubject(int PaperId,   IList<PaperSubject> PaperSubjects)
       {
           dal.UpdatePaperSubject(PaperId,   PaperSubjects);
       }


       public void DeletePaperSubject(int PaperId)
        {
            dal.DeletePaperSubject(PaperId);
        }
    }
}
