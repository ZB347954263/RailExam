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

namespace RailExamWebApp.Common
{
    public partial class SelectEmployees : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listTType.SelectedIndex = 0;

                string strId = Request.QueryString.Get("id");

                if (strId != null && strId != "")
                {
                    if (strId.Substring(0, 1) != "请")
                    {
                        ViewState["ChooseId"] = strId;
                    }
                    else
                    {
                        ViewState["ChooseId"] = "";
                    }
                }
                else
                {
                    ViewState["ChooseId"] = "";
                }

                BindChoosedGrid(ViewState["ChooseId"].ToString());

                OrganizationBLL psBLL = new OrganizationBLL();
                IList<RailExam.Model.Organization> Organizations = psBLL.GetOrganizations();

                if (Organizations != null)
                {
                    for (int i = 0; i < Organizations.Count; i++)
                    {
                        Organization paperSubject = Organizations[i];
                        ListItem Li = new ListItem();
                        Li.Value = paperSubject.OrganizationId.ToString();
                        if (paperSubject.OrganizationId == 1)
                        {
                            Li.Text = paperSubject.ShortName;
                        }
                        else
                        {
                            Li.Text = "----" + paperSubject.ShortName;
                        }

                        listTType.Items.Add(Li);
                    }
                    listTType.SelectedIndex = 0;
                    BindGrid();
                }
            }
        }

        private void BindGrid()
        {
            EmployeeBLL psBLL = new EmployeeBLL();

            IList<RailExam.Model.Employee> Employees = psBLL.GetEmployees(int.Parse(listTType.SelectedValue), "");
            if (Employees != null)
            {
                Grid1.DataSource = Employees;
                Grid1.DataBind();
            }
        }

        private void BindChoosedGrid(string strId)
        {
            EmployeeBLL psBLL = new EmployeeBLL();

            IList<RailExam.Model.Employee> Employees = new List<Employee>();

            string[] eLid = strId.Split(new char[] { ',' });

            foreach (string s in eLid)
            {
                string employId = s.Trim();
                if (employId != "")
                {
                    Employee Employee1 = psBLL.GetEmployee(int.Parse(employId));
                    Employees.Add(Employee1);
                }
            }

            if (Employees != null)
            {
                Grid2.DataSource = Employees;
                Grid2.DataBind();
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            EmployeeBLL psBLL = new EmployeeBLL();

            IList<RailExam.Model.Employee> Employees = psBLL.GetEmployees(int.Parse(listTType.SelectedValue), "",
                textgh.Text, TextBoxPapername.Text, ddlType.SelectedValue,"",Convert.ToInt32(ddlStatus.SelectedValue));

            if (Employees != null)
            {
                Grid1.DataSource = Employees;
                Grid1.DataBind();
            }
        }

        protected void listTType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void btnInput_Click(object sender, EventArgs e)
        {
            string strAllId = ViewState["ChooseId"].ToString();

            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                CheckBox CheckBox1 = (CheckBox)Grid1.Rows[i].FindControl("chkSelect");

                string strEmId = ((Label)Grid1.Rows[i].FindControl("LabelEmployeeID")).Text;
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

            BindChoosedGrid(ViewState["ChooseId"].ToString());
        }

        protected void ButtonOutPut_Click(object sender, EventArgs e)
        {
            string strAllId = ViewState["ChooseId"].ToString();
            if (strAllId == "")
            {
                return;
            }
            string strOldAllId = "," + strAllId + ",";

            for (int i = 0; i < Grid2.Rows.Count; i++)
            {
                CheckBox CheckBox1 = (CheckBox)Grid2.Rows[i].FindControl("chkSelect2");
                string strEmId = ((Label)Grid2.Rows[i].FindControl("LabelEmployeeID")).Text;
                if (CheckBox1.Checked)
                {
                    strOldAllId = strOldAllId.Replace("," + strEmId + ",", ",");
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

            BindChoosedGrid(ViewState["ChooseId"].ToString());
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            string strop = Request.QueryString.Get("op");

            string strEndId = "";

            for (int i = 0; i < Grid2.Rows.Count; i++)
            {
                string strEmId = ((Label)Grid2.Rows[i].FindControl("LabelEmployeeID")).Text;

                if (strEndId.Length == 0)
                {
                    strEndId += strEmId;
                }
                else
                {
                    strEndId += "," + strEmId;
                }
            }

            if (strEndId == "")
            {
                if (strop == "1")
                {
                    SessionSet.PageMessage = "请选择考生！";
                }
                else
                {
                    SessionSet.PageMessage = "请选择评卷人！";
                }

                return;
            }

            if (strop == "1")
            {
                Response.Write("<script>top.window.opener.form1.UserIds.value='" + strEndId + "' ;top.window.opener.form1.submit();top.window.close();</script>");
            }

            if (strop == "2")
            {
                Response.Write("<script>top.window.opener.form1.JudgeIds.value='" + strEndId + "' ;top.window.opener.form1.submit();top.window.close();</script>");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("<script>top.window.close();</script>");
        }

        protected void ButtonInputAll_Click(object sender, EventArgs e)
        {
            string strAllId = ViewState["ChooseId"].ToString();

            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                //CheckBox CheckBox1 = (CheckBox)Grid1.Rows[i].FindControl("chkSelect");

                string strEmId = ((Label)Grid1.Rows[i].FindControl("LabelEmployeeID")).Text;

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

            BindChoosedGrid(ViewState["ChooseId"].ToString());
        }

        protected void ButtonOutPutAll_Click(object sender, EventArgs e)
        {
            ViewState["ChooseId"] = "";

            BindChoosedGrid(ViewState["ChooseId"].ToString());
        }
    }
}
