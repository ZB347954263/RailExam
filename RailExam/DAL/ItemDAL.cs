using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
    public class ItemDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static ItemDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("itemid", "ITEM_ID");
            _ormTable.Add("bookid", "BOOK_ID");
            _ormTable.Add("bookname", "BOOK_NAME");
            _ormTable.Add("chapterid", "CHAPTER_ID");
            _ormTable.Add("chaptername", "CHAPTER_NAME");
            _ormTable.Add("categoryid", "CATEGORY_ID");
            _ormTable.Add("categoryname", "CATEGORY_NAME");
            _ormTable.Add("organizationid", "ORG_ID");
            _ormTable.Add("organizationname", "SHORT_NAME");
            _ormTable.Add("typeid", "TYPE_ID");
            _ormTable.Add("typename", "TYPE_NAME");
            _ormTable.Add("completetime", "COMPLETE_TIME");
            _ormTable.Add("difficultyid", "DIFFICULTY_ID");
            _ormTable.Add("difficultyname", "DIFFICULTY_NAME");
            _ormTable.Add("source", "SOURCE");
            _ormTable.Add("version", "VERSION");
            _ormTable.Add("score", "SCORE");
            _ormTable.Add("content", "CONTENT");
            _ormTable.Add("answercount", "ANSWER_COUNT");
            _ormTable.Add("selectanswer", "SELECT_ANSWER");
            _ormTable.Add("standardanswer", "STANDARD_ANSWER");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("outdatedate", "OUTDATE_DATE");
            _ormTable.Add("usedcount", "USED_COUNT");
            _ormTable.Add("statusid", "STATUS_ID");
            _ormTable.Add("statusname", "STATUS_NAME");
            _ormTable.Add("createperson", "CREATE_PERSON");
            _ormTable.Add("createtime", "CREATE_TIME");
            _ormTable.Add("usageid", "USAGE_ID");
            _ormTable.Add("memo", "MEMO");
            _ormTable.Add("asc", "ASC");
            _ormTable.Add("desc", "DESC");
            _ormTable.Add("haspicture","HASPICTURE");
			_ormTable.Add("keyword","KEY_WORD");
            _ormTable.Add("mothercode", "MOTHER_CODE");
            _ormTable.Add("itemindex", "ITEM_INDEX");
            _ormTable.Add("authors","AUTHORS");
        }

        /// <summary>
        /// 新增试题
        /// </summary>
        /// <param name="item">试题</param>
        /// <returns>新ID</returns>
        public int AddItem(Item item)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_item_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, item.BookId);
            db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, item.ChapterId);

            if (item.CategoryId > 0)
            {
                db.AddInParameter(dbCommand, "p_category_id", DbType.String, item.CategoryId);
            }
            else
            {
                db.AddInParameter(dbCommand, "p_category_id", DbType.String, null);
            }

            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, item.OrganizationId);
            db.AddInParameter(dbCommand, "p_type_id", DbType.Int32, item.TypeId);
            db.AddInParameter(dbCommand, "p_complete_time", DbType.Int32, item.CompleteTime);
            db.AddInParameter(dbCommand, "p_difficulty_id", DbType.Int32, item.DifficultyId);
            db.AddInParameter(dbCommand, "p_source", DbType.String, item.Source);
            db.AddInParameter(dbCommand, "p_version", DbType.String, item.Version);
            db.AddInParameter(dbCommand, "p_score", DbType.Decimal, item.Score);
            db.AddInParameter(dbCommand, "p_content", DbType.String, item.Content);
            db.AddInParameter(dbCommand, "p_answer_count", DbType.Int32, item.AnswerCount);
            db.AddInParameter(dbCommand, "p_select_answer", DbType.String, item.SelectAnswer);
            db.AddInParameter(dbCommand, "p_standard_answer", DbType.String, item.StandardAnswer);
            db.AddInParameter(dbCommand, "p_description", DbType.String, item.Description);
            db.AddInParameter(dbCommand, "p_outdate_date", DbType.DateTime, item.OutDateDate);
            db.AddInParameter(dbCommand, "p_used_count", DbType.Int32, item.UsedCount);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, item.StatusId);
            db.AddInParameter(dbCommand, "p_create_person", DbType.String, item.CreatePerson);
            db.AddInParameter(dbCommand, "p_create_time", DbType.DateTime, item.CreateTime);
            db.AddInParameter(dbCommand, "p_usage_id", DbType.Int32, item.UsageId);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, item.Memo);
			db.AddInParameter(dbCommand, "p_key_word", DbType.String, item.KeyWord);
            db.AddInParameter(dbCommand,"p_has_picture",DbType.Int32,item.HasPicture);
            db.AddInParameter(dbCommand, "p_parent_item_id", DbType.Int32, item.ParentItemId);
            db.AddInParameter(dbCommand, "p_mother_code", DbType.String, item.MotherCode);
            db.AddInParameter(dbCommand, "p_item_index", DbType.Int32, item.ItemIndex);

            int nRecordAffected = db.ExecuteNonQuery(dbCommand);
            item.ItemId = (int)db.GetParameterValue(dbCommand, "p_item_id");

            return item.ItemId;
        }

		public void AddItem(IList<Item> objList)
		{
			Database db = DatabaseFactory.CreateDatabase();
			
			DbConnection connection = db.CreateConnection();
			connection.Open();
			DbTransaction transaction = connection.BeginTransaction();

			try
			{
			    int preItemId = 0;
				foreach (Item item in objList)
				{
					string sqlCommand = "USP_ITEM_I";
					DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

					db.AddOutParameter(dbCommand, "p_item_id", DbType.Int32, 4);
					db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, item.BookId);
					db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, item.ChapterId);

					if (item.CategoryId > 0)
					{
						db.AddInParameter(dbCommand, "p_category_id", DbType.String, item.CategoryId);
					}
					else
					{
						db.AddInParameter(dbCommand, "p_category_id", DbType.String, null);
					}


					db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, item.OrganizationId);
					db.AddInParameter(dbCommand, "p_type_id", DbType.Int32, item.TypeId);
					db.AddInParameter(dbCommand, "p_complete_time", DbType.Int32, item.CompleteTime);
					db.AddInParameter(dbCommand, "p_difficulty_id", DbType.Int32, item.DifficultyId);
					db.AddInParameter(dbCommand, "p_source", DbType.String, item.Source);
					db.AddInParameter(dbCommand, "p_version", DbType.String, item.Version);
					db.AddInParameter(dbCommand, "p_score", DbType.Decimal, item.Score);
					db.AddInParameter(dbCommand, "p_content", DbType.String, item.Content);
					db.AddInParameter(dbCommand, "p_answer_count", DbType.Int32, item.AnswerCount);
					db.AddInParameter(dbCommand, "p_select_answer", DbType.String, item.SelectAnswer);
					db.AddInParameter(dbCommand, "p_standard_answer", DbType.String, item.StandardAnswer);
					db.AddInParameter(dbCommand, "p_description", DbType.String, item.Description);
					db.AddInParameter(dbCommand, "p_outdate_date", DbType.DateTime, item.OutDateDate);
					db.AddInParameter(dbCommand, "p_used_count", DbType.Int32, item.UsedCount);
					db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, item.StatusId);
					db.AddInParameter(dbCommand, "p_create_person", DbType.String, item.CreatePerson);
					db.AddInParameter(dbCommand, "p_create_time", DbType.DateTime, item.CreateTime);
					db.AddInParameter(dbCommand, "p_usage_id", DbType.Int32, item.UsageId);
					db.AddInParameter(dbCommand, "p_memo", DbType.String, item.Memo);
					db.AddInParameter(dbCommand, "p_key_word", DbType.String, item.KeyWord);
					db.AddInParameter(dbCommand, "p_has_picture", DbType.Int32, item.HasPicture);

                    if(item.TypeId == 5)
                    {
                        db.AddInParameter(dbCommand, "p_parent_item_id", DbType.Int32, preItemId);
                    }
                    else
                    {
                        db.AddInParameter(dbCommand, "p_parent_item_id", DbType.Int32, 0);
                    }
                    db.AddInParameter(dbCommand, "p_mother_code", DbType.String, item.MotherCode);
                    db.AddInParameter(dbCommand, "p_item_index", DbType.Int32, item.ItemIndex);

					db.ExecuteNonQuery(dbCommand,transaction);
					item.ItemId = (int)db.GetParameterValue(dbCommand, "p_item_id");

                    //当试题类别为完型填空主题时记录下id
                    if(item.TypeId == 4)
                    {
                        preItemId = item.ItemId;
                    }
				}
				transaction.Commit();
			}
			catch
			{
				transaction.Rollback();
			}
			finally
			{
				connection.Close();
			}
		}

        public IList<Item> GetItemsByParentItemID(int parentItemID)
        {

            IList<Item> items = new List<Item>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_PARENT_ITEM_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_parent_item_id", DbType.Int32, parentItemID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Item item = CreateModelObject(dataReader);
                    item.ItemIndex = Convert.ToInt32(dataReader[GetMappingFieldName("ItemIndex")].ToString());
                    item.MotherCode = DataConvert.ToString(dataReader[GetMappingFieldName("MotherCode")]);
                    items.Add(item);
                }
            }

            return items;
        }

        /// <summary>
        /// 删除试题
        /// </summary>
        /// <param name="itemId">试题ID</param>
        /// <returns>数据库受影响的行数</returns>
        public int DeleteItem(int itemId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_item_id", DbType.Int32, itemId);

            return db.ExecuteNonQuery(dbCommand);
        }

		public int DeleteItemByChapterID(int chapterID)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_ITEM_D_Chapter";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, chapterID);

			return db.ExecuteNonQuery(dbCommand);
		}

		public int DeleteItemByBookID(int bookID)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_ITEM_D_Book";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookID);

			return db.ExecuteNonQuery(dbCommand);
		}

        public int UpdateItemEnabled(int bookId,int chapterId,int stautsId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_U_status";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_item_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookId);
            db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, chapterId);
            db.AddOutParameter(dbCommand,"p_id_path",DbType.String,20);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, stautsId);

            return db.ExecuteNonQuery(dbCommand);
        }

        public int UpdateItemEnabled(int itemId, int statusId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_U_status";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_item_id", DbType.Int32, itemId);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, 0);
            db.AddOutParameter(dbCommand, "p_id_path", DbType.String, 20);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, statusId);

            return db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 删除试题
        /// </summary>
        /// <param name="itemIds">试题</param>
        /// <returns>数据库受影响的行数</returns>
        public int DeleteItem(int[] itemIds)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction tran = conn.BeginTransaction();
            DbCommand dbCommand;
            int nAffectedRecords = 0;

            try
            {
                foreach(int itemId in itemIds)
                {
                    dbCommand = db.GetStoredProcCommand("USP_ITEM_D");
                    db.AddInParameter(dbCommand, "p_item_id", DbType.Int32, itemId);
                    nAffectedRecords += db.ExecuteNonQuery(dbCommand);
                }

                tran.Commit();
            }
            catch(Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return nAffectedRecords;
        }

        /// <summary>
        /// 修改试题
        /// </summary>
        /// <param name="item">试题</param>
        /// <returns>数据库受影响的行数</returns>
        public void UpdateItem(Item item)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_item_id", DbType.Int32, item.ItemId);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, item.BookId);
            db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, item.ChapterId);
            if (item.CategoryId > 0)
            {
                db.AddInParameter(dbCommand, "p_category_id", DbType.String, item.CategoryId);
            }
            else
            {
                db.AddInParameter(dbCommand, "p_category_id", DbType.String, null);
            } 
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, item.OrganizationId);
            db.AddInParameter(dbCommand, "p_type_id", DbType.Int32, item.TypeId);
            db.AddInParameter(dbCommand, "p_complete_time", DbType.Int32, item.CompleteTime);
            db.AddInParameter(dbCommand, "p_difficulty_id", DbType.Int32, item.DifficultyId);
            db.AddInParameter(dbCommand, "p_source", DbType.String, item.Source);
            db.AddInParameter(dbCommand, "p_version", DbType.String, item.Version);
            db.AddInParameter(dbCommand, "p_score", DbType.Decimal, item.Score);
            db.AddInParameter(dbCommand, "p_content", DbType.String, item.Content);
            db.AddInParameter(dbCommand, "p_answer_count", DbType.Int32, item.AnswerCount);
            db.AddInParameter(dbCommand, "p_select_answer", DbType.String, item.SelectAnswer);
            db.AddInParameter(dbCommand, "p_standard_answer", DbType.String, item.StandardAnswer);
            db.AddInParameter(dbCommand, "p_description", DbType.String, item.Description);
            db.AddInParameter(dbCommand, "p_outdate_date", DbType.DateTime, item.OutDateDate);
            db.AddInParameter(dbCommand, "p_used_count", DbType.Int32, item.UsedCount);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, item.StatusId);
            db.AddInParameter(dbCommand, "p_create_person", DbType.String, item.CreatePerson);
            db.AddInParameter(dbCommand, "p_create_time", DbType.DateTime, item.CreateTime);
            db.AddInParameter(dbCommand, "p_usage_id", DbType.Int32, item.UsageId);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, item.Memo);
			db.AddInParameter(dbCommand, "p_key_word", DbType.String, item.KeyWord);
			db.AddInParameter(dbCommand, "p_has_picture", DbType.Int32, item.HasPicture);
            db.AddInParameter(dbCommand, "p_parent_item_id", DbType.Int32, item.ParentItemId);
            db.AddInParameter(dbCommand, "p_mother_code", DbType.String, item.MotherCode);
            db.AddInParameter(dbCommand, "p_item_index", DbType.Int32, item.ItemIndex);

            db.ExecuteNonQuery(dbCommand);
        }


		public void UpdateItem(IList<Item> objList)
		{
			Database db = DatabaseFactory.CreateDatabase();
			
			DbConnection connection = db.CreateConnection();
			connection.Open();
			DbTransaction transaction = connection.BeginTransaction();
			 try
            {
				foreach (Item item in objList)
				{
					DbCommand dbCommand = db.GetStoredProcCommand("USP_ITEM_U");

					db.AddInParameter(dbCommand, "p_item_id", DbType.Int32, item.ItemId);
					db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, item.BookId);
					db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, item.ChapterId);
					if (item.CategoryId > 0)
					{
						db.AddInParameter(dbCommand, "p_category_id", DbType.String, item.CategoryId);
					}
					else
					{
						db.AddInParameter(dbCommand, "p_category_id", DbType.String, null);
					}
					db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, item.OrganizationId);
					db.AddInParameter(dbCommand, "p_type_id", DbType.Int32, item.TypeId);
					db.AddInParameter(dbCommand, "p_complete_time", DbType.Int32, item.CompleteTime);
					db.AddInParameter(dbCommand, "p_difficulty_id", DbType.Int32, item.DifficultyId);
					db.AddInParameter(dbCommand, "p_source", DbType.String, item.Source);
					db.AddInParameter(dbCommand, "p_version", DbType.String, item.Version);
					db.AddInParameter(dbCommand, "p_score", DbType.Decimal, item.Score);
					db.AddInParameter(dbCommand, "p_content", DbType.String, item.Content);
					db.AddInParameter(dbCommand, "p_answer_count", DbType.Int32, item.AnswerCount);
					db.AddInParameter(dbCommand, "p_select_answer", DbType.String, item.SelectAnswer);
					db.AddInParameter(dbCommand, "p_standard_answer", DbType.String, item.StandardAnswer);
					db.AddInParameter(dbCommand, "p_description", DbType.String, item.Description);
					db.AddInParameter(dbCommand, "p_outdate_date", DbType.DateTime, item.OutDateDate);
					db.AddInParameter(dbCommand, "p_used_count", DbType.Int32, item.UsedCount);
					db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, item.StatusId);
					db.AddInParameter(dbCommand, "p_create_person", DbType.String, item.CreatePerson);
					db.AddInParameter(dbCommand, "p_create_time", DbType.DateTime, item.CreateTime);
					db.AddInParameter(dbCommand, "p_usage_id", DbType.Int32, item.UsageId);
					db.AddInParameter(dbCommand, "p_memo", DbType.String, item.Memo);
					db.AddInParameter(dbCommand, "p_key_word", DbType.String, item.KeyWord);
					db.AddInParameter(dbCommand, "p_has_picture", DbType.Int32, item.HasPicture);

					db.ExecuteNonQuery(dbCommand, transaction);
				}
				transaction.Commit();
			}
			catch (Exception ex)
			{
				transaction.Rollback();
				throw ex;
			}
			connection.Close();
		}

        /// <summary>
        /// 按试题ID取试题
        /// </summary>
        /// <param name="itemId">试题ID</param>
        /// <returns>试题</returns>
        public Item GetItem(int itemId)
        {
            Item item = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_item_id", DbType.Int32, itemId);
            db.AddOutParameter(dbCommand, "p_chapter_path", DbType.String, 200);
            db.AddOutParameter(dbCommand, "p_category_path", DbType.String, 200);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    item = CreateModelObject(dataReader);
                    item.HasPicture = Convert.ToInt32(dataReader[GetMappingFieldName("HasPicture")]);
					item.KeyWord = dataReader[GetMappingFieldName("KeyWord")].ToString();
                    break;
                }
            }

            string[] chapters = db.GetParameterValue(dbCommand, "p_chapter_path").ToString().Split('\\');
            Array.Reverse(chapters);
            foreach (string s in chapters)
            {
                item.ChapterPath += s + "\\";
            }
            item.ChapterPath = item.ChapterPath.Substring(0, item.ChapterPath.Length - 1);

            string[] categories = db.GetParameterValue(dbCommand, "p_category_path").ToString().Split('\\');
            Array.Reverse(categories);            
            foreach (string s in categories)
            {
                item.CategoryPath += s + "\\";
            }
            item.CategoryPath = item.CategoryPath.Substring(0, item.CategoryPath.Length - 1);

            return item;
        }

        /// <summary>
        /// 查询所有试题
        /// </summary>
        /// <returns>所有试题</returns>
        public IList<Item> GetItems()
        {
            IList<Item> items = new List<Item>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    items.Add(CreateModelObject(dataReader));
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return items;
        }

        public IList<Item> GetItemsNoPicture()
        {
            IList<Item> items = new List<Item>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_G_No_Pic";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    items.Add(CreateModelObject(dataReader));
                }
            }

            return items;
        }


        public IList<Item> GetEnableItems(int currentPageIndex, int pageSize, string orderBy,int orgID,int railSystemid)
        {
            IList<Item> items = new List<Item>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_Enable__S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_current_page_index", DbType.Int32, currentPageIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, pageSize);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
            db.AddInParameter(dbCommand, "p_rail_system_id", DbType.Int32, railSystemid);
			db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    items.Add(CreateModelObject(dataReader));
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return items;
        }

        public IList<Item> GetEnableOutputItems(int orgID, int railSystemId)
        {
            IList<Item> items = new List<Item>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_OutPut_Enable_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
            db.AddInParameter(dbCommand, "p_rail_system_id", DbType.Int32, railSystemId);
          
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    items.Add(CreateModelObject(dataReader));
                }
            }

            return items;
        }


              public IList<Item> GetOutputItems(string knowledgeIdPath, int bookId, int chapterId, string trainTypeIdPath, string categoryIdPath, 
            int itemTypeId, int itemDifficultyId,int itemScore,
            int StatusId, int usageId,int orgID,int railSystemId)
        {
            IList<Item> items = new List<Item>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_OutPut_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            
            db.AddInParameter(dbCommand, "p_knowledge_id_path", DbType.String, knowledgeIdPath);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookId);
            db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, chapterId);
          
            db.AddInParameter(dbCommand, "p_train_type_id_path", DbType.String, trainTypeIdPath);
            db.AddInParameter(dbCommand, "p_category_id_path", DbType.String, categoryIdPath);        
             
            db.AddInParameter(dbCommand, "p_item_type_id", DbType.Int32, itemTypeId);
            db.AddInParameter(dbCommand, "p_item_difficulty_id", DbType.Int32, itemDifficultyId);
            db.AddInParameter(dbCommand, "p_Score", DbType.Int32, itemScore);
            db.AddInParameter(dbCommand, "p_Status_id", DbType.Int32, StatusId);
            db.AddInParameter(dbCommand, "p_usage_id", DbType.Int32, usageId);  
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
            db.AddInParameter(dbCommand, "p_rail_system_id", DbType.Int32, railSystemId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Item item = CreateModelObject(dataReader);
                    item.Authors = Convert.ToInt32(dataReader[GetMappingFieldName("Authors")].ToString());
                    items.Add(item);
                }
            }         

            return items;
        }


        /// <summary>
        /// 按图书章节查询试题
        /// </summary>
        /// <returns>指定图书章节的所有试题</returns>
        public IList<Item> GetItems(string content,string knowledgeIdPath, int bookId, int chapterId, string trainTypeIdPath, string categoryIdPath, 
              int itemTypeId, int itemDifficultyId,int itemScore,
            int StatusId, int usageId,int currentPageIndex, int pageSize, string orderBy,int orgID,int railSystemid)
        {
            IList<Item> items = new List<Item>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_BCTTC_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_content", DbType.String, content);
			db.AddInParameter(dbCommand, "p_knowledge_id_path", DbType.String, knowledgeIdPath);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookId);
            db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, chapterId);
          
            db.AddInParameter(dbCommand, "p_train_type_id_path", DbType.String, trainTypeIdPath);
            db.AddInParameter(dbCommand, "p_category_id_path", DbType.String, categoryIdPath);        
             
            db.AddInParameter(dbCommand, "p_item_type_id", DbType.Int32, itemTypeId);
            db.AddInParameter(dbCommand, "p_item_difficulty_id", DbType.Int32, itemDifficultyId);
            db.AddInParameter(dbCommand, "p_Score", DbType.Int32, itemScore);
            db.AddInParameter(dbCommand, "p_Status_id", DbType.Int32, StatusId);
            db.AddInParameter(dbCommand, "p_usage_id", DbType.Int32, usageId);
             
            db.AddInParameter(dbCommand, "p_current_page_index", DbType.Int32, currentPageIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, pageSize);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
            db.AddInParameter(dbCommand, "p_rail_system_id", DbType.Int32, railSystemid);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Item item = CreateModelObject(dataReader);
                    item.Authors = Convert.ToInt32(dataReader[GetMappingFieldName("Authors")].ToString());
                    items.Add(item);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return items;
        }

        public IList<Item> GetItems(string knowledgeIdPath, int bookId, int chapterId, string trainTypeIdPath, string categoryIdPath,
   int itemTypeId, int paperId, int itemDifficultyId, int itemScore,
   int StatusId, int usageId, int startRow, int endRow, ref int nItemcount)
        {
            IList<Item> items = new List<Item>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_Select_S1";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_knowledge_id_path", DbType.String, knowledgeIdPath);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookId);
            db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, chapterId);

            db.AddInParameter(dbCommand, "p_train_type_id_path", DbType.String, trainTypeIdPath);
            db.AddInParameter(dbCommand, "p_category_id_path", DbType.String, categoryIdPath);

            db.AddInParameter(dbCommand, "p_item_type_id", DbType.Int32, itemTypeId);
            db.AddInParameter(dbCommand, "p_paper_id", DbType.Int32, paperId);
            db.AddInParameter(dbCommand, "p_item_difficulty_id", DbType.Int32, itemDifficultyId);
            db.AddInParameter(dbCommand, "p_Score", DbType.Int32, itemScore);
            db.AddInParameter(dbCommand, "p_Status_id", DbType.Int32, StatusId);
            db.AddInParameter(dbCommand, "p_usage_id", DbType.Int32, usageId);

            db.AddInParameter(dbCommand, "p_start_row", DbType.Int32, startRow);
            db.AddInParameter(dbCommand, "p_end_row", DbType.Int32, endRow);             
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    items.Add(CreateModelObject(dataReader));
                }
            }

            nItemcount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return items;         

        }



        public IList<Item> GetItems(string knowledgeIdPath, int bookId, int chapterId, string trainTypeIdPath, string categoryIdPath,
     int itemTypeId, int paperId, int itemDifficultyId, int itemScore,
     int StatusId, int usageId, int currentPageIndex, int pageSize, string orderBy)
        {
            IList<Item> items = new List<Item>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_Select_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_knowledge_id_path", DbType.String, knowledgeIdPath);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookId);
            db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, chapterId);

            db.AddInParameter(dbCommand, "p_train_type_id_path", DbType.String, trainTypeIdPath);
            db.AddInParameter(dbCommand, "p_category_id_path", DbType.String, categoryIdPath);

            db.AddInParameter(dbCommand, "p_item_type_id", DbType.Int32, itemTypeId);
            db.AddInParameter(dbCommand, "p_paper_id", DbType.Int32, paperId);
            db.AddInParameter(dbCommand, "p_item_difficulty_id", DbType.Int32, itemDifficultyId);
            db.AddInParameter(dbCommand, "p_Score", DbType.Int32, itemScore);
            db.AddInParameter(dbCommand, "p_Status_id", DbType.Int32, StatusId);
            db.AddInParameter(dbCommand, "p_usage_id", DbType.Int32, usageId);

            db.AddInParameter(dbCommand, "p_current_page_index", DbType.Int32, currentPageIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, pageSize);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    items.Add(CreateModelObject(dataReader));
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return items;         
        }


        /// <summary>
        /// 按图书查询试题
        /// </summary>
        /// <returns>指定图书的所有试题</returns>
        public IList<Item> GetItemsByBookBookId(int bookId)
        {
            IList<Item> items = new List<Item>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_BOOK_CHAPTER_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookId);
			db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, 0);
			db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, int.MaxValue);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    items.Add(CreateModelObject(dataReader));
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return items;
        }

        public int GetItemsByBookID(int bookId,int itemTypeID)
        {
            IList<Item> items = new List<Item>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_BOOK";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookId);
			db.AddInParameter(dbCommand, "p_item_type_id", DbType.Int32, itemTypeID);
			db.AddInParameter(dbCommand, "p_diff", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_max_diff", DbType.Int32, 0);

            db.ExecuteNonQuery(dbCommand);

            return Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));
        }

		public int GetItemsByBookID(int bookId, int itemTypeID,int diff,int maxdiff)
		{
			IList<Item> items = new List<Item>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_ITEM_BOOK";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);
			db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookId);
			db.AddInParameter(dbCommand, "p_item_type_id", DbType.Int32, itemTypeID);
			db.AddInParameter(dbCommand, "p_diff", DbType.Int32, diff);
            db.AddInParameter(dbCommand, "p_max_diff", DbType.Int32, maxdiff);

			db.ExecuteNonQuery(dbCommand);

			return Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));
		}

        /// <summary>
        /// 按图书章节查询试题
        /// </summary>
        /// <returns>指定图书章节的所有试题</returns>
        public IList<Item> GetItemsByBookChapterId(int bookId, int chapterId,int currentPageIndex,int pageSize)
        {
            IList<Item> items = new List<Item>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_G_CHAPTERPATH";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookId);
            db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, chapterId);
            db.AddOutParameter(dbCommand, "p_id_path", DbType.String, 50);
            db.AddInParameter(dbCommand, "p_current_page_index", DbType.Int32, currentPageIndex);
            if(pageSize != 0)
            {
                db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, pageSize);
            }
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    items.Add(CreateModelObject(dataReader));
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return items;
        }

		/// <summary>
		/// 按图书章节查询试题
		/// </summary>
		/// <returns>指定图书章节的所有试题</returns>
		public IList<Item> GetItemsByChapterId(int bookid,int chapterId)
		{
			IList<Item> items = new List<Item>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_ITEM_G_ChapterID";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookid);
			db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, chapterId);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					items.Add(CreateModelObject(dataReader));
				}
			}

			return items;
		}

        /// <summary>
        /// 按图书章节查询练习试题
        /// </summary>
        /// <returns>指定图书章节的单选练习试题</returns>
        public IList<Item> GetExerciseItems(int bookId, int chapterId, int typeId)
        {
            IList<Item> items = new List<Item>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_Exercise_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookId);
            db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, chapterId);
            db.AddInParameter(dbCommand, "p_TYPE_ID", DbType.Int32, typeId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    items.Add(CreateModelObject(dataReader));
                }
            }    

            return items;
        }

        /// <summary>
        /// 按组卷策略查询试题
        /// </summary>
        /// <returns>组卷策略随机试题</returns>
        public IList<Item> GetItemsByStrategy(int RangeType, int DiffId, int RangeId, int ItemTypeId, string strExcludeIDs,int strategyid)
        {
            IList<Item> items = new List<Item>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_Strategy_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_TYPE_ID", DbType.Int32, ItemTypeId);
            db.AddInParameter(dbCommand, "p_DIFFICULTY_ID", DbType.Int32, DiffId);
            db.AddInParameter(dbCommand, "p_range_id", DbType.Int32, RangeId);
            db.AddInParameter(dbCommand, "p_range_type", DbType.Int32, RangeType);
            db.AddInParameter(dbCommand, "p_Exclude_chapter_id", DbType.String, strExcludeIDs);
            db.AddInParameter(dbCommand, "p_strategy_id", DbType.Int32, strategyid);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                	Item obj = CreateModelObject(dataReader);
                	obj.KeyWord = dataReader[GetMappingFieldName("KeyWord")].ToString();
                    items.Add(obj);
                }
            }

            return items;
        }

		/// <summary>
		/// 按组卷策略查询试题
		/// </summary>
		/// <returns>组卷策略随机试题</returns>
		public IList<Item> GetItemsByStrategyNew(int RangeType, int DiffId, int MaxDiffId,int RangeId, int ItemTypeId, string strExcludeIDs)
		{
			IList<Item> items = new List<Item>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_ITEM_Strategy_S_New";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_TYPE_ID", DbType.Int32, ItemTypeId);
			db.AddInParameter(dbCommand, "p_DIFFICULTY_ID", DbType.Int32, DiffId);
            db.AddInParameter(dbCommand, "p_Max_DIFFICULTY_ID", DbType.Int32, MaxDiffId);
			db.AddInParameter(dbCommand, "p_range_id", DbType.Int32, RangeId);
			db.AddInParameter(dbCommand, "p_range_type", DbType.Int32, RangeType);
			db.AddInParameter(dbCommand, "p_Exclude_chapter_id", DbType.String, strExcludeIDs);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					Item obj = CreateModelObject(dataReader);
					obj.KeyWord = dataReader[GetMappingFieldName("KeyWord")].ToString();
					items.Add(obj);
				}
			}

			return items;
		}

        public IList<Item> GetItemsByStrategy(int RangeType, int RangeId)
        {
            IList<Item> items = new List<Item>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_Strategy_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);     
            db.AddInParameter(dbCommand, "p_range_id", DbType.Int32, RangeId);
            db.AddInParameter(dbCommand, "p_range_type", DbType.Int32, RangeType); 
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    items.Add(CreateModelObject(dataReader));
                }
            }

            return items;
        }

        public IList<Item> GetItemsByStrategyItem(int DiffId, int categoryId, int ItemTypeId, string strExcludeIDs)
        {
            IList<Item> items = new List<Item>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_Strategy_Item_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_TYPE_ID", DbType.Int32, ItemTypeId);
            db.AddInParameter(dbCommand, "p_DIFFICULTY_ID", DbType.Int32, DiffId);
            db.AddInParameter(dbCommand, "p_category_id", DbType.Int32, categoryId);
            db.AddInParameter(dbCommand, "p_Exclude_categorys_id", DbType.String, strExcludeIDs);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    items.Add(CreateModelObject(dataReader));
                }
            }

            return items;
        }

        /// <summary>
        /// 按图书章节查询试题
        /// </summary>
        /// <returns>指定图书章节的所有试题</returns>
        public IList<Item> GetItemsByBookChapterId(int bookId, int chapterId, int categoryId)
        {
            IList<Item> items = new List<Item>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_BOOK_CHAPTER_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookId);
            db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, chapterId);
            db.AddInParameter(dbCommand, "p_category_id", DbType.Int32, categoryId);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, 500);
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    items.Add(CreateModelObject(dataReader));
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return items;
        }

        /// <summary>
        /// 按图书章节查询试题
        /// </summary>
        /// <returns>指定图书章节的所有试题</returns>
		public int GetItemsByBookChapterIdPath(string idPath, int itemTypeID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_BOOK_CHAPTER";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_id_path", DbType.String, idPath);
			db.AddInParameter(dbCommand, "p_item_type_id", DbType.String, itemTypeID);
			db.AddInParameter(dbCommand, "p_diff", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_max_diff", DbType.Int32, 0);
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            db.ExecuteNonQuery(dbCommand);

            return Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));
        }

		public int GetItemsByBookChapterIdPath(string idPath, int itemTypeID, int diff,int maxdiff)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_ITEM_BOOK_CHAPTER";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_id_path", DbType.String, idPath);
			db.AddInParameter(dbCommand, "p_item_type_id", DbType.String, itemTypeID);
			db.AddInParameter(dbCommand, "p_diff", DbType.Int32, diff);
            db.AddInParameter(dbCommand, "p_max_diff", DbType.Int32, maxdiff);
			db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

			db.ExecuteNonQuery(dbCommand);

			return Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));
		}

        /// <summary>
        /// 查询符合条件的试题
        /// </summary>
        public IList<Item> GetItems(int itemId, int bookId, int chapterId, int categoryId, int organizationId,
            int typeId, int completeTime, int difficultyId, string source, string version,
            decimal score, string content, int answerCount, string selectAnswer,
            string standardAnswer, string description, DateTime outDateDate, int usedCount,
            int statusId, string createPerson, DateTime createTime, string memo,
            int startRowIndex, int maximumRows, string orderBy)
        {
            IList<Item> items = new List<Item>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            //db.AddInParameter(dbCommand, "p_item_id", DbType.Int32, itemId);
            //db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookId);
            //db.AddInParameter(dbCommand, "p_chapter_id", DbType.String, chapterId);
            //db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, item.ChapterId);
            //db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, organizationId);
            //db.AddInParameter(dbCommand, "p_type_id", DbType.Int32, typeId);
            //db.AddInParameter(dbCommand, "p_complete_time", DbType.Int32, completeTime);
            //db.AddInParameter(dbCommand, "p_difficulty_id", DbType.Int32, difficultyId);
            //db.AddInParameter(dbCommand, "p_source", DbType.String, source);
            //db.AddInParameter(dbCommand, "p_version", DbType.String, version);
            //db.AddInParameter(dbCommand, "p_score", DbType.Decimal, score);
            //db.AddInParameter(dbCommand, "p_content", DbType.String, content);
            //db.AddInParameter(dbCommand, "p_answer_count", DbType.Int32, answerCount);
            //db.AddInParameter(dbCommand, "p_select_answer", DbType.String, selectAnswer);
            //db.AddInParameter(dbCommand, "p_standard_answer", DbType.String, standardAnswer);
            //db.AddInParameter(dbCommand, "p_description", DbType.String, description);
            //if (item.OutDateDate != DateTime.MaxValue)
            //{
            //    db.AddInParameter(dbCommand, "p_outdate_date", DbType.DateTime, outDateDate);
            //}
            //db.AddInParameter(dbCommand, "p_used_count", DbType.Int32, usedCount);
            //db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, statusId);
            //db.AddInParameter(dbCommand, "p_create_person", DbType.String, createPerson);
            //db.AddInParameter(dbCommand, "p_create_time", DbType.DateTime, createTime);
            //db.AddInParameter(dbCommand, "p_memo", DbType.String, memo);
            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    items.Add(CreateModelObject(dataReader));
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return items;
        }

        /// <summary>
        /// 查询结果记录数
        /// </summary>
        public int RecordCount
        {
            get { return _recordCount; }
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
                orderByCondition = System.Text.RegularExpressions.Regex.Replace(s.ToLower(), "\\s+asc$", ",asc");
                orderByCondition = System.Text.RegularExpressions.Regex.Replace(orderByCondition, "\\s+desc$", ",desc");
                orderByCondition = orderByCondition.Trim();

                string[] orderBysOfOneCondition = orderByCondition.Split(new char[] { ',' });

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
                        mappingOrderBy += GetMappingFieldName(orderBysOfOneCondition[0]) + ' ' +
                                          orderBysOfOneCondition[1];
                    }
                }
            }

            return mappingOrderBy;
        }

        public static Item CreateModelObject(IDataReader dataReader)
        {
            return new Item(
                DataConvert.ToInt(dataReader[GetMappingFieldName("ItemId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("BookId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("BookName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ChapterId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ChapterName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("CategoryId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("CategoryName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrganizationId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("OrganizationName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("TypeId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("TypeName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("CompleteTime")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("DifficultyId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("DifficultyName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Source")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Version")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("Score")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Content")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("AnswerCount")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("SelectAnswer")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("StandardAnswer")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("OutDateDate")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("UsedCount")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("StatusId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("StatusName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("CreatePerson")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("CreateTime")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("UsageId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }
    }
}
