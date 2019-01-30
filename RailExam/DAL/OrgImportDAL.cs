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
    public class OrgImportDAL
    {
        private static Hashtable _ormTable;
		private int _recordCount = 0;

        static OrgImportDAL()
		{
			_ormTable = new Hashtable();

			_ormTable.Add("orgid", "ORG_ID");
			_ormTable.Add("orgnamepath", "ORGNAMEPATH");
		}


        public IList<OrgImport> GetOrgImport(int orgID)
        {
            IList<OrgImport> objList = new List<OrgImport>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ORG_IMPORT_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    OrgImport obj = CreateModelObject(dataReader);

                    objList.Add(obj);
                }
            }

            return objList;
        }

		public IList<OrgImport> GetOrgImport(Database db, DbTransaction trans, int orgID)
		{
			IList<OrgImport> objList = new List<OrgImport>();

			string sqlCommand = "USP_ORG_IMPORT_G";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand,trans))
			{
				while (dataReader.Read())
				{
					OrgImport obj = CreateModelObject(dataReader);

					objList.Add(obj);
				}
			}

			return objList;
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

        public static OrgImport CreateModelObject(IDataReader dataReader)
        {
            return new OrgImport(
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrgID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("OrgNamePath")]));
        }
    }
}
