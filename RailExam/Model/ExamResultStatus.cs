namespace RailExam.Model
{
    public class ExamResultStatus
    {
        private int _examResultStatusId = 0;
        private string _statusName = string.Empty;
        private string _description = string.Empty;
        private bool _isDefault = false;
        private string _memo = string.Empty;

        /// <summary>
        /// 考试考生结果状态ID
        /// </summary>
        public int ExamResultStatusId
        {
            get { return _examResultStatusId; }
            set { _examResultStatusId = value; }
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
        public ExamResultStatus() { }

        /// <summary>
        /// 全参数构造函数
        /// </summary>
        /// <param name="examResultStatusId"></param>
        /// <param name="statusName"></param>
        /// <param name="description"></param>
        /// <param name="isDefault"></param>
        /// <param name="memo"></param>
        public ExamResultStatus(int? examResultStatusId, string statusName,
            string description, bool? isDefault, string memo)
        {
            _examResultStatusId = examResultStatusId ?? _examResultStatusId;
            _statusName = statusName;
            _description = description;
            _isDefault = isDefault ?? _isDefault;
            _memo = memo;
        }
    }
}
