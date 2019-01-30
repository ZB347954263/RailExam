using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace RailExam.Model
{
    public class OtherKnowledge
    {
        /// <summary>
        /// 内部成员变量
        /// </summary>
        private int _otherKnowledgeID = 0;
        private int _parentID = 1;
        private string _idPath = string.Empty;
        private int _levelNum = 2;
        private int _orderIndex = 1;
        private string _otherKnowledgeName = string.Empty;
        private string _description = string.Empty;
        private string _memo = string.Empty;

        public OtherKnowledge() { }
   
        public OtherKnowledge(
            int? otherKnowledgeID,
            int? parentID,
            string idPath,
            int? levelNum,
            int? orderIndex,
            string otherKnowledgeName,
            string description,
            string memo)
        {
            _otherKnowledgeID = otherKnowledgeID ?? _otherKnowledgeID;
            _parentID = parentID ?? _parentID;
            _idPath = idPath;
            _levelNum = levelNum ?? _levelNum;
            _orderIndex = orderIndex ?? _orderIndex;
            _otherKnowledgeName = otherKnowledgeName;
            _description = description;
            _memo = memo;
        }

        public int OtherKnowledgeID
        {
            set
            {
                _otherKnowledgeID = value;
            }
            get
            {
                return _otherKnowledgeID;
            }
        }

        public int ParentID
        {
            set
            {
                _parentID = value;
            }
            get
            {
                return _parentID;
            }
        }

        public string IdPath
        {
            set
            {
                _idPath = value;
            }
            get
            {
                return _idPath;
            }
        }

        public int LevelNum
        {
            set
            {
                _levelNum = value;
            }
            get
            {
                return _levelNum;
            }
        }

        public int OrderIndex
        {
            set
            {
                _orderIndex = value;
            }
            get
            {
                return _orderIndex;
            }
        }

        public string OtherKnowledgeName
        {
            set
            {
                _otherKnowledgeName = value;
            }
            get
            {
                return _otherKnowledgeName;
            }
        }

        public string Description
        {
            set
            {
                _description = value;
            }
            get
            {
                return _description;
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
