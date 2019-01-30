using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	public class EmployeeTransfer
	{
		private int _employeeTransferID = 0;
		private int _transferToOrgID = 0;
		private int _employeeID = 0;
		private DateTime _transferOutDate = DateTime.Today;
		private string _employeeName = string.Empty;
		private string _sex = "ÄÐ";
		private string _transferOutOrgName = string.Empty;
		private string _transferToOrgName = string.Empty;
		private string _workNo = string.Empty;
		private string _postNo = string.Empty;
		private string _transferOutOrgPath = string.Empty;
		private string _postName = string.Empty;

		public int EmployeeTransferID
		{
			get { return _employeeTransferID; }
			set { _employeeTransferID = value; }
		}

		public int TransferToOrgID
		{
			get { return _transferToOrgID; }
			set { _transferToOrgID = value; }
		}

		public int EmployeeID
		{
			get { return _employeeID; }
			set { _employeeID = value; }
		}

		public DateTime TransferOutDate
		{
			get { return _transferOutDate; }
			set { _transferOutDate = value; }
		}

		public string EmployeeName
		{
			get { return _employeeName; }
			set { _employeeName = value; }
		}

		public string Sex
		{
			get { return _sex; }
			set { _sex = value; }
		}

		public string TransferOutOrgName
		{
			get { return _transferOutOrgName; }
			set { _transferOutOrgName = value; }
		}

		public string TransferToOrgName
		{
			get { return _transferToOrgName; }
			set { _transferToOrgName = value; }
		}

		public string WorkNo
		{
			get { return _workNo; }
			set { _workNo = value; }
		}

		public string PostNo
		{
			get { return _postNo; }
			set { _postNo = value; }
		}

		public string TransferOutOrgPath
		{
			get { return _transferOutOrgPath; }
			set {_transferOutOrgPath = value; }
		}

		public string PostName
		{
			get { return _postName; }
			set {_postName = value; }
		}

		public EmployeeTransfer () {}

		public EmployeeTransfer(int? employeeTransferID, int? transferToOrgID, int? employeeID, DateTime? transferOutDate,
			string employeeName,string sex,string transferOutOrgName,string transferToOrgName,string workNo,string postNo,
			string transferOutOrgPath,string postName)
		{
			_employeeTransferID = employeeTransferID ?? _employeeTransferID;
			_transferToOrgID = transferToOrgID ?? _transferToOrgID;
			_employeeID = employeeID ?? _employeeID;
			_transferOutDate = transferOutDate ?? _transferOutDate;
			_employeeName = employeeName;
			_sex = sex;
			_transferOutOrgName = transferOutOrgName;
			_transferToOrgName = transferToOrgName;
			_workNo = workNo;
			_postNo = postNo;
			_transferOutOrgPath = transferOutOrgPath;
			_postName = postName;
		}
	}
}
