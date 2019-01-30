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
    public class PostImportDAL
    {
        private static Hashtable _ormTable;
		private int _recordCount = 0;

        static PostImportDAL()
		{
			_ormTable = new Hashtable();

			_ormTable.Add("postid", "POST_ID");
			_ormTable.Add("postnamepath", "POSTNAMEPATH");
		}


        public IList<PostImport> GetPostImport()
        {
            IList<PostImport> objList = new List<PostImport>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_POST_IMPORT_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);


            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    PostImport obj = CreateModelObject(dataReader);

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

        public static PostImport CreateModelObject(IDataReader dataReader)
        {
            return new PostImport(
                DataConvert.ToInt(dataReader[GetMappingFieldName("PostID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("PostNamePath")]));
        }
    }
}
