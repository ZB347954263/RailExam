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
    public partial class TJ_EmployeeOther : PageBase
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
                sql.Append(" count(*) as 合计");

                string strSql = "select a.* from ZJ_CERTIFICATE_LEVEL a "
                                + "inner join ZJ_CERTIFICATE c on a.CERTIFICATE_ID=c.CERTIFICATE_ID "
                                + " order by c.CERTIFICATE_ID,a.ORDER_INDEX";
                DataTable dtLevel= access.RunSqlDataSet(strSql).Tables[0];

                foreach (DataRow dr in dtLevel.Rows)
                {
                    sql.Append(",sum(case   when CERTIFICATE_LEVEL_ID=" + dr["CERTIFICATE_LEVEL_ID"] + " then 1 else 0 end) as \"" + dr["CERTIFICATE_LEVEL_NAME"] + "\"");
                }

                sql.AppendFormat(" from ZJ_EMPLOYEE_CERTIFICATE c "
                    + " inner join Employee a on a.Employee_ID=c.Employee_ID"
                    + " inner join org b on getstationorgid(a.org_id)=b.org_id where a.ISREGISTERED=1 {0}", strwhere);
                sql.Append(" group by b.Short_Name,b.Order_Index   ");

                sql.Append(" union select '合计',0, ");
                sql.Append(" count(*) as 合计");
                foreach (DataRow dr in dtLevel.Rows)
                {
                    sql.Append(",sum(case   when CERTIFICATE_LEVEL_ID=" + dr["CERTIFICATE_LEVEL_ID"] + " then 1 else 0 end) as \"" + dr["CERTIFICATE_LEVEL_NAME"] + "\"");
                }

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
                sql.AppendFormat(" from ZJ_EMPLOYEE_CERTIFICATE c "
                    + " inner join Employee a on a.Employee_ID=c.Employee_ID"
                    + " inner join org b on getstationorgid(a.org_id)=b.org_id where a.ISREGISTERED=1 {0}", strwhere);
                sql.Append(" ) v order by v.order_index");
                DataTable dt = access.RunSqlDataSet(sql.ToString()).Tables[0];
                //dt.Columns.Remove("Order_Index");

                for (int i = 0; i < dt.Rows.Count; i++)
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
                tcl[0].RowSpan = 3;

                tcl.Add(new TableHeaderCell());
                tcl[1].Text = "单位";
                tcl[1].Wrap = false;
                tcl[1].RowSpan = 3;

                OracleAccess access = new OracleAccess();
                string strSql = "select a.* from ZJ_CERTIFICATE_LEVEL a "
                              + "inner join ZJ_CERTIFICATE c on a.CERTIFICATE_ID=c.CERTIFICATE_ID "
                              + " order by c.CERTIFICATE_ID,a.ORDER_INDEX";
                DataTable dtLevel = access.RunSqlDataSet(strSql).Tables[0];

                strSql = "select * from  ZJ_CERTIFICATE order by CERTIFICATE_ID";
                DataTable dt = access.RunSqlDataSet(strSql).Tables[0];

                tcl.Add(new TableHeaderCell());
                tcl[2].Text = "证书名称</th></tr>";
                tcl[2].Wrap = false;
                tcl[2].ColumnSpan = dtLevel.Rows.Count+1;

                tcl.Add(new TableHeaderCell());
                tcl[3].Text = "合计";
                tcl[3].Wrap = false;
                tcl[3].RowSpan = 2;

                for (int i=0; i<dt.Rows.Count; i++)
                {
                    tcl.Add(new TableHeaderCell());
                    if (i + 1 == dt.Rows.Count)
                    {
                        tcl[4 + i].Text = dt.Rows[i]["CERTIFICATE_NAME"] + "</th></tr>";
                    }
                    else
                    {
                        tcl[4 + i].Text = dt.Rows[i]["CERTIFICATE_NAME"].ToString();
                    }
                    tcl[4 + i].Wrap = false;

                    DataRow[] drs = dtLevel.Select("CERTIFICATE_ID=" + dt.Rows[i]["CERTIFICATE_ID"]);

                    tcl[4 + i].ColumnSpan = drs.Length;
                }

                int index = 3 + dt.Rows.Count + 1;

                for (int i = 0; i < dtLevel.Rows.Count; i++)
                {
                    tcl.Add(new TableHeaderCell());
                    if (i + 1 == dtLevel.Rows.Count)
                    {
                        tcl[index + i].Text = dtLevel.Rows[i]["CERTIFICATE_Level_NAME"] + "</th></tr>";
                    }
                    else
                    {
                        tcl[index + i].Text = dtLevel.Rows[i]["CERTIFICATE_Level_NAME"].ToString();
                    }
                    tcl[index + i].Wrap = false;
                }
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
                Session["EmployeeOther"] = ViewState["dt"];

            if (hfRefreshExcel.Value == "true")
            {
                hfRefreshExcel.Value = "";
                DownloadExcel("各单位技其他持证情况统计");
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
