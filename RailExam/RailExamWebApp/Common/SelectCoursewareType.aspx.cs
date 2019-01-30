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
    public partial class SelectCoursewareType : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTree();
            }
        }

        private void BindTree()
        {
            CoursewareTypeBLL coursewareTypeBLL = new CoursewareTypeBLL();

            IList<CoursewareType> coursewareTypeList = coursewareTypeBLL.GetCoursewareTypes(0, 0, "", 0, 0, "", "", "",
                                                                            0, 4000, "LevelNum, CoursewareTypeId ASC");

            Pub.BuildComponentArtTreeView(tvCoursewareType, (IList)coursewareTypeList,
                "CoursewareTypeId", "ParentId", "CoursewareTypeName", "CoursewareTypeName", "CoursewareTypeId", null, null, null);

            //tvCoursewareType.ExpandAll();
        }
    }
}
