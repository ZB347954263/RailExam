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

namespace RailExamWebApp.Exam
{
    public partial class ExamManageSecond : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["mode"] = Request.QueryString.Get("mode");
                hfMode.Value = ViewState["mode"].ToString();

                string strId = Request.QueryString.Get("id");

                ExamBLL examBLL = new ExamBLL();

                RailExam.Model.Exam exam = examBLL.GetExam(int.Parse(strId));

                if (exam != null)
                {
                    txtPaperName.Text = exam.ExamName;
                    if (exam.paperId != 0)
                    {
                        PaperBLL paperBLL = new PaperBLL();

                        IList<RailExam.Model.Paper> paperList = paperBLL.GetPaperByPaperId(exam.paperId);

                        if (paperList != null)
                        {
                            Grid1.DataSource = paperList;
                            Grid1.DataBind();
                        }
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(hfPaperId.Value))
                {
                    PaperBLL paperBLL = new PaperBLL();

                    IList<RailExam.Model.Paper> paperList = paperBLL.GetPaperByPaperId(int.Parse(hfPaperId.Value));

                    if (paperList != null)
                    {
                        Grid1.DataSource = paperList;
                        Grid1.DataBind();
                    }
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


            Response.Redirect("ExamManageFirst.aspx?mode=" + strFlag + "&id=" + strId);
        }

        protected void btnSaveAndNext_Click(object sender, EventArgs e)
        {
            string strId = Request.QueryString.Get("id");
            if (Grid1.Items.Count == 0)
            {
                SessionSet.PageMessage = "请先选择试卷！";
                return;
            }

            if (!string.IsNullOrEmpty(hfPaperId.Value))
            {
                //先删除后新增
                ExamBLL examBLL = new ExamBLL();
                examBLL.UpdateExamPaper(int.Parse(strId), int.Parse(hfPaperId.Value));
            }

            Response.Redirect("SelectEmployeeDetail.aspx?mode=" + ViewState["mode"].ToString() + "&id=" + strId);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>top.window.close();</script>");
        }
    }
}
