using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
    /// <summary>
    /// ҵ���߼����û�
    /// </summary>
    public class SystemUserBLL
    {
        private static readonly SystemUserDAL dal = new SystemUserDAL();

        /// <summary>
        /// ��ȡ�û�����
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <param name="employeeID"></param>
        /// <param name="roleID"></param>
        /// <param name="roleName"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">��ʼ��¼��</param>
        /// <param name="maximumRows">ÿҳ��¼����</param>
        /// <param name="orderBy">�����ַ�������"FieldName ASC"</param>
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
        /// ��IDȡUser
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
		/// ��IDȡUser
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
        /// �����û�
        /// </summary>
        /// <param name="user">�������û���Ϣ</param>
        /// <returns></returns>
        public void AddUser(SystemUser systemUser)
        {
            if(dal.AddUser(systemUser))
            {
                EmployeeBLL objBll = new EmployeeBLL();
                Employee obj = objBll.GetEmployee(systemUser.EmployeeID);
                SystemLogBLL systemLogBLL = new SystemLogBLL();
                systemLogBLL.WriteLog("����Ա����" + obj.EmployeeName + "��"+  obj.WorkNo+  "�����û���¼��Ϣ");
            }
        }

        /// <summary>
        /// �����û�
        /// </summary>
        /// <param name="user">���º���û���Ϣ</param>
        public void UpdateUser(SystemUser systemUser)
        {
            if (dal.UpdateUser(systemUser) && systemUser.Flag)
            {
                EmployeeBLL objBll = new EmployeeBLL();
                Employee obj = objBll.GetEmployee(systemUser.EmployeeID);
                SystemLogBLL systemLogBLL = new SystemLogBLL();
                systemLogBLL.WriteLog("�޸�Ա����" + obj.EmployeeName + "��" + obj.WorkNo + "�����û���¼��Ϣ");
            }
        }

        /// <summary>
        /// ɾ���û�
        /// </summary>
        /// <param name="user">Ҫɾ�����û�</param>
        public void DeleteUser(SystemUser systemUser)
        {
            DeleteUser(systemUser.UserID);
        }

        /// <summary>
        /// ɾ���û�
        /// </summary>
        /// <param name="userID">Ҫɾ�����û�ID</param>
        public void DeleteUser(string userID)
        {
            EmployeeBLL objBll = new EmployeeBLL();
            Employee obj = objBll.GetEmployee(GetUser(userID).EmployeeID);
            SystemLogBLL systemLogBLL = new SystemLogBLL();
            systemLogBLL.WriteLog("ɾ��Ա����" + obj.EmployeeName + "��" + obj.WorkNo + "�����û���¼��Ϣ");
            dal.DeleteUser(userID);
        }

        /// <summary>
        /// ��ȡ��ѯ�����¼��
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
