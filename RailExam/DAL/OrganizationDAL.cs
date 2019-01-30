using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using RailExam.Model;
using DSunSoft.Data;

namespace RailExam.DAL
{
	public class OrganizationDAL
	{
		private static Hashtable _ormTable;
		private int _recordCount = 0;
		
		static OrganizationDAL()
		{
			_ormTable = new Hashtable();

			_ormTable.Add("organizationid", "ORG_ID");
			_ormTable.Add("parentid", "PARENT_ID");
			_ormTable.Add("idpath", "ID_PATH");
			_ormTable.Add("levelnum", "LEVEL_NUM");
			_ormTable.Add("orderindex", "ORDER_INDEX");
			_ormTable.Add("shortname", "SHORT_NAME");
			_ormTable.Add("fullname", "FULL_NAME");
			_ormTable.Add("address", "ADDRESS");
			_ormTable.Add("postcode", "POST_CODE");
			_ormTable.Add("contactperson", "CONTACT_PERSON");
			_ormTable.Add("phone", "PHONE");
			_ormTable.Add("website", "WEB_SITE");
			_ormTable.Add("email", "EMAIL");
			_ormTable.Add("description", "DESCRIPTION");
			_ormTable.Add("memo", "MEMO");
			_ormTable.Add("suitrange","SUIT_RANGE");
            _ormTable.Add("railsystemid", "RAIL_SYSTEM_ID");
            _ormTable.Add("railsystemname", "RAIL_SYSTEM_NAME");
            _ormTable.Add("iseffect", "IS_EFFECT");
		}
				
	    /// <summary>
		/// 查询组织机构
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
            IList<Organization> organizations = new List<Organization>();
			
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_ORG_S";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
			db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
			db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
			db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					Organization organization = CreateModelObject(dataReader);

					organizations.Add(organization);
				}
			}

			_recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

			return organizations;
		}

        /// <summary>
        /// 获取所有组织机构
        /// </summary>
        /// <returns></returns>
		public IList<Organization> GetOrganizations()
		{
			IList<Organization> organizations = new List<Organization>();
			Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_ORG_GET_ALL");

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					Organization organization = CreateModelObject(dataReader);
                    organization.RailSystemID = Convert.ToInt32(dataReader[GetMappingFieldName("RailSystemID")].ToString());
                    organization.IsEffect =
                             Convert.ToInt32(dataReader[GetMappingFieldName("IsEffect")].ToString()) == 1 ? true : false;
					organizations.Add(organization);
				}
			}

		    _recordCount = organizations.Count;

			return organizations;
		}

		public IList<Organization> GetOrganizations(string tableName)
		{
			IList<Organization> organizations = new List<Organization>();
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetStoredProcCommand("USP_GET_ALL");

			db.AddInParameter(dbCommand, "p_table_name", DbType.String, tableName);
			db.AddInParameter(dbCommand, "p_order_by", DbType.String, "LEVEL_NUM, ORDER_INDEX ASC");


			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					Organization organization = CreateModelObject(dataReader);
                    organization.IsEffect =
                             Convert.ToInt32(dataReader[GetMappingFieldName("IsEffect")].ToString()) == 1 ? true : false;
					organizations.Add(organization);
				}
			}

			_recordCount = organizations.Count;

			return organizations;
		}

        /// <summary>
        /// 获取所有组织机构
        /// </summary>
        /// <returns></returns>
        public IList<Organization> GetOrganizationsByParentID(int parentID)
        {
            IList<Organization> organizations = new List<Organization>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_ORG_G_Parent");

            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, parentID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Organization organization = CreateModelObject(dataReader);

                    organizations.Add(organization);
                }
            }

            return organizations;
        }

        /// <summary>
        /// 获取所有组织机构
        /// </summary>
        /// <returns></returns>
        public IList<Organization> GetOrganizationsByLevel(int level)
        {
            IList<Organization> organizations = new List<Organization>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_Org_ALL");

            db.AddInParameter(dbCommand, "p_level_num", DbType.Int32,level );

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Organization organization = CreateModelObject(dataReader);
					organization.SuitRange = Convert.ToInt32(dataReader[GetMappingFieldName("SuitRange")].ToString());
                    organization.RailSystemID = dataReader[GetMappingFieldName("RailSystemID")] == DBNull.Value
                                                    ? 0
                                                    : Convert.ToInt32(
                                                        dataReader[GetMappingFieldName("RailSystemID")].ToString());
                    organization.IsEffect =
                             Convert.ToInt32(dataReader[GetMappingFieldName("IsEffect")].ToString()) == 1 ? true : false;
                    organizations.Add(organization);
                }
            }

            _recordCount = organizations.Count;

            return organizations;
        }

		public IList<Organization> GetOrganizationsByWhereClause(Database db, DbTransaction trans, string whereClause)
		{
			IList<Organization> organizations = new List<Organization>();
			DbCommand dbCommand = db.GetStoredProcCommand("USP_Org_WhereClause");

			db.AddInParameter(dbCommand, "p_sql", DbType.String, whereClause);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand,trans))
			{
				while (dataReader.Read())
				{
					Organization organization = CreateModelObject(dataReader);
                    organization.SuitRange = Convert.ToInt32(dataReader[GetMappingFieldName("SuitRange")].ToString());
                    organization.IsEffect =
                            Convert.ToInt32(dataReader[GetMappingFieldName("IsEffect")].ToString()) == 1 ? true : false;
					organizations.Add(organization);
				}
			}

			_recordCount = organizations.Count;

			return organizations;
		}

		public IList<Organization> GetOrganizationsByWhereClause(string whereClause)
		{
			IList<Organization> organizations = new List<Organization>();
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetStoredProcCommand("USP_Org_WhereClause");

			db.AddInParameter(dbCommand, "p_sql", DbType.String, whereClause);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					Organization organization = CreateModelObject(dataReader);
					organization.SuitRange = Convert.ToInt32(dataReader[GetMappingFieldName("SuitRange")].ToString());
					organizations.Add(organization);
				}
			}

			_recordCount = organizations.Count;

			return organizations;
		}


        public IList<Organization> GetOrganizations(int organizationId)
		{
            IList<Organization> organizations = new List<Organization>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_ORG_G_Station";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, organizationId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Organization organization = CreateModelObject(dataReader);
                    organization.IsEffect =
                             Convert.ToInt32(dataReader[GetMappingFieldName("IsEffect")].ToString()) == 1 ? true : false;
                    organizations.Add(organization);
                }
            }

            _recordCount = organizations.Count;

            return organizations;
		}


        /// <summary>
        /// 通过ID获取组织机构
        /// </summary>
        /// <param name="organizationId">组织机构ID</param>
        /// <returns>组织机构</returns>
        public Organization GetOrganization(int organizationId)
        {
            Organization organization;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ORG_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, organizationId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    organization = CreateModelObject(dataReader);
					organization.SuitRange = Convert.ToInt32(dataReader[GetMappingFieldName("SuitRange")].ToString());
                    organization.RailSystemID = Convert.ToInt32(dataReader[GetMappingFieldName("RailSystemID")].ToString());
                    organization.RailSystemName = dataReader[GetMappingFieldName("RailSystemName")].ToString();
                    organization.IsEffect =
                        Convert.ToInt32(dataReader[GetMappingFieldName("IsEffect")].ToString()) == 1 ? true : false;
                }
                else
                {
                    organization = new Organization();
                }
            }

            return organization;
        }

        public int GetStationOrgID(int orgID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ORG_G_Station_org_ID";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
            db.AddOutParameter(dbCommand, "p_station_org_id", DbType.Int32, 4);

            db.ExecuteNonQuery(dbCommand);

            return Convert.ToInt32(db.GetParameterValue(dbCommand, "p_station_org_id"));
        }

        public int GetOrgIDByOrgNamePath(string strNamePath)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ORG_IMPORT";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_org_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_name_path", DbType.String, strNamePath);

            db.ExecuteNonQuery(dbCommand);

            return Convert.ToInt32(db.GetParameterValue(dbCommand, "p_org_id"));
        }

		/// <summary>
		/// 新增组织机构
		/// </summary>
		/// <param name="organization">新增的组织机构信息</param>
		/// <returns></returns>
		public int AddOrganization(Organization organization)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_ORG_I";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddOutParameter(dbCommand, "p_org_id", DbType.Int32, 4);
			db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, organization.ParentId);
			db.AddOutParameter(dbCommand, "p_id_path", DbType.String, 20);
			db.AddOutParameter(dbCommand, "p_level_num", DbType.Int32, 4);
			db.AddOutParameter(dbCommand, "p_order_index", DbType.Int32, 4);
			db.AddInParameter(dbCommand, "p_short_name", DbType.String, organization.ShortName);
			db.AddInParameter(dbCommand, "p_full_name", DbType.String, organization.FullName);
			db.AddInParameter(dbCommand, "p_address", DbType.String, organization.Address);
			db.AddInParameter(dbCommand, "p_post_code", DbType.String, organization.PostCode);
			db.AddInParameter(dbCommand, "p_contact_person", DbType.String, organization.ContactPerson);
			db.AddInParameter(dbCommand, "p_phone", DbType.String, organization.Phone);
			db.AddInParameter(dbCommand, "p_web_site", DbType.String, organization.WebSite);
			db.AddInParameter(dbCommand, "p_email", DbType.String, organization.Email);
			db.AddInParameter(dbCommand, "p_description", DbType.String, organization.Description);
			db.AddInParameter(dbCommand, "p_memo", DbType.String, organization.Memo);
            db.AddInParameter(dbCommand, "p_rail_system_id", DbType.Int32, organization.RailSystemID);
            db.AddInParameter(dbCommand, "p_is_effect", DbType.Int32, organization.IsEffect?1:0);

			db.ExecuteNonQuery(dbCommand);

			return Convert.ToInt32(db.GetParameterValue(dbCommand, "p_org_id"));
		}

		/// <summary>
		/// 新增组织机构
		/// </summary>
		/// <param name="organization">新增的组织机构信息</param>
		/// <returns></returns>
		public int AddOrganization(Database db, DbTransaction trans, Organization organization)
		{
			string sqlCommand = "USP_ORG_I";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddOutParameter(dbCommand, "p_org_id", DbType.Int32, 4);
			db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, organization.ParentId);
			db.AddOutParameter(dbCommand, "p_id_path", DbType.String, 20);
			db.AddOutParameter(dbCommand, "p_level_num", DbType.Int32, 4);
			db.AddOutParameter(dbCommand, "p_order_index", DbType.Int32, 4);
			db.AddInParameter(dbCommand, "p_short_name", DbType.String, organization.ShortName);
			db.AddInParameter(dbCommand, "p_full_name", DbType.String, organization.FullName);
			db.AddInParameter(dbCommand, "p_address", DbType.String, organization.Address);
			db.AddInParameter(dbCommand, "p_post_code", DbType.String, organization.PostCode);
			db.AddInParameter(dbCommand, "p_contact_person", DbType.String, organization.ContactPerson);
			db.AddInParameter(dbCommand, "p_phone", DbType.String, organization.Phone);
			db.AddInParameter(dbCommand, "p_web_site", DbType.String, organization.WebSite);
			db.AddInParameter(dbCommand, "p_email", DbType.String, organization.Email);
			db.AddInParameter(dbCommand, "p_description", DbType.String, organization.Description);
			db.AddInParameter(dbCommand, "p_memo", DbType.String, organization.Memo);
            db.AddInParameter(dbCommand, "p_rail_system_id", DbType.Int32, organization.RailSystemID);
            db.AddInParameter(dbCommand, "p_is_effect", DbType.Int32, organization.IsEffect ? 1 : 0);

			db.ExecuteNonQuery(dbCommand,trans);

			return Convert.ToInt32(db.GetParameterValue(dbCommand, "p_org_id"));
		}

		/// <summary>
		/// 更新组织机构
		/// </summary>
		/// <param name="organization">更新后的组织机构信息</param>
		public void UpdateOrganization(Organization organization)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_ORG_U";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, organization.OrganizationId);
			db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, organization.ParentId);
			db.AddInParameter(dbCommand, "p_id_path", DbType.String, organization.IdPath);
			db.AddInParameter(dbCommand, "p_level_num", DbType.Int32, organization.LevelNum);
			db.AddInParameter(dbCommand, "p_order_index", DbType.Int32, organization.OrderIndex);
			db.AddInParameter(dbCommand, "p_short_name", DbType.String, organization.ShortName);
			db.AddInParameter(dbCommand, "p_full_name", DbType.String, organization.FullName);
			db.AddInParameter(dbCommand, "p_address", DbType.String, organization.Address);
			db.AddInParameter(dbCommand, "p_post_code", DbType.String, organization.PostCode);
			db.AddInParameter(dbCommand, "p_contact_person", DbType.String, organization.ContactPerson);
			db.AddInParameter(dbCommand, "p_phone", DbType.String, organization.Phone);
			db.AddInParameter(dbCommand, "p_web_site", DbType.String, organization.WebSite);
			db.AddInParameter(dbCommand, "p_email", DbType.String, organization.Email);
			db.AddInParameter(dbCommand, "p_description", DbType.String, organization.Description);
			db.AddInParameter(dbCommand, "p_memo", DbType.String, organization.Memo);
            db.AddInParameter(dbCommand, "p_rail_system_id", DbType.Int32, organization.RailSystemID);
            db.AddInParameter(dbCommand, "p_is_effect", DbType.Int32, organization.IsEffect ? 1 : 0);

			db.ExecuteNonQuery(dbCommand);
		}

	    /// <summary>
	    /// 删除组织机构
	    /// </summary>
		/// <param name="organizationId">要删除的组织机构ID</param>
		public void DeleteOrganization(int organizationId, ref int errorCode)
	    {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ORG_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, organizationId);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand,transaction);
    
                transaction.Commit();
                errorCode = 0;
            }
            catch(OracleException ex)
            {
                transaction.Rollback();
                errorCode = ex.Code;
            }
            connection.Close();
        }

        public void UpdateOrgSynchronizeTime(int orgid,string time)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ORG_U_Time";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);
            db.AddInParameter(dbCommand, "p_time", DbType.String, time);
            db.ExecuteNonQuery(dbCommand);
        }

		public void UpdateOrgService(int orgid, string netName,string ipAdress,bool isAutoUpload)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_ORG_U_Net_IP";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);
			db.AddInParameter(dbCommand, "p_net_name", DbType.String, netName);
			db.AddInParameter(dbCommand, "p_ip_address", DbType.String, ipAdress);
			db.AddInParameter(dbCommand, "p_is_auto_upload", DbType.Int32, isAutoUpload ? 1:0);
			db.ExecuteNonQuery(dbCommand);
		}

        public string GetOrgSynchronizeTime(int orgid)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_ORG_G_Time";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);
            db.AddOutParameter(dbCommand, "p_time", DbType.String, 5);
            db.ExecuteNonQuery(dbCommand);

            return db.GetParameterValue(dbCommand, "p_time").ToString();
        }

		public string GetOrgIPAddress(int orgid)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_ORG_G_IP";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);
			db.AddOutParameter(dbCommand, "p_ip_address", DbType.String, 15);
			db.ExecuteNonQuery(dbCommand);

			return db.GetParameterValue(dbCommand, "p_ip_address").ToString();
		}

		public string GetOrgNetName(int orgid)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_ORG_G_Net_Name";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);
			db.AddOutParameter(dbCommand, "p_net_name", DbType.String, 50);
			db.ExecuteNonQuery(dbCommand);

			return db.GetParameterValue(dbCommand, "p_net_name").ToString();
		}


		public bool IsAutoUpload(int orgid)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_ORG_G_Is_Auto";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);
			db.AddOutParameter(dbCommand, "p_is_auto_upload", DbType.String, 50);
			db.ExecuteNonQuery(dbCommand);

			if (db.GetParameterValue(dbCommand, "p_is_auto_upload").ToString() == "0")
			{
				return false; 
			}
			else
			{
				return true;
			}
		}


		public int[]  GetUnitID(int orgid)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_ORG_G_Unit";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);
			db.AddOutParameter(dbCommand, "p_unit_id", DbType.Int32, 4);
			db.AddOutParameter(dbCommand, "p_Is_Train_Station", DbType.Int32, 4);
			db.ExecuteNonQuery(dbCommand);

			int[] obj = new int[2];
			obj[0] = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_unit_id").ToString());
			obj[1] = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_Is_Train_Station").ToString());

			return obj;
		}

		
		/// <summary>
		/// 查询结果记录数
		/// </summary>
		public int RecordCount
		{
			get
			{
				return _recordCount;
			}
		}

		public static string GetMappingFieldName(string propertyName)
		{
			return (string)_ormTable[propertyName.ToLower()];
		}
		
		public static string GetMappingOrderBy(string orderBy)
		{
			orderBy = orderBy.Trim();
			
			if(string.IsNullOrEmpty(orderBy))
			{
				return string.Empty;
			}

			string mappingOrderBy = string.Empty;
			string[] orderByConditions = orderBy.Split(new char[] {','});

			foreach (string s in orderByConditions)
			{
				string orderByCondition = s.Trim();
				
				string[] orderBysOfOneCondition = orderByCondition.Split(new char[] { ' ' });

				if (orderBysOfOneCondition.Length == 0)
				{
					continue;
				}
				else
				{
					if(mappingOrderBy != string.Empty)
					{
						mappingOrderBy += ',';
					}
					
					if (orderBysOfOneCondition.Length == 1)
					{
						mappingOrderBy += GetMappingFieldName(orderBysOfOneCondition[0]);
					}
					else
					{
						mappingOrderBy += GetMappingFieldName(orderBysOfOneCondition[0]) + ' ' + orderBysOfOneCondition[1];
					}
				}
			}

			return mappingOrderBy;
		}

		public static Organization CreateModelObject(IDataReader dataReader)
		{
			return new Organization(
				DataConvert.ToInt(dataReader[GetMappingFieldName("OrganizationId")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("ParentId")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("IdPath")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("LevelNum")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("OrderIndex")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("ShortName")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("FullName")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("Address")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("PostCode")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("ContactPerson")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("Phone")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("WebSite")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("Email")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
		}

        /// <summary>
        /// 是否可以移动节点
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="bUp"></param>
        /// <returns></returns>
        public bool Move(int organizationId, bool bUp)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCmd = db.GetStoredProcCommand("USP_TREE_NODE_M");

            db.AddInParameter(dbCmd, "p_table_name", DbType.String, "ORG");
            db.AddInParameter(dbCmd, "p_id_field_name", DbType.String, "ORG_ID");
            db.AddInParameter(dbCmd, "p_id", DbType.Int32, organizationId);
            db.AddInParameter(dbCmd, "p_direction", DbType.Int32, (bUp ? 1 : 0));
            db.AddOutParameter(dbCmd, "p_result", DbType.Int32, 4);

            db.ExecuteNonQuery(dbCmd);

            return ((int)db.GetParameterValue(dbCmd, "p_result") == 1) ? true : false;
        }
    }
}
