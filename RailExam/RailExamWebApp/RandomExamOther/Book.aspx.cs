using System;
using System.Collections;
using System.Collections.Generic;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamOther
{
	public partial class Book : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				if (PrjPub.CurrentLoginUser == null)
				{
					Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
					return;
				}
				BindKnowledgeTree();
				BindTrainTypeTree();
				BindPost();
				if (PrjPub.HasEditRight("教材管理") && PrjPub.IsServerCenter) //&& PrjPub.CurrentLoginUser.SuitRange == 1
				{
					HfUpdateRight.Value = "True";
				}
				else
				{
					HfUpdateRight.Value = "False";
				}
			}

			string strRefresh = Request.Form.Get("Refresh");
			if (!string.IsNullOrEmpty(strRefresh))
			{
				BindKnowledgeTree();
				BindTrainTypeTree();
				BindPost();
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
			TrainTypeBLL trainTypeBLL = new TrainTypeBLL();

			IList<TrainType> trainTypes = trainTypeBLL.GetTrainTypes();

			Pub.BuildComponentArtTreeView(tvTrainType, (IList)trainTypes, "TrainTypeID", "ParentID", "TypeName", "TypeName",
										  "IDPath", null, null, null);
		}

		private void BindPost()
		{
			PostBLL PostBLL = new PostBLL();
			IList<Post> posts = PostBLL.GetPosts();

			Pub.BuildComponentArtTreeView(tvPost, (IList)posts, "PostId",
				"ParentId", "PostName", "PostName", "PostId", null, null, null);
		}
	}
}
