using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class SynchronizeLog
    {
        private int _synchronizeLogID  = 0;
        private int _orgID = 1;
        private string _orgName = string.Empty;
        private int _synchronizeTypeID = 1;
        private string _synchronizeContent = string.Empty;
        private DateTime _beginTime = DateTime.Now;
        private DateTime _endTime = DateTime.Now;
        private int _synchronizeStatusID = 1;
        private string _statusContent = string.Empty;
        private int _computerServerNo;

        public string OrgName
        {
            get { return _orgName; }
            set { _orgName = value; }
        }

        public int SynchronizeTypeID
        {
            get { return _synchronizeTypeID; }
            set { _synchronizeTypeID = value; }
        }

        public string SynchronizeContent
        {
            get { return _synchronizeContent; }
            set { _synchronizeContent = value; }
        }

        public DateTime BeginTime
        {
            get { return _beginTime; }
            set { _beginTime = value; }
        }

        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        public int SynchronizeStatusID
        {
            get { return _synchronizeStatusID; }
            set { _synchronizeStatusID = value; }
        }

        public string StatusContent
        {
            get { return _statusContent; }
            set { _statusContent = value; }
        }

        public int SynchronizeLogID 
        {
            get { return _synchronizeLogID ; }
            set { _synchronizeLogID  = value; }
        }

        public int OrgID
        {
            get { return _orgID; }
            set { _orgID = value; }
        }

        public int ComputerServerNo
        {
            get { return _computerServerNo; }
            set { _computerServerNo = value; }
        }

        public SynchronizeLog(int? synchronizeLogID ,int? orgID,string orgName,int? synchronizeTypeID,string synchronizeContent,
            DateTime? beginTime, DateTime? endTime, int? synchronizeStatusID, string statusContent)
        {
            _synchronizeLogID  = synchronizeLogID  ?? _synchronizeLogID ;
            _orgID = orgID ?? _orgID;
            _orgName = orgName;
            _synchronizeTypeID = synchronizeTypeID ?? _synchronizeTypeID;
            _synchronizeContent = synchronizeContent;
            _beginTime = beginTime ?? _beginTime;
            _endTime = endTime ?? _endTime;
            _synchronizeStatusID = synchronizeStatusID ?? _synchronizeStatusID;
            _statusContent = statusContent;
        }

        public SynchronizeLog()
        {
            
        }
    }
}
