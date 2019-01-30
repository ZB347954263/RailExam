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
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
	public partial class SelectPost : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BindPostTree();
			}
		}

		private void BindPostTree()
		{
			string strID = Request.QueryString["id"];

			string strTrainClassID = Request.QueryString.Get("trainClassID");
			if(strTrainClassID == "")
			{
				return;
			}

            OracleAccess db = new OracleAccess();
			string strSql;
			Hashtable htPostID = new Hashtable();
			strSql = "select a.*,b.Post_ID from ZJ_Train_Plan_Employee a "
			         + "inner join Employee b on a.Employee_ID=b.Employee_ID "
			         + " where a.Train_Class_ID in (" + strTrainClassID + ")";
            DataSet ds = db.RunSqlDataSet(strSql);

			PostBLL postBLL = new PostBLL();
		
			TreeViewNode tvn = null;
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
                if (!htPostID.ContainsKey(dr["Post_ID"].ToString()))
				{
                    Post post = postBLL.GetPost(Convert.ToInt32(dr["Post_ID"].ToString()));
					tvn = new TreeViewNode();
					tvn.ID = post.PostId.ToString();
					tvn.Value = post.PostId.ToString();
					tvn.Text = post.PostName;
					tvn.ToolTip = post.PostName;
					tvn.ShowCheckBox = true;

					if (("," + strID + ",").IndexOf("," + post.PostId + ",") >= 0)
					{
						tvn.Checked = true;
					}

					tvPost.Nodes.Add(tvn);

                    htPostID.Add(dr["Post_ID"].ToString(), post.PostId);
				}
			}
		}
	}
}
