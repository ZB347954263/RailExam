using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
    /// <summary>
    /// ҵ���߼�����ɫȨ��
    /// </summary>
    public class SystemRoleRightBLL
    {
        private static readonly SystemRoleRightDAL dal = new SystemRoleRightDAL();

        /// <summary>
        /// ��IDȡRole
        /// </summary>
        /// <param name="roleID">��ɫID</param>
        /// <returns>��ɫ</returns>
        public IList<SystemRoleRight> GetRoleRights(int roleID)
        {
            if (roleID < 1)
            {
                return null;
            }

            return dal.GetRoleRights(roleID);
        }

        /// <summary>
        /// ���½�ɫ
        /// </summary>
        /// <param name="roleID">���µĽ�ɫID</param>
        /// <param name="systemRoleRights">���º�Ľ�ɫ��Ϣ</param>
        public void UpdateRoleRight(int roleID, IList<SystemRoleRight> systemRoleRights)
        {
            if(dal.UpdateRoleRight(roleID, systemRoleRights))
            {
                SystemRoleBLL objBll = new SystemRoleBLL();
                string strName = objBll.GetRole(roleID).RoleName;
                SystemLogBLL systemLogBLL = new SystemLogBLL();

                systemLogBLL.WriteLog("�޸Ľ�ɫ��"+ strName +"����ɫȨ��");
            }
        }
		/// <summary>
		/// ��IDȡRole
		/// </summary>
		/// <param name="roleID">��ɫID</param>
		/// <returns></returns>
		public IList<SystemRoleRight> GetRoleRightsClass(int roleID)
		{
			if (roleID < 1)
			{
				return null;
			}

			//return dal.GetRoleRights(roleID);
			return dal.GetRoleRightsClass(roleID);
		}
		/// <summary>
		/// ���·�ΧȨ��
		/// </summary>
		/// <param name="roleID"></param>
		/// <param name="systemRoleRights"></param>
		public void UpdateRoleRightClass(int roleID, IList<SystemRoleRight> systemRoleRights)
		{
			if (dal.UpdateRoleRightClass(roleID, systemRoleRights))
			{
				SystemRoleBLL objBll = new SystemRoleBLL();
				string strName = objBll.GetRole(roleID).RoleName;
				SystemLogBLL systemLogBLL = new SystemLogBLL();

				systemLogBLL.WriteLog("�޸Ľ�ɫ��" + strName + "����ΧȨ��");
			}
		}
		
    }
}
