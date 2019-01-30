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
using RailExamWebApp.Common.Class;
using System.Text;
using System.IO;

namespace RailExamWebApp
{
	public partial class TJ_EmployeeWorkerStructureByPost : System.Web.UI.Page
	{
		private static int eduTblCount = 0;
		private static int techTblCount = 0;
		string strwhere = "";
			
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
                hfOrgID.Value = Request.QueryString.Get("orgid");
                BindGrid();
			}
		}

        private void BindGrid()
        {
            if (hfOrgID.Value != "0")
            {
                strwhere += " and getstationorgid(e.org_id)=" + hfOrgID.Value;
            }
            else
            {
                int railSystemId = PrjPub.GetRailSystemId();
                if (railSystemId != 0)
                {
                    strwhere += " and (GetRailSystemId(e.org_id)=" + railSystemId + " or getstationorgid(e.org_id)=" + PrjPub.CurrentLoginUser.StationOrgID + ")";
                }
            }

            GridInfo.DataSource = GetInfo();
            GridInfo.DataBind();
            ViewState["dt"] = GridInfo.DataSource;
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

		private DataTable GetInfo()
		{
			try
			{

				DataTable dt = GetEmpAgeInfo();
				DataTable dtEdu = GetEducationLevelInfo();
				DataTable dtTech = GetTechnicianInfo();
				for (int i = 1; i < dtEdu.Columns.Count; i++)
				{
					dt.Columns.Add(dtEdu.Columns[i].ColumnName, typeof (string));

					for (int j = 0; j < dt.Rows.Count; j++)
					{
						dt.Rows[j][dtEdu.Columns[i].ColumnName] = dtEdu.Rows[j][dtEdu.Columns[i].ColumnName];
					}

				}
				for (int i = 1; i < dtTech.Columns.Count; i++)
				{
					dt.Columns.Add(dtTech.Columns[i].ColumnName, typeof (string));

					for (int j = 0; j < dt.Rows.Count; j++)
					{
						dt.Rows[j][dtTech.Columns[i].ColumnName] = dtTech.Rows[j][dtTech.Columns[i].ColumnName];
					}
				}

				eduTblCount = dtEdu.Columns.Count - 1;
				techTblCount = dtTech.Columns.Count - 1;
				hfEduCount.Value = eduTblCount.ToString();   //文化程度的个数
				hfTechCount.Value = techTblCount.ToString();  //技能等级的个数
				return dt;
			}
			catch
			{
				return null;
			}

		}


		private DataTable  GetEmpAgeInfo()
		{

			OracleAccess access = new OracleAccess();
			string sql = string.Format(@"select p.post_id,p.post_name,
                        sum(case when  e.ISREGISTERED=1 then 1 else 0 end) as x,
				        sum(case when GetEmployeeAge(e.birthday)<=25    then 1 else 0 end ) as a ,
						 sum(case when GetEmployeeAge(e.birthday)>25 and GetEmployeeAge(e.birthday)<=30  then 1 else 0 end )  as b,
						 sum(case when GetEmployeeAge(e.birthday)>30 and GetEmployeeAge(e.birthday)<=35  then 1 else 0 end ) as c,
						 sum(case when GetEmployeeAge(e.birthday)>35 and GetEmployeeAge(e.birthday)<=40  then 1 else 0 end ) as d,
						 sum(case when GetEmployeeAge(e.birthday)>40 and GetEmployeeAge(e.birthday)<=45  then 1 else 0 end ) as e,
						 sum(case when GetEmployeeAge(e.birthday)>45 and GetEmployeeAge(e.birthday)<=50  then 1 else 0 end ) as f,
						 sum(case when GetEmployeeAge(e.birthday)>50 and GetEmployeeAge(e.birthday)<=55  then 1 else 0 end ) as g,
						 sum(case when GetEmployeeAge(e.birthday)>55 and GetEmployeeAge(e.birthday)<=60  then 1 else 0 end ) as h,
                        sum(case when GetEmployeeAge(e.birthday)>60  then 1 else 0 end ) as j
				  from employee e 
                 left join post p on p.post_id=GetParentPostID(e.post_id) 
                 where  e.ISREGISTERED=1  {0}
				 group by p.post_id,p.post_name order by  p.post_id ", strwhere);
			DataTable dt = access.RunSqlDataSet(sql).Tables[0];
			dt.Columns.Remove("x");
			dt.Columns.Remove("post_id");
			return dt;
		}

		private DataTable GetEducationLevelInfo()
		{

			DataTable dtEdu = GetEducationLevel();
			OracleAccess access = new OracleAccess();
			StringBuilder sql = new StringBuilder();
			DataTable dt;
            sql.Append(" select p.post_id,p.post_name,Sum(Case when EDUCATION_LEVEL_ID=-1 and e.ISREGISTERED=1 then 1 else 0 end ) as 文化程度未填 ");
			foreach (DataRow rEdu in dtEdu.Rows)
			{
				sql.AppendFormat(", sum(case EDUCATION_LEVEL_ID when {0} then 1 else 0  end) as {1}", rEdu["EDUCATION_LEVEL_ID"],
								 rEdu["EDUCATION_LEVEL_NAME"]);
			}
			sql.AppendFormat(@"
                    from employee e 
                left join post p on p.post_id=GetParentPostID(e.post_id) 
                where e.ISREGISTERED=1  {0}
				group by p.post_id,p.post_name  order by p.post_id 
            ", strwhere);
			dt = access.RunSqlDataSet(sql.ToString()).Tables[0];
			
			dt.Columns.Remove("post_name");
			return dt;
		}

		private DataTable GetTechnicianInfo()
		{

			DataTable dtTech = GetTechnician();
			OracleAccess access = new OracleAccess();
			StringBuilder sql = new StringBuilder();

			sql.Append("select p.post_id,p.post_name ,sum(case   when  e.ISREGISTERED=1   then 1 else 0 end)  小计," +
                       "Sum(Case when (TECHNICIAN_TYPE_ID=-1 or TECHNICIAN_TYPE_ID=0) and e.ISREGISTERED=1  then 1 else 0 end ) as 技能等级未填");
			foreach (DataRow r in dtTech.Rows)
			{
				sql.AppendFormat(", sum(case TECHNICIAN_TYPE_ID when {0} then 1 else 0  end) as {1}", r["technician_type_id"],
								 r["type_name"]);
			}
			sql.AppendFormat(@"
                    from employee e 
                    left join post p on p.post_id=GetParentPostID(e.post_id)     
                     where e.ISREGISTERED=1  {0}
				   group by p.post_id,p.post_name  order by p.post_id 
            ", strwhere);
			DataTable dt=access.RunSqlDataSet(sql.ToString()).Tables[0];
		
			dt.Columns.Remove("post_name");
			return dt;
		}


		/// <summary>
		/// 获取文化程度
		/// </summary>
		/// <returns></returns>
		private DataTable GetEducationLevel()
		{
			OracleAccess access = new OracleAccess();
			return
				access.RunSqlDataSet(
					"select EDUCATION_LEVEL_ID,EDUCATION_LEVEL_NAME from education_level order by order_index").Tables[0
					];
		}

		/// <summary>
		/// 获取技能等级
		/// </summary>
		/// <returns></returns>
		private DataTable GetTechnician()
		{
			OracleAccess access = new OracleAccess();
			return
				access.RunSqlDataSet(
					"select technician_type_id,type_name from technician_type order by technician_type_id").Tables[0];
		}

		protected void GridInfo_RowCreated(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.Header)
			{
				TableCellCollection tcl = e.Row.Cells;
				tcl.Clear();
				tcl.Add(new TableHeaderCell());
				tcl[0].Text = "工种";
				//tcl[0].Wrap = false;
				tcl[0].RowSpan = 2;
				tcl[0].Width = 150;

				tcl.Add(new TableHeaderCell());
				tcl[1].Text = "年 龄 结 构";
				tcl[1].Wrap = false;
				tcl[1].ColumnSpan = 9;

				tcl.Add(new TableHeaderCell());
				tcl[2].Text = " 文 化 程 度";
				tcl[2].Wrap = false;
				tcl[2].ColumnSpan = eduTblCount;

				tcl.Add(new TableHeaderCell());
				tcl[3].Text = " 职 业 技 能 等 级</th></tr><tr>";
				tcl[3].Wrap = false;
				tcl[3].ColumnSpan = techTblCount;

				tcl.Add(new TableHeaderCell());
				tcl[4].Text = "25岁及以下";
				tcl[4].Wrap = false;
				tcl[4].Width = 40;

				tcl.Add(new TableHeaderCell());
				tcl[5].Text = "26岁至30岁";
				tcl[5].Wrap = false;
				tcl[5].Width = 40;

				tcl.Add(new TableHeaderCell());
				tcl[6].Text = "31岁至35岁";
				tcl[6].Wrap = false;
				tcl[6].Width = 40;

				tcl.Add(new TableHeaderCell());
				tcl[7].Text = "36岁至40岁";
				tcl[7].Wrap = false;
				tcl[7].Width = 40;

				tcl.Add(new TableHeaderCell());
				tcl[8].Text = "41岁至45岁";
				tcl[8].Wrap = false;
				tcl[8].Width = 40;

				tcl.Add(new TableHeaderCell());
				tcl[9].Text = "46岁至50岁";
				tcl[9].Wrap = false;
				tcl[9].Width = 40;

				tcl.Add(new TableHeaderCell());
				tcl[10].Text = "51岁至55岁";
				tcl[10].Wrap = false;
				tcl[10].Width = 40;

				tcl.Add(new TableHeaderCell());
				tcl[11].Text = "56岁至60岁";
				tcl[11].Wrap = false;
				tcl[11].Width = 40;

                tcl.Add(new TableHeaderCell());
                tcl[12].Text = "60岁以上";
                tcl[12].Wrap = false;
                tcl[12].Width = 40;

                tcl.Add(new TableHeaderCell());
                tcl[13].Text = "未填";
                tcl[13].Wrap = false;
                tcl[13].Width = 40;

				DataTable dtEdu = GetEducationLevel();
				for (int i = 0; i < dtEdu.Rows.Count; i++)
				{
					tcl.Add(new TableHeaderCell());
					tcl[i + 14].Text = dtEdu.Rows[i]["EDUCATION_LEVEL_NAME"].ToString();
					tcl[i + 14].Wrap = false;
					tcl[i + 14].Width = 40;
				}

				tcl.Add(new TableHeaderCell());
				tcl[14 + dtEdu.Rows.Count].Text = "小计";
				tcl[14 + dtEdu.Rows.Count].Wrap = false;
				tcl[14 + dtEdu.Rows.Count].Width = 40;

                tcl.Add(new TableHeaderCell());
                tcl[15 + dtEdu.Rows.Count].Text = "未填";
                tcl[15 + dtEdu.Rows.Count].Wrap = false;
                tcl[15 + dtEdu.Rows.Count].Width = 40;
				DataTable dtTech = GetTechnician();

				for (int i = 0; i < dtTech.Rows.Count; i++)
				{
					tcl.Add(new TableHeaderCell());
					tcl[i + 16 + dtEdu.Rows.Count].Text = dtTech.Rows[i]["type_name"].ToString();
					tcl[i + 16 + dtEdu.Rows.Count].Wrap = false;
					tcl[i + 16 + dtEdu.Rows.Count].Width = 40;
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

		protected void GridInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			GridInfo.PageIndex = e.NewPageIndex;
			GridInfo.DataSource = ViewState["dt"];
			GridInfo.DataBind();
		}

		protected void btnExcel_Click(object sender, EventArgs e)
		{
			if (ViewState["dt"] != null && hfRefreshExcel.Value == "")
				Session["EmployeeWorkerStructureByPost"] = ViewState["dt"];

			if (hfRefreshExcel.Value == "true")
			{
				hfRefreshExcel.Value = "";
				DownloadExcel("各工种工人结构统计信息");

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
