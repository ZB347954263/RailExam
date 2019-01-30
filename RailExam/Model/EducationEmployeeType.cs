using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	public  class EducationEmployeeType
	{
		private int _educationEmployeeTypeID = 0;
		private string _typeName = string.Empty;

		public int EducationEmployeeTypeID
		{
			set { _educationEmployeeTypeID = value; }
			get { return _educationEmployeeTypeID; }
		}

		public string TypeName
		{
			set { _typeName = value; }
			get { return _typeName; }
		}
	 
		public EducationEmployeeType(int? technicianTitleTypeID, string typeName)
        {
			_educationEmployeeTypeID = technicianTitleTypeID ?? _educationEmployeeTypeID;
            _typeName = typeName;
		
        }
		public EducationEmployeeType()
		{ }

	}
}
