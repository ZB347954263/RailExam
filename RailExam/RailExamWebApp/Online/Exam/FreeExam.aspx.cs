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

namespace RailExamWebApp.Online.Exam
{
    public partial class FreeExam : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
				try
				{
					HfExamTime.Value = (Convert.ToInt32(Request.QueryString.Get("Time")) * 60).ToString();
				}
				catch
				{
					Response.Redirect("/RailExamBao/Common/Error.aspx?error=测试时间设置错误！");
					return;
				}
                GetItems();

                ViewState["IsShow"] = "False";
            }
            string strAnswer = Request.Form.Get("strreturnAnswer");
            if (!string.IsNullOrEmpty(strAnswer))
            {
                ViewState["Answers"] = strAnswer;
                ViewState["Refresh"] = "";
            }
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

        private void FillPaperNoAnswer()
        {
            if (ViewState["SelectItem"].ToString() == "")
            {
                SessionSet.PageMessage = "您输入的条件没有找到试题！";
                return;
            }

            ItemBLL itemBLL = new ItemBLL();
            IList<RailExam.Model.Item> Items = new List<RailExam.Model.Item>();

            string[] strSelectItem = ViewState["SelectItem"].ToString().Split(new char[] { '|' });
            for (int n = 0; n < strSelectItem.Length; n++)
            {
                RailExam.Model.Item item = itemBLL.GetItem(int.Parse(strSelectItem[n]));
                Items.Add(item);
            }

            decimal decTotalScore1 = 0;
            decimal decTotalScore2 = 0;
            int n1 = 0;
            int n2 = 0;

            for (int i = 0; i < Items.Count; i++)
            {
                RailExam.Model.Item item = Items[i];
                if (item.TypeId == 1)
                {
                    decTotalScore1 += item.Score;
                    n1 += 1;
                }
                else
                {

                    decTotalScore2 += item.Score;
                    n2 += 1;
                }
            }

            if (Items != null)
            {
                string strHtml1 = "<table class='ExamContent'><tr><td class='ExamBigTitle' > 一、单选（共" + n1 + "题）</td></tr> ";

                string strHtml2 = "<table class='ExamContent'><tr><td class='ExamBigTitle' > 二、多选（共" + n2 + "题）</td></tr> ";

                int k1 = 0;
                int k2 = 0;

                for (int j = 0; j < Items.Count; j++)
                {
                    RailExam.Model.Item item = Items[j];
                    if (item.TypeId == PrjPub.ITEMTYPE_SINGLECHOOSE)
                    {
                        k1 += 1;

                        strHtml1 += "<tr><td id='Item"+j+"' class='ExamItem' >&nbsp;&nbsp;&nbsp;" + k1 + ".&nbsp;" + item.Content +
                                        "&nbsp;&nbsp;</td></tr>";

                        string[] strAnswer = item.SelectAnswer.Split(new char[] { '|' });
                        for (int n = 0; n < strAnswer.Length; n++)
                        {
                            string strN = intToString(n + 1);
                            string strij = "-1-" + item.ItemId + "-" + j.ToString() +
                                           "-" + n.ToString();
                            string strName = "1" + j.ToString();
                            strHtml1 += "<tr><td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='Radio' onclick='CheckStyle(this)' id='RAnswer" +
                                 strij + "' name='RAnswer" + strName + "'> " + strN + "." + strAnswer[n] +
                                 "</td></tr>";
                        }
                    }
                    else if(item.TypeId == PrjPub.ITEMTYPE_MULTICHOOSE)
                    {
                        k2 += 1;
                        strHtml2 += "<tr><td class='ExamItem'>&nbsp;&nbsp;&nbsp;" + k2 + ".&nbsp;" + item.Content +
                                       "&nbsp;&nbsp;</td></tr>";

                        string[] strAnswer = item.SelectAnswer.Split(new char[] { '|' });
                        for (int n = 0; n < strAnswer.Length; n++)
                        {
                            string strN = intToString(n + 1);
                            string strij = "-2-" + +item.ItemId + "-" + j.ToString() +
                                           "-" + n.ToString();
                            string strName = "2" + j.ToString();
                            strHtml2 += "<tr><td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='checkbox' id='RAnswer" +
                                  strij + "' name='RAnswer" + strName + "'> " + strN + "." + strAnswer[n] + "</td></tr>";

                        }
                    }
                }
                strHtml1 += " </table> ";
                strHtml2 += " </table> ";

                if (k1 > 0)
                {
                    Response.Write(strHtml1);
                }
                if (k2 > 0)
                {
                    Response.Write(strHtml2);
                }

                Response.Write("<div class='ExamButton'><input class='button' type='button' onclick='SaveRecord()' value='提 交'/></div>");
            }

        }

        private void FillPaperWithAnswer()
        {
            if(ViewState["IsShow"].ToString() == "True")
            {
                return;
            }

            decimal decTotalResultScore = 0;

            int rightNum = 0;
            int sumNum = 0;

            string strAllAnswer = ViewState["Answers"].ToString();
            string[] strAllAnswers = strAllAnswer.Split(new char[] { '$' });
            Hashtable hashTable = new Hashtable();
            for (int i = 0; i < strAllAnswers.Length; i++)
            {
                string str2 = strAllAnswers[i];
                string[] str3 = str2.Split(new char[] { '|' });
                string strItemId = str3[0];
                string strTrueAnswer = str2.Substring(strItemId.Length + 1);
                hashTable[int.Parse(strItemId)] = strTrueAnswer;
            }

            ItemBLL itemBLL = new ItemBLL();
            IList<RailExam.Model.Item> Items = new List<RailExam.Model.Item>();

            string[] strSelectItem = ViewState["SelectItem"].ToString().Split(new char[] { '|' });
            for (int n = 0; n < strSelectItem.Length; n++)
            {
                RailExam.Model.Item item = itemBLL.GetItem(int.Parse(strSelectItem[n]));
                Items.Add(item);
            }
            ViewState["SelectItem"] = "";
            decimal decTotalScore1 = 0;
            decimal decTotalScore2 = 0;
            int nCount1 = 0;
            int nCount2 = 0;


            for (int i = 0; i < Items.Count; i++)
            {
                RailExam.Model.Item item = Items[i];
                if (item.TypeId == 1)
                {
                    decTotalScore1 += item.Score;
                    nCount1 += 1;
                }
                else
                {

                    decTotalScore2 += item.Score;
                    nCount2 += 1;
                }
            }

            if (Items != null)
            {
                string strHtml1 = "<table class='ExamContent'><tr><td  class='ExamBigTitle'> 一、单选（共" + nCount1 + "题）</td></tr> ";

                string strHtml2 = "<table class='ExamContent'><tr><td class='ExamBigTitle'> 二、多选（共" + nCount2 + "题）</td></tr> ";

                sumNum = nCount1 + nCount2;

                int k1 = 0;
                int k2 = 0;
                for (int j = 0; j < Items.Count; j++)
                {
                    RailExam.Model.Item item = Items[j];
                    if (item.TypeId == 1)
                    {
                        k1 += 1;

                        strHtml1 += "<tr><td class='ExamItem'>&nbsp;&nbsp;&nbsp;" + k1 + ".&nbsp;" + item.Content +
                                        "&nbsp;&nbsp;</td></tr>";

                        string[] strAnswer = item.SelectAnswer.Split(new char[] { '|' });
                        for (int n = 0; n < strAnswer.Length; n++)
                        {
                            string strN = intToString(n + 1);
                            string strij = "-1-" + item.ItemId + "-" + j.ToString() +
                                           "-" + n.ToString();
                            string strName = "1" + j.ToString();
                            strHtml1 += "<tr><td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='Radio' id='RAnswer" +
                                 strij + "' name='RAnswer" + strName + "'> " + strN + "." + strAnswer[n] +
                                 "</td></tr>";
                        }

                        string[] strRightAnswers = item.StandardAnswer.Split(new char[] { '|' });
                        string strRightAnswer = "";
                        for (int n1 = 0; n1 < strRightAnswers.Length; n1++)
                        {
                            string strN1 = intToString(int.Parse(strRightAnswers[n1]) + 1);
                            if (n1 == 0)
                            {
                                strRightAnswer += strN1;
                            }
                            else
                            {
                                strRightAnswer += "," + strN1;
                            }
                        }

                        string strhashValue = hashTable[item.ItemId].ToString();
                        string strUserAnswer = "";
                        if (strhashValue != "")
                        {
                            string[] strUserAnswers = strhashValue.ToString().Split(new char[] { '|' });

                            for (int n2 = 0; n2 < strUserAnswers.Length; n2++)
                            {
                                string strN2 = intToString(int.Parse(strUserAnswers[n2]) + 1);
                                if (n2 == 0)
                                {
                                    strUserAnswer += strN2;
                                }
                                else
                                {
                                    strUserAnswer += "," + strN2;
                                }
                            }
                        }

                        if (strUserAnswer == strRightAnswer)
                        {
                            decTotalResultScore += item.Score;
                            rightNum = rightNum + 1;
                        }

						if (strUserAnswer == strRightAnswer)
						{
							strHtml1 += "<tr><td class='ExamAnswer'>&nbsp;&nbsp;&nbsp;★标准答案："
							            + "<span id='span-" + item.ItemId + "-0' name='span-" + item.ItemId
							            + "'>" + strRightAnswer + "</span>&nbsp;&nbsp;&nbsp;★考生答案："
							            + "<span id='span-" + item.ItemId + "-1' name='span-" + item.ItemId
							            + "'>" + strUserAnswer + "</span></td></tr>";
						}
						else
						{
							strHtml1 += "<tr><td class='ExamAnswerZero'>&nbsp;&nbsp;&nbsp;★标准答案："
										+ "<span id='span-" + item.ItemId + "-0' name='span-" + item.ItemId
										+ "'>" + strRightAnswer + "</span>&nbsp;&nbsp;&nbsp;★考生答案："
										+ "<span id='span-" + item.ItemId + "-1' name='span-" + item.ItemId
										+ "'>" + strUserAnswer + "</span></td></tr>";
						}
                    }
                    else
                    {
                        k2 += 1;
                        strHtml2 += "<tr><td class='ExamItem'>&nbsp;&nbsp;&nbsp;" + k2 + ".&nbsp;" + item.Content +
                                       "&nbsp;&nbsp;</td></tr>";

                        string[] strAnswer = item.SelectAnswer.Split(new char[] { '|' });
                        for (int n = 0; n < strAnswer.Length; n++)
                        {
                            string strN = intToString(n + 1);
                            string strij = "-2-" + +item.ItemId + "-" + j.ToString() +
                                           "-" + n.ToString();
                            string strName = "2" + j.ToString();
                            strHtml2 += "<tr><td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='checkbox' id='RAnswer" +
                                  strij + "' name='RAnswer" + strName + "'> " + strN + "." + strAnswer[n] + "</td></tr>";

                        }

                        string[] strRightAnswers = item.StandardAnswer.Split(new char[] { '|' });
                        string strRightAnswer = "";
                        for (int n1 = 0; n1 < strRightAnswers.Length; n1++)
                        {
                            string strN1 = intToString(int.Parse(strRightAnswers[n1]) + 1);
                            if (n1 == 0)
                            {
                                strRightAnswer += strN1;
                            }
                            else
                            {
                                strRightAnswer += "," + strN1;
                            }
                        }

                        string strhashValue = hashTable[item.ItemId].ToString();
                        string strUserAnswer = "";
                        if (strhashValue != "")
                        {
                            string[] strUserAnswers = strhashValue.ToString().Split(new char[] { '|' });

                            for (int n2 = 0; n2 < strUserAnswers.Length; n2++)
                            {
                                string strN2 = intToString(int.Parse(strUserAnswers[n2]) + 1);
                                if (n2 == 0)
                                {
                                    strUserAnswer += strN2;
                                }
                                else
                                {
                                    strUserAnswer += "," + strN2;
                                }
                            }
                        }

                        if (strUserAnswer == strRightAnswer)
                        {
                            decTotalResultScore += item.Score;
                            rightNum = rightNum + 1;
                        }

						if (strUserAnswer == strRightAnswer)
						{
							strHtml2 += "<tr><td class='ExamAnswer'>&nbsp;&nbsp;&nbsp;★标准答案："
							            + "<span id='span-" + item.ItemId + "-0' name='span-" + item.ItemId
							            + "'>" + strRightAnswer + "</span>&nbsp;&nbsp;&nbsp;★考生答案："
							            + "<span id='span-" + item.ItemId + "-1' name='span-" + item.ItemId
							            + "'>" + strUserAnswer + "</span></td></tr>";
						}
						else
						{
							strHtml2 += "<tr><td class='ExamAnswerZero'>&nbsp;&nbsp;&nbsp;★标准答案："
										+ "<span id='span-" + item.ItemId + "-0' name='span-" + item.ItemId
										+ "'>" + strRightAnswer + "</span>&nbsp;&nbsp;&nbsp;★考生答案："
										+ "<span id='span-" + item.ItemId + "-1' name='span-" + item.ItemId
										+ "'>" + strUserAnswer + "</span></td></tr>";
						}
                    }
                }
                strHtml1 += " </table> ";
                strHtml2 += " </table> ";
              

                if (k1 > 0)
                {
                    Response.Write(strHtml1);
                }
                if (k2 > 0)
                {
                    Response.Write(strHtml2);
                }

                double decn = 0;
                if (sumNum > 0)
                {
                    decn = rightNum*0.01*10000 / sumNum;
                }

                Response.Write("<table class='ExamContent'><tr><td class='ExamJudge'>★正确题数：" + rightNum + "&nbsp;&nbsp;&nbsp;&nbsp;正确率：" + decn.ToString("0.00") + "%</td></tr></table>");

                string strA = "总题数：" + sumNum + "，正确题数：" + rightNum + "，正确率：" + decn.ToString("0.00") + "%";
                SessionSet.PageMessage = strA;
            }

            ViewState["IsShow"] = "True";
        }

        protected void FillPaper()
        {
            if (ViewState["Refresh"] != null && ViewState["Refresh"].ToString() != "")
            {
                FillPaperNoAnswer();
            }

            if (ViewState["Answers"] != null && ViewState["Answers"].ToString() != "")
            {
                FillPaperWithAnswer();
            }
        }

        protected void GetItems()
        {
            ViewState["Refresh"] = "ok";
            ViewState["Answers"] = "";
            ViewState["SelectItem"] = "";
             ItemBLL itemBLL = new ItemBLL();

            string strBookID = Server.UrlDecode(Request.QueryString.Get("BookID"));
            string strRangeType = Request.QueryString.Get("RangeType");
            int nDifficultR = Convert.ToInt32(Request.QueryString.Get("Num"));

            string[] strBook = strBookID.Split('/');
            string[] strType = strRangeType.Split('/');

            IList<RailExam.Model.Item> itemList = new List<RailExam.Model.Item>();

        	int m = 0;
            if(strType[0] == "2" || strType[0] == "1")
            {
                itemList = itemBLL.GetItemsByStrategy(Convert.ToInt32(strType[0]), Convert.ToInt32(strBook[0]));
            }
            else
            {
				BookChapterBLL objChapterBll = new BookChapterBLL();
				ArrayList objItemID = new ArrayList();
                for (int i = 0; i < strBook.Length; i++)
                {
                    if (strType[i] == "4")
                    {
						BookChapter objChapter = objChapterBll.GetBookChapter(Convert.ToInt32(strBook[i]));
						if (objChapter.IsCannotSeeAnswer)
						{
							m = m + 1;
							break;
						}

                        IList<RailExam.Model.Item> objList = itemBLL.GetItemsByStrategy(4, Convert.ToInt32(strBook[i]));
                        foreach (RailExam.Model.Item item in objList)
                        {
							if(objItemID.IndexOf(item.ItemId) == -1)
							{
								itemList.Add(item);
								objItemID.Add(item.ItemId);
							}
                        }
                    }
                }
            }

			if(m>0)
			{
                Response.Write("<script>alert('存在被屏蔽试题的章节，请重新设置取题范围');window.parent.close();</script>");
				return;
			}

            if (itemList.Count >= nDifficultR)
            {
                Random ObjRandom = new Random();
                Hashtable hashTable = new Hashtable();
            	int n = 0;
                while (hashTable.Count < nDifficultR)
                {
                    int i = ObjRandom.Next(itemList.Count);
                    hashTable[itemList[i].ItemId] = itemList[i].ItemId;

					if(string.IsNullOrEmpty(itemList[i].StandardAnswer))
					{
						n = n + 1;
						break;
					}
                }

				if(n == 1)
				{
                    Response.Write("<script>alert('存在没有标准答案的试题，请重新设置取题范围');window.parent.close();</script>");
					return;
				}

                foreach (int key in hashTable.Keys)
                {
                    string strItemId = hashTable[key].ToString();
                    if (ViewState["SelectItem"] != null && ViewState["SelectItem"].ToString() != "")
                    {
                        ViewState["SelectItem"] += "|" + strItemId;
                    }
                    else
                    {
                        ViewState["SelectItem"] = strItemId;
                    }                  
                }
            }
            else
            {
				//Items = itemList;
				//for (int i = 0; i < Items.Count; i++)
				//{
				//    if (i == 0)
				//    {
				//        ViewState["SelectItem"] = Items[i].ItemId.ToString();
				//    }
				//    else
				//    {
				//        ViewState["SelectItem"] += "|" + Items[i].ItemId.ToString();
				//    }
				//}

				Response.Write("<script>alert('自主考试在设定的取题范围内的试题量不够，请重新设置取题范围');window.parent.close();</script>");
            	return;
            }
        }
    }
}
