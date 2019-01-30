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
        /// 内部成员
        /// </summary>
        private static readonly TrainCourseCoursewareDAL dal = new TrainCourseCoursewareDAL();

        /// <summary>
        /// 新增培训课程的课件信息
        /// </summary>
        /// <param name="trainCourseCourseware"></param>
        public void AddTrainCourseCourseware(TrainCourseCourseware trainCourseCourseware)
        {
            dal.AddTrainCourseCourseware(trainCourseCourseware);
        }

        /// <summary>
        /// 删除培训课程的课件信息
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
        /// 更新培训课程的课件信息
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
        /// 根据培训课件ID确定唯一的课程设计（课件）信息
        /// </summary>
        /// <param name="trainCourseCoursewareID"></param>
        /// <returns></returns>
        public TrainCourseCourseware GetTrainCourseCoursewareInfo(int trainCourseCoursewareID)
        {
            return dal.GetTrainCourseCoursewareInfo(trainCourseCoursewareID);
        }

        /// <summary>
        /// 根据课程ID取得其相关的课程设计（课件）信息
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        public IList<TrainCourseCourseware> GetTrainCourseCoursewareByCourseID(int courseID)
        {
            return dal.GetTrainCourseCoursewareByCourseID(courseID);
        }

        /// <summary>
        /// 根据课件ID和课程ID确定唯一的课程设计（课件）信息
        /// </summary>
        /// <param name="courseID"></param>
        /// <param name="coursewareID"></param>
        ///  /// <returns></returns>
        public TrainCourseCourseware GetTrainCourseCoursewareByWareID(int courseID,int coursewareID)
        {
            return dal.GetTrainCourseCoursewareByWareID(courseID,coursewareID);
        }

        /// <summary>
        /// 根据课程ID和课件类型ID取得在该课程的所有课件中与该课程类型相关的课件信息
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
