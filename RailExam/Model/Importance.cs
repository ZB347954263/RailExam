using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    /// <summary>
    /// ҵ��ʵ�壺�����̶�
    /// </summary>
    public class Importance
    {
        /// <summary>
        /// �ڲ���Ա����
        /// </summary>
        private int _importanceID = 1;
        private string _importanceName = string.Empty;
        private string _description = string.Empty;
        private bool _isDefault = false;
        private string _memo = string.Empty;

        /// <summary>
        /// ȱʡ���캯��
        /// </summary>
        public Importance() { }

        public Importance(
            int? importanceID,
            string importanceName,
            string description,
            bool? isDefault,
            string memo)
        {
            _importanceID = importanceID ?? _importanceID;
            _importanceName = importanceName;
            _description = description;
            _isDefault = isDefault ?? _isDefault;
            _memo = memo;
        }

        public int ImportanceID
        {
            set
            {
                _importanceID = value;
            }
            get
            {
                return _importanceID;
            }
        }

        public string ImportanceName
        {
            set
            {
                _importanceName = value;
            }
            get
            {
                return _importanceName;
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

        public bool IsDefault
        {
            set
            {
                _isDefault = value;
            }
            get
            {
                return _isDefault;
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
