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
    public partial class AssignAccount : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (PrjPub.CurrentLoginUser == null)
            {
                Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                return;
            }
            if (IsPostBack && FormView1.CurrentMode == FormViewMode.Insert)
            {
                ((HiddenField)FormView1.FindControl("hfEmployeeID")).Value = Request.QueryString["id"];
            }
            if (PrjPub.CurrentLoginUser.SuitRange == 1 && PrjPub.CurrentLoginUser.IsAdmin && PrjPub.CurrentLoginUser.UseType == 0)
            {
                hfSuitRange.Value = "1";
            }
            else
            {
                hfSuitRange.Value = "0";
            }
        }

        protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            if(e.AffectedRows !=0)
            {
                Response.Write("<script>window.close();</script>");
                Response.End();
            }
       }

        protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
        {
            Response.Write("<script>window.close();</script>");
            Response.End();
        }

        protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            if(e.AffectedRows != 0)
            {
                Response.Write("<script>window.close();</script>");
                Response.End();
            }
        }

        protected void FormView1_DataBound(object sender, EventArgs e)
        {
            if (FormView1.Row.RowType == DataControlRowType.EmptyDataRow)
                FormView1.ChangeMode(FormViewMode.Insert);
        }

        protected void odsAssignAccount_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
        {
            DropDownList ddlRole = (DropDownList)FormView1.FindControl("ddlRoleNameInsert");
            if (ddlRole.SelectedIndex == 1 && PrjPub.CurrentLoginUser.SuitRange == 0)
            {
                Response.Write("<script>alert('站段超级管理员只能由路局超级管理员分配！');</script>");
                e.Cancel = true;
                return;
            }

            SystemUser user = (SystemUser)e.InputParameters[0];
            string strSql = "select * from System_User where User_ID='"+user.UserID+"'";
            OracleAccess db = new OracleAccess();
            if(db.RunSqlDataSet(strSql).Tables[0].Rows.Count > 0)
            {
                SessionSet.PageMessage = "该登录用户名已经在系统中存在！";
                e.Cancel = true;
                return;
            }
        }

        protected void odsAssignAccount_Updating(object sender, ObjectDataSourceMethodEventArgs e)
        {
            DropDownList ddlRole = (DropDownList)FormView1.FindControl("ddlRoleNameEdit");
            if(ddlRole.SelectedIndex == 1 && PrjPub.CurrentLoginUser.SuitRange == 0)
            {
                Response.Write("<script>alert('站段超级管理员只能由路局超级管理员分配！');</script>");
                e.Cancel = true;
                return;
            }

            SystemUser user = (SystemUser)e.InputParameters[0];
            string strSql = "select * from System_User where User_ID='" + user.UserID + "' and Employee_ID !="+user.EmployeeID;
            OracleAccess db = new OracleAccess();
            if (db.RunSqlDataSet(strSql).Tables[0].Rows.Count > 0)
            {
                Response.Write("<script>alert('该登录用户名已经在系统中存在！');</script>");
                e.Cancel = true;
                return;
            }
        }
    }
}