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
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
	public partial class RandomExamStart : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnRandom_Click(object sender, EventArgs e)
		{
			Random r = new Random();
			txtCode.Text = r.Next(1000, 9999).ToString();
		}

		protected void btnOK_Click(object sender, EventArgs e)
		{
			string strId = Request.QueryString.Get("RandomExamID");
			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(strId));

            #region 验证试卷完整性
            /*OracleAccess db = new OracleAccess();

            //查询本场考试现有生成试卷的总题数（完型填空子题除外）
		    int beginYear = obj.BeginTime.Year;
            string strSql =
            @"select * from Random_Exam_Result_Answer_Cur a
                        inner join Random_Exam_Item_"+beginYear+@" b on a.Random_Exam_Item_ID=b.Random_Exam_Item_ID
                        where b.Type_ID<>5 and Random_Exam_Result_ID in (
                        select Random_Exam_Result_ID from Random_Exam_Result_Current 
                        where Random_Exam_ID=" + strId + ")";
            DataSet dsAnswer = db.RunSqlDataSet(strSql);

            //查询本场考试题目个数
		    strSql = "select Sum(Item_Count) from Random_Exam_Subject where Random_Exam_ID=" + strId;
            DataSet dsSubject = db.RunSqlDataSet(strSql);
		    int itemCount = Convert.ToInt32(dsSubject.Tables[0].Rows[0][0]);

            //查询本服务器所有考生信息
            strSql = "select a.* from Random_Exam_Arrange_Detail a "
                      + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
                      + " inner join Computer_Server c on c.Computer_server_ID=b.Computer_Server_ID"
                      + " where c.Computer_Server_No='" + PrjPub.ServerNo + "' "
                      + " and Random_Exam_ID=" + strId;
            DataSet ds = db.RunSqlDataSet(strSql);
		    string strChooseID = string.Empty;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (string.IsNullOrEmpty(strChooseID))
                {
                    strChooseID += dr["User_Ids"].ToString();
                }
                else
                {
                    strChooseID += "," + dr["User_Ids"];
                }
            }
		    int employeeCount = strChooseID.Split(',').Length;

            if (dsAnswer.Tables[0].Rows.Count != itemCount*employeeCount)
            {
                Response.Write("<script>alert('本场考试试卷生成有误，请联系监考老师删除考试试卷后重新下载后生成！'); top.close();</script>");
                return;
            }*/
            #endregion

            objBll.UpdateStartCode(Convert.ToInt32(strId), PrjPub.ServerNo, txtCode.Text);
            objBll.UpdateIsStart(Convert.ToInt32(strId), PrjPub.ServerNo, 1);
			
			SystemLogBLL objLogBll = new SystemLogBLL();
			objLogBll.WriteLog("“" + obj.ExamName + "”开始考试");
			Response.Write("<script>top.returnValue='true';top.close();</script>");
		}
	}
}
