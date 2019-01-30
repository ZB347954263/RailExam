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
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Exam
{
    public partial class ExamGradeList : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack )
            {
                if (PrjPub.IsServerCenter && SessionSet.OrganizationID != 1)
                {
                    HfUpdateRight.Value = "False";
                }
                else
                {
                    HfUpdateRight.Value = PrjPub.HasEditRight("考试成绩").ToString();
                }
            }
        }

        #region // ComponetArt CallBack Methods

        protected void searchExamCallBack_Callback(object sender, CallBackEventArgs e)
        {
            examsGrid.DataBind();
            examsGrid.RenderControl(e.Output);
        }

        #endregion
    }
}