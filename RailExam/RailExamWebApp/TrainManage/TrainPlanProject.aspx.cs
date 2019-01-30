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
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;
using ComponentArt.Web.UI;

namespace RailExamWebApp.TrainManage
{
    public partial class TrainPlanProject : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TreeViewDataBind();

                string selectedID = Request.QueryString.Get("selectedID");
                if (!String.IsNullOrEmpty(selectedID))
                {
                    foreach (TreeViewNode tvNode in this.tvView.Nodes)
                    {
                        if (tvNode.ID == selectedID)
                        {
                            this.tvView.SelectedNode = tvNode;
                            break;
                        }
                    }
                }

                hfDelete.Value = PrjPub.HasDeleteRight("培训项目").ToString();
                hfUpdate.Value = PrjPub.HasEditRight("培训项目").ToString();

            }
        }

        private void TreeViewDataBind()
        {
            OracleAccess oa = new OracleAccess();
            string sql = "select * from ZJ_TRAINPLAN_TYPE t";
            DataSet dsType = oa.RunSqlDataSet(sql);
            if (dsType != null & dsType.Tables.Count > 0)
            {
                foreach (DataRow row in dsType.Tables[0].Rows)
                {
                    TreeViewNode tvNode = new TreeViewNode();
                    tvNode.ID = row["trainplan_type_id"].ToString();
                    tvNode.Text = row["trainplan_type_name"].ToString();
                    this.tvView.Nodes.Add(tvNode);
                }
                this.tvView.ExpandAll();
            }
        }
    }
}
