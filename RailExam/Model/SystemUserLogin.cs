using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	public class SystemUserLogin
	{
		        /// <summary>
		/// 内部成员变量
		/// </summary>
        private int _employeeID = 0;
        private string _employeeName = string.Empty;
		private string _orgName = string.Empty;
		private string _postName = string.Empty;
		private string _ipAddress = string.Empty;
		
		/// <summary>
		/// 缺省构造函数
		/// </summary>
        public SystemUserLogin() { }
        
        /// <summary>
		/// 带参数的构造函数
		/// </summary>
		/// <param name="userID">用户ID</param>
        /// <param name="password">密码</param>
        /// <param name="employeeID">职员ID</param>
        /// <param name="roleID">角色ID</param>
		/// <param name="memo">备注</param>
		public SystemUserLogin(
			int? employeeID,
            string employeeName,
			string orgName,
			string postName,
			string ipAddress)
		{
			_employeeID = employeeID ?? _employeeID;
			_employeeName = employeeName;
			_orgName = orgName;
			_postName = postName;
			_ipAddress = ipAddress;
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

		public string IPAddress
		{
			set
			{
				_ipAddress = value;
			}
			get
			{
				return _ipAddress;
			}
		}
	}
}
