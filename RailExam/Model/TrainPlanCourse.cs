using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class TrainPlanCourse
    {
        private int _trainPlanID = 0;
        private int _trainCourseID = 0;
        private decimal _process = 0;
        private int _statusID = 1;
        private string _statusName = string.Empty;
        private string _memo = string.Empty;
        private TrainCourse _trainCourseList;

        public int TrainPlanID
        {
            get { return _trainPlanID; }
            set { _trainPlanID = value; }
        }

        public int TrainCourseID
        {
            get { return _trainCourseID; }
            set { _trainCourseID = value; }
        }

        public decimal Process
        {
            get { return _process; }
            set { _process = value; }
        }

        public int StatusID
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

        public TrainCourse TrainCourseList
        {
            get { return _trainCourseList; }
            set { _trainCourseList = value; }
        }

        public TrainPlanCourse() { }

        public TrainPlanCourse(int? trainPlanID,
            int? trainCourseID,
            decimal? process,
            int? statusID,
            string statusName,
            string memo,
            TrainCourse trainCourseList)
        {
            _trainPlanID = trainPlanID ?? _trainPlanID;
            _trainCourseID = trainCourseID ?? _trainCourseID;
            _process = process ?? _process;
            _statusID = statusID ?? _statusID;
            _statusName = statusName;
            _memo = memo;
            _trainCourseList = trainCourseList;
        }
    }
}
