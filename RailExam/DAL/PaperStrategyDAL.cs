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
    public class PaperStrategyDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static PaperStrategyDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("papercategoryid", "Paper_Category_ID");
            _ormTable.Add("paperstrategyid", "paper_strategy_id");
            _ormTable.Add("israndomorder", "Is_Random_Order");
            _ormTable.Add("singleasmultiple", "Single_As_Multiple");
            _ormTable.Add("strategymode", "Strategy_Mode");
            _ormTable.Add("categoryname", "Category_Name");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("memo", "memo");
            _ormTable.Add("paperstrategyname", "Paper_Strategy_Name");
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="PaperCategoryId"></param>
        /// <param name="PaperStrategyId"></param>       
        /// <param name="IsRandomOrder"></param>
        /// <param name="SingleAsMultiple"></param>
        /// <param name="CategoryName"></param>
        /// <param name="description"></param>
        /// <param name="PaperStrategyName"></param>
        /// <param name="startRowIndex">起始记录行</param>
        /// <param name="maximumRows">每页记录条数</param>
        /// <param name="orderBy">排序字符串，如"FieldName ASC"</param>
        /// <returns></returns>
        public IList<PaperStrategy> GetPaperStrategy(int PaperStrategyId, int PaperCategoryId, bool IsRandomOrder, bool SingleAsMultiple,
            string PaperStrategyName, int StrategyMode, string description, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<PaperStrategy> paperCategories = new List<PaperStrategy>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_PAPER_STRATEGY_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.AnsiString, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    PaperStrategy paperCategory = CreateModelObject(dataReader);

                    paperCategories.Add(paperCategory);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return paperCategories;
        }

        public IList<PaperStrategy> GetPaperStrategyByPaperCategoryIDPath(string PaperCategoryIDPath)
        {
            IList<PaperStrategy> paperCategories = new List<PaperStrategy>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_PAPER_STRATEGY_Q1";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_id_path", DbType.String, PaperCategoryIDPath);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    PaperStrategy paperStrategy = CreateModelObject(dataReader);
                    paperCategories.Add(paperStrategy);
                }
            }

            return paperCategories;
        }

        public IList<PaperStrategy> GetPaperStrategyByPaperCategoryId(int PaperCategoryId)
        {
            IList<PaperStrategy> paperCategories = new List<PaperStrategy>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_PAPER_STRATEGY_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Paper_Category_id", DbType.Int32, PaperCategoryId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    PaperStrategy paperStrategy = CreateModelObject(dataReader);
                    paperCategories.Add(paperStrategy);
                }
            }

            return paperCategories;
        }

        public IList<PaperStrategy> GetPaperStrategyByPaperCategoryId(int PaperCategoryId, int StrategyMode)
        {
            IList<PaperStrategy> paperCategories = new List<PaperStrategy>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_PAPER_STRATEGY_F";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Paper_Category_id", DbType.Int32, PaperCategoryId);
            db.AddInParameter(dbCommand, "p_STRATEGY_MODE", DbType.Int32, StrategyMode);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    PaperStrategy paperStrategy = CreateModelObject(dataReader);
                    paperCategories.Add(paperStrategy);
                }
            }

            return paperCategories;
        }

        public PaperStrategy GetPaperStrategy(int PaperStrategyId)
        {
            PaperStrategy paperStrategy = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_PAPER_STRATEGY_G";

            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "p_Paper_Strategy_id", DbType.Int32, PaperStrategyId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    paperStrategy = CreateModelObject(dataReader);
                }
            }

            PaperCategoryDAL paperCategoryDAL = new PaperCategoryDAL();
            PaperCategory paperCategory = paperCategoryDAL.GetPaperCategory(paperStrategy.PaperCategoryId);

            paperStrategy.CategoryNames = GetPaperCategoryNames("/" + paperCategory.CategoryName, paperCategory.ParentId);

            return paperStrategy;
        }

        private string GetPaperCategoryNames(string strName, int nID)
        {
            string strPaperCategoryName = string.Empty;
            if (nID != 0)
            {
                PaperCategoryDAL paperCategoryDAL = new PaperCategoryDAL();
                PaperCategory paperCategory = paperCategoryDAL.GetPaperCategory(nID);

                if (paperCategory.ParentId != 0)
                {
                    strPaperCategoryName = GetPaperCategoryNames("/" + paperCategory.CategoryName, paperCategory.ParentId) + strName;
                }
                else
                {
                    strPaperCategoryName = paperCategory.CategoryName + strName;
                }
            }

            return strPaperCategoryName;
        }


        public int AddPaperStrategy(PaperStrategy paperStrategy)
        {
            int id = 0;
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_PAPER_STRATEGY_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_paper_strategy_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_paper_category_id", DbType.Int32, paperStrategy.PaperCategoryId);
            db.AddInParameter(dbCommand, "p_is_random_order", DbType.Int32, paperStrategy.IsRandomOrder ? 1 : 0);
            db.AddInParameter(dbCommand, "p_single_as_multiple", DbType.Int32, paperStrategy.SingleAsMultiple ? 1 : 0);
            db.AddInParameter(dbCommand, "p_Strategy_Mode", DbType.Int32, paperStrategy.StrategyMode);
            db.AddInParameter(dbCommand, "p_description", DbType.String, paperStrategy.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, paperStrategy.Memo);
            db.AddInParameter(dbCommand, "p_paper_strategy_name", DbType.String, paperStrategy.PaperStrategyName);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_paper_strategy_id"));

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            connection.Close();

            return id;
        }

        public void UpdatePaperStrategy(PaperStrategy paperStrategy)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_PAPER_STRATEGY_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_paper_strategy_id", DbType.Int32, paperStrategy.PaperStrategyId);
            db.AddInParameter(dbCommand, "p_paper_category_id", DbType.Int32, paperStrategy.PaperCategoryId);
            db.AddInParameter(dbCommand, "p_is_random_order", DbType.Int32, paperStrategy.IsRandomOrder ? 1 : 0);
            db.AddInParameter(dbCommand, "p_single_as_multiple", DbType.Int32, paperStrategy.SingleAsMultiple ? 1 : 0);
            db.AddInParameter(dbCommand, "p_description", DbType.String, paperStrategy.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, paperStrategy.Memo);
            db.AddInParameter(dbCommand, "p_paper_strategy_name", DbType.String, paperStrategy.PaperStrategyName);
            db.AddInParameter(dbCommand, "p_strategy_mode", DbType.Int32, paperStrategy.StrategyMode);

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

        public void DeletePaperStrategy(int PaperStrategyId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_PAPER_STRATEGY_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_paper_strategy_id", DbType.Int32, PaperStrategyId);

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

        public static PaperStrategy CreateModelObject(IDataReader dataReader)
        {
            return new PaperStrategy(
                DataConvert.ToInt(dataReader[GetMappingFieldName("PaperStrategyId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("PaperCategoryId")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("IsRandomOrder")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("SingleAsMultiple")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("StrategyMode")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("PaperStrategyName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("CategoryName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("memo")]));
        }
    }
}
