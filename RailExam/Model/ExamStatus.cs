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
        /// ����״̬ID
        /// </summary>
        public int ExamStatusId
        {
            get { return _examStatusId; }
            set { _examStatusId = value; }
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
        public ExamStatus() { }

        /// <summary>
        /// ȫ�������캯��
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
