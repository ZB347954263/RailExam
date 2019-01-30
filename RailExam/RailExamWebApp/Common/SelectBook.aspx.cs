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

namespace RailExamWebApp.Common
{
    public partial class SelectBook : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindKnowledgeTree();
            }
        }

        private void BindKnowledgeTree()
        {
            KnowledgeBLL knowledgeBLL = new KnowledgeBLL();
            IList<RailExam.Model.Knowledge> knowledgeList = knowledgeBLL.GetKnowledges(0, 0, "", 0, 0,
                                                                        "", "", "", 0, 40,
                                                                        "LevelNum,KnowledgeId ASC");

            if (knowledgeList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (RailExam.Model.Knowledge knowledge in knowledgeList)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = knowledge.KnowledgeId.ToString();
                    tvn.Value = knowledge.IdPath;
                    tvn.Text = knowledge.KnowledgeName;
                    tvn.ToolTip = knowledge.KnowledgeName;
                    tvn.Attributes.Add("isKnowledge", "true");
                    tvn.ImageUrl = "~/App_Themes/" + StyleSheetTheme + "/Images/TreeView/Knowledge.gif";

                    if (knowledge.ParentId == 0)
                    {
                        tvBookChapter.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            tvBookChapter.FindNodeById(knowledge.ParentId.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvBookChapter.Nodes.Clear();
                            SessionSet.PageMessage = "数据错误！";
                            return;
                        }
                    }
                }
            }

            Bind(tvBookChapter.Nodes);

            tvBookChapter.DataBind();
            //tvBookChapter.ExpandAll();
        }

        private void Bind(TreeViewNodeCollection tvnNodes)
        {
            foreach (TreeViewNode node in tvnNodes)
            {
                if (node.Nodes.Count == 0)
                {

                    BookBLL objBll = new BookBLL();
                    IList<RailExam.Model.Book> objList = objBll.GetBookByKnowledgeIDPath(node.Value);

                    foreach (RailExam.Model.Book book in objList)
                    {
                        TreeViewNode item = new TreeViewNode();
                        item.ID = book.bookId.ToString();
                        item.Value = book.bookId.ToString();
                        item.Text = book.bookName;
                        item.ToolTip = book.bookName;
                        item.Attributes.Add("isBook", "true");
                        item.ImageUrl = "~/App_Themes/" + StyleSheetTheme + "/Images/TreeView/Book.gif";

                        try
                        {
                            tvBookChapter.FindNodeById(node.ID).Nodes.Add(item);
                        }
                        catch
                        {
                            tvBookChapter.Nodes.Clear();
                            SessionSet.PageMessage = "数据错误！";
                            return;
                        }
                    }
                }
                else
                {
                    Bind(node.Nodes);
                }
            }
        }
    }
}
