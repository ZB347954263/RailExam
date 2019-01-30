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
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Exam
{
    public partial class ExamResultUpdateList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.IsServerCenter && SessionSet.OrganizationID != 1)
                {
                    HfUpdateRight.Value = "False";
                    HfDeleteRight.Value = "False";
                }
                else
                {
                    HfUpdateRight.Value = PrjPub.HasEditRight("手工评卷").ToString();
                    HfDeleteRight.Value = PrjPub.HasDeleteRight("手工评卷").ToString();
                }               
            }
            else
            {   
                if (Request.Form.Get("Refresh") == "true")
                {
                    this.examsGrid.DataBind();
                }
            }
        }
    }
}
