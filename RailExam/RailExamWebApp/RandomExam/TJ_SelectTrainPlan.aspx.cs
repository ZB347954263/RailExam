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
	public partial class SelectTrainPlan :PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (PrjPub.CurrentLoginUser == null)
			{
				Response.Redirect("../Common/Error.aspx?error=Session过期请重新登录本系统！");
				return;
			}
			if (!IsPostBack) 
			{
				hidSql.Value = GetPlanInfo();

				hidPlanID.Value = Request.QueryString.Get("planID");
				hidPlanName.Value = Request.QueryString.Get("planName");
			}
		}
		private string GetPlanInfo()
		{

            string str = "";
            int railSystemId = PrjPub.GetRailSystemId();

            if (railSystemId != 0)
            {
                str = " or SPONSOR_UNIT_ID in (select Org_ID from Org where Rail_System_ID=" + railSystemId +
                      " and level_num=2)  or undertake_unit_id in (select Org_ID from Org where Rail_System_ID=" + railSystemId +
                      " and level_num=2) ";

                string strSql = "select Org_ID from Org where Rail_System_ID=" + railSystemId +
                                " and level_num=2";
                OracleAccess db = new OracleAccess();
                DataSet ds = db.RunSqlDataSet(strSql);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    str += "or  ','|| orgids||',' like '%," + dr["Org_ID"] + ",%'";
                }
            }

			OracleAccess access = new OracleAccess();
            string sql = string.Format("select * from zj_train_plan_view where SPONSOR_UNIT_ID={0} or UNDERTAKE_UNIT_ID={0} or  ','|| orgids||',' like '%,{0},%' {1}", PrjPub.CurrentLoginUser.StationOrgID,str);
			return sql;
		}

		protected void grdEntity_RowCreated(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				RadioButton radio = e.Row.FindControl("radioID") as RadioButton;
				if (radio != null)
				{
					radio.Attributes.Add("onclick", "selectValue(this)");
				}
			}
		}

		protected void grdEntity_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			
		}

		protected void grdEntity_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			 
		}
	}
}
