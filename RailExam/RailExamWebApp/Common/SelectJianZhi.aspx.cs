using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.Model;
using RailExam.BLL;

namespace RailExamWebApp.Common
{
    public partial class SelectJianZhi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPostTree(); 
            }
        }

        //递归赋值已选项
        private void SetTreeNodeSelected(string ids, TreeViewNodeCollection tvNodes)
        {
            foreach (TreeViewNode node in tvNodes)
            {
                if (node.Nodes.Count > 0)
                {
                    SetTreeNodeSelected(ids, node.Nodes);
                }
                else
                {
                    node.Checked = ids.Contains(node.Value);
                }
            }
        }

        private void BindPostTree()
        {
            string strID = Request.QueryString["postId"];
            string selectedIds = Request.QueryString["ids"].ToString();
            
            //string[] strIDS = { };
            //if (!string.IsNullOrEmpty(strID))
            //{
            //    strIDS = strID.Split(',');
            //}
            int postID = 0;
            int.TryParse(strID, out postID);
 
            RailExam.BLL.PostBLL PostBLL = new RailExam.BLL.PostBLL();

            Post post = PostBLL.GetPost(postID);
            string[] strIDs ={ };
            if (!string.IsNullOrEmpty(post.PromotionPostID))
            {
                strIDs = post.PromotionPostID.Split(',');
            }

            TreeViewNode tvn = null;
            foreach (string strID2 in strIDs)
            {
                int.TryParse(strID2, out postID);
                Post post2 = PostBLL.GetPost(postID);
                tvn = new TreeViewNode();
                tvn.ID = post2.PostId.ToString();
                tvn.Value = post2.PostId.ToString();
                tvn.Text = post2.PostName;
                tvn.ToolTip = post2.PostName;
                tvn.ShowCheckBox = true;
                if (selectedIds.Contains(post2.PostId.ToString()))
                {
                    tvn.Checked = true;
                }
                tvPost.Nodes.Add(tvn);
            }

            //if (postsList.Count > 0)
            //{
            //    TreeViewNode tvn = null;

            //    foreach (Post post in postsList)
            //    {
            //        post.PromotionPostID
            //        tvn = new TreeViewNode();
            //        tvn.ID = post.PostId.ToString();
            //        tvn.Value = post.PostId.ToString();
            //        tvn.Text = post.PostName;
            //        tvn.ToolTip = post.PostName;
 

            //        if (postsList1.Count == 0)
            //        {
            //            tvn.ShowCheckBox = true;
            //        }


            //        foreach (string strOrgID in strIDS)
            //        {
            //            if (strOrgID == post.PostId.ToString() && tvn.ShowCheckBox)
            //                tvn.Checked = true;
            //        }

            //        if (post.ParentId == 0)
            //        {
            //            tvPost.Nodes.Add(tvn);
            //        }
            //        else
            //        {
            //            try
            //            {
            //                tvPost.FindNodeById(post.ParentId.ToString()).Nodes.Add(tvn);
            //            }
            //            catch
            //            {
            //                tvPost.Nodes.Clear();
            //                SessionSet.PageMessage = "数据错误！";
            //                return;
            //            }
            //        }
            //    }
            //}

            tvPost.DataBind();
            //tvPost.ExpandAll();
        }
    }
}
