using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;
using System.Collections.Generic;

namespace RailExamWebApp.AssistBook
{
    public partial class AssistBook : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAssistBookCategoryTree();
                //BindTrainTypeTree();
                if (PrjPub.HasEditRight("¸¨µ¼½Ì²Ä") && PrjPub.IsServerCenter && PrjPub.CurrentLoginUser.SuitRange == 1)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }
            }

            string strRefresh = Request.Form.Get("Refresh");
            if (!string.IsNullOrEmpty(strRefresh))
            {
                BindAssistBookCategoryTree();
                //BindTrainTypeTree();
            }
        }

        private void BindAssistBookCategoryTree()
        {
            AssistBookCategoryBLL knowledgeBLL = new AssistBookCategoryBLL();

            IList<RailExam.Model.AssistBookCategory> knowledgeList = knowledgeBLL.GetAssistBookCategorys();

            Pub.BuildComponentArtTreeView(tvAssist, (IList)knowledgeList, "AssistBookCategoryId",
                "ParentId", "AssistBookCategoryName", "AssistBookCategoryName", "IdPath", null, null, null);
        }

        //private void BindTrainTypeTree()
        //{
        //    TrainTypeBLL trainTypeBLL = new TrainTypeBLL();

        //    IList<TrainType> trainTypes = trainTypeBLL.GetTrainTypes();

        //    Pub.BuildComponentArtTreeView(tvTrainType, (IList)trainTypes, "TrainTypeID", "ParentID", "TypeName", "TypeName",
        //                                  "IDPath", null, null, null);
        //}
    }
}