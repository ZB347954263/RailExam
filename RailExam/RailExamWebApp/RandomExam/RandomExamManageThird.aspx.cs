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

namespace RailExamWebApp.RandomExam
{
    public partial class RandomExamManageThird : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack )
            {
                ViewState["mode"] = Request.QueryString.Get("mode");
                ViewState["startmode"] = Request.QueryString.Get("startmode");
                if (ViewState["mode"].ToString() == "ReadOnly")
                {
                    // btnSave.Visible = false;
                    btnCancel.Visible = true;
                }

                string strId = Request.QueryString.Get("id");
                ViewState["ExamId"] = strId;
                RandomExamSubjectBLL paperStrategySubjectBLL = new RandomExamSubjectBLL();
                IList<RandomExamSubject> paperStrategySubjects = paperStrategySubjectBLL.GetRandomExamSubjectByRandomExamId(int.Parse(strId));

                if (paperStrategySubjects != null)
                {
                    RandomExamStrategyBLL objBll = new RandomExamStrategyBLL();
                    int sumItem = 0;
                    decimal sumScore = 0;
                    for (int i = 0; i < paperStrategySubjects.Count; i++)
                    {
                        int j = i + 1;
                        ListItem Li = new ListItem();
                        Li.Value = paperStrategySubjects[i].ItemTypeId + "|" +paperStrategySubjects[i].RandomExamSubjectId.ToString();
                        Li.Text = "��" + j + "�⣺  " + paperStrategySubjects[i].SubjectName;
                        lbType.Items.Add(Li);

                        if (i == 0)
                        {
                            lblSubject.Text = paperStrategySubjects[i].SubjectName + "��" + paperStrategySubjects[i].ItemCount + "��";
                        }
                        else
                        {
                            lblSubject.Text = lblSubject.Text + "    " + paperStrategySubjects[i].SubjectName + "��" + paperStrategySubjects[i].ItemCount + "��";
                        }

                        sumItem += paperStrategySubjects[i].ItemCount;
                        sumScore += paperStrategySubjects[i].ItemCount*paperStrategySubjects[i].UnitScore;

                        IList<RandomExamStrategy> objList = objBll.GetRandomExamStrategys(paperStrategySubjects[i].RandomExamSubjectId);
                        int nowCount = 0;
                        foreach (RandomExamStrategy strategy in objList)
                        {
                            nowCount += strategy.ItemCount;
                        }

                        if (lblSubjectNow.Text == "")
                        {
                            lblSubjectNow.Text = paperStrategySubjects[i].TypeName + "��" + nowCount + "��";
                        }
                        else
                        {
                            lblSubjectNow.Text = lblSubjectNow.Text + "    " + paperStrategySubjects[i].TypeName + "��" + nowCount + "��";
                        }
                    }
                    lblSubject.Text = lblSubject.Text + "    " + "��" + sumItem + "�⣬��" +  System.String.Format("{0:0.##}", sumScore) + "��";


                    lbType.SelectedIndex = 0;
                    ViewState["value"] = lbType.SelectedValue;
                }
            }

            string strRefresh = Request.Form.Get("Refresh");
            if(!string.IsNullOrEmpty(strRefresh))
            {
                OracleAccess db = new OracleAccess();
                string strSql = "delete from Random_Exam_Item_Select where RANDOM_EXAM_STRATEGY_ID=" + strRefresh;
                db.ExecuteNonQuery(strSql);
            }
        }

        protected void callback_callback(object sender, CallBackEventArgs e)
        {
            string strID = ViewState["ExamId"].ToString();

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
                    lblSubjectNow.Text = subject.TypeName + "��" + nowCount + "��";
                }
                else
                {
                    lblSubjectNow.Text = lblSubjectNow.Text + "    " + subject.TypeName + "��" + nowCount + "��";
                }
            }

            lblSubjectNow.RenderControl(e.Output);
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
            Response.Redirect("RandomExamManageSecond.aspx?startmode="+strStartMode+"&mode=" + strFlag + "&id=" + strId);
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            string strId = Request.QueryString.Get("id");
            string strMode = ViewState["mode"].ToString();
            string strStartMode = ViewState["startmode"].ToString();

            if (ViewState["mode"].ToString() == "ReadOnly")
            {
				//if (strStartMode == "Edit")
				//{
				//    Response.Redirect("SelectEmployeeDetailNew.aspx?startmode=Edit&mode=Edit&id=" + strId);
				//    return;
				//}
				//else
				//{
				//    Response.Redirect("SelectEmployeeDetailNew.aspx?startmode=" + strStartMode + "&mode=" + strMode + "&id=" + strId);
				//    return;
				//}

				Response.Redirect("SelectEmployeeDetailNew.aspx?startmode=" + strStartMode + "&mode=" + strMode + "&id=" + strId);
				return;
			}

            if (Pub.HasPaper(Convert.ToInt32(strId)))
            {
                Response.Write("<script>alert('�ÿ����������Ծ����ܱ��༭��');window.close();</script>");
                return;
            }

            RandomExamStrategyBLL psbcBll = new RandomExamStrategyBLL();

            int Ncount = psbcBll.GetRandomExamStrategysByExamID(int.Parse(strId)).Count;

            if (Ncount == 0)
            {
                SessionSet.PageMessage = "����Ӳ��ԣ�";
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
                for (int j = 0; j < strategys.Count;j++ )
                {
                    nItemCount += strategys[j].ItemCount;  //�Ķ�2011-10-17                 
                }

                ExamItemCounts += nItemCount;
                if (nItemCount != nTotalItemCount)
                {
                    SessionSet.PageMessage = "�����趨����������ȡ�ⷶΧ�趨������������ȣ����������ã�";
                    return;
                }
            }

            if (ExamItemCounts==0)
            {
                SessionSet.PageMessage = "���Ե�����������Ϊ0�����������ã�";
                return;
            }

			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(strId));
			int year = obj.BeginTime.Year;
            randomItemBLL.DeleteItems(int.Parse(strId),year);

			Hashtable htItemID = new Hashtable();
        	int count = 0;
            for (int i = 0; i < randomExamSubjects.Count; i++)
            {
                IList<RailExam.Model.Item> itemList = new List<RailExam.Model.Item>();
                int nSubjectId = randomExamSubjects[i].RandomExamSubjectId;
                decimal nUnitScore = randomExamSubjects[i].UnitScore;

                IList<RandomExamStrategy> strategys = strategyBLL.GetRandomExamStrategys(nSubjectId);
                for (int k = 0; k < strategys.Count; k++)
                {
                    //���� 
                    int nChapterId = strategys[k].RangeId;
                    int nRangeType = strategys[k].RangeType;
                    int typeId = strategys[k].ItemTypeId;
                    int StrategyId = strategys[k].RandomExamStrategyId;
                    string strExcludeIDs = strategys[k].ExcludeChapterId;
                    int ndr = 0;
                    IList<RailExam.Model.Item> itemListTemp = new List<RailExam.Model.Item>();
                    itemListTemp = itemBLL.GetItemsByStrategy(nRangeType, ndr, nChapterId, typeId, strExcludeIDs,StrategyId);

                    if (itemListTemp.Count < strategys[k].ItemCount)
                    {
                        SessionSet.PageMessage = "����" + (i + 1).ToString() + "���趨��ȡ�ⷶΧ�ڵ�����������������������ȡ�ⷶΧ��";
                        return;
                    }

					Hashtable htChapter = new Hashtable();
                    for (int m = 0; m < itemListTemp.Count; m++)
                    {
                        itemListTemp[m].StrategyId = StrategyId;
                        if(itemListTemp[m].StatusId == 1 )
                        {
							if(htChapter.ContainsKey(itemListTemp[m].ChapterId))
							{
								ArrayList objList = (ArrayList) htChapter[itemListTemp[m].ChapterId];

                                if (objList.IndexOf(itemListTemp[m].MotherCode) < 0)
								{
									itemList.Add(itemListTemp[m]);
                                    if (itemListTemp[m].MotherCode != "" && itemListTemp[m].MotherCode != null)
									{
										objList.Add(itemListTemp[m].MotherCode);
									}
								}
							}
							else
							{
								ArrayList objList = new ArrayList();
                                if (itemListTemp[m].MotherCode != "" && itemListTemp[m].MotherCode != null)
								{
									objList.Add(itemListTemp[m].MotherCode);
								}

								itemList.Add(itemListTemp[m]);

								htChapter.Add(itemListTemp[m].ChapterId,objList);
							}
                        }
                    }
                }

                if (itemList.Count < randomExamSubjects[i].ItemCount)
                {
					SessionSet.PageMessage = "����" + (i + 1).ToString() + "���趨��ȡ�ⷶΧ�ڵ�����������������������ȡ�ⷶΧ��";
					return;
                }

                IList<RandomExamItem> randomExamItems = new List<RandomExamItem>();

            	int n = 0;
                foreach (RailExam.Model.Item item in itemList)
                {
					if(string.IsNullOrEmpty(item.StandardAnswer) && (item.TypeId == PrjPub.ITEMTYPE_SINGLECHOOSE || item.TypeId == PrjPub.ITEMTYPE_MULTICHOOSE || item.TypeId == PrjPub.ITEMTYPE_JUDGE))
					{
						n = n + 1;
						break;
					}
					if(!htItemID.ContainsKey(item.ItemId))
					{
						htItemID.Add(item.ItemId,item.ItemId);
					}
					else
					{
						count = count + 1;
						break;
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
                    paperItem.ParentItemID = 0;
                    paperItem.MotherCode = item.MotherCode;
                    paperItem.ItemIndex = item.ItemIndex;
                    randomExamItems.Add(paperItem);

                    //�������
                    if(item.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                    {
                        IList<RailExam.Model.Item> itemDetails = itemBLL.GetItemsByParentItemID(item.ItemId);

                        foreach (RailExam.Model.Item itemDetail in itemDetails)
                        {
                           paperItem = new RandomExamItem();
                            paperItem.SubjectId = nSubjectId;
                            paperItem.StrategyId = itemDetail.StrategyId;
                            paperItem.RandomExamId = int.Parse(strId);
                            paperItem.AnswerCount = itemDetail.AnswerCount;
                            paperItem.BookId = itemDetail.BookId;
                            paperItem.CategoryId = itemDetail.CategoryId;
                            paperItem.ChapterId = itemDetail.ChapterId;
                            paperItem.CompleteTime = itemDetail.CompleteTime;
                            paperItem.Content = itemDetail.Content;
                            paperItem.CreatePerson = itemDetail.CreatePerson;
                            paperItem.CreateTime = itemDetail.CreateTime;
                            paperItem.Description = itemDetail.Description;
                            paperItem.DifficultyId = itemDetail.DifficultyId;
                            paperItem.ItemId = itemDetail.ItemId;
                            paperItem.Memo = itemDetail.Memo;
                            paperItem.OrganizationId = itemDetail.OrganizationId;
                            paperItem.OutDateDate = itemDetail.OutDateDate;
                            paperItem.Score = Math.Round(nUnitScore/itemDetails.Count,2);
                            paperItem.SelectAnswer = itemDetail.SelectAnswer;
                            paperItem.Source = itemDetail.Source;
                            paperItem.StandardAnswer = itemDetail.StandardAnswer;
                            paperItem.StatusId = itemDetail.StatusId;
                            paperItem.TypeId = itemDetail.TypeId;
                            paperItem.UsedCount = itemDetail.UsedCount;
                            paperItem.Version = itemDetail.Version;
                            paperItem.ParentItemID = item.ItemId;
                            paperItem.MotherCode = itemDetail.MotherCode;
                            paperItem.ItemIndex = itemDetail.ItemIndex;
                            randomExamItems.Add(paperItem);
                        }
                    }

                }

				if (count == 1)
				{
					SessionSet.PageMessage = "�����Բ����ظ�������������ȡ�ⷶΧ��";
					return;
				}

				if(n == 1)
				{
					SessionSet.PageMessage = "����" + (i + 1).ToString() + "���ޱ�׼�𰸵����⣬����������ȡ�ⷶΧ��";
					return;
				}

                if (randomExamItems.Count > 0)
                {
                    randomItemBLL.AddItem(randomExamItems,year);
                }
            }

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

                    IList<RandomExamItem> itemList = randomItemBLL.GetItemsByStrategyId(nStrategyId,year);
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
                            SessionSet.PageMessage = "����������趨��ȡ�ⷶΧ�ڵ�����������������������ȡ�ⷶΧ��";
                            return;
                        }
                    }
                }
            }

			objBll.UpdateVersion(Convert.ToInt32(strId));

            if(obj.IsComputerExam)
            {
                Response.Redirect("SelectEmployeeDetailNew.aspx?startmode=" + strStartMode + "&mode=" + strMode + "&id=" + strId);
            }
            else
            {
                Response.Write("<script>top.window.opener.form1.Refresh.value='true';top.window.opener.form1.submit();top.window.close();</script>");
            }
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            Response.Write("<script>top.window.opener.form1.Refresh.value='true';top.window.opener.form1.submit();top.window.close();</script>");
        }
    }
}
