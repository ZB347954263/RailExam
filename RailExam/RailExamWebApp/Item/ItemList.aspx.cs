using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

using System.IO;

namespace RailExamWebApp.Item
{
    public partial class ItemList : PageBase
    {
        private ItemBLL _itemBLL = null;
        private int _page = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }
				if (PrjPub.HasEditRight("试题管理") && PrjPub.IsServerCenter)// && PrjPub.CurrentLoginUser.SuitRange == 1
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }
				if (PrjPub.HasDeleteRight("试题管理") && PrjPub.IsServerCenter)//&& PrjPub.CurrentLoginUser.SuitRange == 1
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }
                HfOrgId.Value = PrjPub.CurrentLoginUser.StationOrgID.ToString();

				if(PrjPub.IsWuhanOnly() && PrjPub.IsServerCenter)
				{
					btnAddBookItem.Visible = true;
				}
				else
				{
					btnAddBookItem.Visible = false;
				}

                hfRailSystemId.Value = PrjPub.GetRailSystemId().ToString();
            }


            if (!IsPostBack && !ddlViewChangeCallBack.IsCallback
                && !treeNodeSelectedCallBack.IsCallback && !itemsGrid.IsCallback)
            {
                BindKnowledgeTree("");
            }
            if (hfIsSearchCommand.Value == "true")
            {
                itemsGrid.CurrentPageIndex = 0;
                itemsGrid.DataBind();
                hfIsSearchCommand.Value = "false";
            }


			if (hfRefresh.Value != "")
			{
				if(PrjPub.IsWuhan())
				{
					DownloadWord(hfRefresh.Value);
				}
				else
				{
					DownloadExcel(hfRefresh.Value);
				}
			}

            string strRefresh = Request.Form.Get("refresh");
            if(!string.IsNullOrEmpty(strRefresh))
            {
                itemsGrid.DataBind();
            }

            if (!string.IsNullOrEmpty(hfPostID.Value))
            {
                PostBLL post = new PostBLL();
                txtPost.Text = post.GetPost(Convert.ToInt32(hfPostID.Value)).PostName;
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            tvView.Nodes.Clear();
            BindKnowledgeTree("");
            itemsGrid.DataBind();
        }

		private void DownloadWord(string strName)
		{
			string filename = Server.MapPath("/RailExamBao/Excel/《"+strName+"》试题.doc");
			if (File.Exists(filename))
			{
				FileInfo file = new FileInfo(filename.ToString());
				this.Response.Clear();
				this.Response.Buffer = true;
				this.Response.Charset = "utf-7";
				this.Response.ContentEncoding = Encoding.UTF7;
				// 添加头信息，为"文件下载/另存为"对话框指定默认文件名
				this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode("《" + strName + "》试题") + ".doc");
				// 添加头信息，指定文件大小，让浏览器能够显示下载进度


				this.Response.AddHeader("Content-Length", file.Length.ToString());
				// 指定返回的是一个不能被客户端读取的流，必须被下载


				this.Response.ContentType = "application/ms-word";
				// 把文件流发送到客户端


				this.Response.WriteFile(file.FullName);
			}
		}

		private void DownloadExcel(string strName)
		{
            string filename = Server.MapPath("/RailExamBao/Excel/《" + Server.UrlDecode(strName) + "》试题.xls");

			if (File.Exists(filename))
			{
				FileInfo file = new FileInfo(filename);
				this.Response.Clear();
				this.Response.Buffer = true;
			    this.Response.Charset = "GB2312";// "utf-7";
				this.Response.ContentEncoding = Encoding.UTF7;
				// 添加头信息，为"文件下载/另存为"对话框指定默认文件名
				this.Response.AddHeader("Content-Disposition",
                                        "attachment; filename=" + HttpUtility.UrlEncode("《" + strName + "》试题") + ".xls");
				// 添加头信息，指定文件大小，让浏览器能够显示下载进度


				this.Response.AddHeader("Content-Length", file.Length.ToString());

				// 指定返回的是一个不能被客户端读取的流，必须被下载


				this.Response.ContentType = "application/ms-excel";

				// 把文件流发送到客户端


				this.Response.WriteFile(file.FullName);



			}
		}

        #region // Bind TreeView Methods

        private void BindKnowledgeTree(string strAll)
        {
            #region Bind knowledge tree

            KnowledgeBLL knowledgeBLL = new KnowledgeBLL();

            IList<RailExam.Model.Knowledge> knowledgeList = new List<RailExam.Model.Knowledge>();

			if(strAll=="ALL")
			{
				knowledgeList = knowledgeBLL.GetKnowledges();
			}
			else
			{
				if (PrjPub.CurrentLoginUser.SuitRange == 1)
				{
					knowledgeList = knowledgeBLL.GetKnowledges();
				}
				else
				{
					knowledgeList = knowledgeBLL.GetKnowledgesByOrgID(PrjPub.CurrentLoginUser.StationOrgID);
				}
			}

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
                    tvn.ContentCallbackUrl = "../Common/GetKnowledgeBook.aspx?item=no&source=itemlist&id=" + knowledge.IdPath+"&postId="+hfPostID.Value;

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
					tvn.ContentCallbackUrl = "../Common/GetTrainTypeBook.aspx?item=no&source=itemlist&id=" + trainType.IDPath;

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
            IList<RailExam.Model.ItemCategory> itemCategories = itemCategoryBLL.GetItemCategories(0, 0, string.Empty,
                                                                                                  0, 0, string.Empty, 0,
                                                                                                  string.Empty, 0,
                                                                                                  100,
                                                                                                  "LevelNum, OrderIndex");

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

            //tvView.ExpandAll();
            //tvView.DataBind();

            #endregion
        }

        #endregion

        #region // CallBack Methods

        protected void treeNodeSelectedCallBack_Callback(object sender, CallBackEventArgs e)
        {
           // itemsGrid.CurrentPageIndex = 0;
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
                        BindKnowledgeTree("");
                        break;
                    }
				case "VIEW_KNOWLEDGE_ALL":
					{
						tvView.Nodes.Clear();
						BindKnowledgeTree("ALL");
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
            _page = e.NewIndex;
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
            itemBLL.DeleteItem(int.Parse(e.Item["ItemId"].ToString()));
			itemsGrid.DataBind();
        }

        #endregion


        private string intToString(int intCol)
        {
            if (intCol < 27)
            {
                return Convert.ToChar(intCol + 64).ToString();
            }
            else
            {
                return Convert.ToChar((intCol - 1) / 26 + 64).ToString() + Convert.ToChar((intCol - 1) % 26 + 64 + 1).ToString();
            }
        }
    }
}