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
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
    public partial class Post : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
				if (PrjPub.CurrentLoginUser == null)
				{
					Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
					return;
				}

                if (PrjPub.HasEditRight("工作岗位") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }
                if (PrjPub.HasDeleteRight("工作岗位") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                } 

                BindPostTree();
            }
        }

        private void BindPostTree()
        {
            PostBLL PostBLL = new PostBLL();
            IList<RailExam.Model.Post> posts = PostBLL.GetPosts();

            Pub.BuildComponentArtTreeView(tvPost, (IList)posts, "PostId", "ParentId", "PostName",
                "PostName", "PostId", null, null, null);
        }

        #region // ComponentArt CallBack Methods

        [ComponentArtCallbackMethod]
        public bool tvPostNodeMove(int postId, string direction)
        {
            PostBLL postBLL = new PostBLL();

            if (direction.ToUpper() == "UP")
            {
                return postBLL.MoveUp(postId);
            }
            else if (direction.ToUpper() == "DOWN")
            {
                return postBLL.MoveDown(postId);
            }
            else
            {
                SessionSet.PageMessage = "未知移动方向！";
            }

            return false;
        }

        protected void tvPostChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {

			PostBLL objPostBll = new PostBLL();
			if (e.Parameters[0] == "Insert")
			{
				IList<RailExam.Model.Post> objList = objPostBll.GetPostsByWhereClause("Post_ID = (select Max(Post_ID) from Post)");
				hfMaxID.Value = objList[0].PostId.ToString();
				hfMaxID.RenderControl(e.Output);
			}
			else if (e.Parameters[0] == "Delete")
			{
				hfMaxID.Value = e.Parameters[1];
				hfMaxID.RenderControl(e.Output);
			}

			tvPost.Nodes.Clear();
			BindPostTree();

			tvPost.RenderControl(e.Output);
        }

        #endregion
    }
}