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

namespace RailExamWebApp.Knowledge
{
    public partial class OtherKnowledge : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.HasEditRight("其他知识") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }
                if (PrjPub.HasDeleteRight("其他知识") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                } 
                BindOtherKnowledgeTree();
            }
        }

        private void BindOtherKnowledgeTree()
        {
            OtherKnowledgeBLL otherKnowledgeBLL = new OtherKnowledgeBLL();

            IList<RailExam.Model.OtherKnowledge> otherKnowledgeList = otherKnowledgeBLL.GetOtherKnowledge(0, 0, "", 0, 0,
                                                                    "", "", "", 0, 4000, "LevelNum,OtherKnowledgeID ASC");

            Pub.BuildComponentArtTreeView(tvOtherKnowledge, (IList)otherKnowledgeList, "OtherKnowledgeID",
                "ParentID", "OtherKnowledgeName", "OtherKnowledgeName", "OtherKnowledgeID", null, null, null);

            //tvOtherKnowledge.ExpandAll();
        }

        [ComponentArtCallbackMethod]
        public bool tvOtherKnowledgeNodeMove(int OtherKnowledgeID, string direction)
        {
            OtherKnowledgeBLL otherKnowledgeBLL = new OtherKnowledgeBLL();

            if (direction.ToUpper() == "UP")
            {
                return otherKnowledgeBLL.MoveUp(OtherKnowledgeID);
            }
            else if (direction.ToUpper() == "DOWN")
            {
                return otherKnowledgeBLL.MoveDown(OtherKnowledgeID);
            }
            else
            {
                SessionSet.PageMessage = "未知移动方向！";
            }

            return false;
        }

        protected void tvOtherKnowledgeChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {
            tvOtherKnowledge.Nodes.Clear();
            BindOtherKnowledgeTree();

            tvOtherKnowledge.RenderControl(e.Output);
        }
    }
}