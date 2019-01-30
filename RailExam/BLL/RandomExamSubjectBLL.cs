using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;

namespace RailExam.BLL
{
    public class RandomExamSubjectBLL
    {
        private static readonly RandomExamSubjectDAL dal = new RandomExamSubjectDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();


        public IList<RandomExamSubject> GetRandomExamSubjectByRandomExamId(int RandomExamId)
        {
            IList<RandomExamSubject> PaperList = dal.GetRandomExamSubjectByRandomExamId(RandomExamId);
            return PaperList;
        }

        public RandomExamSubject GetRandomExamSubject(int RandomExamSubjectId)
        {
            return dal.GetRandomExamSubject(RandomExamSubjectId);
        }

        public int AddRandomExamSubject(RandomExamSubject RandomExamSubject)
        {
            return dal.AddRandomExamSubject(RandomExamSubject);
        }

        public void UpdateRandomExamSubject(RandomExamSubject RandomExamSubject)
        {
            dal.UpdateRandomExamSubject(RandomExamSubject);
        }

        public void UpdateRandomExamSubject(IList<RandomExamSubject> RandomExamSubjects)
        {
            dal.UpdateRandomExamSubject(RandomExamSubjects);
        }

        public void DeleteRandomExamSubject(int RandomExamSubjectId)
        {
            dal.DeleteRandomExamSubject(RandomExamSubjectId);
        }
    }
}
