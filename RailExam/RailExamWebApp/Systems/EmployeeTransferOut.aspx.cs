using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
	public partial class EmployeeTransferOut : PageBase
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

                if (PrjPub.HasEditRight("职员调出") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }

                if (PrjPub.HasDeleteRight("职员调出") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }

				BindGrid();
			}

			if(Request.Form.Get("Refresh") != null && Request.Form.Get("Refresh") == "true")
			{
				BindGrid();
			}

			if (Request.Form.Get("DeleteID") != null && Request.Form.Get("DeleteID") != "")
			{
				EmployeeTransferBLL objBll = new EmployeeTransferBLL();
				objBll.DeleteEmployeeTransfer(Convert.ToInt32(Request.Form.Get("DeleteID")));
				BindGrid();
			}
		}

		private void BindGrid()
		{
			EmployeeTransferBLL objBll = new EmployeeTransferBLL();
			IList<EmployeeTransfer> objList = objBll.GetEmployeeTransferOutByOrgID(PrjPub.CurrentLoginUser.StationOrgID);

			grdEntity.DataSource = objList;
			grdEntity.DataBind();
		}
	}
}
