using System;
using System.Collections.Generic;
using System.Text;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class TrainTypeExerciseBLL
    {
        /// <summary>
        /// 内部成员
        /// </summary>
        private static readonly TrainTypeExerciseDAL dal = new TrainTypeExerciseDAL();

        public void AddTrainTypeExercise(TrainTypeExercise trainTypeExercise)
        {
            dal.AddTrainTypeExercise(trainTypeExercise);
        }

        public void UpdateTrainTypeExercise(TrainTypeExercise trainTypeExercise)
        {
            dal.UpdateTrainTypeExercise(trainTypeExercise);
        }

        public void DelTrainTypeExercise(int trainTypeID,int paperID)
        {
            dal.DelTrainTypeExercise(trainTypeID, paperID);
        }

        public IList<TrainTypeExercise> GetTrainTypeExerciseByTrainTypeID(int trainTypeID)
        {
            return dal.GetTrainTypeExerciseByTrainTypeID(trainTypeID);
        }
    }
}
