using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
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

namespace RailExamWebApp.RandomExam
{
    public partial class RandomExamAnswerCurrent : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string orgID = Request.QueryString.Get("orgid");
            string strId = Request.QueryString.Get("id");
            ViewState["NowOrgID"] = Convert.ToInt32(ConfigurationManager.AppSettings["StationID"]);

            if (!string.IsNullOrEmpty(strId))
            {
                FillHeading(strId, orgID);
            }
        }

        protected void FillHeading(string strId,string orgid)
        {
            RandomExamResultCurrentBLL randomExamResultBLL = new RandomExamResultCurrentBLL();
            RandomExamResultCurrent randomExamResult = new RandomExamResultCurrent();
            randomExamResult = randomExamResultBLL.GetRandomExamResult(int.Parse(strId));

            string strOrgName = randomExamResult.OrganizationName;
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

            lblOrg.Text = strStationName;
            lblWorkShop.Text = strOrgName1;
            lblPost.Text = randomExamResult.PostName;
            lblName.Text = randomExamResult.ExamineeName;
            lblTime.Text = randomExamResult.BeginDateTime.ToString("yyyy-MM-dd HH:mm");
            lblScore.Text = System.String.Format("{0:0.##}",randomExamResult.Score);

            int RandomExamId = randomExamResult.RandomExamId;
            RandomExamBLL randomExamBLL = new RandomExamBLL();
            RailExam.Model.RandomExam randomExam = randomExamBLL.GetExam(RandomExamId);

            if (randomExam != null)
            {
                lblTitle.Text = randomExam.ExamName;
            }

            RandomExamSubjectBLL randomExamSubjectBLL = new RandomExamSubjectBLL();
            IList<RailExam.Model.RandomExamSubject> RandomExamSubjects = randomExamSubjectBLL.GetRandomExamSubjectByRandomExamId(RandomExamId);

            int nItemCount = 0;
            decimal nTotalScore = 0;
            for (int i = 0; i < RandomExamSubjects.Count; i++)
            {
                nItemCount += RandomExamSubjects[i].ItemCount;
                nTotalScore += RandomExamSubjects[i].ItemCount * RandomExamSubjects[i].UnitScore;
            }

            lblTitleRight.Text = "�ܹ�" + nItemCount + "�⣬��" +  System.String.Format("{0:0.##}",nTotalScore) + "��";
        }

        private string GetNo(int i)
        {
            string strReturn = "";
            switch (i.ToString())
            {
                case "0":
                    strReturn = "һ";
                    break;
                case "1":
                    strReturn = "��";
                    break;
                case "2":
                    strReturn = "��";
                    break;
                case "3":
                    strReturn = "��";
                    break;
                case "4":
                    strReturn = "��";
                    break;
                case "5":
                    strReturn = "��";
                    break;
                case "6":
                    strReturn = "��";
                    break;
                case "7":
                    strReturn = "��";
                    break;
                case "8":
                    strReturn = "��";
                    break;
                case "9":
                    strReturn = "ʮ";
                    break;
                case "10":
                    strReturn = "ʮһ";
                    break;
                case "11":
                    strReturn = "ʮ��";
                    break;
                case "12":
                    strReturn = "ʮ��";
                    break;
                case "13":
                    strReturn = "ʮ��";
                    break;
                case "14":
                    strReturn = "ʮ��";
                    break;
                case "15":
                    strReturn = "ʮ��";
                    break;
                case "16":
                    strReturn = "ʮ��";
                    break;
                case "17":
                    strReturn = "ʮ��";
                    break;
                case "18":
                    strReturn = "ʮ��";
                    break;
                case "19":
                    strReturn = "��ʮ";
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
                return Convert.ToChar((intCol - 1) / 26 + 64).ToString()
                       + Convert.ToChar((intCol - 1) % 26 + 64 + 1).ToString();
            }
        }

        protected void FillPaper()
        {
            // QueryString id stands for EXAM_RESULT_ID
            string strId = Request.QueryString.Get("id");
            string orgid = Request.QueryString.Get("orgid");
            // Not pass id
            if (string.IsNullOrEmpty(strId))
            {
                SessionSet.PageMessage = "��������";

                return;
            }

            RandomExamResultCurrentBLL randomExamResultBLL = new RandomExamResultCurrentBLL();
            RandomExamResultCurrent randomExamResult = new RandomExamResultCurrent();
            randomExamResult = randomExamResultBLL.GetRandomExamResult(int.Parse(strId));

            int RandomExamId = randomExamResult.RandomExamId;
            int randomExamResultId = int.Parse(strId);

			RandomExamBLL objBll = new RandomExamBLL();
        	int year = objBll.GetExam(RandomExamId).BeginTime.Year;

            RandomExamItemBLL randomItemBLL = new RandomExamItemBLL();
            RandomExamSubjectBLL subjectBLL = new RandomExamSubjectBLL();
            RandomExamResultAnswerCurrentBLL randomExamResultAnswerBLL = new RandomExamResultAnswerCurrentBLL();
            IList<RandomExamSubject> randomExamSubjects = subjectBLL.GetRandomExamSubjectByRandomExamId(RandomExamId);
            IList<RandomExamResultAnswerCurrent> examResultAnswers = new List<RandomExamResultAnswerCurrent>();
            examResultAnswers = randomExamResultAnswerBLL.GetExamResultAnswersCurrent(randomExamResultId);
            
            if (randomExamSubjects != null)
            {
                for (int i = 0; i < randomExamSubjects.Count; i++)
                {
                    RandomExamSubject paperSubject = randomExamSubjects[i];
                    IList<RandomExamItem> PaperItems = new List<RandomExamItem>();
                    PaperItems = randomItemBLL.GetItemsCurrent(paperSubject.RandomExamSubjectId, randomExamResultId,year);
                    
                    Response.Write("<table width='100%'>");
                    Response.Write(" <tr > <td class='ExamBigTitle' >");
                    Response.Write(" " + GetNo(i) + "");
                    Response.Write(".&nbsp;" + paperSubject.SubjectName + "");
                    Response.Write("  ����" + paperSubject.ItemCount + "�⣬��" +  System.String.Format("{0:0.##}",paperSubject.ItemCount * paperSubject.UnitScore) + "�֣�</td></tr >");

                    if (PaperItems != null)
                    {
                        for (int j = 0; j < PaperItems.Count; j++)
                        {
                            RandomExamItem paperItem = PaperItems[j];
                            int k = j + 1;

                            Response.Write("<tr > <td class='ExamResultItem'>&nbsp;&nbsp;&nbsp;"
                                           + k + ".&nbsp; " + paperItem.Content + "&nbsp;&nbsp;��" +  System.String.Format("{0:0.##}",paperSubject.UnitScore) +
                                           "�֣�</td></tr >");

                            // ��֯�û���
                            RandomExamResultAnswerCurrent theExamResultAnswer = null;
                            string[] strUserAnswers = new string[0];
                            string strUserAnswer = string.Empty;

                            foreach (RandomExamResultAnswerCurrent resultAnswer in examResultAnswers)
                            {
                                if (resultAnswer.RandomExamItemId == paperItem.RandomExamItemId)
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
                            if (theExamResultAnswer.Answer != null || theExamResultAnswer.Answer == string.Empty)
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
                                    string strij = "-" + paperItem.RandomExamItemId + "-" + i.ToString() + "-"
                                                   + j.ToString() + "-" + n.ToString();
                                    string strName = i.ToString() + j.ToString();

                                    Response.Write(" <tr ><td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; "
                                                   +  strN + "." + strAnswer[n] + "</td></tr >");
                                }
                            }
                            else
                            {
                                //��ѡ


                                string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
                                for (int n = 0; n < strAnswer.Length; n++)
                                {
                                    string strN = intToString(n + 1);
                                    string strij = "-" + paperItem.RandomExamItemId + "-" + i.ToString() + "-" + j.ToString()
                                                   + "-" + n.ToString();
                                    string strName = i.ToString() + j.ToString();

                                    Response.Write("<tr > <td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; "
                                                   + strN + "." + strAnswer[n] + "</td></tr >");
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

                            string strScore = "0";
                            if (strRightAnswer == strUserAnswer)
                            {
								strScore = System.String.Format("{0:0.##}",paperSubject.UnitScore);
							}

							Response.Write(" <tr><td class='ExamAnswer'>&nbsp;&nbsp;&nbsp;���׼�𰸣�"
                                           + "<span id='span-" + paperItem.RandomExamItemId + "-0' name='span-" +
                                           paperItem.RandomExamItemId
                                           + "'>" + strRightAnswer + "</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�����𰸣�"
                                           + "<span id='span-" + paperItem.RandomExamItemId + "-1' name='span-" +
                                           paperItem.RandomExamItemId
                                           + "'>" + strUserAnswer + "</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�÷֣�" + strScore + "</td></tr>");
                        }
                    }
                    Response.Write(" </table> ");
                }
            }
            else
            {
                SessionSet.PageMessage = "���ݴ���";

                return;
            }
        }
    }
}
