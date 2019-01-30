using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
	public class SystemVersionBLL
	{
		private static readonly SystemVersionDAL dal = new SystemVersionDAL();

		public int GetUsePlace()
		{
			return dal.GetUsePlace();
		}

		public decimal GetVersion()
		{
			return dal.GetVersion();
		}

        public decimal GetVersionToServer()
		{
			return dal.GetVersionToServer();
		}
	}
}
