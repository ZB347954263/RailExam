using System;
using System.Collections.Generic;
using System.Text;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class TrainTypeTaskBLL
    {
        /// <summary>
        /// 内部成员
        /// </summary>
        private static readonly TrainTypeTaskDAL dal = new TrainTypeTaskDAL();

        public void AddTrainTypeTask(TrainTypeTask trainTypeTask)
        {
            dal.AddTrainTypeTask(trainTypeTask);
        }

        public void UpdateTrainTypeTask(TrainTypeTask trainTypeTask)
        {
            dal.UpdateTrainTypeTask(trainTypeTask);
        }

        public void DelTrainTypeTask(int trainTypeID, int paperID)
        {
            dal.DelTrainTypeTask(trainTypeID, paperID);
        }

        public IList<TrainTypeTask> GetTrainTypeTaskByTrainTypeID(int trainTypeID)
        {
            return dal.GetTrainTypeTaskByTrainTypeID(trainTypeID);
        }
    }
}
