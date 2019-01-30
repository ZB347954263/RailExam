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
    public partial class SelectItems : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindKnowledgeTree();
                ViewState["ChooseId"] = "";
                string strPaperid = Request.QueryString.Get("paperId");
                string strItemTypeid = Request.QueryString.Get("ItemType");
                if (strPaperid != null & strPaperid != string.Empty)
                {
                    ViewState["PaperId"] = strPaperid;
                }

                if (strItemTypeid != null & strItemTypeid != string.Empty)
                {
                    ViewState["ItemType"] = strItemTypeid;
                }
                ViewState["StartRow"] = 0;
                ViewState["EndRow"] = Grid1.PageSize;
                hftype.Value = "1";
                BindGrid();
            }
        }

        private void BindGrid()
        {

            int itemTypeId = int.Parse(ViewState["ItemType"].ToString());
            int paperId = int.Parse(ViewState["PaperId"].ToString());
            int startRow = int.Parse(ViewState["StartRow"].ToString());
            int endRow = int.Parse(ViewState["EndRow"].ToString());
            int nItemcount = 0;
            int itemDifficultyId = -1;
            int itemScore = -1;
            int usageId = -1;

            if (divquery.Visible)
            {

                if (this.txtScore.Text != "")
                {
                    itemScore = int.Parse(txtScore.Text);
                }

                if (ddlItemDifficulty.SelectedValue != "")
                {
                    itemDifficultyId = int.Parse(ddlItemDifficulty.SelectedValue);
                }

                usageId = int.Parse(ddlUsage.SelectedValue);
            }


            IList<RailExam.Model.Item> Items = null;
            ItemBLL itemBll = new ItemBLL();

            string knowledgeIdPath = "null";
            int bookId = -1;
            int chapterId = -1;
            string trainTypeIdPath = "null";
            string categoryIdPath = "null";

            if (hftype.Value == "1")
            {
                knowledgeIdPath = tvView.SelectedNode.Value;
            }
            if (hftype.Value == "4")
            {
                trainTypeIdPath = tvView.SelectedNode.Value;
            }
            if (hftype.Value == "5")
            {
                categoryIdPath = tvView.SelectedNode.Value;
            }
            if (hftype.Value == "2")
            {
                bookId = int.Parse(tvView.SelectedNode.Value);
            }
            if (hftype.Value == "3")
            {
                chapterId = int.Parse(tvView.SelectedNode.Value);
            }

            Items = itemBll.GetItems(knowledgeIdPath, bookId, chapterId, trainTypeIdPath, categoryIdPath,
   itemTypeId, paperId, itemDifficultyId, itemScore,
   1, usageId, startRow, endRow, ref nItemcount);

            if (Items.Count > 0)
            {
                Grid1.VirtualItemCount = nItemcount;
                Grid1.DataSource = Items;
                Grid1.DataBind();

            }
            else
            {
                BindEmptyGrid();
            }
        }

        private void BindEmptyGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ItemId", typeof(int)));
            dt.Columns.Add(new DataColumn("OrganizationName", typeof(string)));
            dt.Columns.Add(new DataColumn("TypeName", typeof(string)));
            dt.Columns.Add(new DataColumn("DifficultyName", typeof(string)));
            dt.Columns.Add(new DataColumn("Content", typeof(string)));
            dt.Columns.Add(new DataColumn("StatusName", typeof(string)));
            dt.Columns.Add(new DataColumn("Score", typeof(decimal)));

            DataRow dr = dt.NewRow();

            dr["ItemId"] = 0;
            dr["OrganizationName"] = "";
            dr["TypeName"] = "";
            dr["DifficultyName"] = "";
            dr["Content"] = "";
            dr["StatusName"] = "";
            dr["Score"] = 0;

            dt.Rows.Add(dr);

            Grid1.VirtualItemCount = 1;
            Grid1.DataSource = dt;
            Grid1.DataBind();

            CheckBox CheckBox1 = (CheckBox)this.Grid1.Items[0].FindControl("chkSelect");
            Label LabelScore = (Label)this.Grid1.Items[0].FindControl("LabelScore");
            CheckBox1.Visible = false;
            LabelScore.Visible = false;

        }

        protected void Grid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            string strAllId = ViewState["ChooseId"].ToString();

            for (int i = 0; i < this.Grid1.Items.Count; i++)
            {
                CheckBox CheckBox1 = (CheckBox)this.Grid1.Items[i].FindControl("chkSelect");

                string strEmId = ((Label)this.Grid1.Items[i].FindControl("LabelItemID")).Text;
                if (CheckBox1.Checked)
                {
                    string strOldAllId = "," + strAllId + ",";
                    if (strOldAllId.IndexOf("," + strEmId + ",") == -1)
                    {
                        if (strAllId.Length == 0)
                        {
                            strAllId += strEmId;
                        }
                        else
                        {
                            strAllId += "," + strEmId;
                        }
                    }
                }
            }

            ViewState["ChooseId"] = strAllId;

            this.Grid1.CurrentPageIndex = e.NewPageIndex;
            ViewState["StartRow"] = Grid1.PageSize * e.NewPageIndex;
            ViewState["EndRow"] = Grid1.PageSize * (e.NewPageIndex + 1);

            BindGrid();
        }

        protected void tvView_NodeSelected(object sender, ComponentArt.Web.UI.TreeViewNodeEventArgs e)
        {
            ViewState["StartRow"] = 0;
            ViewState["EndRow"] = Grid1.PageSize;
            this.Grid1.CurrentPageIndex = 0;

            BindGrid();
        }

        protected void ButtonSave_Click(object sender, ImageClickEventArgs e)
        {
            string strAllId = ViewState["ChooseId"].ToString();

            for (int i = 0; i < this.Grid1.Items.Count; i++)
            {
                CheckBox CheckBox1 = (CheckBox)this.Grid1.Items[i].FindControl("chkSelect");

                string strEmId = ((Label)this.Grid1.Items[i].FindControl("LabelItemID")).Text;
                if (CheckBox1.Checked)
                {
                    string strOldAllId = "," + strAllId + ",";
                    if (strOldAllId.IndexOf("," + strEmId + ",") == -1)
                    {
                        if (strAllId.Length == 0)
                        {
                            strAllId += strEmId;
                        }
                        else
                        {
                            strAllId += "," + strEmId;
                        }
                    }
                }
            }

            if (strAllId == "")
            {
                SessionSet.PageMessage = "请选择试题！";
                return;

            }
            Response.Write("<script>top.returnValue='" + strAllId + "';top.window.close();</script>");

        }

        protected void btnQuery_Click(object sender, ImageClickEventArgs e)
        {
            ViewState["StartRow"] = 0;
            ViewState["EndRow"] = Grid1.PageSize;
            this.Grid1.CurrentPageIndex = 0;
            BindGrid();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            divquery.Visible = !divquery.Visible;
        }

        protected void ddlTreeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlTreeType.SelectedValue)
            {
                case "1":
                    {
                        tvView.Nodes.Clear();
                        BindKnowledgeTree();
                        break;
                    }
                case "2":
                    {
                        tvView.Nodes.Clear();
                        BindTrainTypeTree();
                        break;
                    }
                case "3":
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

        }

        #region Bind treeView
        private void BindKnowledgeTree()
        {
            #region Bind knowledge tree

            KnowledgeBLL knowledgeBLL = new KnowledgeBLL();

            IList<RailExam.Model.Knowledge> knowledgeList = knowledgeBLL.GetKnowledges();

            if (knowledgeList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (RailExam.Model.Knowledge knowledge in knowledgeList)
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

            tvView.SelectedNode = tvView.Nodes[0];

            if (tvView.Nodes.Count > 0)
            {
                tvView.Nodes[0].Expanded = true;
            }

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

            tvView.SelectedNode = tvView.Nodes[0];

            if (tvView.Nodes.Count > 0)
            {
                tvView.Nodes[0].Expanded = true;
            }
        }

        private void BindCategoryTree()
        {
            #region Bind category tree

            ItemCategoryBLL itemCategoryBLL = new ItemCategoryBLL();
            IList<RailExam.Model.ItemCategory> itemCategories = itemCategoryBLL.GetItemCategories();
            if (itemCategories.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (RailExam.Model.ItemCategory itemCategory in itemCategories)
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



            tvView.SelectedNode = tvView.Nodes[0];

            if (tvView.Nodes.Count > 0)
            {
                tvView.Nodes[0].Expanded = true;
            }

            #endregion
        }
        #endregion
    }
}
