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
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common;

namespace RailExamWebApp.RandomExam
{
	public partial class AttendExamEnd : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				ViewState["EndTime"] = Request.QueryString.Get("EndTime");
			}

			if (hfAnswer.Value != string.Empty)
			{
				//Response.Write(hfAnswer.Value);
				SaveAnswerToDB(hfAnswer.Value);
			}
		}

		private void SaveAnswerToDB(string strAnswer)
		{
			try
			{
				string strId = Request.QueryString.Get("ExamID");

				RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
				RailExam.Model.RandomExamResultCurrent objResultCurrent =
					objResultCurrentBll.GetNowRandomExamResultInfo(Convert.ToInt32(Request.QueryString["StudentID"].ToString()),
					                                               Convert.ToInt32(strId));

				//���¿��Գɼ���ʱ���������ӦΪվ�εĳɼ��������ID
				objResultCurrent.RandomExamResultId = int.Parse(Request.QueryString["RandomExamResultID"].ToString());
				objResultCurrent.RandomExamId = int.Parse(strId);
				objResultCurrent.AutoScore = 0;
				objResultCurrent.CurrentDateTime = DateTime.Parse(ViewState["EndTime"].ToString());
				objResultCurrent.ExamTime = Convert.ToInt32(Request.QueryString["ExamTime"]);
				objResultCurrent.EndDateTime = DateTime.Parse(ViewState["EndTime"].ToString());
				objResultCurrent.Score = 0;
				objResultCurrent.OrganizationId = int.Parse(Request.QueryString["OrgID"].ToString());
				objResultCurrent.Memo = "";
				objResultCurrent.StatusId = 2;
				objResultCurrent.AutoScore = 0;
				objResultCurrent.CorrectRate = 0;
				objResultCurrent.ExamineeId = int.Parse(Request.QueryString["StudentID"].ToString());

				string[] str1 = strAnswer.Split(new char[] {'$'});
				RandomExamResultAnswerCurrentBLL randomExamResultAnswerBLL = new RandomExamResultAnswerCurrentBLL();
				//randomExamResultAnswerBLL.DeleteExamResultAnswerCurrent(Convert.ToInt32(ViewState["RandomExamResultID"].ToString()));
				IList<RandomExamResultAnswerCurrent> randomExamResultAnswers = new List<RandomExamResultAnswerCurrent>();
				int randomExamResultId = int.Parse(Request.QueryString["RandomExamResultID"].ToString());
				for (int n = 0; n < str1.Length; n++)
				{
					string str2 = str1[n].ToString();
					string[] str3 = str2.Split(new char[] {'|'});
					string strPaperItemId = str3[0].ToString();
					string strTrueAnswer = str2.ToString().Substring(strPaperItemId.Length + 1);

					RandomExamResultAnswerCurrent randomExamResultAnswer = new RandomExamResultAnswerCurrent();
					randomExamResultAnswer.RandomExamResultId = randomExamResultId;
					randomExamResultAnswer.RandomExamItemId = int.Parse(strPaperItemId);
					randomExamResultAnswer.JudgeStatusId = 0;
					randomExamResultAnswer.JudgeRemark = string.Empty;
					randomExamResultAnswer.ExamTime = 0;
					randomExamResultAnswer.Answer = strTrueAnswer;
					randomExamResultAnswers.Add(randomExamResultAnswer);
				}
				//��ʵʱ���Դ��ɾ���������տ��Դ�����
				randomExamResultAnswerBLL.AddExamResultAnswerCurrentSave(randomExamResultId, randomExamResultAnswers);

				//����ʵʱ���Լ�¼
				objResultCurrentBll.UpdateRandomExamResultCurrent(objResultCurrent);

				//��ʵʱ���Լ�¼����ʱ��ת�浽��ʽ���Գɼ���ʹ���
				RandomExamResultBLL objResultBll = new RandomExamResultBLL();
				int randomExamResultID =
					objResultBll.RemoveResultAnswer(Convert.ToInt32(Request.QueryString["RandomExamResultID"].ToString()));

				//�����վ�����浽�濼���ԣ��ɼ��Զ��ϴ���·��
				//if(ViewState["NowStartMode"].ToString() == PrjPub.START_MODE_NO_CONTROL.ToString() && !PrjPub.IsServerCenter)
				//{
				//    objResultBll.RemoveRandomExamResultToServer(Convert.ToInt32(strId), Convert.ToInt32(ConfigurationManager.AppSettings["StationID"]));
				//}

				Response.Write(
					"<script>window.dialogArguments.parent.location = '/RailExamBao/Online/Exam/ExamSuccess.aspx?ExamType=1&ExamResultID=" +
					randomExamResultID + "';window.close();</script>");
			}
			catch
			{
				SessionSet.PageMessage = "�ύʧ�ܣ�";
				btnUpload.Visible = true;
				btnClose.Visible = true;
			}
		}

		protected  void btnUpload_Click(Object sender,EventArgs e)
		{
			btnUpload.Visible = false;
			btnClose.Visible = false;
			SaveAnswerToDB(hfAnswer.Value);
		}
	}
}
