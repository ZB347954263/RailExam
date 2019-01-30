using System;
using System.Collections;
using System.Collections.Generic;
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamOther
{
	public partial class Courseware : PageBase
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

				if (PrjPub.HasEditRight("课件管理") && PrjPub.IsServerCenter)
				{
					HfUpdateRight.Value = "True";
				}
				else
				{
					HfUpdateRight.Value = "False";
				}
				if (PrjPub.HasDeleteRight("课件管理") && PrjPub.IsServerCenter)
				{
					HfDeleteRight.Value = "True";
				}
				else
				{
					HfDeleteRight.Value = "False";
				}
			}

			BindCoursewareTypeTree();
			BindTrainTypeTree();
			BindPost();
		}

		private void BindCoursewareTypeTree()
		{
			CoursewareTypeBLL coursewareTypeBLL = new CoursewareTypeBLL();

			IList<RailExam.Model.CoursewareType> coursewareTypeList = coursewareTypeBLL.GetCoursewareTypes(0, 0, "", 0, 0, "", "", "", 0, 4000, "LevelNum, CoursewareTypeId ASC");

			Pub.BuildComponentArtTreeView(tvCourseware, (IList)coursewareTypeList,
				"CoursewareTypeId", "ParentId", "CoursewareTypeName", "CoursewareTypeName", "IdPath", null, null, null);

			//tvCourseware.ExpandAll();
		}

		private void BindTrainTypeTree()
		{
			TrainTypeBLL trainTypeBLL = new TrainTypeBLL();

			IList<TrainType> trainTypeList = trainTypeBLL.GetTrainTypeInfo(0, 0, 0, "", 0, "", "", false, false, "", 0, 4000, "LevelNum, OrderIndex ASC");

			Pub.BuildComponentArtTreeView(tvTrainType, (IList)trainTypeList,
				"TrainTypeID", "ParentID", "TypeName", "TypeName", "IDPath", null, null, null);

			//tvTrainType.ExpandAll();
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
