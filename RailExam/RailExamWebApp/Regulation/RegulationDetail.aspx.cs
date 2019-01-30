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
using DSunSoft.Web.UI;

namespace RailExamWebApp.Regulation
{
    public partial class RegulationDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (Request.QueryString.Get("mode"))
                {
                    case "Edit":
                        fvRegulation.ChangeMode(FormViewMode.Edit);
                        break;

                    case "Insert":
                        fvRegulation.ChangeMode(FormViewMode.Insert);
                        break;

                    case "ReadOnly":
                        fvRegulation.ChangeMode(FormViewMode.ReadOnly);
                        break;
                }
            }
        }

        protected void fvRegulation_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
        {
            Response.Write("<script>window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");
            Response.End();
        }

        protected void fvRegulation_ItemInserted(object sender, FormViewInsertedEventArgs e)
        {
            Response.Write("<script>window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");
            Response.End();
        }

        protected void fvRegulation_ItemDeleted(object sender, FormViewDeletedEventArgs e)
        {
            Response.Write("<script>window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");
            Response.End();
        }
    }
}