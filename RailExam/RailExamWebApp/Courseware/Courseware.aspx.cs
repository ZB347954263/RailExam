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
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Courseware
{
    public partial class Courseware : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             if(!IsPostBack)
             {
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
    }
}
