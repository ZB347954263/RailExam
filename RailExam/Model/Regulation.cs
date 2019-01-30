using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace RailExam.Model
{
    public class Regulation
    {
        private int _regulationID = 0;
        private int _categoryID = 1;
        private string _categoryName = string.Empty;
        private string _regulationNo = string.Empty;
        private string _regulationName = string.Empty;
        private string _version = string.Empty;
        private string _fileNo = string.Empty;
        private string _titleRemark = string.Empty;
        private DateTime _issueDate = new DateTime();
        private DateTime _executeDate = new DateTime();
        private int _status = 0;
        private string _url = string.Empty;
        private string _memo = string.Empty;
       
        public Regulation() {}

        public Regulation(
            int? regulationID,
            int? categoryID,
            string categoryName,
            string regulationNo,
            string regulationName,
            string version,
            string fileNo,
            string titleRemark,
            DateTime? issueDate,
            DateTime? executeDate,
            int? status,
            string url,
            string memo)
        {
            _regulationID = regulationID ?? _regulationID;
            _categoryID = categoryID ?? _categoryID;
            _categoryName = categoryName;
            _regulationNo = regulationNo;
            _regulationName = regulationName;
            _version = version ?? _version;
            _fileNo = fileNo;
            _titleRemark = titleRemark;
            _issueDate = issueDate ?? _issueDate;
            _executeDate = executeDate ?? _executeDate;
            _status = status ?? _status;
            _url = url;
            _memo = memo;
        }

        public int RegulationID
        {
            set
            {
                _regulationID = value;
            }
            get
            {
                return _regulationID;
            }
        }

        public int CategoryID
        {
            set
            {
                _categoryID = value;
            }
            get
            {
                return _categoryID;
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

        public string RegulationNo
        {
            set
            {
                _regulationNo = value;
            }
            get
            {
                return _regulationNo;
            }
        }

        public string RegulationName
        {
            set
            {
                _regulationName = value;
            }
            get
            {
                return _regulationName;
            }
        }

        public string Version
        {
            set
            {
                _version = value;
            }
            get
            {
                return _version;
            }
        }

        public string FileNo
        {
            set
            {
                _fileNo = value;
            }
            get
            {
                return _fileNo;
            }
        }

        public string TitleRemark
        {
            set
            {
                _titleRemark = value;
            }
            get
            {
                return _titleRemark;
            }
        }

        public DateTime IssueDate
        {
            set
            {
                _issueDate = value;
            }
            get
            {
                return _issueDate;
            }
        }

        public DateTime ExecuteDate
        {
            set
            {
                _executeDate = value;
            }
            get
            {
                return _executeDate;
            }
        }

        public int Status
        {
            set
            {
                _status = value;
            }
            get
            {
                return _status;
            }
        }

        public string Url
        {
            set
            {
                _url = value;
            }
            get
            {
                return _url;
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


