using System;

namespace RailExam.Model
{
    /// <summary> 
    /// This business object represents the properties and methods of the RAILEXAM.TaskResultAnswer Table. 
    /// It corresponds to the Custom Code Class as outlined in the CSLA Newsgroup(s) to support the BaseClass -> Class Framework."
    /// Object was generated on 2007-2-27 15:35:28 - By Administrator 
    /// </summary> 
    /// <remarks> 
    /// Parameters used to generate this class.
    /// Business Object
    ///		ClassNamespace      = 
    ///		CollectionName      = 
    ///		ObjectName          = TaskResultAnswer
    ///		RootTable           = RailExam.RAILEXAM.TaskResultAnswer
    /// </remarks>
    [Serializable]
    public class TaskResultAnswer
    {
        #region // Class Level Private Variables

        private int _taskResultId;
        private int _paperItemId;
        private string _answer;
        private int _taskTime;
        private decimal _judgeScore;
        private int _judgeStatusId;
        private string _judgeRemark;

        #endregion // End of Class Level Private Variables

        #region // Business Properties

        /// <summary>
        /// ��ҵ���ID
        /// </summary>
        public int TaskResultId
        {
            get { return _taskResultId; }
            set { _taskResultId = value; }
        }

        /// <summary>
        /// �Ծ�����ID
        /// </summary>
        public int PaperItemId
        {
            get { return _paperItemId; }
            set { _paperItemId = value; }
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
        /// ��ҵʱ��
        /// </summary>
        public int TaskTime
        {
            get { return _taskTime; }
            set { _taskTime = value; }
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
        /// ����
        /// </summary>
        public string JudgeRemark
        {
            get { return _judgeRemark; }
            set { _judgeRemark = value; }
        }

        #endregion // End of Business Properties

        #region // Constructors

        /// <summary>
        /// �ղ������캯��
        /// </summary>
        public TaskResultAnswer()
        {
            _taskResultId = -1;
            _paperItemId = -1;
            _answer = string.Empty;
            _taskTime = -1;
            _judgeScore = -1;
            _judgeStatusId = -1;
            _judgeRemark = string.Empty;
        }

        /// <summary>
        /// ȫ�������캯��
        /// </summary>
        /// <param name="taskResultId"></param>
        /// <param name="paperItemId"></param>
        /// <param name="answer"></param>
        /// <param name="taskTime"></param>
        /// <param name="judgeScore"></param>
        /// <param name="judgeStatusId"></param>
        /// <param name="judgeRemark"></param>
        public TaskResultAnswer(
            int? taskResultId,
            int? paperItemId,
            string answer,
            int? taskTime,
            decimal? judgeScore,
            int? judgeStatusId,
            string judgeRemark
        )
        {
            _taskResultId = taskResultId ?? _taskResultId;
            _paperItemId = paperItemId ?? _paperItemId;
            _answer = answer;
            _taskTime = taskTime ?? _taskTime;
            _judgeScore = judgeScore ?? _judgeScore;
            _judgeStatusId = judgeStatusId ?? _judgeStatusId;
            _judgeRemark = judgeRemark;
        }

        #endregion // End of Constructors
    }
}
