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
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Common
{
    public partial class SelectSecondPost : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PostBLL postBLL = new PostBLL();
                IList<RailExam.Model.Post> postList = postBLL.GetPostsByWhereClause("id_path ||'/' not like '/1548/%' and post_level<3 ");

                Pub.BuildComponentArtTreeView(tvPost, (IList)postList, "PostId",
                    "ParentId", "PostName", "PostName", "PostId", null, null, null);
            }

        }
    }
}
