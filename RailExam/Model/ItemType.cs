namespace RailExam.Model
{
    /// <summary>
    /// 业务实体：试题类别
    /// </summary>
    public class ItemType
    {
        /// <summary>
        /// 试题类别内部成员
        /// </summary>
        private int _itemTypeId = 0;

        private string _typeName = string.Empty;
        private string _imageFileName = string.Empty;
        private string _description = string.Empty;
        private int _isDefault = 0;
        private int _isValid = 0;
        private string _memo = string.Empty;

        /// <summary>
        /// 试题类别的类别ID
        /// </summary>
        public int ItemTypeId
        {
            get { return _itemTypeId; }
            set { _itemTypeId = value; }
        }

        /// <summary>
        /// 试题类别的类别名称
        /// </summary>
        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        /// <summary>
        /// 试题类别的图片名称
        /// </summary>
        public string ImageFileName
        {
            get { return _imageFileName; }
            set { _imageFileName = value; }
        }

        /// <summary>
        /// 试题类别的描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// 试题类别是否缺省值
        /// </summary>
        public int IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        /// <summary>
        /// 是否显示该试题类别
        /// </summary>
        public int IsValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }

        /// <summary>
        /// 试题类别的备注
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }
        
        /// <summary>
        /// 空构造函数
        /// </summary>
        public ItemType() { }

        /// <summary>
        /// 全参数构造函数
        /// </summary>
        /// <param name="itemTypeId">试题类别ID</param>
        /// <param name="typeName">试题类别名称</param>
        /// <param name="imageFileName">图片名称</param>
        /// <param name="description">试题类别描述</param>
        /// <param name="isDefault">是否缺省试题类别</param>
        /// <param name="memo">试题类别备注</param>
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