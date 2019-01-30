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
	public class RandomExamApplyDAL
	{
		private static Hashtable _ormTable;
        private int _recordCount = 0;

		static RandomExamApplyDAL()
        {
            _ormTable = new Hashtable();

            // 源名称必须小写
            _ormTable.Add("randomexamapplyid", "RANDOM_EXAM_APPLY_ID");
            _ormTable.Add("randomexamid", "RANDOM_EXAM_ID");
            _ormTable.Add("randomexamresultcurid", "RANDOM_EXAM_RESULT_CUR_ID");
            _ormTable.Add("randomexamresultid", "RANDOM_EXAM_RESULT_ID");
            _ormTable.Add("codeflag", "CODE_FLAG");
			_ormTable.Add("applystatus","APPLY_STATUS");
			_ormTable.Add("applystatusname","APPLY_STATUS_NAME");
			_ormTable.Add("employeename","EMPLOYEE_NAME");
			_ormTable.Add("workno","WORK_NO");
			_ormTable.Add("orgname","ORG_NAME");
			_ormTable.Add("ipaddress","IP_ADDRESS");
			_ormTable.Add("employeeid","EMPLOYEE_ID");
			_ormTable.Add("examname", "EXAM_NAME");
			_ormTable.Add("randomexamcode", "RANDOM_EXAM_CODE");
        }

		public int AddRandomExamApply(RandomExamApply exam)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_Apply_I";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddOutParameter(dbCommand, "p_random_exam_apply_Id", DbType.Int32, exam.RandomExamApplyID);
			db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, exam.RandomExamID);
			db.AddInParameter(dbCommand, "p_random_exam_result_cur_id", DbType.Int32, exam.RandomExamResultCurID);
			db.AddInParameter(dbCommand, "p_code_flag", DbType.Int32, exam.CodeFlag ? 1:0);
			db.AddInParameter(dbCommand, "p_apply_status", DbType.Int32, exam.ApplyStatus);
			db.AddInParameter(dbCommand, "p_ip_address", DbType.String, exam.IPAddress);

			int id = 0;

			DbConnection connection = db.CreateConnection();
			connection.Open();
			DbTransaction transaction = connection.BeginTransaction();

			try
			{
				db.ExecuteNonQuery(dbCommand, transaction);
				id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_random_exam_apply_Id"));

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

		public void UpdateRandomExamApply(RandomExamApply exam)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_Apply_U";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_random_exam_apply_Id", DbType.Int32, exam.RandomExamApplyID);
			db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, exam.RandomExamID);
			db.AddInParameter(dbCommand, "p_random_exam_result_cur_id", DbType.Int32, exam.RandomExamResultCurID);
			db.AddInParameter(dbCommand, "p_code_flag", DbType.Int32, exam.CodeFlag ? 1 : 0);
			db.AddInParameter(dbCommand, "p_apply_status", DbType.Int32, exam.ApplyStatus);
			db.AddInParameter(dbCommand, "p_random_exam_result_id", DbType.Int32, exam.ApplyStatus);
			db.AddInParameter(dbCommand, "p_ip_address", DbType.String, exam.IPAddress);

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

		public RandomExamApply GetRandomExamApply(int applyID)
		{
			RandomExamApply obj = new RandomExamApply();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_Apply_G";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_random_exam_apply_Id", DbType.Int32, applyID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					obj = CreateModelObject(dataReader);
				}
			}

			return obj;
		}

		public void DelRandomExamApply(int applyID)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_Apply_D";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_random_exam_apply_Id", DbType.Int32, applyID);

			db.ExecuteNonQuery(dbCommand);
		}

		public void DelRandomExamApplyByExamID(int examID)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_Apply_D_Exam";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, examID);

			db.ExecuteNonQuery(dbCommand);
		}

		public void DelRandomExamApplyByIPAddress(string strIP)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_Apply_D_IP";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_ip_address", DbType.String, strIP);

			db.ExecuteNonQuery(dbCommand);
		}

		public void UpdateRandomExamApplyStatus(int applyID,int applyStatus)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_Random_Exam_Apply_U_STATUS";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_random_exam_apply_Id", DbType.Int32, applyID);
			db.AddInParameter(dbCommand, "p_apply_status", DbType.Int32, applyStatus);

			db.ExecuteNonQuery(dbCommand);
		}

		public IList<RandomExamApply> GetRandomExamApplyByExamID(int examID)
		{
			IList<RandomExamApply> objList = new List<RandomExamApply>();
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_Apply_S");

			db.AddInParameter(dbCommand, "p_exam_id", DbType.String, examID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					RandomExamApply obj = CreateModelObject(dataReader);
					obj.IsChecked = false;
					objList.Add(obj);
				}
			}

			return objList;
		}

		public IList<RandomExamApply> GetRandomExamApplyByOrgID(int orgID,string serverNo)
		{
			IList<RandomExamApply> objList = new List<RandomExamApply>();
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_Apply_S_ALL");

			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
            db.AddInParameter(dbCommand, "p_server_no", DbType.String, serverNo);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					RandomExamApply obj = CreateModelObject(dataReader);
					obj.IsChecked = false;
					obj.ExamName = dataReader[GetMappingFieldName("ExamName")].ToString();
					obj.RandomExamCode = dataReader[GetMappingFieldName("RandomExamCode")].ToString();
					objList.Add(obj);
				}
			}

			return objList;
		}

		public IList<RandomExamApply> GetRandomExamApplyByIPAddress(string strIP)
		{
			IList<RandomExamApply> objList = new List<RandomExamApply>();
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_Apply_G_IP");

			db.AddInParameter(dbCommand, "p_ip_address", DbType.String, strIP);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					RandomExamApply obj = CreateModelObject(dataReader);
					obj.IsChecked = false;
					objList.Add(obj);
				}
			}

			return objList;
		}

		public RandomExamApply GetRandomExamApplyByExamResultCurID(int examresultID)
		{
			RandomExamApply obj = new RandomExamApply();
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_EXAM_Apply_G_CurID");

			db.AddInParameter(dbCommand, "p_exam_result_id", DbType.String, examresultID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					obj = CreateModelObject(dataReader);
				}
			}

			return obj;
		}

		/// <summary>
		/// 查询结果记录数
		/// </summary>
		public int RecordCount
		{
			get
			{
				return _recordCount;
			}
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
						mappingOrderBy += GetMappingFieldName(orderBysOfOneCondition[0]) + ' ' + orderBysOfOneCondition[1];
					}
				}
			}

			return mappingOrderBy;
		}

		public static RandomExamApply CreateModelObject(IDataReader dataReader)
		{
			return new RandomExamApply(
				DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamApplyID")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamID")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamResultCurID")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("RandomExamResultID")]),
				DataConvert.ToBool(dataReader[GetMappingFieldName("CodeFlag")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("ApplyStatus")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("ApplyStatusName")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("EmployeeName")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("WorkNo")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("OrgName")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("IPAddress")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("EmployeeID")]));
		}
	}
}
