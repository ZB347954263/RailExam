using System;
using System.Collections.Generic;
using System.Text;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class TrainCourseCoursewareBLL
    {
        /// <summary>
        /// �ڲ���Ա
        /// </summary>
        private static readonly TrainCourseCoursewareDAL dal = new TrainCourseCoursewareDAL();

        /// <summary>
        /// ������ѵ�γ̵Ŀμ���Ϣ
        /// </summary>
        /// <param name="trainCourseCourseware"></param>
        public void AddTrainCourseCourseware(TrainCourseCourseware trainCourseCourseware)
        {
            dal.AddTrainCourseCourseware(trainCourseCourseware);
        }

        /// <summary>
        /// ɾ����ѵ�γ̵Ŀμ���Ϣ
        /// </summary>
        /// <param name="trainCourseCoursewareID"></param>
        public void DeleteTrainCourseCourseware(int trainCourseCoursewareID)
        {
            dal.DeleteTrainCourseCourseware(trainCourseCoursewareID);
        }

        public void DeleteTrainCourseCourseware(TrainCourseCourseware trainCourseCourseware)
        {
            dal.DeleteTrainCourseCourseware(trainCourseCourseware.CoursewareID);
        }

        /// <summary>
        /// ������ѵ�γ̵Ŀμ���Ϣ
        /// </summary>
        /// <param name="trainCourseCourseware"></param>
        public void UpdateTrainCourseCourseware(TrainCourseCourseware trainCourseCourseware)
        {
            dal.UpdateTrainCourseCourseware(trainCourseCourseware);
        }

        public IList<TrainCourseCourseware> GetTrainCourseCoursewareInfo(int trainCourseCoursewareID,
                                                    int courseID,
                                                    int coursewareID,
                                                    string studyDemand,
                                                    decimal studyHours,
                                                    string memo,
                                                    int startRowIndex,
                                                    int maximumRows,
                                                    string orderBy)
        {
            return dal.GetTrainCourseCoursewareInfo(trainCourseCoursewareID,
                                         courseID,
                                         coursewareID,
                                         studyDemand,
                                         studyHours,
                                         memo,
                                         startRowIndex,
                                         maximumRows,
                                         orderBy);
        }

        /// <summary>
        /// ������ѵ�μ�IDȷ��Ψһ�Ŀγ���ƣ��μ�����Ϣ
        /// </summary>
        /// <param name="trainCourseCoursewareID"></param>
        /// <returns></returns>
        public TrainCourseCourseware GetTrainCourseCoursewareInfo(int trainCourseCoursewareID)
        {
            return dal.GetTrainCourseCoursewareInfo(trainCourseCoursewareID);
        }

        /// <summary>
        /// ���ݿγ�IDȡ������صĿγ���ƣ��μ�����Ϣ
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        public IList<TrainCourseCourseware> GetTrainCourseCoursewareByCourseID(int courseID)
        {
            return dal.GetTrainCourseCoursewareByCourseID(courseID);
        }

        /// <summary>
        /// ���ݿμ�ID�Ϳγ�IDȷ��Ψһ�Ŀγ���ƣ��μ�����Ϣ
        /// </summary>
        /// <param name="courseID"></param>
        /// <param name="coursewareID"></param>
        ///  /// <returns></returns>
        public TrainCourseCourseware GetTrainCourseCoursewareByWareID(int courseID,int coursewareID)
        {
            return dal.GetTrainCourseCoursewareByWareID(courseID,coursewareID);
        }

        /// <summary>
        /// ���ݿγ�ID�Ϳμ�����IDȡ���ڸÿγ̵����пμ�����ÿγ�������صĿμ���Ϣ
        /// </summary>
        /// <param name="courseID"></param>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public IList<TrainCourseCourseware> GetTrainCourseCoursewareByTypeID(int courseID,int typeID)
        {
            return dal.GetTrainCourseCoursewareByTypeID(courseID, typeID);   
        }
    }
}
