using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace RailExam.Model
{
    public class Courseware
    {
        private int _coursewareID = 0;
		private string _coursewareName = string.Empty;
        private int _coursewareTypeID = 1;
        private string _coursewareTypeName = string.Empty;
		private int _provideOrg = 0;
        private string _provideOrgName = string.Empty;
		private DateTime _publishDate = new DateTime();
        private string _authors = string.Empty;
        private string _revisers = string.Empty;
        private string _keyWord = string.Empty;
		private string _description = string.Empty;
        private string _url = string.Empty;
        private string _memo = string.Empty;
        private ArrayList _trainTypeIDAL = new ArrayList();
        private ArrayList _orgIDAL = new ArrayList();
        private ArrayList _postIDAL = new ArrayList();
        private string _trainTypeNames = string.Empty;
        private string _coursewareTypeNames = string.Empty;
        private string _toolTip = string.Empty;
        private int  _isGroupLeader = 2;
        private int _technicianTypeID = 1;
        private int _orderIndex = 1;

        public Courseware() { }

        public Courseware(
			int? coursewareID,
			string coursewareName,
            int? coursewareTypeID,
            string coursewareTypeName,
			int? provideOrg,
            string provideOrgName,
			string description,
            string memo,
            DateTime? publishDate,
            string authors,
            string keyWord,
            string revisers,
            string url,
            int? isGroupLeader,
            int? technicianTypeID,
            int? orderIndex)
		{
			_coursewareID = coursewareID ?? _coursewareID;
			_coursewareName = coursewareName;
            _coursewareTypeID = coursewareTypeID ?? _coursewareTypeID;
            _coursewareTypeName = coursewareTypeName;
			_provideOrg = provideOrg ?? _provideOrg;
            _provideOrgName = provideOrgName;
			_description = description;
			_memo = memo;
            _publishDate = publishDate ?? _publishDate;
            _authors = authors;
            _keyWord = keyWord;
            _revisers = revisers;
            _url = url;
            _isGroupLeader = isGroupLeader ?? _isGroupLeader;
            _technicianTypeID = technicianTypeID ?? _technicianTypeID;
            _orderIndex = orderIndex ?? _orderIndex;
        }

        public int CoursewareID
        {
            set
            {
                _coursewareID = value;
            }
            get
            {
                return _coursewareID;
            }
        }

        public string CoursewareName
        {
            set
            {
                _coursewareName = value;
            }
            get
            {
                return _coursewareName;
            }
        }

        public int CoursewareTypeID
        {
            set
            {
                _coursewareTypeID = value;
            }
            get
            {
                return _coursewareTypeID;
            }
        }

        public string CoursewareTypeName
        {
            set
            {
                _coursewareTypeName = value;
            }
            get
            {
                return _coursewareTypeName;
            }
        }

        public int ProvideOrg
        {
            set
            {
                _provideOrg = value;
            }
            get
            {
                return _provideOrg;
            }
        }

        public string ProvideOrgName
        {
            set
            {
                _provideOrgName = value;
            }
            get
            {
                return _provideOrgName;
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

        public DateTime PublishDate
        {
            set
            {
                _publishDate = value;
            }
            get
            {
                return _publishDate;
            }
        }
        public string Authors
        {
            set
            {
                _authors = value;
            }
            get
            {
                return _authors;
            }
        }
        public string KeyWord
        {
            set
            {
                _keyWord = value;
            }
            get
            {
                return _keyWord;
            }
        }

        public string Revisers
        {
            set
            {
                _revisers = value;
            }
            get
            {
                return _revisers;
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

        public ArrayList TrainTypeIDAL
        {
            set
            {
                _trainTypeIDAL = value;
            }
            get
            {
                return _trainTypeIDAL;
            }
        }

        public ArrayList OrgIDAL
        {
            set
            {
                _orgIDAL = value;
            }
            get
            {
                return _orgIDAL;
            }
        }

        public ArrayList PostIDAL
        {
            set
            {
                _postIDAL = value;
            }
            get
            {
                return _postIDAL;
            }
        }

        public string TrainTypeNames
        {
            set
            {
                _trainTypeNames = value;
            }
            get
            {
                return _trainTypeNames;
            }
        }

        public string ToolTip
        {
            set
            {
                _toolTip = value;
            }
            get
            {
                return _toolTip;
            }
        }

        public string CoursewareTypeNames
        {
            set
            {
                _coursewareTypeNames = value;
            }
            get
            {
                return _coursewareTypeNames;
            }
        }

        public int IsGroupLearder
        {
            set
            {
                _isGroupLeader = value;
            }
            get
            {
                return _isGroupLeader;
            }
        }

        public int TechnicianTypeID
        {
            set
            {
                _technicianTypeID = value;
            }
            get
            {
                return _technicianTypeID;
            }
        }

        public int OrderIndex
        {
            set
            {
                _orderIndex = value;
            }
            get
            {
                return _orderIndex;
            }
        }
    }
}

