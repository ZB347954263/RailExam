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
    public partial class TJ_EmployeePrize : PageBase
    {
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
            Grid.DataSource = GetInfo();
            Grid.DataBind();
            ViewState["dt"] = Grid.DataSource;
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
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

                OracleAccess access = new OracleAccess();
                StringBuilder sql = new StringBuilder();
                sql.Append("select v.* from (select to_char(b.short_name),b.Order_Index,");
                sql.Append(" count(*) as 合计1,");
                sql.Append(" sum(case   when Unit='全国' then 1 else 0 end) as 全国,");
                sql.Append(" sum(case   when Unit='铁道部' then 1 else 0 end) as 铁道部,");
                sql.Append(" sum(case   when Unit='铁路局' then 1 else 0 end) as 铁路局,");
                sql.Append(" sum(case   when Unit='站段' then 1 else 0 end) as 站段,");
                sql.Append(" count(*) as 合计2,");
                sql.Append(" sum(case   when MATCH_TYPE='单项' then 1 else 0 end) as 单项,");
                sql.Append(" sum(case   when MATCH_TYPE='全能' then 1 else 0 end) as 全能,");
                sql.Append(" sum(case   when MATCH_TYPE='团体' then 1 else 0 end) as 团体,");
                sql.Append(" sum(case   when MATCH_TYPE='全国' then 1 else 0 end) as 其他");
                sql.AppendFormat(" from zj_employee_match c "
                    +" inner join Employee a on a.Employee_ID=c.Employee_ID"
                    +" inner join org b on getstationorgid(a.org_id)=b.org_id where a.ISREGISTERED=1 {0}", strwhere);
                sql.Append(" group by b.Short_Name,b.Order_Index   ");

                sql.Append(" union select '合计',0, ");
                sql.Append(" count(*) as 合计1,");
                sql.Append(" sum(case   when Unit='全国' then 1 else 0 end) as 全国,");
                sql.Append(" sum(case   when Unit='铁道部' then 1 else 0 end) as 铁道部,");
                sql.Append(" sum(case   when Unit='铁路局' then 1 else 0 end) as 铁路局,");
                sql.Append(" sum(case   when Unit='站段' then 1 else 0 end) as 站段,");
                sql.Append(" count(*) as 合计2,");
                sql.Append(" sum(case   when MATCH_TYPE='单项' then 1 else 0 end) as 单项,");
                sql.Append(" sum(case   when MATCH_TYPE='全能' then 1 else 0 end) as 全能,");
                sql.Append(" sum(case   when MATCH_TYPE='团体' then 1 else 0 end) as 团体,");
                sql.Append(" sum(case   when MATCH_TYPE='全国' then 1 else 0 end) as 其他");
                strwhere = "";
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
                sql.AppendFormat(" from zj_employee_match c "
                    + " inner join Employee a on a.Employee_ID=c.Employee_ID"
                    + " inner join org b on getstationorgid(a.org_id)=b.org_id where a.ISREGISTERED=1 {0}", strwhere);
                sql.Append(" ) v order by v.order_index");
                DataTable dt = access.RunSqlDataSet(sql.ToString()).Tables[0];
                //dt.Columns.Remove("Order_Index");

                for (int i = 0; i < dt.Rows.Count;i++ )
                {
                    dt.Rows[i]["Order_Index"] = i;
                }

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
                tcl[0].Text = "序号";
                tcl[0].Wrap = false;
                tcl[0].RowSpan = 2;

                tcl.Add(new TableHeaderCell());
                tcl[1].Text = "单位";
                tcl[1].Wrap = false;
                tcl[1].RowSpan = 2;

                tcl.Add(new TableHeaderCell());
                tcl[2].Text = "举办单位";
                tcl[2].Wrap = false;
                tcl[2].ColumnSpan = 5;

                tcl.Add(new TableHeaderCell());
                tcl[3].Text = "竞赛类别</th></tr>";
                tcl[3].Wrap = false;
                tcl[3].ColumnSpan = 5;


                tcl.Add(new TableHeaderCell());
                tcl[4].Text = "合计";
                tcl[4].Wrap = false;

                tcl.Add(new TableHeaderCell());
                tcl[5].Text = "全国";
                tcl[5].Wrap = false;

                tcl.Add(new TableHeaderCell());
                tcl[6].Text = "铁道部";
                tcl[6].Wrap = false;

                tcl.Add(new TableHeaderCell());
                tcl[7].Text = "铁道局";
                tcl[7].Wrap = false;

                tcl.Add(new TableHeaderCell());
                tcl[8].Text = "站段";
                tcl[8].Wrap = false;

                tcl.Add(new TableHeaderCell());
                tcl[9].Text = "合计";
                tcl[9].Wrap = false;

                tcl.Add(new TableHeaderCell());
                tcl[10].Text = "单项";
                tcl[10].Wrap = false;

                tcl.Add(new TableHeaderCell());
                tcl[11].Text = "全能";
                tcl[11].Wrap = false;

                tcl.Add(new TableHeaderCell());
                tcl[12].Text = "团体";
                tcl[12].Wrap = false;

                tcl.Add(new TableHeaderCell());
                tcl[13].Text = "其他";
                tcl[13].Wrap = false;

            }


            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Attributes.Add("class", "HeadingRow");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onclick", "selectCol(this)");
            }
        }

        protected void Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid.PageIndex = e.NewPageIndex;
            Grid.DataSource = ViewState["dt"];
            Grid.DataBind();
        }

        protected void Grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 1; i < e.Row.Cells.Count; i++)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //string param = String.Format("'{0}', {1}, {2}", Server.UrlEncode(e.Row.Cells[0].Text), i, e.Row.Cells[i].Text);
                    //e.Row.Cells[i].Attributes.Add("ondblclick", "ShowDetail(" + param + ")");
                }
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            if (ViewState["dt"] != null && hfRefreshExcel.Value == "")
                Session["EmployeePrize"] = ViewState["dt"];

            if (hfRefreshExcel.Value == "true")
            {
                hfRefreshExcel.Value = "";
                DownloadExcel("各单位技能竞赛统计");

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
