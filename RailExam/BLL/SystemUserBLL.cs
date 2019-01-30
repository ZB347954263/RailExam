using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
    /// <summary>
    /// 业务逻辑：用户
    /// </summary>
    public class SystemUserBLL
    {
        private static readonly SystemUserDAL dal = new SystemUserDAL();

        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="employeeID"></param>
        /// <param name="roleID"></param>
        /// <param name="roleName"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">起始记录行</param>
        /// <param name="maximumRows">每页记录条数</param>
        /// <param name="orderBy">排序字符串，如"FieldName ASC"</param>
        /// <returns></returns>
        public IList<SystemUser> GetUsers(string userID, string password, int employeeID, 
                int roleID, string roleName, string memo, int startRowIndex, int maximumRows, string orderBy)
		{
            IList<SystemUser> userList = dal.GetUsers(userID, password, employeeID, roleID, roleName,
                                                memo, startRowIndex, maximumRows, orderBy);

            return userList;
		}

        public IList<SystemUser> GetUsers()
        {
            IList<SystemUser> systemUserList = dal.GetUsers();

            return systemUserList;
        }

        public IList<SystemUser> GetUsersByEmployeeID(int employeeID)
        {
            IList<SystemUser> systemUserList = dal.GetUsersByEmployeeID(employeeID);

            return systemUserList;
        }
        public IList<SystemUser> GetUsersByRoleID(int roleid)
        {
            return dal.GetUsersByRoleID(roleid);
        }

        public SystemUser GetUserByEmployeeID(int employeeID)
        {
            return dal.GetUserByEmployeeID(employeeID);
        }

        /// <summary>
        /// 按ID取User
        /// </summary>
        /// <param name="userID">ID</param>
        /// <returns>User</returns>
        public SystemUser GetUser(string userID)
        {
            if(string.IsNullOrEmpty(userID))
            {
                return null;
            }

            return dal.GetUser(userID);
        }

		/// <summary>
		/// 按ID取User
		/// </summary>
		/// <param name="userID">ID</param>
		/// <returns>User</returns>
		public SystemUser GetUserByOrgID(string userID,int orgID)
		{
			if (string.IsNullOrEmpty(userID))
			{
				return null;
			}

			return dal.GetUser(userID,orgID);
		}

        public SystemUser GetUser(string userID, string password)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            return dal.GetUser(userID, password);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user">新增的用户信息</param>
        /// <returns></returns>
        public void AddUser(SystemUser systemUser)
        {
            if(dal.AddUser(systemUser))
            {
                EmployeeBLL objBll = new EmployeeBLL();
                Employee obj = objBll.GetEmployee(systemUser.EmployeeID);
                SystemLogBLL systemLogBLL = new SystemLogBLL();
                systemLogBLL.WriteLog("新增员工“" + obj.EmployeeName + "（"+  obj.WorkNo+  "）”用户登录信息");
            }
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="user">更新后的用户信息</param>
        public void UpdateUser(SystemUser systemUser)
        {
            if (dal.UpdateUser(systemUser) && systemUser.Flag)
            {
                EmployeeBLL objBll = new EmployeeBLL();
                Employee obj = objBll.GetEmployee(systemUser.EmployeeID);
                SystemLogBLL systemLogBLL = new SystemLogBLL();
                systemLogBLL.WriteLog("修改员工“" + obj.EmployeeName + "（" + obj.WorkNo + "）”用户登录信息");
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user">要删除的用户</param>
        public void DeleteUser(SystemUser systemUser)
        {
            DeleteUser(systemUser.UserID);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userID">要删除的用户ID</param>
        public void DeleteUser(string userID)
        {
            EmployeeBLL objBll = new EmployeeBLL();
            Employee obj = objBll.GetEmployee(GetUser(userID).EmployeeID);
            SystemLogBLL systemLogBLL = new SystemLogBLL();
            systemLogBLL.WriteLog("删除员工“" + obj.EmployeeName + "（" + obj.WorkNo + "）”用户登录信息");
            dal.DeleteUser(userID);
        }

        /// <summary>
        /// 获取查询结果记录数
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="employeeID"></param>
        /// <param name="roleID"></param>
        /// <param name="roleName"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public int GetCount(string userID, string password, int employeeID, int roleID, string roleName, string memo)
        {
            return dal.RecordCount;
        }

        public void AddUserImport(SystemUser systemUser)
        {
            dal.AddUser(systemUser);
        }

        public void UpdateUserPsw(string userID, string password)
        {
            dal.UpdateUserPsw(userID, password);
        }
    }
}
