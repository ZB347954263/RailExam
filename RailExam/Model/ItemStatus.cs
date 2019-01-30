namespace RailExam.Model
{
    /// <summary>
    /// ҵ��ʵ�壺����״̬
    /// </summary>
    public class ItemStatus
    {
        /// <summary>
        /// ����״̬�ڲ���Ա
        /// </summary>
        private int _itemStatusId = 0;
        private string _statusName = string.Empty;
        private string _description = string.Empty;
        private int _isDefault = 0;
        private string _memo;

        /// <summary>
        /// ����״̬ID����
        /// </summary>
        public int ItemStatusId
        {
            get { return _itemStatusId; }
            set { _itemStatusId = value; }
        }

        /// <summary>
        /// ����״̬״̬����
        /// </summary>
        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        /// <summary>
        /// ����״̬����
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// ����״̬�Ƿ�ȱʡ״̬
        /// </summary>
        public int IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        /// <summary>
        /// ����״̬��ע
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        /// <summary>
        /// ����״̬�չ��캯��
        /// </summary>
        public ItemStatus() { }

        /// <summary>
        /// ����״̬ȫ�������캯��
        /// </summary>
        /// <param name="itemStatusId">����״̬ID</param>
        /// <param name="statusName">����״̬����</param>
        /// <param name="description">����״̬����</param>
        /// <param name="isDefault">�Ƿ�ȱʡ</param>
        /// <param name="memo">����״̬��ע</param>
        public ItemStatus(int? itemStatusId, string statusName,
                          string description, int? isDefault, string memo)
        {
            _itemStatusId = itemStatusId ?? _itemStatusId;
            _statusName = statusName;
            _description = description;
            _isDefault = isDefault ?? _isDefault;
            _memo = memo;
        }
    }
}