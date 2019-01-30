using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class RandomExam
    {
        private int _RandomExamId = 0;
        private int _OrgId = 0;
        private int _categoryId = 0;//考试分类
        private int _ExamTypeId = 0;       
        private string _ExamName = string.Empty;
        private int _ExamTime = 0;
        private DateTime _BeginTime = DateTime.Now;
        private DateTime _EndTime = DateTime.Now;
        private int _ExamModeId = 0;
        private int _MinExamTimes = 0;
        private int _MaxExamTimes = 0;
        private decimal _ConvertTotalScore = 0;
        private decimal _PassScore = 0;
        private int _AutoSaveInterval = 0;
        private bool _IsUnderControl = false;
        private string _categoryName = string.Empty;
        private bool _IsAutoScore = false;
        private bool _CanSeeAnswer = false;
        private bool _CanSeeScore = false;
        private bool _IsPublicScore = false;
        private string _description = string.Empty;
        private int _statusId = 0;
        private string _createPerson = string.Empty;
        private DateTime _createTime = DateTime.Now;
        private string _memo = string.Empty; 
        private int _examineeCount = 0;
        private decimal _examAverageScore = 0.0M;
        private string _stationName = "";
        private int _downloaded = 0;
        private int _startMode = 1;
        private int _isStart = 0;
        private bool  _haspicture = false;
        private string _startModeName = "";
        private string _isStartName = "";
    	private string _code = "";
    	private int _version = 0;
    	private int _examStyle = 0;
    	private string _examStyleName = "";
    	private bool _isComputerExam = true;
    	private string _postID = "";
    	private int _technicianTypeID = 1;
    	private int  _isGroupLeader = 0;
    	private bool _isUpload = false;
    	private bool _hasTrainClass = false;
    	private bool _isReset = false;
        private int _random_Exam_modular_type_id = 0;
        private bool _is_Reduce_Error = false;
        private int _save_Status = 0;
        private DateTime? _save_date;
        private bool _hasPaperDetail = false;

        public bool HasPaperDetail
        {
            get { return _hasPaperDetail; }
            set { _hasPaperDetail = value; }
        }

        public int RandomExamId
        {
            get { return _RandomExamId; }
            set { _RandomExamId = value; }
        }        

        public int OrgId
        {
            get { return _OrgId; }
            set { _OrgId = value; }
        }

        public string ExamName
        {
            get { return _ExamName; }
            set { _ExamName = value; }
        }

        public int AutoSaveInterval
        {
            get { return _AutoSaveInterval; }
            set { _AutoSaveInterval = value; }
        }

        public bool IsUnderControl
        {
            get { return _IsUnderControl; }
            set { _IsUnderControl = value; }
        }

        public int CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }

        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }

        public int ExamTime
        {
            get { return _ExamTime; }
            set { _ExamTime = value; }
        }

        public bool IsAutoScore
        {
            get { return _IsAutoScore; }
            set { _IsAutoScore = value; }
        }

        public int ExamTypeId
        {
            get { return _ExamTypeId; }
            set { _ExamTypeId = value; }
        }

       

        public int ExamModeId
        {
            get { return _ExamModeId; }
            set { _ExamModeId = value; }
        }      

        public int MinExamTimes
        {
            get { return _MinExamTimes; }
            set { _MinExamTimes = value; }
        }

        public bool CanSeeAnswer
        {
            get { return _CanSeeAnswer; }
            set { _CanSeeAnswer = value; }
        }

        public bool CanSeeScore
        {
            get { return _CanSeeScore; }
            set { _CanSeeScore = value; }
        }

        public bool IsPublicScore
        {
            get { return _IsPublicScore; }
            set { _IsPublicScore = value; }
        }

        public int MaxExamTimes
        {
            get { return _MaxExamTimes; }
            set { _MaxExamTimes = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public DateTime BeginTime
        {
            get { return _BeginTime; }
            set { _BeginTime = value; }
        }

        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }

        public decimal ConvertTotalScore
        {
            get { return _ConvertTotalScore; }
            set { _ConvertTotalScore = value; }
        }

        public decimal PassScore
        {
            get { return _PassScore; }
            set { _PassScore = value; }
        }

        public int StatusId
        {
            get { return _statusId; }
            set { _statusId = value; }
        }

        public string CreatePerson
        {
            get { return _createPerson; }
            set { _createPerson = value; }
        }

        public DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }

        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }     

    	public int Version
    	{
			get { return _version; }
			set { _version = value; }
    	}

        /// <summary>
        /// 参考人数
        /// </summary>
        public int ExamineeCount
        {
            get { return _examineeCount; }
            set { _examineeCount = value; }
        }

        /// <summary>
        /// 考试人均分数
        /// </summary>
        public decimal ExamAverageScore
        {
            get { return _examAverageScore; }
            set { _examAverageScore = value; }
        }

        public string StationName
        {
            get { return _stationName; }
            set{ _stationName = value;}
        }

        public int Downloaded
        {
            get { return _downloaded; }
            set { _downloaded = value;}
        }

        public int StartMode
        {
            get { return _startMode; }
            set{ _startMode = value;}
        }

        public int IsStart
        {
            get { return _isStart; }
            set { _isStart = value; }
        }

        public bool HasPaper
        {
            get { return _haspicture; }
            set { _haspicture = value;}
        }

        public string StartModeName
        {
            get { return _startModeName; }
            set{ _startModeName = value;}
        }

        public string IsStartName
        {
            get { return _isStartName; }
            set { _isStartName = value;}
        }

    	public string RandomExamCode
    	{
			get { return _code; }
			set { _code = value;}
    	}

    	public int ExamStyle
    	{
			get { return _examStyle; }
			set { _examStyle = value;}
    	}

    	public string ExamStyleName
    	{
			get { return _examStyleName; }
			set { _examStyleName = value;}
    	}
    	public bool IsComputerExam
    	{
			get { return _isComputerExam; }
			set { _isComputerExam = value;}
    	}

    	public string PostID
    	{
			get { return _postID; }
			set { _postID = value;}
    	}

    	public int TechnicianTypeID
    	{
			get { return _technicianTypeID; }
			set { _technicianTypeID = value;}
    	}

    	public  int IsGroupLeader
    	{
			get { return _isGroupLeader; }
			set  { _isGroupLeader = value;}
    	}

    	public bool IsUpload
    	{
			get { return _isUpload; }
			set { _isUpload = value; }
    	}

    	public bool HasTrainClass
    	{
			get { return _hasTrainClass; }
			set { _hasTrainClass = value; }
    	}

    	public bool IsReset
    	{
			get { return _isReset; }
			set { _isReset = value;}
    	}

        public int RandomExamModularTypeID
        {
            get { return _random_Exam_modular_type_id; }
            set { _random_Exam_modular_type_id = value; }
        }

        public bool IsReduceError
        {
            get { return _is_Reduce_Error; }
            set { _is_Reduce_Error = value; }
        }

        public int SaveStatus
        {
            get { return _save_Status; }
            set { _save_Status = value; }
        }

        public DateTime? SaveDate
        {
            get { return _save_date; }
            set { _save_date = value; }
        }

         public RandomExam() { }

        public RandomExam(int? RandomExamId, int? OrgId, string ExamName, int? AutoSaveInterval, bool? IsUnderControl,
                    int? categoryId, string categoryName, int? ExamTime, bool? IsAutoScore,
                    int? ExamTypeId, int? ExamModeId, int? MinExamTimes, bool? CanSeeAnswer,
                    bool? CanSeeScore, bool? IsPublicScore, decimal? PassScore, int? MaxExamTimes, 
                    string description, DateTime? BeginTime, DateTime? EndTime,
                    decimal? ConvertTotalScore, int? statusId, string createPerson,
                    DateTime? createTime, string memo)
        {
            _RandomExamId = RandomExamId ?? _RandomExamId;
            _OrgId = OrgId ?? _OrgId;
            _ExamName = ExamName;
            _AutoSaveInterval = AutoSaveInterval ?? _AutoSaveInterval;
            _IsUnderControl = IsUnderControl??_IsUnderControl;
            _categoryId = categoryId ?? _categoryId;
            _categoryName = categoryName;
            _ExamTime = ExamTime ?? _ExamTime;
            _IsAutoScore = IsAutoScore ?? _IsAutoScore;
            _ExamTypeId = ExamTypeId ?? _ExamTypeId;            
            _ExamModeId = ExamModeId ?? _ExamModeId;
            _MinExamTimes = MinExamTimes ?? _MinExamTimes;
            _CanSeeAnswer = CanSeeAnswer ?? _CanSeeAnswer;
            _CanSeeScore = CanSeeScore ?? _CanSeeScore;
            _IsPublicScore = IsPublicScore ?? _IsPublicScore; 
            _description = description;
            _BeginTime = BeginTime ?? _BeginTime;
            _EndTime = EndTime ?? _EndTime;
            _ConvertTotalScore = ConvertTotalScore ?? _ConvertTotalScore;
            _PassScore = PassScore ?? _PassScore;
            _MaxExamTimes = MaxExamTimes ?? _MaxExamTimes;
            _statusId = statusId ?? _statusId;            
            _createPerson = createPerson;
            _createTime = createTime ?? _createTime;
            _memo = memo;
        }
    }
}
