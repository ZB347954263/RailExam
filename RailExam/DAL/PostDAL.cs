using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using RailExam.Model;
using DSunSoft.Data;

namespace RailExam.DAL
{
    public class PostDAL
    {
        private static Hashtable _ormTable;
        private int _recordCount = 0;

        static PostDAL()
        {
            _ormTable = new Hashtable();

            // 源名称必须小写
            _ormTable.Add("postid", "POST_ID");
            _ormTable.Add("postlevel", "POST_LEVEL");
            _ormTable.Add("parentid", "PARENT_ID");
            _ormTable.Add("idpath", "ID_PATH");
            _ormTable.Add("orderindex", "ORDER_INDEX");
            _ormTable.Add("postname", "POST_NAME");
            _ormTable.Add("description", "DESCRIPTION");
            _ormTable.Add("technician", "TECHNICIAN");
            _ormTable.Add("promotion", "PROMOTION");
            _ormTable.Add("memo", "MEMO");
            _ormTable.Add("promotionpostid", "PROMOTION_POST_ID");
        }

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
        /// <returns>岗位列表</returns>
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
                                    string promotionPostID,
                                    int startRowIndex,
                                    int maximumRows,
                                    string orderBy)
        {
            IList<Post> posts = new List<Post>();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_POST_S";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_start_row_index", DbType.Int32, startRowIndex);
            db.AddInParameter(dbCommand, "p_page_size", DbType.Int32, maximumRows);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, GetMappingOrderBy(orderBy));
            db.AddOutParameter(dbCommand, "p_count", DbType.Int32, 4);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Post post = CreateModelObject(dataReader);
                    post.PromotionPostID = dataReader[GetMappingFieldName("PromotionPostID")].ToString();
                    posts.Add(post);
                }
            }

            _recordCount = Convert.ToInt32(db.GetParameterValue(dbCommand, "p_count"));

            return posts;
        }

        /// <summary>
        /// 返回全部岗位信息
        /// </summary>
        /// <returns>全部岗位信息</returns>
        public IList<Post> GetPosts()
        {
            IList<Post> posts = new List<Post>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_GET_ALL");

            db.AddInParameter(dbCommand, "p_table_name", DbType.String, "POST");
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, "POST_LEVEL, ORDER_INDEX ASC");

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Post post = CreateModelObject(dataReader);
                    post.PromotionPostID = dataReader[GetMappingFieldName("PromotionPostID")].ToString();
                    posts.Add(post);
                }
            }

            _recordCount = posts.Count;

            return posts;
        }

        /// <summary>
        /// 返回全部岗位信息
        /// </summary>
        /// <returns>全部岗位信息</returns>
        public IList<Post> GetPosts(string tableName)
        {
            IList<Post> posts = new List<Post>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_GET_ALL");

            db.AddInParameter(dbCommand, "p_table_name", DbType.String, tableName);
            db.AddInParameter(dbCommand, "p_order_by", DbType.String, "POST_LEVEL, ORDER_INDEX ASC");

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Post post = CreateModelObject(dataReader);
                    post.PromotionPostID = dataReader[GetMappingFieldName("PromotionPostID")].ToString();
                    posts.Add(post);
                }
            }

            _recordCount = posts.Count;

            return posts;
        }


        public IList<Post> GetPostsByLevel(int level)
        {
            IList<Post> posts = new List<Post>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_POST_G_Level");

            db.AddInParameter(dbCommand, "p_level_num", DbType.String, level);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Post post = CreateModelObject(dataReader);
                    post.PromotionPostID = dataReader[GetMappingFieldName("PromotionPostID")].ToString();
                    posts.Add(post);
                }
            }

            return posts;
        }

        public IList<Post> GetPostsByWhereClause(string whereClause)
        {
            IList<Post> posts = new List<Post>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_POST_WhereClause");

            db.AddInParameter(dbCommand, "p_sql", DbType.String, whereClause);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Post post = CreateModelObject(dataReader);
                    post.PromotionPostID = dataReader[GetMappingFieldName("PromotionPostID")].ToString();
                    posts.Add(post);
                }
            }

            return posts;
        }

        public IList<Post> GetPostsByParentID(int parentID)
        {
            IList<Post> posts = new List<Post>();
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("USP_POST_G_Parent");

            db.AddInParameter(dbCommand, "p_parent_id", DbType.String, parentID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    Post post = CreateModelObject(dataReader);
                    post.PromotionPostID = dataReader[GetMappingFieldName("PromotionPostID")].ToString();
                    posts.Add(post);
                }
            }

            return posts;
        }

        public Post GetPostByRailEdu(int PostID)
        {
            Post post = new Post();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_POST_G_RailEdu";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, PostID);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    post = CreateModelObject(dataReader);
                    post.PromotionPostID = dataReader[GetMappingFieldName("PromotionPostID")].ToString();
                }
            }

            return post;
        }


        public int GetPostByRailExam(int PostID)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_POST_G_RailEdu_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_railedu_post_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_railexam_post_id", DbType.Int32, PostID);

            db.ExecuteNonQuery(dbCommand);


            return Convert.ToInt32(db.GetParameterValue(dbCommand, "p_railedu_post_id"));
        }


        /// <summary>
        /// 取单个岗位信息
        /// </summary>
        /// <param name="postId">岗位ID</param>
        /// <returns>岗位对象</returns>
        public Post GetPost(int postId)
        {
            Post post = new Post();

            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_POST_G";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, postId);

            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                while (dataReader.Read())
                {
                    post = CreateModelObject(dataReader);
                    post.PromotionPostID = dataReader[GetMappingFieldName("PromotionPostID")].ToString();
                }
            }

            return post;
        }

        public int GetPostIDByPostNamePath(string strNamePath)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_POST_IMPORT";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_post_id", DbType.Int32, 4);
            db.AddInParameter(dbCommand, "p_name_path", DbType.String, strNamePath);

            db.ExecuteNonQuery(dbCommand);

            return Convert.ToInt32(db.GetParameterValue(dbCommand, "p_post_id"));
        }

        /// <summary>
        /// 新增岗位信息
        /// </summary>
        /// <param name="post">新增的岗位信息</param>
        /// <returns></returns>
        public void AddPost(Post post)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_POST_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_post_id", DbType.Int32, 4);
            db.AddOutParameter(dbCommand, "p_post_level", DbType.Int32, post.PostLevel);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, post.ParentId);
            db.AddOutParameter(dbCommand, "p_id_path", DbType.String, 50);
            db.AddOutParameter(dbCommand, "p_order_index", DbType.Int32, post.OrderIndex);
            db.AddInParameter(dbCommand, "p_post_name", DbType.String, post.PostName);
            db.AddInParameter(dbCommand, "p_technician", DbType.String, post.Technician);
            db.AddInParameter(dbCommand, "p_promotion", DbType.String, post.Promotion);
            db.AddInParameter(dbCommand, "p_description", DbType.String, post.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, post.Memo);
            db.AddInParameter(dbCommand, "p_promotion_post_id", DbType.String, post.PromotionPostID);

            db.ExecuteNonQuery(dbCommand);

            post.PostLevel = (int)db.GetParameterValue(dbCommand, "p_post_level");
            post.IdPath = (string)db.GetParameterValue(dbCommand, "p_id_path");
            post.OrderIndex = (int)db.GetParameterValue(dbCommand, "p_order_index");
        }

        /// <summary>
        /// 新增岗位信息
        /// </summary>
        /// <param name="post">新增的岗位信息</param>
        /// <returns></returns>
        public int AddPost(Database db, DbTransaction transaction, Post post)
        {
            string sqlCommand = "USP_POST_I";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddOutParameter(dbCommand, "p_post_id", DbType.Int32, 4);
            db.AddOutParameter(dbCommand, "p_post_level", DbType.Int32, post.PostLevel);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, post.ParentId);
            db.AddOutParameter(dbCommand, "p_id_path", DbType.String, 50);
            db.AddOutParameter(dbCommand, "p_order_index", DbType.Int32, post.OrderIndex);
            db.AddInParameter(dbCommand, "p_post_name", DbType.String, post.PostName);
            db.AddInParameter(dbCommand, "p_technician", DbType.String, post.Technician);
            db.AddInParameter(dbCommand, "p_promotion", DbType.String, post.Promotion);
            db.AddInParameter(dbCommand, "p_description", DbType.String, post.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, post.Memo);
            db.AddInParameter(dbCommand, "p_promotion_post_id", DbType.String, post.PromotionPostID);

            db.ExecuteNonQuery(dbCommand, transaction);

            return (int)db.GetParameterValue(dbCommand, "p_post_id");
        }

        /// <summary>
        /// 删除岗位岗位
        /// </summary>
        /// <param name="postId">要删除的岗位ID</param>
        public void DeletePost(int postId, ref int errorCode)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_POST_D";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, postId);

            DbConnection connection = db.CreateConnection();
            connection.Open();
            DbTransaction transaction = connection.BeginTransaction();

            try
            {
                db.ExecuteNonQuery(dbCommand, transaction);
                transaction.Commit();
                errorCode = 0;
            }
            catch (OracleException ex)
            {
                transaction.Rollback();
                errorCode = ex.Code;
            }
            connection.Close();
        }

        /// <summary>
        /// 更新岗位信息
        /// </summary>
        /// <param name="post">更新后的岗位信息</param>
        public void UpdatePost(Post post)
        {
            Database db = DatabaseFactory.CreateDatabase();

            string sqlCommand = "USP_POST_U";
            DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

            db.AddInParameter(dbCommand, "p_post_id", DbType.Int32, post.PostId);
            db.AddOutParameter(dbCommand, "p_post_level", DbType.Int32, post.PostLevel);
            db.AddInParameter(dbCommand, "p_parent_id", DbType.Int32, post.ParentId);
            db.AddOutParameter(dbCommand, "p_id_path", DbType.String, 50);
            db.AddInParameter(dbCommand, "p_order_index", DbType.Int32, post.OrderIndex);
            db.AddInParameter(dbCommand, "p_post_name", DbType.String, post.PostName);
            db.AddInParameter(dbCommand, "p_technician", DbType.Int32, post.Technician);
            db.AddInParameter(dbCommand, "p_promotion", DbType.Int32, post.Promotion);
            db.AddInParameter(dbCommand, "p_description", DbType.String, post.Description);
            db.AddInParameter(dbCommand, "p_memo", DbType.String, post.Memo);
            db.AddInParameter(dbCommand, "p_promotion_post_id", DbType.String, post.PromotionPostID);

            db.ExecuteNonQuery(dbCommand);

            post.PostLevel = (int)db.GetParameterValue(dbCommand, "p_post_level");
            post.IdPath = (string)db.GetParameterValue(dbCommand, "p_id_path");
        }

        /// <summary>
        /// 查询结果记录数
        /// </summary>
        public int RecordCount
        {
            get
            {
                return _recordCount;
            }
        }

        public static string GetMappingFieldName(string propertyName)
        {
            return (string)_ormTable[propertyName.ToLower()];
        }

        public static string GetMappingOrderBy(string orderBy)
        {
            orderBy = orderBy.Trim();

            if (string.IsNullOrEmpty(orderBy))
            {
                return string.Empty;
            }

            string mappingOrderBy = string.Empty;
            string[] orderByConditions = orderBy.Split(new char[] { ',' });

            foreach (string s in orderByConditions)
            {
                string orderByCondition = s.Trim();

                string[] orderBysOfOneCondition = orderByCondition.Split(new char[] { ' ' });

                if (orderBysOfOneCondition.Length == 0)
                {
                    continue;
                }
                else
                {
                    if (mappingOrderBy != string.Empty)
                    {
                        mappingOrderBy += ',';
                    }

                    if (orderBysOfOneCondition.Length == 1)
                    {
                        mappingOrderBy += GetMappingFieldName(orderBysOfOneCondition[0]);
                    }
                    else
                    {
                        mappingOrderBy += GetMappingFieldName(orderBysOfOneCondition[0]) + ' ' + orderBysOfOneCondition[1];
                    }
                }
            }

            return mappingOrderBy;
        }

        public static Post CreateModelObject(IDataReader dataReader)
        {

            return new Post(
           DataConvert.ToInt(dataReader[GetMappingFieldName("PostId")]),
           DataConvert.ToInt(dataReader[GetMappingFieldName("PostLevel")]),
           DataConvert.ToInt(dataReader[GetMappingFieldName("ParentId")]),
           DataConvert.ToString(dataReader[GetMappingFieldName("IdPath")]),
           DataConvert.ToInt(dataReader[GetMappingFieldName("OrderIndex")]),
           DataConvert.ToString(dataReader[GetMappingFieldName("PostName")]),
           DataConvert.ToString(dataReader[GetMappingFieldName("Description")]),
           DataConvert.ToInt(dataReader[GetMappingFieldName("Technician")]),
           DataConvert.ToInt(dataReader[GetMappingFieldName("Promotion")]),
           DataConvert.ToString(dataReader[GetMappingFieldName("Memo")]));
        }

        /// <summary>
        /// 是否可以移动节点
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="bUp"></param>
        /// <returns></returns>
        public bool Move(int postId, bool bUp)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCmd = db.GetStoredProcCommand("USP_TREE_NODE_M");

            db.AddInParameter(dbCmd, "p_table_name", DbType.String, "POST");
            db.AddInParameter(dbCmd, "p_id_field_name", DbType.String, "POST_ID");
            db.AddInParameter(dbCmd, "p_id", DbType.Int32, postId);
            db.AddInParameter(dbCmd, "p_direction", DbType.Int32, (bUp ? 1 : 0));
            db.AddOutParameter(dbCmd, "p_result", DbType.Int32, 4);

            db.ExecuteNonQuery(dbCmd);

            return ((int)db.GetParameterValue(dbCmd, "p_result") == 1) ? true : false;
        }
    }
}
