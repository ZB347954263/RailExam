using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
	public partial class RandomExamAnswerNew : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string orgID = Request.QueryString.Get("orgid");
			string strId = Request.QueryString.Get("id");
			ViewState["NowOrgID"] = Convert.ToInt32(ConfigurationManager.AppSettings["StationID"]);

			if (!string.IsNullOrEmpty(strId))
			{
				FillHeading(strId, orgID);
			}

            if(!IsPostBack)
            {
                if (PrjPub.CurrentLoginUser != null)
                {
                    hfLoginEmployeeID.Value = PrjPub.CurrentLoginUser.EmployeeID.ToString();
                }
                else
                {
                    RandomExamResultBLL bll = new RandomExamResultBLL();
                    RandomExamResult result = bll.GetRandomExamResult(Convert.ToInt32(strId));
                    hfLoginEmployeeID.Value = result.ExamineeId.ToString();

                    if(hfLoginEmployeeID.Value =="0")
                    {
                        RandomExamResult result1 = bll.GetRandomExamResultTemp(Convert.ToInt32(strId));
                        hfLoginEmployeeID.Value = result1.ExamineeId.ToString();
                    }
                }
            }
		}

		protected void FillHeading(string strId, string orgid)
		{
           	RandomExamResultBLL randomExamResultBLL = new RandomExamResultBLL();
			RandomExamResult randomExamResult = new RandomExamResult();
			randomExamResult = randomExamResultBLL.GetRandomExamResultStation(int.Parse(strId));

            OracleAccess db = new OracleAccess();
            string strSql = "select a.*"
                     + " from Random_Exam_Result_Detail a"
                     + " where a.Random_Exam_Result_ID=" + randomExamResult.RandomExamResultId
                     + " and Employee_ID=" + randomExamResult.ExamineeId
                     + " and Random_Exam_ID=" + randomExamResult.RandomExamId;

		    DataTable dtExam = db.RunSqlDataSet(strSql).Tables[0];

            if(dtExam.Rows.Count == 0)
            {
                strSql = "select a.*"
                     + " from Random_Exam_Result_Detail_Temp a"
                     + " where a.Random_Exam_Result_ID=" + randomExamResult.RandomExamResultId
                     + " and Employee_ID=" + randomExamResult.ExamineeId
                     + " and Random_Exam_ID=" + randomExamResult.RandomExamId;

               dtExam = db.RunSqlDataSet(strSql).Tables[0];
            }

            if (dtExam.Rows.Count > 0)
            {
                bool isExists =
                    Directory.Exists(Server.MapPath("/RailExamBao/Online/Photo/" + randomExamResult.RandomExamId + "/")); 

                DataRow drExam = dtExam.Rows[0];
                lblFignerDate.Text = drExam["FingerPrint_Date"] == DBNull.Value
                                         ? string.Empty
                                         : Convert.ToDateTime(drExam["FingerPrint_Date"]).ToString("yyyy-MM-dd HH:mm");
                lblPhotoDate1.Text = drExam["Photo1_Date"] == DBNull.Value
                                         ? string.Empty
                                         : Convert.ToDateTime(drExam["Photo1_Date"]).ToString("yyyy-MM-dd HH:mm");
                lblPhotoDate2.Text = drExam["Photo2_Date"] == DBNull.Value
                                         ? string.Empty
                                         : Convert.ToDateTime(drExam["Photo2_Date"]).ToString("yyyy-MM-dd HH:mm");
                lblPhotoDate3.Text = drExam["Photo3_Date"] == DBNull.Value
                                         ? string.Empty
                                         : Convert.ToDateTime(drExam["Photo3_Date"]).ToString("yyyy-MM-dd HH:mm");

                string filepath = Server.MapPath("/RailExamBao/Online/Photo/" + randomExamResult.RandomExamId + "/") + randomExamResult.ExamineeId + "_" + randomExamResult.RandomExamResultId + "_";
                string path = "../Online/Photo/" + randomExamResult.RandomExamId + "/" + randomExamResult.ExamineeId + "_" + randomExamResult.RandomExamResultId + "_";

                if (PrjPub.IsServerCenter && isExists)
                {
                    if (File.Exists(filepath + "00.jpg"))
                    {
                        fignerImage.ImageUrl = path + "00.jpg";
                    }
                    else
                    {
                        fignerImage.ImageUrl = "../images/empty.jpg";
                    }
                }
                else
                {
                    if (drExam["FingerPrint"] != DBNull.Value)
                    {
                        fignerImage.ImageUrl = "ShowResultImage.aspx?resultDetailID=" +
                                               drExam["Random_Exam_Result_Detail_ID"] + "&typeID=0";
                    }
                    else
                    {
                        fignerImage.ImageUrl = "../images/empty.jpg";
                    }
                }

                DataSet ds = Pub.GetPhotoDateSet(randomExamResult.ExamineeId.ToString());
                if(ds.Tables[0].Rows.Count>0)
                {
                    if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                    {
                        picImage.ImageUrl = "../RandomExamTai/ShowImage.aspx?EmployeeID=" + randomExamResult.ExamineeId;
                    }
                    else
                    {
                        picImage.ImageUrl = "../images/empty.jpg";
                    }
                }
                else
                {
                    picImage.ImageUrl = "../images/empty.jpg";
                }

                if (PrjPub.IsServerCenter && isExists)
                {
                    if (File.Exists(filepath + "01.jpg"))
                    {
                        photoImage1.ImageUrl = path + "01.jpg";
                    }
                    else
                    {
                        photoImage1.ImageUrl = "../images/empty.jpg";
                    }
                }
                else
                {
                    if (drExam["Photo1"] != DBNull.Value)
                    {
                        photoImage1.ImageUrl = "ShowResultImage.aspx?resultDetailID=" +
                                               drExam["Random_Exam_Result_Detail_ID"] + "&typeID=1";
                    }
                    else
                    {
                        photoImage1.ImageUrl = "../images/empty.jpg";
                    }
                }

                if (PrjPub.IsServerCenter && isExists)
                {
                    if (File.Exists(filepath + "02.jpg"))
                    {
                        photoImage2.ImageUrl = path + "02.jpg";
                    }
                    else
                    {
                        photoImage2.ImageUrl = "../images/empty.jpg";
                    }
                }
                else
                {
                    if (drExam["Photo2"] != DBNull.Value)
                    {
                        photoImage2.ImageUrl = "ShowResultImage.aspx?resultDetailID=" +
                                               drExam["Random_Exam_Result_Detail_ID"] + "&typeID=2";
                    }
                    else
                    {
                        photoImage2.ImageUrl = "../images/empty.jpg";
                    }
                }

                if (PrjPub.IsServerCenter && isExists)
                {
                    if (File.Exists(filepath + "03.jpg"))
                    {
                        photoImage3.ImageUrl = path + "03.jpg";
                    }
                    else
                    {
                        photoImage3.ImageUrl = "../images/empty.jpg";
                    }
                }
                else
                {
                    if (drExam["Photo3"] != DBNull.Value)
                    {
                        photoImage3.ImageUrl = "ShowResultImage.aspx?resultDetailID=" +
                                               drExam["Random_Exam_Result_Detail_ID"] + "&typeID=3";
                    }
                    else
                    {
                        photoImage3.ImageUrl = "../images/empty.jpg";
                    }
                }
            }
            else
            {
                fignerImage.ImageUrl = "../images/empty.jpg";
                DataSet ds = Pub.GetPhotoDateSet(randomExamResult.ExamineeId.ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                    {
                        picImage.ImageUrl = "../RandomExamTai/ShowImage.aspx?EmployeeID=" + randomExamResult.ExamineeId;
                    }
                    else
                    {
                        picImage.ImageUrl = "../images/empty.jpg";
                    }
                }
                else
                {
                    picImage.ImageUrl = "../images/empty.jpg";
                }
                photoImage1.ImageUrl = "../images/empty.jpg";
                photoImage2.ImageUrl = "../images/empty.jpg";
                photoImage3.ImageUrl = "../images/empty.jpg";
            }

            string strOrgName = randomExamResult.OrganizationName;
			string strStationName = "";
			string strOrgName1 = "";
			int n = strOrgName.IndexOf("-");
			if (n != -1)
			{
				strStationName = strOrgName.Substring(0, n);
				strOrgName1 = strOrgName.Substring(n + 1);
			}
			else
			{
				strStationName = strOrgName;
				strOrgName1 = "";
			}


			lblOrg.Text = strStationName;
			lblWorkShop.Text = strOrgName1;
			lblPost.Text = randomExamResult.PostName;
			lblName.Text = randomExamResult.ExamineeName;
			lblTime.Text = randomExamResult.BeginDateTime.ToString("yyyy-MM-dd HH:mm");
			lblScore.Text = System.String.Format("{0:0.##}", randomExamResult.Score);

			int RandomExamId = randomExamResult.RandomExamId;
			RandomExamBLL randomExamBLL = new RandomExamBLL();
			RailExam.Model.RandomExam randomExam = randomExamBLL.GetExam(RandomExamId);

			if (randomExam != null)
			{
				lblTitle.Text = randomExam.ExamName;
			}

			RandomExamSubjectBLL randomExamSubjectBLL = new RandomExamSubjectBLL();
			IList<RailExam.Model.RandomExamSubject> RandomExamSubjects = randomExamSubjectBLL.GetRandomExamSubjectByRandomExamId(RandomExamId);

			int nItemCount = 0;
			decimal nTotalScore = 0;
			for (int i = 0; i < RandomExamSubjects.Count; i++)
			{
				nItemCount += RandomExamSubjects[i].ItemCount;
				nTotalScore += RandomExamSubjects[i].ItemCount * RandomExamSubjects[i].UnitScore;
			}

			lblTitleRight.Text = "总共" + nItemCount + "题，共" + System.String.Format("{0:0.##}", nTotalScore) + "分";
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
				return Convert.ToChar((intCol - 1) / 26 + 64).ToString()
					   + Convert.ToChar((intCol - 1) % 26 + 64 + 1).ToString();
			}
		}

        private IList<RandomExamItem> GetSubjectItems(IList<RandomExamItem> objList, int subjectId)
        {
            IList<RandomExamItem> newItems = new List<RandomExamItem>();

            foreach (RandomExamItem randomExamItem in objList)
            {
                if(randomExamItem.SubjectId == subjectId)
                {
                    newItems.Add(randomExamItem);
                }
            }

            return newItems;
        }

		protected void FillPaper()
		{
			// QueryString id stands for EXAM_RESULT_ID
			string strId = Request.QueryString.Get("id");
			string orgid = Request.QueryString.Get("orgid");
			// Not pass id
			if (string.IsNullOrEmpty(strId))
			{
				SessionSet.PageMessage = "参数错误！";
				return;
			}

			RandomExamResultBLL randomExamResultBLL = new RandomExamResultBLL();
			RandomExamResult randomExamResult = new RandomExamResult();
			randomExamResult = randomExamResultBLL.GetRandomExamResultStation(int.Parse(strId));
			int RandomExamId = randomExamResult.RandomExamId;
			int randomExamResultId = int.Parse(strId);

			RandomExamBLL objBll = new RandomExamBLL();
			int year = objBll.GetExam(RandomExamId).BeginTime.Year;

			RandomExamItemBLL randomItemBLL = new RandomExamItemBLL();
			RandomExamSubjectBLL subjectBLL = new RandomExamSubjectBLL();
			IList<RandomExamSubject> randomExamSubjects = subjectBLL.GetRandomExamSubjectByRandomExamId(RandomExamId);

            //RandomExamResultAnswerBLL randomExamResultAnswerBLL = new RandomExamResultAnswerBLL();
            //IList<RandomExamResultAnswer> examResultAnswers = new List<RandomExamResultAnswer>();
            //if (ViewState["NowOrgID"].ToString() != orgid)
            //{
            //    examResultAnswers = randomExamResultAnswerBLL.GetExamResultAnswersStation(randomExamResultId);
            //}
            //else
            //{
            //    examResultAnswers = randomExamResultAnswerBLL.GetExamResultAnswers(randomExamResultId);
            //}

            IList<RandomExamItem> TotalItems = new List<RandomExamItem>();

            //按年取出每张试卷的Random_Exam_Item_Year 表中的记录
            if (ViewState["NowOrgID"].ToString() != orgid)
            {
                TotalItems = randomItemBLL.GetItemsStation(0, randomExamResultId, year);
            }
            else
            {
                TotalItems = randomItemBLL.GetItems(0, randomExamResultId, year);
            }

            OracleAccess db = new OracleAccess();

            //按考试取出每张试卷的完形填空子题
            string strSql = "select * from Random_Exam_Item_" + year + " where Type_ID="+ PrjPub.ITEMTYPE_FILLBLANKDETAIL +" and Random_Exam_ID=" + RandomExamId;
            DataTable dtDetail = db.RunSqlDataSet(strSql).Tables[0];

            //按考试取出每张试卷的完形填空主题
            //strSql = "select * from Random_Exam_Item_" + year + " where Type_ID="+ PrjPub.ITEMTYPE_FILLBLANK +" and Random_Exam_ID=" + RandomExamId;
            //DataTable dt= db.RunSqlDataSet(strSql).Tables[0];

            int z = 1;
		    int hasyear = 0;
			if (randomExamSubjects != null)
			{
				for (int i = 0; i < randomExamSubjects.Count; i++)
				{
					RandomExamSubject paperSubject = randomExamSubjects[i];
					IList<RandomExamItem> PaperItems = new List<RandomExamItem>();

                    PaperItems = GetSubjectItems(TotalItems, paperSubject.RandomExamSubjectId);
					
                    if (ViewState["NowOrgID"].ToString() != orgid)
					{
						//PaperItems = randomItemBLL.GetItemsStation(paperSubject.RandomExamSubjectId, randomExamResultId,  year);
					    hasyear = 1;
					}
					else
					{
						//PaperItems = randomItemBLL.GetItems(paperSubject.RandomExamSubjectId, randomExamResultId, year);
					    hasyear = 0;
					}

					Response.Write("<table width='100%'>");
					Response.Write(" <tr > <td class='ExamBigTitle' >");
					Response.Write(" " + GetNo(i) + "");
					Response.Write(".&nbsp;" + paperSubject.SubjectName + "");
					Response.Write("  （共" + paperSubject.ItemCount + "题，共" + System.String.Format("{0:0.##}", paperSubject.ItemCount * paperSubject.UnitScore) + "分）</td></tr >");

					if (PaperItems != null)
					{
					    int x = 1;
						for (int j = 0; j < PaperItems.Count; j++)
						{
							RandomExamItem paperItem = PaperItems[j];
							int k = j + 1;

                            if (paperItem.TypeId != 5)
                            {
                                if(paperItem.TypeId == 4)
                                {
                                    k = x;
                                    x++;
                                }

                                z = 1;
                                Response.Write("<tr > <td class='ExamResultItem'><a name='Test" + i + j + "' id='Test" + i + j + "'></a>&nbsp;&nbsp;&nbsp;"
                                               + k + ".&nbsp; " + paperItem.Content + "&nbsp;&nbsp;（" +
                                               System.String.Format("{0:0.##}", paperSubject.UnitScore) +
                                               "分）</td></tr >");
                            }
                            else
                            {
                                //string strSql = "select * from Random_Exam_Item_" + year + " where Item_ID='" +
                                //                paperItem.ItemId + "' and Random_Exam_ID=" + RandomExamId;
                                //DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];
                                //IList<RandomExamItem> randomExamItems = randomItemBLL.GetItemsByParentItemID(Convert.ToInt32(dr["Parent_Item_ID"]), RandomExamId, year);

                                //查找当前完型子题的主题ID
                                DataRow[] drsDetail = dtDetail.Select("Item_ID=" + paperItem.ItemId);

                                int detailCount = 0;
                                if (drsDetail.Length > 0)
                                {
                                    //通过完形填空的主题ID查找完形填空的子题个数，为计算每个子题分数做准备
                                    DataRow[] drs = dtDetail.Select("Parent_Item_ID=" + drsDetail[0]["Parent_Item_ID"]);
                                    detailCount = drs.Length;
                                }

                                Response.Write("<tr > <td class='ExamResultItem'><a name='Test" + i + j + "' id='Test" + i + j + "'></a>&nbsp;&nbsp;&nbsp;"//+ z + ").&nbsp; " 
                                               + paperItem.Content + "&nbsp;&nbsp;（" + System.String.Format("{0:0.##}", (decimal)paperSubject.UnitScore / (decimal)detailCount) +
                                               "分）</td></tr >");
                                z++;
                            }

                            string[] strUserAnswers = new string[0];
                            string strUserAnswer = string.Empty;

							// 组织用户答案
                            //RandomExamResultAnswer theExamResultAnswer = null;
                            //foreach (RandomExamResultAnswer resultAnswer in examResultAnswers)
                            //{
                            //    if (resultAnswer.RandomExamItemId == paperItem.RandomExamItemId)
                            //    {
                            //        theExamResultAnswer = resultAnswer;
                            //        break;
                            //    }
                            //}
                            //// 若子表无记录，结束页面输出
                            //if (theExamResultAnswer == null)
                            //{
                            //    SessionSet.PageMessage = "数据错误！";
                            //}
                            //// 否则组织考生答案
                            //if (theExamResultAnswer.Answer != null || theExamResultAnswer.Answer == string.Empty)
                            //{
                            //    strUserAnswers = theExamResultAnswer.Answer.Split(new char[] { '|' });
                            //}

                            if (paperItem.Answer == null)
                            {
                                SessionSet.PageMessage = "数据错误！";
                            }

                            if (!string.IsNullOrEmpty(paperItem.Answer))
                            {
                                strUserAnswers = paperItem.Answer.Split(new char[] { '|' });
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


							if (paperSubject.ItemTypeId == PrjPub.ITEMTYPE_MULTICHOOSE)
							{
								string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
								for (int n = 0; n < strAnswer.Length; n++)
								{
									string strN = intToString(n + 1);
									string strij = "-" + paperItem.RandomExamItemId + "-" + i.ToString() + "-"
												   + j.ToString() + "-" + n.ToString();
									string strName = i.ToString() + j.ToString();

									Response.Write(" <tr ><td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; "
												   + strN + "." + strAnswer[n] + "</td></tr >");
								}
							}
							else if(paperSubject.ItemTypeId == PrjPub.ITEMTYPE_JUDGE || paperSubject.ItemTypeId == PrjPub.ITEMTYPE_SINGLECHOOSE || paperItem.TypeId == 5)
							{
							    //单选


							    string[] strAnswer = paperItem.SelectAnswer.Split(new char[] {'|'});
							    for (int n = 0; n < strAnswer.Length; n++)
							    {
							        string strN = intToString(n + 1);
							        Response.Write("<tr > <td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; "
							                       + strN + "." + strAnswer[n] + "</td></tr >");
							    }
							}

                            if (paperSubject.ItemTypeId == 1 || paperSubject.ItemTypeId == 2 || paperSubject.ItemTypeId == 3 || paperItem.TypeId == 5)
                            {

                                // 组织正确答案
                                string[] strRightAnswers = paperItem.StandardAnswer.Split(new char[] {'|'});
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

                                string strScore = "0";
                                string couldScore = "0";
                                if (paperItem.TypeId == 5)
                                {
                                    if (strRightAnswer == strUserAnswer)
                                    {
                                        strScore = System.String.Format("{0:0.##}", paperItem.Score);
                                    }

                                    couldScore = paperItem.Score.ToString();
                                }
                                else
                                {
                                    if (strRightAnswer == strUserAnswer)
                                    {
                                        strScore = System.String.Format("{0:0.##}", paperSubject.UnitScore);
                                    }

                                    couldScore = paperSubject.UnitScore.ToString();
                                }

                                if (strScore == "0")
                                {
                                    if (hfLoginEmployeeID.Value == "0")
                                    {
                                        string strFirst = @"""";
                                        Response.Write(" <tr><td id='span-" + paperItem.RandomExamItemId+"' class='ExamAnswerZero'>&nbsp;&nbsp;&nbsp;★标准答案："
                                                    + "<span id='span-" + paperItem.RandomExamItemId + "-0' name='span-" +
                                                    paperItem.RandomExamItemId
                                                    + "'>" + strRightAnswer +
                                                    "</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;考生答案："
                                                    + "<span id='span-" + paperItem.RandomExamItemId + "-1' name='span-" +
                                                    paperItem.RandomExamItemId
                                                    + "'>" + strUserAnswer +
                                                    "</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;得分：" +
                                                    "<span id='span-" + paperItem.RandomExamItemId + "-2'>" + strScore +
                                                    "</span>"
                                                    + "&nbsp;&nbsp;&nbsp;<a id='a-" + paperItem.RandomExamItemId + "' onclick=" + strFirst + "updateScore(" + randomExamResultId + "," + paperItem.RandomExamItemId + "," + couldScore + ",'" + paperItem.StandardAnswer + "'," + hasyear + ")" + strFirst
                                                    + " href='#Test" + i + j + "' style='cursor: hand;'>更改考生答案</a></td></tr>"); 
                                    }
                                    else
                                    {
                                        Response.Write(" <tr><td class='ExamAnswerZero'>&nbsp;&nbsp;&nbsp;★标准答案："
                                                    + "<span id='span-" + paperItem.RandomExamItemId + "-0' name='span-" +
                                                    paperItem.RandomExamItemId
                                                    + "'>" + strRightAnswer +
                                                    "</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;考生答案："
                                                    + "<span id='span-" + paperItem.RandomExamItemId + "-1' name='span-" +
                                                    paperItem.RandomExamItemId
                                                    + "'>" + strUserAnswer +
                                                    "</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;得分：" +
                                                    "<span id='span-" + paperItem.RandomExamItemId + "-2'>" + strScore +
                                                    "</span></td></tr>"); 
                                    }
                                    
                                }
                                else
                                {
                                    Response.Write(" <tr><td class='ExamAnswer'>&nbsp;&nbsp;&nbsp;★标准答案："
                                                   + "<span id='span-" + paperItem.RandomExamItemId + "-0' name='span-" +
                                                   paperItem.RandomExamItemId
                                                   + "'>" + strRightAnswer +
                                                   "</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;考生答案："
                                                   + "<span id='span-" + paperItem.RandomExamItemId + "-1' name='span-" +
                                                   paperItem.RandomExamItemId
                                                   + "'>" + strUserAnswer +
                                                   "</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;得分：" + strScore +
                                                   "</td></tr>");
                                }
                            }
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

        protected void updateCallback_Callback(object sender,CallBackEventArgs e)
        {
            string resultId = e.Parameters[0];
            string itemId = e.Parameters[1];
            string answer =  e.Parameters[2];
            string hasYear = e.Parameters[3];
            RandomExamResultAnswerBLL objBll =new RandomExamResultAnswerBLL();

            objBll.UpdateExamResultAnswer(Convert.ToInt32(resultId), Convert.ToInt32(itemId), answer,
                                          Convert.ToInt32(hasYear));
        }
	}
}
