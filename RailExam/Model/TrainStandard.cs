using System;
using System.Collections.Generic;
using System.Text;

namespace RailExam.Model
{
    public class TrainStandard
    {
        /// <summary>
        /// ��Ա����
        /// </summary>
        private int _trainStandardID = 0;
        private int _postID = 0;
        private int _typeID = 0;
        private string _trainTime = string.Empty;
        private string _trainContent = string.Empty;
        private string _trainForm = string.Empty;
        private string _examForm = string.Empty;
        private string _description = string.Empty;
        private string _memo = string.Empty;

        private string _postName = string.Empty;
        private string _typeName = string.Empty;

        /// <summary>
        /// ʵ������
        /// </summary>
        public int TrainStandardID
        {
            get { return _trainStandardID; }
            set { _trainStandardID = value; }
        }

        public int PostID
        {
            get { return _postID; }
            set { _postID = value; }
        }

        public int TypeID
        {
            get { return _typeID; }
            set { _typeID = value; }
        }

        public string TrainTime
        {
            get { return _trainTime; }
            set { _trainTime = value; }
        }

        public string TrainContent
        {
            get { return _trainContent; }
            set { _trainContent = value; }
        }

        public string TrainForm
        {
            get { return _trainForm; }
            set { _trainForm = value; }
        }

        public string ExamForm
        {
            get { return _examForm; }
            set { _examForm = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        public string PostName
        {
            get { return _postName; }
            set { _postName = value; }
        }

        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        /// <summary>
        /// ȱʡ���캯��
        /// </summary>
        public TrainStandard() { }

        /// <summary>
        /// �������Ĺ��캯��
        /// </summary>
        /// <param name="trainStandardID">��ѵ�淶ID</param>
        /// <param name="postID">������λID</param>
        /// <param name="typeID">��ѵ���ID</param>
        /// <param name="trainTime">��ѵʱ��</param>
        /// <param name="trainContent">��ѵ����</param>
        /// <param name="trainForm">��֯��ʽ</param>
        /// <param name="examForm">������ʽ</param>
        /// <param name="description">����</param>
        /// <param name="memo">��ע</param>
        public TrainStandard(int? trainStandardID,
                         int? postID,
                         int? typeID,
                         string trainTime,
                         string trainContent,
                         string trainForm,
                         string examForm,
                         string description,
                         string memo)
        {
            _trainStandardID = trainStandardID ?? _trainStandardID;
            _postID = postID ?? _postID;
            _typeID = typeID ?? _typeID;
            _trainTime = trainTime;
            _trainContent = trainContent;
            _trainForm = trainForm;
            _examForm = examForm;
            _description = description;
            _memo = memo;
        }
    }
}
