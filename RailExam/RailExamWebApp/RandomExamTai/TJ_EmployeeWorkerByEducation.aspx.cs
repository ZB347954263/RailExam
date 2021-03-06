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

namespace RailExamWebApp.RandomExamTai
{
	public partial class TJ_EmployeeWorkerByEducation : PageBase
	{
		
		private static int eduTblCount = 0;
		private static int techTblCount = 0;
		private static int techTitleCount = 0;

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
                hfWhereClause.Value += " and getstationorgid(e.org_id)=" + hfOrgID.Value;
            }
            else
            {
                int railSystemId = PrjPub.GetRailSystemId();
                if (railSystemId != 0)
                {
                    hfWhereClause.Value += " and (GetRailSystemId(e.org_id)=" + railSystemId + " or getstationorgid(e.org_id)=" + PrjPub.CurrentLoginUser.StationOrgID + ")";
                }
            }

            GridInfo.DataSource = GetInfo();
            GridInfo.DataBind();
            ViewState["dt"] = GridInfo.DataSource;
            GroupRows(GridInfo, 0);
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

		private DataTable GetInfo()
		{
			DataTable dt = new DataTable();

			dt.Columns.Add("org_id", typeof (string));
			dt.Columns.Add("short_name", typeof (string));
			dt.Columns.Add("education_employee_type_id", typeof (string));
			dt.Columns.Add("education_employee_type_name", typeof (string));
			DataTable dtOrg = GetOrg();
			DataTable dtEdu = GetEducationType();
			DataTable dtEmpInfo = GetEmpInfo();
			foreach (DataRow rOrg in dtOrg.Rows)
			{
				foreach (DataRow rEdu in dtEdu.Rows)
				{
					DataRow r = dt.NewRow();
					r["org_id"] = rOrg["org_id"];
					r["short_name"] = rOrg["to_char(o.short_name)"];
					r["education_employee_type_id"] = rEdu["education_employee_type_id"];
					r["education_employee_type_name"] = rEdu["education_employee_type_name"];
					dt.Rows.Add(r);
				}
			}
			//为dt添加列
			for (int i = 3; i < dtEmpInfo.Columns.Count; i++)
			{
				dt.Columns.Add(dtEmpInfo.Columns[i].ColumnName, typeof (string));
			}
			int n = dtEmpInfo.Columns.Count;
			//DataTable dtAllInfo = dt.Copy();
			foreach (DataRow rdt in dt.Rows)
			{
				string sql = "org_id=" + rdt["org_id"] + " and education_employee_type_id=" + rdt["education_employee_type_id"];
				DataRow[] arr = dtEmpInfo.Select(sql);

				if (arr.Length > 0)
				{
					for (int i = 3; i < n; i++)
					{
						rdt[i + 1] = arr[0][i];
					}
				}
				else
				{
					for (int i = 3; i < n; i++)
					{
						rdt[i + 1] = 0;
					}
				}
			}
			dt.Columns.Remove("org_id");
			dt.Columns.Remove("education_employee_type_id");

			hfEduType.Value = dtEdu.Rows.Count.ToString();   //职教类别个数
			return dt;
		}

		/// <summary>
		/// 获取各站段人员年龄，学历，技术职称，技能等级信息
		/// </summary>
		/// <returns></returns>
		private DataTable GetEmpInfo()
		{
			DataTable dt = GetEmpAge();
			DataTable dtEdu = GetEducationLevelInfo();
			DataTable dtTechTitle = GetTechnicianTitleInfo();
			DataTable dtTech = GetTechnicianInfo();
			for (int i = 4; i < dtEdu.Columns.Count; i++)
			{
				dt.Columns.Add(dtEdu.Columns[i].ColumnName, typeof (string));

				for (int j = 0; j < dt.Rows.Count; j++)
				{
					dt.Rows[j][dtEdu.Columns[i].ColumnName] = dtEdu.Rows[j][dtEdu.Columns[i].ColumnName];
				}

			}
			for (int i = 4; i < dtTechTitle.Columns.Count; i++)
			{
				dt.Columns.Add(dtTechTitle.Columns[i].ColumnName, typeof (string));

				for (int j = 0; j < dt.Rows.Count; j++)
				{
					dt.Rows[j][dtTechTitle.Columns[i].ColumnName] = dtTechTitle.Rows[j][dtTechTitle.Columns[i].ColumnName];
				}
			}
			for (int i = 4; i < dtTech.Columns.Count; i++)
			{
				string colName = "";
				if (dt.Columns.Contains(dtTech.Columns[i].ColumnName))
					colName = dtTech.Columns[i].ColumnName + "1";
				else
					colName = dtTech.Columns[i].ColumnName;
				dt.Columns.Add(colName, typeof (string));

				for (int j = 0; j < dt.Rows.Count; j++)
				{
					dt.Rows[j][colName] = dtTech.Rows[j][dtTech.Columns[i].ColumnName];
				}
			}

			eduTblCount = dtEdu.Columns.Count - 4;
			techTblCount = dtTech.Columns.Count - 4;
			techTitleCount = dtTechTitle.Columns.Count - 4;

			hfEduCount.Value = eduTblCount.ToString();   //文化程度的个数
			hfTechCount.Value = techTblCount.ToString();  //技能等级的个数
			hfTechTitleCount.Value = techTitleCount.ToString(); //技术职称
			return dt;

		}

		/// <summary>
		/// 获取各站段人员年龄信息
		/// </summary>
		/// <returns></returns>
		private DataTable GetEmpAge()
		{
			OracleAccess access = new OracleAccess();
			string sql =string.Format(
				@" select o.org_id,to_char(o.short_name),e.EDUCATION_EMPLOYEE_TYPE_ID,o.order_index ,
                             sum(case when  e.education_employee_type_id=e.education_employee_type_id   then 1 else 0 end ) as x,
                             sum(case when GetEmployeeAge(e.birthday)<=30  and e.education_employee_type_id=e.EDUCATION_EMPLOYEE_TYPE_ID   then 1 else 0 end ) as a,
                             sum(case when GetEmployeeAge(e.birthday)>30 and GetEmployeeAge(e.birthday)<=40  and e.education_employee_type_id=e.EDUCATION_EMPLOYEE_TYPE_ID   then 1 else 0 end ) as b,
                             sum(case when GetEmployeeAge(e.birthday)>40 and GetEmployeeAge(e.birthday)<=50  and e.education_employee_type_id=e.EDUCATION_EMPLOYEE_TYPE_ID   then 1 else 0 end ) as c, 
                             sum(case when GetEmployeeAge(e.birthday)>50 and e.education_employee_type_id=e.EDUCATION_EMPLOYEE_TYPE_ID   then 1 else 0 end ) as d
                           from employee e
                           inner join org o 
                                       on getstationorgid(e.org_id)=o.org_id  
                            left join  zj_education_employee_type ed on ed.education_employee_type_id=e.education_employee_type_id where e.ISREGISTERED=1  {0}
                            group by o.short_name,e.education_employee_type_id,o.order_index,o.org_id
                            union
                            select 0 ,'合计',ed.EDUCATION_EMPLOYEE_TYPE_ID,0,
                              sum(case when e.education_employee_type_id>0 then 1 else 0 end) x,
                              sum(case when GetEmployeeAge(e.birthday)<=30  and e.education_employee_type_id=e.EDUCATION_EMPLOYEE_TYPE_ID   then 1 else 0 end ) as a,
                              sum(case when GetEmployeeAge(e.birthday)>30 and GetEmployeeAge(e.birthday)<=40  and e.education_employee_type_id=e.EDUCATION_EMPLOYEE_TYPE_ID   then 1 else 0 end ) as b,
                              sum(case when GetEmployeeAge(e.birthday)>40 and GetEmployeeAge(e.birthday)<=50  and e.education_employee_type_id=e.EDUCATION_EMPLOYEE_TYPE_ID   then 1 else 0 end ) as c, 
                              sum(case when GetEmployeeAge(e.birthday)>50 and e.education_employee_type_id=e.EDUCATION_EMPLOYEE_TYPE_ID   then 1 else 0 end ) as d
                            from  employee e 
                               left join  zj_education_employee_type ed on ed.education_employee_type_id=e.education_employee_type_id where e.ISREGISTERED=1  {0}
                               group by ed.education_employee_type_id
                               order by order_index", hfWhereClause.Value);
			DataTable dt = access.RunSqlDataSet(sql).Tables[0];
			dt.Columns.Remove("order_index");
			
			return dt;
		}

		/// <summary>
		/// 获取学历结构详细信息
		/// </summary>
		/// <returns></returns>
		private DataTable GetEducationLevelInfo()
		{
			DataTable dtEdu = GetEducationLevel();

			OracleAccess access = new OracleAccess();
            string sql = "select o.org_id,to_char(o.short_name),e.EDUCATION_EMPLOYEE_TYPE_ID,o.order_index, " +
                         "sum(case  when EDUCATION_LEVEL_ID=-1 then 1 else 0  end)  文化程度未填";

			foreach (DataRow rEdu in dtEdu.Rows)
			{
				sql +=
					string.Format(
						", sum(case  when EDUCATION_LEVEL_ID={0}  then 1 else 0  end) as {1}",
						rEdu["EDUCATION_LEVEL_ID"],
						rEdu["EDUCATION_LEVEL_NAME"]);
			}
			sql +=string.Format(
				@" from employee e
                           inner join org o 
                                       on getstationorgid(e.org_id)=o.org_id  
                            left join  zj_education_employee_type ed on ed.education_employee_type_id=e.education_employee_type_id where e.ISREGISTERED=1  {0}
                            group by o.short_name,e.education_employee_type_id,o.order_index,o.org_id
                       union
                            select 0 ,'合计',ed.EDUCATION_EMPLOYEE_TYPE_ID,0,sum(case  when EDUCATION_LEVEL_ID=-1 then 1 else 0  end)  文化程度未填", hfWhereClause.Value);
			foreach (DataRow rEdu in dtEdu.Rows)
			{
				sql +=
					string.Format(
						", sum(case  when EDUCATION_LEVEL_ID={0}  then 1 else 0  end) as {1}",
						rEdu["EDUCATION_LEVEL_ID"],
						rEdu["EDUCATION_LEVEL_NAME"]);
			}

			sql +=string.Format(
				@" 
                              from  employee e 
                               left join  zj_education_employee_type ed on ed.education_employee_type_id=e.education_employee_type_id where e.ISREGISTERED=1  {0}
                               group by ed.education_employee_type_id
                          order by order_index", hfWhereClause.Value);

			return access.RunSqlDataSet(sql).Tables[0];
		}

		/// <summary>
		/// 获取技术职称详细信息
		/// </summary>
		/// <returns></returns>
		private DataTable GetTechnicianTitleInfo()
		{
			DataTable dtTech = GetTechnicianTitle();
			OracleAccess access = new OracleAccess();
			string sql =
				"select o.org_id,to_char(o.short_name),e.EDUCATION_EMPLOYEE_TYPE_ID,o.order_index ,sum(case   when  TECHNICAL_TITLE_ID>0 and e.education_employee_type_id=e.EDUCATION_EMPLOYEE_TYPE_ID then 1 else 0 end)  小计" +
                ",sum(case  when TECHNICAL_TITLE_ID=-1 then 1 else 0  end)  技术职称未填";
			foreach (DataRow r in dtTech.Rows)
			{
				sql +=
					string.Format(
						", sum(case  when TECHNICAL_TITLE_ID={0}  then 1 else 0  end) as {1}",
						r["technician_title_type_id"],
						r["type_name"]);
			}
			sql +=string.Format(
				@"  from employee e
                           inner join org o 
                                       on getstationorgid(e.org_id)=o.org_id  
                            left join  zj_education_employee_type ed on ed.education_employee_type_id=e.education_employee_type_id where e.ISREGISTERED=1  {0}
                            group by o.short_name,e.education_employee_type_id,o.order_index,o.org_id
                     union  select 0 ,'合计',ed.EDUCATION_EMPLOYEE_TYPE_ID,0,
                            sum(case  when  TECHNICAL_TITLE_ID>0 and e.education_employee_type_id=e.EDUCATION_EMPLOYEE_TYPE_ID then 1 else 0 end)  小计"
                        + ",sum(case  when TECHNICAL_TITLE_ID=-1 then 1 else 0  end)  技术职称未填", hfWhereClause.Value);
			foreach (DataRow r in dtTech.Rows)
			{
				sql +=
					string.Format(
						", sum(case  when TECHNICAL_TITLE_ID={0}  then 1 else 0  end) as {1}",
						r["technician_title_type_id"],
						r["type_name"]);
			}
			sql +=string.Format(
				@" 
                              from  employee e 
                               left join  zj_education_employee_type ed on ed.education_employee_type_id=e.education_employee_type_id where e.ISREGISTERED=1  {0}
                               group by ed.education_employee_type_id
                          order by order_index", hfWhereClause.Value);

			return access.RunSqlDataSet(sql).Tables[0];
		}

		/// <summary>
		/// 获取获取技能等级详细信息
		/// </summary>
		/// <returns></returns>
		private DataTable GetTechnicianInfo()
		{

			DataTable dtTech = GetTechnician();
			OracleAccess access = new OracleAccess();
			string sql =
				"select o.org_id,to_char(o.short_name),e.EDUCATION_EMPLOYEE_TYPE_ID,o.order_index ,sum(case   when  TECHNICIAN_TYPE_ID>0 and e.education_employee_type_id=e.EDUCATION_EMPLOYEE_TYPE_ID then 1 else 0 end)  小计" +
                ",sum(case  when TECHNICIAN_TYPE_ID=-1 or TECHNICIAN_TYPE_ID=0 then 1 else 0  end)  技能等级未填";
			foreach (DataRow r in dtTech.Rows)
			{
				sql +=
					string.Format(
						", sum(case  when TECHNICIAN_TYPE_ID={0}  then 1 else 0  end) as {1}",
						r["technician_type_id"],
						r["type_name"]);
			}
			sql +=string.Format(
				@"  from employee e
                           inner join org o 
                                       on getstationorgid(e.org_id)=o.org_id  
                            left join  zj_education_employee_type ed on ed.education_employee_type_id=e.education_employee_type_id where e.ISREGISTERED=1  {0}
                            group by o.short_name,e.education_employee_type_id,o.order_index,o.org_id
                           union  select 0 ,'合计',ed.EDUCATION_EMPLOYEE_TYPE_ID,0,
                            sum(case  when  TECHNICIAN_TYPE_ID>0 and e.education_employee_type_id=e.EDUCATION_EMPLOYEE_TYPE_ID then 1 else 0 end)  小计"
                        + ",sum(case  when TECHNICIAN_TYPE_ID=-1 or TECHNICIAN_TYPE_ID=0 then 1 else 0  end)  技能等级未填", hfWhereClause.Value);
			foreach (DataRow r in dtTech.Rows)
			{
				sql +=
					string.Format(
						", sum(case  when TECHNICIAN_TYPE_ID={0}  then 1 else 0  end) as {1}",
						r["technician_type_id"],
						r["type_name"]);
			}
			sql +=string.Format(
				@" 
                              from  employee e 
                               left join  zj_education_employee_type ed on ed.education_employee_type_id=e.education_employee_type_id where e.ISREGISTERED=1  {0}
                               group by ed.education_employee_type_id
                          order by order_index", hfWhereClause.Value);
			return access.RunSqlDataSet(sql).Tables[0];
		}

		/// <summary>
		/// 获取学历结构
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

		/// <summary>
		/// 获取所有站段
		/// </summary>
		/// <returns></returns>
		private DataTable GetOrg()
		{
			string str = "";
            if (hfOrgID.Value != "0")
            {
                str += " and getstationorgid(e.org_id)=" + hfOrgID.Value;
            }
            else
            {
                int railSystemId = PrjPub.GetRailSystemId();
                if (railSystemId != 0)
                {
                    str += " and (GetRailSystemId(e.org_id)=" + railSystemId + " or getstationorgid(e.org_id)=" + PrjPub.CurrentLoginUser.StationOrgID + ")";
                }
            }

			OracleAccess access = new OracleAccess();
			string sql =string.Format(
				@"  select to_char(o.short_name) ,o.org_id, o.order_index
							from employee e inner join org o on getstationorgid(e.org_id)=o.org_id  {0}
							group by o.short_name,o.order_index,o.org_id
							union 
							select '合计',0,0 from dual
							order by order_index",str);
			return access.RunSqlDataSet(sql).Tables[0];
		}

		/// <summary>
		/// 获取职教人员类型
		/// </summary>
		/// <returns></returns>
		private DataTable GetEducationType()
		{
			OracleAccess access = new OracleAccess();
			return
				access.RunSqlDataSet(" select * from zj_education_employee_type order by education_employee_type_id").Tables[0];
		}

		/// <summary>
		/// 获取技术职称
		/// </summary>
		/// <returns></returns>
		private DataTable GetTechnicianTitle()
		{
			return new OracleAccess().RunSqlDataSet("select * from  technician_title_type order by order_index").Tables[0];
		}

		protected void GridInfo_RowCreated(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.Header)
			{
			 
				TableCellCollection tcl = e.Row.Cells;
				tcl.Clear();
				tcl.Add(new TableHeaderCell());
				tcl[0].Text = "单位";
				tcl[0].Wrap = false;
				tcl[0].RowSpan = 2;
				tcl[0].ColumnSpan = 2;
				tcl[0].Width = 300;

				tcl.Add(new TableHeaderCell());
				tcl[1].Text = "总计(人)";
				tcl[1].Wrap = false;
				tcl[1].RowSpan = 2;
				tcl[1].Width = 40;

				tcl.Add(new TableHeaderCell());
				tcl[2].Text = "年龄结构";
				tcl[2].Wrap = false;
				tcl[2].ColumnSpan = 4;

				tcl.Add(new TableHeaderCell());
				tcl[3].Text = "学历结构";
				tcl[3].Wrap = false;
				tcl[3].ColumnSpan = eduTblCount;

				tcl.Add(new TableHeaderCell());
				tcl[4].Text = "技术职称";
				tcl[4].Wrap = false;
				tcl[4].ColumnSpan = techTitleCount;

				tcl.Add(new TableHeaderCell());
				tcl[5].Text = "技能等级</th></tr>";
				tcl[5].Wrap = false;
				tcl[5].ColumnSpan = techTblCount;

	            tcl.Add(new TableHeaderCell());
				tcl[6].Text = "30岁及以下";
				tcl[6].Wrap = false;
				tcl[6].Width = 40;

				tcl.Add(new TableHeaderCell());
				tcl[7].Text = "31-40岁";
				tcl[7].Wrap = false;
				tcl[7].Width = 40;

				tcl.Add(new TableHeaderCell());
				tcl[8].Text = "41-50岁";
				tcl[8].Wrap = false;
				tcl[8].Width = 40;

				tcl.Add(new TableHeaderCell());
				tcl[9].Text = "51岁及以上";
				tcl[9].Wrap = false;
				tcl[9].Width = 40;

                tcl.Add(new TableHeaderCell());
                tcl[10].Text = "未填";
                tcl[10].Wrap = false;
                tcl[10].Width = 40;

				int n = 11;
				DataTable dtEdu = GetEducationLevel();
				foreach (DataRow r in dtEdu.Rows)
				{
					tcl.Add(new TableHeaderCell());
					tcl[n].Text = r["EDUCATION_LEVEL_NAME"].ToString();
					//tcl[n].Wrap = false;
					tcl[n].Width = 40;
					n++;
				}

				DataTable dtTitle = GetTechnicianTitle();
				tcl.Add(new TableHeaderCell());
				tcl[n].Text ="小计";
				tcl[n].Wrap = false;
				tcl[n].Width = 40;
				n++;

                tcl.Add(new TableHeaderCell());
                tcl[n].Text = "未填";
                tcl[n].Wrap = false;
                tcl[n].Width = 40;
                n++;

				foreach (DataRow r in dtTitle.Rows)
				{
					tcl.Add(new TableHeaderCell());
					tcl[n].Text = r["type_name"].ToString();
					//tcl[n].Wrap = false;
					tcl[n].Width = 40;
					n++;
				}

				DataTable dtTech = GetTechnician();
				tcl.Add(new TableHeaderCell());
				tcl[n].Text = "小计";
				tcl[n].Wrap = false;
				tcl[n].Width = 40;
				n++;

                tcl.Add(new TableHeaderCell());
                tcl[n].Text = "未填";
                tcl[n].Wrap = false;
                tcl[n].Width = 40;
                n++;

				foreach (DataRow r in dtTech.Rows)
				{
					tcl.Add(new TableHeaderCell());
					tcl[n].Text = r["type_name"].ToString();
					//tcl[n].Wrap = false;
					tcl[n].Width = 40;
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

				e.Row.Cells[0].Wrap = false;
				e.Row.Cells[1].Wrap = false;  
			}
		}

		protected void GridInfo_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			GridInfo.PageIndex = e.NewPageIndex;
			GridInfo.DataSource = ViewState["dt"];
			GridInfo.DataBind();
			GroupRows(GridInfo, 0);
		 
		}

		protected void GridInfo_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			
		}

		private void GroupRows(GridView GridView1, int cellNum)
		{
			int i = 0, rowSpanNum = 1;
			while (i < GridView1.Rows.Count - 1)
			{
				GridViewRow gvr = GridView1.Rows[i];
				for (++i; i < GridView1.Rows.Count; i++)
				{
					GridViewRow gvrNext = GridView1.Rows[i];
					if (gvr.Cells[cellNum].Text == gvrNext.Cells[cellNum].Text)
					{
						gvrNext.Cells[cellNum].Visible = false;
						rowSpanNum++;
					}
					else
					{
						gvr.Cells[cellNum].RowSpan = rowSpanNum;
						rowSpanNum = 1;
						break;
					}
					if (i == GridView1.Rows.Count - 1)
					{
						gvr.Cells[cellNum].RowSpan = rowSpanNum;
					}
				}
			}
		}

		protected void btnExcel_Click(object sender, EventArgs e)
		{
			if (ViewState["dt"] != null && hfRefreshExcel.Value == "")
				Session["EmployeeWorkerByEducation"] = ViewState["dt"];

			if (hfRefreshExcel.Value == "true")
			{
				hfRefreshExcel.Value = "";
				DownloadExcel("各单位职教工作人员统计信息");

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
