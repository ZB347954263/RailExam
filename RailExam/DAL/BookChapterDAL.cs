using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using RailExam.Model;
using DSunSoft.Data;

namespace RailExam.DAL
{
   public class BookChapterDAL
    { 
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static BookChapterDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("bookid", "Book_ID");
            _ormTable.Add("chapterid", "chapter_Id");
            _ormTable.Add("parentid", "PARENT_ID");
            _ormTable.Add("idpath", "ID_PATH");
            _ormTable.Add("levelnum", "LEVEL_NUM");
            _ormTable.Add("orderindex", "ORDER_INDEX");
            _ormTable.Add("chaptername", "chapter_Name");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("memo", "MEMO");
            _ormTable.Add("url", "url");
            _ormTable.Add("referenceregulation", "reference_Regulation");
            _ormTable.Add("studydemand", "study_Demand");
            _ormTable.Add("studyhours", "study_Hours");
            _ormTable.Add("hasexam", "has_Exam");
            _ormTable.Add("examform", "exam_Form");
            _ormTable.Add("lastperson", "LAST_UPDATE_PERSON");
            _ormTable.Add("lastdate", "LAST_UPDATE_DATE");
            _ormTable.Add("namepath","Name_Path");
			_ormTable.Add("iscannotseeanswer","IS_CANNOT_SEEANSWER");
            _ormTable.Add("ismotheritem", "IS_MOTHER_ITEM");
        }

        public BookChapter GetBookChapter(int ChapterID)
        {
            BookChapter bookChapter = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_CHAPTER_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, ChapterID);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, 0);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    bookChapter = CreateModelObject(dataReader);
					bookChapter.NamePath = dataReader[GetMappingFieldName("NamePath")].ToString();
                }
            }

            return bookChapter;
        }

        public IList<BookChapter> GetBookChapterByBookID(int bookId)
        {
            IList<BookChapter> bookChapters = new List<BookChapter>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_CHAPTER_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    BookChapter bookChapter = CreateModelObject(dataReader);
                    bookChapter.NamePath = dataReader[GetMappingFieldName("NamePath")].ToString();
                    bookChapters.Add(bookChapter);
                }
            }

            return bookChapters;
        }

	   public int GetMaxChapterIDByBookID(int bookId)
	   {
		   Database db = DatabaseFactory.CreateDatabase();

		   string sqlCommand = "USP_BOOK_CHAPTER_G_Max";
		   DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

		   db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookId);
		   db.AddOutParameter(dbCommand, "p_max_id", DbType.Int32,4);

		   db.ExecuteNonQuery(dbCommand);
		   int id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_max_id"));

		   return id;
	   }

       public IList<BookChapter> GetBookChapterByParentID(int parentID)
       {
           IList<BookChapter> bookChapters = new List<BookChapter>();

           Database db = DatabaseFactory.CreateDatabase();

           string sqlCommand = "USP_BOOK_CHAPTER_G";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

           db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, 0);
           db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, parentID);

           using (IDataReader dataReader = db.ExecuteReader(dbCommand))
           {
               while (dataReader.Read())
               {
                   BookChapter bookChapter = CreateModelObject(dataReader);
                   bookChapter.NamePath = dataReader[GetMappingFieldName("NamePath")].ToString();
                   bookChapters.Add(bookChapter);
               }
           }

           return bookChapters;
       }

	   public IList<BookChapter> GetBookChapterByIDPath(string idPath)
	   {
		   IList<BookChapter> bookChapters = new List<BookChapter>();

		   Database db = DatabaseFactory.CreateDatabase();

		   string sqlCommand = "USP_BOOK_CHAPTER_G_Path";
		   DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

		   db.AddInParameter(dbCommand, "p_id_path", DbType.String, idPath);

		   using (IDataReader dataReader = db.ExecuteReader(dbCommand))
		   {
			   while (dataReader.Read())
			   {
				   BookChapter bookChapter = CreateModelObject(dataReader);
				   bookChapter.NamePath = dataReader[GetMappingFieldName("NamePath")].ToString();
				   bookChapters.Add(bookChapter);
			   }
		   }

		   return bookChapters;
	   }

        public void AddBookChapter(BookChapter bookChapter, string EmployeeName)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_CHAPTER_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookChapter.BookId);
            db.AddOutParameter(dbCommand, "p_chapter_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, bookChapter.ParentId);
            db.AddOutParameter(dbCommand, "p_id_path", DbType.String, 20);
            db.AddOutParameter(dbCommand, "p_level_num", DbType.Int32, 4);
            db.AddOutParameter(dbCommand, "p_order_index", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_chapter_name", DbType.String, bookChapter.ChapterName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, bookChapter.Description);
            db.AddInParameter(dbCommand, "p_reference_regulation", DbType.String, bookChapter.ReferenceRegulation);
            db.AddInParameter(dbCommand, "p_url", DbType.String, bookChapter.Url);
            db.AddInParameter(dbCommand, "p_last_update_person", DbType.String, EmployeeName);
            //db.AddInParameter(dbCommand, "p_last_update_date", DbType.DateTime, DateTime.Now);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, bookChapter.Memo);
            db.AddInParameter(dbCommand, "p_study_demand", DbType.String, bookChapter.StudyDemand);
            db.AddInParameter(dbCommand, "p_study_hours", DbType.Decimal, bookChapter.StudyHours);
            db.AddInParameter(dbCommand, "p_has_exam", DbType.Int32, bookChapter.HasExam);
            db.AddInParameter(dbCommand, "p_exam_form", DbType.String, bookChapter.ExamForm);
			db.AddInParameter(dbCommand, "p_is_cannot", DbType.Int32, bookChapter.IsCannotSeeAnswer? 1 : 0);
            db.AddInParameter(dbCommand, "p_is_mother_item", DbType.Int32, bookChapter.IsMotherItem ? 1 : 0);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
               db.ExecuteNonQuery(dbCommand, transaction);

               transaction.Commit();
            }
            catch(System.SystemException ex)
            {
               transaction.Rollback();
               throw ex;
            }

            connection.Close();
        }


       public void UpateBookChapterUrl(int chapterID, string strUrl)
       {
           Database db = DatabaseFactory.CreateDatabase();

           string sqlCommand = "USP_BOOK_CHAPTER_UPDATE_URL";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

           db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, chapterID);
           db.AddInParameter(dbCommand, "p_url", DbType.String, strUrl);

           db.ExecuteNonQuery(dbCommand);
       }

       public void UpdateBookChapter(BookChapter bookChapter, string EmployeeName)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_CHAPTER_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookChapter.BookId);
            db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, bookChapter.ChapterId);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, bookChapter.ParentId);
            db.AddInParameter(dbCommand, "p_id_path", DbType.String, bookChapter.IdPath);
            db.AddInParameter(dbCommand, "p_level_num", DbType.Int32, bookChapter.LevelNum);
            db.AddInParameter(dbCommand, "p_order_index", DbType.Int32, bookChapter.OrderIndex);
            db.AddInParameter(dbCommand, "p_chapter_name", DbType.String, bookChapter.ChapterName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, bookChapter.Description);
            db.AddInParameter(dbCommand, "p_reference_regulation", DbType.String, bookChapter.ReferenceRegulation);
            db.AddInParameter(dbCommand, "p_url", DbType.String, bookChapter.Url);
            db.AddInParameter(dbCommand, "p_last_update_person", DbType.String, EmployeeName);
            //db.AddInParameter(dbCommand, "p_last_update_date", DbType.DateTime, DateTime.Now);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, bookChapter.Memo);
            db.AddInParameter(dbCommand, "p_study_demand", DbType.String, bookChapter.StudyDemand);
            db.AddInParameter(dbCommand, "p_study_hours", DbType.Decimal, bookChapter.StudyHours);
            db.AddInParameter(dbCommand, "p_has_exam", DbType.Int32, bookChapter.HasExam);
            db.AddInParameter(dbCommand, "p_exam_form", DbType.String, bookChapter.ExamForm);
			db.AddInParameter(dbCommand, "p_is_cannot", DbType.Int32, bookChapter.IsCannotSeeAnswer ? 1 : 0);
            db.AddInParameter(dbCommand, "p_is_mother_item", DbType.Int32, bookChapter.IsMotherItem ? 1 : 0);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
               db.ExecuteNonQuery(dbCommand, transaction);

               transaction.Commit();
            }
            catch
            {
               transaction.Rollback();
            }
            connection.Close();
        }

       public void DeleteBookChapter(int chapterID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_CHAPTER_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, chapterID);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, 0);

            db.ExecuteNonQuery(dbCommand);
        }

        public void DeleteBookChapterByBookID(int bookID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_CHAPTER_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookID);

            db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 取得某一Book对应的章节信息
        /// </summary>
        /// <returns></returns>
        public IList<BookChapter> GetTrainCourseBookChapterTree(int bookID)
        {
            IList<BookChapter> bookChapterList = new List<BookChapter>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_BOOK_CHAPTER";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    BookChapter bookChapter = CreateModelObject(dataReader);

                    bookChapterList.Add(bookChapter);
                }
            }

            return bookChapterList;
        }

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

        public static BookChapter CreateModelObject(IDataReader dataReader)
        {
            return new BookChapter(
                DataConvert.ToInt(dataReader[GetMappingFieldName("BookId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ChapterId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ParentId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("IdPath")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("LevelNum")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrderIndex")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ChapterName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("referenceRegulation")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("url")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("studyDemand")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("studyHours")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("hasExam")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("examForm")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("LastPerson")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("LastDate")]),
				DataConvert.ToBool(dataReader[GetMappingFieldName("IsCannotSeeAnswer")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("IsMotherItem")]));
        }
    }
}
