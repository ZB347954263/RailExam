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
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Paper
{
    public partial class TaskAnswer : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string strId = Request.QueryString.Get("id");
                if (strId != null && strId != "")
                {
                    ViewState["BeginTime"] = DateTime.Now.ToString();
                    FillPage(strId);
                }
            }

            string strAnswer = Request.Form.Get("strreturnAnswer");
            if (strAnswer != null && strAnswer != "")
            {
                ViewState["EndTime"] = DateTime.Now.ToString();

                CheckAnswer(strAnswer);
            }
        }

        protected void FillPage(string strId)
        {
            PaperBLL paperBLL = new PaperBLL();
            RailExam.Model.Paper paper = paperBLL.GetPaper(int.Parse(strId));
            if (paper != null)
            {
                this.labeltitle.Text = paper.PaperName;

                this.labelTitleRight.Text = "总共" + paper.ItemCount + "题共 " + paper.TotalScore + "分";
                hfPaperItemsCount.Value = paper.ItemCount.ToString();
            }
        }

        private int GetSecondBetweenTwoDate(DateTime dt1, DateTime dt2)
        {
            int i1 = dt1.Hour * 3600 + dt1.Minute * 60 + dt1.Second;
            int i2 = dt2.Hour * 3600 + dt2.Minute * 60 + dt2.Second;

            return i1 - i2;
        }

        private void CheckAnswer(string strAnswer)
        {
            string strId = Request.QueryString.Get("id");
            string strTrainTypeID = Request.QueryString.Get("TrainTypeID");

            TaskResultBLL ERBll = new TaskResultBLL();
            TaskResult ExamResult1 = new TaskResult();
            // ExamResult1.PaperTotalScore = Paper.TotalScore;
            ExamResult1.PaperId = int.Parse(strId);
            ExamResult1.TrainTypeId = int.Parse(strTrainTypeID);
            ExamResult1.AutoScore = 0;
            ExamResult1.BeginTime = DateTime.Parse(ViewState["BeginTime"].ToString());
            ExamResult1.CurrentTime = DateTime.Parse(ViewState["BeginTime"].ToString());

            ExamResult1.UsedTime =
                GetSecondBetweenTwoDate(DateTime.Parse(ViewState["EndTime"].ToString()),
                                        DateTime.Parse(ViewState["BeginTime"].ToString()));


            ExamResult1.EndTime = DateTime.Parse(ViewState["EndTime"].ToString());

            //ExamResult1.JudgeBeginDateTime = DateTime.Parse(ViewState["EndTime"].ToString());
            //ExamResult1.JudgeEndDateTime = DateTime.Parse(ViewState["EndTime"].ToString());
            //ExamResult1.JudgeId = 1;

            ExamResult1.Score = 0;

            ExamResult1.Memo = "";
            ExamResult1.StatusId = 1;
            ExamResult1.AutoScore = 0;
            ExamResult1.CorrectRate = 0;
            ExamResult1.EmployeeId = int.Parse(PrjPub.StudentID);

            string[] str1 = strAnswer.Split(new char[] { '$' });

            int examResultId = ERBll.AddTaskResult(ExamResult1, str1);

            TaskResult ExamResult2 = ERBll.GetTaskResult(examResultId);

            string strA = "得分为" + ExamResult2.Score.ToString() + "，正确率为" + ExamResult2.CorrectRate.ToString("0.00") + "%";

            //string strA = "得分为" + nanswerScore + ",正确率为" + (nanswerScore / nScore * 100).ToString("0.0") + "%";

            Response.Write("<script>alert('提交成功！" + strA + "'); top.window.close();</script>");
        }

        private string GetNo(int i)
        {
            string strReturn = "";
            switch (i.ToString())
            {
                case "0": strReturn = "一";
                    break;
                case "1": strReturn = "二";
                    break;
                case "2": strReturn = "三";
                    break;
                case "3": strReturn = "四";
                    break;
                case "4": strReturn = "五";
                    break;
                case "5": strReturn = "六";
                    break;
                case "6": strReturn = "七";
                    break;
                case "7": strReturn = "八";
                    break;
                case "8": strReturn = "九";
                    break;
                case "9": strReturn = "十";
                    break;
                case "10": strReturn = "十一";
                    break;
                case "11": strReturn = "十二";
                    break;
                case "12": strReturn = "十三";
                    break;
                case "13": strReturn = "十四";
                    break;
                case "14": strReturn = "十五";
                    break;
                case "15": strReturn = "十六";
                    break;
                case "16": strReturn = "十七";
                    break;
                case "17": strReturn = "十八";
                    break;
                case "18": strReturn = "十九";
                    break;
                case "19": strReturn = "二十";
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

        protected void FillResultPaper(string strTaskResultId)
        {
            string strId = strTaskResultId;
            // Not pass id
            if (string.IsNullOrEmpty(strId))
            {
                return;
            }

            PaperItemBLL kBLL = new PaperItemBLL();
            PaperSubjectBLL kBSLL = new PaperSubjectBLL();
            TaskResultBLL examResultBLL = new TaskResultBLL();
            TaskResultAnswerBLL examResultAnswerBLL = new TaskResultAnswerBLL();
            TaskResult examResult = examResultBLL.GetTaskResult(int.Parse(strId));
            // Not found
            if (examResult == null)
            {
                return;
            }

            IList<PaperSubject> PaperSubjects = kBSLL.GetPaperSubjectByPaperId(examResult.PaperId);
            PaperSubject paperSubject = null;
            IList<PaperItem> PaperItems = null;
            IList<TaskResultAnswer> examResultAnswers = examResultAnswerBLL.GetTaskResultAnswers(examResult.TaskResultId);

            if (PaperSubjects != null)
            {
                for (int i = 0; i < PaperSubjects.Count; i++)
                {
                    paperSubject = PaperSubjects[i];
                    PaperItems = kBLL.GetItemsByPaperSubjectId(paperSubject.PaperSubjectId);
                    Response.Write("<table width='100%' class='contentTable'>");
                    Response.Write(" <tr > <td align='left' style='background-color:#54FF9F' >");
                    Response.Write(" " + GetNo(i) + "");
                    Response.Write(".&nbsp;" + paperSubject.SubjectName + "");
                    Response.Write("  （共" + paperSubject.ItemCount + "题，共" + paperSubject.ItemCount*paperSubject.UnitScore + "分）</td></tr >");

                    // 用于前台JS判断是否完成全部试题
                    hfPaperItemsCount.Value = paperSubject.ItemCount.ToString();

                    if (PaperItems != null)
                    {
                        for (int j = 0; j < PaperItems.Count; j++)
                        {
                            PaperItem paperItem = PaperItems[j];
                            int k = j + 1;

                            Response.Write("<tr > <td style='text-align:left; background-color:gainsboro;'>&nbsp;&nbsp;&nbsp;"
                                + k + ".&nbsp; " + paperItem.Content + "&nbsp;&nbsp;（" + paperSubject.UnitScore + "分）</td></tr >");

                            // 组织用户答案
                            TaskResultAnswer theExamResultAnswer = null;
                            string[] strUserAnswers = new string[0];
                            string strUserAnswer = string.Empty;

                            foreach (TaskResultAnswer resultAnswer in examResultAnswers)
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

                                return;
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

                                    Response.Write(" <tr ><td align='left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; "
                                        + "<input type='checkbox' id='Answer" + strij + "' name='Answer" + strName
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

                                    Response.Write("<tr > <td align='left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; "
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

							Response.Write(" <tr><td style='color:green; text-align:left; '>&nbsp;&nbsp;&nbsp;★标准答案："
                                + "<span id='span-" + paperItem.PaperItemId + "-0' name='span-" + paperItem.PaperItemId
                                + "'>" + strRightAnswer + "</span></td></tr>");

							Response.Write(" <tr><td style='color:blue; text-align:left; '>&nbsp;&nbsp;&nbsp;★考生答案："
                                + "<span id='span-" + paperItem.PaperItemId + "-1' name='span-" + paperItem.PaperItemId
                                + "'>" + strUserAnswer + "</span></td></tr>");

                            Response.Write(" <tr score='" + paperItem.Score
                                + "'><td style='color:purple; text-align:left; '>★"// 

                                + "得分<input type='text'  readonly id='txtScore" + "-" + paperItem.PaperItemId
                                + "' name='txtScore" + "-" + paperItem.PaperItemId
                                + "' value='" + theExamResultAnswer.JudgeScore.ToString(".00")
                                + "' size='8'  style='width:20%'></input>&nbsp;&nbsp;&nbsp;&nbsp;"
                                + "评语<input type='text' readonly id='txtMemo" + "-" + paperItem.PaperItemId
                                + "' name='txtMemo" + "-" + paperItem.PaperItemId + "' size='40' value='"
                                + theExamResultAnswer.JudgeRemark + "'  style='width:70%'></input>"
                                + "</td></tr>");
                        }
                    }

                    Response.Write(" </table> ");
                }
            }

            Response.Write("<table width='100%'> <tr></tr><tr><td style='color:purple;text-align:left; '> ★★★该作业最终得分：" + examResult.Score + "分 </td></tr></table>");

            Response.Write("<table width='100%'> <tr><td style='text-align:center; '><a onclick='Close()' href='#'><b>关闭</b></a> </td></tr></table>");
        }

        protected void FillPaper()
        {
            string strId = Request.QueryString.Get("id");
            if (string.IsNullOrEmpty(strId))
            {
                SessionSet.PageMessage = "缺少参数！";

                return;
            }

            string strTrainTypeID = Request.QueryString.Get("TrainTypeID");

            TaskResultBLL examResultBLL = new TaskResultBLL();
            TaskResult examResult = examResultBLL.GetTaskResult(int.Parse(strId), int.Parse(strTrainTypeID), int.Parse(PrjPub.StudentID));

            if (examResult != null)
            {
                FillResultPaper(examResult.TaskResultId.ToString());

                return;
            }

            PaperItemBLL kBLL = new PaperItemBLL();
            // PaperSubjectBLL psBLL = new PaperSubjectBLL();
            PaperSubjectBLL kBSLL = new PaperSubjectBLL();
            IList<PaperSubject> PaperSubjects = kBSLL.GetPaperSubjectByPaperId(int.Parse(strId));

            if (PaperSubjects != null)
            {
                for (int i = 0; i < PaperSubjects.Count; i++)
                {
                    PaperSubject paperSubject = PaperSubjects[i];
                    IList<PaperItem> PaperItems = kBLL.GetItemsByPaperSubjectId(paperSubject.PaperSubjectId);

                    Response.Write("<table width='95%' class='contentTable'>");
                    Response.Write(" <tr> <td align='left' style='background-color:#54FF9F'>");
                    Response.Write(" " + GetNo(i) + "");
                    Response.Write("、" + paperSubject.SubjectName + "");
                    Response.Write("  （共" + paperSubject.ItemCount + "题，共" + paperSubject.TotalScore + "分）</td></tr>");

                    if (PaperItems != null)
                    {
                        for (int j = 0; j < PaperItems.Count; j++)
                        {
                            PaperItem paperItem = PaperItems[j];
                            int k = j + 1;

                            Response.Write("<tr> <td align='left'>&nbsp;&nbsp;&nbsp;" + k + ".&nbsp; " + paperItem.Content +
                                           "&nbsp;&nbsp; （" + paperItem.Score + "分）</td></tr>");
                            //多选



                            if (paperSubject.ItemTypeId == 2)
                            {
                                string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
                                for (int n = 0; n < strAnswer.Length; n++)
                                {
                                    string strN = intToString(n + 1);
                                    string strij = "-" + paperItem.PaperItemId + "-" + i.ToString() + "-" + j.ToString() +
                                                   "-" + n.ToString();
                                    string strName = i.ToString() + j.ToString();

                                    Response.Write(
                                        " <tr><td align='left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input type='checkbox' id='Answer" +
                                        strij + "' name='Answer" + strName + "'> " + strN + "." + strAnswer[n] +
                                        "</td></tr>");
                                }
                            }
                            else
                            {
                                //单选



                                string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
                                for (int n = 0; n < strAnswer.Length; n++)
                                {
                                    string strN = intToString(n + 1);
                                    string strij = "-" + paperItem.PaperItemId + "-" + i.ToString() + "-" + j.ToString() +
                                                   "-" + n.ToString();
                                    string strName = i.ToString() + j.ToString();
                                    Response.Write(
                                        "<tr > <td align='left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input type='Radio' id='RAnswer" +
                                        strij + "' name='RAnswer" + strName + "'> " + strN + "." + strAnswer[n] +
                                        "</td></tr >");
                                }
                            }
                        }
                    }
                    Response.Write(" </table> ");
                }

                Response.Write(" <table><tr><td align='center'><a onclick='SaveRecord()' href='#'><b>提交作业</b></a>  ");
                Response.Write("  &nbsp;&nbsp;&nbsp;&nbsp;<a onclick='Save()' href='#'><b>关闭</b></a> </td></tr></table>");
            }
            else
            {
                SessionSet.PageMessage = "未找到记录！";
            }
        }
    }
}