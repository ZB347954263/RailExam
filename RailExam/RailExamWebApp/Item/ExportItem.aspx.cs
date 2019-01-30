using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using Excel;
using mshtml;
using RailExamWebApp.Common.Class;
using RailExam.BLL;
using RailExam.Model;
using System.Collections.Generic;
using Word;
using Application=Word.Application;
using ApplicationClass=Word.ApplicationClass;
using Range=Word.Range;
using WdAutoFitBehavior=Word.WdAutoFitBehavior;
using WdBreakType=Word.WdBreakType;
using WdDefaultTableBehavior=Word.WdDefaultTableBehavior;
using WdUnits=Word.WdUnits;

namespace RailExamWebApp.Item
{
	public partial class ExportItem : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (PrjPub.CurrentLoginUser == null)
            {
                Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                return;
            }

            if(!IsPostBack)
            {
                if (Request.QueryString.Get("type") == "word")
                {
                    ExportWord();
                }
                else
                {
                    ExportExcel();
                }
            }
		}

		private void ExportWord()
		{
			// 根据 ProgressBar.htm 显示进度条界面
			string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
			StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
			string html = reader.ReadToEnd();
			reader.Close();
			Response.Write(html);
			Response.Flush();
			System.Threading.Thread.Sleep(200);

			string strBookID = Request.QueryString.Get("bid");
			string strChapterID = Request.QueryString.Get("cid");

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

				BookBLL objBookBll = new BookBLL();
				RailExam.Model.Book objBook = objBookBll.GetBook(Convert.ToInt32(strBookID));
				strName = objBook.bookName;

				//生成.doc文件完整路径名 
				filename = Server.MapPath("/RailExamBao/Excel/" + objBook.bookName + "试题.doc");
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

				ItemBLL objItemBll = new ItemBLL();
				IList<RailExam.Model.Item> objItemList = new List<RailExam.Model.Item>();
                //objItemList = objItemBll.GetItems(null,null, Convert.ToInt32(strBookID), Convert.ToInt32(strChapterID), null, null,
                //                                  -1, -1, -1, -1, -1, 0, int.MaxValue, "", -1, PrjPub.CurrentLoginUser.StationOrgID);

                if(strChapterID == "-1")
                {
                    objItemList = objItemBll.GetItemsByChapterId(Convert.ToInt32(strBookID), Convert.ToInt32(strChapterID));
                }
                else
                {
                    objItemList = objItemBll.GetItemsByBookChapterId(Convert.ToInt32(strBookID), Convert.ToInt32(strChapterID), 0,
                                                                     int.MaxValue);
                }
				int totalCount = objItemList.Count;
				int num = 1;
				Range para = null;
				int subjectNum = 0;
				int NowCount = 0;

				#region  单选
				objItemList = objItemBll.GetItems(null,null, Convert.ToInt32(strBookID), Convert.ToInt32(strChapterID), null, null,
												  PrjPub.ITEMTYPE_SINGLECHOOSE, -1, -1, -1, -1, 0, int.MaxValue, "", -1,
                                                  PrjPub.CurrentLoginUser.StationOrgID, PrjPub.CurrentLoginUser.RailSystemID);

				if(objItemList.Count > 0 )
				{
					para = myDoc.Content.Paragraphs[num].Range;
					num = num + 1;
					para.Text = GetNo(subjectNum) + "、单选（共" + objItemList.Count + "题）";
					para.Font.Size = (float)10.5;
					para.Font.Bold = 0;
					para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
					para.InsertParagraphAfter();

					for (int i = 0; i < objItemList.Count; i++ )
					{
						RailExam.Model.Item objItem = objItemList[i];
						int k = i+ 1;

						bool isPictureItem;
						if (objItem.SelectAnswer != null)
						{
							isPictureItem = objItem.Content.ToLower().Contains("<img") ||
											  objItem.SelectAnswer.ToLower().Contains("<img");
						}
						else
						{
							isPictureItem = objItem.Content.ToLower().Contains("<img");
						}

						if (!isPictureItem)
						{
							#region 输出文本试题题目
							para = myDoc.Content.Paragraphs[num].Range;
							num = num + 1;
							para.Text = k + ". " + objItem.Content;
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
							doc.write(new object[] { objItem.Content });
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

							string strItem = objItem.Content;
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
							para.Font.Size = 9;
							para.Font.Bold = 0;
							para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
							para.InsertParagraphAfter();
							#endregion
						}

						string strContent = "";
						string strTest = "";
						int flag = 0;
						if (!isPictureItem)
						{
							#region 输出文本试题答案

							string[] strAnswer = objItem.SelectAnswer.Split('|');
							for (int n = 0; n < strAnswer.Length; n++)
							{
								string strN = intToChar(n).ToString();

								strAnswer[n] = strAnswer[n].Replace("\r", "").Replace("\t", "").Replace("\n", "");

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

							string strStanderAnswer = "";
							string[] str = objItem.StandardAnswer.Split('|');
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
							para = myDoc.Content.Paragraphs[num].Range;
							num = num + 1;
							para.Text = "正确答案：" + strStanderAnswer;
							para.Font.Size = 9;
							para.Font.Bold = 0;
							para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
							para.InsertParagraphAfter();

							#endregion
						}
						else
						{
							#region 输出图片试题答案

							IHTMLDocument2 doc = new HTMLDocumentClass();
							doc.write(new object[] { objItem.SelectAnswer });
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

							string strItem = objItem.SelectAnswer;
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
											myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile,
																								 ref SaveWithDocument, ref Nothing);
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
												myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile,
																									 ref SaveWithDocument, ref Nothing);
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

							string strStanderAnswer = "";
							string[] str = objItem.StandardAnswer.Split('|');
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

							para = myDoc.Content.Paragraphs[num].Range;
							num = num + 1;
							para.Text = "正确答案：" + strStanderAnswer;
							para.Font.Size = 9;
							para.Font.Bold = 0;
							para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
							para.InsertParagraphAfter();

							#endregion
						}

						NowCount = NowCount + 1;

						System.Threading.Thread.Sleep(10);
						jsBlock = "<script>SetPorgressBar('导出试题','" + ((double)(NowCount * 100) / (double)totalCount).ToString("0.00") + "'); </script>";
						Response.Write(jsBlock);
						Response.Flush();
					}

					subjectNum++;
				}
				#endregion

				#region 多选
				objItemList = objItemBll.GetItems(null, null, Convert.ToInt32(strBookID), Convert.ToInt32(strChapterID), null, null,
								  PrjPub.ITEMTYPE_MULTICHOOSE, -1, -1, -1, -1, 0, int.MaxValue, "", -1,
                                  PrjPub.CurrentLoginUser.StationOrgID, PrjPub.CurrentLoginUser.RailSystemID);

				if (objItemList.Count > 0)
				{
					para = myDoc.Content.Paragraphs[num].Range;
					num = num + 1;
					para.Text = GetNo(subjectNum) + "、多选（共" + objItemList.Count + "题）";
					para.Font.Size = (float)10.5;
					para.Font.Bold = 0;
					para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
					para.InsertParagraphAfter();

					for (int i = 0; i < objItemList.Count; i++)
					{
						RailExam.Model.Item objItem = objItemList[i];
						int k = i + 1;

						bool isPictureItem;
						if (objItem.SelectAnswer != null)
						{
							isPictureItem = objItem.Content.ToLower().Contains("<img") ||
											  objItem.SelectAnswer.ToLower().Contains("<img");
						}
						else
						{
							isPictureItem = objItem.Content.ToLower().Contains("<img");
						}

						if (!isPictureItem)
						{
							#region 输出文本试题题目
							para = myDoc.Content.Paragraphs[num].Range;
							num = num + 1;
							para.Text = k + ". " + objItem.Content;
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
							doc.write(new object[] { objItem.Content });
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

							string strItem = objItem.Content;
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
							para.Font.Size = 9;
							para.Font.Bold = 0;
							para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
							para.InsertParagraphAfter();
							#endregion
						}

						string strContent = "";
						string strTest = "";
						int flag = 0;
						if (!isPictureItem)
						{
							#region 输出文本试题答案

							string[] strAnswer = objItem.SelectAnswer.Split('|');
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

							string strStanderAnswer = "";
							string[] str = objItem.StandardAnswer.Split('|');
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
							para = myDoc.Content.Paragraphs[num].Range;
							num = num + 1;
							para.Text = "正确答案：" + strStanderAnswer;
							para.Font.Size = 9;
							para.Font.Bold = 0;
							para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
							para.InsertParagraphAfter();

							#endregion
						}
						else
						{
							#region 输出图片试题答案

							IHTMLDocument2 doc = new HTMLDocumentClass();
							doc.write(new object[] { objItem.SelectAnswer });
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

							string strItem = objItem.SelectAnswer;
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
											myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile,
																								 ref SaveWithDocument, ref Nothing);
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
												myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile,
																									 ref SaveWithDocument, ref Nothing);
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

							string strStanderAnswer = "";
							string[] str = objItem.StandardAnswer.Split('|');
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

							para = myDoc.Content.Paragraphs[num].Range;
							num = num + 1;
							para.Text = "正确答案：" + strStanderAnswer;
							para.Font.Size = 9;
							para.Font.Bold = 0;
							para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
							para.InsertParagraphAfter();

							#endregion
						}

						NowCount = NowCount + 1;

						System.Threading.Thread.Sleep(10);
						jsBlock = "<script>SetPorgressBar('导出试题','" + ((double)(NowCount * 100) / (double)totalCount).ToString("0.00") + "'); </script>";
						Response.Write(jsBlock);
						Response.Flush();
					}

					subjectNum++;
				}
				#endregion

				#region 判断
				objItemList = objItemBll.GetItems(null, null, Convert.ToInt32(strBookID), Convert.ToInt32(strChapterID), null, null,
								  PrjPub.ITEMTYPE_JUDGE, -1, -1, -1, -1, 0, int.MaxValue, "", -1,
                                  PrjPub.CurrentLoginUser.StationOrgID, PrjPub.CurrentLoginUser.RailSystemID);

				if (objItemList.Count > 0)
				{
					para = myDoc.Content.Paragraphs[num].Range;
					num = num + 1;
					para.Text = GetNo(subjectNum) + "、判断（共" + objItemList.Count + "题）";
					para.Font.Size = (float)10.5;
					para.Font.Bold = 0;
					para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
					para.InsertParagraphAfter();

					for (int i = 0; i < objItemList.Count; i++)
					{
						RailExam.Model.Item objItem = objItemList[i];
						int k = i + 1;

						bool isPictureItem;
						if (objItem.SelectAnswer != null)
						{
							isPictureItem = objItem.Content.ToLower().Contains("<img") ||
											  objItem.SelectAnswer.ToLower().Contains("<img");
						}
						else
						{
							isPictureItem = objItem.Content.ToLower().Contains("<img");
						}

						if (!isPictureItem)
						{
							#region 输出文本试题题目
							para = myDoc.Content.Paragraphs[num].Range;
							num = num + 1;
							para.Text = k + ". " + objItem.Content;
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
							doc.write(new object[] { objItem.Content });
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

							string strItem = objItem.Content;
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
							para.Font.Size = 9;
							para.Font.Bold = 0;
							para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
							para.InsertParagraphAfter();
							#endregion
						}

						string strContent = "";
						string strTest = "";
						int flag = 0;
						if (!isPictureItem)
						{
							#region 输出文本试题答案

							string[] strAnswer = objItem.SelectAnswer.Split('|');
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

							string strStanderAnswer = "";
							string[] str = objItem.StandardAnswer.Split('|');
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
							para = myDoc.Content.Paragraphs[num].Range;
							num = num + 1;
							para.Text = "正确答案：" + strStanderAnswer;
							para.Font.Size = 9;
							para.Font.Bold = 0;
							para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
							para.InsertParagraphAfter();

							#endregion
						}
						else
						{
							#region 输出图片试题答案

							IHTMLDocument2 doc = new HTMLDocumentClass();
							doc.write(new object[] { objItem.SelectAnswer });
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

							string strItem = objItem.SelectAnswer;
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
											myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile,
																								 ref SaveWithDocument, ref Nothing);
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
												myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile,
																									 ref SaveWithDocument, ref Nothing);
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

							string strStanderAnswer = "";
							string[] str = objItem.StandardAnswer.Split('|');
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

							para = myDoc.Content.Paragraphs[num].Range;
							num = num + 1;
							para.Text = "正确答案：" + strStanderAnswer;
							para.Font.Size = 9;
							para.Font.Bold = 0;
							para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
							para.InsertParagraphAfter();

							#endregion
						}

						NowCount = NowCount + 1;

						System.Threading.Thread.Sleep(10);
						jsBlock = "<script>SetPorgressBar('导出试题','" + ((double)(NowCount * 100) / (double)totalCount).ToString("0.00") + "'); </script>";
						Response.Write(jsBlock);
						Response.Flush();
					}

					subjectNum++;
				}
				#endregion

                #region 综合选择题
                objItemList = objItemBll.GetItems(null, null, Convert.ToInt32(strBookID), Convert.ToInt32(strChapterID), null, null,
                                  PrjPub.ITEMTYPE_FILLBLANK, -1, -1, -1, -1, 0, int.MaxValue, "", -1,
                                  PrjPub.CurrentLoginUser.StationOrgID, PrjPub.CurrentLoginUser.RailSystemID);

                if (objItemList.Count > 0)
                {
                    para = myDoc.Content.Paragraphs[num].Range;
                    num = num + 1;
                    para.Text = GetNo(subjectNum) + "、综合选择题（共" + objItemList.Count + "题）";
                    para.Font.Size = (float)10.5;
                    para.Font.Bold = 0;
                    para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    para.InsertParagraphAfter();

                    IList<RailExam.Model.Item> itemList = new List<RailExam.Model.Item>();
                    foreach (RailExam.Model.Item item in objItemList)
                    {
                        itemList.Add(item);
                        IList<RailExam.Model.Item> itemDetailList = objItemBll.GetItemsByParentItemID(item.ItemId);
                        foreach(RailExam.Model.Item item1 in itemDetailList)
                        {
                            itemList.Add(item1);
                        }
                    }

                    int z = 1;
                    int y = 1;
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        RailExam.Model.Item objItem = itemList[i];
                        int k = i + 1;

                        if (objItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                        {
                            z = 1;
                            k = y;
                            y++;
                        }
                        else if (objItem.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                        {
                            k = z;
                            z++;
                        }

                        bool isPictureItem;
                        if (objItem.SelectAnswer != null)
                        {
                            isPictureItem = objItem.Content.ToLower().Contains("<img") ||
                                              objItem.SelectAnswer.ToLower().Contains("<img");
                        }
                        else
                        {
                            isPictureItem = objItem.Content.ToLower().Contains("<img");
                        }

                        if (!isPictureItem)
                        {
                            #region 输出文本试题题目
                            para = myDoc.Content.Paragraphs[num].Range;
                            num = num + 1;
                            if (objItem.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                            {
                                para.Text = "(" + k + "). " + objItem.Content;
                            }
                            else
                            {
                                para.Text = k + ". " + objItem.Content.Replace("\n", "");
                            }
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
                            doc.write(new object[] { objItem.Content });
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

                            string strItem = objItem.Content;
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
                            para.Font.Size = 9;
                            para.Font.Bold = 0;
                            para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                            para.InsertParagraphAfter();
                            #endregion
                        }

                        if (objItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                        {
                            NowCount = NowCount + 1;
                            System.Threading.Thread.Sleep(10);
                            jsBlock = "<script>SetPorgressBar('导出试题','" + ((double)(NowCount * 100) / (double)totalCount).ToString("0.00") + "'); </script>";
                            Response.Write(jsBlock);
                            Response.Flush();
                            continue;
                        }

                        string strContent = "";
                        string strTest = "";
                        int flag = 0;
                        if (!isPictureItem)
                        {
                            #region 输出文本试题答案

                            string[] strAnswer = objItem.SelectAnswer.Split('|');
                            for (int n = 0; n < strAnswer.Length; n++)
                            {
                                string strN = intToChar(n).ToString();

                                strAnswer[n] = strAnswer[n].Replace("\r", "").Replace("\t", "").Replace("\n", "");

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

                            string strStanderAnswer = "";
                            string[] str = objItem.StandardAnswer.Split('|');
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
                            para = myDoc.Content.Paragraphs[num].Range;
                            num = num + 1;
                            para.Text = "正确答案：" + strStanderAnswer;
                            para.Font.Size = 9;
                            para.Font.Bold = 0;
                            para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                            para.InsertParagraphAfter();

                            #endregion
                        }
                        else
                        {
                            #region 输出图片试题答案

                            IHTMLDocument2 doc = new HTMLDocumentClass();
                            doc.write(new object[] { objItem.SelectAnswer });
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

                            string strItem = objItem.SelectAnswer;
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
                                            myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile,
                                                                                                 ref SaveWithDocument, ref Nothing);
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
                                                myWord.Application.Selection.InlineShapes.AddPicture(Server.MapPath(src[x]), ref LinkToFile,
                                                                                                     ref SaveWithDocument, ref Nothing);
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

                            string strStanderAnswer = "";
                            string[] str = objItem.StandardAnswer.Split('|');
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

                            para = myDoc.Content.Paragraphs[num].Range;
                            num = num + 1;
                            para.Text = "正确答案：" + strStanderAnswer;
                            para.Font.Size = 9;
                            para.Font.Bold = 0;
                            para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                            para.InsertParagraphAfter();

                            #endregion
                        }

                        NowCount = NowCount + 1;

                        System.Threading.Thread.Sleep(10);
                        jsBlock = "<script>SetPorgressBar('导出试题','" + ((double)(NowCount * 100) / (double)totalCount).ToString("0.00") + "'); </script>";
                        Response.Write(jsBlock);
                        Response.Flush();
                    }

                    subjectNum++;
                }
                #endregion

                #region 填空
                objItemList = objItemBll.GetItems(null, null, Convert.ToInt32(strBookID), Convert.ToInt32(strChapterID), null, null,
                                  PrjPub.ITEMTYPE_QUESTION, -1, -1, -1, -1, 0, int.MaxValue, "", -1,
                                  PrjPub.CurrentLoginUser.StationOrgID, PrjPub.CurrentLoginUser.RailSystemID);

				if (objItemList.Count > 0)
				{
					para = myDoc.Content.Paragraphs[num].Range;
					num = num + 1;
					para.Text = GetNo(subjectNum) + "、填空（共" + objItemList.Count + "题）";
					para.Font.Size = (float)10.5;
					para.Font.Bold = 0;
					para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
					para.InsertParagraphAfter();

					for (int i = 0; i < objItemList.Count; i++)
					{
						RailExam.Model.Item objItem = objItemList[i];
						int k = i + 1;

						bool isPictureItem;
						if (objItem.SelectAnswer != null)
						{
							isPictureItem = objItem.Content.ToLower().Contains("<img") ||
											  objItem.SelectAnswer.ToLower().Contains("<img");
						}
						else
						{
							isPictureItem = objItem.Content.ToLower().Contains("<img");
						}

						if (!isPictureItem)
						{
							#region 输出文本试题题目
							para = myDoc.Content.Paragraphs[num].Range;
							num = num + 1;
							para.Text = k + ". " + objItem.Content;
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
							doc.write(new object[] { objItem.Content });
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

							string strItem = objItem.Content;
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
							para.Font.Size = 9;
							para.Font.Bold = 0;
							para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
							para.InsertParagraphAfter();
							#endregion
						}

						para = myDoc.Content.Paragraphs[num].Range;
						num = num + 1;
						para.Text = "正确答案：" + objItem.StandardAnswer;
						para.Font.Size = 9;
						para.Font.Bold = 0;
						para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
						para.InsertParagraphAfter();

						NowCount = NowCount + 1;

						System.Threading.Thread.Sleep(10);
						jsBlock = "<script>SetPorgressBar('导出试题','" + ((double)(NowCount * 100) / (double)totalCount).ToString("0.00") + "'); </script>";
						Response.Write(jsBlock);
						Response.Flush();
					}

					subjectNum++;
				}
				#endregion

				#region 简答
				objItemList = objItemBll.GetItems(null, null, Convert.ToInt32(strBookID), Convert.ToInt32(strChapterID), null, null,
								  PrjPub.ITEMTYPE_DISCUSS, -1, -1, -1, -1, 0, int.MaxValue, "", -1,
                                  PrjPub.CurrentLoginUser.StationOrgID, PrjPub.CurrentLoginUser.RailSystemID);

				if (objItemList.Count > 0)
				{
					para = myDoc.Content.Paragraphs[num].Range;
					num = num + 1;
					para.Text = GetNo(subjectNum) + "、简答（共" + objItemList.Count + "题）";
					para.Font.Size = (float)10.5;
					para.Font.Bold = 0;
					para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
					para.InsertParagraphAfter();

					for (int i = 0; i < objItemList.Count; i++)
					{
						RailExam.Model.Item objItem = objItemList[i];
						int k = i + 1;

						bool isPictureItem;
						if (objItem.SelectAnswer != null)
						{
							isPictureItem = objItem.Content.ToLower().Contains("<img") ||
											  objItem.SelectAnswer.ToLower().Contains("<img");
						}
						else
						{
							isPictureItem = objItem.Content.ToLower().Contains("<img");
						}

						if (!isPictureItem)
						{
							#region 输出文本试题题目
							para = myDoc.Content.Paragraphs[num].Range;
							num = num + 1;
							para.Text = k + ". " + objItem.Content;
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
							doc.write(new object[] { objItem.Content });
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

							string strItem = objItem.Content;
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
							para.Font.Size = 9;
							para.Font.Bold = 0;
							para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
							para.InsertParagraphAfter();
							#endregion
						}

						para = myDoc.Content.Paragraphs[num].Range;
						num = num + 1;
						para.Text = "正确答案：" + objItem.StandardAnswer;
						para.Font.Size = 9;
						para.Font.Bold = 0;
						para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
						para.InsertParagraphAfter();

						NowCount = NowCount + 1;

						System.Threading.Thread.Sleep(10);
						jsBlock = "<script>SetPorgressBar('导出试题','" + ((double)(NowCount * 100) / (double)totalCount).ToString("0.00") + "'); </script>";
						Response.Write(jsBlock);
						Response.Flush();
					}

					subjectNum++;
				}
				#endregion

				#region 论述
				objItemList = objItemBll.GetItems(null, null, Convert.ToInt32(strBookID), Convert.ToInt32(strChapterID), null, null,
								  PrjPub.ITEMTYPE_LUNSHU, -1, -1, -1, -1, 0, int.MaxValue, "", -1,
                                  PrjPub.CurrentLoginUser.StationOrgID, PrjPub.CurrentLoginUser.RailSystemID);

				if (objItemList.Count > 0)
				{
					para = myDoc.Content.Paragraphs[num].Range;
					num = num + 1;
					para.Text = GetNo(subjectNum) + "、论述（共" + objItemList.Count + "题）";
					para.Font.Size = (float)10.5;
					para.Font.Bold = 0;
					para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
					para.InsertParagraphAfter();

					for (int i = 0; i < objItemList.Count; i++)
					{
						RailExam.Model.Item objItem = objItemList[i];
						int k = i + 1;

						bool isPictureItem;
						if (objItem.SelectAnswer != null)
						{
							isPictureItem = objItem.Content.ToLower().Contains("<img") ||
											  objItem.SelectAnswer.ToLower().Contains("<img");
						}
						else
						{
							isPictureItem = objItem.Content.ToLower().Contains("<img");
						}

						if (!isPictureItem)
						{
							#region 输出文本试题题目
							para = myDoc.Content.Paragraphs[num].Range;
							num = num + 1;
							para.Text = k + ". " + objItem.Content;
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
							doc.write(new object[] { objItem.Content });
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

							string strItem = objItem.Content;
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
							para.Font.Size = 9;
							para.Font.Bold = 0;
							para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
							para.InsertParagraphAfter();
							#endregion
						}

						para = myDoc.Content.Paragraphs[num].Range;
						num = num + 1;
						para.Text = "正确答案：" + objItem.StandardAnswer;
						para.Font.Size = 9;
						para.Font.Bold = 0;
						para.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
						para.InsertParagraphAfter();

						NowCount = NowCount + 1;

						System.Threading.Thread.Sleep(10);
						jsBlock = "<script>SetPorgressBar('导出试题','" + ((double)(NowCount * 100) / (double)totalCount).ToString("0.00") + "'); </script>";
						Response.Write(jsBlock);
						Response.Flush();
					}

					subjectNum++;
				}
				#endregion

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

			Response.Write("<script>top.returnValue='" + strName + "';window.close();</script>");
		}

		private void ExportExcel()
		{
			// 根据 ProgressBar.htm 显示进度条界面
			string templateFileName = Server.MapPath("/RailExamBao/RandomExam/ProgressBar.htm");
			StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("gb2312"));
			string html = reader.ReadToEnd();
			reader.Close();
			Response.Write(html);
			Response.Flush();
			System.Threading.Thread.Sleep(200);

			string strBookID = Request.QueryString.Get("bid");
			string strChapterID = Request.QueryString.Get("cid");

			string jsBlock;

			Excel.Application objApp = new Excel.ApplicationClass();
			Excel.Workbooks objbooks = objApp.Workbooks;
			Excel.Workbook objbook = objbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
			Excel.Worksheet objSheet = (Excel.Worksheet) objbook.Worksheets[1]; //取得sheet1 
			string filename = "";
			string strName = "";
			try
			{
				BookBLL objBookBll = new BookBLL();
				RailExam.Model.Book objBook = objBookBll.GetBook(Convert.ToInt32(strBookID));
				strName = objBook.bookName;

				//生成.xls文件完整路径名 
				filename = Server.MapPath("/RailExamBao/Excel/" + objBook.bookName + "试题.xls");

				if (File.Exists(filename.ToString()))
				{
					File.Delete(filename.ToString());
				}
				objSheet.Cells.Font.Size = 10;
				objSheet.Cells.Font.Name = "宋体";

				objSheet.Cells[1, 1] = "序号";
				((Excel.Range) objSheet.Cells[1, 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

				int m = 2;
				objSheet.Cells[1, 2] = "教材名称";
				objSheet.get_Range(objSheet.Cells[1, 2], objSheet.Cells[1, 2]).Merge(0);
				objSheet.get_Range(objSheet.Cells[1, 2], objSheet.Cells[1, 2]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

				objSheet.Cells[1, m+1] = "章";
				objSheet.get_Range(objSheet.Cells[1, m + 1], objSheet.Cells[1, m + 1]).Merge(0);
				objSheet.get_Range(objSheet.Cells[1, m + 1], objSheet.Cells[1, m + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

				objSheet.Cells[1, m + 2] = "节";
				objSheet.get_Range(objSheet.Cells[1, m + 2], objSheet.Cells[1, m + 2]).Merge(0);
				objSheet.get_Range(objSheet.Cells[1, m + 2], objSheet.Cells[1, m + 2]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

				objSheet.Cells[1, m + 3] = "试题内容";
				objSheet.get_Range(objSheet.Cells[1, m + 3], objSheet.Cells[1, m + 3]).Merge(0);
				objSheet.get_Range(objSheet.Cells[1, m + 3], objSheet.Cells[1, m + 3]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

				objSheet.Cells[1, m + 4] = "试题类型";
				objSheet.get_Range(objSheet.Cells[1, m+4], objSheet.Cells[1, m+4]).Merge(0);
				objSheet.get_Range(objSheet.Cells[1, m+4], objSheet.Cells[1, m+4]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

				objSheet.Cells[1, m + 5] = "选项数目";
				objSheet.get_Range(objSheet.Cells[1, m+5], objSheet.Cells[1, m+5]).Merge(0);
				objSheet.get_Range(objSheet.Cells[1, m+5], objSheet.Cells[1, m+5]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

				objSheet.Cells[1, m + 6] = "过期时间";
				objSheet.get_Range(objSheet.Cells[1, m+6], objSheet.Cells[1, m+6]).Merge(0);
				objSheet.get_Range(objSheet.Cells[1, m+6], objSheet.Cells[1, m+6]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

				objSheet.Cells[1, m + 7] = "关键字";
				objSheet.get_Range(objSheet.Cells[1, m+7], objSheet.Cells[1, m+7]).Merge(0);
				objSheet.get_Range(objSheet.Cells[1, m+7], objSheet.Cells[1, m+7]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                objSheet.Cells[1, m + 8] = "仅用于考试";
                objSheet.get_Range(objSheet.Cells[1, m + 8], objSheet.Cells[1, m + 8]).Merge(0);
                objSheet.get_Range(objSheet.Cells[1, m + 8], objSheet.Cells[1, m + 8]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

				objSheet.Cells[1, m + 9] = "正确选项";
				objSheet.get_Range(objSheet.Cells[1, m+9], objSheet.Cells[1, m+9]).Merge(0);
				objSheet.get_Range(objSheet.Cells[1, m+9], objSheet.Cells[1, m+9]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

				ItemBLL objItemBll = new ItemBLL();
				IList<RailExam.Model.Item> objItemList = new List<RailExam.Model.Item>();
				objItemList = objItemBll.GetItems(null,null, Convert.ToInt32(strBookID), Convert.ToInt32(strChapterID), null, null,
                                                  -1, -1, -1, -1, -1, 0, int.MaxValue, "", -1, PrjPub.CurrentLoginUser.StationOrgID, PrjPub.CurrentLoginUser.RailSystemID);
				int colNum= 2;
				int n = 0;

                IList<RailExam.Model.Item> itemList = new List<RailExam.Model.Item>();
                foreach (RailExam.Model.Item item in objItemList)
                {
                    if (item.HasPicture == 0)
                    {
                        itemList.Add(item);
                        if (item.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                        {
                            IList<RailExam.Model.Item> itemDetailList = objItemBll.GetItemsByParentItemID(item.ItemId);
                            foreach (RailExam.Model.Item item1 in itemDetailList)
                            {
                                itemList.Add(item1);
                                n++;
                            }
                        }

                        if (item.AnswerCount > colNum)
                        {
                            colNum = item.AnswerCount;
                        }
                        n++;
                    }
                }

				int totalCount = n;

				for (int i = 0; i < colNum; i++ )
				{
					objSheet.Cells[1, m+10+i] = intToChar(i).ToString();
					objSheet.get_Range(objSheet.Cells[1, m + 10 + i], objSheet.Cells[1, m + 10 + i]).Merge(0);
					objSheet.get_Range(objSheet.Cells[1, m + 10 + i], objSheet.Cells[1, m + 10 + i]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
				}

				BookChapterBLL objChapterBll = new BookChapterBLL();
                for (int i = 0; i < itemList.Count; i++)
				{
                    RailExam.Model.Item objItem = itemList[i];
					int k = i + 1;

					if(objItem.HasPicture == 0)
					{
						objSheet.Cells[i + 2, 1] = k.ToString();
						objSheet.get_Range(objSheet.Cells[i + 2, 1], objSheet.Cells[i + 2, 1]).Merge(0);
						objSheet.get_Range(objSheet.Cells[i + 2, 1], objSheet.Cells[i + 2, 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

						objSheet.Cells[i + 2, 2] = strName;
						m = 2;

						BookChapter objChapter = objChapterBll.GetBookChapter(objItem.ChapterId);
						if(objChapter.ParentId == 0)
						{
							objSheet.Cells[i + 2, m + 1] = objChapter.ChapterName;
							objSheet.Cells[i + 2, m + 2] = "";
						}
						else
						{
							BookChapter objParentChapter = objChapterBll.GetBookChapter(objChapter.ParentId);
							objSheet.Cells[i + 2, m + 1] = objParentChapter.ChapterName;
							objSheet.Cells[i + 2, m + 2] = objChapter.ChapterName;
						}

                        objSheet.Cells[i + 2, m + 3] = objItem.Content;

						objSheet.Cells[i + 2, m + 4] = objItem.TypeId;
						objSheet.get_Range(objSheet.Cells[i + 2, m + 4], objSheet.Cells[i + 2, m + 4]).Merge(0);
						objSheet.get_Range(objSheet.Cells[i + 2, m + 4], objSheet.Cells[i + 2, m + 4]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

                        if (objItem.TypeId == PrjPub.ITEMTYPE_SINGLECHOOSE || objItem.TypeId == PrjPub.ITEMTYPE_MULTICHOOSE || objItem.TypeId == PrjPub.ITEMTYPE_JUDGE
                            || objItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK || objItem.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
						{
							objSheet.Cells[i + 2, m + 5] = objItem.AnswerCount;
						}
						else
						{
							objSheet.Cells[i + 2, m + 5] = "";
						}
						objSheet.get_Range(objSheet.Cells[i + 2, m + 5], objSheet.Cells[i + 2, m + 5]).Merge(0);
						objSheet.get_Range(objSheet.Cells[i + 2, m + 5], objSheet.Cells[i + 2, m + 5]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

						if(objItem.OutDateDate.Year == 2050)
						{
							objSheet.Cells[i + 2, m + 6] = "";
						}
						else
						{
							objSheet.Cells[i + 2, m + 6] = objItem.OutDateDateString;
						}

						objSheet.Cells[i + 2, m +7] = objItem.KeyWord;

                        objSheet.Cells[i + 2, m + 8] = objItem.UsageId;

                        if (objItem.TypeId == PrjPub.ITEMTYPE_SINGLECHOOSE || objItem.TypeId == PrjPub.ITEMTYPE_JUDGE || objItem.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
						{
							objSheet.Cells[i + 2, m + 9] = intToChar(Convert.ToInt32(objItem.StandardAnswer)).ToString();
							objSheet.get_Range(objSheet.Cells[i + 2, m + 9], objSheet.Cells[i + 2, m + 9]).Merge(0);
							objSheet.get_Range(objSheet.Cells[i + 2, m + 9], objSheet.Cells[i + 2, m + 9]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
						}
						else if(objItem.TypeId == PrjPub.ITEMTYPE_MULTICHOOSE)
						{
							string strAnswer = "";
							string[] str = objItem.StandardAnswer.Split('|');
							for (int j = 0; j < str.Length; j++)
							{
								strAnswer = strAnswer + intToChar(Convert.ToInt32(str[j])).ToString();
							}

							objSheet.Cells[i + 2, m + 9] = strAnswer;
							objSheet.get_Range(objSheet.Cells[i + 2, m + 9], objSheet.Cells[i + 2, m + 9]).Merge(0);
							objSheet.get_Range(objSheet.Cells[i + 2, m + 9], objSheet.Cells[i + 2, m + 9]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
						}
						else
						{
							objSheet.Cells[i + 2, m + 9] = objItem.StandardAnswer;
						}

                        if (objItem.TypeId == PrjPub.ITEMTYPE_SINGLECHOOSE || objItem.TypeId == PrjPub.ITEMTYPE_MULTICHOOSE || objItem.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
						{
							for(int j=0 ; j< objItem.AnswerCount; j++)
							{
								objSheet.Cells[i + 2, m + 10 + j] = objItem.SelectAnswer.Split('|')[j];
								objSheet.get_Range(objSheet.Cells[i + 2, m + 10 + j], objSheet.Cells[i + 2, m + 10 + j]).Merge(0);
								objSheet.get_Range(objSheet.Cells[i + 2, m + 10 + j], objSheet.Cells[i + 2, m + 10 + j]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
							}
						}
						else if (objItem.TypeId == PrjPub.ITEMTYPE_JUDGE)
						{
							objSheet.Cells[i + 2, m + 10] = "对";
							objSheet.get_Range(objSheet.Cells[i + 2, m + 10], objSheet.Cells[i + 2, m + 10]).Merge(0);
							objSheet.get_Range(objSheet.Cells[i + 2, m + 10], objSheet.Cells[i + 2, m + 10]).HorizontalAlignment = XlHAlign.xlHAlignCenter;

							objSheet.Cells[i + 2, m + 11] = "错";
							objSheet.get_Range(objSheet.Cells[i + 2, m + 11], objSheet.Cells[i + 2, m + 11]).Merge(0);
							objSheet.get_Range(objSheet.Cells[i + 2, m + 11], objSheet.Cells[i + 2, m + 11]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
						}
					}

					System.Threading.Thread.Sleep(10);
					jsBlock = "<script>SetPorgressBar('导出试题','" + ((double)((i+1) * 100) / (double)totalCount).ToString("0.00") + "'); </script>";
					Response.Write(jsBlock);
					Response.Flush();
				}

				objSheet.get_Range(objSheet.Cells[1, 9], objSheet.Cells[totalCount + 1, 9]).WrapText = true;
				for (int i = 0; i < colNum; i++)
				{
					objSheet.get_Range(objSheet.Cells[1, 10 + i], objSheet.Cells[totalCount + 1, 10 + i]).WrapText = true;
				}
				m = 1;

				objSheet.get_Range(objSheet.Cells[1, m + 1], objSheet.Cells[totalCount + 1, m + 1]).WrapText = true;
				objSheet.get_Range(objSheet.Cells[1, m + 2], objSheet.Cells[totalCount + 1, m + 2]).WrapText = true;
				objSheet.get_Range(objSheet.Cells[1, m + 3], objSheet.Cells[totalCount + 1, m + 3]).WrapText = true;


				objApp.Visible = false;

				objbook.Saved = true;
				objbook.SaveCopyAs(filename);

				// 处理完成
				jsBlock = "<script>SetCompleted('处理完成。'); </script>";
				Response.Write(jsBlock);
				Response.Flush();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				objbook.Close(Type.Missing, filename, Type.Missing);
				objbooks.Close();
				objApp.Application.Workbooks.Close();
				objApp.Application.Quit();
				objApp.Quit();
				GC.Collect();
			}

            Response.Write("<script>top.returnValue='" + strName + "';window.close();</script>");
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
			}
			return strReturn;
		}

		private char intToChar(int intCol)
		{
			return Convert.ToChar('A' + intCol);
		}
	}
}
