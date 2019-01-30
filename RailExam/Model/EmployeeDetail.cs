using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	public class EmployeeDetail
	{
		private int _employeeID = 0;
		private int _orgID = 1;
		private string _orgName = string.Empty;
		private string _workNo = string.Empty;
		private string _employeeName = string.Empty;
		private int _postID = 1;
		private string _postName = string.Empty;
		private string _sex = string.Empty;
		private DateTime _birthday = new DateTime();
		private string _nativePlace = string.Empty;
		private string _folk = string.Empty;
		private int _wedding = 0;
		private DateTime _beginDate = new DateTime();
		private string _workPhone = string.Empty;
		private string _homePhone = string.Empty;
		private string _mobilePhone = string.Empty;
		private string _email = string.Empty;
		private string _address = string.Empty;
		private string _postCode = string.Empty;
		private bool _dimission = false;
		private string _memo = string.Empty;
		private bool _flag = false;
		private int _isGroupLeader = 2;
		private int _technicianTypeID = 0;
		private string _postNo = string.Empty;
		private string _pinYinCode = string.Empty;
		private int _loginCount = 0;
		private int _loginTime = 0;
		private int _politicalStatusID;
		private int _educationLevelID;
		private int _employeeTypeID;
		private int _technicalTitleID;
		private int _workGroupLeaderTypeID;
		private int _educationEmployeeTypeID;
		private int _committeeHeadShipID;
		private int _employeeTransportTypeID;
		private DateTime _workDate;
		private string _identifyCode;
		private string _graduateUniversity;
		private string _studyMajor;
		private int _employeeLevelID = 0;
		private int _teacherTypeID = 0;
		private int _approvePost = 0;
        private DateTime _postNoDate ;
        private DateTime _groupNoDate;
	    private bool _isOnPost = true;
	    private int? _secondPostID = null;
        private int? _thirdPostID = null;
	    private int? _nowPostID = null;

	    private DateTime? _technicaldate;
        private DateTime? _technicalTitledate;
        private DateTime _postdate;
	    private DateTime _graduatDate;
	    private int _universityType;
	    private string _technicalCode;

	    public DateTime? TechnicalDate
	    {
            set { _technicaldate = value; }
            get { return _technicaldate; }
	    }

        public DateTime? TechnicalTitleDate
        {
            set { _technicalTitledate = value; }
            get { return _technicalTitledate; }
        }

        public DateTime PostDate
        {
            set { _postdate = value; }
            get { return _postdate; }
        }

        public DateTime GraduatDate
        {
            set { _graduatDate = value; }
            get { return _graduatDate; }
        }

        public int UniversityType
        {
            get { return _universityType; }
            set { _universityType = value; }
        }

        public string TechnicalCode
        {
            get { return _technicalCode; }
            set { _technicalCode = value; }
        }

	    public int? SecondPostID
	    {
            get { return _secondPostID; }
            set { _secondPostID = value; }
	    }

        public int? NowPostID
        {
            get { return _nowPostID; }
            set { _nowPostID = value; }
        }

        public int? ThirdPostID
        {
            get { return _thirdPostID; }
            set { _thirdPostID = value; }
        }

	    public bool IsOnPost
	    {
            get { return _isOnPost; }
            set { _isOnPost = value; }
	    }

	    public DateTime PostNoDate
	    {
            get { return _postNoDate; }
            set { _postNoDate = value; }
	    }

        public DateTime GroupNoDate
        {
            get { return _groupNoDate; }
            set { _groupNoDate = value; }
        }

		public int EmployeeID
		{
			get { return _employeeID; }
			set { _employeeID = value; }
		}

		public int OrgID
		{
			set
			{
				_orgID = value;
			}
			get
			{
				return _orgID;
			}
		}

		public string OrgName
		{
			set
			{
				_orgName = value;
			}
			get
			{
				return _orgName;
			}
		}

		public string WorkNo
		{
			set
			{
				_workNo = value;
			}
			get
			{
				return _workNo;
			}
		}

		public string EmployeeName
		{
			set
			{
				_employeeName = value;
			}
			get
			{
				return _employeeName;
			}
		}

		public int PostID
		{
			set
			{
				_postID = value;
			}
			get
			{
				return _postID;
			}
		}

		public string PostName
		{
			set
			{
				_postName = value;
			}
			get
			{
				return _postName;
			}
		}

		public string Sex
		{
			set
			{
				_sex = value;
			}
			get
			{
				return _sex;
			}
		}

		public DateTime Birthday
		{
			set
			{
				_birthday = value;
			}
			get
			{
				return _birthday;
			}
		}

		public string NativePlace
		{
			set
			{
				_nativePlace = value;
			}
			get
			{
				return _nativePlace;
			}
		}

		public string Folk
		{
			set
			{
				_folk = value;
			}
			get
			{
				return _folk;
			}
		}

		public int Wedding
		{
			set
			{
				_wedding = value;
			}
			get
			{
				return _wedding;
			}
		}

		public DateTime BeginDate
		{
			set
			{
				_beginDate = value;
			}
			get
			{
				return _beginDate;
			}
		}

		public string WorkPhone
		{
			set
			{
				_workPhone = value;
			}
			get
			{
				return _workPhone;
			}
		}

		public string HomePhone
		{
			set
			{
				_homePhone = value;
			}
			get
			{
				return _homePhone;
			}
		}

		public string MobilePhone
		{
			set
			{
				_mobilePhone = value;
			}
			get
			{
				return _mobilePhone;
			}
		}

		public string Email
		{
			set
			{
				_email = value;
			}
			get
			{
				return _email;
			}
		}

		public string Address
		{
			set
			{
				_address = value;
			}
			get
			{
				return _address;
			}
		}

		public string PostCode
		{
			set
			{
				_postCode = value;
			}
			get
			{
				return _postCode;
			}
		}

		public bool Dimission
		{
			set
			{
				_dimission = value;
			}
			get
			{
				return _dimission;
			}
		}

		public string Memo
		{
			set
			{
				_memo = value;
			}
			get
			{
				return _memo;
			}
		}

		public bool Flag
		{
			set
			{
				_flag = value;
			}
			get
			{
				return _flag;
			}
		}

		public int IsGroupLeader
		{
			set
			{
				_isGroupLeader = value;
			}
			get
			{
				return _isGroupLeader;
			}
		}

		public int TechnicianTypeID
		{
			set
			{
				_technicianTypeID = value;
			}
			get
			{
				return _technicianTypeID;
			}
		}

		public string PostNo
		{
			set
			{
				_postNo = value;
			}
			get
			{
				return _postNo;
			}
		}

		public string PinYinCode
		{
			set
			{
				_pinYinCode = value;
			}
			get
			{
				return _pinYinCode;
			}
		}

		public int LoginCount
		{
			set
			{
				_loginCount = value;
			}
			get
			{
				return _loginCount;
			}
		}

		public int LoginTime
		{
			set
			{
				_loginTime = value;
			}
			get
			{
				return _loginTime;
			}
		}

		public int PoliticalStatusID
		{
			get { return _politicalStatusID; }
			set { _politicalStatusID = value; }
		}

		public int EducationLevelID
		{
			get { return _educationLevelID; }
			set { _educationLevelID = value; }
		}

		public int EmployeeTypeID
		{
			get { return _employeeTypeID; }
			set { _employeeTypeID = value; }
		}

		public int TechnicalTitleID
		{
			get { return _technicalTitleID; }
			set { _technicalTitleID = value; }
		}

		public int WorkGroupLeaderTypeID
		{
			get { return _workGroupLeaderTypeID; }
			set { _workGroupLeaderTypeID = value; }
		}

		public int EducationEmployeeTypeID
		{
			get { return _educationEmployeeTypeID; }
			set { _educationEmployeeTypeID = value; }
		}

		public int CommitteeHeadShipID
		{
			get { return _committeeHeadShipID; }
			set { _committeeHeadShipID = value; }
		}

		public int EmployeeTransportTypeID
		{
			get { return _employeeTransportTypeID; }
			set { _employeeTransportTypeID = value ; }
		}

		public DateTime WorkDate
		{
			get { return _workDate; }
			set { _workDate = value; }
		}

		public string IdentifyCode
		{
			get { return _identifyCode; }
			set { _identifyCode = value;}
		}

		public string GraduateUniversity
		{
			get { return _graduateUniversity; }
			set { _graduateUniversity = value;}
		}

		public string StudyMajor
		{
			get { return _studyMajor; }
			set { _studyMajor = value;}
		}

		public int EmployeeLevelID
		{
			get { return _employeeLevelID;}
			set { _employeeLevelID = value;}
		}

		public int TeacherTypeID
		{
			get { return _teacherTypeID; }
			set { _teacherTypeID = value;}
		}

		public int ApprovePost
		{
			get { return _approvePost; }
			set { _approvePost = value; }
		}


		public EmployeeDetail()
		{ }

		public EmployeeDetail(int? employeeID,
            int? orgID,
            string orgName,
            string workNo,
            string employeeName,
            int? postID,
            string postName,
            string sex,
            DateTime? birthday,
			string nativePlace,
            string folk,
            int? wedding,
            DateTime? beginDate,
            string workPhone,
            string homePhone,
            string mobilePhone,
            string email,
            string address,
            string postCode,
            bool? dimission,
			string memo,
            int? isGroupLeader,
            int? technicianTypeID,
            string postNo,
			int? LoginCount,
			int? LoginTime,int? politicalStatusID, int? educationLevelID, 
			int? employeeTypeID, int? technicalTitleID, int? workGroupLeaderTypeID,
			int? educationEmployeeTypeID, int? committeeHeadShipID, int? employeeTransportTypeID,
			DateTime? workDate,string identifyCode,string graduateUniversity,string studyMajor,int? employeeLevelID,int? teacherTypeID, int? approvePost )
		{
			_employeeID = employeeID ?? _employeeID;
			_orgID = orgID ?? _orgID;
			_orgName = orgName;
			_workNo = workNo;
			_employeeName = employeeName;
			_postID = postID ?? _postID;
			_postName = postName;
			_sex = sex;
			_birthday = birthday ?? _birthday;
			_nativePlace = nativePlace;
			_folk = folk;
			_wedding = wedding ?? _wedding;
			_beginDate = beginDate ?? _beginDate;
			_workPhone = workPhone;
			_homePhone = homePhone;
			_mobilePhone = mobilePhone;
			_email = email;
			_address = address;
			_postCode = postCode;
			_dimission = dimission ?? _dimission;
			_memo = memo;
			_isGroupLeader = isGroupLeader ?? _isGroupLeader;
			_technicianTypeID = technicianTypeID ?? _technicianTypeID;
			_postNo = postNo;
			_loginCount = LoginCount ?? _loginCount;
			_loginTime = LoginTime ?? _loginTime;
			_politicalStatusID = politicalStatusID ?? _politicalStatusID;
			_educationLevelID = educationLevelID ?? _educationLevelID;
			_employeeTypeID = employeeTypeID ?? _employeeTypeID;
			_technicalTitleID = technicalTitleID ?? _technicalTitleID;
			_workGroupLeaderTypeID = workGroupLeaderTypeID ?? _workGroupLeaderTypeID;
			_educationEmployeeTypeID = educationEmployeeTypeID ?? _educationEmployeeTypeID;
			_committeeHeadShipID = committeeHeadShipID ?? _committeeHeadShipID;
			_employeeTransportTypeID = employeeTransportTypeID ?? _employeeTransportTypeID;
			_workDate = workDate ?? _workDate;
			_graduateUniversity = graduateUniversity;
			_studyMajor = studyMajor;
			_employeeLevelID = employeeLevelID ?? _employeeLevelID;
			_teacherTypeID = teacherTypeID ?? _teacherTypeID;
			_approvePost = approvePost ?? _approvePost;
			_identifyCode = identifyCode;
		}
	}
}
