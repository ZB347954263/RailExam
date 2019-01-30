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
    public class RegulationDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static RegulationDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("regulationid", "REGULATION_ID");
            _ormTable.Add("categoryid", "CATEGORY_ID");
            _ormTable.Add("categoryname", "CATEGORY_NAME");
            _ormTable.Add("regulationno", "REGULATION_NO");
            _ormTable.Add("regulationname", "REGULATION_NAME");
            _ormTable.Add("version", "VERSION");
            _ormTable.Add("fileno", "FILE_NO");
            _ormTable.Add("titleremark", "TITLE_REMARK");
            _ormTable.Add("issuedate", "ISSUE_DATE");
            _ormTable.Add("executedate", "EXECUTE_DATE");
            _ormTable.Add("status", "STATUS");
            _ormTable.Add("url", "URL");
            _ormTable.Add("memo", "MEMO");
        }

        public IList<Regulation> GetRegulations(int regulationID, int categoryID, string regulationNo,
                string regulationName, string version, string fileNo, string titleRemark, 
                DateTime issueDate, DateTime executeDate, int status, string url,
                string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<Regulation> regulations = new List<Regulation>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_REGULATION_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Regulation regulation = CreateModelObject(dataReader);

                    regulations.Add(regulation);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return regulations;
        }

        public IList<Regulation> GetRegulations()
        {
            IList<Regulation> regulations = new List<Regulation>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_REGULATION_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy("regulationid"));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Regulation regulation = CreateModelObject(dataReader);

                    regulations.Add(regulation);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return regulations;
        }

        public IList<Regulation> GetRegulations(string regulationName, string regulationNo, int status)
        {
            IList<Regulation> regulations = new List<Regulation>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_REGULATION_F";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_regulation_name", DbType.String, regulationName);
            db.AddInParameter(dbCommand, "p_regulation_no", DbType.String, regulationNo);
            db.AddInParameter(dbCommand, "p_status", DbType.Int32, status);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Regulation regulation = CreateModelObject(dataReader);

                    regulations.Add(regulation);
                }
            }

            return regulations;
        }

        public IList<Regulation> GetRegulationsByCategoryIDPath(string strCategoryIDPath)
        {
            IList<Regulation> regulations = new List<Regulation>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_REGULATION_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_id_path", DbType.String, strCategoryIDPath);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Regulation regulation = CreateModelObject(dataReader);
                    regulations.Add(regulation);
                }
            }

            return regulations;
        }

        public Regulation GetRegulationByRegulationID(int regulationID)
        {
            Regulation regulation = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_REGULATION_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_regulation_id", DbType.Int32, regulationID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    regulation = CreateModelObject(dataReader);
                }
            }

            return regulation;
        }


        public int AddRegulation(Regulation regulation)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_REGULATION_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_regulation_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_category_id", DbType.Int32, regulation.CategoryID);
            db.AddInParameter(dbCommand, "p_regulation_no", DbType.String, regulation.RegulationNo);
            db.AddInParameter(dbCommand, "p_regulation_name", DbType.String, regulation.RegulationName);
            db.AddInParameter(dbCommand, "p_version", DbType.String, regulation.Version);
            db.AddInParameter(dbCommand, "p_file_no", DbType.String, regulation.FileNo);
            db.AddInParameter(dbCommand, "p_title_remark", DbType.String, regulation.TitleRemark);
            db.AddInParameter(dbCommand, "p_issue_date", DbType.Date, regulation.IssueDate);
            db.AddInParameter(dbCommand, "p_execute_date", DbType.Date, regulation.ExecuteDate);
            db.AddInParameter(dbCommand, "p_status", DbType.Int32, regulation.Status);
            db.AddInParameter(dbCommand, "p_url", DbType.String, regulation.Url);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, regulation.Memo);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
               
                transaction.Commit();
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            connection.Close();

            return Convert.ToInt32(db.GetParameterValue(dbCommand, "p_regulation_id"));;
        }

        public void UpdateRegulation(Regulation regulation)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_REGULATION_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_regulation_id", DbType.Int32, regulation.RegulationID);
            db.AddInParameter(dbCommand, "p_category_id", DbType.Int32, regulation.CategoryID);
            db.AddInParameter(dbCommand, "p_regulation_no", DbType.String, regulation.RegulationNo);
            db.AddInParameter(dbCommand, "p_regulation_name", DbType.String, regulation.RegulationName);
            db.AddInParameter(dbCommand, "p_version", DbType.String, regulation.Version);
            db.AddInParameter(dbCommand, "p_file_no", DbType.String, regulation.FileNo);
            db.AddInParameter(dbCommand, "p_title_remark", DbType.String, regulation.TitleRemark);
            db.AddInParameter(dbCommand, "p_issue_date", DbType.Date, regulation.IssueDate);
            db.AddInParameter(dbCommand, "p_execute_date", DbType.Date, regulation.ExecuteDate);
            db.AddInParameter(dbCommand, "p_status", DbType.Int32, regulation.Status);
            db.AddInParameter(dbCommand, "p_url", DbType.String, regulation.Url);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, regulation.Memo);
          
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;

            }
            connection.Close();
        }

        public void DeleteRegulation(int regulationID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_REGULATION_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_regulation_id", DbType.Int32, regulationID);

            db.ExecuteNonQuery(dbCommand);
        }

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

        public static Regulation CreateModelObject(IDataReader dataReader)
        {
            return new Regulation(
                DataConvert.ToInt(dataReader[GetMappingFieldName("RegulationID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("CategoryID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("CategoryName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("RegulationNo")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("RegulationName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Version")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("FileNo")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("TitleRemark")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("IssueDate")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("ExecuteDate")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("Status")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Url")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }
    }
}

