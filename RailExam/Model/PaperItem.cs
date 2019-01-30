using System;

namespace RailExam.Model
{
    /// <summary>
    /// ҵ��ʵ�壺����
    /// </summary>
    public class PaperItem
    {
        private int _paperItemId = 0;
        private int _paperId = 0;
        private int _paperSubjectId = 0;
        private int _orderIndex = 0;
        private int _itemId = 0;
        private int _bookId = 0;
        private int _chapterId = 0;
        private int _categoryId = 0;
        private int _organizationId = 0;
        private int _typeId = 0;
        private string _typeName = string.Empty;
        private int _completeTime = 0;
        private int _difficultyId = 0;
        private string _source = string.Empty;
        private string _version = string.Empty;
        private decimal _score = (decimal)0.0;
        private string _content = string.Empty;
        private int _answerCount = 0;
        private string _selectAnswer = string.Empty;
        private string _standardAnswer = string.Empty;
        private string _description = string.Empty;
        private DateTime _outDateDate = DateTime.MaxValue;
        private int _usedCount = 0;
        private int _statusId = 0;
        private string _createPerson = string.Empty;
        private DateTime _createTime = DateTime.Now;
        private string _memo = string.Empty;
    
        public int PaperItemId
        {
            get { return _paperItemId; }
            set { _paperItemId = value; }
        }

        public int PaperId
        {
            get { return _paperId; }
            set { _paperId = value; }
        }

        public int PaperSubjectId
        {
            get { return _paperSubjectId; }
            set { _paperSubjectId = value; }
        }

        public int OrderIndex
        {
            get { return _orderIndex; }
            set { _orderIndex = value; }
        }

        /// <summary>
        /// ����ID
        /// </summary>
        public int ItemId
        {
            get { return _itemId; }
            set { _itemId = value; }
        }

        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
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
        /// �½�ID
        /// </summary>
        public int ChapterId
        {
            get { return _chapterId; }
            set { _chapterId = value; }
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
        /// ����ID
        /// </summary>
        public int OrganizationId
        {
            get { return _organizationId; }
            set { _organizationId = value; }
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
        /// ���ʱ�䣨�룩����ʾΪ��hh:mm:ss
        /// </summary>
        public int CompleteTime
        {
            get { return _completeTime; }
            set { _completeTime = value; }
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
        /// ��ע
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        /// <summary>
        /// �ղ������캯��
        /// </summary>
        public PaperItem()��{ }

        /// <summary>
        /// ȫ�������캯��
        /// </summary>
        /// <param name="itemId">����ID</param>
        /// <param name="bookId">�̲�ID</param>
        /// <param name="chapterId">�½�ID</param>
        /// <param name="categoryId">����ID</param>
        /// <param name="organizationId">����ID</param>
        /// <param name="typeId">����ID</param>
        /// <param name="completeTime">���ʱ�䣨�룩����ʾΪ��hh:mm:ss</param>
        /// <param name="difficultyId">�Ѷ�ID</param>
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
        /// <param name="createPerson">������</param>
        /// <param name="createTime">����ʱ��</param>
        /// <param name="memo">��ע</param>
        public PaperItem(int? paperItemId, int? paperId, int? paperSubjectId, int? orderIndex,
            int? itemId, int? bookId, int? chapterId, int? categoryId, int? organizationId,
            int? typeId, string typeName, int? completeTime, int? difficultyId, string source, string version,
            decimal? score, string content, int? answerCount, string selectAnswer,
            string standardAnswer, string description, DateTime? outDateDate, int? usedCount,
            int? statusId, string createPerson, DateTime? createTime, string memo)
        {
            _paperItemId = paperItemId ?? _paperItemId;
            _paperId = paperId ?? _paperId;
            _paperSubjectId = paperSubjectId ?? _paperSubjectId;
            _orderIndex = orderIndex ?? _orderIndex;
            _typeName = typeName;
            _itemId = itemId ?? _itemId;
            _bookId = bookId ?? _bookId;
            _chapterId = chapterId ?? _chapterId;
            _categoryId = categoryId ?? _categoryId;
            _organizationId = organizationId ?? _organizationId;
            _typeId = typeId ?? _typeId;
            _completeTime = completeTime ?? _completeTime;
            _difficultyId = difficultyId ?? _difficultyId;
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
            _createPerson = createPerson;
            _createTime = createTime ?? _createTime;
            _memo = memo;
        }
    }
}