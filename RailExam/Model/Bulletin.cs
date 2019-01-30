using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    /// <summary>
    /// 业务实体：信息公告
    /// </summary>
    public class Bulletin
    {
        /// <summary>
        /// 内部成员变量
        /// </summary>
        private int _bulletinID = 1;
        private string _title = string.Empty;
        private string _content = string.Empty;
        private int _importanceID = 1;
        private string _importanceName = string.Empty;
        private int _dayCount = 1;
        private int _createPersonID = 1;
        private string _employeeName = string.Empty;
        private string _orgName = string.Empty;
        private DateTime _createTime = new DateTime();
        private string _memo = string.Empty;

        /// <summary>
        /// 缺省构造函数
        /// </summary>
        public Bulletin() { }

        public Bulletin(
            int? bulletinID,
            string title,
            string content,
            int? importanceID,
            string importanceName,
            int? dayCount,
            int? createPersonID,
            string employeeName,
            string orgName,
            DateTime? createTime,
            string memo)
        {
            _bulletinID = bulletinID ?? _bulletinID;
            _title = title;
            _content = content;
            _importanceID = importanceID ?? _importanceID;
            _importanceName = importanceName;
            _dayCount = dayCount ?? _dayCount;
            _createPersonID = createPersonID ?? _createPersonID;
            _employeeName = employeeName;
            _orgName = orgName;
            _createTime = createTime ?? _createTime;
            _memo = memo;
        }

        public int BulletinID
        {
            set
            {
                _bulletinID = value;
            }
            get
            {
                return _bulletinID;
            }
        }

        public string Content
        {
            set
            {
                _content = value;
            }
            get
            {
                return _content;
            }
        }

        public string Title
        {
            set
            {
                _title = value;
            }
            get
            {
                return _title;
            }
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

        public int DayCount
        {
            set
            {
                _dayCount = value;
            }
            get
            {
                return _dayCount;
            }
        }

        public int CreatePersonID
        {
            set
            {
                _createPersonID = value;
            }
            get
            {
                return _createPersonID;
            }
        }

        public string EmployeeName
        {
            set
            {
                _employeeName = value;
            }
            get
            {
                return _employeeName;
            }
        }

        public string OrgName
        {
            set
            {
                _orgName = value;
            }
            get
            {
                return _orgName;
            }
        }

        public DateTime CreateTime
        {
            set
            {
                _createTime = value;
            }
            get
            {
                return _createTime;
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
