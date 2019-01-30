using System;
using System.Collections.Generic;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Exam
{
    public partial class ExamManageFirst : PageBase
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

                dateBeginTime.DateValue = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dateEndTime.DateValue = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd HH:mm:ss");

                string ExamCategoryID = Request.QueryString.Get("ExamCategoryIDPath");

                if (ExamCategoryID=="0")
                {
                    ExamCategoryID = "/1";
                }

                if (!string.IsNullOrEmpty(ExamCategoryID))
                {
                    string[] str1 = ExamCategoryID.Split(new char[] { '/' });

                    int nID = int.Parse(str1[str1.LongLength - 1].ToString());

                    hfCategoryId.Value = nID.ToString();
                    ExamCategoryBLL pcl = new ExamCategoryBLL();

                    RailExam.Model.ExamCategory pc = pcl.GetExamCategory(nID);

                    txtCategoryName.Text = pc.CategoryName;
                }


                string strExamID = Request.QueryString.Get("id");
                if (!string.IsNullOrEmpty(strExamID))
                {
                    FillPage(int.Parse(strExamID));
                }
            }
        }

        protected void FillPage(int nExamID)
        {
            ExamBLL examBLL = new ExamBLL();
            RailExam.Model.Exam exam = examBLL.GetExam(nExamID);

            if (exam != null)
            {
                if(ViewState["mode"].ToString() == "Edit")
                {
                    if (exam.Downloaded == 1)
                    {
                        Response.Write("<script>alert('该考试已被下载，不能被编辑！');window.close();</script>");
                    }
                }
                txtCategoryName.Text = exam.CategoryName;
                hfCategoryId.Value = exam.CategoryId.ToString();
                ddlType.SelectedValue = exam.ExamTypeId.ToString();
                txtExamName.Text = exam.ExamName;
                txtExamTime.Text = exam.ExamTime.ToString();
                dateBeginTime.DateValue = exam.BeginTime.ToString("yyyy-MM-dd HH:mm:ss");
                dateEndTime.DateValue = exam.EndTime.ToString("yyyy-MM-dd HH:mm:ss");
                if (exam.ExamModeId == 1)
                {
                    rbExamMode1.Checked = true;
                }
                else
                {
                    rbExamMode2.Checked = true;
                }

                chUD.Checked = exam.IsUnderControl;
                chAutoScore.Checked = exam.IsAutoScore;
                chSeeAnswer.Checked = exam.CanSeeAnswer;
                chSeeScore.Checked = exam.CanSeeScore;
                chPublicScore.Checked = exam.IsPublicScore;
                txtDescription.Text = exam.Description;
                txtMemo.Text = exam.Memo;

                lblCreatePerson.Text = exam.CreatePerson;
                lblCreateTime.Text = exam.CreateTime.ToString("yyyy-MM-dd HH:mm");

                ExamResultBLL reBll = new ExamResultBLL();

                try
                {
                    if (ViewState["mode"].ToString() == "Edit")
                    {
                        IList<RailExam.Model.ExamResult> examResults = reBll.GetExamResultByExamID(exam.ExamId);
                        if (examResults.Count > 0)
                        {
                            ViewState["mode"] = "ReadOnly";
                        }
                    }
                }
                catch
                {
                    Pub.ShowErrorPage("无法连接站段服务器，请检查站段服务器是否打开以及网络连接是否正常！");
                }
            }

            if (ViewState["mode"].ToString() == "ReadOnly")
            {
                txtExamName.Enabled = false;
                dateBeginTime.Enabled = false;
                this.dateEndTime.Enabled = false;
                ddlType.Enabled = false;
                txtExamTime.Enabled = false;
                rbExamMode1.Enabled = false;
                rbExamMode2.Enabled = false;
                
                chUD.Enabled = false;
                chAutoScore.Enabled = false;
                chSeeAnswer.Enabled = false;
                chSeeScore.Enabled = false;
                chPublicScore.Enabled = false;
                txtDescription.Enabled = false;
                txtMemo.Enabled = false;
                 
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        { 
            ExamBLL examBLL = new ExamBLL();
            RailExam.Model.Exam exam = new RailExam.Model.Exam();

            string strID = string.Empty;
            string strMode = ViewState["mode"].ToString();

            if (strMode == "Insert")
            {
                exam.CategoryId = int.Parse(hfCategoryId.Value);
                exam.ExamName = txtExamName.Text;
                exam.Memo = txtMemo.Text;
                exam.CreatePerson = PrjPub.CurrentLoginUser.EmployeeName;
                if (rbExamMode1.Checked)
                {
                    exam.ExamModeId = 1;
                }
                else
                {
                    exam.ExamModeId = 2;
                }

                exam.IsAutoScore = chAutoScore.Checked;
                exam.CanSeeAnswer = chSeeAnswer.Checked;
                exam.CanSeeScore = chSeeScore.Checked;
                exam.IsPublicScore = chPublicScore.Checked;
                exam.IsUnderControl = chUD.Checked;
                exam.paperId = 0;
                exam.MaxExamTimes = 1;
                exam.MinExamTimes = 0;
                exam.BeginTime = DateTime.Parse(dateBeginTime.DateValue.ToString());
                exam.EndTime = DateTime.Parse(dateEndTime.DateValue.ToString());
                exam.ExamTypeId = 1;
                exam.CreateTime = DateTime.Now;
                exam.Description = txtDescription.Text;
                exam.ExamTime = int.Parse(txtExamTime.Text);
                exam.StatusId = 1;

                exam.AutoSaveInterval = 0;
                exam.OrgId = PrjPub.CurrentLoginUser.StationOrgID;

                int id = examBLL.AddExam(exam);
                strID = id.ToString();

                Response.Redirect("ExamManageSecond.aspx?mode=" + strMode + "&id=" + strID);
            }
            else if (strMode == "Edit")
            {
                strID = Request.QueryString.Get("id");

                exam.ExamName = txtExamName.Text;
                exam.Memo = txtMemo.Text;
                exam.ExamId = int.Parse(strID);
                exam.ExamTime = int.Parse(txtExamTime.Text);
                if (rbExamMode1.Checked)
                {
                    exam.ExamModeId = 1;
                }
                else
                {
                    exam.ExamModeId = 2;
                }

                exam.BeginTime = DateTime.Parse(dateBeginTime.DateValue.ToString());
                exam.EndTime = DateTime.Parse(dateEndTime.DateValue.ToString());

                exam.IsAutoScore = chAutoScore.Checked;
                exam.CanSeeAnswer = chSeeAnswer.Checked;
                exam.CanSeeScore = chSeeScore.Checked;
                exam.IsPublicScore = chPublicScore.Checked;
                exam.IsUnderControl = chUD.Checked;
                exam.MaxExamTimes = 1;
                exam.MinExamTimes = 0;

                exam.ExamTypeId = 1;
                exam.Description = txtDescription.Text;
                exam.AutoSaveInterval = 0;
                examBLL.UpdateExam(exam);

                ExamResultBLL reBll = new ExamResultBLL();

                IList<RailExam.Model.ExamResult> examResults = reBll.GetExamResultByExamID(int.Parse(strID));
                if (examResults.Count > 0)
                {
                    Response.Redirect("SelectEmployeeDetail.aspx?mode=" + strMode + "&id=" + strID);
                }
                else
                {
                    Response.Redirect("ExamManageSecond.aspx?mode=" + strMode + "&id=" + strID);             
                }
            }
            else
            {
                strID = Request.QueryString.Get("id");
                ExamResultBLL reBll = new ExamResultBLL();

                IList<RailExam.Model.ExamResult> examResults = reBll.GetExamResultByExamID(int.Parse(strID));
                if (examResults.Count > 0)
                {
                    Response.Redirect("SelectEmployeeDetail.aspx?mode=" + strMode + "&id=" + strID);
                }
                else
                {
                    Response.Redirect("ExamManageSecond.aspx?mode=" + strMode + "&id=" + strID);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>top.window.close();</script>");
        }
    }
}
