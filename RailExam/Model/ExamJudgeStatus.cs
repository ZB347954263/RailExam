namespace RailExam.Model
{
    /// <summary>
    /// ʵ�壺��������״̬
    /// </summary>
    public class ExamJudgeStatus
    {
        /// <summary>
        /// ʵ���ڲ���Ա
        /// </summary>
        private int _examJudgeStatusId = 0;
        private string _statusName = string.Empty;
        private string _description = string.Empty;
        private bool _isDefault = false;
        private decimal _scoreRate = 0.00M;
        private string _memo = string.Empty;

        /// <summary>
        /// ��������״̬ID
        /// </summary>
        public int ExamJudgeStatusId
        {
            get { return _examJudgeStatusId; }
            set { _examJudgeStatusId = value; }
        }

        /// <summary>
        /// ״̬����
        /// </summary>
        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// �Ƿ�ȱʡ
        /// </summary>
        public bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        /// <summary>
        /// �÷ֱ���
        /// </summary>
        public decimal ScoreRate
        {
            get { return _scoreRate; }
            set { _scoreRate = value; }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        /// <summary>
        /// �ղ������캯��
        /// </summary>
        public ExamJudgeStatus() { }

        /// <summary>
        /// ȫ�������캯��
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
