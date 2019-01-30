using System;
using System.Collections.Generic;
using System.Text;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class TrainCourseBLL
    {
        /// <summary>
        /// �ڲ���Ա
        /// </summary>
        private static readonly TrainCourseDAL dal = new TrainCourseDAL();

        /// <summary>
        /// ������ѵ�γ�
        /// </summary>
        /// <param name="trainCourse"></param>
        public void AddTrainCourse(TrainCourse trainCourse)
        {
            dal.AddTrainCourse(trainCourse);
        }

        /// <summary>
        /// ɾ����ѵ�γ�
        /// </summary>
        /// <param name="trainCourseID"></param>
        public void DeleteTrainCourse(int trainCourseID)
        {
            dal.DeleteTrainCourse(trainCourseID);
        }

        public void DeleteTrainCourse(TrainCourse trainCourse)
        {
            dal.DeleteTrainCourse(trainCourse.TrainCourseID);
        }

        /// <summary>
        /// ������ѵ�γ�
        /// </summary>
        /// <param name="trainCourse"></param>
        public void UpdateTrainCourse(TrainCourse trainCourse)
        {
            dal.UpdateTrainCourse(trainCourse);
        }

        public IList<TrainCourse> GetTrainCourseInfo(int trainCourseID,
                                                    int standardID,
                                                    int courseNo,
                                                    string courseName,
                                                    string description,
                                                    string studyDemand,
                                                    decimal studyHours,
                                                    bool hasExam,
                                                    string examForm,
                                                    int requireCourseID,
                                                    string memo,
                                                    int startRowIndex,
                                                    int maximumRows,
                                                    string orderBy)
        {
            return dal.GetTrainCourseInfo(trainCourseID,
                                         standardID,
                                         courseNo,
                                         courseName,
                                         description,
                                         studyDemand,
                                         studyHours,
                                         hasExam,
                                         examForm,
                                         requireCourseID,
                                         memo,
                                         startRowIndex,
                                         maximumRows,
                                         orderBy);
        }

        /// <summary>
        /// ������ѵ�γ�IDȷ��Ψһ����ѵ�γ�
        /// </summary>
        /// <param name="trainCourseID"></param>
        /// <returns></returns>
        public TrainCourse GetTrainCourseInfo(int trainCourseID)
        {
            return dal.GetTrainCourseInfo(trainCourseID);
        }

        /// <summary>
        /// ������ѵ�淶IDȡ�øù淶����صĿγ���Ϣ
        /// </summary>
        /// <param name="standardID"></param>
        /// <returns></returns>
        public IList<TrainCourse> GetTrainStandardCourse(int standardID)
        {
            return dal.GetTrainStandardCourse(standardID);
        }

        /// <summary>
        /// ȡ�ù����γ���Ϣ
        /// </summary>
        /// <returns></returns>
        public IList<TrainCourse> GetTrainCommondCourseInfo()
        {
            return dal.GetTrainCommondCourseInfo();
        }

        /// <summary>
        /// ��ѯ�γ���Ϣ
        /// </summary>
        /// <param name="strSql">��ѯ����</param>
        /// <returns></returns>
        public IList<TrainCourse> GetTrainCourseQueryInfo(string  strSql)
        {
            return dal.GetTrainCourseQueryInfo(strSql);
        }
    }
}
