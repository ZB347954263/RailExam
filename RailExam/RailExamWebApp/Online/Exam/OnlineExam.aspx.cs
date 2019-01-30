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
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Online.Exam
{
	public partial class OnlineExam : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
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

				
				//foreach (RailExam.Model.Exam exam in ExamList)
				//{
				//    exam.ExamName = "<a onclick=AttendExam('" + exam.ExamId + "','" + exam.paperId + "','" + exam.ExamType + "') href=# title='参加考试' > " + exam.ExamName + " </a>";
				//}
				//gvExam.DataSource = ExamList;
				//gvExam.DataBind();
			}
		}

		public void FillPage()
		{
			int orgID;
			if (PrjPub.IsServerCenter)
			{
				orgID = 0;
			}
			else
			{
				orgID = Convert.ToInt32(ConfigurationManager.AppSettings["StationID"]);
			}

            if (hfEmployeeID.Value != string.Empty)
            {
                ExamBLL bkLL = new ExamBLL();
                IList<RailExam.Model.Exam> ExamList = bkLL.GetExamByUserId(hfEmployeeID.Value, orgID, PrjPub.ServerNo);

                string str = "";

                foreach (RailExam.Model.Exam exam in ExamList)
                {
                    str +=
                        "<tr><td align='center' style='border: solid 1px #E0E0E0;padding: 5px;white-space:nowrap;'><a onclick=AttendExam('" +
                        exam.ExamId + "','" + exam.paperId + "','" + exam.ExamType +
                        "') href=# title='参加考试' style='font-size:x-large;color: red;cursor:hand; font-weight:bold;text-decoration: underline'> " +
                        exam.ExamName + " </a></td>";

                    str += "<td align='center' style='border: solid 1px #E0E0E0;padding: 5px;white-space:nowrap;'>" +
                           exam.BeginTime.ToString("f") + "——" + exam.EndTime.ToString("f") + "</td></tr>";
                }

                Response.Write(str);
            }
		}
	}
}
