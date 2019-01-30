using System;
using System.Collections.Generic;
using System.Text;
using RailExam.Model;
using RailExam.DAL;

namespace RailExam.BLL
{
	public class CommitteeHeadShipBLL
	{
		private static readonly CommitteeHeadShipDAL dal = new CommitteeHeadShipDAL();
		public IList<CommitteeHeadShip> GetAllCommitteeHeadShip()
		{
			return dal.GetAllCommitteeHeadShip();
		}
		public IList<CommitteeHeadShip> GetAllCommitteeHeadShipByWhereClause(string sql)
		{
			return dal.GetAllCommitteeHeadShipByWhereClause(sql);
		}
		public void InsertCommitteeHeadShip(CommitteeHeadShip obj)
		{
			dal.InsertCommitteeHeadShip(obj);
		}
		public CommitteeHeadShip GetCommitteeHeadShipByCommitteeHeadShipID(int CommitteeHeadShipID)
		{
			return dal.GetCommitteeHeadShipByCommitteeHeadShipID(CommitteeHeadShipID);
		}
		public void UpdateCommitteeHeadShip(CommitteeHeadShip obj)
		{
			dal.UpdateCommitteeHeadShip(obj);
		}
		public void DeleteCommitteeHeadShip(int ID)
		{
			dal.DeleteCommitteeHeadShip(ID);
		}
	}
}
