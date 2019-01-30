namespace RailExam.Model
{
    /// <summary>
    /// ҵ��ʵ�壺�������
    /// </summary>
    public class ItemCategory
    {
        /// <summary>
        /// ��������ڲ���Ա
        /// </summary>
        private int _itemCategoryId = 0;
        private int _parentId = 0;
        private string _idPath = string.Empty;
        private int _levelNum = 0;
        private int _orderIndex = 0;
        private string _categoryName = string.Empty;
        private string _description = string.Empty;
        private string _memo = string.Empty;

        /// <summary>
        /// �������ID
        /// </summary>
        public int ItemCategoryId
        {
            get { return _itemCategoryId; }
            set { _itemCategoryId = value; }
        }

        /// <summary>
        /// �������ĸ�����ID
        /// </summary>
        public int ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        /// <summary>
        /// ��������������·��
        /// </summary>
        public string IdPath
        {
            get { return _idPath; }
            set { _idPath = value; }
        }

        /// <summary>
        /// �������Ĳ���
        /// </summary>
        public int LevelNum
        {
            get { return _levelNum; }
            set { _levelNum = value; }
        }

        /// <summary>
        /// �����������
        /// </summary>
        public int OrderIndex
        {
            get { return _orderIndex; }
            set { _orderIndex = value; }
        }

        /// <summary>
        /// �������ķ�������
        /// </summary>
        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
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
        /// �������ı�ע
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        /// <summary>
        /// �������չ��캯��
        /// </summary>
        public ItemCategory() { }

        /// <summary>
        /// �������ȫ�������캯��
        /// </summary>
        /// <param name="itemCategoryId">�������ID</param>
        /// <param name="parentId">������ุID</param>
        /// <param name="idPath">�������������·��</param>
        /// <param name="levelNum">�������������</param>
        /// <param name="orderIndex">����������</param>
        /// <param name="categoryName">�����������</param>
        /// <param name="description">�����������</param>
        /// <param name="memo">������౸ע</param>
        public ItemCategory(int? itemCategoryId, int? parentId, string idPath, int? levelNum, 
                            int? orderIndex, string categoryName, string description, string memo)
        {
            _itemCategoryId = itemCategoryId ?? _itemCategoryId;
            _parentId = parentId ?? _parentId;
            _idPath = idPath;
            _levelNum = levelNum ?? _levelNum;
            _orderIndex = orderIndex ?? _orderIndex;
            _categoryName = categoryName;
            _description = description;
            _memo = memo;
        }
    }
}