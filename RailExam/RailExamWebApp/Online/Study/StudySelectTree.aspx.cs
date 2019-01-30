using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Online.Study
{
    public partial class StudySelectTree : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int postID = Convert.ToInt32(Request.QueryString.Get("PostID"));
                KnowledgeTreeNodeBind(postID, null, null);
                BindPleuripotentPost(postID);
            }
        }

        // 手动绑定KNOWLEDGE TreeView
        private void KnowledgeTreeNodeBind(int? postID, string whereClause, TreeViewNode tvNode)
        {
            ComponentArt.Web.UI.TreeView virtualTree = new ComponentArt.Web.UI.TreeView();

            KnowledgeBLL knowledgeBLL = new KnowledgeBLL();
            IList<RailExam.Model.Knowledge> knowledgeList;
            if (String.IsNullOrEmpty(whereClause))
            {
                knowledgeList = knowledgeBLL.GetKnowledges();
            }
            else
            {
                knowledgeList = knowledgeBLL.GetKnowledgesByWhereClause(whereClause, "level_num, order_index");
            }
            if (knowledgeList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (RailExam.Model.Knowledge knowledge in knowledgeList)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = knowledge.KnowledgeId.ToString();
                    tvn.Value = postID == null ? String.Empty : postID.ToString();
                    tvn.Text = knowledge.KnowledgeName;
                    tvn.ToolTip = knowledge.KnowledgeName;

                    if (knowledge.ParentId == 0)
                    {
                        if (tvNode == null)
                        {
                            this.tvKnowledge.Nodes.Add(tvn);
                        }
                        else
                        {
                            virtualTree.Nodes.Add(tvn);
                        }
                    }
                    else
                    {
                        try
                        {
                            if (tvNode == null)
                            {
                                tvKnowledge.FindNodeById(knowledge.ParentId.ToString()).Nodes.Add(tvn);
                            }
                            else
                            {
                                virtualTree.FindNodeById(knowledge.ParentId.ToString()).Nodes.Add(tvn);
                            }
                        }
                        catch
                        {
                            tvKnowledge.Nodes.Clear();
                            return;
                        }
                    }
                }

                if (tvNode != null)
                {
                    foreach (TreeViewNode node in virtualTree.Nodes)
                    {
                        tvNode.Nodes.Add(node);
                    }
                }
            }
        }

        //绑定一专多能节点下的Post
        private void BindPleuripotentPost(int postID)
        {
            OracleAccess oa = new OracleAccess();

            DataSet dsPosts = new DataSet();
            string selectPosts;


            string selectPost = String.Format("select * from POST where POST_ID = {0}", postID);
            DataSet dsPost = oa.RunSqlDataSet(selectPost);
            if (dsPost != null && dsPost.Tables.Count > 0)
            {
                string promotionPostID = Convert.ToString(dsPost.Tables[0].Rows[0]["PROMOTION_POST_ID"]);
                if (!String.IsNullOrEmpty(promotionPostID))
                {
                    selectPosts = String.Format("select * from POST where POST_ID in ({0})", promotionPostID);
                    dsPosts = oa.RunSqlDataSet(selectPosts);
                }
            }


            if (dsPosts != null && dsPosts.Tables.Count > 0)
            {
                TreeViewNode node = FindPleuripotentNode();
                if (node != null)
                {
                    foreach (DataRow row in dsPosts.Tables[0].Rows)
                    {
                        TreeViewNode newNode = new TreeViewNode();
                        newNode.Text = Convert.ToString(row["POST_NAME"]);
                        newNode.Value = Convert.ToString(row["POST_ID"]);
                        newNode.ID = Convert.ToString(row["POST_ID"]);

                        node.Nodes.Add(newNode);
                    }

                    foreach (TreeViewNode postNode in node.Nodes)
                    {
                        KnowledgeTreeNodeBind(postID, "is_promotion = 1", postNode);
                    }
                }
            }
        }

        //找到一专多能节点
        private TreeViewNode FindPleuripotentNode()
        {
            KnowledgeBLL knowledgeBLL = new KnowledgeBLL();

            foreach (TreeViewNode node in this.tvKnowledge.Nodes)
            {
                int id = Int32.Parse(node.ID);
                RailExam.Model.Knowledge knowledge = knowledgeBLL.GetKnowledge(id);
                if (knowledge.IsTemplate)
                {
                    return node;
                }
            }
            return null;
        }
    }
}
