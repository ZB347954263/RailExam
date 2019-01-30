using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    /// <summary>
    /// ҵ��ʵ�壺�û�
    /// </summary>
    public class SystemUser
    {
        /// <summary>
		/// �ڲ���Ա����
		/// </summary>
		private string _userID = string.Empty;
		private string _password = string.Empty;
        private int _employeeID = 0;
        private int _roleID = 0;
        private string _roleName = string.Empty;
		private string _memo = string.Empty;
        private bool _flag = true;
		
		/// <summary>
		/// ȱʡ���캯��
		/// </summary>
        public SystemUser() { }
        
        /// <summary>
		/// �������Ĺ��캯��
		/// </summary>
		/// <param name="userID">�û�ID</param>
        /// <param name="password">����</param>
        /// <param name="employeeID">ְԱID</param>
        /// <param name="roleID">��ɫID</param>
		/// <param name="memo">��ע</param>
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
