namespace RailExam.Model
{
    /// <summary>
    /// 业务实体：试题分类
    /// </summary>
    public class ItemCategory
    {
        /// <summary>
        /// 试题分类内部成员
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
        /// 试题分类ID
        /// </summary>
        public int ItemCategoryId
        {
            get { return _itemCategoryId; }
            set { _itemCategoryId = value; }
        }

        /// <summary>
        /// 试题分类的父分类ID
        /// </summary>
        public int ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        /// <summary>
        /// 试题分类的树访问路径
        /// </summary>
        public string IdPath
        {
            get { return _idPath; }
            set { _idPath = value; }
        }

        /// <summary>
        /// 试题分类的层数
        /// </summary>
        public int LevelNum
        {
            get { return _levelNum; }
            set { _levelNum = value; }
        }

        /// <summary>
        /// 试题分类的序号
        /// </summary>
        public int OrderIndex
        {
            get { return _orderIndex; }
            set { _orderIndex = value; }
        }

        /// <summary>
        /// 试题分类的分类名称
        /// </summary>
        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }

        /// <summary>
        /// 试题分类的描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// 试题分类的备注
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        /// <summary>
        /// 试题分类空构造函数
        /// </summary>
        public ItemCategory() { }

        /// <summary>
        /// 试题分类全参数构造函数
        /// </summary>
        /// <param name="itemCategoryId">试题分类ID</param>
        /// <param name="parentId">试题分类父ID</param>
        /// <param name="idPath">试题分类树访问路径</param>
        /// <param name="levelNum">试题分类树层数</param>
        /// <param name="orderIndex">试题分类序号</param>
        /// <param name="categoryName">试题分类名称</param>
        /// <param name="description">试题分类描述</param>
        /// <param name="memo">试题分类备注</param>
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