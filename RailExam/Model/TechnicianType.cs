using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class TechnicianType
    {
        private int _technicianTypeID = 0;
        private string _typeName = string.Empty;
        private string _description = string.Empty;
        private bool _isDefault = false;
        private string _memo = string.Empty;

        public int TechnicianTypeID
        {
            set { _technicianTypeID = value; }
            get { return _technicianTypeID; }
        }

        public string TypeName
        {
            set { _typeName = value; }
            get { return _typeName; }
        }

        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }

        public bool IsDefault
        {
            set { _isDefault = value; }
            get { return _isDefault; }
        }

        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }

        public TechnicianType(int? technicianTypeID, string typeName,string description, bool? isDefault,string memo)
        {
            _technicianTypeID = technicianTypeID ?? _technicianTypeID;
            _typeName = typeName;
            _description = description;
            _isDefault = isDefault ?? _isDefault;
            _memo = memo;
        }

        public TechnicianType()
        {
            
        }
    }
}
