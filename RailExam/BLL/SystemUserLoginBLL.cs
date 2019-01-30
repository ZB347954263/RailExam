using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
	public class SystemUserLoginBLL
	{
		private static readonly SystemUserLoginDAL dal = new SystemUserLoginDAL();

		public IList<SystemUserLogin> GetSystemUserLogin(int employeeID)
		{
			return dal.GetSystemUserLogin(employeeID);
		}

		public IList<SystemUserLogin> GetSystemUserLoginByOrgID(int orgID)
		{
			return dal.GetSystemUserLoginByOrgID(orgID);
		}

		public void AddSystemUserLogin(SystemUserLogin systemUser)
		{
			dal.AddSystemUserLogin(systemUser);
		}

		public void DeleteSystemUserLogin(int employeeID)
		{
			dal.DeleteSystemUserLogin(employeeID);
		}

		public void DeleteSystemUserLogin()
		{
			dal.DeleteSystemUserLogin();
		}

		public void ClearSystemUserLogin()
		{
			dal.ClearSystemUserLogin();
		}

	}
}
