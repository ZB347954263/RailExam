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
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;
using ComponentArt.Web.UI;


namespace RailExamWebApp.Online.Study
{
    public partial class StudyByKnowledge : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (PrjPub.StudentID == null)
            {
                Response.Write("<script>alert('已超时，请注销后重新登录！');window.close();</script>");
                return;
            } 
            
            if (!IsPostBack)
            {
                BindKnowledgeTree();
                BindCourseware();
            }
        }

        private void BindKnowledgeTree()
        {
            KnowledgeBLL knowledgeBLL = new KnowledgeBLL();

            IList<RailExam.Model.Knowledge> knowledgeList = knowledgeBLL.GetKnowledges();

            Pub.BuildComponentArtTreeView(tvKnowledge, (IList)knowledgeList, "KnowledgeId",
                "ParentId", "KnowledgeName", "KnowledgeName", "IdPath", null, null, null);

        }

        private void BindCourseware()
        {
            CoursewareTypeBLL coursewareTypeBLL = new CoursewareTypeBLL();

            IList<CoursewareType> coursewareTypeList = coursewareTypeBLL.GetCoursewareTypes(0, 0, "", 0, 0, "", "", "", 0, 4000, "LevelNum, CoursewareTypeId ASC");

            Pub.BuildComponentArtTreeView(tvCourseware, (IList)coursewareTypeList,
                "CoursewareTypeId", "ParentId", "CoursewareTypeName", "CoursewareTypeName", "IdPath", null, null, null);

        }
    }
}
