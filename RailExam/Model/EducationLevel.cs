using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	public class EducationLevel
	{
		private int _educationLevelID = 0;
        private string _educationLevelName = string.Empty;
		private string _memo = string.Empty;
		private int _orderIndex = 0;

		public int EducationLevelID
        {
            set { _educationLevelID = value; }
            get { return _educationLevelID; }
        }

		public string EducationLevelName
        {
			set { _educationLevelName = value; }
			get { return _educationLevelName; }
        }

		public string Memo
		{
			set { _memo = value; }
			get { return _memo; }
		}

		public int OrderIndex
		{
			set { _orderIndex = value;}
			get { return _orderIndex; }
		}

		public EducationLevel(int? educationLevelID, string educationLevelName, string memo, int? orderIndex)
        {
			_educationLevelID = educationLevelID ?? _educationLevelID;
			_educationLevelName = educationLevelName;
			_memo = memo;
			_orderIndex = orderIndex ?? _orderIndex;
        }

		public EducationLevel()
        {
            
        }
	}
}
