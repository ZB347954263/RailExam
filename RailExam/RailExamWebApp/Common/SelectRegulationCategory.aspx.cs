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
    public partial class SelectRegulationCategory : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRegulationCategoryTree();
            }
        }

        private void BindRegulationCategoryTree()
        {
            RegulationCategoryBLL regulationCategoryBll = new RegulationCategoryBLL();

            IList<RegulationCategory> regulationCategoryList = regulationCategoryBll.GetRegulationCategories(
                                                0, 0, "", 0, 0, "", "", "", 0, 200, "RegulationCategoryID ASC");

            Pub.BuildComponentArtTreeView(tvRegulationCategory, (IList)regulationCategoryList, "RegulationCategoryID",
                "ParentID", "CategoryName", "CategoryName", "RegulationCategoryID", null, null, null);

            //tvRegulationCategory.ExpandAll();
        }
    }
}
