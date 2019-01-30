using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.IO;
using System.Web;
using System.Xml;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
    public class EmployeeDAL
    {
        private static Hashtable _ormTable;
		private int _recordCount = 0;

        static EmployeeDAL()
        {
            _ormTable = new Hashtable();

            _ormTable.Add("employeeid", "EMPLOYEE_ID");
            _ormTable.Add("orgid", "ORG_ID");
            _ormTable.Add("orgname", "ORG_NAME");
            _ormTable.Add("workno", "WORK_NO");
            _ormTable.Add("employeename", "EMPLOYEE_NAME");
            _ormTable.Add("postid", "POST_ID");
            _ormTable.Add("postname", "POST_NAME");
            _ormTable.Add("sex", "SEX");
            _ormTable.Add("birthday", "BIRTHDAY");
            _ormTable.Add("nativeplace", "NATIVE_PLACE");
            _ormTable.Add("folk", "FOLK");
            _ormTable.Add("wedding", "WEDDING");
            _ormTable.Add("begindate", "BEGIN_DATE");
            _ormTable.Add("workphone", "WORK_PHONE");
            _ormTable.Add("homephone", "HOME_PHONE");
            _ormTable.Add("mobilephone", "MOBILE_PHONE");
            _ormTable.Add("email", "EMAIL");
            _ormTable.Add("address", "ADDRESS");
            _ormTable.Add("postcode", "POST_CODE");
            _ormTable.Add("isonpost", "ISONPOST");
            _ormTable.Add("memo", "MEMO");
            _ormTable.Add("isgroupleader","IS_GROUP_LEADER");
            _ormTable.Add("techniciantypeid","TECHNICIAN_TYPE_ID");
            _ormTable.Add("postno","POST_NO");
            _ormTable.Add("pinyincode","PINYIN_CODE");
			_ormTable.Add("logincount", "LOGIN_COUNT");
			_ormTable.Add("logintime", "LOGIN_TIME");
            _ormTable.Add("strworkno","STR_WORK_NO");
        }

        /// <summary>
        /// 查询员工
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="orgID"></param>
        /// <param name="orgName"></param>
        /// <param name="workNo"></param>
        /// <param name="employeeName"></param>
        /// <param name="postID"></param>
        /// <param name="postName"></param>
        /// <param name="sex"></param>
        /// <param name="birthday"></param>
        /// <param name="nativePlace"></param>
        /// <param name="folk"></param>
        /// <param name="wedding"></param>
        /// <param name="beginDate"></param>
        /// <param name="workPhone"></param>
        /// <param name="homePhone"></param>
        /// <param name="mobilePhone"></param>
        /// <param name="email"></param>
        /// <param name="address"></param>
        /// <param name="postCode"></param>
        /// <param name="dimission"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex">起始记录行</param>
        /// <param name="maximumRows">每页记录条数</param>
        /// <param name="orderBy">排序字符串，如"FieldName ASC"</param>
        /// <returns></returns>
        public IList<Employee> GetEmployees(int employeeID, int orgID, string orgName, string workNo, string employeeName, int postID,
            string postName, string sex, DateTime birthday, string nativePlace, string folk, int wedding, DateTime beginDate, 
            string workPhone, string homePhone, string mobilePhone, string email, string address, string postCode,
            bool dimission, string memo, int startRowIndex, int maximumRows, string orderBy)
        {
            IList<Employee> employees = new List<Employee>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EMPLOYEE_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Employee employee = CreateModelObject(dataReader);

                    employees.Add(employee);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return employees;
        }

        /// <summary>
        /// 查询员工
        /// </summary>
        /// <returns></returns>
        public IList<Employee> GetEmployees()
        {
            IList<Employee> employees = new List<Employee>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EMPLOYEE_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy("employeeid"));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Employee employee = CreateModelObject(dataReader);

                    employees.Add(employee);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return employees;
        }

        public IList<Employee> GetAllEmployees()
        {
            IList<Employee> employees = new List<Employee>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EMPLOYEE_S_All";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Employee employee = CreateModelObject(dataReader);

                    employees.Add(employee);
                }
            }

            return employees;
        }

        public IList<Employee> GetEmployees(int orgid,string postIDPath)
        {
            IList<Employee> employees = new List<Employee>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EMPLOYEE_Q";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);
            db.AddInParameter(dbCommand, "p_id_path", DbType.String, postIDPath);

            using(IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while(dataReader.Read())
                {
                    Employee employee = CreateModelObject(dataReader);

                    employees.Add(employee);
                }
            }

            return employees;
        }

		public IList<Employee> GetRandomExamNoResultEmployee( string strEmployeeID)
		{
			IList<Employee> employees = new List<Employee>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "usp_Random_Exam_noresult";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_employee_id", DbType.String, strEmployeeID);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					Employee employee = CreateModelObject(dataReader);
                    employee.StrWorkNo = dataReader[GetMappingFieldName("StrWorkNo")].ToString();
					employees.Add(employee);
				}
			}

			return employees;
		}

		public IList<Employee> GetEmployeeByWhereClause(string whereClause)
		{
			IList<Employee> employees = new List<Employee>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "usp_Employee_WhereClause";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_sql", DbType.String, whereClause);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					Employee employee = CreateModelObject(dataReader);

					employees.Add(employee);
				}
			}

			return employees;
		}

        /// <summary>
        /// 执行带有clob,blob,nclob大对象参数类型的存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名称</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tempbuff"></param>
        private  DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, byte[] tempbuff)
        {
            string constring = ConfigurationManager.ConnectionStrings["Oracle"].ConnectionString;
            OracleConnection Connection = new OracleConnection(constring);

            Connection.Open();
            OracleTransaction tx = Connection.BeginTransaction();
            OracleCommand cmd = Connection.CreateCommand();
            cmd.Transaction = tx;
            string type = " declare ";
            type = type + " xx  Clob;";
            string createtemp = type + " begin ";
            createtemp = createtemp + " dbms_lob.createtemporary(xx, false, 0); ";
            string setvalue = "";
            setvalue = setvalue + ":templob := xx;";
            cmd.CommandText = createtemp + setvalue + " end;";
            cmd.Parameters.Add(new OracleParameter("templob", OracleType.Clob)).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            OracleLob tempLob = (OracleLob)cmd.Parameters["templob"].Value;
            tempLob.BeginBatch(OracleLobOpenMode.ReadWrite);
            int abc = tempbuff.Length;

            double b = abc / 2;
            double a = Math.Ceiling(b);
            abc = (int)(a * 2);
            tempLob.Write(tempbuff, 0, abc);
            tempLob.EndBatch();
            parameters[0].Value = tempLob;

            cmd.Parameters.Clear();
            cmd.CommandText = storedProcName;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (OracleParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            cmd.ExecuteNonQuery();
           DataSet ds = new DataSet();
            da.Fill(ds);
            //OracleDataReader dataReader = (OracleDataReader)cmd.Parameters[1].Value;
            //IList<Employee> employees = new List<Employee>();
            //while (dataReader.Read())
            //{
            //    Employee employee = CreateModelObject(dataReader);

            //    employees.Add(employee);
            //}
            tx.Commit();
            Connection.Close();
            return ds;
        }


        public DataSet GetEmployeesByEmployeeIdS(string employeeIDs)
        {
            //IList<Employee> employees = new List<Employee>();
            DataSet ds = new DataSet();

            XmlDocument doc = new XmlDocument();
            //Request.PhysicalApplicationPath取得config文件路径
            doc.Load(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config"));
            XmlNode node = doc.SelectSingleNode("configuration/dataConfiguration/@defaultDatabase");
            string  value = node.Value; 

            if(value =="Oracle" )
            {
                OracleParameter para1 = new OracleParameter("p_employeeIDs", OracleType.Clob);
                OracleParameter para2 = new OracleParameter("cur_out", OracleType.Cursor);
                para2.Direction = ParameterDirection.Output;
                IDataParameter[] paras = new IDataParameter[] { para1, para2 };
                ds= RunProcedure("usp_employee_by_employeeIDs", paras, System.Text.Encoding.Unicode.GetBytes(employeeIDs));
            }
 
            return ds;
        }


        public IList<Employee> GetEmployeesByEmployeeId(string employeeIDs)
        {
            IList<Employee> employees = new List<Employee>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "usp_employee_by_employeeID";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_employeeIDs", DbType.String, employeeIDs);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Employee employee = CreateModelObject(dataReader);

                    employees.Add(employee);
                }
            }

            return employees;
        }


        public Employee GetChooseEmployeeInfo(string employeeID)
        {
            Employee employee = new Employee();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "usp_employee_byemployeeID";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_employeeIDs", DbType.String, employeeID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    employee = CreateModelObject(dataReader);
                    employee.StrWorkNo = dataReader[GetMappingFieldName("StrWorkNo")].ToString();
                }
            }

            return employee;
        }




        public IList<Employee> GetEmployeesByOrgIDPath(string orgIDPath)
        {
            IList<Employee> employees = new List<Employee>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EMPLOYEE_Q_Org";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_org_id_path", DbType.String, orgIDPath);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Employee employee = CreateModelObject(dataReader);

                    employees.Add(employee);
                }
            }

            return employees;
        }

        public IList<Employee> GetEmployees(int orgID, string postIDPath, string workNo, string employeeName, string sex, string postName, string strOrderBy)
        {
            IList<Employee> employees = new List<Employee>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "usp_employee_f_Org";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
            db.AddInParameter(dbCommand, "p_id_path", DbType.String, postIDPath);
            db.AddInParameter(dbCommand, "p_work_no", DbType.String, workNo);
            db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employeeName);
            db.AddInParameter(dbCommand, "p_sex", DbType.String, sex);
            db.AddInParameter(dbCommand, "p_post_name", DbType.String, postName);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, strOrderBy);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Employee employee = CreateModelObject(dataReader);

                    employees.Add(employee);
                }
            }

            return employees;
        }

        public IList<Employee> GetEmployees(int orgID, string postIDPath, string workNo, string employeeName, string sex, string postName, string strOrderBy, int startRow, int endRow,ref int nCount)
        {
            IList<Employee> employees = new List<Employee>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "usp_employee_f_Org1";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
            db.AddInParameter(dbCommand, "p_id_path", DbType.String, postIDPath);
            db.AddInParameter(dbCommand, "p_work_no", DbType.String, workNo);
            db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employeeName);
            db.AddInParameter(dbCommand, "p_sex", DbType.String, sex);
            db.AddInParameter(dbCommand, "p_post_name", DbType.String, postName);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, strOrderBy);
            db.AddInParameter(dbCommand, "p_start_row", DbType.Int32, startRow);
            db.AddInParameter(dbCommand, "p_end_row", DbType.Int32, endRow);       
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Employee employee = CreateModelObject(dataReader);

                    employees.Add(employee);
                }
            }

            nCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));
            
            return employees;
        }

        public IList<Employee> GetEmployeesByPost(int PostID, string postIDPath, string workNo, string employeeName, string sex, string postName, string strOrderBy, int startRow, int endRow, ref int nCount)
        {
            IList<Employee> employees = new List<Employee>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "usp_employee_f_Post1";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, PostID);
            db.AddInParameter(dbCommand, "p_id_path", DbType.String, postIDPath);
            db.AddInParameter(dbCommand, "p_work_no", DbType.String, workNo);
            db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employeeName);
            db.AddInParameter(dbCommand, "p_sex", DbType.String, sex);
            db.AddInParameter(dbCommand, "p_post_name", DbType.String, postName);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, strOrderBy);
            db.AddInParameter(dbCommand, "p_start_row", DbType.Int32, startRow);
            db.AddInParameter(dbCommand, "p_end_row", DbType.Int32, endRow);
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Employee employee = CreateModelObject(dataReader);

                    employees.Add(employee);
                }
            }
            nCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));
            return employees;
        }


        public IList<Employee> GetEmployeesSelect(int orgid, int postid, string workNo, string employeeName, string pinyincode, string strOrderBy, int groupLeader, int safeLevelID, int startRow, int endRow, ref int nCount)
        {
            IList<Employee> employees = new List<Employee>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "usp_employee_f_Select";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);
            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, postid);
            db.AddInParameter(dbCommand, "p_work_no", DbType.String, workNo);
            db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employeeName);
			db.AddInParameter(dbCommand, "p_pinyincode", DbType.String, pinyincode.ToUpper());
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, strOrderBy);
			db.AddInParameter(dbCommand,"p_is_group_leader",DbType.Int32,groupLeader);
            db.AddInParameter(dbCommand, "p_safe_level_id", DbType.Int32, safeLevelID);
            db.AddInParameter(dbCommand, "p_start_row", DbType.Int32, startRow);
            db.AddInParameter(dbCommand, "p_end_row", DbType.Int32, endRow);
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Employee employee = CreateModelObject(dataReader);
                    employee.StrWorkNo = dataReader[GetMappingFieldName("StrWorkNo")].ToString();
                    employees.Add(employee);
                }
            }

            nCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return employees;
        }

		public IList<Employee> GetEmployeesSelectByTransfer(int orgid, int postid, string workNo, string employeeName, string pinyincode, string strOrderBy, int groupLeader, int startRow, int endRow, ref int nCount)
		{
			IList<Employee> employees = new List<Employee>();

			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "usp_employee_f_Select_Transfer";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);
			db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, postid);
			db.AddInParameter(dbCommand, "p_work_no", DbType.String, workNo);
			db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employeeName);
			db.AddInParameter(dbCommand, "p_pinyincode", DbType.String, pinyincode.ToUpper());
			db.AddInParameter(dbCommand, "p_order_by", DbType.String, strOrderBy);
			db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, groupLeader);
			db.AddInParameter(dbCommand, "p_start_row", DbType.Int32, startRow);
			db.AddInParameter(dbCommand, "p_end_row", DbType.Int32, endRow);
			db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				while (dataReader.Read())
				{
					Employee employee = CreateModelObject(dataReader);

					employees.Add(employee);
				}
			}

			nCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

			return employees;
		}

		public IList<Employee> GetEmployeesSelect(int orgid, int postid, string workNo, string employeeName, string pinyincode, string strOrderBy, string groupLeader)
        {
            IList<Employee> employees = new List<Employee>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "usp_employee_f_Select1";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgid);
            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, postid);
            db.AddInParameter(dbCommand, "p_work_no", DbType.String, workNo);
            db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employeeName);
			db.AddInParameter(dbCommand, "p_pinyincode", DbType.String, pinyincode.ToUpper());
			db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, groupLeader);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, strOrderBy);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Employee employee = CreateModelObject(dataReader);
                    employee.StrWorkNo = dataReader[GetMappingFieldName("StrWorkNo")].ToString();
                    employees.Add(employee);
                }
            }
            return employees;
        }

		public IList<Employee> GetEmployeesSelect(int orgID, string pinyincode, string workNo, string employeeName, string strOrderBy, int startRow, int endRow, ref int nItemCount)
        {
            IList<Employee> employees = new List<Employee>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "usp_employee_f_Select2";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
			db.AddInParameter(dbCommand, "p_pinyin_code", DbType.String, pinyincode.ToUpper());
            db.AddInParameter(dbCommand, "p_work_no", DbType.String, workNo);
            db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employeeName);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, strOrderBy);
            db.AddInParameter(dbCommand, "p_start_row", DbType.Int32, startRow);
            db.AddInParameter(dbCommand, "p_end_row", DbType.Int32, endRow);
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Employee employee = CreateModelObject(dataReader);
                    employee.StrWorkNo = dataReader[GetMappingFieldName("StrWorkNo")].ToString();
                    employees.Add(employee);
                }
            }
            nItemCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));
            return employees;
        }

        public IList<Employee> GetEmployeesSelect(int orgID,string pinyincode,string workNo, string employeeName, string strOrderBy)
        {
            IList<Employee> employees = new List<Employee>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "usp_employee_f_Select3";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
			db.AddInParameter(dbCommand, "p_pinyin_code", DbType.String, pinyincode.ToUpper());
            db.AddInParameter(dbCommand, "p_work_no", DbType.String, workNo);
            db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employeeName);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, strOrderBy);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Employee employee = CreateModelObject(dataReader);

                    employees.Add(employee);
                }
            }
            return employees;
        }


        public IList<Employee> GetEmployees(int orgID, string postIDPath, string workNo, string employeeName, string sex, string postName, int status)
        {
            IList<Employee> employees = new List<Employee>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "usp_employee_f";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
            db.AddInParameter(dbCommand, "p_id_path", DbType.String, postIDPath);
            db.AddInParameter(dbCommand, "p_work_no", DbType.String, workNo);
            db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employeeName);
            db.AddInParameter(dbCommand, "p_sex", DbType.String, sex);
            db.AddInParameter(dbCommand, "p_post_name", DbType.String, postName);
			db.AddInParameter(dbCommand, "p_status", DbType.String, status);     

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Employee employee = CreateModelObject(dataReader);

                    employees.Add(employee);
                }
            }

            return employees;
        }


        public IList<Employee> GetEmployeesByPost(int PostID, string postIDPath, string workNo, string employeeName, string sex,string postName)
        {
            IList<Employee> employees = new List<Employee>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "usp_employee_ByPost";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, PostID);
            db.AddInParameter(dbCommand, "p_id_path", DbType.String, postIDPath);
            db.AddInParameter(dbCommand, "p_work_no", DbType.String, workNo);
            db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employeeName);
            db.AddInParameter(dbCommand, "p_sex", DbType.String, sex);
            db.AddInParameter(dbCommand, "p_post_name", DbType.String, postName);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Employee employee = CreateModelObject(dataReader);

                    employees.Add(employee);
                }
            }

            return employees;
        }

        public IList<Employee> GetEmployeesByPost(int PostID, string postIDPath, string workNo, string employeeName, string sex, string postName, string strOrderBy)
        {
            IList<Employee> employees = new List<Employee>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "usp_employee_f_Post";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, PostID);
            db.AddInParameter(dbCommand, "p_id_path", DbType.String, postIDPath);
            db.AddInParameter(dbCommand, "p_work_no", DbType.String, workNo);
            db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employeeName);
            db.AddInParameter(dbCommand, "p_sex", DbType.String, sex);
            db.AddInParameter(dbCommand, "p_post_name", DbType.String, postName);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, strOrderBy);
           

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Employee employee = CreateModelObject(dataReader);

                    employees.Add(employee);
                }
            }

            return employees;
        }      


        /// <summary>
        /// 通过ID获取员工
        /// </summary>
        /// <param name="employeeID">员工ID</param>
        /// <returns>员工</returns>
        public Employee GetEmployee(int employeeID)
        {
            Employee employee = null;

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EMPLOYEE_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employeeID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    employee = CreateModelObject(dataReader);
                }
            }

            return employee;
        }

		public int GetEmployeeByWorkNo(string workNo)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_EMPLOYEE_Q_WorkNO";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);
			db.AddInParameter(dbCommand, "p_work_no", DbType.String,workNo);
			db.ExecuteNonQuery(dbCommand);

			int id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));
			return id;
		}

        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="employee">新增的员工信息</param>
        /// <returns></returns>
        public int AddEmployee(Employee employee)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EMPLOYEE_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_employee_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, employee.OrgID);
            db.AddInParameter(dbCommand, "p_work_no", DbType.String, employee.WorkNo);
            db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employee.EmployeeName);
            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, employee.PostID);
            db.AddInParameter(dbCommand, "p_sex", DbType.String, employee.Sex);
            db.AddInParameter(dbCommand, "p_birthday", DbType.Date, employee.Birthday);
            db.AddInParameter(dbCommand, "p_native_place", DbType.String, employee.NativePlace);
            db.AddInParameter(dbCommand, "p_folk", DbType.String, employee.Folk);
            db.AddInParameter(dbCommand, "p_wedding", DbType.Int32, employee.Wedding);
            db.AddInParameter(dbCommand, "p_begin_date", DbType.Date, employee.BeginDate);
            db.AddInParameter(dbCommand, "p_work_phone", DbType.String, employee.WorkPhone);
            db.AddInParameter(dbCommand, "p_home_phone", DbType.String, employee.HomePhone);
            db.AddInParameter(dbCommand, "p_mobile_phone", DbType.String, employee.MobilePhone);
            db.AddInParameter(dbCommand, "p_email", DbType.String, employee.Email);
            db.AddInParameter(dbCommand, "p_address", DbType.String, employee.Address);
            db.AddInParameter(dbCommand, "p_post_code", DbType.String, employee.PostCode);
            db.AddInParameter(dbCommand, "p_dimission", DbType.Int32, employee.IsOnPost ? 1 : 0);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, employee.Memo);
            db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, employee.IsGroupLeader);
            db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, employee.TechnicianTypeID);
            db.AddInParameter(dbCommand, "p_post_no", DbType.String, employee.PostNo);
            db.AddInParameter(dbCommand, "p_pinyin_code",DbType.String,employee.PinYinCode);
			db.AddInParameter(dbCommand, "p_login_count", DbType.Int32, employee.LoginCount);
			db.AddInParameter(dbCommand, "p_login_time", DbType.Int32, employee.LoginTime);

            db.ExecuteNonQuery(dbCommand);
            int id= Convert.ToInt32(db.GetParameterValue(dbCommand, "p_employee_id"));

            return id;
        }

		/// <summary>
		/// 新增员工
		/// </summary>
		/// <param name="employee">新增的员工信息</param>
		/// <returns></returns>
		public int AddEmployee(Database db,DbTransaction trans,Employee employee)
		{
			string sqlCommand = "USP_EMPLOYEE_I";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddOutParameter(dbCommand, "p_employee_id", DbType.Int32, 4);
			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, employee.OrgID);
			db.AddInParameter(dbCommand, "p_work_no", DbType.String, employee.WorkNo);
			db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employee.EmployeeName);
			db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, employee.PostID);
			db.AddInParameter(dbCommand, "p_sex", DbType.String, employee.Sex);
			db.AddInParameter(dbCommand, "p_birthday", DbType.Date, employee.Birthday);
			db.AddInParameter(dbCommand, "p_native_place", DbType.String, employee.NativePlace);
			db.AddInParameter(dbCommand, "p_folk", DbType.String, employee.Folk);
			db.AddInParameter(dbCommand, "p_wedding", DbType.Int32, employee.Wedding);
			db.AddInParameter(dbCommand, "p_begin_date", DbType.Date, employee.BeginDate);
			db.AddInParameter(dbCommand, "p_work_phone", DbType.String, employee.WorkPhone);
			db.AddInParameter(dbCommand, "p_home_phone", DbType.String, employee.HomePhone);
			db.AddInParameter(dbCommand, "p_mobile_phone", DbType.String, employee.MobilePhone);
			db.AddInParameter(dbCommand, "p_email", DbType.String, employee.Email);
			db.AddInParameter(dbCommand, "p_address", DbType.String, employee.Address);
			db.AddInParameter(dbCommand, "p_post_code", DbType.String, employee.PostCode);
            db.AddInParameter(dbCommand, "p_dimission", DbType.Int32, employee.IsOnPost ? 1 : 0);
			db.AddInParameter(dbCommand, "p_memo", DbType.String, employee.Memo);
			db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, employee.IsGroupLeader);
			db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, employee.TechnicianTypeID);
			db.AddInParameter(dbCommand, "p_post_no", DbType.String, employee.PostNo);
			db.AddInParameter(dbCommand, "p_pinyin_code", DbType.String, employee.PinYinCode);
			db.AddInParameter(dbCommand, "p_login_count", DbType.Int32, employee.LoginCount);
			db.AddInParameter(dbCommand, "p_login_time", DbType.Int32, employee.LoginTime);

			db.ExecuteNonQuery(dbCommand,trans);
			int id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_employee_id"));

			string sqlCommand1 = "USP_SYSTEM_USER_I";
			DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand1);

			db.AddInParameter(dbCommand1, "p_user_id", DbType.String, employee.WorkNo);
			db.AddInParameter(dbCommand1, "p_password", DbType.String, "111111");
			db.AddInParameter(dbCommand1, "p_employee_id", DbType.Int32, id);
			db.AddInParameter(dbCommand1, "p_role_id", DbType.Int32, 0);
			db.AddInParameter(dbCommand1, "p_memo", DbType.String, "");
			db.ExecuteNonQuery(dbCommand1, trans);

			return id;
		}

        public void AddEmployeeImport(IList<Employee> objList)
        {
            Database db = DatabaseFactory.CreateDatabase();

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                foreach (Employee employee in objList)
                {
                    string sqlCommand = "USP_EMPLOYEE_I";
                    DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

                    db.AddOutParameter(dbCommand, "p_employee_id", DbType.Int32, 4);
                    db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, employee.OrgID);
                    db.AddInParameter(dbCommand, "p_work_no", DbType.String, employee.WorkNo);
                    db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employee.EmployeeName);
                    db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, employee.PostID);
                    db.AddInParameter(dbCommand, "p_sex", DbType.String, employee.Sex);
                    db.AddInParameter(dbCommand, "p_birthday", DbType.Date, employee.Birthday);
                    db.AddInParameter(dbCommand, "p_native_place", DbType.String, employee.NativePlace);
                    db.AddInParameter(dbCommand, "p_folk", DbType.String, employee.Folk);
                    db.AddInParameter(dbCommand, "p_wedding", DbType.Int32, employee.Wedding);
                    db.AddInParameter(dbCommand, "p_begin_date", DbType.Date, employee.BeginDate);
                    db.AddInParameter(dbCommand, "p_work_phone", DbType.String, employee.WorkPhone);
                    db.AddInParameter(dbCommand, "p_home_phone", DbType.String, employee.HomePhone);
                    db.AddInParameter(dbCommand, "p_mobile_phone", DbType.String, employee.MobilePhone);
                    db.AddInParameter(dbCommand, "p_email", DbType.String, employee.Email);
                    db.AddInParameter(dbCommand, "p_address", DbType.String, employee.Address);
                    db.AddInParameter(dbCommand, "p_post_code", DbType.String, employee.PostCode);
                    db.AddInParameter(dbCommand, "p_dimission", DbType.Int32, employee.IsOnPost ? 1 : 0);
                    db.AddInParameter(dbCommand, "p_memo", DbType.String, employee.Memo);
                    db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, employee.IsGroupLeader);
                    db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, employee.TechnicianTypeID);
                    db.AddInParameter(dbCommand, "p_post_no", DbType.String, employee.PostNo);
                    db.AddInParameter(dbCommand, "p_pinyin_code", DbType.String, employee.PinYinCode);
					db.AddInParameter(dbCommand, "p_login_count", DbType.Int32, employee.LoginCount);
					db.AddInParameter(dbCommand, "p_login_time", DbType.Int32, employee.LoginTime);
                    db.ExecuteNonQuery(dbCommand, transaction);

                    int id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_employee_id"));

                    sqlCommand = "USP_SYSTEM_USER_I";
                    DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand);

                    db.AddInParameter(dbCommand1, "p_user_id", DbType.String, employee.WorkNo);
                    db.AddInParameter(dbCommand1, "p_password", DbType.String, "111111");
                    db.AddInParameter(dbCommand1, "p_employee_id", DbType.Int32, id);
                    db.AddInParameter(dbCommand1, "p_role_id", DbType.Int32, 0);
                    db.AddInParameter(dbCommand1, "p_memo", DbType.String,"");
                    db.ExecuteNonQuery(dbCommand1, transaction);
                }

                transaction.Commit();
            }
            catch (SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            connection.Close();
        }

		public void UpdateEmployeeImport(Hashtable htOld,Hashtable htNew,int orgID)
		{
			Database db = DatabaseFactory.CreateDatabase();

			DbConnection connection = db.CreateConnection();
			connection.Open();
			DbTransaction transaction = connection.BeginTransaction();

			try
			{
				for (int i = 0; i < htOld.Count; i++)
				{
					string  sqlCommand = "USP_EMPLOYEE_U_Import";
					DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
					db.AddInParameter(dbCommand, "p_old_work_no", DbType.String, htOld[i].ToString());
					db.AddInParameter(dbCommand, "p_new_work_no", DbType.String, htNew[i].ToString());
					db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);
					db.ExecuteNonQuery(dbCommand, transaction);
				}
				transaction.Commit();
			}            
            catch (SystemException ex)
            {
                transaction.Rollback();
                throw ex;
            }
            connection.Close();
		}


		/// <summary>
		/// 更新员工
		/// </summary>
		/// <param name="employee">更新后的员工信息</param>
		public bool UpdateEmployee(Employee employee)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EMPLOYEE_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employee.EmployeeID);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, employee.OrgID);
            db.AddInParameter(dbCommand, "p_work_no", DbType.String, employee.WorkNo);
            db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employee.EmployeeName);
            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, employee.PostID);
            db.AddInParameter(dbCommand, "p_sex", DbType.String, employee.Sex);
            db.AddInParameter(dbCommand, "p_birthday", DbType.Date, employee.Birthday);
            db.AddInParameter(dbCommand, "p_native_place", DbType.String, employee.NativePlace);
            db.AddInParameter(dbCommand, "p_folk", DbType.String, employee.Folk);
            db.AddInParameter(dbCommand, "p_wedding", DbType.Int32, employee.Wedding);
            db.AddInParameter(dbCommand, "p_begin_date", DbType.Date, employee.BeginDate);
            db.AddInParameter(dbCommand, "p_work_phone", DbType.String, employee.WorkPhone);
            db.AddInParameter(dbCommand, "p_home_phone", DbType.String, employee.HomePhone);
            db.AddInParameter(dbCommand, "p_mobile_phone", DbType.String, employee.MobilePhone);
            db.AddInParameter(dbCommand, "p_email", DbType.String, employee.Email);
            db.AddInParameter(dbCommand, "p_address", DbType.String, employee.Address);
            db.AddInParameter(dbCommand, "p_post_code", DbType.String, employee.PostCode);
            db.AddInParameter(dbCommand, "p_dimission", DbType.Int32, employee.IsOnPost ? 1 : 0);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, employee.Memo);
            db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, employee.IsGroupLeader);
            db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, employee.TechnicianTypeID );
            db.AddInParameter(dbCommand, "p_post_no", DbType.String, employee.PostNo);
            db.AddInParameter(dbCommand, "p_pinyin_code", DbType.String, employee.PinYinCode);
			db.AddInParameter(dbCommand, "p_login_count", DbType.Int32, employee.LoginCount);
			db.AddInParameter(dbCommand, "p_login_time", DbType.Int32, employee.LoginTime);
       
            if (db.ExecuteNonQuery(dbCommand) > 0)
                return true;
            else
                return false;
        }


        public void UpdateEmployee(Database db, DbTransaction trans, Employee employee)
        {
            string sqlCommand = "USP_EMPLOYEE_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employee.EmployeeID);
            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, employee.OrgID);
            db.AddInParameter(dbCommand, "p_work_no", DbType.String, employee.WorkNo);
            db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employee.EmployeeName);
            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, employee.PostID);
            db.AddInParameter(dbCommand, "p_sex", DbType.String, employee.Sex);
            db.AddInParameter(dbCommand, "p_birthday", DbType.Date, employee.Birthday);
            db.AddInParameter(dbCommand, "p_native_place", DbType.String, employee.NativePlace);
            db.AddInParameter(dbCommand, "p_folk", DbType.String, employee.Folk);
            db.AddInParameter(dbCommand, "p_wedding", DbType.Int32, employee.Wedding);
            db.AddInParameter(dbCommand, "p_begin_date", DbType.Date, employee.BeginDate);
            db.AddInParameter(dbCommand, "p_work_phone", DbType.String, employee.WorkPhone);
            db.AddInParameter(dbCommand, "p_home_phone", DbType.String, employee.HomePhone);
            db.AddInParameter(dbCommand, "p_mobile_phone", DbType.String, employee.MobilePhone);
            db.AddInParameter(dbCommand, "p_email", DbType.String, employee.Email);
            db.AddInParameter(dbCommand, "p_address", DbType.String, employee.Address);
            db.AddInParameter(dbCommand, "p_post_code", DbType.String, employee.PostCode);
            db.AddInParameter(dbCommand, "p_dimission", DbType.Int32, employee.IsOnPost ? 1 : 0);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, employee.Memo);
            db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, employee.IsGroupLeader);
            db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, employee.TechnicianTypeID);
            db.AddInParameter(dbCommand, "p_post_no", DbType.String, employee.PostNo);
            db.AddInParameter(dbCommand, "p_pinyin_code", DbType.String, employee.PinYinCode);
            db.AddInParameter(dbCommand, "p_login_count", DbType.Int32, employee.LoginCount);
            db.AddInParameter(dbCommand, "p_login_time", DbType.Int32, employee.LoginTime);

           db.ExecuteNonQuery(dbCommand,trans);
        }

        /// <summary>
		/// 更新员工
		/// </summary>
		/// <param name="employee">更新后的员工信息</param>
		public bool UpdateEmployeeInStation(Employee employee)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_EMPLOYEE_U_Station";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employee.EmployeeID);
			db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, employee.OrgID);
			db.AddInParameter(dbCommand, "p_work_no", DbType.String, employee.WorkNo);
			db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employee.EmployeeName);
			db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, employee.PostID);
			db.AddInParameter(dbCommand, "p_sex", DbType.String, employee.Sex);
			db.AddInParameter(dbCommand, "p_birthday", DbType.Date, employee.Birthday);
			db.AddInParameter(dbCommand, "p_native_place", DbType.String, employee.NativePlace);
			db.AddInParameter(dbCommand, "p_folk", DbType.String, employee.Folk);
			db.AddInParameter(dbCommand, "p_wedding", DbType.Int32, employee.Wedding);
			db.AddInParameter(dbCommand, "p_begin_date", DbType.Date, employee.BeginDate);
			db.AddInParameter(dbCommand, "p_work_phone", DbType.String, employee.WorkPhone);
			db.AddInParameter(dbCommand, "p_home_phone", DbType.String, employee.HomePhone);
			db.AddInParameter(dbCommand, "p_mobile_phone", DbType.String, employee.MobilePhone);
			db.AddInParameter(dbCommand, "p_email", DbType.String, employee.Email);
			db.AddInParameter(dbCommand, "p_address", DbType.String, employee.Address);
			db.AddInParameter(dbCommand, "p_post_code", DbType.String, employee.PostCode);
            db.AddInParameter(dbCommand, "p_dimission", DbType.Int32, employee.IsOnPost ? 1 : 0);
			db.AddInParameter(dbCommand, "p_memo", DbType.String, employee.Memo);
			db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, employee.IsGroupLeader);
			db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, employee.TechnicianTypeID);
			db.AddInParameter(dbCommand, "p_post_no", DbType.String, employee.PostNo);
			db.AddInParameter(dbCommand, "p_pinyin_code", DbType.String, employee.PinYinCode);
			db.AddInParameter(dbCommand, "p_login_count", DbType.Int32, employee.LoginCount);
			db.AddInParameter(dbCommand, "p_login_time", DbType.Int32, employee.LoginTime);

			if (db.ExecuteNonQuery(dbCommand) > 0)
				return true;
			else
				return false;
		}

		public void UpdateEmployee(IList<Employee> objList, IList<Employee> objAddList)
		{
			Database db = DatabaseFactory.CreateDatabase();

			DbConnection connection = db.CreateConnection();
			connection.Open();
			DbTransaction transaction = connection.BeginTransaction();

			try
			{
				foreach (Employee employee in objList)
				{
					string sqlCommand = "USP_EMPLOYEE_U_Import1";
					DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

					db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employee.EmployeeID);
					db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, employee.OrgID);
					db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employee.EmployeeName);
					db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, employee.PostID);
					db.AddInParameter(dbCommand, "p_sex", DbType.String, employee.Sex);
                    db.AddInParameter(dbCommand, "p_dimission", DbType.Int32, employee.IsOnPost ? 1 : 0);
					db.AddInParameter(dbCommand, "p_post_no", DbType.String, employee.PostNo);
					db.AddInParameter(dbCommand, "p_pinyin_code", DbType.String, employee.PinYinCode);
					db.AddInParameter(dbCommand, "p_work_no", DbType.String, employee.WorkNo);

					db.ExecuteNonQuery(dbCommand, transaction);

					string sqlCommand1 = "USP_SYSTEM_USER_U_Import1";
					DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand1);

					db.AddInParameter(dbCommand1, "p_user_id", DbType.String, employee.WorkNo);
					db.AddInParameter(dbCommand1, "p_employee_id", DbType.Int32, employee.EmployeeID);
					db.ExecuteNonQuery(dbCommand1, transaction);
				}

				foreach (Employee employee in objAddList)
				{
					string sqlCommand = "USP_EMPLOYEE_I_Import";
					DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

					db.AddOutParameter(dbCommand, "p_employee_id", DbType.Int32, 4);
					db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, employee.OrgID);
					db.AddInParameter(dbCommand, "p_employee_name", DbType.String, employee.EmployeeName);
					db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, employee.PostID);
					db.AddInParameter(dbCommand, "p_sex", DbType.String, employee.Sex);
					db.AddInParameter(dbCommand, "p_dimission", DbType.Int32, employee.IsOnPost ? 1 : 0);
					db.AddInParameter(dbCommand, "p_post_no", DbType.String, employee.PostNo);
					db.AddInParameter(dbCommand, "p_pinyin_code", DbType.String, employee.PinYinCode);
					db.AddInParameter(dbCommand, "p_is_group_leader", DbType.Int32, employee.IsGroupLeader);
					db.AddInParameter(dbCommand, "p_tech_id", DbType.Int32, employee.TechnicianTypeID);
					db.AddInParameter(dbCommand, "p_work_no", DbType.String, employee.WorkNo);

					db.ExecuteNonQuery(dbCommand, transaction);

					int id = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_employee_id"));

					string sqlCommand1 = "USP_SYSTEM_USER_I";
					DbCommand dbCommand1 = db.GetStoredProcCommand(sqlCommand1);

					db.AddInParameter(dbCommand1, "p_user_id", DbType.String, employee.WorkNo);
					db.AddInParameter(dbCommand1, "p_password", DbType.String, "111111");
					db.AddInParameter(dbCommand1, "p_employee_id", DbType.Int32, id);
					db.AddInParameter(dbCommand1, "p_role_id", DbType.Int32, 0);
					db.AddInParameter(dbCommand1, "p_memo", DbType.String, "");
					db.ExecuteNonQuery(dbCommand1, transaction);
				}
				transaction.Commit();
			}
			catch (Exception ex)
			{
				transaction.Rollback();
				throw ex;
			}
			connection.Close();
		}

        public bool UpdateEmployee1(Employee employee)
        {
            return UpdateEmployee(employee);
        }

        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="employeeID">要删除的员工ID</param>
        public bool DeleteEmployee(int employeeID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EMPLOYEE_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employeeID);

            if (db.ExecuteNonQuery(dbCommand) > 0)
                return true;
            else
                return false;
        }

		public bool CanDeleteEmployee(int employeeID)
		{
			Database db = DatabaseFactory.CreateDatabase();

			string sqlCommand = "USP_EMPLOYEE_Can_D";
			DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

			db.AddInParameter(dbCommand, "p_employee_id", DbType.Int32, employeeID);
			db.AddOutParameter(dbCommand,"p_count",DbType.Int32,4);

			db.ExecuteNonQuery(dbCommand);

			int count = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

			if (count == 0)
				return true;
			else
				return false;
		}

        public void DeleteEmployeeByOrgID(int orgID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_EMPLOYEE_D_ORG";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_org_id", DbType.Int32, orgID);

            db.ExecuteNonQuery(dbCommand);
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

            if (string.IsNullOrEmpty(orderBy))
            {
                return string.Empty;
            }

            string mappingOrderBy = string.Empty;
            string[] orderByConditions = orderBy.Split(new char[] { ',' });

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
                    if (mappingOrderBy != string.Empty)
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

        public static Employee CreateModelObject(IDataReader dataReader)
        {
            return new Employee(
                DataConvert.ToInt(dataReader[GetMappingFieldName("EmployeeID")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("OrgID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("OrgName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("WorkNo")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("EmployeeName")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("PostID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("PostName")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Sex")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("Birthday")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("NativePlace")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Folk")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("Wedding")]),
                DataConvert.ToDateTime(dataReader[GetMappingFieldName("BeginDate")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("WorkPhone")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("HomePhone")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("MobilePhone")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Email")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Address")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("PostCode")]),
                DataConvert.ToBool(dataReader[GetMappingFieldName("IsOnPost")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("IsGroupLeader")]),
                DataConvert.ToInt(dataReader[GetMappingFieldName("TechnicianTypeID")]),
                DataConvert.ToString(dataReader[GetMappingFieldName("PostNo")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("LoginCount")]),
				DataConvert.ToInt(dataReader[GetMappingFieldName("LoginTime")])
				);
        }
    }
}
