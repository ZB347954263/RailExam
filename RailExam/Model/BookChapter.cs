using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace RailExam.Model
{

    public class BookChapter
    {
        /// <summary>
        /// 内部成员变量
        /// </summary>
        private int _bookId = 0;
        private int _chapterId = 0;
        private int _parentId = 0;
        private string _idPath = string.Empty;
        private int _levelNum = 2;
        private int _orderIndex = 1;
        private string _chapterName = string.Empty;
        private string _description = string.Empty;
        private string _memo = string.Empty;
        private string _referenceRegulation = string.Empty;
        private string _url = string.Empty;
        private string _studyDemand = string.Empty;
        private decimal _studyHours = 0;
        private int _hasExam = 0;
        private string _examForm = string.Empty;
        private string _lastPerson = string.Empty;
        private DateTime _lastDate = DateTime.Today;
        private string _namePath = string.Empty;
        private bool _isEdit = true;
    	private bool _isCannotSeeAnswer = false;
        private bool _isMotherItem = false;

        public BookChapter() { }
   
        public BookChapter(
            int? bookId,
            int? chapterId,
            int? parentId,
            string idPath,
            int? levelNum,
            int? orderIndex,
            string chapterName,
            string description,
            string memo,
            string referenceRegulation,
            string url, 
            string studyDemand,
            decimal? studyHours,
            int? hasExam,
            string examForm,
            string lastPerson,
            DateTime? lastDate,
			Boolean? isCannotSeeAnswer,
            Boolean? isMotherItem)
        {
            _bookId = bookId ?? _bookId;
            _chapterId = chapterId ?? _chapterId;
            _parentId = parentId ?? _parentId;
            _idPath = idPath;
            _levelNum = levelNum ?? _levelNum;
            _orderIndex = orderIndex ?? _orderIndex;
            _chapterName = chapterName;
            _description = description;
            _memo = memo;
            _referenceRegulation = referenceRegulation;
            _url = url;
            _studyDemand = studyDemand;
            _studyHours = studyHours ?? _studyHours;
            _hasExam = hasExam ?? _hasExam;
            _examForm = examForm;
            _lastPerson = lastPerson ;
            _lastDate = lastDate ?? _lastDate;
        	_isCannotSeeAnswer = isCannotSeeAnswer ?? _isCannotSeeAnswer;
            _isMotherItem = isMotherItem ?? _isMotherItem;
        }

        public string StudyDemand
        {
            set
            {
                _studyDemand = value;
            }
            get
            {
                return _studyDemand;
            }
        }

        public DateTime LastDate
        {
            set
            {
                _lastDate = value;
            }
            get
            {
                return _lastDate;
            }
        }

        public string LastPerson
        {
            set
            {
                _lastPerson = value;
            }
            get
            {
                return _lastPerson;
            }
        }

        public decimal StudyHours
        {
            set
            {
                _studyHours = value;
            }
            get
            {
                return _studyHours;
            }
        }

        public int HasExam
        {
            set
            {
                _hasExam = value;
            }
            get
            {
                return _hasExam;
            }
        }

        public string ExamForm
        {
            set
            {
                _examForm = value;
            }
            get
            {
                return _examForm;
            }
        }

        public int BookId
        {
            set
            {
                _bookId = value;
            }
            get
            {
                return _bookId;
            }
        }

        public int ChapterId
        {
            set
            {
                _chapterId = value;
            }
            get
            {
                return _chapterId;
            }
        }

        public int ParentId
        {
            set
            {
                _parentId = value;
            }
            get
            {
                return _parentId;
            }
        }

        public string IdPath
        {
            set
            {
                _idPath = value;
            }
            get
            {
                return _idPath;
            }
        }

        public int LevelNum
        {
            set
            {
                _levelNum = value;
            }
            get
            {
                return _levelNum;
            }
        }

        public int OrderIndex
        {
            set
            {
                _orderIndex = value;
            }
            get
            {
                return _orderIndex;
            }
        }

        public string ChapterName
        {
            set
            {
                _chapterName = value;
            }
            get
            {
                return _chapterName;
            }
        }

        public string Description
        {
            set
            {
                _description = value;
            }
            get
            {
                return _description;
            }
        }

        public string Memo
        {
            set
            {
                _memo = value;
            }
            get
            {
                return _memo;
            }
        }

        public string ReferenceRegulation
        {
            set
            {
                _referenceRegulation = value;
            }
            get
            {
                return _referenceRegulation;
            }
        }

        public string Url
        {
            set
            {
                _url = value;
            }
            get
            {
                return _url;
            }
        }

        public string NamePath
        {
            set
            {
                _namePath = value;
            }
            get
            {
                return _namePath;
            }
        }

        public bool IsEdit
        {
            set
            {
                _isEdit = value;
            }
            get
            {
                return _isEdit;
            }
        }

    	public bool IsCannotSeeAnswer
    	{
    		set
    		{
    			_isCannotSeeAnswer = value;
    		}
			get
			{
				return _isCannotSeeAnswer;
			}
    	}

        public bool IsMotherItem
        {
            set
            {
                _isMotherItem = value;
            }
            get
            {
                return _isMotherItem;
            }
        }
    }
}
