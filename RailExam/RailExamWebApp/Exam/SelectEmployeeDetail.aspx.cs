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
using RailExam.BLL;
using RailExam.Model;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;
using System.Reflection;

namespace RailExamWebApp.Exam
{
    public partial class SelectEmployeeDetail : PageBase
    {
        public string _string = ",";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ButtonOutPutAll.Attributes.Add("onclick", "return confirm('您确定要移除全部考生吗 ？');");
                ButtonInputAll.Attributes.Add("onclick", "return confirm('加入上面列表显示的全部考生可能需要较长时间，您确认吗 ？');");

                ViewState["mode"] = Request.QueryString.Get("mode");
                if (ViewState["mode"].ToString() == "ReadOnly")
                {
                    this.ButtonSave.Visible = false;
                }

                BindOrgTree();
                BindPostTree();
                string strId = Request.QueryString.Get("id");
                if (strId != null && strId != "")
                {
                    ExamArrangeBLL eaBll = new ExamArrangeBLL();
                    IList<RailExam.Model.ExamArrange> ExamArranges = eaBll.GetExamArrangesByExamId(int.Parse(strId));

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
                ViewState["EmploySortField"] = "a.employee_ID";
                ViewState["ChooseEmploySortField"] = "EmployeeID";
                BindChoosedGrid(ViewState["ChooseId"].ToString(), ViewState["ChooseEmploySortField"].ToString());

                ViewState["StartRow"] = 0;
                ViewState["EndRow"] = Grid1.PageSize;
                
                BindGrid(ViewState["EmploySortField"].ToString());
            }
        }

        private void HasExamId()
        {
            string strExamId = Request.QueryString.Get("id");

            //已经参加考试的考生自动填充上

            ExamResultBLL reBll = new ExamResultBLL();
            IList<RailExam.Model.ExamResult> examResults = reBll.GetExamResultByExamID(int.Parse(strExamId));
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

        private void BindEmptyGrid1()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmployeeID", typeof(int)));
            dt.Columns.Add(new DataColumn("OrgName", typeof(string)));
            dt.Columns.Add(new DataColumn("WorkNo", typeof(string)));
            dt.Columns.Add(new DataColumn("EmployeeName", typeof(string)));
            dt.Columns.Add(new DataColumn("PostName", typeof(string)));

            DataRow dr = dt.NewRow();

            dr["EmployeeID"] = 0;
            dr["OrgName"] = "";
            dr["WorkNo"] = "";
            dr["EmployeeName"] = "";
            dr["PostName"] = "";
            dt.Rows.Add(dr);

            Grid1.VirtualItemCount = 1;
            Grid1.DataSource = dt;
            Grid1.DataBind();

            CheckBox CheckBox1 = (CheckBox)this.Grid1.Items[0].FindControl("chkSelect");
            CheckBox1.Visible = false;
            ViewState["EmptyFlag"] = 1;
        }

        private void BindEmptyGrid2()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("EmployeeID", typeof(int)));
            dt.Columns.Add(new DataColumn("OrgName", typeof(string)));
            dt.Columns.Add(new DataColumn("WorkNo", typeof(string)));
            dt.Columns.Add(new DataColumn("EmployeeName", typeof(string)));
            dt.Columns.Add(new DataColumn("PostName", typeof(string)));

            DataRow dr = dt.NewRow();

            dr["EmployeeID"] = 0;
            dr["OrgName"] = "";
            dr["WorkNo"] = "";
            dr["EmployeeName"] = "";
            dr["PostName"] = "";
            dt.Rows.Add(dr);

            Grid2.DataSource = dt;
            Grid2.DataBind();

            CheckBox CheckBox1 = (CheckBox)this.Grid2.Rows[0].FindControl("chkSelect2");
            CheckBox1.Visible = false;
        }


        public static DataTable ConvertToDataTable(Type type)
        {
            DataTable dt = new DataTable(type.Name);

            foreach (PropertyInfo pi in type.GetProperties())
            {
                dt.Columns.Add(pi.Name, pi.PropertyType);
            }
            dt.AcceptChanges();

            return dt;
        }

        public static DataTable ConvertToDataTable(IList list)
        {
            DataTable dt = null;

            if (list.Count > 0)
            {
                dt = new DataTable(list[0].GetType().Name);
                Type type = list[0].GetType();

                foreach (PropertyInfo pi in type.GetProperties())
                {
                    dt.Columns.Add(pi.Name, pi.PropertyType);
                }

                DataRow dr = null;
                foreach (Object o in list)
                {
                    dr = dt.NewRow();

                    foreach (DataColumn dc in dt.Columns)
                    {
                        dr[dc] = type.GetProperty(dc.ColumnName).GetValue(o, null);
                    }
                    dt.Rows.Add(dr);
                }
                dt.AcceptChanges();
            }

            return dt;
        }

        private void BindGrid(string strOrderBy)
        {
            int startRow = int.Parse(ViewState["StartRow"].ToString());
            int endRow = int.Parse(ViewState["EndRow"].ToString());
            int nItemcount = 0;
            EmployeeBLL psBLL = new EmployeeBLL();

            if (rdo1.SelectedValue == "0")
            {
                IList<RailExam.Model.Employee> Employees = psBLL.GetEmployees(int.Parse(tvOrg.SelectedNode.ID), "",
                textgh.Text, TextBoxPapername.Text, "", txtPostName.Text, strOrderBy, startRow, endRow, ref nItemcount);
                if (Employees.Count > 0)
                {
                    //int nCount = (Employees.Count - 1) / this.Grid1.PageSize + 1;

                    //if (this.Grid1.PageIndex >= nCount)
                    //{
                    //    this.Grid1.PageIndex = 0;
                    //}

                    Grid1.VirtualItemCount = nItemcount ;
                   
                    Grid1.DataSource = Employees;
                    Grid1.DataBind();                   
                    ViewState["EmptyFlag"] = 0;
                }
                else
                {
                    BindEmptyGrid1();
                }
            }
            else
            {
                IList<RailExam.Model.Employee> Employees = psBLL.GetEmployeesByPost(int.Parse(tvPost.SelectedNode.ID), tvPost.SelectedNode.Value,
                textgh.Text, TextBoxPapername.Text, "", txtPostName.Text, strOrderBy, startRow, endRow, ref nItemcount);
                if (Employees.Count > 0)
                {
                    //int nCount = (Employees.Count - 1) / this.Grid1.PageSize + 1;

                    //if (this.Grid1.PageIndex >= nCount)
                    //{
                    //    this.Grid1.PageIndex = 0;
                    //}
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


        private void BindPostTree()
        {
            PostBLL PostBLL = new PostBLL();
            IList<RailExam.Model.Post> posts = PostBLL.GetPosts(0, 0, 0, "", 0, "", "", 0, 0, "",String.Empty, 0, 9000, "PostLevel, OrderIndex ASC");

            Pub.BuildComponentArtTreeView(tvPost, (IList)posts, "PostId", "ParentId", "PostName",
                "PostName", "IdPath", null, null, null);
            tvPost.SelectedNode = tvPost.Nodes[0];
            if (tvPost.Nodes.Count > 0)
            {
                tvPost.Nodes[0].Expanded = true;
            }
        }


        private void BindOrgTree()
        {
            OrganizationBLL OrganizationBLL = new OrganizationBLL();

            if (PrjPub.CurrentLoginUser.SuitRange == 1)
            {
                IList<RailExam.Model.Organization> organizationList = OrganizationBLL.GetOrganizations();

                Pub.BuildComponentArtTreeView(this.tvOrg, (IList) organizationList, "OrganizationId",
                                              "ParentId", "ShortName", "FullName", "OrganizationId", null, null, null);
            }
            else
            {
                int stationID = OrganizationBLL.GetStationOrgID(Convert.ToInt32(SessionSet.OrganizationID));
                IList<RailExam.Model.Organization> organizationList =
                    OrganizationBLL.GetOrganizations(stationID);

                if (organizationList.Count > 0)
                {
                    TreeViewNode tvn = null;

                    foreach (RailExam.Model.Organization organization in organizationList)
                    {
                        tvn = new TreeViewNode();
                        tvn.ID = organization.OrganizationId.ToString();
                        tvn.Value = organization.IdPath.ToString();
                        tvn.Text = organization.ShortName;
                        tvn.ToolTip = organization.FullName;

                        if (organization.ParentId == 1)
                        {
                            tvOrg.Nodes.Add(tvn);
                        }
                        else
                        {
                            try
                            {
                                tvOrg.FindNodeById(organization.ParentId.ToString()).Nodes.Add(tvn);
                            }
                            catch
                            {
                                tvOrg.Nodes.Clear();
                                SessionSet.PageMessage = "数据错误！";
                                return;
                            }
                        }
                    }
                }
                tvOrg.DataBind();
            }
            if (tvOrg.Nodes.Count > 0)
            {
                tvOrg.SelectedNode = tvOrg.Nodes[0];
                tvOrg.Nodes[0].Expanded = true;
            }
        }

        private void BindChoosedGrid(string strId, string strOrderBy)
        {
            HasExamId();

            string strExamId = Request.QueryString.Get("id");
            //已经参加考试的考生自动填充上

            ExamResultBLL reBll = new ExamResultBLL();
            IList<RailExam.Model.ExamResult> examResults = reBll.GetExamResultByExamID(int.Parse(strExamId));

            for (int i = 0; i < examResults.Count; i++)
            {
                string strEmId = examResults[i].ExamineeId.ToString();
                string strOldAllId = "," + strId + ",";
                if (strOldAllId.IndexOf("," + strEmId + ",") == -1)
                {
                    if (strId.Length == 0)
                    {
                        strId += strEmId;
                    }
                    else
                    {
                        strId += "," + strEmId;
                    }
                }
            }

            EmployeeBLL psBLL = new EmployeeBLL();
            DataSet ds = new DataSet();

            string strIDs = "," + strId + ",";
            if (strIDs.Length > 2000)
            {
                ds = psBLL.GetEmployeesByEmployeeIdS(strIDs);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //DataView dv = ds.Tables[0].DefaultView;
                    //dv.Sort = strOrderBy;
                    Grid2.DataSource = ds;
                    Grid2.DataBind();
                }
                else
                {
                    BindEmptyGrid2();
                }
            }
            else
            {
                IList<Employee> objList = psBLL.GetEmployeesByEmployeeId(strIDs);
                if (objList.Count > 0)
                {
                    ds.Tables.Add(ConvertToDataTable((IList)objList));
                    Grid2.DataSource = ds;
                    Grid2.DataBind();
                }
                else
                {
                    BindEmptyGrid2();
                }
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            ViewState["EmploySortField"] = "a.employee_ID";
            ViewState["StartRow"] = 0;
            ViewState["EndRow"] = Grid1.PageSize;
            this.Grid1.CurrentPageIndex = 0;
            BindGrid(ViewState["EmploySortField"].ToString());
        }

        protected void tvOrg_NodeSelected(object sender, TreeViewNodeEventArgs e)
        {
            ViewState["StartRow"] = 0;
            ViewState["EndRow"] = Grid1.PageSize;
            this.Grid1.CurrentPageIndex = 0;
            BindGrid(ViewState["EmploySortField"].ToString());
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

            BindChoosedGrid(ViewState["ChooseId"].ToString(), ViewState["ChooseEmploySortField"].ToString());
        }

        protected void ButtonOutPut_Click(object sender, EventArgs e)
        {
            string strAllId = ViewState["ChooseId"].ToString();
            if (strAllId == "")
            {
                return;
            }

            string strOldAllId = "," + strAllId + ",";

            for (int i = 0; i < this.Grid2.Rows.Count; i++)
            {
                CheckBox CheckBox1 = (CheckBox)this.Grid2.Rows[i].FindControl("chkSelect2");
                string strEmId = ((Label)this.Grid2.Rows[i].FindControl("LabelEmployeeID")).Text;
                if (CheckBox1.Checked)
                {
                    strOldAllId = strOldAllId.Replace(strEmId + ",", "");
                }
            }

            int n = strOldAllId.Length;
            if (n == 1)
            {
                ViewState["ChooseId"] = "";
            }
            else
            {
                ViewState["ChooseId"] = strOldAllId.Substring(1, n - 2);
            }
            BindGrid(ViewState["EmploySortField"].ToString());
            BindChoosedGrid(ViewState["ChooseId"].ToString(), ViewState["ChooseEmploySortField"].ToString());
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (ViewState["mode"].ToString() == "Edit")
            {
                Response.Write("<script>window.opener.form1.Refresh.value = 'true',window.opener.form1.submit();window.close();</script>");
            }
            else if (ViewState["mode"].ToString() == "Insert")
            {
                Response.Write("<script>window.opener.form1.Refresh.value = 'true',window.opener.form1.submit();window.close();</script>");
            }
            else
            {
                Response.Write("<script>window.close();</script>");
            }
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            string strId = Request.QueryString.Get("id");

            string strFlag = "";

            if (ViewState["mode"].ToString() == "Insert")
            {
                strFlag = "Edit";
            }
            else
            {
                strFlag = ViewState["mode"].ToString();
            }

            ExamResultBLL reBll = new ExamResultBLL();

            IList<RailExam.Model.ExamResult> examResults = reBll.GetExamResultByExamID(int.Parse(strId));
            if (examResults.Count > 0 && ViewState["mode"].ToString() != "ReadOnly")
            {
                Response.Redirect("ExamManageFirst.aspx?mode=" + strFlag + "&id=" + strId);
            }
            else
            {
                Response.Redirect("ExamManageSecond.aspx?mode=" + strFlag + "&id=" + strId);
            }
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

            if (rdo1.SelectedValue == "0")
            {
                Employees = psBLL.GetEmployees(int.Parse(tvOrg.SelectedNode.ID), "",
                   textgh.Text, TextBoxPapername.Text, "", txtPostName.Text, "a.employee_ID");
            }
            else
            {
                Employees = psBLL.GetEmployeesByPost(int.Parse(tvPost.SelectedNode.ID), tvPost.SelectedNode.Value,
                               textgh.Text, TextBoxPapername.Text, "", txtPostName.Text, "a.employee_ID");
            }

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

            //if (strAllId.Length > 2000)
            //{
            //    SessionSet.PageMessage = "考生超过最大人数，请重新选择！";
            //    return;
            //}

            ViewState["ChooseId"] = strAllId;
            BindChoosedGrid(ViewState["ChooseId"].ToString(), ViewState["ChooseEmploySortField"].ToString());
        }


        protected void ButtonOutPutAll_Click(object sender, EventArgs e)
        {
            ViewState["ChooseId"] = "";

            BindChoosedGrid(ViewState["ChooseId"].ToString(), ViewState["ChooseEmploySortField"].ToString());
            for (int i = 0; i < this.Grid1.Items.Count; i++)
            {
                CheckBox CheckBox1 = (CheckBox)this.Grid1.Items[i].FindControl("chkSelect");
                CheckBox1.Checked = false;
            }
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

        protected void rdo1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdo1.SelectedValue == "0")
            {
                tvOrg.Visible = true;
                tvPost.Visible = false;
                tvOrg.Nodes.Clear();
                BindOrgTree();
            }
            else
            {
                tvOrg.Visible = false;
                tvPost.Visible = true;
                tvPost.Nodes.Clear();
                BindPostTree();
            }
        }

        protected void tvPost_NodeSelected(object sender, TreeViewNodeEventArgs e)
        {
            ViewState["StartRow"] = 0;
            ViewState["EndRow"] = Grid1.PageSize;
            this.Grid1.CurrentPageIndex = 0;
            BindGrid(ViewState["EmploySortField"].ToString());
        }

        protected void ButtonSave_Click(object sender, ImageClickEventArgs e)
        {
            string strId = Request.QueryString.Get("id");

            string strEndId = "";

            for (int i = 0; i < this.Grid2.Rows.Count; i++)
            {
                string strEmId = ((Label)this.Grid2.Rows[i].FindControl("LabelEmployeeID")).Text;

                if (strEndId.Length == 0)
                {
                    strEndId += strEmId;
                }
                else
                {
                    strEndId += "," + strEmId;
                }
            }

            if (strEndId == "" || strEndId == "0")
            {
                SessionSet.PageMessage = "请选择考生！";
                return;
            }

            int PaperId = 0;
            ExamBLL examBLL = new ExamBLL();
            RailExam.Model.Exam exam = examBLL.GetExam(int.Parse(strId));
            if (exam != null)
            {
                PaperId = exam.paperId;
            }

            if (ViewState["UpdateMode"] != null && ViewState["UpdateMode"].ToString() == "0")
            {
                ExamArrange examArrange = new ExamArrange();
                examArrange.ExamId = int.Parse(strId);
                examArrange.PaperId = PaperId;
                examArrange.UserIds = strEndId;
                examArrange.JudgeIds = "1";
                examArrange.BeginTime = DateTime.Now;
                examArrange.EndTime = DateTime.Now;
                examArrange.OrderIndex = 1;
                examArrange.Memo = "";
                ExamArrangeBLL examArrangeBLL = new ExamArrangeBLL();
                examArrangeBLL.AddExamArrange(examArrange);
                ViewState["UpdateMode"] = 1;
                SessionSet.PageMessage = "保存成功！";
                return;
            }

            if (ViewState["UpdateMode"] != null && ViewState["UpdateMode"].ToString() == "1")
            {
                ExamArrangeBLL examArrangeBLL = new ExamArrangeBLL();
                examArrangeBLL.UpdateExamUser(int.Parse(strId), strEndId);
                SessionSet.PageMessage = "保存成功！";
                return;
            }
        }

        protected void Grid1_Sorting(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
        {

            if (ViewState["EmploySortField"] != null && ViewState["EmploySortField"].ToString() == e.SortExpression)
            {
                ViewState["EmploySortField"] = e.SortExpression + " desc ";
            }
            else
            {
                ViewState["EmploySortField"] = e.SortExpression;
            }

            BindGrid(ViewState["EmploySortField"].ToString());
        }


        protected void Grid2_Sorting(object sender, GridViewSortEventArgs e)
        {

            if (ViewState["ChooseEmploySortField"] != null && ViewState["ChooseEmploySortField"].ToString() == e.SortExpression)
            {
                ViewState["ChooseEmploySortField"] = e.SortExpression + " desc ";
            }
            else
            {
                ViewState["ChooseEmploySortField"] = e.SortExpression;
            }
            BindChoosedGrid(ViewState["ChooseId"].ToString(), ViewState["ChooseEmploySortField"].ToString());
        }
    }
}
