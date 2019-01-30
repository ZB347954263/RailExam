using System;

namespace RailExam.Model
{
    public class ExamResult
    {
        #region 内部成员

        private int _examResultId = 0;
        private int _organizationId = 0;
        private string _organizationName = string.Empty;
        private int _examId = 0;
        private string _examName = string.Empty;
        private int _paperId = 0;
        private string _paperName = string.Empty;
        private int _examineeId = 0;
        private string _examineeName = string.Empty;
        private DateTime _beginDateTime;
        private DateTime _currentDateTime;
        private DateTime _endDateTime;
        private int _examTime = 0;
        private decimal _autoScore = (decimal)0.0;
        private decimal _score = (decimal)0.0;
        private int _judgeId = 0;
        private string _judgeName = string.Empty;
        private DateTime _judgeBeginDateTime;
        private DateTime _judgeEndDateTime;
        private decimal _correctRate = (decimal)0.0;
        private bool _isPass = false;
        private int _statusId = 0;
        private string _statusName = string.Empty;
        private string _memo = string.Empty;
        private DateTime _examBeginTime = DateTime.MinValue;
        private DateTime _examEndTime = DateTime.MinValue;
        private string _workNo = string.Empty;
        private string _postName = string.Empty;
        private int _examType = 0;
        private int _examResultIDStation = 0;

        #endregion


        /// <summary>
        /// 考试考生结果ID
        /// </summary>
        public int ExamResultId
        {
            get { return _examResultId; }
            set { _examResultId = value; }
        }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public int OrganizationId
        {
            get { return _organizationId; }
            set { _organizationId = value; }
        }

        /// <summary>
        /// 组织机构
        /// </summary>
        public string OrganizationName
        {
            get { return _organizationName; }
        }

        /// <summary>
        /// 考试ID
        /// </summary>
        public int ExamId
        {
            get { return _examId; }
            set { _examId = value; }
        }

        /// <summary>
        /// 考试
        /// </summary>
        public string ExamName
        {
            get { return _examName; }
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
        /// 试卷
        /// </summary>
        public string PaperName
        {
            get { return _paperName; }
        }

        /// <summary>
        /// 考生ID
        /// </summary>
        public int ExamineeId
        {
            get { return _examineeId; }
            set { _examineeId = value; }
        }

        /// <summary>
        /// 考生
        /// </summary>
        public string ExamineeName
        {
            get { return _examineeName; }
        }

        /// <summary>
        /// 考试开始时间
        /// </summary>
        public DateTime BeginDateTime
        {
            get { return _beginDateTime; }
            set { _beginDateTime = value; }
        }

        /// <summary>
        /// 考试当前时间
        /// </summary>
        public DateTime CurrentDateTime
        {
            get { return _currentDateTime; }
            set { _currentDateTime = value; }
        }

        /// <summary>
        /// 考试结束时间
        /// </summary>
        public DateTime EndDateTime
        {
            get { return _endDateTime; }
            set { _endDateTime = value; }
        }

        /// <summary>
        /// 考试花费时间，秒
        /// </summary>
        public int ExamTime
        {
            get { return _examTime; }
            set { _examTime = value; }
        }

        public string ExamTimeString
        {
            get { return DSunSoft.Common.CommonTool.ConvertSecondsToTimeString(_examTime); }
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
        /// 评分人
        /// </summary>
        public string JudgeName
        {
            get { return _judgeName; }
        }

        /// <summary>
        /// 评分开始时间
        /// </summary>
        public DateTime JudgeBeginDateTime
        {
            get { return _judgeBeginDateTime; }
            set { _judgeBeginDateTime = value; }
        }

        /// <summary>
        /// 评分结束时间
        /// </summary>
        public DateTime JudgeEndDateTime
        {
            get { return _judgeEndDateTime; }
            set { _judgeEndDateTime = value; }
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

        public string StatusName
        {
            get { return _statusName; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        /// <summary>
        /// 考试的开始时间
        /// </summary>
        public DateTime ExamBeginTime
        {
            get { return _examBeginTime; }
            set { _examBeginTime = value; }
        }

        /// <summary>
        /// 开始的结束时间
        /// </summary>
        public DateTime ExamEndTime
        {
            get { return _examEndTime; }
            set { _examEndTime = value; }
        }

        public string WorkNo
        {
            get { return _workNo; }
            set { _workNo = value; }
        }

        public string PostName
        {
            get { return _postName; }
            set { _postName = value; }
        }

        public int ExamType
        {
            get { return _examType; }
            set { _examType = value; }
        }

        public int ExamResultIDStation
        {
            get { return _examResultIDStation; }
            set { _examResultIDStation = value; }
        }

        /// <summary>
        /// 空参数构造函数
        /// </summary>
        public ExamResult() { }

        /// <summary>
        /// 全参数构造函数
        /// </summary>
        /// <param name="examResultId"></param>
        /// <param name="organizationId"></param>
        /// <param name="organizationName"></param>
        /// <param name="examId"></param>
        /// <param name="examName"></param>
        /// <param name="paperId"></param>
        /// <param name="paperName"></param>
        /// <param name="examineeId"></param>
        /// <param name="examineeName"></param>
        /// <param name="beginDateTime"></param>
        /// <param name="currentDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="examTime"></param>
        /// <param name="autoScore"></param>
        /// <param name="score"></param>
        /// <param name="judgeId"></param>
        /// <param name="judgeName"></param>
        /// <param name="judgeBeginDateTime"></param>
        /// <param name="judgeEndDateTime"></param>
        /// <param name="correctRate"></param>
        /// <param name="isPass"></param>
        /// <param name="statusId"></param>
        /// <param name="statusName"></param>
        /// <param name="memo"></param>
        public ExamResult(int? examResultId, int? organizationId, string organizationName, int? examId,
            string examName, int? paperId, string paperName, int? examineeId, string examineeName,
            DateTime? beginDateTime, DateTime? currentDateTime, DateTime? endDateTime, int? examTime,
            decimal? autoScore, decimal? score, int? judgeId, string judgeName, DateTime? judgeBeginDateTime,
            DateTime? judgeEndDateTime, decimal? correctRate, bool? isPass, int? statusId, string statusName, string memo,int? examResultIDStation)
        {
            _examResultId = examResultId ?? _examResultId;
            _organizationId = organizationId ?? _organizationId;
            _organizationName = organizationName;
            _examId = examId ?? _examId;
            _examName = examName;
            _paperId = paperId ?? _paperId;
            _paperName = paperName;
            _examineeId = examineeId ?? _examineeId;
            _examineeName = examineeName;
            _beginDateTime = beginDateTime ?? _beginDateTime;
            _currentDateTime = currentDateTime ?? _currentDateTime;
            _endDateTime = endDateTime ?? _endDateTime;
            _examTime = examTime ?? _examTime;
            _autoScore = autoScore ?? _autoScore;
            _score = score ?? _score;
            _judgeId = judgeId ?? _judgeId;
            _judgeName = judgeName;
            _judgeBeginDateTime = judgeBeginDateTime ?? _judgeBeginDateTime;
            _judgeEndDateTime = judgeEndDateTime ?? _judgeEndDateTime;
            _correctRate = correctRate ?? _correctRate;
            _isPass = isPass ?? _isPass;
            _statusId = statusId ?? _statusId;
            _statusName = statusName;
            _memo = memo;
            _examResultIDStation = examResultIDStation ?? _examResultIDStation;
        }
    }
}
