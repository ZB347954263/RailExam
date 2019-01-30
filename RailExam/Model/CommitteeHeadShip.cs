using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
	public class CommitteeHeadShip
	{
		private int _committee_head_ship_id;
		private string _committee_head_ship_name;

		public int CommitteeHeadShipID
		{
			set { _committee_head_ship_id = value; }
			get { return _committee_head_ship_id; }
		}
		public string CommitteeHeadShipName
		{
			set { _committee_head_ship_name = value; }
			get { return _committee_head_ship_name; }
		}
		public CommitteeHeadShip(int? committeeheadshipid, string committeeheadshipname)
		{
			_committee_head_ship_id = committeeheadshipid ?? _committee_head_ship_id;
			_committee_head_ship_name = committeeheadshipname;
		}
		public CommitteeHeadShip()
		{ }
	}
}
