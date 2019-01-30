namespace RailExam.Model
{
    /// <summary>
    /// ҵ��ʵ�壺�������
    /// </summary>
    public class ItemType
    {
        /// <summary>
        /// ��������ڲ���Ա
        /// </summary>
        private int _itemTypeId = 0;

        private string _typeName = string.Empty;
        private string _imageFileName = string.Empty;
        private string _description = string.Empty;
        private int _isDefault = 0;
        private int _isValid = 0;
        private string _memo = string.Empty;

        /// <summary>
        /// �����������ID
        /// </summary>
        public int ItemTypeId
        {
            get { return _itemTypeId; }
            set { _itemTypeId = value; }
        }

        /// <summary>
        /// ���������������
        /// </summary>
        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        /// <summary>
        /// ��������ͼƬ����
        /// </summary>
        public string ImageFileName
        {
            get { return _imageFileName; }
            set { _imageFileName = value; }
        }

        /// <summary>
        /// ������������
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// ��������Ƿ�ȱʡֵ
        /// </summary>
        public int IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        /// <summary>
        /// �Ƿ���ʾ���������
        /// </summary>
        public int IsValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }

        /// <summary>
        /// �������ı�ע
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }
        
        /// <summary>
        /// �չ��캯��
        /// </summary>
        public ItemType() { }

        /// <summary>
        /// ȫ�������캯��
        /// </summary>
        /// <param name="itemTypeId">�������ID</param>
        /// <param name="typeName">�����������</param>
        /// <param name="imageFileName">ͼƬ����</param>
        /// <param name="description">�����������</param>
        /// <param name="isDefault">�Ƿ�ȱʡ�������</param>
        /// <param name="memo">�������ע</param>
        public ItemType(int? itemTypeId, string typeName,
                        string imageFileName, string description, int? isDefault, int? isValid, string memo)
        {
            _itemTypeId = itemTypeId ?? _itemTypeId;
            _typeName = typeName;
            _imageFileName = imageFileName;
            _description = description;
            _isDefault = isDefault ?? _isDefault;
            _isValid = isValid ?? _isValid;
            _memo = memo;
        }
    }
}