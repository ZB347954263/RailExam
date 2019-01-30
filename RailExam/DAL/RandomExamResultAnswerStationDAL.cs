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
	public class RandomExamResultAnswerStationDAL
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

        public static RandomExamResultAnswerStation CreateModelObject(IDataReader dataReader)
        {
            return new RandomExamResultAnswerStation(
				DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamResultAnswerID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamResultID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamItemID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Answer")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamTime")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("JudgeScore")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("JudgeStatusId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("JudgeStatusName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("JudgeRemark")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamResultIDStation")]));
        }

		static RandomExamResultAnswerStationDAL()
        {
            _ormTable = new Hashtable();

			_ormTable.Add("randomexamresultanswerid", "Random_EXAM_RESULT_ANSWER_ID");
            _ormTable.Add("randomexamresultid", "Random_EXAM_RESULT_ID");
            _ormTable.Add("randomexamitemid", "Random_EXAM_ITEM_ID");
            _ormTable.Add("answer", "ANSWER");
            _ormTable.Add("examtime", "EXAM_TIME");
            _ormTable.Add("judgescore", "JUDGE_SCORE");
            _ormTable.Add("judgestatusid", "JUDGE_STATUS_ID");
            _ormTable.Add("judgestatusname", "STATUS_NAME");
            _ormTable.Add("judgeremark", "JUDGE_REMARK");
			_ormTable.Add("randomexamresultidstation", "Random_EXAM_RESULT_ID_STATION");
        }

        public int AddExamResultAnswerStation(RandomExamResultAnswerStation examResultAnswer)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_ANSWER_STA_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_EXAM_result_id", DbType.Int32, examResultAnswer.RandomExamResultID);
            db.AddInParameter(dbCommand, "p_Random_EXAM_item_id", DbType.Int32, examResultAnswer.RandomExamItemID);
            db.AddInParameter(dbCommand, "p_answer", DbType.String, examResultAnswer.Answer);
            db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, examResultAnswer.ExamTime);
            db.AddInParameter(dbCommand, "p_judge_score", DbType.Decimal, examResultAnswer.JudgeScore);
            db.AddInParameter(dbCommand, "p_judge_status_id", DbType.Int32, examResultAnswer.JudgeStatusID);
            db.AddInParameter(dbCommand, "p_judge_remark", DbType.String, examResultAnswer.JudgeRemark);
			db.AddInParameter(dbCommand, "p_Random_EXAM_result_id_Sta", DbType.Int32, examResultAnswer.RandomExamResultIDStation);

            int nRecordAffected = db.ExecuteNonQuery(dbCommand);

            return nRecordAffected;
        }

		public int UpdateExamResultAnswerStation(RandomExamResultAnswerStation examResultAnswer)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_ANSWER_STA_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_EXAM_result_id", DbType.Int32, examResultAnswer.RandomExamResultID);
            db.AddInParameter(dbCommand, "p_Random_EXAM_item_id", DbType.Int32, examResultAnswer.RandomExamItemID);
            db.AddInParameter(dbCommand, "p_answer", DbType.String, examResultAnswer.Answer);
            db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, examResultAnswer.ExamTime);
            db.AddInParameter(dbCommand, "p_judge_score", DbType.Decimal, examResultAnswer.JudgeScore);
            db.AddInParameter(dbCommand, "p_judge_status_id", DbType.Int32, examResultAnswer.JudgeStatusID);
            db.AddInParameter(dbCommand, "p_judge_remark", DbType.String, examResultAnswer.JudgeRemark);

            int nRecordAffected = db.ExecuteNonQuery(dbCommand);
            return nRecordAffected;
        }

		public void DelExamResultAnswerStation(int randomexamid,int orgid)
		{
			Database db = DatabaseFactory.CreateDatabase();

			DbConnection connection = db.CreateConnection();
			connection.Open();
			DbTransaction transaction = connection.BeginTransaction();

			try
			{
				string sqlCommand = "USP_Random_EXAM_ANSWER_STA_D";
				DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand);
				db.AddInParameter(dbCommand1, "p_random_exam_id", DbType.Int32, randomexamid);
				db.AddInParameter(dbCommand1, "p_org_id", DbType.Int32, orgid);
				db.ExecuteNonQuery(dbCommand1, transaction);
				transaction.Commit();
			}
			catch (System.SystemException ex)
			{
				transaction.Rollback();
				throw ex;
			}
			connection.Close();
		}

		public int AddExamResultAnswerStation(IList<RandomExamResultAnswerStation> examResultAnswers)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
				foreach (RandomExamResultAnswerStation examResultAnswer in examResultAnswers)
                {
					string sqlCommand = "USP_Random_EXAM_ANSWER_STA_I";
                    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                    db.AddInParameter(dbCommand, "p_Random_EXAM_result_id", DbType.Int32, examResultAnswer.RandomExamResultID);
                    db.AddInParameter(dbCommand, "p_Random_EXAM_item_id", DbType.Int32, examResultAnswer.RandomExamItemID);
                    db.AddInParameter(dbCommand, "p_answer", DbType.String, examResultAnswer.Answer);
                    db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, examResultAnswer.ExamTime);
                    db.AddInParameter(dbCommand, "p_judge_score", DbType.Decimal, examResultAnswer.JudgeScore);
                    db.AddInParameter(dbCommand, "p_judge_status_id", DbType.Int32, examResultAnswer.JudgeStatusID);
                    db.AddInParameter(dbCommand, "p_judge_remark", DbType.String, examResultAnswer.JudgeRemark);
					db.AddInParameter(dbCommand, "p_Random_EXAM_result_id_Sta", DbType.Int32, examResultAnswer.RandomExamResultIDStation);
					db.ExecuteNonQuery(dbCommand, transaction);
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

		public int UpdateExamResultAnswerStation(IList<RandomExamResultAnswerStation> examResultAnswers)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
				foreach (RandomExamResultAnswerStation examResultAnswer in examResultAnswers)
                {
					string sqlCommand = "USP_Random_EXAM_ANSWER_STA_U";
                    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                    db.AddInParameter(dbCommand, "p_Random_EXAM_result_id", DbType.Int32, examResultAnswer.RandomExamResultID);
                    db.AddInParameter(dbCommand, "p_Random_EXAM_item_id", DbType.Int32, examResultAnswer.RandomExamItemID);
                    db.AddInParameter(dbCommand, "p_answer", DbType.String, examResultAnswer.Answer);
                    db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, examResultAnswer.ExamTime);
                    db.AddInParameter(dbCommand, "p_judge_score", DbType.Decimal, examResultAnswer.JudgeScore);
                    db.AddInParameter(dbCommand, "p_judge_status_id", DbType.Int32, examResultAnswer.JudgeStatusID);
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


		//public IList<RandomExamResultAnswerStation> GetExamResultAnswers(int randomExamResultId)
		//{
		//    IList<RandomExamResultAnswer> examResultAnswers = new List<RandomExamResultAnswer>();

		//    Database db = DatabaseFactory.CreateDatabase();

		//    string sqlCommand = "USP_Random_EXAM_ANSWER_G";
		//    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

		//    db.AddInParameter(dbCommand, "p_Random_EXAM_result_id", DbType.Int32, randomExamResultId);

		//    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
		//    {
		//        while (dataReader.Read())
		//        {
		//            examResultAnswers.Add(CreateModelObject(dataReader));
		//        }
		//    }
		//    return examResultAnswers;
		//}

		//public IList<RandomExamResultAnswer> GetExamResultAnswersByOrgID(int randomExamResultId, int orgID)
		//{
		//    IList<RandomExamResultAnswer> examResultAnswers = new List<RandomExamResultAnswer>();

		//    Database db = DatabaseFactory.CreateDatabase();

		//    string sqlCommand = "USP_Random_EXAM_ANSWER_G_ORG";
		//    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

		//    db.AddInParameter(dbCommand, "p_Random_EXAM_result_id", DbType.Int32, randomExamResultId);
		//    db.AddInParameter(dbCommand, "p_org_id", DbType.Int32,orgID);

		//    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
		//    {
		//        while (dataReader.Read())
		//        {
		//            examResultAnswers.Add(CreateModelObject(dataReader));
		//        }
		//    }
		//    return examResultAnswers;
		//}
	}
}
