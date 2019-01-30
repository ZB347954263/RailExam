using System;

namespace RailExam.Model
{
    public class ExamResultAnswer
    {
        /// <summary>
        /// ʵ���ڲ���Ա
        /// </summary>
        private int _examResultId = 0;
        private string _examName = string.Empty;
        private int _paperItemId = 0;
        private string _paperName = string.Empty;
        private string _answer = string.Empty;
        private int _examTime = 0;
        private decimal _judgeScore = 0.0M;
        private int _judgeStatusId = 0;
        private string _judgeStatusName = string.Empty;
        private string _judgeRemark = string.Empty;
        private DateTime _beginDateTime = DateTime.MinValue;
        private DateTime _endDateTime = DateTime.MinValue;
        private DateTime _judgeBeginDateTime = DateTime.MinValue;
        private DateTime _judgeEndDateTime = DateTime.MinValue;
        private string _examineeName = string.Empty;
        private string _judgeName = string.Empty;

        /// <summary>
        /// ���Կ������ID
        /// </summary>
        public int ExamResultId
        {
            get { return _examResultId; }
            set { _examResultId = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string ExamName
        {
            get { return _examName; }
        }

        /// <summary>
        /// �Ծ�����ID
        /// </summary>
        public int PaperItemId
        {
            get { return _paperItemId; }
            set { _paperItemId = value; }
        }

        /// <summary>
        /// �Ծ�����
        /// </summary>
        public string PaperName
        {
            get { return _paperName; }
        }

        /// <summary>
        /// �𰸣�ʹ��|�ָ���
        /// </summary>
        public string Answer
        {
            get { return _answer; }
            set { _answer = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public int ExamTime
        {
            get { return _examTime; }
            set { _examTime = value; }
        }

        /// <summary>
        /// ���ַ���
        /// </summary>
        public decimal JudgeScore
        {
            get { return _judgeScore; }
            set { _judgeScore = value; }
        }

        /// <summary>
        /// ����״̬ID
        /// </summary>
        public int JudgeStatusId
        {
            get { return _judgeStatusId; }
            set { _judgeStatusId = value; }
        }

        /// <summary>
        /// ״̬����
        /// </summary>
        public string JudgeStatusName
        {
            get { return _judgeStatusName; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string JudgeRemark
        {
            get { return _judgeRemark; }
            set { _judgeRemark = value; }
        }

        /// <summary>
        /// ���Կ�ʼʱ��
        /// </summary>
        public DateTime BeginDateTime
        {
            get { return _beginDateTime; }
        }

        /// <summary>
        /// ���Խ���ʱ��
        /// </summary>
        public DateTime EndDateTime
        {
            get { return _endDateTime; }
        }

        /// <summary>
        /// ���ֿ�ʼʱ��
        /// </summary>
        public DateTime JudgeBeginDateTime
        {
            get { return _judgeBeginDateTime; }
        }

        /// <summary>
        /// ���ֽ���ʱ��
        /// </summary>
        public DateTime JudgeEndDateTime
        {
            get { return _judgeEndDateTime; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string ExamineeName
        {
            get { return _examineeName; }
        }

        /// <summary>
        /// ��ί����
        /// </summary>
        public string JudgeName
        {
            get { return _judgeName; }
        }

        /// <summary>
        /// �ղ������캯��
        /// </summary>
        public ExamResultAnswer() { }

        /// <summary>
        /// ȫ�������캯��
        /// </summary>
        /// <param name="examResultId"></param>
        /// <param name="examName"></param>
        /// <param name="paperItemId"></param>
        /// <param name="paperName"></param>
        /// <param name="answer"></param>
        /// <param name="examTime"></param>
        /// <param name="judgeScore"></param>
        /// <param name="judgeStatusId"></param>
        /// <param name="judgeStatusName"></param>
        /// <param name="judgeRemark"></param>
        /// <param name="beginDateTime"></param>
        /// <param name="endDateTime"></param>
        /// <param name="judgeBeginDateTime"></param>
        /// <param name="judgeEndDateTime"></param>
        /// <param name="examineeName"></param>
        /// <param name="judgeName"></param>
        public ExamResultAnswer(int? examResultId, 
            string examName,
            int? paperItemId, 
            string paperName, 
            string answer, 
            int? examTime, 
            decimal? judgeScore, 
            int? judgeStatusId, 
            string judgeStatusName,
            string judgeRemark, 
            DateTime? beginDateTime, 
            DateTime? endDateTime, 
            DateTime? judgeBeginDateTime, 
            DateTime? judgeEndDateTime, 
            string examineeName, 
            string judgeName)
        {
            _examResultId = examResultId ?? _examResultId;
            _examName = examName;
            _paperItemId = paperItemId ?? _paperItemId;
            _paperName = paperName;
            _answer = answer;
            _examTime = examTime ?? _examTime;
            _judgeScore = judgeScore ?? _judgeScore;
            _judgeStatusId = judgeStatusId ?? _judgeStatusId;
            _judgeStatusName = judgeStatusName;
            _judgeRemark = judgeRemark;
            _beginDateTime = beginDateTime ?? _beginDateTime;
            _endDateTime = endDateTime ?? _endDateTime;
            _judgeBeginDateTime = judgeBeginDateTime ?? _judgeBeginDateTime;
            _judgeEndDateTime = judgeEndDateTime ?? _judgeEndDateTime;
            _examineeName = examineeName;
            _judgeName = judgeName;
        }
    }
}
