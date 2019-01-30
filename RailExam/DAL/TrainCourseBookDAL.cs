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
    public class TrainCourseBookDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static TrainCourseBookDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("traincoursebookchapterid","TRAIN_COURSE_BOOK_CHAPTER_ID");
            _ormTable.Add("courseid", "COURSE_ID");
            _ormTable.Add("bookid", "BOOK_ID");
            _ormTable.Add("bookname","BOOK_NAME");
            _ormTable.Add("chapterid", "CHAPTER_ID");
            _ormTable.Add("chaptername", "CHAPTER_NAME");
            _ormTable.Add("studydemand", "STUDY_DEMAND");
            _ormTable.Add("studyhours", "STUDY_HOURS");
            _ormTable.Add("memo", "MEMO");
            _ormTable.Add("parentid","PARENT_ID");
        }

        /// <summary>
        /// 新增培训课程的教材信息
        /// </summary>
        /// <param name="obj"></param>
        public void AddTrainCourseBook(TrainCourseBook obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_BOOK_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_train_course_book_chapter_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_course_id", DbType.Int32, obj.CourseID);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, obj.BookID);
            db.AddInParameter(dbCommand, "p_chapter_id", DbType.String, obj.ChapterID);
            db.AddInParameter(dbCommand, "p_study_demand", DbType.String, obj.StudyDemand);
            db.AddInParameter(dbCommand, "p_study_hours", DbType.VarNumeric, obj.StudyHours);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, obj.Memo);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 删除培训课程的教材信息
        /// </summary>
        /// <param name="trainCourseID"></param>
        public void DeleteTrainCourseBook(int trainCourseID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_BOOK_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_course_book_chapter_id", DbType.Int32, trainCourseID);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 更新培训课程的教材信息
        /// </summary>
        /// <param name="obj"></param>
        public void UpdateTrainCourseBook(TrainCourseBook obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_BOOK_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_course_book_chapter_id", DbType.Int32, obj.TrainCourseBookChapterID);
            db.AddInParameter(dbCommand, "p_course_id", DbType.Int32, obj.CourseID);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, obj.BookID);
            db.AddInParameter(dbCommand, "p_chapter_id", DbType.String, obj.ChapterID);
            db.AddInParameter(dbCommand, "p_study_demand", DbType.String, obj.StudyDemand);
            db.AddInParameter(dbCommand, "p_study_hours", DbType.VarNumeric, obj.StudyHours);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, obj.Memo);

            db.ExecuteNonQuery(dbCommand);
        }


        public IList<TrainCourseBook> GetTrainCourseBookInfo(int trainCourseBookChapterID,
                                                    int courseID,
                                                    int bookID,
                                                    string chapterID,
                                                    string studyDemand,
                                                    decimal studyHours,
                                                    string memo,
                                                    int startRowIndex,
                                                    int maximumRows,
                                                    string orderBy)
        {
            IList<TrainCourseBook> objCourseBookList = new List<TrainCourseBook>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_BOOK_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TrainCourseBook obj = CreateModelObject(dataReader);

                    objCourseBookList.Add(obj);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return objCourseBookList;
        }


        public TrainCourseBook GetTrainCourseBookInfo(int trainCourseBookChapterID)
        {
            TrainCourseBook obj = new TrainCourseBook();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_BOOK_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_course_book_chapter_id", DbType.Int32, trainCourseBookChapterID);
            db.AddInParameter(dbCommand, "p_course_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_chapter_id", DbType.String, String.Empty);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, String.Empty);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    obj = CreateModelObject(dataReader);
                }
            }

            return obj;
        }

        public TrainCourseBook GetTrainCourseBookInfo(int courseID,int bookID,string chapterID)
        {
            TrainCourseBook obj = new TrainCourseBook();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_BOOK_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_course_book_chapter_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_course_id", DbType.Int32, courseID);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookID);
            db.AddInParameter(dbCommand, "p_chapter_id", DbType.String, chapterID);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, String.Empty);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    obj = CreateModelObject(dataReader);
                }
            }

            return obj;
        }

        public IList<TrainCourseBook>  GetTrainCourseBookChapter(int courseID,string orderby)
        {
            IList<TrainCourseBook> objCourseBookList = new List<TrainCourseBook>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_BOOK_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
      
            db.AddInParameter(dbCommand, "p_train_course_book_chapter_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_course_id", DbType.Int32, courseID);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_chapter_id", DbType.String, String.Empty);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, orderby);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    TrainCourseBook obj = CreateModelObject(dataReader);

                    objCourseBookList.Add(obj);
                }
            }
            return objCourseBookList;
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

        public static TrainCourseBook CreateModelObject(IDataReader dataReader)
        {
            return new TrainCourseBook(
                DataConvert.ToInt(dataReader[GetMappingFieldName("TrainCourseBookChapterID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("CourseID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("BookID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("BookName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ChapterID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ChapterName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("StudyDemand")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("StudyHours")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ParentID")]));
        }
    }
}
