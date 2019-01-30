using System;

namespace RailExam.Model
{
    /// <summary> 
    /// This business object represents the properties and methods of the RAILEXAM.TASK_JUDGE_STATUS Table. 
    /// It corresponds to the Custom Code Class as outlined in the CSLA Newsgroup(s) to support the BaseClass -> Class Framework."
    /// Object was generated on 2007-2-27 15:18:42 - By Administrator 
    /// </summary> 
    /// <remarks> 
    /// Parameters used to generate this class.
    /// Business Object
    ///		ClassNamespace      = 
    ///		CollectionName      = 
    ///		ObjectName          = TASKJUDGESTATUS
    ///		RootTable           = RailExam.RAILEXAM.TASK_JUDGE_STATUS
    /// </remarks>
    [Serializable]
    public class TaskJudgeStatus
    {
        #region // Class Level Private Variables

        private int _taskJudgeStatusId;
        private string _statusName;
        private string _description;
        private bool _isDefault;
        private decimal _scoreRate;
        private string _memo;

        #endregion // End of Class Level Private Variables

        #region // Business Properties

        /// <summary>
        /// 作业评分状态ID
        /// </summary>
        public int TaskJudgeStatusId
        {
            get { return _taskJudgeStatusId; }
            set { _taskJudgeStatusId = value; }
        }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// 是否缺省
        /// </summary>
        public bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        /// <summary>
        /// 分数比率
        /// </summary>
        public decimal ScoreRate
        {
            get { return _scoreRate; }
            set { _scoreRate = value; }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        #endregion //End of Business Properties

        #region // Constructors
        /// <summary>
        /// 空参数构造函数
        /// </summary>
        public TaskJudgeStatus()
        {
            _taskJudgeStatusId = -1;
            _statusName = string.Empty;
            _description = string.Empty;
            _isDefault = false;
            _scoreRate = -1.0M;
            _memo = string.Empty;
        }

        /// <summary>
        /// 全参数构造函数
        /// </summary>
        /// <param name="taskJudgeStatusId"></param>
        /// <param name="statusName"></param>
        /// <param name="description"></param>
        /// <param name="isDefault"></param>
        /// <param name="scoreRate"></param>
        /// <param name="memo"></param>
        public TaskJudgeStatus(
            int? taskJudgeStatusId,
            string statusName,
            string description,
            bool? isDefault,
            decimal? scoreRate,
            string memo)     
        {
            _taskJudgeStatusId = taskJudgeStatusId ?? _taskJudgeStatusId;
            _statusName = statusName;
            _description = description;
            _isDefault = isDefault ?? _isDefault;
            _scoreRate = scoreRate ?? _scoreRate;
            _memo = memo;
        }
        #endregion //End of Constructors
    }
}