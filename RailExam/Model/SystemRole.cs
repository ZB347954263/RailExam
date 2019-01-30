using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    /// <summary>
    /// 业务实体：角色
    /// </summary>
    public class SystemRole
    {
        /// <summary>
        /// 内部成员变量
        /// </summary>
        private int _roleID = 1;
        private string _roleName = string.Empty;
        private bool _isAdmin = false;
        private string _description = string.Empty;
        private string _memo = string.Empty;
        private int _useType = 0;
        private int _railsystemid = 0;
        private string _railsystemname = string.Empty;
        
        /// <summary>
		/// 缺省构造函数
		/// </summary>
        public SystemRole() {}

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="roleName"></param>
        /// <param name="isAdmin"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="useType"></param>
        public SystemRole(
            int? roleID,
            string roleName,
            bool? isAdmin,
            string description,
			string memo,
            int? useType)
        {
            _roleID = roleID ?? _roleID;
            _roleName = roleName;
            _isAdmin = isAdmin ?? _isAdmin;
            _description = description;
            _memo = memo;
            _useType = useType ?? _useType;
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

        public bool IsAdmin
        {
            set
            {
                _isAdmin = value;
            }
            get
            {
                return _isAdmin;
            }
        }

        public string Description
        {
            set
            {
                _description = value;
            }
            get
            {
                return _description;
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

        public int RailSystemID
        {
            set
            {
                _railsystemid = value;
            }
            get
            {
                return _railsystemid;
            }
        }

        public string RailSystemName
        {
            set
            {
                _railsystemname = value;
            }
            get
            {
                return _railsystemname;
            }
        }
    }
}
