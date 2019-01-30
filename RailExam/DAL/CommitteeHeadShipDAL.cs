using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using DSunSoft.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;

namespace RailExam.DAL
{
	public class CommitteeHeadShipDAL
	{
		private static Hashtable _ormTable;
		private int _recordCount = 0;
		public int RecordCount
		{
			get
			{
				return _recordCount;
			}
		}
		static CommitteeHeadShipDAL()
		{
			_ormTable = new Hashtable();
			_ormTable.Add("committeeheadshipid", "Committee_Head_Ship_ID");
			_ormTable.Add("committeeheadshipname", "Committee_Head_Ship_Name");
		}

		public static string GetMappingFieldName(string propertyName)
		{
			return (string)_ormTable[propertyName.ToLower()];
		}

		public static string GetMappingOrderBy(string orderBy)
		{
			orderBy = orderBy.Trim();

			if (string.IsNullOrEmpty(orderBy))
			{
				return string.Empty;
			}

			string mappingOrderBy = string.Empty;
			string[] orderByConditions = orderBy.Split(new char[] { ',' });

			foreach (string s in orderByConditions)
			{
				string orderByCondition = s.Trim();

				string[] orderBysOfOneCondition = orderByCondition.Split(new char[] { ' ' });

				if (orderBysOfOneCondition.Length == 0)
				{
					continue;
				}
				else
				{
					if (mappingOrderBy != string.Empty)
					{
						mappingOrderBy += ',';
					}

					if (orderBysOfOneCondition.Length == 1)
					{
						mappingOrderBy += GetMappingFieldName(orderBysOfOneCondition[0]);
					}
					else
					{
						mappingOrderBy += GetMappingFieldName(orderBysOfOneCondition[0]) + ' ' + orderBysOfOneCondition[1];
					}
				}
			}

			return mappingOrderBy;
		}

		public IList<CommitteeHeadShip> GetAllCommitteeHeadShip()
		{
			IList<CommitteeHeadShip> objList = new List<CommitteeHeadShip>();

			Database db = DatabaseFactory.CreateDatabase();
			string sql = "select * from zj_committee_head_ship order by committee_head_ship_id";
			using (IDataReader dataReader = db.ExecuteReader(CommandType.Text, sql))
			{
				while (dataReader.Read())
				{
					CommitteeHeadShip obj = CreateModelObject(dataReader);
					objList.Add(obj);
				}
			}

			return objList;
		}

		public IList<CommitteeHeadShip> GetAllCommitteeHeadShipByWhereClause(string sql)
		{
			IList<CommitteeHeadShip> objList = new List<CommitteeHeadShip>();
			Database db = DatabaseFactory.CreateDatabase();
			string sqlStr = string.Format("select * from zj_committee_head_ship where 1=1 and {0}", sql);
			using (IDataReader dataRead = db.ExecuteReader(CommandType.Text, sqlStr))
			{
				while (dataRead.Read())
				{
					CommitteeHeadShip obj = CreateModelObject(dataRead);
					objList.Add(obj);
				}
			}
			return objList;
		}

		public void InsertCommitteeHeadShip(CommitteeHeadShip obj)
		{
			Database db = DatabaseFactory.CreateDatabase();
			string sql = string.Format("insert into zj_committee_head_ship values({0},'{1}')", "COMMITTEE_HEAD_SHIP_SEQ.Nextval", obj.CommitteeHeadShipName);
			db.ExecuteNonQuery(CommandType.Text, sql);
		}

		public CommitteeHeadShip GetCommitteeHeadShipByCommitteeHeadShipID(int CommitteeHeadShipID)
		{
			CommitteeHeadShip objList = new CommitteeHeadShip();
			Database db = DatabaseFactory.CreateDatabase();
			string sql = string.Format("select * from zj_committee_head_ship where committee_head_ship_id={0}", CommitteeHeadShipID);
			using (IDataReader dataReader = db.ExecuteReader(CommandType.Text, sql))
			{
				while (dataReader.Read())
				{
					objList = CreateModelObject(dataReader);
				}
			}
			return objList;
		}

		public void UpdateCommitteeHeadShip(CommitteeHeadShip obj)
		{
			Database db = DatabaseFactory.CreateDatabase();
			string sql = string.Format("update zj_committee_head_ship set committee_head_ship_name='{0}' where committee_head_ship_id={1}", obj.CommitteeHeadShipName, obj.CommitteeHeadShipID);
			db.ExecuteNonQuery(CommandType.Text, sql);
		}
		public void DeleteCommitteeHeadShip(int id)
		{
			Database db = DatabaseFactory.CreateDatabase();
			string sql = string.Format("delete from zj_committee_head_ship where committee_head_ship_id={0}", id);
			db.ExecuteNonQuery(CommandType.Text, sql);
		}
		public static CommitteeHeadShip CreateModelObject(IDataReader dataReader)
		{
			return new CommitteeHeadShip(
				DataConvert.ToInt(dataReader[GetMappingFieldName("committeeheadshipid")]),
				DataConvert.ToString(dataReader[GetMappingFieldName("committeeheadshipname")]));

		}
	}
}
