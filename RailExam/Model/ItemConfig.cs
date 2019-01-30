using System;

namespace RailExam.Model
{
    /// <summary>
    /// 业务实体：试题设置
    /// </summary>
    public class ItemConfig
    {
        /// <summary>
        /// 试题设置内部成员
        /// </summary>
        private int _defaultTypeId = 1;
        private string _defaultTypeName = string.Empty;
        private int _defaultDifficultyId = 3;
        private string _defaultDifficultyName = string.Empty;
        private decimal _defaultScore = (decimal) 2.0;
        private int _defaultAnswerCount = 4;
        private int _defaultCompleteTime = 60;
        private string _defaultSource = string.Empty;
        private string _defaultVersion = "1.0";
        private DateTime _defaultOutDateDate = DateTime.MaxValue;
        private int _defaultStatusId = 1;
        private string _defaultStatusName = string.Empty;
        private int _defaultRemindDays = 30;
        private int _defaultUsageId = 0;
        private int _hasPicture = 0;
        private string _hasPictureText = string.Empty;
		private int _itemConfigId = 1;
		private int _hasPicTure = 0;
		private int _employeeId = 0;
        /// <summary>
        /// 试题设置的试题类别ID
        /// </summary>
        public int DefaultTypeId
        {
            get { return _defaultTypeId; }
            set { _defaultTypeId = value; }
        }

        /// <summary>
        /// 试题设置的试题类别
        /// </summary>
        public string DefaultTypeName
        {
            get { return _defaultTypeName; }
            set { _defaultTypeName = value; }
        }

        /// <summary>
        /// 试题设置的试题难度ID
        /// </summary>
        public int DefaultDifficultyId
        {
            get { return _defaultDifficultyId; }
            set { _defaultDifficultyId = value; }
        }

        /// <summary>
        /// 试题设置的试题难度
        /// </summary>
        public string DefaultDifficultyName
        {
            get { return _defaultDifficultyName; }
            set { _defaultDifficultyName = value; }
        }

        /// <summary>
        /// 试题设置的缺省分值
        /// </summary>
       public decimal DefaultScore
        {
            get { return _defaultScore; }
            set { _defaultScore = value; }
        }

        /// <summary>
        /// 试题设置的后备选项个数
        /// </summary>
        public int DefaultAnswerCount
        {
            get { return _defaultAnswerCount; }
            set { _defaultAnswerCount = value; }
        }

        /// <summary>
        /// 试题设置的完成时间
        /// </summary>
        public int DefaultCompleteTime
        {
            get { return _defaultCompleteTime; }
            set { _defaultCompleteTime = value; }
        }

        /// <summary>
        /// 试题设置的试题来源
        /// </summary>
        public string DefaultSource
        {
            get { return _defaultSource; }
            set { _defaultSource = value; }
        }

        /// <summary>
        /// 试题设置的试题版本
        /// </summary>
        public string DefaultVersion
        {
            get { return _defaultVersion; }
            set { _defaultVersion = value; }
        }

        /// <summary>
        /// 试题设置的过期日期
        /// </summary>
        public DateTime DefaultOutDateDate
        {
            get { return _defaultOutDateDate; }
            set { _defaultOutDateDate = value; }
        }

        /// <summary>
        /// 试题设置的缺省状态ID
        /// </summary>
        public int DefaultStatusId
        {
            get { return _defaultStatusId; }
            set { _defaultStatusId = value; }
        }

        /// <summary>
        /// 试题设置的缺省状态
        /// </summary>
        public string DefaultStatusName
        {
            get { return _defaultStatusName; }
            set { _defaultStatusName = value; }
        }

        /// <summary>
        /// 试题设置的过期提前提醒天数
        /// </summary>
        public int DefaultRemindDays
        {
            get { return _defaultRemindDays; }
            set { _defaultRemindDays = value; }
        }

        /// <summary>
        /// 试题用途（0-仅用作考试；1－可用于练习；2－可用于作业；3－可用于练习和作业）
        /// </summary>
        public int DefaultUsageId
        {
            get { return _defaultUsageId; }
            set { _defaultUsageId = value; }
        }

        public int HasPicture
        {
            get { return _hasPicture; }
            set { _hasPicture = value; }
        }

        public string HasPictureText
        {
            get { return _hasPictureText; }
            set { _hasPictureText = value; }
        }

		/// <summary>
		/// 主键ID
		/// </summary>
		public int ItemConfigId
		{
			get { return _itemConfigId; }
			set { _itemConfigId = value; }
		}
		/// <summary>
		/// 是否是图片试题
		/// </summary>
		public int HasPicTure
		{
			get { return _hasPicTure; }
			set { _hasPicTure = value; }
		}

		/// <summary>
		/// 员工ID
		/// </summary>
		public int EmployeeId
		{
			get { return _employeeId; }
			set { _employeeId = value; }
		}
        /// <summary>
        /// 试题设置空构造函数
        /// </summary>
        public ItemConfig()
        {
        }

        /// <summary>
        /// 试题设置全参数构造函数
        /// </summary>
        /// <param name="defaultTypeId">试题设置的缺省类别ID</param>
        /// <param name="defaultDifficultyId">试题设置缺省难度ID</param>
        /// <param name="defaultScore">试题设置缺省分数</param>
        /// <param name="defaultAnswerCount">试题设置缺省候选项数</param>
        /// <param name="defaultCompleteTime">试题设置缺省完成时间</param>
        /// <param name="defaultSource">试题设置缺省试题来源</param>
        /// <param name="defaultVersion">试题设置缺省版本</param>
        /// <param name="defaultOutDateDate">试题设置缺省过期日期</param>
        /// <param name="defaultStatus">试题设置缺省状态</param>
        /// <param name="defaultRemindDays">试题设置缺省提前提醒天数</param>
        /// <param name="defaultUsageId">试题用途</param>
        public ItemConfig(int? defaultTypeId, int? defaultDifficultyId, decimal? defaultScore,
                          int? defaultAnswerCount, int? defaultCompleteTime, string defaultSource, string defaultVersion,
						  DateTime? defaultOutDateDate, int? defaultStatus, int? defaultRemindDays, int? defaultUsageId,
			int? itemConfigId, int? hasPicTure, int? employeeId)
        {
            _defaultTypeId = defaultTypeId ?? _defaultTypeId;
            _defaultDifficultyId = defaultDifficultyId ?? _defaultDifficultyId;
            _defaultScore = defaultScore ?? _defaultScore;
            _defaultAnswerCount = defaultAnswerCount ?? _defaultAnswerCount;
            _defaultCompleteTime = defaultCompleteTime ?? _defaultCompleteTime;
            _defaultSource = defaultSource;
            _defaultVersion = defaultVersion;
            _defaultOutDateDate = defaultOutDateDate ?? _defaultOutDateDate;
            _defaultStatusId = defaultStatus ?? _defaultStatusId;
            _defaultRemindDays = defaultRemindDays ?? _defaultRemindDays;
            _defaultUsageId = defaultUsageId ?? _defaultUsageId;
			_itemConfigId = itemConfigId ?? _itemConfigId;
			_hasPicture = hasPicTure ?? _hasPicture;
			_employeeId = employeeId ?? _employeeId;
        }
    }
}