using System;
using System.Collections.Generic;
using System.Text;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class TrainStandardBLL
    {
        /// <summary>
        /// �ڲ���Ա
        /// </summary>
        private static readonly TrainStandardDAL dal = new TrainStandardDAL();

        /// <summary>
        /// ������ѵ�淶
        /// </summary>
        /// <param name="trainStandard"></param>
        public void AddTrainStandard(TrainStandard trainStandard)
        {
            dal.AddTrainStandard(trainStandard);
        }

        /// <summary>
        /// ɾ����ѵ�淶
        /// </summary>
        /// <param name="TrainStandardID"></param>
        public void DeleteTrainStandard(int TrainStandardID)
        {
            dal.DeleteTrainStandard(TrainStandardID);
        }

        public void DeleteTrainStandard(TrainStandard trainStandard)
        {
            dal.DeleteTrainStandard(trainStandard.TrainStandardID);
        }

        /// <summary>
        /// ������ѵ�淶
        /// </summary>
        /// <param name="trainStandard"></param>
        public void UpdateTrainStandard(TrainStandard trainStandard)
        {
            dal.UpdateTrainStandard(trainStandard);
        }

        public IList<TrainStandard> GetTrainStandardInfo(int trainStandardID,
                                                 int postID,
                                                 int typeID,
                                                 string trainTime,
                                                 string trainContent,
                                                 string trainForm,
                                                 string examForm,
                                                 string description,
                                                 string memo,
                                               int startRowIndex,
                                               int maximumRows,
                                               string orderBy)
        {
            return dal.GetTrainStandardInfo(trainStandardID,
                                         postID,
                                         typeID,
                                         trainTime,
                                         trainContent,
                                         trainForm,
                                         examForm,
                                         description,
                                         memo,
                                         startRowIndex,
                                         maximumRows,
                                         orderBy);
        }

        /// <summary>
        /// ������ѵ�淶IDȷ��Ψһ����ѵ�淶
        /// </summary>
        /// <param name="trainStandardID"></param>
        /// <returns></returns>
        public TrainStandard GetTrainStandardInfo(int trainStandardID)
        {
            return dal.GetTrainStandardInfo(trainStandardID);
        }

        /// <summary>
        /// ������ѵ��λID����ѵ���IDȷ��Ψһ����ѵ�淶
        /// </summary>
        /// <param name="postID"></param>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public TrainStandard GetTrainStandardInfo(int postID,int typeID)
        {
            return dal.GetTrainStandardInfo(postID, typeID);
        }
    }
}
