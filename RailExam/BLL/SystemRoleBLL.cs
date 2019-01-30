using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
    /// <summary>
    /// 业务逻辑：角色
    /// </summary>
    public class SystemRoleBLL
    {
        private static readonly SystemRoleDAL dal = new SystemRoleDAL();

        /// <summary>
        /// 获取角色数据
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="roleName"></param>
        /// <param name="isAdmin"></param>
        /// <param name="description"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">起始记录行</param>
        /// <param name="maximumRows">每页记录条数</param>
        /// <param name="orderBy">排序字符串，如"FieldName ASC"</param>
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
            obj.RoleName = "--请选择--";
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
        /// 按ID取Role
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <returns>角色</returns>
        public SystemRole GetRole(int roleID)
        {
            if (roleID < 1)
            {
                return null;
            }

            return dal.GetRole(roleID);
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="role">新增的角色信息</param>
        /// <returns></returns>
        public int AddRole(SystemRole systemRole)
        {
            int nRoleID = dal.AddRole(systemRole);
            if(nRoleID > 0)
            {
                SystemLogBLL systemLogBLL = new SystemLogBLL();

                systemLogBLL.WriteLog("新增角色“"+ systemRole.RoleName +"”基本信息");
            }

            return nRoleID;
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role">更新后的角色信息</param>
        public void UpdateRole(SystemRole systemRole)
        {
            if(dal.UpdateRole(systemRole))
            {
                SystemLogBLL systemLogBLL = new SystemLogBLL();

                systemLogBLL.WriteLog("修改角色“" + systemRole.RoleName + "”基本信息");
            }
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="role">要删除的角色</param>
        public void DeleteRole(SystemRole systemRole)
        {
            DeleteRole(systemRole.RoleID);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleID">要删除的角色ID</param>
        public void DeleteRole(int roleID)
        {
            string strName = GetRole(roleID).RoleName;
            SystemLogBLL systemLogBLL = new SystemLogBLL();
            systemLogBLL.WriteLog("删除角色“" + strName + "”基本信息");
            dal.DeleteRole(roleID);
        }

        /// <summary>
        /// 获取查询结果记录数
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
