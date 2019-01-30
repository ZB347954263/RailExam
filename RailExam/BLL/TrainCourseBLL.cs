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
        /// 内部成员
        /// </summary>
        private static readonly TrainCourseDAL dal = new TrainCourseDAL();

        /// <summary>
        /// 新增培训课程
        /// </summary>
        /// <param name="trainCourse"></param>
        public void AddTrainCourse(TrainCourse trainCourse)
        {
            dal.AddTrainCourse(trainCourse);
        }

        /// <summary>
        /// 删除培训课程
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
        /// 更新培训课程
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
        /// 根据培训课程ID确定唯一的培训课程
        /// </summary>
        /// <param name="trainCourseID"></param>
        /// <returns></returns>
        public TrainCourse GetTrainCourseInfo(int trainCourseID)
        {
            return dal.GetTrainCourseInfo(trainCourseID);
        }

        /// <summary>
        /// 根据培训规范ID取得该规范下相关的课程信息
        /// </summary>
        /// <param name="standardID"></param>
        /// <returns></returns>
        public IList<TrainCourse> GetTrainStandardCourse(int standardID)
        {
            return dal.GetTrainStandardCourse(standardID);
        }

        /// <summary>
        /// 取得公共课程信息
        /// </summary>
        /// <returns></returns>
        public IList<TrainCourse> GetTrainCommondCourseInfo()
        {
            return dal.GetTrainCommondCourseInfo();
        }

        /// <summary>
        /// 查询课程信息
        /// </summary>
        /// <param name="strSql">查询条件</param>
        /// <returns></returns>
        public IList<TrainCourse> GetTrainCourseQueryInfo(string  strSql)
        {
            return dal.GetTrainCourseQueryInfo(strSql);
        }
    }
}
