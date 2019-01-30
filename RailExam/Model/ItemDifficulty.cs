namespace RailExam.Model
{
    /// <summary>
    /// ҵ��ʵ�壺�����Ѷ�
    /// </summary>
    public class ItemDifficulty
    {
        /// <summary>
        /// �����Ѷ��ڲ���Ա
        /// </summary>
        private int _itemDifficultyId = 0;
        private string _difficultyName = string.Empty;
        private int _difficultyValue = 0;
        private string _description = string.Empty;
        private int _isDefault = 0;
        private string _memo = string.Empty;

        /// <summary>
        /// �����Ѷ�ID
        /// </summary>
        public int ItemDifficultyId
        {
            get { return _itemDifficultyId; }
            set { _itemDifficultyId = value; }
        }

        /// <summary>
        /// �����Ѷ�����
        /// </summary>
        public string DifficultyName
        {
            get { return _difficultyName; }
            set { _difficultyName = value; }
        }

        /// <summary>
        /// �����Ѷ�Ȩֵ
        /// </summary>
        public int DifficultyValue
        {
            get { return _difficultyValue; }
            set { _difficultyValue = value; }
        }

        /// <summary>
        /// �����Ѷ�����
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// �����Ѷ��Ƿ�ȱʡ�Ѷ�
        /// </summary>
        public int IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        /// <summary>
        /// �����Ѷȱ�ע
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        /// <summary>
        /// �����Ѷȿչ��캯��
        /// </summary>
        public ItemDifficulty() { }

        /// <summary>
        /// �����Ѷ�ȫ�������캯��
        /// </summary>
        /// <param name="itemDifficultyId">�����Ѷ�ID</param>
        /// <param name="difficultyName">�����Ѷ�����</param>
        /// <param name="difficultyValue">�����Ѷ�Ȩֵ</param>
        /// <param name="description">�����Ѷ�����</param>
        /// <param name="memo">�����Ѷȱ�ע</param>
        public ItemDifficulty(int? itemDifficultyId, string difficultyName,
                              int? difficultyValue, string description, int? isDefault, string memo)
        {
            _itemDifficultyId = itemDifficultyId ?? _itemDifficultyId;
            _difficultyName = difficultyName;
            _difficultyValue = difficultyValue ?? _difficultyValue;
            _description = description;
            _isDefault = isDefault ?? _isDefault;
            _memo = memo;
        }
    }
}