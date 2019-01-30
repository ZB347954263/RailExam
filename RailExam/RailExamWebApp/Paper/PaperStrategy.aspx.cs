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

namespace RailExamWebApp.Paper
{
    public partial class PaperStrategy : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.IsServerCenter && SessionSet.OrganizationID != 1)
                {
                    HfUpdateRight.Value = "False";
                }
                else
                {
                    HfUpdateRight.Value = PrjPub.HasEditRight("组卷策略").ToString();
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
                "ParentId", "CategoryName", "CategoryName", "IdPath", null, null, null);

            //tvPaperCategory.ExpandAll();
        }
    }
}