using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	public class SystemUserLogin
	{
		        /// <summary>
		/// �ڲ���Ա����
		/// </summary>
        private int _employeeID = 0;
        private string _employeeName = string.Empty;
		private string _orgName = string.Empty;
		private string _postName = string.Empty;
		private string _ipAddress = string.Empty;
		
		/// <summary>
		/// ȱʡ���캯��
		/// </summary>
        public SystemUserLogin() { }
        
        /// <summary>
		/// �������Ĺ��캯��
		/// </summary>
		/// <param name="userID">�û�ID</param>
        /// <param name="password">����</param>
        /// <param name="employeeID">ְԱID</param>
        /// <param name="roleID">��ɫID</param>
		/// <param name="memo">��ע</param>
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
