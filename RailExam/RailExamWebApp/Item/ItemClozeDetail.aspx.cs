using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using RailExamWebApp.Common.Class;
using RailExam.Model;
using RailExam.BLL;

namespace RailExamWebApp.Item
{
    public partial class ItemClozeDetail : PageBase
    {
        //页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }
                dropStatusItemsBind();

                string mode = Request.QueryString.Get("mode");
                if (!String.IsNullOrEmpty(mode))
                {
                    this.hfMode.Value = mode;
                    //无论新增还是编辑，初始题号设为1
                    this.hfTestNoBefore.Value = "1";
                    switch (mode)
                    {
                        case "insert":
                            string bookID = Request.QueryString.Get("bid"),
                                   chapterID = Request.QueryString.Get("cid");
                            if (!String.IsNullOrEmpty(bookID) && !String.IsNullOrEmpty(chapterID))
                            {
                                DataLoadForInsert(bookID, chapterID);
                            }
                            break;
                        case "edit":
                            string itemID = Request.QueryString.Get("id");
                            if (!String.IsNullOrEmpty(itemID))
                            {
                                DataLoadForEdit(Int32.Parse(itemID));
                            }
                            //禁用题数目文本框
                            this.txtTestCount.Enabled = false;
                            break;
                    }
                }

                dropItemCount_SelectedIndexChanged(null, null);
            }
        }

        //数据加载(新增进入)
        private void DataLoadForInsert(string bookID, string chapterID)
        {
            this.hfBookID.Value = bookID;
            this.hfChapterID.Value = chapterID;

            string chapterName = String.Empty;
            BookBLL bookBLL = new BookBLL();
            RailExam.Model.Book book = bookBLL.GetBook(Int32.Parse(bookID));
            if (book != null)
            {
                chapterName += book.bookName;
            }
            BookChapterBLL chapterBLL = new BookChapterBLL();
            BookChapter chapter = chapterBLL.GetBookChapter(Int32.Parse(chapterID));
            if (chapter != null)
            {
                chapterName += " " + chapter.ChapterName;
            }
            this.lblchapter.Text = chapterName;
            this.txtCreater.Text = PrjPub.CurrentLoginUser.EmployeeName;
            this.dateCreate.DateValue = DateTime.Now.Date.ToShortDateString();
            this.dateOut.DateValue = (new DateTime(2050, 1, 1)).Date.ToShortDateString();
        }

        //数据加载(修改进入)
        private void DataLoadForEdit(int itemID)
        {
            //加载主题数据
            ItemBLL itemBLL = new ItemBLL();
            RailExam.Model.Item mainItem = itemBLL.GetItem(itemID);
            if (mainItem != null)
            {
                DataLoadForEditOnMain(mainItem);
                //主题加载完之后加载第一小题
                txtTestCount_TextChanged(null, null);
                this.dropTestNo.SelectedValue = "1";
                //保存当前变化前的题号
                this.hfTestNoBefore.Value = "1";

                IList<RailExam.Model.Item> subItemList = itemBLL.GetItemsByParentItemID(itemID);
                foreach (RailExam.Model.Item item in subItemList)
                {
                    if (item.ItemIndex.ToString() == "1")
                    {
                        //加载第一题
                        DataLoadForEditOnSubsidiary(item);
                        break;
                    }
                }
            }
        }

        //主题数据加载
        private void DataLoadForEditOnMain(RailExam.Model.Item mainItem)
        {
            this.lblchapter.Text = mainItem.BookName + " " + mainItem.ChapterName;
            this.dropStatus.SelectedValue = mainItem.StatusId.ToString();
            this.dropPurpose.SelectedValue = mainItem.UsageId.ToString();
            this.dateCreate.DateValue = mainItem.CreateTime.Date.ToShortDateString();
            this.dateOut.DateValue = mainItem.OutDateDate.Date.ToShortDateString();
            this.txtCreater.Text = mainItem.CreatePerson;
            this.txtTestCount.Text = mainItem.AnswerCount.ToString();
            this.txtContent.Text = mainItem.Content;
            this.txtKeyWord.Text = mainItem.KeyWord;
            this.txtMemo.Text = mainItem.Memo;
        }

        //子题数据加载
        private void DataLoadForEditOnSubsidiary(RailExam.Model.Item subItem)
        {
            this.dropTestNo.SelectedValue = subItem.ItemIndex.ToString();
            this.dropItemCount.Text = subItem.AnswerCount.ToString();
            this.txtSubContent.Text = subItem.Content;
            this.txtKeyWord.Text = subItem.KeyWord;
            this.txtMemo.Text = subItem.Memo;
            TextBox[] txts ={ this.txtA, this.txtB, this.txtC, this.txtD, this.txtE, this.txtF, this.txtG, this.txtH, this.txtJ, this.txtK, this.txtL };
            string[] selectAnswers = subItem.SelectAnswer.Split('|');
            for (int i = 0; i < selectAnswers.Length; i++)
            {
                if (!String.IsNullOrEmpty(selectAnswers[i]))
                {
                    txts[i].Text = selectAnswers[i];
                }
                else
                {
                    break;
                }
            }
            RadioButton[] radioes ={ this.radioA, this.radioB, this.radioC, this.radioD, this.radioE, this.radioF, this.radioG, this.radioH, this.radioJ, this.radioK, this.radioL };
            for (int i = 0; i < radioes.Length; radioes[i++].Checked = false) ;
            radioes[Int32.Parse(subItem.StandardAnswer)].Checked = true;
            this.hfTestNoBefore.Value = subItem.ItemIndex.ToString();
        }

        //数据检验
        private bool DataCheck()
        {
            if (this.dateCreate.DateValue == null)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "OK", "alert('请输入出题日期');", true);
                return false;
            }
            if (this.dateOut.DateValue == null)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "OK", "alert('请输入过期日期');", true);
                return false;
            }
            return true;
        }

        //清空子题相关项
        private void ItemsClear()
        {
            TextBox[] txts ={ this.txtA, this.txtB, this.txtC, this.txtD, this.txtE, this.txtF, this.txtG, this.txtH, this.txtI, this.txtJ, this.txtK, this.txtL };
            for (int i = 0; i < txts.Length; txts[i++].Text = String.Empty) ;
            this.radioA.Checked = true;
            this.txtSubContent.Text = this.txtKeyWord.Text = this.txtMemo.Text = String.Empty;
        }

        //修改数据
        private void DataUpdate(int testNo)
        {
            this.hfTestNoBefore.Value = testNo.ToString();
            ItemBLL itemBLL = new ItemBLL();
            string itemID = Request["id"].ToString();
            RailExam.Model.Item mainItem = itemBLL.GetItem(Int32.Parse(itemID));
            //修改主题
            mainItem.Content = this.txtContent.Text;
            mainItem.OutDateDate = Convert.ToDateTime(this.dateOut.DateValue);
            mainItem.UsageId = Int32.Parse(this.dropPurpose.SelectedValue);
            mainItem.StatusId = Int32.Parse(this.dropStatus.SelectedValue);
            mainItem.CreatePerson = this.txtCreater.Text;
            mainItem.CreateTime = Convert.ToDateTime(this.dateCreate.DateValue);
            mainItem.Memo = this.txtMemo.Text;
            mainItem.KeyWord = this.txtKeyWord.Text;
            try
            {
                itemBLL.UpdateItem(mainItem);
            }
            catch (Exception)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "OK", "alert('主题修改不成功');", true);
                return;
            }
            //修改小题
            IList<RailExam.Model.Item> subItemList = itemBLL.GetItemsByParentItemID(Int32.Parse(itemID));
            foreach (RailExam.Model.Item item in subItemList)
            {
                if (item.ItemIndex.ToString() == testNo.ToString())
                {
                    string selectAnswer = String.Empty;
                    //存储选项
                    TextBox[] txts ={ this.txtA, this.txtB, this.txtC, this.txtD, this.txtE, this.txtF, this.txtG, this.txtH, this.txtI, this.txtJ, this.txtK, this.txtL };
                    for (int i = 0; i < 12; i++)
                    {
                        if (!String.IsNullOrEmpty(txts[i].Text))
                        {
                            selectAnswer += String.IsNullOrEmpty(selectAnswer) ? txts[i].Text : "|" + txts[i].Text;
                        }
                        else
                        {
                            break;
                        }
                    }
                    item.SelectAnswer = selectAnswer;
                    //存储答案
                    RadioButton[] radioes ={ this.radioA, this.radioB, this.radioC, this.radioD, this.radioE, this.radioF, this.radioG, this.radioH, this.radioI, this.radioJ, this.radioK, this.radioL };
                    for (int i = 0; i < 12; i++)
                    {
                        if (radioes[i].Checked)
                        {
                            item.StandardAnswer = Convert.ToString(i);
                            break;
                        }
                    }
                    item.Content = String.IsNullOrEmpty(this.txtSubContent.Text) ? this.txtContent.Text : this.txtSubContent.Text;
                    item.Memo = this.txtMemo.Text;
                    item.KeyWord = this.txtKeyWord.Text;
                    item.ParentItemId = Int32.Parse(itemID);
                    try
                    {
                        itemBLL.UpdateItem(item);
                        this.ClientScript.RegisterStartupScript(this.GetType(), "OK", "alert('第" + testNo + "小题已修改');", true);
                    }
                    catch (Exception ex)
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "OK", "alert('第" + testNo + "小题修改不成功');", true);
                    }
                    //找到并修改了需要修改的小题后，就可跳出循环了
                    break;
                }
            }

            string strSql = "update Item set Status_ID=" + mainItem.StatusId
                            + " where Item_ID=" + mainItem.ItemId + " or Parent_Item_ID=" + mainItem.ItemId;
            OracleAccess db= new OracleAccess();
            db.ExecuteNonQuery(strSql);
        }

        //新增数据
        private void DataInsert(int testNo)
        {
            this.hfTestNoBefore.Value = testNo.ToString();

            ItemBLL bllItem = new ItemBLL();
            //如果这个hfMainItemID为空,则Cloze的题干尚未保存
            if (String.IsNullOrEmpty(this.hfMainItemID.Value))
            {
                RailExam.Model.Item mainItem = new RailExam.Model.Item();
                mainItem.BookId = Int32.Parse(this.hfBookID.Value);
                mainItem.ChapterId = Int32.Parse(this.hfChapterID.Value);
                mainItem.OrganizationId = PrjPub.CurrentLoginUser.OrgID;
                mainItem.TypeId = 4;
                mainItem.CompleteTime = 60;
                mainItem.DifficultyId = 3;
                mainItem.Score = 4m;
                mainItem.Content = this.txtContent.Text;
                mainItem.AnswerCount = Int32.Parse(this.txtTestCount.Text);
                mainItem.OutDateDate = Convert.ToDateTime(this.dateOut.DateValue);
                mainItem.UsageId = Int32.Parse(this.dropPurpose.SelectedValue);
                mainItem.StatusId = Int32.Parse(this.dropStatus.SelectedValue);
                mainItem.CreatePerson = this.txtCreater.Text;
                mainItem.CreateTime = Convert.ToDateTime(this.dateCreate.DateValue);
                mainItem.UsedCount = 0;
                mainItem.Memo = this.txtMemo.Text;
                mainItem.HasPicture = 0;
                mainItem.KeyWord = this.txtKeyWord.Text;
                mainItem.ParentItemId = 0;

                try
                {
                    int newID = bllItem.AddItem(mainItem);
                    this.hfMainItemID.Value = newID.ToString();
                }
                catch (Exception)
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "OK", "alert('错误：题干保存失败');", true);
                    return;
                }
            }
            //保存选项
            //如果该小题已经保存了,不执行
            if (!this.hfSavedTestNo.Value.Contains(testNo.ToString()))
            {
                RailExam.Model.Item subItem = new RailExam.Model.Item();
                subItem.BookId = Int32.Parse(this.hfBookID.Value);
                subItem.ChapterId = Int32.Parse(this.hfChapterID.Value);
                subItem.OrganizationId = PrjPub.CurrentLoginUser.OrgID;
                subItem.TypeId = 5;
                subItem.CompleteTime = 60;
                subItem.DifficultyId = 3;
                subItem.Score = 4m;
                subItem.Content = String.IsNullOrEmpty(this.txtSubContent.Text) ? this.txtContent.Text : this.txtSubContent.Text;
                subItem.AnswerCount = Int32.Parse(this.dropItemCount.Text);
                string selectAnswer = String.Empty;
                //存储选项
                TextBox[] txts ={ this.txtA, this.txtB, this.txtC, this.txtD, this.txtE, this.txtF, this.txtG, this.txtH, this.txtI, this.txtJ, this.txtK, this.txtL };
                for (int i = 0; i < 12; i++)
                {
                    if (!String.IsNullOrEmpty(txts[i].Text))
                    {
                        selectAnswer += String.IsNullOrEmpty(selectAnswer) ? txts[i].Text : "|" + txts[i].Text;
                    }
                    else
                    {
                        break;
                    }
                }
                subItem.SelectAnswer = selectAnswer;
                //存储答案
                RadioButton[] radioes ={ this.radioA, this.radioB, this.radioC, this.radioD, this.radioE, this.radioF, this.radioG, this.radioH, this.radioI, this.radioJ, this.radioK, this.radioL };
                for (int i = 0; i < 12; i++)
                {
                    if (radioes[i].Checked)
                    {
                        subItem.StandardAnswer = Convert.ToString(i);
                        break;
                    }
                }
                subItem.OutDateDate = Convert.ToDateTime(this.dateOut.DateValue);
                subItem.UsageId = Int32.Parse(this.dropPurpose.SelectedValue);
                subItem.StatusId = Int32.Parse(this.dropStatus.SelectedValue);
                subItem.CreatePerson = this.txtCreater.Text;
                subItem.CreateTime = Convert.ToDateTime(this.dateCreate.DateValue);
                subItem.UsedCount = 0;
                subItem.Memo = this.txtMemo.Text;
                subItem.HasPicture = 0;
                subItem.KeyWord = this.txtKeyWord.Text;
                subItem.ParentItemId = Int32.Parse(this.hfMainItemID.Value);
                subItem.ItemIndex = testNo;
                try
                {
                    bllItem.AddItem(subItem);
                    this.hfSavedTestNo.Value = String.IsNullOrEmpty(this.hfSavedTestNo.Value) ? subItem.ItemIndex.ToString() : this.hfSavedTestNo.Value + "," + subItem.ItemIndex.ToString();

                    this.ClientScript.RegisterStartupScript(this.GetType(), "OK", "alert('第" + testNo + "小题已保存');", true);
                    ItemsClear();
                }
                catch (Exception ex)
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "OK", "alert('错误：选项保存失败. " + ex.Message + "');", true);
                }
            }
        }

        //保存 
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (DataCheck())
            {
                int testNo = Int32.Parse(this.dropTestNo.SelectedValue);
                switch (this.hfMode.Value)
                {
                    case "insert":
                        DataInsert(testNo);
                        break;
                    case "edit":
                        DataUpdate(testNo);
                        break;
                    default:
                        break;
                }
            }
        }

        //取消
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // to do
        }

        //切换题号后保存刚才一小题的数据，并显示新小题的数据
        protected void dropTestNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataCheck())
            {
                int testNo = Int32.Parse(this.hfTestNoBefore.Value);
                switch (this.hfMode.Value)
                {
                    case "insert":
                        DataInsert(testNo);
                        this.hfTestNoBefore.Value = this.dropTestNo.SelectedValue;
                        break;
                    case "edit":
                        DataUpdate(testNo);
                        //加载下一题
                        ItemBLL itemBLL = new ItemBLL();
                        IList<RailExam.Model.Item> subItemList = itemBLL.GetItemsByParentItemID(Convert.ToInt32(this.Request["id"]));
                        foreach (RailExam.Model.Item item in subItemList)
                        {
                            if (item.ItemIndex.ToString() == this.dropTestNo.SelectedValue)
                            {
                                //保存当前变化前的题号
                                this.hfTestNoBefore.Value = item.ItemIndex.ToString();
                                DataLoadForEditOnSubsidiary(item);
                                break;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        //试题状态下拉框数据绑定
        private void dropStatusItemsBind()
        {
            OracleAccess oa = new OracleAccess();
            string sql = "select * from ITEM_STATUS t";
            DataSet dsItemStatus = oa.RunSqlDataSet(sql);
            if (dsItemStatus != null && dsItemStatus.Tables.Count > 0)
            {
                foreach (DataRow row in dsItemStatus.Tables[0].Rows)
                {
                    ListItem li = new ListItem(row["status_name"].ToString(), row["item_status_id"].ToString());
                    this.dropStatus.Items.Add(li);
                }
            }
        }

        //题目数量改变后, 更新题号的下拉框数据项
        protected void txtTestCount_TextChanged(object sender, EventArgs e)
        {
            int testCount;
            if (Int32.TryParse(this.txtTestCount.Text, out testCount))
            {
                if (testCount > 0)
                {
                    this.dropTestNo.Items.Clear();
                    for (int i = 1; i <= testCount; i++)
                    {
                        ListItem li = new ListItem(i.ToString());
                        this.dropTestNo.Items.Add(li);
                    }
                }
            }
        }

        //选项数变化之后..  更新候选项
        protected void dropItemCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            HtmlTableRow[] rows ={ this.trA, this.trB, this.trC, this.trD, this.trE, this.trF, this.trG, this.trH, this.trI, this.trJ, this.trK, this.trL };
            int itemCount = Int32.Parse(this.dropItemCount.SelectedValue);
            for (int i = 0; i < 12; i++)
            {
                rows[i].Visible = false;
            }
            for (int i = 0; i < itemCount; i++)
            {
                rows[i].Visible = true;
            }
        }
    }
}
