using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
	public class EmployeeRailEduBLL
	{
		private static readonly EmployeeRailEduDAL dal = new EmployeeRailEduDAL();

		public void AddEmployeeRailEdu(EmployeeRailEdu employee)
		{
			dal.AddEmployeeRailEdu(employee);
		}

		public void UpdateEmployeeRailEdu(EmployeeRailEdu employee)
		{
			dal.UpdateEmployeeRailEdu(employee);
		}
	}
}
