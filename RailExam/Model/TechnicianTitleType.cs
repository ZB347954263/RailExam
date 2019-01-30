using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	public class TechnicianTitleType
	{
		private int _technicianTitleTypeID = 0;
        private string _typeName = string.Empty;
		private int _typeLevel = 1;
		private string _typeLevelName = string.Empty;
		private int _orderIndex = 0;

        public int TechnicianTitleTypeID
        {
            set { _technicianTitleTypeID = value; }
            get { return _technicianTitleTypeID; }
        }

        public string TypeName
        {
            set { _typeName = value; }
            get { return _typeName; }
        }

		public int TypeLevel
		{
			set { _typeLevel = value; }
			get { return _typeLevel; }
		}

		public string TypeLevelName
		{
			set { _typeLevelName = value;}
			get { return _typeLevelName; }
		}

		public int OrderIndex
		{
			set { _orderIndex = value; }
			get { return _orderIndex; }
		}

		public TechnicianTitleType(int? technicianTitleTypeID, string typeName, int? typeLevel,string typeLevelName,int? orderIndex)
        {
			_technicianTitleTypeID = technicianTitleTypeID ?? _technicianTitleTypeID;
            _typeName = typeName;
			_typeLevel = typeLevel ?? _typeLevel;
			_typeLevelName = typeLevelName;
			_orderIndex = orderIndex ?? _orderIndex;
        }

		public TechnicianTitleType()
        {
            
        }
	}
}
