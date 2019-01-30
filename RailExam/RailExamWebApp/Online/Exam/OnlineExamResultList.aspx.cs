using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;
using mshtml;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;
using System.Collections.Generic;
using DSunSoft.Web.Global;
using Word;

namespace RailExamWebApp.Online.Exam
{
	public partial class OnlineExamResultList : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				lblName.Text = PrjPub.CurrentStudent.EmployeeName;
				OrganizationBLL objOrgBll = new OrganizationBLL();
				lblOrg.Text = objOrgBll.GetOrganization(PrjPub.CurrentStudent.StationOrgID).ShortName;
				lblPost.Text = PrjPub.CurrentStudent.PostName;

				ExamResultBLL objBll = new ExamResultBLL();
                ViewState["NowOrgID"] = Convert.ToInt32(ConfigurationManager.AppSettings["StationID"]);
				IList<RailExam.Model.ExamResult> objList = objBll.GetExamResults(PrjPub.StudentID, 0);
				gvExam.DataSource = objList;
				gvExam.DataBind();

				hfOrgID.Value = ConfigurationManager.AppSettings["StationID"].ToString();
				hfIsServer.Value = PrjPub.IsServerCenter.ToString();

				BindGrid();
			}
			else
			{
				if (Request.Form.Get("OutPut") != null && Request.Form.Get("OutPut") != "")
				{
                    OutputData(Request.Form.Get("OutPut"), Request.Form.Get("OutPutOrg"));
				}

				if (Request.Form.Get("OutPutRandom") != null && Request.Form.Get("OutPutRandom") != "")
				{
					OutputRandomExam(Request.Form.Get("OutPutRandom"));
				}
			}
		}

		private void BindGrid()
		{
			int employeeID = Convert.ToInt32(PrjPub.StudentID);
			hfEmployeeID.Value = PrjPub.StudentID.ToString();
			RandomExamStatisticBLL objBll = new RandomExamStatisticBLL();
			IList<RailExam.Model.RandomExamStatistic> objList =
				objBll.GetErrorItemInfoByEmployeeID(employeeID, DateTime.Today, DateTime.Today);
			examsGrid.DataSource = objList;
			examsGrid.DataBind();
		}

		private void OutputData(string strId,string orgid)
		{
			string str = GetExamOutString(strId,orgid);
			Response.Clear();
			Response.Charset = "utf-7";
			Response.Buffer = true;
			EnableViewState = false;
			Response.AppendHeader("Content-Disposition", "attachment;filename=ExamResult.doc");

			Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-7");
			Response.ContentType = "application/ms-word";
			Response.Write(str);
			Response.End();
		}

		private void OutputRandomExam(string strName)
		{
			ExamResultBLL objBll = new ExamResultBLL();
			IList<RailExam.Model.ExamResult> objList = objBll.GetExamResults(PrjPub.StudentID, 0);
			gvExam.DataSource = objList;
			gvExam.DataBind();

			string filename = Server.MapPath("/RailExamBao/Excel/Word.doc");
			if (File.Exists(filename))
			{
				FileInfo file = new FileInfo(filename.ToString());
				this.Response.Clear();
				this.Response.Buffer = true;
				this.Response.Charset = "utf-7";
				this.Response.ContentEncoding = Encoding.UTF7;
				// ���ͷ��Ϣ��Ϊ"�ļ�����/���Ϊ"�Ի���ָ��Ĭ���ļ���
				this.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(strName) + ".doc");
				// ���ͷ��Ϣ��ָ���ļ���С����������ܹ���ʾ���ؽ���
				this.Response.AddHeader("Content-Length", file.Length.ToString());
				// ָ�����ص���һ�����ܱ��ͻ��˶�ȡ���������뱻����
				this.Response.ContentType = "application/ms-word";
				// ���ļ������͵��ͻ���
				this.Response.WriteFile(file.FullName);
			}
		}

		private string GetExamOutString(string strId,string orgid)
		{
			ExamResultBLL examResultBLL = new ExamResultBLL();
			PaperBLL kBLL = new PaperBLL();
			RailExam.Model.ExamResult examResult = new RailExam.Model.ExamResult();
            if(ViewState["NowOrgID"].ToString() == orgid)
            {
                examResult = examResultBLL.GetExamResult(Convert.ToInt32(strId));
            }
            else
            {
                examResult = examResultBLL.GetExamResultByOrgID(Convert.ToInt32(strId), Convert.ToInt32(orgid));
            }
			RailExam.Model.Paper paper = null;
			EmployeeBLL ebLL = new EmployeeBLL();
			RailExam.Model.Employee Employee = ebLL.GetEmployee(examResult.ExamineeId);
			paper = kBLL.GetPaper(examResult.PaperId);

			PaperItemBLL paperItemBLL = new PaperItemBLL();
			IList<RailExam.Model.PaperItem> paperItems = paperItemBLL.GetItemsByPaperId(paper.PaperId);
			int nItemCount = paperItems.Count;

			decimal nTotalScore = 0;

			for (int i = 0; i < paperItems.Count; i++)
			{
				nTotalScore += paperItems[i].Score;
			}



			string strOrgName = Employee.OrgName;
			string strStationName = "";
			string strOrgName1 = "";
			int n = strOrgName.IndexOf("-");
			if (n != -1)
			{
				strStationName = strOrgName.Substring(0, n);
				strOrgName1 = strOrgName.Substring(n + 1);
			}
			else
			{
				strStationName = strOrgName;
				strOrgName1 = "";
			}

			string str = "<div style='text-align:center;font-size:18pt;'>"+ PrjPub.GetRailName() +"�����Ծ�</div>";
            str += "<div style='text-align:left;font-size:10.5pt;'>��������:" + paper.PaperName + " </div>";
            str += "<div style='text-align:right;font-size:10.5pt;'>�ܹ�" + nItemCount + "�⣬�� " + nTotalScore + "��</div>";
            str += "<div style='text-align:center;'><table width='100%' border='1' cellpadding='0' cellspacing='0' >";
			str += "<tr><td  width='7%' style='font-size:10.5pt' >��λ:</td>";
			str += "<td  width='26%' style='font-size:10.5pt' align='left' >" + strStationName + "</td>";
			str += "<td  width='7%' style='font-size:10.5pt' >����:</td>";
			str += "<td  width='27%' style='font-size:10.5pt' align='left' >" + strOrgName1 + "</td>";
			str += "<td  width='7%' style='font-size:10.5pt' >ְ��:</td>";
			str += "<td  width='26%' style='font-size:10.5pt' align='left' >" + Employee.PostName + "</td></tr>";
			str += "<tr><td  width='7%' style='font-size:10.5pt' >����:</td>";
			str += "<td  width='26%' style='font-size:10.5pt' align='left' >" + Employee.EmployeeName + "</td>";
			str += "<td  width='7%' style='font-size:10.5pt' >ʱ��:</td>";
			str += "<td  width='27%' style='font-size:10.5pt' align='left' >" + examResult.BeginDateTime.ToString("yyyy-MM-dd HH:mm") + "</td>";
			str += "<td  width='7%' style='font-size:10.5pt' >�ɼ�:</td>";
            str += "<td  width='26%' style='font-size:10.5pt' align='left' >" + examResult.Score + "</td></tr></table></div>";
		    str += "<br>";
			str += GetFillExamPaperString(strId,orgid);

            string strReplace;
            if (PrjPub.IsServerCenter)
            {
                strReplace = "http://" + ConfigurationManager.AppSettings["ServerIP"] + "/RailExamBao/";
            }
            else
            {
                strReplace = "http://" + ConfigurationManager.AppSettings["StationIP"] + "/RailExamBao/";
            }
            str = str.Replace("/RailExamBao/", strReplace);

            return str;
		}

		protected string GetFillExamPaperString(string strId,string orgid)
		{
			string strPaperString = "";
			PaperItemBLL kBLL = new PaperItemBLL();
			PaperSubjectBLL kBSLL = new PaperSubjectBLL();
			ExamResultBLL examResultBLL = new ExamResultBLL();
			ExamResultAnswerBLL examResultAnswerBLL = new ExamResultAnswerBLL();
			RailExam.Model.ExamResult examResult = new RailExam.Model.ExamResult();
            if(ViewState["NowOrgID"].ToString() == orgid)
            {
                examResult = examResultBLL.GetExamResult(Convert.ToInt32(strId));
            }
            else
            {
                examResult = examResultBLL.GetExamResultByOrgID(Convert.ToInt32(strId), Convert.ToInt32(orgid));
            }
            IList<PaperSubject> PaperSubjects = new List<PaperSubject>();
            if (ViewState["NowOrgID"].ToString() == orgid)
            {
                PaperSubjects = kBSLL.GetPaperSubjectByPaperId(examResult.PaperId);
            }
            else
            {
                PaperSubjects = kBSLL.GetPaperSubjectByPaperIdByOrgID(examResult.PaperId, Convert.ToInt32(orgid));
            }
            RailExam.Model.PaperSubject paperSubject = null;
			IList<RailExam.Model.PaperItem> PaperItems = null;
            IList<ExamResultAnswer> examResultAnswers = new List<ExamResultAnswer>();

            if (ViewState["NowOrgID"].ToString() == orgid)
            {
                examResultAnswers = examResultAnswerBLL.GetExamResultAnswers(examResult.ExamResultIDStation);
            }
            else
            {
                examResultAnswers = examResultAnswerBLL.GetExamResultAnswersByOrgID(examResult.ExamResultIDStation, Convert.ToInt32(orgid));
            }

			for (int i = 0; i < PaperSubjects.Count; i++)
			{
				paperSubject = PaperSubjects[i];
                if (ViewState["NowOrgID"].ToString() == orgid)
                {
                    PaperItems = kBLL.GetItemsByPaperSubjectId(paperSubject.PaperSubjectId);
                }
                else
                {
                    PaperItems = kBLL.GetItemsByPaperSubjectIdByOrgID(paperSubject.PaperSubjectId, Convert.ToInt32(orgid));
                }
                strPaperString += " <table width='100%' border='0' cellpadding='0' cellspacing='0'>";
				strPaperString += " <tr><td  style='font-size:14pt'>";
				strPaperString += " " + GetNo(i) + "";
				strPaperString += ".&nbsp;" + paperSubject.SubjectName + "";
				strPaperString += "  ����" + paperSubject.ItemCount + "�⣬��" + paperSubject.ItemCount * paperSubject.UnitScore + "�֣�</td></tr >";

				if (PaperItems != null)
				{
					for (int j = 0; j < PaperItems.Count; j++)
					{
						RailExam.Model.PaperItem paperItem = PaperItems[j];
						int k = j + 1;

						strPaperString += "<tr > <td style='font-size:10.5pt'>&nbsp;&nbsp;&nbsp;"
									   + k + ".&nbsp; " + paperItem.Content + "&nbsp;&nbsp;��" + paperSubject.UnitScore +
									   "�֣�</td></tr >";

						// ��֯�û���
						RailExam.Model.ExamResultAnswer theExamResultAnswer = null;
						string[] strUserAnswers = new string[0];
						string strUserAnswer = string.Empty;

						foreach (RailExam.Model.ExamResultAnswer resultAnswer in examResultAnswers)
						{
							if (resultAnswer.PaperItemId == paperItem.PaperItemId)
							{
								theExamResultAnswer = resultAnswer;
								break;
							}
						}

						// ���ӱ��޼�¼������ҳ�����


						if (theExamResultAnswer == null)
						{
							SessionSet.PageMessage = "���ݴ���";
						}

						// ������֯������
						if (theExamResultAnswer.Answer != null)
						{
							strUserAnswers = theExamResultAnswer.Answer.Split(new char[] { '|' });
						}
						for (int n = 0; n < strUserAnswers.Length; n++)
						{
							string strN = intToString(int.Parse(strUserAnswers[n]) + 1);
							if (n == 0)
							{
								strUserAnswer += strN;
							}
							else
							{
								strUserAnswer += "," + strN;
							}
						}

						//��ѡ


						if (paperSubject.ItemTypeId == 2)
						{
							string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
							for (int n = 0; n < strAnswer.Length; n++)
							{
								string strN = intToString(n + 1);
								string strij = "-" + paperItem.PaperItemId + "-" + i.ToString() + "-"
											   + j.ToString() + "-" + n.ToString();
								string strName = i.ToString() + j.ToString();

								strPaperString += " <tr ><td style='font-size:10.5pt'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; "
											   + strN + "." + strAnswer[n] + "</td></tr >";
							}
						}
						else
						{
							//��ѡ

							string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
							for (int n = 0; n < strAnswer.Length; n++)
							{
								string strN = intToString(n + 1);
								string strij = "-" + paperItem.PaperItemId + "-" + i.ToString() + "-" + j.ToString()
											   + "-" + n.ToString();
								string strName = i.ToString() + j.ToString();

								strPaperString += "<tr > <td style='font-size:10.5pt'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; "
											   + strN + "." + strAnswer[n] + "</td></tr >";
							}
						}

						// ��֯��ȷ��
						string[] strRightAnswers = paperItem.StandardAnswer.Split(new char[] { '|' });
						string strRightAnswer = "";
						for (int n = 0; n < strRightAnswers.Length; n++)
						{
							string strN = intToString(int.Parse(strRightAnswers[n]) + 1);
							if (n == 0)
							{
								strRightAnswer += strN;
							}
							else
							{
								strRightAnswer += "," + strN;
							}
						}

						strPaperString += " <tr><td style='font-size:10.5pt'>&nbsp;&nbsp;&nbsp;���׼�𰸣�"
									   + "<span id='span-" + paperItem.PaperItemId + "-0' name='span-" +
									   paperItem.PaperItemId
									   + "'>" + strRightAnswer + "</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�����𰸣�"
									   + "<span id='span-" + paperItem.PaperItemId + "-1' name='span-" +
									   paperItem.PaperItemId
									   + "'>" + strUserAnswer + "</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�÷֣�&nbsp;" + theExamResultAnswer.JudgeScore.ToString() + "</td></tr>";
					}
				}
				strPaperString += " </table> ";
			}

			return strPaperString;
		}

		private string GetNo(int i)
		{
			string strReturn = string.Empty;

			switch (i.ToString())
			{
				case "0": strReturn = "һ";
					break;
				case "1": strReturn = "��";
					break;
				case "2": strReturn = "��";
					break;
				case "3": strReturn = "��";
					break;
				case "4": strReturn = "��";
					break;
				case "5": strReturn = "��";
					break;
				case "6": strReturn = "��";
					break;
				case "7": strReturn = "��";
					break;
				case "8": strReturn = "��";
					break;
				case "9": strReturn = "ʮ";
					break;
				case "10": strReturn = "ʮһ";
					break;
				case "11": strReturn = "ʮ��";
					break;
				case "12": strReturn = "ʮ��";
					break;
				case "13": strReturn = "ʮ��";
					break;
				case "14": strReturn = "ʮ��";
					break;
				case "15": strReturn = "ʮ��";
					break;
				case "16": strReturn = "ʮ��";
					break;
				case "17": strReturn = "ʮ��";
					break;
				case "18": strReturn = "ʮ��";
					break;
				case "19": strReturn = "��ʮ";
					break;
			}
			return strReturn;
		}

		private string intToString(int intCol)
		{
			if (intCol < 27)
			{
				return Convert.ToChar(intCol + 64).ToString();
			}
			else
			{
				return Convert.ToChar((intCol - 1) / 26 + 64).ToString() + Convert.ToChar((intCol - 1) % 26 + 64 + 1).ToString();
			}
		}

		protected void CallBack1_CallBack(object sender, CallBackEventArgs e)
		{
			int i = 2;
		}
	}
}
