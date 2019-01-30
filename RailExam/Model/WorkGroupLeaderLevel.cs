using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	public class WorkGroupLeaderLevel
	{
		private int _workGroupLeaderLevelID = 0;
        private string _levelName = string.Empty;
		private string _memo = string.Empty;
		private int _orderIndex = 0;

		public int WorkGroupLeaderLevelID
        {
            set { _workGroupLeaderLevelID = value; }
            get { return _workGroupLeaderLevelID; }
        }

		public string LevelName
        {
			set { _levelName = value; }
			get { return _levelName; }
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

		public WorkGroupLeaderLevel(int? workGroupLeaderLevelID, string levelName,int? orderIndex, string memo)
        {
			_workGroupLeaderLevelID = workGroupLeaderLevelID ?? _workGroupLeaderLevelID;
			_levelName = levelName;
			_memo = memo;
			_orderIndex = orderIndex ?? _orderIndex;
        }

		public WorkGroupLeaderLevel()
        {
            
        }
	}
}
