using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    /// <summary>
    /// ҵ��ʵ�壺��ѵ���
    /// </summary>
    public class TrainType
    {
        /// <summary>
        /// ��Ա����
        /// </summary>
        private int _trainTypeID = 0;
        private int _parentID = 0;
        private int _levelNum = 1;
        private string _iDPath = string.Empty;
        private int _orderIndex = 1;
        private string _typeName = string.Empty;
        private string _description = string.Empty;
        private bool _isTemplate = true;
        private bool _isPromotion = false;
        private string _memo = string.Empty;

        /// <summary>
        /// ȱʡ���캯��
        /// </summary>
        public TrainType()  {}

        /// <summary>
        /// �������Ĺ��캯��
        /// </summary>
        /// <param name="trainTypeID">��ѵ����ID</param>
        /// <param name="parentID">����ѵ����ID</param>
        /// <param name="levelNum">����</param>
        /// <param name="iDPath">��ѵ����ID·��</param>
        /// <param name="orderIndex">�����±�</param>
        /// <param name="typeName">�������</param>
        /// <param name="description">����</param>
        /// <param name="isTemplate">����ģ��</param>
        /// <param name="isPromotion">�Ƿ�ɽ���</param>
        /// <param name="memo">��ע</param>
        public TrainType(int? trainTypeID,
                         int? parentID,
                         int? levelNum,
                         string iDPath,
                         int? orderIndex,
                         string typeName,
                         string description,
                         bool? isTemplate,
                         bool? isPromotion,
                         string memo)
        {
            _trainTypeID = trainTypeID ?? _trainTypeID;
            _parentID = parentID ?? _parentID;
            _levelNum = levelNum ?? _levelNum;
            _iDPath = iDPath;
            _orderIndex = orderIndex ?? _orderIndex;
            _typeName = typeName;
            _description = description;
            _isTemplate = isTemplate ?? _isTemplate;
            _isPromotion = isPromotion ?? _isPromotion;
            _memo = memo;
        }

        /// <summary>
        /// ʵ������
        /// </summary>
        public int TrainTypeID
        {
            get { return _trainTypeID; }
            set { _trainTypeID = value; }
        }

        public int ParentID
        {
            get { return _parentID; }
            set { _parentID = value; }
        }

        public int LevelNum
        {
            get { return _levelNum; }
            set { _levelNum = value; }
        }

        public string IDPath
        {
            get { return _iDPath; }
            set { _iDPath = value; }
        }

        public int OrderIndex
        {
            get { return _orderIndex; }
            set { _orderIndex = value; }
        }

        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public bool IsTemplate
        {
            get { return _isTemplate; }
            set { _isTemplate = value; }
        }

        public bool IsPromotion
        {
            get { return _isPromotion; }
            set { _isPromotion = value; }
        }

        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }
    }
}
