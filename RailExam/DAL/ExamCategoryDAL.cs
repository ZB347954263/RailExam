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
    public class ExamCategoryDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static ExamCategoryDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("examcategoryid", "Exam_Category_ID");
            _ormTable.Add("parentid", "PARENT_ID");
            _ormTable.Add("idpath", "ID_PATH");
            _ormTable.Add("levelnum", "LEVEL_NUM");
            _ormTable.Add("orderindex", "ORDER_INDEX");
            _ormTable.Add("categoryname", "Category_NAME");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("memo", "MEMO");
        }

        /// <summary>
        /// 查询组织机构
        /// </summary>
        /// <param name="ExamCategoryId"></param>
        /// <param name="parentId"></param>
        /// <param name="idPath"></param>
        /// <param name="levelNum"></param>
        /// <param name="orderIndex"></param>
        /// <param name="CategoryName"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">起始记录行</param>
        /// <param name="maximumRows">每页记录条数</param>
        /// <param name="orderBy">排序字符串，如"FieldName ASC"</param>
        /// <returns></returns>
        public IList<ExamCategory> GetExamCategory(int ExamCategoryId, int parentId, string idPath, int levelNum, int orderIndex,
             string CategoryName, string description, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<ExamCategory> examCategories = new List<ExamCategory>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_Category_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.AnsiString, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    ExamCategory examCategory = CreateModelObject(dataReader);

                    examCategories.Add(examCategory);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return examCategories;
        }

        public IList<ExamCategory> GetExamCategories()
        {
            IList<ExamCategory> examCategories = new List<ExamCategory>();

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_GET_ALL");

            db.AddInParameter(dbCommand, "p_table_name", DbType.String, "EXAM_CATEGORY");
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, "LEVEL_NUM, ORDER_INDEX ASC");

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    ExamCategory examCategory = CreateModelObject(dataReader);

                    examCategories.Add(examCategory);
                }
            }

            _recordCount = examCategories.Count;

            return examCategories;
        }

        public ExamCategory GetExamCategory(int ExamCategoryId)
        {
            ExamCategory examCategory = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_Category_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "p_Exam_Category_id", DbType.Int32, ExamCategoryId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    examCategory = CreateModelObject(dataReader);
                }
            }

            return examCategory;
        }

        public int AddExamCategory(ExamCategory examCategory)
        {
            int id = 0;
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_Category_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_Exam_Category_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, examCategory.ParentId);
            db.AddOutParameter(dbCommand, "p_id_path", DbType.String, 20);
            db.AddOutParameter(dbCommand, "p_level_num", DbType.Int32, 4);
            db.AddOutParameter(dbCommand, "p_order_index", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_Category_name", DbType.String, examCategory.CategoryName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, examCategory.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, examCategory.Memo);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_Exam_Category_id"));

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            connection.Close();

            return id;
        }

        public void UpdateExamCategory(ExamCategory examCategory)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_Category_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Exam_Category_id", DbType.Int32, examCategory.ExamCategoryId);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, examCategory.ParentId);
            db.AddInParameter(dbCommand, "p_id_path", DbType.String, examCategory.IdPath);
            db.AddInParameter(dbCommand, "p_level_num", DbType.Int32, examCategory.LevelNum);
            db.AddInParameter(dbCommand, "p_order_index", DbType.Int32, examCategory.OrderIndex);
            db.AddInParameter(dbCommand, "p_Category_name", DbType.String, examCategory.CategoryName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, examCategory.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, examCategory.Memo);

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

        public void DeleteExamCategory(int ExamCategoryId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Exam_Category_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Exam_Category_id", DbType.Int32, ExamCategoryId);

            db.ExecuteNonQuery(dbCommand);
        }

        public bool Move(int ExamCategoryId, bool bUp)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TREE_NODE_M";
            DbCommand dbCmd = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCmd, "p_table_name", DbType.String, "Exam_Category");
            db.AddInParameter(dbCmd, "p_id_field_name", DbType.String, "Exam_Category_id");
            db.AddInParameter(dbCmd, "p_id", DbType.Int32, ExamCategoryId);
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

        public static ExamCategory CreateModelObject(IDataReader dataReader)
        {
            return new ExamCategory(
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamCategoryId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ParentId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("IdPath")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("LevelNum")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrderIndex")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("CategoryName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }
    }
}
