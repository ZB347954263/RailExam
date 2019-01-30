using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	public class RandomExamApply
	{
		private int _randomExamApplyID = 0;
		private int _randomExamID = 0;
		private int _randomExamResultCurrentID = 0;
		private int _randomExamResultID = 0;
		private int _applyStatus = 0;
		private string _applyStatusName = string.Empty;
		private string _employeeName = string.Empty;
		private string _workNo = string.Empty;
		private string _orgName = string.Empty;
		private bool _codeFlag = true;
		private string _ipAddress = string.Empty;
		private bool _isChecked = false;
		private int _employee_id = 0;
		private string _examName = string.Empty;
		private string _randomExamCode = string.Empty;

		public string ExamName
		{
			get { return _examName; }
			set { _examName = value; }
		}

		public string RandomExamCode
		{
			get { return _randomExamCode; }
			set { _randomExamCode = value; }
		}

		public bool IsChecked
		{
			get { return _isChecked; }
			set { _isChecked = value; }
		}
		public int EmployeeID
		{
			get { return _employee_id; }
			set { _employee_id = value; }
		}
		
		public int RandomExamApplyID
		{
			get { return _randomExamApplyID; }
			set { _randomExamApplyID = value; }
		}

		public int RandomExamID
		{
			get { return _randomExamID; }
			set { _randomExamID = value;}
		}

		public int RandomExamResultCurID
		{
			get { return _randomExamResultCurrentID; }
			set { _randomExamResultCurrentID = value;}
		}

		public int RandomExamResultID
		{
			get { return _randomExamResultID; }
			set { _randomExamResultID = value; }
		}

		public int ApplyStatus
		{
			get { return _applyStatus; }
			set { _applyStatus = value; }
		}

		public string ApplyStatusName
		{
			get { return _applyStatusName; }
			set { _applyStatusName = value;}
		}

		public bool CodeFlag
		{
			get { return _codeFlag; }
			set { _codeFlag = value;}
		}

		public string EmployeeName
		{
			get { return _employeeName; }
			set { _employeeName = value;}
		}

		public string WorkNo
		{
			get { return _workNo; }
			set { _workNo = value;}
		}

		public string OrgName
		{
			get { return _orgName; }
			set { _orgName = value;}
		}

		public string IPAddress
		{
			get { return _ipAddress; }
			set { _ipAddress = value; }
		}

		public RandomExamApply()
		{
			
		}

		public RandomExamApply(int? randomExamApplyID,
			int? randomExamID,
			int? randomExamResultCurrentID,
			int? randomExamResultID,
			bool?  codeFlag,
			int? applyStatus,
			string applyStatusName,
			string employeeName,
			string workNo,
			string orgName,
			string ipAddress,
			int? employeeID)
		{
			_randomExamApplyID = randomExamApplyID ?? _randomExamApplyID;
			_randomExamID = randomExamID ?? _randomExamID;
			_randomExamResultCurrentID = randomExamResultCurrentID ?? _randomExamResultCurrentID;
			_randomExamResultID = randomExamResultID ?? _randomExamResultID;
			_codeFlag = codeFlag ?? _codeFlag;
			_applyStatus = applyStatus ?? _applyStatus;
			_applyStatusName = applyStatusName;
			_employeeName = employeeName;
			_workNo = workNo;
			_orgName = orgName;
			_ipAddress = ipAddress;
			_employee_id = employeeID ?? _employee_id;
		}
	}
}
