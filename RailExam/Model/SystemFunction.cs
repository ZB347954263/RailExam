using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	/// <summary>
	/// 业务实体：系统功能
	/// </summary>
	public class SystemFunction
	{
		/// <summary>
		/// 内部成员变量
		/// </summary>
		private string _functionID = string.Empty;
		private string _functionName = string.Empty;
		private string _description = string.Empty;
		private string _pageUrl = string.Empty;
		private string _menuName = string.Empty;
		private int _toolbarNo = 0;
		private string _toolbarName = string.Empty;
		private bool _isDefault = false;
		private string _memo = string.Empty;

		/// <summary>
		/// 缺省构造函数
		/// </summary>
		public SystemFunction()	{}

		/// <summary>
		/// 带参数的构造函数
		/// </summary>
		/// <param name="functionID"></param>
		/// <param name="functionName"></param>
		/// <param name="description"></param>
		/// <param name="pageUrl"></param>
		/// <param name="menuName"></param>
		/// <param name="toolbarNo"></param>
		/// <param name="toolbarName"></param>
		/// <param name="isDefault"></param>
		/// <param name="memo"></param>
		public SystemFunction(
			string functionID,
			string functionName,
			string description,
			string pageUrl,
			string menuName,
			int? toolbarNo,
			string toolbarName,
			bool? isDefault,
			string memo)
		{
			_functionID = functionID;
			_functionName = functionName;
			_description = description;
			_pageUrl = pageUrl;
			_menuName = menuName;
			_toolbarNo = toolbarNo ?? _toolbarNo;
			_toolbarName = toolbarName;
			_isDefault = isDefault ?? _isDefault;
			_memo = memo;
		}

		public string FunctionID
		{
			set
			{
				_functionID = value;
			}
			get
			{
				return _functionID;
			}
		}

		public string FunctionName
		{
			set
			{
				_functionName = value;
			}
			get
			{
				return _functionName;
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

		public string PageUrl
		{
			set
			{
				_pageUrl = value;
			}
			get
			{
				return _pageUrl;
			}
		}

		public string MenuName
		{
			set
			{
				_menuName = value;
			}
			get
			{
				return _menuName;
			}
		}
		
		public int ToolbarNo
		{
			set
			{
				_toolbarNo = value;
			}
			get
			{
				return _toolbarNo;
			}
		}

		public string ToolbarName
		{
			set
			{
				_toolbarName = value;
			}
			get
			{
				return _toolbarName;
			}
		}

		public bool IsDefault
		{
			set
			{
				_isDefault = value;
			}
			get
			{
				return _isDefault;
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
