using System;

namespace RailExam.Model
{
    public class TaskResult
    {
        #region // Private Memebers

        private int _taskResultId = 0;
        private int _trainTypeId = 0;
        private int _paperId = 0;
        private int _employeeId = 0;
        private DateTime _beginDateTime;
        private DateTime _currentDateTime;
        private DateTime _endDateTime;
        private int _usedTime = 0;
        private decimal _autoScore = (decimal)0.0;
        private decimal _score = (decimal)0.0;
        private int _judgeId = 0;
        private DateTime _judgeBeginTime;
        private DateTime _judgeEndTime;
        private decimal _correctRate = (decimal)0.0;
        private bool _isPass = false;
        private int _statusId = 0;
        private string _memo = string.Empty;
        
        #endregion // End of Private Members

        #region // Extended Members

        private string _trainTypeName = string.Empty;
        private string _paperName = string.Empty;
        private string _employeeName = string.Empty;
        private string _organizationName = string.Empty;
        private string _judgeName = string.Empty;
        private string _statusName = string.Empty;
        
        #endregion // End of Extended Members

        #region // Properties

        /// <summary>
        /// 培训目标作业成绩ID
        /// </summary>
        public int TaskResultId
        {
            get { return _taskResultId; }
            set { _taskResultId = value; }
        }

        /// <summary>
        /// 培训ID
        /// </summary>
        public int TrainTypeId
        {
            get { return _trainTypeId; }
            set { _trainTypeId = value; }
        }

        /// <summary>
        /// 试卷ID
        /// </summary>
        public int PaperId
        {
            get { return _paperId; }
            set { _paperId = value; }
        }

        /// <summary>
        /// 作业人ID
        /// </summary>
        public int EmployeeId
        {
            get { return _employeeId; }
            set { _employeeId = value; }
        }

        /// <summary>
        /// 作业开始时间
        /// </summary>
        public DateTime BeginTime
        {
            get { return _beginDateTime; }
            set { _beginDateTime = value; }
        }

        /// <summary>
        /// 考试当前时间
        /// </summary>
        public DateTime CurrentTime
        {
            get { return _currentDateTime; }
            set { _currentDateTime = value; }
        }

        /// <summary>
        /// 考试结束时间
        /// </summary>
        public DateTime EndTime
        {
            get { return _endDateTime; }
            set { _endDateTime = value; }
        }

        /// <summary>
        /// 考试花费时间，秒
        /// </summary>
        public int UsedTime
        {
            get { return _usedTime; }
            set { _usedTime = value; }
        }

        /// <summary>
        /// 自动评分分数
        /// </summary>
        public decimal AutoScore
        {
            get { return _autoScore; }
            set { _autoScore = value; }
        }

        /// <summary>
        /// 评分分数
        /// </summary>
        public decimal Score
        {
            get { return _score; }
            set { _score = value; }
        }

        /// <summary>
        /// 评分人ID
        /// </summary>
        public int JudgeId
        {
            get { return _judgeId; }
            set { _judgeId = value; }
        }

        /// <summary>
        /// 评分开始时间
        /// </summary>
        public DateTime JudgeBeginTime
        {
            get { return _judgeBeginTime; }
            set { _judgeBeginTime = value; }
        }

        /// <summary>
        /// 评分结束时间
        /// </summary>
        public DateTime JudgeEndTime
        {
            get { return _judgeEndTime; }
            set { _judgeEndTime = value; }
        }

        /// <summary>
        /// 正确率（百分比）
        /// </summary>
        public decimal CorrectRate
        {
            get { return _correctRate; }
            set { _correctRate = value; }
        }

        /// <summary>
        /// 是否通过考试
        /// </summary>
        public bool IsPass
        {
            get { return _isPass; }
            set { _isPass = value; }
        }

        /// <summary>
        /// 考试考生结果状态ID
        /// </summary>
        public int StatusId
        {
            get { return _statusId; }
            set { _statusId = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        #endregion // End of Properties

        #region // Extended Properties

        /// <summary>
        /// 培训类别
        /// </summary>
        public string TrainTypeName
        {
            get { return _trainTypeName; }
            set { _trainTypeName = value; }
        }

        /// <summary>
        /// 作业试卷
        /// </summary>
        public string PaperName
        {
            get { return _paperName; }
            set { _paperName = value; }
        }

        /// <summary>
        /// 作业人
        /// </summary>
        public string EmployeeName
        {
            get { return this._employeeName; }
            set { this._employeeName = value; }
        }

        /// <summary>
        /// 作业人单位
        /// </summary>
        public string OrganizationName
        {
            get { return _organizationName; }
            set { _organizationName = value; }
        }

        /// <summary>
        /// 评分人
        /// </summary>
        public string JudgeName
        {
            get { return _judgeName; }
            set { _judgeName = value; }
        }

        /// <summary>
        /// 作业人结果状态
        /// </summary>
        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        /// <summary>
        /// 作业时长的时间表示
        /// </summary>
        public string UsedTimeString
        {
            get { return DSunSoft.Common.CommonTool.ConvertSecondsToTimeString(_usedTime); }
            set { _usedTime = DSunSoft.Common.CommonTool.ConvertTimeStringToSeconds(value); }
        }

        #endregion // End of Extended Properties

        #region // Ctors

        public TaskResult() { }

        public TaskResult(int? TaskResultId, int? TrainTypeId, int? paperId, int? EmployeeId,
            DateTime? BeginTime, DateTime? CurrentTime, DateTime? EndTime, int? UsedTime,
            decimal? autoScore, decimal? score, int? judgeId, DateTime? JudgeBeginTime,
            DateTime? JudgeEndTime, decimal? correctRate, bool? isPass, int? statusId, string memo)
        {
            _taskResultId = TaskResultId ?? _taskResultId;
            _trainTypeId = TrainTypeId ?? _trainTypeId;
            _paperId = paperId ?? _paperId;
            _employeeId = EmployeeId ?? _employeeId;
            _beginDateTime = BeginTime ?? _beginDateTime;
            _currentDateTime = CurrentTime ?? _currentDateTime;
            _endDateTime = EndTime ?? _endDateTime;
            _usedTime = UsedTime ?? _usedTime;
            _autoScore = autoScore ?? _autoScore;
            _score = score ?? _score;
            _judgeId = judgeId ?? _judgeId;
            _judgeBeginTime = JudgeBeginTime ?? _judgeBeginTime;
            _judgeEndTime = JudgeEndTime ?? _judgeEndTime;
            _correctRate = correctRate ?? _correctRate;
            _isPass = isPass ?? _isPass;
            _statusId = statusId ?? _statusId;
            _memo = memo;
        }

        #endregion // End of Ctors
    }
}
