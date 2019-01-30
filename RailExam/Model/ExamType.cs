using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class ExamType
    {
        #region �ڲ���Ա

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

        #region ����

        /// <summary>
        /// �������ͱ��
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
        /// �Ƿ�Ĭ�Ͽ�������
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
        /// ������������
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
        /// ������������
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
        /// �������ͱ�ע
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
