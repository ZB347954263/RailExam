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

namespace RailExamWebApp.Train
{
    public partial class TrainAimTask : PageBase
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
            PaperCategoryBLL paperCategoryBLL = new PaperCategoryBLL();

            IList<RailExam.Model.PaperCategory> paperCategoryList = paperCategoryBLL.GetPaperCategories();

            if (paperCategoryList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (PaperCategory paperCategory in paperCategoryList)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = paperCategory.PaperCategoryId.ToString();
                    tvn.Value = paperCategory.PaperCategoryId.ToString();
                    tvn.Text = paperCategory.CategoryName;
                    tvn.ToolTip = paperCategory.CategoryName;

                    if (paperCategory.ParentId == 0)
                    {
                        TreeView1.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            TreeView1.FindNodeById(paperCategory.ParentId.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            TreeView1.Nodes.Clear();
                            SessionSet.PageMessage = "数据错误！";
                            return;
                        }
                    }
                }
            }

            TreeView1.DataBind();
            TreeView1.ExpandAll();
        }
    }
}