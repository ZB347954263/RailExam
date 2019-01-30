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
using System.Collections.Generic;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class AttendExamTitleNew : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack && !CallBack1.IsCallback)
            {
                    string strExamId = Request.QueryString.Get("id");
                    ViewState["BeginTime"] = DateTime.Now.ToString();
					ViewState["StudentID"] = Request.QueryString.Get("employeeID");

                    if (strExamId != null && strExamId != "")
                    {
                        RandomExamBLL randomExamBLL = new RandomExamBLL();
                        RailExam.Model.RandomExam randomExam = randomExamBLL.GetExam(int.Parse(strExamId));
						
						#region 考前判断（屏蔽）
						//if (randomExam.StartMode == 2 && randomExam.OrgId.ToString() != ConfigurationManager.AppSettings["StationID"].ToString() && randomExam.OrgId != 32)
						//{
						//    OrganizationBLL OrgBll = new OrganizationBLL();
						//    string strOrgName = OrgBll.GetOrganization(randomExam.OrgId).ShortName;
                        //    Response.Write("<script>alert('该考试为" + strOrgName + "统一时间考试，必须在" + strOrgName + "参加！'); top.window.close();</script>");
						//    return;
						//}
						////如果当前考试为手动控制，则需判断考试是否开始
						//if (randomExam.StartMode == 2 && randomExam.IsStart == 0)
						//{
						//    Response.Write("<script>alert('该考试还没有开始，请耐心等待！'); top.window.close();</script>");
						//    return;
						//}
						//if (randomExam.StartMode == 2 && randomExam.IsStart == 2)
						//{
						//    Response.Write("<script>alert('该考试已经结束！'); top.window.close();</script>");
						//    return;
						//}
						#endregion
						
						HiddenFieldExamTime.Value = DateTime.Now.AddMinutes(randomExam.ExamTime).ToString();
                        HfExamTime.Value = (randomExam.ExamTime*60).ToString();

						//获取当前考生最新需要做的考试试卷主表记录
                        RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
                        RailExam.Model.RandomExamResultCurrent objResultCurrent =
                            objResultCurrentBll.GetNowRandomExamResultInfo(Convert.ToInt32(ViewState["StudentID"].ToString()),
                                                                           Convert.ToInt32(strExamId));
						//设置当前考生本次考试还剩下的考试时间
                        HfExamTime.Value = ((randomExam.ExamTime * 60) - objResultCurrent.ExamTime).ToString();
                        FillPage(strExamId);
                    }
            }
        }


        protected void FillPage(string strExamId)
        {
			//获取考试基本信息
            RandomExamBLL randomExamBLL = new RandomExamBLL();
            RailExam.Model.RandomExam randomExam = randomExamBLL.GetExam(int.Parse(strExamId));
            if (randomExam != null)
            {
                lblTitle.Text = randomExam.ExamName;
            }

			//获取考试题数和分数
            RandomExamSubjectBLL randomExamSubjectBLL = new RandomExamSubjectBLL();
            IList<RailExam.Model.RandomExamSubject> RandomExamSubjects = randomExamSubjectBLL.GetRandomExamSubjectByRandomExamId(int.Parse(strExamId));
            int nItemCount = 0;
            decimal nTotalScore = 0;
            for (int i = 0; i < RandomExamSubjects.Count; i++)
            {
                nItemCount += RandomExamSubjects[i].ItemCount;
                nTotalScore += RandomExamSubjects[i].ItemCount * RandomExamSubjects[i].UnitScore;
            }

            // 用于前台JS判断是否完成全部试题
            hfPaperItemsCount.Value = nItemCount.ToString();
            lblTitleRight.Text = "总共" + nItemCount + "题，共" + System.String.Format("{0:0.##}", nTotalScore) + "分";


            string strSql = "select a.*,b.post_name, GetOrgName(a.org_id) as org_name from Employee a "
                    + "  left join post b on a.post_id = b.post_id"
                    + " where a.Employee_ID=" + Request.QueryString.Get("employeeID");
            OracleAccess db = new OracleAccess();

            DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

            lblWorkNo.Text = dr["Work_No"].ToString();
            lblIDCard.Text = dr["Identity_CardNo"].ToString();
            lblSex.Text = dr["Sex"].ToString();
            lblOrgName.Text = dr["org_name"].ToString();
            lblPost.Text = dr["post_name"].ToString();
            lblName.Text = dr["Employee_Name"].ToString();
        }

        protected  void CallBack1_Callback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            lblServerDateTime.Text = DateTime.Now.ToLongDateString()+" "+DateTime.Now.ToLongTimeString();
            lblServerDateTime.RenderControl(e.Output);
        }

        private int GetSecondBetweenTwoDate(DateTime dt1, DateTime dt2)
        {
            int i1 = dt1.Hour * 3600 + dt1.Minute * 60 + dt1.Second;
            int i2 = dt2.Hour * 3600 + dt2.Minute * 60 + dt2.Second;

            return i1 - i2;
        }
    }
}
