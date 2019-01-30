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
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamOther
{
	public partial class InputRandomExamResultDetail : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				string strID = Request.QueryString.Get("RandomExamID");
				ViewState["ExamID"] = strID;
				ViewState["ExamOrgID"] = Request.QueryString.Get("OrgID");
				OrganizationBLL objOrgBll = new OrganizationBLL();
				ViewState["ExamOrgName"] = objOrgBll.GetOrganization(Convert.ToInt32(ViewState["ExamOrgID"])).ShortName;
				BindGrid();
			}
		}

		private void BindGrid()
		{
			RandomExamResultBLL objResultBll = new RandomExamResultBLL();
			IList<RandomExamResult> objResultList = objResultBll.GetRandomExamResultByExamID(Convert.ToInt32(ViewState["ExamID"]));
			Hashtable htResult = new Hashtable();
			foreach (RandomExamResult result in objResultList)
			{
				htResult.Add(result.ExamineeId,result.ExamineeName);
			}

			DataTable dt = new DataTable();
			dt.Columns.Add(new DataColumn("RandomExamResultID", typeof(int)));
			dt.Columns.Add(new DataColumn("ExamineeID", typeof(int)));
			dt.Columns.Add(new DataColumn("ExamineeName", typeof(string)));
			dt.Columns.Add(new DataColumn("WorkNo", typeof(string)));
			dt.Columns.Add(new DataColumn("PostName", typeof(string)));
			dt.Columns.Add(new DataColumn("OrganizationName", typeof(string)));
			dt.Columns.Add(new DataColumn("ExamOrgName", typeof(string)));
			dt.Columns.Add(new DataColumn("Score", typeof(string)));

			RandomExamArrangeBLL objArrangeBll = new RandomExamArrangeBLL();
			IList<RandomExamArrange> objArrangeList = objArrangeBll.GetRandomExamArranges(Convert.ToInt32(ViewState["ExamID"]));
			if (objArrangeList.Count == 0)
			{
				Grid1.DataSource = dt;
				Grid1.DataBind();
				return;
			}
			RandomExamArrange objArrange = objArrangeList[0];
			string[] str = objArrange.UserIds.Split(',');


			EmployeeBLL objEmployeeBll = new EmployeeBLL();
			for(int i = 0 ; i< str.Length; i++)
			{
				Employee objEmployee = objEmployeeBll.GetEmployee(Convert.ToInt32(str[i]));
				if(!htResult.ContainsKey(Convert.ToInt32(str[i])))
				{
					DataRow dr = dt.NewRow();
					dr["RandomExamResultID"] = 0;
					dr["ExamineeID"] = Convert.ToInt32(str[i]);
					dr["ExamineeName"] = objEmployee.EmployeeName;
					dr["WorkNo"] = objEmployee.WorkNo;
					dr["PostName"] = objEmployee.PostName;
					dr["OrganizationName"] = objEmployee.OrgName;
					dr["ExamOrgName"] = ViewState["ExamOrgName"].ToString();
					dr["Score"] = "";
					dt.Rows.Add(dr);
				}
				else
				{
					RandomExamResult objResult =
						objResultBll.GetRandomExamResultByExamineeID(Convert.ToInt32(str[i]), Convert.ToInt32(ViewState["ExamID"]))[0];
					DataRow dr = dt.NewRow();
					dr["RandomExamResultID"] = objResult.RandomExamResultId;
					dr["ExamineeID"] = Convert.ToInt32(str[i]);
					dr["ExamineeName"] = objResult.ExamineeName;
					dr["WorkNo"] = objResult.WorkNo;
					dr["PostName"] = objEmployee.PostName;
					dr["OrganizationName"] = objEmployee.OrgName;
					dr["ExamOrgName"] = ViewState["ExamOrgName"].ToString();
					dr["Score"] = objResult.Score;
					dt.Rows.Add(dr);
				}
			}

			Grid1.DataSource = dt;
			Grid1.DataBind();
		}

		protected void Grid1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			Grid1.EditIndex = -1;
			BindGrid();
		}

		protected void Grid1_RowEditing(object sender, GridViewEditEventArgs e)
		{
			if (Grid1.EditIndex != -1)
			{
				SessionSet.PageMessage = "请先保存正在编辑的项！";
				return;
			}

			Grid1.EditIndex = e.NewEditIndex;
			BindGrid();
		}

		protected void Grid1_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			string resultID = ((HiddenField)Grid1.Rows[e.RowIndex].FindControl("hfID")).Value;
			string employeeID = ((HiddenField)Grid1.Rows[e.RowIndex].FindControl("hfExamineeID")).Value;
			string score = ((TextBox)Grid1.Rows[e.RowIndex].FindControl("txtScore")).Text;

			RandomExamResultBLL objResultBll = new RandomExamResultBLL();
			if(resultID == "0")
			{
				RandomExamResult objResult = new RandomExamResult();
				objResult.OrganizationId = Convert.ToInt32(ViewState["ExamOrgID"]);
				objResult.RandomExamId = Convert.ToInt32(ViewState["ExamID"]);
				objResult.ExamineeId = Convert.ToInt32(employeeID);
				objResult.BeginDateTime = DateTime.Now;
				objResult.EndDateTime = DateTime.Now;
				objResult.CurrentDateTime = DateTime.Now;
				objResult.ExamTime = 0;
				objResult.AutoScore = 0;
				objResult.Score = Convert.ToDecimal(score);
				objResult.CorrectRate = Convert.ToDecimal(score);
				objResult.IsPass = Convert.ToDecimal(score) >= 60 ? true : false;
				objResult.StatusId = 2;
				objResultBll.AddRandomExamResult(objResult);
			}
			else
			{
				RandomExamResult objResult = objResultBll.GetRandomExamResult(Convert.ToInt32(resultID));
				objResult.Score = Convert.ToDecimal(score);
				objResult.CorrectRate = Convert.ToDecimal(score);
				objResult.IsPass = Convert.ToDecimal(score) >= 60 ? true : false;
				objResultBll.UpdateRandomExamResultOther(objResult);
			}

			Grid1.EditIndex = -1;
			BindGrid();
		}

		protected void Grid1_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			string strID = Grid1.DataKeys[e.RowIndex].Values[0].ToString();
			if (strID == "0")
			{
				SessionSet.PageMessage = "删除成功！";
			}
			else
			{
				RandomExamResultBLL objResultBll = new RandomExamResultBLL();
				objResultBll.DeleteRandomExamResult(Convert.ToInt32(strID));
				SessionSet.PageMessage = "删除成功！";
			}
			BindGrid();
		}

		protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			Grid1.PageIndex = e.NewPageIndex;
			BindGrid();
		}

		protected void btnEnd_Click(object sender, EventArgs e)
		{
			RandomExamBLL objBll = new RandomExamBLL();
            objBll.UpdateIsStart(Convert.ToInt32(ViewState["ExamID"].ToString()), PrjPub.ServerNo, 2);
            objBll.UpdateIsUpload(Convert.ToInt32(ViewState["ExamID"].ToString()), PrjPub.ServerNo, 1);
		}
	}
}
