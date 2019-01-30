using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class TrainPlanStatus
    {
        private int _trainPlanStatusID = 0;
        private string _statusName = string.Empty;
        private string _description = string.Empty;
        private bool _isDefault = true;
        private string _memo = string.Empty;

        public int TrainPlanStatusID
        {
            get { return _trainPlanStatusID; }
            set { _trainPlanStatusID = value; }
        }

        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        public bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        public TrainPlanStatus() { }

        public TrainPlanStatus(int? trainPlanStatusID,string statusName,string description,bool? isDefault,string memo)
        {
            _trainPlanStatusID = trainPlanStatusID ?? _trainPlanStatusID;
            _statusName = statusName;
            _description = description;
            _isDefault = isDefault ?? _isDefault;
            _memo = memo;
        }
    }
}
