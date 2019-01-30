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
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Systems
{
    public partial class OrganizationDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (fvOrganization.CurrentMode == FormViewMode.Insert)
            {
                if (hfInsert.Value == "-1")
                {
                    ((HiddenField)fvOrganization.FindControl("hfParentId")).Value = Request.QueryString["id"];
                }
                else
                {
                    ((HiddenField)fvOrganization.FindControl("hfParentId")).Value = hfInsert.Value;
                }
            }

            string strDeleteID = Request.Form.Get("DeleteID");
            if (strDeleteID != "" && strDeleteID != null)
            {
                OrganizationBLL objBll = new OrganizationBLL();
                RailExam.Model.Organization organization = objBll.GetOrganization(Convert.ToInt32(strDeleteID));
                int code = 0;
				string sqlStr = "select count(*) from item where org_id=" + strDeleteID;
            	DataTable dt = new OracleAccess().RunSqlDataSet(sqlStr).Tables[0];
				if (dt != null && Convert.ToInt32(dt.Rows[0][0]) > 0)
				{
					SessionSet.PageMessage = "该组织机构已被引用，不能删除！";
				}
				else
				{
					objBll.DeleteOrganization(Convert.ToInt32(strDeleteID), ref code);

					if (code != 0) //code=2292
					{
						SessionSet.PageMessage = "该组织机构已被引用，不能删除！";
					}
					else
					{
						ClientScript.RegisterStartupScript(GetType(),
						                                   "jsSelectFirstNode",
						                                   @"window.parent.tvOrganizationChangeCallBack.callback(" + organization.ParentId +
						                                   @", 'Delete');                        
                            if(window.parent.tvOrganization.get_nodes().get_length() > 0)
                            {
                                window.parent.tvOrganization.get_nodes().getNode(0).select();
                            }",
						                                   true);
					}
				}
            }
        }

        protected  void fvOrganization_DataBound(object sender, EventArgs e)
        {
            if (fvOrganization.CurrentMode != FormViewMode.ReadOnly)
            {
                DropDownList dropDownList = ((DropDownList)fvOrganization.FindControl("ddlRailSystem"));
                ListItem item = new ListItem();
                item.Text = "--请选择--";
                item.Value = "0";
                dropDownList.Items.Add(item);

                string strSql = "select * from Rail_System";
                OracleAccess db = new OracleAccess();
                DataSet ds = db.RunSqlDataSet(strSql);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    item = new ListItem();
                    item.Text = dr["Rail_System_Name"].ToString();
                    item.Value = dr["Rail_System_ID"].ToString();
                    dropDownList.Items.Add(item);
                }

                if (fvOrganization.CurrentMode == FormViewMode.Edit)
                {
                    dropDownList.SelectedValue = ((HiddenField)fvOrganization.FindControl("hfRailSystem")).Value;

                    if (((HiddenField)fvOrganization.FindControl("hfLevelNum")).Value != "2")
                    {
                        dropDownList.Enabled = false;
                    }
                }
                else
                {
                    if (((HiddenField)fvOrganization.FindControl("hfParentId")).Value != "1")
                    {
                        dropDownList.Enabled = false;
                    }
                }
            }
        }

        protected  void fvOrganization_ItemInserting(object sender,FormViewInsertEventArgs e)
        {
            DropDownList dropDownList = ((DropDownList)fvOrganization.FindControl("ddlRailSystem"));
            ((HiddenField)fvOrganization.FindControl("hfRailSystem")).Value = dropDownList.SelectedValue;
            e.Values["RailSystemID"] = dropDownList.SelectedValue;
        }

        protected void fvOrganization_ItemUpdating(object sender,FormViewUpdateEventArgs e)
        {
            CheckBox chk = ((CheckBox)fvOrganization.FindControl("chkIsEffect"));
            if(!chk.Checked)
            {
                HiddenField hfIdPath = ((HiddenField) fvOrganization.FindControl("hfIdPath"));
                EmployeeBLL objBll = new EmployeeBLL();
                IList<RailExam.Model.Employee> objList = objBll.GetEmployeesByOrgIDPath(hfIdPath.Value);
                if(objList.Count>0)
                {
                    e.Cancel = true;
                    ClientScript.RegisterStartupScript(GetType(),
                        "jsSelectFirstNode",
                        @"alert('该组织机构下有职员信息，不能设置为无效组织机构！');
                        window.parent.tvOrganizationChangeCallBack.callback(" + e.Keys["OrganizationId"] + @", 'Update');                        
                                if(window.parent.tvOrganization.get_nodes().get_length() > 0)
                                {
                                    window.parent.tvOrganization.get_nodes().getNode(0).select();
                                }",
                        true);
                    return;
                }
            }

            DropDownList dropDownList = ((DropDownList)fvOrganization.FindControl("ddlRailSystem"));
            ((HiddenField)fvOrganization.FindControl("hfRailSystem")).Value = dropDownList.SelectedValue;
            e.NewValues["RailSystemID"] = dropDownList.SelectedValue;
        }

        protected void fvOrganization_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            string strId = e.Values["ParentId"].ToString();
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvOrganizationChangeCallBack.callback("+strId+@", 'Insert');                        
            if(window.parent.tvOrganization.get_nodes().get_length() > 0)
            {
                window.parent.tvOrganization.get_nodes().getNode(0).select();
            }",
                true);
        }

        protected void fvOrganization_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(),
                "jsSelectFirstNode",
                @"window.parent.tvOrganizationChangeCallBack.callback(" + e.Keys["OrganizationId"] + @", 'Update');                        
            if(window.parent.tvOrganization.get_nodes().get_length() > 0)
            {
                window.parent.tvOrganization.get_nodes().getNode(0).select();
            }",
                true);
        }
    }
}