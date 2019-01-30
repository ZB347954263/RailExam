using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	public class EmployeeRailEdu
	{
		private int _employeeID;
		private int _railEduEmployeeID;

		public int EmployeeID
		{
			get { return _employeeID; }
			set { _employeeID = value; }
		}

		public int RailEduEmployeeID
		{
			get { return _railEduEmployeeID; }
			set { _railEduEmployeeID = value; }
		}

		public EmployeeRailEdu()
		{
		}

		public EmployeeRailEdu(int? employeeID,int? railEduEmployeeID)
		{
			_employeeID = employeeID ?? _employeeID;
			_railEduEmployeeID = railEduEmployeeID ?? _railEduEmployeeID;
		}
	}
}
