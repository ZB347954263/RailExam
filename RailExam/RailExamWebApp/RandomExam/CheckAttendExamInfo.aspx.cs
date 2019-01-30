using System;
using System.Collections.Generic;
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

namespace RailExamWebApp.RandomExam
{
    public partial class CheckAttendExamInfo : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                string strExamId = Request.QueryString.Get("id");
				ViewState["StudentID"] = Request.QueryString.Get("employeeID");
				ViewState["ExamID"] = strExamId;

                if (strExamId != null && strExamId != "")
                {
                    RandomExamBLL randomExamBLL = new RandomExamBLL();
                    RailExam.Model.RandomExam randomExam = randomExamBLL.GetExam(int.Parse(strExamId));

                    OracleAccess db = new OracleAccess();
                    string strSql =
                    @"select * from Random_Exam_Result_Answer_Cur where Random_Exam_Result_ID in (
                        select Random_Exam_Result_ID from Random_Exam_Result_Current 
                        where Examinee_ID=" +
                    ViewState["StudentID"] + " and  Random_Exam_ID=" + strExamId + ")";
                    DataSet dsAnswer = db.RunSqlDataSet(strSql);

                    strSql = "select * from Random_Exam_Result_Current  where Examinee_ID=" +
                    ViewState["StudentID"] + " and  Random_Exam_ID=" + strExamId;
                    DataSet dsCurrent= db.RunSqlDataSet(strSql);

                    if (dsAnswer.Tables[0].Rows.Count == 0 && dsCurrent.Tables[0].Rows.Count > 0)
                    {
                        Response.Write("<script>alert('您的考试试卷生成有误，请联系监考老师删除您的考试试卷后重新生成！'); top.close();</script>");
                        return;
                    }

                    strSql = "select * from Random_Exam_Computer_Server where Random_Exam_ID=" + ViewState["ExamID"].ToString()
                           + " and Computer_Server_No=" + PrjPub.ServerNo;
                    DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

                    //如果当前考试为手动控制，则需判断考试是否开始
                    if (randomExam.StartMode == 2 && dr["Is_Start"].ToString() == "0")
                    {
                        Response.Write("<script>alert('该考试还没有开始，请耐心等待！'); top.close();</script>");
                        return;
                    }
                    if (randomExam.StartMode == 2 && dr["Is_Start"].ToString() == "2")
                    {
                        Response.Write("<script>alert('该考试已经结束！'); top.close();</script>");
                        return;
                    }

                    RandomExamResultBLL RandomExamResultBLL = new RandomExamResultBLL();
                    int nowCount;

                    IList<RandomExamResult> RandomExamResults =
                        RandomExamResultBLL.GetRandomExamResultByExamineeID(
                            Convert.ToInt32(Request.QueryString.Get("employeeID")), int.Parse(strExamId));
                    nowCount = RandomExamResults.Count;

                    if (nowCount > (randomExam.MaxExamTimes - 1))
                    {
                        Response.Write("<script>alert('您参加考试的次数已经达到考试设定的最大次数！'); top.close();</script>");
                        return;
                    }

                    RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
                    RailExam.Model.RandomExamResultCurrent randomExamResult =
                        objResultCurrentBll.GetNowRandomExamResultInfo(
                            Convert.ToInt32(Request.QueryString.Get("employeeID")), Convert.ToInt32(strExamId));

                    if (randomExamResult.RandomExamResultId == 0) 
                    {
                        Response.Write("<script>alert('该考试已经结束！'); top.close();</script>");
                        return;
                    }
                }
                FillPage();
            }
        }

        private  void FillPage()
        {
            string strText = string.Empty;
            string strInfo = string.Empty;

            string strSql = "select * from System_Exam where System_Exam_ID=1";
            OracleAccess db = new OracleAccess();
            DataTable dt = db.RunSqlDataSet(strSql).Tables[0];

            if(dt.Rows.Count > 0)
            {
                strInfo = dt.Rows[0]["Exam_Message"].ToString();
            }
            else
            {
                strInfo = string.Empty;
            }

            strText += "<span style='word-break:break-all;'>" + strInfo.Replace("\r\n","<br>") + "</span><br>";

            examInfo.InnerHtml = strText;
        }
    }
}
