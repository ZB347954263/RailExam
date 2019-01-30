using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class BookTrainType
    {
        /// <summary>
        /// 内部成员变量
        /// </summary>
        private int _bookId = 0;
        private int _trainTypeID = 0;
        private string _trainTypeName = string.Empty;
        private int _orderIndex = 1;

        public int BookID
        {
            set { _bookId = value; }
            get { return _bookId; }
        }

        public int TrainTypeID
        {
            set { _trainTypeID = value; }
            get { return _trainTypeID;}
        }

        public string TrainTypeName
        {
            set { _trainTypeName = value; }
            get { return _trainTypeName; }
        }

        public int OrderIndex
        {
            set { _orderIndex = value;}
            get { return _orderIndex; }
        }

        public BookTrainType() { }

        public BookTrainType(
            int? bookID,
            int? trainTypeID,
            string trainTypeName,
            int? orderIndex)
        {
            _bookId = bookID ?? _bookId;
            _trainTypeID = trainTypeID ?? _trainTypeID;
            _trainTypeName = trainTypeName;
            _orderIndex = orderIndex ?? _orderIndex;
        }
    }
}
