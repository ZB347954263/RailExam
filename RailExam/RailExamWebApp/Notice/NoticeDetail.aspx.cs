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

namespace RailExamWebApp.Notice
{
    public partial class NoticeDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (Request.QueryString.Get("mode"))
                {
                    case "Edit":
                        FormView1.ChangeMode(FormViewMode.Edit);
                        break;

                    case "Insert":
                        FormView1.ChangeMode(FormViewMode.Insert);
                        break;

                    case "ReadOnly":
                        FormView1.ChangeMode(FormViewMode.ReadOnly);
                        break;
                }
            }
        }

        protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            Response.Write("<script>window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");
            Response.End();
        }

        protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            Response.Write("<script>window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");
            Response.End();
        }

        protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
        {
            Response.Write("<script>window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");
            Response.End();
        }

        protected void FormView1_DataBound(object sender, EventArgs e)
        {
            if (Request.QueryString.Get("mode") == "Insert")
            {
                ((Label)FormView1.FindControl("lblOrgNameInsert")).Text = SessionSet.OrganizationName;
                ((Label)FormView1.FindControl("lblEmployeeNameInsert")).Text = SessionSet.EmployeeName;
            }
        }
    }
}