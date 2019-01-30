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
    public partial class SelectItemCategory : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindItemCategoryTree();
            }
        }

        private void BindItemCategoryTree()
        {
            ItemCategoryBLL ItemCategoryBLL = new ItemCategoryBLL();

            IList<RailExam.Model.ItemCategory> ItemCategories = ItemCategoryBLL.GetItemCategories(0, 0, string.Empty,
                0, 0, string.Empty, 0, string.Empty, 0, 100, "LevelNum, OrderIndex");

            Pub.BuildComponentArtTreeView(tvItemCategory, (IList)ItemCategories, "ItemCategoryId",
                                          "ParentId", "CategoryName", "CategoryName", "ItemCategoryId", null, null, null);

            //tvItemCategory.ExpandAll();
        }
    }
}
