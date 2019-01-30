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

namespace RailExamWebApp.Paper
{
    public partial class PaperStrategyEditFourth : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strID = Request.QueryString.Get("id");
                ViewState["mode"] = Request.QueryString.Get("mode");
                if (ViewState["mode"].ToString() == "Insert")
                {
                    if (!string.IsNullOrEmpty(strID))
                    {
                        PaperStrategySubjectBLL paperStrategySubjectBLL = new PaperStrategySubjectBLL();
                        PaperStrategySubject paperStrategySubject = paperStrategySubjectBLL.GetPaperStrategySubject(int.Parse(strID));

                        if (paperStrategySubject != null)
                        {
                            txtSubjectName.Text = paperStrategySubject.SubjectName;
                            txtScore.Text = paperStrategySubject.UnitScore.ToString();
                            ddlType.SelectedValue = paperStrategySubject.ItemTypeId.ToString();
                            labelTotalCount.Text = paperStrategySubject.ItemCount.ToString();
                        }

                        hfSubjectId.Value = strID;
                    }
                }
                else
                {
                    FillPage(int.Parse(strID));                   
                }
            }
        }

        private void FillPage(int nID)
        {
            PaperStrategyBookChapterBLL paperStrategyBookChapterBLL = new PaperStrategyBookChapterBLL();

            PaperStrategyBookChapter paperStrategyBookChapter = paperStrategyBookChapterBLL.GetPaperStrategyBookChapter(nID);

            if (paperStrategyBookChapter != null)
            {
                txtMemo.Text = paperStrategyBookChapter.Memo;
                txtChapterName.Text = paperStrategyBookChapter.RangeName ;
                HfRangeName.Value = paperStrategyBookChapter.RangeName;
                txtSubjectName.Text = paperStrategyBookChapter.SubjectName;
                txtExCludeChapters.Text = paperStrategyBookChapter.ExcludeChapterId;

                if (string.IsNullOrEmpty(paperStrategyBookChapter.ExcludeChapterId) == false)
                {
                    FillExcludeCategorysID(paperStrategyBookChapter.ExcludeChapterId);
                }
                txtNd1.Text = paperStrategyBookChapter.ItemDifficulty1Count.ToString();
                txtNd2.Text = paperStrategyBookChapter.ItemDifficulty2Count.ToString();
                txtNd3.Text = paperStrategyBookChapter.ItemDifficulty3Count.ToString();
                txtNd4.Text = paperStrategyBookChapter.ItemDifficulty4Count.ToString();
                txtNd5.Text = paperStrategyBookChapter.ItemDifficulty5Count.ToString();
                txtNDR.Text = paperStrategyBookChapter.ItemDifficultyRandomCount.ToString();
                txtScore.Text = paperStrategyBookChapter.UnitScore.ToString();
                txtSeconds.Text = paperStrategyBookChapter.UnitLimitTime.ToString();
                hfSubjectId.Value = paperStrategyBookChapter.StrategySubjectId.ToString();
                HfRangeType.Value = paperStrategyBookChapter.RangeType.ToString();
                HfChapterId.Value = paperStrategyBookChapter.RangeId.ToString();
                ddlType.SelectedValue = paperStrategyBookChapter.ItemTypeId.ToString();

                PaperStrategySubjectBLL paperStrategySubjectBLL = new PaperStrategySubjectBLL();
                PaperStrategySubject paperStrategySubject = paperStrategySubjectBLL.GetPaperStrategySubject(int.Parse(hfSubjectId.Value));

                if (paperStrategySubject != null)
                {
                    labelTotalCount.Text = paperStrategySubject.ItemCount.ToString();
                }
            }

            if (ViewState["mode"].ToString() == "ReadOnly")
            {
                SaveButton.Visible = false;
                CancelButton.Visible = true;

                ddlType.Enabled = false;
                txtNDR.Enabled = false;
                txtNd1.Enabled = false;
                txtNd2.Enabled = false;
                txtNd3.Enabled = false;
                txtNd4.Enabled = false;
                txtNd5.Enabled = false;
                txtSeconds.Enabled = false;
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
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (txtNd1.Text == "")
            {
                txtNd1.Text = "0";
            }

            if (txtNd2.Text == "")
            {
                txtNd2.Text = "0";
            }

            if (txtNd3.Text == "")
            {
                txtNd3.Text = "0";
            }

            if (txtNd4.Text == "")
            {
                txtNd4.Text = "0";
            }

            if (txtNd5.Text == "")
            {
                txtNd5.Text = "0";
            }

            if (txtNDR.Text == "")
            {
                txtNDR.Text = "0";
            }

            if (txtScore.Text == "")
            {
                txtScore.Text = "0";
            }

            if (txtSeconds.Text == "")
            {
                txtSeconds.Text = "0";
            }

            PaperStrategyBookChapterBLL paperStrategyBookChapterBLL = new PaperStrategyBookChapterBLL();
            PaperStrategyBookChapter paperStrategyBookChapter = new PaperStrategyBookChapter();

            paperStrategyBookChapter.StrategySubjectId = int.Parse(hfSubjectId.Value);
            paperStrategyBookChapter.RangeType = int.Parse(HfRangeType.Value);
            paperStrategyBookChapter.RangeId = int.Parse(HfChapterId.Value);
            paperStrategyBookChapter.ItemTypeId = int.Parse(ddlType.SelectedValue);
            paperStrategyBookChapter.ItemDifficultyRandomCount = int.Parse(txtNDR.Text);
            paperStrategyBookChapter.ItemDifficulty1Count = int.Parse(txtNd1.Text);
            paperStrategyBookChapter.ItemDifficulty2Count = int.Parse(txtNd2.Text);
            paperStrategyBookChapter.ItemDifficulty3Count = int.Parse(txtNd3.Text);
            paperStrategyBookChapter.ItemDifficulty4Count = int.Parse(txtNd4.Text);
            paperStrategyBookChapter.ItemDifficulty5Count = int.Parse(txtNd5.Text);
            paperStrategyBookChapter.UnitScore = decimal.Parse(txtScore.Text);
            paperStrategyBookChapter.ExcludeChapterId = HfExCludeChaptersId.Value;
            paperStrategyBookChapter.UnitLimitTime = int.Parse(txtSeconds.Text);
            paperStrategyBookChapter.Memo = txtMemo.Text;
            paperStrategyBookChapter.RangeName = HfRangeName.Value;


            if (ViewState["mode"].ToString() == "Insert")
            {
                paperStrategyBookChapterBLL.AddPaperStrategyBookChapter(paperStrategyBookChapter);
            }
            else if (ViewState["mode"].ToString() == "Edit")
            {
                string strId = Request.QueryString.Get("id");

                paperStrategyBookChapter.PaperStrategyBookChapterId = int.Parse(strId);

                paperStrategyBookChapterBLL.UpdatePaperStrategyBookChapter(paperStrategyBookChapter);
            }

            Response.Write("<script>var p = window.opener; if(p) p.document.form1.submit();window.close();</script>");
        }
    }
}