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
    public class RandomExamResultCurrentDAL
    {
         private static Hashtable _ormTable;
        private int _recordCount = 0;

        /// <summary>
        /// 空参数构造函数
        /// </summary>
        public RandomExamResultCurrentDAL()
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
            _ormTable.Add("examseqno","EXAM_SEQ_NO");
        }

        public int AddRandomExamResultCurrent(RandomExamResultCurrent examResult)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_RESULT_Cur_I");

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
            db.AddInParameter(dbCommand, "p_exam_seq_no", DbType.Int32, examResult.ExamSeqNo);
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

        public int UpdateRandomExamResultCurrent(RandomExamResultCurrent examResult)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_RESULT_Cur_U");

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
            db.AddInParameter(dbCommand, "p_exam_seq_no", DbType.Int32, examResult.ExamSeqNo);
            return db.ExecuteNonQuery(dbCommand);
        }

		public void UpdateRandomExamResultCurrent(IList<RandomExamResultCurrent> objList)
		{
			Database db = DatabaseFactory.CreateDatabase();

			DbConnection connection = db.CreateConnection();
			connection.Open();
			DbTransaction transaction = connection.BeginTransaction();

			try
			{
				foreach (RandomExamResultCurrent current in objList)
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

                    DbCommand dbCommand1 = db.GetStoredProcCommand("USP_Random_EXAM_Result_Re_C");
					db.AddInParameter(dbCommand1, "p_random_result_id", DbType.Int32, current.RandomExamResultId);
					db.AddOutParameter(dbCommand1, "p_next_id", DbType.Int32, 4);
					db.ExecuteNonQuery(dbCommand1, transaction);
				}
				transaction.Commit();
			}
			catch (System.SystemException ex)
			{
				transaction.Rollback();
				throw ex;

			}
			connection.Close();
		}


		public void UpdateRandomExamResultCurrentCenter(IList<RandomExamResultCurrent> objList)
		{
			Database db = DatabaseFactory.CreateDatabase();

			DbConnection connection = db.CreateConnection();
			connection.Open();
			DbTransaction transaction = connection.BeginTransaction();

			try
			{
				foreach (RandomExamResultCurrent current in objList)
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
				}
				transaction.Commit();
			}
			catch (System.SystemException ex)
			{
				transaction.Rollback();
				throw ex;

			}
			connection.Close();
		}


    	public IList<RandomExamResultCurrent> GetRandomExamResultInfo(int examID)
        {
            IList<RandomExamResultCurrent> examResults = new List<RandomExamResultCurrent>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_RESULT_Cur_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_exam_id", DbType.Int32, examID);


            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                	RandomExamResultCurrent obj = CreateModelObject(dataReader);
					obj.WorkNo = DataConvert.ToString(dataReader[GetMappingFieldName("WorkNo")]);
					examResults.Add(obj);
				}
            }

            return examResults;
        }

        public IList<RandomExamResultCurrent> GetRandomExamResultInfoByExamID(int examID,string sql)
        {
            IList<RandomExamResultCurrent> examResults = new List<RandomExamResultCurrent>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_RESULT_Cur_Q1";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_Random_exam_id", DbType.Int32, examID);
            db.AddInParameter(dbCommand, "p_sql", DbType.String, sql);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    RandomExamResultCurrent obj = CreateModelObject(dataReader);
                    obj.WorkNo = DataConvert.ToString(dataReader[GetMappingFieldName("WorkNo")]);
                    examResults.Add(obj);
                }
            }

            return examResults;
        }

		/// <summary>
		/// 查找本地正在进行的考试
		/// </summary>
		/// <param name="examID"></param>
		/// <returns></returns>
        public IList<RandomExamResultCurrent> GetStartRandomExamResultInfo(int examID)
        {
            IList<RandomExamResultCurrent> examResults = new List<RandomExamResultCurrent>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_RESULT_Cur_S";
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

		/// <summary>
		///  查找路局正在进行的考试
		/// </summary>
		/// <param name="examID"></param>
		/// <returns></returns>
		public IList<RandomExamResultCurrent> GetCenterStartRandomExamResultInfo(int examID)
		{
			IList<RandomExamResultCurrent> examResults = new List<RandomExamResultCurrent>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_EXAM_RESULT_Cur_S_C";
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

        public RandomExamResultCurrent GetRandomExamResult(int examResultID)
        {
            RandomExamResultCurrent RandomExamResult = new RandomExamResultCurrent();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_RESULT_Cur_G";
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

        public RandomExamResultCurrent GetNowRandomExamResultInfo(int employeeid,int examid)
        {
            RandomExamResultCurrent RandomExamResult = new RandomExamResultCurrent();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_Random_EXAM_RESULT_Cur_N";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employeeid);
            db.AddInParameter(dbCommand, "p_Random_exam_id", DbType.Int32, examid);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    RandomExamResult = CreateModelObject(dataReader);
                }
            }

            return RandomExamResult;
        }

        public void DelRandomExamResultCurrent(int randomExamID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_RESULT_Cur_D");

            db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, randomExamID);
            db.ExecuteNonQuery(dbCommand);
        }

        public void DelRandomExamResultCurrentByResultID(int employeeID,int resultID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_RESULT_Cur_D1");

            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employeeID);
            db.AddInParameter(dbCommand, "p_random_exam_result_id", DbType.Int32, resultID);
            db.ExecuteNonQuery(dbCommand);
        }

        public void ClearRandomExamResultCurrentByResultID(int employeeID, int resultID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_RESULT_D2");

            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employeeID);
            db.AddInParameter(dbCommand, "p_random_exam_result_id", DbType.Int32, resultID);
            db.ExecuteNonQuery(dbCommand);
        }

        public void ReplyRandomExamResultCurrentByResultID(int employeeID, int resultID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_RESULT_D3");

            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employeeID);
            db.AddInParameter(dbCommand, "p_random_exam_result_id", DbType.Int32, resultID);
            db.ExecuteNonQuery(dbCommand);
        }

        public void RemoveRandomExamResultTemp(int randomExamID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_Result_Re_T1");

            db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, randomExamID);
            db.ExecuteNonQuery(dbCommand);
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



        public static RandomExamResultCurrent CreateModelObject(IDataReader dataReader)
        {
            return new RandomExamResultCurrent(
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
                DataConvert.ToInt(dataReader[GetMappingFieldName("ExamSeqNo")]));
        }
    }
}
