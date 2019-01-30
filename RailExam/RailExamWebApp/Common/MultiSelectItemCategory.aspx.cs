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

namespace RailExamWebApp.Common
{
    public partial class MultiSelectItemCategory : PageBase
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
            ItemCategoryBLL itemCategoryBLL = new ItemCategoryBLL();

            IList<RailExam.Model.ItemCategory> itemCategories = itemCategoryBLL.GetItemCategories(0, 0, string.Empty,
                0, 0, string.Empty, 0, string.Empty, 0, 100, "LevelNum, OrderIndex");

            if (itemCategories.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (RailExam.Model.ItemCategory  itemCategory in itemCategories)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = itemCategory.ItemCategoryId.ToString();
                    tvn.Value = itemCategory.ItemCategoryId.ToString();
                    tvn.Text = itemCategory.CategoryName;
                    tvn.ToolTip = itemCategory.CategoryName;
                    tvn.ShowCheckBox = true;

                    if (itemCategory.ParentId == 0)
                    {
                        tvItemCategory.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            tvItemCategory.FindNodeById(itemCategory.ParentId.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvItemCategory.Nodes.Clear();
                            SessionSet.PageMessage = "数据错误！";
                            return;
                        }
                    }
                }
            }

            tvItemCategory.DataBind();
            //tvItemCategory.ExpandAll();
        }
    }
}
