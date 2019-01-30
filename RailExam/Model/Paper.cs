using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace RailExam.Model
{
    public class Paper
    {
        private int _PaperId = 0;
        private int _OrgId = 0;
        private int _CategoryId = 0;
        private string _PaperName = string.Empty;
        private int _CreateMode = 0;
        private int _StrategyId = 0;
        private string _CategoryName = string.Empty;
        private string _description = string.Empty;
        private decimal _TotalScore = 0;
        private int _ItemCount = 0;
        private int _StatusId = 0;
        private int _UsedCount = 0;
        private string _CreatePerson = string.Empty;
        private DateTime _CreateTime = DateTime.Now;
        private string _memo = string.Empty;
        private bool _flag = false;
        private string _stationName = "";

        #region // Extended Members

        private string _organizationName = string.Empty;
        private string _statusName = string.Empty;
        private string _taskTrainTypeName = string.Empty;
        private int _currentItemCount = 0;
        private decimal _currentTotalScore = 0;

        #endregion // End of Extended Members

        public Paper() { }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        public Paper(
            int? PaperId,
            int? OrgId,
            int? CategoryId,
            string PaperName,
            int? CreateMode,
            int? StrategyId,
            string CategoryName,
            string description,
            string memo,
            decimal? TotalScore,
            int? ItemCount,
            int? UsedCount,
            int? StatusId,
            string CreatePerson,
            DateTime? CreateTime)
        {
            _PaperId = PaperId ?? _PaperId;
            _OrgId = OrgId ?? _OrgId;
            _PaperName = PaperName;
            _CreateMode = CreateMode ?? _CreateMode;
            _StrategyId = StrategyId ?? _StrategyId;
            _CategoryName = CategoryName;
            _description = description;
            _memo = memo;
            _TotalScore = TotalScore ?? _TotalScore;
            _ItemCount = ItemCount ?? _ItemCount;
            _UsedCount = UsedCount ?? _UsedCount;
            _StatusId = StatusId ?? _StatusId;
            _CategoryId = CategoryId ?? _CategoryId;
            _CreatePerson = CreatePerson;
            _CreateTime = CreateTime ?? _CreateTime;
        }

        public DateTime CreateTime
        {
            set
            {
                _CreateTime = value;
            }
            get
            {
                return _CreateTime;
            }
        }

        public string CreatePerson
        {
            set
            {
                _CreatePerson = value;
            }
            get
            {
                return _CreatePerson;
            }
        }

        public int StatusId
        {
            set
            {
                _StatusId = value;
            }
            get
            {
                return _StatusId;
            }
        }

        public int UsedCount
        {
            set 
            {
                _UsedCount = value;
            }
            get
            {
                return _UsedCount;
            }
        }

        public int ItemCount
        {
            set
            {
                _ItemCount = value;
            }
            get
            {
                return _ItemCount;
            }
        }

        public int CategoryId
        {
            set
            {
                _CategoryId = value;
            }
            get
            {
                return _CategoryId;
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

        public int OrgId
        {
            set
            {
                _OrgId = value;
            }
            get
            {
                return _OrgId;
            }
        }

        public string PaperName
        {
            set
            {
                _PaperName = value;
            }
            get
            {
                return _PaperName;
            }
        }

        public int CreateMode
        {
            set
            {
                _CreateMode = value;
            }
            get
            {
                return _CreateMode;
            }
        }

        public decimal TotalScore
        {
            set
            {
                _TotalScore = value;
            }
            get
            {
                return _TotalScore;
            }
        }

        public int StrategyId
        {
            set
            {
                _StrategyId = value;
            }
            get
            {
                return _StrategyId;
            }
        }

        public string CategoryName
        {
            set
            {
                _CategoryName = value;
            }
            get
            {
                return _CategoryName;
            }
        }

        public string Description
        {
            set
            {
                _description = value;
            }
            get
            {
                return _description;
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

        public bool Flag
        {
            set
            {
                _flag = value;
            }
            get
            {
                return _flag;
            }
        }

        public string StationName
        {
            set
            {
                _stationName = value;
            }
            get
            {
                return _stationName;
            }
        }

        #region // Extended Properties     

        public int CurrentItemCount 
        {
            get { return _currentItemCount; }
            set { _currentItemCount = value; }
        }

        public decimal CurrentTotalScore
        {
            get { return _currentTotalScore; }
            set { _currentTotalScore = value; }
        }

        public string OrganizationName
        {
            get { return _organizationName; }
            set { _organizationName = value; }
        }

        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        public string TaskTrainTypeName
        {
            get { return _taskTrainTypeName; }
            set { _taskTrainTypeName = value; }
        }

        #endregion // End of Extended Properties
    }
}
