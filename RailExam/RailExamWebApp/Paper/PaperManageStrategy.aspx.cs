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
using DSunSoft.Web.UI;
using DSunSoft.Web.Global;

namespace RailExamWebApp.Paper
{
    public partial class PaperManageStrategy : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["mode"] = Request.QueryString.Get("mode");
                hfMode.Value = ViewState["mode"].ToString();
                btnPreview.Visible = false;
                btnEdit.Visible = false;
                string strId = Request.QueryString.Get("id");
                this.btnPreview.Attributes.Add("onclick", "ManagePaper(" + strId + ")");
                PaperBLL paperBLL = new PaperBLL();
                RailExam.Model.Paper paper = paperBLL.GetPaper(int.Parse(strId));

                if (paper != null)
                {
                    lblPaperName.Text = paper.PaperName;
                    lblPaperCategoryName.Text = paper.CategoryName;
                    hfCategoryId.Value = paper.CategoryId.ToString();
                }
            }
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            string strId = Request.QueryString.Get("id");

            string strFlag = "";

            if (ViewState["mode"].ToString() == "Insert")
            {
                strFlag = "Edit";
            }
            else
            {
                strFlag = ViewState["mode"].ToString();
            }

            Response.Redirect("PaperManageEdit.aspx?mode=" + strFlag + "&id=" + strId);
        }

        private void SavePaperItem(IList<RailExam.Model.Item> itemList, int nDifficulty, int nPaperId, int nPaperSubjectId, decimal nUnitScore)
        {
            if (itemList.Count > 0)
            {
                PaperItemBLL paperItemBLL = new PaperItemBLL();
                IList<PaperItem> paperItems = new List<PaperItem>();

                if (itemList.Count > nDifficulty)
                {
                    Random ObjRandom = new Random();
                    Hashtable hashTable = new Hashtable();
                    while (hashTable.Count < nDifficulty)
                    {
                        int i = ObjRandom.Next(itemList.Count);
                        hashTable[itemList[i].ItemId] = itemList[i].ItemId;
                    }

                    ItemBLL itemBLL = new ItemBLL();

                    //把hashtable里的试题加到考试表

                    foreach (int key in hashTable.Keys)
                    {
                        string strItemId = hashTable[key].ToString();

                        RailExam.Model.Item item = itemBLL.GetItem(int.Parse(strItemId));

                        PaperItem paperItem = new PaperItem();

                        paperItem.PaperId = nPaperId;
                        paperItem.PaperSubjectId = nPaperSubjectId;
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
                        paperItem.OrderIndex = 0;
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

                        paperItems.Add(paperItem);
                    }
                }
                else    //直接把这些试题加到考试表

                {
                    foreach (RailExam.Model.Item item in itemList)
                    {
                        PaperItem paperItem = new PaperItem();

                        paperItem.PaperId = nPaperId;
                        paperItem.PaperSubjectId = nPaperSubjectId;
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
                        paperItem.OrderIndex = 0;
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

                        paperItems.Add(paperItem);
                    }
                }
                if (paperItems.Count > 0)
                {
                    paperItemBLL.AddPaperItem(paperItems);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int nStrategyId = int.Parse(hfStrategyId.Value);
            int nPaperId = int.Parse(Request.QueryString.Get("id"));

            PaperStrategyBLL paperStrategyBLL = new PaperStrategyBLL();

            RailExam.Model.PaperStrategy paperStrategy = paperStrategyBLL.GetPaperStrategy(nStrategyId);

            PaperStrategySubjectBLL paperStrategySubjectBLL = new PaperStrategySubjectBLL();

            IList<PaperStrategySubject> paperStrategySubjects = paperStrategySubjectBLL.GetPaperStrategySubjectByPaperStrategyId(nStrategyId);

            int nStrategyMode = paperStrategy.StrategyMode;
            txtStrategyName.Text = paperStrategy.PaperStrategyName;

            for (int i = 0; i < paperStrategySubjects.Count; i++)
            {
                //大题               
                PaperSubjectBLL paperSubjectBLL = new PaperSubjectBLL();

                PaperSubject paperSubject = new PaperSubject();

                paperSubject.ItemCount = paperStrategySubjects[i].ItemCount;
                paperSubject.ItemTypeId = paperStrategySubjects[i].ItemTypeId;
                paperSubject.Memo = paperStrategySubjects[i].Memo;
                paperSubject.OrderIndex = paperStrategySubjects[i].OrderIndex;
                paperSubject.PaperId = nPaperId;
                paperSubject.Remark = paperStrategySubjects[i].Remark;
                paperSubject.SubjectName = paperStrategySubjects[i].SubjectName;
                paperSubject.TotalScore = paperStrategySubjects[i].TotalScore;
                paperSubject.UnitScore = paperStrategySubjects[i].UnitScore;

                decimal nUnitScore = paperSubject.UnitScore;

                int nPaperSubjectId = paperSubjectBLL.AddPaperSubject(paperSubject);

                ItemBLL itemBLL = new ItemBLL();

                if (nStrategyMode == 2)  //教材章节模式
                {
                    PaperStrategyBookChapterBLL paperStrategyBookChapterBLL = new PaperStrategyBookChapterBLL();

                    IList<PaperStrategyBookChapter> paperStrategyBookChapters = paperStrategyBookChapterBLL.GetItemsByPaperStrategySubjectID(paperStrategySubjects[i].PaperStrategySubjectId);

                    for (int k = 0; k < paperStrategyBookChapters.Count; k++)
                    {
                        //策略 
                        int nChapterId = paperStrategyBookChapters[k].RangeId;
                        int typeId = paperStrategyBookChapters[k].ItemTypeId;
                        int nRangeType = paperStrategyBookChapters[k].RangeType;
                        int nDifficultR = paperStrategyBookChapters[k].ItemDifficultyRandomCount;
                        //int nDifficulty1 = paperStrategyBookChapters[k].ItemDifficulty1Count;
                        //int nDifficulty2 = paperStrategyBookChapters[k].ItemDifficulty2Count;
                        //int nDifficulty3 = paperStrategyBookChapters[k].ItemDifficulty3Count;
                        //int nDifficulty4 = paperStrategyBookChapters[k].ItemDifficulty4Count;
                        //int nDifficulty5 = paperStrategyBookChapters[k].ItemDifficulty5Count;
                        decimal uScore = paperStrategyBookChapters[k].UnitScore;
                        string strExcludeIDs = paperStrategyBookChapters[k].ExcludeChapterId;

                        ////难度1
                        //IList<RailExam.Model.Item> itemList = itemBLL.GetItemsByStrategy(nRangeType, 1, nChapterId, typeId, strExcludeIDs);

                        //SavePaperItem(itemList, nDifficulty1, nPaperId, nPaperSubjectId, nUnitScore);

                        ////难度2
                        //itemList = itemBLL.GetItemsByStrategy(nRangeType, 2, nChapterId, typeId, strExcludeIDs);

                        //SavePaperItem(itemList, nDifficulty2, nPaperId, nPaperSubjectId, nUnitScore);

                        ////难度3
                        //itemList = itemBLL.GetItemsByStrategy(nRangeType, 3, nChapterId, typeId, strExcludeIDs);

                        //SavePaperItem(itemList, nDifficulty3, nPaperId, nPaperSubjectId, nUnitScore);

                        ////难度4
                        //itemList = itemBLL.GetItemsByStrategy(nRangeType, 4, nChapterId, typeId, strExcludeIDs);

                        //SavePaperItem(itemList, nDifficulty4, nPaperId, nPaperSubjectId, nUnitScore);

                        ////难度5 
                        //itemList = itemBLL.GetItemsByStrategy(nRangeType, 5, nChapterId, typeId, strExcludeIDs);

                        //SavePaperItem(itemList, nDifficulty5, nPaperId, nPaperSubjectId, nUnitScore);

                        //随机难度

                        Random ObjRandom = new Random();

                        int ndr = ObjRandom.Next(1, 5);

                        //IList<RailExam.Model.Item> itemList = itemBLL.GetItemsByStrategy(nRangeType, ndr, nChapterId, typeId, strExcludeIDs);

                        //SavePaperItem(itemList, nDifficultR, nPaperId, nPaperSubjectId, nUnitScore);
                    }
                }
                else    //试题辅助分类模式
                {
                    PaperStrategyItemCategoryBLL paperStrategyItemCategoryBLL = new PaperStrategyItemCategoryBLL();

                    IList<PaperStrategyItemCategory> paperStrategyItemCategorys = paperStrategyItemCategoryBLL.GetItemsByPaperSubjectId(paperStrategySubjects[i].PaperStrategySubjectId);

                    for (int k = 0; k < paperStrategyItemCategorys.Count; k++)
                    {
                        //策略 
                        int nChapterId = paperStrategyItemCategorys[k].ItemCategoryId;
                        int typeId = paperStrategyItemCategorys[k].ItemTypeId;
                        int nDifficultR = paperStrategyItemCategorys[k].ItemDifficultyRandomCount;
                        //int nDifficulty1 = paperStrategyItemCategorys[k].ItemDifficulty1Count;
                        //int nDifficulty2 = paperStrategyItemCategorys[k].ItemDifficulty2Count;
                        //int nDifficulty3 = paperStrategyItemCategorys[k].ItemDifficulty3Count;
                        //int nDifficulty4 = paperStrategyItemCategorys[k].ItemDifficulty4Count;
                        //int nDifficulty5 = paperStrategyItemCategorys[k].ItemDifficulty5Count;
                        decimal uScore = paperStrategyItemCategorys[k].UnitScore;
                        string strExcludeIDs = paperStrategyItemCategorys[k].ExcludeCategorysId;

                        ////难度1 
                        //IList<RailExam.Model.Item> itemList = itemBLL.GetItemsByStrategyItem(1, nChapterId, typeId, strExcludeIDs);

                        //SavePaperItem(itemList, nDifficulty1, nPaperId, nPaperSubjectId, nUnitScore);

                        ////难度2
                        //itemList = itemBLL.GetItemsByStrategyItem(2, nChapterId, typeId, strExcludeIDs);

                        //SavePaperItem(itemList, nDifficulty2, nPaperId, nPaperSubjectId, nUnitScore);

                        ////难度3
                        //itemList = itemBLL.GetItemsByStrategyItem(3, nChapterId, typeId, strExcludeIDs);

                        //SavePaperItem(itemList, nDifficulty3, nPaperId, nPaperSubjectId, nUnitScore);

                        ////难度4
                        //itemList = itemBLL.GetItemsByStrategyItem(4, nChapterId, typeId, strExcludeIDs);

                        //SavePaperItem(itemList, nDifficulty4, nPaperId, nPaperSubjectId, nUnitScore);

                        ////难度5 
                        //itemList = itemBLL.GetItemsByStrategyItem(5, nChapterId, typeId, strExcludeIDs);

                        //SavePaperItem(itemList, nDifficulty5, nPaperId, nPaperSubjectId, nUnitScore);

                        //随机难度

                        Random ObjRandom = new Random();

                        int ndr = ObjRandom.Next(1, 5);

                        IList<RailExam.Model.Item>  itemList = itemBLL.GetItemsByStrategyItem(ndr, nChapterId, typeId, strExcludeIDs);

                        SavePaperItem(itemList, nDifficultR, nPaperId, nPaperSubjectId, nUnitScore);
                    }
                }
            }

            int itemCount = 0;
            decimal totalScore = 0;

            PaperItemBLL paperBLL = new PaperItemBLL();
            IList<RailExam.Model.PaperItem> PaperItems = paperBLL.GetItemsByPaperId(nPaperId);

            if (PaperItems.Count>0)
            {
                itemCount = PaperItems.Count;
                totalScore = PaperItems[0].Score * PaperItems.Count;
            }

            SessionSet.PageMessage = "随机生成试卷共" + itemCount + "题，" + totalScore + "分，如果试卷题数不等于设定的总题数请手工进行修改!";           
            this.btnSave.Visible = false;
            this.btnLast.Visible = false;
            btnPreview.Visible = true;
            btnEdit.Visible = true;
           
        }

        protected void btnEdit_Click(object sender, ImageClickEventArgs e)
        {
            string strId = Request.QueryString.Get("id");
            string strMode = "update";
            Response.Redirect("PaperManageEditSecond.aspx?mode=" + strMode + "&id=" + strId);
        }         
    }
}