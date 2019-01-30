using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace RailExam.Model
{
    public class CoursewareType
    {
        /// <summary>
        /// �ڲ���Ա����
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
        /// �������Ĺ��캯��
        /// </summary>
        /// <param name="CoursewareTypeId">�μ����ID</param>
        /// <param name="parentId">���μ����ID</param>
        /// <param name="idPath">�μ����ID·��</param>
        /// <param name="levelNum">�μ���𼶱�</param>
        /// <param name="orderIndex">��������</param>
        /// <param name="CoursewareTypeName">���</param>		
        /// <param name="description">���</param>
        /// <param name="memo">��ע</param>
        /// <param name="orgidAL">org����</param>
        /// <param name="postidAL">post����</param>
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
