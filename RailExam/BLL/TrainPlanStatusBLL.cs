using System;
using System.Collections.Generic;
using RailExam.DAL;
using RailExam.Model;
namespace RailExam.BLL
{
    public class TrainPlanStatusBLL
    {
        /// <summary>
        /// �ڲ���Ա
        /// </summary>
        private static readonly TrainPlanStatusDAL dal = new TrainPlanStatusDAL();

        public IList<TrainPlanStatus> GetAllTrainPlanStatusInfo()
        {
            return dal.GetAllTrainPlanStatusInfo();
        }

        public TrainPlanStatus GetTrainPlanStatusInfo(int trainPlanStatusID)
        {
            return dal.GetTrainPlanStatusInfo(trainPlanStatusID);
        }
    }
}
