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
    public partial class PaperStrategyItemFourth : PageBase
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
            PaperStrategyItemCategoryBLL paperStrategyItemCategoryBLL = new PaperStrategyItemCategoryBLL();

            PaperStrategyItemCategory paperStrategyItemCategory = paperStrategyItemCategoryBLL.GetPaperStrategyItemCategory(nID);

            if (paperStrategyItemCategory != null)
            {
                if (paperStrategyItemCategory.ExcludeCategorysId != null)
                {
                    FillExcludeCategorysID(paperStrategyItemCategory.ExcludeCategorysId);
                }

                txtMemo.Text = paperStrategyItemCategory.Memo;
                txtItemCategoryName.Text = paperStrategyItemCategory.CategoryName;
                txtSubjectName.Text = paperStrategyItemCategory.SubjectName;
                hfExCludeItemCategoryIDS.Value = paperStrategyItemCategory.ExcludeCategorysId;
                txtNd1.Text = paperStrategyItemCategory.ItemDifficulty1Count.ToString();
                txtNd2.Text = paperStrategyItemCategory.ItemDifficulty2Count.ToString();
                txtNd3.Text = paperStrategyItemCategory.ItemDifficulty3Count.ToString();
                txtNd4.Text = paperStrategyItemCategory.ItemDifficulty4Count.ToString();
                txtNd5.Text = paperStrategyItemCategory.ItemDifficulty5Count.ToString();
                txtNDR.Text = paperStrategyItemCategory.ItemDifficultyRandomCount.ToString();
                txtScore.Text = paperStrategyItemCategory.UnitScore.ToString();
                txtSeconds.Text = paperStrategyItemCategory.UnitLimitTime.ToString();
                hfSubjectId.Value = paperStrategyItemCategory.StrategySubjectId.ToString();

                hfItemCategoryID.Value = paperStrategyItemCategory.ItemCategoryId.ToString();
                ddlType.SelectedValue = paperStrategyItemCategory.ItemTypeId.ToString();

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

                ItemCategoryBLL itemCategoryBLL = new ItemCategoryBLL();
                ItemCategory itemCategory = itemCategoryBLL.GetItemCategory(int.Parse(str1[i]));

                strName += itemCategory.CategoryName;
            }

            txtExCludeItemCategorys.Text = strName;
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

            PaperStrategyItemCategoryBLL paperStrategyItemCategoryBLL = new PaperStrategyItemCategoryBLL();
            PaperStrategyItemCategory paperStrategyItemCategory = new PaperStrategyItemCategory();

            paperStrategyItemCategory.ItemDifficultyRandomCount = int.Parse(txtNDR.Text);
            paperStrategyItemCategory.ItemDifficulty1Count = int.Parse(txtNd1.Text);
            paperStrategyItemCategory.ItemDifficulty2Count = int.Parse(txtNd2.Text);
            paperStrategyItemCategory.ItemDifficulty3Count = int.Parse(txtNd3.Text);
            paperStrategyItemCategory.ItemDifficulty4Count = int.Parse(txtNd4.Text);
            paperStrategyItemCategory.ItemDifficulty5Count = int.Parse(txtNd5.Text);
            paperStrategyItemCategory.Memo = txtMemo.Text;

            paperStrategyItemCategory.ItemCategoryId = int.Parse(hfItemCategoryID.Value);
            paperStrategyItemCategory.ExcludeCategorysId = hfExCludeItemCategoryIDS.Value;

            paperStrategyItemCategory.StrategySubjectId = int.Parse(hfSubjectId.Value);

            paperStrategyItemCategory.ItemTypeId = int.Parse(ddlType.SelectedValue);
            paperStrategyItemCategory.UnitScore = decimal.Parse(txtScore.Text);
            paperStrategyItemCategory.UnitLimitTime = int.Parse(txtSeconds.Text);

            if (ViewState["mode"].ToString() == "Insert")
            {
                paperStrategyItemCategoryBLL.AddPaperStrategyItemCategory(paperStrategyItemCategory);
            }
            else if (ViewState["mode"].ToString() == "Edit")
            {
                string strId = Request.QueryString.Get("id");
                paperStrategyItemCategory.StrategyItemCategoryId = int.Parse(strId);

                paperStrategyItemCategoryBLL.UpdatePaperStrategyItemCategory(paperStrategyItemCategory);
            }

            Response.Write("<script>  window.opener.form1.submit();window.close();</script>");
        }
    }
}