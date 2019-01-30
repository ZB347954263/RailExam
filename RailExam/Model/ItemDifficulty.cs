namespace RailExam.Model
{
    /// <summary>
    /// 业务实体：试题难度
    /// </summary>
    public class ItemDifficulty
    {
        /// <summary>
        /// 试题难度内部成员
        /// </summary>
        private int _itemDifficultyId = 0;
        private string _difficultyName = string.Empty;
        private int _difficultyValue = 0;
        private string _description = string.Empty;
        private int _isDefault = 0;
        private string _memo = string.Empty;

        /// <summary>
        /// 试题难度ID
        /// </summary>
        public int ItemDifficultyId
        {
            get { return _itemDifficultyId; }
            set { _itemDifficultyId = value; }
        }

        /// <summary>
        /// 试题难度名称
        /// </summary>
        public string DifficultyName
        {
            get { return _difficultyName; }
            set { _difficultyName = value; }
        }

        /// <summary>
        /// 试题难度权值
        /// </summary>
        public int DifficultyValue
        {
            get { return _difficultyValue; }
            set { _difficultyValue = value; }
        }

        /// <summary>
        /// 试题难度描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// 试题难度是否缺省难度
        /// </summary>
        public int IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        /// <summary>
        /// 试题难度备注
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        /// <summary>
        /// 试题难度空构造函数
        /// </summary>
        public ItemDifficulty() { }

        /// <summary>
        /// 试题难度全参数构造函数
        /// </summary>
        /// <param name="itemDifficultyId">试题难度ID</param>
        /// <param name="difficultyName">试题难度名称</param>
        /// <param name="difficultyValue">试题难度权值</param>
        /// <param name="description">试题难度描述</param>
        /// <param name="memo">试题难度备注</param>
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