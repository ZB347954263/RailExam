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
    public partial class SelectPaper : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPaperCategoryTree();
            }
        }

        private void BindPaperCategoryTree()
        {
            //string strCategoryID = Request.QueryString.Get("Op");

            PaperCategoryBLL paperCategoryBLL = new PaperCategoryBLL();

            //IList<RailExam.Model.PaperCategory> PaperCategoryList = PaperCategoryBLL.GetPaperCategories(int.Parse(strCategoryID));//选择考试试卷 PaperCategoryId=3
            IList<RailExam.Model.PaperCategory> paperCategoryList = paperCategoryBLL.GetPaperCategories();

            Pub.BuildComponentArtTreeView(tvPaperCategory, (IList)paperCategoryList, "PaperCategoryId",
                "ParentId", "CategoryName", "CategoryName", "IdPath", null, null, null);

            //tvPaperCategory.ExpandAll();
        }
    }
}