using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
	public class TechnicianTitleTypeBLL
	{
		private static readonly TechnicianTitleTypeDAL dal = new TechnicianTitleTypeDAL();

		public IList<TechnicianTitleType> GetAllTechnicianTitleType()
		{
			return dal.GetAllTechnicianTitleType();
		}

		public void DeleteTechnicianTitleType(int ID)
		{
			dal.DeleteTechnicianTitleType(ID);
		}

		public IList<TechnicianTitleType> GetTechnicianTitleType()
		{
			IList<TechnicianTitleType> objList = dal.GetAllTechnicianTitleType();

			TechnicianTitleType obj = new TechnicianTitleType();
			obj.TechnicianTitleTypeID = 0;
			obj.TypeName = "--«Î—°‘Ò--";

			objList.Insert(0, obj);

			return objList;
		}

		public TechnicianTitleType GetTechnicianTitleTypeByTechnicianTitleTypeID(int technicianTitleTypeID)
		{
			return dal.GetTechnicianTitleTypeByTechnicianTitleTypeID(technicianTitleTypeID);
		}

		public void UpdateTechnicianTitleType(TechnicianTitleType obj)
		{
			dal.UpdateTechnicianTitleType(obj);
		}

		public void InsertTechnicianTitleType(TechnicianTitleType obj)
		{
			dal.InsertTechnicianTitleType(obj);
		}

		public IList<TechnicianTitleType> GetTechnicianTitleTypeByWhereClause(string sql)
		{
			return dal.GetTechnicianTitleTypeByWhereClause(sql);
		}
	}
}
