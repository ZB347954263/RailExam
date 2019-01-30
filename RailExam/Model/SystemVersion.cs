using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	public class SystemVersion
	{
		private int _userPlace = 1;

		public int UserPlace
		{
			get { return _userPlace; }
			set { _userPlace = value;}
		}

		public SystemVersion()
		{
		}

		public SystemVersion(int? userPlace)
		{
			_userPlace = userPlace ?? _userPlace;
		}
	}
}
