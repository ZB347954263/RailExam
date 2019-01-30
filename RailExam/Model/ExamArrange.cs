using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace RailExam.Model
{
    public class ExamArrange
    {
        /// <summary>
        /// 内部成员变量
        /// </summary>
        private int _ExamArrangeId = 0;
        private int _ExamId = 0;
        private int _PaperId = 0;
        private int _orderIndex = 1;
        private DateTime _BeginTime = DateTime.Now;
        private DateTime _EndTime = DateTime.Now;       
        private string _UserIds = string.Empty;
        private string _JudgeIds = string.Empty;
        private string _memo = string.Empty;

        public ExamArrange() { }

        public ExamArrange(
            int? ExamArrangeId,
            int? ExamId,            
            int? PaperId,
            int? orderIndex,
            DateTime? BeginTime,
            DateTime? EndTime,
            string UserIds,
            string JudgeIds,
            string memo)
        {
            _ExamArrangeId = ExamArrangeId ?? _ExamArrangeId;
            _ExamId = ExamId ?? _ExamId;           
            _PaperId = PaperId ?? _PaperId;
            _orderIndex = orderIndex ?? _orderIndex;
            _UserIds = UserIds;
            _JudgeIds = JudgeIds;
            _memo = memo;
            _BeginTime = BeginTime ?? _BeginTime;
            _EndTime = EndTime ?? _EndTime;
        }

        public DateTime BeginTime
        {
            get { return _BeginTime; }
            set { _BeginTime = value; }
        }

        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }

        public int ExamArrangeId
        {
            set
            {
                _ExamArrangeId = value;
            }
            get
            {
                return _ExamArrangeId;
            }
        }

        public int ExamId
        {
            set
            {
                _ExamId = value;
            }
            get
            {
                return _ExamId;
            }
        }

        public int PaperId
        {
            set
            {
                _PaperId = value;
            }
            get
            {
                return _PaperId;
            }
        }

        public int OrderIndex
        {
            set
            {
                _orderIndex = value;
            }
            get
            {
                return _orderIndex;
            }
        }

        public string UserIds
        {
            set
            {
                _UserIds = value;
            }
            get
            {
                return _UserIds;
            }
        }

        public string JudgeIds
        {
            set
            {
                _JudgeIds = value;
            }
            get
            {
                return _JudgeIds;
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
