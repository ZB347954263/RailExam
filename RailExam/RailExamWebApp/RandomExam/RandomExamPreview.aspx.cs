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

namespace RailExamWebApp.RandomExam
{
    public partial class RandomExamPreview : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strExamId = Request.QueryString.Get("id");
                ViewState["BeginTime"] = DateTime.Now.ToString();

                if (strExamId != null && strExamId != "")
                {
                    FillPage(strExamId);
                }
            }
        }

        protected void FillPage(string strExamId)
        {
            RandomExamBLL randomExamBLL = new RandomExamBLL();
            RailExam.Model.RandomExam randomExam = randomExamBLL.GetExam(int.Parse(strExamId));
            
			if (randomExam != null)
            {
				ViewState["Year"] = randomExam.BeginTime.Year.ToString();
                lblTitle.Text = randomExam.ExamName;
            }

            RandomExamSubjectBLL randomExamSubjectBLL = new RandomExamSubjectBLL();
            IList<RailExam.Model.RandomExamSubject> RandomExamSubjects = randomExamSubjectBLL.GetRandomExamSubjectByRandomExamId(int.Parse(strExamId));

            int nItemCount = 0;
            decimal nTotalScore = 0;
            for (int i = 0; i < RandomExamSubjects.Count; i++)
            {
                nItemCount += RandomExamSubjects[i].ItemCount;
                nTotalScore += RandomExamSubjects[i].ItemCount * RandomExamSubjects[i].UnitScore;
            }

            // 用于前台JS判断是否完成全部试题
            hfPaperItemsCount.Value = nItemCount.ToString();

            lblTitleRight.Text = "总共" + nItemCount + "题，共 " + nTotalScore + "分";

        }

        protected void FillPaper()
        {
            string strId = Request.QueryString.Get("id");
            if (string.IsNullOrEmpty(strId))
            {
                SessionSet.PageMessage = "缺少参数！";

                return;
            }

            RandomExamItemBLL randomItemBLL = new RandomExamItemBLL();
            RandomExamSubjectBLL subjectBLL = new RandomExamSubjectBLL();
            RandomExamStrategyBLL strategyBLL = new RandomExamStrategyBLL();

            IList<RandomExamSubject> randomExamSubjects = subjectBLL.GetRandomExamSubjectByRandomExamId(int.Parse(strId));

            if (randomExamSubjects != null)
            {
                Hashtable hashTableItemIds = new Hashtable();
                for (int i = 0; i < randomExamSubjects.Count; i++)
                {
                    RandomExamSubject paperSubject = randomExamSubjects[i];
                    int nSubjectId = paperSubject.RandomExamSubjectId;
                  //  int nItemCount = paperSubject.ItemCount;

                    Hashtable htSubjectItemIds = new Hashtable();
                    IList<RandomExamStrategy> strategys = strategyBLL.GetRandomExamStrategys(nSubjectId);
                    for (int j = 0; j < strategys.Count; j++)
                    {
                        int nStrategyId = strategys[j].RandomExamStrategyId;
                        int nItemCount = strategys[j].ItemCount;
                        IList<RandomExamItem> itemList = randomItemBLL.GetItemsByStrategyId(nStrategyId,Convert.ToInt32(ViewState["Year"].ToString()));

                        // IList<RandomExamItem> itemList = randomItemBLL.GetItemsBySubjectId(nSubjectId);

                        Random ObjRandom = new Random();
                        Hashtable hashTable = new Hashtable();
                        Hashtable hashTableCount = new Hashtable();
                        while (hashTable.Count < nItemCount)
                        {
                            int k = ObjRandom.Next(itemList.Count);
                            hashTableCount[k] = k;
                            int itemID = itemList[k].ItemId;
                            int examItemID = itemList[k].RandomExamItemId;
                            if (!hashTableItemIds.ContainsKey(itemID))
                            {
                                hashTable[examItemID] = examItemID;
                                hashTableItemIds[itemID] = itemID;
                                htSubjectItemIds[examItemID] = examItemID;
                            }
                            if (hashTableCount.Count == itemList.Count && hashTable.Count < nItemCount)
                            {
                                SessionSet.PageMessage = "随机考试在设定的取题范围内的试题量不够，请重新设置取题范围！";
                                return;
                            }
                        }
                    }

                    IList<RandomExamItem> paperItems = new List<RandomExamItem>();

                    foreach (int key in htSubjectItemIds.Keys)
                    {
                        string strItemId = htSubjectItemIds[key].ToString();
                        RandomExamItem item = randomItemBLL.GetRandomExamItem(int.Parse(strItemId),Convert.ToInt32(ViewState["Year"].ToString()));
                        paperItems.Add(item);
                    }


                    Response.Write("<table width='95%' class='ExamContent'>");
                    Response.Write(" <tr> <td class='ExamBigTitle' >");
                    Response.Write(" " + GetNo(i) + "");
                    Response.Write("、" + paperSubject.SubjectName + "");
                    Response.Write("  （共" + paperItems.Count + "题，共" + paperItems.Count * paperSubject.UnitScore + "分）</td></tr >");

                    if (paperItems != null)
                    {
                        int y = 1;
                        for (int j = 0; j < paperItems.Count; j++)
                        {
                            RandomExamItem paperItem = paperItems[j];
                            int k = j + 1;

                            if(paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                            {
                                k = y;
                                y++;
                            }
                            else if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                            {
                                continue;
                            }

                            Response.Write("<tr><td class='ExamItem'>&nbsp;&nbsp;&nbsp;" + k + ".&nbsp; " + paperItem.Content +
                                           "&nbsp;&nbsp; （" + paperSubject.UnitScore + "分）</td></tr >");

                            if (paperSubject.ItemTypeId == PrjPub.ITEMTYPE_MULTICHOOSE)   //多选
                            {
                                string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
                                for (int n = 0; n < strAnswer.Length; n++)
                                {
                                    string strN = intToString(n + 1);
                                    string strij = "-" + paperItem.RandomExamItemId + "-" + i.ToString() + "-" + j.ToString() +
                                                   "-" + n.ToString();
                                    string strName = i.ToString() + j.ToString();

                                    Response.Write(
                                        "<tr><td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input type='checkbox' id='Answer" +
                                        strij + "' name='Answer" + strName + "'> " + strN + "." + strAnswer[n] +
                                        "</td></tr>");
                                }
                            }
                            else if(paperSubject.ItemTypeId == PrjPub.ITEMTYPE_SINGLECHOOSE || paperSubject.ItemTypeId == PrjPub.ITEMTYPE_JUDGE)    //单选
                            {

                                string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
                                for (int n = 0; n < strAnswer.Length; n++)
                                {
                                    string strN = intToString(n + 1);
                                    string strij = "-" + paperItem.RandomExamItemId + "-" + i.ToString() + "-" + j.ToString() +
                                                   "-" + n.ToString();
                                    string strName = i.ToString() + j.ToString();
                                    Response.Write(
                                        "<tr><td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input type='Radio' id='RAnswer" +
                                        strij + "' name='RAnswer" + strName + "'> " + strN + "." + strAnswer[n] +
                                        "</td></tr>");
                                }
                            }
                            else if(paperSubject.ItemTypeId == PrjPub.ITEMTYPE_FILLBLANK)
                            {
                                IList<RandomExamItem> randomExamItems =
                                    randomItemBLL.GetItemsByParentItemID(paperItem.ItemId,Convert.ToInt32(strId),
                                                                         Convert.ToInt32(ViewState["Year"].ToString()));

                                int z = 1;
                                foreach (RandomExamItem randomExamItem in randomExamItems)
                                {
                                    Response.Write("<tr><td class='ExamItem'>&nbsp;&nbsp;&nbsp;(" + z + ").&nbsp; " + randomExamItem.Content +
                                        "&nbsp;&nbsp; （" + System.String.Format("{0:0.##}", (decimal)paperSubject.UnitScore / (decimal)randomExamItems.Count) + "分）</td></tr >");

                                    string[] strAnswer = randomExamItem.SelectAnswer.Split(new char[] { '|' });
                                    for (int n = 0; n < strAnswer.Length; n++)
                                    {
                                        string strN = intToString(n + 1);
                                        string strij = "-" + randomExamItem.RandomExamItemId + "-" + i.ToString() + "-" + j.ToString() +
                                                       "-" + n.ToString() + "-" + z.ToString();
                                        string strName = i.ToString() + j.ToString() + z.ToString();

                                        Response.Write(
                                            "<tr><td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input type='Radio' id='RAnswer" +
                                            strij + "' name='RAnswer" + strName + "'> " + strN + "." + strAnswer[n] +
                                            "</td></tr>");
                                    }
                                    z++;
                                }
                            }
							else
							{
								Response.Write(
										"<tr><td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <textarea id='"+ paperItem.RandomExamItemId +"' rows='5' cols='120' ></textarea></td></tr>");
							}
                        }
                    }
                    Response.Write("</table>");
                }

                Response.Write(" <div class='ExamButton'><input id='btnClose' class='button' name='btnClose' type='button' value='关闭' onclick='Save()' /></div>");
            }
            else
            {
                SessionSet.PageMessage = "未找到记录！";
            }
        }

        #region Common function
        private int GetSecondBetweenTwoDate(DateTime dt1, DateTime dt2)
        {
            int i1 = dt1.Hour * 3600 + dt1.Minute * 60 + dt1.Second;
            int i2 = dt2.Hour * 3600 + dt2.Minute * 60 + dt2.Second;

            return i1 - i2;
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
        #endregion
    }
}
