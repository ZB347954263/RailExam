using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
    public class ExamJudgeStatusDAL
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

        public static ExamJudgeStatus CreateModelObject(IDataReader dataReader)
        {
            return new ExamJudgeStatus(
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamJudgeStatusId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("StatusName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("IsDefault")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("ScoreRate")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }

        /// <summary>
        /// 空参数构造函数
        /// </summary>
        static ExamJudgeStatusDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("examjudgestatusid", "EXAM_JUDGE_STATUS_ID");
            _ormTable.Add("statusname", "STATUS_NAME");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("isdefault", "IS_DEFAULT");
            _ormTable.Add("scorerate", "SCORE_RATE");
            _ormTable.Add("memo", "MEMO");
        }

        /// <summary>
        /// 新增考试评分状态
        /// </summary>
        /// <param name="examJudgeStatus">考试评分状态</param>
        /// <returns>数据库受影响的行数</returns>
        public int AddExamJudgeStatus(ExamJudgeStatus examJudgeStatus)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_JUDGE_STATUS_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_exam_judge_status_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_status_name", DbType.String, examJudgeStatus.StatusName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, examJudgeStatus.Description);
            db.AddInParameter(dbCommand, "p_is_default", DbType.Int32, examJudgeStatus.IsDefault);
            db.AddInParameter(dbCommand, "p_score_rate", DbType.Decimal, examJudgeStatus.ScoreRate);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, examJudgeStatus.Memo);

            int nRecordAffected = db.ExecuteNonQuery(dbCommand);
            examJudgeStatus.ExamJudgeStatusId = (int)db.GetParameterValue(dbCommand, "p_exam_judge_status_id");

            return nRecordAffected;
        }

        /// <summary>
        /// 删除考试评分状态
        /// </summary>
        /// <param name="examJudgeStatusId">考试评分状态ID</param>
        /// <returns>数据库受影响的行数</returns>
        public int DeleteExamJudgeStatus(int examJudgeStatusId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_JUDGE_STATUS_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_exam_judge_status_id", DbType.Int32, examJudgeStatusId);

            return db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 修改考试评分状态
        /// </summary>
        /// <param name="examJudgeStatus">考试评分状态</param>
        /// <returns>数据库受影响的行数</returns>
        public int UpdateExamJudgeStatus(ExamJudgeStatus examJudgeStatus)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_JUDGE_STATUS_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_exam_judge_status_id", DbType.Int32, examJudgeStatus.ExamJudgeStatusId);
            db.AddInParameter(dbCommand, "p_status_name", DbType.String, examJudgeStatus.StatusName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, examJudgeStatus.Description);
            db.AddInParameter(dbCommand, "p_is_default", DbType.Int32, examJudgeStatus.IsDefault);
            db.AddInParameter(dbCommand, "p_score_rate", DbType.Decimal, examJudgeStatus.ScoreRate);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, examJudgeStatus.Memo);

            return db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 按考试评分状态ID取考试评分状态
        /// </summary>
        /// <param name="examJudgeStatusId">考试评分状态ID</param>
        /// <returns>考试评分状态</returns>
        public ExamJudgeStatus GetExamJudgeStatus(int examJudgeStatusId)
        {
            ExamJudgeStatus examJudgeStatus = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_JUDGE_STATUS_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_exam_judge_status_id", DbType.Int32, examJudgeStatusId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    examJudgeStatus = CreateModelObject(dataReader);
                    break;
                }
            }

            return examJudgeStatus;
            
        }

        /// <summary>
        /// 查询所有考试评分状态
        /// </summary>
        /// <returns>所有考试评分状态</returns>
        public IList<ExamJudgeStatus> GetExamJudgeStatuses()
        {
            IList<ExamJudgeStatus> examJudgeStatuses = new List<ExamJudgeStatus>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_GET_ALL");

            db.AddInParameter(dbCommand, "p_table_name", DbType.String, "EXAM_JUDGE_STATUS");

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    examJudgeStatuses.Add(CreateModelObject(dataReader));
                }
            }

            _recordCount = examJudgeStatuses.Count;

            return examJudgeStatuses;
        }

        /// <summary>
        /// 查询符合条件的考试评分状态
        /// </summary>
        /// <param name="examJudgeStatusId"></param>
        /// <param name="statusName"></param>
        /// <param name="description"></param>
        /// <param name="isDefault"></param>
        /// <param name="memo"></param>
        /// <returns>符合条件的考试评分状态</returns>
        public IList<ExamJudgeStatus> GetExamJudgeStatuses(int examJudgeStatusId, string statusName,
            string description, int isDefault, decimal scoreRate, string memo)
        {
            IList<ExamJudgeStatus> examJudgeStatuses = new List<ExamJudgeStatus>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_JUDGE_STATUS_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_exam_judge_status_id", DbType.Int32, examJudgeStatusId);
            db.AddInParameter(dbCommand, "p_status_name", DbType.String, statusName);
            db.AddInParameter(dbCommand, "p_description", DbType.String, description);
            db.AddInParameter(dbCommand, "p_is_default", DbType.Int32, isDefault);
            db.AddInParameter(dbCommand, "p_score_rate", DbType.Decimal, scoreRate);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, memo);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    examJudgeStatuses.Add(CreateModelObject(dataReader));
                }
            }

            return examJudgeStatuses;
        }
    }
}