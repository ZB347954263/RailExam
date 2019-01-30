using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class AssistBookCategory
    {
                /// <summary>
		/// �ڲ���Ա����
		/// </summary>
		private int _assistBookCategoryId = 0;
		private int _parentId = 1;
		private string _idPath = string.Empty;
		private int _levelNum = 2;
		private int _orderIndex = 1;
		private string _assistBookCategoryName = string.Empty;
		private string _description = string.Empty;
		private string _memo = string.Empty;
     
        public AssistBookCategory()
        {
            _assistBookCategoryId = 0;
            _parentId = 0;
            _idPath = "";
            _levelNum = 1;
            _orderIndex = 1;
            _assistBookCategoryName = "";
            _description = "";
            _memo = "";
        }

        /// <summary>
		/// �������Ĺ��캯��
		/// </summary>
		/// <param name="assistBookCategoryId">֪ʶ���ID</param>
		/// <param name="parentId">��֪ʶ���ID</param>
		/// <param name="idPath">֪ʶ���ID·��</param>
		/// <param name="levelNum">֪ʶ��𼶱�</param>
		/// <param name="orderIndex">��������</param>
		/// <param name="assistBookCategoryName">���</param>
		/// <param name="description">���</param>
		/// <param name="memo">��ע</param>
        public AssistBookCategory(
			int? assistBookCategoryId,
			int? parentId,
			string idPath,
			int? levelNum,
			int? orderIndex,
			string assistBookCategoryName,
			string description,
            string memo)
		{
			_assistBookCategoryId = assistBookCategoryId ?? _assistBookCategoryId;
			_parentId = parentId ?? _parentId;
			_idPath = idPath;
			_levelNum = levelNum ?? _levelNum;
			_orderIndex = orderIndex ?? _orderIndex;
			_assistBookCategoryName = assistBookCategoryName;
			_description = description;
			_memo = memo;
		}

 		public int AssistBookCategoryId
		{
			set
			{
				_assistBookCategoryId = value;
			}
			get
			{
				return _assistBookCategoryId;
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

		public string AssistBookCategoryName
		{
			set
			{
				_assistBookCategoryName = value;
			}
			get
			{
				return _assistBookCategoryName;
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
