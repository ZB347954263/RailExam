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
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;
using System.Collections.Generic;

namespace RailExamWebApp.RandomExam
{
	public partial class AttendExamStart : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack && !CallBack1.IsCallback)
			{
				string strExamId = Request.QueryString.Get("id");
				ViewState["StudentID"] = Request.QueryString.Get("employeeID");
				ViewState["ExamID"] = strExamId;

			    ViewState["Search"] = "?id=" + strExamId + "&employeeID=" + ViewState["StudentID"];

				if (strExamId != null && strExamId != "")
				{
					RandomExamBLL randomExamBLL = new RandomExamBLL();
					RailExam.Model.RandomExam randomExam = randomExamBLL.GetExam(int.Parse(strExamId));
					
                    //if (randomExam.OrgId.ToString() != ConfigurationManager.AppSettings["StationID"].ToString())
                    //{
                    //    OrganizationBLL OrgBll = new OrganizationBLL();
                    //    Organization org = OrgBll.GetOrganization(randomExam.OrgId);
                    //    string strOrgName = org.ShortName;
                    //    if(org.SuitRange != 1)
                    //    {
                    //        Response.Write("<script>alert('该考试为" + strOrgName + "考试，必须在" + strOrgName + "参加！'); top.window.close();</script>");
                    //        return;
                    //    }
                    //}

                    #region 该段代码转移到CheckAttendExamInfo.aspx页面，先弹出考场规则然后再申请
                    /*
					//如果当前考试为手动控制，则需判断考试是否开始
					if (randomExam.StartMode == 2 && randomExam.IsStart == 0)
					{
						Response.Write("<script>alert('该考试还没有开始，请耐心等待！'); top.close();</script>");
						return;
					}
					if (randomExam.StartMode == 2 && randomExam.IsStart == 2)
					{
						Response.Write("<script>alert('该考试已经结束！'); top.close();</script>");
						return;
					}

					RandomExamResultBLL RandomExamResultBLL = new RandomExamResultBLL();
					int nowCount;

                    IList<RandomExamResult> RandomExamResults = RandomExamResultBLL.GetRandomExamResultByExamineeID(Convert.ToInt32(Request.QueryString.Get("employeeID")), int.Parse(strExamId));
					nowCount = RandomExamResults.Count;

					if (nowCount > (randomExam.MaxExamTimes - 1))
					{
						Response.Write("<script>alert('您参加考试的次数已经达到考试设定的最大次数！'); top.close();</script>");
						return;
					}

					RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
                    RailExam.Model.RandomExamResultCurrent randomExamResult = objResultCurrentBll.GetNowRandomExamResultInfo(Convert.ToInt32(Request.QueryString.Get("employeeID")), Convert.ToInt32(strExamId));

					if (randomExamResult.RandomExamResultId == 0)
					{
						Response.Write("<script>alert('该考试已经结束！'); top.close();</script>");
						return;
					}
                    */
                    #endregion

                    if (randomExam.StartMode == PrjPub.START_MODE_NO_CONTROL)
					{
                        Response.Redirect("AttendExamLeft.aspx?id=" + strExamId + "&employeeID=" + Request.QueryString.Get("employeeID"));
                        //Response.Redirect("CheckAttendExamInfo.aspx?id=" + strExamId + "&employeeID=" + Request.QueryString.Get("employeeID"));
					}

					Session["IPAddress"] = Pub.GetRealIP();
					//txtCode.Focus();
				}
			}
		}

		protected void CallBack1_Callback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
		{
			if (hfID.Value != "")
			{
				RandomExamApplyBLL objBll = new RandomExamApplyBLL();
				RandomExamApply obj = objBll.GetRandomExamApply(Convert.ToInt32(hfID.Value));

				if (obj.RandomExamApplyID != 0)
				{
					if (obj.ApplyStatus == 1)
					{
						hfNow.Value = "1";
					}
					else
					{
						hfNow.Value = "0";
					}
				}
				else
				{
					hfNow.Value = "-1";
				}
			}
			else
			{
				hfNow.Value = "0";
			}

			hfNow.RenderControl(e.Output);
		}

		protected void btnOK_Click(object sender, EventArgs e)
		{
			//获取当前考试的生成试卷的状态和次数
			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam obj = objBll.GetExam(Convert.ToInt32(ViewState["ExamID"].ToString()));

            string strSql = "select * from Random_Exam_Computer_Server where Random_Exam_ID=" + ViewState["ExamID"].ToString()
		                    + " and Computer_Server_No=" + PrjPub.ServerNo;
            OracleAccess db = new OracleAccess();
		    DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

			if(txtCode.Text != dr["Random_Exam_Code"].ToString())
			{
				SessionSet.PageMessage = "验证码输入有误，请重新输入！";
				txtCode.Text = "";
				return;
			}

			//获取当前考生即将考试的试卷
			string strIP = Pub.GetRealIP();
			RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
			RailExam.Model.RandomExamResultCurrent objResultCurrent =
				objResultCurrentBll.GetNowRandomExamResultInfo(Convert.ToInt32(ViewState["StudentID"].ToString()),
															   Convert.ToInt32(ViewState["ExamID"].ToString()));

			ViewState["CurrentID"] = objResultCurrent.RandomExamResultId;
			//objResultCurrentBll.UpdateRandomExamResultCurrentIP(Convert.ToInt32(ViewState["CurrentID"].ToString()), strIP);
			
			RandomExamApplyBLL objApplyBll = new RandomExamApplyBLL();
			RandomExamApply objNowApply = objApplyBll.GetRandomExamApplyByExamResultCurID(objResultCurrent.RandomExamResultId);
			if (objNowApply.RandomExamApplyID == 0)
			{
				RandomExamApply objApply = new RandomExamApply();
				objApply.RandomExamResultCurID = Convert.ToInt32(ViewState["CurrentID"].ToString());
				objApply.RandomExamID = Convert.ToInt32(ViewState["ExamID"].ToString());
                objApply.CodeFlag = txtCode.Text == dr["Random_Exam_Code"].ToString();
				objApply.ApplyStatus = 0;
				objApply.IPAddress = strIP;
				hfID.Value = objApplyBll.AddRandomExamApply(objApply).ToString();
			}
			else
			{
                objNowApply.CodeFlag = txtCode.Text == dr["Random_Exam_Code"].ToString();
				objNowApply.ApplyStatus = 0;
				objNowApply.IPAddress = Pub.GetRealIP();
				objApplyBll.UpdateRandomExamApply(objNowApply);
				hfID.Value = objNowApply.RandomExamApplyID.ToString();
			}

			lblApply.Text = "已提交考试请求，请等待回复......";
			btnClose.Visible = true;
			btnOK.Visible = false;
			lblTitle.Visible = false;
			txtCode.Visible = false;
			img.Visible = true;
		}

		protected void btnClose_Click(object sender, EventArgs e)
		{
			RandomExamApplyBLL objBll = new RandomExamApplyBLL();
			objBll.DelRandomExamApply(Convert.ToInt32(hfID.Value));
			Response.Write("<script>top.close();</script>");
		}
	}
}
