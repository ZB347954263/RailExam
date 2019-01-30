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
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Exam
{
    public partial class ExamPaperList : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }

                if (PrjPub.IsServerCenter && SessionSet.OrganizationID != 1)
                {
                    HfUpdateRight.Value = "False";
                }
                else
                {
                    HfUpdateRight.Value = PrjPub.HasEditRight("手工评卷").ToString();
                }

                hfJudgeId.Value = PrjPub.CurrentLoginUser.EmployeeID.ToString();

                string strId = Request.QueryString.Get("eid");
                hfExamID.Value = strId;
                RailExam.Model.Exam exam = new RailExam.Model.Exam();
                ExamBLL eBll = new ExamBLL();
                exam = eBll.GetExam(int.Parse(strId));
                TextBoxExamCategory.Text = exam.CategoryName;
                TextBoxExamName.Text = exam.ExamName;
                TextBoxExamTime.Text = exam.BeginTime.ToString() + "/" + exam.EndTime.ToString();
            }
            else
            {
                papersGrid.DataBind();
            }
            

            string strRefresh = Request.Form.Get("Refresh");
            if (strRefresh != null && strRefresh != "")
            {
                papersGrid.DataBind();
            }

        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            papersGrid.DataBind();
        }
      
    }
}
