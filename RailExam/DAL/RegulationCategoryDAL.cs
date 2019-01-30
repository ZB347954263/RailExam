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
    public class RegulationCategoryDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static RegulationCategoryDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("regulationcategoryid", "REGULATION_CATEGORY_ID");
            _ormTable.Add("parentid", "PARENT_ID");
            _ormTable.Add("idpath", "ID_PATH");
            _ormTable.Add("levelnum", "LEVEL_NUM");
            _ormTable.Add("orderindex", "ORDER_INDEX");
            _ormTable.Add("categoryname", "CATEGORY_NAME");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("memo", "MEMO");
        }

        /// <summary>
        /// 查询组织机构
        /// </summary>
        /// <param name="regulationCategoryID"></param>
        /// <param name="parentID"></param>
        /// <param name="idPath"></param>
        /// <param name="levelNum"></param>
        /// <param name="orderIndex"></param>
        /// <param name="categoryName"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">起始记录行</param>
        /// <param name="maximumRows">每页记录条数</param>
        /// <param name="orderBy">排序字符串，如"FieldName ASC"</param>
        /// <returns></returns>
        public IList<RegulationCategory> GetRegulationCategories(int regulationCategoryID, int parentID, string idPath, int levelNum, int orderIndex,
             string categoryName, string description, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<RegulationCategory> regulationCategories = new List<RegulationCategory>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_REGULATION_CATEGORY_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.AnsiString, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    RegulationCategory regulationCategory = CreateModelObject(dataReader);

                    regulationCategories.Add(regulationCategory);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return regulationCategories;
        }

        public IList<RegulationCategory> GetRegulationCategories()
        {
            IList<RegulationCategory> regulationCategories = new List<RegulationCategory>();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_GET_ALL");

            db.AddInParameter(dbCommand, "p_table_name", DbType.String, "REGULATION_CATEGORY");
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, "LEVEL_NUM, ORDER_INDEX ASC");

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    RegulationCategory regulationCategory = CreateModelObject(dataReader);

                    regulationCategories.Add(regulationCategory);
                }
            }

            _recordCount = regulationCategories.Count;

            return regulationCategories;
        }

        public RegulationCategory GetRegulationCategory(int regulationCategoryID)
        {
            RegulationCategory regulationCategory = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_REGULATION_CATEGORY_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_regulation_category_id", DbType.Int32, regulationCategoryID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    regulationCategory = CreateModelObject(dataReader);
                }
            }

            return regulationCategory;
        }

        public int AddRegulationCategory(RegulationCategory regulationCategory)
        {
            int id = 0;
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_REGULATION_CATEGORY_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_regulation_category_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, regulationCategory.ParentID);
            db.AddOutParameter(dbCommand, "p_id_path", DbType.String, 20);
            db.AddOutParameter(dbCommand, "p_level_num", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_order_index", DbType.Int32, regulationCategory.OrderIndex);
            db.AddInParameter(dbCommand, "p_category_name", DbType.String, regulationCategory.CategoryName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, regulationCategory.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, regulationCategory.Memo);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_regulation_category_id"));

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }

            connection.Close();

            return id;
        }

        public void UpdateRegulationCategory(RegulationCategory regulationCategory)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_REGULATION_CATEGORY_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_regulation_category_id", DbType.Int32, regulationCategory.RegulationCategoryID);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, regulationCategory.ParentID);
            db.AddInParameter(dbCommand, "p_id_path", DbType.String, regulationCategory.IdPath);
            db.AddInParameter(dbCommand, "p_level_num", DbType.Int32, regulationCategory.LevelNum);
            db.AddInParameter(dbCommand, "p_order_index", DbType.Int32, regulationCategory.OrderIndex);
            db.AddInParameter(dbCommand, "p_category_name", DbType.String, regulationCategory.CategoryName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, regulationCategory.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, regulationCategory.Memo);

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

        public void DeleteRegulationCategory(int regulationCategoryID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_REGULATION_CATEGORY_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_regulation_category_id", DbType.Int32, regulationCategoryID);

            db.ExecuteNonQuery(dbCommand);
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

        public static RegulationCategory CreateModelObject(IDataReader dataReader)
        {
            return new RegulationCategory(
                DataConvert.ToInt(dataReader[GetMappingFieldName("RegulationCategoryID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ParentID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("IdPath")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("LevelNum")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrderIndex")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("CategoryName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }
    }
}
