using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class RandomExamResultCurrent
    {
             #region �ڲ���Ա

        private int _randomExamResultId = 0;
        private int _organizationId = 0;
        private string _organizationName = string.Empty;
        private int _randomExamId = 0;
        private string _examName = string.Empty;
        private int _examineeId = 0;
        private string _examineeName = string.Empty;
        private DateTime _beginDateTime = DateTime.Now;
        private DateTime _currentDateTime = DateTime.Now;
        private DateTime _endDateTime;
        private int _examTime = 0;
        private decimal _autoScore = (decimal)0.0;
        private decimal _score = (decimal)0.0;
        private decimal _correctRate = (decimal)0.0;
        private bool _isPass = false;
        private int _statusId = 0;
        private string _statusName = string.Empty;
        private string _memo = string.Empty;   
        private string _workNo = string.Empty;
        private string _postName = string.Empty;
        private int _examSeqNo = 1;
        #endregion


        /// <summary>
        /// ���Կ������ID
        /// </summary>
        public int RandomExamResultId
        {
            get { return _randomExamResultId; }
            set { _randomExamResultId = value; }
        }

        /// <summary>
        /// ��֯����ID
        /// </summary>
        public int OrganizationId
        {
            get { return _organizationId; }
            set { _organizationId = value; }
        }

        /// <summary>
        /// ��֯����
        /// </summary>
        public string OrganizationName
        {
            get { return _organizationName; }
        }

        /// <summary>
        /// ����ID
        /// </summary>
        public int RandomExamId
        {
            get { return _randomExamId; }
            set { _randomExamId = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string ExamName
        {
            get { return _examName; }
        }



        /// <summary>
        /// ����ID
        /// </summary>
        public int ExamineeId
        {
            get { return _examineeId; }
            set { _examineeId = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string ExamineeName
        {
            get { return _examineeName; }
        }

        /// <summary>
        /// ���Կ�ʼʱ��
        /// </summary>
        public DateTime BeginDateTime
        {
            get { return _beginDateTime; }
            set { _beginDateTime = value; }
        }

        /// <summary>
        /// ���Ե�ǰʱ��
        /// </summary>
        public DateTime CurrentDateTime
        {
            get { return _currentDateTime; }
            set { _currentDateTime = value; }
        }

        /// <summary>
        /// ���Խ���ʱ��
        /// </summary>
        public DateTime EndDateTime
        {
            get { return _endDateTime; }
            set { _endDateTime = value; }
        }

        /// <summary>
        /// ���Ի���ʱ�䣬��
        /// </summary>
        public int ExamTime
        {
            get { return _examTime; }
            set { _examTime = value; }
        }

        public string ExamTimeString
        {
            get { return DSunSoft.Common.CommonTool.ConvertSecondsToTimeString(_examTime); }
        }

        /// <summary>
        /// �Զ����ַ���
        /// </summary>
        public decimal AutoScore
        {
            get { return _autoScore; }
            set { _autoScore = value; }
        }

        /// <summary>
        /// ���ַ���
        /// </summary>
        public decimal Score
        {
            get { return _score; }
            set { _score = value; }
        }



        /// <summary>
        /// ��ȷ�ʣ��ٷֱȣ�
        /// </summary>
        public decimal CorrectRate
        {
            get { return _correctRate; }
            set { _correctRate = value; }
        }

        /// <summary>
        /// �Ƿ�ͨ������
        /// </summary>
        public bool IsPass
        {
            get { return _isPass; }
            set { _isPass = value; }
        }

        /// <summary>
        /// ���Կ������״̬ID
        /// </summary>
        public int StatusId
        {
            get { return _statusId; }
            set { _statusId = value; }
        }

        public string StatusName
        {
            get { return _statusName; }
        }

        /// <summary>
        /// ��ע
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }       

        public string WorkNo
        {
            get { return _workNo; }
            set { _workNo = value; }
        }

        public string PostName
        {
            get { return _postName; }
            set { _postName = value; }
        }
        public int ExamSeqNo
        {
            get { return _examSeqNo; }
            set { _examSeqNo = value; }
        }

        /// <summary>
        /// �ղ������캯��
        /// </summary>
        public RandomExamResultCurrent() { }

        public RandomExamResultCurrent(int? randomExamResultId, int? organizationId, string organizationName, int? randomExamId,
            string examName, int? examineeId, string examineeName,
            DateTime? beginDateTime, DateTime? currentDateTime, DateTime? endDateTime, int? examTime,
            decimal? autoScore, decimal? score, decimal? correctRate, bool? isPass, int? statusId, string statusName, string memo, int? examSeqNo)
        {
            _randomExamResultId = randomExamResultId ?? _randomExamResultId;
            _organizationId = organizationId ?? _organizationId;
            _organizationName = organizationName;
            _randomExamId = randomExamId ?? _randomExamId;
            _examName = examName;
            _examineeId = examineeId ?? _examineeId;
            _examineeName = examineeName;
            _beginDateTime = beginDateTime ?? _beginDateTime;
            _currentDateTime = currentDateTime ?? _currentDateTime;
            _endDateTime = endDateTime ?? _endDateTime;
            _examTime = examTime ?? _examTime;
            _autoScore = autoScore ?? _autoScore;
            _score = score ?? _score;
            _correctRate = correctRate ?? _correctRate;
            _isPass = isPass ?? _isPass;
            _statusId = statusId ?? _statusId;
            _statusName = statusName;
            _memo = memo;
            _examSeqNo = examSeqNo ?? _examSeqNo;
        }
    }
}
