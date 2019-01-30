using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;
using DSunSoft.Data;

namespace RailExam.DAL
{
    public class ExamResultStatusDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        /// <summary>
        /// 查询结果记录数
        /// </summary>
        public int RecordCount
        {
            get { return _recordCount; }
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
                        mappingOrderBy += GetMappingFieldName(orderBysOfOneCondition[0]) + ' ' +
                                          orderBysOfOneCondition[1];
                    }
                }
            }

            return mappingOrderBy;
        }

        public static ExamResultStatus CreateModelObject(IDataReader dataReader)
        {
            return new ExamResultStatus(
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamResultStatusId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("StatusName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("IsDefault")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }

        /// <summary>
        /// 空参数构造函数
        /// </summary>
        public ExamResultStatusDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("examresultstatusid", "EXAM_RESULT_STATUS_ID");
            _ormTable.Add("statusname", "STATUS_NAME");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("isdefault", "IS_DEFAULT");
            _ormTable.Add("memo", "MEMO");
        }

        /// <summary>
        /// 获取所有考试考生结果状态
        /// </summary>
        /// <returns></returns>
        public IList<ExamResultStatus> GetExamResultStatuses()
        {
            IList<ExamResultStatus> examResultStatuss = new List<ExamResultStatus>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_RESULT_STATUS_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    examResultStatuss.Add(CreateModelObject(dataReader));
                }
            }

            return examResultStatuss;
        }
    }
}
