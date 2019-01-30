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
    public class PaperSubjectDAL
    {

        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static PaperSubjectDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("papersubjectid", "Paper_Subject_Id");
            _ormTable.Add("paperid", "Paper_Id");
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
        public PaperSubject GetPaperSubject(int PaperSubjectId)
        {
            PaperSubject Paper;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Paper_Subject_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "p_Paper_Subject_id", DbType.Int32, PaperSubjectId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    Paper = SubjectNamelObject(dataReader);
                }
                else
                {
                    Paper = new PaperSubject();
                }
            }



            return Paper;
        }


        public IList<PaperSubject> GetPaperSubjectByPaperId(int PaperId)
        {
            IList<PaperSubject> Papers = new List<PaperSubject>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_PAPER_SUBJECT_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_PAPER_id", DbType.Int32, PaperId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    PaperSubject Paper = SubjectNamelObject(dataReader);
                    Papers.Add(Paper);
                }
            }

            return Papers;
        }

        public IList<PaperSubject> GetPaperSubjectByPaperIdByOrgID(int PaperId,int orgID)
        {
            IList<PaperSubject> Papers = new List<PaperSubject>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_PAPER_SUBJECT_Q_ORG";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_PAPER_id", DbType.Int32, PaperId);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
            db.AddOutParameter(dbCommand, "p_net_name", DbType.String, 50);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    PaperSubject Paper = SubjectNamelObject(dataReader);
                    Papers.Add(Paper);
                }
            }
            return Papers;
        }

        public void UpdatePaperSubject(int PaperId  , IList<PaperSubject> PaperSubjects)
        {

            Database db = DatabaseFactory.CreateDatabase();
           

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
           

                foreach (PaperSubject ps in PaperSubjects)
                {
                    string sqlCommand1 = "USP_PAPER_SUBJECT_Update_name";
                    DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand1);

                    db.AddInParameter(dbCommand1, "p_Paper_Subject_Id", DbType.Int32, ps.PaperSubjectId);               
                    db.AddInParameter(dbCommand1, "p_Subject_Name", DbType.String, ps.SubjectName);
                    db.AddInParameter(dbCommand1, "p_Unit_Score", DbType.Decimal, ps.UnitScore);
                    db.AddInParameter(dbCommand1, "p_item_count", DbType.Decimal, ps.ItemCount);
                    db.ExecuteNonQuery(dbCommand1, transaction);

                }

                string sqlCommand2 = "USP_paper_Update_score";
                DbCommand dbCommand2 = db.GetStoredProcCommand(sqlCommand2);
                db.AddInParameter(dbCommand2, "p_paper_Id", DbType.Int32, PaperId);
                db.ExecuteNonQuery(dbCommand2, transaction);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            connection.Close();



        }




        public void UpdatePaperSubject(PaperSubject Paper)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_PAPER_SUBJECT_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_PAPER_ID", DbType.Int32, Paper.PaperId);
            db.AddInParameter(dbCommand, "p_Paper_Subject_Id", DbType.Int32, Paper.PaperSubjectId);
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


        public int AddPaperSubject(PaperSubject Paper)
        {
            int i = 0;
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_PAPER_SUBJECT_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_PAPER_ID", DbType.Int32, Paper.PaperId);
            db.AddOutParameter(dbCommand, "p_Paper_Subject_Id", DbType.Int32, Paper.PaperSubjectId);
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

                i = (int)db.GetParameterValue(dbCommand, "p_Paper_Subject_Id");
                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;

            }
            connection.Close();
            return i;
        }

        public void DeletePaperSubject(int PaperId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_PAPER_SUBJECT_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_PAPER_SUBJECT_ID", DbType.Int32, PaperId);

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



        public static PaperSubject SubjectNamelObject(IDataReader dataReader)
        {



            return new PaperSubject(
                DataConvert.ToInt(dataReader[GetMappingFieldName("PaperId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("PaperSubjectId")]),
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
