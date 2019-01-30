using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using RailExam.Model;
using DSunSoft.Data;

namespace RailExam.DAL
{
    public class KnowledgeDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static KnowledgeDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("knowledgeid", "KNOWLEDGE_ID");
            _ormTable.Add("parentid", "PARENT_ID");
            _ormTable.Add("idpath", "ID_PATH");
            _ormTable.Add("levelnum", "LEVEL_NUM");
            _ormTable.Add("orderindex", "ORDER_INDEX");
            _ormTable.Add("knowledgename", "KNOWLEDGE_NAME");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("memo", "MEMO");
            _ormTable.Add("istemplate", "IS_TEMPLATE");
            _ormTable.Add("ispromotion", "IS_PROMOTION");
        }

        /// <summary>
        /// 查询组织机构
        /// </summary>
        /// <param name="knowledgeId"></param>
        /// <param name="parentId"></param>
        /// <param name="idPath"></param>
        /// <param name="levelNum"></param>
        /// <param name="orderIndex"></param>
        /// <param name="knowledgeName"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">起始记录行</param>
        /// <param name="maximumRows">每页记录条数</param>
        /// <param name="orderBy">排序字符串，如"FieldName ASC"</param>
        /// <returns></returns>
        public IList<Knowledge> GetKnowledges(int knowledgeId, int parentId, string idPath, int levelNum, int orderIndex,
             string knowledgeName, string description, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<Knowledge> knowledges = new List<Knowledge>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_KNOWLEDGE_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.AnsiString, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Knowledge knowledge = CreateModelObject(dataReader);

                    knowledges.Add(knowledge);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return knowledges;
        }

        /// <summary>
        /// 获取所有知识
        /// </summary>
        /// <returns>所有知识</returns>
        public IList<Knowledge> GetKnowledges()
        {
            IList<Knowledge> knowledges = new List<Knowledge>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_GET_ALL");

            db.AddInParameter(dbCommand, "p_table_name", DbType.String, "KNOWLEDGE");
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, "LEVEL_NUM, ORDER_INDEX");

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Knowledge knowledge = CreateModelObject(dataReader);

                    knowledges.Add(knowledge);
                }
            }

            _recordCount = knowledges.Count;

            return knowledges;
        }

		/// <summary>
		/// 获取所有知识
		/// </summary>
		/// <returns>所有知识</returns>
		public IList<Knowledge> GetKnowledgesByOrgID(int orgID)
		{
			IList<Knowledge> knowledges = new List<Knowledge>();
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetStoredProcCommand("USP_Knowledge_G_Org");

			db.AddInParameter(dbCommand, "p_org_id", DbType.String, orgID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					Knowledge knowledge = CreateModelObject(dataReader);

					knowledges.Add(knowledge);
				}
			}

			_recordCount = knowledges.Count;

			return knowledges;
		}

		public IList<Knowledge> GetKnowledgesByWhereClause(string whereClause, string orderby)
		{
			IList<Knowledge> knowledges = new List<Knowledge>();
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetStoredProcCommand("USP_Knowledge_WhereClause");

			db.AddInParameter(dbCommand, "p_sql", DbType.String, whereClause);
			db.AddInParameter(dbCommand, "p_order_by", DbType.String, orderby);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					Knowledge knowledge = CreateModelObject(dataReader);

					knowledges.Add(knowledge);
				}
			}

			_recordCount = knowledges.Count;

			return knowledges;
		}

		public IList<Knowledge> GetKnowledgesByParentID(int parentID)
		{
			IList<Knowledge> knowledges = new List<Knowledge>();
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetStoredProcCommand("USP_KNOWLEDGE_P");

			db.AddInParameter(dbCommand, "p_parent_id", DbType.String, parentID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					Knowledge knowledge = CreateModelObject(dataReader);

					knowledges.Add(knowledge);
				}
			}

			_recordCount = knowledges.Count;

			return knowledges;
		}

        public Knowledge GetKnowledge(int KnowledgeId)
        {
            Knowledge knowledge = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_KNOWLEDGE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "p_Knowledge_id", DbType.Int32, KnowledgeId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    knowledge = CreateModelObject(dataReader);
                }
            }

            return knowledge;
        }

        public int AddKnowledge(Knowledge knowledge)
        {
            int id = 0;
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_KNOWLEDGE_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_knowledge_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, knowledge.ParentId);
            db.AddOutParameter(dbCommand, "p_id_path", DbType.String, 50);
            db.AddOutParameter(dbCommand, "p_level_num", DbType.Int32, 4);
            db.AddOutParameter(dbCommand, "p_order_index", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_knowledge_name", DbType.String, knowledge.KnowledgeName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, knowledge.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, knowledge.Memo);
            db.AddInParameter(dbCommand, "p_is_template", DbType.Int32, knowledge.IsTemplate ? 1 : 0);
            db.AddInParameter(dbCommand, "p_is_promotion", DbType.Int32, knowledge.IsPromotion ? 1 : 0);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_knowledge_id"));
               
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            connection.Close();

            return id;
        }

        public void UpdateKnowledge(Knowledge knowledge)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_KNOWLEDGE_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_knowledge_id", DbType.Int32, knowledge.KnowledgeId);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, knowledge.ParentId);
            db.AddInParameter(dbCommand, "p_id_path", DbType.String, knowledge.IdPath);
            db.AddInParameter(dbCommand, "p_level_num", DbType.Int32, knowledge.LevelNum);
            db.AddInParameter(dbCommand, "p_order_index", DbType.Int32, knowledge.OrderIndex);
            db.AddInParameter(dbCommand, "p_knowledge_name", DbType.String, knowledge.KnowledgeName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, knowledge.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, knowledge.Memo);
            db.AddInParameter(dbCommand, "p_is_template", DbType.Int32, knowledge.IsTemplate?1:0);
            db.AddInParameter(dbCommand, "p_is_promotion", DbType.Int32, knowledge.IsPromotion ? 1 : 0);

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

        public void DeleteKnowledge(int knowledgeId,ref int  errorCode)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_KNOWLEDGE_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_knowledge_id", DbType.Int32, knowledgeId);

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

        public bool Move(int knowledgeId, bool bUp)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_TREE_NODE_M";
            DbCommand dbCmd = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCmd, "p_table_name", DbType.String, "KNOWLEDGE");
            db.AddInParameter(dbCmd, "p_id_field_name", DbType.String, "KNOWLEDGE_ID");
            db.AddInParameter(dbCmd, "p_id", DbType.Int32, knowledgeId);
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

        public static Knowledge CreateModelObject(IDataReader dataReader)
        {
            return new Knowledge(
                DataConvert.ToInt(dataReader[GetMappingFieldName("KnowledgeId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ParentId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("IdPath")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("LevelNum")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrderIndex")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("KnowledgeName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("IsTemplate")])==1?true:false,
                DataConvert.ToInt(dataReader[GetMappingFieldName("IsPromotion")]) == 1 ? true : false,
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }
    }
}
