using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace RailExam.Model
{
    public class RandomExamArrange
    {
        private int _RandomExamArrangeId = 0;
        private int _RandomExamId = 0;       
        private string _UserIds = string.Empty;      
        private string _memo = string.Empty;

        public RandomExamArrange() { }

        public RandomExamArrange(
            int? RandomExamArrangeId,
            int? RandomExamId, 
            string UserIds,           
            string memo)
        {
            _RandomExamArrangeId = RandomExamArrangeId ?? _RandomExamArrangeId;
            _RandomExamId = RandomExamId ?? _RandomExamId; 
            _UserIds = UserIds;         
            _memo = memo;
        }


        public int RandomExamArrangeId
        {
            set
            {
                _RandomExamArrangeId = value;
            }
            get
            {
                return _RandomExamArrangeId;
            }
        }

        public int RandomExamId
        {
            set
            {
                _RandomExamId = value;
            }
            get
            {
                return _RandomExamId;
            }
        }      

        public string UserIds
        {
            set
            {
                _UserIds = value;
            }
            get
            {
                return _UserIds;
            }
        }      

        public string Memo
        {
            set
            {
                _memo = value;
            }
            get
            {
                return _memo;
            }
        }
    }
}

