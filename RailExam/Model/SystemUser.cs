using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    /// <summary>
    /// 业务实体：用户
    /// </summary>
    public class SystemUser
    {
        /// <summary>
		/// 内部成员变量
		/// </summary>
		private string _userID = string.Empty;
		private string _password = string.Empty;
        private int _employeeID = 0;
        private int _roleID = 0;
        private string _roleName = string.Empty;
		private string _memo = string.Empty;
        private bool _flag = true;
		
		/// <summary>
		/// 缺省构造函数
		/// </summary>
        public SystemUser() { }
        
        /// <summary>
		/// 带参数的构造函数
		/// </summary>
		/// <param name="userID">用户ID</param>
        /// <param name="password">密码</param>
        /// <param name="employeeID">职员ID</param>
        /// <param name="roleID">角色ID</param>
		/// <param name="memo">备注</param>
		public SystemUser(
            string userID,
            string password,
			int? employeeID,
			int? roleID,
            string roleName,
			string memo)
		{
            _userID = userID;
            _password = password;
            _employeeID = employeeID ?? _employeeID;
            _roleID = roleID ?? _roleID;
            _roleName = roleName;
			_memo = memo;
		}

        public string UserID
        {
            set
            {
                _userID = value;
            }
            get
            {
                return _userID;
            }
        }

        public string Password
        {
            set
            {
                _password = value;
            }
            get
            {
                return _password;
            }
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

        public int RoleID
        {
            set
            {
                _roleID = value;
            }
            get
            {
                return _roleID;
            }
        }

        public string RoleName
        {
            set
            {
                _roleName = value;
            }
            get
            {
                return _roleName;
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
    }
}
