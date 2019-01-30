using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace RailExam.Model
{
    public class PaperCategory
    {
        /// <summary>
        /// 内部成员变量
        /// </summary>
        private int _PaperCategoryId = 0;
        private int _parentId = 1;
        private string _idPath = string.Empty;
        private int _levelNum = 2;
        private int _orderIndex = 1;
        private string _CategoryName = string.Empty;
        private string _description = string.Empty;
        private string _memo = string.Empty;
        

        public PaperCategory() { }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        public PaperCategory(
            int? PaperCategoryId,
            int? parentId,
            string idPath,
            int? levelNum,
            int? orderIndex,
            string CategoryName,
            string description,
            string memo )
        {
            _PaperCategoryId = PaperCategoryId ?? _PaperCategoryId;
            _parentId = parentId ?? _parentId;
            _idPath = idPath;
            _levelNum = levelNum ?? _levelNum;
            _orderIndex = orderIndex ?? _orderIndex;
            _CategoryName = CategoryName;
            _description = description;
            _memo = memo;
             
        }

        public int PaperCategoryId
        {
            set
            {
                _PaperCategoryId = value;
            }
            get
            {
                return _PaperCategoryId;
            }
        }

        public int ParentId
        {
            set
            {
                _parentId = value;
            }
            get
            {
                return _parentId;
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
                _CategoryName = value;
            }
            get
            {
                return _CategoryName;
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
