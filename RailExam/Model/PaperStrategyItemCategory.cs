using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace RailExam.Model
{
    public class PaperStrategyItemCategory
    {       
        private int _StrategyItemCategoryId = 0;
        private int _StrategySubjectId = 0;
        private int _ItemCategoryId = 0;
        private int _ItemTypeId = 0;
        private string _ItemTypeName = string.Empty;
        private decimal _UnitScore = 0;
        private int _UnitLimitTime = 0;
        private string _CategoryName = string.Empty;
        private string _memo = string.Empty;
        private string _ExcludeCategorysId = string.Empty;
        private int _ItemDifficultyRandomCount = 0;
        private int _ItemDifficulty1Count = 0;
        private int _ItemDifficulty2Count = 0;
        private int _ItemDifficulty3Count = 0;
        private int _ItemDifficulty4Count = 0;
        private int _ItemDifficulty5Count = 0;
        private string _SubjectName = string.Empty;
      
        public PaperStrategyItemCategory() { }

        public PaperStrategyItemCategory(
            int? StrategyItemCategoryId,
            int? StrategySubjectId,
            int? ItemCategoryId,
            int? ItemTypeId,
            string ItemTypeName,
            decimal? UnitScore,
            int? UnitLimitTime,
            string CategoryName,
            string memo,
            string ExcludeCategorysId,
            int? ItemDifficultyRandomCount,
            int? ItemDifficulty1Count,
            int? ItemDifficulty2Count,
            int? ItemDifficulty3Count,
            int? ItemDifficulty4Count,
            int? ItemDifficulty5Count,
            string SubjectName)
        {
            _StrategyItemCategoryId = StrategyItemCategoryId ?? _StrategyItemCategoryId;
            _StrategySubjectId = StrategySubjectId ?? _StrategySubjectId;
            _ItemCategoryId = ItemCategoryId ?? _ItemCategoryId;
            _ItemTypeId = ItemTypeId ?? _ItemTypeId;
            _ItemTypeName = ItemTypeName;
            _UnitScore = UnitScore ?? _UnitScore;
            _UnitLimitTime = UnitLimitTime ?? _UnitLimitTime;
            _CategoryName = CategoryName;
            _memo = memo;
            _SubjectName = SubjectName;
            _ExcludeCategorysId = ExcludeCategorysId;
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

        public int StrategyItemCategoryId
        {
            set
            {
                _StrategyItemCategoryId = value;
            }
            get
            {
                return _StrategyItemCategoryId;
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

        public int ItemCategoryId
        {
            set
            {
                _ItemCategoryId = value;
            }
            get
            {
                return _ItemCategoryId;
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

        public string CategoryName
        {
            set
            {
                _CategoryName = value;
            }
            get
            {
                return _CategoryName;
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

        public string ExcludeCategorysId
        {
            set
            {
                _ExcludeCategorysId = value;
            }
            get
            {
                return _ExcludeCategorysId;
            }
        }
    }
}