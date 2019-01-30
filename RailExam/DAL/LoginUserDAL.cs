using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using RailExam.Model;
using DSunSoft.Data;

namespace RailExam.DAL
{
    public class LoginUserDAL
    {
        private static Hashtable _ormTable;

        static LoginUserDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("userid", "USER_ID");
            _ormTable.Add("password", "PASSWORD");
            _ormTable.Add("employeeid", "EMPLOYEE_ID");
            _ormTable.Add("employeename", "EMPLOYEE_NAME");
            _ormTable.Add("orgid", "ORG_ID");
            _ormTable.Add("orgname", "ORG_NAME");
            _ormTable.Add("postid", "POST_ID");
            _ormTable.Add("postname", "POST_NAME");
            _ormTable.Add("roleid", "ROLE_ID");
            _ormTable.Add("rolename", "ROLE_NAME");
            _ormTable.Add("isadmin", "IS_ADMIN");
            _ormTable.Add("isgroupleader", "IS_GROUP_LEADER");
            _ormTable.Add("techniciantypeid", "TECHNICIAN_TYPE_ID");
            _ormTable.Add("suitrange","SUIT_RANGE");
            _ormTable.Add("stationorgid","Station_Org_ID");
            _ormTable.Add("usetype","Use_Type");
            _ormTable.Add("railsystemid","RAIL_SYSTEM_ID");
        }

        public LoginUser GetLoginUser(string userID, string password)
        {
            LoginUser loginUser = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_LOGIN_USER_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_user_id", DbType.String, userID);
            db.AddInParameter(dbCommand, "p_password", DbType.String, password);

            using(IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if(dataReader.Read())
                {
                    loginUser = CreateModelObject(dataReader);
                }
            }

            return loginUser;
        }
		public LoginUser GetLoginUserByCardID(string cardID, string password)
		{
			LoginUser loginUser = null;
			Database db = DatabaseFactory.CreateDatabase();
			System.Text.StringBuilder strSql = new StringBuilder();
			strSql.Append("select a.*,b.is_group_leader,b.technician_type_id,c.suit_range, b.employee_name,");
            strSql.Append(" b.org_id,getorgname(b.org_id) as org_name, b.post_id, d.post_name, a.role_id,c.rail_system_id,");
			strSql.Append(" nvl(e.role_name,'нч╫ги╚') as Role_Name, nvl(e.is_admin,0) as Is_Admin,getstationorgid(b.org_id) as Station_Org_ID,e.use_type ");
			strSql.Append(" from system_user a  left join employee b on a.employee_id = b.employee_id ");
			strSql.Append("  left join org c on b.org_id = c.org_id left join post d on b.post_id = d.post_id ");
			strSql.Append(" left join system_role e on a.role_id = e.role_id ");
            strSql.AppendFormat(" where  upper(b.identity_cardno) =  upper('{0}') and a.password = '{1}'", cardID, password);
			using (IDataReader dataReader = db.ExecuteReader(CommandType.Text,strSql.ToString()))
			{
				if (dataReader.Read())
					loginUser = CreateModelObject(dataReader);
			}
			return loginUser;
		}

		public LoginUser GetLoginUserByOrgID(int orgID,string userID, string password)
		{
			LoginUser loginUser = null;

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_LOGIN_USER_G_ORG";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_user_id", DbType.String, userID);
			db.AddInParameter(dbCommand, "p_password", DbType.String, password);
			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				if (dataReader.Read())
				{
					loginUser = CreateModelObject(dataReader);
				}
			}

			return loginUser;
		}

        public static string GetMappingFieldName(string propertyName)
        {
            return (string)_ormTable[propertyName.ToLower()];
        }

        public static LoginUser CreateModelObject(IDataReader dataReader)
        {
            return new LoginUser(
                Convert.ToString(dataReader[GetMappingFieldName("UserID")]),
                Convert.ToString(dataReader[GetMappingFieldName("Password")]),
                Convert.ToInt32(dataReader[GetMappingFieldName("EmployeeID")]),
                Convert.ToString(dataReader[GetMappingFieldName("EmployeeName")]),
                Convert.ToInt32(dataReader[GetMappingFieldName("OrgID")]),
                Convert.ToString(dataReader[GetMappingFieldName("OrgName")]),
                Convert.ToInt32(dataReader[GetMappingFieldName("PostID")]),
                Convert.ToString(dataReader[GetMappingFieldName("PostName")]),
                Convert.ToInt32(dataReader[GetMappingFieldName("RoleID")]),
                Convert.ToString(dataReader[GetMappingFieldName("RoleName")]),
                Convert.ToBoolean(dataReader[GetMappingFieldName("IsAdmin")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("IsGroupLeader")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("TechnicianTypeID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("SuitRange")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("StationOrgID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("UseType")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("RailSystemID")]),
                null);
        }
    }
}
 