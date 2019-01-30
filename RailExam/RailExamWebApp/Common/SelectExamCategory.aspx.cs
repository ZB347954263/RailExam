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
    public partial class SelectExamCategory : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindExamCategoryTree();
            }
        }

        private void BindExamCategoryTree()
        {
            ExamCategoryBLL examCategoryBLL = new ExamCategoryBLL();

            //IList<RailExam.Model.ExamCategory> examCategoryList = examCategoryBLL.GetExamCategories(
            //                                    0, 0, "", 0, 0, "", "", "", false, 0, 200, "ExamCategoryId ASC");

            IList<RailExam.Model.ExamCategory> examCategoryList = examCategoryBLL.GetExamCategories();

            Pub.BuildComponentArtTreeView(tvExamCategory, (IList)examCategoryList, "ExamCategoryId",
                "ParentId", "CategoryName", "CategoryName", "ExamCategoryId", null, null, null);

            //tvExamCategory.ExpandAll();
        }
    }
}
