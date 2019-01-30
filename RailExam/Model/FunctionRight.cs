using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	/// <summary>
	/// 业务实体：功能权限
	/// </summary>
	public class FunctionRight
	{
		/// <summary>
		/// 内部成员变量
		/// </summary>
		private SystemFunction _systemFunction;
		int _functionRight = 0;
		
		/// <summary>
		/// 缺省构造函数
		/// </summary>
		public FunctionRight() {}

		/// <summary>
		/// 带参数的构造函数
		/// </summary>
		/// <param name="systemFunction"></param>
		/// <param name="functionRight"></param>
		public FunctionRight(
			SystemFunction systemFunction,
			int? functionRight)
		{
			_systemFunction = systemFunction;
			_functionRight = functionRight ?? _functionRight;
		}

		public SystemFunction Function
		{
			set
			{
				_systemFunction = value;
			}
			get
			{
				return _systemFunction;
			}
		}

		public int Right
		{
			set
			{
				_functionRight = value;
			}
			get
			{
				return _functionRight;
			}
		}
	}
}
