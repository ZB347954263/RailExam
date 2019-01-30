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
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Train
{
    public partial class TrainTypes : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
				if (PrjPub.CurrentLoginUser == null)
				{
					Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
					return;
				}

                if (PrjPub.HasEditRight("培训类别") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }
                if (PrjPub.HasDeleteRight("培训类别") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }

                BindTrainTypeTree();

                
            }
        }

        private void BindTrainTypeTree()
        {
            TrainTypeBLL objTrainType = new TrainTypeBLL();

            IList<TrainType> train = objTrainType.GetTrainTypes();

            Pub.BuildComponentArtTreeView(tvTrainType, (IList)train, "TrainTypeID", "ParentID", "TypeName", "TypeName", "TrainTypeID", null, null, null);
        }

        protected void tvTrainTypeMoveCallBack_Callback(object sender, CallBackEventArgs e)
        {
            ComponentArt.Web.UI.TreeViewNode node = tvTrainType.FindNodeById(e.Parameters[0]);

            if (node != null && e.Parameters[1] == "CanMoveUp")
            {
                if (node.PreviousSibling != null)
                {
                    hfCanMove.Value = "true";
                    hfCanMove.RenderControl(e.Output);
                }
                else
                {
                    hfCanMove.Value = string.Empty;
                    hfCanMove.RenderControl(e.Output);
                }
            }
            else if (node != null && e.Parameters[1] == "CanMoveDown")
            {
                if (node.NextSibling != null)
                {
                    hfCanMove.Value = "true";
                    hfCanMove.RenderControl(e.Output);
                }
                else
                {
                    hfCanMove.Value = string.Empty;
                    hfCanMove.RenderControl(e.Output);
                }
            }
        }

        protected void tvTrainTypeChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {
            TrainTypeBLL objTrainType = new TrainTypeBLL();
            TrainType traintype = objTrainType.GetTrainTypeInfo(int.Parse(e.Parameters[0]));
            int cout = traintype.ParentID == 0 ? tvTrainType.Nodes.Count : tvTrainType.FindNodeById(traintype.ParentID.ToString()).Nodes.Count;

            if (e.Parameters[1] == "MoveUp")
            {
                if (traintype.OrderIndex <= cout && traintype.OrderIndex >= 2)
                {
                    traintype.OrderIndex--;
                    objTrainType.UpdateTrainType(traintype);

                    traintype = objTrainType.GetTrainTypeInfo(int.Parse(tvTrainType.FindNodeById(e.Parameters[0]).PreviousSibling.ID));
                    traintype.OrderIndex++;
                    objTrainType.UpdateTrainType(traintype);
                }
            }
            if (e.Parameters[1] == "MoveDown")
            {
                if (traintype.OrderIndex <= cout - 1 && traintype.OrderIndex >= 1)
                {
                    traintype.OrderIndex++;
                    objTrainType.UpdateTrainType(traintype);

                    traintype = objTrainType.GetTrainTypeInfo(int.Parse(tvTrainType.FindNodeById(e.Parameters[0]).NextSibling.ID));
                    traintype.OrderIndex--;
                    objTrainType.UpdateTrainType(traintype);
                }
            }

			if (e.Parameters[1] == "Insert")
			{
				IList<RailExam.Model.TrainType> objList = objTrainType.GetTrainTypeByWhereClause("1=1", "Train_Type_ID desc");
				hfMaxID.Value = objList[0].TrainTypeID.ToString();
				hfMaxID.RenderControl(e.Output);
			}
			else if (e.Parameters[1] == "Delete")
			{
				hfMaxID.Value = e.Parameters[2];
				hfMaxID.RenderControl(e.Output);
			}

            tvTrainType.Nodes.Clear();
            BindTrainTypeTree();

            tvTrainType.RenderControl(e.Output);
        }
    }
}