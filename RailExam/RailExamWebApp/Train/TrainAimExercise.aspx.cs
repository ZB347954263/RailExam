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
    public partial class TrainAimExercise : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTree();
            }
        }

        private void BindTree()
        {
            PaperCategoryBLL PaperCategoryBLL = new PaperCategoryBLL();

            IList<RailExam.Model.PaperCategory> PaperCategoryList = PaperCategoryBLL.GetPaperCategories();

            if (PaperCategoryList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (PaperCategory PaperCategory in PaperCategoryList)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = PaperCategory.PaperCategoryId.ToString();
                    tvn.Value = PaperCategory.PaperCategoryId.ToString();
                    tvn.Text = PaperCategory.CategoryName;
                    tvn.ToolTip = PaperCategory.CategoryName;

                    if (PaperCategory.ParentId == 0)
                    {
                        TreeView1.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            TreeView1.FindNodeById(PaperCategory.ParentId.ToString()).Nodes.Add(tvn);
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
            //TreeView1.ExpandAll();
        }
    }
}