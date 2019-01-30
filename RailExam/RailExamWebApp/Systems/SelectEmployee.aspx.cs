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
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
	public partial class SelectEmployee : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
            {

				if (PrjPub.IsWuhan())
				{
					Grid1.Columns[3].HeaderText = "‘±π§±‡¬Î";
				}
				else
				{
					Grid1.Columns[3].HeaderText = "π§◊ ±‡∫≈";
				}

				string strId = Request.QueryString.Get("employeeID");
				if (strId != null && strId != "")
				{
					ViewState["ChooseId"] = strId;
					ViewState["UpdateMode"] = 1;
				}
				else
				{
					ViewState["ChooseId"] = "";
					ViewState["UpdateMode"] = 0;
				}

				BindStationStart();
				BindOrgStart();
				BindWorkShopStart();
				BindSystemStart();
				BindTypeStart();
				BindPostStart();

                EmployeeBLL bll = new EmployeeBLL();
                RailExam.Model.Employee employee = bll.GetEmployee(Convert.ToInt32(Request.QueryString.Get("nowEmployeeID")));
                OrganizationBLL orgbll =new OrganizationBLL();
                int stationOrgID = orgbll.GetStationOrgID(employee.OrgID);
                if (stationOrgID != 200)
				{
                    ddlStation.SelectedValue = stationOrgID.ToString();
					OrganizationBLL objBll = new OrganizationBLL();
					IList<RailExam.Model.Organization> objList =
						objBll.GetOrganizationsByParentID(Convert.ToInt32(ddlStation.SelectedValue));
					foreach (RailExam.Model.Organization organization in objList)
					{
						ListItem item = new ListItem();
						item.Value = organization.OrganizationId.ToString();
						item.Text = organization.ShortName;
						ddlWorkShop.Items.Add(item);
					}

				    ddlStation.Enabled = false;
				}

				ViewState["StartRow"] = 0;
				ViewState["EndRow"] = Grid1.PageSize;

				ViewState["EmploySortField"] = "nlssort(a.employee_name,'NLS_SORT=SCHINESE_PINYIN_M')";
				BindGrid(ViewState["EmploySortField"].ToString());
			}
		}

		private void BindStationStart()
		{
			ddlStation.Items.Clear();
			OrganizationBLL organizationBLL = new OrganizationBLL();

			ListItem i = new ListItem();
			i.Text = "--«Î—°‘Ò--";
			i.Value = "0";
			ddlStation.Items.Add(i);

			IList<RailExam.Model.Organization> organizationList = organizationBLL.GetOrganizationsByLevel(2);
			foreach (RailExam.Model.Organization organization in organizationList)
			{
				if (organization.OrganizationId != 1)
				{
					ListItem item = new ListItem();
					item.Value = organization.OrganizationId.ToString();
					item.Text = organization.ShortName;
					ddlStation.Items.Add(item);
				}
			}
		}

		private void BindWorkShopStart()
		{
			ddlWorkShop.Items.Clear();
			ListItem i = new ListItem();
			i.Text = "--«Î—°‘Ò--";
			i.Value = "0";
			ddlWorkShop.Items.Add(i);
		}


		private void BindOrgStart()
		{
			ddlOrg.Items.Clear();
			ListItem i = new ListItem();
			i.Text = "--«Î—°‘Ò--";
			i.Value = "0";
			ddlOrg.Items.Add(i);
		}

		private void BindSystemStart()
		{
			ddlSystem.Items.Clear();
			PostBLL postBll = new PostBLL();
			ListItem i = new ListItem();
			i.Text = "--«Î—°‘Ò--";
			i.Value = "0";
			ddlSystem.Items.Add(i);

			IList<RailExam.Model.Post> objList = postBll.GetPostsByLevel(1);
			foreach (RailExam.Model.Post post in objList)
			{
				ListItem item = new ListItem();
				item.Value = post.PostId.ToString();
				item.Text = post.PostName;
				ddlSystem.Items.Add(item);
			}
		}

		private void BindTypeStart()
		{
			ddlType.Items.Clear();
			ListItem i = new ListItem();
			i.Text = "--«Î—°‘Ò--";
			i.Value = "0";
			ddlType.Items.Add(i);
		}

		private void BindPostStart()
		{
			ddlPost.Items.Clear();
			ListItem i = new ListItem();
			i.Text = "--«Î—°‘Ò--";
			i.Value = "0";
			ddlPost.Items.Add(i);
		}

		private void BindGrid(string strOrderBy)
		{
			int startRow = int.Parse(ViewState["StartRow"].ToString());
			int endRow = int.Parse(ViewState["EndRow"].ToString());
			int nItemcount = 0;
			EmployeeBLL psBLL = new EmployeeBLL();


			if (ddlOrg.SelectedValue != "0")
			{
				ViewState["OrgPath"] = ddlOrg.SelectedValue;
			}
			else
			{
				if (ddlWorkShop.SelectedValue != "0")
				{
					ViewState["OrgPath"] = ddlWorkShop.SelectedValue;
				}
				else
				{
					ViewState["OrgPath"] = ddlStation.SelectedValue;
				}
			}

			if (ddlPost.SelectedValue != "0")
			{
				ViewState["PostPath"] = ddlPost.SelectedValue;
			}
			else
			{
				if (ddlType.SelectedValue != "0")
				{
					ViewState["PostPath"] = ddlType.SelectedValue;
				}
				else
				{
					ViewState["PostPath"] = ddlSystem.SelectedValue;
				}
			}

			if (ddlGroupLeader.SelectedValue != "-1")
			{
				ViewState["GroupLeader"] = ddlGroupLeader.SelectedValue;
			}
			else
			{
				ViewState["GroupLeader"] = "-1";
			}

			ViewState["WorkNo"] = "";// txtWorkNo.Text;
			ViewState["EmployeeName"] = txtEmployeeName.Text;
			ViewState["PinyinCode"] = txtPinyinCode.Text;

			if (ddlStation.SelectedValue == "0" && ddlSystem.SelectedValue == "0" && ddlGroupLeader.SelectedValue == "-1" && txtEmployeeName.Text == "" && txtPinyinCode.Text == "")
			{
				BindEmptyGrid1();
			}
			else
			{
				IList<RailExam.Model.Employee> Employees = psBLL.GetEmployeesSelectByTransfer(ViewState["OrgPath"].ToString(), ViewState["PostPath"].ToString(), null, txtEmployeeName.Text, txtPinyinCode.Text, strOrderBy, ViewState["GroupLeader"].ToString(), startRow, endRow, ref nItemcount);
				if (Employees.Count > 0)
				{
					Grid1.VirtualItemCount = nItemcount;
					Grid1.DataSource = Employees;
					Grid1.DataBind();
					ViewState["EmptyFlag"] = 0;
				}
				else
				{
					BindEmptyGrid1();
				}
			}
		}

		private void BindEmptyGrid1()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add(new DataColumn("EmployeeID", typeof(int)));
			dt.Columns.Add(new DataColumn("OrgName", typeof(string)));
			dt.Columns.Add(new DataColumn("WorkNo", typeof(string)));
			dt.Columns.Add(new DataColumn("EmployeeName", typeof(string)));
			dt.Columns.Add(new DataColumn("PostName", typeof(string)));
			dt.Columns.Add(new DataColumn("Sex", typeof(string)));
			dt.Columns.Add(new DataColumn("IsGroupLeader", typeof(bool)));

			DataRow dr = dt.NewRow();

			dr["EmployeeID"] = 0;
			dr["OrgName"] = "";
			dr["WorkNo"] = "";
			dr["EmployeeName"] = "";
			dr["PostName"] = "";
			dr["Sex"] = "";
			dr["IsGroupLeader"] = false;
			dt.Rows.Add(dr);

			Grid1.VirtualItemCount = 1;
			Grid1.DataSource = dt;
			Grid1.DataBind();

			CheckBox CheckBox1 = (CheckBox)this.Grid1.Items[0].FindControl("chkSelect");
			CheckBox1.Visible = false;
			ViewState["EmptyFlag"] = 1;
		}

		protected void Grid1_PageIndexChanging(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			string strAllId = ViewState["ChooseId"].ToString();

			for (int i = 0; i < this.Grid1.Items.Count; i++)
			{
				CheckBox CheckBox1 = (CheckBox)this.Grid1.Items[i].FindControl("chkSelect");

				string strEmId = ((Label)this.Grid1.Items[i].FindControl("LabelEmployeeID")).Text;
				if (CheckBox1.Checked)
				{
					string strOldAllId = "," + strAllId + ",";
					if (strOldAllId.IndexOf("," + strEmId + ",") == -1)
					{
						if (strAllId.Length == 0)
						{
							strAllId += strEmId;
						}
						else
						{
							strAllId += "," + strEmId;
						}
					}
				}
			}

			ViewState["ChooseId"] = strAllId;

			this.Grid1.CurrentPageIndex = e.NewPageIndex;
			ViewState["StartRow"] = Grid1.PageSize * e.NewPageIndex;
			ViewState["EndRow"] = Grid1.PageSize * (e.NewPageIndex + 1);

			BindGrid(ViewState["EmploySortField"].ToString());
		}


		protected void Grid1_Sorting(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			if (ViewState["EmploySortField"] != null && ViewState["EmploySortField"].ToString() == e.SortExpression)
			{
				ViewState["EmploySortField"] = "nlssort(" + e.SortExpression + ",'NLS_SORT=SCHINESE_PINYIN_M')  desc";
			}
			else
			{
				ViewState["EmploySortField"] = "nlssort(" + e.SortExpression + ",'NLS_SORT=SCHINESE_PINYIN_M')";
			}

			BindGrid(ViewState["EmploySortField"].ToString());
		}

		protected void btnQuery_Click(object sender, ImageClickEventArgs e)
		{
			ViewState["EmploySortField"] = "nlssort(a.employee_name,'NLS_SORT=SCHINESE_PINYIN_M')";
			ViewState["StartRow"] = 0;
			ViewState["EndRow"] = Grid1.PageSize;
			this.Grid1.CurrentPageIndex = 0;
			BindGrid(ViewState["EmploySortField"].ToString());
		}

		protected void ddlStation_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ddlStation.SelectedValue == "0")
			{
				BindWorkShopStart();
				BindOrgStart();
			}
			else
			{
				BindWorkShopStart();
				OrganizationBLL objBll = new OrganizationBLL();
				IList<RailExam.Model.Organization> objList =
					objBll.GetOrganizationsByParentID(Convert.ToInt32(ddlStation.SelectedValue));
				foreach (RailExam.Model.Organization organization in objList)
				{
					ListItem item = new ListItem();
					item.Value = organization.OrganizationId.ToString();
					item.Text = organization.ShortName;
					ddlWorkShop.Items.Add(item);
				}
			}
		}

		protected void ddlWorkShop_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ddlWorkShop.SelectedValue == "0")
			{
				BindOrgStart();
			}
			else
			{
				BindOrgStart();
				OrganizationBLL objBll = new OrganizationBLL();
				IList<RailExam.Model.Organization> objList =
					objBll.GetOrganizationsByParentID(Convert.ToInt32(ddlWorkShop.SelectedValue));
				foreach (RailExam.Model.Organization organization in objList)
				{
					ListItem item = new ListItem();
					item.Value = organization.OrganizationId.ToString();
					item.Text = organization.ShortName;
					ddlOrg.Items.Add(item);
				}
			}
		}

		protected void ddlSystem_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ddlSystem.SelectedValue == "0")
			{
				BindTypeStart();
				BindPostStart();
			}
			else
			{
				BindTypeStart();
				PostBLL objBll = new PostBLL();
				IList<RailExam.Model.Post> objList =
					objBll.GetPostsByParentID(Convert.ToInt32(ddlSystem.SelectedValue));
				foreach (RailExam.Model.Post post in objList)
				{
					ListItem item = new ListItem();
					item.Value = post.PostId.ToString();
					item.Text = post.PostName;
					ddlType.Items.Add(item);
				}
			}
		}

		protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ddlType.SelectedValue == "0")
			{
				BindPostStart();
			}
			else
			{
				BindPostStart();
				PostBLL objBll = new PostBLL();
				IList<RailExam.Model.Post> objList =
					objBll.GetPostsByParentID(Convert.ToInt32(ddlType.SelectedValue));
				foreach (RailExam.Model.Post post in objList)
				{
					ListItem item = new ListItem();
					item.Value = post.PostId.ToString();
					item.Text = post.PostName;
					ddlPost.Items.Add(item);
				}
			}
		}


		protected void btnInput_Click(object sender, EventArgs e)
		{
			if (ViewState["EmptyFlag"].ToString() != null && ViewState["EmptyFlag"].ToString() == "1")
			{
				return;
			}

			string strAllId = ViewState["ChooseId"].ToString();

			for (int i = 0; i < this.Grid1.Items.Count; i++)
			{
				CheckBox CheckBox1 = (CheckBox)this.Grid1.Items[i].FindControl("chkSelect");

				string strEmId = ((Label)this.Grid1.Items[i].FindControl("LabelEmployeeID")).Text;
				if (CheckBox1.Checked)
				{
					string strOldAllId = "," + strAllId + ",";
					if (strOldAllId.IndexOf("," + strEmId + ",") == -1)
					{
						if (strAllId.Length == 0)
						{
							strAllId += strEmId;
						}
						else
						{
							strAllId += "," + strEmId;
						}
					}
				}
			}

			ViewState["ChooseId"] = strAllId;
			Response.Write("<script>window.opener.document.getElementById('hfEmployeeID').value='" + ViewState["ChooseId"].ToString() + "';window.opener.form1.submit();window.close();</script>");
		}
	}
}
