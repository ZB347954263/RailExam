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
    public class TrainCourseDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static TrainCourseDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("traincourseid","TRAIN_COURSE_ID");
            _ormTable.Add("standardid", "STANDARD_ID");
            _ormTable.Add("courseno", "COURSE_NO");
            _ormTable.Add("coursename", "COURSE_NAME");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("studydemand", "STUDY_DEMAND");
            _ormTable.Add("studyhours", "STUDY_HOURS");
            _ormTable.Add("hasexam","HAS_EXAM");
            _ormTable.Add("examform", "EXAM_FORM");
            _ormTable.Add("requirecourseid", "REQUIRE_COURSE_ID");
            _ormTable.Add("requirecoursename","REQUIRECOURSENAME");
            _ormTable.Add("memo", "MEMO");
            _ormTable.Add("postname","Post_Name");
            _ormTable.Add("typename","Type_Name");
        }

        /// <summary>
        /// 新增培训课程
        /// </summary>
        /// <param name="obj"></param>
        public void AddTrainCourse(TrainCourse obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_train_course_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_standard_id", DbType.Int32, obj.StandardID);
            db.AddOutParameter(dbCommand, "p_course_no", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_course_name", DbType.String, obj.CourseName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, obj.Description);
            db.AddInParameter(dbCommand, "p_study_demand", DbType.String, obj.StudyDemand);
            db.AddInParameter(dbCommand, "p_study_hours", DbType.VarNumeric, obj.StudyHours);
            db.AddInParameter(dbCommand, "p_has_exam", DbType.Int32, obj.HasExam ? 1:0 );
            db.AddInParameter(dbCommand, "p_exam_form", DbType.String, obj.ExamForm);
            db.AddInParameter(dbCommand, "p_require_course_id", DbType.Int32, obj.RequireCourseID);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, obj.Memo);

            db.ExecuteNonQuery(dbCommand);

            obj.TrainCourseID = (int) db.GetParameterValue(dbCommand, "p_train_course_id");
            obj.CourseNo = (int) db.GetParameterValue(dbCommand, "p_course_no");
        }

        /// <summary>
        /// 删除培训课程
        /// </summary>
        /// <param name="trainCourseID"></param>
        public void DeleteTrainCourse(int trainCourseID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_course_id", DbType.Int32, trainCourseID);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 更新培训课程
        /// </summary>
        /// <param name="obj"></param>
        public void UpdateTrainCourse(TrainCourse obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_course_id", DbType.Int32, obj.TrainCourseID);
            db.AddInParameter(dbCommand, "p_standard_id", DbType.Int32, obj.StandardID);
            db.AddInParameter(dbCommand, "p_course_no", DbType.Int32, obj.CourseNo);
            db.AddInParameter(dbCommand, "p_course_name", DbType.String, obj.CourseName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, obj.Description);
            db.AddInParameter(dbCommand, "p_study_demand", DbType.String, obj.StudyDemand);
            db.AddInParameter(dbCommand, "p_study_hours", DbType.VarNumeric, obj.StudyHours );
            db.AddInParameter(dbCommand, "p_has_exam", DbType.Int32, obj.HasExam ? 1 : 0);
            db.AddInParameter(dbCommand, "p_exam_form", DbType.String, obj.ExamForm);
            db.AddInParameter(dbCommand, "p_require_course_id", DbType.Int32, obj.RequireCourseID);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, obj.Memo);

            db.ExecuteNonQuery(dbCommand);
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
            IList<TrainCourse> objCourseList = new List<TrainCourse>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            if(standardID != 0)
            {
                db.AddInParameter(dbCommand, "p_standard_id", DbType.Int32, standardID);
            }
            if(courseNo != 0)
            {
                db.AddInParameter(dbCommand, "p_course_no", DbType.Int32, courseNo);
            }
            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TrainCourse obj = CreateModelObject(dataReader);

                    objCourseList.Add(obj);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return objCourseList;
        }

        public IList<TrainCourse> GetTrainCommondCourseInfo()
        {
            IList<TrainCourse> objCourseList = new List<TrainCourse>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COMMON_COURSE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TrainCourse obj = CreateModelObject(dataReader);

                    objCourseList.Add(obj);
                }
            }

            return objCourseList;
        }

        public IList<TrainCourse> GetTrainCourseQueryInfo(string strSql)
        {
            IList<TrainCourse> objCourseList = new List<TrainCourse>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_strSql", DbType.String, strSql);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TrainCourse obj = CreateModelObject(dataReader);

                    objCourseList.Add(obj);
                }
            }

            return objCourseList;
        }

        /// <summary>
        /// 根据培训规范ID取得该培训规范所有课程信息
        /// </summary>
        /// <param name="standardID"></param>
        /// <returns></returns>
         public IList<TrainCourse> GetTrainStandardCourse(int standardID)
         {
             IList<TrainCourse> objCourseList = new List<TrainCourse>();

             Database db = DatabaseFactory.CreateDatabase();

             string sqlCommand = "USP_TRAIN_COURSE_G";
             DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

             db.AddInParameter(dbCommand, "p_train_course_id", DbType.Int32, 0);
             db.AddInParameter(dbCommand, "p_standard_id", DbType.Int32, standardID);

             using (IDataReader dataReader = db.ExecuteReader(dbCommand))
             {
                 while (dataReader.Read())
                 {
                     TrainCourse obj = CreateModelObject(dataReader);
                     objCourseList.Add(obj);
                 }
             }

             return objCourseList;
         }

        public TrainCourse GetTrainCourseInfo(int trainCourseID)
        {
            TrainCourse obj = new TrainCourse();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_course_id", DbType.Int32, trainCourseID);
            db.AddInParameter(dbCommand, "p_standard_id", DbType.Int32, 0);

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

        public static TrainCourse CreateModelObject(IDataReader dataReader)
        {
            return new TrainCourse(
                DataConvert.ToInt(dataReader[GetMappingFieldName("TrainCourseID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("StandardID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("CourseNo")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("CourseName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("StudyDemand")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("StudyHours")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("HasExam")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ExamForm")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("RequireCourseID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("RequireCourseName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("PostName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("TypeName")]));
        }
    }
}
