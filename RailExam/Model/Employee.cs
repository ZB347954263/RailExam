using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    /// <summary>
    /// 业务实体：员工
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// 内部成员变量
        /// </summary>
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
        private bool _isonpost = true;
        private string _memo = string.Empty;
        private bool _flag = false;
        private int _isGroupLeader = 2;
        private int _technicianTypeID = 0;
        private string _postNo = string.Empty;
        private string _pinYinCode = string.Empty;
        private int _rowNum = 0;
		private int _loginCount = 0;
		private int _loginTime = 0;
        private string _strWorkNo = string.Empty;
        
        /// <summary>
		/// 缺省构造函数
		/// </summary>
        public Employee(){}

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="employeeID">职员ID</param>
        /// <param name="orgID">组织机构ID</param>
        /// <param name="orgName">组织机构简称</param>
        /// <param name="workNo">工号</param>
        /// <param name="employeeName">姓名</param>
        /// <param name="postID">工作岗位ID</param>
        /// <param name="postName">工作岗位名称</param>
        /// <param name="sex">性别</param>
        /// <param name="birthday">生日</param>
        /// <param name="nativePlace">籍贯</param>
        /// <param name="folk">民族</param>
        /// <param name="wedding">婚否</param>
        /// <param name="beginDate">入职日期</param>
        /// <param name="workPhone">办公电话</param>
        /// <param name="homePhone">家庭电话</param>
        /// <param name="mobilePhone">移动电话</param>
        /// <param name="email">电子邮件</param>
        /// <param name="address">通讯地址</param>
        /// <param name="postCode">邮政编码</param>
        /// <param name="dimission">已离职</param>
        /// <param name="isGroupLeader">是否班组长</param>
        /// <param name="technicianTypeID">技师类别</param>
        /// <param name="postNo">车间</param>
        /// <param name="memo">备注</param>
        public Employee(
            int? employeeID,
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
            bool? isonpost,
			string memo,
            int? isGroupLeader,
            int? technicianTypeID,
            string postNo,
			int? LoginCount,
			int? LoginTime)
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
            _isonpost = isonpost ?? _isonpost;
			_memo = memo;
            _isGroupLeader = isGroupLeader ?? _isGroupLeader;
            _technicianTypeID = technicianTypeID ?? _technicianTypeID;
            _postNo = postNo;
			_loginCount = LoginCount ?? _loginCount;
			_loginTime = LoginTime ?? _loginTime;
		}

        public int EmployeeID
        {
            set
            {
                _employeeID = value;
            }
            get
            {
                return _employeeID;
            }
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
        
        public bool IsOnPost
        {
            set
            {
                _isonpost = value;
            }
            get
            {
                return _isonpost;
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

        public int RowNum
        {
            set
            {
                _rowNum = value;
            }
            get
            {
                return _rowNum;
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

        public string StrWorkNo
        {
            set
            {
                _strWorkNo = value;
            }
            get
            {
                return _strWorkNo;
            }
        }
    }
}
