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
    public class BulletinDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static BulletinDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("bulletinid", "BULLETIN_ID");
            _ormTable.Add("title", "TITLE");
            _ormTable.Add("content", "CONTENT");
            _ormTable.Add("importanceid", "IMPORTANCE_ID");
            _ormTable.Add("importancename", "IMPORTANCE_NAME");
            _ormTable.Add("daycount", "DAY_COUNT");
            _ormTable.Add("createpersonid", "CREATE_PERSON_ID");
            _ormTable.Add("employeename", "EMPLOYEE_NAME");
            _ormTable.Add("orgname", "ORG_NAME");
            _ormTable.Add("createtime", "CREATE_TIME");
            _ormTable.Add("memo", "MEMO");
        }

        public IList<Bulletin> GetBulletins(int bulletinID, string title, string content, int importanceID, string importanceName,
            int dayCount, int createPersonID, string employeeName, string orgName, DateTime createTime, string memo,
            int startRowIndex, int maximumRows, string orderBy)
        {
            IList<Bulletin> bulletins = new List<Bulletin>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BULLETIN_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Bulletin bulletin = CreateModelObject(dataReader);

                    bulletins.Add(bulletin);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return bulletins;
        }

        public IList<Bulletin> GetBulletins(string title, int importanceID, string OrgName, string EmployeeName,
            DateTime beginTime, DateTime endTime, bool IsAdmin, int EmployeeID)
        {
            IList<Bulletin> bulletins = new List<Bulletin>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BULLETIN_F";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_title", DbType.String, title);
            db.AddInParameter(dbCommand, "p_importance_id", DbType.Int32, importanceID);
            db.AddInParameter(dbCommand, "p_org_name", DbType.String, OrgName);
            db.AddInParameter(dbCommand, "p_employee_name", DbType.String, EmployeeName);
            db.AddInParameter(dbCommand, "p_begin_time", DbType.Date, beginTime);
            db.AddInParameter(dbCommand, "p_end_time", DbType.Date, endTime);

            db.AddInParameter(dbCommand, "p_is_admin", DbType.Boolean, IsAdmin);
            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, EmployeeID);

            

            using(IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while(dataReader.Read())
                {
                    Bulletin bulletin = CreateModelObject(dataReader);

                    bulletins.Add(bulletin);
                }
            }

            return bulletins;
        }

        public IList<Bulletin> GetBulletins(int nNum)
        {
            IList<Bulletin> bulletins = new List<Bulletin>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BULLETIN_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_number", DbType.Int32, nNum);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy("bulletinid"));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Bulletin bulletin = CreateModelObject(dataReader);

                    bulletins.Add(bulletin);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return bulletins;
        }

        public IList<Bulletin> GetBulletins1()
        {
            IList<Bulletin> bulletins = new List<Bulletin>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BULLETIN_S1";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Bulletin bulletin = CreateModelObject(dataReader);

                    bulletins.Add(bulletin);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return bulletins;
        }

        public Bulletin GetBulletin(int bulletinID)
        {
            Bulletin bulletin = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BULLETIN_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_bulletin_id", DbType.Int32, bulletinID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    bulletin = CreateModelObject(dataReader);
                }
            }

            return bulletin;
        }

        public int AddBulletin(Bulletin bulletin, int nEmployeeID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BULLETIN_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_bulletin_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_title", DbType.String, bulletin.Title);
            db.AddInParameter(dbCommand, "p_content", DbType.String, bulletin.Content);
            db.AddInParameter(dbCommand, "p_importance_id", DbType.Int32, bulletin.ImportanceID);
            db.AddInParameter(dbCommand, "p_day_count", DbType.Int32, bulletin.DayCount);
            db.AddInParameter(dbCommand, "p_create_person_id", DbType.Int32, nEmployeeID);
            //db.AddInParameter(dbCommand, "p_create_time", DbType.Date, bulletin.CreateTime);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, bulletin.Memo);

            db.ExecuteNonQuery(dbCommand);

            return Convert.ToInt32(db.GetParameterValue(dbCommand, "p_bulletin_id"));
        }

        public bool UpdateBulletin(Bulletin bulletin)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BULLETIN_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_bulletin_id", DbType.Int32, bulletin.BulletinID);
            db.AddInParameter(dbCommand, "p_title", DbType.String, bulletin.Title);
            db.AddInParameter(dbCommand, "p_content", DbType.String, bulletin.Content);
            db.AddInParameter(dbCommand, "p_importance_id", DbType.Int32, bulletin.ImportanceID);
            db.AddInParameter(dbCommand, "p_day_count", DbType.Int32, bulletin.DayCount);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, bulletin.Memo);

            if (db.ExecuteNonQuery(dbCommand) > 0)
                return true;
            else
                return false;
        }

        public bool DeleteBulletin(int bulletinID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_BULLETIN_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_bulletin_id", DbType.Int32, bulletinID);

            if (db.ExecuteNonQuery(dbCommand) > 0)
                return true;
            else
                return false;
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

        public static Bulletin CreateModelObject(IDataReader dataReader)
        {
            return new Bulletin(
                DataConvert.ToInt(dataReader[GetMappingFieldName("BulletinID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Title")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Content")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ImportanceID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ImportanceName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("DayCount")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("CreatePersonID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("EmployeeName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("OrgName")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("CreateTime")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }
    }
}
