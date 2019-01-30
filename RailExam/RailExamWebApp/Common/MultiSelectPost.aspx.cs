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

namespace RailExamWebApp.Common
{
    public partial class MultiSelectPost : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPostTree();
                string ids = Request.QueryString["ids"].ToString();
                SetTreeNodeSelected(ids, this.tvPost.Nodes);
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
            string strID = Request.QueryString["id"];
            string[] strIDS = { };
            if (!string.IsNullOrEmpty(strID))
            {
                strIDS = strID.Split(',');
            }

            RailExam.BLL.PostBLL PostBLL = new RailExam.BLL.PostBLL();
            IList<Post> postsList = PostBLL.GetPosts();

            if (postsList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (Post post in postsList)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = post.PostId.ToString();
                    tvn.Value = post.PostId.ToString();
                    tvn.Text = post.PostName;
                    tvn.ToolTip = post.PostName;

                    IList<Post> postsList1 = PostBLL.GetPostsByParentID(post.PostId);

                    if (postsList1.Count == 0)
                    {
                        tvn.ShowCheckBox = true;
                    }


                    foreach (string strOrgID in strIDS)
                    {
                        if (strOrgID == post.PostId.ToString() && tvn.ShowCheckBox)
                            tvn.Checked = true;
                    }

                    if (post.ParentId == 0)
                    {
                        tvPost.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            tvPost.FindNodeById(post.ParentId.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvPost.Nodes.Clear();
                            SessionSet.PageMessage = "数据错误！";
                            return;
                        }
                    }
                }
            }

            tvPost.DataBind();
            //tvPost.ExpandAll();
        }
    }
}
