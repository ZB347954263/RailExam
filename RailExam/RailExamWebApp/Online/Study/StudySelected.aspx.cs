using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;
using ComponentArt.Web.UI;
using System.Collections.Generic;

namespace RailExamWebApp.Online.Study
{
    public partial class StudySelected : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.CurrentStudent == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }

                //BindKnowledgeTree();
                //BindTrainTypeTree();
                //if (PrjPub.HasEditRight("教材管理") && PrjPub.IsServerCenter)
                //{
                //    HfUpdateRight.Value = "True";
                //}
                //else
                //{
                //    HfUpdateRight.Value = "False";
                //}

                int employeeID, postID;
                employeeID = PrjPub.CurrentStudent.EmployeeID;
                postID = PrjPub.CurrentStudent.PostID;

                KnowledgeTreeNodeBind(postID, null, null);
                BindPleuripotentPost(employeeID, postID);
            }

            string strRefresh = Request.Form.Get("Refresh");
            if (!string.IsNullOrEmpty(strRefresh))
            {
                int employeeID, postID;
                employeeID = PrjPub.CurrentStudent.EmployeeID;
                postID = PrjPub.CurrentStudent.PostID;

                KnowledgeTreeNodeBind(postID, null, null);
                BindPleuripotentPost(employeeID, postID);
            }
        }

        // 手动绑定KNOWLEDGE TreeView
        private void KnowledgeTreeNodeBind(int postID, string whereClause, TreeViewNode tvNode)
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
                    tvn.Value = postID.ToString();
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

        private void BindKnowledgeTree()
        {
            KnowledgeBLL knowledgeBLL = new KnowledgeBLL();

            IList<RailExam.Model.Knowledge> knowledgeList = knowledgeBLL.GetKnowledges();

            Pub.BuildComponentArtTreeView(tvKnowledge, (IList)knowledgeList, "KnowledgeId",
                "ParentId", "KnowledgeName", "KnowledgeName", "IdPath", null, null, null);
        }

        private void BindTrainTypeTree()
        {
            //TrainTypeBLL trainTypeBLL = new TrainTypeBLL();

            //IList<TrainType> trainTypes = trainTypeBLL.GetTrainTypes();

            //Pub.BuildComponentArtTreeView(tvTrainType, (IList)trainTypes, "TrainTypeID", "ParentID", "TypeName", "TypeName",
            //                              "IDPath", null, null, null);
        }

        //绑定一专多能节点下的Post
        private void BindPleuripotentPost(int employeeID, int postID)
        {
            OracleAccess oa = new OracleAccess();
            string selectEmployee = String.Format("select * from EMPLOYEE where EMPLOYEE_ID = {0}", employeeID);
            DataSet dsEmployee = oa.RunSqlDataSet(selectEmployee);
            if (dsEmployee != null && dsEmployee.Tables.Count > 0)
            {
                DataSet dsPosts = new DataSet();
                string selectPosts;

                string couldPostID = Convert.ToString(dsEmployee.Tables[0].Rows[0]["COULD_POST_ID"]);
                if (String.IsNullOrEmpty(couldPostID))
                {
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
                }
                else
                {
                    selectPosts = String.Format("select * from POST where POST_ID in ({0})", couldPostID);
                    dsPosts = oa.RunSqlDataSet(selectPosts);
                }

                if (dsPosts != null && dsPosts.Tables.Count > 0)
                {
                    TreeViewNode node = FindPleuripotentNode();
                    if (node != null)
                    {
                        int orgID = PrjPub.CurrentStudent.OrgID;
                        foreach (DataRow row in dsPosts.Tables[0].Rows)
                        {
                            TreeViewNode newNode = new TreeViewNode();
                            newNode.Text = Convert.ToString(row["POST_NAME"]);
                            newNode.Value = Convert.ToString(row["POST_ID"]);
                            newNode.ID = Convert.ToString(row["POST_ID"]);

                            node.Nodes.Add(newNode);
                        }

                        //string selectCount = "select max(level_num) from knowledge";
                        //DataSet dsLevelNumCount = oa.RunSqlDataSet(selectCount);
                        //if (dsLevelNumCount != null & dsLevelNumCount.Tables.Count > 0)
                        //{
                        //    int maxLevelNum = Convert.ToInt32(dsLevelNumCount.Tables[0].Rows[0][0]);
                        //    foreach (TreeViewNode postNode in node.Nodes)
                        //    {
                        //        BindKnowledgeByLevelNum(1, 0, postNode, postID);
                        //    }
                        //}

                        foreach (TreeViewNode postNode in node.Nodes)
                        {
                            KnowledgeTreeNodeBind(postID, "is_promotion = 1", postNode);
                        }
                    }
                }
            }
        }

        //一层一层的手工递归绑定一专多能下的KNOWLEDGE节点
        private void BindKnowledgeByLevelNum(int levelNum, int parentID, TreeViewNode tvNode, int postID)
        {
            KnowledgeBLL knowledgeBLL = new KnowledgeBLL();
            string whereClause = String.Format("is_promotion = 1 and level_num = {0} and (parent_id = {1} or {1} = 0)", levelNum, parentID),
                    orderKey = "level_num, order_index";
            IList<RailExam.Model.Knowledge> knowledgeList = knowledgeBLL.GetKnowledgesByWhereClause(whereClause, orderKey);
            foreach (RailExam.Model.Knowledge knowledge in knowledgeList)
            {
                TreeViewNode newNode = new TreeViewNode();
                newNode.ID = knowledge.KnowledgeId.ToString();
                newNode.Text = knowledge.KnowledgeName;
                newNode.Value = postID.ToString();

                string whereClause2 = String.Format("PARENT_ID = {0} and IS_PROMOTION = 1", knowledge.KnowledgeId);
                string orderKey2 = "LEVEL_NUM, ORDER_INDEX";
                IList<RailExam.Model.Knowledge> childKnowledgeList = knowledgeBLL.GetKnowledgesByWhereClause(whereClause2, orderKey2);
                if (childKnowledgeList.Count > 0)
                {
                    BindKnowledgeByLevelNum(++levelNum, knowledge.KnowledgeId, newNode, postID);
                }

                tvNode.Nodes.Add(newNode);
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
