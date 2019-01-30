namespace RailExam.Model
{
    /// <summary>
    /// 业务实体：试题状态
    /// </summary>
    public class ItemStatus
    {
        /// <summary>
        /// 试题状态内部成员
        /// </summary>
        private int _itemStatusId = 0;
        private string _statusName = string.Empty;
        private string _description = string.Empty;
        private int _isDefault = 0;
        private string _memo;

        /// <summary>
        /// 试题状态ID属性
        /// </summary>
        public int ItemStatusId
        {
            get { return _itemStatusId; }
            set { _itemStatusId = value; }
        }

        /// <summary>
        /// 试题状态状态名称
        /// </summary>
        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        /// <summary>
        /// 试题状态描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// 试题状态是否缺省状态
        /// </summary>
        public int IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        /// <summary>
        /// 试题状态备注
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        /// <summary>
        /// 试题状态空构造函数
        /// </summary>
        public ItemStatus() { }

        /// <summary>
        /// 试题状态全参数构造函数
        /// </summary>
        /// <param name="itemStatusId">试题状态ID</param>
        /// <param name="statusName">试题状态名称</param>
        /// <param name="description">试题状态描述</param>
        /// <param name="isDefault">是否缺省</param>
        /// <param name="memo">试题状态备注</param>
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