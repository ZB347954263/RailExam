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
    public class PaperItemDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static PaperItemDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("paperitemid", "paper_Item_Id");
            _ormTable.Add("paperid", "paper_Id");
            _ormTable.Add("papersubjectid", "paper_Subject_Id");
            _ormTable.Add("orderindex", "order_Index");
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
        }

        public int AddPaperItem(PaperItem paperItem)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_paper_ITEM_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_paper_Item_Id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_paper_Id", DbType.Int32, paperItem.PaperId);
            db.AddInParameter(dbCommand, "p_paper_Subject_Id", DbType.Int32, paperItem.PaperSubjectId);
            db.AddInParameter(dbCommand, "p_order_Index", DbType.Int32, paperItem.OrderIndex);
            db.AddInParameter(dbCommand, "p_item_id", DbType.Int32, paperItem.ItemId);
            db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, paperItem.BookId);
            db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, paperItem.ChapterId);
            db.AddInParameter(dbCommand, "p_category_id", DbType.String, paperItem.CategoryId);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, paperItem.OrganizationId);
            db.AddInParameter(dbCommand, "p_type_id", DbType.Int32, paperItem.TypeId);
            db.AddInParameter(dbCommand, "p_complete_time", DbType.Int32, paperItem.CompleteTime);
            db.AddInParameter(dbCommand, "p_difficulty_id", DbType.Int32, paperItem.DifficultyId);
            db.AddInParameter(dbCommand, "p_source", DbType.String, paperItem.Source);
            db.AddInParameter(dbCommand, "p_version", DbType.String, paperItem.Version);
            db.AddInParameter(dbCommand, "p_score", DbType.Decimal, paperItem.Score);
            db.AddInParameter(dbCommand, "p_content", DbType.String, paperItem.Content);
            db.AddInParameter(dbCommand, "p_answer_count", DbType.Int32, paperItem.AnswerCount);
            db.AddInParameter(dbCommand, "p_select_answer", DbType.String, paperItem.SelectAnswer);
            db.AddInParameter(dbCommand, "p_standard_answer", DbType.String, paperItem.StandardAnswer);
            db.AddInParameter(dbCommand, "p_description", DbType.String, paperItem.Description);
            db.AddInParameter(dbCommand, "p_outdate_date", DbType.DateTime, paperItem.OutDateDate);
            db.AddInParameter(dbCommand, "p_used_count", DbType.Int32, paperItem.UsedCount);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, paperItem.StatusId);
            db.AddInParameter(dbCommand, "p_create_person", DbType.String, paperItem.CreatePerson);
            db.AddInParameter(dbCommand, "p_create_time", DbType.DateTime, paperItem.CreateTime);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, paperItem.Memo);

            int nRecordAffected = db.ExecuteNonQuery(dbCommand);
            paperItem.ItemId = (int)db.GetParameterValue(dbCommand, "p_paper_Item_Id");

            return nRecordAffected;
        }
        
        public int DeletePaperItem(int paperItemId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();
            int k = 0;
            
            try
            {
                string sqlCommand = "USP_paper_ITEM_D";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                db.AddInParameter(dbCommand, "p_PAPER_item_id", DbType.Int32, paperItemId);
                db.AddOutParameter(dbCommand, "p_paper_Id", DbType.Int32, 0);
                db.AddOutParameter(dbCommand, "p_paper_subject_Id", DbType.Int32, 0);              

                k = db.ExecuteNonQuery(dbCommand, transaction);

                int i = (int)db.GetParameterValue(dbCommand, "p_paper_subject_Id");
                int j = (int)db.GetParameterValue(dbCommand, "p_paper_Id");

                string sqlCommand1 = "USP_paper_Subject_Update_score";
                DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand1);
                db.AddInParameter(dbCommand1, "p_paper_subject_Id", DbType.Int32, i);
                db.ExecuteNonQuery(dbCommand1, transaction);

                string sqlCommand2 = "USP_paper_Update_score";
                DbCommand dbCommand2 = db.GetStoredProcCommand(sqlCommand2);
                db.AddInParameter(dbCommand2, "p_paper_Id", DbType.Int32, j);
                db.ExecuteNonQuery(dbCommand2, transaction);

                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }

            connection.Close();

            return k;
        }

        public void AddPaperItem(IList<PaperItem> PaperItems)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();
            int i = 0;
            int j = 0;

            try
            {
                foreach (PaperItem item in PaperItems)
                {
                    string sqlCommand = "USP_paper_ITEM_I";

                    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                    db.AddOutParameter(dbCommand, "p_paper_Item_Id", DbType.Int32, 4);
                    db.AddInParameter(dbCommand, "p_paper_Id", DbType.Int32, item.PaperId);
                    db.AddInParameter(dbCommand, "p_paper_Subject_Id", DbType.Int32, item.PaperSubjectId);
                    db.AddInParameter(dbCommand, "p_order_Index", DbType.Int32, item.OrderIndex);
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

                    i = item.PaperId;
                    j = item.PaperSubjectId;

                    db.ExecuteNonQuery(dbCommand, transaction);
                }

                string sqlCommand1 = "USP_paper_Subject_Update_score";
                DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand1);
                db.AddInParameter(dbCommand1, "p_paper_subject_Id", DbType.Int32, j);
                db.ExecuteNonQuery(dbCommand1, transaction);

                string sqlCommand2 = "USP_paper_Update_score";
                DbCommand dbCommand2 = db.GetStoredProcCommand(sqlCommand2);
                db.AddInParameter(dbCommand2, "p_paper_Id", DbType.Int32, i);
                db.ExecuteNonQuery(dbCommand2, transaction);

                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            connection.Close();
        }

        public int UpdatePaperItem(PaperItem item)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_paper_ITEM_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_paper_Item_Id", DbType.Int32, item.PaperItemId);
            db.AddInParameter(dbCommand, "p_paper_Id", DbType.Int32, item.PaperId);
            db.AddInParameter(dbCommand, "p_paper_Subject_Id", DbType.Int32, item.PaperSubjectId);
            db.AddInParameter(dbCommand, "p_order_Index", DbType.Int32, item.OrderIndex);
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

            return db.ExecuteNonQuery(dbCommand);
        }

        public PaperItem GetPaperItem(int paperItemId)
        {
            PaperItem item = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_paper_ITEM_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_paper_item_id", DbType.Int32, paperItemId);
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

        public IList<PaperItem> GetItemsByPaperId(int paperId)
        {
            IList<PaperItem> items = new List<PaperItem>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_paper_ITEM_L";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Paper_id", DbType.Int32, paperId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    items.Add(CreateModelObject(dataReader));
                }
            }

            return items;
        }

        public IList<PaperItem> GetItemsByPaperSubjectId(int paperSubjectId)
        {
            IList<PaperItem> items = new List<PaperItem>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_paper_ITEM_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Paper_Subject_id", DbType.Int32, paperSubjectId);        
            
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
        /// 根据试卷大题ID查询试卷试题(路局查询站段)
        /// </summary>
        /// <param name="paperSubjectId"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public IList<PaperItem> GetItemsByPaperSubjectIdByOrgID(int paperSubjectId,int orgID)
        {
            IList<PaperItem> items = new List<PaperItem>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_paper_ITEM_Q_Org";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Paper_Subject_id", DbType.Int32, paperSubjectId);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
            db.AddOutParameter(dbCommand, "p_net_name", DbType.String, 50);

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

        public static PaperItem CreateModelObject(IDataReader dataReader)
        {
            return new PaperItem(
                DataConvert.ToInt(dataReader[GetMappingFieldName("PaperItemId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("PaperId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("PaperSubjectId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrderIndex")]),
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
