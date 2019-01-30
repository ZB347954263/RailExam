using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    /// <summary>
    /// 业务实体：日志
    /// </summary>
    public class SystemLog
    {
        /// <summary>
        /// 内部成员变量
        /// </summary>
        private int _logID = 1;
        private DateTime _actionTime = new DateTime();
        private string _actionOrgName = string.Empty;
        private string _actionUserID = string.Empty;
        private string _actionEmployeeName = string.Empty;
        private string _actionContent = string.Empty;
        private string _memo = string.Empty;
        
        /// <summary>
		/// 缺省构造函数
		/// </summary>
        public SystemLog(){}

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="logID"></param>
        /// <param name="actionTime"></param>
        /// <param name="actionOrgName"></param>
        /// <param name="actionUserID"></param>
        /// <param name="actionEmployeeName"></param>
        /// <param name="actionContent"></param>
        /// <param name="memo"></param>
        public SystemLog(
            int? logID,
            DateTime? actionTime,
            string actionOrgName,
            string actionUserID,
            string actionEmployeeName,
            string actionContent,
			string memo)
        {
            _logID = logID ?? _logID;
            _actionTime = actionTime ?? _actionTime;
            _actionOrgName = actionOrgName;
            _actionUserID = actionUserID;
            _actionEmployeeName = actionEmployeeName;
            _actionContent = actionContent;
            _memo = memo;
        }

        public int LogID
        {
            set
            {
                _logID = value;
            }
            get
            {
                return _logID;
            }
        }

        public DateTime ActionTime
        {
            set
            {
                _actionTime = value;
            }
            get
            {
                return _actionTime;
            }
        }

        public string ActionOrgName
        {
            set
            {
                _actionOrgName = value;
            }
            get
            {
                return _actionOrgName;
            }
        }

        public string ActionUserID
        {
            set
            {
                _actionUserID = value;
            }
            get
            {
                return _actionUserID;
            }
        }

        public string ActionEmployeeName
        {
            set
            {
                _actionEmployeeName = value;
            }
            get
            {
                return _actionEmployeeName;
            }
        }

        public string ActionContent
        {
            set
            {
                _actionContent = value;
            }
            get
            {
                return _actionContent;
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
