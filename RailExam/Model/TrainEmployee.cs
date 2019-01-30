using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class TrainEmployee
    {
        private int _trainEmployeeID = 0;
        private int _employeeID = 0;
        private int _trainTypeID = 0;

        public int TrainEmployeeID
        {
            get { return _trainEmployeeID; }
            set { _trainEmployeeID = value; }
        }

        public int EmployeeID
        {
            get { return _employeeID; }
            set { _employeeID = value; }
        }

        public int TrainTypeID
        {
            get { return _trainTypeID; }
            set { _trainTypeID = value; }
        }

        public TrainEmployee() { }

        public TrainEmployee(int? trainEmployeeID, int? employeeID, int? trainTypeID)
        {
            _trainEmployeeID = trainEmployeeID ?? _trainEmployeeID;
            _employeeID = employeeID ?? _employeeID;
            _trainTypeID = trainTypeID ?? _trainTypeID;
        }
    }
}
