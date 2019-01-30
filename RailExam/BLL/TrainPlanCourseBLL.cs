using System;
using System.Collections.Generic;
using System.Collections;
using RailExam.DAL;
using RailExam.Model;

namespace RailExam.BLL
{
    public class TrainPlanCourseBLL
    {
        /// <summary>
        /// 内部成员
        /// </summary>
        private static readonly TrainPlanCourseDAL dal = new TrainPlanCourseDAL();

         /// <summary>
        /// 添加培训计划课程
        /// </summary>
        /// <param name="obj"></param>
        public void AddTrainPlanCourse(TrainPlanCourse obj)
        {
            dal.AddTrainPlanCourse(obj);
        }

        /// <summary>
        /// 更新培训计划课程
        /// </summary>
        /// <param name="obj"></param>
        public void UpdateTrainPlanCourse(TrainPlanCourse obj)
        {
            dal.UpdateTrainPlanCourse(obj);
        }

        /// <summary>
        /// 删除培训计划课程
        /// </summary>
        /// <param name="planid"></param>
        /// <param name="courseid"></param>
        public void DeleteTrainPlanCourse(int planid, int courseid)
        {
            dal.DeleteTrainPlanCourse(planid,courseid);
        }

        public IList<TrainPlanCourse> GetTrainPlanCourseInfo(int trainPlanID,
                                                    int trainCourseID,
                                                    decimal process,
                                                    int statusID,
                                                    string memo,
                                                    int startRowIndex,
                                                    int maximumRows,
                                                    string orderBy)
        {
            return dal.GetTrainPlanCourseInfo(trainPlanID,
                                              trainCourseID,
                                              process,
                                              statusID,
                                              memo,
                                              startRowIndex,
                                              maximumRows,
                                              orderBy);
        }

        /// <summary>
        /// 根据计划ID和课程ID返回唯一的培训计划课程信息
        /// </summary>
        /// <param name="trainPlanID"></param>
        /// <param name="trainCourseID"></param>
        /// <returns></returns>
        public TrainPlanCourse GetTrainPlanCourseInfo(int trainPlanID, int trainCourseID)
        {
            return dal.GetTrainPlanCourseInfo(trainPlanID, trainCourseID);
        }

        /// <summary>
        /// 返回某一培训计划所有的培训计划课程信息
        /// </summary>
        /// <param name="trainPlanID"></param>
        /// <returns></returns>
        public IList<TrainPlanCourse> GetTrainPlanCourseInfoByPlanID(int trainPlanID)
        {
            return dal.GetTrainPlanCourseInfoByPlanID(trainPlanID);
        }

        /// <summary>
        /// 返回某一培训计划所有的公共课程的信息
        /// </summary>
        /// <param name="trainPlanID"></param>
        /// <returns></returns>
        public IList<TrainCourse> GetTrainCommandCourseInfoByPlanID(int trainPlanID)
        {
            TrainCourseBLL objTrainCourseBLL = new TrainCourseBLL();
            IList<TrainCourse> objTrainCourseList = objTrainCourseBLL.GetTrainCommondCourseInfo();
            ArrayList objList = GetCourseList(trainPlanID);

            foreach (TrainCourse course in objTrainCourseList)
            {
                if(objList.IndexOf(course.TrainCourseID) != -1)
                {
                    course.Flag = true;
                }
                else
                {
                    course.Flag = false;
                }
            }

            return objTrainCourseList;
        }


        /// <summary>
        /// 查询课程信息
        /// </summary>
        /// <param name="strSql">查询条件</param>
        /// <param name="trainPlanID">培训计划ID</param>
        /// <returns></returns>
        public IList<TrainCourse> GetTrainCourseQueryInfo(string strSql,int trainPlanID)
        {
            TrainCourseBLL objTrainCourseBLL = new TrainCourseBLL();
            IList<TrainCourse> objTrainCourseList = objTrainCourseBLL.GetTrainCourseQueryInfo(strSql);
            ArrayList objList = GetCourseList(trainPlanID);

            foreach (TrainCourse course in objTrainCourseList)
            {
                if (objList.IndexOf(course.TrainCourseID) != -1)
                {
                    course.Flag = true;
                }
                else
                {
                    course.Flag = false;
                }
            }

            return objTrainCourseList;
        }

        /// <summary>
        /// 根据某培训计划ID返回该计划所含课程的ID
        /// </summary>
        /// <param name="trainPlanID"></param>
        /// <returns></returns>
        public ArrayList GetCourseList(int trainPlanID)
        {
            ArrayList objList = new ArrayList();
            IList<TrainPlanCourse> objTrainPlanCourseList = GetTrainPlanCourseInfoByPlanID(trainPlanID);

            foreach (TrainPlanCourse course in objTrainPlanCourseList)
            {
                objList.Add(course.TrainCourseID);
            }

            return objList;
        }
    }
}
