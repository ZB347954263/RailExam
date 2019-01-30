using System;
using System.Data;
using System.Configuration;
using System.Collections;
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
using System.Collections.Generic;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Online.Study
{
    public partial class StudyBookTree : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			//得到是否是武汉铁路局
			if (PrjPub.IsWuhan())
			{
				hfIswuhan.Value = "1";
			}
			else
			{
				hfIswuhan.Value = "0";
			}

            if (!IsPostBack)
            {
                BindTree();
                BindTrainTypeTree();
            }

            string str1 = Request.Form.Get("Test1");
            if (str1 != null & str1 != "")
            {
                BindTree();
                BindTrainTypeTree();
            }
        }

        private void BindTree()
        {
            KnowledgeBLL knowledgeBLL = new KnowledgeBLL();

            IList<RailExam.Model.Knowledge> knowledgeList = knowledgeBLL.GetKnowledges();

            if (knowledgeList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (RailExam.Model.Knowledge knowledge in knowledgeList)
                {
					if (knowledge.LevelNum == 1)
					{
						tvn = new TreeViewNode();
						tvn.ID = knowledge.KnowledgeId.ToString();
						tvn.Value = knowledge.IdPath.ToString();
						tvn.Text = knowledge.KnowledgeName;
						tvn.ToolTip = knowledge.KnowledgeName;

						if (knowledge.ParentId == 0)
						{
							TreeView1.Nodes.Add(tvn);
						}
					}
                	//else
					//{
					//    try
					//    {
					//        TreeView1.FindNodeById(knowledge.ParentId.ToString()).Nodes.Add(tvn);
					//    }
					//    catch
					//    {
					//        TreeView1.Nodes.Clear();
					//        SessionSet.PageMessage = "数据错误！";
					//        return;
					//    }
					//}
                }
            }

            TreeView1.DataBind();
            //TreeView1.ExpandAll();
        }

        private void BindTrainTypeTree()
        {
            TrainTypeBLL objTrainType = new TrainTypeBLL();

            IList<TrainType> train = objTrainType.GetTrainTypes();

			//Pub.BuildComponentArtTreeView(TreeView2, (IList)train, "TrainTypeID", "ParentID", "TypeName", "TypeName", "IDPath", null, null, null);
            //TreeView2.ExpandAll();

			if (train.Count > 0)
			{
				TreeViewNode tvn = null;

				foreach (RailExam.Model.TrainType obj in train)
				{
					if (obj.LevelNum == 1)
					{
						tvn = new TreeViewNode();
						tvn.ID = obj.TrainTypeID.ToString();
						tvn.Value = obj.IDPath.ToString();
						tvn.Text = obj.TypeName;
						tvn.ToolTip = obj.TypeName;

						if (obj.ParentID == 0)
						{
							TreeView2.Nodes.Add(tvn);
						}
					}
				}
			}
        }

		///// <summary>
		///// 记时
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//protected void Callback1_Callback(object sender, CallBackEventArgs e)
		//{
		//    //if (PrjPub.IsWuhan())
		//    //{
		//    //    return;
		//    //}

		//    //TimeSpan ts = DateTime.Now.Subtract(DateTime.Now);
		//    //int a = ts.Minutes;

		//    //EmployeeBLL employeeBLL = new EmployeeBLL();
		//    //Employee employyee = employeeBLL.GetEmployee(Convert.ToInt32( Request.QueryString["EmployeID"]));
		//    //int oldLogintime = employyee.LoginTime;

		//    //int minute = Convert.ToInt32(e.Parameters[0]) / 60;
		//    //employyee.LoginTime = oldLogintime + Convert.ToInt32(e.Parameters[0]);

		//    //employeeBLL.UpdateEmployee(employyee);
		//}
    }
}
