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
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Exam
{
    public partial class ExamJudgeList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //HfUpdateRight.Value = PrjPub.HasEditRight("ÊÖ¹¤ÆÀ¾í").ToString();

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
