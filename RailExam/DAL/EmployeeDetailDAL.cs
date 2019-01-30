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
	public class EmployeeDetailDAL
	{
		private static Hashtable _ormTable;
		private int _recordCount = 0;

		static EmployeeDetailDAL()
        {
            _ormTable = new Hashtable();

             _ormTable.Add("employeeid", "EMPLOYEE_ID");
            _ormTable.Add("orgid", "ORG_ID");
            _ormTable.Add("orgname", "ORG_NAME");
            _ormTable.Add("workno", "WORK_NO");
            _ormTable.Add("employeename", "EMPLOYEE_NAME");
            _ormTable.Add("postid", "POST_ID");
            _ormTable.Add("postname", "POST_NAME");
            _ormTable.Add("sex", "SEX");
            _ormTable.Add("birthday", "BIRTHDAY");
            _ormTable.Add("nativeplace", "NATIVE_PLACE");
            _ormTable.Add("folk", "FOLK");
            _ormTable.Add("wedding", "WEDDING");
            _ormTable.Add("begindate", "BEGIN_DATE");
            _ormTable.Add("workphone", "WORK_PHONE");
            _ormTable.Add("homephone", "HOME_PHONE");
            _ormTable.Add("mobilephone", "MOBILE_PHONE");
            _ormTable.Add("email", "EMAIL");
            _ormTable.Add("address", "ADDRESS");
            _ormTable.Add("postcode", "POST_CODE");
            _ormTable.Add("dimission", "DIMISSION");
            _ormTable.Add("memo", "MEMO");
            _ormTable.Add("isgroupleader","IS_GROUP_LEADER");
            _ormTable.Add("techniciantypeid","TECHNICIAN_TYPE_ID");
            _ormTable.Add("postno","POST_NO");
            _ormTable.Add("pinyincode","PINYIN_CODE");
			_ormTable.Add("logincount", "LOGIN_COUNT");
			_ormTable.Add("logintime", "LOGIN_TIME");
            _ormTable.Add("politicalstatusid", "POLITICAL_STATUS_ID");
            _ormTable.Add("educationlevelid", "EDUCATION_LEVEL_ID");
            _ormTable.Add("employeetypeid", "EMPLOYEE_TYPE_ID");
            _ormTable.Add("technicaltitleid", "TECHNICAL_TITLE_ID");
            _ormTable.Add("workgroupleadertypeid", "WORK_GROUPLEADER_TYPE_ID");
            _ormTable.Add("educationemployeetypeid", "EDUCATION_EMPLOYEE_TYPE_ID");
            _ormTable.Add("committeeheadshipid", "COMMITTEE_HEADSHIP_ID");
            _ormTable.Add("employeetransporttypeid", "EMPLOYEE_TRANSPORT_TYPE_ID");
			_ormTable.Add("workdate","WORK_DATE");
			_ormTable.Add("identifycode","IDENTIFY_CODE");
			_ormTable.Add("graduateuniversity","GRADUATE_UNIVERSITY");
			_ormTable.Add("studymajor","STUDY_MAJOR");
			_ormTable.Add("employeelevelid","EMPLOYEE_LEVEL_ID");
			_ormTable.Add("teachertypeid","TEACHER_TYPE_ID");
			_ormTable.Add("approvepost","APPROVE_POST");
        }

		/// <summary>
		/// 新增员工
		/// </summary>
		/// <param name="employee">新增的员工信息</param>
		/// <returns></returns>
		public int AddEmployee(Database db, DbTransaction trans, EmployeeDetail employee)
		{
			string sqlCommand = "USP_EMPLOYEE_Detail_I";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddOutParameter(dbCommand, "p_employee_id", DbType.Int32, employee.EmployeeID);
			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, employee.OrgID);
			db.AddInParameter(dbCommand, "p_work_no", DbType.String, employee.WorkNo);
			db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employee.EmployeeName);
			db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, employee.PostID);
			db.AddInParameter(dbCommand, "p_sex", DbType.String, employee.Sex);
			db.AddInParameter(dbCommand, "p_birthday", DbType.Date, employee.Birthday);
			db.AddInParameter(dbCommand, "p_native_place", DbType.String, employee.NativePlace);
			db.AddInParameter(dbCommand, "p_folk", DbType.String, employee.Folk);
			db.AddInParameter(dbCommand, "p_wedding", DbType.Int32, employee.Wedding);
			db.AddInParameter(dbCommand, "p_begin_date", DbType.Date, employee.BeginDate);
			db.AddInParameter(dbCommand, "p_work_phone", DbType.String, employee.WorkPhone);
			db.AddInParameter(dbCommand, "p_home_phone", DbType.String, employee.HomePhone);
			db.AddInParameter(dbCommand, "p_mobile_phone", DbType.String, employee.MobilePhone);
			db.AddInParameter(dbCommand, "p_email", DbType.String, employee.Email);
			db.AddInParameter(dbCommand, "p_address", DbType.String, employee.Address);
			db.AddInParameter(dbCommand, "p_post_code", DbType.String, employee.PostCode);
			db.AddInParameter(dbCommand, "p_dimission", DbType.Int32, employee.Dimission ? 1 :0);//在册
			db.AddInParameter(dbCommand, "p_memo", DbType.String, employee.Memo);
			db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, employee.IsGroupLeader);
			db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, employee.TechnicianTypeID);
			db.AddInParameter(dbCommand, "p_post_no", DbType.String, employee.PostNo);
			db.AddInParameter(dbCommand, "p_pinyin_code", DbType.String, employee.PinYinCode);
			db.AddInParameter(dbCommand, "p_login_count", DbType.Int32, employee.LoginCount);
			db.AddInParameter(dbCommand, "p_login_time", DbType.Int32, employee.LoginTime);

            db.AddInParameter(dbCommand, "p_identify_code", DbType.String, employee.IdentifyCode);
			db.AddInParameter(dbCommand, "p_political_status_id", DbType.Int32, employee.PoliticalStatusID);
            db.AddInParameter(dbCommand, "p_work_date", DbType.Date, employee.WorkDate);
            db.AddInParameter(dbCommand, "p_education_level_id", DbType.Int32, employee.EducationLevelID);
			db.AddInParameter(dbCommand, "p_employee_type_id", DbType.Int32, employee.EmployeeTypeID);
            
            db.AddInParameter(dbCommand, "p_second_id", DbType.Int32, employee.SecondPostID);
            db.AddInParameter(dbCommand, "p_third_id", DbType.Int32, employee.ThirdPostID);
            db.AddInParameter(dbCommand, "p_now_post_id", DbType.Int32, employee.NowPostID);
			
			db.AddInParameter(dbCommand, "p_technical_title_id", DbType.Int32, employee.TechnicalTitleID);
			db.AddInParameter(dbCommand, "p_work_groupleader_type_id", DbType.Int32, employee.WorkGroupLeaderTypeID);

            db.AddInParameter(dbCommand, "p_group_date", DbType.Date, employee.GroupNoDate);

			db.AddInParameter(dbCommand, "p_education_employee_type_id", DbType.Int32, employee.EducationEmployeeTypeID);
			db.AddInParameter(dbCommand, "p_committee_headship_id", DbType.Int32, employee.CommitteeHeadShipID);
			db.AddInParameter(dbCommand, "p_employee_transport_type_id", DbType.Int32, employee.EmployeeTransportTypeID);

            db.AddInParameter(dbCommand, "p_award_date", DbType.Date, employee.PostNoDate);

            db.AddInParameter(dbCommand, "p_is_on_post", DbType.Int32, employee.IsOnPost ? 1 : 0);//在岗

            db.AddInParameter(dbCommand, "p_technical_date", DbType.Date, employee.TechnicalDate);
            db.AddInParameter(dbCommand, "p_technical_title_date", DbType.Date, employee.TechnicalTitleDate);
            db.AddInParameter(dbCommand, "p_post_date", DbType.Date, employee.PostDate);
            db.AddInParameter(dbCommand, "p_graduate_date", DbType.Date, employee.GraduatDate);
            db.AddInParameter(dbCommand, "p_graduate_university", DbType.String, employee.GraduateUniversity);
            db.AddInParameter(dbCommand, "p_study_major", DbType.String, employee.StudyMajor);
            db.AddInParameter(dbCommand, "p_university_type", DbType.Int32, employee.UniversityType);
            db.AddInParameter(dbCommand, "p_technical_code", DbType.String, employee.TechnicalCode);


			db.ExecuteNonQuery(dbCommand, trans);
			int id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_employee_id"));

            string sqlCommand1 = "USP_SYSTEM_USER_I";
            DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand1);

            db.AddInParameter(dbCommand1, "p_user_id", DbType.String, employee.WorkNo == string.Empty ? employee.IdentifyCode : employee.WorkNo);
            db.AddInParameter(dbCommand1, "p_password", DbType.String, "111111");
            db.AddInParameter(dbCommand1, "p_employee_id", DbType.Int32, id);
            db.AddInParameter(dbCommand1, "p_role_id", DbType.Int32, 0);
            db.AddInParameter(dbCommand1, "p_memo", DbType.String, "");
            db.ExecuteNonQuery(dbCommand1, trans);

		    return id;
		}

        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="employee">新增的员工信息</param>
        /// <returns></returns>
        public void UpdateEmployee(Database db, DbTransaction trans, EmployeeDetail employee)
        {
            string sqlCommand = "USP_EMPLOYEE_Detail_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employee.EmployeeID);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, employee.OrgID);
            db.AddInParameter(dbCommand, "p_work_no", DbType.String, employee.WorkNo);
            db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employee.EmployeeName);
            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, employee.PostID);
            db.AddInParameter(dbCommand, "p_sex", DbType.String, employee.Sex);
            db.AddInParameter(dbCommand, "p_birthday", DbType.Date, employee.Birthday);
            db.AddInParameter(dbCommand, "p_native_place", DbType.String, employee.NativePlace);
            db.AddInParameter(dbCommand, "p_folk", DbType.String, employee.Folk);
            db.AddInParameter(dbCommand, "p_wedding", DbType.Int32, employee.Wedding);
            db.AddInParameter(dbCommand, "p_begin_date", DbType.Date, employee.BeginDate);
            db.AddInParameter(dbCommand, "p_work_phone", DbType.String, employee.WorkPhone);
            db.AddInParameter(dbCommand, "p_home_phone", DbType.String, employee.HomePhone);
            db.AddInParameter(dbCommand, "p_mobile_phone", DbType.String, employee.MobilePhone);
            db.AddInParameter(dbCommand, "p_email", DbType.String, employee.Email);
            db.AddInParameter(dbCommand, "p_address", DbType.String, employee.Address);
            db.AddInParameter(dbCommand, "p_post_code", DbType.String, employee.PostCode);
            db.AddInParameter(dbCommand, "p_dimission", DbType.Int32, employee.Dimission ? 1 : 0);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, employee.Memo);
            db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, employee.IsGroupLeader);
            db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, employee.TechnicianTypeID);
            db.AddInParameter(dbCommand, "p_post_no", DbType.String, employee.PostNo);
            db.AddInParameter(dbCommand, "p_pinyin_code", DbType.String, employee.PinYinCode);
            db.AddInParameter(dbCommand, "p_login_count", DbType.Int32, employee.LoginCount);
            db.AddInParameter(dbCommand, "p_login_time", DbType.Int32, employee.LoginTime);

            db.AddInParameter(dbCommand, "p_identify_code", DbType.String, employee.IdentifyCode);
            db.AddInParameter(dbCommand, "p_political_status_id", DbType.Int32, employee.PoliticalStatusID);
            db.AddInParameter(dbCommand, "p_work_date", DbType.Date, employee.WorkDate);
            db.AddInParameter(dbCommand, "p_education_level_id", DbType.Int32, employee.EducationLevelID);
            db.AddInParameter(dbCommand, "p_employee_type_id", DbType.Int32, employee.EmployeeTypeID);

            db.AddInParameter(dbCommand, "p_second_id", DbType.Int32, employee.SecondPostID);
            db.AddInParameter(dbCommand, "p_third_id", DbType.Int32, employee.ThirdPostID);
            db.AddInParameter(dbCommand, "p_now_post_id", DbType.Int32, employee.NowPostID);

            db.AddInParameter(dbCommand, "p_technical_title_id", DbType.Int32, employee.TechnicalTitleID);
            db.AddInParameter(dbCommand, "p_work_groupleader_type_id", DbType.Int32, employee.WorkGroupLeaderTypeID);

            db.AddInParameter(dbCommand, "p_group_date", DbType.Date, employee.GroupNoDate);

            db.AddInParameter(dbCommand, "p_education_employee_type_id", DbType.Int32, employee.EducationEmployeeTypeID);
            db.AddInParameter(dbCommand, "p_committee_headship_id", DbType.Int32, employee.CommitteeHeadShipID);
            db.AddInParameter(dbCommand, "p_employee_transport_type_id", DbType.Int32, employee.EmployeeTransportTypeID);

            db.AddInParameter(dbCommand, "p_award_date", DbType.Date, employee.PostNoDate);

            db.AddInParameter(dbCommand, "p_is_on_post", DbType.Int32, employee.IsOnPost ? 1 : 0);

            db.AddInParameter(dbCommand, "p_technical_date", DbType.Date, employee.TechnicalDate);
            db.AddInParameter(dbCommand, "p_technical_title_date", DbType.Date, employee.TechnicalTitleDate);
            db.AddInParameter(dbCommand, "p_post_date", DbType.Date, employee.PostDate);
            db.AddInParameter(dbCommand, "p_graduate_date", DbType.Date, employee.GraduatDate);
            db.AddInParameter(dbCommand, "p_graduate_university", DbType.String, employee.GraduateUniversity);
            db.AddInParameter(dbCommand, "p_study_major", DbType.String, employee.StudyMajor);
            db.AddInParameter(dbCommand, "p_university_type", DbType.Int32, employee.UniversityType);
            db.AddInParameter(dbCommand, "p_technical_code", DbType.String, employee.TechnicalCode);

            db.ExecuteNonQuery(dbCommand, trans);
        }


		/// <summary>
		/// 新增员工
		/// </summary>
		/// <param name="employee">新增的员工信息</param>
		/// <returns></returns>
		public int AddEmployee(EmployeeDetail employee)
		{
			Database db = DatabaseFactory.CreateDatabase();

			DbConnection connection = db.CreateConnection();
			connection.Open();
			DbTransaction transaction = connection.BeginTransaction();

			int id = 0;
			try
			{
				string sqlCommand = "USP_EMPLOYEE_Detail_I";
				DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

				db.AddOutParameter(dbCommand, "p_employee_id", DbType.Int32, employee.EmployeeID);
				db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, employee.OrgID);
				db.AddInParameter(dbCommand, "p_work_no", DbType.String, employee.WorkNo);
				db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employee.EmployeeName);
				db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, employee.PostID);
				db.AddInParameter(dbCommand, "p_sex", DbType.String, employee.Sex);
				db.AddInParameter(dbCommand, "p_birthday", DbType.Date, employee.Birthday);
				db.AddInParameter(dbCommand, "p_native_place", DbType.String, employee.NativePlace);
				db.AddInParameter(dbCommand, "p_folk", DbType.String, employee.Folk);
				db.AddInParameter(dbCommand, "p_wedding", DbType.Int32, employee.Wedding);
				db.AddInParameter(dbCommand, "p_begin_date", DbType.Date, employee.BeginDate);
				db.AddInParameter(dbCommand, "p_work_phone", DbType.String, employee.WorkPhone);
				db.AddInParameter(dbCommand, "p_home_phone", DbType.String, employee.HomePhone);
				db.AddInParameter(dbCommand, "p_mobile_phone", DbType.String, employee.MobilePhone);
				db.AddInParameter(dbCommand, "p_email", DbType.String, employee.Email);
				db.AddInParameter(dbCommand, "p_address", DbType.String, employee.Address);
				db.AddInParameter(dbCommand, "p_post_code", DbType.String, employee.PostCode);
				db.AddInParameter(dbCommand, "p_dimission", DbType.Int32, employee.Dimission ? 1 : 0);
				db.AddInParameter(dbCommand, "p_memo", DbType.String, employee.Memo);
				db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, employee.IsGroupLeader);
				db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, employee.TechnicianTypeID);
				db.AddInParameter(dbCommand, "p_post_no", DbType.String, employee.PostNo);
				db.AddInParameter(dbCommand, "p_pinyin_code", DbType.String, employee.PinYinCode);
				db.AddInParameter(dbCommand, "p_login_count", DbType.Int32, employee.LoginCount);
				db.AddInParameter(dbCommand, "p_login_time", DbType.Int32, employee.LoginTime);
				db.AddInParameter(dbCommand, "p_political_status_id", DbType.Int32, employee.PoliticalStatusID);
				db.AddInParameter(dbCommand, "p_education_level_id", DbType.Int32, employee.EducationLevelID);
				db.AddInParameter(dbCommand, "p_employee_type_id", DbType.Int32, employee.EmployeeTypeID);
				db.AddInParameter(dbCommand, "p_technical_title_id", DbType.Int32, employee.TechnicalTitleID);
				db.AddInParameter(dbCommand, "p_work_groupleader_type_id", DbType.Int32, employee.WorkGroupLeaderTypeID);
				db.AddInParameter(dbCommand, "p_education_employee_type_id", DbType.Int32, employee.EducationEmployeeTypeID);
				db.AddInParameter(dbCommand, "p_committee_headship_id", DbType.Int32, employee.CommitteeHeadShipID);
				db.AddInParameter(dbCommand, "p_employee_transport_type_id", DbType.Int32, employee.EmployeeTransportTypeID);
				db.AddInParameter(dbCommand, "p_work_date", DbType.Date, employee.WorkDate);
				db.AddInParameter(dbCommand, "p_identify_code", DbType.String, employee.IdentifyCode);
				db.AddInParameter(dbCommand, "p_graduate_university", DbType.String, employee.GraduateUniversity);
				db.AddInParameter(dbCommand, "p_study_major", DbType.String, employee.StudyMajor);
				db.AddInParameter(dbCommand, "p_employee_level_id", DbType.Int32, employee.EmployeeLevelID);
				db.AddInParameter(dbCommand, "p_teacher_type_id", DbType.Int32, employee.TeacherTypeID);
				db.AddInParameter(dbCommand, "p_approve_post", DbType.Int32, employee.ApprovePost);

				db.ExecuteNonQuery(dbCommand, transaction);
				id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_employee_id"));

                string sqlCommand1 = "USP_SYSTEM_USER_I";
                DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand1);

                db.AddInParameter(dbCommand1, "p_user_id", DbType.String, employee.WorkNo==string.Empty ? employee.IdentifyCode:employee.WorkNo);
                db.AddInParameter(dbCommand1, "p_password", DbType.String, "111111");
                db.AddInParameter(dbCommand1, "p_employee_id", DbType.Int32, id);
                db.AddInParameter(dbCommand1, "p_role_id", DbType.Int32, 0);
                db.AddInParameter(dbCommand1, "p_memo", DbType.String, "");
                db.ExecuteNonQuery(dbCommand1, transaction);
                transaction.Commit();
			}
			catch (Exception ex)
			{
				transaction.Rollback();
				throw ex;
			}
			connection.Close();

			return id;
		}

		/// <summary>
		/// 新增员工
		/// </summary>
		/// <param name="employee">新增的员工信息</param>
		/// <returns></returns>
		public void UpdateEmployee(EmployeeDetail employee)
		{
			Database db = DatabaseFactory.CreateDatabase();

			DbConnection connection = db.CreateConnection();
			connection.Open();
			DbTransaction transaction = connection.BeginTransaction();

			try
			{
				string sqlCommand = "USP_EMPLOYEE_Detail_U";
				DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

				db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employee.EmployeeID);
				db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, employee.OrgID);
				db.AddInParameter(dbCommand, "p_work_no", DbType.String, employee.WorkNo);
				db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employee.EmployeeName);
				db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, employee.PostID);
				db.AddInParameter(dbCommand, "p_sex", DbType.String, employee.Sex);
				db.AddInParameter(dbCommand, "p_birthday", DbType.Date, employee.Birthday);
				db.AddInParameter(dbCommand, "p_native_place", DbType.String, employee.NativePlace);
				db.AddInParameter(dbCommand, "p_folk", DbType.String, employee.Folk);
				db.AddInParameter(dbCommand, "p_wedding", DbType.Int32, employee.Wedding);
				db.AddInParameter(dbCommand, "p_begin_date", DbType.Date, employee.BeginDate);
				db.AddInParameter(dbCommand, "p_work_phone", DbType.String, employee.WorkPhone);
				db.AddInParameter(dbCommand, "p_home_phone", DbType.String, employee.HomePhone);
				db.AddInParameter(dbCommand, "p_mobile_phone", DbType.String, employee.MobilePhone);
				db.AddInParameter(dbCommand, "p_email", DbType.String, employee.Email);
				db.AddInParameter(dbCommand, "p_address", DbType.String, employee.Address);
				db.AddInParameter(dbCommand, "p_post_code", DbType.String, employee.PostCode);
				db.AddInParameter(dbCommand, "p_dimission", DbType.Int32, employee.Dimission ? 1 : 0);
				db.AddInParameter(dbCommand, "p_memo", DbType.String, employee.Memo);
				db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, employee.IsGroupLeader);
				db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, employee.TechnicianTypeID);
				db.AddInParameter(dbCommand, "p_post_no", DbType.String, employee.PostNo);
				db.AddInParameter(dbCommand, "p_pinyin_code", DbType.String, employee.PinYinCode);
				db.AddInParameter(dbCommand, "p_login_count", DbType.Int32, employee.LoginCount);
				db.AddInParameter(dbCommand, "p_login_time", DbType.Int32, employee.LoginTime);
				db.AddInParameter(dbCommand, "p_political_status_id", DbType.Int32, employee.PoliticalStatusID);
				db.AddInParameter(dbCommand, "p_education_level_id", DbType.Int32, employee.EducationLevelID);
				db.AddInParameter(dbCommand, "p_employee_type_id", DbType.Int32, employee.EmployeeTypeID);
				db.AddInParameter(dbCommand, "p_technical_title_id", DbType.Int32, employee.TechnicalTitleID);
				db.AddInParameter(dbCommand, "p_work_groupleader_type_id", DbType.Int32, employee.WorkGroupLeaderTypeID);
				db.AddInParameter(dbCommand, "p_education_employee_type_id", DbType.Int32, employee.EducationEmployeeTypeID);
				db.AddInParameter(dbCommand, "p_committee_headship_id", DbType.Int32, employee.CommitteeHeadShipID);
				db.AddInParameter(dbCommand, "p_employee_transport_type_id", DbType.Int32, employee.EmployeeTransportTypeID);
				db.AddInParameter(dbCommand, "p_work_date", DbType.Date, employee.WorkDate);
				db.AddInParameter(dbCommand, "p_identify_code", DbType.String, employee.IdentifyCode);
				db.AddInParameter(dbCommand, "p_graduate_university", DbType.String, employee.GraduateUniversity);
				db.AddInParameter(dbCommand, "p_study_major", DbType.String, employee.StudyMajor);
				db.AddInParameter(dbCommand, "p_employee_level_id", DbType.Int32, employee.EmployeeLevelID);
				db.AddInParameter(dbCommand, "p_teacher_type_id", DbType.Int32, employee.TeacherTypeID);
				db.AddInParameter(dbCommand, "p_approve_post", DbType.Int32, employee.ApprovePost);

				db.ExecuteNonQuery(dbCommand,transaction);

				transaction.Commit();
			}
			catch (Exception ex)
			{
				transaction.Rollback();
				throw ex;
			}
			connection.Close();
		}

		
		public EmployeeDetail GetEmployee(int employeeID)
		{
			EmployeeDetail employee = null;

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_EMPLOYEE_Detail_G";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employeeID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				if (dataReader.Read())
				{
					employee = CreateModelObject(dataReader);
				}
			}

			return employee;
		}

		public IList<EmployeeDetail> GetEmployee(string employeeName, string identifyCode)
		{
			IList<EmployeeDetail> employees = new List<EmployeeDetail>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_EMPLOYEE_Detail_Q";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employeeName);
			db.AddInParameter(dbCommand, "p_identify_code", DbType.String, identifyCode);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					EmployeeDetail employee = CreateModelObject(dataReader);

					employees.Add(employee);
				}
			}

			return employees;
		}

		/// <summary>
		/// 删除员工
		/// </summary>
		/// <param name="employeeID">要删除的员工ID</param>
		public bool DeleteEmployeeDetail(int employeeID)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_EMPLOYEE_Detail_D";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employeeID);

			if (db.ExecuteNonQuery(dbCommand) > 0)
				return true;
			else
				return false;
		}

		public IList<EmployeeDetail> GetEmployeeByWhereClause(string whereClause)
		{
			IList<EmployeeDetail> details = new List<EmployeeDetail>();
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetStoredProcCommand("USP_Employee_Detail_Where");

			db.AddInParameter(dbCommand, "p_sql", DbType.String, whereClause);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					EmployeeDetail detail = CreateModelObject(dataReader);

					details.Add(detail);
				}
			}

			_recordCount = details.Count;

			return details;
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

		public static EmployeeDetail CreateModelObject(IDataReader dataReader)
		{
			return new EmployeeDetail(
				                DataConvert.ToInt(dataReader[GetMappingFieldName("EmployeeID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrgID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("OrgName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("WorkNo")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("EmployeeName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("PostID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("PostName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Sex")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("Birthday")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("NativePlace")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Folk")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("Wedding")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("BeginDate")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("WorkPhone")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("HomePhone")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("MobilePhone")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Email")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Address")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("PostCode")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("Dimission")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("IsGroupLeader")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("TechnicianTypeID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("PostNo")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("LoginCount")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("LoginTime")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("PoliticalStatusID")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("EducationLevelID")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("EmployeeTypeID")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("TechnicalTitleID")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("WorkGroupLeaderTypeID")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("EducationEmployeeTypeID")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("CommitteeHeadShipID")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("EmployeeTransportTypeID")]),
				DataConvert.ToDateTime(dataReader[GetMappingFieldName("WorkDate")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("IdentifyCode")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("GraduateUniversity")]),
			   DataConvert.ToString(dataReader[GetMappingFieldName("StudyMajor")]),
			   DataConvert.ToInt(dataReader[GetMappingFieldName("EmployeeLevelID")]),
			   DataConvert.ToInt(dataReader[GetMappingFieldName("TeacherTypeID")]),
			   DataConvert.ToInt(dataReader[GetMappingFieldName("ApprovePost")])
				);
		}

		/// <summary>
		/// 获取员工表中是否包含此数据
		/// </summary>
		/// <param name="strWhere">查询条件</param>
		/// <returns>object对象</returns>
		public object GetEmployeeByWhere(string strWhere)
		{
			Database db = DatabaseFactory.CreateDatabase();
			string sql = string.Format("select count(*) from employee where  {0}", strWhere);
			return db.ExecuteScalar(CommandType.Text, sql);
		}
	}
}
