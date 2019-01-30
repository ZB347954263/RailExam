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
	public class EmployeeTransferBLL
	{
		private static readonly EmployeeTransferDAL dal = new EmployeeTransferDAL();

		public void AddEmployeeTransfer(IList<EmployeeTransfer> objList)
		{
			dal.AddEmployeeTransfer(objList);
		}

		public bool DeleteEmployeeTransfer(int employeeTransferID)
		{
			return dal.DeleteEmployeeTransfer(employeeTransferID);
		}

		public IList<EmployeeTransfer> GetEmployeeTransferOutByOrgID(int orgID)
		{
			return dal.GetEmployeeTransferOutByOrgID(orgID);
		}

		public IList<EmployeeTransfer> GetEmployeeTransferToByOrgID(int orgID)
		{
			return dal.GetEmployeeTransferToByOrgID(orgID);
		}

		public EmployeeTransfer GetEmployeeTransfer(int transferID)
		{
			return dal.GetEmployeeTransfer(transferID);
		}

	}
}
