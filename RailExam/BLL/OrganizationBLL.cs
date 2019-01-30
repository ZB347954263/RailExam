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
	/// ҵ���߼�����֯����
	/// </summary>
	public class OrganizationBLL
	{
		private static readonly OrganizationDAL dal = new OrganizationDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

		/// <summary>
		/// ��ȡ��֯��������
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
		/// <param name="startRowIndex">��ʼ��¼��</param>
		/// <param name="maximumRows">ÿҳ��¼����</param>
		/// <param name="orderBy">�����ַ�������"FieldName ASC"</param>
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
		/// ��IDȡORG
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
		/// ������֯����
		/// </summary>
		/// <param name="organization">��������֯������Ϣ</param>
		/// <returns></returns>
		public int AddOrganization(Organization organization)
		{
            objLogBll.WriteLog("������֯������"+ organization.ShortName +"��������Ϣ");
			return dal.AddOrganization(organization);
		}

		/// <summary>
		/// ������֯����
		/// </summary>
		/// <param name="organization">��������֯������Ϣ</param>
		/// <returns></returns>
		public int AddOrganization(Database db, DbTransaction trans, Organization organization)
		{
			return dal.AddOrganization(db,trans,organization);
		}

		/// <summary>
		/// ������֯����
		/// </summary>
		/// <param name="organization">���º����֯������Ϣ</param>
		public void UpdateOrganization(Organization organization)
		{
            objLogBll.WriteLog("�޸���֯������" + organization.ShortName + "��������Ϣ");
			dal.UpdateOrganization(organization);
		}

		/// <summary>
		/// ɾ����֯����
		/// </summary>
		/// <param name="organization">Ҫɾ������֯����</param>
		public void DeleteOrganization(Organization organization)
		{
		    int code = 0;
		    string strName = GetOrganization(organization.OrganizationId).ShortName;
            dal.DeleteOrganization(organization.OrganizationId,ref code);
            if (code == 0)
            {
                objLogBll.WriteLog("ɾ����֯������" + strName + "��������Ϣ");
            }
        }

		/// <summary>
		/// ɾ����֯����
		/// </summary>
		/// <param name="organizationId">Ҫɾ������֯����ID</param>
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
                objLogBll.WriteLog("ɾ����֯������" + strName + "��������Ϣ");
            }
		}

		/// <summary>
		/// ��ȡ��ѯ�����¼��
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
        /// �Ƿ�������ƽڵ�
        /// </summary>
        /// <param name="organizationId"></param>
        /// <returns></returns>
        public bool MoveUp(int organizationId)
        {
            return dal.Move(organizationId, true);
        }

        /// <summary>
        /// �Ƿ�������ƽڵ�
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

		//�����Ƿ��Զ�����
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
