using System;
using System.Collections.Generic;
using System.Text;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class RandomExamComputerServerBLL
    {
        private static readonly RandomExamComputerServerDAL dal = new RandomExamComputerServerDAL();

        public RandomExamComputerServer GetRandomExamComputerServer(int examId,int serverNo)
        {
            return dal.GetRandomExamComputerServer(examId,serverNo);
        }
    }
}
