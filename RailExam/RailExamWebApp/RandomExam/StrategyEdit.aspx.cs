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
using RailExam.BLL;
using RailExam.Model;
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class StrategyEdit : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strID = Request.QueryString.Get("id");
                ViewState["mode"] = Request.QueryString.Get("mode");
            	hfItemType.Value = Request.QueryString.Get("itemTypeID");
            	ddlType.SelectedValue = hfItemType.Value;
                if (ViewState["mode"].ToString() == "Insert")
                {
                    string subjectId = Request.QueryString.Get("subjectid");
                    hfSubjectId.Value = subjectId;
                    if (!string.IsNullOrEmpty(subjectId))
                    {
                        RandomExamSubjectBLL paperStrategySubjectBLL = new RandomExamSubjectBLL();
                        RandomExamSubject paperStrategySubject = paperStrategySubjectBLL.GetRandomExamSubject(int.Parse(subjectId));

                        if (paperStrategySubject != null)
                        {
                            hfExamID.Value = paperStrategySubject.RandomExamId.ToString();
                            txtSubjectName.Text = paperStrategySubject.SubjectName;
                            ddlType.SelectedValue = paperStrategySubject.ItemTypeId.ToString();
                            labelTotalCount.Text = paperStrategySubject.ItemCount.ToString();

                            RandomExamStrategyBLL randomExamStrategyBLL = new RandomExamStrategyBLL();
                            IList<RailExam.Model.RandomExamStrategy> RandomExamStrategys = randomExamStrategyBLL.GetRandomExamStrategys(paperStrategySubject.RandomExamSubjectId);

                            int hasCount = 0;
                            foreach (RailExam.Model.RandomExamStrategy randomExamStrategy in RandomExamStrategys)
                            {
                                hasCount += randomExamStrategy.ItemCount;
                            }

                            labelLeaveCount.Text = (paperStrategySubject.ItemCount - hasCount).ToString();

                            txtNDR.Text = labelLeaveCount.Text;

                            RandomExamResultBLL reBll = new RandomExamResultBLL();
                            IList<RailExam.Model.RandomExamResult> examResults = reBll.GetRandomExamResultByExamID(paperStrategySubject.RandomExamId);

                            if (examResults.Count > 0)
                            {
                                SaveButton.Visible = false;
                            }
                        }

                        hfSubjectId.Value = subjectId;

                        mother1.Visible = false;
                        mother2.Visible = false;
                    }

                    OracleAccess db = new OracleAccess();
                    string strSql = "select RANDOM_EXAM_STRATEGY_SEQ.nextval from dual";
                    DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];
                    hfKeyID.Value = dr[0].ToString();
                }
                else
                {
                    hfKeyID.Value = strID;
                    FillPage(int.Parse(strID));
                }
            }

            if(!string.IsNullOrEmpty(HfChapterId.Value))
            {
                if(HfRangeType.Value == "4" )
                {
                    BookChapterBLL chapterBll = new BookChapterBLL();
                    BookChapter chapter = chapterBll.GetBookChapter(Convert.ToInt32(HfChapterId.Value));

                    txtChapterName.Text = chapter.ChapterName;
                }
                else
                {
                    BookBLL bookBll = new BookBLL();
                    RailExam.Model.Book book= bookBll.GetBook(Convert.ToInt32(HfChapterId.Value));

                    txtChapterName.Text = book.bookName;
                }
            }

            if(!string.IsNullOrEmpty(HfExCludeChaptersId.Value))
            {
                string strSql = "select * from Book_Chapter where Chapter_ID in (" + HfExCludeChaptersId.Value + ")";

                OracleAccess db = new OracleAccess();
                DataSet ds = db.RunSqlDataSet(strSql);

                string strName = string.Empty;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if(strName == string.Empty)
                    {
                        strName += dr["Chapter_Name"].ToString();
                    }
                    else
                    {
                        strName += "," + dr["Chapter_Name"].ToString();
                    }
                }

                txtExCludeChapters.Text = strName;
            }
        }

        private void FillPage(int nID)
        {
            RandomExamStrategyBLL paperStrategyBookChapterBLL = new RandomExamStrategyBLL();

            RandomExamStrategy paperStrategyBookChapter = paperStrategyBookChapterBLL.GetRandomExamStrategy(nID);

            if (paperStrategyBookChapter != null)
            {
                txtMemo.Text = paperStrategyBookChapter.Memo;
                txtChapterName.Text = paperStrategyBookChapter.RangeName;
                HfRangeName.Value = paperStrategyBookChapter.RangeName;

                hfSubjectId.Value = paperStrategyBookChapter.SubjectId.ToString();
                HfRangeType.Value = paperStrategyBookChapter.RangeType.ToString();
                HfChapterId.Value = paperStrategyBookChapter.RangeId.ToString();
                ddlType.SelectedValue = paperStrategyBookChapter.ItemTypeId.ToString();
                txtNDR.Text = paperStrategyBookChapter.ItemCount.ToString();



                RandomExamSubjectBLL paperStrategySubjectBLL = new RandomExamSubjectBLL();
                RandomExamSubject paperStrategySubject = paperStrategySubjectBLL.GetRandomExamSubject(int.Parse(hfSubjectId.Value));

                if (paperStrategySubject != null)
                {
                    hfExamID.Value = paperStrategySubject.RandomExamId.ToString();
                    txtSubjectName.Text = paperStrategySubject.SubjectName;
                    labelTotalCount.Text = paperStrategySubject.ItemCount.ToString();

                    IList<RailExam.Model.RandomExamStrategy> RandomExamStrategys = paperStrategyBookChapterBLL.GetRandomExamStrategys(paperStrategySubject.RandomExamSubjectId);

                    int hasCount = 0;
                    foreach (RailExam.Model.RandomExamStrategy randomExamStrategy in RandomExamStrategys)
                    {
                        if(randomExamStrategy.RandomExamStrategyId != nID)
                        {
                            hasCount += randomExamStrategy.ItemCount;
                        }
                    }

                    labelLeaveCount.Text = (paperStrategySubject.ItemCount - hasCount).ToString();

                    RandomExamResultBLL reBll = new RandomExamResultBLL();
                    IList<RailExam.Model.RandomExamResult> examResults = reBll.GetRandomExamResultByExamID(paperStrategySubject.RandomExamId);

                    if (examResults.Count > 0)
                    {
                        ViewState["mode"] = "ReadOnly";
                    }
                }

                string strSql = "select a.random_exam_strategy_id as RandomExamStrategyId,a.Item_Count as ItemCount,"
                                + "GetBookChapterName(b.Chapter_ID) ChapterName, b.Chapter_ID as ChapterId,b.ID_Path as IDPath "
                                + "from random_exam_strategy a"
                                + " inner join Book_Chapter b on a.Range_ID=b.Chapter_ID "
                                + " where a.Is_Mother_Item=1 and a.Mother_ID=" + nID;

                OracleAccess db = new OracleAccess();

                DataSet ds = db.RunSqlDataSet(strSql);


                if (paperStrategyBookChapter.RangeType == 3 && ds.Tables[0].Rows.Count == 0)
                {
                    txtExCludeChapters.Text = paperStrategyBookChapter.ExcludeChapterId;

                    if (string.IsNullOrEmpty(paperStrategyBookChapter.ExcludeChapterId) == false)
                    {
                        FillExcludeCategorysID(paperStrategyBookChapter.ExcludeChapterId);
                    }
                }

                int sumTotalCount = 0;
                ItemBLL itembll = new ItemBLL();
                if (HfRangeType.Value == "3")
                {

                    sumTotalCount = itembll.GetItemsByBookID(Convert.ToInt32(HfChapterId.Value),
                                                          Convert.ToInt32(hfItemType.Value));
                }
                else
                {
                    BookChapterBLL bookChapterBll = new BookChapterBLL();
                    BookChapter bookChapter = bookChapterBll.GetBookChapter(Convert.ToInt32(HfChapterId.Value));
                    sumTotalCount = itembll.GetItemsByBookChapterIdPath(bookChapter.IdPath,
                                                                              Convert.ToInt32(hfItemType.Value));
                }

                if (ds.Tables[0].Rows.Count == 0)
                {
                    mother1.Visible = false;
                    mother2.Visible = false;
                }
                else
                {
                    mother1.Visible = true;
                    mother2.Visible = true;

                    DataColumn dc1 = ds.Tables[0].Columns.Add("MaxItemCount");

                    ItemBLL item = new ItemBLL();
                    int sumCount = 0;
                    int sumMaxCount = 0;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dr["MaxItemCount"] = item.GetItemsByBookChapterIdPath(dr["IDPath"].ToString(),
                                                                              Convert.ToInt32(hfItemType.Value));

                        sumCount += Convert.ToInt32(dr["ItemCount"].ToString());
                        sumMaxCount += Convert.ToInt32(dr["MaxItemCount"].ToString());
                    }

                    txtNDR.Text = (paperStrategyBookChapter.ItemCount + sumCount).ToString();

                    Grid1.DataSource = ds;
                    Grid1.DataBind();

                    lblMotherInfo.Text = "其中子题最大题数：" + (sumTotalCount - sumMaxCount) + ";母题最大题数：" + sumMaxCount;
                }
                strSql = "select * from Item a inner join Book_Chapter b on a.Chapter_ID=b.Chapter_ID " + GetSelectSql();
                lblTotalCount.Text = db.RunSqlDataSet(strSql).Tables[0].Rows.Count.ToString();
                strSql = " select * from  Random_Exam_Item_Select where RANDOM_EXAM_STRATEGY_ID=" + nID;
                lblSelectCount.Text = db.RunSqlDataSet(strSql).Tables[0].Rows.Count.ToString();
            }

            if (ViewState["mode"].ToString() == "ReadOnly")
            {
                SaveButton.Visible = false;
                CancelButton.Visible = true;
                ddlType.Enabled = false;               
                txtMemo.Enabled = false;
            }
        }

        private void FillExcludeCategorysID(string strIDs)
        {
            string strName = string.Empty;
            string[] str1 = strIDs.Split(new char[] { ',' });
            for (int i = 0; i < str1.Length; i++)
            {
                if (i > 0)
                {
                    strName += ",";
                }

                BookChapterBLL bookChapterBLL = new BookChapterBLL();
                BookChapter bookChapter = bookChapterBLL.GetBookChapter(int.Parse(str1[i]));

                strName += bookChapter.ChapterName;
            }
            txtExCludeChapters.Text = strName;
            HfExCludeChaptersId.Value = strIDs;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            if (txtNDR.Text == "")
            {
                txtNDR.Text = "0";
            }

            RandomExamStrategyBLL paperStrategyBookChapterBLL = new RandomExamStrategyBLL();
            RandomExamStrategy paperStrategyBookChapter;
            IList<RandomExamStrategy> objList = new List<RandomExamStrategy>();


            int sumCount = 0;
            string strErrorMessage = "";

            #region 原母题
            /*
            if (mother2.Visible)
            {
                foreach (GridViewRow gridRow in Grid1.Rows)
                {
                    Label lblName = (Label) gridRow.FindControl("ChapterName");
                    HiddenField chapterId = (HiddenField) gridRow.FindControl("hfChapterId");
                    Label lblMaxCount = (Label) gridRow.FindControl("hfMaxItemCount");
                    TextBox txtCount = (TextBox) gridRow.FindControl("txtItemCount");

                    int maxCount = Convert.ToInt32(lblMaxCount.Text);
                    int nCount;
                    try
                    {
                        nCount = Convert.ToInt32(txtCount.Text);

                        if (nCount < 0)
                        {
                            strErrorMessage = "【" + lblName.Text + "】选题数必须为正整数！";
                            break;
                        }
                    }
                    catch
                    {
                        strErrorMessage = "【" + lblName.Text + "】选题数必须为正整数！";
                        break;
                    }

                    if (nCount > maxCount)
                    {
                        strErrorMessage = "【" + lblName.Text + "】选题数必须小于最大题数！";
                        break;
                    }

                    sumCount += nCount;

                    paperStrategyBookChapter = new RandomExamStrategy();

                    paperStrategyBookChapter.SubjectId = int.Parse(hfSubjectId.Value);
                    paperStrategyBookChapter.RangeType = 4;
                    paperStrategyBookChapter.RangeId = int.Parse(chapterId.Value);
                    paperStrategyBookChapter.ItemTypeId = int.Parse(ddlType.SelectedValue);
                    paperStrategyBookChapter.ExcludeChapterId = "";
                    paperStrategyBookChapter.Memo = "";
                    paperStrategyBookChapter.RangeName = lblName.Text;
                    paperStrategyBookChapter.ItemCount = nCount;
                    paperStrategyBookChapter.ItemDifficultyID = 3;
                    paperStrategyBookChapter.IsMotherItem = true;
                    objList.Add(paperStrategyBookChapter);

                    if (("," + HfExCludeChaptersId + ",").IndexOf("," + chapterId.Value + ",") <= 0)
                    {
                        if (HfExCludeChaptersId.Value == "")
                        {
                            HfExCludeChaptersId.Value = chapterId.Value;
                        }
                        else
                        {
                            HfExCludeChaptersId.Value += "," + chapterId.Value;
                        }
                    }
                }
            }*/
            #endregion

            if (!string.IsNullOrEmpty(strErrorMessage))
            {
                SessionSet.PageMessage = strErrorMessage;
                return;
            }

            int totalCount = int.Parse(lblTotalCount.Text);
            int selectCount = int.Parse(lblSelectCount.Text);
            int leaveCount = int.Parse(labelLeaveCount.Text);
            int nowCount;

            try
            {
                nowCount = Convert.ToInt32(txtNDR.Text);
            }
            catch (Exception)
            {
                SessionSet.PageMessage = "选择题数必须为数字！";
                return;
            }

            if (nowCount>selectCount)
            {
                SessionSet.PageMessage = "选择题数必须小于等于可选题数！";
                return;
            }

            if (nowCount > leaveCount)
            {
                SessionSet.PageMessage = "选择题数必须小于等于还剩题数！";
                return;
            }

            //if(totalCount- sumCount <0)
            //{
            //    SessionSet.PageMessage = "组题策略的选题数必须大于母题取题数之和！";
            //    return;
            //}

            paperStrategyBookChapter = new RandomExamStrategy();
            paperStrategyBookChapter.SubjectId = int.Parse(hfSubjectId.Value);
            paperStrategyBookChapter.RangeType = int.Parse(HfRangeType.Value);
            paperStrategyBookChapter.RangeId = int.Parse(HfChapterId.Value);
            paperStrategyBookChapter.ItemTypeId = int.Parse(ddlType.SelectedValue);
            paperStrategyBookChapter.ExcludeChapterId = HfExCludeChaptersId.Value;
            paperStrategyBookChapter.Memo = txtMemo.Text;
            paperStrategyBookChapter.RangeName = HfRangeName.Value;
            paperStrategyBookChapter.ItemCount = nowCount;
            paperStrategyBookChapter.IsMotherItem = false;
        	paperStrategyBookChapter.ItemDifficultyID = 3;

            if (ViewState["mode"].ToString() == "Insert")
            {
                paperStrategyBookChapter.RandomExamStrategyId = Convert.ToInt32(hfKeyID.Value);
                paperStrategyBookChapterBLL.AddRandomExamStrategy(paperStrategyBookChapter);

                #region 原母题

                /*
                foreach (RandomExamStrategy obj in objList)
                {
                    obj.MotherID = key;
                    paperStrategyBookChapterBLL.AddRandomExamStrategy(obj);
                }
                */

                #endregion
            }
            else if (ViewState["mode"].ToString() == "Edit")
            {
                string strId = Request.QueryString.Get("id");

                if (Pub.HasPaper(Convert.ToInt32(strId)))
                {
                    Response.Write("<script>alert('该考试已生成试卷，不能被编辑！');window.close();</script>");
                    return;
                }

                paperStrategyBookChapter.RandomExamStrategyId = int.Parse(strId);
                paperStrategyBookChapterBLL.UpdateRandomExamStrategy(paperStrategyBookChapter);

                #region 原母题
                /*
                OracleAccess db = new OracleAccess();
                string strSql = "delete from Random_Exam_Strategy where Is_Mother_Item=1 and Mother_ID=" + strId;
                db.ExecuteNonQuery(strSql);

                foreach (RandomExamStrategy obj in objList)
                {
                    obj.MotherID = int.Parse(strId);
                    paperStrategyBookChapterBLL.AddRandomExamStrategy(obj);
                }*/
                #endregion
            }

            //Response.Write("<script>var p = window.opener; if(p) p.document.form1.submit();window.close();</script>");

            Response.Write("<script>window.returnValue='true';window.close();</script>");
        }

        protected void btnShowMother_Click(object sender, EventArgs e)
        {
            #region 原母题
            //改变教材章节，清空母题
            string strSql = "delete from random_exam_strategy "
                            + " where random_exam_strategy_id in ("
                            + " select random_exam_strategy_id from "
                            + " random_exam_strategy a "
                            + " inner join Book_Chapter b on a.Range_ID =b.Chapter_ID "
                            + " where a.Is_Mother_Item = 1 ";

            if(HfRangeType.Value == "3")
            {
                strSql += " and b.Book_ID=" + HfChapterId.Value;
            }
            else
            {
                strSql += " and b.ID_Path ||'/' like (select ID_Path from Book_Chapter where Chapter_ID=" +
                          HfChapterId.Value + ") ||'/%' ";
            }

            //有屏蔽章节
            if (!string.IsNullOrEmpty(HfExCludeChaptersId.Value))
            {
                strSql += " and b.Chapter_ID not in (" + HfExCludeChaptersId.Value + ") ";
            }

            strSql += ")";

            OracleAccess db = new OracleAccess();
            db.ExecuteNonQuery(strSql);


            ItemBLL item = new ItemBLL();
            BookChapterBLL bookChapterBll = new BookChapterBLL();
            strSql = "select GetBookChapterName(Chapter_ID) ChapterName, Chapter_ID as ChapterId,ID_Path as IDPath"
                            + " from Book_Chapter "
                            + " where Is_Mother_Item=1";

            int sumTotalCount = 0;
            if(HfRangeType.Value == "3")
            {
                strSql += " and Book_ID='" + HfChapterId.Value +"'";

                sumTotalCount = item.GetItemsByBookID(Convert.ToInt32(HfChapterId.Value),
                                                      Convert.ToInt32(hfItemType.Value));
            }
            else
            {
                BookChapter bookChapter = bookChapterBll.GetBookChapter(Convert.ToInt32(HfChapterId.Value));

                strSql += " and ID_Path||'/' like '" + bookChapter.IdPath + "/%'";

                sumTotalCount = item.GetItemsByBookChapterIdPath(bookChapter.IdPath,
                                                                          Convert.ToInt32(hfItemType.Value));
            }

            //有屏蔽章节
            if(!string.IsNullOrEmpty(HfExCludeChaptersId.Value))
            {
                strSql += " and Chapter_ID not in (" + HfExCludeChaptersId.Value + ")";
            }
            

            DataSet ds = db.RunSqlDataSet(strSql);

            if(ds.Tables[0].Rows.Count == 0)
            {
                mother1.Visible = false;
                mother2.Visible = false;
            }
            else
            {
                mother1.Visible = true;
                mother2.Visible = true;

                DataColumn dc1 = ds.Tables[0].Columns.Add("MaxItemCount");
                DataColumn dc2 = ds.Tables[0].Columns.Add("ItemCount");
                DataColumn dc3 = ds.Tables[0].Columns.Add("RandomExamStrategyId");

                int i = 1;
                int sumCount = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dr["MaxItemCount"] = item.GetItemsByBookChapterIdPath(dr["IDPath"].ToString(),
                                                                          Convert.ToInt32(hfItemType.Value));
                    dr["ItemCount"] = "";

                    dr["RandomExamStrategyId"] = -i;

                    sumCount += Convert.ToInt32(dr["MaxItemCount"]);

                    i++;
                }

                Grid1.DataSource = ds;
                Grid1.DataBind();

                lblMotherInfo.Text = "其中子题最大题数："+(sumTotalCount-sumCount)+";母题最大题数："+sumCount;
            }
            #endregion
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            string strID = Request.QueryString.Get("id") == null ? "null" : Request.QueryString.Get("id");

            OracleAccess db  = new OracleAccess();
            string strSql = "select * from Random_Exam_Item_Select where  RANDOM_EXAM_STRATEGY_ID=" + hfKeyID.Value;
            lblSelectCount.Text = db.RunSqlDataSet(strSql).Tables[0].Rows.Count.ToString();
        }

        private string GetSelectSql()
        {
            string strSql = "";
            //筛选可用题数，根据教材ID或章节ID查询Item表，排除Random_Exam_Item_Select中已选中的题。
            if (HfRangeType.Value == "4")
            {
                OracleAccess db = new OracleAccess();
                string str = "select * from Book_Chapter where Chapter_ID=" + HfChapterId.Value;
                string idPath = db.RunSqlDataSet(str).Tables[0].Rows[0]["Id_Path"].ToString();

                strSql = "where  b.id_path || '/' like '" + idPath + "/%' and a.Type_ID=" +
                            hfItemType.Value + " and Status_ID=1 ";
            }
            else
            {
                strSql = "where a.Book_ID=" + HfChapterId.Value + " and a.Type_ID=" +
                            hfItemType.Value + " and a.Status_ID=1";
            }

            //当组卷策略ID不为空时，需要排除当前组卷策略ID下选的试题
            strSql += " and a.Item_ID not in (select Item_ID from Random_Exam_Item_Select where Random_Exam_ID=" + hfExamID.Value
                        + "  and RANDOM_EXAM_STRATEGY_ID!=" + hfKeyID.Value+")";

            return strSql;
        }

        protected void btnSelectChapter_Click(object sender, EventArgs e)
        {
            string strSql;
            OracleAccess db = new OracleAccess();
            strSql = "select * from Item a"
                + " inner join Book_Chapter b on a.Chapter_ID=b.Chapter_ID "
                + GetSelectSql();

            lblTotalCount.Text = db.RunSqlDataSet(strSql).Tables[0].Rows.Count.ToString();
            lblSelectCount.Text = db.RunSqlDataSet(strSql).Tables[0].Rows.Count.ToString();

            //先在Random_Exam_Item_Select中删除所选教材章节的试题
            strSql = "delete from Random_Exam_Item_Select where RANDOM_EXAM_STRATEGY_ID=" + hfKeyID.Value;
            db.ExecuteNonQuery(strSql);

            string strID = Request.QueryString.Get("id") == null ? "null" : Request.QueryString.Get("id");

            //再在Random_Exam_Item_Select中插入所选教材章节可选的试题
            strSql = "insert into Random_Exam_Item_Select " +
                     " select Random_Exam_Item_Select_Seq.nextval,a.Item_ID," + hfExamID.Value + @"," + hfKeyID.Value + "," + hfSubjectId.Value 
                     + @" from Item a  inner join Book_Chapter b on a.Chapter_ID=b.Chapter_ID " + GetSelectSql();

            db.ExecuteNonQuery(strSql);

            if(strID != "null")
            {
                RandomExamStrategyBLL randomExamStrategyBll = new RandomExamStrategyBLL();
                RandomExamStrategy randomExamStrategy =
                    randomExamStrategyBll.GetRandomExamStrategy(Convert.ToInt32(strID));
                randomExamStrategy.RangeType = int.Parse(HfRangeType.Value);
                randomExamStrategy.RangeId = int.Parse(HfChapterId.Value);
                randomExamStrategy.ItemTypeId = int.Parse(ddlType.SelectedValue);
                randomExamStrategy.RangeName = HfRangeName.Value;
                randomExamStrategy.ExcludeChapterId = "";
                randomExamStrategyBll.UpdateRandomExamStrategy(randomExamStrategy);
            }
        }

        protected void btnExClude_Click(object sender, EventArgs e)
        {

            string strSql;
            OracleAccess db = new OracleAccess();

            string strID = Request.QueryString.Get("id") == null ? "null" : Request.QueryString.Get("id");

            //先在Random_Exam_Item_Select中删除所选教材章节的试题
            strSql = "delete from Random_Exam_Item_Select where RANDOM_EXAM_STRATEGY_ID=" + hfKeyID.Value;
            db.ExecuteNonQuery(strSql);

            //再在Random_Exam_Item_Select中插入所选教材章节的试题
            if(HfExCludeChaptersId.Value != "")
            {
                strSql = "insert into Random_Exam_Item_Select " +
                          " select Random_Exam_Item_Select_Seq.nextval,a.Item_ID," + hfExamID.Value + @"," + hfKeyID.Value + "," + hfSubjectId.Value +
                          @" from Item a  inner join Book_Chapter b on a.Chapter_ID=b.Chapter_ID "  + GetSelectSql() +" and  instr(','||'" + HfExCludeChaptersId.Value + "'||',',','||a.CHAPTER_ID||',' )=0 ";
            }
            else
            {
                strSql = "insert into Random_Exam_Item_Select " +
                  " select Random_Exam_Item_Select_Seq.nextval,a.Item_ID," + hfExamID.Value + @"," + hfKeyID.Value + "," + hfSubjectId.Value +
                  @" from Item a inner join Book_Chapter b on a.Chapter_ID=b.Chapter_ID " + GetSelectSql();

            }
            db.ExecuteNonQuery(strSql);

            strSql = "select * from Random_Exam_Item_Select where RANDOM_EXAM_STRATEGY_ID=" + hfKeyID.Value;
            lblTotalCount.Text = db.RunSqlDataSet(strSql).Tables[0].Rows.Count.ToString();
            lblSelectCount.Text = db.RunSqlDataSet(strSql).Tables[0].Rows.Count.ToString();

            if (strID != "null")
            {
                RandomExamStrategyBLL randomExamStrategyBll = new RandomExamStrategyBLL();
                RandomExamStrategy randomExamStrategy =
                    randomExamStrategyBll.GetRandomExamStrategy(Convert.ToInt32(strID));
                randomExamStrategy.RangeType = int.Parse(HfRangeType.Value);
                randomExamStrategy.RangeId = int.Parse(HfChapterId.Value);
                randomExamStrategy.ItemTypeId = int.Parse(ddlType.SelectedValue);
                randomExamStrategy.RangeName = HfRangeName.Value;
                randomExamStrategy.ExcludeChapterId = HfExCludeChaptersId.Value;
                randomExamStrategyBll.UpdateRandomExamStrategy(randomExamStrategy);
            }
        }
    }
}
