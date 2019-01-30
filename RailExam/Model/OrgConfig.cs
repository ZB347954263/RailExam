using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class OrgConfig
    {
        private int _orgID = 2;
        private int _hour = 1;
        private int _serverNo = 201;

        public int OrgID
        {
            get { return _orgID; }
            set { _orgID = value; }
        }

        public int Hour
        {
            get { return _hour; }
            set { _hour = value; }
        }

        public int ServerNo
        {
            get { return _serverNo; }
            set { _serverNo = value; }
        }

        public OrgConfig(int? orgID,int? hour,int? serverNo)
        {
            _orgID = orgID ?? _orgID;
            _hour = hour ?? _hour;
            _serverNo = serverNo ?? _serverNo;
        }

        public OrgConfig()
        {
            
        }
    }
}
