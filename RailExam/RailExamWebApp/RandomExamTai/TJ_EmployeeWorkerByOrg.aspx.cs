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
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamTai
{
    public partial class TJ_EmployeeWorkerByOrg : PageBase
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
            OracleAccess oracle = new OracleAccess();
            string strwhere = "where 1=1";
            if (hfOrgID.Value != "0")
            {
                strwhere += " and getstationorgid(e.org_id)=" + hfOrgID.Value;
            }
            else
            {
                int railSystemId = PrjPub.GetRailSystemId();
                if(railSystemId !=0)
                {
                    strwhere += " and (GetRailSystemId(e.org_id)=" + railSystemId + " or getstationorgid(e.org_id)="+PrjPub.CurrentLoginUser.StationOrgID+")";
                }
            }
            string sql = string.Format(@"select a.* 
                               from
                               (
                                 select rownum row_index,w.* from (
                                 select o.order_index, o.parent_id,
                                        to_char(o.full_name) as full_name,
                                        count(*) as amount,
                                        sum(case when e.isonpost=1 then 1 else 0 end) as is_on_post,
                                        sum(case when e.isonpost=0 then 1 else 0 end) as is_not_on_post,
                                        sum(case when e.ISREGISTERED=1 then 1 else 0 end) as ISREGISTERED,
                                        sum(case when e.ISREGISTERED=0 then 1 else 0 end) as IS_not_REGISTERED,
                                        sum(case when e.isonpost=1 and e.Employee_Type_ID=0 then 1 else 0 end) as Is_on_post_worker,
                                        sum(case when e.isonpost=1 and e.Employee_Type_ID=0 and a.photo is null  then 1 else 0 end) as Is_on_post_worker_no_photo,
                                        sum(case when e.isonpost=1 and e.Employee_Type_ID=0 and b.fingernum is null  then 1 else 0 end) as Is_on_post_worker_no_finger
                                 from employee e
                                 inner join org o 
                                       on getstationorgid(e.org_id)=o.org_id 
                                 left   join employee_photo a on a.Employee_id=e.Employee_ID
                                 left  join (select employee_id,count(*) fingernum from employee_fingerprint group by employee_id) b
                                        on e.Employee_ID=b.Employee_ID {0}
                                 group by o.full_name,o.order_index, o.parent_id order by o.parent_id,o.order_index) w
                                 union 
                                 select 0,0,0,
                                        '����',
                                        count(e.employee_id),
                                        sum(case when e.isonpost = 1 then 1 else 0 end),
                                        sum(case when e.isonpost = 0 then 1 else 0 end),
                                        sum(case when e.ISREGISTERED=1 then 1 else 0 end) as ISREGISTERED,
                                        sum(case when e.ISREGISTERED=0 then 1 else 0 end) as IS_not_REGISTERED,
                                        sum(case when e.isonpost=1 and e.Employee_Type_ID=0 then 1 else 0 end) as Is_on_post_worker,
                                        sum(case when e.isonpost=1 and e.Employee_Type_ID=0 and a.photo is null  then 1 else 0 end) as Is_on_post_worker_no_photo,
                                        sum(case when e.isonpost=1 and e.Employee_Type_ID=0 and b.fingernum is null  then 1 else 0 end) as Is_on_post_worker_no_finger
                                 from employee e 
                                 left   join employee_photo a on a.Employee_id=e.Employee_ID
                                 left  join (select employee_id,count(*) fingernum from employee_fingerprint group by employee_id) b
                                        on e.Employee_ID=b.Employee_ID {0}
                               ) a
                               order by a.row_index", strwhere);
            try
            {
                this.grid1.DataSource = oracle.RunSqlDataSet(sql);
                this.grid1.DataBind();
                ViewState["dt"] = grid1.DataSource;
            }
            catch { }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

		protected void btnExcel_Click(object sender, EventArgs e)
		{
			if (ViewState["dt"] != null && hfRefreshExcel.Value == "")
				Session["EmployeeWorkerByOrg"] = ViewState["dt"];

			if (hfRefreshExcel.Value == "true")
			{
				hfRefreshExcel.Value = "";
				DownloadExcel("����λ��������ͳ����Ϣ");

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
