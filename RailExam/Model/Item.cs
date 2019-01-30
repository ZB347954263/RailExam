using System;
using DSunSoft.Common;
namespace RailExam.Model
{
    /// <summary>
    /// 业务实体：试题
    /// </summary>
    public class Item
    {
        private int _itemId = 0;
        private int _bookId = 0;
        private string _bookName = string.Empty;
        private int _chapterId = 0;
        private string _chapterName = string.Empty;
        private string _chapterPath = string.Empty;
        private int _categoryId = 0;
        private string _categoryName = string.Empty;
        private string _categoryPath = string.Empty;
        private int _organizationId = 0;
        private string _organizationName = string.Empty;
        private int _typeId = 0;
        private string _typeName = string.Empty;
        private int _completeTime = 0;
        private int _difficultyId = 0;
        private string _difficultyName = string.Empty;
        private string _source = string.Empty;
        private string _version = string.Empty;
        private decimal _score = (decimal) 0.0;
        private string _content = string.Empty;
        private int _answerCount = 0;
        private string _selectAnswer = string.Empty;
        private string _standardAnswer = string.Empty;
        private string _description = string.Empty;
        private DateTime _outDateDate = DateTime.MinValue;
        private int _usedCount = 0;
        private int _statusId = 0;
        private string _statusName = string.Empty;
        private string _createPerson = string.Empty;
        private DateTime _createTime = DateTime.Now;
        private int _usageId = 0;
        private string _memo = string.Empty;
        private int _strategyId = 0;
        private int _hasPicture = 0;
    	private string _keyWord = string.Empty;
        private int _parentItemId = 0;
        private string _mothercode = string.Empty;
        private int _itemIndex = 0;
        private int _authors = -1;

        public int Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }

        public int ItemIndex
        {
            get { return _itemIndex; }
            set { _itemIndex = value; }
        }

        /// <summary>
        /// 试题ID
        /// </summary>
        public int ItemId
        {
            get { return _itemId; }
            set { _itemId = value; }
        }

		public string KeyWord
		{
			get { return _keyWord; }
			set { _keyWord = value; }
		}

        public string MotherCode
        {
            get { return _mothercode; }
            set { _mothercode = value; }
        }

        /// <summary>
        /// 教材ID
        /// </summary>
        public int BookId
        {
            get { return _bookId; }
            set { _bookId = value; }
        }

        /// <summary>
        /// 图书名称
        /// </summary>
        public string BookName
        {
            get { return _bookName; }
            set { _bookName = value; }
        }

        /// <summary>
        /// 章节ID
        /// </summary>
        public int ChapterId
        {
            get { return _chapterId; }
            set { _chapterId = value; }
        }

        /// <summary>
        /// 章节名称
        /// </summary>
        public string ChapterName
        {
            get { return _chapterName; }
            set { _chapterName = value; }
        }

        /// <summary>
        /// 章节路径
        /// </summary>
        public string ChapterPath
        {
            get { return _chapterPath; }
            set { _chapterPath = value; }
        }

        /// <summary>
        /// 分类ID
        /// </summary>
        public int CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }

        /// <summary>
        /// 辅助分类名称
        /// </summary>
        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }

        /// <summary>
        /// 辅助分类路径
        /// </summary>
        public string CategoryPath
        {
            get { return _categoryPath; }
            set { _categoryPath = value; }
        }

        /// <summary>
        /// 机构ID
        /// </summary>
        public int OrganizationId
        {
            get { return _organizationId; }
            set { _organizationId = value; }
        }

        /// <summary>
        /// 组织结构名称
        /// </summary>
        public string OrganizationName
        {
            get { return _organizationName; }
            set { _organizationName = value; }
        }

        /// <summary>
        /// 类型ID
        /// </summary>
        public int TypeId
        {
            get { return _typeId; }
            set { _typeId = value; }
        }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        /// <summary>
        /// 完成时间（秒），显示为：hh:mm:ss
        /// </summary>
        public int CompleteTime
        {
            get { return _completeTime; }
            set { _completeTime = value; }
        }

        /// <summary>
        /// 完成时间（秒），显示为：hh:mm:ss
        /// </summary>
        public string CompleteTimeString
        {
            get { return CommonTool.ConvertSecondsToTimeString(_completeTime); }
            set { _completeTime = CommonTool.ConvertTimeStringToSeconds(value); }
        }

        /// <summary>
        /// 难度ID
        /// </summary>
        public int DifficultyId
        {
            get { return _difficultyId; }
            set { _difficultyId = value; }
        }

        /// <summary>
        /// 难度名称
        /// </summary>
        public string DifficultyName
        {
            get { return _difficultyName; }
            set { _difficultyName = value; }
        }

        /// <summary>
        /// 来源
        /// </summary>
        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        /// <summary>
        /// 分值
        /// </summary>
        public decimal Score
        {
            get { return _score; }
            set { _score = value; }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        /// <summary>
        /// 候选项个数
        /// </summary>
        public int AnswerCount
        {
            get { return _answerCount; }
            set { _answerCount = value; }
        }

        /// <summary>
        /// 候选项
        /// </summary>
        public string SelectAnswer
        {
            get { return _selectAnswer; }
            set { _selectAnswer = value; }
        }

        /// <summary>
        /// 标答
        /// </summary>
        public string StandardAnswer
        {
            get { return _standardAnswer; }
            set { _standardAnswer = value; }
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime OutDateDate
        {
            get { return _outDateDate; }
            set { _outDateDate = value; }
        }

        /// <summary>
        /// 过期时间
        /// </summary>
        public string OutDateDateString
        {
            get
            {
                return _outDateDate == DateTime.MinValue ? string.Empty : _outDateDate.ToString("yyyy-MM-dd"); 
            }
            set
            {
                if (!DateTime.TryParse(value, out _outDateDate))
                {
                    _outDateDate = DateTime.MinValue;
                }
            }
        }

        /// <summary>
        /// 考卷中使用次数
        /// </summary>
        public int UsedCount
        {
            get { return _usedCount; }
            set { _usedCount = value; }
        }

        /// <summary>
        /// 状态ID
        /// </summary>
        public int StatusId
        {
            get { return _statusId; }
            set { _statusId = value; }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        /// <summary>
        /// 出题人
        /// </summary>
        public string CreatePerson
        {
            get { return _createPerson; }
            set { _createPerson = value; }
        }

        /// <summary>
        /// 出题时间
        /// </summary>
        public DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        /// <summary>
        /// 试题用途ID
        /// </summary>
        public int UsageId
        {
            get { return _usageId; }
            set { _usageId = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        public int StrategyId
        {
            get { return _strategyId; }
            set { _strategyId = value; }
        }

        public int HasPicture
        {
            get { return _hasPicture; }
            set { _hasPicture = value; }
        }

        public int ParentItemId
        {
            get { return _parentItemId; }
            set { _parentItemId = value; }
        }

        /// <summary>
        /// 空参数构造函数
        /// </summary>
        public Item() { }

        /// <summary>
        /// 全参数构造函数
        /// </summary>
        /// <param name="itemId">试题ID</param>
        /// <param name="bookId">教材ID</param>
        /// <param name="bookName">教材</param>
        /// <param name="chapterId">章节ID</param>
        /// <param name="chapterName">章节</param>
        /// <param name="categoryId">分类ID</param>
        /// <param name="categoryName">分类</param>
        /// <param name="organizationId">机构ID</param>
        /// <param name="organizationName">机构</param>
        /// <param name="typeId">类型ID</param>
        /// <param name="typeName">类型</param>
        /// <param name="completeTime">完成时间（秒），显示为：hh:mm:ss</param>
        /// <param name="difficultyId">难度ID</param>
        /// <param name="difficultyName">难度</param>
        /// <param name="source">来源</param>
        /// <param name="version">版本</param>
        /// <param name="score">分值</param>
        /// <param name="content">内容</param>
        /// <param name="answerCount">候选项个数</param>
        /// <param name="selectAnswer">候选项</param>
        /// <param name="standardAnswer">标答</param>
        /// <param name="description">描述</param>
        /// <param name="outDateDate">过期时间</param>
        /// <param name="usedCount">考卷中使用次数</param>
        /// <param name="statusId">状态ID</param>
        /// <param name="statusName">状态</param>
        /// <param name="createPerson">出题人</param>
        /// <param name="createTime">出题时间</param>
        /// <param name="memo">备注</param>
        /// <param name="usageId">试题用途ID</param>
        public Item(int? itemId, int? bookId, string bookName, int? chapterId, string chapterName,
                    int? categoryId, string categoryName, int? organizationId, string organizationName, 
                    int? typeId, string typeName, int? completeTime, int? difficultyId, string difficultyName, 
                    string source, string version, decimal? score, string content, int? answerCount, 
                    string selectAnswer, string standardAnswer, string description, DateTime? outDateDate, 
                    int? usedCount, int? statusId, string statusName, string createPerson, 
                    DateTime? createTime, int? usageId, string memo)
        {
            _itemId = itemId ?? _itemId;
            _bookId = bookId ?? _bookId;
            _bookName = bookName;
            _chapterId = chapterId ?? _chapterId;
            _chapterName = chapterName;
            _categoryId = categoryId ?? _categoryId;
            _categoryName = categoryName;
            _organizationId = organizationId ?? _organizationId;
            _organizationName = organizationName;
            _typeId = typeId ?? _typeId;
            _typeName = typeName;
            _completeTime = completeTime ?? _completeTime;
            _difficultyId = difficultyId ?? _difficultyId;
            _difficultyName = difficultyName;
            _source = source;
            _version = version;
            _score = score ?? _score;
            _content = content;
            _answerCount = answerCount ?? _answerCount;
            _selectAnswer = selectAnswer;
            _standardAnswer = standardAnswer;
            _description = description;
            _outDateDate = outDateDate ?? _outDateDate;
            _usedCount = usedCount ?? _usedCount;
            _statusId = statusId ?? _statusId;
            _statusName = statusName;
            _createPerson = createPerson;
            _createTime = createTime ?? _createTime;
            _usageId = usageId ?? _usageId;
            _memo = memo;
        }
    }
}