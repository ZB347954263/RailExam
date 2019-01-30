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
        /// ���Կ������״̬ID
        /// </summary>
        public int ExamResultStatusId
        {
            get { return _examResultStatusId; }
            set { _examResultStatusId = value; }
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
        public ExamResultStatus() { }

        /// <summary>
        /// ȫ�������캯��
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
