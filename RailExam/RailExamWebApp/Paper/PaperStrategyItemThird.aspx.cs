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
    public partial class PaperStrategyItemThird : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strPaperID = Request.QueryString.Get("Paperid");
            if (!string.IsNullOrEmpty(strPaperID))
            {
                this.btnPreview.Attributes.Add("onclick", "ManagePaper(" + strPaperID + ");");
            }

            if (!IsPostBack)
            {
                ViewState["mode"] = Request.QueryString.Get("mode");
                if (ViewState["mode"].ToString() == "ReadOnly")
                {
                    NewButton.Visible = false;
                    btnSave.Visible = false;
                    btnCancel.Visible = true;
                }

              
                string strId = Request.QueryString.Get("id");
                 strPaperID = Request.QueryString.Get("Paperid");
                if (!string.IsNullOrEmpty(strPaperID))
                {
                    HfstrategyID.Value = strId;
                    Hfpaperid.Value = strPaperID;
                }

                PaperStrategySubjectBLL paperStrategySubjectBLL = new PaperStrategySubjectBLL();
                IList<PaperStrategySubject> paperStrategySubjects = paperStrategySubjectBLL.GetPaperStrategySubjectByPaperStrategyId(int.Parse(strId));

                if (paperStrategySubjects != null)
                {
                    for (int i = 0; i < paperStrategySubjects.Count; i++)
                    {
                        int j = i + 1;
                        ListItem Li = new ListItem();
                        Li.Value = paperStrategySubjects[i].PaperStrategySubjectId.ToString();
                        Li.Text = "第" + j + "题：  " + paperStrategySubjects[i].SubjectName;
                        lbType.Items.Add(Li);
                    }
                    lbType.SelectedIndex = 0;

                    ViewState["value"] = lbType.SelectedValue;
                }
            }

            string strFlag = Request.Form.Get("Flag");
            if (strFlag != null && strFlag == "1")
            {
                string strId = Request.QueryString.Get("id");
                if (!string.IsNullOrEmpty(strId))
                {
                    PaperStrategyBLL paperStrategyBLL = new PaperStrategyBLL();
                    paperStrategyBLL.DeletePaperStrategy(int.Parse(strId));
                    ViewState["DeleteFlag"] = 0;

                    if (ViewState["mode"].ToString() == "Insert")
                    {
                        ViewState["mode"] = "Edit";
                    }

                    Response.Redirect("PaperManageChooseItem.aspx?mode=" + ViewState["mode"].ToString() + "&id=" + Request.QueryString.Get("Paperid"));
               
                }
            }

            if (strFlag != null && strFlag == "0")
            {
                ViewState["DeleteFlag"] = 0;

                if (ViewState["mode"].ToString() == "Insert")
                {
                    ViewState["mode"] = "Edit";
                }

                PaperStrategyBLL paperStrategyBLL = new PaperStrategyBLL();
                string strId = Request.QueryString.Get("id");
                RailExam.Model.PaperStrategy ps = paperStrategyBLL.GetPaperStrategy(int.Parse(strId));
                ps.PaperStrategyName = HFStrategyName.Value;
                paperStrategyBLL.UpdatePaperStrategy(ps);

                Response.Redirect("PaperManageChooseItem.aspx?mode=" + ViewState["mode"].ToString() + "&id=" + Request.QueryString.Get("Paperid"));
               
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

            string strPaperID = Request.QueryString.Get("Paperid");
            if (!string.IsNullOrEmpty(strPaperID))
            {
                Response.Redirect("PaperStrategyEditSecond.aspx?mode=" + strFlag + "&id=" + strId + "&Paperid=" + strPaperID);

            }
            else
            {
                Response.Redirect("PaperStrategyEditSecond.aspx?mode=" + strFlag + "&id=" + strId);
            }

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
            PaperStrategyItemCategoryBLL psbcBll = new PaperStrategyItemCategoryBLL();
            int Ncount = psbcBll.GetItemCount(int.Parse(Request.QueryString.Get("id")));

            if(Ncount==0)
            {
                SessionSet.PageMessage="请添加策略！";
                return;
            }
            

            string strPaperID = Request.QueryString.Get("Paperid");
            if (!string.IsNullOrEmpty(strPaperID))
            {   
                int nStrategyId = int.Parse(Request.QueryString.Get("id"));
                int nPaperId = int.Parse(strPaperID);

                PaperStrategyBLL paperStrategyBLL = new PaperStrategyBLL();

                RailExam.Model.PaperStrategy paperStrategy = paperStrategyBLL.GetPaperStrategy(nStrategyId);

                PaperStrategySubjectBLL paperStrategySubjectBLL = new PaperStrategySubjectBLL();

                IList<PaperStrategySubject> paperStrategySubjects = paperStrategySubjectBLL.GetPaperStrategySubjectByPaperStrategyId(nStrategyId);

                int nStrategyMode = paperStrategy.StrategyMode;

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

                    if (nStrategyMode == 3)   //试题辅助分类模式
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

                            //难度1 
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

                            IList<RailExam.Model.Item> itemList = itemBLL.GetItemsByStrategyItem(ndr, nChapterId, typeId, strExcludeIDs);

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

                btnPreview.Visible = true;
                this.btnedit.Visible = true;
                this.btnSave.Visible = false;
                btnLast.Visible = false;
                NewButton.Visible = false;

                SessionSet.PageMessage = "随机生成试卷共" + itemCount + "题，" + totalScore + "分，如果试卷题数不等于设定的总题数请手工进行修改!"; 
                ViewState["DeleteFlag"] = 1;
            }
            else
            {
                Response.Write("<script>top.window.opener.form1.Refresh.value='true';top.window.opener.form1.submit();top.window.close();</script>");
            }
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            string strId = Request.QueryString.Get("Paperid");
            string strMode = "update";
            Response.Redirect("PaperManageEditSecond.aspx?mode=" + strMode + "&id=" + strId);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            if (ViewState["DeleteFlag"] != null && ViewState["DeleteFlag"].ToString() == "1")
            {
                ViewState["DeleteFlag"] = 0;
                Response.Write("<script>ConfirmDelete();</script>");
            }
        }
    }
}