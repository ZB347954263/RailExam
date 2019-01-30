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

namespace RailExamWebApp.Exam
{
    public partial class ExamResult : System.Web.UI.Page
    {
        private static string _examJudgeInputs = string.Empty;

        public static int JudgeStatusCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // QueryString id stands for EXAM_RESULT_ID
                string orgID = Request.QueryString.Get("orgID");
                string strId = Request.QueryString.Get("id");
                if (orgID == "1")
                {
                    Response.Redirect("../Online/Exam/ExamResult.aspx?id=" + strId);
                }

                ViewState["OrgID"] = orgID;

                if (!string.IsNullOrEmpty(strId))
                {
                    FillHeading(strId);
                }
            }
        }

        public static string ExamJudgeInputs
        {
            get
            {
                if (string.IsNullOrEmpty(_examJudgeInputs))
                {
                    ExamJudgeStatusBLL examJudgeStatusBLL = new ExamJudgeStatusBLL();
                    IList<ExamJudgeStatus> examJudgeStatuses = examJudgeStatusBLL.GetExamJudgeStatuses();

                    // 评分输入栏


                    string strExamJudgInputs = string.Empty;
                    int index = 0;
                    foreach (ExamJudgeStatus judgeStatus in examJudgeStatuses)
                    {
                        index++;
                        strExamJudgInputs += "<input type='radio' disabled id='rbnJudgeStatus-{0}-" + (index - 1)
                                             + "' name='rbnJudge" + "-{0}' rate='" + judgeStatus.ScoreRate.ToString("0.00")
                                             + "' onclick='rbnJudgeStatus_onClick(this);' {" + index + "}>"
                                             + judgeStatus.StatusName + "</input>";
                    }
                    JudgeStatusCount = index;

                    return strExamJudgInputs;
                }
                else
                {
                    return _examJudgeInputs;
                }
            }
        }

        private static string GetJudgeInputs(int itemId, int statusId)
        {
            string judgeInputs = ExamJudgeInputs;
            object[] inputs = new object[JudgeStatusCount + 1];

            inputs[0] = itemId;
            for (int i = 1; i < inputs.Length; i++)
            {
                if (i == statusId + 1)
                {
                    inputs[i] = "checked";
                }
                else
                {
                    inputs[i] = string.Empty;
                }
            }

            return string.Format(judgeInputs, inputs);
        }
        protected void FillHeading(string strId)
        {
            ExamResultBLL examResultBLL = new ExamResultBLL();
            PaperBLL kBLL = new PaperBLL();
            RailExam.Model.ExamResult examResult = examResultBLL.GetExamResultByOrgID(Convert.ToInt32(strId), Convert.ToInt32(ViewState["OrgID"].ToString()));
            RailExam.Model.Paper paper = null;
            EmployeeBLL ebLL = new EmployeeBLL();
            RailExam.Model.Employee Employee = ebLL.GetEmployee(examResult.ExamineeId);

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

            lblOrg.Text = strStationName;
            lblWorkShop.Text = strOrgName1;
            lblPost.Text = Employee.PostName;
            lblName.Text = Employee.EmployeeName;
            lblTime.Text = examResult.BeginDateTime.ToString("yyyy-MM-dd HH:mm");
            lblScore.Text = examResult.Score.ToString();

            if (examResult != null)
            {
                paper = kBLL.GetPaper(examResult.PaperId);
            }


            PaperItemBLL paperItemBLL = new PaperItemBLL();
            IList<RailExam.Model.PaperItem> paperItems = paperItemBLL.GetItemsByPaperId(paper.PaperId);
            int nItemCount = paperItems.Count;

            decimal nTotalScore = 0;

            for (int i = 0; i < paperItems.Count; i++)
            {
                nTotalScore += paperItems[i].Score;
            }


            if (paper != null)
            {
                lblTitle.Text = paper.PaperName;
                lblTitleRight.Text = "总共 " + nItemCount + " 题，共 " + nTotalScore + " 分";
            }
        }

        private string GetNo(int i)
        {
            string strReturn = "";
            switch (i.ToString())
            {
                case "0":
                    strReturn = "一";
                    break;
                case "1":
                    strReturn = "二";
                    break;
                case "2":
                    strReturn = "三";
                    break;
                case "3":
                    strReturn = "四";
                    break;
                case "4":
                    strReturn = "五";
                    break;
                case "5":
                    strReturn = "六";
                    break;
                case "6":
                    strReturn = "七";
                    break;
                case "7":
                    strReturn = "八";
                    break;
                case "8":
                    strReturn = "九";
                    break;
                case "9":
                    strReturn = "十";
                    break;
                case "10":
                    strReturn = "十一";
                    break;
                case "11":
                    strReturn = "十二";
                    break;
                case "12":
                    strReturn = "十三";
                    break;
                case "13":
                    strReturn = "十四";
                    break;
                case "14":
                    strReturn = "十五";
                    break;
                case "15":
                    strReturn = "十六";
                    break;
                case "16":
                    strReturn = "十七";
                    break;
                case "17":
                    strReturn = "十八";
                    break;
                case "18":
                    strReturn = "十九";
                    break;
                case "19":
                    strReturn = "二十";
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
            // Not pass id
            if (string.IsNullOrEmpty(strId))
            {
                SessionSet.PageMessage = "参数错误！";

                return;
            }

            PaperItemBLL kBLL = new PaperItemBLL();
            PaperSubjectBLL kBSLL = new PaperSubjectBLL();
            ExamResultBLL examResultBLL = new ExamResultBLL();
            ExamResultAnswerBLL examResultAnswerBLL = new ExamResultAnswerBLL();
            RailExam.Model.ExamResult examResult = examResultBLL.GetExamResultByOrgID(Convert.ToInt32(strId), Convert.ToInt32(ViewState["OrgID"].ToString()));
            // Not found
            if (examResult == null)
            {
                SessionSet.PageMessage = "数据错误！";

                return;
            }

            IList<PaperSubject> PaperSubjects = kBSLL.GetPaperSubjectByPaperIdByOrgID(examResult.PaperId, Convert.ToInt32(ViewState["OrgID"].ToString()));
            PaperSubject paperSubject = null;
            IList<PaperItem> PaperItems = null;
            IList<ExamResultAnswer> examResultAnswers = examResultAnswerBLL.GetExamResultAnswersByOrgID(examResult.ExamResultIDStation, Convert.ToInt32(ViewState["OrgID"].ToString()));

            if (PaperSubjects != null)
            {
                for (int i = 0; i < PaperSubjects.Count; i++)
                {
                    paperSubject = PaperSubjects[i];
                    PaperItems = kBLL.GetItemsByPaperSubjectIdByOrgID(paperSubject.PaperSubjectId, Convert.ToInt32(ViewState["OrgID"].ToString()));
                    Response.Write("<table width='100%'>");
                    Response.Write(" <tr > <td class='ExamBigTitle' >");
                    Response.Write(" " + GetNo(i) + "");
                    Response.Write(".&nbsp;" + paperSubject.SubjectName + "");
                    Response.Write("  （共" + paperSubject.ItemCount + "题，共" + paperSubject.ItemCount * paperSubject.UnitScore + "分）</td></tr >");

                    if (PaperItems != null)
                    {
                        for (int j = 0; j < PaperItems.Count; j++)
                        {
                            PaperItem paperItem = PaperItems[j];
                            int k = j + 1;

                            Response.Write("<tr > <td class='ExamResultItem'>&nbsp;&nbsp;&nbsp;"
                                           + k + ".&nbsp; " + paperItem.Content + "&nbsp;&nbsp;（" + paperSubject.UnitScore +
                                           "分）</td></tr >");

                            // 组织用户答案
                            ExamResultAnswer theExamResultAnswer = null;
                            string[] strUserAnswers = new string[0];
                            string strUserAnswer = string.Empty;

                            foreach (ExamResultAnswer resultAnswer in examResultAnswers)
                            {
                                if (resultAnswer.PaperItemId == paperItem.PaperItemId)
                                {
                                    theExamResultAnswer = resultAnswer;
                                    break;
                                }
                            }

                            // 若子表无记录，结束页面输出


                            if (theExamResultAnswer == null)
                            {
                                SessionSet.PageMessage = "数据错误！";
                            }

                            // 否则组织考生答案
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

                            //多选


                            if (paperSubject.ItemTypeId == 2)
                            {
                                string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
                                for (int n = 0; n < strAnswer.Length; n++)
                                {
                                    string strN = intToString(n + 1);
                                    string strij = "-" + paperItem.PaperItemId + "-" + i.ToString() + "-"
                                                   + j.ToString() + "-" + n.ToString();
                                    string strName = i.ToString() + j.ToString();

                                    Response.Write(" <tr ><td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; "
                                                   + "<input type='checkbox' id='Answer" + strij + "' name='Answer" +
                                                   strName
                                                   + "' " + (Array.IndexOf(strUserAnswers, n) > -1 ? "checked" : "")
                                                   + " disabled/> " + strN + "." + strAnswer[n] + "</td></tr >");
                                }
                            }
                            else
                            {
                                //单选


                                string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
                                for (int n = 0; n < strAnswer.Length; n++)
                                {
                                    string strN = intToString(n + 1);
                                    string strij = "-" + paperItem.PaperItemId + "-" + i.ToString() + "-" + j.ToString()
                                                   + "-" + n.ToString();
                                    string strName = i.ToString() + j.ToString();

                                    Response.Write("<tr > <td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; "
                                                   + "<input type='Radio' id='RAnswer" + strij + "' name='RAnswer" + strName
                                                   + "' " + (Array.IndexOf(strUserAnswers, n) > -1 ? "checked" : "")
                                                   + " disabled/> " + strN + "." + strAnswer[n] + "</td></tr >");
                                }
                            }

                            // 组织正确答案
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

							Response.Write(" <tr><td class='ExamAnswer'>&nbsp;&nbsp;&nbsp;★标准答案："
                                           + "<span id='span-" + paperItem.PaperItemId + "-0' name='span-" +
                                           paperItem.PaperItemId
                                           + "'>" + strRightAnswer + "</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;★考生答案："
                                           + "<span id='span-" + paperItem.PaperItemId + "-1' name='span-" +
                                           paperItem.PaperItemId
                                           + "'>" + strUserAnswer + "</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;得分：&nbsp;" + theExamResultAnswer.JudgeScore.ToString() + "</td></tr>");
                        }
                    }
                    Response.Write(" </table> ");
                }
            }
            else
            {
                SessionSet.PageMessage = "数据错误！";

                return;
            }
        }
    }
}
