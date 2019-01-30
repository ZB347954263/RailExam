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
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class AttendExamNavigation :PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void FillPaper()
        {
            string strId = Request.QueryString.Get("id");

            RandomExamBLL randomExamBLL = new RandomExamBLL();
            RailExam.Model.RandomExam randomExam = randomExamBLL.GetExam(int.Parse(strId));
            ViewState["Year"] = randomExam.BeginTime.Year.ToString();

            RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
            RailExam.Model.RandomExamResultCurrent randomExamResult = objResultCurrentBll.GetNowRandomExamResultInfo(Convert.ToInt32(Request.QueryString.Get("employeeID")), Convert.ToInt32(strId));

            int RandomExamId = Convert.ToInt32(strId);
            int randomExamResultId = randomExamResult.RandomExamResultId;

            RandomExamItemBLL randomItemBLL = new RandomExamItemBLL();
            RandomExamSubjectBLL subjectBLL = new RandomExamSubjectBLL();
            IList<RandomExamSubject> randomExamSubjects = subjectBLL.GetRandomExamSubjectByRandomExamId(RandomExamId);

            //RandomExamResultAnswerCurrentBLL randomExamResultAnswerBLL = new RandomExamResultAnswerCurrentBLL();
            //IList<RandomExamResultAnswerCurrent> examResultAnswers = new List<RandomExamResultAnswerCurrent>();
            //examResultAnswers = randomExamResultAnswerBLL.GetExamResultAnswersCurrent(randomExamResultId);
            //OracleAccess db = new OracleAccess();

            if (randomExamSubjects != null)
            {
                for (int i = 0; i < randomExamSubjects.Count; i++)
                {
                    RandomExamSubject paperSubject = randomExamSubjects[i];
                    IList<RandomExamItem> PaperItems = new List<RandomExamItem>();
                    PaperItems = randomItemBLL.GetItemsCurrent(paperSubject.RandomExamSubjectId, randomExamResultId, Convert.ToInt32(ViewState["Year"].ToString()));

                    Response.Write("<br>");
                    Response.Write("<span class='StudentLeftInfo'><b> ��" + GetNo(i) + "���⣺" + paperSubject.SubjectName + "</b></span>");
                    Response.Write("<br>");

                    if (PaperItems != null)
                    {
                        Response.Write("<table width='100%'  border='1' style='background-color:#ffffff'>");
                        int z = 1;
                        int tempK = 0;
                        int count = 1;
                        for (int j = 0; j < PaperItems.Count; j++)
                        {
                            RandomExamItem paperItem = PaperItems[j];
                            int k = j + 1;

                            

                            if (paperItem.TypeId != PrjPub.ITEMTYPE_FILLBLANKDETAIL && paperItem.TypeId != PrjPub.ITEMTYPE_FILLBLANK)
                            {
                                z = 1;

                                if (k % 5 == 1)
                                {
                                    Response.Write("</tr >");
                                    Response.Write("<tr><td class='StudentTableInfo' id='Item" + i + j + "' >"
                                        + "<a href='AttendExamNew.aspx?id=" + strId + "&employeeID=" + Request.QueryString.Get("employeeID")
                                        + "#Test" + i + j + "' target='ifExamInfo' style='cursor: hand;'><b>" + k + "</b></a></td>");
                                }
                                else
                                {
                                    Response.Write("<td class='StudentTableInfo' id='Item" + i + j + "' >"
                                        + "<a href='AttendExamNew.aspx?id=" + strId + "&employeeID=" + Request.QueryString.Get("employeeID")
                                        + "#Test" + i + j + "' target='ifExamInfo' style='cursor: hand;'><b>" + k + "</b></a></td>");
                                }
                            }
                            else
                            {
                                if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                                {
                                    z = 1;
                                    tempK++;
                                    continue;
                                }

                                if (count % 3 == 1)
                                {
                                    Response.Write("</tr >");
                                    Response.Write("<tr><td class='StudentTableInfo' id='Item" + i + j + "' >"
                                        + "<a href='AttendExamNew.aspx?id=" + strId + "&employeeID=" + Request.QueryString.Get("employeeID")
                                        + "#Test" + i + j + "' target='ifExamInfo' style='cursor: hand;'><b>" + tempK + "-(" + z + ")</b></a></td>");
                                }
                                else
                                {
                                    Response.Write("<td class='StudentTableInfo' id='Item" + i + j + "' >"
                                        + "<a href='AttendExamNew.aspx?id=" + strId + "&employeeID=" + Request.QueryString.Get("employeeID")
                                        + "#Test" + i + j + "' target='ifExamInfo' style='cursor: hand;'><b>" + tempK + "-(" + z + ")</b></a></td>");
                                }

                                z++;
                                count++;
                            }
                        }

                        Response.Write("</tr >");
                        Response.Write("</table>");
                    }
                }

                ClientScript.RegisterStartupScript(GetType(), "StartStyle", "<script>StartStyle()</script>");
            }
            else
            {
                SessionSet.PageMessage = "δ�ҵ���¼��";
            }
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
    }
}
