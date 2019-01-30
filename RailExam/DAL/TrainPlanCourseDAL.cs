using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;
using DSunSoft.Data;
using System.Text;

namespace RailExam.DAL
{
    public class TrainPlanCourseDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static TrainPlanCourseDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("trainplanid", "TRAIN_PLAN_ID");
            _ormTable.Add("traincourseid", "TRAIN_COURSE_ID");
            _ormTable.Add("process", "PROCESS");
            _ormTable.Add("statusid", "STATUS_ID");
            _ormTable.Add("statusname", "STATUS_NAME");
            _ormTable.Add("memo", "MEMO");
        }

        /// <summary>
        /// 添加培训计划课程
        /// </summary>
        /// <param name="obj"></param>
        public void AddTrainPlanCourse(TrainPlanCourse obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_PLAN_COURSE_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_plan_id", DbType.Int32, obj.TrainPlanID);
            db.AddInParameter(dbCommand, "p_train_course_id", DbType.Int32, obj.TrainCourseID);
            db.AddInParameter(dbCommand, "p_process", DbType.VarNumeric, obj.Process);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, obj.StatusID);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, obj.Memo);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 更新培训计划课程
        /// </summary>
        /// <param name="obj"></param>
        public void UpdateTrainPlanCourse(TrainPlanCourse obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_PLAN_COURSE_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_plan_id", DbType.Int32, obj.TrainPlanID);
            db.AddInParameter(dbCommand, "p_train_course_id", DbType.Int32, obj.TrainCourseID);
            db.AddInParameter(dbCommand, "p_process", DbType.VarNumeric, obj.Process);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, obj.StatusID);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, obj.Memo);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 删除培训计划课程
        /// </summary>
        /// <param name="planid"></param>
        /// <param name="courseid"></param>
        public void DeleteTrainPlanCourse(int planid, int courseid)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_PLAN_COURSE_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_plan_id", DbType.Int32, planid);
            db.AddInParameter(dbCommand, "p_train_course_id", DbType.Int32, courseid);

            db.ExecuteNonQuery(dbCommand);
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
            IList<TrainPlanCourse> objCourseList = new List<TrainPlanCourse>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_PLAN_COURSE_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TrainPlanCourse obj = CreateModelObject(dataReader);

                    objCourseList.Add(obj);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return objCourseList;
        }

        /// <summary>
        /// 根据计划ID和课程ID返回唯一的培训计划课程信息
        /// </summary>
        /// <param name="trainPlanID"></param>
        /// <param name="trainCourseID"></param>
        /// <returns></returns>
        public TrainPlanCourse GetTrainPlanCourseInfo(int trainPlanID, int trainCourseID)
        {
            TrainPlanCourse obj = new TrainPlanCourse();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_PLAN_COURSE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_plan_id", DbType.Int32, trainPlanID);
            db.AddInParameter(dbCommand, "p_train_course_id", DbType.Int32, trainCourseID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    obj = CreateModelObject(dataReader);
                }
            }

            return obj;
        }

        /// <summary>
        /// 返回某一培训计划所有的培训计划课程信息
        /// </summary>
        /// <param name="trainPlanID"></param>
        /// <returns></returns>
        public IList<TrainPlanCourse> GetTrainPlanCourseInfoByPlanID(int trainPlanID)
        {
            IList<TrainPlanCourse> objCourseList = new List<TrainPlanCourse>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_PLAN_COURSE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_plan_id", DbType.Int32, trainPlanID);
            db.AddInParameter(dbCommand, "p_train_course_id", DbType.Int32, 0);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TrainPlanCourse obj = CreateModelObject(dataReader);
                    objCourseList.Add(obj);
                }
            }

            return objCourseList;
        }

        /// <summary>
        /// 查询结果记录数
        /// </summary>
        public int RecordCount
        {
            get
            {
                return _recordCount;
            }
        }

        public static string GetMappingFieldName(string propertyName)
        {
            return (string)_ormTable[propertyName.ToLower()];
        }

        public static string GetMappingOrderBy(string orderBy)
        {
            orderBy = orderBy.Trim();

            if (string.IsNullOrEmpty(orderBy))
            {
                return string.Empty;
            }

            string mappingOrderBy = string.Empty;
            string[] orderByConditions = orderBy.Split(new char[] { ',' });

            foreach (string s in orderByConditions)
            {
                string orderByCondition = s.Trim();

                string[] orderBysOfOneCondition = orderByCondition.Split(new char[] { ' ' });

                if (orderBysOfOneCondition.Length == 0)
                {
                    continue;
                }
                else
                {
                    if (mappingOrderBy != string.Empty)
                    {
                        mappingOrderBy += ',';
                    }

                    if (orderBysOfOneCondition.Length == 1)
                    {
                        mappingOrderBy += GetMappingFieldName(orderBysOfOneCondition[0]);
                    }
                    else
                    {
                        mappingOrderBy += GetMappingFieldName(orderBysOfOneCondition[0]) + ' ' + orderBysOfOneCondition[1];
                    }
                }
            }

            return mappingOrderBy;
        }

        public static TrainPlanCourse CreateModelObject(IDataReader dataReader)
        {
            return new TrainPlanCourse(
                DataConvert.ToInt(dataReader[GetMappingFieldName("TrainPlanID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("TrainCourseID")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("Process")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("StatusID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("StatusName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]),
                new TrainCourse(DataConvert.ToInt(dataReader[TrainCourseDAL.GetMappingFieldName("TrainCourseID")]),
                DataConvert.ToInt(dataReader[TrainCourseDAL.GetMappingFieldName("StandardID")]),
                DataConvert.ToInt(dataReader[TrainCourseDAL.GetMappingFieldName("CourseNo")]),
                DataConvert.ToString(dataReader[TrainCourseDAL.GetMappingFieldName("CourseName")]),
                DataConvert.ToString(dataReader[TrainCourseDAL.GetMappingFieldName("Description")]),
                DataConvert.ToString(dataReader[TrainCourseDAL.GetMappingFieldName("StudyDemand")]),
                DataConvert.ToDecimal(dataReader[TrainCourseDAL.GetMappingFieldName("StudyHours")]),
                DataConvert.ToBool(dataReader[TrainCourseDAL.GetMappingFieldName("HasExam")]),
                DataConvert.ToString(dataReader[TrainCourseDAL.GetMappingFieldName("ExamForm")]),
                DataConvert.ToInt(dataReader[TrainCourseDAL.GetMappingFieldName("RequireCourseID")]),
                DataConvert.ToString(dataReader[TrainCourseDAL.GetMappingFieldName("RequireCourseName")]),
                DataConvert.ToString(dataReader[TrainCourseDAL.GetMappingFieldName("Memo")]),
                DataConvert.ToString(dataReader[TrainCourseDAL.GetMappingFieldName("PostName")]),
                DataConvert.ToString(dataReader[TrainCourseDAL.GetMappingFieldName("TypeName")])));
        }
    }
}
