using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;
using System.Collections.Generic;
using ComponentArt.Web.UI;

namespace RailExamWebApp.Online.Exam
{
    public partial class OnlineExamList : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack && !CallBack1.IsCallback)
            {
                string struesrId = PrjPub.StudentID;
            	hfEmployeeID.Value = struesrId;
            	try
            	{
					lblName.Text = PrjPub.CurrentStudent.EmployeeName;
				}
            	catch
            	{
					Response.Redirect("/RailExamBao/Common/Error.aspx?error=超时时间已到，请重新登录");
            	}
				OrganizationBLL objOrgBll = new OrganizationBLL();
				lblOrg.Text = objOrgBll.GetOrganization(PrjPub.CurrentStudent.StationOrgID).ShortName;
            	lblPost.Text = PrjPub.CurrentStudent.PostName;

            	int orgID;
				if(PrjPub.IsServerCenter)
				{
					orgID = 0;
				}
				else
				{
					orgID = Convert.ToInt32(ConfigurationManager.AppSettings["StationID"]);
				}
                ExamBLL bkLL = new ExamBLL();
                IList<RailExam.Model.Exam> ExamList = bkLL.GetExamByUserId(struesrId, orgID, PrjPub.ServerNo);
            	foreach (RailExam.Model.Exam exam in ExamList)
            	{
					exam.ExamName = "<a onclick=AttendExam('" + exam.ExamId + "','" + exam.paperId + "','" + exam.ExamType + "') href=# title='参加考试' > " + exam.ExamName + " </a>";
            	}
                gvExam.DataSource = ExamList;
                gvExam. DataBind();
            }
        }

		protected  void CallBack1_CallBack(object sender, CallBackEventArgs e)
		{
			int i = 2;
		}
    }
}
