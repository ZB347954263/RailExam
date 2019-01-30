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

		#region ����һ�������Ծ�
		private void OutputWord()
		{
			// ���� ProgressBar.htm ��ʾ����������
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
				//����.doc�ļ�����·���� 
				filename = Server.MapPath("/RailExamBao/Excel/Word.doc");

				if (File.Exists(filename.ToString()))
				{
					File.Delete(filename.ToString());
				}
				
				//����һ��word�ļ����ļ�����ϵͳʱ�����ɾ�ȷ������ 
				myDoc = myWord.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
				myDoc.Activate();

				myDoc.PageSetup.LeftMargin = (float)56.8;//��߾�2cm
				myDoc.PageSetup.RightMargin = (float)56.8;
				myDoc.PageSetup.TopMargin = (float)56.8;
				myDoc.PageSetup.BottomMargin = (float)56.8;

				Word.Range para = myDoc.Content.Paragraphs[1].Range;
				para.Text = "�人��·��ְ����ѵ�����Ծ�";
				para.Font.Bold = 1;
				para.Font.Size = 12;
				para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
				para.InsertParagraphAfter();

				# region ������Ϣ
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
				para.Text = "�������ƣ�" + randomExam.ExamName;
				para.Font.Size = 9;
				para.Font.Bold = 0;
				para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
				para.InsertParagraphAfter();

				para = myDoc.Content.Paragraphs[3].Range;
				para.Text = "�ܹ�" + nItemCount + "�⣬�� " + String.Format("{0:0.#}", nTotalScore) + "��";
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
				para.Tables[1].Cell(1, 1).Range.Text = "��λ:";
				para.Tables[1].Cell(1, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

				para.Tables[1].Cell(1, 2).Range.Text = strStationName;
				para.Tables[1].Cell(1, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

				para.Tables[1].Cell(1, 3).Range.Text = "����:";
				para.Tables[1].Cell(1, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

				para.Tables[1].Cell(1, 4).Range.Text = strOrgName1;
				para.Tables[1].Cell(1, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

				para.Tables[1].Cell(1, 5).Range.Text = "ְ��:";
				para.Tables[1].Cell(1, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

				para.Tables[1].Cell(1, 6).Range.Text = randomExamResult.PostName;
				para.Tables[1].Cell(1, 6).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

				para.Tables[1].Cell(2, 1).Range.Text = "����:";
				para.Tables[1].Cell(2, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

				strName = randomExamResult.ExamineeName;
				para.Tables[1].Cell(2, 2).Range.Text = randomExamResult.ExamineeName;
				para.Tables[1].Cell(2, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

				para.Tables[1].Cell(2, 3).Range.Text = "ʱ��:";
				para.Tables[1].Cell(2, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

				para.Tables[1].Cell(2, 4).Range.Text = randomExamResult.BeginDateTime.ToString("yyyy-MM-dd HH:mm");
				para.Tables[1].Cell(2, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

				para.Tables[1].Cell(2, 5).Range.Text = "�ɼ�:";
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
					para.Text = GetNo(i) + "��" + paperSubject.SubjectName + "   ����" + paperSubject.ItemCount + "�⣬��" +
								String.Format("{0:0.#}", paperSubject.ItemCount * paperSubject.UnitScore) + "�֣�";
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
								#region ����ı�������Ŀ
								para = myDoc.Content.Paragraphs[num].Range;
								num = num + 1;
								para.Text = k + ". " + paperItem.Content + "   ��" + String.Format("{0:0.#}", paperSubject.UnitScore) + "�֣�";
								para.Font.Size = 9;
								para.Font.Bold = 0;
								para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
								para.InsertParagraphAfter();
								#endregion
							}
							else
							{
								#region ���ͼƬ������Ŀ
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
								myWord.Application.Selection.TypeText("   ��" + String.Format("{0:0.#}", paperSubject.UnitScore) + "�֣�");
								para.Font.Size = 9;
								para.Font.Bold = 0;
								para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
								para.InsertParagraphAfter();
								#endregion
							}

							// ��֯�û���
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

							// ���ӱ��޼�¼������ҳ�����
							if (theExamResultAnswer == null)
							{
								SessionSet.PageMessage = "���ݴ���";
							}

							// ������֯������
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
								#region ����ı������
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
								#region ���ͼƬ�����
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

							// ��֯��ȷ��
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
							para.Text = "���׼�𰸣�" + strRightAnswer + "      �����𰸣�" + strUserAnswer + "      �÷֣�" + strScore;
							para.Font.Size = 9;
							para.Font.Bold = 0;
							para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
							para.InsertParagraphAfter();

							NowCount = NowCount + 1;

							System.Threading.Thread.Sleep(10);
							jsBlock = "<script>SetPorgressBar('�����Ծ�','" + ((double)(NowCount * 100) / (double)nItemCount).ToString("0.00") + "'); </script>";
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

				// �������
				jsBlock = "<script>SetCompleted('������ɡ�'); </script>";
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

		#region ����һ�������Ծ�
		private void OutputWordAll()
		{
			// ���� ProgressBar.htm ��ʾ����������
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

				//��ѯ��ǰ���ԵĴ�����
				RandomExamSubjectBLL subjectBLL = new RandomExamSubjectBLL();
				IList<RandomExamSubject> randomExamSubjects = subjectBLL.GetRandomExamSubjectByRandomExamId(Convert.ToInt32(strExamID));
				int nItemCount = 0;
				decimal nTotalScore = 0;
				for (int i = 0; i < randomExamSubjects.Count; i++)
				{
					//�㵱ǰ���Ե�������
					nItemCount += randomExamSubjects[i].ItemCount;
					//�㵱ǰ���Ե��ܷ���
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

					//����.doc�ļ�����·���� 
					filename = Server.MapPath("/RailExamBao/Excel/" + obj.ExamName + "/" + examResults[emp].ExamineeName +"(" + examResults[emp].WorkNo+")" + ".doc");
					if (File.Exists(filename.ToString()))
					{
						File.Delete(filename.ToString());
					}

					myWord = new Word.ApplicationClass();
					myDoc = new Word.DocumentClass();

					myDoc = myWord.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
					myDoc.Activate();

					myDoc.PageSetup.LeftMargin = (float)56.8;//��߾�2cm
					myDoc.PageSetup.RightMargin = (float)56.8;
					myDoc.PageSetup.TopMargin = (float)56.8;
					myDoc.PageSetup.BottomMargin = (float)56.8;

					Word.Range para;

					para = myDoc.Content.Paragraphs[myDoc.Content.Paragraphs.Count].Range;
					para.Text = "�人��·��ְ����ѵ�����Ծ�";
					para.Font.Bold = 1;
					para.Font.Size = 12;
					para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
					para.InsertParagraphAfter();

					# region ������Ϣ
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
					para.Text = "�������ƣ�" + randomExam.ExamName;
					para.Font.Size = 9;
					para.Font.Bold = 0;
					para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
					para.InsertParagraphAfter();

					para = myDoc.Content.Paragraphs[myDoc.Content.Paragraphs.Count].Range;
					para.Text = "�ܹ�" + nItemCount + "�⣬�� " + System.String.Format("{0:0.##}", nTotalScore) +"��";
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
					para.Tables[1].Cell(1, 1).Range.Text = "��λ:";
					para.Tables[1].Cell(1, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

					para.Tables[1].Cell(1, 2).Range.Text = strStationName;
					para.Tables[1].Cell(1, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

					para.Tables[1].Cell(1, 3).Range.Text = "����:";
					para.Tables[1].Cell(1, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

					para.Tables[1].Cell(1, 4).Range.Text = strOrgName1;
					para.Tables[1].Cell(1, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

					para.Tables[1].Cell(1, 5).Range.Text = "ְ��:";
					para.Tables[1].Cell(1, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

					para.Tables[1].Cell(1, 6).Range.Text = examResult.PostName;
					para.Tables[1].Cell(1, 6).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

					para.Tables[1].Cell(2, 1).Range.Text = "����:";
					para.Tables[1].Cell(2, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

					para.Tables[1].Cell(2, 2).Range.Text = examResult.ExamineeName;
					para.Tables[1].Cell(2, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

					para.Tables[1].Cell(2, 3).Range.Text = "ʱ��:";
					para.Tables[1].Cell(2, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

					para.Tables[1].Cell(2, 4).Range.Text = examResult.BeginDateTime.ToString("yyyy-MM-dd HH:mm");
					para.Tables[1].Cell(2, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

					para.Tables[1].Cell(2, 5).Range.Text = "�ɼ�:";
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
						para.Text = GetNo(i) + "��" + paperSubject.SubjectName + "   ����" + paperSubject.ItemCount + "�⣬��" +
									System.String.Format("{0:0.##}", paperSubject.ItemCount * paperSubject.UnitScore) + "�֣�";
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
									#region ����ı�������Ŀ
									para = myDoc.Content.Paragraphs[num].Range;
									num = num + 1;
									para.Text = k + ". " + paperItem.Content + "   ��" + System.String.Format("{0:0.##}", paperSubject.UnitScore) + "�֣�";
									para.Font.Size = 9;
									para.Font.Bold = 0;
									para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
									para.InsertParagraphAfter();
									#endregion
								}
								else
								{
									#region ���ͼƬ������Ŀ
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
									myWord.Application.Selection.TypeText("   ��" + System.String.Format("{0:0.##}", paperSubject.UnitScore) + "�֣�");
									para.Font.Size = 9;
									para.Font.Bold = 0;
									para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
									para.InsertParagraphAfter();
									#endregion
								}

								// ��֯�û���
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

								// ���ӱ��޼�¼������ҳ�����
								if (theExamResultAnswer == null)
								{
									SessionSet.PageMessage = "���ݴ���";
								}

								// ������֯������
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
									#region ����ı������
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
									#region ���ͼƬ�����
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

								// ��֯��ȷ��
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
								para.Text = "���׼�𰸣�" + strRightAnswer + "      �����𰸣�" + strUserAnswer + "      �÷֣�" + strScore;
								//para.Font.Size = 9;
								//para.Font.Bold = 0;
								para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
								para.InsertParagraphAfter();

								NowCount = NowCount + 1;

								System.Threading.Thread.Sleep(10);
								jsBlock = "<script>SetPorgressBar('�����Ծ�','" + ((double) (NowCount * 100) / (double)SumCount).ToString("0.00") + "'); </script>";
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


				// �������
				jsBlock = "<script>SetCompleted('������ɡ�'); </script>";
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
                case "0": strReturn = "һ";
                    break;
                case "1": strReturn = "��";
                    break;
                case "2": strReturn = "��";
                    break;
                case "3": strReturn = "��";
                    break;
                case "4": strReturn = "��";
                    break;
                case "5": strReturn = "��";
                    break;
                case "6": strReturn = "��";
                    break;
                case "7": strReturn = "��";
                    break;
                case "8": strReturn = "��";
                    break;
                case "9": strReturn = "ʮ";
                    break;
                case "10": strReturn = "ʮһ";
                    break;
                case "11": strReturn = "ʮ��";
                    break;
                case "12": strReturn = "ʮ��";
                    break;
                case "13": strReturn = "ʮ��";
                    break;
                case "14": strReturn = "ʮ��";
                    break;
                case "15": strReturn = "ʮ��";
                    break;
                case "16": strReturn = "ʮ��";
                    break;
                case "17": strReturn = "ʮ��";
                    break;
                case "18": strReturn = "ʮ��";
                    break;
                case "19": strReturn = "��ʮ";
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
