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
    public partial class SelectPaperCategory : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPaperCategoryTree();
            }
        }

        private void BindPaperCategoryTree()
        {
            PaperCategoryBLL paperCategoryBLL = new PaperCategoryBLL();

            //IList<RailExam.Model.PaperCategory> paperCategoryList = paperCategoryBLL.GetPaperCategories(
            //                                    0, 0, "", 0, 0, "", "", "", false, 0, 200, "PaperCategoryId ASC");

            IList<RailExam.Model.PaperCategory> paperCategoryList = paperCategoryBLL.GetPaperCategories();

            Pub.BuildComponentArtTreeView(tvPaperCategory, (IList)paperCategoryList, "PaperCategoryId",
                "ParentId", "CategoryName", "CategoryName", "PaperCategoryId", null, null, null);

            //tvPaperCategory.ExpandAll();
        }
    }
}
