using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class TrainCourseCourseware
    {
        /// <summary>
        /// 成员变量
        /// </summary>
        private int _trainCourseCoursewareID = 0;
        private int _courseID = 0;
        private int _coursewareID = 0;
        private string _coursewareName = string.Empty;
        private string _studyDemand = string.Empty;
        private decimal  _studyHours = 0;
        private string _memo = string.Empty;
        private int _coursewareTypeID = 0;

        public int TrainCourseCoursewareID
        {
            get { return _trainCourseCoursewareID; }
            set { _trainCourseCoursewareID = value; }
        }

        public int CourseID
        {
            get { return _courseID; }
            set { _courseID = value; }
        }

        public int CoursewareID
        {
            get { return _coursewareID; }
            set { _coursewareID = value; }
        }

        public string CoursewareName
        {
            get { return _coursewareName; }
            set { _coursewareName = value; }
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

        public int CoursewareTypeID
        {
            get { return _coursewareTypeID; }
            set { _coursewareTypeID = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public TrainCourseCourseware() { }

        public TrainCourseCourseware(int? trainCourseCoursewareID,
                               int? courseID,
                               int? coursewareID,
                               string coursewareName,
                               string studyDemand,
                               decimal? studyHours,
                               string memo,
                               int? coursewareTypeID)
        {
            _trainCourseCoursewareID = trainCourseCoursewareID ?? _trainCourseCoursewareID;
            _courseID = courseID ?? _courseID;
            _coursewareID = coursewareID ?? _coursewareID;
            _coursewareName = coursewareName;
            _studyDemand = studyDemand;
            _studyHours = studyHours ?? _studyHours;
            _memo = memo;
            _coursewareTypeID = coursewareTypeID ?? _coursewareTypeID;
        }
    }
}
