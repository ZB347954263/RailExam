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

namespace RailExamWebApp.Task
{
    public partial class TaskGrade : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void dvTaskResult_OnModeChanged(object s, EventArgs e)
        {
            if (dvTaskResult.CurrentMode == DetailsViewMode.ReadOnly)
            {
                editButton.Visible = true;
                updateButton.Visible = false;
                updateCancelButton.Visible = false;
            }
            else if (dvTaskResult.CurrentMode == DetailsViewMode.Edit)
            {
                editButton.Visible = false;
                updateButton.Visible = true;
                updateCancelButton.Visible = true;
            }
        }
    }
}