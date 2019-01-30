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

namespace RailExamWebApp.Paper
{
    public partial class PaperManageChooseItem : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["mode"] = Request.QueryString.Get("mode");

                string strId = Request.QueryString.Get("id");
                hfPaperId.Value = strId;
                this.btnSave.Attributes.Add("onclick", "ManagePaper(" + strId + ")");
                PaperSubjectBLL paperSubjectBLL = new PaperSubjectBLL();
                IList<PaperSubject> paperSubjects = paperSubjectBLL.GetPaperSubjectByPaperId(int.Parse(strId));

                if (paperSubjects != null)
                {
                    for (int i = 0; i < paperSubjects.Count; i++)
                    {
                        int j = i + 1;
                        ListItem Li = new ListItem();
                        Li.Value = paperSubjects[i].PaperSubjectId.ToString();
                        Li.Text = "第" + j + "题：  " + paperSubjects[i].SubjectName;
                        lbType.Items.Add(Li);
                    }
                    lbType.SelectedIndex = 0;
                    hfItemType.Value = paperSubjectBLL.GetPaperSubject(Convert.ToInt32(lbType.SelectedValue)).ItemTypeId.ToString();
                    BindData();
                }
            }
        }

        private void BindData()
        {
            Grid1.DataBind();
            SetTotalScore(int.Parse(lbType.SelectedValue));
        }

        private void SetTotalScore(int nPaperSubjectId)
        {
            if (nPaperSubjectId > 0)
            {
                PaperSubjectBLL paperSubjectBLL = new PaperSubjectBLL();
                PaperSubject paperSubject = paperSubjectBLL.GetPaperSubject(nPaperSubjectId);

                if (paperSubject != null)
                {
                    ViewState["UnitScore"] = paperSubject.UnitScore;
                    lblTotalScore.Text = paperSubject.TotalScore.ToString();
                    LabelItemCount.Text = paperSubject.ItemCount.ToString();
                    this.LabelCurrentItemCount.Text = Grid1.Rows.Count.ToString();
                    this.LabelCurrentTotalScore.Text = (Grid1.Rows.Count * paperSubject.UnitScore).ToString();
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


            Response.Redirect("PaperManageEditSecond.aspx?mode=" + strFlag + "&id=" + strId);
        }
        

        protected void btnCancel_Click(object sender, EventArgs e)
        {
             string strId = Request.QueryString.Get("id");
             PaperBLL paperBLL = new PaperBLL();
             RailExam.Model.Paper paper= paperBLL.GetPaper(int.Parse(strId));
            
             int nPaperCount=paper.ItemCount;
             
             PaperItemBLL ptBll=new PaperItemBLL();
             IList<RailExam.Model.PaperItem> PaperItems = ptBll.GetItemsByPaperId(int.Parse(strId));

             int nItemCount = 0;
             if (PaperItems.Count>0)
            {
                nItemCount = PaperItems.Count;
            }

            if (nItemCount != nPaperCount)
            {
                SessionSet.PageMessage = "试题总数与试卷设定的题数不相等，请手工进行修改！";
                return;
            }


            Response.Write("<script>top.window.opener.form1.Refresh.value='true';top.window.opener.form1.submit();top.window.close();</script>");
        }

        protected void lbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PaperSubjectBLL objBll = new PaperSubjectBLL();
            hfItemType.Value = objBll.GetPaperSubject(Convert.ToInt32(lbType.SelectedValue)).ItemTypeId.ToString();
            BindData();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                bool bChecked = ((CheckBox)Grid1.Rows[i].FindControl("chSelect")).Checked;
                if (bChecked)
                {
                    string strPaperItemId = ((HiddenField)Grid1.Rows[i].FindControl("hfPaperItemId")).Value;

                    PaperItemBLL paperItemBLL = new PaperItemBLL();
                    paperItemBLL.DeletePaperItem(int.Parse(strPaperItemId));
                }
            }

            BindData();
        }

        protected void btnSelectItem_Click(object sender, EventArgs e)
        {
            string strItemIds = Request.Form.Get("hfItemIds");
            if (!string.IsNullOrEmpty(strItemIds))
            {
                string strOldItemsId = ",";

                for (int i = 0; i < Grid1.Rows.Count; i++)
                {
                    string strItemId = ((HiddenField)Grid1.Rows[i].FindControl("hfItemId")).Value;

                    strOldItemsId += strItemId + ",";
                }


                int unitScore = 0;
                if (ViewState["UnitScore"] != null && ViewState["UnitScore"].ToString() != "")
                {
                    unitScore = int.Parse(ViewState["UnitScore"].ToString());
                }

                string strId = Request.QueryString.Get("id");
                string[] eLid = strItemIds.Split(new char[] { ',' });
                ItemBLL itemBLL = new ItemBLL();

                PaperItemBLL paperItemBLL = new PaperItemBLL();
                IList<PaperItem> paperItems = new List<PaperItem>();

                foreach (string s in eLid)
                {
                    string strItemId = s.Trim();
                    if (strItemId != "" && (strOldItemsId.IndexOf("," + strItemId + ",") == -1))
                    {
                        RailExam.Model.Item item = itemBLL.GetItem(int.Parse(strItemId));

                        PaperItem paperItem = new PaperItem();

                        paperItem.PaperId = int.Parse(strId);
                        paperItem.PaperSubjectId = int.Parse(lbType.SelectedValue);
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
                        paperItem.Score = unitScore;
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

                if (paperItems.Count>0)
                {
                  paperItemBLL.AddPaperItem(paperItems);
                }

                BindData();
            }
        }
    }
}
