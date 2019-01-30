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
    public class PaperStrategySubjectDAL
    {

        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static PaperStrategySubjectDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("paperstrategysubjectid", "PAPER_STRATEGY_SUBJECT_ID");
            _ormTable.Add("paperstrategyid", "PAPER_STRATEGY_ID");
            _ormTable.Add("orderindex", "Order_Index");
            _ormTable.Add("subjectname", "Subject_Name");

            _ormTable.Add("typename", "Type_Name");         
            _ormTable.Add("remark", "Remark");
            _ormTable.Add("memo", "MEMO");
            _ormTable.Add("totalscore", "Total_Score");
            _ormTable.Add("itemcount", "Item_Count");
            _ormTable.Add("itemtypeid", "Item_Type_Id");
            _ormTable.Add("unitscore", "Unit_Score");



        }


        public PaperStrategySubject GetPaperStrategySubject(int PaperStrategySubjectId)
        {
            PaperStrategySubject Paper;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_PAPER_STRATEGY_SUBJECT_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "p_PAPER_STRATEGY_SUBJECT_ID", DbType.Int32, PaperStrategySubjectId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    Paper = SubjectNamelObject(dataReader);
                }
                else
                {
                    Paper = new PaperStrategySubject();
                }
            }



            return Paper;
        }


        public IList<PaperStrategySubject> GetPaperStrategySubjectByPaperStrategyId(int PaperStrategyId)
        {
            IList<PaperStrategySubject> Papers = new List<PaperStrategySubject>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_PAPER_STRATEGY_SUBJECT_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_PAPER_STRATEGY_ID", DbType.Int32, PaperStrategyId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    PaperStrategySubject Paper = SubjectNamelObject(dataReader);
                    Papers.Add(Paper);
                }
            }

            return Papers;
        }

        public void UpdatePaperStrategySubject(int PaperStrategyId ,int TotalScore, IList<PaperStrategySubject> PaperStrategySubjects)
        {

            Database db = DatabaseFactory.CreateDatabase();

           
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {  
                foreach (PaperStrategySubject ps in PaperStrategySubjects)
                {
                    string sqlCommand1 = "USP_Strategy_SUBJECT_Update";
                    DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand1);

                    db.AddInParameter(dbCommand1, "p_PAPER_STRATEGY_SUBJECT_ID", DbType.Int32, ps.PaperStrategySubjectId);               
                    db.AddInParameter(dbCommand1, "p_Subject_Name", DbType.String, ps.SubjectName);
                    db.AddInParameter(dbCommand1, "p_Unit_Score", DbType.Decimal, ps.UnitScore);
                    db.AddInParameter(dbCommand1, "p_item_count", DbType.Decimal, ps.ItemCount);                  
                    db.ExecuteNonQuery(dbCommand1, transaction);
                    string sqlCommand3 = "USP_Strategy_SUBJECT_son_u";
                    DbCommand dbCommand3 = db.GetStoredProcCommand(sqlCommand3);

                    db.AddInParameter(dbCommand3, "p_PAPER_STRATEGY_SUBJECT_ID", DbType.Int32, ps.PaperStrategySubjectId);                   
                    db.AddInParameter(dbCommand3, "p_Unit_Score", DbType.Decimal, ps.UnitScore);
                    db.ExecuteNonQuery(dbCommand3, transaction);
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            connection.Close();



        }
        

        public void AddPaperStrategySubject(PaperStrategySubject Paper)
        {

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_PAPER_STRATEGY_SUBJECT_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_PAPER_STRATEGY_ID", DbType.Int32, Paper.PaperStrategyId);
            db.AddOutParameter(dbCommand, "p_PAPER_STRATEGY_SUBJECT_ID", DbType.Int32, Paper.PaperStrategySubjectId);
            db.AddInParameter(dbCommand, "p_Order_Index", DbType.String, Paper.OrderIndex);
            db.AddInParameter(dbCommand, "p_Item_Count", DbType.Int32, Paper.ItemCount);
            db.AddInParameter(dbCommand, "p_Item_Type_Id", DbType.Int32, Paper.ItemTypeId);
            db.AddInParameter(dbCommand, "p_Remark", DbType.String, Paper.Remark);
            db.AddInParameter(dbCommand, "p_Subject_Name", DbType.String, Paper.SubjectName);
            db.AddInParameter(dbCommand, "p_Total_Score", DbType.Decimal, Paper.TotalScore);
            db.AddInParameter(dbCommand, "p_Unit_Score", DbType.Decimal, Paper.UnitScore);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, Paper.Memo);

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

        public void DeletePaperStrategySubject(int PaperStrategyId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_PAPER_STRATEGY_SUBJECT_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_PAPER_STRATEGY_SUBJECT_ID", DbType.Int32, PaperStrategyId);

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



        public static PaperStrategySubject SubjectNamelObject(IDataReader dataReader)
        {



            return new PaperStrategySubject(
                DataConvert.ToInt(dataReader[GetMappingFieldName("PaperStrategyId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("PaperStrategySubjectId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrderIndex")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("SubjectName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ItemTypeId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("TypeName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Remark")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("TotalScore")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ItemCount")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("UnitScore")]));

        }
    }
}
