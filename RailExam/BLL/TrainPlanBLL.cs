using System;
using System.Collections.Generic;
using System.Text;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class TrainPlanBLL
    {
        /// <summary>
        /// �ڲ���Ա
        /// </summary>
        private static readonly TrainPlanDAL dal = new TrainPlanDAL();

        /// <summary>
        /// ������ѵ���
        /// </summary>
        /// <param name="trainplan"></param>
        public void AddTrainPlan(TrainPlan trainplan)
        {
            dal.AddTrainPlan(trainplan);
        }

        /// <summary>
        /// ɾ����ѵ���
        /// </summary>
        /// <param name="TrainPlanID"></param>
        public void DeleteTrainPlan(int TrainPlanID)
        {
            dal.DeleteTrainPlan(TrainPlanID);
        }

        public void DeleteTrainPlan(TrainPlan trainplan)
        {
            dal.DeleteTrainPlan(trainplan.TrainPlanID);
        }

        /// <summary>
        /// ������ѵ���
        /// </summary>
        /// <param name="trainplan"></param>
        public void UpdateTrainPlan(TrainPlan trainplan)
        {
            dal.UpdateTrainPlan(trainplan);
        }

        public IList<TrainPlan> GetTrainPlanInfo(int trainPlanID,
                                                string trainName,
                                                string trainContent,
                                                DateTime beginDate,
                                                DateTime endDate,
                                                bool hasExam,
                                                string examForm,
                                                int statusID,
                                                string memo,
                                                int startRowIndex,
                                                int maximumRows,
                                                string orderBy)
        {
            return dal.GetTrainPlanInfo(trainPlanID,
                                        trainName,
                                        trainContent,
                                        beginDate,
                                        endDate,
                                        hasExam,
                                        examForm,
                                        statusID,
                                        memo,
                                        startRowIndex,
                                        maximumRows,
                                        orderBy);
        }


        /// <summary>
        /// ������ѵ�ƻ�ID����Ψһ����ѵ�ƻ���Ϣ
        /// </summary>
        /// <param name="TrainPlanID"></param>
        /// <returns></returns>
        public TrainPlan GetTrainPlanInfo(int TrainPlanID)
        {
            return dal.GetTrainPlanInfo(TrainPlanID);
        }

        public IList<TrainPlan> GetAllTrainPlanInfo()
        {
            return dal.GetAllTrainPlanInfo();
        }
    }
}
