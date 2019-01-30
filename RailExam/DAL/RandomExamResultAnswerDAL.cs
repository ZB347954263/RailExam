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
    public class RandomExamResultAnswerDAL
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

        public static RandomExamResultAnswer CreateModelObject(IDataReader dataReader)
        {
            return new RandomExamResultAnswer(
                DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamResultId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamItemId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Answer")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamTime")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("JudgeScore")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("JudgeStatusId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("JudgeStatusName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("JudgeRemark")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("SelectAnswer")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("StandardAnswer")]));
        }

        static RandomExamResultAnswerDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("randomexamresultid", "Random_EXAM_RESULT_ID");
            _ormTable.Add("randomexamitemid", "Random_EXAM_ITEM_ID");
            _ormTable.Add("answer", "ANSWER");
            _ormTable.Add("examtime", "EXAM_TIME");
            _ormTable.Add("judgescore", "JUDGE_SCORE");
            _ormTable.Add("judgestatusid", "JUDGE_STATUS_ID");
            _ormTable.Add("judgestatusname", "STATUS_NAME");
            _ormTable.Add("judgeremark", "JUDGE_REMARK");
            _ormTable.Add("selectanswer", "SELECT_ANSWER");
            _ormTable.Add("standardanswer", "STANDARD_ANSWER");
        }

        public int AddExamResultAnswer(RandomExamResultAnswer examResultAnswer)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_ANSWER_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_EXAM_result_id", DbType.Int32, examResultAnswer.RandomExamResultId);
            db.AddInParameter(dbCommand, "p_Random_EXAM_item_id", DbType.Int32, examResultAnswer.RandomExamItemId);
            db.AddInParameter(dbCommand, "p_answer", DbType.String, examResultAnswer.Answer);
            db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, examResultAnswer.ExamTime);
            db.AddInParameter(dbCommand, "p_judge_score", DbType.Decimal, examResultAnswer.JudgeScore);
            db.AddInParameter(dbCommand, "p_judge_status_id", DbType.Int32, examResultAnswer.JudgeStatusId);
            db.AddInParameter(dbCommand, "p_judge_remark", DbType.String, examResultAnswer.JudgeRemark);
            db.AddInParameter(dbCommand, "p_select_answer", DbType.String, examResultAnswer.SelectAnswer);
            db.AddInParameter(dbCommand, "p_standard_answer", DbType.String, examResultAnswer.StandardAnswer);

            int nRecordAffected = db.ExecuteNonQuery(dbCommand);

            return nRecordAffected;
        }

        public int UpdateExamResultAnswer(RandomExamResultAnswer examResultAnswer)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_ANSWER_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_EXAM_result_id", DbType.Int32, examResultAnswer.RandomExamResultId);
            db.AddInParameter(dbCommand, "p_Random_EXAM_item_id", DbType.Int32, examResultAnswer.RandomExamItemId);
            db.AddInParameter(dbCommand, "p_answer", DbType.String, examResultAnswer.Answer);
            db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, examResultAnswer.ExamTime);
            db.AddInParameter(dbCommand, "p_judge_score", DbType.Decimal, examResultAnswer.JudgeScore);
            db.AddInParameter(dbCommand, "p_judge_status_id", DbType.Int32, examResultAnswer.JudgeStatusId);
            db.AddInParameter(dbCommand, "p_judge_remark", DbType.String, examResultAnswer.JudgeRemark);

            int nRecordAffected = db.ExecuteNonQuery(dbCommand);
            return nRecordAffected;
        }

        public int UpdateExamResultAnswer(int resultId,int itemId,string answer,int hasYear)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_ANSWER_U_New";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_EXAM_result_id", DbType.Int32, resultId);
            db.AddInParameter(dbCommand, "p_Random_EXAM_item_id", DbType.Int32, itemId);
            db.AddInParameter(dbCommand, "p_answer", DbType.String, answer);
            db.AddInParameter(dbCommand, "p_has_year", DbType.String,hasYear);

            int nRecordAffected = db.ExecuteNonQuery(dbCommand);
            return nRecordAffected;
        }

        public int AddExamResultAnswer(IList<RandomExamResultAnswer> examResultAnswers)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                foreach (RandomExamResultAnswer examResultAnswer in examResultAnswers)
                {
                    string sqlCommand = "USP_Random_EXAM_ANSWER_I1";
                    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                    db.AddInParameter(dbCommand, "p_Random_EXAM_result_id", DbType.Int32, examResultAnswer.RandomExamResultId);
                    db.AddInParameter(dbCommand, "p_Random_EXAM_item_id", DbType.Int32, examResultAnswer.RandomExamItemId);
                    db.AddInParameter(dbCommand, "p_answer", DbType.String, examResultAnswer.Answer);
                    db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, examResultAnswer.ExamTime);
                    db.AddInParameter(dbCommand, "p_judge_score", DbType.Decimal, examResultAnswer.JudgeScore);
                    db.AddInParameter(dbCommand, "p_judge_status_id", DbType.Int32, examResultAnswer.JudgeStatusId);
                    db.AddInParameter(dbCommand, "p_judge_remark", DbType.String, examResultAnswer.JudgeRemark);
                    db.ExecuteNonQuery(dbCommand,transaction);
                }

                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            connection.Close();

            return 0;
        }

        public int UpdateExamResultAnswer(IList<RandomExamResultAnswer> examResultAnswers)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                foreach (RandomExamResultAnswer examResultAnswer in examResultAnswers)
                {
                    string sqlCommand = "USP_Random_EXAM_ANSWER_U";
                    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                    db.AddInParameter(dbCommand, "p_Random_EXAM_result_id", DbType.Int32, examResultAnswer.RandomExamResultId);
                    db.AddInParameter(dbCommand, "p_Random_EXAM_item_id", DbType.Int32, examResultAnswer.RandomExamItemId);
                    db.AddInParameter(dbCommand, "p_answer", DbType.String, examResultAnswer.Answer);
                    db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, examResultAnswer.ExamTime);
                    db.AddInParameter(dbCommand, "p_judge_score", DbType.Decimal, examResultAnswer.JudgeScore);
                    db.AddInParameter(dbCommand, "p_judge_status_id", DbType.Int32, examResultAnswer.JudgeStatusId);
                    db.AddInParameter(dbCommand, "p_judge_remark", DbType.String, examResultAnswer.JudgeRemark);
                    db.ExecuteNonQuery(dbCommand);
                }

                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            connection.Close();

            return 0;
        }


        public IList<RandomExamResultAnswer> GetExamResultAnswers(int randomExamResultId)
        {
            IList<RandomExamResultAnswer> examResultAnswers = new List<RandomExamResultAnswer>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_ANSWER_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_EXAM_result_id", DbType.Int32, randomExamResultId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    examResultAnswers.Add(CreateModelObject(dataReader));
                }
            }
            return examResultAnswers;
        }

        public IList<RandomExamResultAnswer> GetExamResultAnswersByOrgID(int randomExamResultId, int orgID)
        {
            IList<RandomExamResultAnswer> examResultAnswers = new List<RandomExamResultAnswer>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_ANSWER_G_ORG";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_EXAM_result_id", DbType.Int32, randomExamResultId);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32,orgID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    examResultAnswers.Add(CreateModelObject(dataReader));
                }
            }
            return examResultAnswers;
        }

		public IList<RandomExamResultAnswer> GetExamResultAnswersStation(int randomExamResultId)
		{
			IList<RandomExamResultAnswer> examResultAnswers = new List<RandomExamResultAnswer>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_EXAM_ANSWER_G_STA";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_Random_EXAM_result_id", DbType.Int32, randomExamResultId);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					examResultAnswers.Add(CreateModelObject(dataReader));
				}
			}
			return examResultAnswers;
		}
    }
}
