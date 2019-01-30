using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class RegulationCategory
    {
        private int _regulationCategoryID = 0;
        private int _parentID = 1;
        private string _idPath = string.Empty;
        private int _levelNum = 2;
        private int _orderIndex = 1;
        private string _categoryName = string.Empty;
        private string _description = string.Empty;
        private string _memo = string.Empty;

        public RegulationCategory()  {}
      
        public RegulationCategory(
            int? regulationCategoryID,
            int? parentID,
            string idPath,
            int? levelNum,
            int? orderIndex,
            string categoryName,
            string description,
            string memo)
        {
            _regulationCategoryID = regulationCategoryID ?? _regulationCategoryID;
            _parentID = parentID ?? _parentID;
            _idPath = idPath;
            _levelNum = levelNum ?? _levelNum;
            _orderIndex = orderIndex ?? _orderIndex;
            _categoryName = categoryName;
            _description = description;
            _memo = memo;
        }

        public int RegulationCategoryID
        {
            set
            {
                _regulationCategoryID = value;
            }
            get
            {
                return _regulationCategoryID;
            }
        }

        public int ParentID
        {
            set
            {
                _parentID = value;
            }
            get
            {
                return _parentID;
            }
        }

        public string IdPath
        {
            set
            {
                _idPath = value;
            }
            get
            {
                return _idPath;
            }
        }

        public int LevelNum
        {
            set
            {
                _levelNum = value;
            }
            get
            {
                return _levelNum;
            }
        }

        public int OrderIndex
        {
            set
            {
                _orderIndex = value;
            }
            get
            {
                return _orderIndex;
            }
        }

        public string CategoryName
        {
            set
            {
                _categoryName = value;
            }
            get
            {
                return _categoryName;
            }
        }

        public string Description
        {
            set
            {
                _description = value;
            }
            get
            {
                return _description;
            }
        }

        public string Memo
        {
            set
            {
                _memo = value;
            }
            get
            {
                return _memo;
            }
        }
    }
}
