using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;
using System.Collections.Generic;

namespace RailExamWebApp.Exam
{
    public partial class ExamKSTitle : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strId = Request.QueryString.Get("PaperId");
            string strExamId = Request.QueryString.Get("ExamId");
            ViewState["BeginTime"] = DateTime.Now.ToString();

            ViewState["OrgID"] = PrjPub.CurrentStudent.OrgID;
            ViewState["StudentID"] = PrjPub.StudentID;

            if (strId != null && strId != "")
            {
                try
                {
                    ExamResultBLL examResultBLL = new ExamResultBLL();
                    RailExam.Model.ExamResult examResults = examResultBLL.GetExamResult(int.Parse(strId), int.Parse(strExamId), PrjPub.CurrentStudent.EmployeeID);
                    if (examResults != null)
                    {
                        Response.Write("<script>alert('���Ѿ��μӹ��ÿ��ԣ�'); top.window.close();</script>");
                        return;
                    }
                }
                catch
                {
                    Pub.ShowErrorPage("�޷�����·�ַ�����������վ�η��������������Ƿ�������");
                }
                ExamBLL examBLL = new ExamBLL();
                RailExam.Model.Exam exam = examBLL.GetExam(int.Parse(strExamId));
                HiddenFieldExamTime.Value = DateTime.Now.AddMinutes(exam.ExamTime).ToString();
                HfExamTime.Value = (exam.ExamTime * 60).ToString();

                FillPage(strId);
            }
        }

        protected void FillPage(string strId)
        {
            PaperBLL paperBLL = new PaperBLL();
            RailExam.Model.Paper paper = paperBLL.GetPaper(int.Parse(strId));

            if (paper != null)
            {
                lblTitle.Text = paper.PaperName;
            }

            PaperItemBLL paperItemBLL = new PaperItemBLL();
            IList<RailExam.Model.PaperItem> paperItems = paperItemBLL.GetItemsByPaperId(int.Parse(strId));
            int nItemCount = paperItems.Count;

            // ����ǰ̨JS�ж��Ƿ����ȫ������
            hfPaperItemsCount.Value = nItemCount.ToString();
            decimal nTotalScore = 0;

            for (int i = 0; i < paperItems.Count; i++)
            {
                nTotalScore += paperItems[i].Score;
            }

            lblTitleRight.Text = "�ܹ�" + nItemCount + "�⣬�� " + nTotalScore + "��";

            lblOrgName.Text = PrjPub.CurrentStudent.OrgName;
            lblPost.Text = PrjPub.CurrentStudent.PostName;
            lblName.Text = PrjPub.CurrentStudent.EmployeeName;
        }
    }
}
