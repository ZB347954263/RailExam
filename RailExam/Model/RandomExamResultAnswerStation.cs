using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	public class RandomExamResultAnswerStation
	{
		private int _randomExamResultAnswerID = 0;
		private int _randomExamResultID = 0;
        private int _randomExamItemID = 0;
        private int _examTime = 0;
        private string _answer = string.Empty;
        private decimal _judgeScore = 0.0M;
        private int _judgeStatusID = 0;
        private string _judgeStatusName = string.Empty;
        private string _judgeRemark = string.Empty;
		private int _randomExamResultIDStation = 0;

		public int RandomExamResultAnswerID
		{
			get { return _randomExamResultAnswerID; }
			set { _randomExamResultAnswerID = value; }
		}

        /// <summary>
        /// 考试考生结果ID
        /// </summary>
        public int RandomExamResultID
        {
            get { return _randomExamResultID; }
            set { _randomExamResultID = value; }
        }
 

        /// <summary>
        /// 试卷试题ID
        /// </summary>
        public int RandomExamItemID
        {
            get { return _randomExamItemID; }
            set { _randomExamItemID = value; }
        }

       
        /// <summary>
        /// 答案（使用|分隔）
        /// </summary>
        public string Answer
        {
            get { return _answer; }
            set { _answer = value; }
        }

        /// <summary>
        /// 考试时间
        /// </summary>
        public int ExamTime
        {
            get { return _examTime; }
            set { _examTime = value; }
        }

        /// <summary>
        /// 评分分数
        /// </summary>
        public decimal JudgeScore
        {
            get { return _judgeScore; }
            set { _judgeScore = value; }
        }

        /// <summary>
        /// 评分状态ID
        /// </summary>
        public int JudgeStatusID
        {
            get { return _judgeStatusID; }
            set { _judgeStatusID = value; }
        }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string JudgeStatusName
        {
            get { return _judgeStatusName; }
        }

        /// <summary>
        /// 评语
        /// </summary>
        public string JudgeRemark
        {
            get { return _judgeRemark; }
            set { _judgeRemark = value; }
        }

		public int RandomExamResultIDStation
		{
			get { return _randomExamResultIDStation; }
			set { _randomExamResultIDStation = value; }
		}
       
        /// <summary>
        /// 空参数构造函数
        /// </summary>
        public RandomExamResultAnswerStation() { }

        public RandomExamResultAnswerStation(
			 int? randomExamResultAnswerID,
			 int? randomExamResultID,
             int? randomExamItemID,            
             string answer,
             int? examTime,
             decimal? judgeScore,
             int? judgeStatusID,
             string judgeStatusName,
             string judgeRemark,
			int? randomExamResultIDStation)
        {
        	_randomExamResultAnswerID = randomExamResultAnswerID ?? _randomExamResultAnswerID;
            _randomExamResultID = randomExamResultID ?? _randomExamResultID;            
            _randomExamItemID = randomExamItemID ?? _randomExamItemID;            
            _answer = answer;
            _examTime = examTime ?? _examTime;
            _judgeScore = judgeScore ?? _judgeScore;
            _judgeStatusID = judgeStatusID ?? _judgeStatusID;
            _judgeStatusName = judgeStatusName;
            _judgeRemark = judgeRemark;
        	_randomExamResultIDStation = randomExamResultIDStation ?? _randomExamResultIDStation;
        }
	}
}
