using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class PostImport
    {
        private int _postID = 2;
        private string _postNamePath = "";

        public int PostID
        {
            get { return _postID; }
            set { _postID = value; }
        }

        public string PostNamePath
        {
            get { return _postNamePath; }
            set { _postNamePath = value; }
        }

        public PostImport(int? postID, string postNamePath)
        {
            _postID = postID ?? _postID;
            _postNamePath = postNamePath ?? _postNamePath;
        }
    }
}
