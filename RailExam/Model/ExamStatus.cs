namespace RailExam.Model
{
    public class ExamStatus
    {
        private int _examStatusId = 0;
        private string _statusName = string.Empty;
        private string _description = string.Empty;
        private bool _isDefault = false;
        private string _memo = string.Empty;

        /// <summary>
        /// 考试状态ID
        /// </summary>
        public int ExamStatusId
        {
            get { return _examStatusId; }
            set { _examStatusId = value; }
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
        public ExamStatus() { }

        /// <summary>
        /// 全参数构造函数
        /// </summary>
        /// <param name="examStatusId"></param>
        /// <param name="statusName"></param>
        /// <param name="description"></param>
        /// <param name="isDefault"></param>
        /// <param name="memo"></param>
        public ExamStatus(int? examStatusId, string statusName, 
            string description, bool? isDefault, string memo)
        {
            _examStatusId = examStatusId ?? _examStatusId;
            _statusName = statusName;
            _description = description;
            _isDefault = isDefault ?? _isDefault;
            _memo = memo;
        }
    }
}
