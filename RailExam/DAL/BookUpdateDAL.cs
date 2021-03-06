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
    public class BookUpdateDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static BookUpdateDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("bookid", "Book_ID");
            _ormTable.Add("chapterid", "chapter_Id");
            _ormTable.Add("bookupdateid", "BOOK_UPDATE_ID");
            _ormTable.Add("updatecause", "update_Cause");
            _ormTable.Add("updatecontent", "update_Content");
            _ormTable.Add("updateperson", "update_Person");
            _ormTable.Add("updatedate", "update_Date");
            _ormTable.Add("memo", "MEMO");
            _ormTable.Add("bookname","Book_Name");
            _ormTable.Add("chaptername","Chapter_Name");
            _ormTable.Add("updateobject","UPDATE_OBJECT");
            _ormTable.Add("booknamebak","BOOK_NAME_BAK");
            _ormTable.Add("chapternamebak", "CHAPTER_NAME_BAK");
        }

        public BookUpdate GetBookUpdate(int ChapterUpdateId)
        {
            BookUpdate BookChapter;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_UPDATE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "p_book_update_id", DbType.Int32, ChapterUpdateId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    BookChapter = CreateModelObject(dataReader);
                }
                else
                {
                    BookChapter = new BookUpdate();
                }
            }

            return BookChapter;
        }

        public IList<BookUpdate> GetBookUpdateByChapterID(int chapterID,int bookID)
        {
            IList<BookUpdate> objList = new List<BookUpdate>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_UPDATE_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, chapterID);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    BookUpdate obj = CreateModelObject(dataReader);
                    objList.Add(obj);
                }
            }

            return objList;
        }

        public IList<BookUpdate> GetBookUpdateBySelect( int bookID,string bookname,string person, DateTime begin, DateTime end, string updateobject)
        {
            IList<BookUpdate> objList = new List<BookUpdate>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_UPDATE_Q_Select";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookID);
            db.AddInParameter(dbCommand, "p_book_name", DbType.String, bookname);
            db.AddInParameter(dbCommand, "p_person", DbType.String, person);
            db.AddInParameter(dbCommand, "p_begin_date", DbType.Date, begin);
            db.AddInParameter(dbCommand, "p_end_date", DbType.Date, end);
            db.AddInParameter(dbCommand, "p_update_object", DbType.String, updateobject);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    BookUpdate obj = CreateModelObject(dataReader);
                    objList.Add(obj);
                }
            }

            return objList;
        }


        public int RecordCount
        {
            get
            {
                return _recordCount;
            }
        }

        public void AddBookUpdate(BookUpdate BookChapter)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_UPDATE_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddOutParameter(dbCommand, "p_book_update_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, BookChapter.BookId);
            db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, BookChapter.ChapterId);
            db.AddInParameter(dbCommand, "p_update_person", DbType.String, BookChapter.updatePerson);
            db.AddInParameter(dbCommand, "p_update_date", DbType.DateTime, BookChapter.updateDate);
            db.AddInParameter(dbCommand, "p_update_cause", DbType.String, BookChapter.updateCause);
            db.AddInParameter(dbCommand, "p_update_content", DbType.String, BookChapter.updateContent);
            db.AddInParameter(dbCommand, "p_update_object", DbType.String, BookChapter.UpdateObject);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, BookChapter.Memo);
            db.AddInParameter(dbCommand, "p_book_name", DbType.String, BookChapter.BookNameBak);
            db.AddInParameter(dbCommand, "p_chapter_name", DbType.String, BookChapter.ChapterNameBak);

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

        public void UpdateBookUpdate(BookUpdate BookChapter)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_UPDATE_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_book_update_id", DbType.Int32, BookChapter.bookUpdateId);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, BookChapter.BookId);
            db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, BookChapter.ChapterId);
            db.AddInParameter(dbCommand, "p_update_person", DbType.String, BookChapter.updatePerson);
            db.AddInParameter(dbCommand, "p_update_date", DbType.DateTime, BookChapter.updateDate);
            db.AddInParameter(dbCommand, "p_update_cause", DbType.String, BookChapter.updateCause);
            db.AddInParameter(dbCommand, "p_update_content", DbType.String, BookChapter.updateContent);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, BookChapter.Memo);

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

        public void DeleteBookUpdate(int BookUpdateID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_UPDATE_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_book_update_id", DbType.Int32, BookUpdateID);

            db.ExecuteNonQuery(dbCommand);
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

        public static BookUpdate CreateModelObject(IDataReader dataReader)
        {
            return new BookUpdate(
                DataConvert.ToInt(dataReader[GetMappingFieldName("BookId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("BookUpdateId")]),
                DateTime.Parse(dataReader[GetMappingFieldName("updateDate")].ToString()),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ChapterId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("updateCause")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("updateContent")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("updatePerson")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("BookName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ChapterName")]),
                 DataConvert.ToString(dataReader[GetMappingFieldName("UpdateObject")]),
                 DataConvert.ToString(dataReader[GetMappingFieldName("BookNameBak")]),
                 DataConvert.ToString(dataReader[GetMappingFieldName("ChapterNameBak")]));
        }
    }
}

