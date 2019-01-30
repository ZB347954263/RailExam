using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace RailExam.Model
{
    public class PaperStrategyBookChapter
    {
        private int _RangeType = 0;
		private int _PaperStrategyBookChapterId = 0;     
        private int _StrategySubjectId = 0;
        private int _RangeId = 0;
		private int _ItemTypeId = 0;
        private string _ItemTypeName = string.Empty;
		private decimal _UnitScore = 0;
        private int _UnitLimitTime = 0;
        private string _RangeName = string.Empty;		 
		private string _memo = string.Empty;       
        private string _ExcludeChapterId = string.Empty;
        private int _ItemDifficultyRandomCount = 0;
        private int _ItemDifficulty1Count = 0;
        private int _ItemDifficulty2Count = 0;
        private int _ItemDifficulty3Count = 0;
        private int _ItemDifficulty4Count = 0;
        private int _ItemDifficulty5Count = 0;
        private string _SubjectName = string.Empty;
     

        public PaperStrategyBookChapter() { }

        public PaperStrategyBookChapter(
			int? rangeType,
			int? PaperStrategyBookChapterId,
            int? StrategySubjectId,
            int? RangeId,
			int? ItemTypeId,
            string ItemTypeName,
            decimal? UnitScore,
            int? UnitLimitTime,
            string RangeName,
            string memo,
            string ExcludeChapterId,
            int? ItemDifficultyRandomCount,
            int? ItemDifficulty1Count,
            int? ItemDifficulty2Count,
            int? ItemDifficulty3Count,
            int? ItemDifficulty4Count,
            int? ItemDifficulty5Count,
            string SubjectName)
		{
            _RangeType = rangeType ?? _RangeType;
			_PaperStrategyBookChapterId = PaperStrategyBookChapterId ?? _PaperStrategyBookChapterId;
            _StrategySubjectId = StrategySubjectId ?? _StrategySubjectId;
            _RangeId = RangeId ?? _RangeId;
			_ItemTypeId = ItemTypeId ?? _ItemTypeId;
            _ItemTypeName = ItemTypeName;
			_UnitScore = UnitScore ?? _UnitScore;
            _UnitLimitTime = UnitLimitTime ?? _UnitLimitTime;
            _RangeName = RangeName;
			_memo = memo;
            _SubjectName = SubjectName;
              
            _ExcludeChapterId = ExcludeChapterId;
            _ItemDifficultyRandomCount = ItemDifficultyRandomCount ?? _ItemDifficultyRandomCount;
            _ItemDifficulty1Count = ItemDifficulty1Count ?? _ItemDifficulty1Count;
            _ItemDifficulty2Count = ItemDifficulty2Count ?? _ItemDifficulty2Count;
            _ItemDifficulty3Count = ItemDifficulty3Count ?? _ItemDifficulty3Count;
            _ItemDifficulty4Count = ItemDifficulty4Count ?? _ItemDifficulty4Count;
            _ItemDifficulty5Count = ItemDifficulty5Count ?? _ItemDifficulty5Count;
		}

        public string SubjectName
        {
            set
            {
                _SubjectName = value;
            }
            get
            {
                return _SubjectName;
            }
        }
     

        public int ItemDifficulty5Count
        {
            set
            {
                _ItemDifficulty5Count = value;
            }
            get
            {
                return _ItemDifficulty5Count;
            }
        }

        public int ItemDifficulty4Count
        {
            set
            {
                _ItemDifficulty4Count = value;
            }
            get
            {
                return _ItemDifficulty4Count;
            }
        }

        public int ItemDifficulty3Count
        {
            set
            {
                _ItemDifficulty3Count = value;
            }
            get
            {
                return _ItemDifficulty3Count;
            }
        }

        public int ItemDifficulty2Count
        {
            set
            {
                _ItemDifficulty2Count = value;
            }
            get
            {
                return _ItemDifficulty2Count;
            }
        }

        public int ItemDifficulty1Count
        {
            set
            {
                _ItemDifficulty1Count = value;
            }
            get
            {
                return _ItemDifficulty1Count;
            }

        }

        public int ItemDifficultyRandomCount
        {
            set
            {
                _ItemDifficultyRandomCount = value;
            }
            get
            {
                return _ItemDifficultyRandomCount;
            }
        }

		public int RangeType
		{
			set
			{
                _RangeType = value;
			}
			get
			{
                return _RangeType;
			}
		}
		
		public int PaperStrategyBookChapterId
		{
			set
			{
                _PaperStrategyBookChapterId = value;
			}
			get
			{
				return _PaperStrategyBookChapterId;
			}
		}

        public int StrategySubjectId
        {
            set
            {
                _StrategySubjectId = value;
            }
            get
            {
                return _StrategySubjectId;
            }

        }

        public int RangeId
		{
			set
			{
                _RangeId = value;
			}
			get
			{
                return _RangeId;
			}
		}

		public int ItemTypeId
		{
			set
			{
                _ItemTypeId = value;
			}
			get
			{
				return _ItemTypeId;
			}
		}

        public string ItemTypeName
		{
			set
			{
                _ItemTypeName = value;
			}
			get
			{
				return _ItemTypeName;
			}
		}

        public decimal UnitScore
		{
			set
			{
				_UnitScore = value;
			}
			get
			{
				return _UnitScore;
			}
		}

        public int UnitLimitTime
        {
            set
            {
                _UnitLimitTime = value;
            }
            get
            {
                return _UnitLimitTime;
            }
        }

		public string RangeName
		{
			set
			{
                _RangeName = value;
			}
			get
			{
                return _RangeName;
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
       
        public string ExcludeChapterId
        {
            set
            {
                _ExcludeChapterId = value;
            }
            get
            {
                return _ExcludeChapterId;
            }
        }
	}
}