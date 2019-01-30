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
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
	public partial class DeleteRandomExamApply : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				if (PrjPub.CurrentLoginUser == null)
				{
					Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
					return;
				}
                if (PrjPub.HasEditRight("登录信息") )
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }

                if (PrjPub.HasDeleteRight("登录信息") )
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }
				hfIsServer.Value = PrjPub.IsServerCenter.ToString();
				hfSuitRange.Value = PrjPub.CurrentLoginUser.SuitRange.ToString();
				BindGrid();
			}

			string strDel = Request.Form.Get("deleteid");
			if (strDel != "" && strDel != null)
			{
				SystemUserLoginBLL objbll = new SystemUserLoginBLL();
				objbll.DeleteSystemUserLogin(Convert.ToInt32(strDel));
                BindGrid();
			}
		}

		private void BindGrid()
		{
			SystemUserLoginBLL objbll = new SystemUserLoginBLL();
			if (PrjPub.IsServerCenter)
			{
				if (PrjPub.CurrentLoginUser.SuitRange == 1)
				{
					Grid1.DataSource = objbll.GetSystemUserLogin(0);
					Grid1.DataBind();
				}
				else
				{
					Grid1.DataSource = objbll.GetSystemUserLoginByOrgID(PrjPub.CurrentLoginUser.StationOrgID);
					Grid1.DataBind();
				}
			}
			else
			{
				Grid1.DataSource = objbll.GetSystemUserLogin(0);
				Grid1.DataBind();
			}
		}

		protected void gridCallback_callback(object sender, CallBackEventArgs e)
		{
			SystemUserLoginBLL objbll = new SystemUserLoginBLL();
			Grid1.DataSource = objbll.GetSystemUserLogin(0);
			Grid1.DataBind();
			Grid1.RenderControl(e.Output);
		}

		protected void btnClear_Click(object sender, EventArgs e)
		{
			SystemUserLoginBLL objbll = new SystemUserLoginBLL();
			objbll.ClearSystemUserLogin();
			BindGrid();
		}

		protected void btnDelete_Click(object sender, EventArgs e)
		{
			SystemUserLoginBLL objbll = new SystemUserLoginBLL();
			objbll.DeleteSystemUserLogin();
			BindGrid();
		}
	}
}
