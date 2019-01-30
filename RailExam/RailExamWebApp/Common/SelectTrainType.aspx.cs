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
using RailExam.Model;
using System.Collections.Generic;

namespace RailExamWebApp.Common
{
    public partial class SelectTrainType : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTrainTypeTree();
            }
        }

        private void BindTrainTypeTree()
        {
            //ArrayList objList = GetTypeList();

            TrainTypeBLL trainTypeBLL = new TrainTypeBLL();
            IList<TrainType> trainTypes = trainTypeBLL.GetTrainTypes();

            string strID = Request.QueryString["id"];
            string[] strIDS = { };
            if (!string.IsNullOrEmpty(strID))
            {
                strIDS = strID.Split(',');
            }

            if (trainTypes.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (TrainType trainType in trainTypes)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = trainType.TrainTypeID.ToString();
                    tvn.Value = trainType.TrainTypeID.ToString();
                    tvn.Text = trainType.TypeName;
                    tvn.ToolTip = trainType.TypeName;

                    if (trainType.ParentID == 0)
                    {
                        tvTrainType.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            tvTrainType.FindNodeById(trainType.ParentID.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvTrainType.Nodes.Clear();
                            SessionSet.PageMessage = "Êý¾Ý´íÎó£¡";
                            return;
                        }
                    }
                }
            }

            tvTrainType.DataBind();
            //tvTrainType.ExpandAll();
        }
    }
}
