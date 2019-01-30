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
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.UI;
using DSunSoft.Web.Global;

namespace RailExamWebApp.Common
{
    public partial class SelectItem : PageBase
    {
        private ItemBLL _itemBLL = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !ddlViewChangeCallBack.IsCallback
                && !treeNodeSelectedCallBack.IsCallback && !itemsGrid.IsCallback)
            {
                BindKnowledgeTree();
            }
            if (hfIsSearchCommand.Value == "true")
            {
                itemsGrid.CurrentPageIndex = 0;
                itemsGrid.DataBind();
                hfIsSearchCommand.Value = "false";
            }
            hfSaveItemIDs.Value = hfSaveItemIDs.Value;
        }

        #region // Bind TreeView Methods

        private void BindKnowledgeTree()
        {
            #region Bind knowledge tree

            KnowledgeBLL knowledgeBLL = new KnowledgeBLL();

            IList<RailExam.Model.Knowledge> knowledgeList = knowledgeBLL.GetKnowledges();

            if (knowledgeList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (RailExam.Model.Knowledge  knowledge in knowledgeList)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = knowledge.KnowledgeId.ToString();
                    tvn.Value = knowledge.IdPath;
                    tvn.Text = knowledge.KnowledgeName;
                    tvn.ToolTip = knowledge.KnowledgeName;
                    tvn.Attributes.Add("isKnowledge", "true");
                    tvn.ImageUrl = "~/App_Themes/" + StyleSheetTheme + "/Images/TreeView/Knowledge.gif";
                    tvn.ContentCallbackUrl = "../Common/GetKnowledgeBook.aspx?id=" + knowledge.IdPath;

                    if (knowledge.ParentId == 0)
                    {
                        tvView.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            tvView.FindNodeById(knowledge.ParentId.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvView.Nodes.Clear();
                            SessionSet.PageMessage = "数据错误！";

                            return;
                        }
                    }
                }
            }
            //tvView.DataBind();
            //tvView.ExpandAll();

            #endregion
        }

        private void BindTrainTypeTree()
        {
            TrainTypeBLL trainTypeBLL = new TrainTypeBLL();

            IList<TrainType> trainTypeList = trainTypeBLL.GetTrainTypes();
            if (trainTypeList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (TrainType trainType in trainTypeList)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = trainType.TrainTypeID.ToString();
                    tvn.Value = trainType.IDPath;
                    tvn.Text = trainType.TypeName;
                    tvn.ToolTip = trainType.TypeName;
                    tvn.Attributes.Add("isTrainType", "true");
                    tvn.ImageUrl = "~/App_Themes/" + StyleSheetTheme + "/Images/TreeView/Knowledge.gif";
                    tvn.ContentCallbackUrl = "../Common/GetTrainTypeBook.aspx?id=" + trainType.IDPath;

                    if (trainType.ParentID == 0)
                    {
                        tvView.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            tvView.FindNodeById(trainType.ParentID.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvView.Nodes.Clear();

                            SessionSet.PageMessage = "数据错误！";
                            return;
                        }
                    }
                }
            }
            //tvView.DataBind();
            //tvView.ExpandAll();
        }

        private void BindCategoryTree()
        {
            #region Bind category tree

            ItemCategoryBLL itemCategoryBLL = new ItemCategoryBLL();
            IList<RailExam.Model.ItemCategory> itemCategories = itemCategoryBLL.GetItemCategories();
            if (itemCategories.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (RailExam.Model.ItemCategory  itemCategory in itemCategories)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = itemCategory.ItemCategoryId.ToString();
                    tvn.Value = itemCategory.IdPath;
                    tvn.Text = itemCategory.CategoryName;
                    tvn.ToolTip = itemCategory.CategoryName;
                    tvn.Attributes.Add("isCategory", "true");
                    tvn.ImageUrl = "~/App_Themes/" + StyleSheetTheme + "/Images/TreeView/Knowledge.gif";

                    if (itemCategory.ParentId == 0)
                    {
                        tvView.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            tvView.FindNodeById(itemCategory.ParentId.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvView.Nodes.Clear();

                            SessionSet.PageMessage = "数据错误！";
                            return;
                        }
                    }
                }
            }

            //tvView.ExpandAll();
            //tvView.DataBind();

            #endregion
        }

        #endregion

        #region // CallBack Methods

        protected void treeNodeSelectedCallBack_Callback(object sender, CallBackEventArgs e)
        {
            itemsGrid.CurrentPageIndex = 0;
            itemsGrid.DataBind();
            itemsGrid.RenderControl(e.Output);
        }

        protected void ddlViewChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {
            switch (e.Parameter)
            {
                case "VIEW_KNOWLEDGE":
                    {
                        tvView.Nodes.Clear();
                        BindKnowledgeTree();
                        break;
                    }
                case "VIEW_TRAINTYPE":
                    {
                        tvView.Nodes.Clear();
                        BindTrainTypeTree();
                        break;
                    }
                case "VIEW_CATEGORY":
                    {
                        tvView.Nodes.Clear();
                        BindCategoryTree();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            tvView.RenderControl(e.Output);
        }

        [ComponentArtCallbackMethod]
        public static int DeleteItem(string itemIds)
        {
            int nAffectedRecord = 0;

            if (!string.IsNullOrEmpty(itemIds))
            {
                string[] strIds = itemIds.Split('|');
                int[] nIds = new int[strIds.Length];

                for (int i = 0; i < nIds.Length; i++)
                {
                    nIds[i] = int.Parse(strIds[i]);
                }

                ItemBLL itemBLL = new ItemBLL();

                nAffectedRecord = itemBLL.DeleteItem(nIds);
            }

            return nAffectedRecord;
        }

        #endregion

        #region // DataBind Control EventHandlers

        protected void odsItems_ObjectCreated(object sender, ObjectDataSourceEventArgs e)
        {
            _itemBLL = (ItemBLL)e.ObjectInstance;
        }

        protected void itemsGrid_DataBinding(object sender, EventArgs e)
        {
            if (_itemBLL != null)
            {
                itemsGrid.RecordCount = _itemBLL.GetCount();
            }
        }

        protected void itemsGrid_PageIndexChanged(object sender, GridPageIndexChangedEventArgs e)
        {
            itemsGrid.CurrentPageIndex = e.NewIndex;
            itemsGrid.DataBind();
        }

        protected void itemsGrid_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            itemsGrid.Sort = e.SortExpression;
            itemsGrid.CurrentPageIndex = 0;
            itemsGrid.DataBind();
        }

        protected void itemsGrid_DeleteCommand(object sender, GridItemEventArgs e)
        {
            ItemBLL itemBLL = new ItemBLL();

            itemBLL.DeleteItem(int.Parse((((object[])(e.Item.DataItem))[0]).ToString()));
            itemsGrid.DataBind();
        }

        #endregion
    }
}
