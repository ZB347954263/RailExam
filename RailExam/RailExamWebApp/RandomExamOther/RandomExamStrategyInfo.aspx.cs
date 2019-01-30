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
using RailExam.BLL;
using RailExam.Model;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamOther
{
	public partial class RandomExamStrategyInfo : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (PrjPub.CurrentLoginUser == null)
            {
                Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                return;
            }
			if (!IsPostBack && !subjectCallback.IsCallback)
			{
				ViewState["mode"] = Request.QueryString.Get("mode");
				ViewState["startmode"] = Request.QueryString.Get("startmode");
				if (ViewState["mode"].ToString() == "ReadOnly")
				{
					// btnSave.Visible = false;
					btnCancel.Visible = true;
				}

				string strId = Request.QueryString.Get("id");
				RandomExamSubjectBLL paperStrategySubjectBLL = new RandomExamSubjectBLL();
				IList<RandomExamSubject> paperStrategySubjects = paperStrategySubjectBLL.GetRandomExamSubjectByRandomExamId(int.Parse(strId));

				if (paperStrategySubjects != null)
				{
					RandomExamStrategyBLL objBll = new RandomExamStrategyBLL();
					for (int i = 0; i < paperStrategySubjects.Count; i++)
					{
						if(i==0)
						{
							lblSubject.Text = paperStrategySubjects[i].SubjectName + "：" + paperStrategySubjects[i].ItemCount + "题";
						}
						else
						{
							lblSubject.Text = lblSubject.Text  + "    " + paperStrategySubjects[i].SubjectName + "：" + paperStrategySubjects[i].ItemCount + "题";
						}

						IList<RandomExamStrategy> objList = objBll.GetRandomExamStrategys(paperStrategySubjects[i].RandomExamSubjectId);
						int nowCount = 0;
						foreach (RandomExamStrategy strategy in objList)
						{
							nowCount += strategy.ItemCount;
						}

						if (lblSubjectNow.Text == "")
						{
							lblSubjectNow.Text = paperStrategySubjects[i].TypeName + "：" + nowCount + "题";
						}
						else
						{
							lblSubjectNow.Text = lblSubjectNow.Text + "    " + paperStrategySubjects[i].TypeName + "：" + nowCount + "题";
						}
					}
				}

				BindBookTree();
			}
		}

		private void BindBookTree()
		{
			string strId = Request.QueryString.Get("id");
			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam objRandomExam = objBll.GetExam(Convert.ToInt32(strId));


			BookBLL bookBLL = new BookBLL();
			IList<RailExam.Model.Book> bookList = null;

			string strPostID = objRandomExam.PostID;
			string[] str = strPostID.Split(',');
			int orgID = PrjPub.CurrentLoginUser.OrgID;
			int leader = objRandomExam.IsGroupLeader;
			int techID = objRandomExam.TechnicianTypeID;

			PostBLL objPostBll = new PostBLL();
			Hashtable htBook = new Hashtable();
			for (int i = 0; i < str.Length; i++)
			{
				int postID = Convert.ToInt32(str[i]);
				IList<Post> objPostList = objPostBll.GetPostsByParentID(postID);
				if(objPostList.Count > 0)
				{
					continue;
				}

				bookList = bookBLL.GetEmployeeStudyBookInfoByKnowledgeID(-1, orgID, postID, leader, techID, 0);

				if (bookList.Count > 0)
				{
					TreeViewNode tvn = null;

					foreach (RailExam.Model.Book book in bookList)
					{
						if (!htBook.ContainsKey(book.bookId))
						{

							tvn = new TreeViewNode();
							tvn.ID = book.bookId.ToString();
							tvn.Value = book.bookId.ToString();
							//int n = objItemBll.GetItemsByBookID(book.bookId, Convert.ToInt32(strItemTypeID));
							tvn.Text = book.bookName;
							tvn.ToolTip = book.bookName;
							tvn.Attributes.Add("isBook", "true");
							tvn.ImageUrl = "/RailExamBao/App_Themes/" + StyleSheetTheme + "/Images/TreeView/Book.gif";
							tvn.ContentCallbackUrl = "/RailExamBao/Common/GetBookChapter.aspx?itemTypeID=1&item=no&id=" +
							                         book.bookId;

							tvBookChapter.Nodes.Add(tvn);

							htBook.Add(book.bookId,book.bookName);
						}
					}
				}
			}
		}

		protected void btnLast_Click(object sender, ImageClickEventArgs e)
		{
			string strId = Request.QueryString.Get("id");
			string strStartMode = ViewState["startmode"].ToString();
			string strFlag = "";

			if (ViewState["mode"].ToString() == "Insert")
			{
				strFlag = "Edit";
			}
			else
			{
				strFlag = ViewState["mode"].ToString();
			}
			Response.Redirect("RandomExamSubjectInfo.aspx?startmode=" + strStartMode + "&mode=" + strFlag + "&id=" + strId);
		}

		protected void btnSave_Click(object sender, ImageClickEventArgs e)
		{
			string strId = Request.QueryString.Get("id");
			string strMode = ViewState["mode"].ToString();
			string strStartMode = ViewState["startmode"].ToString();

			if (ViewState["mode"].ToString() == "ReadOnly")
			{
				if (strStartMode == "Edit")
				{
					//Response.Redirect("/RailExamBao/RandomExamOther/RandomExamStudent.aspx?startmode=Edit&mode=Edit&id=" + strId);
					Response.Redirect("/RailExamBao/RandomExam/SelectEmployeeDetailNew.aspx?startmode=Edit&mode=Edit&id=" + strId);
					return;
				}
				else
				{
					Response.Redirect("/RailExamBao/RandomExam/SelectEmployeeDetailNew.aspx?startmode=" + strStartMode + "&mode=" + strMode + "&id=" + strId);
					return;
				}
			}

			RandomExamStrategyBLL psbcBll = new RandomExamStrategyBLL();

			int Ncount = psbcBll.GetRandomExamStrategysByExamID(int.Parse(strId)).Count;

			if (Ncount == 0)
			{
				SessionSet.PageMessage = "请添加策略！";
				return;
			}

			RandomExamSubjectBLL subjectBLL = new RandomExamSubjectBLL();
			RandomExamStrategyBLL strategyBLL = new RandomExamStrategyBLL();
			ItemBLL itemBLL = new ItemBLL();
			RandomExamItemBLL randomItemBLL = new RandomExamItemBLL();

			IList<RandomExamSubject> randomExamSubjects = subjectBLL.GetRandomExamSubjectByRandomExamId(int.Parse(strId));
			int ExamItemCounts = 0;
			for (int i = 0; i < randomExamSubjects.Count; i++)
			{
				int nSubjectId = randomExamSubjects[i].RandomExamSubjectId;
				decimal nTotalItemCount = randomExamSubjects[i].ItemCount;

				IList<RandomExamStrategy> strategys = strategyBLL.GetRandomExamStrategys(nSubjectId);
				int nItemCount = 0;
				for (int j = 0; j < strategys.Count; j++)
				{
					nItemCount += strategys[j].ItemCount;
				}

				ExamItemCounts += nItemCount;
				if (nItemCount != nTotalItemCount)
				{
					SessionSet.PageMessage = "大题设定的试题数和取题范围设定的总题数不相等，请重新设置！";
					return;
				}
			}

			if (ExamItemCounts == 0)
			{
				SessionSet.PageMessage = "考试的总题数不能为0，请重新设置！";
				return;
			}

			//获取考试信息
			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(strId));
			int year = obj.BeginTime.Year;
			//删除可能的取题范围
			randomItemBLL.DeleteItems(int.Parse(strId), year);

			Hashtable htItemID = new Hashtable();
			Hashtable htChapter = new Hashtable();
			for (int i = 0; i < randomExamSubjects.Count; i++)
			{
				IList<RailExam.Model.Item> itemList = new List<RailExam.Model.Item>();
				int nSubjectId = randomExamSubjects[i].RandomExamSubjectId;
				decimal nUnitScore = randomExamSubjects[i].UnitScore;

				IList<RandomExamStrategy> strategys = strategyBLL.GetRandomExamStrategys(nSubjectId);
				for (int k = 0; k < strategys.Count; k++)
				{
					//策略 
					int nChapterId = strategys[k].RangeId;
					int nRangeType = strategys[k].RangeType;
					int typeId = strategys[k].ItemTypeId;
					int StrategyId = strategys[k].RandomExamStrategyId;
					int strDiffId = strategys[k].ItemDifficultyID;
				    int strMaxDiffId = strategys[k].MaxItemDifficultyID;
					string excludesChapterID = strategys[k].ExcludeChapterId;

					IList<RailExam.Model.Item> itemListTemp = new List<RailExam.Model.Item>();
					itemListTemp = itemBLL.GetItemsByStrategyNew(nRangeType, strDiffId,strMaxDiffId, nChapterId, typeId,excludesChapterID);

					if (itemListTemp.Count < strategys[k].ItemCount)
					{
						SessionSet.PageMessage = "大题" + (i + 1).ToString() + "在设定的取题范围内的试题量不够，请重新设置取题范围！";
						return;
					}

					for (int m = 0; m < itemListTemp.Count; m++)
					{
						itemListTemp[m].StrategyId = StrategyId;
						if (itemListTemp[m].StatusId == 1)
						{
							if (htChapter.ContainsKey(itemListTemp[m].ChapterId))
							{
								ArrayList objList = (ArrayList)htChapter[itemListTemp[m].ChapterId];

								if (objList.IndexOf(itemListTemp[m].KeyWord) < 0)
								{
									itemList.Add(itemListTemp[m]);
									if (itemListTemp[m].KeyWord != "" && itemListTemp[m].KeyWord != null)
									{
										objList.Add(itemListTemp[m].KeyWord);
									}
								}
							}
							else
							{
								ArrayList objList = new ArrayList();
								if (itemListTemp[m].KeyWord != "" && itemListTemp[m].KeyWord != null)
								{
									objList.Add(itemListTemp[m].KeyWord);
								}

								itemList.Add(itemListTemp[m]);

								htChapter.Add(itemListTemp[m].ChapterId, objList);
							}
						}
					}
				}

				if (itemList.Count < randomExamSubjects[i].ItemCount)
				{
					SessionSet.PageMessage = "大题" + (i + 1).ToString() + "在设定的取题范围内符合要求的试题量不够，请重新设置取题范围！";
					return;
				}

				IList<RandomExamItem> randomExamItems = new List<RandomExamItem>();

				int n = 0;
				foreach (RailExam.Model.Item item in itemList)
				{
					if (string.IsNullOrEmpty(item.StandardAnswer) && (item.TypeId == PrjPub.ITEMTYPE_SINGLECHOOSE || item.TypeId == PrjPub.ITEMTYPE_MULTICHOOSE || item.TypeId == PrjPub.ITEMTYPE_JUDGE))
					{
						n = n + 1;
						break;
					}
					if (!htItemID.ContainsKey(item.ItemId))
					{
						htItemID.Add(item.ItemId, item.ItemId);
					}
					else
					{
						continue;
					}
					RandomExamItem paperItem = new RandomExamItem();
					paperItem.SubjectId = nSubjectId;
					paperItem.StrategyId = item.StrategyId;
					paperItem.RandomExamId = int.Parse(strId);
					paperItem.AnswerCount = item.AnswerCount;
					paperItem.BookId = item.BookId;
					paperItem.CategoryId = item.CategoryId;
					paperItem.ChapterId = item.ChapterId;
					paperItem.CompleteTime = item.CompleteTime;
					paperItem.Content = item.Content;
					paperItem.CreatePerson = item.CreatePerson;
					paperItem.CreateTime = item.CreateTime;
					paperItem.Description = item.Description;
					paperItem.DifficultyId = item.DifficultyId;
					paperItem.ItemId = item.ItemId;
					paperItem.Memo = item.Memo;
					paperItem.OrganizationId = item.OrganizationId;
					paperItem.OutDateDate = item.OutDateDate;
					paperItem.Score = nUnitScore;
					paperItem.SelectAnswer = item.SelectAnswer;
					paperItem.Source = item.Source;
					paperItem.StandardAnswer = item.StandardAnswer;
					paperItem.StatusId = item.StatusId;
					paperItem.TypeId = item.TypeId;
					paperItem.UsedCount = item.UsedCount;
					paperItem.Version = item.Version;
					randomExamItems.Add(paperItem);
				}

				if (n == 1)
				{
					SessionSet.PageMessage = "大题" + (i + 1).ToString() + "有无标准答案的试题，请重新设置取题范围！";
					return;
				}

				if (randomExamItems.Count > 0)
				{
					randomItemBLL.AddItem(randomExamItems, year);
				}
			}

			/*
			Hashtable hashTableItemIds = new Hashtable();
			for (int i = 0; i < randomExamSubjects.Count; i++)
			{
				int nSubjectId = randomExamSubjects[i].RandomExamSubjectId;
				//int nItemCount = randomExamSubjects[i].ItemCount;

				IList<RandomExamStrategy> strategys = strategyBLL.GetRandomExamStrategys(nSubjectId);
				for (int j = 0; j < strategys.Count; j++)
				{
					int nStrategyId = strategys[j].RandomExamStrategyId;
					int nItemCount = strategys[j].ItemCount;

					IList<RandomExamItem> itemList = randomItemBLL.GetItemsByStrategyId(nStrategyId, year);
					Random ObjRandom = new Random();
					Hashtable hashTable = new Hashtable();
					Hashtable hashTableCount = new Hashtable();
					while (hashTable.Count < nItemCount)
					{
						int k = ObjRandom.Next(itemList.Count);
						hashTableCount[k] = k;
						int itemID = itemList[k].ItemId;
						if (!hashTableItemIds.ContainsKey(itemID))
						{
							hashTable[itemID] = itemID;
							hashTableItemIds[itemID] = itemID;
						}

						if (hashTableCount.Count == itemList.Count && hashTable.Count < nItemCount)
						{
							SessionSet.PageMessage = "随机考试在设定的取题范围内的试题量不够，请重新设置取题范围！";
							return;
						}
					}
				}
			}*/
			Response.Redirect("/RailExamBao/RandomExam/SelectEmployeeDetailNew.aspx?startmode=" + strStartMode + "&mode=" + strMode + "&id=" + strId);
		}

		protected void btnCancel_Click(object sender, ImageClickEventArgs e)
		{
			//Response.Write("<script>top.window.opener.form1.Refresh.value='true';top.window.opener.form1.submit();top.window.close();</script>");
			Response.Write("<script>top.returnValue='true';top.window.close();</script>");
		}

		protected  void callback_callback(object sender,CallBackEventArgs e)
		{
			string strID = e.Parameters[0];
			RandomExamSubjectBLL objSubjectBll = new RandomExamSubjectBLL();
			IList<RandomExamSubject> objSubjectList = objSubjectBll.GetRandomExamSubjectByRandomExamId(Convert.ToInt32(strID));

			lblSubjectNow.Text = "";
			RandomExamStrategyBLL objBll = new RandomExamStrategyBLL();
			foreach (RandomExamSubject subject in objSubjectList)
			{
				IList<RandomExamStrategy> objList = objBll.GetRandomExamStrategys(subject.RandomExamSubjectId);
				int nowCount = 0;
				foreach (RandomExamStrategy strategy in objList)
				{
					nowCount += strategy.ItemCount;
				}

				if (lblSubjectNow.Text == "")
				{
					lblSubjectNow.Text = subject.TypeName + "：" + nowCount + "题";
				}
				else
				{
					lblSubjectNow.Text = lblSubjectNow.Text + "    " + subject.TypeName + "：" + nowCount + "题";
				}
			}

			lblSubjectNow.RenderControl(e.Output);
		}
	}
}
