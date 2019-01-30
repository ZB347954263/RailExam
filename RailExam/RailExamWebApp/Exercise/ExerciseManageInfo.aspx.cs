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

namespace RailExamWebApp.Exercise
{
    public partial class ExerciseManageInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strAnswer = Request.Form.Get("strreturnAnswer");
            if (!string.IsNullOrEmpty(strAnswer))
            {
                ViewState["Answers"] = strAnswer;
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
            ItemBLL itemBLL = new ItemBLL();
            string strBookId = Request.QueryString.Get("bookid");
            string strChapterId = Request.QueryString.Get("ChapterId");

            if (strChapterId == "0")
            {
                IList<RailExam.Model.Item> Items1 = itemBLL.GetExerciseItems(int.Parse(strBookId), int.Parse(strChapterId), 1);
                IList<RailExam.Model.Item> Items2 = itemBLL.GetExerciseItems(int.Parse(strBookId), int.Parse(strChapterId), 2);
                IList<RailExam.Model.Item> Items3 = itemBLL.GetExerciseItems(int.Parse(strBookId), int.Parse(strChapterId), 3);
                IList<RailExam.Model.Item> Items4 = itemBLL.GetExerciseItems(int.Parse(strBookId), int.Parse(strChapterId), 4);

                Response.Write("<div class='ExamBigTitle' style='text-align:center;' >");
                Response.Write(" 本教材共有" + Items1.Count + "道单选题、" + Items2.Count + "道多选题、<br>" + Items3.Count + "道判断题、" + Items4.Count + "道综合选择题，请点击章节查看练习题！");
                Response.Write("</div>");
            }
            else
            {
                BookChapterBLL objBll = new BookChapterBLL();
                BookChapter obj = objBll.GetBookChapter(Convert.ToInt32(strChapterId));
                hfCannotSeeAnswer.Value = obj.IsCannotSeeAnswer.ToString();

                if (Convert.ToBoolean(hfCannotSeeAnswer.Value))
                {
                    Response.Write("<div class='ExamBigTitle' style='text-align:center;' >");
                    Response.Write(" 本章节练习题已被屏蔽！");
                    Response.Write("</div>");
                    return;
                }


                int nCount = 0;
                //单选
                IList<RailExam.Model.Item> Items =
                    itemBLL.GetExerciseItems(int.Parse(strBookId), int.Parse(strChapterId), 1);
                decimal decTotalScore = 0;
                for (int i = 0; i < Items.Count; i++)
                {
                    RailExam.Model.Item item = Items[i];
                    decTotalScore += item.Score;
                }
                nCount += Items.Count;

                if (Items != null)
                {
                    Response.Write("<table class='ExamContent'>");
                    Response.Write("<tr><td class='ExamBigTitle' >");
                    Response.Write(" 一、单选（共" + Items.Count + "题）</td></tr>");

                    for (int j = 0; j < Items.Count; j++)
                    {
                        RailExam.Model.Item item = Items[j];
                        int k = j + 1;

                        Response.Write("<tr><td id='Item-1-"+j+"' class='ExamItem'>&nbsp;&nbsp;&nbsp;" + k + ".&nbsp;" + item.Content +
                                       "</td></tr>");

                        string[] strAnswer = item.SelectAnswer.Split(new char[] {'|'});
                        for (int n = 0; n < strAnswer.Length; n++)
                        {
                            string strN = intToString(n + 1);
                            string strij = "-1-" + item.ItemId + "-" + j.ToString() +
                                           "-" + n.ToString();
                            string strName = "1" + j.ToString();
                            Response.Write(
                                "<tr><td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='Radio' onclick='CheckStyle(this)'  id='RAnswer" +
                                strij + "' name='RAnswer" + strName + "'> " + strN + "." + strAnswer[n] +
                                "</td></tr>");
                        }
                    }
                    Response.Write(" </table> ");
                }

                //多选
                Items = itemBLL.GetExerciseItems(int.Parse(strBookId), int.Parse(strChapterId), 2);
                decTotalScore = 0;
                for (int i = 0; i < Items.Count; i++)
                {
                    RailExam.Model.Item item = Items[i];
                    decTotalScore += item.Score;
                }

                nCount += Items.Count;
                hfPaperItemsCount.Value = nCount.ToString();
                if (Items != null)
                {
                    Response.Write("<table class='ExamContent'>");
                    Response.Write("<tr> <td class='ExamBigTitle' >");
                    Response.Write(" 二、多选（共" + Items.Count + "题）</td></tr>");

                    for (int j = 0; j < Items.Count; j++)
                    {
                        RailExam.Model.Item item = Items[j];
                        int k = j + 1;

                        Response.Write("<tr><td id='Item-2-" + j + "' class='ExamItem'>&nbsp;&nbsp;&nbsp;" + k + ".&nbsp;" + item.Content +
                                       "</td></tr>");

                        string[] strAnswer = item.SelectAnswer.Split(new char[] {'|'});
                        for (int n = 0; n < strAnswer.Length; n++)
                        {
                            string strN = intToString(n + 1);
                            string strij = "-2-" + +item.ItemId + "-" + j.ToString() +
                                           "-" + n.ToString();
                            string strName = "2" + j.ToString();
                            Response.Write(
                                "<tr><td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='checkbox' onclick='CheckStyle(this)' id='RAnswer" +
                                strij + "' name='RAnswer" + strName + "'> " + strN + "." + strAnswer[n] + "</td></tr>");
                        }
                    }
                    Response.Write("</table> ");
                }

                //判断
                Items =
                    itemBLL.GetExerciseItems(int.Parse(strBookId), int.Parse(strChapterId), 3);
                decTotalScore = 0;
                for (int i = 0; i < Items.Count; i++)
                {
                    RailExam.Model.Item item = Items[i];
                    decTotalScore += item.Score;
                }
                nCount += Items.Count;
                hfPaperItemsCount.Value = nCount.ToString();

                if (Items != null)
                {
                    Response.Write("<table class='ExamContent'>");
                    Response.Write("<tr><td class='ExamBigTitle' >");
                    Response.Write("三、判断（共" + Items.Count + "题）</td></tr>");

                    for (int j = 0; j < Items.Count; j++)
                    {
                        RailExam.Model.Item item = Items[j];
                        int k = j + 1;

                        Response.Write("<tr><td id='Item-3-" + j + "' class='ExamItem'>&nbsp;&nbsp;&nbsp;" + k + ".&nbsp;" + item.Content +
                                       "</td></tr>");

                        string[] strAnswer = item.SelectAnswer.Split(new char[] { '|' });
                        for (int n = 0; n < strAnswer.Length; n++)
                        {
                            string strN = intToString(n + 1);
                            string strij = "-3-" + item.ItemId + "-" + j.ToString() +
                                           "-" + n.ToString();
                            string strName = "3" + j.ToString();
                            Response.Write(
                                "<tr><td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='Radio' onclick='CheckStyle(this)'  id='RAnswer" +
                                strij + "' name='RAnswer" + strName + "'> " + strN + "." + strAnswer[n] +
                                "</td></tr>");
                        }
                    }
                    Response.Write(" </table> ");
                }

                //综合选择题
                Items =
                    itemBLL.GetExerciseItems(int.Parse(strBookId), int.Parse(strChapterId), 4);
                decTotalScore = 0;
                for (int i = 0; i < Items.Count; i++)
                {
                    RailExam.Model.Item item = Items[i];
                    decTotalScore += item.Score;
                }
                nCount += Items.Count;
                hfPaperItemsCount.Value = nCount.ToString();

                if (Items != null)
                {
                    Response.Write("<table class='ExamContent'>");
                    Response.Write("<tr><td class='ExamBigTitle' colspan='3'>");
                    Response.Write("四、综合选择题（共" + Items.Count + "题）</td></tr>");

                    for (int j = 0; j < Items.Count; j++)
                    {
                        RailExam.Model.Item item = Items[j];
                        int k = j + 1;

                        Response.Write("<tr><td id='Item" + j + "' class='ExamItem' colspan='3'>&nbsp;&nbsp;&nbsp;" + k + ".&nbsp;" + item.Content +
                                       "</td></tr>");

                        IList<RailExam.Model.Item> objChildList = itemBLL.GetItemsByParentItemID(item.ItemId);

                        foreach (RailExam.Model.Item item1 in objChildList)
                        {
                            

                            string[] strAnswer = item1.SelectAnswer.Split(new char[] { '|' });
                            for (int n = 0; n < strAnswer.Length; n++)
                            {
                                string strN = intToString(n + 1);
                                string strij = "-4-" + item1.ItemId + "-" + item1.ItemId +
                                               "-" + n.ToString();
                                string strName = "4" + item1.ItemId;

                                if(n==0)
                                {
                                    int row = strAnswer.Length % 2 == 0 ? strAnswer.Length / 2 : strAnswer.Length / 2 + 1;
                                    Response.Write("<tr><td id='Item-4-" + item1.ItemId + "' class='ExamItemOther' style='width:10%;' RowSpan='" + row + "'>&nbsp;&nbsp;&nbsp;(" + item1.ItemIndex + ").&nbsp;</td>");
                                }

                                if (n % 2 == 0 && n != 0)
                                {
                                    Response.Write("<tr>");
                                }

                                Response.Write(
                                    "<td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='Radio' onclick='CheckStyle(this)'  id='RAnswer" +
                                    strij + "' name='RAnswer" + strName + "'> " + strN + "." + strAnswer[n] +
                                    "</td>");

                                if (n % 2 == 1)
                                {
                                    Response.Write("</tr>");
                                }
                            }
                        }
                    }
                    Response.Write(" </table> ");
                }

				if(!Convert.ToBoolean(hfCannotSeeAnswer.Value))
				{
					Response.Write("<div class='ExamButton'><input class='button' type='button' onclick='SaveRecord()' value='提交练习'/></div>");
				}
                Response.Write("<br><br>");
            }
        }

        private void FillPaperWithAnswer()
        {
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

            decimal decTotalResultScore = 0;
            ItemBLL itemBLL = new ItemBLL();
            string strBookId = Request.QueryString.Get("bookid");
            string strChapterId = Request.QueryString.Get("ChapterId");

            //单选
            decimal rightNum = 0;
            decimal sumNum = 0;

            IList<RailExam.Model.Item> Items = itemBLL.GetExerciseItems(int.Parse(strBookId), int.Parse(strChapterId), 1);
            decimal decTotalScore = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                RailExam.Model.Item item = Items[i];
                decTotalScore += item.Score;
            }

            if (Items != null)
            {
                sumNum = sumNum + Items.Count;
                Response.Write("<table width='95%' class='ExamContent'>");
                Response.Write("<tr><td class='ExamBigTitle'>");
                Response.Write(" 一、单选（共" + Items.Count + "题）</td></tr>");

                for (int j = 0; j < Items.Count; j++)
                {
                    RailExam.Model.Item item = Items[j];
                    int k = j + 1;

                    Response.Write("<tr><td class='ExamItem'>&nbsp;&nbsp;&nbsp;" + k + ".&nbsp;" + item.Content +"</td></tr>");

                    string[] strAnswer = item.SelectAnswer.Split(new char[] { '|' });
                    for (int n = 0; n < strAnswer.Length; n++)
                    {
                        string strN = intToString(n + 1);
                        string strij = "-" + item.ItemId + "-" + j.ToString() +
                                       "-" + n.ToString();
                        string strName = "1" + j.ToString();
                        Response.Write(
                            "<tr><td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='Radio' id='RAnswer" +
                            strij + "' name='RAnswer" + strName + "'> " + strN + "." + strAnswer[n] +
                            "</td></tr>");
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

					if(strUserAnswer == strRightAnswer)
					{
						Response.Write("<tr><td class='ExamAnswer'>&nbsp;&nbsp;&nbsp;★标准答案："
							+ "<span id='span-" + item.ItemId + "-0' name='span-" + item.ItemId
							+ "'>" + strRightAnswer + "</span>&nbsp;&nbsp;&nbsp;★考生答案："
							+ "<span id='span-" + item.ItemId + "-1' name='span-" + item.ItemId
							+ "'>" + strUserAnswer + "</span></td></tr>");
					}
					else
					{
						Response.Write("<tr><td class='ExamAnswerZero'>&nbsp;&nbsp;&nbsp;★标准答案："
							+ "<span id='span-" + item.ItemId + "-0' name='span-" + item.ItemId
							+ "'>" + strRightAnswer + "</span>&nbsp;&nbsp;&nbsp;★考生答案："
							+ "<span id='span-" + item.ItemId + "-1' name='span-" + item.ItemId
							+ "'>" + strUserAnswer + "</span></td></tr>");

					}
                }
                Response.Write("</table>");
            }

            //多选
            Items = itemBLL.GetExerciseItems(int.Parse(strBookId), int.Parse(strChapterId), 2);
            decimal decTotalScore1 = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                RailExam.Model.Item item = Items[i];
                decTotalScore1 += item.Score;
            }

            if (Items != null)
            {
                sumNum = sumNum + Items.Count;
                Response.Write("<table class='ExamContent'>");
                Response.Write("<tr><td  class='ExamBigTitle'>");
                Response.Write(" 二、多选（共" + Items.Count + "题）</td></tr>");

                for (int j = 0; j < Items.Count; j++)
                {
                    RailExam.Model.Item item = Items[j];
                    int k = j + 1;

                    Response.Write("<tr><td class='ExamItem'>&nbsp;&nbsp;&nbsp;" + k + ".&nbsp;" + item.Content + "</td></tr>");

                    string[] strAnswer = item.SelectAnswer.Split(new char[] { '|' });
                    for (int n = 0; n < strAnswer.Length; n++)
                    {
                        string strN = intToString(n + 1);
                        string strij = "-" + item.ItemId + "-" + j.ToString() +
                                       "-" + n.ToString();
                        string strName = "2" + j.ToString();
                        Response.Write(
                            "<tr><td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='checkbox' id='RAnswer" +
                            strij + "' name='RAnswer" + strName + "'> " + strN + "." + strAnswer[n] +
                            "</td></tr>");
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
						Response.Write("<tr><td class='ExamAnswer'>&nbsp;&nbsp;&nbsp;★标准答案："
						               + "<span id='span-" + item.ItemId + "-0' name='span-" + item.ItemId
						               + "'>" + strRightAnswer + "</span>&nbsp;&nbsp;&nbsp;★考生答案："
						               + "<span id='span-" + item.ItemId + "-1' name='span-" + item.ItemId
						               + "'>" + strUserAnswer + "</span></td></tr>");
					}
					else
					{
						Response.Write("<tr><td class='ExamAnswerZero'>&nbsp;&nbsp;&nbsp;★标准答案："
									   + "<span id='span-" + item.ItemId + "-0' name='span-" + item.ItemId
									   + "'>" + strRightAnswer + "</span>&nbsp;&nbsp;&nbsp;★考生答案："
									   + "<span id='span-" + item.ItemId + "-1' name='span-" + item.ItemId
									   + "'>" + strUserAnswer + "</span></td></tr>");
					}
                }
                Response.Write(" </table> ");
            }


            //判断
            Items = itemBLL.GetExerciseItems(int.Parse(strBookId), int.Parse(strChapterId), 3);
            decTotalScore = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                RailExam.Model.Item item = Items[i];
                decTotalScore += item.Score;
            }

            if (Items != null)
            {
                sumNum = sumNum + Items.Count;
                Response.Write("<table width='95%' class='ExamContent'>");
                Response.Write("<tr><td class='ExamBigTitle'>");
                Response.Write(" 三、判断（共" + Items.Count + "题）</td></tr>");

                for (int j = 0; j < Items.Count; j++)
                {
                    RailExam.Model.Item item = Items[j];
                    int k = j + 1;

                    Response.Write("<tr><td class='ExamItem'>&nbsp;&nbsp;&nbsp;" + k + ".&nbsp;" + item.Content + "</td></tr>");

                    string[] strAnswer = item.SelectAnswer.Split(new char[] { '|' });
                    for (int n = 0; n < strAnswer.Length; n++)
                    {
                        string strN = intToString(n + 1);
                        string strij = "-" + item.ItemId + "-" + j.ToString() +
                                       "-" + n.ToString();
                        string strName = "3" + j.ToString();
                        Response.Write(
                            "<tr><td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='Radio' id='RAnswer" +
                            strij + "' name='RAnswer" + strName + "'> " + strN + "." + strAnswer[n] +
                            "</td></tr>");
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
                        Response.Write("<tr><td class='ExamAnswer'>&nbsp;&nbsp;&nbsp;★标准答案："
                            + "<span id='span-" + item.ItemId + "-0' name='span-" + item.ItemId
                            + "'>" + strRightAnswer + "</span>&nbsp;&nbsp;&nbsp;★考生答案："
                            + "<span id='span-" + item.ItemId + "-1' name='span-" + item.ItemId
                            + "'>" + strUserAnswer + "</span></td></tr>");
                    }
                    else
                    {
                        Response.Write("<tr><td class='ExamAnswerZero'>&nbsp;&nbsp;&nbsp;★标准答案："
                            + "<span id='span-" + item.ItemId + "-0' name='span-" + item.ItemId
                            + "'>" + strRightAnswer + "</span>&nbsp;&nbsp;&nbsp;★考生答案："
                            + "<span id='span-" + item.ItemId + "-1' name='span-" + item.ItemId
                            + "'>" + strUserAnswer + "</span></td></tr>");

                    }
                }
                Response.Write("</table>");
            }

            //综合选择题
            Items = itemBLL.GetExerciseItems(int.Parse(strBookId), int.Parse(strChapterId), 4);
            decTotalScore = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                RailExam.Model.Item item = Items[i];
                decTotalScore += item.Score;
            }

            if (Items != null)
            {
                sumNum = sumNum + Items.Count;
                Response.Write("<table width='95%' class='ExamContent'>");
                Response.Write("<tr><td class='ExamBigTitle' colspan='3'>");
                Response.Write(" 四、综合选择题（共" + Items.Count + "题）</td></tr>");

                for (int j = 0; j < Items.Count; j++)
                {
                    RailExam.Model.Item item = Items[j];
                    int k = j + 1;

                    Response.Write("<tr><td class='ExamItem' colspan='3'>&nbsp;&nbsp;&nbsp;" + k + ".&nbsp;" + item.Content + "</td></tr>");


                     IList<RailExam.Model.Item> objChildList = itemBLL.GetItemsByParentItemID(item.ItemId);

                     foreach (RailExam.Model.Item item1 in objChildList)
                     {
                         string[] strAnswer = item1.SelectAnswer.Split(new char[] {'|'});
                         for (int n = 0; n < strAnswer.Length; n++)
                         {
                             string strN = intToString(n + 1);
                             string strij = "-" + item1.ItemId + "-" + item1.ItemId +
                                            "-" + n.ToString();
                             string strName = "4" + item1.ItemId;

                             if (n == 0)
                             {
                                 int row = strAnswer.Length % 2 == 0 ? strAnswer.Length / 2 : strAnswer.Length / 2 + 1;
                                 Response.Write("<tr><td class='ExamItemOther' style='width:10%' RowSpan='" + row + "'>&nbsp;&nbsp;&nbsp;(" + item1.ItemIndex + ").&nbsp;</td>");
                             }

                             if (n % 2 == 0 && n != 0)
                             {
                                 Response.Write("<tr>");
                             }

                             Response.Write(
                                 "<td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='Radio' id='RAnswer" +
                                 strij + "' name='RAnswer" + strName + "'> " + strN + "." + strAnswer[n] +
                                 "</td>");

                             if (n % 2 == 1)
                             {
                                 Response.Write("</tr>");
                             }
                         }

                         string[] strRightAnswers = item1.StandardAnswer.Split(new char[] { '|' });
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

                         string strhashValue = hashTable[item1.ItemId].ToString();
                         string strUserAnswer = "";
                         if (strhashValue != "")
                         {
                             string[] strUserAnswers = strhashValue.ToString().Split(new char[] {'|'});

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
                             decTotalResultScore += item1.Score;
                             rightNum = rightNum + (decimal)1 / (decimal)objChildList.Count;
                         }

                         if (strUserAnswer == strRightAnswer)
                         {
                             Response.Write("<tr><td class='ExamAnswer' colspan='3'>&nbsp;&nbsp;&nbsp;★标准答案："
                                            + "<span id='span-" + item1.ItemId + "-0' name='span-" + item1.ItemId
                                            + "'>" + strRightAnswer + "</span>&nbsp;&nbsp;&nbsp;★考生答案："
                                            + "<span id='span-" + item1.ItemId + "-1' name='span-" + item1.ItemId
                                            + "'>" + strUserAnswer + "</span></td></tr>");
                         }
                         else
                         {
                             Response.Write("<tr><td class='ExamAnswerZero' colspan='3'>&nbsp;&nbsp;&nbsp;★标准答案："
                                            + "<span id='span-" + item1.ItemId + "-0' name='span-" + item1.ItemId
                                            + "'>" + strRightAnswer + "</span>&nbsp;&nbsp;&nbsp;★考生答案："
                                            + "<span id='span-" + item1.ItemId + "-1' name='span-" + item1.ItemId
                                            + "'>" + strUserAnswer + "</span></td></tr>");

                         }
                     }
                }
                Response.Write("</table>");
            }

            decimal decn = 0;
            if (sumNum > 0)
            {
                decn = rightNum * (decimal)0.01 * (decimal)10000 / sumNum;
            }

            Response.Write("<table class='ExamContent'><tr><td class='ExamJudge'>★正确题数：" + rightNum + "&nbsp;&nbsp;&nbsp;&nbsp;正确率：" + decn.ToString("0.00") + "%</td></tr></table>");

            string strA = "总题数：" + sumNum + "，正确题数：" + rightNum + "，正确率：" + decn.ToString("0.00") + "%";
            SessionSet.PageMessage = strA;
        }

        protected void FillPaper()
        {
            if (ViewState["Answers"] != null && ViewState["Answers"].ToString() != "")
            {
                FillPaperWithAnswer();
            }
            else
            {
                FillPaperNoAnswer();
            }
        }
    }
}
