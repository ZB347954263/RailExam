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
                sql.Append(" count(*) as �ϼ�1,");
                sql.Append(" sum(case   when Unit='ȫ��' then 1 else 0 end) as ȫ��,");
                sql.Append(" sum(case   when Unit='������' then 1 else 0 end) as ������,");
                sql.Append(" sum(case   when Unit='��·��' then 1 else 0 end) as ��·��,");
                sql.Append(" sum(case   when Unit='վ��' then 1 else 0 end) as վ��,");
                sql.Append(" count(*) as �ϼ�2,");
                sql.Append(" sum(case   when MATCH_TYPE='����' then 1 else 0 end) as ����,");
                sql.Append(" sum(case   when MATCH_TYPE='ȫ��' then 1 else 0 end) as ȫ��,");
                sql.Append(" sum(case   when MATCH_TYPE='����' then 1 else 0 end) as ����,");
                sql.Append(" sum(case   when MATCH_TYPE='ȫ��' then 1 else 0 end) as ����");
                sql.AppendFormat(" from zj_employee_match c "
                    +" inner join Employee a on a.Employee_ID=c.Employee_ID"
                    +" inner join org b on getstationorgid(a.org_id)=b.org_id where a.ISREGISTERED=1 {0}", strwhere);
                sql.Append(" group by b.Short_Name,b.Order_Index   ");

                sql.Append(" union select '�ϼ�',0, ");
                sql.Append(" count(*) as �ϼ�1,");
                sql.Append(" sum(case   when Unit='ȫ��' then 1 else 0 end) as ȫ��,");
                sql.Append(" sum(case   when Unit='������' then 1 else 0 end) as ������,");
                sql.Append(" sum(case   when Unit='��·��' then 1 else 0 end) as ��·��,");
                sql.Append(" sum(case   when Unit='վ��' then 1 else 0 end) as վ��,");
                sql.Append(" count(*) as �ϼ�2,");
                sql.Append(" sum(case   when MATCH_TYPE='����' then 1 else 0 end) as ����,");
                sql.Append(" sum(case   when MATCH_TYPE='ȫ��' then 1 else 0 end) as ȫ��,");
                sql.Append(" sum(case   when MATCH_TYPE='����' then 1 else 0 end) as ����,");
                sql.Append(" sum(case   when MATCH_TYPE='ȫ��' then 1 else 0 end) as ����");
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
                tcl[0].Text = "���";
                tcl[0].Wrap = false;
                tcl[0].RowSpan = 2;

                tcl.Add(new TableHeaderCell());
                tcl[1].Text = "��λ";
                tcl[1].Wrap = false;
                tcl[1].RowSpan = 2;

                tcl.Add(new TableHeaderCell());
                tcl[2].Text = "�ٰ쵥λ";
                tcl[2].Wrap = false;
                tcl[2].ColumnSpan = 5;

                tcl.Add(new TableHeaderCell());
                tcl[3].Text = "�������</th></tr>";
                tcl[3].Wrap = false;
                tcl[3].ColumnSpan = 5;


                tcl.Add(new TableHeaderCell());
                tcl[4].Text = "�ϼ�";
                tcl[4].Wrap = false;

                tcl.Add(new TableHeaderCell());
                tcl[5].Text = "ȫ��";
                tcl[5].Wrap = false;

                tcl.Add(new TableHeaderCell());
                tcl[6].Text = "������";
                tcl[6].Wrap = false;

                tcl.Add(new TableHeaderCell());
                tcl[7].Text = "������";
                tcl[7].Wrap = false;

                tcl.Add(new TableHeaderCell());
                tcl[8].Text = "վ��";
                tcl[8].Wrap = false;

                tcl.Add(new TableHeaderCell());
                tcl[9].Text = "�ϼ�";
                tcl[9].Wrap = false;

                tcl.Add(new TableHeaderCell());
                tcl[10].Text = "����";
                tcl[10].Wrap = false;

                tcl.Add(new TableHeaderCell());
                tcl[11].Text = "ȫ��";
                tcl[11].Wrap = false;

                tcl.Add(new TableHeaderCell());
                tcl[12].Text = "����";
                tcl[12].Wrap = false;

                tcl.Add(new TableHeaderCell());
                tcl[13].Text = "����";
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
                DownloadExcel("����λ���ܾ���ͳ��");

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
