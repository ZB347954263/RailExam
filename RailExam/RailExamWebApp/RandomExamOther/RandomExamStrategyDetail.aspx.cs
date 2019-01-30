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

namespace RailExamWebApp.RandomExamOther
{
	public partial class RandomExamStrategyDetail : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				ViewState["ExamID"] = Request.QueryString.Get("id");
				BindGrid();
			}
		}

        private DataTable GetDataTable()
        {
            string strBookID = Request.QueryString.Get("bookChapterID");
            string type = Request.QueryString.Get("type");
            string rangeType;
            if (type == "book")
            {
                rangeType = "3";
            }
            else
            {
                rangeType = "4";
            }

            RandomExamStrategyBLL objBll = new RandomExamStrategyBLL();
            IList<RandomExamStrategy> objList = objBll.GetRandomExamStrategy(Convert.ToInt32(ViewState["ExamID"].ToString()),
                                                                             Convert.ToInt32(rangeType), Convert.ToInt32(strBookID));

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("RandomExamStrategyId", typeof(int)));
            dt.Columns.Add(new DataColumn("RangeType", typeof(int)));
            dt.Columns.Add(new DataColumn("SubjectId", typeof(int)));
            dt.Columns.Add(new DataColumn("RangeId", typeof(int)));
            dt.Columns.Add(new DataColumn("ItemTypeId", typeof(int)));
            dt.Columns.Add(new DataColumn("SubjectName", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemTypeName", typeof(string)));
            dt.Columns.Add(new DataColumn("MaxCount", typeof(int)));
            dt.Columns.Add(new DataColumn("ItemCount", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemDifficultyID", typeof(string)));
            dt.Columns.Add(new DataColumn("MaxItemDifficultyID", typeof(string)));

            ArrayList objTypeList = new ArrayList();
            foreach (RandomExamStrategy strategy in objList)
            {
                objTypeList.Add(strategy.ItemTypeId);
            }

            RandomExamSubjectBLL objSubjectBll = new RandomExamSubjectBLL();
            IList<RandomExamSubject> objSubjectList =
                objSubjectBll.GetRandomExamSubjectByRandomExamId(Convert.ToInt32(ViewState["ExamID"]));

            ItemBLL objItemBll = new ItemBLL();
            BookChapterBLL objBookChapterBll = new BookChapterBLL();
            foreach (RandomExamSubject subject in objSubjectList)
            {
                //对数据库中没有添加组卷策略的题型在内存添加初始组卷策略
                if (objTypeList.IndexOf(subject.ItemTypeId) < 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["RandomExamStrategyId"] = 0;
                    dr["RangeType"] = Convert.ToInt32(rangeType);
                    dr["SubjectId"] = subject.RandomExamSubjectId;
                    dr["RangeId"] = Convert.ToInt32(strBookID);
                    dr["ItemTypeId"] = subject.ItemTypeId;
                    dr["ItemTypeName"] = subject.TypeName;
                    dr["SubjectName"] = subject.SubjectName;
                    dr["ItemCount"] = string.Empty;
                    dr["ItemDifficultyID"] = string.Empty;
                    dr["MaxItemDifficultyID"] = string.Empty;
                    if (type == "book")
                    {
                        dr["MaxCount"] = objItemBll.GetItemsByBookID(Convert.ToInt32(strBookID), subject.ItemTypeId);
                    }
                    else
                    {
                        string idPath = objBookChapterBll.GetBookChapter(Convert.ToInt32(strBookID)).IdPath;
                        dr["MaxCount"] = objItemBll.GetItemsByBookChapterIdPath(idPath, subject.ItemTypeId);
                    }
                    dt.Rows.Add(dr);
                }
                else
                {
                    IList<RandomExamStrategy> strategys = objBll.GetRandomExamStrategyBySubjectIDAndRangeID(subject.RandomExamSubjectId, Convert.ToInt32(strBookID),Convert.ToInt32(rangeType));
                    foreach (RandomExamStrategy strategy in strategys)
                    {
                        DataRow dr = dt.NewRow();
                        dr["RandomExamStrategyId"] = strategy.RandomExamStrategyId;
                        dr["RangeType"] = Convert.ToInt32(rangeType);
                        dr["SubjectId"] = strategy.SubjectId;
                        dr["RangeId"] = Convert.ToInt32(strBookID);
                        dr["ItemTypeId"] = strategy.ItemTypeId;
                        dr["ItemTypeName"] = strategy.ItemTypeName;
                        dr["SubjectName"] = strategy.SubjectName;
                        dr["ItemCount"] = strategy.ItemCount;
                        dr["ItemDifficultyID"] = strategy.ItemDifficultyID;
                        dr["MaxItemDifficultyID"] = strategy.MaxItemDifficultyID;
                        if (type == "book")
                        {
                            dr["MaxCount"] = objItemBll.GetItemsByBookID(Convert.ToInt32(strBookID), strategy.ItemTypeId, strategy.ItemDifficultyID, strategy.MaxItemDifficultyID);
                        }
                        else
                        {
                            string idPath = objBookChapterBll.GetBookChapter(Convert.ToInt32(strBookID)).IdPath;
                            dr["MaxCount"] = objItemBll.GetItemsByBookChapterIdPath(idPath, strategy.ItemTypeId, strategy.ItemDifficultyID, strategy.MaxItemDifficultyID);
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }

		private void BindGrid()
		{
			Grid1.DataSource = GetDataTable();
			Grid1.DataBind();

			if(Request.QueryString.Get("mode") == "ReadOnly")
			{
				Grid1.Columns[5].Visible = false;
			    btnAdd.Visible = false;
			}
		}

		protected void Grid1_RowEditing(object sender, GridViewEditEventArgs e)
		{
			if (Grid1.EditIndex != -1)
			{
				SessionSet.PageMessage = "请先保存正在编辑的项！";
				return;
			}

			string maxCount = ((Label)Grid1.Rows[e.NewEditIndex].FindControl("lblMaxCount")).Text;
			if(maxCount == "0")
			{
				SessionSet.PageMessage = "该教材章节当前题型的最大题数为0，无需设置组卷策略！";
				return;
			}

			Grid1.EditIndex = e.NewEditIndex;
			BindGrid();

            ((Label)Grid1.Rows[Grid1.EditIndex].FindControl("lblEditTypeName")).Visible = true;
            ((DropDownList)Grid1.Rows[Grid1.EditIndex].FindControl("ddlItemType")).Visible = false;
            if(((HiddenField)Grid1.Rows[Grid1.EditIndex].FindControl("hfMaxItemDiff")).Value == string.Empty)
            {
                ((DropDownList) Grid1.Rows[Grid1.EditIndex].FindControl("ddlMaxItemDiff")).SelectedValue = "5";
            }
		}

		protected void Grid1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			Grid1.EditIndex = -1;
			BindGrid();
		}

		protected void Grid1_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			string maxCount = ((Label)Grid1.Rows[e.RowIndex].FindControl("lblMaxCount")).Text;
			string itemCount = ((TextBox) Grid1.Rows[e.RowIndex].FindControl("txtItemCount")).Text;
			string itemDiff = ((DropDownList)Grid1.Rows[e.RowIndex].FindControl("ddlItemDiff")).SelectedValue;
            string maxItemDiff = ((DropDownList)Grid1.Rows[e.RowIndex].FindControl("ddlMaxItemDiff")).SelectedValue;
			string strID = ((HiddenField)Grid1.Rows[e.RowIndex].FindControl("hfEditID")).Value;
            string itemTypeId = ((HiddenField)Grid1.Rows[e.RowIndex].FindControl("hfEditItemTypeId")).Value;
			string subjectId = ((HiddenField)Grid1.Rows[e.RowIndex].FindControl("hfSubjectId")).Value;

            if(itemCount == "")
            {
                SessionSet.PageMessage = "请设置试题数量！";
                return;
            }

            if (Convert.ToInt32(itemCount) == 0)
            {
                string errorMessage = "";
                foreach (GridViewRow row in Grid1.Rows)
                {
                    if (Grid1.EditIndex != row.RowIndex)
                    {
                        if (((HiddenField)row.FindControl("hfItemTypeId")).Value == itemTypeId)
                        {
                            errorMessage = "当前章节有多个" + ((Label)Grid1.Rows[e.RowIndex].FindControl("lblEditTypeName")).Text + "题的组卷策略，不能将取题数设置为0！";
                        }
                    }
                }

                if (errorMessage != "")
                {
                    SessionSet.PageMessage = errorMessage;
                    return;
                }

                itemDiff = "1";
                maxItemDiff = "5";
            }

            //if (itemDiff == "" || maxItemDiff == "")
            //{
            //    SessionSet.PageMessage = "请选择试题难度！";
            //    return;
            //}

            if (Convert.ToInt32(itemDiff) > Convert.ToInt32(maxItemDiff))
            {
                SessionSet.PageMessage = "最小难度不能大于最大难度！";
                return;
            }

			if(Convert.ToInt32(maxCount) < Convert.ToInt32(itemCount))
			{
				SessionSet.PageMessage = "设置题数不能超过所选教材章节最大题数！";
				return;
			}

			RandomExamStrategyBLL objBll = new RandomExamStrategyBLL();
			string strBookID = Request.QueryString.Get("bookChapterID");
			string type = Request.QueryString.Get("type");

			BookChapterBLL objChapterBll = new BookChapterBLL();
			if (type == "book")
			{
				if(Convert.ToInt32(itemCount) == 0)
				{
					SessionSet.PageMessage = "为教材设置取题范围时，题数必须大于0！";
					return;
				}
			}
			else
			{
				
				BookChapter objChapter = objChapterBll.GetBookChapter(Convert.ToInt32(strBookID));
				if(objChapter.ParentId != 0)
				{
					IList<RandomExamStrategy> objStrategyList = objBll.GetRandomExamStrategyBySubjectIDAndRangeID(Convert.ToInt32(subjectId), objChapter.ParentId,4);
					if(objStrategyList.Count > 0)
					{
						if(objStrategyList[0].ItemCount == 0)
						{
							SessionSet.PageMessage = "该章节上级章节处于屏蔽中，不能修改本章节取题范围！";
							return;
						}
					}
				}
			}

			RandomExamSubjectBLL objSubjectBll = new RandomExamSubjectBLL();
			RandomExamSubject objSubject = objSubjectBll.GetRandomExamSubject(Convert.ToInt32(subjectId));

			IList<RandomExamStrategy> objList = objBll.GetRandomExamStrategys(Convert.ToInt32(subjectId));
			int nowCount = 0;
			foreach (RandomExamStrategy strategy in objList)
			{
				nowCount += strategy.ItemCount;	
			}

			if (Convert.ToInt32(itemCount) > 0)
			{
                if (type == "book")
                {
                    IList<BookChapter> objChapterList = objChapterBll.GetBookChapterByBookID(Convert.ToInt32(strBookID));
                    int errorNum = 0;
                    foreach (BookChapter chapter in objChapterList)
                    {
                        if (chapter.ParentId == 0)
                        {
                            IList<RandomExamStrategy> objRandomExamStrategyList =
                            objBll.GetRandomExamStrategyBySubjectIDAndRangeID(Convert.ToInt32(subjectId), chapter.ChapterId, 4);
                            if (objRandomExamStrategyList.Count > 0)
                            {
                                foreach (RandomExamStrategy strategy in objRandomExamStrategyList)
                                {
                                    if (strategy.ItemCount > 0)
                                    {
                                        errorNum++;
                                        break;
                                    }
                                }
                            }

                            if (errorNum > 0)
                            {
                                break;
                            }
                        }
                    }

                    if (errorNum > 0)
                    {
                        SessionSet.PageMessage = "该章节下级章节已经存在组卷策略，不能重复录入！";
                        return;
                    }
                }
                else
                {
                    BookChapter objChapter = objChapterBll.GetBookChapter(Convert.ToInt32(strBookID));
                    int parentID = objChapter.ParentId;
                    if (parentID != 0)
                    {
                        IList<RandomExamStrategy> objRandomExamStrategyList =
                            objBll.GetRandomExamStrategyBySubjectIDAndRangeID(Convert.ToInt32(subjectId), parentID, 4);
                        if (objRandomExamStrategyList.Count > 0)
                        {
                            foreach (RandomExamStrategy strategy in objRandomExamStrategyList)
                            {
                                if (strategy.ItemCount > 0)
                                {
                                    SessionSet.PageMessage = "该章节上级章节已经存在组卷策略，不能重复录入！";
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        IList<RandomExamStrategy> objRandomExamStrategyList =
                            objBll.GetRandomExamStrategyBySubjectIDAndRangeID(Convert.ToInt32(subjectId),
                                                                              objChapter.BookId, 3);
                        if (objRandomExamStrategyList.Count > 0)
                        {
                            foreach (RandomExamStrategy strategy in objRandomExamStrategyList)
                            {
                                if (strategy.ItemCount > 0)
                                {
                                    SessionSet.PageMessage = "该章节上级章节已经存在组卷策略，不能重复录入！";
                                    return;
                                }
                            }
                        }
                    }

                    IList<BookChapter> objChapterList =
                        objChapterBll.GetBookChapterByParentID(Convert.ToInt32(strBookID));
                    int errorNum = 0;
                    foreach (BookChapter chapter in objChapterList)
                    {
                        IList<RandomExamStrategy> objRandomExamStrategyList =
                            objBll.GetRandomExamStrategyBySubjectIDAndRangeID(Convert.ToInt32(subjectId),
                                                                              chapter.ChapterId, 4);
                        if (objRandomExamStrategyList.Count > 0)
                        {
                            foreach (RandomExamStrategy strategy in objRandomExamStrategyList)
                            {
                                if (strategy.ItemCount > 0)
                                {
                                    errorNum++;
                                    break;
                                }
                            }
                        }

                        if (errorNum > 0)
                        {
                            break;
                        }
                    }

                    if (errorNum > 0)
                    {
                        SessionSet.PageMessage = "该章节下级章节已经存在组卷策略，不能重复录入！";
                        return;
                    }
                }
			}

            //判断同一题型难度是否重复
            IList<RandomExamStrategy> objNowList =
                objBll.GetRandomExamStrategyBySubjectIDAndRangeID(Convert.ToInt32(subjectId), Convert.ToInt32(strBookID), (type=="book"?3:4));
            Hashtable htDiff = new Hashtable();
            bool isDiff = false;
            for (int i = Convert.ToInt32(itemDiff); i <= Convert.ToInt32(maxItemDiff); i++)
            {
                htDiff.Add(i, i);
            }
		    foreach (RandomExamStrategy randomExamStrategy in objNowList)
            {
                if(randomExamStrategy.RandomExamStrategyId.ToString() != strID)
                {
                    for (int i = randomExamStrategy.ItemDifficultyID; i <= randomExamStrategy.MaxItemDifficultyID; i++)
                    {
                        if (htDiff.ContainsKey(i))
                        {
                            isDiff = true;
                            break;
                        }
                        else
                        {
                            htDiff.Add(i, i);
                        }
                    }
                }
            }

            if (isDiff)
            {
                SessionSet.PageMessage = "该章节组卷策略同一题型难度重复，请重新选择难度！";
                return;
            }

			if(strID == "0")
			{
				if(nowCount + Convert.ToInt32(itemCount) > objSubject.ItemCount)
				{
					SessionSet.PageMessage = "设置题数已超过大题设置题数！";
					return;
				}
				string rangeType;
				if (type == "book")
				{
					rangeType = "3";
				}
				else
				{
					rangeType = "4";
				}

				RandomExamStrategy obj = new RandomExamStrategy();
				obj.SubjectId = Convert.ToInt32(subjectId);
				obj.RangeType = Convert.ToInt32(rangeType);
				obj.RangeId = Convert.ToInt32(strBookID);
				obj.ItemTypeId = Convert.ToInt32(itemTypeId);
				obj.ItemCount = Convert.ToInt32(itemCount);
				obj.ItemDifficultyID = Convert.ToInt32(itemDiff);
			    obj.MaxItemDifficultyID = Convert.ToInt32(maxItemDiff);
			    obj.Memo = "哈尔滨";
				if (Convert.ToInt32(itemCount) == 0)
				{
					string strExcludeChapter = "";
					BookChapter objBookChapter = objChapterBll.GetBookChapter(Convert.ToInt32(strBookID));
					IList<BookChapter> objExcludeChapterList = objChapterBll.GetBookChapterByIDPath(objBookChapter.IdPath);
					foreach (BookChapter bookChapter in objExcludeChapterList)
					{
						if (strExcludeChapter == "")
						{
							strExcludeChapter = bookChapter.ChapterId.ToString();
						}
						else
						{
							strExcludeChapter = strExcludeChapter + "," + bookChapter.ChapterId;
						}
					}
					obj.ExcludeChapterId = strExcludeChapter;
				}
				else
				{
					string strExcludeChapter = "";
					if (type == "book")
					{
						IList<RandomExamStrategy> objStrategyLists = objBll.GetRandomExamStrategys(Convert.ToInt32(subjectId));

						foreach (RandomExamStrategy objStrategy in objStrategyLists)
						{
							BookChapter chapter = objChapterBll.GetBookChapter(objStrategy.RangeId);

							if (objStrategy.ItemCount == 0 && objStrategy.RangeType == 4 && chapter.BookId == Convert.ToInt32(strBookID))
							{
								if (strExcludeChapter == "")
								{
									strExcludeChapter = objStrategy.RangeId.ToString();
								}
								else
								{
									strExcludeChapter = strExcludeChapter + "," + objStrategy.RangeId;
								}
							}
						}
					}
					else
					{
						BookChapter objParentChapter = objChapterBll.GetBookChapter(Convert.ToInt32(strBookID));
						IList<BookChapter> bookChapters = objChapterBll.GetBookChapterByIDPath(objParentChapter.IdPath);
						string strChapterIds = "";
						foreach (BookChapter bookChapter in bookChapters)
						{
							if (strChapterIds == "")
							{
								strChapterIds = bookChapter.ChapterId.ToString();
							}
							else
							{
								strChapterIds = strChapterIds + "," + bookChapter.ChapterId;
							}
						}

						IList<RandomExamStrategy> objStrategyLists = objBll.GetRandomExamStrategys(Convert.ToInt32(subjectId));

						foreach (RandomExamStrategy objStrategy in objStrategyLists)
						{
							if (objStrategy.ItemCount == 0 && objStrategy.RangeType == 4 && ("," + bookChapters + ",").IndexOf("," + objStrategy.RangeId + ",") >= 0)
							{
								if (strExcludeChapter == "")
								{
									strExcludeChapter = objStrategy.RangeId.ToString();
								}
								else
								{
									strExcludeChapter = strExcludeChapter + "," + objStrategy.RangeId;
								}
							}
						}
					}

					obj.ExcludeChapterId = strExcludeChapter;
				}
				objBll.AddRandomExamStrategy(obj);
			}
			else
			{
				RandomExamStrategy obj = objBll.GetRandomExamStrategy(Convert.ToInt32(strID));
				if (nowCount - obj.ItemCount + Convert.ToInt32(itemCount) > objSubject.ItemCount)
				{
					SessionSet.PageMessage = "设置题数已超过大题设置题数！";
					return;
				}
				obj.ItemCount = Convert.ToInt32(itemCount);
				obj.ItemDifficultyID = Convert.ToInt32(itemDiff);
                obj.MaxItemDifficultyID = Convert.ToInt32(maxItemDiff);
                obj.Memo = "哈尔滨";
				if (Convert.ToInt32(itemCount) == 0)
				{
					string strExcludeChapter = "";
					BookChapter objBookChapter = objChapterBll.GetBookChapter(Convert.ToInt32(strBookID));
					IList<BookChapter> objExcludeChapterList = objChapterBll.GetBookChapterByIDPath(objBookChapter.IdPath);
					foreach (BookChapter bookChapter in objExcludeChapterList)
					{
						if (strExcludeChapter == "")
						{
							strExcludeChapter = bookChapter.ChapterId.ToString();
						}
						else
						{
							strExcludeChapter = strExcludeChapter + "," + bookChapter.ChapterId;
						}
					}
					obj.ExcludeChapterId = strExcludeChapter;
				}
				objBll.UpdateRandomExamStrategy(obj);
			}

			if (Convert.ToInt32(itemCount) == 0)
			{
				BookChapter objChapter = objChapterBll.GetBookChapter(Convert.ToInt32(strBookID));
				IList<BookChapter> objChapterList = objChapterBll.GetBookChapterByIDPath(objChapter.IdPath);
				if(objChapterList.Count > 0)
				{
					foreach (BookChapter chapter in objChapterList)
					{
						IList<RandomExamStrategy> objStrategyList = objBll.GetRandomExamStrategyBySubjectIDAndRangeID(Convert.ToInt32(subjectId), chapter.ChapterId, 4);
						if(objStrategyList.Count == 0)
						{
							RandomExamStrategy obj = new RandomExamStrategy();
							obj.SubjectId = Convert.ToInt32(subjectId);
							obj.RangeType = 4;
							obj.RangeId = chapter.ChapterId;
							obj.ItemTypeId = Convert.ToInt32(itemTypeId);
							obj.ItemCount = 0;
							obj.ItemDifficultyID = Convert.ToInt32(itemDiff);
                            obj.MaxItemDifficultyID = Convert.ToInt32(maxItemDiff);
							string strExcludeChapter = "";
							IList<BookChapter> objExcludeChapterList = objChapterBll.GetBookChapterByIDPath(chapter.IdPath);
							foreach (BookChapter bookChapter in objExcludeChapterList)
							{
								if(strExcludeChapter == "")
								{
									strExcludeChapter = bookChapter.ChapterId.ToString();
								}
								else
								{
									strExcludeChapter = strExcludeChapter + "," +bookChapter.ChapterId;
								}
							}
							obj.ExcludeChapterId = strExcludeChapter;
                            obj.Memo = "哈尔滨";
							objBll.AddRandomExamStrategy(obj);
						}
						else
						{
							foreach (RandomExamStrategy strategy in objStrategyList)
							{
								RandomExamStrategy obj = objBll.GetRandomExamStrategy(strategy.RandomExamStrategyId);
								obj.ItemCount = 0;
								obj.ItemDifficultyID = Convert.ToInt32(itemDiff);
                                obj.MaxItemDifficultyID = Convert.ToInt32(maxItemDiff);
								string strExcludeChapter = "";
								IList<BookChapter> objExcludeChapterList = objChapterBll.GetBookChapterByIDPath(chapter.IdPath);
								foreach (BookChapter bookChapter in objExcludeChapterList)
								{
									if (strExcludeChapter == "")
									{
										strExcludeChapter = bookChapter.ChapterId.ToString();
									}
									else
									{
										strExcludeChapter = strExcludeChapter + "," + bookChapter.ChapterId;
									}
								}
								obj.ExcludeChapterId = strExcludeChapter;
                                obj.Memo = "哈尔滨";
								objBll.UpdateRandomExamStrategy(obj);
							}
						}
					}
				}

				int parentID = objChapter.ParentId;
				while(parentID != 0)
				{
					BookChapter objParentChapter = objChapterBll.GetBookChapter(objChapter.ParentId);
					IList<BookChapter> bookChapters = objChapterBll.GetBookChapterByIDPath(objParentChapter.IdPath);
					string strChapterIds = "";
					foreach (BookChapter bookChapter in bookChapters)
					{
						if (strChapterIds == "")
						{
							strChapterIds = bookChapter.ChapterId.ToString();
						}
						else
						{
							strChapterIds = strChapterIds + "," + bookChapter.ChapterId;
						}
					}

					IList<RandomExamStrategy> objStrategyLists = objBll.GetRandomExamStrategys(Convert.ToInt32(subjectId));

					string excludeChapter = "";

					foreach (RandomExamStrategy objStrategy in objStrategyLists)
					{
						if (objStrategy.ItemCount == 0 && objStrategy.RangeType == 4 && ("," + bookChapters + ",").IndexOf("," + objStrategy.RangeId+",")>=0)
						{
							if (excludeChapter == "")
							{
								excludeChapter = objStrategy.RangeId.ToString();
							}
							else
							{
								excludeChapter = excludeChapter + "," + objStrategy.RangeId;
							}
						}
					}

					IList<RandomExamStrategy> objRandomExamStrategyList =
						objBll.GetRandomExamStrategyBySubjectIDAndRangeID(Convert.ToInt32(subjectId), objParentChapter.ChapterId, 4);
					if(objRandomExamStrategyList.Count > 0)
					{
						foreach (RandomExamStrategy strategy in objRandomExamStrategyList)
						{
							RandomExamStrategy obj = objBll.GetRandomExamStrategy(strategy.RandomExamStrategyId);
							if (excludeChapter != string.Empty)
							{
								string[] str = excludeChapter.Split(',');
								for (int i = 0; i < str.Length; i++)
								{
									if (("," + obj.ExcludeChapterId + ",").IndexOf("," + str[i] + ",") < 0)
									{

										if (string.IsNullOrEmpty(obj.ExcludeChapterId))
										{
											obj.ExcludeChapterId = objChapter.ChapterId.ToString();
										}
										else
										{
											obj.ExcludeChapterId = obj.ExcludeChapterId + "," + str[i];
										}
									}
								}
							}
							else
							{
								obj.ExcludeChapterId = string.Empty;
							}
							obj.Memo = "哈尔滨";
							objBll.UpdateRandomExamStrategy(obj);
						}
					}

					parentID = objParentChapter.ParentId;
				}

                if(parentID == 0)
                {
					IList<RandomExamStrategy> objStrategyLists = objBll.GetRandomExamStrategys(Convert.ToInt32(subjectId));

					string excludeChapter = "";

					foreach (RandomExamStrategy objStrategy in objStrategyLists)
					{
						BookChapter chapter = objChapterBll.GetBookChapter(objStrategy.RangeId);

						if (objStrategy.ItemCount == 0 && objStrategy.RangeType == 4 && chapter.BookId==objChapter.BookId)
						{
							if (excludeChapter == "")
							{
								excludeChapter = objStrategy.RangeId.ToString();
							}
							else
							{
								excludeChapter = excludeChapter + "," + objStrategy.RangeId;
							}
						}
					}

                    IList<RandomExamStrategy> objRandomExamStrategyList =
                        objBll.GetRandomExamStrategyBySubjectIDAndRangeID(Convert.ToInt32(subjectId), objChapter.BookId, 3);
                    if (objRandomExamStrategyList.Count > 0)
                    {
                        foreach (RandomExamStrategy strategy in objRandomExamStrategyList)
                        {
                            RandomExamStrategy obj = objBll.GetRandomExamStrategy(strategy.RandomExamStrategyId);
							if (excludeChapter != string.Empty)
							{
								string[] str = excludeChapter.Split(',');
								for (int i = 0; i < str.Length; i++)
								{
									if (("," + obj.ExcludeChapterId + ",").IndexOf("," + str[i] + ",") < 0)
									{

										if (string.IsNullOrEmpty(obj.ExcludeChapterId))
										{
											obj.ExcludeChapterId = objChapter.ChapterId.ToString();
										}
										else
										{
											obj.ExcludeChapterId = obj.ExcludeChapterId + "," + str[i];
										}
									}
								}
							}
							else
							{
								obj.ExcludeChapterId = string.Empty;
							}
                            obj.Memo = "哈尔滨";
                            objBll.UpdateRandomExamStrategy(obj);
                        }
                    }
                }
			}

			Grid1.EditIndex = -1;
			BindGrid();

			ClientScript.RegisterStartupScript(GetType(),
			"jsSelectFirstNode",
			@"showItem();",
			true);
		}

		protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (((DropDownList)e.Row.FindControl("ddlItemDiff")) != null)
			{
				DropDownList ddlDiff = (DropDownList)e.Row.FindControl("ddlItemDiff");
				ItemDifficultyBLL objBll =new ItemDifficultyBLL();
				IList<ItemDifficulty> objList = objBll.GetItemDifficulties();
                //ddlDiff.Items.Add(new ListItem("--请选择--",""));
				foreach (ItemDifficulty difficulty in objList)
				{
					ListItem item = new ListItem();
					item.Text = difficulty.DifficultyName;
					item.Value = difficulty.ItemDifficultyId.ToString();
					ddlDiff.Items.Add(item);
				}
				ddlDiff.SelectedValue = ((HiddenField)e.Row.FindControl("hfItemDiff")).Value;
			}

            if (((DropDownList)e.Row.FindControl("ddlMaxItemDiff")) != null)
            {
                DropDownList ddlDiff = (DropDownList)e.Row.FindControl("ddlMaxItemDiff");
                ItemDifficultyBLL objBll = new ItemDifficultyBLL();
                IList<ItemDifficulty> objList = objBll.GetItemDifficulties();
                //ddlDiff.Items.Add(new ListItem("--请选择--", ""));
                foreach (ItemDifficulty difficulty in objList)
                {
                    ListItem item = new ListItem();
                    item.Text = difficulty.DifficultyName;
                    item.Value = difficulty.ItemDifficultyId.ToString();
                    ddlDiff.Items.Add(item);
                }
                ddlDiff.SelectedValue = ((HiddenField)e.Row.FindControl("hfMaxItemDiff")).Value;
            }

            if (((DropDownList)e.Row.FindControl("ddlItemType")) != null)
            {
                DropDownList ddlDiff = (DropDownList)e.Row.FindControl("ddlItemType");
                RandomExamSubjectBLL objSubjectBll = new RandomExamSubjectBLL();
                IList<RandomExamSubject> objSubjectList =
                    objSubjectBll.GetRandomExamSubjectByRandomExamId(Convert.ToInt32(ViewState["ExamID"]));
                ddlDiff.Items.Add(new ListItem("--请选择--", ""));
                foreach (RandomExamSubject randomExamSubject in objSubjectList)
                {
                    ListItem item = new ListItem();
                    item.Text = randomExamSubject.TypeName;
                    item.Value = randomExamSubject.ItemTypeId.ToString();
                    ddlDiff.Items.Add(item); 
                }

                ddlDiff.SelectedValue = ((HiddenField)e.Row.FindControl("hfEditItemTypeId")).Value;
            }
		}

		protected void Grid1_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			string strID = Grid1.DataKeys[e.RowIndex].Values[0].ToString();
			if(strID == "0")
			{
				SessionSet.PageMessage = "清除成功！";
			}
			else
			{
				RandomExamStrategyBLL objBll = new RandomExamStrategyBLL();
				RandomExamStrategy objStrategy = objBll.GetRandomExamStrategy(Convert.ToInt32(strID));
				objBll.DeleteRandomExamStrategy(Convert.ToInt32(strID));

				if (objStrategy.ItemCount == 0)
				{
					BookChapterBLL objChapterBll = new BookChapterBLL();
					BookChapter objChapter = objChapterBll.GetBookChapter(objStrategy.RangeId);
					IList<BookChapter> objChapterList = objChapterBll.GetBookChapterByIDPath(objChapter.IdPath);
					if (objChapterList.Count > 0)
					{
						foreach (BookChapter chapter in objChapterList)
						{
							IList<RandomExamStrategy> objStrategyList = objBll.GetRandomExamStrategyBySubjectIDAndRangeID(objStrategy.SubjectId, chapter.ChapterId, 4);
							if (objStrategyList.Count > 0)
							{
								foreach (RandomExamStrategy strategy in objStrategyList)
								{
									objBll.DeleteRandomExamStrategy(strategy.RandomExamStrategyId);
								}
							}
						}
					}

					string subjectId = ((HiddenField) Grid1.Rows[e.RowIndex].FindControl("hfSubjectId")).Value;
					int parentID = objChapter.ParentId;
					while (parentID != 0)
					{
						BookChapter objParentChapter = objChapterBll.GetBookChapter(objChapter.ParentId);
						IList<RandomExamStrategy> objRandomExamStrategyList =
							objBll.GetRandomExamStrategyBySubjectIDAndRangeID(Convert.ToInt32(subjectId), objParentChapter.ChapterId, 4);
						if (objRandomExamStrategyList.Count > 0)
						{
							foreach (RandomExamStrategy strategy in objRandomExamStrategyList)
							{
								RandomExamStrategy obj = objBll.GetRandomExamStrategy(strategy.RandomExamStrategyId);
								if (("," + obj.ExcludeChapterId + ",").IndexOf(","+objChapter.ChapterId+",") >=0)
								{
									string strExclude = "";
									string[] str = obj.ExcludeChapterId.Split(',');
									for(int i = 0 ; i<str.Length; i++)
									{
										if (str[i] == objChapter.ChapterId.ToString())
										{
											continue;
										}

										if(strExclude == "")
										{
											strExclude = str[i];
										}
										else
										{
											strExclude = strExclude + "," + str[i];
										}
									}

									obj.ExcludeChapterId = strExclude;
								}
								objBll.UpdateRandomExamStrategy(obj);
							}
						}

						parentID = objParentChapter.ParentId;
					}

					if(parentID == 0)
					{
						IList<RandomExamStrategy> objStrategyLists = objBll.GetRandomExamStrategys(Convert.ToInt32(subjectId));

						string excludeChapter = "";

						foreach (RandomExamStrategy strategy in objStrategyLists)
						{
							BookChapter chapter = objChapterBll.GetBookChapter(strategy.RangeId);

							if (objStrategy.ItemCount == 0 && objStrategy.RangeType == 4 && chapter.BookId == objChapter.BookId)
							{
								if (excludeChapter == "")
								{
									excludeChapter = strategy.RangeId.ToString();
								}
								else
								{
									excludeChapter = excludeChapter + "," + strategy.RangeId;
								}
							}
						}

						IList<RandomExamStrategy> objRandomExamStrategyList =
							objBll.GetRandomExamStrategyBySubjectIDAndRangeID(Convert.ToInt32(subjectId), objChapter.BookId, 3);
						if (objRandomExamStrategyList.Count > 0)
						{
							foreach (RandomExamStrategy strategy in objRandomExamStrategyList)
							{
								RandomExamStrategy obj = objBll.GetRandomExamStrategy(strategy.RandomExamStrategyId);

								if (excludeChapter != string.Empty)
								{
									string[] str = excludeChapter.Split(',');
									for (int i = 0; i < str.Length; i++)
									{
										if (("," + obj.ExcludeChapterId + ",").IndexOf("," + str[i] + ",") < 0)
										{

											if (string.IsNullOrEmpty(obj.ExcludeChapterId))
											{
												obj.ExcludeChapterId = objChapter.ChapterId.ToString();
											}
											else
											{
												obj.ExcludeChapterId = obj.ExcludeChapterId + "," + str[i];
											}
										}
									}
								}
								else
								{
									obj.ExcludeChapterId = string.Empty;
								}
									
								obj.Memo = "哈尔滨";
								objBll.UpdateRandomExamStrategy(obj);
							}
						}
					}
				}
			}

			BindGrid();

			ClientScript.RegisterStartupScript(GetType(),
			"jsSelectFirstNode",
			@"showItem();",
			true);
		}

		public void ddlItemDiffChange(object sender, System.EventArgs e)
		{
			DropDownList ddlPipe = (DropDownList)sender;
			string strPipeID = ddlPipe.SelectedValue;
			TableCell cell = (TableCell)ddlPipe.Parent;
			GridViewRow item = (GridViewRow)cell.Parent;

            string maxItemDiff = ((DropDownList)item.FindControl("ddlMaxItemDiff")).SelectedValue;

            if (!string.IsNullOrEmpty(strPipeID) && !string.IsNullOrEmpty(maxItemDiff))
            {
                SetMaxCount(item, Convert.ToInt32(strPipeID),Convert.ToInt32(maxItemDiff));
            }
            else if (strPipeID == string.Empty && !string.IsNullOrEmpty(maxItemDiff))
            {
                SetMaxCount(item, 0, Convert.ToInt32(maxItemDiff));
            }
            else if (!string.IsNullOrEmpty(strPipeID) && maxItemDiff == string.Empty)
            {
                SetMaxCount(item, Convert.ToInt32(strPipeID), 0);
            }
            else
            {
                SetMaxCount(item, 0, 0);
            }
		}

        public void ddlMaxItemDiffChange(object sender, System.EventArgs e)
        {
            DropDownList ddlPipe = (DropDownList)sender;
            string strPipeID = ddlPipe.SelectedValue;
            TableCell cell = (TableCell)ddlPipe.Parent;
            GridViewRow item = (GridViewRow)cell.Parent;

            string ItemDiff = ((DropDownList)item.FindControl("ddlItemDiff")).SelectedValue;


            if (!string.IsNullOrEmpty(strPipeID) && !string.IsNullOrEmpty(ItemDiff))
            {
                SetMaxCount(item, Convert.ToInt32(ItemDiff), Convert.ToInt32(strPipeID));
            }
            else if (strPipeID == string.Empty && !string.IsNullOrEmpty(ItemDiff))
            {
                SetMaxCount(item, Convert.ToInt32(ItemDiff), 0);
            }
            else if (!string.IsNullOrEmpty(strPipeID) && ItemDiff == string.Empty)
            {
                SetMaxCount(item, 0, Convert.ToInt32(strPipeID));
            }
            else
            {
                SetMaxCount(item, 0, 0);
            }
        }

	    private void SetMaxCount(GridViewRow item,int MinValue,int MaxValue)
        {
            HiddenField hfItemTypeID = (HiddenField)item.FindControl("hfEditItemTypeId");
            Label lblMaxCount = (Label)item.FindControl("lblMaxCount");
            ItemBLL objItemBll = new ItemBLL();
            string strBookID = Request.QueryString.Get("bookChapterID");
            string type = Request.QueryString.Get("type");
            if (type == "book")
            {
                lblMaxCount.Text = objItemBll.GetItemsByBookID(Convert.ToInt32(strBookID), Convert.ToInt32(hfItemTypeID.Value), MinValue,MaxValue).ToString();
            }
            else
            {
                BookChapterBLL objBookChapterBll = new BookChapterBLL();
                string idPath = objBookChapterBll.GetBookChapter(Convert.ToInt32(strBookID)).IdPath;
                lblMaxCount.Text = objItemBll.GetItemsByBookChapterIdPath(idPath, Convert.ToInt32(hfItemTypeID.Value), MinValue,MaxValue).ToString();
            }
	        
	    }

        public void ddlItemTypeChange(object sender, System.EventArgs e)
        {
            DropDownList ddlPipe = (DropDownList)sender;
            string strPipeID = ddlPipe.SelectedValue;
            TableCell cell = (TableCell)ddlPipe.Parent;
            GridViewRow item = (GridViewRow)cell.Parent;

            string strBookID = Request.QueryString.Get("bookChapterID");
            string type = Request.QueryString.Get("type");

            //当所选题型为“请选择”时
            if(strPipeID == "")
            {
                ((HiddenField)item.FindControl("hfEditItemTypeId")).Value = "";
                ((Label)item.FindControl("lblSubjectName")).Text = "";
                ((Label)item.FindControl("lblMaxCount")).Text = "";
                ((Label) item.FindControl("lblEditTypeName")).Text = "";
            }
            else
            {
                string errorMessage = "";
                foreach (GridViewRow row in Grid1.Rows)
                {
                    if(Grid1.EditIndex != row.RowIndex)
                    {
                        if (((HiddenField)row.FindControl("hfItemTypeId")).Value == strPipeID)
                        {
                            if (((HiddenField)row.FindControl("hfID")).Value == "0")
                            {
                                errorMessage = "请先编辑" + ddlPipe.SelectedItem.Text + "题初始组卷策略，再添加该题型附属组卷策略！";
                                break;
                            }
                        }
                    }
                }

                if(errorMessage != "")
                {
                    SessionSet.PageMessage = errorMessage;
                    ddlPipe.SelectedValue = "";
                    return;
                }

                RandomExamSubjectBLL objSubjectBll = new RandomExamSubjectBLL();
                IList<RandomExamSubject> objSubjectList =
                    objSubjectBll.GetRandomExamSubjectByRandomExamId(Convert.ToInt32(ViewState["ExamID"]));

                ItemBLL objItemBll = new ItemBLL();
                BookChapterBLL objBookChapterBll = new BookChapterBLL();
                foreach (RandomExamSubject subject in objSubjectList)
                {
                    if (subject.ItemTypeId == Convert.ToInt32(ddlPipe.SelectedValue))
                    {
                        ((HiddenField)item.FindControl("hfSubjectId")).Value = subject.RandomExamSubjectId.ToString();
                        ((HiddenField)item.FindControl("hfEditItemTypeId")).Value = subject.ItemTypeId.ToString();
                        ((Label)item.FindControl("lblEditTypeName")).Text = subject.TypeName;
                        ((Label)item.FindControl("lblSubjectName")).Text = subject.SubjectName;
                        if (type == "book")
                        {
                            ((Label)item.FindControl("lblMaxCount")).Text =
                                objItemBll.GetItemsByBookID(Convert.ToInt32(strBookID), subject.ItemTypeId).ToString();
                        }
                        else
                        {
                            string idPath = objBookChapterBll.GetBookChapter(Convert.ToInt32(strBookID)).IdPath;
                            ((Label)item.FindControl("lblMaxCount")).Text =
                                objItemBll.GetItemsByBookChapterIdPath(idPath, subject.ItemTypeId).ToString();
                        }
                    }
                }

                string strMin = ((DropDownList)item.FindControl("ddlItemDiff")).SelectedValue;
                string strMax = ((DropDownList)item.FindControl("ddlMaxItemDiff")).SelectedValue;
                if (!string.IsNullOrEmpty(strMin) && !string.IsNullOrEmpty(strMax))
                {
                    SetMaxCount(item, Convert.ToInt32(strMin), Convert.ToInt32(strMax));
                }
                else if (strMax == string.Empty && !string.IsNullOrEmpty(strMin))
                {
                    SetMaxCount(item, Convert.ToInt32(strMin), 0);
                }
                else if (!string.IsNullOrEmpty(strMax) && strMin == string.Empty)
                {
                    SetMaxCount(item, 0, Convert.ToInt32(strMax));
                }
                else
                {
                    SetMaxCount(item, 0, 0);
                }   
            }
        }

	    protected void btnAdd_Click(object sender, System.EventArgs e)
        {
            if (Grid1.EditIndex != -1)
            {
                SessionSet.PageMessage = "请先保存正在编辑的项！";
                return;
            }

            string strBookID = Request.QueryString.Get("bookChapterID");
            string type = Request.QueryString.Get("type");
            string rangeType;
            if (type == "book")
            {
                rangeType = "3";
            }
            else
            {
                rangeType = "4";
            }

            DataTable dt = GetDataTable();

            DataRow dr = dt.NewRow();
            dr["RandomExamStrategyId"] = 0;
            dr["RangeType"] = Convert.ToInt32(rangeType);
            dr["SubjectId"] = 0;
            dr["RangeId"] = Convert.ToInt32(strBookID);
            dr["ItemTypeId"] = 0;
            dr["ItemTypeName"] = string.Empty;
            dr["SubjectName"] = string.Empty;
            dr["ItemCount"] = string.Empty;
            dr["ItemDifficultyID"] = "1";
            dr["MaxItemDifficultyID"] = "5";
            dr["MaxCount"] = 0;
            dt.Rows.InsertAt(dr,0);
            Grid1.EditIndex = 0;
            Grid1.DataSource = dt;
            Grid1.DataBind();

            ((Label)Grid1.Rows[Grid1.EditIndex].FindControl("lblEditTypeName")).Visible = false;
            ((DropDownList)Grid1.Rows[Grid1.EditIndex].FindControl("ddlItemType")).Visible = true;
        }
	}
}
