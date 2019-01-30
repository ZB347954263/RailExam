using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
    public class SynchronizeLogDAL
    {
         private static Hashtable _ormTable;
		private int _recordCount = 0;

        static SynchronizeLogDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("synchronizelogid", "SYNCHRONIZE_LOG_ID");
            _ormTable.Add("orgid", "ORG_ID");
            _ormTable.Add("orgname", "ORG_NAME");
            _ormTable.Add("synchronizetypeid","SYNCHRONIZE_TYPE_ID");
            _ormTable.Add("synchronizecontent", "SYNCHRONIZE_CONTENT");
            _ormTable.Add("begintime", "BEGIN_TIME");
            _ormTable.Add("endtime", "END_TIME");
            _ormTable.Add("synchronizestatusid", "SYNCHRONIZE_STATUS_ID");
            _ormTable.Add("statuscontent", "STATUS_CONTENT");
        }

        public IList<SynchronizeLog> GetSynchronizeLogByOrgID(int orgID)
        {
            IList<SynchronizeLog> systemlog = new List<SynchronizeLog>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYNCHRONIZE_LOG_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    SynchronizeLog obj = CreateModelObject(dataReader);

                    systemlog.Add(obj);
                }
            }


            return systemlog;
        }


		public IList<SynchronizeLog> GetSynchronizeLogByOrgIDAndTypeID(int orgID,int typeID)
		{
			IList<SynchronizeLog> systemlog = new List<SynchronizeLog>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_SYNCHRONIZE_LOG_G_Upload";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
			db.AddInParameter(dbCommand, "p_type_id", DbType.Int32, typeID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					SynchronizeLog obj = CreateModelObject(dataReader);
					systemlog.Add(obj);
				}
			}


			return systemlog;
		}

        public void DeleteSynchronizeLog(int orgID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYNCHRONIZE_LOG_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_log_id", DbType.Int32, orgID);

            db.ExecuteNonQuery(dbCommand);
        }

         public void UpdateSynchronizeLog(SynchronizeLog obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYNCHRONIZE_LOG_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_log_id", DbType.Int32, obj.SynchronizeLogID);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, obj.OrgID);
            db.AddInParameter(dbCommand, "p_type_id", DbType.Int32, obj.SynchronizeTypeID);
            db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, obj.BeginTime);
            db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, obj.EndTime);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, obj.SynchronizeStatusID);

            db.ExecuteNonQuery(dbCommand);
        }

        public void WriteLog(SynchronizeLog obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_SYNCHRONIZE_LOG_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_log_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, obj.OrgID);
            db.AddInParameter(dbCommand, "p_type_id", DbType.Int32, obj.SynchronizeTypeID);
            db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, obj.BeginTime);
            db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, obj.EndTime);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, obj.SynchronizeStatusID);

            db.ExecuteNonQuery(dbCommand);
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

        public static SynchronizeLog CreateModelObject(IDataReader dataReader)
        {
            return new SynchronizeLog(
                DataConvert.ToInt(dataReader[GetMappingFieldName("SynchronizeLogID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrgID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("OrgName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("SynchronizeTypeID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("SynchronizeContent")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("BeginTime")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("EndTime")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("SynchronizeStatusID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("StatusContent")]));
        }
    }
}
