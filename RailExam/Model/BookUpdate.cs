using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace RailExam.Model
{
    public class BookUpdate
    {
        private int _bookId = 0;
        private int _bookUpdateId = 0;
        private int _chapterId =0;
        private DateTime _updateDate = DateTime.Now;      
        private string _updateCause= string.Empty;
        private string _updateContent = string.Empty;
        private string _memo = string.Empty;
        private string _updatePerson = string.Empty;
        private string _bookName = string.Empty;
        private string _chapterName = string.Empty;
        private string _updateObject = string.Empty;
        private string _bookNameBak = string.Empty;
        private string _chapterNameBak = string.Empty;
    
        public BookUpdate() { }

        public BookUpdate(
            int? bookId,
            int? bookUpdateId,
            DateTime? updateDate,
            int? chapterId,
            string updateCause,
            string updateContent,
            string updatePerson,
            string memo,
            string bookName,
            string chapterName,
            string updateobject,
            string bookNameBak,
            string chapterNameBak)
        {
            _bookId = bookId ?? _bookId;
            _chapterId = chapterId ?? _chapterId;
            _bookUpdateId = bookUpdateId ?? _bookUpdateId;
            _updateDate = updateDate ?? _updateDate; 
            _updateCause = updateCause;
            _updatePerson = updatePerson;
            _updateContent = updateContent;
            _memo = memo;
            _bookName = bookName;
            _chapterName = chapterName;
            _updateObject = updateobject;
            _bookNameBak = bookNameBak;
            _chapterNameBak = chapterNameBak;
        }

        public int BookId
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

        public int ChapterId
        {
            set
            {
                _chapterId = value;
            }
            get
            {
                return _chapterId;
            }
        }

        public DateTime updateDate
        {
            set
            {
                _updateDate = value;
            }
            get
            {
                return _updateDate;
            }
        }

        public int bookUpdateId
        {
            set
            {
                _bookUpdateId = value;
            }
            get
            {
                return _bookUpdateId;
            }
        }

        public string updateCause
        {
            set
            {
                _updateCause = value;
            }
            get
            {
                return _updateCause;
            }
        }

        public string updatePerson
        {
            set
            {
                _updatePerson = value;
            }
            get
            {
                return _updatePerson;
            }
        }

        public string updateContent
        {
            set
            {
                _updateContent = value;
            }
            get
            {
                return _updateContent;
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

        public string BookName
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

        public string ChapterName
        {
            set
            {
                _chapterName = value;
            }
            get
            {
                return _chapterName;
            }
        }

        public string UpdateObject
        {
            set
            {
                _updateObject = value;
            }
            get
            {
                return _updateObject;
            }
        }

        public string BookNameBak
        {
            set
            {
                _bookNameBak = value;
            }
            get
            {
                return _bookNameBak;
            }
        }
        public string ChapterNameBak
        {
            set
            {
                _chapterNameBak = value;
            }
            get
            {
                return _chapterNameBak;
            }
        }
    }
}
