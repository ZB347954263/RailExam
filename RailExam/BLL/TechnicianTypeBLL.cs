using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;
using System.Collections;
using DSunSoft.Web.Global;

namespace RailExam.BLL
{
    public class TechnicianTypeBLL
    {
        private static readonly TechnicianTypeDAL  dal = new TechnicianTypeDAL();

        public IList<TechnicianType> GetAllTechnicianType()
        {
            return dal.GetAllTechnicianType();
        }
		public void DeleteTechnicianType(int ID)
		{
			dal.DeleteTechnicianType(ID);
		}
		public void DeleteTechnicianType(int ID,string str)
		{
			dal.DeleteTechnicianType(ID,"");
		}
        public IList<TechnicianType> GetTechnicianType()
        {
            IList<TechnicianType> objList = dal.GetAllTechnicianType();

            TechnicianType obj = new TechnicianType();
            obj.TechnicianTypeID = 0;
            obj.TypeName = "--«Î—°‘Ò--";
            obj.IsDefault = false;
            obj.Description = "";
            obj.Memo = "";

            objList.Insert(0,obj);

            return objList;
        }

		public TechnicianType GetTechnicianTypeByTechnicianTypeID(int technicianTypeID)
		{
			return dal.GetTechnicianTypeByTechnicianTypeID(technicianTypeID);
		}

		public void UpdateTechnicianType(TechnicianType obj)
		{
			dal.UpdateTechnicianType(obj);
		}
		public void UpdateTechnicianType(TechnicianType obj,string str)
		{
			dal.UpdateTechnicianType(obj,str);
		}
		public void InsertTechnicianType(string typeName, string memo)
		{
			dal.InsertTechnicianType(typeName, memo);
		}
		public void InsertTechnicianType(string typeName, string memo,string str)
		{
			dal.InsertTechnicianType(typeName, memo,str);
		}
		public IList<TechnicianType> GetTechnicianTypeByWhereClause(string sql)
		{
			return dal.GetTechnicianTypeByWhereClause(sql);
		}

		public IList<TechnicianType> GetTechnicianTypeByWhereClause(string sql,string str)
		{
			return dal.GetTechnicianTypeByWhereClause(sql,str);
		}
    }
}
