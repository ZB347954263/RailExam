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

namespace RailExamWebApp.Common
{
    public partial class MultiSelectTrainType : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTrainTypeTree();
            }
        }

        //private ArrayList GetTypeList()
        //{
        //    ArrayList objList = new ArrayList();
        //    string str = Request.QueryString.Get("id");
        //    string[] strType = str.Split(',');

        //    for(int i=0; i< strType.Length; i++)
        //    {
        //        objList.Add(strType[i].ToString());
        //    }

        //    return objList;
        //}

        private void BindTrainTypeTree()
        {
            //ArrayList objList = GetTypeList();

            TrainTypeBLL trainTypeBLL = new TrainTypeBLL();
        	IList<TrainType> trainTypes = new List<TrainType>();

			if(!string.IsNullOrEmpty(Request.QueryString.Get("type")))
			{
				trainTypes = trainTypeBLL.GetTrainTypeByWhereClause("id_path || '/' like '/363/%'", "LEVEL_NUM, ORDER_INDEX ASC");
			}
			else
			{
				trainTypes = trainTypeBLL.GetTrainTypes();
			}

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

                    IList<TrainType> trainTypes1 = trainTypeBLL.GetTrainTypeByParentId(trainType.TrainTypeID);

                    if (trainTypes1.Count == 0)
                    {
                        tvn.ShowCheckBox = true;
                    }

                    foreach (string strOrgID in strIDS)
                    {
                        if (strOrgID == trainType.TrainTypeID.ToString() &&  tvn.ShowCheckBox)
                            tvn.Checked = true;
                    }

                    //if(objList.IndexOf(tvn.ID) != -1)
                    //{
                    //    tvn.Checked = true;
                    //}

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
                            SessionSet.PageMessage = "数据错误！";
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
