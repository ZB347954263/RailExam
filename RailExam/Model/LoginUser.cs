using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	public class LoginUser
	{
        /// <summary>
		/// 内部成员变量
		/// </summary>
		private string _userID = string.Empty;
		private string _password = string.Empty;
        private int _employeeID = 0;
		private string _employeeName = string.Empty;
		private int _orgID = 0;
		private string _orgName = string.Empty;
		private int _postID = 0;
		private string _postName = string.Empty;
		private int _roleID = 0;
		private string _roleName = string.Empty;
		private bool _isAdmin = false;
	    private bool _isGroupLeader = false;
	    private int _technicianTypeID = 0;
		private IList<FunctionRight> _functionRights;
	    private int _suitRange = 0;
	    private int _stationOrgID = 0;
	    private int _useType = 0;
	    private int _railSystemID = 0;
	    private bool _isDangan = false;

		/// <summary>
		/// 缺省构造函数
		/// </summary>
		public LoginUser(){}
        
        /// <summary>
		/// 带参数的构造函数
        /// </summary>
		/// <param name="userID">用户ID</param>
        /// <param name="password">密码</param>
        /// <param name="employeeID">职员ID</param>
        /// <param name="employeeName">职员姓名</param>
        /// <param name="orgID">组织机构ID</param>
        /// <param name="orgName">组织机构名称</param>
        /// <param name="postID">工作岗位ID</param>
        /// <param name="postName">工作岗位名称</param>
        /// <param name="roleID">角色ID</param>
		/// <param name="roleName">角色名称</param>
		/// <param name="isAdmin">是否系统管理员</param>
		public LoginUser(
            string userID,
            string password,
			int employeeID,
            string employeeName,
            int orgID,
            string orgName,
            int postID,
            string postName,
			int roleID,
        	string roleName,
        	bool isAdmin,
            bool? isGroupLeader,
            int? technicianTypeID,
            int? suitRange,
            int? stationOrgID,
            int? useType,
            int? railsystemId,
			IList<FunctionRight> functionRights)
		{
			_userID = userID;
            _password = password;
			_employeeID = employeeID;
			_employeeName = employeeName;
			_orgID = orgID;
			_orgName = orgName;
			_postID = postID;
			_postName = postName;
			_roleID = roleID;
			_roleName = roleName;
			_isAdmin = isAdmin;
            _isGroupLeader = isGroupLeader ?? _isGroupLeader;
            _technicianTypeID = technicianTypeID ?? _technicianTypeID;
            _suitRange = suitRange ?? _suitRange;
            _stationOrgID = stationOrgID ?? _stationOrgID;
            _functionRights = functionRights;
            _useType = useType ?? _useType;
            _railSystemID = railsystemId ?? _railSystemID;
		}

	    public int RailSystemID
	    {
            get
            {
                return _railSystemID;
            }
            set
            {
                _railSystemID = value;
            } 
	    }

		//用户ID
		public string UserID
		{
			get
			{
				return _userID;
			}
			set
			{
				_userID = value;
			}
		}

		//密码
		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
			}
		}

		//职员ID
		public int EmployeeID
		{
			get
			{
				return _employeeID;
			}
			set
			{
				_employeeID = value;
			}
		}

		//职员姓名
		public string EmployeeName
		{
			get
			{
				return _employeeName;
			}
			set
			{
				_employeeName = value;
			}
		}

		//用户组织机构ID
		public int OrgID
		{
			get
			{
				return _orgID;
			}
			set
			{
				_orgID = value;
			}
		}

		//用户组织机构名称
		public string OrgName
		{
			get
			{
				return _orgName;
			}
			set
			{
				_orgName = value;
			}
		}

        //用户工作岗位ID
		public int PostID
		{
			get
			{
				return _postID;
			}
			set
			{
				_postID = value;
			}
		}

        //用户工作岗位名称
		public string PostName
		{
			get
			{
				return _postName;
			}
			set
			{
				_postName = value;
			}
		}

		//用户角色ID
		public int RoleID
		{
			get
			{
				return _roleID;
			}
			set
			{
				_roleID = value;
			}
		}

        //用户角色名称
		public string RoleName
		{
			get
			{
				return _roleName;
			}
			set
			{
				_roleName = value;
			}
		}

		//是否系统管理员
		public bool IsAdmin
		{
			get
			{
				return _isAdmin;
			}
			set
			{
				_isAdmin = value;
			}
		}

		//功能权限
		public IList<FunctionRight> FunctionRights
		{
			get
			{
				return _functionRights;
			}
			set
			{
				_functionRights = value;
			}
		}
        public bool IsGroupLearder
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

	    public int SuitRange
	    {
	        set
	        {
	            _suitRange = value;
	        }
            get
            {
                return _suitRange;
            }
	    }

	    public int StationOrgID
	    {
	        set
	        {
	            _stationOrgID = value;
	        }
            get
            {
                return _stationOrgID;
            }
	    }

	    public int UseType
	    {
	        set
	        {
	            _useType = value;
	        }
            get
            {
                return _useType;
            }
	    }

        public bool IsDangan
        {
            set
            {
                _isDangan = value;
            }
            get
            {
                return _isDangan;
            }
        }
	}
}
