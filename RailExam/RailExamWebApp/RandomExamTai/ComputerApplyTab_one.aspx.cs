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
	public partial class ComputerApplyTab_one : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (PrjPub.CurrentLoginUser == null)
				{
					Response.Redirect("../Common/Error.aspx?error=Session���������µ�¼��ϵͳ��");
					return;
				}
				else
				{
                    if (PrjPub.HasEditRight("΢������Ԥ��") && PrjPub.IsServerCenter)
                    {
                        HfUpdateRight.Value = "True";
                    }
                    else
                    {
                        HfUpdateRight.Value = "False";
                    }

                    if (PrjPub.HasDeleteRight("΢������Ԥ��") && PrjPub.IsServerCenter)
                    {
                        HfDeleteRight.Value = "True";
                    }
                    else
                    {
                        HfDeleteRight.Value = "False";
                    }

					int orgID = PrjPub.CurrentLoginUser.StationOrgID;
					grdEntity1Bind(orgID);
				}
			}

		    string strRefresh = Request.Form.Get("Refresh");
            if(!string.IsNullOrEmpty(strRefresh))
            {
                int orgID = PrjPub.CurrentLoginUser.StationOrgID;
                grdEntity1Bind(orgID);
            }
		}
		protected void grdEntity_RowCreated(object sender, GridViewRowEventArgs e)
		{ }


		private void grdEntity1Bind(int ORGID)
		{
			DataSet ds = new DataSet();
			OracleAccess OrA = new OracleAccess();
			if (ORGID < 0)
			{
				this.grdEntity1 = null;
				grdEntity1.DataBind();
			}
			else
			{
                string str = "";
                int railSystemId = PrjPub.GetRailSystemId();

                if (railSystemId != 0)
                {
                    str = " or Org_ID in (select Org_ID from Org where Rail_System_ID=" + railSystemId +
                          " and level_num=2)";
                }

				ds = OrA.RunSqlDataSet(string.Format("select * from computer_room_apply_one_view where ORG_ID={0} "+str, ORGID));
				grdEntity1.DataSource = ds.Tables.Count > 0 ? ds.Tables[0] : null;
				grdEntity1.DataBind();
			}
		}
		private void OutApply_Click(object sender, EventArgs e)
		{
			if (null != PrjPub.CurrentLoginUser)
			{
				int orgid = PrjPub.CurrentLoginUser.OrgID;
				grdEntity1Bind(orgid);
				//grdEntity2Bind(orgid);
			}
		}

		protected void grdEntity_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			
			OracleAccess ora = new OracleAccess();

			string Id = e.CommandArgument.ToString();
			if (e.CommandName == "del")
			{
				try
				{
					int roomID = 0;
					int.TryParse(Id, out roomID);
					ora.ExecuteNonQuery("delete COMPUTER_ROOM_APPLY where COMPUTER_ROOM_APPLY_ID=" + roomID);
				}
				catch
				{
					ClientScript.RegisterStartupScript(GetType(), "Error", "alert('��΢����������ʹ�ã�����ɾ����')", true);
					return;
				}
				ClientScript.RegisterStartupScript(GetType(), "OK", "alert('ɾ���ɹ���')", true);
				if (PrjPub.CurrentLoginUser == null)
				{
					Response.Redirect("../Common/Error.aspx?error=Session���������µ�¼��ϵͳ��");
					return;
				}
				else
				{
					int orgID = PrjPub.CurrentLoginUser.OrgID;
					grdEntity1Bind(orgID);
				}
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
			OxMessageBox.MsgBox3("ɾ���ɹ���");
			int orgID = PrjPub.CurrentLoginUser.StationOrgID;
			grdEntity1Bind(orgID);
		}
	}
}
