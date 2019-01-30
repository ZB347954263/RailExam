using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using RailExam.Model;
using DSunSoft.Data;
using System.Data.OracleClient;

namespace RailExam.DAL
{
    public class BookDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static BookDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("bookid", "Book_ID");
            _ormTable.Add("knowledgeid", "KNOWLEDGE_ID");
            _ormTable.Add("knowledgename", "knowledge_Name");
            _ormTable.Add("bookno", "Book_No");
            _ormTable.Add("publishorg", "Publish_Org");
            _ormTable.Add("publishorgname", "publish_Org_Name");
            _ormTable.Add("pagecount", "Page_Count");
            _ormTable.Add("wordcount", "Word_Count");
            _ormTable.Add("bookname", "Book_Name");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("memo", "MEMO");
            _ormTable.Add("publishdate", "Publish_Date");
            _ormTable.Add("authors", "Authors");
            _ormTable.Add("keywords", "KeyWords");
            _ormTable.Add("revisers", "Revisers");
            _ormTable.Add("bookmaker", "bookmaker");
            _ormTable.Add("coverdesigner", "COVER_DESIGNER");
            _ormTable.Add("url", "url");
            _ormTable.Add("isgroupleader", "IS_GROUP_LEADER");
            _ormTable.Add("techniciantypeid", "TECHNICIAN_TYPE_ID");
            _ormTable.Add("orderindex","ORDER_INDEX");
            _ormTable.Add("version","VERSION");
            _ormTable.Add("authorsname", "Authors_Name");
        }

        /// <summary>
        /// 查询
        /// </summary>  
        /// <param name="bookId"></param>
        /// <param name="bookName"></param>
        /// <param name="knowledgeId"></param>
        /// <param name="bookNo"></param>
        /// <param name="publishOrg"></param>
        /// <param name="publishDate"></param>
        /// <param name="pageCount"></param>
        /// <param name="wordCount"></param>
        /// <param name="authors"></param>
        /// <param name="keyWords"></param>
        /// <param name="description">简介</param>
        /// <param name="memo">备注</param>       
        /// <param name="startRowIndex">起始记录行</param>
        /// <param name="maximumRows">每页记录条数</param>
        /// <param name="orderBy">排序字符串，如"FieldName ASC"</param>
        /// <returns></returns>
        public IList<Book> GetBook(int bookId, string bookName, int knowledgeId, string bookNo, int publishOrg,
            DateTime publishDate, int pageCount, int wordCount, string authors, string keyWords, string revisers,
            string bookmaker, string coverdesigner, string url, string description, string memo,
            int startRowIndex, int maximumRows, string orderBy)
        {
            IList<Book> books = new List<Book>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.AnsiString, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Book book = CreateModelObject(dataReader);

                    books.Add(book);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return books;
        }

        public Book GetBook(int bookID)
        {
            Book book = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookID);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, 0);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    book = CreateModelObject(dataReader);
                    book.Version = Convert.ToInt32(dataReader[GetMappingFieldName("Version")].ToString());
                }
            }

            sqlCommand = "USP_BOOK_RANGE_ORG_S";
            DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand1, "p_book_id", DbType.Int32, bookID);

            sqlCommand = "USP_BOOK_RANGE_POST_S";
            DbCommand dbCommand2 = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand2, "p_book_id", DbType.Int32, bookID);

            sqlCommand = "USP_BOOK_TRAIN_TYPE_S";
            DbCommand dbCommand3 = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand3, "p_book_id", DbType.Int32, bookID);

            IDataReader dataReader1 = db.ExecuteReader(dbCommand1);
            IDataReader dataReader2 = db.ExecuteReader(dbCommand2);
            IDataReader dataReader3 = db.ExecuteReader(dbCommand3);

            ArrayList orgidAL = new ArrayList();
            ArrayList postidAL = new ArrayList();
            ArrayList trainTypeidAL = new ArrayList();
            string strTrainTypeNames = string.Empty;

            KnowledgeDAL knowledgeDAL = new KnowledgeDAL();
            Knowledge knowledge = knowledgeDAL.GetKnowledge(book.knowledgeId);

            book.KnowledgeNames = GetKnowledgeNames("/" + knowledge.KnowledgeName, knowledge.ParentId);

            while (dataReader1.Read())
            {
                if (dataReader1["ORG_ID"].ToString() != "")
                {
                    orgidAL.Add(DataConvert.ToInt(dataReader1["ORG_ID"].ToString()));
                }
            }

            while (dataReader2.Read())
            {
                if (dataReader2["POST_ID"].ToString() != "")
                {
                    postidAL.Add(DataConvert.ToInt(dataReader2["POST_ID"].ToString()));
                }
            }

            while (dataReader3.Read())
            {
                if (dataReader3["TRAIN_TYPE_ID"].ToString() != "")
                {
                    trainTypeidAL.Add(DataConvert.ToInt(dataReader3["TRAIN_TYPE_ID"].ToString()));

                    strTrainTypeNames += GetTrainTypeNames("/" + dataReader3["TRAIN_TYPE_NAME"].ToString(), int.Parse(dataReader3["PARENT_ID"].ToString())) + ",";
                }
            }

            if (strTrainTypeNames.Length > 0)
            {
                strTrainTypeNames = strTrainTypeNames.Substring(0, strTrainTypeNames.Length - 1);
            }

            book.orgidAL = orgidAL;
            book.postidAL = postidAL;
            book.trainTypeidAL = trainTypeidAL;
            book.trainTypeNames = strTrainTypeNames;

            return book;
        }

        private string GetKnowledgeNames(string strName, int nID)
        {
            string strKnowledgeName = string.Empty;
            if (nID != 0)
            {
                KnowledgeDAL knowledgeDAL = new KnowledgeDAL();
                Knowledge knowledge = knowledgeDAL.GetKnowledge(nID);

                if (knowledge.ParentId != 0)
                {
                    strKnowledgeName = GetKnowledgeNames("/" + knowledge.KnowledgeName, knowledge.ParentId) + strName;
                }
                else
                {
                    strKnowledgeName = knowledge.KnowledgeName + strName;
                }
            }
            else
            {
                strKnowledgeName = strName.Replace("/", "");
            }

            return strKnowledgeName;
        }

        private string GetTrainTypeNames(string strName, int nID)
        {
            string strTrainTypeName = string.Empty;
            if (nID != 0)
            {
                TrainTypeDAL trainTypeDAL = new TrainTypeDAL();
                TrainType trainType = trainTypeDAL.GetTrainTypeInfo(nID);

                if (trainType.ParentID != 0)
                {
                    strTrainTypeName = GetTrainTypeNames("/" + trainType.TypeName, trainType.ParentID) + strName;
                }
                else
                {
                    strTrainTypeName = trainType.TypeName + strName;
                }
            }
            else
            {
                strTrainTypeName = strName.Replace("/", "");
            }
            return strTrainTypeName;
        }

        /// <summary>
        /// 取得某一培训岗位对应的Book信息
        /// </summary>
        /// <param name="postID"></param>
        /// <returns></returns>
        public IList<Book> GetTrainCourseBookChapterTree(int postID)
        {
            IList<Book> bookList = new List<Book>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TRAIN_COURSE_BOOK";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, postID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Book book = CreateModelObject(dataReader);

                    bookList.Add(book);
                }
            }

            return bookList;
        }

        /// <summary>
        /// 取得某学员当前需要学习的课程
        /// </summary>
        /// <param name="trainTypeID">培训类别ID</param>
        /// <param name="orgID">所属单位ID</param>
        /// <param name="postID">岗位ID</param>
        /// <returns></returns>
        public IList<Book> GetEmployeeStudyBookInfo(int trainTypeID, int orgID, int postID, bool isGroupleader,int techniciantypeid,int row)
        {
            IList<Book> bookList = new List<Book>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_TRAIN_EMPLOYEE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_type_id", DbType.Int32, trainTypeID);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, postID);
            db.AddInParameter(dbCommand, "p_row_num", DbType.Int32, row);
            db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, isGroupleader ? 1 : 0);
            db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, techniciantypeid); 
            
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Book book = CreateModelObject(dataReader);

                    bookList.Add(book);
                }
            }

            return bookList;
        }


        /// <summary>
        /// 取得当前需要学习的课程
        /// </summary>
        /// <param name="trainTypeID">培训类别ID</param>
        /// <param name="orgID">所属单位ID</param>
        /// <param name="postID">岗位ID</param>
        /// <param name="isGroupleader">是否班组长</param>
        /// <param name="tech">技能等级</param>
        /// <param name="row">取得记录条数</param>
        /// <returns></returns>
        public IList<Book> GetEmployeeStudyBookInfoByTrainTypeID(int trainTypeID, int orgID, int postID,int  isGroupleader,int tech,int row)
        {
            IList<Book> bookList = new List<Book>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_TRAIN_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_knowledge_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_train_type_id", DbType.Int32, trainTypeID);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, postID);
            db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, isGroupleader);
            db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, tech); 
            db.AddInParameter(dbCommand, "p_row_num", DbType.Int32, row);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Book book = CreateModelObject(dataReader);

                    bookList.Add(book);
                }
            }

            return bookList;
        }

        /// <summary>
        /// 取得当前需要学习的课程
        /// </summary>
        /// <param name="knowledgeID">教材体系ID</param>
        /// <param name="orgID">所属单位ID</param>
        /// <param name="postID">岗位ID</param>
        /// <param name="isGroupleader">是否班组长</param>
        /// <param name="tech">技能等级</param>
        /// <param name="row">取得记录条数</param>
        /// <returns></returns>
        public IList<Book> GetEmployeeStudyBookInfoByKnowledgeID(int knowledgeID, int orgID, int postID, int isGroupleader, int tech, int row)
        {
            IList<Book> bookList = new List<Book>();
            //temp solution
            if (knowledgeID == postID)
            {
                return bookList;
            }

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_TRAIN_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_knowledge_id", DbType.Int32, knowledgeID);
            db.AddInParameter(dbCommand, "p_train_type_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, postID);
            db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, isGroupleader);
            db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, tech);
            db.AddInParameter(dbCommand, "p_row_num", DbType.Int32, row);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Book book = CreateModelObject(dataReader);

                    bookList.Add(book);
                }
            }

            return bookList;
        }

        public IList<Book> GetBookInfoByDate(int row)
        {
            IList<Book> bookList = new List<Book>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_row_num", DbType.Int32, row);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Book book = CreateModelObject(dataReader);

                    bookList.Add(book);
                }
            }

            return bookList;
        }

        /// <summary>
        /// 取得所有book信息
        /// </summary>
        /// <returns></returns>
        public IList<Book> GetAllBookInfo(int orgID)
        {
            IList<Book> bookList = new List<Book>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32,orgID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Book book = CreateModelObject(dataReader);
                    book.AuthorsName = dataReader[GetMappingFieldName("AuthorsName")].ToString();
                    bookList.Add(book);
                }
            }

            return bookList;
        }


        public IList<Book> GetBookByTrainTypeIDPath(string trainTypeIDPath)
        {
            IList<Book> books = new List<Book>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_QByTrainID";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_id_path", DbType.String, trainTypeIDPath);
           

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Book book = CreateModelObject(dataReader);
                    books.Add(book);
                }
            }

            return books;
        }

        public IList<Book> GetBookByKnowledgeIDPath(string knowledgeIDPath )
        {
            IList<Book> books = new List<Book>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_QByKnowledgeID";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_id_path", DbType.String, knowledgeIDPath);
             

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Book book = CreateModelObject(dataReader);
                    books.Add(book);
                }
            }

            return books;
        }



        public IList<Book> GetBookByKnowledgeIDPath(string knowledgeIDPath, int orgid)
        {
            IList<Book> books = new List<Book>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_id_path", DbType.String, knowledgeIDPath);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Book book = CreateModelObject(dataReader);
                    books.Add(book);
                }
            }

            return books;
        }

        public int GetBookMaxID()
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_MAXID";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_max_id", DbType.Int32, 4);

            db.ExecuteNonQuery(dbCommand);

            int maxBookID = (int)db.GetParameterValue(dbCommand, "p_max_id");

            return maxBookID;
        }


        public IList<Book> GetBookByKnowledgeID(int knowledgeId, string bookName, string keyWords, string authors, int orgid)
        {
            IList<Book> books = new List<Book>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_ByCondition_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_knowledge_id", DbType.Int32, knowledgeId);
            db.AddInParameter(dbCommand, "p_book_Name", DbType.String, bookName);
            db.AddInParameter(dbCommand, "p_keyWords", DbType.String, keyWords);
            db.AddInParameter(dbCommand, "p_authors", DbType.String, authors);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Book book = CreateModelObject(dataReader);
					book.AuthorsName = dataReader[GetMappingFieldName("AuthorsName")].ToString();
                    books.Add(book);
                }
            }

            return books;
        }


        public IList<Book> GetBookByKnowledgeID(int knowledgeId,int orgid)
        {
            IList<Book> books = new List<Book>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_Q_KnowledgeID";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_knowledge_id", DbType.Int32, knowledgeId);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Book book = CreateModelObject(dataReader);
					book.AuthorsName = dataReader[GetMappingFieldName("AuthorsName")].ToString();
                    books.Add(book);
                }
            }

            return books;
        }


        public IList<Book> GetBookByKnowledgeOnline(int orgid,int postid, string idpath, bool isGroupleader,int techniciantypeid)
        {
            IList<Book> books = new List<Book>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_Q_Study";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);
            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, postid);
            db.AddInParameter(dbCommand, "p_id_path", DbType.String, idpath);
            db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, isGroupleader?1:0);
            db.AddInParameter(dbCommand, "p_tech", DbType.Int32, techniciantypeid);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Book book = CreateModelObject(dataReader);
                    books.Add(book);
                }
            }

            return books;
        }

        public IList<Book> GetBookByTrainTypeID(int trainTypeID, string bookName, string keyWords, string authors, int orgid)
        {
            IList<Book> books = new List<Book>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_ByConditionTrain_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_type_ID", DbType.Int32, trainTypeID);
            db.AddInParameter(dbCommand, "p_book_Name", DbType.String, bookName);
            db.AddInParameter(dbCommand, "p_keyWords", DbType.String, keyWords);
            db.AddInParameter(dbCommand, "p_authors", DbType.String, authors);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Book book = CreateModelObject(dataReader);
                    books.Add(book);
                }
            }

            return books;
        }

        public IList<Book> GetBookByTrainTypeID(int trainTypeID, int orgid)
        {
            IList<Book> books = new List<Book>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_Q_TrainTypeID";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_type_ID", DbType.Int32, trainTypeID);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Book book = CreateModelObject(dataReader);
                    books.Add(book);
                }
            }

            return books;
        }

		public IList<Book> GetBookByName(string strName)
		{
			IList<Book> books = new List<Book>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_BOOK_Q_Name";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_book_name", DbType.String, strName);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					Book book = CreateModelObject(dataReader);
					books.Add(book);
				}
			}

			return books;
		}

		public IList<Book> GetBookByPostID(int postID, string bookName, string keyWords, string authors, int orgid)
		{
			IList<Book> books = new List<Book>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_BOOK_ByCondition_Post_Q";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, postID);
			db.AddInParameter(dbCommand, "p_book_Name", DbType.String, bookName);
			db.AddInParameter(dbCommand, "p_keyWords", DbType.String, keyWords);
			db.AddInParameter(dbCommand, "p_authors", DbType.String, authors);
			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					Book book = CreateModelObject(dataReader);
					books.Add(book);
				}
			}

			return books;
		}

		public IList<Book> GetBookByPostID(int postID, int orgid)
		{
			IList<Book> books = new List<Book>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_BOOK_Q_PostID";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, postID);
			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					Book book = CreateModelObject(dataReader);
					books.Add(book);
				}
			}

			return books;
		}

        public IList<Book> GetBookByTrainTypeIDPath(string trainTypeIDPath, int orgid)
        {
            IList<Book> books = new List<Book>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_Q1";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_id_path", DbType.String, trainTypeIDPath);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Book book = CreateModelObject(dataReader);
                    books.Add(book);
                }
            }

            return books;
        }

        public int AddBook(Book book, string EmployeeName)
        {
            int id = 0;
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_book_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_book_name", DbType.String, book.bookName);
            db.AddInParameter(dbCommand, "p_knowledge_id", DbType.Int32, book.knowledgeId);
            db.AddInParameter(dbCommand, "p_book_no", DbType.String, book.bookNo);
            db.AddInParameter(dbCommand, "p_publish_org", DbType.Int32, book.publishOrg);
            db.AddInParameter(dbCommand, "p_publish_date", DbType.Date, book.publishDate);
            db.AddInParameter(dbCommand, "p_authors", DbType.String, book.authors);
            db.AddInParameter(dbCommand, "p_revisers", DbType.String, book.revisers);
            db.AddInParameter(dbCommand, "p_bookmaker", DbType.String, book.bookmaker);
            db.AddInParameter(dbCommand, "p_cover_designer", DbType.String, book.coverDesigner);
            db.AddInParameter(dbCommand, "p_keywords", DbType.String, book.keyWords);
            db.AddInParameter(dbCommand, "p_page_count", DbType.Int32, book.pageCount);
            db.AddInParameter(dbCommand, "p_word_count", DbType.Int32, book.wordCount);
            db.AddInParameter(dbCommand, "p_description", DbType.String, book.Description);
            db.AddInParameter(dbCommand, "p_url", DbType.String, book.url);
            db.AddInParameter(dbCommand, "p_last_update_person", DbType.String, EmployeeName);
            //db.AddInParameter(dbCommand, "p_last_update_date", DbType.Date, DateTime.Now);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, book.Memo);
            db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, book.IsGroupLearder );
            db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, book.TechnicianTypeID);
            db.AddOutParameter(dbCommand, "p_order_index", DbType.Int32, 4);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_book_id"));

                for (int i = 0; i < book.orgidAL.Count; i ++)
                {
                    sqlCommand = "USP_BOOK_RANGE_ORG_I";
                    DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand);

                    db.AddInParameter(dbCommand1, "p_book_id", DbType.Int32, id);
                    db.AddInParameter(dbCommand1, "p_org_id", DbType.Int32, int.Parse(book.orgidAL[i].ToString()));
                    db.ExecuteNonQuery(dbCommand1, transaction);
                }

                for (int i = 0; i < book.postidAL.Count; i ++)
                {
                    sqlCommand = "USP_BOOK_RANGE_POST_I";
                    DbCommand dbCommand4 = db.GetStoredProcCommand(sqlCommand);

                    db.AddInParameter(dbCommand4, "p_book_id", DbType.Int32, id);
                    db.AddInParameter(dbCommand4, "p_post_id", DbType.Int32, int.Parse(book.postidAL[i].ToString()));
                    db.ExecuteNonQuery(dbCommand4, transaction);
                }

                string strTrainType = "";

                for (int i = 0; i < book.trainTypeidAL.Count; i++)
                {
                    if(i==0)
                    {
                        strTrainType = strTrainType + book.trainTypeidAL[i].ToString();
                    }
                    else
                    {
                        strTrainType = strTrainType + "," + book.trainTypeidAL[i].ToString();
                    }
                }

                for (int i = 0; i < book.trainTypeidAL.Count; i ++)
                {
                        sqlCommand = "USP_BOOK_TRAIN_TYPE_I";
                        DbCommand dbCommand6 = db.GetStoredProcCommand(sqlCommand);

                        db.AddInParameter(dbCommand6, "p_book_id", DbType.Int32, id);
                        db.AddInParameter(dbCommand6, "p_train_type_id", DbType.Int32, int.Parse(book.trainTypeidAL[i].ToString()));
                        db.AddOutParameter(dbCommand6, "p_order_index", DbType.Int32, 4);
                        db.ExecuteNonQuery(dbCommand6, transaction);
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            connection.Close();

            return id;
        }

        public void UpateBookUrl(int bookID, string strUrl)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_UPDATE_URL";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookID);
            db.AddInParameter(dbCommand, "p_url", DbType.String, strUrl);

            db.ExecuteNonQuery(dbCommand);
        }

        public void  UpdateBook(Book book, string EmployeeName)
        {
            Database db =  DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, book.bookId);
            db.AddInParameter(dbCommand, "p_book_name", DbType.String, book.bookName);
            db.AddInParameter(dbCommand, "p_knowledge_id", DbType.Int32, book.knowledgeId);
            db.AddInParameter(dbCommand, "p_book_no", DbType.String, book.bookNo);
            db.AddInParameter(dbCommand, "p_publish_org", DbType.Int32, book.publishOrg);
            db.AddInParameter(dbCommand, "p_publish_date", DbType.DateTime, book.publishDate);
            db.AddInParameter(dbCommand, "p_authors", DbType.String, book.authors);
            db.AddInParameter(dbCommand, "p_revisers", DbType.String, book.revisers);
            db.AddInParameter(dbCommand, "p_bookmaker", DbType.String, book.bookmaker);
            db.AddInParameter(dbCommand, "p_cover_designer", DbType.String, book.coverDesigner);
            db.AddInParameter(dbCommand, "p_keywords", DbType.String, book.keyWords);
            db.AddInParameter(dbCommand, "p_page_count", DbType.Int32, book.pageCount);
            db.AddInParameter(dbCommand, "p_word_count", DbType.Int32, book.wordCount);
            db.AddInParameter(dbCommand, "p_description", DbType.String, book.Description);
            db.AddInParameter(dbCommand, "p_url", DbType.String, book.url);
            db.AddInParameter(dbCommand, "p_last_update_person", DbType.String, EmployeeName);
            //db.AddInParameter(dbCommand, "p_last_update_date", DbType.DateTime, DateTime.Now);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, book.Memo);
            db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, book.IsGroupLearder);
            db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, book.TechnicianTypeID);
            db.AddInParameter(dbCommand, "p_order_index", DbType.Int32, book.OrderIndex);

            sqlCommand = "USP_BOOK_RANGE_ORG_D";
            DbCommand dbCommand2 = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand2, "p_book_id", DbType.Int32, book.bookId);

            sqlCommand = "USP_BOOK_RANGE_POST_D";
            DbCommand dbCommand3 = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand3, "p_book_id", DbType.Int32, book.bookId);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);

                if(book.orgidAL.Count != 0)
                {
                    db.ExecuteNonQuery(dbCommand2, transaction);
                }

                if(book.postidAL.Count != 0)
                {
                    db.ExecuteNonQuery(dbCommand3, transaction);
                }

                for (int i = 0; i < book.orgidAL.Count; i ++)
                {
                    sqlCommand = "USP_BOOK_RANGE_ORG_I";
                    dbCommand = db.GetStoredProcCommand(sqlCommand);

                    db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, book.bookId);
                    db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, int.Parse(book.orgidAL[i].ToString()));
                    db.ExecuteNonQuery(dbCommand, transaction);
                }

                for (int i = 0; i < book.postidAL.Count; i ++)
                {
                    sqlCommand = "USP_BOOK_RANGE_POST_I";
                    dbCommand = db.GetStoredProcCommand(sqlCommand);

                    db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, book.bookId);
                    db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, int.Parse(book.postidAL[i].ToString()));
                    db.ExecuteNonQuery(dbCommand, transaction);
                }

                ArrayList objList = new ArrayList();
                BookTrainTypeDAL dal = new BookTrainTypeDAL();
                IList<BookTrainType> objTrainTypeList = dal.GetBookTrainTypeByBookID(book.bookId);

                foreach (BookTrainType type in objTrainTypeList)
                {
                    objList.Add(type.TrainTypeID.ToString());
                    if(book.trainTypeidAL.IndexOf(type.TrainTypeID.ToString()) == -1)
                    {
                        sqlCommand = "USP_BOOK_TRAIN_TYPE_D";
                        DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand);
                        db.AddInParameter(dbCommand1, "p_book_id", DbType.Int32, book.bookId);
                        db.AddInParameter(dbCommand1, "p_train_type_id", DbType.String, type.TrainTypeID);
                        db.ExecuteNonQuery(dbCommand1, transaction);
                    }
                }

                for (int i = 0; i < book.trainTypeidAL.Count; i++)
                {
                    //新增的培训类别
                    if (objList.IndexOf(book.trainTypeidAL[i].ToString()) == -1)
                    {
                        sqlCommand = "USP_BOOK_TRAIN_TYPE_I";
                        DbCommand dbCommand6 = db.GetStoredProcCommand(sqlCommand);

                        db.AddInParameter(dbCommand6, "p_book_id", DbType.Int32, book.bookId);
                        db.AddInParameter(dbCommand6, "p_train_type_id", DbType.Int32, int.Parse(book.trainTypeidAL[i].ToString()));
                        db.AddOutParameter(dbCommand6, "p_order_index", DbType.Int32, 4);
                        db.ExecuteNonQuery(dbCommand6, transaction);
                    }
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();

            }
            connection.Close();
        }

        public void DeleteBook(int bookID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookID);

            db.ExecuteNonQuery(dbCommand);
        }

        public void UpdateBookVersion(int bookID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BOOK_U_Version";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookID);

            db.ExecuteNonQuery(dbCommand);
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

        public static Book CreateModelObject(IDataReader dataReader)
        {
            return new Book(
                DataConvert.ToInt(dataReader[GetMappingFieldName("BookId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("BookName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("KnowledgeId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("KnowledgeName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("BookNo")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("PublishOrg")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("PublishOrgName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("PageCount")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("WordCount")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("PublishDate")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Authors")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("KeyWords")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Revisers")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Bookmaker")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("CoverDesigner")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Url")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("IsGroupLeader")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("TechnicianTypeID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrderIndex")]));
        }
    }
}