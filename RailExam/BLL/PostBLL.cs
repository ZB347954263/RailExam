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
        /// �ڲ���Ա
        /// </summary>
        private static readonly PostDAL dal = new PostDAL();
        private SystemLogBLL objLogBll = new SystemLogBLL();

        /// <summary>
        /// ȡ�����λ��Ϣ
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
        /// <returns>��λ��Ϣ�б�</returns>
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
        /// ��ȱʡֵ�����м�¼
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
        /// ȡ������λ��Ϣ  
        /// </summary>
        /// <param name="postId"></param>
        /// <returns>��λ��Ϣ</returns>
        public Post GetPost(int postId)
        {
            return dal.GetPost(postId);
        }

        /// <summary>
        /// ������λ��Ϣ
        /// </summary>
        /// <param name="post">�����Ĺ�����λ��Ϣ</param>
        /// <returns></returns>
        public void AddPost(Post post)
        {
            objLogBll.WriteLog("������λ��"  +��post.PostName +  "��������Ϣ");
            dal.AddPost(post);
        }

		/// <summary>
		/// ������λ��Ϣ
		/// </summary>
		/// <param name="post">�����Ĺ�����λ��Ϣ</param>
		/// <returns></returns>
		public int AddPost(Database db, DbTransaction transaction, Post post)
		{
			return dal.AddPost(db,transaction,post);
		}

        /// <summary>
        /// ɾ����λ��Ϣ
        /// </summary>
        /// <param name="postId">Ҫɾ���ĸ�λID</param>
        public void DeletePost(int postId,ref int errorCode)
        {
            int code = 0;
            string strName = GetPost(postId).PostName;
            dal.DeletePost(postId,ref code);
            errorCode = code;

            if(code == 0)
            {
                objLogBll.WriteLog("ɾ����λ��" + strName + "��������Ϣ");
            }
        }

        /// <summary>
        /// ɾ����λ��Ϣ
        /// </summary>
        /// <param name="post">Ҫɾ���ĸ�λ</param>
        public void DeletePost(Post post)
        {
            int code = 0;
            DeletePost(post.PostId, ref code);
        }

        /// <summary>
        /// ���¸�λ��Ϣ
        /// </summary>
        /// <param name="post">���º�ĸ�λ��Ϣ</param>
        public void UpdatePost(Post post)
        {
            objLogBll.WriteLog("�޸ĸ�λ��" + post.PostName + "��������Ϣ");
            dal.UpdatePost(post);
        }

        /// <summary>
        /// �Ƿ�������ƽڵ�
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public bool MoveUp(int postId)
        {
            return dal.Move(postId, true);
        }

        /// <summary>
        /// �Ƿ�������ƽڵ�
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
