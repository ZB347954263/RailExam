using System;

namespace RailExam.Model
{
    public class RandomExamResult
    {
        #region 内部成员

        private int _randomExamResultId = 0;
        private int _organizationId = 0;
        private string _organizationName = string.Empty;
        private int _randomExamId = 0;
        private string _examName = string.Empty;
        private int _examineeId = 0;
        private string _examineeName = string.Empty;
        private DateTime _beginDateTime;
        private DateTime _currentDateTime;
        private DateTime _endDateTime;
        private int _examTime = 0;
        private decimal _autoScore = (decimal)0.0;
        private decimal _score = (decimal)0.0;
        private decimal _correctRate = (decimal)0.0;
        private bool _isPass = false;
        private int _statusId = 0;
        private string _statusName = string.Empty;
        private string _memo = string.Empty;   
        private string _workNo = string.Empty;
        private string _postName = string.Empty;
        private int _randomExamResultIdStation = 0;
    	private string _examOrgName = string.Empty;
    	private string _examStyleName = string.Empty;
        private string _examTimeName = string.Empty;
        private int _isTemp = 0;

        #endregion

        public int IsTemp
        {
            get { return _isTemp; }
            set { _isTemp = value; }
        }

    	public string ExamStyleName
    	{
			get { return _examStyleName; }
			set { _examStyleName = value; }
    	}

        /// <summary>
        /// 考试考生结果ID
        /// </summary>
        public int RandomExamResultId
        {
            get { return _randomExamResultId; }
            set { _randomExamResultId = value; }
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
        public int RandomExamId
        {
            get { return _randomExamId; }
            set { _randomExamId = value; }
        }

        /// <summary>
        /// 考试
        /// </summary>
        public string ExamName
        {
            get { return _examName; }
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
        public int RandomExamResultIDStation
        {
            get { return _randomExamResultIdStation; }
            set { _randomExamResultIdStation = value;}
        }

    	public string ExamOrgName
    	{
			get { return _examOrgName; }
			set { _examOrgName = value; }
    	}

        public string ExamTimeName
        {
            get { return _examTimeName; }
            set { _examTimeName = value; }
        }

        /// <summary>
        /// 空参数构造函数
        /// </summary>
        public RandomExamResult() { }

        public RandomExamResult(int? randomExamResultId, int? organizationId, string organizationName, int? randomExamId,
            string examName, int? examineeId, string examineeName,
            DateTime? beginDateTime, DateTime? currentDateTime, DateTime? endDateTime, int? examTime,
            decimal? autoScore, decimal? score, decimal? correctRate, bool? isPass, int? statusId, string statusName, string memo,int?  randomExamResultIDStation)
        {
            _randomExamResultId = randomExamResultId ?? _randomExamResultId;
            _organizationId = organizationId ?? _organizationId;
            _organizationName = organizationName;
            _randomExamId = randomExamId ?? _randomExamId;
            _examName = examName;
            _examineeId = examineeId ?? _examineeId;
            _examineeName = examineeName;
            _beginDateTime = beginDateTime ?? _beginDateTime;
            _currentDateTime = currentDateTime ?? _currentDateTime;
            _endDateTime = endDateTime ?? _endDateTime;
            _examTime = examTime ?? _examTime;
            _autoScore = autoScore ?? _autoScore;
            _score = score ?? _score;
            _correctRate = correctRate ?? _correctRate;
            _isPass = isPass ?? _isPass;
            _statusId = statusId ?? _statusId;
            _statusName = statusName;
            _memo = memo;
            _randomExamResultIdStation = randomExamResultIDStation ?? _randomExamResultIdStation;
        }
    }
}

