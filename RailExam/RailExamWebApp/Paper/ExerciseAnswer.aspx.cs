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

namespace RailExamWebApp.Paper
{
    public partial class ExerciseAnswer : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strId = Request.QueryString.Get("id");
                if (strId != null && strId != "")
                {
                    FillPage(strId);
                }
            }

            string strAnswer = Request.Form.Get("strreturnAnswer");
            if (strAnswer != null && strAnswer != "")
            {
                CheckAnswer(strAnswer);
            }
        }

        protected void FillPage(string strId)
        {
            PaperBLL paperBLL = new PaperBLL();
            RailExam.Model.Paper paper = paperBLL.GetPaper(int.Parse(strId));
            if (paper != null)
            {
                lblTitle.Text = paper.PaperName;

                lblTitleRight.Text = "总共" + paper.ItemCount + "题共 " + paper.TotalScore + "分";
                hfPaperItemsCount.Value = paper.ItemCount.ToString();
            }
        }

        private void CheckAnswer(string strAnswer)
        {
            string strId = Request.QueryString.Get("id");
            PaperBLL paperBLL = new PaperBLL();
            RailExam.Model.Paper paper = paperBLL.GetPaper(int.Parse(strId));
            PaperItemBLL paperItemBLL = new PaperItemBLL();

            string[] strAnswers = strAnswer.Split(new char[] { '$' });

            decimal nanswerScore = 0;
            decimal nScore = paper.TotalScore;

            for (int n = 0; n < strAnswers.Length; n ++)
            {
                string str2 = strAnswers[n].ToString();
                string[] str3 = str2.Split(new char[] { '|' });
                string strPaperItemId = str3[0].ToString();
                PaperItem paperItem = paperItemBLL.GetPaperItem(int.Parse(strPaperItemId));
                string strTrueAnswer = str2.ToString().Substring(strPaperItemId.Length + 1);
                if (paperItem.StandardAnswer == strTrueAnswer)
                {
                    nanswerScore += paperItem.Score;
                }
            }

            string strA = "得分为" + nanswerScore + "，正确率为" + (nanswerScore * 100 / nScore).ToString("0.00") + "%";

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

        protected void FillPaper()
        {
            string strId = Request.QueryString.Get("id");
            if (string.IsNullOrEmpty(strId))
            {
                SessionSet.PageMessage = "缺少参数！";

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
                    Response.Write(" <tr> <td align='left' style='background-color:#54FF9F' >");
                    Response.Write(" " + GetNo(i) + "");
                    Response.Write("、" + paperSubject.SubjectName + "");
                    Response.Write("  （共" + paperSubject.ItemCount + "题，共" + paperSubject.TotalScore + "分）</td></tr >");

                    if (PaperItems != null)
                    {
                        for (int j = 0; j < PaperItems.Count; j++)
                        {
                            PaperItem paperItem = PaperItems[j];
                            int k = j + 1;

                            Response.Write("<tr > <td align='left'>&nbsp;&nbsp;&nbsp;" + k + ".&nbsp; " + paperItem.Content +
                                           "&nbsp;&nbsp; （" + paperItem.Score + "分）</td></tr >");
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
                                        " <tr ><td align='left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input type='checkbox' id='Answer" +
                                        strij + "' name='Answer" + strName + "'> " + strN + "." + strAnswer[n] +
                                        "</td></tr >");
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

                            //string[] strRightAnswer = paperItem.StandardAnswer.Split(new char[] { '|' });
                            //string strNew = "";
                            //for (int n = 0; n < strRightAnswer.Length; n++)
                            //{
                            //    string strN = intToString(int.Parse(strRightAnswer[n])+1);
                            //    if (n == 0)
                            //    {
                            //        strNew += strN;
                            //    }
                            //    else
                            //    {

                            //        strNew += "," + strN;
                            //    }
                            //}

                            //Response.Write(" <tr ><td style='background-color:#7EC0EE' align='left'>★标准答案： " + strNew + "</td></tr >");
                        }
                    }
                    Response.Write(" </table> ");
                }

                Response.Write(" <table><tr><td align='center'><a  onclick='SaveRecord()'  href='#'><b>提交练习</b></a>  ");
                Response.Write("  &nbsp;&nbsp;&nbsp;&nbsp;<a  onclick='Save()'  href='#'><b>关闭</b></a> </td></tr></table>");
            }
            else
            {
                SessionSet.PageMessage = "未找到记录！";
            }
        }

        //protected void btnOK_Click(object sender, ImageClickEventArgs e)
        //{
        //    string strId = Request.QueryString.Get("id");

        //    PaperItemBLL kBLL = new PaperItemBLL();
        //    PaperSubjectBLL kBSLL = new PaperSubjectBLL();
        //    IList<PaperSubject> PaperSubjects = kBSLL.GetPaperSubjectByPaperId(int.Parse(strId));

        //    //当本练习有试卷大题时
        //    if (PaperSubjects != null)
        //    {
        //        for (int i = 0; i < PaperSubjects.Count; i++)
        //        {
        //            PaperSubject paperSubject = PaperSubjects[i];

        //            IList<PaperItem> PaperItems = kBLL.GetItemsByPaperSubjectId(paperSubject.PaperSubjectId);

        //            //当本大题下题目存在时
        //            if (PaperItems != null)
        //            {
        //                for (int j = 0; j < PaperItems.Count; j++)
        //                {
        //                    PaperItem paperItem = PaperItems[j];
        //                    int k = j + 1;
        //                    string strij;

        //                    if (paperSubject.ItemTypeId == 2)
        //                    {
        //                        string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
        //                        for (int n = 0; n < strAnswer.Length; n++)
        //                        {
        //                            strij = (i + 1).ToString() + k.ToString();
        //                            Response.Write(strij + "=" + Request.Form.Get("Answer" + strij) +",");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
        //                        for (int n = 0; n < strAnswer.Length; n++)
        //                        {
        //                            strij = (i + 1).ToString() + k.ToString();
        //                            Response.Write(strij + "=" + Request.Form.Get("RAnswer" + strij) + ",");
        //                        }
        //                    }
        //                }
        //            }
        //          }
        //    }
        //}
    }
}
