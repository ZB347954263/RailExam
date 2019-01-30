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
    public partial class SelectType : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["PostID"] = Request.QueryString.Get("PostID");

                lblTitle.Text = Request.QueryString.Get("PostName");
                BindTypeTree();
            }
        }

        private void BindTypeTree()
        {
            TrainTypeBLL objBll = new TrainTypeBLL();

            IList<TrainType> train = objBll.GetTrainStandardTypeInfo(Convert.ToInt32(ViewState["PostID"].ToString()), 1);

            if (train.Count > 0)
            {
                TreeViewNode tvn = null;

                foreach (TrainType traintype in train)
                {
                    tvn = new TreeViewNode();
                    tvn.ID = traintype.TrainTypeID.ToString();
                    tvn.Value = traintype.TrainTypeID.ToString();
                    tvn.Text = traintype.TypeName;
                    tvn.ToolTip = traintype.TypeName;

                    if (traintype.ParentID == 0)
                    {
                        tvType.Nodes.Add(tvn);
                    }
                    else
                    {
                        try
                        {
                            tvType.FindNodeById(traintype.ParentID.ToString()).Nodes.Add(tvn);
                        }
                        catch
                        {
                            tvType.Nodes.Clear();
                            Response.Write("数据错误！");
                            return;
                        }
                    }
                }
            }

            tvType.DataBind();
            //tvType.ExpandAll();
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (tvType.SelectedNode == null)
            {
                Response.Write("<script> alert('请选择培训类别！'); </script>");
                return;
            }

            Response.Write("<script>window.opener.form1.txtTypeID.value='" + tvType.SelectedNode.ID + "';window.opener.form1.txtTypeName.value='" + tvType.SelectedNode.Text + "';window.opener.form1.submit();window.close();</script>");
        }
    }
}
