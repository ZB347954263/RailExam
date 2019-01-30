using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class TrainPlanEmployee
    {
        private int _trainPlanID = 0;
        private int _trainPlanEmployeeID = 0;
        private decimal _process = 0;
        private int _statusID = 1;
        private string _statusName = string.Empty;
        private string _memo = string.Empty;
        private Employee _objEmployee;

        public int TrainPlanID
        {
            get { return _trainPlanID; }
            set { _trainPlanID = value; }
        }

        public int TrainPlanEmployeeID
        {
            get { return _trainPlanEmployeeID; }
            set { _trainPlanEmployeeID = value; }
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

        public Employee TrainEmployeeList
        {
            get { return _objEmployee; }
            set { _objEmployee = value; }
        }

        public TrainPlanEmployee() { }

        public TrainPlanEmployee(int? trainPlanID,
            int? trainPlanEmployeeID,
            decimal? process,
            int? statusID,
            string statusName,
            string memo,
            Employee objEmployee)
        {
            _trainPlanID = trainPlanID ?? _trainPlanID;
            _trainPlanEmployeeID = trainPlanEmployeeID ?? _trainPlanEmployeeID;
            _process = process ?? _process;
            _statusID = statusID ?? _statusID;
            _statusName = statusName;
            _memo = memo;
            _objEmployee = objEmployee;
        }
    }
}
