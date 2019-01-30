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
    public class TrainCourseCoursewareDAL
    {
         private static Hashtable _ormTable;
        private int _recordCount = 0;

        static TrainCourseCoursewareDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("traincoursecoursewareid","TRAIN_COURSE_COURSEWARE_ID");
            _ormTable.Add("courseid", "COURSE_ID");
            _ormTable.Add("coursewareid", "COURSEWARE_ID");
            _ormTable.Add("coursewarename", "COURSEWARE_NAME");
            _ormTable.Add("studydemand", "STUDY_DEMAND");
            _ormTable.Add("studyhours", "STUDY_HOURS");
            _ormTable.Add("memo", "MEMO");
            _ormTable.Add("coursewaretypeid","COURSEWARETYPEID");
        }

        /// <summary>
        /// 新增培训课程的课件信息
        /// </summary>
        /// <param name="obj"></param>
        public void AddTrainCourseCourseware(TrainCourseCourseware obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_WARE_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_train_course_courseware_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_course_id", DbType.Int32, obj.CourseID);
            db.AddInParameter(dbCommand, "p_courseware_id", DbType.Int32, obj.CoursewareID);
            db.AddInParameter(dbCommand, "p_study_demand", DbType.String, obj.StudyDemand);
            db.AddInParameter(dbCommand, "p_study_hours", DbType.VarNumeric, obj.StudyHours);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, obj.Memo);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 删除培训课程的课件信息
        /// </summary>
        /// <param name="trainCourseCoursewareID"></param>
        public void DeleteTrainCourseCourseware(int trainCourseCoursewareID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_WARE_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_course_courseware_id", DbType.Int32, trainCourseCoursewareID);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 更新培训课程的课件信息
        /// </summary>
        /// <param name="obj"></param>
        public void UpdateTrainCourseCourseware(TrainCourseCourseware obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_WARE_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_course_courseware_id", DbType.Int32, obj.TrainCourseCoursewareID);
            db.AddInParameter(dbCommand, "p_course_id", DbType.Int32, obj.CourseID);
            db.AddInParameter(dbCommand, "p_courseware_id", DbType.Int32, obj.CoursewareID);
            db.AddInParameter(dbCommand, "p_study_demand", DbType.String, obj.StudyDemand);
            db.AddInParameter(dbCommand, "p_study_hours", DbType.VarNumeric, obj.StudyHours);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, obj.Memo);

            db.ExecuteNonQuery(dbCommand);
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
            IList<TrainCourseCourseware> objCourseCoursewareList = new List<TrainCourseCourseware>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_WARE_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TrainCourseCourseware obj = CreateModelObject(dataReader);

                    objCourseCoursewareList.Add(obj);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return objCourseCoursewareList;
        }


        public TrainCourseCourseware GetTrainCourseCoursewareInfo(int trainCourseCoursewareID)
        {
            TrainCourseCourseware obj = new TrainCourseCourseware();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_WARE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_course_courseware_id", DbType.Int32, trainCourseCoursewareID);
            db.AddInParameter(dbCommand, "p_course_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_courseware_type_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_courseware_id", DbType.Int32, 0);

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
        /// 根据课件ID和课程ID确定唯一的课程设计（课件）信息
        /// </summary>
        /// <param name="courseID"></param>
        /// <param name="coursewareID"></param>
        ///  /// <returns></returns>    
        public TrainCourseCourseware GetTrainCourseCoursewareByWareID(int courseID,int coursewareID)
        {
            TrainCourseCourseware obj = new TrainCourseCourseware();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_WARE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_course_courseware_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_course_id", DbType.Int32, courseID);
            db.AddInParameter(dbCommand, "p_courseware_type_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_courseware_id", DbType.Int32, coursewareID);


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
        /// 根据课程ID取得其相关的课程设计（课件）信息
        /// </summary>
        /// <param name="courseID"></param>
        /// <returns></returns>
        public IList<TrainCourseCourseware>  GetTrainCourseCoursewareByCourseID(int courseID)
        {
            IList<TrainCourseCourseware> objCourseCoursewareList = new List<TrainCourseCourseware>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_WARE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
      
            db.AddInParameter(dbCommand, "p_train_course_courseware_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_course_id", DbType.Int32, courseID);
            db.AddInParameter(dbCommand, "p_courseware_type_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_courseware_id", DbType.Int32, 0);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TrainCourseCourseware obj = CreateModelObject(dataReader);

                    objCourseCoursewareList.Add(obj);
                }
            }
            return objCourseCoursewareList;
        }

        /// <summary>
        /// 根据课程ID和课件类型ID取得在该课程的所有课件中与该课程类型相关的课件信息
        /// </summary>
        /// <param name="courseID"></param>
        /// <param name="typeID"></param>
        /// <returns></returns>
        public IList<TrainCourseCourseware> GetTrainCourseCoursewareByTypeID(int courseID,int typeID)
        {
            IList<TrainCourseCourseware> objCourseCoursewareList = new List<TrainCourseCourseware>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_WARE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_course_courseware_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_course_id", DbType.Int32, courseID);
            db.AddInParameter(dbCommand, "p_courseware_type_id", DbType.Int32, typeID);
            db.AddInParameter(dbCommand, "p_courseware_id", DbType.Int32, 0);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TrainCourseCourseware obj = CreateModelObject(dataReader);

                    objCourseCoursewareList.Add(obj);
                }
            }
            return objCourseCoursewareList;
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

        public static TrainCourseCourseware CreateModelObject(IDataReader dataReader)
        {
            return new TrainCourseCourseware(
                DataConvert.ToInt(dataReader[GetMappingFieldName("TrainCourseCoursewareID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("CourseID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("CoursewareID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("CoursewareName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("StudyDemand")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("StudyHours")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("CoursewareTypeID")]));
        }
    }
}
