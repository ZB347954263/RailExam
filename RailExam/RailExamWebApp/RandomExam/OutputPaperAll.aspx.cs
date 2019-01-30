using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using mshtml;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;
using Word;

namespace RailExamWebApp.RandomExam
{
	public partial class OutputPaperAll : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				if(Request.QueryString.Get("Mode") == "one")
				{
					OutputWord();
				}
				else if(Request.QueryString.Get("Mode") == "All")
				{
					OutputWordAll();
				}
			}
		}

		#region 导出一个考生试卷
		private void OutputWord()
		{
			// 根据 ProgressBar.htm 显示进度条界面
			string templateFileName = Path.Combine(Server.MapPath("."), "ProgressBar.htm");
			StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
			string html = reader.ReadToEnd();
			reader.Close();
			Response.Write(html);
			Response.Flush();
			System.Threading.Thread.Sleep(200);

			string strId = Request.QueryString.Get("eid");
			string orgid = Request.QueryString.Get("OrgID");
			string strName = "";
			object filename = null;

			object tableBehavior = WdDefaultTableBehavior.wdWord9TableBehavior;
			object autoFitBehavior = WdAutoFitBehavior.wdAutoFitFixed;

			object unit = WdUnits.wdStory;
			object extend = Missing.Value;
			object breakType = (int)WdBreakType.wdSectionBreakNextPage;

			object count = 1;
			object character = WdUnits.wdCharacter;

			object Nothing = Missing.Value;

			object LinkToFile = false;
			object SaveWithDocument = true;

			Application myWord = null;
			_Document myDoc = null;
			try
			{
				string jsBlock;
				myWord = new ApplicationClass();
				myDoc = new DocumentClass();
				//生成.doc文件完整路径名 
				filename = Server.MapPath("/RailExamBao/Excel/Word.doc");

				if (File.Exists(filename.ToString()))
				{
					File.Delete(filename.ToString());
				}
				
				//创建一个word文件，文件名用系统时间生成精确到毫秒 
				myDoc = myWord.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
				myDoc.Activate();

				myDoc.PageSetup.LeftMargin = (float)56.8;//左边距2cm
				myDoc.PageSetup.RightMargin = (float)56.8;
				myDoc.PageSetup.TopMargin = (float)56.8;
				myDoc.PageSetup.BottomMargin = (float)56.8;

				Word.Range para = myDoc.Content.Paragraphs[1].Range;
				para.Text = "武汉铁路局职工培训考试试卷";
				para.Font.Bold = 1;
				para.Font.Size = 12;
				para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
				para.InsertParagraphAfter();

				# region 考卷信息
				RandomExamResultBLL randomExamResultBLL = new RandomExamResultBLL();
				RandomExamResult randomExamResult = new RandomExamResult();
				if (PrjPub.IsServerCenter)
				{
					randomExamResult = randomExamResultBLL.GetRandomExamResultByOrgID(int.Parse(strId), int.Parse(orgid));
				}
				else
				{
					randomExamResult = randomExamResultBLL.GetRandomExamResult(int.Parse(strId));
				}

				string strOrgName = randomExamResult.OrganizationName;
				string strStationName = "";
				string strOrgName1 = "";
				int m = strOrgName.IndexOf("-");
				if (m != -1)
				{
					strStationName = strOrgName.Substring(0, m);
					strOrgName1 = strOrgName.Substring(m + 1);
				}
				else
				{
					strStationName = strOrgName;
					strOrgName1 = "";
				}

				int RandomExamId = randomExamResult.RandomExamId;
				RandomExamBLL randomExamBLL = new RandomExamBLL();
				RailExam.Model.RandomExam randomExam = randomExamBLL.GetExam(RandomExamId);
				int year = randomExam.BeginTime.Year;
				RandomExamSubjectBLL randomExamSubjectBLL = new RandomExamSubjectBLL();
				IList<RandomExamSubject> randomExamSubjects = randomExamSubjectBLL.GetRandomExamSubjectByRandomExamId(RandomExamId);

				int nItemCount = 0;
				decimal nTotalScore = 0;
				for (int i = 0; i < randomExamSubjects.Count; i++)
				{
					nItemCount += randomExamSubjects[i].ItemCount;
					nTotalScore += randomExamSubjects[i].ItemCount * randomExamSubjects[i].UnitScore;
				}

				#endregion

				para = myDoc.Content.Paragraphs[2].Range;
				para.Text = "考试名称：" + randomExam.ExamName;
				para.Font.Size = 9;
				para.Font.Bold = 0;
				para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
				para.InsertParagraphAfter();

				para = myDoc.Content.Paragraphs[3].Range;
				para.Text = "总共" + nItemCount + "题，共 " + String.Format("{0:0.#}", nTotalScore) + "分";
				para.Font.Size = 9;
				para.Font.Bold = 0;
				para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
				para.InsertParagraphAfter();

				para = myDoc.Content.Paragraphs[4].Range;
				myDoc.Tables.Add(para, 2, 6, ref tableBehavior, ref autoFitBehavior);
				para.Tables[1].Columns[1].PreferredWidth = 45;
				para.Tables[1].Columns[2].PreferredWidth = 120;
				para.Tables[1].Columns[3].PreferredWidth = 45;
				para.Tables[1].Columns[4].PreferredWidth = 120;
				para.Tables[1].Columns[5].PreferredWidth = 45;
				para.Tables[1].Columns[6].PreferredWidth = 120;
				para.Tables[1].Cell(1, 1).Range.Text = "单位:";
				para.Tables[1].Cell(1, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

				para.Tables[1].Cell(1, 2).Range.Text = strStationName;
				para.Tables[1].Cell(1, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

				para.Tables[1].Cell(1, 3).Range.Text = "车间:";
				para.Tables[1].Cell(1, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

				para.Tables[1].Cell(1, 4).Range.Text = strOrgName1;
				para.Tables[1].Cell(1, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

				para.Tables[1].Cell(1, 5).Range.Text = "职名:";
				para.Tables[1].Cell(1, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

				para.Tables[1].Cell(1, 6).Range.Text = randomExamResult.PostName;
				para.Tables[1].Cell(1, 6).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

				para.Tables[1].Cell(2, 1).Range.Text = "姓名:";
				para.Tables[1].Cell(2, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

				strName = randomExamResult.ExamineeName;
				para.Tables[1].Cell(2, 2).Range.Text = randomExamResult.ExamineeName;
				para.Tables[1].Cell(2, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

				para.Tables[1].Cell(2, 3).Range.Text = "时间:";
				para.Tables[1].Cell(2, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

				para.Tables[1].Cell(2, 4).Range.Text = randomExamResult.BeginDateTime.ToString("yyyy-MM-dd HH:mm");
				para.Tables[1].Cell(2, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

				para.Tables[1].Cell(2, 5).Range.Text = "成绩:";
				para.Tables[1].Cell(2, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

				para.Tables[1].Cell(2, 6).Range.Text = String.Format("{0:0.#}", randomExamResult.Score);
				para.Tables[1].Cell(2, 6).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

				para.Font.Size = 9;
				para.Font.Bold = 0;
				para.InsertParagraphAfter();


				int randomExamResultId = int.Parse(strId);
				RandomExamItemBLL randomItemBLL = new RandomExamItemBLL();
				RandomExamResultAnswerBLL randomExamResultAnswerBLL = new RandomExamResultAnswerBLL();
				IList<RandomExamResultAnswer> examResultAnswers;

				if (PrjPub.IsServerCenter)
				{
					examResultAnswers = randomExamResultAnswerBLL.GetExamResultAnswersByOrgID(int.Parse(strId), int.Parse(orgid));
				}
				else
				{
					examResultAnswers = randomExamResultAnswerBLL.GetExamResultAnswers(int.Parse(strId));
				}

				int num = 18;
				int NowCount = 0;
				for (int i = 0; i < randomExamSubjects.Count; i++)
				{
					RandomExamSubject paperSubject = randomExamSubjects[i];

					IList<RandomExamItem> paperSubjectItems;
					if (PrjPub.IsServerCenter)
					{
						paperSubjectItems = randomItemBLL.GetItemsByOrgID(paperSubject.RandomExamSubjectId, randomExamResultId, int.Parse(orgid),year);
					}
					else
					{
						paperSubjectItems = randomItemBLL.GetItems(paperSubject.RandomExamSubjectId, randomExamResultId,year);
					}

					para = myDoc.Content.Paragraphs[num].Range;
					num = num + 1;
					para.Text = GetNo(i) + "、" + paperSubject.SubjectName + "   （共" + paperSubject.ItemCount + "题，共" +
								String.Format("{0:0.#}", paperSubject.ItemCount * paperSubject.UnitScore) + "分）";
					para.Font.Size = (float)10.5;
					para.Font.Bold = 0;
					para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
					para.InsertParagraphAfter();

					if (paperSubjectItems != null)
					{
						for (int j = 0; j < paperSubjectItems.Count; j++)
						{
							RandomExamItem paperItem = paperSubjectItems[j];
							int k = j + 1;

							bool isPictureItem = paperItem.Content.ToLower().Contains("<img") ||
												 paperItem.SelectAnswer.ToLower().Contains("<img");
							if (!isPictureItem)
							{
								#region 输出文本试题题目
								para = myDoc.Content.Paragraphs[num].Range;
								num = num + 1;
								para.Text = k + ". " + paperItem.Content + "   （" + String.Format("{0:0.#}", paperSubject.UnitScore) + "分）";
								para.Font.Size = 9;
								para.Font.Bold = 0;
								para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
								para.InsertParagraphAfter();
								#endregion
							}
							else
							{
								#region 输出图片试题题目
								IHTMLDocument2 doc = new HTMLDocumentClass();
								doc.write(new object[] { paperItem.Content });
								doc.close();

								string[] src = new string[doc.images.length];
								string[] strImage = new string[doc.images.length];
								int t = 0;
								foreach (IHTMLImgElement image in doc.images)
								{
									IHTMLElement element = (IHTMLElement)image;
									strImage[t] = element.outerHTML;
									src[t] = (string)element.getAttribute("src", 2);
									t = t + 1;
								}

								string strItem = paperItem.Content;
								for (int x = 0; x < strImage.Length; x++)
								{
									strItem = strItem.Replace(strImage[x], "@");
								}

								string[] strText = strItem.Split('@');

								para = myDoc.Content.Paragraphs[num].Range;
								num = num + 1;
								para.Select();
								myWord.Application.Selection.TypeText(k + ". ");
								for (int x = 0; x < strText.Length; x++)
								{
									myWord.Application.Selection.TypeText(ItemBLL.NoHTML(strText[x]));
									if (x < src.Length)
									{
										myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile, ref SaveWithDocument, ref Nothing);
									}
								}
								myWord.Application.Selection.TypeText("   （" + String.Format("{0:0.#}", paperSubject.UnitScore) + "分）");
								para.Font.Size = 9;
								para.Font.Bold = 0;
								para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
								para.InsertParagraphAfter();
								#endregion
							}

							// 组织用户答案
							RandomExamResultAnswer theExamResultAnswer = null;
							string[] strUserAnswers = new string[0];
							string strUserAnswer = string.Empty;

							foreach (RandomExamResultAnswer resultAnswer in examResultAnswers)
							{
								if (resultAnswer.RandomExamItemId == paperItem.RandomExamItemId)
								{
									theExamResultAnswer = resultAnswer;
									break;
								}
							}

							// 若子表无记录，结束页面输出
							if (theExamResultAnswer == null)
							{
								SessionSet.PageMessage = "数据错误！";
							}

							// 否则组织考生答案
							if (theExamResultAnswer.Answer != null || theExamResultAnswer.Answer == string.Empty)
							{
								strUserAnswers = theExamResultAnswer.Answer.Split('|');
							}
							for (int n = 0; n < strUserAnswers.Length; n++)
							{
								string strN = intToChar(int.Parse(strUserAnswers[n])).ToString();
								if (n == 0)
								{
									strUserAnswer += strN;
								}
								else
								{
									strUserAnswer += "," + strN;
								}
							}

							string strContent = "";
							string strTest = "";
							int flag = 0;
							if (!isPictureItem)
							{
								#region 输出文本试题答案
								string[] strAnswer = paperItem.SelectAnswer.Split('|');
								for (int n = 0; n < strAnswer.Length; n++)
								{
									string strN = intToChar(n).ToString();

									if (flag == 0)
									{
										strContent = "";
										strTest = "";
									}

									if (strContent == "")
									{
										strContent = strTest + strN + "." + strAnswer[n];
									}
									else
									{
										strContent = strTest + "    " + strN + "." + strAnswer[n];
									}

									strTest = strContent;

									if (n + 1 < strAnswer.Length)
									{
										strContent = strContent + "    " + intToChar(n + 1) + "." + strAnswer[n + 1];
									}

									if (Pub.GetStringRealLength(strContent) > 100)
									{
										para = myDoc.Content.Paragraphs[num].Range;
										num = num + 1;
										para.Text = "  " + strTest;
										para.Font.Size = 9;
										para.Font.Bold = 0;
										para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
										para.InsertParagraphAfter();

										flag = 0;
									}
									else
									{
										if (n + 1 == strAnswer.Length && strTest != "")
										{
											para = myDoc.Content.Paragraphs[num].Range;
											num = num + 1;
											para.Text = "  " + strTest;
											para.Font.Size = 9;
											para.Font.Bold = 0;
											para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
											para.InsertParagraphAfter();
										}
										flag = flag + 1;
									}
								}
								#endregion
							}
							else
							{
								#region 输出图片试题答案
								IHTMLDocument2 doc = new HTMLDocumentClass();
								doc.write(new object[] { paperItem.SelectAnswer });
								doc.close();

								string[] src = new string[doc.images.length];
								string[] strImage = new string[doc.images.length];
								int t = 0;
								foreach (IHTMLImgElement image in doc.images)
								{
									IHTMLElement element = (IHTMLElement)image;
									strImage[t] = element.outerHTML;
									src[t] = (string)element.getAttribute("src", 2);
									t = t + 1;
								}

								string strItem = paperItem.SelectAnswer;
								for (int x = 0; x < strImage.Length; x++)
								{
									strItem = strItem.Replace(strImage[x], "@");
								}

								string[] strAnswer = strItem.Split('|');
								for (int n = 0; n < strAnswer.Length; n++)
								{
									string strN = intToChar(n).ToString();

									if (flag == 0)
									{
										strContent = "";
										strTest = "";
									}

									if (strContent == "")
									{
										strContent = strTest + strN + "." + strAnswer[n];
									}
									else
									{
										strContent = strTest + "    " + strN + "." + strAnswer[n];
									}

									strTest = strContent;

									if (n + 1 < strAnswer.Length)
									{
										strContent = strContent + "    " + intToChar(n + 1) + "." + strAnswer[n + 1];
									}

									if (Pub.GetStringRealLength(strContent) > 100)
									{
										string[] strText = strTest.Split('@');
										para = myDoc.Content.Paragraphs[num].Range;
										num = num + 1;
										para.Select();
										myWord.Application.Selection.TypeText("  ");
										for (int x = 0; x < strText.Length; x++)
										{
											myWord.Application.Selection.TypeText(ItemBLL.NoHTML(strText[x]));
											if (x < src.Length)
											{
												myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile, ref SaveWithDocument, ref Nothing);
											}
										}
										para.Font.Size = 9;
										para.Font.Bold = 0;
										para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
										para.InsertParagraphAfter();

										flag = 0;
									}
									else
									{
										if (n + 1 == strAnswer.Length && strTest != "")
										{
											string[] strText = strTest.Split('@');
											para = myDoc.Content.Paragraphs[num].Range;
											num = num + 1;
											para.Select();
											myWord.Application.Selection.TypeText("  ");
											for (int x = 0; x < strText.Length; x++)
											{
												myWord.Application.Selection.TypeText(ItemBLL.NoHTML(strText[x]));
												if (x < src.Length)
												{
													myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile, ref SaveWithDocument, ref Nothing);
												}
											}
											para.Font.Size = 9;
											para.Font.Bold = 0;
											para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
											para.InsertParagraphAfter();
										}
										flag = flag + 1;
									}
								}
								#endregion
							}

							// 组织正确答案
							string[] strRightAnswers = paperItem.StandardAnswer.Split('|');
							string strRightAnswer = "";
							for (int n = 0; n < strRightAnswers.Length; n++)
							{
								string strN = intToChar(int.Parse(strRightAnswers[n])).ToString();
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
							if (strRightAnswer == strUserAnswer)
							{
								strScore = String.Format("{0:0.#}", paperSubject.UnitScore);
							}

							para = myDoc.Content.Paragraphs[num].Range;
							num = num + 1;
							para.Text = "★标准答案：" + strRightAnswer + "      考生答案：" + strUserAnswer + "      得分：" + strScore;
							para.Font.Size = 9;
							para.Font.Bold = 0;
							para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
							para.InsertParagraphAfter();

							NowCount = NowCount + 1;

							System.Threading.Thread.Sleep(10);
							jsBlock = "<script>SetPorgressBar('导出试卷','" + ((double)(NowCount * 100) / (double)nItemCount).ToString("0.00") + "'); </script>";
							Response.Write(jsBlock);
							Response.Flush();
						}
					}
				}
				myWord.Application.Selection.EndKey(ref unit, ref extend);
				myWord.Application.Selection.TypeParagraph();
				myWord.Application.Selection.InsertBreak(ref breakType);
				myWord.Application.Selection.TypeBackspace();
				myWord.Application.Selection.Delete(ref character, ref count);
				myWord.Application.Selection.HomeKey(ref unit, ref extend);

				myDoc.SaveAs2000(ref filename, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
								 ref Nothing, ref Nothing, ref Nothing, ref Nothing);
				myWord.Visible = true;

				// 处理完成
				jsBlock = "<script>SetCompleted('处理完成。'); </script>";
				Response.Write(jsBlock);
				Response.Flush();
			}
			catch (Exception ex)
			{
				myDoc.SaveAs2000(ref filename, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
				 ref Nothing, ref Nothing, ref Nothing, ref Nothing);
				throw ex;
			}
			finally
			{
				myDoc.Close(ref Nothing, ref Nothing, ref Nothing);
				myWord.Application.Quit(ref Nothing, ref Nothing, ref Nothing);
				if (myDoc != null)
				{
					Marshal.ReleaseComObject(myDoc);
					myDoc = null;
				}
				if (myWord != null)
				{
					Marshal.ReleaseComObject(myWord);
					myWord = null;
				}
				GC.Collect();
			}
			Response.Write("<script>top.returnValue='"+ strName +"';window.close();</script>");
		}
		#endregion

		#region 导出一批考生试卷
		private void OutputWordAll()
		{
			// 根据 ProgressBar.htm 显示进度条界面
			string templateFileName = Path.Combine(Server.MapPath("."), "ProgressBar.htm");
			StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
			string html = reader.ReadToEnd();
			reader.Close();
			Response.Write(html);
			Response.Flush();
			System.Threading.Thread.Sleep(200);

			string strExamID = Request.QueryString.Get("eid");
			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(strExamID));
			IList<RailExam.Model.RandomExamResult> examResults = null;
			RandomExamResultBLL bllExamResult = new RandomExamResultBLL();
            examResults = bllExamResult.GetRandomExamResults(int.Parse(strExamID), "", "", "", string.Empty, 0,
					 1000, Convert.ToInt32(Request.QueryString.Get("OrgID")));

			string strNowOrgID = ConfigurationManager.AppSettings["StationID"].ToString();
			object filename = null;

			object tableBehavior = Word.WdDefaultTableBehavior.wdWord9TableBehavior;
			object autoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitFixed;

			object unit = Word.WdUnits.wdStory;
			object extend = System.Reflection.Missing.Value;
			object breakType = (int)Word.WdBreakType.wdSectionBreakNextPage;

			object count = 1;
			object character = Word.WdUnits.wdCharacter;

			object Nothing = System.Reflection.Missing.Value;

			object LinkToFile = false;
			object SaveWithDocument = true;


			Word.Application myWord = null;
			Word._Document myDoc = null;
			try
			{
				string jsBlock;
				//myWord = new Word.ApplicationClass();
				//myDoc = new Word.DocumentClass();

				string strFloderPath = Server.MapPath("/RailExamBao/Excel/" + obj.ExamName);
				if (!Directory.Exists(strFloderPath))
				{
					Directory.CreateDirectory(strFloderPath);
				}

				//查询当前考试的大题数
				RandomExamSubjectBLL subjectBLL = new RandomExamSubjectBLL();
				IList<RandomExamSubject> randomExamSubjects = subjectBLL.GetRandomExamSubjectByRandomExamId(Convert.ToInt32(strExamID));
				int nItemCount = 0;
				decimal nTotalScore = 0;
				for (int i = 0; i < randomExamSubjects.Count; i++)
				{
					//算当前考试的总题数
					nItemCount += randomExamSubjects[i].ItemCount;
					//算当前考试的总分数
					nTotalScore += randomExamSubjects[i].ItemCount * randomExamSubjects[i].UnitScore;
				}

				int SumCount = examResults.Count*nItemCount;
				int NowCount = 0;

				for (int emp = 0; emp < examResults.Count; emp++)
				{
					RandomExamResult examResult = examResults[emp];
					string strId = examResult.RandomExamResultIDStation.ToString();
					string orgid = examResult.OrganizationId.ToString();

					if(!PrjPub.IsServerCenter)
					{
						if(orgid.ToString() != ConfigurationManager.AppSettings["StationID"].ToString())
						{
							continue;
						}
					}

					//生成.doc文件完整路径名 
					filename = Server.MapPath("/RailExamBao/Excel/" + obj.ExamName + "/" + examResults[emp].ExamineeName +"(" + examResults[emp].WorkNo+")" + ".doc");
					if (File.Exists(filename.ToString()))
					{
						File.Delete(filename.ToString());
					}

					myWord = new Word.ApplicationClass();
					myDoc = new Word.DocumentClass();

					myDoc = myWord.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
					myDoc.Activate();

					myDoc.PageSetup.LeftMargin = (float)56.8;//左边距2cm
					myDoc.PageSetup.RightMargin = (float)56.8;
					myDoc.PageSetup.TopMargin = (float)56.8;
					myDoc.PageSetup.BottomMargin = (float)56.8;

					Word.Range para;

					para = myDoc.Content.Paragraphs[myDoc.Content.Paragraphs.Count].Range;
					para.Text = "武汉铁路局职工培训考试试卷";
					para.Font.Bold = 1;
					para.Font.Size = 12;
					para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
					para.InsertParagraphAfter();

					# region 考卷信息
					string strOrgName = examResult.OrganizationName;
					string strStationName = "";
					string strOrgName1 = "";
					int m = strOrgName.IndexOf("-");
					if (m != -1)
					{
						strStationName = strOrgName.Substring(0, m);
						strOrgName1 = strOrgName.Substring(m + 1);
					}
					else
					{
						strStationName = strOrgName;
						strOrgName1 = "";
					}

					int RandomExamId = examResult.RandomExamId;
					RandomExamBLL randomExamBLL = new RandomExamBLL();
					RailExam.Model.RandomExam randomExam = randomExamBLL.GetExam(RandomExamId);
					int year = randomExam.BeginTime.Year;
					#endregion

					para = myDoc.Content.Paragraphs[myDoc.Content.Paragraphs.Count].Range;
					para.Text = "考试名称：" + randomExam.ExamName;
					para.Font.Size = 9;
					para.Font.Bold = 0;
					para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
					para.InsertParagraphAfter();

					para = myDoc.Content.Paragraphs[myDoc.Content.Paragraphs.Count].Range;
					para.Text = "总共" + nItemCount + "题，共 " + System.String.Format("{0:0.##}", nTotalScore) +"分";
					para.Font.Size = 9;
					para.Font.Bold = 0;
					para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
					para.InsertParagraphAfter();

					para = myDoc.Content.Paragraphs[myDoc.Content.Paragraphs.Count].Range;
					myDoc.Tables.Add(para, 2, 6, ref tableBehavior, ref autoFitBehavior);
					para.Tables[1].Columns[1].PreferredWidth = 45;
					para.Tables[1].Columns[2].PreferredWidth = 120;
					para.Tables[1].Columns[3].PreferredWidth = 45;
					para.Tables[1].Columns[4].PreferredWidth = 120;
					para.Tables[1].Columns[5].PreferredWidth = 45;
					para.Tables[1].Columns[6].PreferredWidth = 120;
					para.Tables[1].Cell(1, 1).Range.Text = "单位:";
					para.Tables[1].Cell(1, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

					para.Tables[1].Cell(1, 2).Range.Text = strStationName;
					para.Tables[1].Cell(1, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

					para.Tables[1].Cell(1, 3).Range.Text = "车间:";
					para.Tables[1].Cell(1, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

					para.Tables[1].Cell(1, 4).Range.Text = strOrgName1;
					para.Tables[1].Cell(1, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

					para.Tables[1].Cell(1, 5).Range.Text = "职名:";
					para.Tables[1].Cell(1, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

					para.Tables[1].Cell(1, 6).Range.Text = examResult.PostName;
					para.Tables[1].Cell(1, 6).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

					para.Tables[1].Cell(2, 1).Range.Text = "姓名:";
					para.Tables[1].Cell(2, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

					para.Tables[1].Cell(2, 2).Range.Text = examResult.ExamineeName;
					para.Tables[1].Cell(2, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

					para.Tables[1].Cell(2, 3).Range.Text = "时间:";
					para.Tables[1].Cell(2, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

					para.Tables[1].Cell(2, 4).Range.Text = examResult.BeginDateTime.ToString("yyyy-MM-dd HH:mm");
					para.Tables[1].Cell(2, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

					para.Tables[1].Cell(2, 5).Range.Text = "成绩:";
					para.Tables[1].Cell(2, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

					para.Tables[1].Cell(2, 6).Range.Text = System.String.Format("{0:0.##}", examResult.Score);
					para.Tables[1].Cell(2, 6).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

					para.Font.Size = 9;
					para.Font.Bold = 0;
					para.InsertParagraphAfter();

					int randomExamResultId = int.Parse(strId);
					RandomExamItemBLL randomItemBLL = new RandomExamItemBLL();
					RandomExamResultAnswerBLL randomExamResultAnswerBLL = new RandomExamResultAnswerBLL();
					IList<RandomExamResultAnswer> examResultAnswers = new List<RandomExamResultAnswer>();
					if (strNowOrgID != orgid)
					{
						examResultAnswers = randomExamResultAnswerBLL.GetExamResultAnswersByOrgID(int.Parse(strId), int.Parse(orgid));
					}
					else
					{
						examResultAnswers = randomExamResultAnswerBLL.GetExamResultAnswers(int.Parse(strId));
					}

					int num = myDoc.Content.Paragraphs.Count;
					for (int i = 0; i < randomExamSubjects.Count; i++)
					{
						RandomExamSubject paperSubject = randomExamSubjects[i];
						IList<RandomExamItem> PaperItems = new List<RandomExamItem>();

						if (strNowOrgID != orgid)
						{
							PaperItems =
								randomItemBLL.GetItemsByOrgID(paperSubject.RandomExamSubjectId, randomExamResultId, int.Parse(orgid),year);
						}
						else
						{
							PaperItems = randomItemBLL.GetItems(paperSubject.RandomExamSubjectId, randomExamResultId,year);
						}

						para = myDoc.Content.Paragraphs[num].Range;
						num = num + 1;
						para.Text = GetNo(i) + "、" + paperSubject.SubjectName + "   （共" + paperSubject.ItemCount + "题，共" +
									System.String.Format("{0:0.##}", paperSubject.ItemCount * paperSubject.UnitScore) + "分）";
						para.Font.Size = (float)10.5;
						para.Font.Bold = 0;
						para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
						para.InsertParagraphAfter();

						if (PaperItems != null)
						{
							for (int j = 0; j < PaperItems.Count; j++)
							{
								RandomExamItem paperItem = PaperItems[j];
								int k = j + 1;

								bool isPictureItem = paperItem.Content.ToLower().Contains("<img") ||
													 paperItem.SelectAnswer.ToLower().Contains("<img");
								if (!isPictureItem)
								{
									#region 输出文本试题题目
									para = myDoc.Content.Paragraphs[num].Range;
									num = num + 1;
									para.Text = k + ". " + paperItem.Content + "   （" + System.String.Format("{0:0.##}", paperSubject.UnitScore) + "分）";
									para.Font.Size = 9;
									para.Font.Bold = 0;
									para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
									para.InsertParagraphAfter();
									#endregion
								}
								else
								{
									#region 输出图片试题题目
									IHTMLDocument2 doc = new HTMLDocumentClass();
									doc.write(new object[] { paperItem.Content });
									doc.close();

									string[] src = new string[doc.images.length];
									string[] strImage = new string[doc.images.length];
									int t = 0;
									foreach (IHTMLImgElement image in doc.images)
									{
										IHTMLElement element = (IHTMLElement)image;
										strImage[t] = element.outerHTML;
										src[t] = (string)element.getAttribute("src", 2);
										t = t + 1;
									}

									string strItem = paperItem.Content;
									for (int x = 0; x < strImage.Length; x++)
									{
										strItem = strItem.Replace(strImage[x], "@");
									}

									string[] strText = strItem.Split('@');

									para = myDoc.Content.Paragraphs[num].Range;
									num = num + 1;
									para.Select();
									myWord.Application.Selection.TypeText(k + ". ");
									for (int x = 0; x < strText.Length; x++)
									{
										myWord.Application.Selection.TypeText(ItemBLL.NoHTML(strText[x]));
										if (x < src.Length)
										{
											myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile, ref SaveWithDocument, ref Nothing);
										}
									}
									myWord.Application.Selection.TypeText("   （" + System.String.Format("{0:0.##}", paperSubject.UnitScore) + "分）");
									para.Font.Size = 9;
									para.Font.Bold = 0;
									para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
									para.InsertParagraphAfter();
									#endregion
								}

								// 组织用户答案
								RandomExamResultAnswer theExamResultAnswer = null;
								string[] strUserAnswers = new string[0];
								string strUserAnswer = string.Empty;

								foreach (RandomExamResultAnswer resultAnswer in examResultAnswers)
								{
									if (resultAnswer.RandomExamItemId == paperItem.RandomExamItemId)
									{
										theExamResultAnswer = resultAnswer;
										break;
									}
								}

								// 若子表无记录，结束页面输出
								if (theExamResultAnswer == null)
								{
									SessionSet.PageMessage = "数据错误！";
								}

								// 否则组织考生答案
								if (theExamResultAnswer.Answer != null || theExamResultAnswer.Answer == string.Empty)
								{
									strUserAnswers = theExamResultAnswer.Answer.Split('|');
								}
								for (int n = 0; n < strUserAnswers.Length; n++)
								{
									string strN = intToChar(int.Parse(strUserAnswers[n])).ToString();
									if (n == 0)
									{
										strUserAnswer += strN;
									}
									else
									{
										strUserAnswer += "," + strN;
									}
								}

								string strContent = "";
								string strTest = "";
								int flag = 0;
								if (!isPictureItem)
								{
									#region 输出文本试题答案
									string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
									for (int n = 0; n < strAnswer.Length; n++)
									{
										string strN = intToChar(n).ToString();

										if (flag == 0)
										{
											strContent = "";
											strTest = "";
										}

										if (strContent == "")
										{
											strContent = strTest + strN + "." + strAnswer[n];
										}
										else
										{
											strContent = strTest + "    " + strN + "." + strAnswer[n];
										}

										strTest = strContent;

										if (n + 1 < strAnswer.Length)
										{
											strContent = strContent + "    " + intToChar(n + 1) + "." + strAnswer[n + 1];
										}

										if (Pub.GetStringRealLength(strContent) > 100)
										{
											para = myDoc.Content.Paragraphs[num].Range;
											num = num + 1;
											para.Text = "  " + strTest;
											//para.Font.Size = 9;
											//para.Font.Bold = 0;
											para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
											para.InsertParagraphAfter();

											flag = 0;
										}
										else
										{
											if (n + 1 == strAnswer.Length && strTest != "")
											{
												para = myDoc.Content.Paragraphs[num].Range;
												num = num + 1;
												para.Text = "  " + strTest;
												//para.Font.Size = 9;
												//para.Font.Bold = 0;
												para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
												para.InsertParagraphAfter();
											}
											flag = flag + 1;
										}
									}
									#endregion
								}
								else
								{
									#region 输出图片试题答案
									IHTMLDocument2 doc = new HTMLDocumentClass();
									doc.write(new object[] { paperItem.SelectAnswer });
									doc.close();

									string[] src = new string[doc.images.length];
									string[] strImage = new string[doc.images.length];
									int t = 0;
									foreach (IHTMLImgElement image in doc.images)
									{
										IHTMLElement element = (IHTMLElement)image;
										strImage[t] = element.outerHTML;
										src[t] = (string)element.getAttribute("src", 2);
										t = t + 1;
									}

									string strItem = paperItem.SelectAnswer;
									for (int x = 0; x < strImage.Length; x++)
									{
										strItem = strItem.Replace(strImage[x], "@");
									}

									string[] strAnswer = strItem.Split('|');
									for (int n = 0; n < strAnswer.Length; n++)
									{
										string strN = intToChar(n).ToString();

										if (flag == 0)
										{
											strContent = "";
											strTest = "";
										}

										if (strContent == "")
										{
											strContent = strTest + strN + "." + strAnswer[n];
										}
										else
										{
											strContent = strTest + "    " + strN + "." + strAnswer[n];
										}

										strTest = strContent;

										if (n + 1 < strAnswer.Length)
										{
											strContent = strContent + "    " + intToChar(n + 1) + "." + strAnswer[n + 1];
										}

										if (Pub.GetStringRealLength(strContent) > 100)
										{
											string[] strText = strTest.Split('@');
											para = myDoc.Content.Paragraphs[num].Range;
											num = num + 1;
											para.Select();
											myWord.Application.Selection.TypeText("  ");
											for (int x = 0; x < strText.Length; x++)
											{
												myWord.Application.Selection.TypeText(ItemBLL.NoHTML(strText[x]));
												if (x < src.Length)
												{
													myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile, ref SaveWithDocument, ref Nothing);
												}
											}
											//para.Font.Size = 9;
											//para.Font.Bold = 0;
											para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
											para.InsertParagraphAfter();

											flag = 0;
										}
										else
										{
											if (n + 1 == strAnswer.Length && strTest != "")
											{
												string[] strText = strTest.Split('@');
												para = myDoc.Content.Paragraphs[num].Range;
												num = num + 1;
												para.Select();
												myWord.Application.Selection.TypeText("  ");
												for (int x = 0; x < strText.Length; x++)
												{
													myWord.Application.Selection.TypeText(ItemBLL.NoHTML(strText[x]));
													if (x < src.Length)
													{
														myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile, ref SaveWithDocument, ref Nothing);
													}
												}
												//para.Font.Size = 9;
												//para.Font.Bold = 0;
												para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
												para.InsertParagraphAfter();
											}
											flag = flag + 1;
										}
									}
									#endregion
								}

								// 组织正确答案
								string[] strRightAnswers = paperItem.StandardAnswer.Split('|');
								string strRightAnswer = "";
								for (int n = 0; n < strRightAnswers.Length; n++)
								{
									string strN = intToChar(int.Parse(strRightAnswers[n])).ToString();
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
								if (strRightAnswer == strUserAnswer)
								{
									strScore = System.String.Format("{0:0.##}",paperSubject.UnitScore);
								}

								para = myDoc.Content.Paragraphs[num].Range;
								num = num + 1;
								para.Text = "★标准答案：" + strRightAnswer + "      考生答案：" + strUserAnswer + "      得分：" + strScore;
								//para.Font.Size = 9;
								//para.Font.Bold = 0;
								para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
								para.InsertParagraphAfter();

								NowCount = NowCount + 1;

								System.Threading.Thread.Sleep(10);
								jsBlock = "<script>SetPorgressBar('导出试卷','" + ((double) (NowCount * 100) / (double)SumCount).ToString("0.00") + "'); </script>";
								Response.Write(jsBlock);
								Response.Flush();
							}
						}
					}
					myWord.Application.Selection.EndKey(ref unit, ref extend);
					myWord.Application.Selection.TypeParagraph();
					myWord.Application.Selection.InsertBreak(ref breakType);
					myWord.Application.Selection.TypeBackspace();
					myWord.Application.Selection.Delete(ref character, ref count);
					myWord.Application.Selection.HomeKey(ref unit, ref extend);
					myDoc.SaveAs2000(ref filename, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
									 ref Nothing, ref Nothing, ref Nothing, ref Nothing);
					myWord.Visible = true;
					if(emp < examResults.Count -1)
					{
						myDoc.Close(ref Nothing, ref Nothing, ref Nothing);
						myWord.Application.Quit(ref Nothing, ref Nothing, ref Nothing);
					}
				}


				// 处理完成
				jsBlock = "<script>SetCompleted('处理完成。'); </script>";
				Response.Write(jsBlock);
				Response.Flush();
			}
			catch (Exception ex)
			{
				myDoc.SaveAs2000(ref filename, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
				 ref Nothing, ref Nothing, ref Nothing, ref Nothing);
				throw ex;
			}
			finally
			{
				myDoc.Close(ref Nothing, ref Nothing, ref Nothing);
				myWord.Application.Quit(ref Nothing, ref Nothing, ref Nothing);
			}
			Response.Write("<script>top.returnValue='true';window.close();</script>");
		}
		#endregion

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

        private char intToChar(int intCol)
        {
			return Convert.ToChar('A' + intCol);
        }
	}
}
