using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
    /// <summary>
    /// ҵ���߼�����ɫ
    /// </summary>
    public class SystemRoleBLL
    {
        private static readonly SystemRoleDAL dal = new SystemRoleDAL();

        /// <summary>
        /// ��ȡ��ɫ����
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="roleName"></param>
        /// <param name="isAdmin"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">��ʼ��¼��</param>
        /// <param name="maximumRows">ÿҳ��¼����</param>
        /// <param name="orderBy">�����ַ�������"FieldName ASC"</param>
        /// <returns></returns>
        public IList<SystemRole> GetRoles(int roleID, string roleName, bool isAdmin, string description, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<SystemRole> systemRoleList = dal.GetRoles(roleID, roleName, isAdmin, description, memo, startRowIndex, maximumRows, orderBy);

            return systemRoleList;
        }

        public IList<SystemRole> GetRoles(int  suitRange)
        {
            IList<SystemRole> systemRoleList = dal.GetRoles(suitRange);

            SystemRole obj = new SystemRole();
            obj.RoleID = 0;
            obj.RoleName = "--��ѡ��--";
            obj.IsAdmin = false;
            obj.Description = "";
            obj.Memo = "";

            systemRoleList.Insert(0,obj);

            return systemRoleList;
        }

        public IList<SystemRole> GetRolesAll()
        {
            IList<SystemRole> systemRoleList = dal.GetRoles(1);
            return systemRoleList;
        }


        /// <summary>
        /// ��IDȡRole
        /// </summary>
        /// <param name="roleID">��ɫID</param>
        /// <returns>��ɫ</returns>
        public SystemRole GetRole(int roleID)
        {
            if (roleID < 1)
            {
                return null;
            }

            return dal.GetRole(roleID);
        }

        /// <summary>
        /// ������ɫ
        /// </summary>
        /// <param name="role">�����Ľ�ɫ��Ϣ</param>
        /// <returns></returns>
        public int AddRole(SystemRole systemRole)
        {
            int nRoleID = dal.AddRole(systemRole);
            if(nRoleID > 0)
            {
                SystemLogBLL systemLogBLL = new SystemLogBLL();

                systemLogBLL.WriteLog("������ɫ��"+ systemRole.RoleName +"��������Ϣ");
            }

            return nRoleID;
        }

        /// <summary>
        /// ���½�ɫ
        /// </summary>
        /// <param name="role">���º�Ľ�ɫ��Ϣ</param>
        public void UpdateRole(SystemRole systemRole)
        {
            if(dal.UpdateRole(systemRole))
            {
                SystemLogBLL systemLogBLL = new SystemLogBLL();

                systemLogBLL.WriteLog("�޸Ľ�ɫ��" + systemRole.RoleName + "��������Ϣ");
            }
        }

        /// <summary>
        /// ɾ����ɫ
        /// </summary>
        /// <param name="role">Ҫɾ���Ľ�ɫ</param>
        public void DeleteRole(SystemRole systemRole)
        {
            DeleteRole(systemRole.RoleID);
        }

        /// <summary>
        /// ɾ����ɫ
        /// </summary>
        /// <param name="roleID">Ҫɾ���Ľ�ɫID</param>
        public void DeleteRole(int roleID)
        {
            string strName = GetRole(roleID).RoleName;
            SystemLogBLL systemLogBLL = new SystemLogBLL();
            systemLogBLL.WriteLog("ɾ����ɫ��" + strName + "��������Ϣ");
            dal.DeleteRole(roleID);
        }

        /// <summary>
        /// ��ȡ��ѯ�����¼��
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="roleName"></param>
        /// <param name="isAdmin"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public int GetCount(int roleID, string roleName, bool isAdmin, string description, string memo)
        {
            return dal.RecordCount;
        }
    }
}
