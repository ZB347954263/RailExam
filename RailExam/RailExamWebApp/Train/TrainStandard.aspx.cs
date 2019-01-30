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
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Train
{
    public partial class TrainStandard : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindtvPost();
            }
        }

        private void BindtvPost()
        {
            PostBLL PostBLL = new PostBLL();
            IList<RailExam.Model.Post> posts = PostBLL.GetPosts(0, 0, 0, "", 0, "", "", 0, 0, "", String.Empty, 0, 200, "PostLevel, OrderIndex ASC");

            Pub.BuildComponentArtTreeView(tvPost, (IList)posts, "PostId", "ParentId", "PostName",
                "PostName", "PostId", null, null, null);
        }
    }
}