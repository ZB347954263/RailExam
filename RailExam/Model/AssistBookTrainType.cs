using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class AssistBookTrainType
    {
                /// <summary>
        /// 内部成员变量
        /// </summary>
        private int _assistBookId = 0;
        private int _trainTypeID = 0;
        private string _trainTypeName = string.Empty;

        public int AssistBookID
        {
            set { _assistBookId = value; }
            get { return _assistBookId; }
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

        public AssistBookTrainType() { }

        public AssistBookTrainType(
            int? assistbookID,
            int? trainTypeID,
            string trainTypeName)
        {
            _assistBookId = assistbookID ?? _assistBookId;
            _trainTypeID = trainTypeID ?? _trainTypeID;
            _trainTypeName = trainTypeName;
        }
    }
}
