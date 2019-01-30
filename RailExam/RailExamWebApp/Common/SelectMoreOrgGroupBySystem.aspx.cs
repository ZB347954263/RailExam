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
using System.Collections.Generic;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;

namespace RailExamWebApp.Common
{
	public partial class SelectMoreOrgGroupBySystem : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				ArrayList orgIDAL = new ArrayList();
				BindOrganizationTree(orgIDAL);
			}
		}

		private void BindOrganizationTree(ArrayList orgidAL)
		{
			string strIds = Request.QueryString.Get("selectOrgID");
			string classId = Request.QueryString.Get("classId");

			OracleAccess access = new OracleAccess();

			string strSql = "select * from ZJ_Train_Plan_Post_Class_Org where Train_Plan_Post_Class_ID=" + classId;
			DataSet ds = access.RunSqlDataSet(strSql);

			if (ds.Tables[0].Rows.Count > 0)
			{
				strSql = "select org_id,short_name,full_name,rail_system_id from  org "
                         + " where level_num=2 and Is_Effect=1  "
						 + " and org_id not in (select org_id from ZJ_Train_Plan_Post_Class_Org "
						 + " where Train_Plan_Post_Class_ID=" + classId
						 + ") order by order_index";
			}
			else
			{
				strSql = "select org_id,short_name,full_name,rail_system_id from  org "
                         + " where level_num=2 and Is_Effect=1  order by order_index";
			}

			DataTable dt =
				access.RunSqlDataSet(strSql).Tables[0];
		    strSql = "select * from rail_system where rail_system_Id>0 order by rail_system_id ";
			DataTable dtsystem = access.RunSqlDataSet(strSql).Tables[0];
			if (dtsystem != null && dtsystem.Rows.Count > 0)
			{
				DataRow dr = dtsystem.NewRow();
				dr["rail_system_id"] = 0;
				dr["rail_system_name"] = "其他";
				dtsystem.Rows.Add(dr);

				TreeViewNode tvn = null;
				foreach (DataRow r in dtsystem.Rows)
				{
					//绑定第一级
					tvn = new TreeViewNode();
					tvn.ID = r["rail_system_id"].ToString();
					tvn.Value = "0";
					tvn.Text = r["rail_system_name"].ToString();
					tvn.ToolTip = r["rail_system_name"].ToString();
					tvn.ShowCheckBox = true;
					tvOrg.Nodes.Add(tvn);
					try
					{
						strSql = " rail_system_id='"+r["rail_system_id"]+"'";
						DataRow[] arr= dt.Select(strSql);
						if (arr.Length>0)
						{
							//绑定第二级
							TreeViewNode tvn1 = null;
							foreach (DataRow dataRow in arr)
							{
								tvn1 = new TreeViewNode();
								tvn1.ID = dataRow["org_id"].ToString();
								tvn1.Value = dataRow["org_id"].ToString();
								tvn1.Text = dataRow["short_name"].ToString();
								tvn1.ToolTip = dataRow["full_name"].ToString();
								tvn1.ShowCheckBox = true;

								if (("," + strIds + ",").IndexOf("," + dataRow["org_id"] + ",") >= 0)
								{
									tvn1.Checked = true;
									tvn.Checked = true;
								}

								if (orgidAL.Count > 0)
								{
									if (orgidAL.IndexOf(dataRow["org_id"]) != -1)
									{
										tvn1.Checked = true;
									}
								}
								tvn.Nodes.Add(tvn1);
								 
							}
						}

					}
					catch
					{
						tvOrg.Nodes.Clear();
						SessionSet.PageMessage = "数据错误！";
						return;
					}
				}
				tvOrg.DataBind();
				tvOrg.ExpandAll();
			}
		}

		protected void btnOK_Click(object sender, EventArgs e)
		{
			
			List<string> lst = new List<string>();
			foreach (TreeViewNode tn in tvOrg.Nodes)
			{
				if (tvOrg.Nodes.Count > 0)
				{
					foreach (TreeViewNode tvn in tn.Nodes)
					{
						if (tvn.Checked && tvn.Value != "0")
						{
							lst.Add(tvn.ID);
						}
					}
				}
			}
			string strArr = string.Join(",", lst.ToArray());
			if (lst.Count <= 0)
				ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('请选择单位！')", true);
			else
				ClientScript.RegisterClientScriptBlock(GetType(), "", "col('" + strArr + "')", true);

		}
	}
}
