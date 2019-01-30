using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	/// <summary>
	/// ҵ��ʵ�壺����Ȩ��
	/// </summary>
	public class FunctionRight
	{
		/// <summary>
		/// �ڲ���Ա����
		/// </summary>
		private SystemFunction _systemFunction;
		int _functionRight = 0;
		
		/// <summary>
		/// ȱʡ���캯��
		/// </summary>
		public FunctionRight() {}

		/// <summary>
		/// �������Ĺ��캯��
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
