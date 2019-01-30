using System;
using System.Collections.Generic;
using System.Text;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class TrainEmployeeBLL
    {
        private static readonly TrainEmployeeDAL dal = new TrainEmployeeDAL();

        public void AddTrainEmployee(TrainEmployee trainEmployee)
        {
            dal.AddTrainEmployee(trainEmployee);
        }

        public void UpdateTrainEmployee(TrainEmployee trainEmployee)
        {
            dal.UpdateTrainEmployee(trainEmployee);
        }

        public TrainEmployee GetTrainEmployeeByEmployeeID(int employeeID)
        {
            return dal.GetTrainEmployeeByEmployee(employeeID);
        }
    }
}
