using System;
using System.Collections;
using System.Collections.Generic;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Online.Study
{
    public partial class StudyAssistBook : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTree();
                //BindTrainTypeTree();
            }

            string str1 = Request.Form.Get("Test1");
            if (str1 != null & str1 != "")
            {
                BindTree();
                //BindTrainTypeTree();
            }
        }

        private void BindTree()
        {
            AssistBookCategoryBLL objBll = new AssistBookCategoryBLL();

            IList<AssistBookCategory> objList = objBll.GetAssistBookCategorys();

            if (objList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (AssistBookCategory obj in objList)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = obj.AssistBookCategoryId.ToString();
                    tvn.Value = obj.IdPath.ToString();
                    tvn.Text = obj.AssistBookCategoryName;
                    tvn.ToolTip = obj.AssistBookCategoryName;

                    if (obj.ParentId == 0)
                    {
                        tvAssist.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            tvAssist.FindNodeById(obj.ParentId.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvAssist.Nodes.Clear();
                            SessionSet.PageMessage = "Êý¾Ý´íÎó£¡";
                            return;
                        }
                    }
                }
            }

            tvAssist.DataBind();
            //tvAssist.ExpandAll();
        }

        //private void BindTrainTypeTree()
        //{
        //    TrainTypeBLL objTrainType = new TrainTypeBLL();

        //    IList<TrainType> train = objTrainType.GetTrainTypes();

        //    Pub.BuildComponentArtTreeView(tvTrainType, (IList)train, "TrainTypeID", "ParentID", "TypeName", "TypeName", "IDPath", null, null, null);
        //    //TreeView2.ExpandAll();
        //}
    }    
}
