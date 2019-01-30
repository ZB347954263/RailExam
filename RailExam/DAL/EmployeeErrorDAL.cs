using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.IO;
using System.Web;
using System.Xml;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
	public class EmployeeErrorDAL
	{
		private static Hashtable _ormTable;
		private int _recordCount = 0;

		static EmployeeErrorDAL()
        {
            _ormTable = new Hashtable();

             _ormTable.Add("employeeerrorid", "EMPLOYEE_ERROR_ID");
            _ormTable.Add("orgid", "ORG_ID");
            _ormTable.Add("importtype", "IMPORT_TYPE");
            _ormTable.Add("excelno", "EXCEL_NO");
            _ormTable.Add("workno", "WORK_NO");
            _ormTable.Add("employeename", "EMPLOYEE_NAME");
			_ormTable.Add("sex", "SEX");
			_ormTable.Add("orgpath", "ORG_PATH");
			_ormTable.Add("postpath", "POST_PATH");
			_ormTable.Add("errorreason", "ERROR_REASON");
			_ormTable.Add("operatemode", "OPERATE_MODE");
			_ormTable.Add("orgname","ORG_NAME");
			_ormTable.Add("groupname","GROUP_NAME");
			_ormTable.Add("identifycode", "IDENTIFY_CODE");
			_ormTable.Add("postno", "POST_NO");
			_ormTable.Add("nativeplace", "NATIVE_PLACE");
			_ormTable.Add("folk", "FOLK");
			_ormTable.Add("wedding", "WEDDING");
			_ormTable.Add("politicalstatus", "POLITICAL_STATUS");
			_ormTable.Add("educationlevel", "EDUCATION_LEVEL");
			_ormTable.Add("graduateuniversity", "GRADUATE_UNIVERSITY");
			_ormTable.Add("studymajor", "STUDY_MAJOR");
			_ormTable.Add("address", "ADDRESS");
			_ormTable.Add("employeelevel", "EMPLOYEE_LEVEL");
			_ormTable.Add("birthday", "BIRTHDAY");
			_ormTable.Add("begindate", "BEGIN_DATE");
			_ormTable.Add("workdate", "WORK_DATE");
			_ormTable.Add("employeetype", "EMPLOYEE_TYPE");
			_ormTable.Add("workgroupleader", "WORK_GROUPLEADER");
			_ormTable.Add("teachertype", "TEACHER_TYPE");
			_ormTable.Add("onpost","ON_POST");
			_ormTable.Add("technicaltitle", "TECHNICAL_TITLE");
			_ormTable.Add("technicalskill", "TECHNICAL_SKILL");
			_ormTable.Add("postcode", "POST_CODE");
			_ormTable.Add("educationemployee", "EDUCATION_EMPLOYEE");
			_ormTable.Add("committeeheadship", "COMMITTEE_HEADSHIP");
			_ormTable.Add("employeetransporttype", "EMPLOYEE_TRANSPORT_TYPE");
			_ormTable.Add("employeeid","EMPLOYEE_ID");
			_ormTable.Add("salaryno","SALARY_NO");
		}


		public void AddEmployeeError(IList<EmployeeError> employeeErrorList)
		{
			 Database db = DatabaseFactory.CreateDatabase();

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
				foreach (EmployeeError employee in employeeErrorList)
				{
					string sqlCommand = "USP_EMPLOYEE_Error_I";
					DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

					db.AddOutParameter(dbCommand, "p_error_id", DbType.Int32, employee.EmployeeErrorID);
					db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, employee.OrgID);
                    //db.AddInParameter(dbCommand, "p_import_type", DbType.Int32, employee.ImportType);
					db.AddInParameter(dbCommand, "p_excel_no", DbType.Int32, employee.ExcelNo);
					db.AddInParameter(dbCommand, "p_work_no", DbType.String, employee.WorkNo);
					db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employee.EmployeeName);
					db.AddInParameter(dbCommand, "p_sex", DbType.String, employee.Sex);
					db.AddInParameter(dbCommand, "p_org_path", DbType.String, employee.OrgPath);
					db.AddInParameter(dbCommand, "p_post_path", DbType.String, employee.PostPath);
					db.AddInParameter(dbCommand, "p_error_reason", DbType.String, employee.ErrorReason);
                    //db.AddInParameter(dbCommand, "p_operate_mode", DbType.Int32, employee.OperateMode);
					db.AddInParameter(dbCommand, "p_org_name", DbType.String, employee.OrgName);
					db.AddInParameter(dbCommand, "p_group_name", DbType.String, employee.GroupName);
					db.AddInParameter(dbCommand, "p_identify_code", DbType.String, employee.IdentifyCode);
					db.AddInParameter(dbCommand, "p_post_no", DbType.String, employee.PostNo);
					db.AddInParameter(dbCommand, "p_native_place", DbType.String, employee.NativePlace);
					db.AddInParameter(dbCommand, "p_folk", DbType.String, employee.Folk);
					db.AddInParameter(dbCommand, "p_wedding", DbType.String, employee.Wedding);
					db.AddInParameter(dbCommand, "p_political_status", DbType.String, employee.PoliticalStatus);
					db.AddInParameter(dbCommand, "p_education_level", DbType.String, employee.EducationLevel);
					db.AddInParameter(dbCommand, "p_graduate_university", DbType.String, employee.GraduateUniversity);
					db.AddInParameter(dbCommand, "p_study_major", DbType.String, employee.StudyMajor);
					db.AddInParameter(dbCommand, "p_address", DbType.String, employee.Address);
					db.AddInParameter(dbCommand, "p_employee_level", DbType.String, employee.EmployeeLevel);
					db.AddInParameter(dbCommand, "p_birthday", DbType.String, employee.Birthday);
					db.AddInParameter(dbCommand, "p_begin_date", DbType.String, employee.BeginDate);
					db.AddInParameter(dbCommand, "p_work_date", DbType.String, employee.WorkDate);
					db.AddInParameter(dbCommand, "p_employee_type", DbType.String, employee.EmployeeType);
					db.AddInParameter(dbCommand, "p_work_groupleader", DbType.String, employee.WorkGroupLeader);
					db.AddInParameter(dbCommand, "p_teacher_type", DbType.String, employee.TeacherType);
					db.AddInParameter(dbCommand, "p_on_post", DbType.String, employee.OnPost);
					db.AddInParameter(dbCommand, "p_technical_title", DbType.String, employee.TechnicalTitle);
					db.AddInParameter(dbCommand, "p_technical_skill", DbType.String, employee.TechnicalSkill);
					db.AddInParameter(dbCommand, "p_post_code", DbType.String, employee.PostCode);
					db.AddInParameter(dbCommand, "p_education_employee", DbType.String, employee.EducationEmployee);
					db.AddInParameter(dbCommand, "p_committee_headship", DbType.String, employee.CommitteeHeadShip);
					db.AddInParameter(dbCommand, "p_employee_transport_type", DbType.String, employee.EmployeeTransportType);
					db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employee.EmployeeID);
                    //db.AddInParameter(dbCommand, "p_salary_no", DbType.String, employee.SalaryNo);

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
		}

		public IList<EmployeeError> GetEmployeeErrorByOrgIDAndImportTypeID(int orgID)
		{
			IList<EmployeeError> objList = new List<EmployeeError>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_EMPLOYEE_Error_G";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					objList.Add(CreateModelObject(dataReader));
				}
			}
			return objList;
		}

		public EmployeeError GetEmployeeError(int employeeErrorID)
		{
			EmployeeError employee = null;

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_EMPLOYEE_Error_S";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_error_id", DbType.Int32, employeeErrorID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				if (dataReader.Read())
				{
					employee = CreateModelObject(dataReader);
				}
			}

			return employee;
		}


		public void DeleteEmployeeErrorByOrgIDAndImportTypeID(int orgID)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_EMPLOYEE_Error_D_Org";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);

			db.ExecuteNonQuery(dbCommand);
		}


		public void DeleteEmployeeError(int employeeErrorID)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_EMPLOYEE_Error_D";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_error_id", DbType.Int32, employeeErrorID);

			db.ExecuteNonQuery(dbCommand);
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

		public static EmployeeError CreateModelObject(IDataReader dataReader)
		{
			return new EmployeeError(
				DataConvert.ToInt(dataReader[GetMappingFieldName("EmployeeErrorID")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("OrgID")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("ImportType")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("ExcelNo")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("WorkNo")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("EmployeeName")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("Sex")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("OrgPath")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("PostPath")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("ErrorReason")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("OperateMode")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("OrgName")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("GroupName")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("IdentifyCode")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("PostNo")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("NativePlace")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("Folk")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("Wedding")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("PoliticalStatus")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("EducationLevel")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("GraduateUniversity")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("StudyMajor")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("Address")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("EmployeeLevel")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("Birthday")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("BeginDate")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("WorkDate")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("EmployeeType")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("WorkGroupLeader")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("TeacherType")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("OnPost")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("TechnicalTitle")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("TechnicalSkill")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("PostCode")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("EducationEmployee")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("CommitteeHeadShip")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("EmployeeTransportType")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("EmployeeID")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("SalaryNo")])
				);
		}
	}
}
