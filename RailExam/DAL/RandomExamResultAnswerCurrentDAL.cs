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
    public class RandomExamResultAnswerCurrentDAL
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

        public static RandomExamResultAnswerCurrent CreateModelObject(IDataReader dataReader)
        {
            return new RandomExamResultAnswerCurrent(
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

        static RandomExamResultAnswerCurrentDAL()
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

        public int AddExamResultAnswerCurrent(RandomExamResultAnswerCurrent examResultAnswer)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_ANSWER_Cur_I";
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

        public void UpdateExamResultAnswerCurrent(RandomExamResultAnswerCurrent examResultAnswer)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_ANSWER_Cur_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_EXAM_result_id", DbType.Int32, examResultAnswer.RandomExamResultId);
            db.AddInParameter(dbCommand, "p_Random_EXAM_item_id", DbType.Int32, examResultAnswer.RandomExamItemId);
            db.AddInParameter(dbCommand, "p_answer", DbType.String, examResultAnswer.Answer);
            db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, examResultAnswer.ExamTime);
            //db.AddInParameter(dbCommand, "p_judge_score", DbType.Decimal, examResultAnswer.JudgeScore);
            //db.AddInParameter(dbCommand, "p_judge_status_id", DbType.Int32, examResultAnswer.JudgeStatusId);
            db.AddInParameter(dbCommand, "p_judge_remark", DbType.String, examResultAnswer.JudgeRemark);

            db.ExecuteNonQuery(dbCommand);
        }

        public int AddExamResultAnswerCurrent(IList<RandomExamResultAnswerCurrent> examResultAnswers)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                foreach (RandomExamResultAnswerCurrent examResultAnswer in examResultAnswers)
                {
                    string sqlCommand = "USP_Random_EXAM_ANSWER_Cur_I";
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

        public int AddExamResultAnswerCurrentSave(int randomExamResultID,IList<RandomExamResultAnswerCurrent> examResultAnswers)
        {
            Database db = DatabaseFactory.CreateDatabase();

            //DbConnection connection = db.CreateConnection();
            //connection.Open();
            //DbTransaction transaction = connection.BeginTransaction();

            try
            {
				//string sqlCommand1 = "USP_Random_EXAM_ANSWER_Cur_D";
				//DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand1);

				//db.AddInParameter(dbCommand1, "p_random_exam_result_id", DbType.Int32, randomExamResultID);
				//db.ExecuteNonQuery(dbCommand1,transaction);

				foreach (RandomExamResultAnswerCurrent current in examResultAnswers)
            	{
            	    try
            	    {
                        string sqlCommand = "USP_Random_EXAM_ANSWER_Cur_U";
                        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                        db.AddInParameter(dbCommand, "p_Random_EXAM_result_id", DbType.Int32, current.RandomExamResultId);
                        db.AddInParameter(dbCommand, "p_Random_EXAM_item_id", DbType.Int32, current.RandomExamItemId);
                        db.AddInParameter(dbCommand, "p_answer", DbType.String, current.Answer);
                        db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, current.ExamTime);
                        //db.AddInParameter(dbCommand, "p_judge_score", DbType.Decimal, current.JudgeScore);
                        //db.AddInParameter(dbCommand, "p_judge_status_id", DbType.Int32, current.JudgeStatusId);
                        db.AddInParameter(dbCommand, "p_judge_remark", DbType.String, current.JudgeRemark);

                        db.ExecuteNonQuery(dbCommand);
            	    }
            	    catch
                    {
                        continue; 
                    }
				}

                //transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                //transaction.Rollback();
                throw ex;
            }
            //connection.Close();

            return 0;
        }

        public void AddExamResultAnswerCurrent(int randomExamResultId,string strRandomID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            string sqlCommand = "USP_Random_EXAM_ANSWER_Cur_I1";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_random_exam_result_id", DbType.Int32, randomExamResultId);
            db.AddInParameter(dbCommand, "p_random_IDs", DbType.String, strRandomID);

            db.ExecuteNonQuery(dbCommand);
        }

        public void DeleteExamResultAnswerCurrent(int randomExamResultID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbConnection connection = db.CreateConnection();

            string sqlCommand = "USP_Random_EXAM_ANSWER_Cur_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_random_exam_result_id", DbType.Int32, randomExamResultID);
            db.ExecuteNonQuery(dbCommand);
        }

        public int UpdateExamResultAnswerCurrent(IList<RandomExamResultAnswerCurrent> examResultAnswers)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                foreach (RandomExamResultAnswerCurrent examResultAnswer in examResultAnswers)
                {
                    string sqlCommand = "USP_Random_EXAM_ANSWER_U";
                    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                    db.AddInParameter(dbCommand, "p_Random_EXAM_result_id", DbType.Int32, examResultAnswer.RandomExamResultId);
                    db.AddInParameter(dbCommand, "p_Random_EXAM_item_id", DbType.Int32, examResultAnswer.RandomExamItemId);
                    db.AddInParameter(dbCommand, "p_answer", DbType.String, examResultAnswer.Answer);
                    db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, examResultAnswer.ExamTime);
                    //db.AddInParameter(dbCommand, "p_judge_score", DbType.Decimal, examResultAnswer.JudgeScore);
                    //db.AddInParameter(dbCommand, "p_judge_status_id", DbType.Int32, examResultAnswer.JudgeStatusId);
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


        public IList<RandomExamResultAnswerCurrent> GetExamResultAnswersCurrent(int randomExamResultId)
        {
            IList<RandomExamResultAnswerCurrent> examResultAnswers = new List<RandomExamResultAnswerCurrent>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_ANSWER_Cur_G";
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

        public IList<RandomExamResultAnswerCurrent> GetExamResultAnswersCurrentByOrgID(int randomExamResultId, int orgID)
        {
            IList<RandomExamResultAnswerCurrent> examResultAnswers = new List<RandomExamResultAnswerCurrent>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_ANSWER_Cur_ORG";
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

        public RandomExamResultAnswerCurrent GetExamResultAnswerCurrent(int randomExamResultID,int examItemID)
        {
            RandomExamResultAnswerCurrent exam = null;

            Database db = DatabaseFactory.CreateDatabase();
            string sqlCommand = "USP_Random_EXAM_ANSWER_Cur_G1";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_random_Exam_result_id", DbType.Int32, randomExamResultID);
            db.AddInParameter(dbCommand, "p_item_id", DbType.Int32, examItemID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    exam = CreateModelObject(dataReader);
                }
            }

            return exam;
        }
    }
}
