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
    public class ImportanceDAL
    {
        private static Hashtable _ormTable;
		private int _recordCount = 0;

        static ImportanceDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("importanceid", "IMPORTANCE_ID");
            _ormTable.Add("importancename", "IMPORTANCE_NAME");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("isdefault", "IS_DEFAULT");
            _ormTable.Add("memo", "MEMO");
        }

        public IList<Importance> GetImportances(int importanceID, string importanceName, string Description, 
            bool isDefault, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<Importance> importances = new List<Importance>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_IMPORTANCE_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Importance importance = CreateModelObject(dataReader);

                    importances.Add(importance);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return importances;
        }

        public IList<Importance> GetImportances()
        {
            IList<Importance> importances = new List<Importance>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_IMPORTANCE_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy("importanceid"));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Importance importance = CreateModelObject(dataReader);

                    importances.Add(importance);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return importances;
        }

        public Importance GetImportance(int importanceID)
        {
            Importance importance;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_IMPORTANCE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_importance_id", DbType.Int32, importanceID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    importance = CreateModelObject(dataReader);
                }
                else
                {
                    importance = new Importance();
                }
            }

            return importance;
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

        public static Importance CreateModelObject(IDataReader dataReader)
        {
            return new Importance(
                DataConvert.ToInt(dataReader[GetMappingFieldName("ImportanceID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ImportanceName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("IsDefault")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }
    }
}
