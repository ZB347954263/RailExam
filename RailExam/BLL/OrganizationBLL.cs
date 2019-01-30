using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace RailExam.BLL
{
	/// <summary>
	/// 业务逻辑：组织机构
	/// </summary>
	public class OrganizationBLL
	{
		private static readonly OrganizationDAL dal = new OrganizationDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

		/// <summary>
		/// 获取组织机构数据
		/// </summary>
		/// <param name="organizationId"></param>
		/// <param name="parentId"></param>
		/// <param name="idPath"></param>
		/// <param name="levelNum"></param>
		/// <param name="orderIndex"></param>
		/// <param name="shortName"></param>
		/// <param name="fullName"></param>
		/// <param name="address"></param>
		/// <param name="postCode"></param>
		/// <param name="contactPerson"></param>
		/// <param name="phone"></param>
		/// <param name="webSite"></param>
		/// <param name="email"></param>
		/// <param name="description"></param>
		/// <param name="memo"></param>
		/// <param name="startRowIndex">起始记录行</param>
		/// <param name="maximumRows">每页记录条数</param>
		/// <param name="orderBy">排序字符串，如"FieldName ASC"</param>
		/// <returns></returns>
		public IList<Organization> GetOrganizations(int organizationId, int parentId, string idPath, int levelNum, int orderIndex,
			string shortName, string fullName, string address, string postCode, string contactPerson,
			string phone, string webSite, string email, string description, string memo, int startRowIndex, int maximumRows, string orderBy)
		{
			IList<Organization> organizationList = dal.GetOrganizations(organizationId, parentId, idPath, levelNum, orderIndex,
																		shortName, fullName, address, postCode, contactPerson, phone, webSite, email, description, memo, startRowIndex, maximumRows, orderBy);

			return organizationList;
		}

		public IList<Organization> GetOrganizations(int startRowIndex, int maximumRows, string orderBy)
		{
			IList<Organization> organizationList = dal.GetOrganizations(0, 0, "", 0, 0,
																		"", "", "", "", "", "", "", "", "", "", startRowIndex, maximumRows, orderBy);

			return organizationList;
		}

		public IList<Organization> GetOrganizations()
		{
			IList<Organization> organizationList = dal.GetOrganizations();

			return organizationList;
		}

		public IList<Organization> GetOrganizations(string tableName)
		{
			IList<Organization> organizationList = dal.GetOrganizations(tableName);

			return organizationList;
		}

        public IList<Organization> GetOrganizations(int orgID)
        {
            IList<Organization> organizationList = dal.GetOrganizations(orgID);

            return organizationList;
        }

		/// <summary>
		/// 按ID取ORG
		/// </summary>
		/// <param name="organizationId">ID</param>
		/// <returns>ORG</returns>
		public Organization GetOrganization(int organizationId)
		{
			if (organizationId < 1)
			{
				return null;
			}

			return dal.GetOrganization(organizationId);
		}

        public int GetStationOrgID(int orgID)
        {
            return dal.GetStationOrgID(orgID);
        }

		/// <summary>
		/// 新增组织机构
		/// </summary>
		/// <param name="organization">新增的组织机构信息</param>
		/// <returns></returns>
		public int AddOrganization(Organization organization)
		{
            objLogBll.WriteLog("新增组织机构“"+ organization.ShortName +"”基本信息");
			return dal.AddOrganization(organization);
		}

		/// <summary>
		/// 新增组织机构
		/// </summary>
		/// <param name="organization">新增的组织机构信息</param>
		/// <returns></returns>
		public int AddOrganization(Database db, DbTransaction trans, Organization organization)
		{
			return dal.AddOrganization(db,trans,organization);
		}

		/// <summary>
		/// 更新组织机构
		/// </summary>
		/// <param name="organization">更新后的组织机构信息</param>
		public void UpdateOrganization(Organization organization)
		{
            objLogBll.WriteLog("修改组织机构“" + organization.ShortName + "”基本信息");
			dal.UpdateOrganization(organization);
		}

		/// <summary>
		/// 删除组织机构
		/// </summary>
		/// <param name="organization">要删除的组织机构</param>
		public void DeleteOrganization(Organization organization)
		{
		    int code = 0;
		    string strName = GetOrganization(organization.OrganizationId).ShortName;
            dal.DeleteOrganization(organization.OrganizationId,ref code);
            if (code == 0)
            {
                objLogBll.WriteLog("删除组织机构“" + strName + "”基本信息");
            }
        }

		/// <summary>
		/// 删除组织机构
		/// </summary>
		/// <param name="organizationId">要删除的组织机构ID</param>
		public void DeleteOrganization(int organizationId,ref int errorCode)
		{
		    int code = 0;
            string strName = GetOrganization(organizationId).ShortName;
			EmployeeBLL objEmployeeBll = new EmployeeBLL();
			if (objEmployeeBll.GetEmployeeByWhereClause("a.Org_ID=" + organizationId).Count > 0)
			{
				errorCode = 1;
				return;
			}

			dal.DeleteOrganization(organizationId,ref code);
		    errorCode = code;

            if(code == 0)
            {
                objLogBll.WriteLog("删除组织机构“" + strName + "”基本信息");
            }
		}

		/// <summary>
		/// 获取查询结果记录数
		/// </summary>
		/// <param name="organizationId"></param>
		/// <param name="parentId"></param>
		/// <param name="idPath"></param>
		/// <param name="levelNum"></param>
		/// <param name="orderIndex"></param>
		/// <param name="shortName"></param>
		/// <param name="fullName"></param>
		/// <param name="address"></param>
		/// <param name="postCode"></param>
		/// <param name="contactPerson"></param>
		/// <param name="phone"></param>
		/// <param name="webSite"></param>
		/// <param name="email"></param>
		/// <param name="description"></param>
		/// <param name="memo"></param>
		/// <returns></returns>
		public int GetCount(int organizationId, int parentId, string idPath, int levelNum, int orderIndex,
			string shortName, string fullName, string address, string postCode, string contactPerson,
			string phone, string webSite, string email, string description, string memo)
		{
			return dal.RecordCount;
		}

        /// <summary>
        /// 是否可以上移节点
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        public bool MoveUp(int organizationId)
        {
            return dal.Move(organizationId, true);
        }

        /// <summary>
        /// 是否可以下移节点
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        public bool MoveDown(int organizationId)
        {
            return dal.Move(organizationId, false);
        }

        public IList<Organization> GetOrganizationsByLevel(int level)
        {
            return dal.GetOrganizationsByLevel(level);
        }

		public IList<Organization> GetOrganizationsByWhereClause(Database db, DbTransaction trans, string whereClause)
		{
			return dal.GetOrganizationsByWhereClause(db,trans,whereClause);
		}

		public IList<Organization> GetOrganizationsByWhereClause(string whereClause)
		{
			return dal.GetOrganizationsByWhereClause(whereClause);
		}


        public IList<Organization> GetOrganizationsByParentID(int parentID)
        {
            return dal.GetOrganizationsByParentID(parentID);
        }

        public int GetOrgIDByOrgNamePath(string strNamePath)
        {
            return dal.GetOrgIDByOrgNamePath(strNamePath);
        }
        
        public void UpdateOrgSynchronizeTime(int orgid, string time)
        {
            dal.UpdateOrgSynchronizeTime(orgid, time);
        }

		public void UpdateOrgService(int orgid, string netName, string ipAdress,bool isAutoUpload)
		{
			dal.UpdateOrgService(orgid,netName,ipAdress,isAutoUpload);
		}

		public string GetOrgSynchronizeTime(int orgid)
        {
            return dal.GetOrgSynchronizeTime(orgid);
        }

		public string GetOrgIPAddress(int orgid)
		{
			return dal.GetOrgIPAddress(orgid);
		}

		public string GetOrgNetName(int orgid)
		{
			return dal.GetOrgNetName(orgid);
		}

		//考试是否自动结束
		public bool IsAutoUpload(int orgid)
		{
			return dal.IsAutoUpload(orgid);
		}

		public int[] GetUnitID(int orgid)
		{
			return dal.GetUnitID(orgid);
		}
	}
}
