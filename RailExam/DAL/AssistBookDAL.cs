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
    public class AssistBookDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static AssistBookDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("assistbookid", "Assist_Book_ID");
            _ormTable.Add("assistbookcategoryid", "CATEGORY_ID");
            _ormTable.Add("assistbookcategoryname", "CATEGORY_Name");
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
        }

        /// <summary>
        /// 查询
        /// </summary>  
        /// <param name="AssistBookId"></param>
        /// <param name="bookName"></param>
        /// <param name="AssistCategoryId"></param>
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
        public IList<AssistBook> GetAssistBook(int AssistBookId, string bookName, int AssistCategoryId, string bookNo, int publishOrg,
            DateTime publishDate, int pageCount, int wordCount, string authors, string keyWords, string revisers,
            string bookmaker, string coverdesigner, string url, string description, string memo,
            int startRowIndex, int maximumRows, string orderBy)
        {
            IList<AssistBook> books = new List<AssistBook>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.AnsiString, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    AssistBook book = CreateModelObject(dataReader);

                    books.Add(book);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return books;
        }

        /// <summary>
        /// 根据辅导教材ID查询教材
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns></returns>
        public AssistBook GetAssistBook(int bookID)
        {
            AssistBook book = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "p_assist_book_id", DbType.Int32, bookID);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, 0);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    book = CreateModelObject(dataReader);
                }
            }

            sqlCommand = "USP_ASSIST_BOOK_RANGE_ORG_S";
            DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand1, "p_assist_book_id", DbType.Int32, bookID);

            sqlCommand = "USP_ASSIST_BOOK_RANGE_POST_S";
            DbCommand dbCommand2 = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand2, "p_assist_book_id", DbType.Int32, bookID);

            sqlCommand = "USP_ASSIST_BOOK_TRAIN_TYPE_S";
            DbCommand dbCommand3 = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand3, "p_assist_book_id", DbType.Int32, bookID);

            IDataReader dataReader1 = db.ExecuteReader(dbCommand1);
            IDataReader dataReader2 = db.ExecuteReader(dbCommand2);
            IDataReader dataReader3 = db.ExecuteReader(dbCommand3);

            ArrayList orgidAL = new ArrayList();
            ArrayList postidAL = new ArrayList();
            ArrayList trainTypeidAL = new ArrayList();
            string strTrainTypeNames = string.Empty;

            AssistBookCategoryDAL assistDAL = new AssistBookCategoryDAL();
            AssistBookCategory assistcategory = assistDAL.GetAssistBookCategory(book.AssistBookCategoryId);

            book.AssistBookCategoryName = GetAssistAssistBookCategoryNames("/" + assistcategory.AssistBookCategoryName, assistcategory.ParentId);

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

                    strTrainTypeNames += GetTrainTypeNames("/" + dataReader3["TYPE_NAME"].ToString(), int.Parse(dataReader3["PARENT_ID"].ToString())) + ",";
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

        private string GetAssistAssistBookCategoryNames(string strName, int nID)
        {
            string strAssistAssistBookCategoryName = string.Empty;
            if (nID != 0)
            {
                AssistBookCategoryDAL AssistCategoryDAL = new AssistBookCategoryDAL();
                AssistBookCategory AssistCategory = AssistCategoryDAL.GetAssistBookCategory(nID);

                if (AssistCategory.ParentId != 0)
                {
                    strAssistAssistBookCategoryName = GetAssistAssistBookCategoryNames("/" + AssistCategory.AssistBookCategoryName, AssistCategory.ParentId) + strName;
                }
                else
                {
                    strAssistAssistBookCategoryName = AssistCategory.AssistBookCategoryName + strName;
                }
            }
            else
            {
                strAssistAssistBookCategoryName = strName.Replace("/", "");
            }
            return strAssistAssistBookCategoryName;
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

            return strTrainTypeName;
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
        public IList<AssistBook> GetStudyAssistBookInfoByTrainTypeID(int trainTypeID, int orgID, int postID,int  isGroupleader,int tech,int row)
        {
            IList<AssistBook> bookList = new List<AssistBook>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_TRAIN_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_assist_category_id", DbType.Int32, 0);
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
                    AssistBook book = CreateModelObject(dataReader);

                    bookList.Add(book);
                }
            }

            return bookList;
        }

        /// <summary>
        /// 取得当前需要学习的课程
        /// </summary>
        /// <param name="AssistCategoryID">教材体系ID</param>
        /// <param name="orgID">所属单位ID</param>
        /// <param name="postID">岗位ID</param>
        /// <param name="isGroupleader">是否班组长</param>
        /// <param name="tech">技能等级</param>
        /// <param name="row">取得记录条数</param>
        /// <returns></returns>
        public IList<AssistBook> GetStudyAssistBookInfoByAssistBookCategoryID(int AssistCategoryID, int orgID, int postID, int isGroupleader, int tech, int row)
        {
            IList<AssistBook> bookList = new List<AssistBook>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_TRAIN_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_assist_category_id", DbType.Int32, AssistCategoryID);
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
                    AssistBook book = CreateModelObject(dataReader);

                    bookList.Add(book);
                }
            }

            return bookList;
        }

        /// <summary>
        /// 取得所有book信息
        /// </summary>
        /// <returns></returns>
        public IList<AssistBook> GetAllAssistBookInfo(int orgID)
        {
            IList<AssistBook> bookList = new List<AssistBook>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_assist_book_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32,orgID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    AssistBook book = CreateModelObject(dataReader);

                    bookList.Add(book);
                }
            }

            return bookList;
        }


        public IList<AssistBook> GetAssistBookByTrainTypeIDPath(string trainTypeIDPath)
        {
            IList<AssistBook> books = new List<AssistBook>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_TrainIDPath";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_id_path", DbType.String, trainTypeIDPath);
           

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    AssistBook book = CreateModelObject(dataReader);
                    books.Add(book);
                }
            }

            return books;
        }

        public IList<AssistBook> GetAssistBookByAssistBookCategoryIDPath(string AssistCategoryIDPath )
        {
            IList<AssistBook> books = new List<AssistBook>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_CategoryIDPath";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_id_path", DbType.String, AssistCategoryIDPath);
             

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    AssistBook book = CreateModelObject(dataReader);
                    books.Add(book);
                }
            }

            return books;
        }



        public IList<AssistBook> GetAssistBookByAssistBookCategoryIDPath(string AssistCategoryIDPath, int orgid)
        {
            IList<AssistBook> books = new List<AssistBook>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_S_Category";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_id_path", DbType.String, AssistCategoryIDPath);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    AssistBook book = CreateModelObject(dataReader);
                    books.Add(book);
                }
            }

            return books;
        }


        public IList<AssistBook> GetAssistBookByAssistBookCategoryID(int AssistCategoryId, string bookName, string keyWords, string authors, int orgid)
        {
            IList<AssistBook> books = new List<AssistBook>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_Q_Category";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_assist_category_id", DbType.Int32, AssistCategoryId);
            db.AddInParameter(dbCommand, "p_book_Name", DbType.String, bookName);
            db.AddInParameter(dbCommand, "p_keyWords", DbType.String, keyWords);
            db.AddInParameter(dbCommand, "p_authors", DbType.String, authors);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    AssistBook book = CreateModelObject(dataReader);
                    books.Add(book);
                }
            }

            return books;
        }

        public IList<AssistBook> GetAssistBookByTrainTypeID(int trainTypeID, string bookName, string keyWords, string authors, int orgid)
        {
            IList<AssistBook> books = new List<AssistBook>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_Q_TrainType";
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
                    AssistBook book = CreateModelObject(dataReader);
                    books.Add(book);
                }
            }

            return books;
        }


        public IList<AssistBook> GetAssistBookByAssistBookCategoryID(int AssistCategoryId,  int orgid)
        {
            IList<AssistBook> books = new List<AssistBook>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_Q_CategoryID";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_assist_category_id", DbType.Int32, AssistCategoryId);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    AssistBook book = CreateModelObject(dataReader);
                    books.Add(book);
                }
            }

            return books;
        }

        public IList<AssistBook> GetAssistBookByTrainTypeID(int trainTypeID,  int orgid)
        {
            IList<AssistBook> books = new List<AssistBook>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_Q_TrainTypeID";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_train_type_ID", DbType.Int32, trainTypeID);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    AssistBook book = CreateModelObject(dataReader);
                    books.Add(book);
                }
            }

            return books;
        }

        public IList<AssistBook> GetAssistBookByTrainTypeIDPath(string trainTypeIDPath, int orgid)
        {
            IList<AssistBook> books = new List<AssistBook>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_S_TrainType";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_id_path", DbType.String, trainTypeIDPath);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    AssistBook book = CreateModelObject(dataReader);
                    books.Add(book);
                }
            }

            return books;
        }

        public int AddAssistBook(AssistBook book, string EmployeeName)
        {
            int id = 0;
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_assist_book_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_book_name", DbType.String, book.BookName);
            db.AddInParameter(dbCommand, "p_assist_category_id", DbType.Int32, book.AssistBookCategoryId);
            db.AddInParameter(dbCommand, "p_book_no", DbType.String, book.BookNo);
            db.AddInParameter(dbCommand, "p_publish_org", DbType.Int32, book.PublishOrg);
            db.AddInParameter(dbCommand, "p_publish_date", DbType.DateTime, book.PublishDate);
            db.AddInParameter(dbCommand, "p_authors", DbType.String, book.Authors);
            db.AddInParameter(dbCommand, "p_revisers", DbType.String, book.Revisers);
            db.AddInParameter(dbCommand, "p_bookmaker", DbType.String, book.Bookmaker);
            db.AddInParameter(dbCommand, "p_cover_designer", DbType.String, book.CoverDesigner);
            db.AddInParameter(dbCommand, "p_keywords", DbType.String, book.KeyWords);
            db.AddInParameter(dbCommand, "p_page_count", DbType.Int32, book.PageCount);
            db.AddInParameter(dbCommand, "p_word_count", DbType.Int32, book.WordCount);
            db.AddInParameter(dbCommand, "p_description", DbType.String, book.Description);
            db.AddInParameter(dbCommand, "p_url", DbType.String, book.url);
            db.AddInParameter(dbCommand, "p_last_update_person", DbType.String, EmployeeName);
            //db.AddInParameter(dbCommand, "p_last_update_date", DbType.Date, DateTime.Now);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, book.Memo);
            db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, book.IsGroupLearder );
            db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, book.TechnicianTypeID);
            db.AddOutParameter(dbCommand,"p_order_index",DbType.Int32,4);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_assist_book_id"));

                sqlCommand = "USP_ASSIST_BOOK_RANGE_ORG_D";
                DbCommand dbCommand2 = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand2, "p_assist_book_id", DbType.Int32, id);
                db.ExecuteNonQuery(dbCommand2, transaction);

                sqlCommand = "USP_ASSIST_BOOK_RANGE_POST_D";
                DbCommand dbCommand3 = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand3, "p_assist_book_id", DbType.Int32, id);
                db.ExecuteNonQuery(dbCommand3, transaction);

                sqlCommand = "USP_ASSIST_BOOK_TRAIN_TYPE_D";
                DbCommand dbCommand5 = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand5, "p_assist_book_id", DbType.Int32, id);
                db.ExecuteNonQuery(dbCommand5, transaction);

                for (int i = 0; i < book.orgidAL.Count; i ++)
                {
                    sqlCommand = "USP_ASSIST_BOOK_RANGE_ORG_I";
                    DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand);

                    db.AddInParameter(dbCommand1, "p_assist_book_id", DbType.Int32, id);
                    db.AddInParameter(dbCommand1, "p_org_id", DbType.Int32, int.Parse(book.orgidAL[i].ToString()));
                    db.ExecuteNonQuery(dbCommand1, transaction);
                }

                for (int i = 0; i < book.postidAL.Count; i ++)
                {
                    sqlCommand = "USP_ASSIST_BOOK_RANGE_POST_I";
                    DbCommand dbCommand4 = db.GetStoredProcCommand(sqlCommand);

                    db.AddInParameter(dbCommand4, "p_assist_book_id", DbType.Int32, id);
                    db.AddInParameter(dbCommand4, "p_post_id", DbType.Int32, int.Parse(book.postidAL[i].ToString()));
                    db.ExecuteNonQuery(dbCommand4, transaction);
                }

                for (int i = 0; i < book.trainTypeidAL.Count; i ++)
                {
                    sqlCommand = "USP_ASSIST_BOOK_TRAIN_TYPE_I";
                    DbCommand dbCommand6 = db.GetStoredProcCommand(sqlCommand);

                    db.AddInParameter(dbCommand6, "p_assist_book_id", DbType.Int32, id);
                    db.AddInParameter(dbCommand6, "p_train_type_id", DbType.Int32, int.Parse(book.trainTypeidAL[i].ToString()));
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

        public void UpateAssistBookUrl(int bookID, string strUrl)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_UPDATE_URL";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_assist_book_id", DbType.Int32, bookID);
            db.AddInParameter(dbCommand, "p_url", DbType.String, strUrl);

            db.ExecuteNonQuery(dbCommand);
        }

        public void  UpdateAssistBook(AssistBook book, string EmployeeName)
        {
            Database db =  DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_assist_book_id", DbType.Int32, book.AssistBookId);
            db.AddInParameter(dbCommand, "p_book_name", DbType.String, book.BookName);
            db.AddInParameter(dbCommand, "p_assist_category_id", DbType.Int32, book.AssistBookCategoryId);
            db.AddInParameter(dbCommand, "p_book_no", DbType.String, book.BookNo);
            db.AddInParameter(dbCommand, "p_publish_org", DbType.Int32, book.PublishOrg);
            db.AddInParameter(dbCommand, "p_publish_date", DbType.DateTime, book.PublishDate);
            db.AddInParameter(dbCommand, "p_authors", DbType.String, book.Authors);
            db.AddInParameter(dbCommand, "p_revisers", DbType.String, book.Revisers);
            db.AddInParameter(dbCommand, "p_bookmaker", DbType.String, book.Bookmaker);
            db.AddInParameter(dbCommand, "p_cover_designer", DbType.String, book.CoverDesigner);
            db.AddInParameter(dbCommand, "p_keywords", DbType.String, book.KeyWords);
            db.AddInParameter(dbCommand, "p_page_count", DbType.Int32, book.PageCount);
            db.AddInParameter(dbCommand, "p_word_count", DbType.Int32, book.WordCount);
            db.AddInParameter(dbCommand, "p_description", DbType.String, book.Description);
            db.AddInParameter(dbCommand, "p_url", DbType.String, book.url);
            db.AddInParameter(dbCommand, "p_last_update_person", DbType.String, EmployeeName);
            //db.AddInParameter(dbCommand, "p_last_update_date", DbType.DateTime, DateTime.Now);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, book.Memo);
            db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, book.IsGroupLearder);
            db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, book.TechnicianTypeID);
            db.AddInParameter(dbCommand, "p_order_index", DbType.Int32, book.OrderIndex);

            sqlCommand = "USP_ASSIST_BOOK_TRAIN_TYPE_D";
            DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand1, "p_assist_book_id", DbType.Int32, book.AssistBookId);

            sqlCommand = "USP_ASSIST_BOOK_RANGE_ORG_D";
            DbCommand dbCommand2 = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand2, "p_assist_book_id", DbType.Int32, book.AssistBookId);

            sqlCommand = "USP_ASSIST_BOOK_RANGE_POST_D";
            DbCommand dbCommand3 = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand3, "p_assist_book_id", DbType.Int32, book.AssistBookId);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                if(book.trainTypeidAL.Count != 0)
                {
                    db.ExecuteNonQuery(dbCommand1, transaction);
                }
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
                    sqlCommand = "USP_ASSIST_BOOK_RANGE_ORG_I";
                    dbCommand = db.GetStoredProcCommand(sqlCommand);

                    db.AddInParameter(dbCommand, "p_assist_book_id", DbType.Int32, book.AssistBookId);
                    db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, int.Parse(book.orgidAL[i].ToString()));
                    db.ExecuteNonQuery(dbCommand, transaction);
                }

                for (int i = 0; i < book.postidAL.Count; i ++)
                {
                    sqlCommand = "USP_ASSIST_BOOK_RANGE_POST_I";
                    dbCommand = db.GetStoredProcCommand(sqlCommand);

                    db.AddInParameter(dbCommand, "p_assist_book_id", DbType.Int32, book.AssistBookId);
                    db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, int.Parse(book.postidAL[i].ToString()));
                    db.ExecuteNonQuery(dbCommand, transaction);
                }

                for (int i = 0; i < book.trainTypeidAL.Count; i ++)
                {
                    sqlCommand = "USP_ASSIST_BOOK_TRAIN_TYPE_I";
                    dbCommand = db.GetStoredProcCommand(sqlCommand);

                    db.AddInParameter(dbCommand, "p_assist_book_id", DbType.Int32, book.AssistBookId);
                    db.AddInParameter(dbCommand, "p_train_type_id", DbType.Int32, int.Parse(book.trainTypeidAL[i].ToString()));
                    db.ExecuteNonQuery(dbCommand, transaction);
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            connection.Close();
        }

        public void DeleteAssistBook(int bookID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_assist_book_id", DbType.Int32, bookID);

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

        public static AssistBook CreateModelObject(IDataReader dataReader)
        {
            return new AssistBook(
                DataConvert.ToInt(dataReader[GetMappingFieldName("AssistBookId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("BookName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("AssistBookCategoryId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("AssistBookCategoryName")]),
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
