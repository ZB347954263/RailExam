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

namespace RailExamWebApp.RandomExamTai
{
	public partial class ComputerApplyTab_two : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (!IsPostBack)
				{
					if (PrjPub.CurrentLoginUser == null)
					{
						Response.Redirect("../Common/Error.aspx?error=Session过期请重新登录本系统！");
						return;
					}
					else
					{
						int orgID = PrjPub.CurrentLoginUser.StationOrgID;
						grdEntity2Bind(orgID);

                        if (PrjPub.HasEditRight("微机教室预订") && PrjPub.IsServerCenter)
                        {
                            HfUpdateRight.Value = "True";
                        }
                        else
                        {
                            HfUpdateRight.Value = "False";
                        }

                        if (PrjPub.HasDeleteRight("微机教室预订") && PrjPub.IsServerCenter)
                        {
                            HfDeleteRight.Value = "True";
                        }
                        else
                        {
                            HfDeleteRight.Value = "False";
                        }
					}
				}
			}

            string strRefresh = Request.Form.Get("Refresh");
            if (!string.IsNullOrEmpty(strRefresh))
            {
                int orgID = PrjPub.CurrentLoginUser.StationOrgID;
                grdEntity2Bind(orgID);
            }
		}

		private void grdEntity2Bind(int ORGID)
		{
			DataSet ds = new DataSet();
			OracleAccess OrA = new OracleAccess();
			if (ORGID < 0)
			{
				this.grdEntity2 = null;
				grdEntity2.DataBind();
			}
			else
			{
                string str = "";
                int railSystemId = PrjPub.GetRailSystemId();

                if (railSystemId != 0)
                {
                    str = " or APPLY_ORG_ID in (select Org_ID from Org where Rail_System_ID=" + railSystemId +
                          " and level_num=2)";
                }

				ds = OrA.RunSqlDataSet(string.Format("select * from computer_room_apply_two_view where APPLY_ORG_ID={0}"+str, ORGID));
				grdEntity2.DataSource = ds.Tables.Count > 0 ? ds.Tables[0] : null;
				grdEntity2.DataBind();
			}
		}

		protected void grdEntity2_RowCommand(object sender, GridViewCommandEventArgs e)
		{

		}

		protected void grdEntity2_RowCreated(object sender, GridViewRowEventArgs e)
		{

		}
		private void IntoApply_Click()
		{
			if (PrjPub.CurrentLoginUser == null)
			{
				Response.Redirect("../Common/Error.aspx?error=Session过期请重新登录本系统！");
				return;
			}
			else
			{
				int orgID = PrjPub.CurrentLoginUser.OrgID;
				grdEntity2Bind(orgID);
			}
		}
		protected void btnDelete_Click(object sender, EventArgs e)
		{
			string argument = Request.Form["__EVENTARGUMENT"];
			OracleAccess ora = new OracleAccess();
			try
			{
				ora.ExecuteNonQuery("delete COMPUTER_ROOM_APPLY where COMPUTER_ROOM_APPLY_ID=" + argument);
			}
			catch (Exception ex)
			{
				throw ex;
			}
			OxMessageBox.MsgBox3("删除成功！");
			IntoApply_Click();
		}
	}
}
