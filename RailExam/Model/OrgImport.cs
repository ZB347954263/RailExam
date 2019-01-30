using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class OrgImport
    {
        private int _orgID = 2;
        private string _orgNamePath = "";

        public int OrgID
        {
            get { return _orgID; }
            set { _orgID = value; }
        }

        public string OrgNamePath
        {
            get { return _orgNamePath; }
            set { _orgNamePath = value; }
        }

        public OrgImport(int? orgID, string orgNamePath)
        {
            _orgID = orgID ?? _orgID;
            _orgNamePath = orgNamePath ?? _orgNamePath;
        }
    }
}
