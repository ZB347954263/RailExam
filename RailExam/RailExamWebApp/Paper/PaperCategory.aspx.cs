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
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Paper
{
    public partial class PaperCategory : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.IsServerCenter && SessionSet.OrganizationID != 1)
                {
                    HfUpdateRight.Value = "False";
                    HfDeleteRight.Value = "False";
                }
                else
                {
                    HfUpdateRight.Value = PrjPub.HasEditRight("试卷类别").ToString();
                    HfDeleteRight.Value = PrjPub.HasDeleteRight("试卷类别").ToString();
                } 
                BindPaperCategoryTree();
            }
        }

        private void BindPaperCategoryTree()
        {
            PaperCategoryBLL paperCategoryBLL = new PaperCategoryBLL();

            //IList<RailExam.Model.PaperCategory> paperCategoryList = paperCategoryBLL.GetPaperCategories();
            IList<RailExam.Model.PaperCategory> paperCategoryList = paperCategoryBLL.GetPaperCategories();

            Pub.BuildComponentArtTreeView(tvPaperCategory, (IList)paperCategoryList, "PaperCategoryId",
                "ParentId", "CategoryName", "CategoryName", "PaperCategoryId", null, null, null);

            //tvPaperCategory.ExpandAll();
        }

        [ComponentArtCallbackMethod]
        public bool tvPaperCategoryNodeMove(int PaperCategoryId, string direction)
        {
            PaperCategoryBLL paperCategoryBLL = new PaperCategoryBLL();

            if (direction.ToUpper() == "UP")
            {
                return paperCategoryBLL.MoveUp(PaperCategoryId);
            }
            else if (direction.ToUpper() == "DOWN")
            {
                return paperCategoryBLL.MoveDown(PaperCategoryId);
            }
            else
            {
                SessionSet.PageMessage = "未知移动方向！";
            }

            return false;
        }

        protected void tvPaperCategoryChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {
            tvPaperCategory.Nodes.Clear();
            BindPaperCategoryTree();

            tvPaperCategory.RenderControl(e.Output);
        }
    }
}