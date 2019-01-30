using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace RailExam.Model
{
    public class PaperStrategy
    {
        private int _paperStrategyId = 0;
        private int _paperCategoryId = 1;
        private bool _isRandomOrder = false;
        private bool _singleAsMultiple = false;
        private int _strategyMode = 1;
        private string _paperStrategyName = string.Empty;
        private string _categoryName = string.Empty;
        private string _categoryNames = string.Empty;
        private string _description = string.Empty;
        private string _memo = string.Empty; 

        public PaperStrategy() { }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>

        public PaperStrategy(
            int? paperStrategyId,
            int? paperCategoryId,
            bool? isRandomOrder,
            bool? singleAsMultiple,
            int? strategyMode,
            string paperStrategyName,
            string categoryName,
            string description,
            string memo)
        {
            _paperStrategyId = paperStrategyId ?? _paperStrategyId;
            _paperCategoryId = paperCategoryId ?? _paperCategoryId;
            _isRandomOrder = isRandomOrder ?? _isRandomOrder;
            _singleAsMultiple = singleAsMultiple ?? _singleAsMultiple;
            _strategyMode = strategyMode ?? _strategyMode;
            _paperStrategyName = paperStrategyName;
            _categoryName = categoryName;
            _description = description;
            _memo = memo;
        }

        public int PaperStrategyId
        {
            set
            {
                _paperStrategyId = value;
            }
            get
            {
                return _paperStrategyId;
            }
        }

        public int PaperCategoryId
        {
            set
            {
                _paperCategoryId = value;
            }
            get
            {
                return _paperCategoryId;
            }
        }

        public bool IsRandomOrder
        {
            set
            {
                _isRandomOrder = value;
            }
            get
            {
                return _isRandomOrder;
            }
        }

        public bool SingleAsMultiple
        {
            set
            {
                _singleAsMultiple = value;
            }
            get
            {
                return _singleAsMultiple;
            }
        }

        public int StrategyMode
        {
            set
            {
                _strategyMode = value;
            }
            get
            {
                return _strategyMode;
            }
        }

        public string PaperStrategyName
        {
            set
            {
                _paperStrategyName = value;
            }
            get
            {
                return _paperStrategyName;
            }
        }

        public string CategoryName
        {
            set
            {
                _categoryName = value;
            }
            get
            {
                return _categoryName;
            }
        }

        public string CategoryNames
        {
            set
            {
                _categoryNames = value;
            }
            get
            {
                return _categoryNames;
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
    }
}
