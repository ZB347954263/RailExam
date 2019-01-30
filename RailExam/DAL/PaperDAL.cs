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
    public class PaperDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static PaperDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("categoryid", "CATEGORY_ID");
            _ormTable.Add("paperid", "PAPER_ID");
            _ormTable.Add("orgid", "ORG_ID");
            _ormTable.Add("createmode", "CREATE_MODE");
            _ormTable.Add("strategyid", "STRATEGY_ID");
            _ormTable.Add("categoryname", "CATEGORY_NAME");
            _ormTable.Add("papername", "PAPER_NAME");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("memo", "MEMO");
            _ormTable.Add("totalscore", "TOTAL_SCORE");
            _ormTable.Add("itemcount", "ITEM_COUNT");
            _ormTable.Add("usedcount", "USED_COUNT");
            _ormTable.Add("statusid", "STATUS_ID");
            _ormTable.Add("createperson", "CREATE_PERSON");
            _ormTable.Add("createtime", "CREATE_TIME");
            _ormTable.Add("asc", "ASC");
            _ormTable.Add("desc", "DESC");

            #region // Extended Fields

            _ormTable.Add("organizationname", "ORGANIZATION_NAME");
            _ormTable.Add("statusname", "STATUS_NAME");
            _ormTable.Add("tasktraintypename", "TASK_TRAIN_TYPE_NAME");
            _ormTable.Add("currentitemcount", "Current_Item_Count");
            _ormTable.Add("currenttotalscore", "Current_Total_Score");           

            #endregion // End of Extended Fields
        }

        public Paper GetPaper(int PaperId)
        {
            Paper paper = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Paper_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
            db.AddInParameter(dbCommand, "p_Paper_id", DbType.Int32, PaperId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    paper = CreateModelObject(dataReader);
                }
            }

            return paper;
        }

        public IList<Paper> GetPaperByCategoryID(int CategoryId)
        {
            IList<Paper> papers = new List<Paper>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Paper_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Category_id", DbType.Int32, CategoryId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Paper paper = CreateModelObject(dataReader);
                    papers.Add(paper);
                }
            }

            return papers;
        }

        public IList<Paper> GetTopPapers()
        {

            IList<Paper> papers = new List<Paper>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Paper_Top";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

           
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Paper paper = CreateModelObject(dataReader);
                    papers.Add(paper);
                }
            }

            return papers;
        }



        public IList<Paper> GetPaperByPaperId(int PaperId)
        {
            IList<Paper> papers = new List<Paper>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Paper_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Paper_id", DbType.Int32, PaperId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Paper paper = CreateModelObject(dataReader);
                    paper.CurrentItemCount = Convert.ToInt32(dataReader[GetMappingFieldName("CurrentItemCount")]);
                    paper.CurrentTotalScore = Convert.ToDecimal(dataReader[GetMappingFieldName("CurrentTotalScore")]);
                    papers.Add(paper);
                }
            }

            return papers;
        }

        public IList<Paper> GetPaperByCategoryID(int CategoryID, int CreateMode, string PaperName, string CreatePerson)
        {
            IList<Paper> papers = new List<Paper>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Paper_F";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Category_id", DbType.Int32, CategoryID);
            db.AddInParameter(dbCommand, "p_Create_Mode", DbType.Int32, CreateMode);
            db.AddInParameter(dbCommand, "p_Paper_name", DbType.String, PaperName);
            db.AddInParameter(dbCommand, "p_Create_Person", DbType.String, CreatePerson);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Paper paper = CreateModelObject(dataReader);
                    papers.Add(paper);
                }
            }

            return papers;
        }

        public IList<Paper> GetPaperByCategoryIDPath(string idPath, int createMode, string paperName, string createPerson, int OrgId)
        {
            IList<Paper> papers = new List<Paper>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Paper_Q1";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_id_path", DbType.String, idPath);
            db.AddInParameter(dbCommand, "p_create_mode", DbType.Int32, createMode);
            db.AddInParameter(dbCommand, "p_paper_name", DbType.String, paperName);
            db.AddInParameter(dbCommand, "p_create_person", DbType.String, createPerson);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, OrgId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Paper paper = CreateModelObject(dataReader);

                    paper.CurrentItemCount = Convert.ToInt32(dataReader[GetMappingFieldName("CurrentItemCount")]);
                    paper.CurrentTotalScore = Convert.ToDecimal(dataReader[GetMappingFieldName("CurrentTotalScore")]);                   

                    papers.Add(paper);
                }
            }

            return papers;
        }



        public IList<Paper> GetTaskPapers(string paperName, int organizationId, int categoryId, string createPerson, 
            int statusId, int currentPageIndex, int pageSize, string orderBy)
        {
            IList<Paper> papers = new List<Paper>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_PAPER_TASK_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_paper_name", DbType.String, paperName);
            db.AddInParameter(dbCommand, "p_organization_id", DbType.Int32, organizationId);
            db.AddInParameter(dbCommand, "p_category_id", DbType.Int32, categoryId);
            db.AddInParameter(dbCommand, "p_create_person", DbType.String, createPerson);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, statusId);
            db.AddInParameter(dbCommand, "p_current_page_index", DbType.Int32, currentPageIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, pageSize);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                Paper paper = null;
                while (dataReader.Read())
                {
                    paper = CreateModelObject(dataReader);
                    paper.OrganizationName = Convert.ToString(dataReader[GetMappingFieldName("OrganizationName")]);
                    paper.StatusName = Convert.ToString(dataReader[GetMappingFieldName("StatusName")]);
                    paper.TaskTrainTypeName = Convert.ToString(dataReader[GetMappingFieldName("TaskTrainTypeName")]);

                    papers.Add(paper);
                }
            }

            _recordCount = (int)db.GetParameterValue(dbCommand, "p_count");

            return papers;
        }

        public int AddPaper(Paper paper)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Paper_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_Paper_id", DbType.Int32, paper.PaperId);
            db.AddInParameter(dbCommand, "p_Category_id", DbType.Int32, paper.CategoryId);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, paper.OrgId);
            db.AddInParameter(dbCommand, "p_Item_Count", DbType.Int32, paper.ItemCount);
            db.AddInParameter(dbCommand, "p_Status_Id", DbType.Int32, paper.StatusId);
            db.AddInParameter(dbCommand, "p_Paper_name", DbType.String, paper.PaperName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, paper.Description);
            db.AddInParameter(dbCommand, "p_Create_Mode", DbType.Int32, paper.CreateMode);
            db.AddInParameter(dbCommand, "p_Total_Score", DbType.String, paper.TotalScore);
            db.AddInParameter(dbCommand, "p_Create_Person", DbType.String, paper.CreatePerson);
            //db.AddInParameter(dbCommand, "p_Create_Time", DbType.DateTime, DateTime.Now);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, paper.Memo);

            int id = 0;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_Paper_id"));

                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;

            }
            connection.Close();

            return id;
        }

        public void UpdatePaper(Paper paper)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Paper_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Paper_id", DbType.Int32, paper.PaperId);
            db.AddInParameter(dbCommand, "p_Paper_name", DbType.String, paper.PaperName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, paper.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, paper.Memo);

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

        public void DeletePaper(int PaperId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Paper_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Paper_id", DbType.Int32, PaperId);

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
                orderByCondition = System.Text.RegularExpressions.Regex.Replace(s.ToLower(), "\\s+asc$", ",asc");
                orderByCondition = System.Text.RegularExpressions.Regex.Replace(orderByCondition, "\\s+desc$", ",desc");
                orderByCondition = orderByCondition.Trim();

                string[] orderBysOfOneCondition = orderByCondition.Split(new char[] { ',' });

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

        public static Paper CreateModelObject(IDataReader dataReader)
        {
            return new Paper(
                DataConvert.ToInt(dataReader[GetMappingFieldName("PaperId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrgId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("CategoryId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("PaperName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("CreateMode")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("StrategyId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("CategoryName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("TotalScore")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ItemCount")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("UsedCount")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("StatusID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("createperson")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("createtime")]));
        }
    }
}

