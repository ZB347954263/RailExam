using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
    /// <summary>
    /// 业务逻辑：角色权限
    /// </summary>
    public class SystemRoleRightBLL
    {
        private static readonly SystemRoleRightDAL dal = new SystemRoleRightDAL();

        /// <summary>
        /// 按ID取Role
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns>角色</returns>
        public IList<SystemRoleRight> GetRoleRights(int roleID)
        {
            if (roleID < 1)
            {
                return null;
            }

            return dal.GetRoleRights(roleID);
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="roleID">更新的角色ID</param>
        /// <param name="systemRoleRights">更新后的角色信息</param>
        public void UpdateRoleRight(int roleID, IList<SystemRoleRight> systemRoleRights)
        {
            if(dal.UpdateRoleRight(roleID, systemRoleRights))
            {
                SystemRoleBLL objBll = new SystemRoleBLL();
                string strName = objBll.GetRole(roleID).RoleName;
                SystemLogBLL systemLogBLL = new SystemLogBLL();

                systemLogBLL.WriteLog("修改角色“"+ strName +"”角色权限");
            }
        }
		/// <summary>
		/// 按ID取Role
		/// </summary>
		/// <param name="roleID">角色ID</param>
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
		/// 更新范围权限
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

				systemLogBLL.WriteLog("修改角色“" + strName + "”范围权限");
			}
		}
		
    }
}
