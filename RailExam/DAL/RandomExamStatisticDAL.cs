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
	public class RandomExamStatisticDAL
	{
		private static Hashtable _ormTable;
        private int _recordCount = 0;

		static RandomExamStatisticDAL()
        {
            _ormTable = new Hashtable();

            // 源名称必须小写
            _ormTable.Add("itemid", "ITEM_ID");
            _ormTable.Add("content", "CONTENT");
            _ormTable.Add("bookname", "BOOK_NAME");
            _ormTable.Add("chaptername", "CHAPTER_NAME");
            _ormTable.Add("errornum", "ERROR_NUM");
			_ormTable.Add("examcount","EXAM_COUNT");
			_ormTable.Add("errorrate", "ERROR_RATE");
			_ormTable.Add("employeeid","EMPLOYEE_ID");
			_ormTable.Add("employeename", "EMPLOYEE_NAME");
			_ormTable.Add("workno", "WORK_NO");
			_ormTable.Add("orgname", "ORG_NAME");
			_ormTable.Add("examname", "EXAM_NAME");
			_ormTable.Add("answer", "ANSWER");
            _ormTable.Add("standardanswer", "STANDARD_ANSWER");
			_ormTable.Add("score","SCORE");
			_ormTable.Add("randomexamresultid","RANDOM_EXAM_RESULT_ID");
			_ormTable.Add("orgid","ORG_ID");
            _ormTable.Add("randomexamitemid","RANDOM_EXAM_ITEM_ID");
            _ormTable.Add("randomexamid", "RANDOM_EXAM_ID");
            _ormTable.Add("selectanswer", "SELECT_ANSWER");
        }

		public IList<RandomExamStatistic> GetErrorItemInfo(int bookID,int chapterID,int type,int randomExamID,DateTime beginTime,DateTime endTime,int orgID)
		{
			IList<RandomExamStatistic> objList = new List<RandomExamStatistic>();
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_Exam_Statistic");
			if(bookID !=0)
			{
				db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookID);
			}
			if(chapterID != 0)
			{
				db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, chapterID);
			}
			db.AddInParameter(dbCommand, "p_type_id", DbType.Int32,type);
			if(randomExamID != 0)
			{
				db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, randomExamID);
			}
			db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime,beginTime);
			db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, endTime);
			if(orgID != 0)
			{
				db.AddInParameter(dbCommand,"p_org_id",DbType.Int32,orgID);
			}

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					RandomExamStatistic obj = CreateModelObject(dataReader);
                    obj.RandomExamItemID = Convert.ToInt32(dataReader[GetMappingFieldName("RandomExamItemID")].ToString());
                    obj.RandomExamID = Convert.ToInt32(dataReader[GetMappingFieldName("RandomExamID")].ToString());
					objList.Add(obj);
				}
			}
			return objList;
		}

		public IList<RandomExamStatistic> GetErrorItemInfoByEmployeeID(int EmployeeID, DateTime beginTime, DateTime endTime)
		{
			IList<RandomExamStatistic> objList = new List<RandomExamStatistic>();
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_Exam_Statistic_User");

			db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, EmployeeID);
			if(beginTime==DateTime.Today && endTime==DateTime.Today && beginTime==endTime)
			{
				beginTime =Convert.ToDateTime("1000-01-01");
				endTime = Convert.ToDateTime("1000-01-01");
			}
			db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, beginTime);
			db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, endTime);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					RandomExamStatistic obj = CreateModelObject(dataReader);
                    obj.RandomExamItemID = Convert.ToInt32(dataReader[GetMappingFieldName("RandomExamItemID")].ToString());
                    obj.RandomExamID = Convert.ToInt32(dataReader[GetMappingFieldName("RandomExamID")].ToString());
					objList.Add(obj);
				}
			}
			return objList;
		}

		public IList<RandomExamStatistic> GetErrorItemInfoByItemID(int bookID, int chapterID, int type, int randomExamID, DateTime beginTime, DateTime endTime,int orgID,int employeeID,int ItemID)
		{
			IList<RandomExamStatistic> objList = new List<RandomExamStatistic>();
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetStoredProcCommand("USP_Random_Exam_Statistic_Item");

			if (bookID != 0)
			{
				db.AddInParameter(dbCommand, "p_book_id", DbType.Int32, bookID);
			}
			if (chapterID != 0)
			{
				db.AddInParameter(dbCommand, "p_chapter_id", DbType.Int32, chapterID);
			} 
			db.AddInParameter(dbCommand, "p_type_id", DbType.Int32, type);
			if (randomExamID != 0)
			{
				db.AddInParameter(dbCommand, "p_random_exam_id", DbType.Int32, randomExamID);
			}
			db.AddInParameter(dbCommand, "p_begin_time", DbType.DateTime, beginTime);
			db.AddInParameter(dbCommand, "p_end_time", DbType.DateTime, endTime);
			if (orgID != 0)
			{
				db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
			}
			if (employeeID != 0)
			{
				db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employeeID);
			}
			db.AddInParameter(dbCommand, "p_item_id", DbType.Int32, ItemID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					RandomExamStatistic obj = CreateModelObject(dataReader);
					obj.EmployeeID = Convert.ToInt32(dataReader[GetMappingFieldName("EmployeeID")].ToString());
					obj.EmployeeName = dataReader[GetMappingFieldName("EmployeeName")].ToString();
					obj.WorkNo = dataReader[GetMappingFieldName("WorkNo")].ToString();
					obj.OrgName = dataReader[GetMappingFieldName("OrgName")].ToString();
					obj.ExamName = dataReader[GetMappingFieldName("ExamName")].ToString();
					obj.Answer = dataReader[GetMappingFieldName("Answer")].ToString();
                    obj.StandardAnswer = dataReader[GetMappingFieldName("StandardAnswer")].ToString();
                    obj.SelectAnswer = dataReader[GetMappingFieldName("SelectAnswer")].ToString();
					obj.Score = Convert.ToDecimal(dataReader[GetMappingFieldName("Score")].ToString());
					obj.RandomExamResultID = Convert.ToInt32(dataReader[GetMappingFieldName("RandomExamResultID")].ToString());
					obj.OrgID = Convert.ToInt32(dataReader[GetMappingFieldName("OrgID")].ToString());
                    obj.RandomExamItemID = Convert.ToInt32(dataReader[GetMappingFieldName("RandomExamItemID")].ToString());
					objList.Add(obj);
				}
			}
			return objList;
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

		public static RandomExamStatistic CreateModelObject(IDataReader dataReader)
		{
			return new RandomExamStatistic(
				DataConvert.ToInt(dataReader[GetMappingFieldName("ItemID")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("Content")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("BookName")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("ChapterName")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("ErrorNum")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("ExamCount")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("ErrorRate")]));
		}
	}
}
