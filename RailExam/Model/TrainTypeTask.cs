using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class TrainTypeTask
    {
        private int _trainTypeID = 0;
        private int _paperID = 0;
        private string _memo = string.Empty;
        private Paper _objPaper;

        public int TrainTypeID
        {
            get { return _trainTypeID; }
            set { _trainTypeID = value; }
        }

        public int PaperID
        {
            get { return _paperID; }
            set { _paperID = value; }
        }

        public Paper ObjPaper
        {
            get { return _objPaper; }
            set { _objPaper = value; }
        }

        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        public TrainTypeTask()  { }

        public TrainTypeTask(int? trainTypeID ,int? paperID, Paper objPaper)
        {
            _trainTypeID = trainTypeID ?? _trainTypeID;
            _paperID = paperID ?? _paperID;
            _objPaper = objPaper;
        }
    }
}
