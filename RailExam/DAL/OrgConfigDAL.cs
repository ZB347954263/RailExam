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
    public class OrgConfigDAL
    {
        private static Hashtable _ormTable;
		private int _recordCount = 0;

        static OrgConfigDAL()
		{
			_ormTable = new Hashtable();

			_ormTable.Add("orgid", "ORG_ID");
			_ormTable.Add("hour", "HOUR");
            _ormTable.Add("serverno","SERVER_NO");
		}


        public void AddOrgConfig(OrgConfig orgconfig)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ORG_CONFIG_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgconfig.OrgID);
            db.AddInParameter(dbCommand, "p_hour", DbType.Int32, orgconfig.Hour);

            db.ExecuteNonQuery(dbCommand);
        }


        public void UpdateOrgConfig(OrgConfig orgconfig)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ORG_CONFIG_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgconfig.OrgID);
            db.AddInParameter(dbCommand, "p_hour", DbType.Int32, orgconfig.Hour);

            db.ExecuteNonQuery(dbCommand);
        }

        public OrgConfig GetOrgConfig()
        {
            OrgConfig obj = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ORG_CONFIG_G";
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

        public static OrgConfig CreateModelObject(IDataReader dataReader)
        {
            return new OrgConfig(
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrgID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("Hour")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ServerNo")]));
        }
    }
}
