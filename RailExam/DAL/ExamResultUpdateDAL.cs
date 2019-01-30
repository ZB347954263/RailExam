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
   public class ExamResultUpdateDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        /// <summary>
        /// 空参数构造函数
        /// </summary>
       public ExamResultUpdateDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("examresultupdateid", "exam_result_update_id");
            _ormTable.Add("examresultid", "exam_result_id");
            _ormTable.Add("oldscore", "old_score");
            _ormTable.Add("newscore", "new_score");
            _ormTable.Add("updateperson", "update_person");
            _ormTable.Add("updatedate", "update_date");
            _ormTable.Add("updatecause", "update_cause");
            _ormTable.Add("memo", "memo");
            _ormTable.Add("examname", "exam_name");
            _ormTable.Add("employeename", "employee_name");
            _ormTable.Add("orgname", "Org_Name");
                         
        }

       public static ExamResultUpdate CreateModelObject(IDataReader dataReader)
       {
           return new ExamResultUpdate(
               DataConvert.ToInt(dataReader[GetMappingFieldName("examresultupdateid")]),
               DataConvert.ToInt(dataReader[GetMappingFieldName("examresultid")]),
               DataConvert.ToDecimal(dataReader[GetMappingFieldName("oldscore")]),
               DataConvert.ToDecimal(dataReader[GetMappingFieldName("newscore")]),
               DataConvert.ToString(dataReader[GetMappingFieldName("updateperson")]),
               DataConvert.ToDateTime(dataReader[GetMappingFieldName("updatedate")]),
               DataConvert.ToString(dataReader[GetMappingFieldName("updatecause")]),
               DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]) ,
                DataConvert.ToString(dataReader[GetMappingFieldName("examName")]) ,
                DataConvert.ToString(dataReader[GetMappingFieldName("employeeName")]) ,
                DataConvert.ToString(dataReader[GetMappingFieldName("OrgName")]));
       }

       public IList<ExamResultUpdate> GetExamResultUpdates()
       {
           IList<ExamResultUpdate> examResultUpdates = new List<ExamResultUpdate>();

           Database db = DatabaseFactory.CreateDatabase();

           string sqlCommand = "USP_exam_result_update_S";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            

           using (IDataReader dataReader = db.ExecuteReader(dbCommand))
           {
               while (dataReader.Read())
               {
                   examResultUpdates.Add(CreateModelObject(dataReader));
               }
           }

           return examResultUpdates;
       }

       public ExamResultUpdate GetExamResultUpdate(int ExamResultUpdateId)
       {
           ExamResultUpdate examResultUpdate = null;

           Database db = DatabaseFactory.CreateDatabase();
           string sqlCommand = "USP_exam_result_update_G";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

           db.AddInParameter(dbCommand, "p_exam_result_update_id", DbType.Int32, ExamResultUpdateId);

           using (IDataReader dataReader = db.ExecuteReader(dbCommand))
           {
               if (dataReader.Read())
               {
                   examResultUpdate = CreateModelObject(dataReader);
               }
           }

           return examResultUpdate;
       }

       public void UpdateExamResultUpdate(int ExamResultUpdateId, string updatecause,string memo)
       {
           Database db = DatabaseFactory.CreateDatabase();

           string sqlCommand = "USP_exam_result_update_u";
           DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
           db.AddInParameter(dbCommand, "p_exam_result_update_id", DbType.Int32, ExamResultUpdateId);
           db.AddInParameter(dbCommand, "p_update_cause", DbType.String, updatecause);
           db.AddInParameter(dbCommand, "p_memo", DbType.String, memo);

           DbConnection connection = db.CreateConnection();
           connection.Open();
           DbTransaction transaction = connection.BeginTransaction();

           try
           {
               db.ExecuteNonQuery(dbCommand, transaction);

               transaction.Commit();
           }
           catch (System.SystemException ex)
           {
               transaction.Rollback();
               throw ex;
           }
           connection.Close();
       }



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

    }
}
