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
	public partial class OutputPaperAllNew : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				ViewState["NowOrgID"] = Convert.ToInt32(ConfigurationManager.AppSettings["StationID"]);

				if (Request.QueryString.Get("Mode") == "one")
				{
                    OutputWord();
				}
				else if (Request.QueryString.Get("Mode") == "All")
				{
					OutputWordAll();
				}
				else if (Request.QueryString.Get("Mode") == "blank")
				{
					OutputWordBlank();
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
				para.Text = PrjPub.GetRailName()+"��·��ְ����ѵ�����Ծ�";
				para.Font.Bold = 1;
				para.Font.Size = 12;
				para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
				para.InsertParagraphAfter();

				# region ������Ϣ
				RandomExamResultBLL randomExamResultBLL = new RandomExamResultBLL();
				RandomExamResult randomExamResult = new RandomExamResult();
				randomExamResult = randomExamResultBLL.GetRandomExamResultStation(int.Parse(strId));

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

				OrganizationBLL objOrgBll = new OrganizationBLL();
				para = myDoc.Content.Paragraphs[3].Range;
				para.Text = "������λ��" + objOrgBll.GetOrganization(randomExam.OrgId).ShortName;
				para.Font.Size = 9;
				para.Font.Bold = 0;
				para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
				para.InsertParagraphAfter();

				para = myDoc.Content.Paragraphs[4].Range;
				para.Text = "�ܹ�" + nItemCount + "�⣬�� " + String.Format("{0:0.#}", nTotalScore) + "��";
				para.Font.Size = 9;
				para.Font.Bold = 0;
				para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
				para.InsertParagraphAfter();

				para = myDoc.Content.Paragraphs[5].Range;
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

				strName = randomExamResult.ExamineeName + "��" + strStationName + "��";
				if (!PrjPub.IsWuhan())
				{
					para.Tables[1].Cell(2, 2).Range.Text = randomExamResult.ExamineeName + "(" + randomExamResult.WorkNo + ")";
					para.Tables[1].Cell(2, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
				}
				else
				{
					para.Tables[1].Cell(2, 2).Range.Text = randomExamResult.ExamineeName;
					para.Tables[1].Cell(2, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
				}

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

				if (ViewState["NowOrgID"].ToString() != orgid)
				{
					examResultAnswers = randomExamResultAnswerBLL.GetExamResultAnswersStation(int.Parse(strId));
				}
				else
				{
					examResultAnswers = randomExamResultAnswerBLL.GetExamResultAnswers(int.Parse(strId));
				}

			    nItemCount = examResultAnswers.Count;

				int num = 19;
				int NowCount = 0;
				for (int i = 0; i < randomExamSubjects.Count; i++)
				{
					RandomExamSubject paperSubject = randomExamSubjects[i];

					IList<RandomExamItem> paperSubjectItems;
					if (ViewState["NowOrgID"].ToString() != orgid)
					{
						paperSubjectItems = randomItemBLL.GetItemsStation(paperSubject.RandomExamSubjectId, randomExamResultId, year);
					}
					else
					{
						paperSubjectItems = randomItemBLL.GetItems(paperSubject.RandomExamSubjectId, randomExamResultId, year);
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
					    int z = 1;
					    int y = 1;
						for (int j = 0; j < paperSubjectItems.Count; j++)
						{
							RandomExamItem paperItem = paperSubjectItems[j];
							int k = j + 1;

                            if(string.IsNullOrEmpty(paperItem.SelectAnswer))
                            {
                                paperItem.SelectAnswer = string.Empty;
                            }

                            if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                            {
                                z = 1;
                                k = y;
                                y++;
                            }
                            else if(paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                            {
                                k = z;
                                z++;
                            }

						    bool isPictureItem = paperItem.Content.ToLower().Contains("<img") ||
												 paperItem.SelectAnswer.ToLower().Contains("<img");
							if (!isPictureItem)
							{
								#region ����ı�������Ŀ
								para = myDoc.Content.Paragraphs[num].Range;
								num = num + 1;
                                if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                                {
                                    para.Text = "(" + k + "). " + paperItem.Content + "   ��" + String.Format("{0:0.#}", paperItem.Score) + "�֣�";
                                }
                                else
                                {
                                    para.Text = k + ". " + paperItem.Content.Replace("\n","") + "   ��" + String.Format("{0:0.#}", paperSubject.UnitScore) + "�֣�";
                                }
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

                            if(paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                            {
                                NowCount = NowCount + 1;
                                System.Threading.Thread.Sleep(10);
                                jsBlock = "<script>SetPorgressBar('�����Ծ�','" + ((double)(NowCount * 100) / (double)nItemCount).ToString("0.00") + "'); </script>";
                                Response.Write(jsBlock);
                                Response.Flush();
                                continue;
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
			Response.Write("<script>top.returnValue='" + strName + "';window.close();</script>");
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
			string strType = Request.QueryString.Get("Type");

			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(strExamID));
			IList<RailExam.Model.RandomExamResult> examResults = null;
			RandomExamResultBLL bllExamResult = new RandomExamResultBLL();

			if(strType == "Pass")
			{
                examResults = bllExamResult.GetRandomExamResults(int.Parse(strExamID), "", "", "", string.Empty, obj.PassScore,
						 1000, Convert.ToInt32(Request.QueryString.Get("OrgID")));
			}
			else
			{
                examResults = bllExamResult.GetRandomExamResults(int.Parse(strExamID), "", "", "", string.Empty, 0,
						 1000, Convert.ToInt32(Request.QueryString.Get("OrgID")));
			}

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

				string strFloderPath = "";
				if (strType == "Pass")
				{
					strFloderPath = Server.MapPath("/RailExamBao/Excel/" + obj.ExamName+"�ϸ��Ծ�");
				}
				else
				{
					strFloderPath = Server.MapPath("/RailExamBao/Excel/" + obj.ExamName);
				}
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

				int SumCount = examResults.Count * nItemCount;
				int NowCount = 0;

				for (int emp = 0; emp < examResults.Count; emp++)
				{
					RandomExamResult examResult = examResults[emp];
					string strId = examResult.RandomExamResultId.ToString();
					string orgid = examResult.OrganizationId.ToString();

					if (!PrjPub.IsServerCenter)
					{
						if (orgid.ToString() != ConfigurationManager.AppSettings["StationID"].ToString())
						{
							continue;
						}
					}

					//����.doc�ļ�����·���� 
					EmployeeBLL objEmployeeBll = new EmployeeBLL();
					Employee objEmployee = objEmployeeBll.GetEmployee(examResults[emp].ExamineeId);
					OrganizationBLL objOrgBll = new OrganizationBLL();
					Organization objOrg = objOrgBll.GetOrganization(objOrgBll.GetStationOrgID(objEmployee.OrgID));
					if (strType == "Pass")
					{
						filename = Server.MapPath("/RailExamBao/Excel/" + obj.ExamName + "�ϸ��Ծ�/" + examResults[emp].ExamineeName + "(" + objOrg.ShortName + ")" + ".doc");
					}
					else
					{
						filename = Server.MapPath("/RailExamBao/Excel/" + obj.ExamName + "/" + examResults[emp].ExamineeName + "(" + objOrg.ShortName + ")" + ".doc");
					}
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
					para.Text = PrjPub.GetRailName()+"��·��ְ����ѵ�����Ծ�";
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
					para.Text = "������λ��" + objOrgBll.GetOrganization(randomExam.OrgId).ShortName;
					para.Font.Size = 9;
					para.Font.Bold = 0;
					para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
					para.InsertParagraphAfter();

					para = myDoc.Content.Paragraphs[myDoc.Content.Paragraphs.Count].Range;
					para.Text = "�ܹ�" + nItemCount + "�⣬�� " + System.String.Format("{0:0.##}", nTotalScore) + "��";
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

					if (!PrjPub.IsWuhan())
					{
						para.Tables[1].Cell(2, 2).Range.Text = examResult.ExamineeName + "(" + examResult.WorkNo + ")";
						para.Tables[1].Cell(2, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
					}
					else
					{
						para.Tables[1].Cell(2, 2).Range.Text = examResult.ExamineeName;
						para.Tables[1].Cell(2, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
					}

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
						examResultAnswers = randomExamResultAnswerBLL.GetExamResultAnswersStation(int.Parse(strId));
					}
					else
					{
						examResultAnswers = randomExamResultAnswerBLL.GetExamResultAnswers(int.Parse(strId));
					}

                    nItemCount = examResultAnswers.Count;

					int num = myDoc.Content.Paragraphs.Count;
					for (int i = 0; i < randomExamSubjects.Count; i++)
					{
						RandomExamSubject paperSubject = randomExamSubjects[i];
						IList<RandomExamItem> PaperItems = new List<RandomExamItem>();

						if (strNowOrgID != orgid)
						{
							PaperItems =
								randomItemBLL.GetItemsStation(paperSubject.RandomExamSubjectId, randomExamResultId,  year);
						}
						else
						{
							PaperItems = randomItemBLL.GetItems(paperSubject.RandomExamSubjectId, randomExamResultId, year);
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
                            int z = 1;
                            int y = 1;
							for (int j = 0; j < PaperItems.Count; j++)
							{
								RandomExamItem paperItem = PaperItems[j];
								int k = j + 1;

                                if (string.IsNullOrEmpty(paperItem.SelectAnswer))
                                {
                                    paperItem.SelectAnswer = string.Empty;
                                }

                                if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                                {
                                    z = 1;
                                    k = y;
                                    y++;
                                }
                                else if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                                {
                                    k = z;
                                    z++;
                                }

								bool isPictureItem = paperItem.Content.ToLower().Contains("<img") ||
													 paperItem.SelectAnswer.ToLower().Contains("<img");
								if (!isPictureItem)
								{
									#region ����ı�������Ŀ
									para = myDoc.Content.Paragraphs[num].Range;
									num = num + 1;
                                    if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                                    {
                                        para.Text = "(" + k + "). " + paperItem.Content + "   ��" + String.Format("{0:0.#}", paperItem.Score) + "�֣�";

                                    }
                                    else
                                    {
                                        para.Text = k + ". " + paperItem.Content.Replace("\n", "") + "   ��" + String.Format("{0:0.#}", paperSubject.UnitScore) + "�֣�";
                                    } 
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

                                if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                                {
                                    NowCount = NowCount + 1;
                                    System.Threading.Thread.Sleep(10);
                                    jsBlock = "<script>SetPorgressBar('�����Ծ�','" + ((double)(NowCount * 100) / (double)nItemCount).ToString("0.00") + "'); </script>";
                                    Response.Write(jsBlock);
                                    Response.Flush();
                                    continue;
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
									strScore = System.String.Format("{0:0.##}", paperSubject.UnitScore);
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
								jsBlock = "<script>SetPorgressBar('�����Ծ�','" + ((double)(NowCount * 100) / (double)SumCount).ToString("0.00") + "'); </script>";
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
					if (emp < examResults.Count - 1)
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

		#region �����հ��Ծ�
		private void OutputWordBlank()
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

            int RandomExamId = int.Parse(strId);
            RandomExamBLL randomExamBLL = new RandomExamBLL();
            RailExam.Model.RandomExam randomExam = randomExamBLL.GetExam(RandomExamId);

            OracleAccess db = new OracleAccess();
            string strSql = "select * from Random_Exam_Item_" + randomExam.BeginTime.Year + " where Random_Exam_ID=" + RandomExamId;
            if (db.RunSqlDataSet(strSql).Tables[0].Rows.Count == 0)
            {
                Response.Write("<script>alert('δ����������Բ��裬���ܵ����Ծ�');top.returnValue=''; window.close();</script>");
                return;
            }


			string strName = "";
			object filename = null;
			object filename1 = null;

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
			_Document myDoc1 = null;
			try
			{
				string jsBlock;
				myWord = new ApplicationClass();
				myDoc = new DocumentClass();

				string strFloderPath = Server.MapPath("/RailExamBao/Excel/" + randomExam.ExamName + "�Ծ�");
				if (!Directory.Exists(strFloderPath))
				{
					Directory.CreateDirectory(strFloderPath);
				}

				//����.doc�ļ�����·���� 
				filename = Server.MapPath("/RailExamBao/Excel/" + randomExam.ExamName + "�Ծ�/�հ��Ծ�.doc");
				if (File.Exists(filename.ToString()))
				{
					File.Delete(filename.ToString());
				}

				//����һ��word�ļ����ļ�����ϵͳʱ�����ɾ�ȷ������ 
				myDoc = myWord.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
				myDoc.Activate();
				myDoc.ActiveWindow.View.Type = WdViewType.wdPrintView;
				myDoc.ActiveWindow.View.SeekView = WdSeekView.wdSeekCurrentPageHeader;

                //myDoc.PageSetup.LeftMargin = (float)56.8;//��߾�2cm
                //myDoc.PageSetup.RightMargin = (float)56.8;
                //myDoc.PageSetup.TopMargin = (float)56.8;
                //myDoc.PageSetup.BottomMargin = (float)56.8;
                myDoc.PageSetup.PageWidth = (float)596.4; //21
                myDoc.PageSetup.PageHeight = (float)843.48; //29.7;
                //myDoc.PageSetup.PageWidth = (float) 843.48; //29.7
                //myDoc.PageSetup.PageHeight = (float) 1192.8; //42;

				filename1 = Server.MapPath("/RailExamBao/Excel/" + randomExam.ExamName + "�Ծ�/�����Ծ�.doc");
				if (File.Exists(filename1.ToString()))
				{
					File.Delete(filename1.ToString());
				}
				
				myDoc1 = new DocumentClass();
				//����һ��word�ļ����ļ�����ϵͳʱ�����ɾ�ȷ������ 
				myDoc1 = myWord.Documents.Add(ref Nothing, ref Nothing, ref Nothing, ref Nothing);
				myDoc1.Activate();

                myDoc1.PageSetup.LeftMargin = (float)56.8;//��߾�2cm
                myDoc1.PageSetup.RightMargin = (float)56.8;
                myDoc1.PageSetup.TopMargin = (float)56.8;
                myDoc1.PageSetup.BottomMargin = (float)56.8;
                //myDoc1.PageSetup.PageWidth = (float)843.48; //29.7
                //myDoc1.PageSetup.PageHeight = (float)1192.8; //42;

				myDoc.ActiveWindow.ActivePane.Selection.Font.Size = (float)13.5; ;
				myDoc.ActiveWindow.ActivePane.Selection.Font.Bold = 1;
				myDoc.ActiveWindow.ActivePane.Selection.TypeText(PrjPub.GetRailName()+"����"+randomExam.ExamName+"�Ծ�");
				myDoc.ActiveWindow.ActivePane.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;


				Word.Range para = null;
				//Word.Range para = myDoc.Content.Paragraphs[1].Range;
				//para.Text = PrjPub.GetRailName()+"��·��ְ����ѵ�����Ծ�";
				//para.Font.Bold = 1;
				//para.Font.Size = (float)13.5;
				//para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
				//para.InsertParagraphAfter();

				Word.Range para1 = myDoc1.Content.Paragraphs[1].Range;
				para1.Text = PrjPub.GetRailName()+"����"+randomExam.ExamName+"�Ծ�";
				para1.Font.Bold = 1;
				para1.Font.Size = (float)13.5;
				para1.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
				para1.InsertParagraphAfter();

				# region ������Ϣ
				int year = randomExam.BeginTime.Year;

				RandomExamSubjectBLL randomExamSubjectBLL = new RandomExamSubjectBLL();
				IList<RailExam.Model.RandomExamSubject> randomExamSubjects = randomExamSubjectBLL.GetRandomExamSubjectByRandomExamId(RandomExamId);

				int nItemCount = 0;
				decimal nTotalScore = 0;
				for (int i = 0; i < randomExamSubjects.Count; i++)
				{
					nItemCount += randomExamSubjects[i].ItemCount;
					nTotalScore += randomExamSubjects[i].ItemCount * randomExamSubjects[i].UnitScore;
				}
				#endregion

				strName = randomExam.ExamName;
				OrganizationBLL objOrgBll = new OrganizationBLL();
                //myDoc.ActiveWindow.ActivePane.Selection.TypeParagraph();
                //myDoc.ActiveWindow.ActivePane.Selection.Font.Size = (float)10.5; ;
                //myDoc.ActiveWindow.ActivePane.Selection.Font.Bold = 0;
                //myDoc.ActiveWindow.ActivePane.Selection.TypeText("�ƾ�λ��" + objOrgBll.GetOrganization(randomExam.OrgId).ShortName);
                //myDoc.ActiveWindow.ActivePane.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;


                //para1 = myDoc1.Content.Paragraphs[2].Range;
                //para1.Text = "�ƾ�λ��" + objOrgBll.GetOrganization(randomExam.OrgId).ShortName;
                //para1.Font.Size = (float)10.5;
                //para1.Font.Bold = 0;
                //para1.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                //para1.InsertParagraphAfter();


                //myDoc.ActiveWindow.ActivePane.Selection.TypeParagraph();
                //myDoc.ActiveWindow.ActivePane.Selection.Font.Size = (float)10.5; ;
                //myDoc.ActiveWindow.ActivePane.Selection.Font.Bold = 0;
                //myDoc.ActiveWindow.ActivePane.Selection.TypeText("Ա����ţ�ѧ�ţ���__________________                                                                   �ܹ�" +
                //            nItemCount + "�⣬�� " + String.Format("{0:0.#}", nTotalScore) + "��");
                //myDoc.ActiveWindow.ActivePane.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;


                //para1 = myDoc1.Content.Paragraphs[3].Range;
                //para1.Text = "Ա����ţ�ѧ�ţ���__________________                                                                   �ܹ�" +
                //             nItemCount + "�⣬�� " + String.Format("{0:0.#}", nTotalScore) + "��";
                //para1.Font.Size = (float)10.5;
                //para1.Font.Bold = 0;
                //para1.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                //para1.InsertParagraphAfter();

				myDoc.ActiveWindow.ActivePane.Selection.TypeParagraph();
				myDoc.ActiveWindow.ActivePane.Selection.Font.Bold = 0;
				myDoc.ActiveWindow.ActivePane.Selection.Font.Size = (float)10.5;
				myDoc.ActiveWindow.ActivePane.Selection.Tables.Add(myDoc.ActiveWindow.ActivePane.Selection.Range, 2, 6, ref tableBehavior, ref autoFitBehavior);
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Columns[1].PreferredWidth = 45;
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Columns[2].PreferredWidth = 180;
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Columns[3].PreferredWidth = 45;
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Columns[4].PreferredWidth = 180;
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Columns[5].PreferredWidth = 45;
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Columns[6].PreferredWidth = 180;
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(1, 1).Range.Text = "��λ:";
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(1, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(1, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(1, 2).Range.Text = "";
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(1, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(1, 2).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;


				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(1, 3).Range.Text = "����:";
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(1, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(1, 3).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;


				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(1, 4).Range.Text = "";
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(1, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(1, 4).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;


				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(1, 5).Range.Text = "ְ��:";
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(1, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(1, 5).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;


				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(1, 6).Range.Text = "";
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(1, 6).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(1, 6).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;


				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(2, 1).Range.Text = "����:";
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(2, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(2, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;


				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(2, 2).Range.Text = "";
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(2, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(2, 2).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;


				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(2, 3).Range.Text = "����:";
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(2, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(2, 3).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;


				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(2, 4).Range.Text = "";
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(2, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(2, 4).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;


				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(2, 5).Range.Text = "�ɼ�:";
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(2, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(2, 5).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;


				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(2, 6).Range.Text = "";
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(2, 6).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
				myDoc.ActiveWindow.ActivePane.Selection.Tables[1].Cell(2, 6).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;


				myDoc.ActiveWindow.ActivePane.Selection.ParagraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceExactly;
				myDoc.ActiveWindow.ActivePane.Selection.ParagraphFormat.LineSpacing = 20;
				myDoc.ActiveWindow.ActivePane.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

				para1 = myDoc1.Content.Paragraphs[2].Range;
				myDoc1.Tables.Add(para1, 2, 6, ref tableBehavior, ref autoFitBehavior);
				para1.Tables[1].Columns[1].PreferredWidth = 45;
				para1.Tables[1].Columns[2].PreferredWidth = 180;
				para1.Tables[1].Columns[3].PreferredWidth = 45;
				para1.Tables[1].Columns[4].PreferredWidth = 180;
				para1.Tables[1].Columns[5].PreferredWidth = 45;
				para1.Tables[1].Columns[6].PreferredWidth = 180;
				para1.Tables[1].Cell(1, 1).Range.Text = "��λ:";
				para1.Tables[1].Cell(1, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

				para1.Tables[1].Cell(1, 2).Range.Text = "";
				para1.Tables[1].Cell(1, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

				para1.Tables[1].Cell(1, 3).Range.Text = "����:";
				para1.Tables[1].Cell(1, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

				para1.Tables[1].Cell(1, 4).Range.Text = "";
				para1.Tables[1].Cell(1, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

				para1.Tables[1].Cell(1, 5).Range.Text = "ְ��:";
				para1.Tables[1].Cell(1, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

				para1.Tables[1].Cell(1, 6).Range.Text = "";
				para1.Tables[1].Cell(1, 6).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

				para1.Tables[1].Cell(2, 1).Range.Text = "����:";
				para1.Tables[1].Cell(2, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

				para1.Tables[1].Cell(2, 2).Range.Text = "";
				para1.Tables[1].Cell(2, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

				para1.Tables[1].Cell(2, 3).Range.Text = "����:";
				para1.Tables[1].Cell(2, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

				para1.Tables[1].Cell(2, 4).Range.Text = "";
				para1.Tables[1].Cell(2, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

				para1.Tables[1].Cell(2, 5).Range.Text = "�ɼ�:";
				para1.Tables[1].Cell(2, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

				para1.Tables[1].Cell(2, 6).Range.Text = "";
				para1.Tables[1].Cell(2, 6).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

				para1.Font.Size = (float)10.5;
				//para1.ParagraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceExactly;
				//para1.ParagraphFormat.LineSpacing = 20;
				para1.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
				para1.Font.Bold = 0;
				para1.InsertParagraphAfter();

				object nowUnit = WdUnits.wdLine;
				object nowCount = 2;
				myDoc.ActiveWindow.ActivePane.Selection.MoveDown(ref nowUnit, ref nowCount, ref extend);
				myDoc.ActiveWindow.ActivePane.Selection.Font.Size = (float)10.5; ;
				myDoc.ActiveWindow.ActivePane.Selection.Font.Bold = 0;
                myDoc.ActiveWindow.ActivePane.Selection.TypeText("===========================��=====��====��===========================");
				//myDoc.ActiveWindow.ActivePane.Selection.TypeText("==================================================��=====��====��===========================================");
                myDoc.ActiveWindow.ActivePane.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

			    myDoc.ActiveWindow.View.SeekView = WdSeekView.wdSeekCurrentPageFooter;
                myDoc.ActiveWindow.ActivePane.Selection.Font.Size = (float)10.5; ;
                myDoc.ActiveWindow.ActivePane.Selection.Font.Bold = 0;
                //myDoc.ActiveWindow.ActivePane.Selection.TypeText("��");
                //object page = WdFieldType.wdFieldPage;
                //myDoc.ActiveWindow.ActivePane.Selection.Fields.Add(myDoc.ActiveWindow.ActivePane.Selection.Range, ref��page, ref��Nothing, ref��Nothing);
                //myDoc.ActiveWindow.ActivePane.Selection.TypeText("ҳ/��");
                //object pages = WdFieldType.wdFieldNumPages;
                //myDoc.ActiveWindow.ActivePane.Selection.Fields.Add(myDoc.ActiveWindow.ActivePane.Selection.Range, ref��pages, ref��Nothing, ref��Nothing);
                //myDoc.ActiveWindow.ActivePane.Selection.TypeText("ҳ    ");
                myDoc.ActiveWindow.ActivePane.Selection.TypeText("�ƾ�λ��" + objOrgBll.GetOrganization(randomExam.OrgId).ShortName);
                myDoc.ActiveWindow.ActivePane.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;


				//para = myDoc.ActiveWindow.ActivePane.Selection.Paragraphs[19].Range;
				//para.Text = "==================================================��=====��====��===========================================";
				//para.Font.Size = (float)10.5;
				//para.Font.Bold = 0;
				//para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
				//para.InsertParagraphAfter();

				para1 = myDoc1.Content.Paragraphs[17].Range;
                para1.Text = "================================��=====��====��================================";
				//para1.Text = "==================================================��=====��====��===========================================";
				para1.Font.Size = (float)10.5;
				para1.Font.Bold = 0;
				para1.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
				para1.InsertParagraphAfter();

				myDoc.ActiveWindow.View.SeekView = WdSeekView.wdSeekMainDocument;

                para = myDoc.Content.Paragraphs[1].Range;
                para.Text = "";
                para.Font.Name = "����";
                para.Font.Size = (float)10.5;
                para.Font.Bold = 0;
                para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                para.InsertParagraphAfter();

				para = myDoc.Content.Paragraphs[2].Range;
				myDoc.Tables.Add(para, 2, 8, ref tableBehavior, ref autoFitBehavior);
				myDoc.Tables[1].Rows.Alignment = WdRowAlignment.wdAlignRowCenter;
				for (int i = 1; i <= 8; i++)
				{
					para.Tables[1].Columns[i].PreferredWidth = 50;
				}

				//para.Tables[1].Rows[2].Height = 30;

				para.Tables[1].Cell(1, 1).Range.Text = "���";
				para.Tables[1].Cell(1, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

				para.Tables[1].Cell(1, 2).Range.Text = "һ";
				para.Tables[1].Cell(1, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

				para.Tables[1].Cell(1, 3).Range.Text = "��";
				para.Tables[1].Cell(1, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

				para.Tables[1].Cell(1, 4).Range.Text = "��";
				para.Tables[1].Cell(1, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

				para.Tables[1].Cell(1, 5).Range.Text = "��";
				para.Tables[1].Cell(1, 5).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

				para.Tables[1].Cell(1, 6).Range.Text = "��";
				para.Tables[1].Cell(1, 6).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

				para.Tables[1].Cell(1, 7).Range.Text = "��";
				para.Tables[1].Cell(1, 7).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

				para.Tables[1].Cell(1, 8).Range.Text = "�ܷ�";
				para.Tables[1].Cell(1, 8).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

				para.Tables[1].Cell(2, 1).Range.Text = "�÷�";
				para.Tables[1].Cell(2, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
				para.Tables[1].Cell(2, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

				for (int i = 2; i <= 8; i++ )
				{
					para.Tables[1].Cell(2, i).Range.Text = "";
					para.Tables[1].Cell(2, i).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
				}

				para.Font.Size = (float)12;
				para.Font.Name = "����";
				para.ParagraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceExactly;
				para.ParagraphFormat.LineSpacing = 20;
				para.Font.Bold = 0;
				para.InsertParagraphAfter();

				para1 = myDoc1.Content.Paragraphs[18].Range;
				para1.Text = "";
				para1.Font.Name = "����";
				para1.Font.Size = (float)10.5;
				para1.Font.Bold = 0;
				para1.InsertParagraphAfter();


                para = myDoc.Content.Paragraphs[myDoc.Paragraphs.Count].Range;
                para.Text = "ע�����";
                para.Font.Size = (float)10.5;
                para.Font.Bold = 0;
                para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                para.InsertParagraphAfter();

                para = myDoc.Content.Paragraphs[myDoc.Paragraphs.Count].Range;
                para.Text = "1.���ǰ��װ�����Ϸ�����Ŀ��д���";
                para.Font.Size = (float)10.5;
                para.Font.Bold = 0;
                para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                para.InsertParagraphAfter();

                para = myDoc.Content.Paragraphs[myDoc.Paragraphs.Count].Range;
                para.Text = "2.����������ɫ���ɫ�ֱʡ�Բ��ʣ�������Ǧ�ʻ���";
                para.Font.Size = (float)10.5;
                para.Font.Bold = 0;
                para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                para.InsertParagraphAfter();

                para = myDoc.Content.Paragraphs[myDoc.Paragraphs.Count].Range;
                para.Text = "3.�����Ծ�" + randomExamSubjects.Count + "�����⣬����" + String.Format("{0:0.#}", nTotalScore) + "�֣�����ʱ��" + randomExam.ExamTime + "����";
                para.Font.Size = (float)10.5;
                para.Font.Bold = 0;
                para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                para.InsertParagraphAfter();


				RandomExamItemBLL randomItemBLL = new RandomExamItemBLL();
				RandomExamStrategyBLL strategyBLL = new RandomExamStrategyBLL();

				Hashtable hashTableItemIds = new Hashtable();
				int num = myDoc.Paragraphs.Count;
				int num1 = myDoc1.Paragraphs.Count;
				for (int i = 0; i < randomExamSubjects.Count; i++)
				{
					RandomExamSubject paperSubject = randomExamSubjects[i];
					int nSubjectId = paperSubject.RandomExamSubjectId;

					Hashtable htSubjectItemIds = new Hashtable();
					IList<RandomExamStrategy> strategys = strategyBLL.GetRandomExamStrategys(nSubjectId);
					for (int j = 0; j < strategys.Count; j++)
					{
						int nStrategyId = strategys[j].RandomExamStrategyId;
						int nNowItemCount = strategys[j].ItemCount;
						IList<RandomExamItem> itemList = randomItemBLL.GetItemsByStrategyId(nStrategyId, year);

						Random ObjRandom = new Random();
						Hashtable hashTable = new Hashtable();
						Hashtable hashTableCount = new Hashtable();
						int index = 0;
						while (hashTable.Count < nNowItemCount)
						{
							int k = ObjRandom.Next(itemList.Count);
							hashTableCount[index] = k;
							index = index + 1;
							int itemID = itemList[k].ItemId;
							int examItemID = itemList[k].RandomExamItemId;
							if (!hashTableItemIds.ContainsKey(itemID))
							{
								hashTable[examItemID] = examItemID;
								hashTableItemIds[itemID] = itemID;
								htSubjectItemIds[examItemID] = examItemID;
							}

							//if (hashTableCount.Count == itemList.Count && hashTable.Count < nNowItemCount)
							//{
							//    SessionSet.PageMessage = "����������趨��ȡ�ⷶΧ�ڵ�����������������������ȡ�ⷶΧ��";
							//    return;
							//}
						}
					}

					IList<RandomExamItem> paperItems = new List<RandomExamItem>();

					foreach (int key in htSubjectItemIds.Keys)
					{
						string strItemId = htSubjectItemIds[key].ToString();
						RandomExamItem item = randomItemBLL.GetRandomExamItem(int.Parse(strItemId), year);
                        paperItems.Add(item);

                        if(item.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                        {
                            IList<RandomExamItem> itemList = randomItemBLL.GetItemsByParentItemID(item.ItemId, int.Parse(strId), year);
                            foreach (RandomExamItem randomExamItem in itemList)
                            {
                                paperItems.Add(randomExamItem);
                            }
                        }
					}

                    num = myDoc.Paragraphs.Count;
                    para = myDoc.Content.Paragraphs[num].Range;
                    para.Text = "";
                    para.Font.Name = "����";
                    para.Font.Size = (float)10.5;
                    para.Font.Bold = 0;
                    para.InsertParagraphAfter();

					num = myDoc.Paragraphs.Count;
					para = myDoc.Content.Paragraphs[num].Range;

					para.Font.Name = "����";
					para.Font.Size = (float)12;
					myDoc.Tables.Add(para, 1, 4, ref tableBehavior, ref autoFitBehavior);
					para.Tables[1].Columns[1].PreferredWidth = 50;
					para.Tables[1].Columns[2].PreferredWidth = 50;
					para.Tables[1].Columns[3].PreferredWidth = 50;
					para.Tables[1].Columns[4].PreferredWidth = 50;

					para.Tables[1].Rows[1].HeightRule = WdRowHeightRule.wdRowHeightExactly;
					para.Tables[1].Rows[1].Height = 18;

					para.Tables[1].Cell(1, 1).Range.Text = "������";
					para.Tables[1].Cell(1, 1).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

					para.Tables[1].Cell(1, 2).Range.Text = "";
					para.Tables[1].Cell(1, 2).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

					para.Tables[1].Cell(1, 3).Range.Text = "�÷�";
					para.Tables[1].Cell(1, 3).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

					para.Tables[1].Cell(1, 4).Range.Text = "";
					para.Tables[1].Cell(1, 4).Range.Paragraphs.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;

					//para.Tables[1].Rows.WrapAroundText = 1;
                    //if (paperSubject.ItemTypeId == PrjPub.ITEMTYPE_JUDGE)
                    //{
                    //    para.Tables[1].Rows.HorizontalPosition = 270;
                    //}
                    //else
                    //{
                    //    para.Tables[1].Rows.HorizontalPosition = 200;
                    //}
					
                    //para.ParagraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceExactly;
                    //para.ParagraphFormat.LineSpacing = 20;
					//para.InsertParagraphAfter();

					num = myDoc.Paragraphs.Count;
					para = myDoc.Content.Paragraphs[num].Range;
                    //para.Select();
                    //object tableUnit = WdUnits.wdLine;
                    //para.Application.Selection.MoveUp(ref tableUnit, ref count, ref extend);
					if (paperSubject.ItemTypeId == PrjPub.ITEMTYPE_JUDGE)
					{
                        para.Text=GetNo(i) + "��" + paperSubject.SubjectName + "��   ����ȷ�����������ڴ򡱡̡�,��������������ڴ򡱡�����ÿ��" + paperSubject.UnitScore + "�֣���" +
									String.Format("{0:0.#}", paperSubject.ItemCount * paperSubject.UnitScore) + "�֣�";
					}
                    else if (paperSubject.ItemTypeId == PrjPub.ITEMTYPE_SINGLECHOOSE || paperSubject.ItemTypeId == PrjPub.ITEMTYPE_MULTICHOOSE || paperSubject.ItemTypeId==PrjPub.ITEMTYPE_FILLBLANK)
					{
                        para.Text = GetNo(i) + "��" + paperSubject.SubjectName + "��   ���뽫��ȷ�𰸵Ĵ������������ڣ�ÿ��" + paperSubject.UnitScore + "�֣���" +
                                    String.Format("{0:0.#}", paperSubject.ItemCount * paperSubject.UnitScore) + "�֣�";
                    }
                    else if (paperSubject.ItemTypeId == PrjPub.ITEMTYPE_DISCUSS || paperSubject.ItemTypeId == PrjPub.ITEMTYPE_LUNSHU)
                    {
                        para.Text = GetNo(i) + "��" + paperSubject.SubjectName + "��   ��ÿ��" + paperSubject.UnitScore + "�֣���" +
                                    String.Format("{0:0.#}", paperSubject.ItemCount * paperSubject.UnitScore) + "�֣�";
                    }
                     else if (paperSubject.ItemTypeId == PrjPub.ITEMTYPE_QUESTION)
                     {
                         para.Text = GetNo(i) + "��" + paperSubject.SubjectName + "��   ���뽫��ȷ�����������ڣ�ÿ��" + paperSubject.UnitScore + "�֣���" +
                                    String.Format("{0:0.#}", paperSubject.ItemCount * paperSubject.UnitScore) + "�֣�";
                     }
                     para.Font.Size = (float)12;
                     para.Font.Bold = 0;
                     para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                     para.InsertParagraphAfter();
                    //para.Application.Selection.Font.Size = (float)12;
                    //para.Application.Selection.Font.Name = "����";
                    //para.Application.Selection.InsertParagraphAfter();
                    //para.Application.Selection.Delete(ref character, ref count);

					num = myDoc.Paragraphs.Count;

					para1 = myDoc1.Content.Paragraphs[num1].Range;
					num1 = num1 + 1;
					if (paperSubject.ItemTypeId == PrjPub.ITEMTYPE_JUDGE)
					{
                        para1.Text = GetNo(i) + "��" + paperSubject.SubjectName + "��   ����ȷ�����������ڴ򡱡̡�,��������������ڴ򡱡�����ÿ��" + paperSubject.UnitScore + "�֣���" +
									String.Format("{0:0.#}", paperSubject.ItemCount * paperSubject.UnitScore) + "�֣�";
					}
					else
					{
                        para1.Text = GetNo(i) + "��" + paperSubject.SubjectName + "��   ��ÿ��" + paperSubject.UnitScore + "�֣���" +
									String.Format("{0:0.#}", paperSubject.ItemCount * paperSubject.UnitScore) + "�֣�";
					} 
                    para1.Font.Size = (float)12;
					para1.Font.Name = "����";
					para1.Font.Bold = 0;
					para1.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
					para1.InsertParagraphAfter();

                    int NowCount = 0;
				    nItemCount = paperItems.Count;
                    System.Threading.Thread.Sleep(10);
                    jsBlock = "<script>SetPorgressBar('�����Ծ�','0.00'); </script>";
                    Response.Write(jsBlock);
                    Response.Flush();

					#region �����հ��Ծ�
					if (paperItems != null)
					{
                        int z = 1;
					    int blankIndex = 1;
						for (int j = 0; j < paperItems.Count; j++)
						{
							RandomExamItem paperItem = paperItems[j];
							int k = j + 1;

                            if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                            {
                                z = 1;
                                k = blankIndex;
                                blankIndex++;
                            }
                            else if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                            {
                                k = z;
                                z++;
                            }

							bool isPictureItem;
							if (paperItem.SelectAnswer != null)
							{
								isPictureItem = paperItem.Content.ToLower().Contains("<img") ||
								                paperItem.SelectAnswer.ToLower().Contains("<img") ;
							}
							else
							{
								isPictureItem = paperItem.Content.ToLower().Contains("<img");
							}

							if (!isPictureItem)
							{
								#region ����ı�������Ŀ

								para = myDoc.Content.Paragraphs[num].Range;
								num = num + 1;
								para.ParagraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceExactly;
								para.ParagraphFormat.LineSpacing = 20;
                                if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                                {
                                    para.Text = "(" + k + "). " + paperItem.Content; //+"   ��" + String.Format("{0:0.#}", paperItem.Score) + "�֣�";
                                }
                                else
                                {
                                    para.Text = k + ". " + paperItem.Content.Replace("\n", ""); // +"   ��" + String.Format("{0:0.#}", paperSubject.UnitScore) + "�֣�";
                                }
                                para.Font.Name = "����";
								para.Font.Size = (float)10.5;;
								para.Font.Bold = 0;
								para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
								para.InsertParagraphAfter();

								#endregion
							}
							else
							{
								#region ���ͼƬ������Ŀ

								IHTMLDocument2 doc = new HTMLDocumentClass();
								doc.write(new object[] {paperItem.Content});
								doc.close();

								string[] src = new string[doc.images.length];
								string[] strImage = new string[doc.images.length];
								int t = 0;
								foreach (IHTMLImgElement image in doc.images)
								{
									IHTMLElement element = (IHTMLElement) image;
									strImage[t] = element.outerHTML;
									src[t] = (string) element.getAttribute("src", 2);
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
								para.ParagraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
								para.Select();
								myWord.Application.Selection.TypeText(k + ". ");
								for (int x = 0; x < strText.Length; x++)
								{
									myWord.Application.Selection.TypeText(ItemBLL.NoHTML(strText[x]));
									if (x < src.Length)
									{
										myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile,
										                                                     ref SaveWithDocument, ref Nothing);
									}
								}
								//myWord.Application.Selection.TypeText("   ��" + String.Format("{0:0.#}", paperSubject.UnitScore) + "�֣�");
								para.Font.Size = (float)10.5;;
								para.Font.Bold = 0;
								para.Font.Name = "����";
								para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
								para.InsertParagraphAfter();

								#endregion
							}

                            if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                            {
                                NowCount = NowCount + 1;
                                System.Threading.Thread.Sleep(10);
                                jsBlock = "<script>SetPorgressBar('�����Ծ�','" + ((double)(NowCount * 100) / ((double)nItemCount * 2)).ToString("0.00") + "'); </script>";
                                Response.Write(jsBlock);
                                Response.Flush();
                                continue;
                            }

							string strContent = "";
							string strTest = "";
							int flag = 0;
							if (!isPictureItem)
							{
                                if (paperItem.TypeId == PrjPub.ITEMTYPE_SINGLECHOOSE || paperItem.TypeId == PrjPub.ITEMTYPE_MULTICHOOSE || paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL )
								{
									#region ����ı�����ѡ��

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
											para.ParagraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceExactly;
											para.ParagraphFormat.LineSpacing = 20;
											para.Text = "  " + strTest;
											para.Font.Size = (float)10.5;;
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
												para.ParagraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceExactly;
												para.ParagraphFormat.LineSpacing = 20;
												para.Text = "  " + strTest;
												para.Font.Size = (float)10.5;;
												para.Font.Bold = 0;
												para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
												para.InsertParagraphAfter();
											}
											flag = flag + 1;
										}
									}

									#endregion
								}
                                else if (paperItem.TypeId == PrjPub.ITEMTYPE_DISCUSS || paperItem.TypeId == PrjPub.ITEMTYPE_LUNSHU)
								{
									int y;
									int answerLen = Pub.GetStringRealLength(paperItem.StandardAnswer);
									if (answerLen%100 == 0)
									{
										y = answerLen/100;
									}
									else
									{
										y = answerLen/100 + 1;
									}
									for (int x = 0; x < y + 1; x++)
									{
										para = myDoc.Content.Paragraphs[num].Range;
										num = num + 1;
										para.ParagraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceExactly;
										para.ParagraphFormat.LineSpacing = 20;
										para.Text = "";
										para.InsertParagraphAfter();
									}
								}
							}
							else
							{
                                if (paperItem.TypeId == PrjPub.ITEMTYPE_SINGLECHOOSE || paperItem.TypeId == PrjPub.ITEMTYPE_MULTICHOOSE)// || paperItem.TypeId == PrjPub.ITEMTYPE_JUDGE
								{
									#region ���ͼƬ����ѡ��

									IHTMLDocument2 doc = new HTMLDocumentClass();
									doc.write(new object[] {paperItem.SelectAnswer});
									doc.close();

									string[] src = new string[doc.images.length];
									string[] strImage = new string[doc.images.length];
									int t = 0;
									foreach (IHTMLImgElement image in doc.images)
									{
										IHTMLElement element = (IHTMLElement) image;
										strImage[t] = element.outerHTML;
										src[t] = (string) element.getAttribute("src", 2);
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
											para.ParagraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
											para.Select();
											myWord.Application.Selection.TypeText("  ");
											for (int x = 0; x < strText.Length; x++)
											{
												myWord.Application.Selection.TypeText(ItemBLL.NoHTML(strText[x]));
												if (x < src.Length)
												{
													myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile,
													                                                     ref SaveWithDocument, ref Nothing);
												}
											}
											para.Font.Size = (float)10.5;;
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
												para.ParagraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
												para.Select();
												myWord.Application.Selection.TypeText("  ");
												for (int x = 0; x < strText.Length; x++)
												{
													myWord.Application.Selection.TypeText(ItemBLL.NoHTML(strText[x]));
													if (x < src.Length)
													{
														myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile,
														                                                     ref SaveWithDocument, ref Nothing);
													}
												}
												para.Font.Size = (float)10.5;;
												para.Font.Bold = 0;
												para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
												para.InsertParagraphAfter();
											}
											flag = flag + 1;
										}
									}

									#endregion
								}
                                else if (paperItem.TypeId == PrjPub.ITEMTYPE_DISCUSS || paperItem.TypeId == PrjPub.ITEMTYPE_LUNSHU)
								{
									//int y;
									//int answerLen = Pub.GetStringRealLength(paperItem.StandardAnswer);
									//if (answerLen%100 == 0)
									//{
									//    y = answerLen/100;
									//}
									//else
									//{
									//    y = answerLen/100 + 1;
									//}
									//for (int x = 0; x < y + 1; x++)
									//{
									//    para = myDoc.Content.Paragraphs[num].Range;
									//    num = num + 1;
									//    para.ParagraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceExactly;
									//    para.ParagraphFormat.LineSpacing = 20;
									//    para.Text = "";
									//    para.InsertParagraphAfter();
									//}
									for (int x = 0; x < 10; x++)
									{
										para = myDoc.Content.Paragraphs[num].Range;
										num = num + 1;
										para.ParagraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceExactly;
										para.ParagraphFormat.LineSpacing = 20;
										para.Text = "";
										para.InsertParagraphAfter();
									}
								}
							}

							NowCount = NowCount + 1;

							System.Threading.Thread.Sleep(10);
							jsBlock = "<script>SetPorgressBar('�����Ծ�','" + ((double)(NowCount * 100) / ((double)nItemCount * 2)).ToString("0.00") +
							          "'); </script>";
							Response.Write(jsBlock);
							Response.Flush();
						}
					}
					#endregion 

					#region ���������Ծ�

					if (paperItems != null)
					{
                        int z = 1;
                        int blankIndex = 1;
						for (int j = 0; j < paperItems.Count; j++)
						{
							RandomExamItem paperItem = paperItems[j];
							int k = j + 1;

                            if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                            {
                                z = 1;
                                k = blankIndex;
                                blankIndex++;
                            }
                            else if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                            {
                                k = z;
                                z++;
                            }

							bool isPictureItem;
							if (paperItem.SelectAnswer != null)
							{
								isPictureItem = paperItem.Content.ToLower().Contains("<img") ||
												paperItem.SelectAnswer.ToLower().Contains("<img") ;
							}
							else
							{
								isPictureItem = paperItem.Content.ToLower().Contains("<img");
							}


							if (!isPictureItem)
							{
								#region ����ı�������Ŀ
								para1 = myDoc1.Content.Paragraphs[num1].Range;
								num1 = num1 + 1;
                                if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                                {
                                    para1.Text = "(" + k + "). " + paperItem.Content + "   ��" + String.Format("{0:0.#}", paperItem.Score) + "�֣�";
                                }
                                else
                                {
                                    para1.Text = k + ". " + paperItem.Content.Replace("\n", "") + "   ��" + String.Format("{0:0.#}", paperSubject.UnitScore) + "�֣�";
                                }
                                para1.Font.Name = "����";
								para1.Font.Size = (float)10.5;
								para1.Font.Bold = 0;
								para1.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
								para1.InsertParagraphAfter();
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

								para1 = myDoc1.Content.Paragraphs[num1].Range;
								num1 = num1 + 1;
								para1.Select();
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
								para1.Font.Size = (float)10.5;
								para1.Font.Bold = 0;
								para1.Font.Name = "����";
								para1.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
								para1.InsertParagraphAfter();
								#endregion
							}

                            if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                            {
                                NowCount = NowCount + 1;
                                System.Threading.Thread.Sleep(10);
                                jsBlock = "<script>SetPorgressBar('�����Ծ�','" + ((double)(NowCount * 100) / ((double)nItemCount*2)).ToString("0.00") + "'); </script>";
                                Response.Write(jsBlock);
                                Response.Flush();
                                continue;
                            }

							string strContent = "";
							string strTest = "";
							int flag = 0;

							if (paperItem.SelectAnswer != null)
							{
								isPictureItem = paperItem.SelectAnswer.ToLower().Contains("<img") ||
												paperItem.StandardAnswer.ToLower().Contains("<img");
							}
							else
							{
								isPictureItem = paperItem.StandardAnswer.ToLower().Contains("<img");
							}

							if (!isPictureItem)
							{
								if (paperItem.TypeId == PrjPub.ITEMTYPE_SINGLECHOOSE || paperItem.TypeId == PrjPub.ITEMTYPE_MULTICHOOSE || paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL) //|| paperItem.TypeId == PrjPub.ITEMTYPE_JUDGE
								{
									#region ����ı�����ѡ��

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
											para1 = myDoc1.Content.Paragraphs[num1].Range;
											num1 = num1 + 1;
											para1.Text = "  " + strTest;
											para1.Font.Size = (float)10.5;
											para1.Font.Bold = 0;
											para1.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
											para1.InsertParagraphAfter();

											flag = 0;
										}
										else
										{
											if (n + 1 == strAnswer.Length && strTest != "")
											{
												para1 = myDoc1.Content.Paragraphs[num1].Range;
												num1 = num1 + 1;
												para1.Text = "  " + strTest;
												para1.Font.Size = (float)10.5;
												para1.Font.Bold = 0;
												para1.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
												para1.InsertParagraphAfter();
											}
											flag = flag + 1;
										}
									}

									string strStanderAnswer = "";
									string[] str = paperItem.StandardAnswer.Split('|');
									for (int s = 0; s < str.Length; s++)
									{
										if (s == 0)
										{
											strStanderAnswer = intToChar(Convert.ToInt32(str[s])).ToString();
										}
										else
										{
											strStanderAnswer = strStanderAnswer + intToChar(Convert.ToInt32(str[s]));
										}
									}
									para1 = myDoc1.Content.Paragraphs[num1].Range;
									num1 = num1 + 1;
									para1.Text = "��ȷ�𰸣�" + strStanderAnswer;
									para1.Font.Size = (float)10.5;
									para1.Font.Bold = 0;
									para1.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
									para1.InsertParagraphAfter();

									#endregion
								}
								else if( paperItem.TypeId == PrjPub.ITEMTYPE_JUDGE)
								{
									para1 = myDoc1.Content.Paragraphs[num1].Range;
									num1 = num1 + 1;
								    para1.Text = "��ȷ�𰸣�" + (Convert.ToInt32(paperItem.StandardAnswer) == 0 ? "��" : "��");
									para1.Font.Size = (float)10.5;
									para1.Font.Bold = 0;
									para1.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
									para1.InsertParagraphAfter();
								}
								else
								{
                                    if (paperItem.StandardAnswer.IndexOf("\n") >= 0)
                                    {
                                        string strAnswer = paperItem.StandardAnswer.Replace("\n", "|");
                                        string[] str = strAnswer.Split('|');
                                        for(int x=0; x< str.Length; x++)
                                        {
                                            para1 = myDoc1.Content.Paragraphs[num1].Range;
                                            num1 = num1 + 1;
                                            if(x==0)
                                            {
                                                para1.Text = "��ȷ�𰸣�" + str[x];
                                            }
                                            else
                                            {
                                                para1.Text = str[x];
                                            }
                                            para1.Font.Size = (float)10.5;
                                            para1.Font.Bold = 0;
                                            para1.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                                            para1.InsertParagraphAfter(); 
                                        }
                                    }
                                    else
                                    {
                                        para1 = myDoc1.Content.Paragraphs[num1].Range;
                                        num1 = num1 + 1;
                                        para1.Text = "��ȷ�𰸣�" + paperItem.StandardAnswer;
                                        para1.Font.Size = (float)10.5;
                                        para1.Font.Bold = 0;
                                        para1.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                                        para1.InsertParagraphAfter();
                                    }
								}
							}
							else
							{
								if (paperItem.TypeId == PrjPub.ITEMTYPE_SINGLECHOOSE || paperItem.TypeId == PrjPub.ITEMTYPE_MULTICHOOSE)// || paperItem.TypeId == PrjPub.ITEMTYPE_JUDGE
								{
									#region ���ͼƬ����ѡ��

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
											para1 = myDoc1.Content.Paragraphs[num1].Range;
											num1 = num1 + 1;
											para1.Select();
											myWord.Application.Selection.TypeText("  ");
											for (int x = 0; x < strText.Length; x++)
											{
												myWord.Application.Selection.TypeText(ItemBLL.NoHTML(strText[x]));
												if (x < src.Length)
												{
													myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile,
																										 ref SaveWithDocument, ref Nothing);
												}
											}
											para1.Font.Size = (float)10.5;
											para1.Font.Bold = 0;
											para1.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
											para1.InsertParagraphAfter();

											flag = 0;
										}
										else
										{
											if (n + 1 == strAnswer.Length && strTest != "")
											{
												string[] strText = strTest.Split('@');
												para1 = myDoc1.Content.Paragraphs[num1].Range;
												num1 = num1 + 1;
												para1.Select();
												myWord.Application.Selection.TypeText("  ");
												for (int x = 0; x < strText.Length; x++)
												{
													myWord.Application.Selection.TypeText(ItemBLL.NoHTML(strText[x]));
													if (x < src.Length)
													{
														myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile,
																											 ref SaveWithDocument, ref Nothing);
													}
												}
												para1.Font.Size = (float)10.5;
												para1.Font.Bold = 0;
												para1.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
												para1.InsertParagraphAfter();
											}
											flag = flag + 1;
										}
									}

									string strStanderAnswer = "";
									string[] str = paperItem.StandardAnswer.Split('|');
									for (int s = 0; s < str.Length; s++)
									{
										if (s == 0)
										{
											strStanderAnswer = intToChar(Convert.ToInt32(str[s])).ToString();
										}
										else
										{
											strStanderAnswer = strStanderAnswer + intToChar(Convert.ToInt32(str[s]));
										}
									}

									para1 = myDoc1.Content.Paragraphs[num1].Range;
									num1 = num1 + 1;
									para1.Text = "��ȷ�𰸣�" + strStanderAnswer;
									para1.Font.Size = (float)10.5;
									para1.Font.Bold = 0;
									para1.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
									para1.InsertParagraphAfter();

									#endregion
								}
								else if (paperItem.TypeId == PrjPub.ITEMTYPE_JUDGE)
								{
									para1 = myDoc1.Content.Paragraphs[num1].Range;
									num1 = num1 + 1;
                                    para1.Text = "��ȷ�𰸣�" + (Convert.ToInt32(paperItem.StandardAnswer) == 0 ? "��" : "��");
									para1.Font.Size = (float)10.5;
									para1.Font.Bold = 0;
									para1.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
									para1.InsertParagraphAfter();
								}
								else
								{
									#region ���ͼƬ�����
									IHTMLDocument2 doc = new HTMLDocumentClass();
									doc.write(new object[] { paperItem.StandardAnswer });
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

									string strItem = paperItem.StandardAnswer;
									for (int x = 0; x < strImage.Length; x++)
									{
										strItem = strItem.Replace(strImage[x], "@");
									}

									string[] strText = strItem.Split('@');

									para1 = myDoc1.Content.Paragraphs[num1].Range;
									num1 = num1 + 1;
									para1.Select();
									myWord.Application.Selection.TypeText("��ȷ�𰸣� ");
									for (int x = 0; x < strText.Length; x++)
									{
										myWord.Application.Selection.TypeText(ItemBLL.NoHTML(strText[x]));
										if (x < src.Length)
										{
											myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile, ref SaveWithDocument, ref Nothing);
										}
									}

									para1.Font.Size = (float)10.5;
									para1.Font.Bold = 0;
									para1.Font.Name = "����";
									para1.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
									para1.InsertParagraphAfter();
									#endregion
								}
							}

							NowCount = NowCount + 1;

							System.Threading.Thread.Sleep(10);
							jsBlock = "<script>SetPorgressBar('�����Ծ�','" + ((double)(NowCount * 100) / ((double)nItemCount * 2)).ToString("0.00") + "'); </script>";
							Response.Write(jsBlock);
							Response.Flush();
						}

					}
					 #endregion

				}
				myWord.Application.Selection.EndKey(ref unit, ref extend);
				myWord.Application.Selection.TypeParagraph();
				myWord.Application.Selection.InsertBreak(ref breakType);
				myWord.Application.Selection.TypeBackspace();
				myWord.Application.Selection.Delete(ref character, ref count);
				myWord.Application.Selection.HomeKey(ref unit, ref extend);

				myDoc.SaveAs2000(ref filename, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
								 ref Nothing, ref Nothing, ref Nothing, ref Nothing);
				myDoc1.SaveAs2000(ref filename1, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
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
				myDoc1.SaveAs2000(ref filename1, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing, ref Nothing,
				 ref Nothing, ref Nothing, ref Nothing, ref Nothing);
				throw ex;
			}
			finally
			{
				myDoc.Close(ref Nothing, ref Nothing, ref Nothing);
				myDoc1.Close(ref Nothing, ref Nothing, ref Nothing);
				myWord.Application.Quit(ref Nothing, ref Nothing, ref Nothing);
				if (myDoc != null)
				{
					Marshal.ReleaseComObject(myDoc);
					myDoc = null;
				}
				if (myDoc1 != null)
				{
					Marshal.ReleaseComObject(myDoc1);
					myDoc1 = null;
				}
				if (myWord != null)
				{
					Marshal.ReleaseComObject(myWord);
					myWord = null;
				}
				GC.Collect();
			}

			Response.Write("<script>top.returnValue='" + strName + "';window.close();</script>");
		}
		#endregion

		#region ��������
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
		#endregion
	}
}
