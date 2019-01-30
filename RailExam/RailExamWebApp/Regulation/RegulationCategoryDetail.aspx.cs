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

namespace RailExamWebApp.Regulation
{
    public partial class RegulationCategoryDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Request.QueryString.Get("mode"))
            {
                case "Edit":
                    fvRegulationCategory.ChangeMode(FormViewMode.Edit);
                    break;

                case "Insert":
                    fvRegulationCategory.ChangeMode(FormViewMode.Insert);

                    ((HiddenField) fvRegulationCategory.FindControl("hfParentId")).Value = Request.QueryString["id"];
                    ((HiddenField) fvRegulationCategory.FindControl("hfRegulationCategoryId")).Value =
                        Request.QueryString["id"];

                    break;

                case "ReadOnly":
                    fvRegulationCategory.ChangeMode(FormViewMode.ReadOnly);
                    break;
            }
        }

        protected void fvRegulationCategory_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            Response.Write("<script>window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");
            Response.End();
        }

        protected void fvRegulationCategory_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            Response.Write("<script>window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");
            Response.End();
        }

        protected void fvRegulationCategory_ItemDeleted(object sender, FormViewDeletedEventArgs e)
        {
            Response.Write("<script>window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");
            Response.End();
        }
    }
}