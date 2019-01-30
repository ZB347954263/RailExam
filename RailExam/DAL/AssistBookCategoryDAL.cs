using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
    public class AssistBookCategoryDAL
    {
       private static Hashtable _ormTable;
        private int _recordCount = 0;

        static AssistBookCategoryDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("assistbookcategoryid", "ASSIST_BOOK_CATEGORY_ID");
            _ormTable.Add("parentid", "PARENT_ID");
            _ormTable.Add("idpath", "ID_PATH");
            _ormTable.Add("levelnum", "LEVEL_NUM");
            _ormTable.Add("orderindex", "ORDER_INDEX");
            _ormTable.Add("assistbookcategoryname", "CATEGORY_NAME");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("memo", "MEMO");
        }

        /// <summary>
        /// 查询组织机构
        /// </summary>
        /// <param name="assistBookCategoryId"></param>
        /// <param name="parentId"></param>
        /// <param name="idPath"></param>
        /// <param name="levelNum"></param>
        /// <param name="orderIndex"></param>
        /// <param name="assistBookCategoryName"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">起始记录行</param>
        /// <param name="maximumRows">每页记录条数</param>
        /// <param name="orderBy">排序字符串，如"FieldName ASC"</param>
        /// <returns></returns>
        public IList<AssistBookCategory> GetAssistBookCategorys(int assistBookCategoryId, int parentId, string idPath, int levelNum, int orderIndex,
             string assistBookCategoryName, string description, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<AssistBookCategory> assistBookCategorys = new List<AssistBookCategory>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_CATEGORY_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.AnsiString, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    AssistBookCategory assistBookCategory = CreateModelObject(dataReader);

                    assistBookCategorys.Add(assistBookCategory);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return assistBookCategorys;
        }

        /// <summary>
        /// 获取所有知识
        /// </summary>
        /// <returns>所有知识</returns>
        public IList<AssistBookCategory> GetAssistBookCategorys()
        {
            IList<AssistBookCategory> assistBookCategorys = new List<AssistBookCategory>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_GET_ALL");

            db.AddInParameter(dbCommand, "p_table_name", DbType.String, "ASSIST_BOOK_CATEGORY");
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, "LEVEL_NUM, ORDER_INDEX,ASSIST_BOOK_CATEGORY_ID ASC");

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    AssistBookCategory assistBookCategory = CreateModelObject(dataReader);

                    assistBookCategorys.Add(assistBookCategory);
                }
            }

            _recordCount = assistBookCategorys.Count;

            return assistBookCategorys;
        }

        public AssistBookCategory GetAssistBookCategory(int AssistBookCategoryId)
        {
            AssistBookCategory assistBookCategory = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_CATEGORY_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "p_assist_book_category_id", DbType.Int32, AssistBookCategoryId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    assistBookCategory = CreateModelObject(dataReader);
                }
            }

            return assistBookCategory;
        }

        public int AddAssistBookCategory(AssistBookCategory assistBookCategory)
        {
            int id = 0;
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_CATEGORY_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_assist_book_category_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, assistBookCategory.ParentId);
            db.AddOutParameter(dbCommand, "p_id_path", DbType.String, 20);
            db.AddOutParameter(dbCommand, "p_level_num", DbType.Int32, 4);
            db.AddOutParameter(dbCommand, "p_order_index", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_assist_book_category_name", DbType.String, assistBookCategory.AssistBookCategoryName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, assistBookCategory.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, assistBookCategory.Memo);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_assist_book_category_id"));
               
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            connection.Close();

            return id;
        }

        public void UpdateAssistBookCategory(AssistBookCategory assistBookCategory)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_CATEGORY_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_assist_book_category_id", DbType.Int32, assistBookCategory.AssistBookCategoryId);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, assistBookCategory.ParentId);
            db.AddInParameter(dbCommand, "p_id_path", DbType.String, assistBookCategory.IdPath);
            db.AddInParameter(dbCommand, "p_level_num", DbType.Int32, assistBookCategory.LevelNum);
            db.AddInParameter(dbCommand, "p_order_index", DbType.Int32, assistBookCategory.OrderIndex);
            db.AddInParameter(dbCommand, "p_assist_book_category_name", DbType.String, assistBookCategory.AssistBookCategoryName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, assistBookCategory.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, assistBookCategory.Memo);

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

        public void DeleteAssistBookCategory(int assistBookCategoryId,ref int  errorCode)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ASSIST_BOOK_CATEGORY_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_assist_book_category_id", DbType.Int32, assistBookCategoryId);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);

                transaction.Commit();
                errorCode = 0;
            }
            catch(OracleException  ex)
            {
                transaction.Rollback();
                errorCode = ex.Code;
            }
            connection.Close();
        }

        public bool Move(int assistBookCategoryId, bool bUp)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TREE_NODE_M";
            DbCommand dbCmd = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCmd, "p_table_name", DbType.String, "ASSIST_BOOK_CATEGORY");
            db.AddInParameter(dbCmd, "p_id_field_name", DbType.String, "ASSIST_BOOK_CATEGORY_ID");
            db.AddInParameter(dbCmd, "p_id", DbType.Int32, assistBookCategoryId);
            db.AddInParameter(dbCmd, "p_direction", DbType.Int32, bUp ? 1 : 0);
            db.AddOutParameter(dbCmd, "p_result", DbType.Int32, 4);

            db.ExecuteNonQuery(dbCmd);

            return ((int)db.GetParameterValue(dbCmd, "p_result") == 1) ? true : false;
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

        public static AssistBookCategory CreateModelObject(IDataReader dataReader)
        {
            return new AssistBookCategory(
                DataConvert.ToInt(dataReader[GetMappingFieldName("AssistBookCategoryId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ParentId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("IdPath")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("LevelNum")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrderIndex")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("AssistBookCategoryName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }
    }
}
