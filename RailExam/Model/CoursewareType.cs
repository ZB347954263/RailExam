using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace RailExam.Model
{
    public class CoursewareType
    {
        /// <summary>
        /// 内部成员变量
        /// </summary>
        private int _coursewareTypeId = 0;
        private int _parentId = 1;
        private string _idPath = string.Empty;
        private int _levelNum = 2;
        private int _orderIndex = 1;
        private string _coursewareTypeName = string.Empty;
        private string _description = string.Empty;
        private string _memo = string.Empty;

        public CoursewareType() { }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="CoursewareTypeId">课件类别ID</param>
        /// <param name="parentId">父课件类别ID</param>
        /// <param name="idPath">课件类别ID路径</param>
        /// <param name="levelNum">课件类别级别</param>
        /// <param name="orderIndex">排序索引</param>
        /// <param name="CoursewareTypeName">简称</param>		
        /// <param name="description">简介</param>
        /// <param name="memo">备注</param>
        /// <param name="orgidAL">org数组</param>
        /// <param name="postidAL">post数组</param>
        public CoursewareType(
            int? coursewareTypeId,
            int? parentId,
            string idPath,
            int? levelNum,
            int? orderIndex,
            string coursewareTypeName,
            string description,
            string memo)
        {
            _coursewareTypeId = coursewareTypeId ?? _coursewareTypeId;
            _parentId = parentId ?? _parentId;
            _idPath = idPath;
            _levelNum = levelNum ?? _levelNum;
            _orderIndex = orderIndex ?? _orderIndex;
            _coursewareTypeName = coursewareTypeName;
            _description = description;
            _memo = memo;
        }

        public int CoursewareTypeId
        {
            set
            {
                _coursewareTypeId = value;
            }
            get
            {
                return _coursewareTypeId;
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

        public string CoursewareTypeName
        {
            set
            {
                _coursewareTypeName = value;
            }
            get
            {
                return _coursewareTypeName;
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
