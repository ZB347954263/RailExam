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

namespace RailExamWebApp.Online.Study
{
    public partial class ReadCourse : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTree();
                BindTrainTypeTree();
            }
        }

        private void BindTree()
        {
            CoursewareTypeBLL coursewareTypeBLL = new CoursewareTypeBLL();

            IList<CoursewareType> coursewareTypeList = coursewareTypeBLL.GetCoursewareTypes();

            Pub.BuildComponentArtTreeView(tvCourseware, (IList)coursewareTypeList, "CoursewareTypeId", "ParentId", "CoursewareTypeName", "CoursewareTypeName", "IdPath", null, null, null);

            //tvCourseware.ExpandAll();
        }

        private void BindTrainTypeTree()
        {
            TrainTypeBLL trainTypeBLL = new TrainTypeBLL();

            IList<TrainType> trainTypeList = trainTypeBLL.GetTrainTypes();

            Pub.BuildComponentArtTreeView(tvTrainType, (IList)trainTypeList, "TrainTypeID", "ParentID", "TypeName", "TypeName", "IDPath", null, null, null);

            //tvTrainType.ExpandAll();
        }
    }
}
