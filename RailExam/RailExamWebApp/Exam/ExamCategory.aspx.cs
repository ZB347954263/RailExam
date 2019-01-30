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

namespace RailExamWebApp.Exam
{
    public partial class ExamCategory : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(PrjPub.IsServerCenter && SessionSet.OrganizationID != 1)
                {
                    HfUpdateRight.Value = "False";
                    HfDeleteRight.Value = "False";
                }
                else
                {
                    HfUpdateRight.Value = PrjPub.HasEditRight("考试分类").ToString();
                    HfDeleteRight.Value = PrjPub.HasDeleteRight("考试分类").ToString();
                }

                BindExamCategoryTree();
            }
        }

        private void BindExamCategoryTree()
        {
            ExamCategoryBLL examCategoryBLL = new ExamCategoryBLL();

            IList<RailExam.Model.ExamCategory> examCategoryList = examCategoryBLL.GetExamCategories();

            Pub.BuildComponentArtTreeView(tvExamCategory, (IList)examCategoryList, "ExamCategoryId",
                "ParentId", "CategoryName", "CategoryName", "ExamCategoryId", null, null, null);

            //tvExamCategory.ExpandAll();
        }

        [ComponentArtCallbackMethod]
        public bool tvExamCategoryNodeMove(int examCategoryId, string direction)
        {
            ExamCategoryBLL examCategoryBLL = new ExamCategoryBLL();

            if (direction.ToUpper() == "UP")
            {
                return examCategoryBLL.MoveUp(examCategoryId);
            }
            else if (direction.ToUpper() == "DOWN")
            {
                return examCategoryBLL.MoveDown(examCategoryId);
            }
            else
            {
                SessionSet.PageMessage = "未知移动方向！";
            }

            return false;
        }

        protected void tvExamCategoryChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {
            tvExamCategory.Nodes.Clear();
            BindExamCategoryTree();

            tvExamCategory.RenderControl(e.Output);
        }
    }
}
