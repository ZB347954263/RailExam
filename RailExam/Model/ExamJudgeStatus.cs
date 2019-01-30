namespace RailExam.Model
{
    /// <summary>
    /// 实体：考试评分状态
    /// </summary>
    public class ExamJudgeStatus
    {
        /// <summary>
        /// 实体内部成员
        /// </summary>
        private int _examJudgeStatusId = 0;
        private string _statusName = string.Empty;
        private string _description = string.Empty;
        private bool _isDefault = false;
        private decimal _scoreRate = 0.00M;
        private string _memo = string.Empty;

        /// <summary>
        /// 考试评分状态ID
        /// </summary>
        public int ExamJudgeStatusId
        {
            get { return _examJudgeStatusId; }
            set { _examJudgeStatusId = value; }
        }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// 是否缺省
        /// </summary>
        public bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        /// <summary>
        /// 得分比率
        /// </summary>
        public decimal ScoreRate
        {
            get { return _scoreRate; }
            set { _scoreRate = value; }
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
        /// 空参数构造函数
        /// </summary>
        public ExamJudgeStatus() { }

        /// <summary>
        /// 全参数构造函数
        /// </summary>
        /// <param name="examJudgeStatusId"></param>
        /// <param name="statusName"></param>
        /// <param name="description"></param>
        /// <param name="isDefault"></param>
        /// <param name="memo"></param>
        public ExamJudgeStatus(int? examJudgeStatusId,
            string statusName,
            string description,
            bool? isDefault,
            decimal? scoreRate,
            string memo)
        {
            _examJudgeStatusId = examJudgeStatusId ?? _examJudgeStatusId;
            _statusName = statusName;
            _description = description;
            _isDefault = isDefault ?? _isDefault;
            _scoreRate = scoreRate ?? _scoreRate;
            _memo = memo;
        }
    }
}
