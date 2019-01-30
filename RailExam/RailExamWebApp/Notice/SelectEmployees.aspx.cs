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
using DSunSoft.Web.UI;

namespace RailExamWebApp.Notice
{
    public partial class SelectEmployees : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbOrgType.SelectedIndex = 0;

                string strIDS = Request.QueryString["id"];

                if (!string.IsNullOrEmpty(strIDS))
                {
                    ViewState["ChooseID"] = strIDS;
                }
                else
                {
                    ViewState["ChooseID"] = "";
                }

                BindChoosedGrid(ViewState["ChooseID"].ToString());

                OrganizationBLL organizationBLL = new OrganizationBLL();
                IList<RailExam.Model.Organization> organizations = organizationBLL.GetOrganizations();

                if (organizations != null)
                {
                    foreach (Organization organization in organizations)
                    {
                        ListItem Li = new ListItem();
                        Li.Value = organization.OrganizationId.ToString();
                        if (organization.OrganizationId == 1)
                        {
                            Li.Text = organization.ShortName;
                        }
                        else
                        {
                            Li.Text = "--" + organization.ShortName;
                        }

                        lbOrgType.Items.Add(Li);
                    }
                    lbOrgType.SelectedIndex = 0;
                    BindGrid();
                }
            }
        }

        private void BindGrid()
        {
            EmployeeBLL employeeBLL = new EmployeeBLL();

            IList<RailExam.Model.Employee> employees = employeeBLL.GetEmployees(int.Parse(lbOrgType.SelectedValue), "");
            if (employees != null)
            {
                Grid1.DataSource = employees;
                Grid1.DataBind();
            }
        }

        private void BindChoosedGrid(string strIDS)
        {
            EmployeeBLL employeeBLL = new EmployeeBLL();

            IList<RailExam.Model.Employee> employees = new List<Employee>();

            if (strIDS != string.Empty)
            {
                string[] strIDArr = strIDS.Split(',');

                foreach (string strEmployeeID in strIDArr)
                {
                    Employee employee = employeeBLL.GetEmployee(int.Parse(strEmployeeID));
                    employees.Add(employee);
                }
            }

            if (employees != null)
            {
                Grid2.DataSource = employees;
                Grid2.DataBind();
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            EmployeeBLL employeeBLL = new EmployeeBLL();

            IList<RailExam.Model.Employee> employees = employeeBLL.GetEmployees(int.Parse(lbOrgType.SelectedValue), "",
                txtWorkNo.Text, txtName.Text, ddlSex.SelectedValue,"",0);
            if (employees != null)
            {
                Grid1.DataSource = employees;
                Grid1.DataBind();
            }
        }

        protected void lbOrgType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnInput_Click(object sender, EventArgs e)
        {
            string strIDS = ViewState["ChooseID"].ToString();

            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)Grid1.Rows[i].FindControl("chkSelect");

                string strEmployeeID = ((Label)Grid1.Rows[i].FindControl("lblEmployeeID")).Text;
                if (chkSelect.Checked)
                {
                    string strOldIDS = "," + strIDS + ",";

                    if (strOldIDS.IndexOf("," + strEmployeeID + ",") == -1)
                    {
                        if (strIDS.Length == 0)
                        {
                            strIDS += strEmployeeID;
                        }
                        else
                        {
                            strIDS += "," + strEmployeeID;
                        }
                    }
                }
            }

            ViewState["ChooseID"] = strIDS;

            BindChoosedGrid(ViewState["ChooseID"].ToString());
        }

        protected void btnOutPut_Click(object sender, EventArgs e)
        {
            string strIDS = ViewState["ChooseID"].ToString();

            if (strIDS == string.Empty)
            {
                return;
            }

            string strOldIDS = "," + strIDS + ",";

            for (int i = 0; i < Grid2.Rows.Count; i++)
            {
                CheckBox chkSelect = (CheckBox)Grid2.Rows[i].FindControl("chkSelect");

                string strEmployeeID = ((Label)Grid2.Rows[i].FindControl("lblEmployeeID")).Text;

                if (chkSelect.Checked)
                {
                    strOldIDS = strOldIDS.Replace("," + strEmployeeID + ",", ",");
                }
            }

            int nLength = strOldIDS.Length;
            if (nLength == 1)   //实际上这时的strOldIDS = ","
            {
                ViewState["ChooseID"] = "";
            }
            else
            {
                ViewState["ChooseID"] = strOldIDS.Substring(1, nLength - 2);
            }

            BindChoosedGrid(ViewState["ChooseID"].ToString());
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strIDS = string.Empty;
            string strScript = string.Empty;

            for (int i = 0; i < Grid2.Rows.Count; i++)
            {
                strIDS += ((Label)Grid2.Rows[i].FindControl("lblEmployeeID")).Text + ",";
            }

            if (strIDS.Length > 1)
            {
                strIDS = strIDS.Remove(strIDS.Length - 1, 1);
            }

            strScript = "<script>window.opener.form1.FormView1_hfReceiveEmployeeIDS.value='" + strIDS
                      + "';window.close();</script>";

            Response.Write(strScript);
            Response.End();
        }

        protected void btnInputAll_Click(object sender, EventArgs e)
        {
            string strIDS = ViewState["ChooseID"].ToString();

            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string strEmployeeID = ((Label)Grid1.Rows[i].FindControl("lblEmployeeID")).Text;

                string strOldIDS = "," + strIDS + ",";

                if (strOldIDS.IndexOf("," + strEmployeeID + ",") == -1)
                {
                    if (strIDS.Length == 0)
                    {
                        strIDS += strEmployeeID;
                    }
                    else
                    {
                        strIDS += "," + strEmployeeID;
                    }
                }
            }

            ViewState["ChooseID"] = strIDS;

            BindChoosedGrid(ViewState["ChooseID"].ToString());
        }

        protected void btnOutPutAll_Click(object sender, EventArgs e)
        {
            ViewState["ChooseID"] = "";

            BindChoosedGrid(ViewState["ChooseID"].ToString());
        }
    }
}