using System;
using System.Collections;
using System.Collections.Generic;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Knowledge
{
    public partial class Knowledge : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
				if (PrjPub.CurrentLoginUser == null)
				{
					Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
					return;
				}

                BindKnowledgeTree();
				if (PrjPub.HasEditRight("知识体系") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }
                if (PrjPub.HasDeleteRight("知识体系") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                } 
            }
        }

        private void BindKnowledgeTree()
        {
            KnowledgeBLL knowledgeBLL = new KnowledgeBLL();

            IList<RailExam.Model.Knowledge> knowledgeList = knowledgeBLL.GetKnowledges();

            Pub.BuildComponentArtTreeView(tvKnowledge, (IList)knowledgeList, "KnowledgeId",
                "ParentId", "KnowledgeName", "KnowledgeName", "KnowledgeId", null, null, null);

            //tvKnowledge.ExpandAll();
        }

        [ComponentArtCallbackMethod]
        public bool tvKnowledgeNodeMove(int knowledgeId, string direction)
        {
            KnowledgeBLL knowledgeBLL = new KnowledgeBLL();

            if (direction.ToUpper() == "UP")
            {
                return knowledgeBLL.MoveUp(knowledgeId);
            }
            else if (direction.ToUpper() == "DOWN")
            {
                return knowledgeBLL.MoveDown(knowledgeId);
            }
            else
            {
                SessionSet.PageMessage = "未知移动方向！";
            }

            return false;
        }

        protected void tvKnowledgeChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {
			KnowledgeBLL objKnowledgeBll = new KnowledgeBLL();
			if (e.Parameters[0] == "Insert")
			{
				IList<RailExam.Model.Knowledge> objList = objKnowledgeBll.GetKnowledgesByWhereClause("1=1", "Knowledge_ID desc");
				hfMaxID.Value = objList[0].KnowledgeId.ToString();
				hfMaxID.RenderControl(e.Output);
			}
            else if (e.Parameters[0] == "Delete" || e.Parameters[0] == "Update")
			{
				hfMaxID.Value = e.Parameters[1];
				hfMaxID.RenderControl(e.Output);
			}
            else if(e.Parameters[0] == "UP"|| e.Parameters[0] == "DOWN")
            {
                hfMaxID.Value = e.Parameters[1];
                hfMaxID.RenderControl(e.Output);
            }

            tvKnowledge.Nodes.Clear();
            BindKnowledgeTree();
            tvKnowledge.RenderControl(e.Output);
        }
    }
}
