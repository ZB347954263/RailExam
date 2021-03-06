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
using DSunSoft.Web.Global;
using RailExam.BLL;
using RailExam.Model;
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;
using System.IO;

namespace RailExamWebApp.Paper
{
    public partial class PaperManageInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }
                if (PrjPub.HasEditRight("试卷信息") && PrjPub.IsServerCenter)
                {
                    HfUpdateRight.Value = "True";
                }
                else
                {
                    HfUpdateRight.Value = "False";
                }
                if (PrjPub.HasDeleteRight("试卷信息") && PrjPub.IsServerCenter)
                {
                    HfDeleteRight.Value = "True";
                }
                else
                {
                    HfDeleteRight.Value = "False";
                }
                hfOrgID.Value = PrjPub.CurrentLoginUser.StationOrgID.ToString();
                HfPaperCategoryId.Value = Request.QueryString.Get("id");
            }

            if (IsPostBack)
            {
                string strDeleteID = Request.Form.Get("DeleteID");
                if (!string.IsNullOrEmpty(strDeleteID))
                {
                    DeleteData(int.Parse(strDeleteID));
                    Grid1.DataBind();
                }

                if (Request.Form.Get("Refresh") == "true")
                {
                    Grid1.DataBind();
                }

                if (Request.Form.Get("OutPut")!=null&&Request.Form.Get("OutPut") != "")
                {
                    OutputData(Request.Form.Get("OutPut"));
                }

                if (Request.Form.Get("strategyID") != null && Request.Form.Get("strategyID") != "")
                {
                    DeleteStrategyData(Request.Form.Get("strategyID"));
                    Grid1.DataBind();
                }
            }
        }

        private void DeleteStrategyData(string strategyID)
        {
            PaperStrategyBLL paperStrategyBLL = new PaperStrategyBLL();
            paperStrategyBLL.DeletePaperStrategy(int.Parse(strategyID));
        }

        private void DeleteData(int nPaperID)
        {
            PaperBLL paperBLL = new PaperBLL();

            paperBLL.DeletePaper(nPaperID);
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            Grid1.DataBind();
        }

        private void OutputData(string id)
        {
            PaperBLL paperBLL = new PaperBLL();
            RailExam.Model.Paper paper = paperBLL.GetPaper(int.Parse(id));

            PaperItemBLL paperItemBLL = new PaperItemBLL();
            IList<RailExam.Model.PaperItem> paperItems = paperItemBLL.GetItemsByPaperId(int.Parse(id));

            int nItemCount = paperItems.Count;
            decimal nTotalScore = 0;

            for (int i = 0; i < paperItems.Count; i++)
            {
                nTotalScore += paperItems[i].Score;
            }

            string str = "<table border='0' cellpadding='0' cellspacing='0' >";
            str += "<tr><td colspan='2' align='center' style='font-size:28px' >" + paper.PaperName + "</td> </tr><tr> <td  colspan='2' align='right'>总共" + nItemCount + "题共 " + nTotalScore + "分</td></tr> ";                     

            str += "<tr> <td style='width: 15%; vertical-align: top'>";
            str += "<table border='1' cellpadding='0' cellspacing='0' ><tr><td style='width: 100px; height: 50px' valign='top'> 所属单位：</td> </tr> <tr><td style='height: 50px' valign='top'>车间：</td> </tr><tr><td style='height: 50px' valign='top'>姓名：</td></tr><tr><td style='height: 50px' valign='top'>职名：</td></tr><tr><td style='height: 50px' valign='top'>考试日期：</td></tr></table> </td> ";


            str += " <td style='width: 85%; vertical-align: top'>   ";
            str += GetFillPaperString(id);
            str += "</td></tr></table> ";

            Response.Clear();
            Response.Charset = "utf-7";
            Response.Buffer = true;
            EnableViewState = false;
            Response.AppendHeader("Content-Disposition", "attachment;filename=Paper.doc");

            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-7");
            Response.ContentType = "application/ms-word";
            Response.Write(str);
            Response.End();
        }

        private string GetFillPaperString(string strId)
        {
            string strPaperString = "";
            PaperItemBLL paperItemBLL = new PaperItemBLL();
            PaperSubjectBLL paperSubjectBLL = new PaperSubjectBLL();
            IList<PaperSubject> paperSubjects = paperSubjectBLL.GetPaperSubjectByPaperId(int.Parse(strId));

            if (paperSubjects != null)
            {
                for (int i = 0; i < paperSubjects.Count; i++)
                {
                    PaperSubject paperSubject = paperSubjects[i];

                    IList<PaperItem> paperItems = paperItemBLL.GetItemsByPaperSubjectId(paperSubject.PaperSubjectId);

                    strPaperString += " <table width='100%' border='0' cellpadding='0' cellspacing='0'>";
                    strPaperString += " <tr><td  style='font-size:21px'>";
                    strPaperString+=" " + GetNo(i) + "、" + paperSubject.SubjectName ;
                    strPaperString += " （共" + paperItems.Count + "题，共" + paperItems.Count*paperSubject.UnitScore + "分）</td></tr>";
                    if (paperItems != null)
                    {
                        for (int j = 0; j < paperItems.Count; j++)
                        {
                            PaperItem paperItem = paperItems[j];
                            int k = j + 1;
                            string strij = i.ToString() + j.ToString();
                            strPaperString+="<tr><td >&nbsp;&nbsp;&nbsp;" + k + "." + paperItem.Content + " （" + paperItem.Score + "分）</td></tr >";

                            if (paperSubject.ItemTypeId == 2)   //多选


                            {
                                string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
                                for (int n = 0; n < strAnswer.Length; n++)
                                {
                                    string strN = intToString(n + 1);
                                    strPaperString+="<tr><td >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + strN + "." + strAnswer[n] + "</td></tr>";
                                }
                            }
                            else    //单选


                            {
                                string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
                                for (int n = 0; n < strAnswer.Length; n++)
                                {
                                    string strN = intToString(n + 1);

                                    strPaperString+="<tr><td >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + strN + "." + strAnswer[n] + "</td></tr>";
                                }
                            }
                          
                        }
                    }

                    strPaperString+="</table>";

                }
            }

            return strPaperString;
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
    }
}