using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	public class PoliticalStatus
	{
		private int _politicalStatusID = 0;
		private string _politicalStatusName = string.Empty;
		private string _memo = string.Empty;
		private int _orderIndex = 0;

		public int PoliticalStatusID
        {
			set { _politicalStatusID = value; }
			get { return _politicalStatusID; }
        }

		public string PoliticalStatusName
        {
			set { _politicalStatusName = value; }
			get { return _politicalStatusName; }
        }

		public int OrderIndex
		{
			set { _orderIndex = value; }
			get { return _orderIndex; }
		}

		public string Memo
		{
			set { _memo = value; }
			get { return _memo; }
		}

		public PoliticalStatus(int? politicalStatusID, string politicalStatusName, int? orderIndex, string memo)
        {
			_politicalStatusID = politicalStatusID ?? _politicalStatusID;
			_politicalStatusName = politicalStatusName;
			_memo = memo;
			_orderIndex = orderIndex ?? _orderIndex;
        }

		public PoliticalStatus()
        {
            
        }
	}
}
