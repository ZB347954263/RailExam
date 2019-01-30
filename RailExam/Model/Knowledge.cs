using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace RailExam.Model
{    
    public class Knowledge
    {
        /// <summary>
		/// 内部成员变量
		/// </summary>
		private int _knowledgeId = 0;
		private int _parentId = 1;
		private string _idPath = string.Empty;
		private int _levelNum = 2;
		private int _orderIndex = 1;
		private string _knowledgeName = string.Empty;
		private string _description = string.Empty;
		private string _memo = string.Empty;
        private bool _isTemplate = false;
        private bool _isPromotion = false;
     
        public Knowledge()  { }

        /// <summary>
		/// 带参数的构造函数
		/// </summary>
		/// <param name="knowledgeId">知识类别ID</param>
		/// <param name="parentId">父知识类别ID</param>
		/// <param name="idPath">知识类别ID路径</param>
		/// <param name="levelNum">知识类别级别</param>
		/// <param name="orderIndex">排序索引</param>
		/// <param name="knowledgeName">简称</param>
		/// <param name="description">简介</param>
		/// <param name="memo">备注</param>
        /// <param name="orgidAL">org数组</param>
        /// <param name="postidAL">post数组</param>
		public Knowledge(
			int? knowledgeId,
			int? parentId,
			string idPath,
			int? levelNum,
			int? orderIndex,
			string knowledgeName,
			string description,
            bool? istemplate,
            bool? isPromotion,
            string memo)
		{
			_knowledgeId = knowledgeId ?? _knowledgeId;
			_parentId = parentId ?? _parentId;
			_idPath = idPath;
			_levelNum = levelNum ?? _levelNum;
			_orderIndex = orderIndex ?? _orderIndex;
			_knowledgeName = knowledgeName;
			_description = description;
            _isTemplate = istemplate ?? _isTemplate;
            _isPromotion = isPromotion ?? _isPromotion;
			_memo = memo;
		}

 		public int KnowledgeId
		{
			set
			{
				_knowledgeId = value;
			}
			get
			{
				return _knowledgeId;
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

		public string KnowledgeName
		{
			set
			{
				_knowledgeName = value;
			}
			get
			{
				return _knowledgeName;
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

        public bool IsTemplate
        {
            set
            {
                _isTemplate = value;
            }
            get
            {
                return _isTemplate;
            }
        }

        public bool IsPromotion
        {
            set { _isPromotion = value; }
            get { return _isPromotion;}
        }
	}
}
