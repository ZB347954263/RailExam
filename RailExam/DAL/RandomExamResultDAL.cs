using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;
using DSunSoft.Data;

namespace RailExam.DAL
{
    public class RandomExamResultDAL
    {
         private static Hashtable _ormTable;
        private int _recordCount = 0;

        /// <summary>
        /// 空参数构造函数
        /// </summary>
        public RandomExamResultDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("randomexamresultid", "Random_EXAM_RESULT_ID");
            _ormTable.Add("organizationid", "ORG_ID");
            _ormTable.Add("organizationname", "ORG_NAME");
            _ormTable.Add("randomexamid", "Random_EXAM_ID");
            _ormTable.Add("examname", "EXAM_NAME");          
            _ormTable.Add("examineeid", "EXAMINEE_ID");
            _ormTable.Add("examineename", "EXAMINEE_NAME");
            _ormTable.Add("begindatetime", "BEGIN_TIME");
            _ormTable.Add("currentdatetime", "CURRENT_TIME");
            _ormTable.Add("enddatetime", "END_TIME");
            _ormTable.Add("examtime", "EXAM_TIME");
            _ormTable.Add("autoscore", "AUTO_SCORE");
            _ormTable.Add("score", "SCORE");          
            _ormTable.Add("correctrate", "CORRECT_RATE");
            _ormTable.Add("ispass", "IS_PASS");
            _ormTable.Add("statusid", "STATUS_ID");
            _ormTable.Add("statusname", "STATUS_NAME");           
            _ormTable.Add("memo", "MEMO");         
            _ormTable.Add("workno", "WORK_NO");
            _ormTable.Add("postname", "POST_NAME");
            _ormTable.Add("asc", "ASC");
            _ormTable.Add("desc", "DESC");
			_ormTable.Add("examorgname", "EXAM_ORG_NAME");
            _ormTable.Add("randomexamresultidstation","Random_Exam_Result_ID_Station");
			_ormTable.Add("examstylename","EXAM_STYLE_NAME");
            _ormTable.Add("istemp", "IS_TEMP");
		}

        public int AddRandomExamResult(RandomExamResult examResult)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_RESULT_I");

            db.AddOutParameter(dbCommand, "p_Random_exam_result_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, examResult.OrganizationId);
            db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, examResult.RandomExamId);           
            db.AddInParameter(dbCommand, "p_examinee_id", DbType.Int32, examResult.ExamineeId);
            db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, examResult.BeginDateTime);
            db.AddInParameter(dbCommand, "p_current_time", DbType.DateTime, examResult.CurrentDateTime);
            db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, examResult.EndDateTime);
            db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, examResult.ExamTime);
            db.AddInParameter(dbCommand, "p_auto_score", DbType.Decimal, examResult.AutoScore);
            db.AddInParameter(dbCommand, "p_score", DbType.Decimal, examResult.Score);          
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
                id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_Random_exam_result_id"));              

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

        public int AddRandomExamResultToServer(RandomExamResult examResult)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_RESULT_I_Other");

            db.AddOutParameter(dbCommand, "p_Random_exam_result_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, examResult.OrganizationId);
            db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, examResult.RandomExamId);
            db.AddInParameter(dbCommand, "p_examinee_id", DbType.Int32, examResult.ExamineeId);
            db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, examResult.BeginDateTime);
            db.AddInParameter(dbCommand, "p_current_time", DbType.DateTime, examResult.CurrentDateTime);
            db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, examResult.EndDateTime);
            db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, examResult.ExamTime);
            db.AddInParameter(dbCommand, "p_auto_score", DbType.Decimal, examResult.AutoScore);
            db.AddInParameter(dbCommand, "p_score", DbType.Decimal, examResult.Score);
            db.AddInParameter(dbCommand, "p_correct_rate", DbType.Decimal, examResult.CorrectRate);
            db.AddInParameter(dbCommand, "p_is_pass", DbType.Int32, 0);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, examResult.StatusId);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, examResult.Memo);
            db.AddInParameter(dbCommand, "p_random_exam_result_id_other", DbType.Int32, examResult.RandomExamResultId);

            int id = 0;

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_Random_exam_result_id"));

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

        public void RemoveRandomExamResultToServer(int randomExamID,int orgID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_RANDOM_EXAM_RESULT_SERVER");

            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
            db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, randomExamID);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);

                //DbCommand dbCommand4 = db.GetStoredProcCommand("USP_RANDOM_EXAM_RESULT_DOWN");
                //db.AddInParameter(dbCommand4, "p_random_exam_id", DbType.Int32, randomExamID);
                //db.ExecuteNonQuery(dbCommand4, transaction);

                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;

            }
            connection.Close();
        }

        public void RemoveRandomExamResultToServerAnswer(int randomExamID, int orgID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_Result_Ser_Ans");

            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
            db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, randomExamID);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);

                //DbCommand dbCommand4 = db.GetStoredProcCommand("USP_RANDOM_EXAM_RESULT_DOWN");
                //db.AddInParameter(dbCommand4, "p_random_exam_id", DbType.Int32, randomExamID);
                //db.ExecuteNonQuery(dbCommand4, transaction);

                transaction.Commit();
            }
            catch (System.SystemException ex)
            {
                transaction.Rollback();
                throw ex;

            }
            connection.Close();
        }

        public int UpdateRandomExamResult(RandomExamResult examResult)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_RESULT_U");

            db.AddInParameter(dbCommand, "p_Random_exam_result_id", DbType.Int32, examResult.RandomExamResultId);         
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, examResult.OrganizationId);
            db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, examResult.RandomExamId);
            db.AddInParameter(dbCommand, "p_examinee_id", DbType.Int32, examResult.ExamineeId);
            db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, examResult.BeginDateTime);
            db.AddInParameter(dbCommand, "p_current_time", DbType.DateTime, examResult.CurrentDateTime);
            db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, examResult.EndDateTime);
            db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, examResult.ExamTime);
            db.AddInParameter(dbCommand, "p_auto_score", DbType.Decimal, examResult.AutoScore);
            db.AddInParameter(dbCommand, "p_score", DbType.Decimal, examResult.Score);
            db.AddInParameter(dbCommand, "p_correct_rate", DbType.Decimal, examResult.CorrectRate);
            db.AddOutParameter(dbCommand, "p_is_pass", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, examResult.StatusId);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, examResult.Memo);

            return db.ExecuteNonQuery(dbCommand);
        }

		public int UpdateRandomExamResultOther(RandomExamResult examResult)
		{
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_RESULT_U_OTHER");

			db.AddInParameter(dbCommand, "p_Random_exam_result_id", DbType.Int32, examResult.RandomExamResultId);
			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, examResult.OrganizationId);
			db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, examResult.RandomExamId);
			db.AddInParameter(dbCommand, "p_examinee_id", DbType.Int32, examResult.ExamineeId);
			db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, examResult.BeginDateTime);
			db.AddInParameter(dbCommand, "p_current_time", DbType.DateTime, examResult.CurrentDateTime);
			db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, examResult.EndDateTime);
			db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, examResult.ExamTime);
			db.AddInParameter(dbCommand, "p_auto_score", DbType.Decimal, examResult.AutoScore);
			db.AddInParameter(dbCommand, "p_score", DbType.Decimal, examResult.Score);
			db.AddInParameter(dbCommand, "p_correct_rate", DbType.Decimal, examResult.CorrectRate);
			db.AddOutParameter(dbCommand, "p_is_pass", DbType.Int32, 4);
			db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, examResult.StatusId);
			db.AddInParameter(dbCommand, "p_memo", DbType.String, examResult.Memo);

			return db.ExecuteNonQuery(dbCommand);
		}

        public int UpdateRandomExamResultToServer(RandomExamResult examResult)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_RESULT_U_Other");

            db.AddInParameter(dbCommand, "p_Random_exam_result_id", DbType.Int32, examResult.RandomExamResultIDStation);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, examResult.OrganizationId);
            db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, examResult.RandomExamId);
            db.AddInParameter(dbCommand, "p_examinee_id", DbType.Int32, examResult.ExamineeId);
            db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, examResult.BeginDateTime);
            db.AddInParameter(dbCommand, "p_current_time", DbType.DateTime, examResult.CurrentDateTime);
            db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, examResult.EndDateTime);
            db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, examResult.ExamTime);
            db.AddInParameter(dbCommand, "p_auto_score", DbType.Decimal, examResult.AutoScore);
            db.AddInParameter(dbCommand, "p_score", DbType.Decimal, examResult.Score);
            db.AddInParameter(dbCommand, "p_correct_rate", DbType.Decimal, examResult.CorrectRate);
            db.AddOutParameter(dbCommand, "p_is_pass", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, examResult.StatusId);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, examResult.Memo);

            return db.ExecuteNonQuery(dbCommand);
        }

        public RandomExamResult GetRandomExamResultByOrgID(int examResultID,int orgID)
        {
            RandomExamResult RandomExamResult = new RandomExamResult();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_RESULT_s_org";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_exam_result_id", DbType.Int32, examResultID);
            db.AddInParameter(dbCommand,"p_org_id",DbType.Int32,orgID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {    
                 
                while (dataReader.Read())
                {
                    RandomExamResult = CreateModelObject(dataReader);
                    RandomExamResult.WorkNo = DataConvert.ToString(dataReader[GetMappingFieldName("WorkNo")]);
                    RandomExamResult.PostName = DataConvert.ToString(dataReader[GetMappingFieldName("PostName")]);                      
                }
            }

            return RandomExamResult;
        }

        public RandomExamResult GetRandomExamResult(int examResultID)
        {
            RandomExamResult RandomExamResult = new RandomExamResult();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_RESULT_s";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_exam_result_id", DbType.Int32, examResultID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {

                while (dataReader.Read())
                {
                    RandomExamResult = CreateModelObject(dataReader);
                    RandomExamResult.WorkNo = DataConvert.ToString(dataReader[GetMappingFieldName("WorkNo")]);
                    RandomExamResult.PostName = DataConvert.ToString(dataReader[GetMappingFieldName("PostName")]);
                }
            }

            return RandomExamResult;
        }

        public RandomExamResult GetRandomExamResultTemp(int examResultID)
        {
            RandomExamResult RandomExamResult = new RandomExamResult();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_RESULT_s_Temp";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_exam_result_id", DbType.Int32, examResultID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {

                while (dataReader.Read())
                {
                    RandomExamResult = CreateModelObject(dataReader);
                    RandomExamResult.WorkNo = DataConvert.ToString(dataReader[GetMappingFieldName("WorkNo")]);
                    RandomExamResult.PostName = DataConvert.ToString(dataReader[GetMappingFieldName("PostName")]);
                }
            }

            return RandomExamResult;
        }

		public RandomExamResult GetRandomExamResultStation(int examResultID)
		{
			RandomExamResult RandomExamResult = new RandomExamResult();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_EXAM_RESULT_S_STA";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_Random_exam_result_id", DbType.Int32, examResultID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{

				while (dataReader.Read())
				{
					RandomExamResult = CreateModelObject(dataReader);
					RandomExamResult.WorkNo = DataConvert.ToString(dataReader[GetMappingFieldName("WorkNo")]);
					RandomExamResult.PostName = DataConvert.ToString(dataReader[GetMappingFieldName("PostName")]);
				}
			}

			return RandomExamResult;
		}

        public IList<RandomExamResult> GetRandomExamResultByExamID(int examID)
        {
            IList<RandomExamResult> examResults = new List<RandomExamResult>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_RESULT_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_exam_id", DbType.Int32, examID);


            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    RandomExamResult examResult = CreateModelObject(dataReader);
                    examResult.IsTemp = Convert.ToInt32(dataReader[GetMappingFieldName("IsTemp")]);
                    examResults.Add(examResult);
                }
            }

            return examResults;

        }

        public IList<RandomExamResult> GetRandomExamResultInfo(int examID)
        {
            IList<RandomExamResult> examResults = new List<RandomExamResult>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_RESULT_Q1";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_exam_id", DbType.Int32, examID);


            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    examResults.Add(CreateModelObject(dataReader));
                }
            }

            return examResults;
        }


		public IList<RandomExamResult> GetRandomExamResultInfoStation(int examID,int orgID)
		{
			IList<RandomExamResult> examResults = new List<RandomExamResult>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_EXAM_RESULT_Q_STA";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_Random_exam_id", DbType.Int32, examID);
			db.AddInParameter(dbCommand,"p_org_id",DbType.Int32,orgID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					examResults.Add(CreateModelObject(dataReader));
				}
			}

			return examResults;
		}

        public IList<RandomExamResult> GetRandomExamResultByExamineeID(int ExamineeID, int examID)
        {
            IList<RandomExamResult> examResults = new List<RandomExamResult>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_RESULT_g";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_exam_id", DbType.Int32, examID);
            db.AddInParameter(dbCommand, "p_examinee_id", DbType.Int32, ExamineeID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    examResults.Add(CreateModelObject(dataReader));
                }
            }

            return examResults;

        }

		public IList<RandomExamResult> GetRandomExamResultByExamineeIDFromServer(int ExamineeID, int examID)
		{
			IList<RandomExamResult> examResults = new List<RandomExamResult>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_EXAM_RESULT_g_Ser";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_Random_exam_id", DbType.Int32, examID);
			db.AddInParameter(dbCommand, "p_examinee_id", DbType.Int32, ExamineeID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					examResults.Add(CreateModelObject(dataReader));
				}
			}

			return examResults;

		}


        public RandomExamResult GetNewRandomExamResultByExamineeID(int ExamineeID, int examID)
        {
            RandomExamResult examResults = new RandomExamResult();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_RESULT_G_New";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_exam_id", DbType.Int32, examID);
            db.AddInParameter(dbCommand, "p_examinee_id", DbType.Int32, ExamineeID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    examResults = CreateModelObject(dataReader);
                }
            }
            return examResults;
        }

        public int RemoveResultAnswer(int randomResultExamID)
        {
            int id = 0;
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_Result_Remove");

            db.AddInParameter(dbCommand, "p_random_result_id", DbType.Int32, randomResultExamID);
            db.AddOutParameter(dbCommand,"p_next_id",DbType.Int32,4);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                transaction.Commit();
                id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_next_id"));
            }
            catch(Exception ex) 
            {
                transaction.Rollback();
                throw ex;
            }
            connection.Close();

            return id;
        }


        public int RemoveResultAnswerCurrent(int randomResultExamID)
        {
            int id = 0;
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_Result_Re_C");

            db.AddInParameter(dbCommand, "p_random_result_id", DbType.Int32, randomResultExamID);
            db.AddOutParameter(dbCommand, "p_next_id", DbType.Int32, 4);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                transaction.Commit();
                id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_next_id"));
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            connection.Close();

            return id;
        }


        public void RemoveResultAnswerTemp(int randomResultExamID)
        {
            int id = 0;
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_Result_Re_T");

            db.AddInParameter(dbCommand, "p_random_result_id", DbType.Int32, randomResultExamID);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            connection.Close();
        }

        public void RemoveResultAnswerAfterEnd(IList<RandomExamResultCurrent> objResultCurrent,int randomExamID,bool isServerCenter)
        {
            int id = 0;
            Database db = DatabaseFactory.CreateDatabase();

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                foreach (RandomExamResultCurrent current in objResultCurrent)
                {
                    DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_RESULT_Cur_U");
                    db.AddInParameter(dbCommand, "p_Random_exam_result_id", DbType.Int32, current.RandomExamResultId);
                    db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, current.OrganizationId);
                    db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, current.RandomExamId);
                    db.AddInParameter(dbCommand, "p_examinee_id", DbType.Int32, current.ExamineeId);
                    db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, current.BeginDateTime);
                    db.AddInParameter(dbCommand, "p_current_time", DbType.DateTime, current.CurrentDateTime);
                    db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, current.EndDateTime);
                    db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, current.ExamTime);
                    db.AddInParameter(dbCommand, "p_auto_score", DbType.Decimal, current.AutoScore);
                    db.AddInParameter(dbCommand, "p_score", DbType.Decimal, current.Score);
                    db.AddInParameter(dbCommand, "p_correct_rate", DbType.Decimal, current.CorrectRate);
                    db.AddOutParameter(dbCommand, "p_is_pass", DbType.Int32, 4);
                    db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, current.StatusId);
                    db.AddInParameter(dbCommand, "p_memo", DbType.String, current.Memo);
                    db.AddInParameter(dbCommand, "p_exam_seq_no", DbType.Int32, current.ExamSeqNo);

                    db.ExecuteNonQuery(dbCommand, transaction);

                    //将正在进行中考试直接移动到正式表
                    DbCommand dbCommand1 = db.GetStoredProcCommand("USP_Random_EXAM_Result_Remove");
                    db.AddInParameter(dbCommand1, "p_random_result_id", DbType.Int32, current.RandomExamResultId);
                    db.AddOutParameter(dbCommand1, "p_next_id", DbType.Int32, 4);

                    db.ExecuteNonQuery(dbCommand1, transaction);
                    id = Convert.ToInt32(db.GetParameterValue(dbCommand1, "p_next_id"));

					#region 上传路局
					//if (!isServerCenter)
					//{
					//    RandomExamResult randomExamResult = new RandomExamResult();
					//    DbCommand dbCommand2 = db.GetStoredProcCommand("USP_Random_EXAM_RESULT_s");
					//    db.AddInParameter(dbCommand2, "p_Random_exam_result_id", DbType.Int32, id);
					//    using (IDataReader dataReader = db.ExecuteReader(dbCommand2, transaction))
					//    {
					//        while (dataReader.Read())
					//        {
					//            randomExamResult = CreateModelObject(dataReader);
					//            randomExamResult.WorkNo = DataConvert.ToString(dataReader[GetMappingFieldName("WorkNo")]);
					//            randomExamResult.PostName = DataConvert.ToString(dataReader[GetMappingFieldName("PostName")]);
					//        }
					//    }

					//    DbCommand dbCommand3 = db.GetStoredProcCommand("USP_Random_EXAM_RESULT_I_Other");
					//    db.AddOutParameter(dbCommand3, "p_Random_exam_result_id", DbType.Int32, 4);
					//    db.AddInParameter(dbCommand3, "p_org_id", DbType.Int32, randomExamResult.OrganizationId);
					//    db.AddInParameter(dbCommand3, "p_random_exam_id", DbType.Int32, randomExamResult.RandomExamId);
					//    db.AddInParameter(dbCommand3, "p_examinee_id", DbType.Int32, randomExamResult.ExamineeId);
					//    db.AddInParameter(dbCommand3, "p_begin_time", DbType.DateTime, randomExamResult.BeginDateTime);
					//    db.AddInParameter(dbCommand3, "p_current_time", DbType.DateTime, randomExamResult.CurrentDateTime);
					//    db.AddInParameter(dbCommand3, "p_end_time", DbType.DateTime, randomExamResult.EndDateTime);
					//    db.AddInParameter(dbCommand3, "p_exam_time", DbType.Int32, randomExamResult.ExamTime);
					//    db.AddInParameter(dbCommand3, "p_auto_score", DbType.Decimal, randomExamResult.AutoScore);
					//    db.AddInParameter(dbCommand3, "p_score", DbType.Decimal, randomExamResult.Score);
					//    db.AddInParameter(dbCommand3, "p_correct_rate", DbType.Decimal, randomExamResult.CorrectRate);
					//    db.AddInParameter(dbCommand3, "p_is_pass", DbType.Int32, 0);
					//    db.AddInParameter(dbCommand3, "p_status_id", DbType.Int32, randomExamResult.StatusId);
					//    db.AddInParameter(dbCommand3, "p_memo", DbType.String, randomExamResult.Memo);
					//    db.AddInParameter(dbCommand3, "p_random_exam_result_id_other", DbType.Int32, randomExamResult.RandomExamResultId);
					//    db.ExecuteNonQuery(dbCommand3, transaction);

					//}
					#endregion
				}

                //if(!isServerCenter)
                //{
                //    DbCommand dbCommand4 = db.GetStoredProcCommand("USP_RANDOM_EXAM_RESULT_SERVER");

                //    db.AddInParameter(dbCommand4, "p_org_id", DbType.Int32, Convert.ToInt32(ConfigurationManager.AppSettings["StationID"]));
                //    db.AddInParameter(dbCommand4, "p_random_exam_id", DbType.Int32, randomExamID);

                //    db.ExecuteNonQuery(dbCommand4, transaction);
                //}

                //先将中间表已完成考试移到正式表中
                DbCommand dbCommand6 = db.GetStoredProcCommand("USP_Random_EXAM_Result_Re_T1");
                db.AddInParameter(dbCommand6, "p_random_exam_id", DbType.Int32, randomExamID);
                db.ExecuteNonQuery(dbCommand6, transaction);

				//结束考试后，删除临时表中剩余的考卷
				DbCommand dbCommand5 = db.GetStoredProcCommand("USP_Random_EXAM_RESULT_Cur_D");
				db.AddInParameter(dbCommand5, "p_random_exam_id", DbType.Int32, randomExamID);
				db.ExecuteNonQuery(dbCommand5,transaction);

                transaction.Commit();
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            connection.Close();
        }

		public void DeleteRandomExamResult(int resultID)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_RANDOM_EXAM_Result_D";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_random_exam_result_id", DbType.Int32, resultID);

			db.ExecuteNonQuery(dbCommand);
		}

		/// <summary>
		/// 结束路局正在进行的考试
		/// </summary>
		/// <param name="objResultCurrent"></param>
		/// <param name="randomExamID"></param>
		public void RemoveResultAnswerAfterEndCenter(IList<RandomExamResultCurrent> objResultCurrent, int randomExamID)
		{
			int id = 0;
			Database db = DatabaseFactory.CreateDatabase();

			DbConnection connection = db.CreateConnection();
			connection.Open();
			DbTransaction transaction = connection.BeginTransaction();

			try
			{
				foreach (RandomExamResultCurrent current in objResultCurrent)
				{
					DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_RESULT_Cur_U_C");
					db.AddInParameter(dbCommand, "p_Random_exam_result_id", DbType.Int32, current.RandomExamResultId);
					db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, current.OrganizationId);
					db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, current.RandomExamId);
					db.AddInParameter(dbCommand, "p_examinee_id", DbType.Int32, current.ExamineeId);
					db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, current.BeginDateTime);
					db.AddInParameter(dbCommand, "p_current_time", DbType.DateTime, current.CurrentDateTime);
					db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, current.EndDateTime);
					db.AddInParameter(dbCommand, "p_exam_time", DbType.Int32, current.ExamTime);
					db.AddInParameter(dbCommand, "p_auto_score", DbType.Decimal, current.AutoScore);
					db.AddInParameter(dbCommand, "p_score", DbType.Decimal, current.Score);
					db.AddInParameter(dbCommand, "p_correct_rate", DbType.Decimal, current.CorrectRate);
					db.AddOutParameter(dbCommand, "p_is_pass", DbType.Int32, 4);
					db.AddInParameter(dbCommand, "p_status_id", DbType.Int32, current.StatusId);
					db.AddInParameter(dbCommand, "p_memo", DbType.String, current.Memo);
					db.AddInParameter(dbCommand, "p_exam_seq_no", DbType.Int32, current.ExamSeqNo);

					db.ExecuteNonQuery(dbCommand, transaction);

					DbCommand dbCommand1 = db.GetStoredProcCommand("USP_Random_EXAM_Result_R_C");
					db.AddInParameter(dbCommand1, "p_random_result_id", DbType.Int32, current.RandomExamResultId);
					db.AddOutParameter(dbCommand1, "p_next_id", DbType.Int32, 4);

					db.ExecuteNonQuery(dbCommand1, transaction);
					id = Convert.ToInt32(db.GetParameterValue(dbCommand1, "p_next_id"));
				}

				DbCommand dbCommand4 = db.GetStoredProcCommand("USP_RANDOM_EXAM_RESULT_DOWN");
				db.AddInParameter(dbCommand4, "p_random_exam_id", DbType.Int32, randomExamID);
				db.ExecuteNonQuery(dbCommand4, transaction);

				transaction.Commit();
			}
			catch (Exception ex)
			{
				transaction.Rollback();
				throw ex;
			}
			connection.Close();
		}



        public IList<RandomExamResult> GetRandomExamResults(int ExamId, string OrganizationName, string workShopName,string strExamineeName,string workno, decimal dScoreLower,
                      decimal dScoreUpper, int orgID)
        {

            IList<RandomExamResult> examResults = new List<RandomExamResult>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_RESULT_f";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_exam_id", DbType.Int32, ExamId);
            db.AddInParameter(dbCommand, "p_Org_name", DbType.String, OrganizationName);
            db.AddInParameter(dbCommand, "p_work_shop", DbType.String, workShopName);
            db.AddInParameter(dbCommand, "p_examinee_name", DbType.String, strExamineeName);
			db.AddInParameter(dbCommand, "p_work_no", DbType.String, workno);
            db.AddInParameter(dbCommand, "p_score_lower", DbType.Decimal, dScoreLower);
            db.AddInParameter(dbCommand, "p_score_upper", DbType.Decimal, dScoreUpper);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);


            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                RandomExamResult er = null;
                while (dataReader.Read())
                {
                    er = CreateModelObject(dataReader);
                    er.WorkNo = DataConvert.ToString(dataReader[GetMappingFieldName("WorkNo")]);
                    er.PostName = DataConvert.ToString(dataReader[GetMappingFieldName("PostName")]);
                    er.ExamOrgName = DataConvert.ToString(dataReader[GetMappingFieldName("ExamOrgName")]);
                    examResults.Add(er);                     
                }
            }
            return examResults;
        }

		public IList<RandomExamResult> GetRandomExamResults(string strWhere)
		{
			IList<RandomExamResult> examResults = new List<RandomExamResult>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_EXAM_RESULT_Where";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_sql", DbType.String, strWhere);
			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				RandomExamResult er = null;
				while (dataReader.Read())
				{
					er = CreateModelObject(dataReader);
					er.WorkNo = DataConvert.ToString(dataReader[GetMappingFieldName("WorkNo")]);
					er.PostName = DataConvert.ToString(dataReader[GetMappingFieldName("PostName")]);
					er.ExamOrgName = DataConvert.ToString(dataReader[GetMappingFieldName("ExamOrgName")]);
					er.ExamStyleName = DataConvert.ToString(dataReader[GetMappingFieldName("ExamStyleName")]);
					examResults.Add(er);
				}
			}
			return examResults;
		}

		public IList<RandomExamResult> GetRandomExamResultsFromServer(int ExamId, string OrganizationName, string strExamineeName, decimal dScoreLower,
			  decimal dScoreUpper, int orgID)
		{

			IList<RandomExamResult> examResults = new List<RandomExamResult>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_EXAM_RESULT_f_Ser";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_Random_exam_id", DbType.Int32, ExamId);
			db.AddInParameter(dbCommand, "p_Org_name", DbType.String, OrganizationName);
			db.AddInParameter(dbCommand, "p_examinee_name", DbType.String, strExamineeName);
			db.AddInParameter(dbCommand, "p_score_lower", DbType.Decimal, dScoreLower);
			db.AddInParameter(dbCommand, "p_score_upper", DbType.Decimal, dScoreUpper);
			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);


			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				RandomExamResult er = null;
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


        public void DeleteRandomExamResultServer(int randomExamID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_RANDOM_EXAM_RESULT_SER_DEL");

            db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, randomExamID);

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

        public void InsertRandomExamResultServer(int randomExamResultId, int serverId,int examid)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_RANDOM_EXAM_RESULT_SER_INS");

            db.AddInParameter(dbCommand, "p_result_id", DbType.Int32, randomExamResultId);
            db.AddInParameter(dbCommand, "p_server_id", DbType.Int32, serverId);
            db.AddInParameter(dbCommand, "p_exam_id", DbType.Int32, examid);


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

        public void InsertRandomExamResultAnswerServer(int examid,int randomExamResultId, int serverId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_RANDOM_EXAM_RESULT_ANS_INS");

            db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, examid);
            db.AddInParameter(dbCommand, "p_result_id", DbType.Int32, randomExamResultId);
            db.AddInParameter(dbCommand, "p_server_id", DbType.Int32, serverId);

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

        public static RandomExamResult CreateModelObject(IDataReader dataReader)
        {
            return new RandomExamResult(
                DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamResultId")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrganizationId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("OrganizationName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ExamName")]),         
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamineeId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("ExamineeName")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("BeginDateTime")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("CurrentDateTime")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("EndDateTime")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamTime")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("AutoScore")]),
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("Score")]),             
                DataConvert.ToDecimal(dataReader[GetMappingFieldName("CorrectRate")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("IsPass")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("StatusId")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("StatusName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamResultIDStation")]));
        }
    }
}
