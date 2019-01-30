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
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Online.Study
{
    public partial class ReadBook : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
				if(!PrjPub.IsShowOnline())
				{
					Response.Redirect("/RailExamBao/Common/Error.aspx?error=您当前时间无权使用此功能");
				}
                BindTree();
                BindTrainTypeTree();
            }

            string str1 = Request.Form.Get("Test1");
            if (str1 != null & str1 != "")
            {
                BindTree();
                BindTrainTypeTree();
            }
        }

        private void BindTree()
        {
            KnowledgeBLL knowledgeBLL = new KnowledgeBLL();

            IList<RailExam.Model.Knowledge> knowledgeList = knowledgeBLL.GetKnowledges();

            if (knowledgeList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (RailExam.Model.Knowledge knowledge in knowledgeList)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = knowledge.KnowledgeId.ToString();
                    tvn.Value = knowledge.IdPath.ToString();
                    tvn.Text = knowledge.KnowledgeName;
                    tvn.ToolTip = knowledge.KnowledgeName;

                    if (knowledge.ParentId == 0)
                    {
                        TreeView1.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            TreeView1.FindNodeById(knowledge.ParentId.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            TreeView1.Nodes.Clear();
                            SessionSet.PageMessage = "数据错误！";
                            return;
                        }
                    }
                }
            }

            TreeView1.DataBind();
            //TreeView1.ExpandAll();
        }

        private void BindTrainTypeTree()
        {
            TrainTypeBLL objTrainType = new TrainTypeBLL();

            IList<TrainType> train = objTrainType.GetTrainTypes();

            Pub.BuildComponentArtTreeView(TreeView2, (IList)train, "TrainTypeID", "ParentID", "TypeName", "TypeName","IDPath", null, null, null);
            //TreeView2.ExpandAll();
        }
    }
}
