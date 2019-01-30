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
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Item
{
    public partial class ItemCategory : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.HasEditRight("辅助分类") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }
                if (PrjPub.HasDeleteRight("辅助分类") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }
                BindItemCategoryTree();
            }
        }

        private void BindItemCategoryTree()
        {
            ItemCategoryBLL itemCategoryBLL = new ItemCategoryBLL();
            IList<RailExam.Model.ItemCategory> itemCategorys = itemCategoryBLL.GetItemCategories(0, 0, string.Empty,
                                                                                  0, 0, string.Empty, 0, string.Empty, 0,
                                                                                  100, "LevelNum, OrderIndex");

            Pub.BuildComponentArtTreeView(tvItemCategory, (IList)itemCategorys, "ItemCategoryId",
                                          "ParentId", "CategoryName", "CategoryName", "ItemCategoryId", null, null, null);
            //tvItemCategory.ExpandAll();
        }

        #region // ComponentArt CallBack Methods

        protected void tvItemCategoryChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {
            tvItemCategory.Nodes.Clear();
            BindItemCategoryTree();

            tvItemCategory.RenderControl(e.Output);
        }

        [ComponentArtCallbackMethod]
        public bool tvItemCategoryNodeCanMove(int itemCategoryId, string direction)
        {
            ItemCategoryBLL itemCategoryBLL = new ItemCategoryBLL();

            if (direction.ToUpper() == "UP")
            {
                return itemCategoryBLL.MoveUp(itemCategoryId);
            }
            else if (direction.ToUpper() == "DOWN")
            {
                return itemCategoryBLL.MoveDown(itemCategoryId);
            }
            else
            {
                SessionSet.PageMessage = "未知移动方向！";
            }

            return false;
        }

        #endregion
    }
}