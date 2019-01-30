using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
    public class ExamResultAnswerDAL
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

        public static ExamResultAnswer CreateModelObject(IDataReader dataReader)
        {
            return new ExamResultAnswer(
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamResultId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ExamName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("PaperItemId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("PaperName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Answer")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamTime")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("JudgeScore")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("JudgeStatusId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("JudgeStatusName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("JudgeRemark")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("BeginDateTime")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("EndDateTime")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("JudgeBeginDateTime")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("JudgeEndDateTime")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ExamineeName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("JudgeName")]));
        }

        static ExamResultAnswerDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("examresultid", "EXAM_RESULT_ID");
            _ormTable.Add("examname", "EXAM_NAME");
            _ormTable.Add("paperitemid", "PAPER_ITEM_ID");
            _ormTable.Add("papername", "PAPER_NAME");
            _ormTable.Add("answer", "ANSWER");
            _ormTable.Add("examtime", "EXAM_TIME");
            _ormTable.Add("judgescore", "JUDGE_SCORE");
            _ormTable.Add("judgestatusid", "JUDGE_STATUS_ID");
            _ormTable.Add("judgestatusname", "STATUS_NAME");
            _ormTable.Add("judgeremark", "JUDGE_REMARK");
            _ormTable.Add("begindatetime", "BEGIN_TIME");
            _ormTable.Add("enddatetime", "END_TIME");
            _ormTable.Add("judgebegindatetime", "JUDGE_BEGIN_TIME");
            _ormTable.Add("judgeenddatetime", "JUDGE_END_TIME");
            _ormTable.Add("examineename", "EXAMINEE_NAME");
            _ormTable.Add("judgename", "JUDGE_NAME");
        }

        #region 增
        /// <summary>
        /// 插入考试考生结果答案
        /// </summary>
        /// <param name="examResultAnswer"></param>
        /// <returns></returns>
        public int AddExamResultAnswer(ExamResultAnswer examResultAnswer)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_RESULT_ANSWER_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, examResultAnswer.ExamResultId);
            db.AddInParameter(dbCommand, "p_paper_item_id", DbType.Int32, examResultAnswer.PaperItemId);
            db.AddInParameter(dbCommand, "p_answer", DbType.String, examResultAnswer.Answer);
            db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, examResultAnswer.ExamTime);
            db.AddInParameter(dbCommand, "p_judge_score", DbType.Decimal, examResultAnswer.JudgeScore);
            db.AddInParameter(dbCommand, "p_judge_status_id", DbType.Int32, examResultAnswer.JudgeStatusId);
            db.AddInParameter(dbCommand, "p_judge_remark", DbType.String, examResultAnswer.JudgeRemark);

            int nRecordAffected = db.ExecuteNonQuery(dbCommand);

            return nRecordAffected;
        }
        #endregion 

        #region 删
        /// <summary>
        /// 删除给定考卷的所有考生结果答案
        /// </summary>
        /// <param name="examResultId"></param>
        /// <returns></returns>
        public int DeleteExamResultAnswers(int examResultId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_RESULT_ANSWER_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, examResultId);

            int nRecordAffected = db.ExecuteNonQuery(dbCommand);

            return nRecordAffected;
        }

        /// <summary>
        /// 删除给定考卷、试题编号的考生结果答案
        /// </summary>
        /// <param name="examResultId"></param>
        /// <param name="paperItemId"></param>
        /// <returns></returns>
        public int DeleteExamResultAnswer(int examResultId, int paperItemId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_RESULT_ANSWER_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, examResultId);
            db.AddInParameter(dbCommand, "p_paper_item_id", DbType.Int32, paperItemId);

            int nRecordAffected = db.ExecuteNonQuery(dbCommand);

            return nRecordAffected;
        }
        #endregion

        #region 改
        /// <summary>
        /// 更新考试考生结果答案
        /// </summary>
        /// <param name="examResultAnswer"></param>
        /// <returns></returns>
        public int UpdateExamResultAnswer(ExamResultAnswer examResultAnswer)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_RESULT_ANSWER_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, examResultAnswer.ExamResultId);
            db.AddInParameter(dbCommand, "p_paper_item_id", DbType.Int32, examResultAnswer.PaperItemId);
            db.AddInParameter(dbCommand, "p_answer", DbType.String, examResultAnswer.Answer);
            db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, examResultAnswer.ExamTime);
            db.AddInParameter(dbCommand, "p_judge_score", DbType.Decimal, examResultAnswer.JudgeScore);
            db.AddInParameter(dbCommand, "p_judge_status_id", DbType.Int32, examResultAnswer.JudgeStatusId);
            db.AddInParameter(dbCommand, "p_judge_remark", DbType.String, examResultAnswer.JudgeRemark);

            int nRecordAffected = db.ExecuteNonQuery(dbCommand);

            return nRecordAffected;
        }
        #endregion

        #region 查
        /// <summary>
        /// 查询给定考卷、试题编号的考生结果答案
        /// </summary>
        /// <param name="examResultId"></param>
        /// <param name="paperItemId"></param>
        /// <returns></returns>
        public ExamResultAnswer GetExamResultAnswer(int examResultId, int paperItemId)
        {
            ExamResultAnswer examResultAnswer = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_RESULT_ANSWER_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, examResultId);
            db.AddInParameter(dbCommand, "p_paper_item_id", DbType.Int32, paperItemId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    examResultAnswer= CreateModelObject(dataReader);
                    break;
                }
            }

            return examResultAnswer;
        }

        /// <summary>
        /// 查询给定考卷的所有考生结果答案
        /// </summary>
        /// <param name="examResultId"></param>
        /// <returns></returns>
        public IList<ExamResultAnswer> GetExamResultAnswers(int examResultId)
        {
            IList<ExamResultAnswer> examResultAnswers = new List<ExamResultAnswer>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_RESULT_ANSWER_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, examResultId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    examResultAnswers.Add(CreateModelObject(dataReader));
                }
            }

            return examResultAnswers;
        }

        /// <summary>
        /// 查询给定考卷的所有考生结果答案(路局查询站段)
        /// </summary>
        /// <param name="examResultId"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public IList<ExamResultAnswer> GetExamResultAnswersByOrgID(int examResultId,int orgID)
        {
            IList<ExamResultAnswer> examResultAnswers = new List<ExamResultAnswer>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_RESULT_ANSWER_G_ORG";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, examResultId);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
            db.AddOutParameter(dbCommand, "p_net_name", DbType.String, 50);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    examResultAnswers.Add(CreateModelObject(dataReader));
                }
            }

            return examResultAnswers;
        }
        #endregion
    }
}
