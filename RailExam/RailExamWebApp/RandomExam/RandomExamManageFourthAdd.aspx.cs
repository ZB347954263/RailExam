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
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using System.Collections.Generic;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class RandomExamManageFourthAdd : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //if (PrjPub.IsWuhan())
                //{
                //    Grid1.Columns[3].HeaderText = "员工编码";
                //    lblTitle.Text = "员工编码";
                //}
                //else
                //{
                //    Grid1.Columns[3].HeaderText = "工资编号";
                //    lblTitle.Text = "工资编号";
                //}

                lblTitle.Text = "员工编码";

                BindEmptyGrid1();

                string strId = Request.QueryString.Get("id");
                if (strId != null && strId != "")
                {
                    RandomExamArrangeBLL eaBll = new RandomExamArrangeBLL();
                    IList<RailExam.Model.RandomExamArrange> ExamArranges = eaBll.GetRandomExamArranges(int.Parse(strId));

                    if (ExamArranges.Count > 0)
                    {
                        ViewState["UpdateMode"] = 1;
                    }
                    else
                    {
                        ViewState["UpdateMode"] = 0;
                    }
                }

                OrganizationBLL objOrgBll = new OrganizationBLL();
                IList<RailExam.Model.Organization> objOrgList = objOrgBll.GetOrganizationsByLevel(2);

                ListItem item = new ListItem();
                item.Text = "--请选择--";
                item.Value = "0";
                ddlOrg.Items.Add(item);

                foreach (Organization organization in objOrgList)
                {
                    if (organization.OrganizationId != 1)
                    {
                        item = new ListItem();
                        item.Text = organization.ShortName;
                        item.Value = organization.OrganizationId.ToString();
                        ddlOrg.Items.Add(item);
                    }
                }

            	string strType = Request.QueryString.Get("type");
				if(strType != null && strType != "")
				{
					btnOK.Visible = true;
					btnClose.Visible = true;
					btnInput.Visible = false;
					ViewState["title"] = "选择学员";

                    if(PrjPub.CurrentLoginUser.SuitRange == 0)
                    {
                        ddlOrg.SelectedValue = PrjPub.CurrentLoginUser.StationOrgID.ToString();
                        ddlOrg.Enabled = false;
                    }
				}
				else
				{
					btnOK.Visible = false;
					btnClose.Visible = true;
					btnInput.Visible = true;
					ViewState["title"] = "单个添加考生";
				}

                ViewState["StartRow"] = 0;
                ViewState["EndRow"] = Grid1.PageSize;
                ViewState["EmploySortField"] = "nlssort(a.employee_name,'NLS_SORT=SCHINESE_PINYIN_M')";
            }
        }

        private void BindGrid(string strOrderBy)
        {
            int startRow = int.Parse(ViewState["StartRow"].ToString());
            int endRow = int.Parse(ViewState["EndRow"].ToString());
            int nItemcount = 0;
            EmployeeBLL psBLL = new EmployeeBLL();

            ViewState["PinYin"] = txtPinYin.Text;
            ViewState["WorkNo"] = txtWorkNo.Text;
            ViewState["EmployeeName"] = txtEmployeeName.Text;
            IList<RailExam.Model.Employee> Employees = psBLL.GetEmployeesSelect(Convert.ToInt32(ddlOrg.SelectedValue),ViewState["PinYin"].ToString(), ViewState["WorkNo"].ToString(), ViewState["EmployeeName"].ToString(), strOrderBy, startRow, endRow, ref nItemcount);
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

        private void BindEmptyGrid1()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmployeeID", typeof(string)));
            dt.Columns.Add(new DataColumn("OrgName", typeof(string)));
            dt.Columns.Add(new DataColumn("StrWorkNo", typeof(string)));
            dt.Columns.Add(new DataColumn("EmployeeName", typeof(string)));
            dt.Columns.Add(new DataColumn("PostName", typeof(string)));
            dt.Columns.Add(new DataColumn("Sex", typeof(string)));

            DataRow dr = dt.NewRow();

            dr["EmployeeID"] = "rbn";
            dr["OrgName"] = "";
            dr["StrWorkNo"] = "";
            dr["EmployeeName"] = "";
            dr["PostName"] = "";
            dr["Sex"] = "";
            dt.Rows.Add(dr);

            Grid1.CurrentPageIndex = 0;
            Grid1.VirtualItemCount = 1;
            Grid1.DataSource = dt;
            Grid1.DataBind();

            ViewState["EmptyFlag"] = 1;
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
            if(txtWorkNo.Text == "" && txtEmployeeName.Text == ""&& txtPinYin.Text == "" && ddlOrg.SelectedValue == "0")
            {
                SessionSet.PageMessage = "请输入查询条件！";
                return;
            }

            ViewState["EmploySortField"] = "nlssort(a.employee_name,'NLS_SORT=SCHINESE_PINYIN_M')";
            ViewState["StartRow"] = 0;
            ViewState["EndRow"] = Grid1.PageSize;
            this.Grid1.CurrentPageIndex = 0;
            BindGrid(ViewState["EmploySortField"].ToString());
        }

        protected void Grid1_PageIndexChanging(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.Grid1.CurrentPageIndex = e.NewPageIndex;
            ViewState["StartRow"] = Grid1.PageSize * e.NewPageIndex;
            ViewState["EndRow"] = Grid1.PageSize * (e.NewPageIndex + 1);

            BindGrid(ViewState["EmploySortField"].ToString());
        }

        protected void btnInput_Click(object sender, EventArgs e)
        {
            if (ViewState["EmptyFlag"].ToString() != null && ViewState["EmptyFlag"].ToString() == "1")
            {
                return;
            }

            string strId = Request.QueryString.Get("id");
            string strAllId = hfrbnID.Value;
            IList<RailExam.Model.Employee> Employees = null;
            EmployeeBLL psBLL = new EmployeeBLL();

			Employees = psBLL.GetEmployeesSelect(Convert.ToInt32(ddlOrg.SelectedValue), ViewState["PinYin"].ToString(), ViewState["WorkNo"].ToString(), ViewState["EmployeeName"].ToString(), "a.employee_Name");

            for (int i = 0; i < Employees.Count; i++)
            {
                string strEmId = Employees[i].EmployeeID.ToString();
                string strOldAllId = strAllId;
                if (strOldAllId == strEmId)
                {
                    strAllId = strEmId;
                }
            }

            ViewState["ChooseId"] = strAllId;

            if (strAllId == "")
            {
				SessionSet.PageMessage = "请选择考生！";
				return;
			}
            else
            {
                RandomExamBLL objBll = new RandomExamBLL();
                RailExam.Model.RandomExam random = objBll.GetExam(int.Parse(strId));

                if (random.RandomExamModularTypeID > 1)
                {
                    OracleAccess db = new OracleAccess();
                    string strSql = "select * from Random_Exam_Modular_Type where Random_Exam_Modular_Type_ID=" +
                                    random.RandomExamModularTypeID;
                    DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

                    strSql = "select * from Random_Exam_Modular_Type where Level_Num=" +
                             (Convert.ToInt32(dr["Level_NUM"]) - 1);
                    DataRow drPre = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

                    strSql = " select a.* "
                             + "from Random_Exam_Result a "
                             + " inner join Random_Exam b on a.Random_Exam_ID=b.Random_Exam_ID "
                             +
                             " left join Random_Exam_Modular_Type c on b.Random_Exam_Modular_Type_ID=c.Random_Exam_Modular_Type_ID "
                             + " where c.Level_Num=" + (Convert.ToInt32(dr["Level_NUM"]) - 1);
                    DataSet ds = db.RunSqlDataSet(strSql);

                    strSql = "select * from Employee where Employee_ID = " + strAllId;
                    DataRow drEmp = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

                    string strErrorMessage = string.Empty;

                    DataRow[] drs = ds.Tables[0].Select("EXAMINEE_ID=" + strAllId);

                    if (drs.Length == 0)
                    {
                        strErrorMessage = "所选考生【" + drEmp["Employee_Name"] + "】未能通过【" +
                                          drPre["Random_Exam_Modular_Type_Name"] + "】考试，不能参加【" +
                                          dr["Random_Exam_Modular_Type_Name"] + "】考试！";
                    }


                    if (!string.IsNullOrEmpty(strErrorMessage))
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

                 if (objList.Count == 0)
                 {
                     RandomExamArrange examArrange = new RandomExamArrange();
                     examArrange.RandomExamId = int.Parse(strId);
                     examArrange.UserIds = strAllId;
                     examArrange.Memo = "";
                     examArrangeBLL.AddRandomExamArrange(examArrange);
                     ViewState["UpdateMode"] = 1;
                     if (txtWorkNo.Text != "")
                     {
                         txtWorkNo.Text = "";
                         txtWorkNo.Focus();
                     }
                     if (txtPinYin.Text != "")
                     {
                         txtPinYin.Text = "";
                         txtPinYin.Focus();
                     }
                     if (txtEmployeeName.Text != "")
                     {
                         txtEmployeeName.Text = "";
                         txtEmployeeName.Focus();
                     }
                     BindEmptyGrid1();
                     SessionSet.PageMessage = "添加成功！";
                     return;
                 }
                 else
                 {
                     ViewState["UpdateMode"] = "1";
                 }
            }

            if (ViewState["UpdateMode"] != null && ViewState["UpdateMode"].ToString() == "1")
            {
                RandomExamArrangeBLL examArrangeBLL = new RandomExamArrangeBLL();
                string strEndId = examArrangeBLL.GetRandomExamArranges(int.Parse(strId))[0].UserIds;

                if((","+strEndId+",").IndexOf(","+strAllId+",") >= 0)
                {
                    SessionSet.PageMessage = "该学员已添加！";
                    return; 
                }

                if(strEndId == "")
                {
                    examArrangeBLL.UpdateRandomExamArrange(int.Parse(strId), strAllId);
                }
                else
                {
                    examArrangeBLL.UpdateRandomExamArrange(int.Parse(strId), strEndId + "," + strAllId);
                }

                if (txtWorkNo.Text != "")
                {
                    txtWorkNo.Text = "";
                    txtWorkNo.Focus();
                }
                if (txtPinYin.Text != "")
                {
                    txtPinYin.Text = "";
                    txtPinYin.Focus();
                }
                if (txtEmployeeName.Text != "")
                {
                    txtEmployeeName.Text = "";
                    txtEmployeeName.Focus();
                }
                BindEmptyGrid1();
                SessionSet.PageMessage = "添加成功！";

                ClientScript.RegisterClientScriptBlock(GetType(), "", " window.opener.form1.Refresh.value = 'true',window.opener.form1.submit();", true);
                return;
            }           
        }
    }
}
