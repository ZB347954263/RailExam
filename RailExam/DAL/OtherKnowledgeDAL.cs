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
    public class OtherKnowledgeDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static OtherKnowledgeDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("otherknowledgeid", "Other_Knowledge_ID");
            _ormTable.Add("parentid", "PARENT_ID");
            _ormTable.Add("idpath", "ID_PATH");
            _ormTable.Add("levelnum", "LEVEL_NUM");
            _ormTable.Add("orderindex", "ORDER_INDEX");
            _ormTable.Add("otherknowledgename", "Other_Knowledge_NAME");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("memo", "MEMO");
        }

        /// <summary>
        /// 查询组织机构
        /// </summary>
        /// <param name="otherKnowledgeID"></param>
        /// <param name="parentID"></param>
        /// <param name="idPath"></param>
        /// <param name="levelNum"></param>
        /// <param name="orderIndex"></param>
        /// <param name="otherKnowledgeName"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">起始记录行</param>
        /// <param name="maximumRows">每页记录条数</param>
        /// <param name="orderBy">排序字符串，如"FieldName ASC"</param>
        /// <returns></returns>
        public IList<OtherKnowledge> GetOtherKnowledge(int otherKnowledgeID, int parentID, string idPath, int levelNum, int orderIndex,
             string otherKnowledgeName, string description, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<OtherKnowledge> otherKnowledges = new List<OtherKnowledge>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Other_Knowledge_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.AnsiString, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    OtherKnowledge otherKnowledge = CreateModelObject(dataReader);

                    otherKnowledges.Add(otherKnowledge);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return otherKnowledges;
        }

        public OtherKnowledge GetOtherKnowledge(int OtherKnowledgeID)
        {
            OtherKnowledge otherKnowledge = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USp_Other_Knowledge_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "p_Other_Knowledge_id", DbType.Int32, OtherKnowledgeID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    otherKnowledge = CreateModelObject(dataReader);
                }
            }

            return otherKnowledge;
        }

        public int AddOtherKnowledge(OtherKnowledge otherKnowledge)
        {
            int id = 0;
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Other_Knowledge_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_other_knowledge_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, otherKnowledge.ParentID);
            db.AddOutParameter(dbCommand, "p_id_path", DbType.String, 20);
            db.AddOutParameter(dbCommand, "p_level_num", DbType.Int32, 4);
            db.AddOutParameter(dbCommand, "p_order_index", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_Other_Knowledge_name", DbType.String, otherKnowledge.OtherKnowledgeName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, otherKnowledge.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, otherKnowledge.Memo);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_Other_Knowledge_id"));

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            connection.Close();

            return id;
        }

        public void UpdateOtherKnowledge(OtherKnowledge otherKnowledge)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Other_Knowledge_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Other_Knowledge_id", DbType.Int32, otherKnowledge.OtherKnowledgeID);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, otherKnowledge.ParentID);
            db.AddInParameter(dbCommand, "p_id_path", DbType.String, otherKnowledge.IdPath);
            db.AddInParameter(dbCommand, "p_level_num", DbType.Int32, otherKnowledge.LevelNum);
            db.AddInParameter(dbCommand, "p_order_index", DbType.Int32, otherKnowledge.OrderIndex);
            db.AddInParameter(dbCommand, "p_Other_Knowledge_name", DbType.String, otherKnowledge.OtherKnowledgeName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, otherKnowledge.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, otherKnowledge.Memo);

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

        public void DeleteOtherKnowledge(int OtherKnowledgeID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Other_Knowledge_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Other_Knowledge_id", DbType.Int32, OtherKnowledgeID);

            db.ExecuteNonQuery(dbCommand);
        }

        public bool Move(int OtherKnowledgeID, bool bUp)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCmd = db.GetStoredProcCommand("USP_TREE_NODE_M");

            db.AddInParameter(dbCmd, "p_table_name", DbType.String, "OTHERKNOWLEDGE");
            db.AddInParameter(dbCmd, "p_id_field_name", DbType.String, "OTHER_KNOWLEDGE_ID");
            db.AddInParameter(dbCmd, "p_id", DbType.Int32, OtherKnowledgeID);
            db.AddInParameter(dbCmd, "p_direction", DbType.Int32, (bUp ? 1 : 0));
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

        public static OtherKnowledge CreateModelObject(IDataReader dataReader)
        {
            return new OtherKnowledge(
                DataConvert.ToInt(dataReader[GetMappingFieldName("OtherKnowledgeID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ParentID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("IdPath")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("LevelNum")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrderIndex")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("OtherKnowledgeName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }
    }
}
