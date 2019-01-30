using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class TrainPlan
    {
        private int _trainPlanID = 0;
        private string _trainName = string.Empty;
        private string _trainContent = string.Empty;
        private DateTime _beginDate = DateTime.Today;
        private DateTime _endDate = DateTime.Today;
        private bool _hasExam = true;
        private string _examForm = string.Empty;
        private int _statusID = 1;
        private string _statusName = string.Empty;
        private string _memo = string.Empty;

        public int TrainPlanID
        {
            get { return _trainPlanID; }
            set { _trainPlanID = value; }
        }

        public string TrainName
        {
            get { return _trainName; }
            set { _trainName = value; }
        }

        public string TrainContent
        {
            get { return _trainContent; }
            set { _trainContent = value; }
        }

        public DateTime BeginDate
        {
            get { return _beginDate; }
            set { _beginDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public bool  HasExam
        {
            get { return _hasExam; }
            set { _hasExam = value; }
        }

        public string ExamForm
        {
            get { return _examForm; }
            set { _examForm = value; }
        }

        public int  StatusID
        {
            get { return _statusID; }
            set { _statusID = value; }
        }

        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        public TrainPlan() { }

        public TrainPlan (int?¡¡trainPlanID,
            string trainName, 
            string trainContent, 
            DateTime? beginDate, 
            DateTime? endDate, 
            bool? hasExam, 
            string examForm , 
            int? statusID, 
            string statusName, 
            string memo)
        {
            _trainPlanID = trainPlanID ?? _trainPlanID;
            _trainName = trainName;
            _trainContent = trainContent;
            _beginDate = beginDate ?? _beginDate;
            _endDate = endDate ?? _endDate;
            _hasExam = hasExam ?? _hasExam;
            _examForm = examForm;
            _statusID = statusID ?? _statusID;
            _statusName = statusName;
            _memo = memo;
        }
    }
}
