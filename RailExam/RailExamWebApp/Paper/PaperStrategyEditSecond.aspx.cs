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
    public partial class PaperStrategyEditSecond : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["mode"] = Request.QueryString.Get("mode");

                hfMode.Value = ViewState["mode"].ToString();

                if (ViewState["mode"].ToString() == "ReadOnly")
                {
                    btnSaveAs.Visible = false;
                    btnInput.Enabled = false;
                }

                string strId = Request.QueryString.Get("id");
                string strPaperID = Request.QueryString.Get("Paperid");
                if (!string.IsNullOrEmpty(strPaperID))
                {
                    HfstrategyID.Value = strId;
                    Hfpaperid.Value = strPaperID;
                }

                PaperStrategyBLL paperStrategyBLL = new PaperStrategyBLL();
                RailExam.Model.PaperStrategy paperStrategy = paperStrategyBLL.GetPaperStrategy(int.Parse(strId));
                if (paperStrategy != null)
                {
                    txtPaperName.Text = paperStrategy.PaperStrategyName;
                }

                Grid1.DataBind();
            }
            else
            {
                string strDeleteID = Request.Form.Get("DeleteID");
                if (!string.IsNullOrEmpty(strDeleteID))
                {
                    int nID = Int32.Parse(strDeleteID);

                    PaperStrategySubjectBLL paperStrategySubjectBLL = new PaperStrategySubjectBLL();
                    paperStrategySubjectBLL.DeletePaperStrategySubject(nID);

                    Grid1.DataBind();
                }
            }
        }

        protected void btnSaveAndNext_Click(object sender, EventArgs e)
        {
            string strId = Request.QueryString.Get("id");
            string strMode = ViewState["mode"].ToString();

            if (strMode != "ReadOnly")
            {
                if (Grid1.Rows.Count == 0)
                {
                    SessionSet.PageMessage = "请选择大题！";
                    return;
                }

                int totalScore = 0;

                IList<PaperStrategySubject> paperStrategySubjects = new List<PaperStrategySubject>();

                for (int i = 0; i < Grid1.Rows.Count; i++)
                {
                    string strPaperStrategySubjectId = ((HiddenField)Grid1.Rows[i].FindControl("hfPaperStrategySubjectId")).Value;
                    string strItemTypeId = ((HiddenField)Grid1.Rows[i].FindControl("hfItemTypeId")).Value;
                    string strSubjectName = ((TextBox)Grid1.Rows[i].FindControl("txtSubjectName")).Text;
                    string strUnitScore = ((TextBox)Grid1.Rows[i].FindControl("txtUnitScore")).Text;
                    string strItemCount = ((TextBox)Grid1.Rows[i].FindControl("txtItemCount")).Text;

                    if (strUnitScore == "")
                    {
                        strUnitScore = "0";
                    }

                    if (strItemCount == "")
                    {
                        strItemCount = "0";
                    }


                    totalScore += int.Parse(strUnitScore);

                    PaperStrategySubject paperStrategySubject = new PaperStrategySubject();

                    paperStrategySubject.PaperStrategySubjectId = int.Parse(strPaperStrategySubjectId);
                    paperStrategySubject.PaperStrategyId = int.Parse(strId);
                    paperStrategySubject.ItemCount = int.Parse(strItemCount);
                    paperStrategySubject.ItemTypeId = int.Parse(strItemTypeId);
                    paperStrategySubject.OrderIndex = 0;
                    paperStrategySubject.Remark = "";
                    paperStrategySubject.SubjectName = strSubjectName;
                    paperStrategySubject.UnitScore = int.Parse(strUnitScore);
                    paperStrategySubject.TotalScore = int.Parse(strUnitScore);
                    paperStrategySubject.Memo = "";

                    paperStrategySubjects.Add(paperStrategySubject);
                }

                PaperStrategySubjectBLL paperStrategySubjectBLL = new PaperStrategySubjectBLL();

                paperStrategySubjectBLL.UpdatePaperStrategySubject(int.Parse(strId), totalScore, paperStrategySubjects);
            }

            PaperStrategyBLL paperStrategyBLL = new PaperStrategyBLL();
            RailExam.Model.PaperStrategy paperStrategy = paperStrategyBLL.GetPaperStrategy(int.Parse(strId));

            if (paperStrategy.StrategyMode == 2)   //按教材

            {
                string strPaperID = Request.QueryString.Get("Paperid");
                if (!string.IsNullOrEmpty(strPaperID))
                {
                    Response.Redirect("PaperStrategyEditThird.aspx?mode=" + strMode + "&id=" + strId + "&Paperid=" + strPaperID);

                }
                else
                {
                    Response.Redirect("PaperStrategyEditThird.aspx?mode=" + strMode + "&id=" + strId);
                }
            }
            if (paperStrategy.StrategyMode == 3)   //按试题辅助分类

            {
                string strPaperID = Request.QueryString.Get("Paperid");
                if (!string.IsNullOrEmpty(strPaperID))
                {
                    Response.Redirect("PaperStrategyItemThird.aspx?mode=" + strMode + "&id=" + strId + "&Paperid=" + strPaperID);

                }
                else
                {
                    Response.Redirect("PaperStrategyItemThird.aspx?mode=" + strMode + "&id=" + strId);
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


            string strPaperID = Request.QueryString.Get("Paperid");
            if (!string.IsNullOrEmpty(strPaperID))
            {
                Response.Redirect("PaperManageEdit.aspx?mode=" + strFlag + "&Strategyid=" + strId + "&id=" + strPaperID);

            }
            else
            {
                Response.Redirect("PaperStrategyEdit.aspx?mode=" + strFlag + "&id=" + strId);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>top.window.close();</script>");
        }

        protected void btnInput_Click(object sender, EventArgs e)
        {
            if (lbType.SelectedIndex < 0)
            {
                SessionSet.PageMessage = "请先选题型！";
                return;
            }

            string strId = Request.QueryString.Get("id");

            PaperStrategySubjectBLL paperStrategySubjectBLL = new PaperStrategySubjectBLL();
            PaperStrategySubject paperStrategySubject = new PaperStrategySubject();

            paperStrategySubject.PaperStrategyId = int.Parse(strId);
            paperStrategySubject.PaperStrategySubjectId = Grid1.Rows.Count + 1;
            paperStrategySubject.ItemTypeId = int.Parse(lbType.SelectedValue);
            paperStrategySubject.TypeName = lbType.SelectedItem.Text;
            paperStrategySubject.SubjectName = lbType.SelectedItem.Text;
            paperStrategySubject.UnitScore = 0;
            paperStrategySubject.TotalScore = 0;
            paperStrategySubject.Memo = "";
            paperStrategySubject.ItemCount = 0;
            paperStrategySubject.OrderIndex = 0;
            paperStrategySubject.Remark = "";

            paperStrategySubjectBLL.AddPaperStrategySubject(paperStrategySubject);

            Grid1.DataBind();
        }

        protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ViewState["mode"].ToString() == "ReadOnly")
                {
                    ((TextBox)e.Row.FindControl("txtSubjectName")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtUnitScore")).Enabled = false;
                }

                ((Label)e.Row.FindControl("lblID")).Text = (e.Row.RowIndex + 1).ToString();
            }
        }

        protected void btnSaveAs_Click(object sender, EventArgs e)
        {
            int totalScore = 0;
            string strId = Request.QueryString.Get("id");
            IList<PaperStrategySubject> paperStrategySubjects = new List<PaperStrategySubject>();

            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string strPaperStrategySubjectId = ((HiddenField)Grid1.Rows[i].FindControl("hfPaperStrategySubjectId")).Value;
                string strItemTypeId = ((HiddenField)Grid1.Rows[i].FindControl("hfItemTypeId")).Value;
                string strSubjectName = ((TextBox)Grid1.Rows[i].FindControl("txtSubjectName")).Text;
                string strUnitScore = ((TextBox)Grid1.Rows[i].FindControl("txtUnitScore")).Text;
                string strItemCount = ((TextBox)Grid1.Rows[i].FindControl("txtItemCount")).Text;

                if (strUnitScore == "")
                {
                    strUnitScore = "0";
                }

                if (strItemCount == "")
                {
                    strItemCount = "0";
                }

                totalScore += int.Parse(strUnitScore);

                PaperStrategySubject paperStrategySubject = new PaperStrategySubject();

                paperStrategySubject.PaperStrategySubjectId = int.Parse(strPaperStrategySubjectId);
                paperStrategySubject.PaperStrategyId = int.Parse(strId);
                paperStrategySubject.ItemCount = int.Parse(strItemCount);
                paperStrategySubject.ItemTypeId = int.Parse(strItemTypeId);
                paperStrategySubject.OrderIndex = 0;
                paperStrategySubject.Remark = "";
                paperStrategySubject.SubjectName = strSubjectName;
                paperStrategySubject.UnitScore = int.Parse(strUnitScore);
                paperStrategySubject.TotalScore = int.Parse(strUnitScore);
                paperStrategySubject.Memo = "";

                paperStrategySubjects.Add(paperStrategySubject);
            }

            PaperStrategySubjectBLL paperStrategySubjectBLL = new PaperStrategySubjectBLL();
            paperStrategySubjectBLL.UpdatePaperStrategySubject(int.Parse(strId), totalScore, paperStrategySubjects);

            Grid1.DataBind();
        }
    }
}