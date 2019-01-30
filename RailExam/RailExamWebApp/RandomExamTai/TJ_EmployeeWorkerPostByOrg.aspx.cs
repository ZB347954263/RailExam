using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;
 
using System.Collections.Generic;
 

namespace RailExamWebApp.RandomExamTai
{
	public partial class TJ_EmployeeWorkerPostByOrg :PageBase
	{
		 private static Dictionary<string,string > dic;
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                hfOrgID.Value = Request.QueryString.Get("orgid");
			}
		}

        private void BindGrid()
        {
            if (hfPostIDs.Value == "")
                Grid.DataSource = null;
            else
                Grid.DataSource = GetInfo();
            Grid.DataBind();
            txtPostName.Text = hfPostNames.Value;
            ViewState["dt"] = Grid.DataSource;
            hfRefreshExcel.Value = "";
        }
 
		private DataTable GetInfo()
		{
			try
			{
				string strwhere = "";
                if (hfOrgID.Value != "0")
                {
                    strwhere += " and getstationorgid(a.org_id)=" + hfOrgID.Value;
                }
                else
                {
                    int railSystemId = PrjPub.GetRailSystemId();
                    if (railSystemId != 0)
                    {
                        strwhere += " and (GetRailSystemId(a.org_id)=" + railSystemId + " or getstationorgid(a.org_id)=" + PrjPub.CurrentLoginUser.StationOrgID + ")";
                    }
                }

				dic = new Dictionary<string, string>();

				if (hfPostIDs.Value != "")
				{
					string[] ids = hfPostIDs.Value.Split(',');
					//string[] names = hfPostNames.Value.Split(',');
					//if (ids.Length == names.Length)
					//{  
						for (int i = 0; i < ids.Length; i++)
						{
							string sqlsel = string.Format("select  post_name from post where id_path ={0}", ids[i]);
							DataTable dtname = new OracleAccess().RunSqlDataSet(sqlsel).Tables[0];
							string name = "";
							if (dtname != null)
								name = dtname.Rows[0][0].ToString();
							dic.Add(ids[i],
									name.Replace("、", "#").Replace("(", "AA").Replace(")", "BB").Replace("（", "AA").Replace("）", "BB"));
						}
					//}
				}

				OracleAccess access = new OracleAccess();
				StringBuilder sql = new StringBuilder();
				sql.Append("select * from (select b.short_name,b.Order_Index");
				//foreach (string k in dic.Keys)
				//{
				//    sql.AppendFormat(", sum(case POST_ID when {0} then 1 else 0  end) as {1}", k,
				//                     dic[k]);
				//}

				foreach (string k in dic.Keys)
				{
					sql.AppendFormat(@", sum(case when '/'||c.id_path||'/' like '%/'||{0}||'/%' then 1 else 0  end) as {1}", k,
									 dic[k]);
				}

				//sql.Append(" from employee a  inner join org b on getstationorgid(a.org_id)=b.org_id ");
				sql.Append(" from employee a  inner join org b on getstationorgid(a.org_id)=b.org_id ");
				sql.Append(" inner join post c on c.post_id=a.post_id  ");
				sql.AppendFormat(" where  a.ISONPOST=1 and a.ISREGISTERED=1 {0} group by b.Short_Name,b.Order_Index) order by order_index ", strwhere);

				DataTable dt = access.RunSqlDataSet(sql.ToString()).Tables[0];
				dt.Columns.Remove("Order_Index");
				return dt;
			}
			catch
			{
				return null;
			}
		}



		protected void Grid_RowCreated(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.Header)
			{
				TableCellCollection tcl = e.Row.Cells;
				tcl.Clear();
				tcl.Add(new TableHeaderCell());
				tcl[0].Text = "单位";
				//tcl[0].Wrap = false;
				//tcl[0].Width = 100;
				tcl[0].HorizontalAlign = HorizontalAlign.Center;
				int n = 1;
				foreach (string k in dic.Keys)
				{
					tcl.Add(new TableHeaderCell());
					tcl[n].Text = dic[k].Replace("#", "、").Replace("AA", "(").Replace("BB", ")");
					tcl[n].Wrap = false;
					if (dic.Keys.Count > 13)
						tcl[n].Width = 50;
					n++;
				}
			}
			if (e.Row.RowType == DataControlRowType.Header)
			{
				e.Row.Attributes.Add("class", "HeadingRow");
			}

			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes.Add("onclick", "selectCol(this)");
				e.Row.Cells[0].Wrap = true;
			}
		}

		protected void btnOK_Click(object sender, EventArgs e)
		{
		    BindGrid();
		}

		protected void Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			Grid.PageIndex = e.NewPageIndex;
			Grid.DataSource = ViewState["dt"] ;
			Grid.DataBind();
		}

		protected void btnPost_Click(object sender, EventArgs e)
		{
			string name = "";
			OracleAccess access = new OracleAccess();
			if (hfPostIDs.Value != "")
			{
				string sql = string.Format("select  post_name from post where id_path in ({0})", hfPostIDs.Value);
				DataTable dt = access.RunSqlDataSet(sql).Tables[0];

				foreach (DataRow r in dt.Rows)
				{
					name += r[0] + ",";

				}
				txtPostName.Text = name.Substring(0, name.Length - 1);
			}
			else
				txtPostName.Text = "";
			hfPostNames.Value = txtPostName.Text;
		}

		protected void btnExcel_Click(object sender, EventArgs e)
		{
			if (ViewState["dt"] != null && hfRefreshExcel.Value=="")
				Session["EmployeeWorkerPostByOrg"] = ViewState["dt"];

			if (hfRefreshExcel.Value == "true")
			{
			    hfRefreshExcel.Value = "";
			    DownloadExcel("各单位工种人数统计信息");
			    
			}
		}
		 

		private void DownloadExcel(string strName)
		{
			string filename = Server.MapPath("/RailExamBao/Excel/" + strName + ".xls");

			if (File.Exists(filename))
			{
				FileInfo file = new FileInfo(filename);
				this.Response.Clear();
				this.Response.Buffer = true;
				this.Response.Charset = "utf-7";
				this.Response.ContentEncoding = Encoding.UTF7;
				// 添加头信息，为"文件下载/另存为"对话框指定默认文件名
				this.Response.AddHeader("Content-Disposition",
										"attachment; filename=" + HttpUtility.UrlEncode(strName) + ".xls");
				// 添加头信息，指定文件大小，让浏览器能够显示下载进度

				this.Response.AddHeader("Content-Length", file.Length.ToString());

				// 指定返回的是一个不能被客户端读取的流，必须被下载

				this.Response.ContentType = "application/ms-excel";

				// 把文件流发送到客户端

				this.Response.WriteFile(file.FullName);
			}
		}
	}
}