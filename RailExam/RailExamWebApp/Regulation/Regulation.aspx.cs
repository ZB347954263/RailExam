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
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Regulation
{
    public partial class Regulation : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.HasEditRight("政策法规") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }
                if (PrjPub.HasDeleteRight("政策法规") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }
                BindTree();
            }
            else
            {
                string strDeleteID = Request.Form.Get("DeleteID");

                if (!string.IsNullOrEmpty(strDeleteID))
                {
                    RegulationCategoryBLL regulationCategoryBll = new RegulationCategoryBLL();

                    regulationCategoryBll.DeleteRegulationCategory(int.Parse(strDeleteID));

                    BindTree();
                }

                if (Request.Form.Get("Refresh") == "true")
                {
                    BindTree();
                }
            }
        }

        private void BindTree()
        {
            RegulationCategoryBLL regulationCategoryBll = new RegulationCategoryBLL();

            IList<RegulationCategory> regulationCategoryList = regulationCategoryBll.GetRegulationCategories(
                                                0, 0, "", 0, 0, "", "", "", 0, 200, "LevelNum,RegulationCategoryID ASC");

            Pub.BuildComponentArtTreeView(tvRegulationCategory, (IList)regulationCategoryList, "RegulationCategoryID",
                "ParentID", "CategoryName", "CategoryName", "IdPath", null, null, null);

            //tvRegulationCategory.ExpandAll();
        }
    }
}