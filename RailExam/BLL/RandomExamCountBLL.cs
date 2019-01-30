using System;
using System.Collections.Generic;
using System.Text;
using RailExam.DAL;

namespace RailExam.BLL
{
    public class RandomExamCountBLL
    {
        private static readonly RandomExamCountDAL dal = new RandomExamCountDAL();

        public void  UpdateRandomExamCount(int employeeID, int randomExamID)
        {
            dal.UpdateRandomExamCount(employeeID,randomExamID);
        }

        public int GetRandomExamCount(int employeeID,int randomExamID)
        {
            return dal.GetRandomExamCount(employeeID, randomExamID);
        }
    }
}
