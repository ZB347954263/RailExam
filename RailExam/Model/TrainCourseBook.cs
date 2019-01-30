using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class TrainCourseBook
    {
        /// <summary>
        /// 成员变量
        /// </summary>
        private int _trainCourseBookChapterID = 0;
        private int _courseID = 0;
        private int _bookID = 0;
        private string _bookName = string.Empty;
        private string _chapterID = string.Empty;
        private string _chapterName = string.Empty;
        private string _studyDemand = string.Empty;
        private decimal  _studyHours = 0;
        private string _memo = string.Empty;
        private int _parentID = 0;

        public int TrainCourseBookChapterID
        {
            get { return _trainCourseBookChapterID; }
            set { _trainCourseBookChapterID = value; }
        }

        public int CourseID
        {
            get { return _courseID; }
            set { _courseID = value; }
        }

        public int BookID
        {
            get { return _bookID; }
            set { _bookID = value; }
        }

        public string BookName
        {
            get { return _bookName; }
            set { _bookName = value; }
        }

        public string ChapterID
        {
            get { return _chapterID; }
            set { _chapterID = value; }
        }

        public string ChapterName
        {
            get { return _chapterName; }
            set { _chapterName = value; }
        }

        public string StudyDemand
        {
            get { return _studyDemand; }
            set { _studyDemand = value; }
        }

        public decimal  StudyHours
        {
            get { return _studyHours;}
            set { _studyHours = value;}
        }

        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        public int ParentID
        {
            get { return _parentID; }
            set { _parentID = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public TrainCourseBook () { }

        public TrainCourseBook(int? trainCourseBookChapterID,
                               int? courseID,
                               int? bookID,
                               string bookName,
                               string chapterID,
                               string chapterName,
                               string studyDemand,
                               decimal? studyHours,
                               string memo,
                               int? parentID)
        {
            _trainCourseBookChapterID = trainCourseBookChapterID ?? _trainCourseBookChapterID;
            _courseID = courseID ?? _courseID;
            _bookID = bookID ?? _bookID;
            _bookName = bookName;
            _chapterID = chapterID;
            _chapterName = chapterName;
            _studyDemand = studyDemand;
            _studyHours = studyHours ?? _studyHours;
            _memo = memo;
            _parentID = parentID ?? _parentID;
        }
    }
}
