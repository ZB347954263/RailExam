using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
	public class EducationEmployeeTypeDAL
	{
		private static Hashtable _ormTable;
		private int _recordCount = 0;

		static EducationEmployeeTypeDAL()
		{
			_ormTable = new Hashtable();

			_ormTable.Add("educationemployeetypeid", "EDUCATION_EMPLOYEE_TYPE_ID");
			_ormTable.Add("educationemployeetypename", "EDUCATION_EMPLOYEE_TYPE_Name");
		}
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

		public IList<EducationEmployeeType> GetAllEducationEmployeeType()
		{
			IList<EducationEmployeeType> objList = new List<EducationEmployeeType>();

			Database db = DatabaseFactory.CreateDatabase();
			string sql = "select * from zj_education_employee_type order by education_employee_type_id";
			using (IDataReader dataReader = db.ExecuteReader(CommandType.Text,sql))
			{
				while (dataReader.Read())
				{
					EducationEmployeeType obj = CreateModelObject(dataReader);
					objList.Add(obj);
				}
			}

			return objList;
		}
		public IList<EducationEmployeeType> GetAllEducationEmployeeTypeByWhereClause(string sql)
		{
			IList<EducationEmployeeType> objList = new List<EducationEmployeeType>();
			Database db = DatabaseFactory.CreateDatabase();
			string sqlStr = string.Format("select * from zj_education_employee_type where 1=1 and {0}",sql);
			using (IDataReader dataReader = db.ExecuteReader(CommandType.Text, sqlStr))
			{
				while (dataReader.Read())
				{
					EducationEmployeeType obj = CreateModelObject(dataReader);
					objList.Add(obj);
				}
			}

			return objList;
		}
		public EducationEmployeeType GetEducationEmployeeTypeByEducationEmployeeTypeID(int EducationEmployeeTypeID)
		{
			EducationEmployeeType objList = new EducationEmployeeType();
			Database db = DatabaseFactory.CreateDatabase();
			string sql = string.Format("select * from zj_education_employee_type where Education_Employee_Type_ID={0}", EducationEmployeeTypeID);
			using (IDataReader dataReader = db.ExecuteReader(CommandType.Text,sql))
			{
				while (dataReader.Read())
				{
					objList = CreateModelObject(dataReader);
				}
			}
			return objList;
		}
		public void InsertEducationEmployeeType(EducationEmployeeType obj)
		{
			Database db = DatabaseFactory.CreateDatabase();
			string sql = string.Format("insert into zj_education_employee_type values({0},'{1}')", "EDUCATION_EMPLOYEE_TYPE_SEQ.nextval", obj.TypeName);
			db.ExecuteNonQuery(CommandType.Text, sql);
		}

		public void UpdateEducationEmployeeType(EducationEmployeeType obj)
		{
			Database db = DatabaseFactory.CreateDatabase();
			string sql =string.Format( "update zj_education_employee_type set education_employee_type_name='{0}' where education_employee_type_id={1}",obj.TypeName,obj.EducationEmployeeTypeID);
			db.ExecuteNonQuery(CommandType.Text,sql);
		}

		public void DeleteEducationEmployeeType(int ID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			string sql = string.Format("delete from zj_education_employee_type where education_employee_type_id={0}",ID);
			db.ExecuteNonQuery(CommandType.Text,sql);
		}

		public static EducationEmployeeType CreateModelObject(IDataReader dataReader)
		{
			return new EducationEmployeeType(
				DataConvert.ToInt(dataReader[GetMappingFieldName("educationemployeetypeid")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("educationemployeetypename")]));
				 
		}
	}
}
