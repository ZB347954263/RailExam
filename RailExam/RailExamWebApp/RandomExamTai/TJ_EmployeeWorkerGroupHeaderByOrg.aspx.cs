using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RailExamWebApp.Common.Class;
using System.Text;
using DSunSoft.Web.UI;

namespace RailExamWebApp
{
    public partial class TJ_EmployeeWorkerGroupHeaderByOrg : PageBase
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

                DataTable dtGroup = GetGroupHeader();

                OracleAccess access = new OracleAccess();
                StringBuilder sql = new StringBuilder();
                sql.Append("select v.* from (select to_char(b.short_name),b.Order_Index,");
                sql.Append(" sum(case   when WORKGROUPLEADER_TYPE_ID>0 then 1 else 0 end) as ���鳤����");
                foreach (DataRow r in dtGroup.Rows)
                {
                    sql.AppendFormat(", sum(case WORKGROUPLEADER_TYPE_ID when {0} then 1 else 0  end) as {1}",
                                     r["WORKGROUPLEADER_LEVEL_ID"],
                                     r["LEVEL_NAME"]);

                }
				sql.AppendFormat(" from employee a  inner join org b on getstationorgid(a.org_id)=b.org_id where a.ISREGISTERED=1 {0}", strwhere);
                sql.Append(" group by b.Short_Name,b.Order_Index   ");

                sql.Append(" union select '�ϼ�',0, sum(case   when  WORKGROUPLEADER_TYPE_ID>0 then 1 else 0 end) as ���鳤���� ");
                foreach (DataRow r in dtGroup.Rows)
                {
                    sql.AppendFormat(", sum(case WORKGROUPLEADER_TYPE_ID when {0} then 1 else 0  end) as {1}",
                                     r["WORKGROUPLEADER_LEVEL_ID"],
                                     r["LEVEL_NAME"]);

                }

                strwhere = "";
                if (hfOrgID.Value != "0")
                {
                    strwhere += " and getstationorgid(employee.org_id)=" + hfOrgID.Value;
                }
                else
                {
                    int railSystemId = PrjPub.GetRailSystemId();
                    if (railSystemId != 0)
                    {
                        strwhere += " and (GetRailSystemId(employee.org_id)=" + railSystemId + " or getstationorgid(employee.org_id)=" + PrjPub.CurrentLoginUser.StationOrgID + ")";
                    }
                }
				sql.AppendFormat(" from employee where employee.ISREGISTERED=1 {0}) v order by v.order_index", strwhere);
                DataTable dt = access.RunSqlDataSet(sql.ToString()).Tables[0];
                dt.Columns.Remove("Order_Index");
                return dt;
            }
            catch
            {
                return null;
            }
        }

        private DataTable GetGroupHeader()
        {
            OracleAccess access = new OracleAccess();
            return access.RunSqlDataSet(" select * from workgroupleader_level order by order_index").Tables[0];
        }


        protected void Grid_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                DataTable dtGroup = GetGroupHeader();
                TableCellCollection tcl = e.Row.Cells;
                tcl.Clear();
                tcl.Add(new TableHeaderCell());
                tcl[0].Text = "��λ";
                tcl[0].Wrap = false;
                tcl[0].RowSpan = 2;

                tcl.Add(new TableHeaderCell());
                tcl[1].Text = "���鳤����";
                tcl[1].Wrap = false;
                tcl[1].RowSpan = 2;

                tcl.Add(new TableHeaderCell());
                tcl[2].Text = "����</th></tr>";
                tcl[2].Wrap = false;
                tcl[2].ColumnSpan = dtGroup.Rows.Count;

                int n = 3;
                foreach (DataRow r in dtGroup.Rows)
                {
                    tcl.Add(new TableHeaderCell());
                    tcl[n].Text = r["LEVEL_NAME"].ToString();
                    tcl[n].Wrap = false;
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
                    string param = String.Format("'{0}', {1}, {2}", Server.UrlEncode(e.Row.Cells[0].Text), i, e.Row.Cells[i].Text);
                    e.Row.Cells[i].Attributes.Add("ondblclick", "ShowDetail(" + param + ")");
                }
            }
        }

		protected void btnExcel_Click(object sender, EventArgs e)
		{
			if (ViewState["dt"] != null && hfRefreshExcel.Value == "")
				Session["EmployeeWorkerGroupHeaderByOrg"] = ViewState["dt"];

			if (hfRefreshExcel.Value == "true")
			{
				hfRefreshExcel.Value = "";
				DownloadExcel("����λ���鳤����ͳ����Ϣ");

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
				// ���ͷ��Ϣ��Ϊ"�ļ�����/���Ϊ"�Ի���ָ��Ĭ���ļ���
				this.Response.AddHeader("Content-Disposition",
										"attachment; filename=" + HttpUtility.UrlEncode(strName) + ".xls");
				// ���ͷ��Ϣ��ָ���ļ���С����������ܹ���ʾ���ؽ���

				this.Response.AddHeader("Content-Length", file.Length.ToString());

				// ָ�����ص���һ�����ܱ��ͻ��˶�ȡ���������뱻����

				this.Response.ContentType = "application/ms-excel";

				// ���ļ������͵��ͻ���

				this.Response.WriteFile(file.FullName);
			}
		}
    }
}
