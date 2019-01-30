using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    /// <summary>
    /// ҵ��ʵ�壺��ɫȨ��
    /// </summary>
    public class SystemRoleRight
    {
        /// <summary>
        /// �ڲ���Ա����
        /// </summary>
        private int _roleID = 1;
        private string _functionID = string.Empty;
        private string _functionName = string.Empty;
        private int _functionRight = 0;
		private int _rangeRight = 0;
        /// <summary>
		/// ȱʡ���캯��
		/// </summary>
        public SystemRoleRight() {}

        /// <summary>
        /// �������Ĺ��캯��
        /// </summary>
        /// <param name="roleID"></param>
        /// <param name="functionID"></param>
        /// <param name="functionName"></param>
        /// <param name="functionRight"></param>
        public SystemRoleRight(
            int? roleID,
            string functionID,
            string functionName,
			int? functionRight,int? rangeRight
			)
        {
            _roleID = roleID ?? _roleID;
            _functionID = functionID;
            _functionName = functionName;
            _functionRight = functionRight ?? _functionRight;
			_rangeRight = rangeRight ?? _rangeRight;
        }

        public int RoleID
        {
            set
            {
                _roleID = value;
            }
            get
            {
                return _roleID;
            }
        }

        public string FunctionID
        {
            set
            {
                _functionID = value;
            }
            get
            {
                return _functionID;
            }
        }

		public string FunctionName
		{
			set
			{
				_functionName = value;
			}
			get
			{
				return _functionName;
			}
		}

        public int FunctionRight
        {
            set
            {
                _functionRight = value;
            }
            get
            {
                return _functionRight;
            }
        }
		public int RangeRight
		{
			get { return _rangeRight; }
			set { _rangeRight = value; }
		}
    }
}
