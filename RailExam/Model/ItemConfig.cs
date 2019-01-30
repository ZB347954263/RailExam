using System;

namespace RailExam.Model
{
    /// <summary>
    /// ҵ��ʵ�壺��������
    /// </summary>
    public class ItemConfig
    {
        /// <summary>
        /// ���������ڲ���Ա
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
        /// �������õ��������ID
        /// </summary>
        public int DefaultTypeId
        {
            get { return _defaultTypeId; }
            set { _defaultTypeId = value; }
        }

        /// <summary>
        /// �������õ��������
        /// </summary>
        public string DefaultTypeName
        {
            get { return _defaultTypeName; }
            set { _defaultTypeName = value; }
        }

        /// <summary>
        /// �������õ������Ѷ�ID
        /// </summary>
        public int DefaultDifficultyId
        {
            get { return _defaultDifficultyId; }
            set { _defaultDifficultyId = value; }
        }

        /// <summary>
        /// �������õ������Ѷ�
        /// </summary>
        public string DefaultDifficultyName
        {
            get { return _defaultDifficultyName; }
            set { _defaultDifficultyName = value; }
        }

        /// <summary>
        /// �������õ�ȱʡ��ֵ
        /// </summary>
       public decimal DefaultScore
        {
            get { return _defaultScore; }
            set { _defaultScore = value; }
        }

        /// <summary>
        /// �������õĺ�ѡ�����
        /// </summary>
        public int DefaultAnswerCount
        {
            get { return _defaultAnswerCount; }
            set { _defaultAnswerCount = value; }
        }

        /// <summary>
        /// �������õ����ʱ��
        /// </summary>
        public int DefaultCompleteTime
        {
            get { return _defaultCompleteTime; }
            set { _defaultCompleteTime = value; }
        }

        /// <summary>
        /// �������õ�������Դ
        /// </summary>
        public string DefaultSource
        {
            get { return _defaultSource; }
            set { _defaultSource = value; }
        }

        /// <summary>
        /// �������õ�����汾
        /// </summary>
        public string DefaultVersion
        {
            get { return _defaultVersion; }
            set { _defaultVersion = value; }
        }

        /// <summary>
        /// �������õĹ�������
        /// </summary>
        public DateTime DefaultOutDateDate
        {
            get { return _defaultOutDateDate; }
            set { _defaultOutDateDate = value; }
        }

        /// <summary>
        /// �������õ�ȱʡ״̬ID
        /// </summary>
        public int DefaultStatusId
        {
            get { return _defaultStatusId; }
            set { _defaultStatusId = value; }
        }

        /// <summary>
        /// �������õ�ȱʡ״̬
        /// </summary>
        public string DefaultStatusName
        {
            get { return _defaultStatusName; }
            set { _defaultStatusName = value; }
        }

        /// <summary>
        /// �������õĹ�����ǰ��������
        /// </summary>
        public int DefaultRemindDays
        {
            get { return _defaultRemindDays; }
            set { _defaultRemindDays = value; }
        }

        /// <summary>
        /// ������;��0-���������ԣ�1����������ϰ��2����������ҵ��3����������ϰ����ҵ��
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
		/// ����ID
		/// </summary>
		public int ItemConfigId
		{
			get { return _itemConfigId; }
			set { _itemConfigId = value; }
		}
		/// <summary>
		/// �Ƿ���ͼƬ����
		/// </summary>
		public int HasPicTure
		{
			get { return _hasPicTure; }
			set { _hasPicTure = value; }
		}

		/// <summary>
		/// Ա��ID
		/// </summary>
		public int EmployeeId
		{
			get { return _employeeId; }
			set { _employeeId = value; }
		}
        /// <summary>
        /// �������ÿչ��캯��
        /// </summary>
        public ItemConfig()
        {
        }

        /// <summary>
        /// ��������ȫ�������캯��
        /// </summary>
        /// <param name="defaultTypeId">�������õ�ȱʡ���ID</param>
        /// <param name="defaultDifficultyId">��������ȱʡ�Ѷ�ID</param>
        /// <param name="defaultScore">��������ȱʡ����</param>
        /// <param name="defaultAnswerCount">��������ȱʡ��ѡ����</param>
        /// <param name="defaultCompleteTime">��������ȱʡ���ʱ��</param>
        /// <param name="defaultSource">��������ȱʡ������Դ</param>
        /// <param name="defaultVersion">��������ȱʡ�汾</param>
        /// <param name="defaultOutDateDate">��������ȱʡ��������</param>
        /// <param name="defaultStatus">��������ȱʡ״̬</param>
        /// <param name="defaultRemindDays">��������ȱʡ��ǰ��������</param>
        /// <param name="defaultUsageId">������;</param>
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