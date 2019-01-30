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
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;

namespace RailExamWebApp.Common
{
    public partial class SelectStrategyChapter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
				if (Request.QueryString.Get("itemTypeID") != null)
				{
					ViewState["ItemType"] = Request.QueryString.Get("itemTypeID");
					hfItemType.Value = ViewState["ItemType"].ToString();
				}
				else
				{
					ViewState["ItemType"] = "1";
					hfItemType.Value = "1";
				}
				BindKnowledgeTree();
            }
        }

        private void BindKnowledgeTree()
        {
            KnowledgeBLL knowledgeBLL = new KnowledgeBLL();
            IList<RailExam.Model.Knowledge> knowledgeList = knowledgeBLL.GetKnowledges();

            if (knowledgeList.Count > 0)
            {
                TreeViewNode tvn = null;
                string strflag = Request.QueryString.Get("flag");

                foreach (RailExam.Model.Knowledge knowledge in knowledgeList)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = knowledge.KnowledgeId.ToString();
                    tvn.Value = knowledge.IdPath;
                    tvn.Text = knowledge.KnowledgeName;
                    tvn.ToolTip = knowledge.KnowledgeName;
                    tvn.Attributes.Add("isKnowledge", "true");
                    tvn.ImageUrl = "~/App_Themes/" + StyleSheetTheme + "/Images/TreeView/Knowledge.gif";

                    if (strflag != null )
                    {
                        if(strflag == "2")
                        {
                            string strPostID = Request.QueryString.Get("PostID");
                            string strOrgID = Request.QueryString.Get("OrgID");
                            string strLeader = Request.QueryString.Get("Leader");
                            string strTech = Request.QueryString.Get("Tech");
                            tvn.ContentCallbackUrl = "~/Common/GetKnowledgeBook.aspx?itemTypeID="+ViewState["ItemType"].ToString()+"&flag=2&id=" + knowledge.KnowledgeId + "&OrgID="+strOrgID+"&PostID="+strPostID+"&Leader="+strLeader+"&Tech="+strTech ;
                        }
                        else if(strflag == "3")
                        {
							tvn.ContentCallbackUrl = "~/Common/GetKnowledgeBook.aspx?itemTypeID=" + ViewState["ItemType"].ToString() + "&flag=3&id=" + knowledge.IdPath;
                        }
                        else if (strflag == "4")
                        {
                            string strBookIds = Request.QueryString.Get("bookIds");
                            tvn.ContentCallbackUrl = "~/Common/GetKnowledgeBook.aspx?itemTypeID=" + ViewState["ItemType"].ToString() + "&flag=4&item=no&id=" + knowledge.IdPath + "&bookIds=" + strBookIds;
                        }
                        else
                        {
							tvn.ContentCallbackUrl = "~/Common/GetKnowledgeBook.aspx?itemTypeID=" + ViewState["ItemType"].ToString() + "&flag=1&id=" + knowledge.IdPath;
                        }
                    }
                    else
                    {
                        if(!string.IsNullOrEmpty(Request.QueryString.Get("RandomExamID")))
                        {
                            tvn.ContentCallbackUrl = "~/Common/GetKnowledgeBook.aspx?RandomExamID=" + Request.QueryString.Get("RandomExamID") + "&itemTypeID=" + ViewState["ItemType"].ToString() + "&id=" + knowledge.IdPath;
                        }
                        else
                        {
                            tvn.ContentCallbackUrl = "~/Common/GetKnowledgeBook.aspx?itemTypeID=" + ViewState["ItemType"].ToString() + "&id=" + knowledge.IdPath;
                        }
                    }

                    if (knowledge.ParentId == 0)
                    {
                        tvBookChapter.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            tvBookChapter.FindNodeById(knowledge.ParentId.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvBookChapter.Nodes.Clear();
                            SessionSet.PageMessage = "数据错误！";
                            return;
                        }
                    }
                }
            }
            //tvBookChapter.DataBind();
            //tvBookChapter.ExpandAll();
        }

        private void BindTrainTypeTree()
        {
            TrainTypeBLL trainTypeBLL = new TrainTypeBLL();

            IList<TrainType> trainTypeList = trainTypeBLL.GetTrainTypes();

            if (trainTypeList.Count > 0)
            {
                TreeViewNode tvn = null;
                string strflag = Request.QueryString.Get("flag");

                foreach (TrainType trainType in trainTypeList)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = trainType.TrainTypeID.ToString();
                    tvn.Value = trainType.IDPath;
                    tvn.Text = trainType.TypeName;
                    tvn.ToolTip = trainType.TypeName;
                    tvn.Attributes.Add("isTrainType", "true");
                    tvn.ImageUrl = "~/App_Themes/" + StyleSheetTheme + "/Images/TreeView/Knowledge.gif";

                    if (strflag != null)
                    {
                        if (strflag == "2")
                        {
                            string strPostID = Request.QueryString.Get("PostID");
                            string strOrgID = Request.QueryString.Get("OrgID");
                            string strLeader = Request.QueryString.Get("Leader");
                            string strTech = Request.QueryString.Get("Tech");
							tvn.ContentCallbackUrl = "~/Common/GetTrainTypeBook.aspx?itemTypeID=" + ViewState["ItemType"].ToString() + "&flag=2&id=" + trainType.TrainTypeID + "&OrgID=" + strOrgID + "&PostID=" + strPostID + "&Leader=" + strLeader + "&Tech=" + strTech;
                        }
                        else
                        {
							tvn.ContentCallbackUrl = "~/Common/GetTrainTypeBook.aspx?itemTypeID=" + ViewState["ItemType"].ToString() + "&flag=1&id=" + trainType.IDPath;
                        }
                    }
                    else
                    {
						tvn.ContentCallbackUrl = "~/Common/GetTrainTypeBook.aspx?itemTypeID=" + ViewState["ItemType"].ToString() + "&id=" + trainType.IDPath;
                    }

                    if (trainType.ParentID == 0)
                    {
                        tvBookChapter.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            tvBookChapter.FindNodeById(trainType.ParentID.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvBookChapter.Nodes.Clear();
                            SessionSet.PageMessage = "数据错误！";
                            return;
                        }
                    }
                }
            }
            //tvBookChapter.DataBind();
            //tvBookChapter.ExpandAll();
        }

        protected void ddlViewChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {
            switch (e.Parameter)
            {
                case "VIEW_KNOWLEDGE":
                    {
                        tvBookChapter.Nodes.Clear();
                        BindKnowledgeTree();
                        break;
                    }
                case "VIEW_TRAINTYPE":
                    {
                        tvBookChapter.Nodes.Clear();
                        BindTrainTypeTree();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            tvBookChapter.RenderControl(e.Output);
        }
    }
}
