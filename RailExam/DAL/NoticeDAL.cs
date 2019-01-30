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
    public class NoticeDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static NoticeDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("noticeid", "NOTICE_ID");
            _ormTable.Add("title", "TITLE");
            _ormTable.Add("content", "CONTENT");
            _ormTable.Add("importanceid", "IMPORTANCE_ID");
            _ormTable.Add("importancename", "IMPORTANCE_NAME");
            _ormTable.Add("daycount", "DAY_COUNT");
            _ormTable.Add("createpersonid", "CREATE_PERSON_ID");
            _ormTable.Add("employeename", "EMPLOYEE_NAME");
            _ormTable.Add("orgname", "ORG_NAME");
            _ormTable.Add("createtime", "CREATE_TIME");
            _ormTable.Add("receiveorgids", "RECEIVE_ORG_IDS");
            _ormTable.Add("receiveemployeeids", "RECEIVE_EMPLOYEE_IDS");
            _ormTable.Add("memo", "MEMO");
        }

        public IList<Notice> GetNotices(int noticeID, string title, string content, int importanceID, string importanceName,
            int dayCount, int createPersonID, string employeeName, string orgName, DateTime createTime, string receiveOrgIDS, 
            string receiveEmployeeIDS, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<Notice> notices = new List<Notice>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_NOTICE_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Notice notice = CreateModelObject(dataReader);

                    notices.Add(notice);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return notices;
        }

        public IList<Notice> GetNotices(string title, int importanceID, string OrgName, string EmployeeName, 
            DateTime beginTime, DateTime endTime,bool isAdmin, Int32 empolyeeID) 
        {
            IList<Notice> notices = new List<Notice>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_NOTICE_F";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_title", DbType.String, title);
            db.AddInParameter(dbCommand, "p_importance_id", DbType.Int32, importanceID);
            db.AddInParameter(dbCommand, "p_org_name", DbType.String, OrgName);
            db.AddInParameter(dbCommand, "p_employee_name", DbType.String, EmployeeName);
            db.AddInParameter(dbCommand, "p_begin_time", DbType.Date, beginTime);
            db.AddInParameter(dbCommand, "p_end_time", DbType.Date, endTime);
            db.AddInParameter(dbCommand, "p_is_admin", DbType.Boolean, isAdmin);
            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, empolyeeID);



            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Notice notice = CreateModelObject(dataReader);

                    notices.Add(notice);
                }
            }

            return notices;
        }

        public IList<Notice> GetNotices(int nNum)
        {
            IList<Notice> notices = new List<Notice>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_NOTICE_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_number", DbType.Int32, nNum);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy("noticeid"));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Notice notice = CreateModelObject(dataReader);

                    notices.Add(notice);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return notices;
        }

        public IList<Notice> GetNotices(int nNum, string examineeId, string OrgId)
        {
            IList<Notice> notices = new List<Notice>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_NOTICE_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            if(examineeId=="")
            {
                examineeId = "-1";
            }

            db.AddInParameter(dbCommand, "p_number", DbType.Int32, nNum);
            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, int.Parse(examineeId));
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, int.Parse(OrgId));

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Notice notice = CreateModelObject(dataReader);

                    notices.Add(notice);
                }
            }

            return notices;
        }




        public IList<Notice> GetNotices1()
        {
            IList<Notice> notices = new List<Notice>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_NOTICE_S1";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Notice notice = CreateModelObject(dataReader);

                    notices.Add(notice);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return notices;
        }

        public Notice GetNotice(int noticeID)
        {
            Notice notice;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_NOTICE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_notice_id", DbType.Int32, noticeID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    notice = CreateModelObject(dataReader);
                }
                else
                {
                    notice = new Notice();
                }
            }

            return notice;
        }

        public int AddNotice(Notice notice, int nEmployeeID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_NOTICE_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_notice_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_title", DbType.String, notice.Title);
            db.AddInParameter(dbCommand, "p_content", DbType.String, notice.Content);
            db.AddInParameter(dbCommand, "p_importance_id", DbType.Int32, notice.ImportanceID);
            db.AddInParameter(dbCommand, "p_day_count", DbType.Int32, notice.DayCount);
            db.AddInParameter(dbCommand, "p_create_person_id", DbType.Int32, nEmployeeID);
            //db.AddInParameter(dbCommand, "p_create_time", DbType.Date, notice.CreateTime);
            db.AddInParameter(dbCommand, "p_receive_org_ids", DbType.String, notice.ReceiveOrgIDS);
            db.AddInParameter(dbCommand, "p_receive_employee_ids", DbType.String, notice.ReceiveEmployeeIDS);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, notice.Memo);

            db.ExecuteNonQuery(dbCommand);

            return Convert.ToInt32(db.GetParameterValue(dbCommand, "p_notice_id"));
        }

        public bool UpdateNotice(Notice notice)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_NOTICE_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_notice_id", DbType.Int32, notice.NoticeID);
            db.AddInParameter(dbCommand, "p_title", DbType.String, notice.Title);
            db.AddInParameter(dbCommand, "p_content", DbType.String, notice.Content);
            db.AddInParameter(dbCommand, "p_importance_id", DbType.Int32, notice.ImportanceID);
            db.AddInParameter(dbCommand, "p_day_count", DbType.Int32, notice.DayCount);
            db.AddInParameter(dbCommand, "p_receive_org_ids", DbType.String, notice.ReceiveOrgIDS);
            db.AddInParameter(dbCommand, "p_receive_employee_ids", DbType.String, notice.ReceiveEmployeeIDS);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, notice.Memo);

            if (db.ExecuteNonQuery(dbCommand) > 0)
                return true;
            else
                return false;
        }

        public bool DeleteNotice(int noticeID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_NOTICE_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_notice_id", DbType.Int32, noticeID);

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

        public static Notice CreateModelObject(IDataReader dataReader)
        {
            return new Notice(
                DataConvert.ToInt(dataReader[GetMappingFieldName("NoticeID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Title")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Content")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ImportanceID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ImportanceName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("DayCount")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("CreatePersonID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("EmployeeName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("OrgName")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("CreateTime")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ReceiveOrgIDS")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ReceiveEmployeeIDS")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }
    }
}
