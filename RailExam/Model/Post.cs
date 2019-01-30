using System;
namespace RailExam.Model
{
    /// <summary>
    /// 业务实体：组织机构
    /// </summary>
    public class Post
    {
        #region 内部成员变量

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

        #region 实体属性

        /// <summary>
        /// 岗位编号
        /// </summary>
        public int PostId
        {
            get { return _postId; }
            set { _postId = value; }
        }

        /// <summary>
        /// 岗位深度
        /// </summary>
        public int PostLevel
        {
            get { return _postLevel; }
            set { _postLevel = value; }
        }

        /// <summary>
        /// 父级岗位
        /// </summary>
        public int ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        /// <summary>
        /// 岗位树路径
        /// </summary>
        public string IdPath
        {
            get { return _idPath; }
            set { _idPath = value; }
        }

        /// <summary>
        /// 排序序号
        /// </summary>
        public int OrderIndex
        {
            get { return _orderIndex; }
            set { _orderIndex = value; }
        }

        /// <summary>
        /// 岗位名称
        /// </summary>
        public string PostName
        {
            get { return _postName; }
            set { _postName = value; }
        }

        /// <summary>
        /// 岗位描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// 岗位是否技师
        /// </summary>
        public int Technician
        {
            get { return _technician; }
            set { _technician = value; }
        }

        /// <summary>
        /// 岗位是否晋升
        /// </summary>
        public int Promotion
        {
            get { return _promotion; }
            set { _promotion = value; }
        }

        /// <summary>
        /// 岗位备注
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
        /// 缺省构造函数
        /// </summary>
        public Post() { }

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="postId">工作岗位ID</param>
        /// <param name="postLevel">工作岗位类别：1－系统、2－工种、3－职名</param>
        /// <param name="parentId">父工作岗位ID</param>
        /// <param name="idPath">机构ID路径</param>
        /// <param name="orderIndex">排序索引</param>
        /// <param name="postName">岗位名称</param>
        /// <param name="description">简介</param>
        /// <param name="technician">工作岗位类别为3的工作岗位是否技师岗位</param>
        /// <param name="promotion">工作岗位类别为3的工作岗位是否晋升岗位</param>
        /// <param name="memo">备注</param>
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