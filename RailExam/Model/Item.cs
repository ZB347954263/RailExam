using System;
using DSunSoft.Common;
namespace RailExam.Model
{
    /// <summary>
    /// ҵ��ʵ�壺����
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
        /// ����ID
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
        /// �̲�ID
        /// </summary>
        public int BookId
        {
            get { return _bookId; }
            set { _bookId = value; }
        }

        /// <summary>
        /// ͼ������
        /// </summary>
        public string BookName
        {
            get { return _bookName; }
            set { _bookName = value; }
        }

        /// <summary>
        /// �½�ID
        /// </summary>
        public int ChapterId
        {
            get { return _chapterId; }
            set { _chapterId = value; }
        }

        /// <summary>
        /// �½�����
        /// </summary>
        public string ChapterName
        {
            get { return _chapterName; }
            set { _chapterName = value; }
        }

        /// <summary>
        /// �½�·��
        /// </summary>
        public string ChapterPath
        {
            get { return _chapterPath; }
            set { _chapterPath = value; }
        }

        /// <summary>
        /// ����ID
        /// </summary>
        public int CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }

        /// <summary>
        /// ������������
        /// </summary>
        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }

        /// <summary>
        /// ��������·��
        /// </summary>
        public string CategoryPath
        {
            get { return _categoryPath; }
            set { _categoryPath = value; }
        }

        /// <summary>
        /// ����ID
        /// </summary>
        public int OrganizationId
        {
            get { return _organizationId; }
            set { _organizationId = value; }
        }

        /// <summary>
        /// ��֯�ṹ����
        /// </summary>
        public string OrganizationName
        {
            get { return _organizationName; }
            set { _organizationName = value; }
        }

        /// <summary>
        /// ����ID
        /// </summary>
        public int TypeId
        {
            get { return _typeId; }
            set { _typeId = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        /// <summary>
        /// ���ʱ�䣨�룩����ʾΪ��hh:mm:ss
        /// </summary>
        public int CompleteTime
        {
            get { return _completeTime; }
            set { _completeTime = value; }
        }

        /// <summary>
        /// ���ʱ�䣨�룩����ʾΪ��hh:mm:ss
        /// </summary>
        public string CompleteTimeString
        {
            get { return CommonTool.ConvertSecondsToTimeString(_completeTime); }
            set { _completeTime = CommonTool.ConvertTimeStringToSeconds(value); }
        }

        /// <summary>
        /// �Ѷ�ID
        /// </summary>
        public int DifficultyId
        {
            get { return _difficultyId; }
            set { _difficultyId = value; }
        }

        /// <summary>
        /// �Ѷ�����
        /// </summary>
        public string DifficultyName
        {
            get { return _difficultyName; }
            set { _difficultyName = value; }
        }

        /// <summary>
        /// ��Դ
        /// </summary>
        public string Source
        {
            get { return _source; }
            set { _source = value; }
        }

        /// <summary>
        /// �汾
        /// </summary>
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        /// <summary>
        /// ��ֵ
        /// </summary>
        public decimal Score
        {
            get { return _score; }
            set { _score = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        /// <summary>
        /// ��ѡ�����
        /// </summary>
        public int AnswerCount
        {
            get { return _answerCount; }
            set { _answerCount = value; }
        }

        /// <summary>
        /// ��ѡ��
        /// </summary>
        public string SelectAnswer
        {
            get { return _selectAnswer; }
            set { _selectAnswer = value; }
        }

        /// <summary>
        /// ���
        /// </summary>
        public string StandardAnswer
        {
            get { return _standardAnswer; }
            set { _standardAnswer = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime OutDateDate
        {
            get { return _outDateDate; }
            set { _outDateDate = value; }
        }

        /// <summary>
        /// ����ʱ��
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
        /// ������ʹ�ô���
        /// </summary>
        public int UsedCount
        {
            get { return _usedCount; }
            set { _usedCount = value; }
        }

        /// <summary>
        /// ״̬ID
        /// </summary>
        public int StatusId
        {
            get { return _statusId; }
            set { _statusId = value; }
        }

        /// <summary>
        /// ״̬
        /// </summary>
        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public string CreatePerson
        {
            get { return _createPerson; }
            set { _createPerson = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        /// <summary>
        /// ������;ID
        /// </summary>
        public int UsageId
        {
            get { return _usageId; }
            set { _usageId = value; }
        }

        /// <summary>
        /// ��ע
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
        /// �ղ������캯��
        /// </summary>
        public Item() { }

        /// <summary>
        /// ȫ�������캯��
        /// </summary>
        /// <param name="itemId">����ID</param>
        /// <param name="bookId">�̲�ID</param>
        /// <param name="bookName">�̲�</param>
        /// <param name="chapterId">�½�ID</param>
        /// <param name="chapterName">�½�</param>
        /// <param name="categoryId">����ID</param>
        /// <param name="categoryName">����</param>
        /// <param name="organizationId">����ID</param>
        /// <param name="organizationName">����</param>
        /// <param name="typeId">����ID</param>
        /// <param name="typeName">����</param>
        /// <param name="completeTime">���ʱ�䣨�룩����ʾΪ��hh:mm:ss</param>
        /// <param name="difficultyId">�Ѷ�ID</param>
        /// <param name="difficultyName">�Ѷ�</param>
        /// <param name="source">��Դ</param>
        /// <param name="version">�汾</param>
        /// <param name="score">��ֵ</param>
        /// <param name="content">����</param>
        /// <param name="answerCount">��ѡ�����</param>
        /// <param name="selectAnswer">��ѡ��</param>
        /// <param name="standardAnswer">���</param>
        /// <param name="description">����</param>
        /// <param name="outDateDate">����ʱ��</param>
        /// <param name="usedCount">������ʹ�ô���</param>
        /// <param name="statusId">״̬ID</param>
        /// <param name="statusName">״̬</param>
        /// <param name="createPerson">������</param>
        /// <param name="createTime">����ʱ��</param>
        /// <param name="memo">��ע</param>
        /// <param name="usageId">������;ID</param>
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