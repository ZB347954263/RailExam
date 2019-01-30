using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace RailExam.Model
{
   public class PaperStrategySubject
    { 
        private int _PaperStrategyId = 0;
        private int _PaperStrategySubjectId  = 0;
        private int _OrderIndex = 0;
        private string _SubjectName = string.Empty;
        private int _ItemTypeId = 0;    
        private string _TypeName = string.Empty;
        private string _Remark = string.Empty;
        private decimal _TotalScore = 0;
        private int _ItemCount = 0;
        private Decimal _UnitScore = 0;        
        private string _memo = string.Empty;

        public PaperStrategySubject()  { }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        public PaperStrategySubject(
            int? PaperStrategyId,
            int? PaperStrategySubjectId ,
            int? OrderIndex,
            string SubjectName,
            int? ItemTypeId,          
            string TypeName,
            string Remark,
            string memo,
            decimal? TotalScore,
            int? ItemCount,
            Decimal? UnitScore)
        {
            _PaperStrategyId = PaperStrategyId ?? _PaperStrategyId;
            _PaperStrategySubjectId  = PaperStrategySubjectId  ?? _PaperStrategySubjectId ;
            _SubjectName = SubjectName;
            _ItemTypeId = ItemTypeId ?? _ItemTypeId;
            _TypeName = TypeName;
            _Remark = Remark;
            _memo = memo;
            _TotalScore = TotalScore ?? _TotalScore;
            _ItemCount = ItemCount ?? _ItemCount;
            _UnitScore = UnitScore ?? _UnitScore;
            _OrderIndex = OrderIndex ?? _OrderIndex;
        }

        public Decimal UnitScore
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

        public int OrderIndex
        {
            set
            {
                _OrderIndex = value;
            }
            get
            {
                return _OrderIndex;
            }
        }

        public int PaperStrategyId
        {
            set
            {
                _PaperStrategyId = value;
            }
            get
            {
                return _PaperStrategyId;
            }
        }

        public int PaperStrategySubjectId 
        {
            set
            {
                _PaperStrategySubjectId  = value;
            }
            get
            {
                return _PaperStrategySubjectId ;
            }
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

        public decimal TotalScore
        {
            set
            {
                _TotalScore = value;
            }
            get
            {
                return _TotalScore;
            }
        }

        public string TypeName
        {
            set
            {
                _TypeName = value;
            }
            get
            {
                return _TypeName;
            }
        }

        public string Remark
        {
            set
            {
                _Remark = value;
            }
            get
            {
                return _Remark;
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

