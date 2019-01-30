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
	public class RandomExamCountStatisticDAL
	{
		private static Hashtable _ormTable;
        private int _recordCount = 0;

		static RandomExamCountStatisticDAL()
        {
            _ormTable = new Hashtable();

            // 源名称必须小写
            _ormTable.Add("orgid", "ORG_ID");
            _ormTable.Add("orgname", "ORG_NAME");
            _ormTable.Add("examcount", "EXAM_COUNT");
            _ormTable.Add("employeecount", "EMPLOYEE_COUNT");
        }

        public IList<RandomExamCountStatistic> GetCountWithOrg(int SuitRange, int OrgID, DateTime DateFrom, DateTime DateTo, int railSystemId,int style)
		{
			IList<RandomExamCountStatistic> objList = new List<RandomExamCountStatistic>();
			Database db = DatabaseFactory.CreateDatabase();

			//DbCommand dbCommand = db.GetStoredProcCommand("USP_EXAM_GRADE_Org_Count");

			string sqlCommand = "USP_EXAM_GRADE_Org_Count";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
			db.AddInParameter(dbCommand, "p_random_exam_suitrange", DbType.Int32, SuitRange);
			db.AddInParameter(dbCommand,"p_random_exam_orgid",DbType.Int32,OrgID);
			db.AddInParameter(dbCommand, "p_random_exam_dateFrom", DbType.DateTime, DateFrom);
			db.AddInParameter(dbCommand, "p_random_exam_dateTo", DbType.DateTime, DateTo);
            db.AddInParameter(dbCommand, "p_rail_system_id", DbType.Int32, railSystemId);
            db.AddInParameter(dbCommand, "p_exam_style", DbType.Int32, style);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					RandomExamCountStatistic obj = CreateModelObject(dataReader);

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

		public static RandomExamCountStatistic CreateModelObject(IDataReader dataReader)
		{
			return new RandomExamCountStatistic(
				DataConvert.ToInt(dataReader[GetMappingFieldName("OrgID")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("OrgName")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("ExamCount")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("EmployeeCount")]));
		}
	}
}
