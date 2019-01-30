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
    public class RandomExamItemDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static RandomExamItemDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("randomexamitemid", "random_exam_Item_Id");
            _ormTable.Add("subjectid", "Subject_Id");
            _ormTable.Add("strategyid", "Strategy_Id");
            _ormTable.Add("randomexamid", "Random_Exam_Id");
            _ormTable.Add("itemid", "ITEM_ID");
            _ormTable.Add("bookid", "BOOK_ID");
            _ormTable.Add("chapterid", "CHAPTER_ID");
            _ormTable.Add("categoryid", "CATEGORY_ID");
            _ormTable.Add("organizationid", "ORG_ID");
            _ormTable.Add("typeid", "TYPE_ID");
            _ormTable.Add("typename", "type_name");
            _ormTable.Add("completetime", "COMPLETE_TIME");
            _ormTable.Add("difficultyid", "DIFFICULTY_ID");
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
            _ormTable.Add("createperson", "CREATE_PERSON");
            _ormTable.Add("createtime", "CREATE_TIME");
            _ormTable.Add("memo", "MEMO");
            _ormTable.Add("mothercode", "MOTHER_CODE");
            _ormTable.Add("answer", "ANSWER");
        }

        public void AddItem(IList<RandomExamItem> Items, int year)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();
            int itemid;
            try
            {
                foreach (RandomExamItem item in Items)
                {
                    try
                    {
                        string sqlCommand = "USP_random_exam_ITEM_I";

                        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                        itemid = item.ItemId;
                        db.AddOutParameter(dbCommand, "p_random_exam_Item_Id", DbType.Int32, 4);
                        db.AddInParameter(dbCommand, "p_Subject_Id", DbType.Int32, item.SubjectId);
                        db.AddInParameter(dbCommand, "p_Strategy_Id", DbType.Int32, item.StrategyId);
                        db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, item.RandomExamId);
                        db.AddInParameter(dbCommand, "p_item_id", DbType.Int32, item.ItemId);
                        db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, item.BookId);
                        db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, item.ChapterId);
                        db.AddInParameter(dbCommand, "p_category_id", DbType.String, item.CategoryId);
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
                        db.AddInParameter(dbCommand, "p_memo", DbType.String, item.Memo);
                        db.AddInParameter(dbCommand, "p_year", DbType.Int32, year);
                        db.AddInParameter(dbCommand, "p_parent_item_id", DbType.Int32, item.ParentItemID);
                        db.AddInParameter(dbCommand, "p_mother_code", DbType.String, item.MotherCode);
                        db.AddInParameter(dbCommand, "p_item_index", DbType.Int32, item.ItemIndex);

                        db.ExecuteNonQuery(dbCommand, transaction);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }

                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            connection.Close();
        }

        public void DeleteItems(int ExamId,int year)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_random_exam_ITEM_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_Exam_Id", DbType.Int32, ExamId);
			db.AddInParameter(dbCommand, "p_year", DbType.Int32, year);
            db.ExecuteNonQuery(dbCommand);
        }

        public IList<RandomExamItem> GetItemsByStrategyId(int StrategyId,int year)
        {
            IList<RandomExamItem> items = new List<RandomExamItem>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_random_exam_ITEM_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Strategy_Id", DbType.Int32, StrategyId);
			db.AddInParameter(dbCommand, "p_year", DbType.Int32, year);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    items.Add(CreateModelObject(dataReader));
                }
            }

            return items;
        }


        public IList<RandomExamItem> GetItemsBySubjectId(int SubjectId,int year)
        {
            IList<RandomExamItem> items = new List<RandomExamItem>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_random_exam_ITEM_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Subject_id", DbType.Int32, SubjectId);
			db.AddInParameter(dbCommand, "p_year", DbType.Int32, year);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    items.Add(CreateModelObject(dataReader));
                }
            }

            return items;
        }


        public RandomExamItem GetRandomExamItem(int randomExaID,int year)
        {
            RandomExamItem exam = null;

            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "USP_Random_Exam_item_f";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, randomExaID);
			db.AddInParameter(dbCommand, "p_year", DbType.Int32, year);	

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    exam = CreateModelObject(dataReader);
                }
            }

            return exam;
        }

        public IList<RandomExamItem> GetItemsByParentItemID(int parentItemID, int randomExamID, int year)
        {
            IList<RandomExamItem> items = new List<RandomExamItem>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_random_exam_ITEM_Parent";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_parent_item_id", DbType.Int32, parentItemID);
            db.AddInParameter(dbCommand, "p_exam_id", DbType.Int32, randomExamID);
            db.AddInParameter(dbCommand, "p_year", DbType.Int32, year);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    items.Add(CreateModelObject(dataReader));
                }
            }

            return items;
        }



        public IList<RandomExamItem> GetItems(int SubjectId, int randomExamResultId,int year)
        {
            IList<RandomExamItem> items = new List<RandomExamItem>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_random_exam_ITEM_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Subject_id", DbType.Int32, SubjectId);
            db.AddInParameter(dbCommand, "p_random_exam_result_id", DbType.Int32, randomExamResultId);
			db.AddInParameter(dbCommand, "p_year", DbType.Int32, year);


            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    RandomExamItem item = CreateModelObject(dataReader);
                    item.Answer = dataReader[GetMappingFieldName("Answer")].ToString();
                    items.Add(item);
                }
            }

            return items;
        }

        public IList<RandomExamItem> GetItemsCurrent(int SubjectId, int randomExamResultId,int year)
        {
            IList<RandomExamItem> items = new List<RandomExamItem>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_random_exam_ITEM_G_Cur";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Subject_id", DbType.Int32, SubjectId);
            db.AddInParameter(dbCommand, "p_random_exam_result_id", DbType.Int32, randomExamResultId);
			db.AddInParameter(dbCommand, "p_year", DbType.Int32, year);


            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    RandomExamItem item = CreateModelObject(dataReader);
                    item.Answer = dataReader[GetMappingFieldName("Answer")].ToString();
                    items.Add(item);
                }
            }

            return items;
        }

        public IList<RandomExamItem> GetItemsCurrentCheck()
        {
            IList<RandomExamItem> items = new List<RandomExamItem>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_random_exam_ITEM_G_CHK";
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

        public IList<RandomExamItem> GetItemsByOrgID(int SubjectId, int randomExamResultId,int orgID,int year)
        {
            IList<RandomExamItem> items = new List<RandomExamItem>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_random_exam_ITEM_G_ORG";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Subject_id", DbType.Int32, SubjectId);
            db.AddInParameter(dbCommand, "p_random_exam_result_id", DbType.Int32, randomExamResultId);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
			db.AddInParameter(dbCommand, "p_year", DbType.Int32, year);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    items.Add(CreateModelObject(dataReader));
                }
            }

            return items;
        }


		public IList<RandomExamItem> GetItemsStation(int SubjectId, int randomExamResultId, int year)
		{
			IList<RandomExamItem> items = new List<RandomExamItem>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_random_exam_ITEM_G_STA";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_Subject_id", DbType.Int32, SubjectId);
			db.AddInParameter(dbCommand, "p_random_exam_result_id", DbType.Int32, randomExamResultId);
			db.AddInParameter(dbCommand, "p_year", DbType.Int32, year);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
				    RandomExamItem item = CreateModelObject(dataReader);
                    item.Answer = dataReader[GetMappingFieldName("Answer")].ToString();
                    items.Add(item);
				}
			}

			return items;
		}



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
                        mappingOrderBy += GetMappingFieldName(orderBysOfOneCondition[0]) + ' ' +
                                          orderBysOfOneCondition[1];
                    }
                }
            }

            return mappingOrderBy;
        }

        public static RandomExamItem CreateModelObject(IDataReader dataReader)
        {
            return new RandomExamItem(
                DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamItemId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("SubjectId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("StrategyId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ItemId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("BookId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ChapterId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("CategoryId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrganizationId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("TypeId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("typeName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("CompleteTime")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("DifficultyId")]),
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
                DataConvert.ToString(dataReader[GetMappingFieldName("CreatePerson")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("CreateTime")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }

    }
}
