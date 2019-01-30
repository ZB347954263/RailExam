﻿using System;
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
using DSunSoft.Common;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;

namespace RailExamWebApp.Task
{
    public partial class TaskResult : PageBase
    {
        private static string _taskJudgeInputs = string.Empty;

        public static int JudgeStatusCount = 0;

        public static string TaskJudgeInputs
        {
            get
            {
                if (string.IsNullOrEmpty(_taskJudgeInputs))
                {
                    TaskJudgeStatusBLL taskJudgeStatusBLL = new TaskJudgeStatusBLL();
                    IList<TaskJudgeStatus> taskJudgeStatuses = taskJudgeStatusBLL.GetTaskJudgeStatuses();

                    // 评分输入栏



                    string strExamJudgInputs = string.Empty;
                    int index = 0;
                    foreach (TaskJudgeStatus judgeStatus in taskJudgeStatuses)
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
                    return _taskJudgeInputs;
                }
            }
        }

        private static string GetJudgeInputs(int itemId, int statusId)
        {
            string judgeInputs = TaskJudgeInputs;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // QueryString id stands for EXAM_RESULT_ID
                string strId = Request.QueryString.Get("id");
                if (!string.IsNullOrEmpty(strId))
                {
                    FillHeading(strId);
                }
            }
        }

        protected void FillHeading(string strId)
        {
            TaskResultBLL taskResultBLL = new TaskResultBLL();
            PaperBLL kBLL = new PaperBLL();
            RailExam.Model.TaskResult taskResult = taskResultBLL.GetTaskResult(int.Parse(strId));
            RailExam.Model.Paper paper = null;

            if (taskResult != null)
            {
                paper = kBLL.GetPaper(taskResult.PaperId);
            }
            if (paper != null)
            {
                lblTitle.Text = paper.PaperName;
                lblTitleRight.Text = "总共 " + paper.ItemCount + " 题，共 " + paper.TotalScore + " 分";
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
            TaskResultBLL taskResultBLL = new TaskResultBLL();
            TaskResultAnswerBLL taskResultAnswerBLL = new TaskResultAnswerBLL();
            RailExam.Model.TaskResult taskResult = taskResultBLL.GetTaskResult(int.Parse(strId));
            // Not found
            if (taskResult == null)
            {
                SessionSet.PageMessage = "数据错误！";

                return;
            }

            IList<PaperSubject> PaperSubjects = kBSLL.GetPaperSubjectByPaperId(taskResult.PaperId);
            PaperSubject paperSubject = null;
            IList<PaperItem> PaperItems = null;
            IList<TaskResultAnswer> taskResultAnswers = taskResultAnswerBLL.GetTaskResultAnswers(taskResult.TaskResultId);

            if (PaperSubjects != null)
            {
                for (int i = 0; i < PaperSubjects.Count; i++)
                {
                    paperSubject = PaperSubjects[i];
                    PaperItems = kBLL.GetItemsByPaperSubjectId(paperSubject.PaperSubjectId);
                    Response.Write("<table style='width:100%;'>");
                    Response.Write(
                        " <tr class=\"tableFont\" > <td colspan='3' align='left' style='background-color:#54FF9F' >");
                    Response.Write(" " + CommonTool.GetChineseNumber(i + 1) + "");
                    Response.Write(".&nbsp;" + paperSubject.SubjectName + "");
                    Response.Write("  （共" + paperSubject.ItemCount + "题，共" + paperSubject.ItemCount * paperSubject.UnitScore + "分）</td></tr >");

                    if (PaperItems != null)
                    {
                        for (int j = 0; j < PaperItems.Count; j++)
                        {
                            PaperItem paperItem = PaperItems[j];
                            int k = j + 1;

                            Response.Write("<tr class=\"tableFont\" > <td colspan='3' style='text-align:left; background-color:gainsboro;'>&nbsp;&nbsp;&nbsp;"
                                           + k + ".&nbsp; " + paperItem.Content + "&nbsp;&nbsp;（" + paperSubject.UnitScore +
                                           "分）</td></tr >");

                            // 组织用户答案
                            TaskResultAnswer theTaskResultAnswer = null;
                            string[] strUserAnswers = new string[0];
                            string strUserAnswer = string.Empty;

                            foreach (TaskResultAnswer resultAnswer in taskResultAnswers)
                            {
                                if (resultAnswer.PaperItemId == paperItem.PaperItemId)
                                {
                                    theTaskResultAnswer = resultAnswer;
                                    break;
                                }
                            }

                            // 若子表无记录，结束页面输出



                            if (theTaskResultAnswer == null)
                            {
                                SessionSet.PageMessage = "数据错误！";

                                return;
                            }

                            // 否则组织考生答案
                            if (theTaskResultAnswer.Answer != null)
                            {
                                strUserAnswers = theTaskResultAnswer.Answer.Split(new char[] { '|' });
                            }
                            for (int n = 0; n < strUserAnswers.Length; n++)
                            {
                                if (n == 0)
                                {
                                    strUserAnswer += CommonTool.GetSelectLetter(int.Parse(strUserAnswers[n]) + 1);
                                }
                                else
                                {
                                    strUserAnswer += "," + CommonTool.GetSelectLetter(int.Parse(strUserAnswers[n]) + 1);
                                }
                            }

                            //多选



                            if (paperSubject.ItemTypeId == 2)
                            {
                                string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
                                for (int n = 0; n < strAnswer.Length; n++)
                                {
                                    string strij = "-" + paperItem.PaperItemId + "-" + i.ToString() + "-"
                                                   + j.ToString() + "-" + n.ToString();
                                    string strName = i.ToString() + j.ToString();

                                    Response.Write(" <tr class=\"tableFont\" ><td colspan='3' align='left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; "
                                                   + "<input type='checkbox' id='Answer" + strij + "' name='Answer" +
                                                   strName
                                                   + "' " + (Array.IndexOf(strUserAnswers, n) > -1 ? "checked" : "")
                                                   + " disabled/> " + CommonTool.GetSelectLetter(n + 1)
                                                   + "." + strAnswer[n] + "</td></tr >");
                                }
                            }
                            else
                            {
                                //单选



                                string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
                                for (int n = 0; n < strAnswer.Length; n++)
                                {
                                    string strij = "-" + paperItem.PaperItemId + "-" + i.ToString() + "-" + j.ToString()
                                                   + "-" + n.ToString();
                                    string strName = i.ToString() + j.ToString();

                                    Response.Write("<tr class=\"tableFont\" > <td colspan='3' align='left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; "
                                                   + "<input type='Radio' id='RAnswer" + strij + "' name='RAnswer" + strName
                                                   + "' " + (Array.IndexOf(strUserAnswers, n) > -1 ? "checked" : "")
                                                   + " disabled/> " + CommonTool.GetSelectLetter(n + 1)
                                                   + "." + strAnswer[n] + "</td></tr >");
                                }
                            }

                            // 组织正确答案
                            string[] strRightAnswers = paperItem.StandardAnswer.Split(new char[] { '|' });
                            string strRightAnswer = "";
                            for (int n = 0; n < strRightAnswers.Length; n++)
                            {
                                if (n == 0)
                                {
                                    strRightAnswer += CommonTool.GetSelectLetter(n + 1);
                                }
                                else
                                {
                                    strRightAnswer += "," + CommonTool.GetSelectLetter(n + 1);
                                }
                            }

							Response.Write(" <tr class=\"tableFont\"><td colspan='3' style='color:green; text-align:left; '>&nbsp;&nbsp;&nbsp;★标准答案："
                                           + "<span id='span-" + paperItem.PaperItemId + "-0' name='span-" +
                                           paperItem.PaperItemId
                                           + "'>" + strRightAnswer + "</span></td></tr>");
							Response.Write(" <tr class=\"tableFont\"><td colspan='3' style='color:blue; text-align:left; '>&nbsp;&nbsp;&nbsp;★考生答案："
                                           + "<span id='span-" + paperItem.PaperItemId + "-1' name='span-" +
                                           paperItem.PaperItemId
                                           + "'>" + strUserAnswer + "</span></td></tr>");
                            Response.Write(" <tr class=\"tableFont\" score='" + paperItem.Score
                                           + "'><td style='color:purple; text-align:left; width:20%; '>★评分结果："
                                //+ "<input type='radio' id='rbnCorrect" + "-" + paperItem.PaperItemId 
                                //+ "' name='rbnJudge" + "-" + paperItem.PaperItemId
                                //+ "' " + (strRightAnswer.Equals(strUserAnswer) ? "checked" : "") 
                                //+ "><font color='green'>对</font></input>"
                                //+ "<input type='radio' id='rbnIncorrect" + "-" + paperItem.PaperItemId 
                                //+ "' name='rbnJudge" + "-" + paperItem.PaperItemId
                                //+ "' " + (strRightAnswer.Equals(strUserAnswer) ? "" : "checked") 
                                //+ "><font color='red'>错</font></input>"
                                //+ "<input type='radio' id='rbnPartlyCorrect" + "-" + paperItem.PaperItemId 
                                //+ "' name='rbnJudge" + "-" + paperItem.PaperItemId + "'>半对</input>"
                                           + GetJudgeInputs(paperItem.PaperItemId, theTaskResultAnswer.JudgeStatusId)
                                           + "&nbsp;&nbsp;&nbsp;&nbsp;"
                                           + "得分&nbsp;" + theTaskResultAnswer.JudgeScore.ToString()
                                           + "&nbsp;&nbsp;&nbsp;"
                                           + "评语&nbsp;" + theTaskResultAnswer.JudgeRemark + ""
                                           + "</td></tr>");
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