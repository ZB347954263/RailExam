using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;
using RailExam.DAL;


namespace RailExam.BLL
{
    public class PostBLL
    {
        /// <summary>
        /// 内部成员
        /// </summary>
        private static readonly PostDAL dal = new PostDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

        /// <summary>
        /// 取多个岗位信息
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="postLevel"></param>
        /// <param name="parentId"></param>
        /// <param name="idPath"></param>
        /// <param name="orderIndex"></param>
        /// <param name="postName"></param>
        /// <param name="description"></param>
        /// <param name="technician"></param>
        /// <param name="promotion"></param>
        /// <param name="memo"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="orderBy"></param>
        /// <returns>岗位信息列表</returns>
        public IList<Post> GetPosts(int postId,
                                    int postLevel,
                                    int parentId,
                                    string idPath,
                                    int orderIndex,
                                    string postName,
                                    string description,
                                    int technician,
                                    int promotion,                                    
                                    string memo,
                                    string promotion_post_id,
                                    int startRowIndex,
                                    int maximumRows,
                                    string orderBy)
        {
            return dal.GetPosts(postId,
                                postLevel,
                                parentId,
                                idPath,
                                orderIndex,
                                postName,
                                description,
                                technician,
                                promotion,
                                promotion_post_id,
                                memo,
                                startRowIndex,
                                maximumRows,
                                orderBy);
        }
        /// <summary>
        /// 按缺省值查所有记录
        /// </summary>
        /// <returns></returns>
        public IList<Post> GetPosts()
        {
            IList<Post> postList = dal.GetPosts();

            return postList;
        }

		public IList<Post> GetPosts(string tableName)
		{
			IList<Post> postList = dal.GetPosts(tableName);

			return postList;
		}

        /// <summary>
        /// 取单个岗位信息  
        /// </summary>
        /// <param name="postId"></param>
        /// <returns>岗位信息</returns>
        public Post GetPost(int postId)
        {
            return dal.GetPost(postId);
        }

        /// <summary>
        /// 新增岗位信息
        /// </summary>
        /// <param name="post">新增的工作岗位信息</param>
        /// <returns></returns>
        public void AddPost(Post post)
        {
            objLogBll.WriteLog("新增岗位“"  +　post.PostName +  "”基本信息");
            dal.AddPost(post);
        }

		/// <summary>
		/// 新增岗位信息
		/// </summary>
		/// <param name="post">新增的工作岗位信息</param>
		/// <returns></returns>
		public int AddPost(Database db, DbTransaction transaction, Post post)
		{
			return dal.AddPost(db,transaction,post);
		}

        /// <summary>
        /// 删除岗位信息
        /// </summary>
        /// <param name="postId">要删除的岗位ID</param>
        public void DeletePost(int postId,ref int errorCode)
        {
            int code = 0;
            string strName = GetPost(postId).PostName;
            dal.DeletePost(postId,ref code);
            errorCode = code;

            if(code == 0)
            {
                objLogBll.WriteLog("删除岗位“" + strName + "”基本信息");
            }
        }

        /// <summary>
        /// 删除岗位信息
        /// </summary>
        /// <param name="post">要删除的岗位</param>
        public void DeletePost(Post post)
        {
            int code = 0;
            DeletePost(post.PostId, ref code);
        }

        /// <summary>
        /// 更新岗位信息
        /// </summary>
        /// <param name="post">更新后的岗位信息</param>
        public void UpdatePost(Post post)
        {
            objLogBll.WriteLog("修改岗位“" + post.PostName + "”基本信息");
            dal.UpdatePost(post);
        }

        /// <summary>
        /// 是否可以上移节点
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public bool MoveUp(int postId)
        {
            return dal.Move(postId, true);
        }

        /// <summary>
        /// 是否可以下移节点
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public bool MoveDown(int postId)
        {
            return dal.Move(postId, false);
        }

        public int GetPostIDByPostNamePath(string strNamePath)
        {
            return dal.GetPostIDByPostNamePath(strNamePath);
        }

        public IList<Post> GetPostsByLevel(int level)
        {
            return dal.GetPostsByLevel(level);
        }

		public IList<Post> GetPostsByWhereClause(string whereClause)
		{
			return dal.GetPostsByWhereClause(whereClause);
		}

        public IList<Post> GetPostsByParentID(int parentID)
        {
            return dal.GetPostsByParentID(parentID);
        }

		public Post GetPostByRailEdu(int PostID)
		{
			return dal.GetPostByRailEdu(PostID);
		}

		public int GetPostByRailExam(int PostID)
		{
			return dal.GetPostByRailExam(PostID);
		}
    }
}
