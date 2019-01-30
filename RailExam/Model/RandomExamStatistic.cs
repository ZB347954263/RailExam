using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	public class RandomExamStatistic
	{
		private int _itemID = 0;
		private int _year = 2007;
		private string _content = string.Empty;
		private string _bookName = string.Empty;
		private string _chapterName = string.Empty;
		private int _errorNum = 0;
		private int _examCount = 0;
		private string _errorRate = "0.00%";
		private int _employeeID = 0; 
		private string _employeeName = string.Empty;
		private string _workNo = string.Empty;
		private string _orgName = string.Empty;
		private string _examName = string.Empty;
		private string _answer = string.Empty;
        private string _standardanswer = string.Empty;
		private decimal _score = 0;
		private int _randomExamReulstID = 0;
		private int _orgID = 0;
	    private int _random_exam_item_id = 0;
	    private int _random_exam_id = -1;
	    private string _selectAnswer;

        public string SelectAnswer
        {
            get { return _selectAnswer; }
            set { _selectAnswer = value; }
        }

        public string StandardAnswer
	    {
            get { return _standardanswer; }
            set { _standardanswer = value; }
	    }

	    public int RandomExamID
	    {
            get { return _random_exam_id; }
            set { _random_exam_id = value; }
	    }

		public int ItemID
		{
			get { return _itemID; }
			set { _itemID = value;}
		}

        public int RandomExamItemID
        {
            get { return _random_exam_item_id; }
            set { _random_exam_item_id = value; }
        }

		public int Year
		{
			get { return _year; }
			set { _year = value;}
		}

		public string Content
		{
			get { return _content; }
			set { _content = value; }
		}

		public string  BookName
		{
			get { return _bookName; }
			set { _bookName = value;}
		}

		public string ChapterName
		{
			get { return _chapterName; }
			set { _chapterName = value;}
		}

		public int ErrorNum
		{
			get { return _errorNum; }
			set { _errorNum = value;}
		}

		public int ExamCount
		{
			get { return _examCount; }
			set { _examCount = value;}
		}

		public string ErrorRate
		{
			get { return _errorRate; }
			set { _errorRate = value;}
		}

		public int EmployeeID
		{
			get { return _employeeID; }
			set { _employeeID = value;}
		}

		public string EmployeeName
		{
			get { return _employeeName; }
			set { _employeeName = value;}
		}

		public string WorkNo
		{
			get { return _workNo; }
			set { _workNo = value;}
		}

		public string OrgName
		{
			get { return _orgName; }
			set { _orgName = value;}
		}

		public string ExamName
		{
			get { return _examName; }
			set { _examName = value;}
		}

		public string Answer
		{
			get { return _answer; }
			set { _answer = value;}
		}

		public decimal  Score
		{
			get { return _score; }
			set { _score = value; }
		}

		public int RandomExamResultID
		{
			get { return _randomExamReulstID; }
			set { _randomExamReulstID = value; }
		}

		public int OrgID
		{
			get { return _orgID; }
			set { _orgID = value;}
		}

		public  RandomExamStatistic()
		{
		}

		public RandomExamStatistic(int? itemID,string content,string bookName,string chapterName,int? errorNum,int? examCount, string errorRate)
		{
			_itemID = itemID ?? _itemID;
			_content = content;
			_bookName = bookName;
			_chapterName = chapterName;
			_errorNum = errorNum ?? _errorNum;
			_examCount = examCount ?? _examCount;
			_errorRate = errorRate;
		}
	}
}
