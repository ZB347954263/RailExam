using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	public class RandomExamCountStatistic
	{
		private int _orgID = 0;
		private string _orgName = string.Empty;
		private int _examCount = 0;
		private int _employeeCount = 0;

		public int OrgID
		{
			get { return _orgID; }
			set { _orgID = value;}
		}

		public string OrgName
		{
			get { return _orgName; }
			set { _orgName = value; }
		}

		public int ExamCount
		{
			get { return _examCount; }
			set { _examCount = value; }
		}

		public int EmployeeCount
		{
			get { return _employeeCount; }
			set { _employeeCount = value; }
		}

		public  RandomExamCountStatistic()
		{
		}

		public RandomExamCountStatistic(int? orgID,string orgName,int? examCount, int? employeeCount)
		{
			_orgID = orgID ?? _orgID;
			_orgName = orgName;
			_examCount = examCount ?? _examCount;
			_employeeCount = employeeCount ?? _employeeCount;
		}
	}
}
