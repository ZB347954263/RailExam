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
    public partial class MultiSelectBookChapter : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

                foreach (RailExam.Model.Knowledge  knowledge in knowledgeList)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = knowledge.KnowledgeId.ToString();
                    tvn.Value = knowledge.KnowledgeId.ToString();
                    tvn.Text = knowledge.KnowledgeName;
                    tvn.ToolTip = knowledge.KnowledgeName;
                    tvn.Attributes.Add("isKnowledge", "true");
                    tvn.ImageUrl = "~/App_Themes/" + StyleSheetTheme + "/Images/TreeView/Knowledge.gif";
                    tvn.ContentCallbackUrl = "~/Common/GetKnowledgeBook.aspx?flag=1&id=" + knowledge.IdPath + "&StrategyID=" + Request.QueryString.Get("StrategyID");

                    if (knowledge.ParentId == 0)
                    {
                        tvKnowledge.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            tvKnowledge.FindNodeById(knowledge.ParentId.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvKnowledge.Nodes.Clear();
                            SessionSet.PageMessage = "数据错误！";
                            return;
                        }
                    }
                }
            }
            //tvKnowledge.DataBind();
            //tvKnowledge.ExpandAll();
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
                    tvn.Value = trainType.TrainTypeID.ToString();
                    tvn.Text = trainType.TypeName;
                    tvn.ToolTip = trainType.TypeName;
                    tvn.Attributes.Add("isTrainType", "true");
                    tvn.ImageUrl = "~/App_Themes/" + StyleSheetTheme + "/Images/TreeView/Knowledge.gif";

                    tvn.ContentCallbackUrl = "~/Common/GetTrainTypeBook.aspx?flag=1&id=" + trainType.IDPath;

                    if (trainType.ParentID == 0)
                    {
                        tvKnowledge.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            tvKnowledge.FindNodeById(trainType.ParentID.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvKnowledge.Nodes.Clear();
                            SessionSet.PageMessage = "数据错误！";
                            return;
                        }
                    }
                }
            }
            //tvKnowledge.DataBind();
            //tvKnowledge.ExpandAll();
        }

        protected void ddlViewChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {
            switch (e.Parameter)
            {
                case "VIEW_KNOWLEDGE":
                    {
                        tvKnowledge.Nodes.Clear();
                        this.BindKnowledgeTree();
                        break;
                    }
                case "VIEW_TRAINTYPE":
                    {
                        tvKnowledge.Nodes.Clear();
                        this.BindTrainTypeTree();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            tvKnowledge.RenderControl(e.Output);
        }
    }
}
