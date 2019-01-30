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
    public partial class PaperManageEditSecond : PageBase
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

                PaperBLL paperBLL = new PaperBLL();
                RailExam.Model.Paper paper = paperBLL.GetPaper(int.Parse(strId));
                if (paper != null)
                {
                    txtPaperName.Text = paper.PaperName;
                }

                Grid1.DataBind();
            }
            else
            {
                string strDeleteID = Request.Form.Get("DeleteID");
                if (!string.IsNullOrEmpty(strDeleteID))
                {
                    int nID = Int32.Parse(strDeleteID);

                    PaperSubjectBLL paperSubjectBLL = new PaperSubjectBLL();
                    paperSubjectBLL.DeletePaperSubject(nID);

                    Grid1.DataBind();
                }
            }
        }

        protected void btnSaveAndNext_Click(object sender, EventArgs e)
        {
            string strId = Request.QueryString.Get("id");
            string strMode = ViewState["mode"].ToString();

            if (Grid1.Rows.Count == 0)
            {
                SessionSet.PageMessage = "请选择大题！";
                return;
            }

            if (strMode != "ReadOnly")
            {
                
                int totalScore = 0;

                IList<PaperSubject> paperSubjects = new List<PaperSubject>();

                for (int i = 0; i < Grid1.Rows.Count; i++)
                {
                    string strPaperSubjectId = ((HiddenField)Grid1.Rows[i].FindControl("hfPaperSubjectId")).Value;
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

                    PaperSubject paperSubject = new PaperSubject();

                    paperSubject.PaperSubjectId = int.Parse(strPaperSubjectId);
                    paperSubject.PaperId = int.Parse(strId);
                    paperSubject.ItemCount = int.Parse(strItemCount);
                    paperSubject.ItemTypeId = int.Parse(strItemTypeId);
                    paperSubject.OrderIndex = 0;
                    paperSubject.Remark = "";
                    paperSubject.SubjectName = strSubjectName;
                    paperSubject.UnitScore = int.Parse(strUnitScore);
                    paperSubject.TotalScore = int.Parse(strUnitScore);
                    paperSubject.Memo = "";

                    paperSubjects.Add(paperSubject);
                }

                PaperSubjectBLL paperSubjectBLL = new PaperSubjectBLL();
                paperSubjectBLL.UpdatePaperSubject(int.Parse(strId), paperSubjects);
            }

            Response.Redirect("PaperManageChooseItem.aspx?mode=" + strMode + "&id=" + strId);
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

            PaperSubjectBLL paperSubjectBLL = new PaperSubjectBLL();
            PaperSubject paperSubject = new PaperSubject();

            paperSubject.PaperId = int.Parse(strId);
            paperSubject.PaperSubjectId = Grid1.Rows.Count + 1;
            paperSubject.ItemTypeId = int.Parse(lbType.SelectedValue);
            paperSubject.TypeName = lbType.SelectedItem.Text;
            paperSubject.SubjectName = lbType.SelectedItem.Text;
            paperSubject.UnitScore = 0;
            paperSubject.TotalScore = 0;
            paperSubject.Memo = "";
            paperSubject.ItemCount = 0;
            paperSubject.OrderIndex = 0;
            paperSubject.Remark = "";

            paperSubjectBLL.AddPaperSubject(paperSubject);

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
            IList<PaperSubject> paperSubjects = new List<PaperSubject>();

            for (int i = 0; i < Grid1.Rows.Count; i++)
            {
                string strPaperSubjectId = ((HiddenField)Grid1.Rows[i].FindControl("hfPaperSubjectId")).Value;
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

                PaperSubject paperSubject = new PaperSubject();

                paperSubject.PaperSubjectId = int.Parse(strPaperSubjectId);
                paperSubject.PaperId = int.Parse(strId);
                paperSubject.ItemCount = int.Parse(strItemCount);
                paperSubject.ItemTypeId = int.Parse(strItemTypeId);
                paperSubject.OrderIndex = 0;
                paperSubject.Remark = "";
                paperSubject.SubjectName = strSubjectName;
                paperSubject.UnitScore = int.Parse(strUnitScore);
                paperSubject.TotalScore = int.Parse(strUnitScore);
                paperSubject.Memo = "";

                paperSubjects.Add(paperSubject);
            }

            PaperSubjectBLL paperSubjectBLL = new PaperSubjectBLL();
            paperSubjectBLL.UpdatePaperSubject(int.Parse(strId), paperSubjects);

            Grid1.DataBind();
        }
    }
}