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

namespace RailExamWebApp.Train
{
    public partial class TrainAim : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTrainTypeTree();
				BindTrainPostTree();
            }
        }

        private void BindTrainTypeTree()
        {
            TrainTypeBLL trainTypeBLL = new TrainTypeBLL();

            IList<TrainType> trainTypes = trainTypeBLL.GetTrainTypes();

            Pub.BuildComponentArtTreeView(tvTrainType, (IList)trainTypes, "TrainTypeID", "ParentID", "TypeName", "TypeName", "TrainTypeID", null, null, null);

            //tvTrainType.ExpandAll();
        }
		private void BindTrainPostTree()
		{
			PostBLL PostBLL = new PostBLL();
			IList<Post> posts = PostBLL.GetPosts();

			Pub.BuildComponentArtTreeView(tvTrainPost, (IList)posts, "PostId",
				"ParentId", "PostName", "PostName", "PostId", null, null, null);
		}
    }
}