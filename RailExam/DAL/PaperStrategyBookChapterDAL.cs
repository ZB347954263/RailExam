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
    public class PaperStrategyBookChapterDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static PaperStrategyBookChapterDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("rangetype", "range_type");
            _ormTable.Add("paperstrategybookchapterid", "Paper_Strategy_Book_Chapter_Id");
            _ormTable.Add("strategysubjectid", "Strategy_Subject_Id");
            _ormTable.Add("rangeid", "Range_Id");
            _ormTable.Add("itemtypeid", "Item_Type_Id");
            _ormTable.Add("itemtypename", "Type_Name");
            _ormTable.Add("unitscore", "Unit_Score");
            _ormTable.Add("unitlimittime", "Unit_Limit_Time");
            _ormTable.Add("rangename", "range_name");
            _ormTable.Add("memo", "memo");
            _ormTable.Add("excludechapterid", "Exclude_Chapters_Id");
            _ormTable.Add("itemdifficultyrandomcount", "Item_Difficulty_Random_Count");
            _ormTable.Add("itemdifficulty1count", "Item_Difficulty_1_Count");
            _ormTable.Add("itemdifficulty2count", "Item_Difficulty_2_Count");
            _ormTable.Add("itemdifficulty3count", "Item_Difficulty_3_Count");
            _ormTable.Add("itemdifficulty4count", "Item_Difficulty_4_Count");
            _ormTable.Add("itemdifficulty5count", "Item_Difficulty_5_Count");
            _ormTable.Add("subjectname", "Subject_Name");
        
        }

        public int AddPaperStrategyBookChapter(PaperStrategyBookChapter item)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            int nRecordAffected = 0;
            try
            {
                string sqlCommand = "USP_STRATEGY_BOOK_CHAPTER_I";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddOutParameter(dbCommand, "p_strategy_book_chapter_id", DbType.Int32, 4);
                db.AddInParameter(dbCommand, "p_strategy_subject_id", DbType.Int32, item.StrategySubjectId);
                db.AddInParameter(dbCommand, "p_Range_type", DbType.Int32, item.RangeType);
                db.AddInParameter(dbCommand, "p_Range_id", DbType.Int32, item.RangeId);
                db.AddInParameter(dbCommand, "p_item_type_id", DbType.Int32, item.ItemTypeId);

                db.AddInParameter(dbCommand, "p_item_difficulty_random_count", DbType.Int32, item.ItemDifficultyRandomCount);
                db.AddInParameter(dbCommand, "p_item_difficulty_1_count", DbType.Int32, item.ItemDifficulty1Count);
                db.AddInParameter(dbCommand, "p_item_difficulty_2_count", DbType.Int32, item.ItemDifficulty2Count);
                db.AddInParameter(dbCommand, "p_range_name", DbType.String, item.RangeName);
                db.AddInParameter(dbCommand, "p_item_difficulty_3_count", DbType.Int32, item.ItemDifficulty3Count);
                db.AddInParameter(dbCommand, "p_item_difficulty_4_count", DbType.Int32, item.ItemDifficulty4Count);
                db.AddInParameter(dbCommand, "p_item_difficulty_5_count", DbType.Int32, item.ItemDifficulty5Count);
                db.AddInParameter(dbCommand, "p_unit_score", DbType.Decimal, item.UnitScore);
                db.AddInParameter(dbCommand, "p_exclude_chapters_id", DbType.String, item.ExcludeChapterId);
                db.AddInParameter(dbCommand, "p_unit_limit_time", DbType.String, item.UnitLimitTime);
                db.AddInParameter(dbCommand, "p_memo", DbType.String, item.Memo);

                nRecordAffected = db.ExecuteNonQuery(dbCommand, transaction);

                string sqlCommand1 = "USP_strategy_Subject_U_score";
                DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand1);
                db.AddInParameter(dbCommand1, "p_STRATEGY_SUBJECT_ID", DbType.Int32, item.StrategySubjectId);
                db.ExecuteNonQuery(dbCommand1, transaction);

                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }

            connection.Close();

            return nRecordAffected;
        }

        public int UpdatePaperStrategyBookChapter(PaperStrategyBookChapter item)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            int nRecordAffected = 0;
            try
            {
                string sqlCommand = "USP_STRATEGY_BOOK_CHAPTER_U";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "p_strategy_book_chapter_id", DbType.Int32, item.PaperStrategyBookChapterId);
                db.AddInParameter(dbCommand, "p_strategy_subject_id", DbType.Int32, item.StrategySubjectId);
                db.AddInParameter(dbCommand, "p_Range_Type", DbType.Int32, item.RangeType);
                db.AddInParameter(dbCommand, "p_Range_id", DbType.Int32, item.RangeId);
                db.AddInParameter(dbCommand, "p_item_type_id", DbType.Int32, item.ItemTypeId);
                db.AddInParameter(dbCommand, "p_range_name", DbType.String, item.RangeName);
                db.AddInParameter(dbCommand, "p_item_difficulty_random_count", DbType.Int32, item.ItemDifficultyRandomCount);
                db.AddInParameter(dbCommand, "p_item_difficulty_1_count", DbType.Int32, item.ItemDifficulty1Count);
                db.AddInParameter(dbCommand, "p_item_difficulty_2_count", DbType.String, item.ItemDifficulty2Count);
                db.AddInParameter(dbCommand, "p_item_difficulty_3_count", DbType.Int32, item.ItemDifficulty3Count);
                db.AddInParameter(dbCommand, "p_item_difficulty_4_count", DbType.Int32, item.ItemDifficulty4Count);
                db.AddInParameter(dbCommand, "p_item_difficulty_5_count", DbType.Int32, item.ItemDifficulty5Count);
                db.AddInParameter(dbCommand, "p_unit_score", DbType.Decimal, item.UnitScore);
                db.AddInParameter(dbCommand, "p_exclude_chapters_id", DbType.String, item.ExcludeChapterId);
                db.AddInParameter(dbCommand, "p_unit_limit_time", DbType.String, item.UnitLimitTime);
                db.AddInParameter(dbCommand, "p_memo", DbType.String, item.Memo);

                nRecordAffected = db.ExecuteNonQuery(dbCommand, transaction);

                string sqlCommand1 = "USP_strategy_Subject_U_score";
                DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand1);
                db.AddInParameter(dbCommand1, "p_STRATEGY_SUBJECT_ID", DbType.Int32, item.StrategySubjectId);
                db.ExecuteNonQuery(dbCommand1, transaction);

                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
           
            connection.Close();           

            return nRecordAffected;
        }

        public int DeletePaperStrategyBookChapter(int PaperStrategyBookChapterId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();
            int i = 0;
            try
            {
                string sqlCommand = "USP_STRATEGY_BOOK_CHAPTER_D";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "p_strategy_book_chapter_id", DbType.Int32, PaperStrategyBookChapterId);
                
                i =  db.ExecuteNonQuery(dbCommand, transaction);             

                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }

            connection.Close();
            return i;
        }

        public int GetBookChapterCount(int PaperStrategyID)
        {
             
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_strategy_Book_Count_s";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_paper_strategy_id", DbType.Int32, PaperStrategyID);
            db.AddOutParameter(dbCommand, "p_item_count", DbType.Int32, 4);
            db.ExecuteNonQuery(dbCommand);

            int n = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_item_count"));

            return n ;
        }

        public PaperStrategyBookChapter GetPaperStrategyBookChapter(int paperItemId)
        {
            PaperStrategyBookChapter item = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_STRATEGY_BOOK_CHAPTER_g";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_strategy_book_chapter_id", DbType.Int32, paperItemId);
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    item = CreateModelObject(dataReader);
                    break;
                }
            }

            return item;
        }

        public IList<PaperStrategyBookChapter> GetItemsByPaperStrategySubjectID(int paperStrategySubjectID)
        {
            IList<PaperStrategyBookChapter> items = new List<PaperStrategyBookChapter>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_STRATEGY_BOOK_CHAPTER_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_strategy_subject_id", DbType.Int32, paperStrategySubjectID);

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

        public static PaperStrategyBookChapter CreateModelObject(IDataReader dataReader)
        {

            return new PaperStrategyBookChapter(
                DataConvert.ToInt(dataReader[GetMappingFieldName("RangeType")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("PaperStrategyBookChapterId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("StrategySubjectId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("RangeId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ItemTypeId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ItemTypeName")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("UnitScore")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("UnitLimitTime")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("RangeName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("memo")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ExcludeChapterId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ItemDifficultyRandomCount")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ItemDifficulty1Count")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ItemDifficulty2Count")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ItemDifficulty3Count")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ItemDifficulty4Count")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ItemDifficulty5Count")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("SubjectName")]));
        }
    }
}
