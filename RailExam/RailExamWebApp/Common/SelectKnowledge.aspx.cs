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

namespace RailExamWebApp.Common
{
    public partial class SelectKnowledge : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTree();
            }
        }

        private void BindTree()
        {
            KnowledgeBLL knowledgeBLL = new KnowledgeBLL();

            IList<RailExam.Model.Knowledge> knowledgeList = new List<RailExam.Model.Knowledge>();

			if(!string.IsNullOrEmpty(Request.QueryString.Get("type")))
			{
				knowledgeList = knowledgeBLL.GetKnowledgesByWhereClause("id_path || '/' like '/790/%'", "LEVEL_NUM, ORDER_INDEX ASC");

				TreeViewNode tvn = null;

				foreach (RailExam.Model.Knowledge obj in knowledgeList)
				{
					tvn = new TreeViewNode();
					tvn.ID = obj.KnowledgeId.ToString();
					tvn.Value = obj.KnowledgeId.ToString();
					tvn.Text = obj.KnowledgeName;
					tvn.ToolTip = obj.KnowledgeName;

					if (obj.ParentId == 0)
					{
						tvKnowledge.Nodes.Add(tvn);
					}
					else
					{
						try
						{
							tvKnowledge.FindNodeById(obj.ParentId.ToString()).Nodes.Add(tvn);
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
			else
			{
				knowledgeList = knowledgeBLL.GetKnowledges();
				Pub.BuildComponentArtTreeView(tvKnowledge, (IList)knowledgeList, "KnowledgeId","ParentId", "KnowledgeName", "KnowledgeName", "KnowledgeId", null, null, null);
			}
            //tvKnowledge.ExpandAll();
        }
    }
}
