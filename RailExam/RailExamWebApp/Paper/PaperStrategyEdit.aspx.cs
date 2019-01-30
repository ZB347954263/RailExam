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
    public partial class PaperStrategyEdit : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["mode"] = Request.QueryString.Get("mode");
                hfMode.Value = ViewState["mode"].ToString();


                string PaperCategoryID = Request.QueryString.Get("PaperCategoryIDPath");
                if (!string.IsNullOrEmpty(PaperCategoryID))
                {
                    string[] str1 = PaperCategoryID.Split(new char[] { '/' });

                    int nID = int.Parse(str1[str1.LongLength - 1].ToString());


                    hfPaperCategoryID.Value = nID.ToString();
                    PaperCategoryBLL pcl = new PaperCategoryBLL();

                    RailExam.Model.PaperCategory pc = pcl.GetPaperCategory(nID);

                    txtCategoryName.Text = pc.CategoryName;
                }


                string strPaperStrategyID = Request.QueryString.Get("id");
                if (!string.IsNullOrEmpty(strPaperStrategyID))
                {
                    FillPage(int.Parse(strPaperStrategyID));
                }
            }
        }

        protected void FillPage(int nPaperStrategyID)
        {
            PaperStrategyBLL paperStrategyBLL = new PaperStrategyBLL();

            RailExam.Model.PaperStrategy paperStrategy = paperStrategyBLL.GetPaperStrategy(nPaperStrategyID);

            if (paperStrategy != null)
            {
                txtCategoryName.Text = paperStrategy.CategoryName;
                txtPaperStrategyName.Text = paperStrategy.PaperStrategyName;
                hfPaperCategoryID.Value = paperStrategy.PaperCategoryId.ToString();
                ddlStrategyMode.SelectedValue = paperStrategy.StrategyMode.ToString();
                ddlStrategyMode.Enabled = false;

                chDisplay.Checked = paperStrategy.SingleAsMultiple;
                chChoose.Checked = paperStrategy.IsRandomOrder;
                txtDescription.Text = paperStrategy.Description;
                txtMemo.Text = paperStrategy.Memo;
            }

            if (ViewState["mode"].ToString() == "ReadOnly")
            {
                txtPaperStrategyName.Enabled = false;
                chDisplay.Enabled = false;
                chChoose.Enabled = false;
                txtDescription.Enabled = false;
                txtMemo.Enabled = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            PaperStrategyBLL paperStrategyBLL = new PaperStrategyBLL();
            RailExam.Model.PaperStrategy paperStrategy = new RailExam.Model.PaperStrategy();

            string strID = string.Empty;
            string strMode = ViewState["mode"].ToString();

            if (strMode == "Insert")
            {
                paperStrategy.PaperCategoryId = int.Parse(hfPaperCategoryID.Value);
                paperStrategy.PaperStrategyName = txtPaperStrategyName.Text;
                paperStrategy.StrategyMode = int.Parse(ddlStrategyMode.SelectedValue);
                paperStrategy.SingleAsMultiple = chDisplay.Checked;
                paperStrategy.IsRandomOrder = chChoose.Checked;
                paperStrategy.Description = txtDescription.Text;
                paperStrategy.Memo = txtMemo.Text;

                strID = paperStrategyBLL.AddPaperStrategy(paperStrategy).ToString();
            }
            else if (strMode == "Edit")
            {
                strID = Request.QueryString.Get("id");

                paperStrategy.PaperCategoryId = int.Parse(hfPaperCategoryID.Value);
                paperStrategy.PaperStrategyName = txtPaperStrategyName.Text;
                paperStrategy.PaperStrategyId = int.Parse(strID);
                paperStrategy.StrategyMode = int.Parse(ddlStrategyMode.SelectedValue);
                paperStrategy.SingleAsMultiple = chDisplay.Checked;
                paperStrategy.IsRandomOrder = chChoose.Checked;
                paperStrategy.Description = txtDescription.Text;
                paperStrategy.Memo = txtMemo.Text;

                paperStrategyBLL.UpdatePaperStrategy(paperStrategy);
            }
            else
            {
                strID = Request.QueryString.Get("id");
            }
            string strPaperID = Request.QueryString.Get("Paperid");
            if (!string.IsNullOrEmpty(strPaperID))
            {
                Response.Redirect("PaperStrategyEditSecond.aspx?mode=" + strMode + "&id=" + strID + "&Paperid=" + strPaperID);

            }
            else
            {
                Response.Redirect("PaperStrategyEditSecond.aspx?mode=" + strMode + "&id=" + strID);
            }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>top.window.close();</script>");
        }
    }
}