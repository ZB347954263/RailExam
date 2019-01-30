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

namespace RailExamWebApp.Exam
{
    public partial class ExamResultUpdateFirst : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                string strId = Request.QueryString.Get("id");
                if (strId != null && strId!="")
                {
                    ViewState["UpdateID"]=strId;
                    ExamResultUpdateBLL eruBll = new ExamResultUpdateBLL();
                    ExamResultUpdate examResultUpdate = eruBll.GetExamResultUpdate(int.Parse(strId));
                    if (examResultUpdate!=null)
                    {
                        this.lblExamineeName.Text = examResultUpdate.EmployeeName;
                        this.LabelExamName.Text = examResultUpdate.ExamName;
                        this.TextBoxCause.Text = examResultUpdate.updateCause;
                        this.LabelTime.Text = examResultUpdate.updateDate.ToString("yyyy-MM-dd"); ;
                        this.LabelWorkMan.Text = examResultUpdate.updatePerson;
                        this.TextBoxRemark.Text = examResultUpdate.Memo;
                        this.LabelNewScore.Text = examResultUpdate.newScore.ToString()+"·Ö";
                        this.LabelOldScore.Text = examResultUpdate.oldScore.ToString()+ "·Ö";
                    }
                }

                string strmode = Request.QueryString.Get("mode");
                if (strmode != null && strmode == "ReadOnly")
                {
                    this.btnsave.Visible = false;
                }
            }
        }

        protected void btnsave_Click(object sender, ImageClickEventArgs e)
        {
            ExamResultUpdateBLL eruBll = new ExamResultUpdateBLL();
            eruBll.UpdateExamResultUpdate(int.Parse(ViewState["UpdateID"].ToString()),TextBoxCause.Text,TextBoxRemark.Text);

            Response.Write("<script>top.window.opener.form1.Refresh.value='true';top.window.opener.form1.submit();top.window.close();</script>");
             
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>top.window.close();</script>");
        }
    }
}
