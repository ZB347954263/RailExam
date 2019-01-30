using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	public class EmployeeError
	{
		private int _employeeErrorID = 0;
		private int _orgID = 200;
		private int _importType = 1;
		private int _excelNo = 1;
		private string _workNo = string.Empty;
		private string _employeeName = string.Empty;
		private string _sex = "ÄÐ";
		private string _orgPath = string.Empty;
		private string _postPath = string.Empty;
		private string _errorReason = string.Empty;
		private int _operateMode = 1;
		private string _orgName = string.Empty;
		private string _groupName = string.Empty;
		private string _identifyCode = string.Empty;
		private string _postNo = string.Empty;
		private string _nativePlace = string.Empty;
		private string _folk = string.Empty;
		private string _wedding = string.Empty;
		private string _politicalStatus = string.Empty;
		private string _educationLevel = string.Empty;
		private string _graduateUniversity = string.Empty;
		private string _studyMajor = string.Empty;
		private string _address = string.Empty;
		private string _employeeLevel = string.Empty;
		private string _birthday = string.Empty;
		private string _beginDate = string.Empty;
		private string _workDate = string.Empty;
		private string _employeeType = string.Empty;
		private string _workGroupLeader = string.Empty;
		private string _teacherType = string.Empty;
		private string _onPost = string.Empty;
		private string _technicalTitle = string.Empty;
		private string _technicalSkill = string.Empty;
		private string _postCode = string.Empty;
		private string _educationEmployee = string.Empty;
		private string _committeeHeadShip = string.Empty;
		private string _employeeTransportType = string.Empty;
		private string _salaryno = string.Empty;
		private int _employeeID = 0;

		public int EmployeeErrorID
		{
			get { return _employeeErrorID; }
			set { _employeeErrorID = value; }
		}

		public int OrgID
		{
			get { return _orgID; }
			set { _orgID = value; }
		}

		public int ImportType
		{
			get { return _importType; }
			set { _importType = value; }
		}

		public int ExcelNo
		{
			get { return _excelNo; }
			set { _excelNo = value; }
		}

		public string WorkNo
		{
			get { return _workNo; }
			set { _workNo = value; }
		}

		public string EmployeeName
		{
			get { return _employeeName; }
			set { _employeeName = value; }
		}

		public string Sex
		{
			get { return _sex; }
			set { _sex = value; }
		}

		public string OrgPath
		{
			get { return _orgPath; }
			set { _orgPath = value; }
		}

		public string PostPath
		{
			get { return _postPath; }
			set { _postPath = value; }
		}

		public string ErrorReason
		{
			get { return _errorReason; }
			set { _errorReason = value; }
		}

		public int OperateMode
		{
			get { return _operateMode; }
			set { _operateMode = value; }
		}

		public string OrgName
		{
			get { return _orgName; }
			set { _orgName = value; }
		}

		public string GroupName
		{
			get { return _groupName; }
			set { _groupName = value; }
		}

		public string IdentifyCode
		{
			get { return _identifyCode; }
			set { _identifyCode = value; }
		}

		public string PostNo
		{
			get { return _postNo; }
			set { _postNo = value; }
		}

		public string NativePlace
		{
			get { return _nativePlace; }
			set { _nativePlace = value; }
		}

		public string Folk
		{
			get { return _folk; }
			set { _folk = value; }
		}

		public string Wedding
		{
			get { return _wedding; }
			set { _wedding = value; }
		}

		public string PoliticalStatus
		{
			get { return _politicalStatus; }
			set { _politicalStatus = value; }
		}

		public string EducationLevel
		{
			get { return _educationLevel; }
			set { _educationLevel = value; }
		}

		public string GraduateUniversity
		{
			get { return _graduateUniversity; }
			set { _graduateUniversity = value; }
		}

		public string StudyMajor
		{
			get { return _studyMajor; }
			set { _studyMajor = value; }
		}

		public string Address
		{
			get { return _address; }
			set { _address = value; }
		}

		public string EmployeeLevel
		{
			get { return _employeeLevel; }
			set { _employeeLevel = value; }
		}

		public string Birthday
		{
			get { return _birthday; }
			set { _birthday = value; }
		}

		public string BeginDate
		{
			get { return _beginDate; }
			set { _beginDate = value; }
		}

		public string WorkDate
		{
			get { return _workDate; }
			set { _workDate = value; }
		}

		public string EmployeeType
		{
			get { return _employeeType; }
			set { _employeeType = value; }
		}

		public string WorkGroupLeader
		{
			get { return _workGroupLeader; }
			set { _workGroupLeader = value; }
		}

		public string TeacherType
		{
			get { return _teacherType; }
			set { _teacherType = value; }
		}

		public string OnPost
		{
			get { return _onPost; }
			set { _onPost = value; }
		}

		public string TechnicalTitle
		{
			get { return _technicalTitle; }
			set { _technicalTitle = value; }
		}

		public string TechnicalSkill
		{
			get { return _technicalSkill; }
			set { _technicalSkill = value; }
		}

		public string PostCode
		{
			get { return _postCode; }
			set { _postCode = value; }
		}

		public string EducationEmployee
		{
			get { return _educationEmployee; }
			set { _educationEmployee = value; }
		}

		public string CommitteeHeadShip
		{
			get { return _committeeHeadShip; }
			set { _committeeHeadShip = value; }
		}

		public string EmployeeTransportType
		{
			get { return _employeeTransportType; }
			set { _employeeTransportType = value; }
		}

		public int EmployeeID
		{
			get { return _employeeID; }
			set { _employeeID = value;}
		}

		public string SalaryNo
		{
			get { return _salaryno; }
			set { _salaryno = value; }
		}

		public EmployeeError() { }

		public EmployeeError(int? employeeErrorID, int? orgID, int? importType, int? excelNo, string workNo, string employeeName,
			string sex, string orgPath, string postPath, string errorReason, int? operateMode, string orgName, string groupName,
			string identifyCode, string postNo, string nativePlace, string folk, string wedding, string politicalStatus,
			string educationLevel, string graduateUniversity, string studyMajor, string address, string employeeLevel, string birthday,
			string beginDate, string workDate, string employeeType, string workGroupLeader, string teacherType, string onPost,
			string technicalTitle, string technicalSkill, string postCode, string educationEmployee, string committeeHeadShip, 
			string employeeTransportType,int? employeeID,string salaryNo)
		{
			_employeeErrorID = employeeErrorID ?? _employeeErrorID;
			_importType = importType ?? _importType;
			_orgID = orgID ?? _orgID;
			_excelNo = excelNo ?? _excelNo;
			_workNo = workNo;
			_employeeName = employeeName;
			_sex = sex;
			_orgPath = orgPath;
			_postPath = postPath;
			_errorReason = errorReason;
			_operateMode = operateMode ?? _operateMode;
			_orgName = orgName;
			_groupName = groupName;
			_identifyCode = identifyCode;
			_postNo = postNo;
			_nativePlace = nativePlace;
			_folk = folk;
			_wedding = wedding;
			_politicalStatus = politicalStatus;
			_educationLevel = educationLevel;
			_graduateUniversity = graduateUniversity;
			_studyMajor = studyMajor;
			_address = address;
			_employeeLevel = employeeLevel;
			_birthday = birthday;
			_beginDate = beginDate;
			_workDate = workDate;
			_employeeType = employeeType;
			_workGroupLeader = workGroupLeader;
			_teacherType = teacherType;
			_onPost = onPost;
			_technicalTitle = technicalTitle;
			_technicalSkill = technicalSkill;
			_postCode = postCode;
			_educationEmployee = educationEmployee;
			_committeeHeadShip = committeeHeadShip;
			_employeeTransportType = employeeTransportType;
			_employeeID = employeeID ?? _employeeID;
			_salaryno = SalaryNo;
		}
	}
}
