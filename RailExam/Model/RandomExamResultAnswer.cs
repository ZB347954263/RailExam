using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class RandomExamResultAnswer
    {
        private int _randomExamResultId = 0;
        private int _randomExamItemId = 0;
        private int _examTime = 0;
        private string _answer = string.Empty;
        private decimal _judgeScore = 0.0M;
        private int _judgeStatusId = 0;
        private string _judgeStatusName = string.Empty;
        private string _judgeRemark = string.Empty;
        private string _selectAnswer = string.Empty;
        private string _standardAnswer = string.Empty;


        /// <summary>
        /// ���Կ������ID
        /// </summary>
        public int RandomExamResultId
        {
            get { return _randomExamResultId; }
            set { _randomExamResultId = value; }
        }
 

        /// <summary>
        /// �Ծ�����ID
        /// </summary>
        public int RandomExamItemId
        {
            get { return _randomExamItemId; }
            set { _randomExamItemId = value; }
        }

       
        /// <summary>
        /// �𰸣�ʹ��|�ָ���
        /// </summary>
        public string Answer
        {
            get { return _answer; }
            set { _answer = value; }
        }

        /// <summary>
        /// ����ʱ��
        /// </summary>
        public int ExamTime
        {
            get { return _examTime; }
            set { _examTime = value; }
        }

        /// <summary>
        /// ���ַ���
        /// </summary>
        public decimal JudgeScore
        {
            get { return _judgeScore; }
            set { _judgeScore = value; }
        }

        /// <summary>
        /// ����״̬ID
        /// </summary>
        public int JudgeStatusId
        {
            get { return _judgeStatusId; }
            set { _judgeStatusId = value; }
        }

        /// <summary>
        /// ״̬����
        /// </summary>
        public string JudgeStatusName
        {
            get { return _judgeStatusName; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string JudgeRemark
        {
            get { return _judgeRemark; }
            set { _judgeRemark = value; }
        } 

        public string SelectAnswer
        {
            get { return _selectAnswer; }
            set { _selectAnswer = value; }
        }

        public string StandardAnswer
        {
            get { return _standardAnswer; }
            set { _standardAnswer = value; }
        }
       
        /// <summary>
        /// �ղ������캯��
        /// </summary>
        public RandomExamResultAnswer() { }

        public RandomExamResultAnswer(int? randomExamResultId,
             int? randomExamItemId,            
             string answer,
             int? examTime,
             decimal? judgeScore,
             int? judgeStatusId,
             string judgeStatusName,
             string judgeRemark,
            string selectAnswer,
            string standardAnswer)
        {
            _randomExamResultId = randomExamResultId ?? _randomExamResultId;            
            _randomExamItemId = randomExamItemId ?? _randomExamItemId;            
            _answer = answer;
            _examTime = examTime ?? _examTime;
            _judgeScore = judgeScore ?? _judgeScore;
            _judgeStatusId = judgeStatusId ?? _judgeStatusId;
            _judgeStatusName = judgeStatusName;
            _judgeRemark = judgeRemark;
            _selectAnswer = selectAnswer;
            _standardAnswer = standardAnswer;          
        }
    }
}

