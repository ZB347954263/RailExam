using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class RandomExamComputerServer
    {
        private int _randomExamId;
        private int _computerServerNo;
        private int _statusID;
        private int _isStart;
        private bool _hasPaper;
        private string _randomExamCode=string.Empty;
        private bool _isUpload;
        private int _downloaded;

        public int RandomExamId
        {
            get { return _randomExamId; }
            set { _randomExamId = value; }
        }

        public int ComputerServerNo
        {
            get { return _computerServerNo; }
            set { _computerServerNo = value; }
        }
        public int StatusID
        {
            get { return _statusID; }
            set { _statusID = value; }
        }

        public int IsStart
        {
            get { return _isStart; }
            set { _isStart = value; }
        }

        public bool HasPaper
        {
            get { return _hasPaper; }
            set { _hasPaper = value; }
        }

        public string RandomExamCode
        {
            get { return _randomExamCode; }
            set { _randomExamCode = value; }
        }

        public bool IsUpload
        {
            get { return _isUpload; }
            set { _isUpload = value; }
        }

        public int Downloaded
        {
            get { return _downloaded; }
            set { _downloaded = value; }
        }

        public RandomExamComputerServer()
        {
            
        }

        public RandomExamComputerServer(int? randomExamId, int? computerServerNo, int? statusId, int? isStart, 
            bool? hasPaper, string randomExamCode, bool? isUpload, int? downloaded)
        {
            _randomExamId = randomExamId ?? _randomExamId;
            _computerServerNo = computerServerNo ?? _computerServerNo;
            _statusID = statusId ?? _statusID;
            _isStart = isStart ?? _isStart;
            _hasPaper = hasPaper ?? _hasPaper;
            _randomExamCode = randomExamCode;
            _isUpload = isUpload ?? _isUpload;
            _downloaded = downloaded ?? _downloaded;
        }
    }
}
