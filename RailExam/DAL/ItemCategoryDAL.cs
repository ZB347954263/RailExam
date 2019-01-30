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
    public class ItemCategoryDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

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

        public static ItemCategory CreateModelObject(IDataReader dataReader)
        {
            return new ItemCategory(
                DataConvert.ToInt(dataReader[GetMappingFieldName("ItemCategoryId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ParentId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("IdPath")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("LevelNum")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrderIndex")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("CategoryName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }

        static ItemCategoryDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("itemcategoryid", "ITEM_CATEGORY_ID");
            _ormTable.Add("parentid", "PARENT_ID");
            _ormTable.Add("idpath", "ID_PATH");
            _ormTable.Add("levelnum", "LEVEL_NUM");
            _ormTable.Add("orderindex", "ORDER_INDEX");
            _ormTable.Add("categoryname", "CATEGORY_NAME");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("memo", "MEMO");
        }

        /// <summary>
        /// 新增试题分类
        /// </summary>
        /// <param name="itemCategory">试题分类</param>
        /// <returns>数据库受影响的行数</returns>
        public int AddItemCategory(ItemCategory itemCategory)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_CATEGORY_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_item_category_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, itemCategory.ParentId);
            db.AddOutParameter(dbCommand, "p_id_path", DbType.String, 50);
            db.AddOutParameter(dbCommand, "p_level_num", DbType.Int32, 4);
            db.AddOutParameter(dbCommand, "p_order_index", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_category_name", DbType.String, itemCategory.CategoryName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, itemCategory.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, itemCategory.Memo);

            int nRecordAffected = db.ExecuteNonQuery(dbCommand);
            itemCategory.ItemCategoryId = (int)db.GetParameterValue(dbCommand, "p_item_category_id");
            itemCategory.IdPath = (string)db.GetParameterValue(dbCommand, "p_id_path");
            itemCategory.LevelNum = (int)db.GetParameterValue(dbCommand, "p_level_num");
            itemCategory.OrderIndex = (int)db.GetParameterValue(dbCommand, "p_order_index");

            return nRecordAffected;
        }

        /// <summary>
        /// 删除试题分类
        /// </summary>
        /// <param name="itemCategoryId">试题分类ID</param>
        /// <returns>数据库受影响的行数</returns>
        public void DeleteItemCategory(int itemCategoryId,ref int errorCode)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_CATEGORY_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_item_category_id", DbType.Int32, itemCategoryId);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand,transaction);
                transaction.Commit();
                errorCode = 0;
            }
            catch(OracleException ex)
            {
                transaction.Rollback();
                errorCode = ex.Code;
            }
            connection.Close();
        }

        /// <summary>
        /// 修改试题分类
        /// </summary>
        /// <param name="itemCategory">试题分类</param>
        /// <returns>数据库受影响的行数</returns>
        public int UpdateItemCategory(ItemCategory itemCategory)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_CATEGORY_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_item_category_id", DbType.Int32, itemCategory.ItemCategoryId);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, itemCategory.ParentId);
            db.AddInParameter(dbCommand, "p_id_path", DbType.String, itemCategory.IdPath);
            db.AddInParameter(dbCommand, "p_level_num", DbType.Int32, itemCategory.LevelNum);
            db.AddInParameter(dbCommand, "p_order_index", DbType.Int32, itemCategory.OrderIndex);
            db.AddInParameter(dbCommand, "p_category_name", DbType.String, itemCategory.CategoryName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, itemCategory.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, itemCategory.Memo);

            return db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 按试题分类ID取试题分类
        /// </summary>
        /// <param name="itemCategoryId">试题分类ID</param>
        /// <returns>试题分类</returns>
        public ItemCategory GetItemCategory(int itemCategoryId)
        {
            ItemCategory itemCategory = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_CATEGORY_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_item_category_id", DbType.Int32, itemCategoryId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    itemCategory = CreateModelObject(dataReader);
                    break;
                }
            }

            return itemCategory;
        }


        /// <summary>
        /// 查询所有试题辅助分类
        /// </summary>
        /// <returns>所有试题辅助分类</returns>
        public IList<ItemCategory> GetItemCategories()
        {
            IList<ItemCategory> itemCategories = new List<ItemCategory>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_GET_ALL");

            db.AddInParameter(dbCommand, "p_table_name", DbType.String, "ITEM_CATEGORY");
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, "LEVEL_NUM, ORDER_INDEX ASC");

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    itemCategories.Add(CreateModelObject(dataReader));
                }
            }

            _recordCount = itemCategories.Count;

            return itemCategories;
        }

        /// <summary>
        /// 查询符合条件的试题分类
        /// </summary>
        /// <param name="itemCategoryId"></param>
        /// <param name="parentId"></param>
        /// <param name="idPath"></param>
        /// <param name="orderIndex"></param>
        /// <param name="categoryName"></param>
        /// <param name="levelNum"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="orderBy"></param>
        /// <returns>符合条件的试题分类</returns>
        public IList<ItemCategory> GetItemCategories(int itemCategoryId, int parentId, string idPath, int levelNum,
            int orderIndex, string categoryName, int description, string memo, 
            int startRowIndex,int maximumRows, string orderBy)
        {
            IList<ItemCategory> itemCategories = new List<ItemCategory>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ITEM_CATEGORY_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            //db.AddInParameter(dbCommand, "p_item_category_id", DbType.Int32, itemCategoryId);
            //db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, parentId);
            //db.AddInParameter(dbCommand, "p_id_path", DbType.String, idPath);
            //db.AddInParameter(dbCommand, "p_level_num", DbType.Int32, levelNum);
            //db.AddInParameter(dbCommand, "p_order_index", DbType.Int32, orderIndex);
            //db.AddInParameter(dbCommand, "p_category_name", DbType.String, categoryName);
            //db.AddInParameter(dbCommand, "p_description", DbType.String, description);
            //db.AddInParameter(dbCommand, "p_memo", DbType.String, memo);
            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    itemCategories.Add(CreateModelObject(dataReader));
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return itemCategories;
        }

        /// <summary>
        /// 是否可以移动节点
        /// </summary>
        /// <param name="itemCategoryId"></param>
        /// <param name="bUp"></param>
        /// <returns></returns>
        public bool Move(int itemCategoryId, bool bUp)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCmd = db.GetStoredProcCommand("USP_TREE_NODE_M");

            db.AddInParameter(dbCmd, "p_table_name", DbType.String, "ITEM_CATEGORY");
            db.AddInParameter(dbCmd, "p_id_field_name", DbType.String, "ITEM_CATEGORY_ID");
            db.AddInParameter(dbCmd, "p_id", DbType.Int32, itemCategoryId);
            db.AddInParameter(dbCmd, "p_direction", DbType.Int32, (bUp ? 1 : 0));
            db.AddOutParameter(dbCmd, "p_result", DbType.Int32, 4);

            db.ExecuteNonQuery(dbCmd);

            return ((int)db.GetParameterValue(dbCmd, "p_result") == 1) ? true : false;
        }
    }
}