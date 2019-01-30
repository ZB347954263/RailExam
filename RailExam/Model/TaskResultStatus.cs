using System;

namespace RailExam.Model
{
    /// <summary> 
    /// This business object represents the properties and methods of the RAILEXAM.TaskResultStatus Table. 
    /// It corresponds to the Custom Code Class as outlined in the CSLA Newsgroup(s) to support the BaseClass -> Class Framework."
    /// Object was generated on 2007-2-27 15:31:52 - By Administrator 
    /// </summary> 
    /// <remarks> 
    /// Parameters used to generate this class.
    /// Business Object
    ///		ClassNamespace      = 
    ///		CollectionName      = 
    ///		ObjectName          = TaskResultStatus
    ///		RootTable           = RailExam.RAILEXAM.TaskResultStatus
    /// </remarks>
    [Serializable]
    public class TaskResultStatus
    {
        #region // Class Level Private Variables

        private int _taskResultStatusId;
        private string _statusName;
        private string _description;
        private bool _isDefault;
        private string _memo;

        #endregion //End of Class Level Private Variables

        #region // Business Properties

        public int TaskResultStatusId
        {
            get { return _taskResultStatusId; }
            set { _taskResultStatusId = value; }
        }
        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        #endregion // End of Business Properties

        #region Constructors

        public TaskResultStatus()
        {
            _taskResultStatusId = -1;
            _statusName = string.Empty;
            _description = string.Empty;
            _isDefault = false;
            _memo = string.Empty;
        }


        public TaskResultStatus(
            int? taskResultStatusId,
            string statusName,
            string description,
            bool? isDefault,
            string memo
        )
        {
            _taskResultStatusId = taskResultStatusId ?? _taskResultStatusId;
            _statusName = statusName;
            _description = description;
            _isDefault = isDefault ?? _isDefault;
            _memo = memo;
        }

        #endregion // End of Constructors
    }
}
