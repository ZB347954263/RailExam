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
    public class ExamTypeDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static ExamTypeDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("examtypeid", "Exam_Type_Id");
            _ormTable.Add("isdefault", "Is_Default");
            _ormTable.Add("typename", "Type_Name");
            _ormTable.Add("description", "description");
            _ormTable.Add("memo", "memo");
        }

        /// <summary>
        /// 获取所有考试类型
        /// </summary>
        /// <returns></returns>
        public IList<ExamType> GetExamTypes()
        {
            IList<ExamType> itemTypes = new List<ExamType>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_exam_TYPE_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    itemTypes.Add(CreateModelObject(dataReader));
                }
            }

            return itemTypes;
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

        public static ExamType CreateModelObject(IDataReader dataReader)
        {
            return new ExamType(
                DataConvert.ToInt(dataReader[GetMappingFieldName("examtypeid")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("isdefault")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("typename")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("description")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("memo")])
                );
        }

    }
}
