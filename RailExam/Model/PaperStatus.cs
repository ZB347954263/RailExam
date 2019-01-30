using System;

namespace RailExam.Model
{
    /// <summary> 
    /// This business object represents the properties and methods of the RAILEXAM.PaperStatus Table. 
    /// It corresponds to the Custom Code Class as outlined in the CSLA Newsgroup(s) to support the BaseClass -> Class Framework."
    /// Object was generated on 2007-2-28 14:35:00 - By Administrator 
    /// </summary> 
    /// <remarks> 
    /// Parameters used to generate this class.
    /// Business Object
    ///		ClassNamespace      = RailExam.Model
    ///		CollectionName      = 
    ///		ObjectName          = PaperStatus
    ///		RootTable           = RailExam.RAILEXAM.PaperStatus
    /// </remarks>
    [Serializable]
    public class PaperStatus
    {
        #region // Class Level Private Variables

        private int _paperStatusId;
        private string _statusName;
        private string _description;
        private bool _isDefault;
        private string _memo;

        #endregion // End of Class Level Private Variables

        #region // Business Properties

        /// <summary>
        /// ÊÔ¾í×´Ì¬ID
        /// </summary>
        public int PaperStatusId
        {
            get { return _paperStatusId; }
            set { _paperStatusId = value; }
        }

        /// <summary>
        /// ×´Ì¬Ãû³Æ
        /// </summary>
        public string StatusName
        {
            get { return _statusName; }
            set { _statusName = value; }
        }

        /// <summary>
        /// ÃèÊö
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// ÊÇ·ñÈ±Ê¡
        /// </summary>
        public bool IsDefault
        {
            get { return _isDefault; }
            set { _isDefault = value; }
        }

        /// <summary>
        /// ±¸×¢
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        #endregion // End of Business Properties

        #region // Constructors

        public PaperStatus()
        {
            _paperStatusId = -1;
            _statusName = string.Empty;
            _description = string.Empty;
            _isDefault = false;
            _memo = string.Empty;
        }

        public PaperStatus(
            int? paperStatusId,
            string statusName,
            string description,
            bool? isDefault,
            string memo
        )
        {
            _paperStatusId = paperStatusId ?? _paperStatusId;
            _statusName = statusName;
            _description = description;
            _isDefault = isDefault ?? _isDefault;
            _memo = memo;
        }

        #endregion // End of Constructors
    }
}
