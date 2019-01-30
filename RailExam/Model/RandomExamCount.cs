using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class RandomExamCount
    {
        private int _employeeID = 0;
        private int _randomExamID = 0;
        private int _count = 0;

        public RandomExamCount()
        {
            
        }

        public RandomExamCount(int? employeeID, int? randomExamID,int? count)
        {
            _employeeID = employeeID ?? _employeeID;
            _randomExamID = randomExamID ?? _randomExamID;
            _count = count ?? _count;
        }

        public int EmployeeID
        {
            get
            {
                return _employeeID;
            }
            set
            {
                _employeeID = value;
            }
        }

        public int RandomExamID
        {
            get
            {
                return _randomExamID;
            }
            set
            {
                _randomExamID = value;
            }
        }

        public int Count
        {
            get
            {
                return _count;
            }
            set
            {
                _count = value;
            }
        }
    }
}
