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
using RailExam.BLL;
using RailExam.Model;
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;

namespace RailExamWebApp.Paper
{
    public partial class PaperPreview : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strId = Request.QueryString.Get("id");
                if (!string.IsNullOrEmpty(strId))
                {
                    FillPage(int.Parse(strId));
                }
            }
        }

        protected void FillPage(int nId)
        {
            PaperBLL paperBLL = new PaperBLL();
            RailExam.Model.Paper paper = paperBLL.GetPaper(nId);

            if (paper != null)
            {
                lblTitle.Text = paper.PaperName;
            }           

            PaperItemBLL paperItemBLL = new PaperItemBLL();
            IList<RailExam.Model.PaperItem> paperItems = paperItemBLL.GetItemsByPaperId(nId);

            int nItemCount = paperItems.Count;
            decimal nTotalScore = 0;

            for (int i = 0; i < paperItems.Count; i++)
            {
                nTotalScore += paperItems[i].Score;
            }

            lblTitleRight.Text = "总共" + nItemCount + "题，共 " + nTotalScore + "分";
        }

        private string GetNo(int i)
        {
            string strReturn = string.Empty;

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

            PaperItemBLL paperItemBLL = new PaperItemBLL();
            PaperSubjectBLL paperSubjectBLL = new PaperSubjectBLL();
            IList<PaperSubject> paperSubjects = paperSubjectBLL.GetPaperSubjectByPaperId(int.Parse(strId));

            if (paperSubjects != null)
            {
                for (int i = 0; i < paperSubjects.Count; i++)
                {
                    PaperSubject paperSubject = paperSubjects[i];

                    IList<PaperItem> paperItems = paperItemBLL.GetItemsByPaperSubjectId(paperSubject.PaperSubjectId);

                    Response.Write(" <table width='95%' >");
                    Response.Write(" <tr><td id='PaperBigTitle' >");
                    Response.Write(" " + GetNo(i) + "、" + paperSubject.SubjectName + "");
                    Response.Write("  （共" + paperItems.Count + "题，共" + paperItems.Count * paperSubject.UnitScore + "分）</td></tr>");
                    if (paperItems != null)
                    {
                        for (int j = 0; j < paperItems.Count; j++)
                        {
                            PaperItem paperItem = paperItems[j];
                            int k = j + 1;
                            string strij = i.ToString() + j.ToString();
                            Response.Write("<tr><td id='PaperItem'>&nbsp;&nbsp;&nbsp;" + k + ".&nbsp; " + paperItem.Content + "&nbsp;&nbsp; （" + paperSubject.UnitScore + "分）</td></tr >");

                            if (paperSubject.ItemTypeId == 2)   //多选


                            {
                                string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
                                for (int n = 0; n < strAnswer.Length; n++)
                                {
                                    string strN = intToString(n + 1);
                                    Response.Write("<tr><td id='PaperItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='checkbox' id='Answer" + strij + "' name='Answer" + strij + "'> " + strN + "." + strAnswer[n] + "</td></tr>");
                                }
                            }
                            else    //单选


                            {
                                string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
                                for (int n = 0; n < strAnswer.Length; n++)
                                {
                                    string strN = intToString(n + 1);

                                    Response.Write("<tr><td id='PaperItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='Radio' id='RAnswer" + strij + "' name='RAnswer" + strij + "'> " + strN + "." + strAnswer[n] + "</td></tr>");
                                }
                            }

                            string[] strRightAnswer = paperItem.StandardAnswer.Split(new char[] { '|' });
                            string strNew = string.Empty;
                            for (int n = 0; n < strRightAnswer.Length; n++)
                            {
                                string strN = intToString(int.Parse(strRightAnswer[n]) + 1);
                                if (n == 0)
                                {
                                    strNew += strN;
                                }
                                else
                                {
                                    strNew += "," + strN;
                                }
                            }

							Response.Write("<tr><td id='PaperAnswer'>&nbsp;&nbsp;&nbsp;★标准答案： " + strNew + "</td></tr>");
                        }
                    }

                    Response.Write("<tr><td>&nbsp;</td></tr></table>");
                }
            }
        }
    }
}