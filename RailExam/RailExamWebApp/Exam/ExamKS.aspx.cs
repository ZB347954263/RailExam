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

namespace RailExamWebApp.Exam
{
    public partial class ExamKS : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strId = Request.QueryString.Get("PaperId");
                string strExamId = Request.QueryString.Get("ExamId");
                ViewState["BeginTime"] = DateTime.Now.ToString();

                ViewState["OrgID"] = ConfigurationManager.AppSettings["StationID"];          
                ViewState["StudentID"] = PrjPub.StudentID;

                if (strId != null && strId != "")
                {
                    //ExamResultBLL examResultBLL = new ExamResultBLL();
                    //RailExam.Model.ExamResult examResults = examResultBLL.GetExamResult(int.Parse(strId), int.Parse(strExamId), PrjPub.CurrentStudent.EmployeeID);
                    ExamBLL examBLL = new ExamBLL();
                    RailExam.Model.Exam exam = examBLL.GetExam(int.Parse(strExamId));
                    HiddenFieldExamTime.Value = DateTime.Now.AddMinutes(exam.ExamTime).ToString();
                    HfExamTime.Value = (exam.ExamTime * 60).ToString();
                    FillPage(strId);
                }
            }

            string strAnswer = Request.Form.Get("strreturnAnswer");
            if (strAnswer != null && strAnswer != "")
            {
                ViewState["EndTime"] = DateTime.Now.ToString();

                SaveAnswerToDB(strAnswer);
            }
        }

        private int GetSecondBetweenTwoDate(DateTime dt1, DateTime dt2)
        {
            int i1 = dt1.Hour * 3600 + dt1.Minute * 60 + dt1.Second;
            int i2 = dt2.Hour * 3600 + dt2.Minute * 60 + dt2.Second;

            return i1 - i2;
        }

        private void SaveAnswerToDB(string strAnswer)
        {
            string strId = Request.QueryString.Get("PaperId");
            string strExamId = Request.QueryString.Get("ExamId");

            //PaperBLL paperBLL = new PaperBLL();
            //Paper paper = paperBLL.GetPaper(int.Parse(strId));

            ExamResultBLL examResultBLL = new ExamResultBLL();
            RailExam.Model.ExamResult examResult = new RailExam.Model.ExamResult();

            //examResult.PaperTotalScore = Paper.TotalScore;
            examResult.PaperId = int.Parse(strId);
            examResult.ExamId = int.Parse(strExamId);
            examResult.AutoScore = 0;
            examResult.BeginDateTime = DateTime.Parse(ViewState["BeginTime"].ToString());
            examResult.CurrentDateTime = DateTime.Parse(ViewState["BeginTime"].ToString());

            examResult.ExamTime =
                GetSecondBetweenTwoDate(DateTime.Parse(ViewState["EndTime"].ToString()),
                                        DateTime.Parse(ViewState["BeginTime"].ToString()));

            examResult.EndDateTime = DateTime.Parse(ViewState["EndTime"].ToString());  
            examResult.Score = 0;            

            examResult.OrganizationId = int.Parse(ViewState["OrgID"].ToString());
            examResult.Memo = "";
            examResult.StatusId = 1;
            examResult.AutoScore = 0;
            examResult.CorrectRate = 0;
            examResult.ExamineeId = int.Parse(ViewState["StudentID"].ToString()); // 

            string[] str1 = strAnswer.Split(new char[] { '$' });

            int examResultId = examResultBLL.AddExamResult(examResult, str1);

            try
            {
                if (!PrjPub.IsServerCenter)
                {
                    RailExam.Model.ExamResult obj = examResultBLL.GetExamResult(examResultId);
                    obj.ExamResultIDStation = examResultId;
                    examResultBLL.AddExamResultToServer(obj);
                }
            }
            catch
            {
                Pub.ShowErrorPage("无法连接路局服务器，请检查站段服务器网络连接是否正常！");
            }

            Response.Write("<script>window.parent.location = '/RailExamBao/Online/Exam/ExamSuccess.aspx'</script>");
        }

        protected void FillPage(string strId)
        {
            PaperItemBLL paperItemBLL = new PaperItemBLL();
            IList<RailExam.Model.PaperItem> paperItems = paperItemBLL.GetItemsByPaperId(int.Parse(strId));
            int nItemCount = paperItems.Count;

            // 用于前台JS判断是否完成全部试题
            hfPaperItemsCount.Value = nItemCount.ToString();
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
                return Convert.ToChar((intCol - 1) / 26 + 64).ToString() + Convert.ToChar((intCol - 1) % 26 + 64 + 1).ToString();
            }
        }

        protected void FillPaper()
        {
            string strId = Request.QueryString.Get("PaperId");
            if (string.IsNullOrEmpty(strId))
            {
                SessionSet.PageMessage = "缺少参数！";

                return;
            }
            string strExamId = Request.QueryString.Get("ExamId");
            ExamResultBLL examResultBLL = new ExamResultBLL();          

            //try
            //{
            //    RailExam.Model.ExamResult examResult = examResultBLL.GetExamResult(int.Parse(strId), int.Parse(strExamId), int.Parse(ViewState["StudentID"].ToString()));

            //    if (examResult != null)
            //    {
            //        hfExamed.Value = "true";
            //        FillResultPaper(examResult.ExamResultIDStation.ToString(),examResult.OrganizationId);
            //        return;
            //    }
            //}
            //catch
            //{
            //    Pub.ShowErrorPage("无法连接路局服务器，请检查站段服务器网络连接是否正常！");
            //}

            PaperItemBLL paperItemBLL = new PaperItemBLL();
            PaperSubjectBLL paperSubjectBLL = new PaperSubjectBLL();

            IList<PaperSubject> paperSubjects = paperSubjectBLL.GetPaperSubjectByPaperId(int.Parse(strId));

            if (paperSubjects != null)
            {
                for (int i = 0; i < paperSubjects.Count; i++)
                {
                    PaperSubject paperSubject = paperSubjects[i];
                    IList<PaperItem> paperItems = paperItemBLL.GetItemsByPaperSubjectId(paperSubject.PaperSubjectId);

                    Response.Write("<table width='95%' class='ExamContent'>");
                    Response.Write(" <tr> <td class='ExamBigTitle' >");
                    Response.Write(" " + GetNo(i) + "");
                    Response.Write("、" + paperSubject.SubjectName + "");
                    Response.Write("  （共" + paperItems.Count + "题，共" + paperItems.Count * paperSubject.UnitScore + "分）</td></tr >"); 

                    if (paperItems != null)
                    {
                        for (int j = 0; j < paperItems.Count; j++)
                        {
                            PaperItem paperItem = paperItems[j];
                            int k = j + 1;

                            Response.Write("<tr><td id='Item" + j + "' class='ExamItem'>&nbsp;&nbsp;&nbsp;" + k + ".&nbsp; " + paperItem.Content +
                                           "&nbsp;&nbsp; （" + paperSubject.UnitScore + "分）</td></tr >");

                            if (paperSubject.ItemTypeId == 2)   //多选


                            {
                                string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
                                for (int n = 0; n < strAnswer.Length; n++)
                                {
                                    string strN = intToString(n + 1);
                                    string strij = "-" + paperItem.PaperItemId + "-" + i.ToString() + "-" + j.ToString() +
                                                   "-" + n.ToString();
                                    string strName = i.ToString() + j.ToString();

                                    Response.Write(
                                        "<tr><td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input type='checkbox' id='Answer" +
                                        strij + "' name='Answer" + strName + "'> " + strN + "." + strAnswer[n] +
                                        "</td></tr>");
                                }
                            }
                            else    //单选


                            {

                                string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
                                for (int n = 0; n < strAnswer.Length; n++)
                                {
                                    string strN = intToString(n + 1);
                                    string strij = "-" + paperItem.PaperItemId + "-" + i.ToString() + "-" + j.ToString() +
                                                   "-" + n.ToString();
                                    string strName = i.ToString() + j.ToString();
                                    Response.Write(
                                        "<tr><td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input onclick='CheckStyle(this)'  type='Radio' id='RAnswer" +
                                        strij + "' name='RAnswer" + strName + "'> " + strN + "." + strAnswer[n] +
                                        "</td></tr>");
                                }
                            }
                        }
                    }
                    Response.Write("</table>");
                }

                Response.Write(" <table width='100%'><tr><td class='ExamButton'><input id='btnClose' class='button' name='btnSave' type='button' value='提交答卷'  onclick='SaveRecord()'/>  ");
                Response.Write(" </td></tr></table>");
            }
            else
            {
                SessionSet.PageMessage = "未找到记录！";
            }
        }

        protected void FillResultPaper(string strExamResultId,int orgid)
        {
            // QueryString id stands for EXAM_RESULT_ID
            string strId = strExamResultId;
            // Not pass id
            if (string.IsNullOrEmpty(strId))
            {
                return;
            }

            ExamResultBLL examResultBLL = new ExamResultBLL();
            RailExam.Model.ExamResult examResult = examResultBLL.GetExamResult(int.Parse(strId));
            // Not found
            if (examResult == null)
            {
                return;
            }

            string strExamId = Request.QueryString.Get("ExamId");
            ExamBLL examBLL = new ExamBLL();
            RailExam.Model.Exam exam = examBLL.GetExam(int.Parse(strExamId));

            PaperItemBLL paperItemBLL = new PaperItemBLL();

            PaperSubjectBLL paperSubjectBLL = new PaperSubjectBLL();

            IList<PaperSubject> paperSubjects = paperSubjectBLL.GetPaperSubjectByPaperId(examResult.PaperId);

            PaperSubject paperSubject = null;
            PaperItem paperItem = null;
            IList<PaperItem> paperItems = null;

            ExamResultAnswerBLL examResultAnswerBLL = new ExamResultAnswerBLL();
            IList<ExamResultAnswer> examResultAnswers = examResultAnswerBLL.GetExamResultAnswers(examResult.ExamResultId);

            if (paperSubjects != null)
            {
                for (int i = 0; i < paperSubjects.Count; i++)
                {
                    paperSubject = paperSubjects[i];
                    paperItems = paperItemBLL.GetItemsByPaperSubjectId(paperSubject.PaperSubjectId);

                    Response.Write("<table width='100%'>");
                    Response.Write("<tr> <td class='ExamBigTitle'>");
                    Response.Write(" " + GetNo(i) + "");
                    Response.Write(".&nbsp;" + paperSubject.SubjectName + "");                   
                    Response.Write("  （共" + paperItems.Count + "题，共" + paperItems.Count * paperSubject.UnitScore + "分）</td></tr >"); 

                    // 用于前台JS判断是否完成全部试题
                    hfPaperItemsCount.Value = paperItems.Count.ToString();

                    if (paperItems != null)
                    {
                        for (int j = 0; j < paperItems.Count; j++)
                        {
                            paperItem = paperItems[j];
                            int k = j + 1;

                            Response.Write("<tr > <td class='ExamResultItem'>&nbsp;&nbsp;&nbsp;"
                                + k + ".&nbsp; " + paperItem.Content + "&nbsp;&nbsp;（" + paperSubject.UnitScore + "分）</td></tr >");

                            // 组织用户答案
                            ExamResultAnswer examResultAnswer = null;

                            foreach (ExamResultAnswer resultAnswer in examResultAnswers)
                            {
                                if (resultAnswer.PaperItemId == paperItem.PaperItemId)
                                {
                                    examResultAnswer = resultAnswer;
                                    break;
                                }
                            }

                            // 若子表无记录，结束页面输出



                            if (examResultAnswer == null)
                            {
                                SessionSet.PageMessage = "数据错误！";

                                return;
                            }

                            string[] strUserAnswers = new string[0];
                            string strUserAnswer = string.Empty;

                            // 否则组织考生答案
                            if (examResultAnswer.Answer != null)
                            {
                                strUserAnswers = examResultAnswer.Answer.Split(new char[] { '|' });
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

                                    Response.Write("<tr><td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; "
                                        + "<input type='checkbox' id='Answer" + strij + "' name='Answer" + strName
                                        + "' " + (Array.IndexOf(strUserAnswers, n) > -1 ? "checked" : "")
                                        + " disabled/> " + strN + "." + strAnswer[n] + "</td></tr>");
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

                                    Response.Write("<tr><td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; "
                                        + "<input type='Radio' id='RAnswer" + strij + "' name='RAnswer" + strName
                                        + "' " + (Array.IndexOf(strUserAnswers, n) > -1 ? "checked" : "")
                                        + " disabled/> " + strN + "." + strAnswer[n] + "</td></tr>");
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

                            if (exam.CanSeeAnswer)
                            {
                                if (exam.EndTime.AddDays(1) < DateTime.Now)
                                {
									Response.Write(" <tr><td class='ExamAnswer'>&nbsp;&nbsp;&nbsp;★标准答案："
                                    + "<span id='span-" + paperItem.PaperItemId + "-0' name='span-" + paperItem.PaperItemId
                                    + "'>" + strRightAnswer + "</span></td></tr>");
                                }
                            }

							Response.Write(" <tr><td class='ExamStudentAnswer'>&nbsp;&nbsp;&nbsp;★考生答案："
                                + "<span id='span-" + paperItem.PaperItemId + "-1' name='span-" + paperItem.PaperItemId
                                + "'>" + strUserAnswer + "</span></td></tr>");

                            if (exam.EndTime.AddDays(1) < DateTime.Now)
                            {
                                if (exam.CanSeeScore)
                                {
                                    Response.Write(" <tr score='" + paperItem.Score
                                        + "'><td class='ExamJudge'>★"
                                        + "得分<input type='text' readonly id='txtScore" + "-" + paperItem.PaperItemId
                                        + "' name='txtScore" + "-" + paperItem.PaperItemId
                                        + "' value='" + examResultAnswer.JudgeScore.ToString(".00")
                                        + "' size='8'></input>&nbsp;&nbsp;&nbsp;&nbsp;"
                                        + "评语<input type='text' readonly id='txtMemo" + "-" + paperItem.PaperItemId
                                        + "' name='txtMemo" + "-" + paperItem.PaperItemId + "' size='40' value='"
                                        + examResultAnswer.JudgeRemark + "' ></input>"
                                        + "</td></tr>");
                                }
                            }
                        }
                    }

                    Response.Write("</table> ");
                }
            }

            if (exam.EndTime.AddDays(1) < DateTime.Now)
            {
                if (exam.CanSeeScore)
                {
                    Response.Write("<table width='100%'> <tr></tr><tr><td id='ExamJudge'> ★★★考生总分：" + examResult.Score + "分 </td></tr></table>");
                }
            }
            Response.Write("<div class='ExamButton'><input id='btnClose' class='button' name='btnClose' type='button' value='关闭'  onclick='Close()' /> </div>");
        }
    }
}