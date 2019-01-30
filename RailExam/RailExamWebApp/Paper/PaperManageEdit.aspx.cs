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
using RailExamWebApp.Common.Class;


namespace RailExamWebApp.Paper
{
    public partial class PaperManageEdit : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (PrjPub.CurrentLoginUser == null)
                {
                    Response.Redirect("/RailExamBao/Common/Error.aspx?error=Session过期请重新登录本系统！");
                    return;
                }
                ViewState["mode"] = Request.QueryString.Get("mode");
                hfMode.Value = ViewState["mode"].ToString();
                string PaperCategoryID = Request.QueryString.Get("PaperCategoryIDPath");
                if (!string.IsNullOrEmpty(PaperCategoryID))
                {
                    string[] str1 = PaperCategoryID.Split(new char[] { '/' });

                    int nID = int.Parse(str1[str1.LongLength - 1].ToString());


                    hfCategoryId.Value = nID.ToString();
                    PaperCategoryBLL pcl = new PaperCategoryBLL();

                    RailExam.Model.PaperCategory pc = pcl.GetPaperCategory(nID);

                    txtCategoryName.Text = pc.CategoryName;
                }
                string strPaperID = Request.QueryString.Get("id");
                if (!string.IsNullOrEmpty(strPaperID))
                {
                    FillPage(int.Parse(strPaperID));
                }
            }
        }

        protected void FillPage(int nId)
        {
            PaperBLL paperBLL = new PaperBLL();
            RailExam.Model.Paper paper = paperBLL.GetPaper(nId);

            if (paper != null)
            {
                txtCategoryName.Text = paper.CategoryName;
                ddlMode.SelectedValue = paper.CreateMode.ToString();

                PaperSubjectBLL paperSubjectBLL = new PaperSubjectBLL();

                IList<PaperSubject> psList = paperSubjectBLL.GetPaperSubjectByPaperId(paper.PaperId);

                if (psList.Count > 0)
                {
                    ddlMode.Enabled = false;
                    ddlStrategyMode.Enabled = false;                     
                }
                else
                {
                    ddlMode.Enabled = true;
                    ddlStrategyMode.Enabled = true;
                }

                txtPaperName.Text = paper.PaperName;
                hfCategoryId.Value = paper.CategoryId.ToString();
                txtDescription.Text = paper.Description;
                txtMemo.Text = paper.Memo;

                if (ddlMode.SelectedValue == "3")
                {
                    ddlStrategyMode.Visible = true;
                    LabelMode.Visible = true;
                }
            }

            if (ViewState["mode"].ToString() == "ReadOnly")
            {
                txtPaperName.Enabled = false;
                txtDescription.Enabled = false;
                txtMemo.Enabled = false;
            }
        }

        protected void btnSaveAndNext_Click(object sender, EventArgs e)
        {
            PaperBLL paperBLL = new PaperBLL();
            RailExam.Model.Paper paper = new RailExam.Model.Paper();

            string strID = string.Empty;
            string strMode = ViewState["mode"].ToString();

            if (strMode == "Insert")
            {
                paper.CategoryId = int.Parse(hfCategoryId.Value);
                paper.PaperName = txtPaperName.Text;
                paper.Description = txtDescription.Text;
                paper.Memo = txtMemo.Text;
                paper.CreatePerson = PrjPub.CurrentLoginUser.EmployeeName;
                paper.CreateMode = int.Parse(ddlMode.SelectedValue);
                paper.CreateTime = DateTime.Now;
                paper.ItemCount = 0;
                paper.StatusId = 1;
                paper.TotalScore = 0;
                paper.OrgId = PrjPub.CurrentLoginUser.StationOrgID;

                int id = paperBLL.AddPaper(paper);

                strID = id.ToString();

                if (ddlMode.SelectedValue == "1")
                {
                    Response.Redirect("PaperManageEditSecond.aspx?mode=" + strMode + "&id=" + strID);
                }
                else if (ddlMode.SelectedValue == "2")
                {
                    Response.Redirect("PaperManageStrategy.aspx?mode=" + strMode + "&id=" + strID);
                }
                else
                {   
                    PaperStrategyBLL paperStrategyBLL = new PaperStrategyBLL();
                    RailExam.Model.PaperStrategy paperStrategy = new RailExam.Model.PaperStrategy();

                    paperStrategy.PaperCategoryId = int.Parse(hfCategoryId.Value);
                    paperStrategy.PaperStrategyName = "temp";
                    paperStrategy.StrategyMode = int.Parse(ddlStrategyMode.SelectedValue);
                    paperStrategy.SingleAsMultiple = false;
                    paperStrategy.IsRandomOrder = false;
                    paperStrategy.Description = "";
                    paperStrategy.Memo = "";

                    string strStrategyID = paperStrategyBLL.AddPaperStrategy(paperStrategy).ToString();
                    Response.Redirect("PaperStrategyEditSecond.aspx?mode=" + strMode + "&id=" + strStrategyID + "&Paperid=" + strID);                    
                }
            }
            else
            {
                strID = Request.QueryString.Get("id");

                paper.PaperName = txtPaperName.Text;
                paper.PaperId = int.Parse(strID);
                paper.Memo = txtMemo.Text;
                paper.Description = txtDescription.Text;
                paper.TotalScore = 0;

                paperBLL.UpdatePaper(paper);

                if (ddlMode.SelectedValue == "2")
                {
                    PaperSubjectBLL paperSubjectBLL = new PaperSubjectBLL();

                    IList<PaperSubject> psList = paperSubjectBLL.GetPaperSubjectByPaperId(int.Parse(strID));

                    if (psList.Count > 0)
                    {
                        Response.Redirect("PaperManageEditSecond.aspx?mode=" + strMode + "&id=" + strID);
                    }
                    else
                    {
                        Response.Redirect("PaperManageStrategy.aspx?mode=" + strMode + "&id=" + strID);
                    }
                }
                else if (ddlMode.SelectedValue == "3")
                {
                    PaperSubjectBLL paperSubjectBLL = new PaperSubjectBLL();

                    IList<PaperSubject> psList = paperSubjectBLL.GetPaperSubjectByPaperId(int.Parse(strID));

                    if (psList.Count > 0)
                    {
                        Response.Redirect("PaperManageEditSecond.aspx?mode=" + strMode + "&id=" + strID);
                    }
                    else
                    {
                        string strStrategyID = Request.QueryString.Get("Strategyid");
                        if (!string.IsNullOrEmpty(strStrategyID))
                        {
                            Response.Redirect("PaperStrategyEditSecond.aspx?mode=" + strMode + "&id=" + strStrategyID + "&Paperid=" + strID);
                        }
                        else
                        {
                            PaperStrategyBLL paperStrategyBLL = new PaperStrategyBLL();
                            RailExam.Model.PaperStrategy paperStrategy = new RailExam.Model.PaperStrategy();

                            paperStrategy.PaperCategoryId = int.Parse(hfCategoryId.Value);
                            paperStrategy.PaperStrategyName = "temp";
                            paperStrategy.StrategyMode = int.Parse(ddlStrategyMode.SelectedValue);
                            paperStrategy.SingleAsMultiple = false;
                            paperStrategy.IsRandomOrder = false;
                            paperStrategy.Description = "";
                            paperStrategy.Memo = "";
                            strStrategyID = paperStrategyBLL.AddPaperStrategy(paperStrategy).ToString();
                            Response.Redirect("PaperStrategyEditSecond.aspx?mode=" + strMode + "&id=" + strStrategyID + "&Paperid=" + strID);
                        }
                    }               
                }
                else
                {
                    Response.Redirect("PaperManageEditSecond.aspx?mode=" + strMode + "&id=" + strID);
                }

            }           
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>top.window.close();</script>");
        }

        protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMode.SelectedValue == "3")
            {
                ddlStrategyMode.Visible = true;
                LabelMode.Visible = true;
            }
        }
    }
}