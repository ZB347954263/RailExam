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
using RailExam.DAL;
using RailExam.Model;
using RailExam.BLL;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DSunSoft.Data;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using Oracle.DataAccess.Client;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Online
{
    public partial class ExamList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(PrjPub.StudentID))
            {
                Response.Write("<script>alert('����û�е�¼�����ܲ鿴���߿�����Ϣ��');window.close();</script>");
                return;
            }

            if (!IsPostBack)
            {
                ImageButton0_Click(null, null);
            }
        }
         

        protected void ImageButton0_Click(object sender, ImageClickEventArgs e)
        {
            this.labelTitle.Text = "��ǰ����";
            Grid1.Visible = true;
            Grid2.Visible = false;
            Grid3.Visible = false;
            Grid4.Visible = false;

            string struesrId = PrjPub.StudentID;
            ExamBLL bkLL = new ExamBLL();
            IList<RailExam.Model.Exam> ExamList = bkLL.GetExamByUserId(struesrId,0,PrjPub.ServerNo);
            this.Grid1.DataSource = ExamList;
            this.Grid1.DataBind();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            this.labelTitle.Text = "��������";
            Grid1.Visible = false;
            Grid2.Visible = true;
            Grid3.Visible = false;
            Grid4.Visible = false;
            string struesrId = PrjPub.StudentID;
            ExamBLL bkLL = new ExamBLL();
            IList<RailExam.Model.Exam> ExamList1 = bkLL.GetComingExamByUserId(struesrId);

            this.Grid2.DataSource = ExamList1;
            this.Grid2.DataBind();
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            this.labelTitle.Text = "��ʷ����";
            Grid1.Visible = false;
            Grid2.Visible = false;
            Grid3.Visible = true;
            Grid4.Visible = false;

            string struesrId = PrjPub.StudentID;
            ExamBLL bkLL = new ExamBLL();
            IList<RailExam.Model.Exam> ExamList2 = bkLL.GetHistoryExamByUserId(struesrId);

            this.Grid3.DataSource = ExamList2;
            this.Grid3.DataBind();

        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            this.labelTitle.Text = "���Գɼ�";
            Grid1.Visible = false;
            Grid2.Visible = false;
            Grid3.Visible = false;
            Grid4.Visible = true;

            string struesrId = PrjPub.StudentID;
            ExamResultBLL bkLL = new ExamResultBLL();
            IList<RailExam.Model.ExamResult> ExamList2 = bkLL.GetExamResults(struesrId, 8000);

            this.Grid4.DataSource = ExamList2;
            this.Grid4.DataBind();
        }
    }
}
