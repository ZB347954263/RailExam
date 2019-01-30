using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
	public class EducationEmployeeTypeBLL
	{
		private static readonly EducationEmployeeTypeDAL dal = new EducationEmployeeTypeDAL();

		public IList<EducationEmployeeType> GetAllEducationEmployeeType()
		{
			return dal.GetAllEducationEmployeeType();
		}
		public IList<EducationEmployeeType> GetAllEducationEmployeeTypeByWhereClause(string sql)
		{
			return dal.GetAllEducationEmployeeTypeByWhereClause(sql);
		}
		public void InsertEducationEmployeeType(EducationEmployeeType obj)
		{
			dal.InsertEducationEmployeeType(obj);
		}
		public EducationEmployeeType GetEducationEmployeeTypeByEducationEmployeeTypeID(int EducationEmployeeTypeID)
		{
			return dal.GetEducationEmployeeTypeByEducationEmployeeTypeID(EducationEmployeeTypeID);
		}
		public void UpdateEducationEmployeeType(EducationEmployeeType obj)
		{
			dal.UpdateEducationEmployeeType(obj);
		}
		public void DeleteEducationEmployeeType(int ID)
		{
			dal.DeleteEducationEmployeeType(ID);
		}
	}
}
