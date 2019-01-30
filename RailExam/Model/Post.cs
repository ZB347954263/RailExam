using System;
namespace RailExam.Model
{
    /// <summary>
    /// ҵ��ʵ�壺��֯����
    /// </summary>
    public class Post
    {
        #region �ڲ���Ա����

        private int _postId = 0;
        private int _postLevel = 3;
        private int _parentId = 1;
        private string _idPath = string.Empty;
        private int _orderIndex = 1;
        private string _postName = string.Empty;
        private string _description = string.Empty;
        private int _technician = 0;
        private int _promotion = 0;
        private string _memo = string.Empty;
        private string _promotion_post_id = String.Empty;

        #endregion

        #region ʵ������

        /// <summary>
        /// ��λ���
        /// </summary>
        public int PostId
        {
            get { return _postId; }
            set { _postId = value; }
        }

        /// <summary>
        /// ��λ���
        /// </summary>
        public int PostLevel
        {
            get { return _postLevel; }
            set { _postLevel = value; }
        }

        /// <summary>
        /// ������λ
        /// </summary>
        public int ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        /// <summary>
        /// ��λ��·��
        /// </summary>
        public string IdPath
        {
            get { return _idPath; }
            set { _idPath = value; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public int OrderIndex
        {
            get { return _orderIndex; }
            set { _orderIndex = value; }
        }

        /// <summary>
        /// ��λ����
        /// </summary>
        public string PostName
        {
            get { return _postName; }
            set { _postName = value; }
        }

        /// <summary>
        /// ��λ����
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// ��λ�Ƿ�ʦ
        /// </summary>
        public int Technician
        {
            get { return _technician; }
            set { _technician = value; }
        }

        /// <summary>
        /// ��λ�Ƿ����
        /// </summary>
        public int Promotion
        {
            get { return _promotion; }
            set { _promotion = value; }
        }

        /// <summary>
        /// ��λ��ע
        /// </summary>
        public string Memo
        {
            get { return _memo; }
            set { _memo = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string PromotionPostID
        {
            get { return _promotion_post_id; }
            set { _promotion_post_id = value; }
        }

        #endregion


        /// <summary>
        /// ȱʡ���캯��
        /// </summary>
        public Post() { }

        /// <summary>
        /// �������Ĺ��캯��
        /// </summary>
        /// <param name="postId">������λID</param>
        /// <param name="postLevel">������λ���1��ϵͳ��2�����֡�3��ְ��</param>
        /// <param name="parentId">��������λID</param>
        /// <param name="idPath">����ID·��</param>
        /// <param name="orderIndex">��������</param>
        /// <param name="postName">��λ����</param>
        /// <param name="description">���</param>
        /// <param name="technician">������λ���Ϊ3�Ĺ�����λ�Ƿ�ʦ��λ</param>
        /// <param name="promotion">������λ���Ϊ3�Ĺ�����λ�Ƿ������λ</param>
        /// <param name="memo">��ע</param>
        public Post(int? postId,
                    int? postLevel,
                    int? parentId,
                    string idPath,
                    int? orderIndex,
                    string postName,
                    string description,
                    int? technician,
                    int? promotion,
                    string memo
                    )
        {
            _postId = postId ?? _postId;
            _postLevel = postLevel ?? _postLevel;
            _parentId = parentId ?? _parentId;
            _idPath = idPath;
            _orderIndex = orderIndex ?? _orderIndex;
            _postName = postName;
            _description = description;
            _technician = technician ?? _technician;
            _promotion = promotion ?? _promotion;
            _memo = memo;
        }
    }
}