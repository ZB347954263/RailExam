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
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamTai
{
	public partial class EmployeeCertificate : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string type = Request.QueryString.Get("Type");
			if (type == "0" || !PrjPub.IsServerCenter)
			{
				int columnsCount = this.grdEntity.Levels[0].Columns.Count;
				this.grdEntity.Levels[0].Columns[columnsCount - 1].Visible = false;
			}

			hfIsServerCenter.Value = PrjPub.IsServerCenter.ToString();

			if(!IsPostBack)
			{
				hfID.Value = Request.QueryString.Get("ID");
				LoadInfo();

			}
		}

		protected void btnPost_Click(object sender, EventArgs e)
		{
			if (hfAction.Value == "del")
			{
				DeleteInfo();
				hfAction.Value = "";
			}
			LoadInfo();
		}
 

		private void LoadInfo()
		{
		  OracleAccess access=new OracleAccess();
		  string sql =string.Format(@" select a.employee_certificate_id,  getorgname(h.org_id) unitName,b.employee_name,b.sex,b.birthday,b.identity_cardno,
					 c.education_level_name ,i.post_name  postName,d.certificate_name,e.certificate_level_name,a.certificate_date,
					g.train_unit_name,f.certificate_unit_name,a.certificate_no,
					a.check_date,a.check_unit,a.check_result,a.begin_date,a.end_date ,a.memo,a.check_cycle
					   from zj_employee_certificate a 
					 left join employee b on a.employee_id=b.employee_id
					 left join education_level c on b.education_level_id=c.education_level_id
					 left join zj_certificate d on d.certificate_id=a.certificate_id
					 left join zj_certificate_level e on a.certificate_level_id=e.certificate_level_id
					 left join zj_certificate_unit f on f.certificate_unit_id=a.certificate_unit_id
                     left join zj_train_unit g on g.train_unit_id=a.train_unit_id 
                     left join employee h on h.employee_id=a.employee_id 
                    left join post i on i.post_id=h.post_id
                   where a.employee_id={0} ", Request.QueryString.Get("ID"));
		     DataTable dt = access.RunSqlDataSet(sql).Tables[0];
			 
			grdEntity.DataSource = dt;

			grdEntity.DataBind();
		}

		private void DeleteInfo()
		{
			int id = hfDelID.Value == "" ? 0 : Convert.ToInt32(hfDelID.Value);
			OracleAccess access=new OracleAccess();
			string sql = string.Format(" delete from zj_employee_certificate where employee_certificate_id={0}",
			                           id);
			try
			{
				access.ExecuteNonQuery(sql);
			}
			catch (Exception)
			{
			}
			
		}
	}
}
