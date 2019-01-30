using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class TrainCourse
    {
        /// <summary>
        /// 成员变量
        /// </summary>
        private int _trainCourseID = 0;
        private int _standardID = 0;
        private int _courseNo = 0;
        private string _courseName = string.Empty ;
        private string _description = string.Empty;
        private string _studyDemand = string.Empty;
        private decimal _studyHours = 0;
        private bool _hasExam = true;
        private string _examForm = string.Empty;
        private int _requireCourseID = 0;
        private string _requireCourseName = string.Empty;
        private string _memo = string.Empty;
        private bool _flag = false;
        private string _postName = string.Empty;
        private string _typeName = string.Empty;

        public int TrainCourseID
        {
            get { return _trainCourseID; }
            set { _trainCourseID = value; }
        }

        public int StandardID
        {
            get { return _standardID; }
            set { _standardID = value; }
        }

        public  int CourseNo
        {
            get { return _courseNo; }
            set { _courseNo = value; }
        }

        public string CourseName
        {
            get { return _courseName; }
            set { _courseName = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string StudyDemand
        {
            get { return _studyDemand; }
            set { _studyDemand = value; }
        }

        public decimal  StudyHours
        {
            get { return _studyHours; }
            set { _studyHours = value; }
        }

        public bool  HasExam
        {
            get { return _hasExam; }
            set { _hasExam = value; }
        }

        public string ExamForm
        {
            get { return _examForm; }
            set { _examForm = value; }
        }

        public int RequireCourseID
        {
            get { return _requireCourseID; }
            set { _requireCourseID = value; }
        }

        public string RequireCourseName
        {
            get { return _requireCourseName; }
            set { _requireCourseName = value; }
        }

        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        public bool Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }

        public string PostName
        {
            get { return _postName; }
            set { _postName = value; }
        }

        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        public TrainCourse() { }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="trainCourseID">培训课程ID</param>
        /// <param name="standardID">培训规范ID</param>
        /// <param name="courseNo">课程序号</param>
        /// <param name="courseName">课程名称</param>
        /// <param name="description">内容简要</param>
        /// <param name="studyDemand">学习要求</param>
        /// <param name="studyHours">在线学时</param>
        /// <param name="hasExam">是否考试</param>
        /// <param name="examForm">考试形式</param>
        /// <param name="requireCourseID">约束关系</param>
        /// <param name="requireCourseName">约束关系</param>
        /// <param name="postName">工作岗位</param>
        /// <param name="typeName">课程类别</param>
        /// <param name="memo">备注</param>
        public TrainCourse(int? trainCourseID,
                           int? standardID,
                           int? courseNo,
                           string courseName,
                           string description,
                           string studyDemand,
                           decimal? studyHours,
                           bool? hasExam,
                           string examForm,
                           int? requireCourseID,
                           string requireCourseName,
                           string memo,
                           string postName,
                           string typeName)
        {
            _trainCourseID = trainCourseID ?? _trainCourseID;
            _standardID = standardID ?? _standardID;
            _courseNo = courseNo ?? _courseNo;
            _courseName = courseName;
            _description = description;
            _studyDemand = studyDemand;
            _studyHours = studyHours ?? _studyHours;
            _hasExam = hasExam ?? _hasExam;
            _examForm = examForm;
            _requireCourseID = requireCourseID ?? _requireCourseID;
            _requireCourseName = requireCourseName;
            _memo = memo;
            _postName = postName ?? _postName;
            _typeName = typeName ?? _typeName;
        }
    }
}
