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
using DSunSoft.Web.UI;

namespace RailExamWebApp.Train
{
    public partial class TrainStandardType : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["PostID"] = Request.QueryString.Get("id");
                BindtvType(ViewState["PostID"].ToString());
            }
            string strRefresh = Request.Form.Get("Refresh");
            if (strRefresh != null & strRefresh != "")
            {
                BindtvType(strRefresh);
            }

            btnAddType.Attributes.Add("onclick", "AddRecord(" + ViewState["PostID"].ToString() + ");");
        }

        private void BindtvType(string str)
        {
            ViewState["PostID"] = str;
            TrainStandardBLL trainStandardBLL = new TrainStandardBLL();
            TrainTypeBLL trainTypeBLL = new TrainTypeBLL();

            IList<TrainType> trainTypeList = trainTypeBLL.GetTrainStandardTypeInfo(Convert.ToInt32(str), 1);

            if (trainTypeList.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (TrainType trainType in trainTypeList)
                {
                    tvn = new TreeViewNode();
                    RailExam.Model.TrainStandard trainStandard = new RailExam.Model.TrainStandard();
                    trainStandard = trainStandardBLL.GetTrainStandardInfo(Convert.ToInt32(ViewState["PostID"].ToString()),
                                                                 trainType.TrainTypeID);
                    int nStandardID = trainStandard.TrainStandardID;
                    tvn.ID = nStandardID.ToString();
                    tvn.Value = nStandardID.ToString();
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
                            trainStandard = trainStandardBLL.GetTrainStandardInfo(Convert.ToInt32(ViewState["PostID"].ToString()),
                                                                         trainType.ParentID);
                            int nParentStandardID = trainStandard.TrainStandardID;
                            tvTrainType.FindNodeById(nParentStandardID.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvTrainType.Nodes.Clear();
                            Response.Write("数据错误！");
                            return;
                        }
                    }
                }
            }

            tvTrainType.DataBind();
            tvTrainType.ExpandAll();
        }

        private void GetNowType()
        {
            TrainTypeBLL objTrainType = new TrainTypeBLL();

            IList<TrainType> train = objTrainType.GetTrainStandardTypeInfo(Convert.ToInt32(ViewState["PostID"].ToString()), 1);

            ArrayList objList = new ArrayList();

            foreach (TrainType traintype in train)
            {
                objList.Add(traintype.TrainTypeID);
            }

            AddImplateType(objList);
        }

        private void AddImplateType(ArrayList objList)
        {
            tvTrainType.Nodes.Clear();
            TrainTypeBLL trainTypeBLL = new TrainTypeBLL();

            IList<TrainType> trainTypeList = trainTypeBLL.GetTrainTypeInfo(0, 0, 0, "", 0, "", "", true, false, "", 0, 200, "LevelNum,OrderIndex ASC");

            if (trainTypeList.Count > 0)
            {
                foreach (TrainType trainType in trainTypeList)
                {
                    if (objList.Count > 0)
                    {
                        if (objList.IndexOf(trainType.TrainTypeID) != -1)
                        {
                            continue;
                        }
                    }

                    TrainStandardBLL trainStandardBLL = new TrainStandardBLL();
                    RailExam.Model.TrainStandard trainStandard = new RailExam.Model.TrainStandard();

                    trainStandard.PostID = Convert.ToInt32(ViewState["PostID"].ToString());
                    trainStandard.TypeID = Convert.ToInt32(trainType.TrainTypeID);
                    trainStandard.TrainTime = "";
                    trainStandard.TrainContent = "";
                    trainStandard.TrainForm = "";
                    trainStandard.ExamForm = "";
                    trainStandard.Description = "";
                    trainStandard.Memo = "";

                    trainStandardBLL.AddTrainStandard(trainStandard);
                }
            }

            BindtvType(ViewState["PostID"].ToString());
        }

        protected void btnInsertType_Click(object sender, EventArgs e)
        {
            GetNowType();
        }

        protected void tvTrainTypeChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {
            tvTrainType.Nodes.Clear();
            BindtvType(ViewState["PostID"].ToString());
            tvTrainType.RenderControl(e.Output);
        }
    }
}