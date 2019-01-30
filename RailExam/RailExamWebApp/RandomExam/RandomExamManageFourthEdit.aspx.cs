using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.OracleClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using System.Collections.Generic;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class RandomExamManageFourthEdit : PageBase
    {
		int RecordCount, CurrentPage, PageCount, JumpPage; 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonInputAll.Attributes.Add("onclick", "return confirm('加入上面列表显示的全部考生可能需要较长时间，您确认吗 ？');");

				if (!string.IsNullOrEmpty(Request.QueryString.Get("selectType")))
				{
					ButtonInputAll.Visible = false;
				}

            	string strId = Request.QueryString.Get("id");
            	ViewState["ExamID"] = strId;
                if (strId != null && strId != "")
                {
                    RandomExamArrangeBLL eaBll = new RandomExamArrangeBLL();
                    IList<RailExam.Model.RandomExamArrange> ExamArranges = eaBll.GetRandomExamArranges(int.Parse(strId));

                    if (ExamArranges.Count > 0)
                    {
                        ViewState["ChooseId"] = ExamArranges[0].UserIds;
                        ViewState["UpdateMode"] = 1;
                    }
                    else
                    {
                        ViewState["ChooseId"] = "";
                        ViewState["UpdateMode"] = 0;
                    }
                }

                BindStationStart();
                BindOrgStart();
                BindWorkShopStart();
                BindSystemStart();
                BindTypeStart();
                BindPostStart();

                ListItem item = new ListItem();
                item.Text = "--请选择--";
                item.Value = "-1";
                ddlSafe.Items.Add(item);

                OracleAccess db =new OracleAccess();
                string strSql = "select * from ZJ_Safe_Level order by Order_Index";
                DataSet ds = db.RunSqlDataSet(strSql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    item = new ListItem();
                    item.Text = dr["Safe_Level_Name"].ToString();
                    item.Value = dr["Safe_Level_ID"].ToString();
                    ddlSafe.Items.Add(item);
                }

                if (PrjPub.CurrentLoginUser.SuitRange != 1)
                {
                    ddlStation.SelectedValue = PrjPub.CurrentLoginUser.StationOrgID.ToString();
                    OrganizationBLL objBll = new OrganizationBLL();
                    IList<Organization> objList =
                        objBll.GetOrganizationsByParentID(Convert.ToInt32(ddlStation.SelectedValue));
                    foreach (Organization organization in objList)
                    {
                        item = new ListItem();
                        item.Value = organization.OrganizationId.ToString();
                        item.Text = organization.ShortName;
                        ddlWorkShop.Items.Add(item);
                    }
                }

                ViewState["StartRow"] = 0;
                ViewState["EndRow"] = Grid1.PageSize;

            	ViewState["EmploySortField"] = "nlssort(a.employee_name,'NLS_SORT=SCHINESE_PINYIN_M')";

				RecordCount = GetCount();   
				//计算总页数（加上OverPage()函数防止有余数造成显示数据不完整）   
				PageCount = RecordCount / Grid1.PageSize + OverPage();
				ViewState["RecordCount"] = RecordCount;
				//保存总页参数到ViewState（减去ModPage()函数防止SQL语句执行时溢出查询范围，可以用存储过程分页算法来理解这句）   
				ViewState["PageCounts"] = RecordCount / Grid1.PageSize - ModPage();
				//保存一个为0的页面索引值到ViewState   
				ViewState["PageIndex"] = 0;
				//保存PageCount到ViewState，跳页时判断用户输入数是否超出页码范围   
				ViewState["JumpPages"] = PageCount;
				//显示LPageCount、LRecordCount的状态   
				this.lbPageCount.Text = PageCount.ToString();
				this.lbRecordCount.Text = RecordCount.ToString();   
				//判断跳页文本框失效   
				if (RecordCount <= Grid1.PageSize)
				{
					btnJumpPage.Enabled = false;
				}
				else
				{
					btnJumpPage.Enabled = true;
				}

                BindGrid(ViewState["EmploySortField"].ToString());
            }
        }

        private void BindStationStart()
        {
            ddlStation.Items.Clear();
            OrganizationBLL organizationBLL = new OrganizationBLL();

            ListItem i = new ListItem();
            i.Text = "--请选择--";
            i.Value = "0";
            ddlStation.Items.Add(i);

            IList<RailExam.Model.Organization> organizationList = organizationBLL.GetOrganizationsByLevel(2);
            foreach (Organization organization in organizationList)
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
            i.Text = "--请选择--";
            i.Value = "0";
            ddlWorkShop.Items.Add(i);
        }


        private void BindOrgStart()
        {
            ddlOrg.Items.Clear();
            ListItem i = new ListItem();
            i.Text = "--请选择--";
            i.Value = "0";
            ddlOrg.Items.Add(i);
        }

        private void BindSystemStart()
        {
            ddlSystem.Items.Clear();
            PostBLL postBll = new PostBLL();
            ListItem i = new ListItem();
            i.Text = "--请选择--";
            i.Value = "0";
            ddlSystem.Items.Add(i);

            IList<RailExam.Model.Post> objList = postBll.GetPostsByLevel(1);
            foreach (Post post in objList)
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
            i.Text = "--请选择--";
            i.Value = "0";
            ddlType.Items.Add(i);
        }

        private void BindPostStart()
        {
            ddlPost.Items.Clear();
            ListItem i = new ListItem();
            i.Text = "--请选择--";
            i.Value = "0";
            ddlPost.Items.Add(i);
        }

		//查找已生成试卷的人员信息
		private void HasExamId()
		{
			string strExamId = ViewState["ExamID"].ToString();

			if (!string.IsNullOrEmpty(Request.QueryString.Get("selectType")))
			{
                //RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
                //IList<RandomExamResultCurrent> examResults = objResultCurrentBll.GetRandomExamResultInfo(Convert.ToInt32(strExamId));
                //string strId = "";
                //for (int i = 0; i < examResults.Count; i++)
                //{
                //    string strEmId = examResults[i].ExamineeId.ToString();

                //    if (("," + strId + ",").IndexOf("," + strEmId + ",") < 0)
                //    {
                //        if (strId.Length == 0)
                //        {
                //            strId += strEmId;
                //        }
                //        else
                //        {
                //            strId += "," + strEmId;
                //        }
                //    }
                //}
                //ViewState["HasExamId"] = strId; 

                string strSql = "select * from Random_Exam_Arrange_Detail where Random_Exam_ID=" + strExamId;
                OracleAccess db = new OracleAccess();
			    DataSet ds = db.RunSqlDataSet(strSql);

			    string strId = "";
			    foreach (DataRow dr in ds.Tables[0].Rows)
			    {
			        if(strId=="")
			        {
			            strId = dr["User_Ids"].ToString();
			        }
			        else
			        {
                        strId += "," + dr["User_Ids"];
			        }
			    }

                ViewState["HasExamId"] = strId;
			}
			else
			{
				RandomExamResultBLL reBll = new RandomExamResultBLL();
				IList<RailExam.Model.RandomExamResult> examResults = reBll.GetRandomExamResultByExamID(int.Parse(strExamId));
				string strId = "";
				for (int i = 0; i < examResults.Count; i++)
				{
					string strEmId = examResults[i].ExamineeId.ToString();

					if (strId.Length == 0)
					{
						strId += strEmId;
					}
					else
					{
						strId += "," + strEmId;
					}
				}
				ViewState["HasExamId"] = strId; ;
			}
		}

		private void GetWhereClause()
		{
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
		}

		private int GetCount()
		{
			EmployeeBLL psBLL = new EmployeeBLL();
			int nItemcount = 0;
			int startRow = int.Parse(ViewState["StartRow"].ToString());
			int endRow = int.MaxValue;

			GetWhereClause();
			if (ddlStation.SelectedValue == "0" && ddlSystem.SelectedValue == "0" && ddlGroupLeader.SelectedValue == "-1" && txtEmployeeName.Text == "" && txtPinyinCode.Text == "")
			{
				return 0;
			}
			else
			{
				IList<RailExam.Model.Employee> Employees = psBLL.GetEmployeesSelect(ViewState["OrgPath"].ToString(),
																					ViewState["PostPath"].ToString(), null,
																					txtEmployeeName.Text, txtPinyinCode.Text,
																					"a.Employee_Name", ViewState["GroupLeader"].ToString(),
                                                                                    Convert.ToInt32(ddlSafe.SelectedValue),
																					startRow, endRow, ref nItemcount);

				return Employees.Count;
			}
		}

        private void BindGrid(string strOrderBy)
        {
        	HasExamId();

			EmployeeBLL psBLL = new EmployeeBLL();
			int nItemcount = 0;
			int startRow = int.Parse(ViewState["StartRow"].ToString());
			int endRow = int.Parse(ViewState["EndRow"].ToString());

        	GetWhereClause();

			//从ViewState中读取页码值保存到CurrentPage变量中进行按钮失效运算   
			CurrentPage = (int)ViewState["PageIndex"];
			//从ViewState中读取总页参数进行按钮失效运算   
			PageCount = (int)ViewState["PageCounts"];
			//判断四个按钮（首页、上页、下页、尾页）状态   
			if (CurrentPage + 1 > 1)
			{
				Fistpage.Enabled = true;
				Prevpage.Enabled = true;
			}
			else
			{
				Fistpage.Enabled = false;
				Prevpage.Enabled = false;
			}
			if (CurrentPage == PageCount)
			{
				Nextpage.Enabled = false;
				Lastpage.Enabled = false;
			}
			else
			{
				Nextpage.Enabled = true;
				Lastpage.Enabled = true;
			}


			if (ddlStation.SelectedValue == "0" && ddlSystem.SelectedValue == "0" && ddlGroupLeader.SelectedValue == "-1" && txtEmployeeName.Text == "" && txtPinyinCode.Text == "")
            {
                BindEmptyGrid1();
				txtJumpPage.Text = (CurrentPage + 1).ToString();
            	RecordCount = 0;
            }
            else
            {
				IList<RailExam.Model.Employee> Employees = psBLL.GetEmployeesSelect(ViewState["OrgPath"].ToString(), ViewState["PostPath"].ToString(), null, txtEmployeeName.Text, txtPinyinCode.Text, strOrderBy, ViewState["GroupLeader"].ToString(), Convert.ToInt32(ddlSafe.SelectedValue),startRow, endRow, ref nItemcount);
                if (Employees.Count > 0)
                {
                    Grid1.VirtualItemCount = nItemcount;
                    Grid1.DataSource = Employees;
                    Grid1.DataBind();
                    ViewState["EmptyFlag"] = 0;

                	RecordCount = Employees.Count;

					//显示文本框控件txtJumpPage状态   
					txtJumpPage.Text = (CurrentPage + 1).ToString();
                }
                else
                {
                    BindEmptyGrid1();
					txtJumpPage.Text = (CurrentPage + 1).ToString();
					RecordCount = 0;
                }
            }
        }

        private void BindEmptyGrid1()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmployeeID", typeof(int)));
            dt.Columns.Add(new DataColumn("OrgName", typeof(string)));
            dt.Columns.Add(new DataColumn("StrWorkNo", typeof(string)));
            dt.Columns.Add(new DataColumn("EmployeeName", typeof(string)));
            dt.Columns.Add(new DataColumn("PostName", typeof(string)));
            dt.Columns.Add(new DataColumn("Sex", typeof (string)));
			dt.Columns.Add(new DataColumn("IsGroupLeader", typeof(bool)));

            DataRow dr = dt.NewRow();

            dr["EmployeeID"] = 0;
            dr["OrgName"] = "";
            dr["StrWorkNo"] = "";
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

			RecordCount = (int)ViewState["RecordCount"];
			PageCount = RecordCount / Grid1.PageSize + OverPage();
			ViewState["PageCounts"] = RecordCount / Grid1.PageSize - ModPage();
			ViewState["JumpPages"] = PageCount;
			this.lbPageCount.Text = PageCount.ToString();
			if (RecordCount <= Grid1.PageSize)
			{
				btnJumpPage.Enabled = false;
			}
			else
			{
				btnJumpPage.Enabled = true;
			} 

            BindGrid(ViewState["EmploySortField"].ToString());
        }

		//计算余页   
		public int OverPage()
		{
			int pages = 0;
			if (RecordCount % Grid1.PageSize != 0)
				pages = 1;
			else
				pages = 0;
			return pages;
		}
		//计算余页，防止SQL语句执行时溢出查询范围   
		public int ModPage()
		{
			int pages = 0;
			if (RecordCount % Grid1.PageSize == 0 && RecordCount != 0)
				pages = 1;
			else
				pages = 0;
			return pages;
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

			RecordCount = GetCount();
			//计算总页数（加上OverPage()函数防止有余数造成显示数据不完整）   
			PageCount = RecordCount / Grid1.PageSize + OverPage();
			ViewState["RecordCount"] = RecordCount;
			//保存总页参数到ViewState（减去ModPage()函数防止SQL语句执行时溢出查询范围，可以用存储过程分页算法来理解这句）   
			ViewState["PageCounts"] = RecordCount / Grid1.PageSize - ModPage();
			//保存一个为0的页面索引值到ViewState   
			ViewState["PageIndex"] = 0;
			//保存PageCount到ViewState，跳页时判断用户输入数是否超出页码范围   
			ViewState["JumpPages"] = PageCount;
			//显示LPageCount、LRecordCount的状态   
			this.lbPageCount.Text = PageCount.ToString();
			this.lbRecordCount.Text = RecordCount.ToString();
			//判断跳页文本框失效   
			if (RecordCount <= Grid1.PageSize)
			{
				btnJumpPage.Enabled = false;
			}
			else
			{
				btnJumpPage.Enabled = true;
			}  

            BindGrid(ViewState["EmploySortField"].ToString());
        }

        protected void ButtonInputAll_Click(object sender, EventArgs e)
        {
            if (ViewState["EmptyFlag"].ToString() != null && ViewState["EmptyFlag"].ToString() == "1")
            {
                return;
            }

            string strAllId = ViewState["ChooseId"].ToString();
            IList<RailExam.Model.Employee> Employees = null;
            EmployeeBLL psBLL = new EmployeeBLL();

			Employees = psBLL.GetEmployeesSelect(ViewState["OrgPath"].ToString(), ViewState["PostPath"].ToString(),null, ViewState["EmployeeName"].ToString(), ViewState["PinyinCode"].ToString(), "a.employee_Name", ViewState["GroupLeader"].ToString());

            for (int i = 0; i < Employees.Count; i++)
            {
                string strEmId = Employees[i].EmployeeID.ToString();
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

            ViewState["ChooseId"] = strAllId;

            SaveChoose();
            //Response.Write("<script>window.opener.form1.ChooseID.value='" + ViewState["ChooseId"].ToString() + "';window.opener.form1.submit();window.close();</script>");
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
                else
                {
                    string strOldAllId = "," + strAllId + ",";
                    if (strOldAllId.IndexOf("," + strEmId + ",") >= 0)
                    {
                        strOldAllId = strOldAllId.Replace("," + strEmId + ",", ",");
                    }

                    strAllId = strOldAllId.TrimStart(',').TrimEnd(',');
                }
            }

            ViewState["ChooseId"] = strAllId;
            SaveChoose();

            if (string.IsNullOrEmpty(Request.QueryString.Get("selectType")))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                       " window.opener.form1.Refresh.value = 'true',window.opener.form1.submit();",
                                                       true);
            }

            //Response.Write("<script>window.opener.form1.ChooseID.value='" + ViewState["ChooseId"].ToString() + "';window.opener.form1.submit();window.close();</script>");
        }

        protected void ddlStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlStation.SelectedValue == "0")
            {
                BindWorkShopStart();
                BindOrgStart();
            }
            else
            {
               BindWorkShopStart();
                OrganizationBLL objBll = new OrganizationBLL();
                IList<Organization> objList =
                    objBll.GetOrganizationsByParentID(Convert.ToInt32(ddlStation.SelectedValue));
                foreach (Organization organization in objList)
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
                IList<Organization> objList =
                    objBll.GetOrganizationsByParentID(Convert.ToInt32(ddlWorkShop.SelectedValue));
                foreach (Organization organization in objList)
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
                IList<Post> objList =
                    objBll.GetPostsByParentID(Convert.ToInt32(ddlSystem.SelectedValue));
                foreach (Post post in objList)
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
                IList<Post> objList =
                    objBll.GetPostsByParentID(Convert.ToInt32(ddlType.SelectedValue));
                foreach (Post post in objList)
                {
                    ListItem item = new ListItem();
                    item.Value = post.PostId.ToString();
                    item.Text = post.PostName;
                    ddlPost.Items.Add(item);
                }
            }
        }

        protected void SaveChoose()
        {
            string strId = Request.QueryString.Get("id");

            string strEndId = ViewState["ChooseId"].ToString();


            if (strEndId == "")
            {
                strEndId = "0";
            }
            else
            {
                RandomExamBLL objBll = new RandomExamBLL();
                RailExam.Model.RandomExam random = objBll.GetExam(int.Parse(strId));

                if(random.RandomExamModularTypeID > 1)
                {
                    OracleAccess db = new OracleAccess();
                    string strSql = "select * from Random_Exam_Modular_Type where Random_Exam_Modular_Type_ID=" +
                                    random.RandomExamModularTypeID;
                    DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

                    strSql = "select * from Random_Exam_Modular_Type where Level_Num=" + (Convert.ToInt32(dr["Level_NUM"]) - 1);
                     DataRow drPre = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

                    strSql = " select a.* "
                             + "from Random_Exam_Result a "
                             + " inner join Random_Exam b on a.Random_Exam_ID=b.Random_Exam_ID "
                             + " left join Random_Exam_Modular_Type c on b.Random_Exam_Modular_Type_ID=c.Random_Exam_Modular_Type_ID "
                             + " where c.Level_Num=" + (Convert.ToInt32(dr["Level_NUM"]) - 1);
                    DataSet ds = db.RunSqlDataSet(strSql);

                    strSql = "select * from Employee where Employee_ID in (" + strEndId + ")";
                    DataSet dsEmp = db.RunSqlDataSet(strSql);

                    string[] str = strEndId.Split(',');

                    string strErrorMessage = string.Empty;
                    for (int i = 0; i < str.Length; i++ )
                    {
                        DataRow[] drs = ds.Tables[0].Select("EXAMINEE_ID=" + str[i]);

                        if(drs.Length==0)
                        {
                            DataRow[] drsEmp = dsEmp.Tables[0].Select("Employee_ID=" + str[i]);

                            strErrorMessage = "所选考生【" + drsEmp[0]["Employee_Name"] + "】未能通过【" +
                                              drPre["Random_Exam_Modular_Type_Name"] + "】考试，不能参加【" +
                                              dr["Random_Exam_Modular_Type_Name"] + "】考试！";
                            break;
                        }
                    }

                    if(!string.IsNullOrEmpty(strErrorMessage))
                    {
                        SessionSet.PageMessage = strErrorMessage;
                        return;
                    }
                }
            }

            if (ViewState["UpdateMode"] != null && ViewState["UpdateMode"].ToString() == "0")
            {
                RandomExamArrangeBLL examArrangeBLL = new RandomExamArrangeBLL();
                IList<RandomExamArrange> objList = examArrangeBLL.GetRandomExamArranges(int.Parse(strId));

                if(objList.Count == 0)
                {
                    RandomExamArrange examArrange = new RandomExamArrange();
                    examArrange.RandomExamId = int.Parse(strId);
                    examArrange.UserIds = strEndId;
                    examArrange.Memo = "";
                    if (!string.IsNullOrEmpty(Request.QueryString.Get("selectType")) && !PrjPub.IsServerCenter)
                    {
                        examArrangeBLL.AddRandomExamArrangeToServer(examArrange);
                    }
                    else
                    {
                        examArrangeBLL.AddRandomExamArrange(examArrange);
                    }
                    ViewState["UpdateMode"] = 1;

                    if (!string.IsNullOrEmpty(Request.QueryString.Get("selectType")))
                    {
                        Response.Write("<script>alert('添加成功!');window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");
                    }
                    else
                    {
                        string strSql = "";
                        OracleAccess db = new OracleAccess();
                        strSql = "update Random_Exam set Is_All_Arrange = 0 where Random_Exam_ID=" + strId;
                        db.ExecuteNonQuery(strSql);

                        SessionSet.PageMessage = "添加成功！";
                        return;
                    }
                }
                else
                {
                    ViewState["UpdateMode"] ="1";
                }
            }

            if (ViewState["UpdateMode"] != null && ViewState["UpdateMode"].ToString() == "1")
            {
                RandomExamArrangeBLL examArrangeBLL = new RandomExamArrangeBLL();
                IList<RandomExamArrange> objList = examArrangeBLL.GetRandomExamArranges(int.Parse(strId));

                string strNew = objList[0].UserIds;

                string[] str = strEndId.Split(',');

                for (int i = 0; i < str.Length; i++ )
                {
                    if((","+strNew+",").IndexOf(","+str[i]+",")<0)
                    {
                        strNew += strNew == string.Empty ? str[i] : "," + str[i];
                    }
                }

                if (!string.IsNullOrEmpty(Request.QueryString.Get("selectType")))
                {
                    //examArrangeBLL.UpdateRandomExamArrangeToServer(int.Parse(strId), strAdd);
                    string strAddIds = string.Empty;

                    for (int i = 0; i < str.Length; i++)
                    {
                        if (("," + objList[0].UserIds + ",").IndexOf("," + str[i] + ",") < 0)
                        {
                            strAddIds += strAddIds == string.Empty ? str[i] : "," + str[i];
                        }
                    }

                    OracleAccess db = new OracleAccess();

                    OracleAccess dbCenter = new OracleAccess(System.Configuration.ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString);
                    XmlDocument doc = new XmlDocument();
                    //Request.PhysicalApplicationPath取得config文件路径
                    doc.Load(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config"));
                    XmlNode node = doc.SelectSingleNode("configuration/dataConfiguration/@defaultDatabase");
                    string value = node.Value;
                    if (value == "Oracle")
                    {
                        OracleParameter para1 = new OracleParameter("p_User_Ids", OracleType.Clob);
                        OracleParameter para2 = new OracleParameter("p_random_Exam_ID", OracleType.Number);
                        para2.Value = int.Parse(strId);
                        IDataParameter[] paras = new IDataParameter[] { para1, para2 };
                        examArrangeBLL.RunUpdateProcedure(true, "USP_random_Exam_Arrange_U", paras, System.Text.Encoding.Unicode.GetBytes(strEndId));
                    }

                    string  strSql = "select a.* from Random_Exam_Arrange_Detail a "
                              + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
                              + " inner join Computer_Server c on c.Computer_server_ID=b.Computer_Server_ID"
                              + " where c.Computer_Server_No='" + PrjPub.ServerNo + "' "
                              + " and Random_Exam_ID=" + strId;
                    DataSet ds = db.RunSqlDataSet(strSql);

                    strSql = "update Random_Exam_Arrange_Detail "
                                    + "set User_ids = User_ids || '," + strAddIds + "' "
                                    + "where   Random_Exam_Arrange_Detail_ID =" + ds.Tables[0].Rows[0]["Random_Exam_Arrange_Detail_ID"];
                    dbCenter.ExecuteNonQuery(strSql);

                    if(!PrjPub.IsServerCenter)
                    {
                        examArrangeBLL.RefreshRandomExamArrange();
                    }
                }
                else
                {
                    examArrangeBLL.UpdateRandomExamArrange(int.Parse(strId), strNew);
                }

                if (!string.IsNullOrEmpty(Request.QueryString.Get("selectType")))
                {
                    //SessionSet.PageMessage = "添加成功！";
                    Response.Write("<script>alert('添加成功!');window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");
                }
                else
                {
                    string strSql = "";
                    OracleAccess db = new OracleAccess();
                    strSql = "update Random_Exam set Is_All_Arrange = 0 where Random_Exam_ID=" + strId;
                    db.ExecuteNonQuery(strSql);

                    SessionSet.PageMessage = "添加成功！";
                    return;
                }
            }
        }

		protected void btnJumpPage_Click(object sender, EventArgs e)
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

			JumpPage = (int)ViewState["JumpPages"];
			//判断用户输入值是否超过可用页数范围值   
			if (Int32.Parse(this.txtJumpPage.Text.Trim()) > JumpPage || Int32.Parse(this.txtJumpPage.Text.Trim()) <= 0)
			{
				CurrentPage = (int)ViewState["PageIndex"];
				this.txtJumpPage.Text = (CurrentPage + 1).ToString();
				SessionSet.PageMessage = "页码范围越界";
				return;
			}
			else
			{
				int InputPage = Int32.Parse(this.txtJumpPage.Text.Trim()) - 1;
				ViewState["PageIndex"] = InputPage;

				ViewState["StartRow"] = Grid1.PageSize * InputPage;
				ViewState["EndRow"] = Grid1.PageSize * (InputPage + 1);

				BindGrid(ViewState["EmploySortField"].ToString());
			}
		}

		protected void Page_OnClick(object sender, EventArgs e)
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

			//从ViewState中读取页码值保存到CurrentPage变量中进行参数运算   
			CurrentPage = (int)ViewState["PageIndex"];
			//从ViewState中读取总页参数运算   
			PageCount = (int)ViewState["PageCounts"];
			string cmd = ((LinkButton)sender).CommandArgument;
			switch (cmd)
			{
				case "Next":
					CurrentPage++;
					break;
				case "Prev":
					CurrentPage--;
					break;
				case "Last":
					CurrentPage = PageCount;
					break;
				default:
					CurrentPage = 0;
					break;
			}
			//将运算后的CurrentPage变量再次保存至ViewState   
			ViewState["PageIndex"] = CurrentPage;

			ViewState["StartRow"] = Grid1.PageSize * CurrentPage;
			ViewState["EndRow"] = Grid1.PageSize * (CurrentPage + 1);


			BindGrid(ViewState["EmploySortField"].ToString());
		}

    }
}
