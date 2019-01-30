using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class Synchronize
    {
        private int _detectInterval = 1;
        private int _retryCount = 1;

        public int DetectInterval
        {
            get { return _detectInterval; }
            set { _detectInterval = value; }
        }

        public int RetryCount
        {
            get { return _retryCount; }
            set { _retryCount = value; }
        }

        public Synchronize(int? detectInterval,int? retryCount)
        {
            _detectInterval = detectInterval ?? _detectInterval;
            _retryCount = retryCount ?? _retryCount;
        }

        public Synchronize()
        {
            
        }
    }
}
