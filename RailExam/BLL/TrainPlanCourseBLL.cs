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
        /// �ڲ���Ա
        /// </summary>
        private static readonly TrainPlanCourseDAL dal = new TrainPlanCourseDAL();

         /// <summary>
        /// �����ѵ�ƻ��γ�
        /// </summary>
        /// <param name="obj"></param>
        public void AddTrainPlanCourse(TrainPlanCourse obj)
        {
            dal.AddTrainPlanCourse(obj);
        }

        /// <summary>
        /// ������ѵ�ƻ��γ�
        /// </summary>
        /// <param name="obj"></param>
        public void UpdateTrainPlanCourse(TrainPlanCourse obj)
        {
            dal.UpdateTrainPlanCourse(obj);
        }

        /// <summary>
        /// ɾ����ѵ�ƻ��γ�
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
        /// ���ݼƻ�ID�Ϳγ�ID����Ψһ����ѵ�ƻ��γ���Ϣ
        /// </summary>
        /// <param name="trainPlanID"></param>
        /// <param name="trainCourseID"></param>
        /// <returns></returns>
        public TrainPlanCourse GetTrainPlanCourseInfo(int trainPlanID, int trainCourseID)
        {
            return dal.GetTrainPlanCourseInfo(trainPlanID, trainCourseID);
        }

        /// <summary>
        /// ����ĳһ��ѵ�ƻ����е���ѵ�ƻ��γ���Ϣ
        /// </summary>
        /// <param name="trainPlanID"></param>
        /// <returns></returns>
        public IList<TrainPlanCourse> GetTrainPlanCourseInfoByPlanID(int trainPlanID)
        {
            return dal.GetTrainPlanCourseInfoByPlanID(trainPlanID);
        }

        /// <summary>
        /// ����ĳһ��ѵ�ƻ����еĹ����γ̵���Ϣ
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
        /// ��ѯ�γ���Ϣ
        /// </summary>
        /// <param name="strSql">��ѯ����</param>
        /// <param name="trainPlanID">��ѵ�ƻ�ID</param>
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
        /// ����ĳ��ѵ�ƻ�ID���ظüƻ������γ̵�ID
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
