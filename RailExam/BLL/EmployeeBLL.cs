using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace RailExam.BLL
{
    /// <summary>
    /// 业务逻辑：员工
    /// </summary>
    public class EmployeeBLL
    {
        private static readonly EmployeeDAL dal = new EmployeeDAL();

        /// <summary>
        /// 获取员工数据
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
            IList<Employee> employeeList = dal.GetEmployees(employeeID, orgID, orgName, workNo, employeeName, postID, postName, sex,
                birthday, nativePlace, folk, wedding, beginDate, workPhone, homePhone, mobilePhone, email,
                address, postCode, dimission, memo, startRowIndex, maximumRows, orderBy);

            return employeeList;
        }

        public IList<Employee> GetEmployees()
        {
            IList<Employee> employeeList = dal.GetEmployees();

            return employeeList;
        }

        public DataSet  GetEmployeesByEmployeeIdS(string employeeIDs)
        {
            DataSet ds = dal.GetEmployeesByEmployeeIdS(employeeIDs);

            return ds;
        }

        public IList<Employee> GetEmployeesByEmployeeId(string employeeIDs)
        {
            return dal.GetEmployeesByEmployeeId(employeeIDs);
        }


        public IList<Employee> GetEmployees(int orgid, string postIDPath)
        {
            IList<Employee> employeeList = dal.GetEmployees(orgid, postIDPath);

            return employeeList;
        }

        public Employee GetChooseEmployeeInfo(string employeeID)
        {
            return dal.GetChooseEmployeeInfo(employeeID);
        }


        public IList<Employee> GetEmployeesByOrgIDPath(string orgIDPath)
        {
            return dal.GetEmployeesByOrgIDPath(orgIDPath);
        }

        public IList<Employee> GetEmployees(int orgID, string postIDPath, string workNo, string employeeName, string sex, string postName, int status)
        {
			IList<Employee> employeeList = dal.GetEmployees(orgID, postIDPath, workNo, employeeName, sex, postName, status);

            return employeeList;
        }


        public IList<Employee> GetEmployees(int orgID, string postIDPath, string workNo, string employeeName, string sex, string postName, string strOrderBy)
        {
            IList<Employee> employeeList = dal.GetEmployees(orgID, postIDPath, workNo, employeeName, sex, postName, strOrderBy);

            return employeeList;
        }

        public IList<Employee> GetEmployees(int orgID, string postIDPath, string workNo, string employeeName, string sex, string postName, string strOrderBy, int startRow, int endRow, ref int nCount)
        {
            IList<Employee> employeeList = dal.GetEmployees(orgID, postIDPath, workNo, employeeName, sex, postName, strOrderBy, startRow, endRow,ref nCount);

            return employeeList;
        }


        public IList<Employee> GetEmployeesByPost(int PostID, string postIDPath, string workNo, string employeeName, string sex, string postName)
        {
            IList<Employee> employeeList = dal.GetEmployeesByPost(PostID, postIDPath, workNo, employeeName, sex, postName);

            return employeeList;
        }

        public IList<Employee> GetEmployeesByPost(int PostID, string postIDPath, string workNo, string employeeName, string sex, string postName, string strOrderBy)
        {
            IList<Employee> employeeList = dal.GetEmployeesByPost(PostID, postIDPath, workNo, employeeName, sex, postName, strOrderBy);

            return employeeList;
        }


        public IList<Employee> GetEmployeesByPost(int PostID, string postIDPath, string workNo, string employeeName, string sex, string postName, string strOrderBy, int startRow, int endRow, ref int nCount)
        {
            IList<Employee> employeeList = dal.GetEmployeesByPost(PostID, postIDPath, workNo, employeeName, sex, postName, strOrderBy, startRow, endRow, ref nCount);

            return employeeList;
        }

        public IList<Employee> GetEmployeesSelect(string orgID, string postID, string workNo, string employeeName, string pinyincode,string strOrderBy,string groupLeader,int safeLevelID, int startRow, int endRow, ref int nCount)
        {
            return
                dal.GetEmployeesSelect(Convert.ToInt32(orgID), Convert.ToInt32(postID), workNo, employeeName, pinyincode, strOrderBy, Convert.ToInt32(groupLeader), safeLevelID,startRow, endRow,
                                       ref nCount);
        }

		public IList<Employee> GetEmployeesSelectByTransfer(string orgID, string postID, string workNo, string employeeName, string pinyincode, string strOrderBy, string groupLeader, int startRow, int endRow, ref int nCount)
		{
			return
				dal.GetEmployeesSelectByTransfer(Convert.ToInt32(orgID), Convert.ToInt32(postID), workNo, employeeName, pinyincode, strOrderBy, Convert.ToInt32(groupLeader), startRow, endRow,
									   ref nCount);
		}

    	public IList<Employee> GetEmployeesSelect(string orgID, string postID, string workNo, string employeeName, string pinyincode, string strOrderBy, string groupLeader)
        {
            return dal.GetEmployeesSelect(Convert.ToInt32(orgID), Convert.ToInt32(postID), workNo, employeeName, pinyincode,strOrderBy,groupLeader);
        }

        public IList<Employee> GetEmployeesSelect(int orgID,string pinyincode,string workNo, string employeeName, string strOrderBy, int startRow, int endRow,ref int nItemCount)
        {
            return dal.GetEmployeesSelect(orgID,pinyincode,workNo, employeeName, strOrderBy, startRow, endRow,ref nItemCount);
        }

        public IList<Employee> GetEmployeesSelect(int orgID,string pinyincode, string workNo, string employeeName,string strOrderBy)
        {
            return dal.GetEmployeesSelect(orgID,pinyincode,workNo, employeeName, strOrderBy);
        }

        /// <summary>
        /// 按ID取Employee
        /// </summary>
        /// <param name="employeeID">ID</param>
        /// <returns>Employee</returns>
        public Employee GetEmployee(int employeeID)
        {
            //if (employeeID < 1)
            //{
            //    return null;
            //}

            return dal.GetEmployee(employeeID);
        }

		public int GetEmployeeByWorkNo(string workNo)
		{
			return dal.GetEmployeeByWorkNo(workNo);
		}

        /// <summary>
        /// 新增员工
        /// </summary>
        /// <param name="employee">新增的员工信息</param>
        /// <returns></returns>
        public int AddEmployee(Employee employee)
        {
            int nEmployeeID = dal.AddEmployee(employee);
            if (nEmployeeID > 0)
            {
                SystemLogBLL systemLogBLL = new SystemLogBLL();

                systemLogBLL.WriteLog("新增员工：" + employee.EmployeeName + "（" + employee.WorkNo + "）基本信息");
            }

            return nEmployeeID;
        }

		/// <summary>
		/// 新增员工
		/// </summary>
		/// <param name="employee">新增的员工信息</param>
		/// <returns></returns>
		public int AddEmployee(Database db, DbTransaction trans, Employee employee)
		{
			return dal.AddEmployee(db, trans, employee);
		}

    	/// <summary>
        /// 更新员工
        /// </summary>
        /// <param name="employee">更新后的员工信息</param>
        public void UpdateEmployee(Employee employee)
        {
            if (dal.UpdateEmployee(employee))
            {
                SystemLogBLL systemLogBLL = new SystemLogBLL();

                systemLogBLL.WriteLog("修改员工：" + employee.EmployeeName + "（" + employee.WorkNo + "）基本信息");
            }
        }

        public void UpdateEmployee(Database db, DbTransaction trans, Employee employee)
        {
            dal.UpdateEmployee(db, trans, employee);
        }

		public void UpdateEmployeeInStation(Employee employee)
		{
			dal.UpdateEmployeeInStation(employee);
		}

        public void UpdateEmployee1(Employee employee)
        {
            if (dal.UpdateEmployee1(employee) && employee.Flag)
            {
                SystemLogBLL systemLogBLL = new SystemLogBLL();

                systemLogBLL.WriteLog("修改员工：" + employee.EmployeeName + "（" + employee.WorkNo + "）基本信息");
            }
        }

        public void UpdateEmployeeWithOutLog(Employee employee)
        {
             dal.UpdateEmployee(employee);
        }

        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="employee">要删除的员工</param>
        public void DeleteEmployee(Employee employee)
        {
            Employee obj = GetEmployee(employee.EmployeeID);
            SystemLogBLL systemLogBLL = new SystemLogBLL();
            systemLogBLL.WriteLog("删除员工：" + obj.EmployeeName + "（" + obj.WorkNo + "）基本信息");
            dal.DeleteEmployee(employee.EmployeeID);
        }

        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="employeeID">要删除的员工ID</param>
        public void DeleteEmployee(int employeeID)
        {
            dal.DeleteEmployee(employeeID);
        }

		public bool CanDeleteEmployee(int employeeID)
		{
			return dal.CanDeleteEmployee(employeeID);
		}

        public int GetCount(int employeeID, int orgID, string orgName, string workNo, string employeeName, int postID,
            string postName, string sex, DateTime birthday, string nativePlace, string folk, bool wedding,
            DateTime beginDate, string workPhone, string homePhone, string mobilePhone, string email,
            string address, string postCode, bool dimission, string memo)
        {
            return dal.RecordCount;
        }

        public void DeleteEmployeeByOrgID(int orgID)
        {
            dal.DeleteEmployeeByOrgID(orgID);
        }

        public void AddEmployeeImport(IList<Employee> objList)
        {
            dal.AddEmployeeImport(objList);
        }

        public IList<Employee> GetAllEmployees()
        {
            return dal.GetAllEmployees();
        }

		public void UpdateEmployeeImport(Hashtable htOld, Hashtable htNew,int orgID)
		{
			dal.UpdateEmployeeImport(htOld,htNew,orgID);
		}

		public void UpdateEmployee(IList<Employee> objList, IList<Employee> objAddList)
		{
			dal.UpdateEmployee(objList, objAddList);
		}

		public IList<Employee> GetRandomExamNoResultEmployee(string strEmployeeID)
		{
			return dal.GetRandomExamNoResultEmployee(strEmployeeID);
		}

		public IList<Employee> GetEmployeeByWhereClause(string whereClause)
		{
			return dal.GetEmployeeByWhereClause(whereClause);
		}
    }
}
