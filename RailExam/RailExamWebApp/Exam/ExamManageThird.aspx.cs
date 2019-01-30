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
using RailExamWebApp.Common.Control.Date;

namespace RailExamWebApp.Exam
{
    public partial class ExamManageThird : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["mode"] = Request.QueryString.Get("mode");
                hfMode.Value = ViewState["mode"].ToString();

                Grid1.DataBind();
            }
            else
            {
                string strDeleteID = Request.Form.Get("DeleteID");
                if (!string.IsNullOrEmpty(strDeleteID))
                {
                    int nID = Int32.Parse(strDeleteID);

                    ExamArrangeBLL examArrangeBLL = new ExamArrangeBLL();
                    examArrangeBLL.DeleteExamArrange(nID);

                    Grid1.DataBind();
                }

                string strUserIds = Request.Form.Get("UserIds");
                if (!string.IsNullOrEmpty(strUserIds))
                {
                    string ExamArrangeId = Request.Form.Get("ExamArrangeId");
                    ExamArrangeBLL examArrangeBLL = new ExamArrangeBLL();
                    examArrangeBLL.UpdateExamArrangeUser(int.Parse(ExamArrangeId), strUserIds);
                    Grid1.DataBind();
                }

                string strJudgeIds = Request.Form.Get("JudgeIds");
                if (!string.IsNullOrEmpty(strJudgeIds))
                {
                    string ExamArrangeId = Request.Form.Get("ExamArrangeId");
                    ExamArrangeBLL examArrangeBLL = new ExamArrangeBLL();
                    examArrangeBLL.UpdateExamArrangeJudge(int.Parse(ExamArrangeId), strJudgeIds);
                    Grid1.DataBind();
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

            Response.Redirect("ExamManageSecond.aspx?mode=" + strFlag + "&id=" + strId);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Grid1.Rows.Count == 0)
            {
                SessionSet.PageMessage = "请选择考生、指定评卷人！";
                return;
            }

            string strId = Request.QueryString.Get("id");

            IList<ExamArrange> examArranges = new List<ExamArrange>();

            for (int i = 0; i < Grid1.Rows.Count; i ++)
            {
                string strExamArrangeId = ((HiddenField)Grid1.Rows[i].FindControl("hfExamArrangeId")).Value;
                string strBeginTime = ((DateTimeUC)Grid1.Rows[i].FindControl("dateBeginTime")).DateValue.ToString();
                string strEndTime = ((DateTimeUC)Grid1.Rows[i].FindControl("dateEndtime")).DateValue.ToString();
                string strUserIds = ((TextBox)Grid1.Rows[i].FindControl("txtUserIds")).Text;
                string strJudgeIds = ((TextBox)Grid1.Rows[i].FindControl("txtJudgeIds")).Text;

                ExamArrange examArrange = new ExamArrange();

                examArrange.ExamId = int.Parse(strId);
                examArrange.ExamArrangeId = int.Parse(strExamArrangeId);
                examArrange.UserIds = strUserIds;
                examArrange.JudgeIds = strJudgeIds;
                examArrange.BeginTime = DateTime.Parse(strBeginTime);
                examArrange.EndTime = DateTime.Parse(strEndTime);

                examArranges.Add(examArrange);
            }

            ExamArrangeBLL examArrangeBLL = new ExamArrangeBLL();
            examArrangeBLL.UpdateExamArrange(examArranges);

            Response.Write("<script>top.window.opener.form1.Refresh.value='true';top.window.opener.form1.submit();top.window.close();</script>");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>top.window.close();</script>");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string strId = Request.QueryString.Get("id");
            int i = Grid1.Rows.Count;

            ExamArrange examArrange = new ExamArrange();

            ExamBLL examBLL = new ExamBLL();

            RailExam.Model.Exam exam = examBLL.GetExam(int.Parse(strId));

            if (exam != null)
            {
                examArrange.ExamId = int.Parse(strId);
                examArrange.PaperId = exam.paperId;
                examArrange.UserIds = "选择考生！";
                examArrange.JudgeIds = "选择评卷人！";
                examArrange.BeginTime = DateTime.Now;
                examArrange.EndTime = DateTime.Now.AddHours(1);
                examArrange.OrderIndex = 1;
                examArrange.Memo = "";

                ExamArrangeBLL examArrangeBLL = new ExamArrangeBLL();
                examArrangeBLL.AddExamArrange(examArrange);

                Grid1.DataBind();
            }
        }
    }
}
