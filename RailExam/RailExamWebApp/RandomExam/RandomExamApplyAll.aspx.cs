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
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
	public partial class RandomExamApplyAll : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack && !gridCallback.IsCallback)
			{
				BindGrid();
			}

			string strChoose = Request.Form.Get("ChooseID");
			if (strChoose != "" && strChoose != null)
			{
				RandomExamApplyBLL objBll = new RandomExamApplyBLL();
				string[] str = strChoose.ToString().Split(',');
				string strEmployeeID = "";
				for (int i = 0; i < str.Length; i++)
				{
					RandomExamApply obj = objBll.GetRandomExamApply(Convert.ToInt32(str[i]));
					if (("," + strEmployeeID + ",").IndexOf("," + obj.RandomExamID + "#" + obj.EmployeeID + ",") < 0)
					{
						if (strEmployeeID == "")
						{
							strEmployeeID = obj.RandomExamID + "#" + obj.EmployeeID.ToString();
						}
						else
						{
							strEmployeeID = strEmployeeID + "," + obj.RandomExamID + "#" + obj.EmployeeID;
						}
					}
					else
					{
						SessionSet.PageMessage = "不能同时通过同一考生的两次同一考试请求！";
						ClientScript.RegisterStartupScript(GetType(),
								"jsSelectFirstNode",
								@"getGrid('" + strChoose + "');",
								true);
						return;
					}
				}

				for (int i = 0; i < str.Length; i++)
				{
					objBll.UpdateRandomExamApplyStatus(Convert.ToInt32(str[i]), 1);
				}
				BindGrid();
			}

			string strChooseID = Request.Form.Get("ChooseOneID");
			if (strChooseID != "" && strChooseID != null)
			{
				string[] str = strChooseID.Split('|');
				string strApplyID = str[0];
				string strEmployeeID = str[1];
				RandomExamApplyBLL objBll = new RandomExamApplyBLL();
				RandomExamApply objApply = objBll.GetRandomExamApply(Convert.ToInt32(strApplyID));

				IList<RandomExamApply> objList = objBll.GetRandomExamApplyByOrgID(Convert.ToInt32(Request.QueryString.Get("OrgID")),PrjPub.ServerNo.ToString());
				foreach (RandomExamApply apply in objList)
				{
					if (apply.RandomExamApplyID.ToString() != strApplyID && apply.RandomExamID== objApply.RandomExamID && apply.EmployeeID.ToString() == strEmployeeID && apply.ApplyStatus == 1)
					{
						SessionSet.PageMessage = "已有该考生的考试请求获得通过！";
						ClientScript.RegisterStartupScript(GetType(),
							"jsSelectFirstNode",
							@"getGrid('" + strApplyID + "');",
							true);
						return;
					}
				}
				objBll.UpdateRandomExamApplyStatus(Convert.ToInt32(strApplyID), 1);
				BindGrid();
			}

			string strDel = Request.Form.Get("deleteid");
			if (strDel != "" && strDel != null)
			{
				RandomExamApplyBLL objBll = new RandomExamApplyBLL();
				objBll.DelRandomExamApply(Convert.ToInt32(strDel));
				BindGrid();
			}
		}

		private void BindGrid()
		{
			RandomExamApplyBLL objbll = new RandomExamApplyBLL();
			IList<RandomExamApply> objList = objbll.GetRandomExamApplyByOrgID(Convert.ToInt32(Request.QueryString.Get("OrgID")),PrjPub.ServerNo.ToString());
			Grid1.DataSource = objList;
			Grid1.DataBind();
		}


		protected void gridCallback_callback(object sender, CallBackEventArgs e)
		{
			RandomExamApplyBLL objbll = new RandomExamApplyBLL();
			Grid1.DataSource = objbll.GetRandomExamApplyByOrgID(Convert.ToInt32(Request.QueryString.Get("OrgID")),PrjPub.ServerNo.ToString());
			Grid1.DataBind();
			Grid1.RenderControl(e.Output);
		}
	}
}
