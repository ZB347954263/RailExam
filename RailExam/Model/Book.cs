using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace RailExam.Model
{
    public class Book
    {
        /// <summary>
		/// 内部成员变量
		/// </summary>
		private int _bookId = 0;
        private string _bookName = string.Empty;
		private int _knowledgeId = 0;
        private string _knowledgeName = string.Empty;
		private string _bookNo = string.Empty;
		private int _publishOrg = 0;
        private string _publishOrgName = string.Empty;
		private int _pageCount = 0;
        private int _wordCount = 0;
		private string _description = string.Empty;
		private string _memo = string.Empty;
        private DateTime _publishDate = new DateTime();
        private string _authors = string.Empty;
        private string _keyWords = string.Empty;
        private string _revisers = string.Empty;
        private string _bookmaker = string.Empty;
        private string _coverDesigner = string.Empty;
        private string _url = string.Empty;
        private ArrayList _orgidAL = new ArrayList();
        private ArrayList _postidAL = new ArrayList();
        private ArrayList _trainTypeidAL = new ArrayList();
        private string _trainTypeNames = string.Empty;
        private string _knowledgeNames = string.Empty;
        private string _toolTip = string.Empty;
        private int  _isGroupLeader = 2;
        private int _technicianTypeID = 1;
        private int _orderIndex = 1;
        private int _version = 1;
        private string _authorsName = string.Empty;

        public Book() { }

         /// <summary>
		/// 带参数的构造函数
		/// </summary>
		/// <param name="bookId"></param>
		/// <param name="knowledgeId"></param>
		/// <param name="bookNo"></param>
		/// <param name="publishOrg"></param>
        /// <param name="publishDate"></param>
		/// <param name="pageCount"></param>
        /// <param name="wordCount"></param>
        /// <param name="authors"></param>
        /// <param name="keyWords"></param>
        /// <param name="revisers"></param>
        /// <param name="bookmaker"></param>
        /// <param name="coverDesigner"></param>
        /// <param name="url"></param>
		/// <param name="bookName"></param>		
		/// <param name="description">简介</param>
		/// <param name="memo">备注</param>
        /// <param name="technicianTypeID">技师类别</param>
        /// <param name="isGroupLeader">班组长</param>
        public Book(
			int? bookId,
			string bookName,
            int? knowledgeId, 
            string knowledgeName,
			string bookNo,
			int? publishOrg,
            string publishOrgName,
			int? pageCount,
            int? wordCount,
			string description,
            string memo,
            DateTime? publishDate,
            string authors,
            string keyWords, 
            string revisers,
            string bookmaker,
            string coverDesigner,
            string url,
            int? isGroupLeader,
            int? technicianTypeID,
            int? orderIndex)
		{
			_bookId = bookId ?? _bookId;
            _bookName = bookName;
			_knowledgeId = knowledgeId ?? _knowledgeId;
            _knowledgeName = knowledgeName;
			_bookNo = bookNo;
			_publishOrg = publishOrg ?? _publishOrg;
            _publishOrgName = publishOrgName;
			_pageCount = pageCount ?? _pageCount;
            _wordCount = wordCount ?? _wordCount;
			_description = description;
			_memo = memo;
            _publishDate = publishDate ?? _publishDate;
            _authors = authors;
            _keyWords = keyWords;
            _revisers = revisers;
            _bookmaker = bookmaker;
            _coverDesigner = coverDesigner;
            _url = url;
            _isGroupLeader = isGroupLeader ?? _isGroupLeader;
            _technicianTypeID = technicianTypeID ?? _technicianTypeID;
             _orderIndex = orderIndex ?? _orderIndex;
        }   

		public int bookId
		{
			set
			{
				_bookId = value;
			}
			get
			{
				return _bookId;
			}
		}

        public string bookName
        {
            set
            {
                _bookName = value;
            }
            get
            {
                return _bookName;
            }
        }
		
		public int knowledgeId
		{
			set
			{
                _knowledgeId = value;
			}
			get
			{
				return _knowledgeId;
			}
		}

        public string knowledgeName
        {
            set
            {
                _knowledgeName = value;
            }
            get
            {
                return _knowledgeName;
            }
        }

		public string bookNo
		{
			set
			{
				_bookNo = value;
			}
			get
			{
				return _bookNo;
			}
		}

		public int publishOrg
		{
			set
			{
                _publishOrg = value;
			}
			get
			{
				return _publishOrg;
			}
		}

        public string publishOrgName
		{
			set
			{
                _publishOrgName = value;
			}
			get
			{
				return _publishOrgName;
			}
		}

		public int pageCount
		{
			set
			{
				_pageCount = value;
			}
			get
			{
				return _pageCount;
			}
		}

        public int wordCount
        {
            set
            {
                _wordCount = value;
            }
            get
            {
                return _wordCount;
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

        public DateTime publishDate
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

        public string authors
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

        public string keyWords
        {
            set
            {
                _keyWords = value;
            }
            get
            {
                return _keyWords;
            }
        }

        public string revisers
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

        public string url
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

        public string coverDesigner
        {
            set
            {
                _coverDesigner = value;
            }
            get
            {
                return _coverDesigner;
            }
        }

        public string bookmaker
        {
            set
            {
                _bookmaker = value;
            }
            get
            {
                return _bookmaker;
            }
        }
        
        public ArrayList orgidAL
        {
            set
            {
                _orgidAL = value;
            }
            get
            {
                return _orgidAL;
            }
        }

        public ArrayList postidAL
        {
            set
            {
                _postidAL = value;
            }
            get
            {
                return _postidAL;
            }
        }

        public ArrayList trainTypeidAL
        {
            set
            {
                _trainTypeidAL = value;
            }
            get
            {
                return _trainTypeidAL;
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

        public string KnowledgeNames
        {
            set
            {
                _knowledgeNames = value;
            }
            get
            {
                return _knowledgeNames;
            }
        }

        public string trainTypeNames
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

        public  int Version
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

        public string AuthorsName
        {
            set { _authorsName = value; }
            get { return _authorsName; }
        }
    }
}
