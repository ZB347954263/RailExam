using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;
using DSunSoft.Data;

namespace RailExam.DAL
{
    public class TaskJudgeStatusDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        #region // Helper methods

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

        public static TaskJudgeStatus CreateModelObject(IDataReader dataReader)
        {
            return new TaskJudgeStatus(
                DataConvert.ToInt(dataReader[GetMappingFieldName("TaskJudgeStatusId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("StatusName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("IsDefault")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("ScoreRate")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }

        #endregion // End of helper methods

        #region // Ctors

        static TaskJudgeStatusDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("taskjudgestatusid", "TASK_JUDGE_STATUS_ID");
            _ormTable.Add("statusname", "STATUS_NAME");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("isdefault", "IS_DEFAULT");
            _ormTable.Add("scorerate", "SCORE_RATE");
            _ormTable.Add("memo", "MEMO");
            _ormTable.Add("asc", "ASC");
            _ormTable.Add("desc", "DESC");
        }

        #endregion // End of ctors

        #region // DAL

        /// <summary>
        /// 查询所有作业评分状态
        /// </summary>
        /// <returns>所有作业评分状态</returns>
        public IList<TaskJudgeStatus> GetTaskJudgeStatuses()
        {
            IList<TaskJudgeStatus> taskJudgeStatuses = new List<TaskJudgeStatus>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_GET_ALL");

            db.AddInParameter(dbCommand, "p_table_name", DbType.String, "TASK_JUDGE_STATUS");

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    taskJudgeStatuses.Add(CreateModelObject(dataReader));
                }
            }

            _recordCount = taskJudgeStatuses.Count;

            return taskJudgeStatuses;
        }

        public int RecordCount
        {
            get { return _recordCount; }
        }

        #endregion // End of DAL
    }
}
