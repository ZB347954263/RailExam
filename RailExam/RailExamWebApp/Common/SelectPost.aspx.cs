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
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Common
{
    public partial class SelectPost : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string src = Request.QueryString.Get("src");
                if (!String.IsNullOrEmpty(src))
                {
                    this.hfSource.Value = src;
                }
                BindPostTree();
            }
        }

        private void BindPostTree()
        {
            PostBLL postBLL = new PostBLL();
            IList<RailExam.Model.Post> postList = postBLL.GetPosts();

            Pub.BuildComponentArtTreeView(tvPost, (IList)postList, "PostId",
                "ParentId", "PostName", "PostName", "PostId", null, null, null);

            //tvPost.ExpandAll();
        }
    }
}
