using System;
using System.Collections;
using System.Collections.Generic;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;
namespace RailExamWebApp.AssistBook
{
    public partial class AssistBookCategory : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindAssistBookCategoryTree();
                if (PrjPub.HasEditRight("辅导体系") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }
                if (PrjPub.HasDeleteRight("辅导体系") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }
            }
        }

        private void BindAssistBookCategoryTree()
        {
            AssistBookCategoryBLL assistBookCategoryBLL = new AssistBookCategoryBLL();

            IList<RailExam.Model.AssistBookCategory> assistBookCategoryList = assistBookCategoryBLL.GetAssistBookCategorys();

            Pub.BuildComponentArtTreeView(tvAssistBookCategory, (IList)assistBookCategoryList, "AssistBookCategoryId",
                "ParentId", "AssistBookCategoryName", "AssistBookCategoryName", "AssistBookCategoryId", null, null, null);

            //tvAssistBookCategory.ExpandAll();
        }

        [ComponentArtCallbackMethod]
        public bool tvAssistBookCategoryNodeMove(int assistBookCategoryId, string direction)
        {
            AssistBookCategoryBLL assistBookCategoryBLL = new AssistBookCategoryBLL();

            if (direction.ToUpper() == "UP")
            {
                return assistBookCategoryBLL.MoveUp(assistBookCategoryId);
            }
            else if (direction.ToUpper() == "DOWN")
            {
                return assistBookCategoryBLL.MoveDown(assistBookCategoryId);
            }
            else
            {
                SessionSet.PageMessage = "未知移动方向！";
            }

            return false;
        }

        protected void tvAssistBookCategoryChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {
            tvAssistBookCategory.Nodes.Clear();
            BindAssistBookCategoryTree();
            tvAssistBookCategory.RenderControl(e.Output);
        }
    }
}
