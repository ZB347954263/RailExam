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
        /// 内部成员
        /// </summary>
        private static readonly TrainPlanDAL dal = new TrainPlanDAL();

        /// <summary>
        /// 新增培训类别
        /// </summary>
        /// <param name="trainplan"></param>
        public void AddTrainPlan(TrainPlan trainplan)
        {
            dal.AddTrainPlan(trainplan);
        }

        /// <summary>
        /// 删除培训类别
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
        /// 更新培训类别
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
        /// 根据培训计划ID返回唯一的培训计划信息
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
