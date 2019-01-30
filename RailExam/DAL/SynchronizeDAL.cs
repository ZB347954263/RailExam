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
    public class SynchronizeDAL
    {
        private static Hashtable _ormTable;
		private int _recordCount = 0;

        static SynchronizeDAL()
		{
			_ormTable = new Hashtable();

			_ormTable.Add("detectinterval", "DETECT_INTERVAL");
			_ormTable.Add("retrycount", "RETRY_COUNT");
		}

        public void UpdateSynchronize(Synchronize obj)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Synchronize_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_detect_interval", DbType.Int32, obj.DetectInterval);
            db.AddInParameter(dbCommand, "p_retry_count", DbType.Int32, obj.RetryCount);

            db.ExecuteNonQuery(dbCommand);
        }

        public Synchronize GetSynchronize()
        {
            Synchronize obj = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Synchronize_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    obj = CreateModelObject(dataReader);
                }
            }

            return obj;
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

        public static Synchronize CreateModelObject(IDataReader dataReader)
        {
            return new Synchronize(
                DataConvert.ToInt(dataReader[GetMappingFieldName("DetectInterval")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("RetryCount")]));
        }
    }
}
