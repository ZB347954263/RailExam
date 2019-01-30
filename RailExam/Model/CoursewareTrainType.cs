using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class CoursewareTrainType
    {
        /// <summary>
        /// 内部成员变量
        /// </summary>
        private int _coursewareId = 0;
        private int _trainTypeID = 0;
        private string _trainTypeName = string.Empty;
        private int _orderIndex = 1;

        public int CoursewareID
        {
            set { _coursewareId = value; }
            get { return _coursewareId; }
        }

        public int TrainTypeID
        {
            set { _trainTypeID = value; }
            get { return _trainTypeID; }
        }

        public string TrainTypeName
        {
            set { _trainTypeName = value; }
            get { return _trainTypeName; }
        }

        public int OrderIndex
        {
            set { _orderIndex = value; }
            get { return _orderIndex; }
        }

        public CoursewareTrainType() { }

        public CoursewareTrainType(
            int? coursewareID,
            int? trainTypeID,
            string trainTypeName,
            int? orderIndex)
        {
            _coursewareId = coursewareID ?? _coursewareId;
            _trainTypeID = trainTypeID ?? _trainTypeID;
            _trainTypeName = trainTypeName;
            _orderIndex = orderIndex ?? _orderIndex;
        }
    }
}
