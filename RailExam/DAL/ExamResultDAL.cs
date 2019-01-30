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
    public class ExamResultDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        /// <summary>
        /// 空参数构造函数
        /// </summary>
        public ExamResultDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("examresultid", "EXAM_RESULT_ID");
            _ormTable.Add("organizationid", "ORG_ID");
            _ormTable.Add("organizationname", "ORG_NAME");
            _ormTable.Add("examid", "EXAM_ID");
            _ormTable.Add("examname", "EXAM_NAME");
            _ormTable.Add("paperid", "PAPER_ID");
            _ormTable.Add("papername", "PAPER_NAME");
            _ormTable.Add("examineeid", "EXAMINEE_ID");
            _ormTable.Add("examineename", "EXAMINEE_NAME");
            _ormTable.Add("begindatetime", "BEGIN_TIME");
            _ormTable.Add("currentdatetime", "CURRENT_TIME");
            _ormTable.Add("enddatetime", "END_TIME");
            _ormTable.Add("examtime", "EXAM_TIME");
            _ormTable.Add("autoscore", "AUTO_SCORE");
            _ormTable.Add("score", "SCORE");
            _ormTable.Add("judgeid", "JUDGE_ID");
            _ormTable.Add("judgename", "JUDGE_NAME");
            _ormTable.Add("judgebegindatetime", "JUDGE_BEGIN_TIME");
            _ormTable.Add("judgeenddatetime", "JUDGE_END_TIME");
            _ormTable.Add("correctrate", "CORRECT_RATE");
            _ormTable.Add("ispass", "IS_PASS");
            _ormTable.Add("statusid", "STATUS_ID");
            _ormTable.Add("statusname", "STATUS_NAME");
            _ormTable.Add("papertotalscore", "PAPER_TOTAL_SCORE");
            _ormTable.Add("memo", "MEMO");
            _ormTable.Add("exambegintime", "EXAM_BEGIN_TIME");
            _ormTable.Add("examendtime", "EXAM_END_TIME");
            _ormTable.Add("workno", "WORK_NO");
            _ormTable.Add("postname", "POST_NAME");
            _ormTable.Add("asc", "ASC");
            _ormTable.Add("desc", "DESC");
            _ormTable.Add("examtype","Exam_Type");
            _ormTable.Add("examresultidstation","EXAM_RESULT_ID_STATION");
        }

        public int AddExamResult(ExamResult examResult, string[] strAnswers)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_EXAM_RESULT_I");

            db.AddOutParameter(dbCommand, "p_exam_result_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, examResult.OrganizationId);
            db.AddInParameter(dbCommand, "p_exam_id", DbType.Int32, examResult.ExamId);
            db.AddInParameter(dbCommand, "p_paper_id", DbType.Int32, examResult.PaperId);
            db.AddInParameter(dbCommand, "p_examinee_id", DbType.Int32, examResult.ExamineeId);
            db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, examResult.BeginDateTime);
            db.AddInParameter(dbCommand, "p_current_time", DbType.DateTime, examResult.CurrentDateTime);
            db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, examResult.EndDateTime);
            db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, examResult.ExamTime);
            db.AddInParameter(dbCommand, "p_auto_score", DbType.Decimal, examResult.AutoScore);
            db.AddInParameter(dbCommand, "p_score", DbType.Decimal, examResult.Score);
            db.AddInParameter(dbCommand, "p_judge_id", DbType.Int32, examResult.JudgeId);
            db.AddInParameter(dbCommand, "p_judge_begin_time", DbType.DateTime, examResult.JudgeBeginDateTime);
            db.AddInParameter(dbCommand, "p_judge_end_time", DbType.DateTime, examResult.JudgeEndDateTime);
            db.AddInParameter(dbCommand, "p_correct_rate", DbType.Decimal, examResult.CorrectRate);
            db.AddInParameter(dbCommand, "p_is_pass", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, examResult.StatusId);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, examResult.Memo);

            int id = 0;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_exam_result_id"));

                for (int n = 0; n < strAnswers.Length; n++)
                {
                    string str2 = strAnswers[n].ToString();
                    string[] str3 = str2.Split(new char[] { '|' });
                    string strPaperItemId = str3[0].ToString();

                    string strTrueAnswer = str2.ToString().Substring(strPaperItemId.Length + 1);

                    string sqlCommand1 = "USP_EXAM_RESULT_ANSWER_NS_I";
                    dbCommand = db.GetStoredProcCommand(sqlCommand1);

                    db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, id);
                    db.AddInParameter(dbCommand, "p_paper_item_id", DbType.Int32, int.Parse(strPaperItemId));
                    db.AddInParameter(dbCommand, "p_answer", DbType.String, strTrueAnswer);
                    db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, 0);
                    db.AddOutParameter(dbCommand, "p_judge_score", DbType.Decimal, 4);
                    db.AddInParameter(dbCommand, "p_judge_status_id", DbType.Int32, 1);
                    db.AddInParameter(dbCommand, "p_judge_remark", DbType.String, "");
                    db.ExecuteNonQuery(dbCommand, transaction);

                }

                string sqlCommand2 = "USP_EXAM_RESULT_Update";
                DbCommand dbCommand2 = db.GetStoredProcCommand(sqlCommand2);

                db.AddInParameter(dbCommand2, "p_exam_result_id", DbType.Int32, id);
                db.AddInParameter(dbCommand2, "p_exam_id", DbType.Int32, examResult.ExamId);
                db.AddInParameter(dbCommand2, "p_paper_id", DbType.Int32, examResult.PaperId);
                db.AddOutParameter(dbCommand2, "p_is_pass", DbType.Int32, 4);
                db.ExecuteNonQuery(dbCommand2, transaction);

                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;

            }
            connection.Close();

            return id;
        }

        public void AddExamResultToServer(ExamResult examResult)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_EXAM_RESULT_I_OTHER");

            db.AddOutParameter(dbCommand, "p_exam_result_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, examResult.OrganizationId);
            db.AddInParameter(dbCommand, "p_exam_id", DbType.Int32, examResult.ExamId);
            db.AddInParameter(dbCommand, "p_paper_id", DbType.Int32, examResult.PaperId);
            db.AddInParameter(dbCommand, "p_examinee_id", DbType.Int32, examResult.ExamineeId);
            db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, examResult.BeginDateTime);
            db.AddInParameter(dbCommand, "p_current_time", DbType.DateTime, examResult.CurrentDateTime);
            db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, examResult.EndDateTime);
            db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, examResult.ExamTime);
            db.AddInParameter(dbCommand, "p_auto_score", DbType.Decimal, examResult.AutoScore);
            db.AddInParameter(dbCommand, "p_score", DbType.Decimal, examResult.Score);
            db.AddInParameter(dbCommand, "p_judge_id", DbType.Int32, examResult.JudgeId);
            db.AddInParameter(dbCommand, "p_judge_begin_time", DbType.DateTime, examResult.JudgeBeginDateTime);
            db.AddInParameter(dbCommand, "p_judge_end_time", DbType.DateTime, examResult.JudgeEndDateTime);
            db.AddInParameter(dbCommand, "p_correct_rate", DbType.Decimal, examResult.CorrectRate);
            db.AddInParameter(dbCommand, "p_is_pass", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, examResult.StatusId);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, examResult.Memo);
            db.AddInParameter(dbCommand, "p_exam_result_id_station", DbType.Int32, examResult.ExamResultIDStation);


            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;

            }
            connection.Close();

        }

       /// <summary>
        /// 删除给定考生结果ID的考试考生结果
        /// </summary>
        /// <param name="examResultId"></param>
        /// <returns></returns>
        public int DeleteExamResult(int examResultId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_EXAM_RESULT_D");

            db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, examResultId);

            return db.ExecuteNonQuery(dbCommand);
        }

        /// <summary>
        /// 删除给定考生结果ID的考试考生结果
        /// </summary>
        /// <param name="examResultId"></param>
        /// <returns></returns>
        public int DeleteExamResults(int[] examResultIds)
        {
            int nRecordAffected = 0;
            Database db = DatabaseFactory.CreateDatabase();
            DbConnection conn = db.CreateConnection();
            conn.Open();
            DbTransaction tran = conn.BeginTransaction();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_EXAM_RESULT_D");

            try
            {
                foreach (int n in examResultIds)
                {
                    db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, n);
                    // Exception thrown when using the following statement:
                    // dbCommand.ExecuteNonQuery();
                    db.ExecuteNonQuery(dbCommand);

                    nRecordAffected++;
                }
                //throw new Exception();
                tran.Commit();

            }
            catch 
            {
                tran.Rollback();
            }
            conn.Close();

            return nRecordAffected;
        }

        public int UpdateJudgeId(int examResultId, int judgeId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_EXAM_RESULT_UpdateJudgeId");

            db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, examResultId);
            db.AddInParameter(dbCommand, "p_judge_id", DbType.Int32, judgeId);

            return db.ExecuteNonQuery(dbCommand);
        }

        public int UpdateExamResult(ExamResult examResult)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_EXAM_RESULT_U");

            db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, examResult.ExamResultId);
            db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, examResult.BeginDateTime);
            db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, examResult.EndDateTime);
            db.AddInParameter(dbCommand, "p_score", DbType.Decimal, examResult.Score);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, examResult.StatusId);

            return db.ExecuteNonQuery(dbCommand);
        }

        public int UpdateJudgeBeginTime(int examResultId, DateTime beginTime)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_EXAM_RESULT_JUDGE_TIME_U");

            db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, examResultId);
            db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, beginTime);

            return db.ExecuteNonQuery(dbCommand);
        }

        public int UpdateExamResultAnswers(int examResultId, IList<ExamResultAnswer> examResultAnswers, string strResultCause, string strRemark, decimal oldScore, string EmployeeName)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                decimal totalJudgeScore = 0.0M;
                DbCommand dbCommand;

                foreach (ExamResultAnswer answer in examResultAnswers)
                {
                    dbCommand = db.GetStoredProcCommand("USP_EXAM_RESULT_ANSWER_SSR_U");

                    db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, examResultId);
                    db.AddInParameter(dbCommand, "p_paper_item_id", DbType.Int32, answer.PaperItemId);
                    db.AddInParameter(dbCommand, "p_judge_score", DbType.Decimal, answer.JudgeScore);

                    totalJudgeScore += answer.JudgeScore;

                    db.AddInParameter(dbCommand, "p_judge_status_id", DbType.Int32, answer.JudgeStatusId);
                    db.AddInParameter(dbCommand, "p_judge_remark", DbType.String, answer.JudgeRemark);
                    db.ExecuteNonQuery(dbCommand, transaction);
                }

                dbCommand = db.GetStoredProcCommand("USP_EXAM_RESULT_JUDGE_SCORE_U");
                db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, examResultId);
                db.AddInParameter(dbCommand, "p_score", DbType.Decimal, totalJudgeScore);

                db.ExecuteNonQuery(dbCommand);

                //修改记录 

                dbCommand = db.GetStoredProcCommand("USP_EXAM_RESULT_update_I");
                db.AddOutParameter(dbCommand, "p_exam_result_update_id", DbType.Int32, 4);
                db.AddInParameter(dbCommand, "p_UPDATE_CAUSE", DbType.String, strResultCause);
                db.AddInParameter(dbCommand, "p_old_score", DbType.Decimal, oldScore);
                db.AddInParameter(dbCommand, "p_memo", DbType.String, strRemark);
                db.AddInParameter(dbCommand, "p_new_score", DbType.Decimal, totalJudgeScore);
                db.AddInParameter(dbCommand, "p_UPDATE_PERSON", DbType.String, EmployeeName);
                db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, examResultId);

                db.ExecuteNonQuery(dbCommand);


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

        public int UpdateExamResultAndItsAnswers(ExamResult examResult, IList<ExamResultAnswer> examResultAnswers)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_EXAM_RESULT_U");

            db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, examResult.ExamResultId);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, examResult.OrganizationId);
            db.AddInParameter(dbCommand, "p_exam_id", DbType.Int32, examResult.ExamId);
            db.AddInParameter(dbCommand, "p_paper_id", DbType.Int32, examResult.PaperId);
            db.AddInParameter(dbCommand, "p_examinee_id", DbType.Int32, examResult.ExamineeId);
            db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, examResult.BeginDateTime);
            db.AddInParameter(dbCommand, "p_current_time", DbType.DateTime, examResult.CurrentDateTime);
            db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, examResult.EndDateTime);
            db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, examResult.ExamTime);
            db.AddInParameter(dbCommand, "p_auto_score", DbType.Decimal, examResult.AutoScore);
            db.AddInParameter(dbCommand, "p_score", DbType.Decimal, examResult.Score);
            db.AddInParameter(dbCommand, "p_judge_id", DbType.Int32, examResult.JudgeId);
            db.AddInParameter(dbCommand, "p_judge_begin_time", DbType.DateTime, examResult.JudgeBeginDateTime);
            db.AddInParameter(dbCommand, "p_judge_end_time", DbType.DateTime, examResult.JudgeEndDateTime);
            db.AddInParameter(dbCommand, "p_correct_rate", DbType.Decimal, examResult.CorrectRate);
            db.AddInParameter(dbCommand, "p_is_pass", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, examResult.StatusId);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, examResult.Memo);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);

                foreach (ExamResultAnswer answer in examResultAnswers)
                {
                    dbCommand = db.GetStoredProcCommand("USP_EXAM_RESULT_ANSWER_U");

                    db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, answer.ExamResultId);
                    db.AddInParameter(dbCommand, "p_paper_item_id", DbType.Int32, answer.PaperItemId);
                    db.AddInParameter(dbCommand, "p_answer", DbType.String, answer.Answer);
                    db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, answer.ExamTime);
                    db.AddInParameter(dbCommand, "p_judge_score", DbType.Decimal, answer.JudgeScore);
                    db.AddInParameter(dbCommand, "p_judge_status_id", DbType.Int32, answer.JudgeStatusId);
                    db.AddInParameter(dbCommand, "p_judge_remark", DbType.String, answer.JudgeRemark);
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

        /// <summary>
        /// 取给定考生结果ID的考试考生结果
        /// </summary>
        /// <param name="examResultId"></param>
        /// <returns></returns>
        public ExamResult GetExamResult(int examResultId)
        {
            ExamResult examResult = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_RESULT_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, examResultId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    examResult = CreateModelObject(dataReader);
                    break;
                }
            }

            return examResult;
        }

        /// <summary>
        /// 取给定考生结果ID的考试考生结果（路局查站段）
        /// </summary>
        /// <param name="examResultId"></param>
        /// <param name="orgID"></param>
        /// <returns></returns>
        public ExamResult GetExamResultByOrgID(int examResultId,int orgID)
        {
            ExamResult examResult = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_RESULT_G_ORG";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_exam_result_id", DbType.Int32, examResultId);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32,orgID);
            db.AddOutParameter(dbCommand,"p_net_name",DbType.String,50);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    examResult = CreateModelObject(dataReader);
                    break;
                }
            }

            return examResult;
        }


        /// <summary>
        /// 取给定考生ID、试卷id、考试id的考生考试结果
        /// </summary>
        /// <param name="examResultId"></param>
        /// <returns></returns>
        public ExamResult GetExamResult(int paperID, int examID, int examineeID)
        {
            ExamResult examResult = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_RESULT_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_exam_id", DbType.Int32, examID);
            db.AddInParameter(dbCommand, "p_paper_id", DbType.Int32, paperID);
            db.AddInParameter(dbCommand, "p_examinee_id", DbType.Int32, examineeID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    examResult = CreateModelObject(dataReader);
                    break;
                }
            }

            return examResult;
        }

        /// <summary>
        /// 返回所有试卷
        /// </summary>
        /// <returns>所有试卷</returns>
        public IList<ExamResult> GetExamResults()
        {
            IList<ExamResult> examResults = new List<ExamResult>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_RESULT_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    examResults.Add(CreateModelObject(dataReader));
                }
            }

            _recordCount = (int)db.GetParameterValue(dbCommand, "p_count");

            return examResults;
        }


        public IList<ExamResult> GetExamResultByExamID(int examID)
        {

            IList<ExamResult> examResults = new List<ExamResult>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_RESULT_ByExamId";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_exam_id", DbType.Int32, examID);         
           

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    examResults.Add(CreateModelObject(dataReader));
                }
            }          

            return examResults;

        }


        /// <summary>
        /// 返回考生考试试卷
        /// </summary>
        /// <param name="examineeId">
        /// 考生编号
        /// </param>
        /// <returns>试卷列表</returns>
        public IList<ExamResult> GetExamResults(int examineeId)
        {
            IList<ExamResult> examResults = new List<ExamResult>();

            Database db = DatabaseFactory.CreateDatabase();

            //string sqlCommand = "USP_EXAM_RESULT_S";
            string sqlCommand = "USP_EXAM_RESULT_G_USER";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_examinee_id", DbType.Int32, examineeId);
            db.AddInParameter(dbCommand, "p_can_see_score", DbType.Int32, 1);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                ExamResult result = null;
                while (dataReader.Read())
                {
                    result = CreateModelObject(dataReader);
                    result.ExamBeginTime = Convert.ToDateTime(dataReader[GetMappingFieldName("ExamBeginTime")]);
                    result.ExamEndTime = Convert.ToDateTime(dataReader[GetMappingFieldName("ExamEndTime")]);
                    result.ExamType = Convert.ToInt32(dataReader[GetMappingFieldName("ExamType")].ToString());
                    examResults.Add(result);
                }
            }

            return examResults;
        }

     

        public IList<ExamResult> GetExamResults(int examId, string strOrganizationName, string examineeName,string workno,
       decimal paperTotalScoreLower, decimal paperTotalScoreUpper, int examResultStatusId)
        {
            IList<ExamResult> examResults = new List<ExamResult>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_RESULT_GRADE_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

           
            db.AddInParameter(dbCommand, "p_exam_id", DbType.Int32, examId);
            db.AddInParameter(dbCommand, "p_organization_name", DbType.String, strOrganizationName);
            db.AddInParameter(dbCommand, "p_examinee_name", DbType.String, examineeName);
			db.AddInParameter(dbCommand, "p_work_no", DbType.String, workno);
            db.AddInParameter(dbCommand, "p_score_lower", DbType.Decimal, paperTotalScoreLower);
            db.AddInParameter(dbCommand, "p_score_upper", DbType.Decimal, paperTotalScoreUpper);
            db.AddInParameter(dbCommand, "p_exam_result_status_id", DbType.Int32, examResultStatusId);          

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                ExamResult er = null;
                while (dataReader.Read())
                {
                    er = CreateModelObject(dataReader);
                    er.WorkNo = DataConvert.ToString(dataReader[GetMappingFieldName("WorkNo")]);
                    er.PostName = DataConvert.ToString(dataReader[GetMappingFieldName("PostName")]);
                    examResults.Add(er);
                }
            }          

            return examResults;
        }


        public IList<ExamResult> GetExamResultsByOrgID( int examId, string strOrganizationName, string examineeName,string workno,
            decimal paperTotalScoreLower, decimal paperTotalScoreUpper, int examResultStatusId,int orgID)
        {
            IList<ExamResult> examResults = new List<ExamResult>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_RESULT_GRADE_ORG";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

         
            db.AddInParameter(dbCommand, "p_exam_id", DbType.Int32, examId);
            db.AddInParameter(dbCommand, "p_organization_name", DbType.String, strOrganizationName);
            db.AddInParameter(dbCommand, "p_examinee_name", DbType.String, examineeName);
			db.AddInParameter(dbCommand, "p_work_no", DbType.String, workno);
            db.AddInParameter(dbCommand, "p_score_lower", DbType.Decimal, paperTotalScoreLower);
            db.AddInParameter(dbCommand, "p_score_upper", DbType.Decimal, paperTotalScoreUpper);
            db.AddInParameter(dbCommand, "p_exam_result_status_id", DbType.Int32, examResultStatusId);      
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
			//db.AddOutParameter(dbCommand, "p_net_name", DbType.String, 50);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                ExamResult er = null;
                while (dataReader.Read())
                {
                    er = CreateModelObject(dataReader);
                    er.WorkNo = DataConvert.ToString(dataReader[GetMappingFieldName("WorkNo")]);
                    er.PostName = DataConvert.ToString(dataReader[GetMappingFieldName("PostName")]);
                    examResults.Add(er);
                }
            }          

            return examResults;
        }


        /// <summary>
        /// 返回符合查询条件的试卷
        /// </summary>
        /// <returns>符合查询条件的试卷</returns>
        public IList<ExamResult> GetExamResults(int examId, string organizationName, /*int examId,*/ string examineeName,
            decimal scoreLower, decimal scoreUpper, int statusId,  /*int judgeId,*/ int startRowIndex, int pageSize, string orderBy)
        {
            IList<ExamResult> examResults = new List<ExamResult>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EXAM_RESULT_F";//USP_EXAM_RESULT_S
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_exam_id", DbType.Int32, examId);
            db.AddInParameter(dbCommand, "p_org_name", DbType.String, organizationName);
            //db.AddInParameter(dbCommand, "p_exam_id", DbType.Int32, examId);
            db.AddInParameter(dbCommand, "p_examinee_name", DbType.String, examineeName);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, statusId);
            //db.AddInParameter(dbCommand, "p_judge_id", DbType.Int32, judgeId);
            db.AddInParameter(dbCommand, "p_score_lower", DbType.Decimal, scoreLower);
            db.AddInParameter(dbCommand, "p_score_upper", DbType.Decimal, scoreUpper);
            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, pageSize);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            //db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                ExamResult er = null;
                while (dataReader.Read())
                {
                    er = CreateModelObject(dataReader);
                    er.WorkNo = DataConvert.ToString(dataReader[GetMappingFieldName("WorkNo")]);
                    er.PostName = DataConvert.ToString(dataReader[GetMappingFieldName("PostName")]);
                    examResults.Add(er);
                }
            }

            //_recordCount = (int)db.GetParameterValue(dbCommand, "p_count");

            return examResults;
        }

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

        public static ExamResult CreateModelObject(IDataReader dataReader)
        {
            return new ExamResult(
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamResultId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrganizationId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("OrganizationName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ExamName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("PaperId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("PaperName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamineeId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ExamineeName")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("BeginDateTime")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("CurrentDateTime")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("EndDateTime")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamTime")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("AutoScore")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("Score")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("JudgeId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("JudgeName")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("JudgeBeginDateTime")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("JudgeEndDateTime")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("CorrectRate")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("IsPass")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("StatusId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("StatusName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamResultIDStation")]));
        }
    }
}
