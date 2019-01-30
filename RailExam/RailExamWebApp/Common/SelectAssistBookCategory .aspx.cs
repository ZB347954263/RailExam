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
using RailExamWebApp.Common.Class;
using System.Collections.Generic;

namespace RailExamWebApp.Common
{
    public partial class SelectAssistBookCategory : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindAssistBookCategoryTree();
            }
        }

        private void BindAssistBookCategoryTree()
        {
            AssistBookCategoryBLL knowledgeBLL = new AssistBookCategoryBLL();

            IList<RailExam.Model.AssistBookCategory> knowledgeList = knowledgeBLL.GetAssistBookCategorys();

            Pub.BuildComponentArtTreeView(tvKnowledge, (IList)knowledgeList, "AssistBookCategoryId",
                "ParentId", "AssistBookCategoryName", "AssistBookCategoryName", "IdPath", null, null, null);
        }
    }
}
