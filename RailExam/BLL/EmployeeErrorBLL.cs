using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace RailExam.BLL
{
	public class EmployeeErrorBLL
	{
		private static readonly EmployeeErrorDAL dal = new EmployeeErrorDAL();

		public void AddEmployeeError(IList<EmployeeError> objList)
		{
			dal.AddEmployeeError(objList);
		}

		public IList<EmployeeError> GetEmployeeErrorByOrgIDAndImportTypeID(int orgID)
		{
			return dal.GetEmployeeErrorByOrgIDAndImportTypeID(orgID);
		}

		public EmployeeError GetEmployeeError(int employeeErrorID)
		{
			return dal.GetEmployeeError(employeeErrorID);
		}

		public void DeleteEmployeeErrorByOrgIDAndImportTypeID(int orgID)
		{
			dal.DeleteEmployeeErrorByOrgIDAndImportTypeID(orgID);
		}

		public void DeleteEmployeeError(int employeeErrorID)
		{
			dal.DeleteEmployeeError(employeeErrorID);
		}
	}
}
