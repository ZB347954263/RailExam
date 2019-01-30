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
	public class EmployeeDetailBLL
	{
		private static readonly EmployeeDetailDAL dal = new EmployeeDetailDAL();

		/// <summary>
		/// 新增员工
		/// </summary>
		/// <param name="employee">新增的员工信息</param>
		/// <returns></returns>
		public int AddEmployee(Database db, DbTransaction trans, EmployeeDetail employee)
		{
			return dal.AddEmployee(db, trans, employee);
		}

		public int  AddEmployee(EmployeeDetail employee)
		{
			return dal.AddEmployee(employee);
		}

		public void UpdateEmployee(Database db, DbTransaction trans, EmployeeDetail employee)
		{
			dal.UpdateEmployee(db,trans,employee);
		}

		public void UpdateEmployee(EmployeeDetail employee)
		{
			dal.UpdateEmployee(employee);
		}

		public EmployeeDetail GetEmployee(int employeeID)
		{
			return dal.GetEmployee(employeeID);
		}

		public IList<EmployeeDetail> GetEmployee(string employeeName, string identifyCode)
		{
			return dal.GetEmployee(employeeName,identifyCode);
		}

		public bool DeleteEmployeeDetail(int employeeID)
		{
			return dal.DeleteEmployeeDetail(employeeID);
		}

		public IList<EmployeeDetail> GetEmployeeByWhereClause(string whereClause)
		{
			return dal.GetEmployeeByWhereClause(whereClause);
		}

		public int GetEmployeeByWhere(string strWhere)
		{
			return Convert.ToInt32(dal.GetEmployeeByWhere(strWhere));
		}
	}
}
