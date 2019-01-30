using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class ExamType
    {
        #region 内部成员

        private int _ExamTypeId = 0;
        private int _IsDefault = 0;       
        private string _TypeName = string.Empty;
        private string _description = string.Empty;
        private string _memo = string.Empty;

        #endregion


        public ExamType() { }

        public ExamType(
            int? ExamTypeId,
            int? IsDefault,      
            string TypeName,
            string description,
            string memo)
        {
            _ExamTypeId = ExamTypeId ?? _ExamTypeId;
            _IsDefault = IsDefault ?? _IsDefault; 
            _TypeName = TypeName;
            _description = description;
            _memo = memo;
        }

        #region 属性

        /// <summary>
        /// 考试类型编号
        /// </summary>
        public int ExamTypeId
        {
            set
            {
                _ExamTypeId = value;
            }
            get
            {
                return _ExamTypeId;
            }
        }

        /// <summary>
        /// 是否默认考试类型
        /// </summary>
        public int IsDefault
        {
            set
            {
                _IsDefault = value;
            }
            get
            {
                return _IsDefault;
            }
        }

        /// <summary>
        /// 考试类型名称
        /// </summary>
        public string  TypeName
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

        /// <summary>
        /// 考试类型描述
        /// </summary>
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

        /// <summary>
        /// 考试类型备注
        /// </summary>
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

        #endregion

    }
}
