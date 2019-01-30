using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace RailExam.Model
{
    public class ExamResultUpdate
    {

 /// <summary>
        /// 内部成员变量
        /// </summary>
        private int _examResultUpdateId = 0;
        private int _examResultId = 1;
        private string _updatePerson = string.Empty;
        private decimal _oldScore = 0;
        private decimal _newScore = 0;
        private string _updateCause = string.Empty;       
        private string _memo = string.Empty;
        private DateTime _updateDate;
        private string _examName = string.Empty;
        private string _employeeName = string.Empty;
        private string _orgName = string.Empty;

        public ExamResultUpdate() { } 

        public ExamResultUpdate(
            int? examResultUpdateId,
            int? examResultId,
            decimal? oldScore,
            decimal? newScore,
            string updatePerson,
            DateTime? updateDate,
            string updateCause,
            string memo,
             string examName,
             string employeeName,
             string OrgName
            )
        {
            _examResultUpdateId = examResultUpdateId ?? _examResultUpdateId;
            _examResultId = examResultId ?? _examResultId;
            _updatePerson = updatePerson;
            _oldScore = oldScore ?? _oldScore;
            _newScore = newScore ?? _newScore;
            _updateDate = updateDate ?? _updateDate;
            _updateCause = updateCause;
            _memo = memo;
            _examName = examName;
            _employeeName = employeeName;
            _orgName = OrgName;
        }

        public string EmployeeName
        {
            set
            {
                _employeeName = value;
            }
            get
            {
                return _employeeName;
            }
        }
        public string OrgName
        {
            set
            {
                _orgName = value;
            }
            get
            {
                return _orgName;
            }
        }

        public string ExamName
        {
            set
            {
                _examName = value;
            }
            get
            {
                return _examName;
            }
        }




        public int examResultUpdateId
        {
            set
            {
                _examResultUpdateId = value;
            }
            get
            {
                return _examResultUpdateId;
            }
        }

        public int examResultId
        {
            set
            {
                _examResultId = value;
            }
            get
            {
                return _examResultId;
            }
        }

        public string updatePerson
        {
            set
            {
                _updatePerson = value;
            }
            get
            {
                return _updatePerson;
            }
        }

        public decimal oldScore
        {
            set
            {
                _oldScore = value;
            }
            get
            {
                return _oldScore;
            }
        }

        public decimal newScore
        {
            set
            {
                _newScore = value;
            }
            get
            {
                return _newScore;
            }
        }

        public DateTime updateDate
        {
            set
            {
                _updateDate = value;
            }
            get
            {
                return _updateDate;
            }
        }

        public string updateCause
        {
            set
            {
                _updateCause = value;
            }
            get
            {
                return _updateCause;
            }
        }

        public string Memo
        {
            set
            {
                _memo = value;
            }
            get
            {
                return _memo;
            }
        }
    }
}

