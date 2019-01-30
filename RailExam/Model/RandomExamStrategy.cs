using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class RandomExamStrategy
    {
        private int _RangeType = 0;
        private int _RandomExamStrategyId = 0;
        private int _SubjectId = 0;
        private int _RangeId = 0;
        private int _ItemTypeId = 0;
        private string _ItemTypeName = string.Empty;
        private string _RangeName = string.Empty;
        private string _memo = string.Empty;
        private string _ExcludeChapterId = string.Empty;
        private int _ItemCount = 0;
		private int _ItemDifficultyID = 0;
    	private string _ItemDifficultyName = string.Empty;
        private int _MaxItemDifficultyID = 0;
        private string _MaxItemDifficultyName = string.Empty;
    	private string _subjectName = string.Empty;
        private bool _isMotherItem = false;
        private int _motherID = 0;
        private int _totalItemCount = 0;
        private int _selectCount = 0;

        public RandomExamStrategy() { }

        public RandomExamStrategy(
             int? rangeType,
             int? RandomExamStrategyId,
             int? SubjectId,
             int? RangeId,
             int? ItemTypeId,
             string ItemTypeName,                      
             string RangeName,
             string memo,
             string ExcludeChapterId, int? ItemCount,int? ItemDiffcultyID,string ItemDifficultyName,string subjectName)
        {
            _RangeType = rangeType ?? _RangeType;
            _RandomExamStrategyId = RandomExamStrategyId ?? _RandomExamStrategyId;
            _SubjectId = SubjectId ?? _SubjectId;
            _RangeId = RangeId ?? _RangeId;
            _ItemTypeId = ItemTypeId ?? _ItemTypeId;
            _ItemTypeName = ItemTypeName;           
            _RangeName = RangeName;
            _memo = memo; 
            _ExcludeChapterId = ExcludeChapterId;
            _ItemCount = ItemCount ?? _ItemCount;
        	_ItemDifficultyID = ItemDiffcultyID ?? _ItemDifficultyID;
        	_ItemDifficultyName = ItemDifficultyName;
        	_subjectName = subjectName;
        }

        public int ItemCount
        {
            set
            {
                _ItemCount = value;
            }
            get
            {
                return _ItemCount;
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

        public int RandomExamStrategyId
        {
            set
            {
                _RandomExamStrategyId = value;
            }
            get
            {
                return _RandomExamStrategyId;
            }
        }

        public int SubjectId
        {
            set
            {
                _SubjectId = value;
            }
            get
            {
                return _SubjectId;
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

    	public int ItemDifficultyID
    	{
    		set { _ItemDifficultyID = value;}
			get { return _ItemDifficultyID; }
    	}

    	public string ItemDifficultyName
    	{
    		set { _ItemDifficultyName = value;}
			get { return _ItemDifficultyName; }
    	}

        public int MaxItemDifficultyID
        {
            set { _MaxItemDifficultyID = value; }
            get { return _MaxItemDifficultyID; }
        }

        public string MaxItemDifficultyName
        {
            set { _MaxItemDifficultyName = value; }
            get { return _MaxItemDifficultyName; }
        }

    	public  string SubjectName
    	{
    		set { _subjectName = value;}
			get { return _subjectName; }
    	}

        public bool IsMotherItem
        {
            set { _isMotherItem = value; }
            get { return _isMotherItem; }
        }

        public int MotherID
        {
            set { _motherID = value; }
            get { return _motherID; }
        }

        public int TotalItemCount
        {
            set { _totalItemCount = value; }
            get { return _totalItemCount; }
        }

        public int SelectCount
        {
            set { _selectCount = value; }
            get { return _selectCount; }
        }
    }
}
