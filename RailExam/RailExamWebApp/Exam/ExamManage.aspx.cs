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

namespace RailExamWebApp.Exam
{
    public partial class ExamManage : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.HasEditRight("考试设计") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }
                BindExamCategoryTree();
            }
        }

        private void BindExamCategoryTree()
        {
            ExamCategoryBLL examCategoryBLL = new ExamCategoryBLL();

            IList<RailExam.Model.ExamCategory> examCategoryList = examCategoryBLL.GetExamCategories();

            Pub.BuildComponentArtTreeView(tvExamCategory, (IList)examCategoryList, "ExamCategoryId",
                "ParentId", "CategoryName", "CategoryName", "IdPath", null, null, null);

            //tvExamCategory.ExpandAll();
            //tvExamCategory.SelectedNode = tvExamCategory.Nodes[0];
        }
    }
}
