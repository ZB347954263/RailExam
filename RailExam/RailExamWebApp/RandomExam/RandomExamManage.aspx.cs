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

namespace RailExamWebApp.RandomExam
{
    public partial class RandomExamManage : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (PrjPub.IsServerCenter && PrjPub.CurrentLoginUser.SuitRange != 1)
                //{
                //    HfUpdateRight.Value = "False";
                //}
                //else
                //{
                //    HfUpdateRight.Value = PrjPub.HasEditRight("考试设计").ToString();
                //}

				if (PrjPub.CurrentLoginUser == null)
				{
					Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统");
					return;
				}

                if (PrjPub.HasEditRight("新增考试") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }

                BindExamCategoryTree();
            }
        }


        private void BindExamCategoryTree()
        {
            ExamCategoryBLL examCategoryBLL = new ExamCategoryBLL();

            IList<RailExam.Model.ExamCategory> examCategoryList = examCategoryBLL.GetExamCategories();

            Pub.BuildComponentArtTreeView(tvExamCategory, (IList)examCategoryList, "ExamCategoryId",
                "ParentId", "CategoryName", "CategoryName", "IdPath", null, null, null);

            //tvExamCategory.SelectedNode = tvExamCategory.Nodes[0];
        }
    }
   
}
