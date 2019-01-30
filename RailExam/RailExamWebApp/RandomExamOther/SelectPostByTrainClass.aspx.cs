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

namespace RailExamWebApp.RandomExamOther
{
	public partial class SelectPostByTrainClass : PageBase
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
			OracleAccess db = new OracleAccess();
			string strID = Request.QueryString["id"];

			string strTrainClassID = Request.QueryString.Get("trainClassID");
			if (strTrainClassID == "")
			{
				return;
			}

			string[] str = strTrainClassID.Split(',');
			string strArchivesID = string.Empty;
			for (int j = 0; j < str.Length; j++)
			{
				if (strArchivesID == string.Empty)
				{
					strArchivesID = "'" + str[j] + "'";
				}
				else
				{
					strArchivesID = strArchivesID + ",'" + str[j] + "'";
				}
			}

			string strSql;
			Hashtable htPostID = new Hashtable();
			strSql = "select b.Post_ID,c.Post_Name from CheckRoll_Info a "
					 + "inner join Employee@link_RailExam b on a.Employee_ID=b.Employee_ID "
					 + "inner join Post@link_RailExam c on b.Post_ID=c.Post_ID "
					 + " where a.Archives_ID in (" + strArchivesID + ")";
			DataSet ds = db.RunSqlDataSet(strSql);

			PostBLL postBLL = new PostBLL();

			TreeViewNode tvn = null;
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				if (!htPostID.ContainsKey(dr["Post_ID"].ToString()))
				{
					tvn = new TreeViewNode();
					tvn.ID = dr["Post_ID"].ToString();
					tvn.Value = dr["Post_ID"].ToString();
					tvn.Text = dr["Post_Name"].ToString();
					tvn.ToolTip = dr["Post_Name"].ToString();
					tvn.ShowCheckBox = true;

					if (("," + strID + ",").IndexOf("," + dr["Post_ID"] + ",") >= 0)
					{
						tvn.Checked = true;
					}

					tvPost.Nodes.Add(tvn);

					htPostID.Add(dr["Post_ID"].ToString(), dr["Post_ID"].ToString());
				}
			}
		}
	}
}
