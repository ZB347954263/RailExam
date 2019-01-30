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

namespace RailExamWebApp.Train
{
    public partial class TrainStandardTypeSelect : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string str = Request.QueryString.Get("id");
                ViewState["PostID"] = str;

                if (str != null && str != string.Empty)
                {
                    BindTypeTree(str);
                }
            }
        }

        private void BindTypeTree(string str)
        {
            TrainTypeBLL objTrainType = new TrainTypeBLL();

            IList<TrainType> train = objTrainType.GetTrainStandardTypeInfo(Convert.ToInt32(ViewState["PostID"].ToString()), 0);

            PostBLL objPostBll = new PostBLL();
            Post objPost = objPostBll.GetPost(Convert.ToInt32(ViewState["PostID"].ToString()));

            if (train.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (TrainType traintype in train)
                {
                    if (objPost.Promotion == 0 && traintype.IsPromotion == true)
                    {
                        continue;
                    }
                    tvn = new TreeViewNode();
                    tvn.ID = traintype.TrainTypeID.ToString();
                    tvn.Value = traintype.TrainTypeID.ToString();
                    tvn.Text = traintype.TypeName;
                    tvn.ToolTip = traintype.TypeName;
                    tvn.ShowCheckBox = true;

                    if (traintype.ParentID == 0)
                    {
                        tvTrainType.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            tvTrainType.FindNodeById(traintype.ParentID.ToString()).Nodes.Add(tvn);
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
        protected void btnOk_Click(object sender, EventArgs e)
        {
            string str = ViewState["PostID"].ToString();
            foreach (TreeViewNode node in tvTrainType.Nodes)
            {
                if (node.Checked == true)
                {
                    TrainStandardBLL objTrainStandard = new TrainStandardBLL();
                    IList<RailExam.Model.TrainStandard> objTrainStandardList =
                        objTrainStandard.GetTrainStandardInfo(0, Convert.ToInt32(str), Convert.ToInt32(node.Value), "", "", "", "", "", "", 0, 200, "");

                    if (objTrainStandardList.Count == 0)
                    {
                        RailExam.Model.TrainStandard obj = new RailExam.Model.TrainStandard();

                        obj.PostID = Convert.ToInt32(str);
                        obj.TypeID = Convert.ToInt32(node.Value);
                        obj.TrainTime = "";
                        obj.TrainContent = "";
                        obj.TrainForm = "";
                        obj.ExamForm = "";
                        obj.Description = "";
                        obj.Memo = "";

                        objTrainStandard.AddTrainStandard(obj);
                    }
                }

                if (node.Nodes.Count > 0)
                {
                    foreach (TreeViewNode childnode in node.Nodes)
                    {
                        if (childnode.Checked == true)
                        {
                            TrainStandardBLL objTrainStandard = new TrainStandardBLL();
                            IList<RailExam.Model.TrainStandard> objTrainStandardList =
                                objTrainStandard.GetTrainStandardInfo(0, Convert.ToInt32(str), Convert.ToInt32(childnode.Value), "", "", "", "", "", "", 0, 200, "");

                            if (objTrainStandardList.Count == 0)
                            {
                                RailExam.Model.TrainStandard obj = new RailExam.Model.TrainStandard();

                                obj.PostID = Convert.ToInt32(str);
                                obj.TypeID = Convert.ToInt32(childnode.Value);
                                obj.TrainTime = "";
                                obj.TrainContent = "";
                                obj.TrainForm = "";
                                obj.ExamForm = "";
                                obj.Description = "";
                                obj.Memo = "";

                                objTrainStandard.AddTrainStandard(obj);
                            }
                        }
                    }
                }
            }
            Response.Write("<script>window.opener.form1.Refresh.value='" + str + "' ;window.opener.form1.submit();window.close();</script>");
        }
    }
}