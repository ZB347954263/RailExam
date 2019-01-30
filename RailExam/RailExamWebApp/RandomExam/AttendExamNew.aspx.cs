using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ComponentArt.Web.UI;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
	public partial class AttendExamNew : PageBase
	{
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack && !Callback1.IsCallback && !ItemAnswerChangeCallBack.IsCallback)
			{
				string strExamId = Request.QueryString.Get("id");
				ViewState["ExamID"] = strExamId;
				ViewState["BeginTime"] = DateTime.Now.ToString();
				//记录当前考试所在地的OrgID
				ViewState["OrgID"]= ConfigurationManager.AppSettings["StationID"];
				ViewState["EmployeeID"] = Request.QueryString.Get("employeeID");

				if (strExamId != null && strExamId != "")
				{
					RandomExamBLL randomExamBLL = new RandomExamBLL();
					RailExam.Model.RandomExam randomExam = randomExamBLL.GetExam(int.Parse(strExamId));
					ViewState["Year"] = randomExam.BeginTime.Year.ToString();
					HiddenFieldExamTime.Value = DateTime.Now.AddMinutes(randomExam.ExamTime).ToString();
					HfExamTime.Value = (randomExam.ExamTime * 60).ToString();

					//获取当前考生最新需要做的考试试卷主表记录
					RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
					RailExam.Model.RandomExamResultCurrent objResultCurrent =
						objResultCurrentBll.GetNowRandomExamResultInfo(Convert.ToInt32(ViewState["EmployeeID"].ToString()),
																	   Convert.ToInt32(strExamId));
					ViewState["LastExamTime"] = objResultCurrent.ExamTime.ToString();
					HfExamTime.Value = ((randomExam.ExamTime * 60) - objResultCurrent.ExamTime).ToString();

                    if (objResultCurrent.ExamTime < 600 && randomExam.ExamTime*60>600)
                    {
                        hfNeed.Value = (600 - objResultCurrent.ExamTime).ToString();
                    }
                    else
                    {
                        hfNeed.Value = "";
                    }

					//获取该考生当前考试次数
					RandomExamResultBLL RandomExamResultBLL = new RandomExamResultBLL();
					int nowCount;

					//如果在站段考试为随到随考，需要检测路局异地考试的情况
					//if (randomExam.StartMode == PrjPub.START_MODE_NO_CONTROL && !PrjPub.IsServerCenter)
					//{
					//    IList<RandomExamResult> RandomExamResultsServer =
					//    RandomExamResultBLL.GetRandomExamResultByExamineeIDFromServer(PrjPub.CurrentStudent.EmployeeID,
					//                                                        int.Parse(strExamId));
					//    nowCount = RandomExamResultsServer.Count;
					//}
					//else
					//{
					//    IList<RandomExamResult> RandomExamResults = RandomExamResultBLL.GetRandomExamResultByExamineeID(PrjPub.CurrentStudent.EmployeeID, int.Parse(strExamId));
					//    nowCount = RandomExamResults.Count;
					//}

					//如果是随到随考,需要往回复请求表加入一条记录
					//if (randomExam.StartMode == PrjPub.START_MODE_NO_CONTROL)
					//{
					RandomExamApplyBLL objApplyBll = new RandomExamApplyBLL();
					RandomExamApply objNowApply = objApplyBll.GetRandomExamApplyByExamResultCurID(objResultCurrent.RandomExamResultId);
					if (objNowApply.RandomExamApplyID == 0)
					{
						RandomExamApply objApply = new RandomExamApply();
						objApply.RandomExamID = Convert.ToInt32(strExamId);
						objApply.RandomExamResultCurID = objResultCurrent.RandomExamResultId;
						objApply.ApplyStatus = 1;
						objApply.CodeFlag = true;
						objApply.IPAddress = Pub.GetRealIP();
						objApplyBll.AddRandomExamApply(objApply);
					}
					else
					{
						objNowApply.IPAddress = Pub.GetRealIP();
						objApplyBll.UpdateRandomExamApply(objNowApply);
					}
					//}

					IList<RandomExamResult> RandomExamResults = RandomExamResultBLL.GetRandomExamResultByExamineeID(Convert.ToInt32(ViewState["EmployeeID"].ToString()), int.Parse(strExamId));
					nowCount = RandomExamResults.Count;

					ViewState["NowStartMode"] = randomExam.StartMode.ToString();
					HiddenFieldMaxCount.Value = (randomExam.MaxExamTimes - nowCount - 1).ToString();

					//更新考试状态
					UpdateResultToDB(strExamId);
					FillPage(strExamId);
				}
			}

			string strAnswer = Request.Form.Get("strreturnAnswer");
			if (strAnswer != null && strAnswer != "")
			{
				ViewState["EndTime"] = DateTime.Now.ToString();
				SaveAnswerToDB(strAnswer);

				//int examTime = Convert.ToInt32(ViewState["LastExamTime"].ToString()) +
				//GetSecondBetweenTwoDate(DateTime.Parse(ViewState["EndTime"].ToString()),
				//                        DateTime.Parse(ViewState["BeginTime"].ToString()));
				//ClientScript.RegisterStartupScript(GetType(), "jsHide", "<script>showProgressbar(" +
				//                                                        Request.QueryString.Get("id") + "," +
				//                                                        ViewState["RandomExamResultID"] + "," +
				//                                                        ViewState["EmployeeID"]+ "," + ViewState["OrgID"]+
				//                                                        ",'" +
				//                                                        ViewState["EndTime"] + "'," +
				//                                                        examTime + ")</script>");
			}
		}

		private void UpdateResultToDB(string strId)
		{
			//获取当前考试的考试结果ID,并且更新考试状态和开考时间
			RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
			RailExam.Model.RandomExamResultCurrent randomExamResult = objResultCurrentBll.GetNowRandomExamResultInfo(Convert.ToInt32(ViewState["EmployeeID"].ToString()), Convert.ToInt32(strId));

			if (randomExamResult.RandomExamResultId == 0)
			{
				//AddResultToDB(strId);
			    return;
			}
			else
			{
				ViewState["RandomExamResultID"] = randomExamResult.RandomExamResultId;
			}

			if (randomExamResult.StatusId == 0)
			{
				randomExamResult.BeginDateTime = DateTime.Parse(ViewState["BeginTime"].ToString());
				randomExamResult.ExamTime = 0;
			}
			randomExamResult.CurrentDateTime = DateTime.Parse(ViewState["BeginTime"].ToString());
			randomExamResult.EndDateTime = DateTime.Parse(ViewState["BeginTime"].ToString());
			randomExamResult.OrganizationId = int.Parse(ViewState["OrgID"].ToString());
			randomExamResult.Memo = "";
			//参加考试将当前考试的标志置为1－正在进行
			randomExamResult.StatusId = 1;
			randomExamResult.ExamineeId = int.Parse(ViewState["EmployeeID"].ToString());

			objResultCurrentBll.UpdateRandomExamResultCurrent(randomExamResult);

            //更新考试机位
            string strSql = "";
            string mac_dest = "";
            try
            {
                mac_dest = GetCustomerMac(GetClientIP());
            }
            catch
            {
                mac_dest = "";
            }

            if (!string.IsNullOrEmpty(mac_dest))
            {
                strSql = "select * from Computer_Room_Detail"
                    + " where MAC_Address='" + mac_dest + "'";

                OracleAccess db = new OracleAccess();

                DataSet ds = db.RunSqlDataSet(strSql);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    strSql = "update Random_Exam_Result_Detail set Computer_Room_Seat=" +
                             ds.Tables[0].Rows[0]["Computer_Room_Seat"]
                             + " where Random_Exam_ID=" + randomExamResult.RandomExamId +
                             " and Random_Exam_Result_ID=" + randomExamResult.RandomExamResultId +
                             " and Employee_ID=" + randomExamResult.ExamineeId;
                    db.ExecuteNonQuery(strSql);
                }
            }
		}

		private void SaveAnswerToDB(string strAnswer)
		{
			string strId = Request.QueryString.Get("id");

            RandomExamItemBLL randomItemBLL = new RandomExamItemBLL();
			RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
			RailExam.Model.RandomExamResultCurrent objResultCurrent = objResultCurrentBll.GetNowRandomExamResultInfo(Convert.ToInt32(ViewState["EmployeeID"].ToString()), Convert.ToInt32(strId));

			//更新考试成绩表时传入的主键应为站段的成绩表的主键ID
			objResultCurrent.RandomExamResultId = int.Parse(ViewState["RandomExamResultID"].ToString());
			objResultCurrent.RandomExamId = int.Parse(strId);
			objResultCurrent.AutoScore = 0;
			objResultCurrent.CurrentDateTime = DateTime.Parse(ViewState["EndTime"].ToString());
			objResultCurrent.ExamTime = Convert.ToInt32(ViewState["LastExamTime"].ToString()) +
				GetSecondBetweenTwoDate(DateTime.Parse(ViewState["EndTime"].ToString()),
										DateTime.Parse(ViewState["BeginTime"].ToString()));
			objResultCurrent.EndDateTime = DateTime.Parse(ViewState["EndTime"].ToString());
			objResultCurrent.Score = 0;
			objResultCurrent.OrganizationId = int.Parse(ViewState["OrgID"].ToString());
			objResultCurrent.Memo = "";
			objResultCurrent.StatusId = 2;
			objResultCurrent.AutoScore = 0;
			objResultCurrent.CorrectRate = 0;
			objResultCurrent.ExamineeId = int.Parse(ViewState["EmployeeID"].ToString());

			string[] str1 = strAnswer.Split(new char[] { '$' });
			RandomExamResultAnswerCurrentBLL randomExamResultAnswerBLL = new RandomExamResultAnswerCurrentBLL();
			//randomExamResultAnswerBLL.DeleteExamResultAnswerCurrent(Convert.ToInt32(ViewState["RandomExamResultID"].ToString()));
			//IList<RandomExamResultAnswerCurrent> randomExamResultAnswers = new List<RandomExamResultAnswerCurrent>();
			int randomExamResultId = int.Parse(ViewState["RandomExamResultID"].ToString());
			for (int n = 0; n < str1.Length; n++)
			{
				string str2 = str1[n].ToString();
				string[] str3 = str2.Split(new char[] { '|' });
				string strPaperItemId = str3[0].ToString();
				string strTrueAnswer = str2.ToString().Substring(strPaperItemId.Length + 1);

				RandomExamResultAnswerCurrent randomExamResultAnswer = new RandomExamResultAnswerCurrent();
				randomExamResultAnswer.RandomExamResultId = randomExamResultId;
				randomExamResultAnswer.RandomExamItemId = int.Parse(strPaperItemId);
				randomExamResultAnswer.JudgeStatusId = 0;
				randomExamResultAnswer.JudgeRemark = string.Empty;
				randomExamResultAnswer.ExamTime = 0;
				randomExamResultAnswer.Answer = strTrueAnswer;
				//randomExamResultAnswers.Add(randomExamResultAnswer);
			    randomExamResultAnswerBLL.UpdateExamResultAnswerCurrent(randomExamResultAnswer);
			}

		    try
		    {
                //将更新答卷信息
                //randomExamResultAnswerBLL.AddExamResultAnswerCurrentSave(randomExamResultId, randomExamResultAnswers);

                //更新实时考试记录
                objResultCurrentBll.UpdateRandomExamResultCurrent(objResultCurrent);

                //获取最后考试成绩
                //RandomExamResultCurrent randomExamResultCurrent = objResultCurrentBll.GetRandomExamResult(Convert.ToInt32(ViewState["RandomExamResultID"].ToString()));
                //decimal nowScore = randomExamResultCurrent.Score;
              
                //将实时考试记录（临时表）转存到正式考试成绩表和答卷表
                RandomExamResultBLL objResultBll = new RandomExamResultBLL();
                //int randomExamResultID = objResultBll.RemoveResultAnswer(Convert.ToInt32(ViewState["RandomExamResultID"].ToString()));

                //将实时考试记录（临时表）转存到中间考试成绩表和答卷表
                int randomExamResultID = objResultBll.RemoveResultAnswerCurrent(Convert.ToInt32(ViewState["RandomExamResultID"].ToString()));

                //删除登录信息
                SystemUserLoginBLL objloginBll = new SystemUserLoginBLL();
                objloginBll.DeleteSystemUserLogin(Convert.ToInt32(ViewState["EmployeeID"].ToString()));

                Response.Write("<script>window.parent.parent.location = '/RailExamBao/Online/Exam/ExamSuccess.aspx?ExamType=1&ExamResultID=" + randomExamResultID + "'</script>");

		    }
		    catch
		    {
                SystemUserLoginBLL objloginBll = new SystemUserLoginBLL();
                objloginBll.DeleteSystemUserLogin(Convert.ToInt32(ViewState["EmployeeID"].ToString()));

		        string strSql = "update Random_Exam_Result_Current set Exam_Time=Exam_Time-180 where Random_Exam_ID=" + strId + " and Examinee_ID="+ViewState["EmployeeID"];
                OracleAccess db = new OracleAccess();                                                                                                                                                                                               
                db.ExecuteNonQuery(strSql);

                Response.Write("<script>window.parent.parent.location = '/RailExamBao/Common/OtherError.aspx?error=提交试卷失败，请重新进入考试再次进行提交'</script>");
            }
			

			//如果在站段是随到随考考试，成绩自动上传至路局
			//if(ViewState["NowStartMode"].ToString() == PrjPub.START_MODE_NO_CONTROL.ToString() && !PrjPub.IsServerCenter)
			//{
			//    objResultBll.RemoveRandomExamResultToServer(Convert.ToInt32(strId), Convert.ToInt32(ConfigurationManager.AppSettings["StationID"]));
			//}
		}

        private string GetClientIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }

        /// <summary>   
        /// 获取Mac地址信息   
        /// </summary>   
        /// <param name="IP"></param>   
        /// <returns></returns>   
        public static string GetCustomerMac(string IP)
        {
            Int32 ldest = inet_addr(IP);
            Int64 macinfo = new Int64();
            Int32 len = 6;
            int res = SendARP(ldest, 0, ref macinfo, ref len);
            string mac_src = macinfo.ToString("X");

            while (mac_src.Length < 12)
            {
                mac_src = mac_src.Insert(0, "0");
            }

            string mac_dest = "";

            for (int i = 0; i < 11; i++)
            {
                if (0 == (i % 2))
                {
                    if (i == 10)
                    {
                        mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                    }
                    else
                    {
                        mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                    }
                }
            }

            return mac_dest;
        }

		protected void FillPage(string strExamId)
		{
			RandomExamSubjectBLL randomExamSubjectBLL = new RandomExamSubjectBLL();
			IList<RailExam.Model.RandomExamSubject> RandomExamSubjects = randomExamSubjectBLL.GetRandomExamSubjectByRandomExamId(int.Parse(strExamId));
			int nItemCount = 0;
			for (int i = 0; i < RandomExamSubjects.Count; i++)
			{
				nItemCount += RandomExamSubjects[i].ItemCount;
			}
			// 用于前台JS判断是否完成全部试题
			hfPaperItemsCount.Value = nItemCount.ToString();
		}

		protected void FillPaper()
		{
			string strId = Request.QueryString.Get("id");
			if (string.IsNullOrEmpty(strId))
			{
				SessionSet.PageMessage = "缺少参数！";

				return;
			}

			int RandomExamId = Convert.ToInt32(strId);
			int randomExamResultId = Convert.ToInt32(ViewState["RandomExamResultID"]);

			RandomExamItemBLL randomItemBLL = new RandomExamItemBLL();
			RandomExamSubjectBLL subjectBLL = new RandomExamSubjectBLL();
			IList<RandomExamSubject> randomExamSubjects = subjectBLL.GetRandomExamSubjectByRandomExamId(RandomExamId);

            //RandomExamResultAnswerCurrentBLL randomExamResultAnswerBLL = new RandomExamResultAnswerCurrentBLL();
            //IList<RandomExamResultAnswerCurrent> examResultAnswers = new List<RandomExamResultAnswerCurrent>();
            //examResultAnswers = randomExamResultAnswerBLL.GetExamResultAnswersCurrent(randomExamResultId);

            IList<RandomExamItem> totalItems = randomItemBLL.GetItemsCurrent(0, randomExamResultId, Convert.ToInt32(ViewState["Year"].ToString()));

			if (randomExamSubjects != null)
			{
				for (int i = 0; i < randomExamSubjects.Count; i++)
				{
					RandomExamSubject paperSubject = randomExamSubjects[i];
					IList<RandomExamItem> PaperItems = new List<RandomExamItem>();
					//PaperItems = randomItemBLL.GetItemsCurrent(paperSubject.RandomExamSubjectId, randomExamResultId, Convert.ToInt32(ViewState["Year"].ToString()));
                    PaperItems = GetSubjectItems(totalItems, paperSubject.RandomExamSubjectId);

				    int itemCount = 0;

                    if(paperSubject.ItemTypeId == PrjPub.ITEMTYPE_FILLBLANK)
                    {
                        foreach (RandomExamItem randomExamItem in PaperItems)
                        {
                            if (randomExamItem.TypeId ==  PrjPub.ITEMTYPE_FILLBLANK)
                            {
                                itemCount++;
                            }
                        }
                    }
                    else
                    {
                        itemCount = PaperItems.Count;
                    }

				    Response.Write("<table width='95%' class='ExamContent'>");
                    Response.Write(" <tr> <td class='ExamBigTitle' colspan='3'>");
					Response.Write(" " + GetNo(i) + "");
					Response.Write("、" + paperSubject.SubjectName + "");
                    Response.Write("  （共" + itemCount + "题，共" + System.String.Format("{0:0.##}", itemCount * paperSubject.UnitScore) + "分）</td></tr >");

					if (PaperItems != null)
					{
					    int z = 1;
					    int x = 1;
						for (int j = 0; j < PaperItems.Count; j++)
						{
							RandomExamItem paperItem = PaperItems[j];
							int k = j + 1;

                            if (paperItem.TypeId != PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                            {
                                z = 1;

                                if (paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANK)
                                {
                                    k = x;
                                    x++;

                                    IList<RandomExamItem> randomExamItems = randomItemBLL.GetItemsByParentItemID(paperItem.ItemId, RandomExamId, Convert.ToInt32(ViewState["Year"].ToString()));
                                    Response.Write("<tr><td id='Item" + i + j + "' class='ExamItem' colspan='3'><a name='Test" + i + j + "' id='Test" + i + j + "'></a>&nbsp;&nbsp;&nbsp;" + k +
                                         ".&nbsp; " + paperItem.Content +
                                         "&nbsp;&nbsp; （共" + System.String.Format("{0:0.##}", paperSubject.UnitScore) + "分，其中"
                                         + "每题" + System.String.Format("{0:0.##}", (randomExamItems.Count != 0 ? (decimal)paperSubject.UnitScore / (decimal)randomExamItems.Count : 0)) + "分）"
                                         + "</td></tr >");
                                }
                                else
                                {
                                    Response.Write("<tr><td id='Item" + i + j + "' class='ExamItem' colspan='3'><a name='Test" + i + j + "' id='Test" + i + j + "'></a>&nbsp;&nbsp;&nbsp;" + k +
                                           ".&nbsp; " + paperItem.Content +
                                           "&nbsp;&nbsp; （" + System.String.Format("{0:0.##}", paperSubject.UnitScore) + "分）&nbsp;&nbsp;"
                                           + "<a href='#Test" + i + j + "' id='Empty" + i + j + "' onclick='clickEmpty(this)'style='cursor: hand;' title='清空选择'>"
                                           + "<img src='../images/clear.png'  style='border:0'/></a>"
                                           + "</td></tr >");
                                }
                            }
                            //else
                            //{
                            //    string strSql = "select * from Random_Exam_Item_" + ViewState["Year"] + " where Item_ID='" +
                            //                     paperItem.ItemId + "' and Random_Exam_ID=" + strId;
                            //    DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

                            //    IList<RandomExamItem> randomExamItems = randomItemBLL.GetItemsByParentItemID(Convert.ToInt32(dr["Parent_Item_ID"]),RandomExamId,Convert.ToInt32(ViewState["Year"].ToString()));

                            //    Response.Write("<tr><td id='Item" + i + j + "' class='ExamItem' colspan='2'><a name='Test" + i + j + "' id='Test" + i + j + "'></a>"
                            //            +"&nbsp;&nbsp;&nbsp;(" + z + ").&nbsp; " + paperItem.Content +
                            //            "&nbsp;&nbsp; （" + System.String.Format("{0:0.##}", (decimal)paperSubject.UnitScore / (decimal)randomExamItems.Count) + "分）&nbsp;&nbsp;"
                            //            + "<a href='#Test" + i + j + "' id='Empty" + i + j + "' onclick='clickEmpty(this)'style='cursor: hand;' title='清空选择'>"
                            //            + "<img src='../images/clear.png'  style='border:0'/></a>"
                            //            +"</td></tr >");
                            //    z++;
                            //}


						    // 组织用户答案
                            string[] strUserAnswers = new string[0];
                            string strUserAnswer = string.Empty;

                            //RandomExamResultAnswerCurrent theExamResultAnswer = null;
                            //foreach (RandomExamResultAnswerCurrent resultAnswer in examResultAnswers)
                            //{
                            //    if (resultAnswer.RandomExamItemId == paperItem.RandomExamItemId)
                            //    {
                            //        theExamResultAnswer = resultAnswer;
                            //        break;
                            //    }
                            //}
							// 若子表无记录，结束页面输出
                            //if (theExamResultAnswer == null)
                            //{
                            //    SessionSet.PageMessage = "数据错误！";
                            //}
                            //// 否则组织考生答案
                            //if (theExamResultAnswer.Answer != null || theExamResultAnswer.Answer == string.Empty)
                            //{
                            //    strUserAnswers = theExamResultAnswer.Answer.Split(new char[] { '|' });
                            //}

                            if (paperItem.Answer == null)
                            {
                                SessionSet.PageMessage = "数据错误！";
                            }

                            if (!string.IsNullOrEmpty(paperItem.Answer))
                            {
                                strUserAnswers = paperItem.Answer.Split(new char[] { '|' });
                            }

							for (int n = 0; n < strUserAnswers.Length; n++)
							{
								string strN = intToString(int.Parse(strUserAnswers[n]) + 1);
								if (n == 0)
								{
									strUserAnswer += strN;
								}
								else
								{
									strUserAnswer += "," + strN;
								}
							}


							if (paperSubject.ItemTypeId == PrjPub.ITEMTYPE_MULTICHOOSE)   //多选
							{
								string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
								for (int n = 0; n < strAnswer.Length; n++)
								{
									string strN = intToString(n + 1);
									string strij = "-" + paperItem.RandomExamItemId + "-" + i.ToString() + "-" + j.ToString() +
												   "-" + n.ToString();
									string strName = i.ToString() + j.ToString();

									if (("," + strUserAnswer + ",").IndexOf("," + strN + ",") != -1)
									{
										Response.Write(
                                             "<tr><td class='ExamItemAnswer' colspan='3'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input onclick='CheckStyle(this)' type='checkbox' checked='checked' id='Answer" +
                                             strij + "' name='Answer" + strName + "'><label for='Answer" + strij + "'> " + strN + "." + strAnswer[n] +
											 "</label></td></tr>");
									}
									else
									{
										Response.Write(
                                             "<tr><td class='ExamItemAnswer' colspan='3'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input onclick='CheckStyle(this)' type='checkbox' id='Answer" +
                                             strij + "' name='Answer" + strName + "'><label for='Answer" + strij + "'> " + strN + "." + strAnswer[n] +
                                             "</label></td></tr>");
									}
								}
							}
                            else if (paperSubject.ItemTypeId == PrjPub.ITEMTYPE_SINGLECHOOSE || paperSubject.ItemTypeId == PrjPub.ITEMTYPE_JUDGE)    //单选
							{
								string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
								for (int n = 0; n < strAnswer.Length; n++)
								{
									string strN = intToString(n + 1);
									string strij = "-" + paperItem.RandomExamItemId + "-" + i.ToString() + "-" + j.ToString() +
												   "-" + n.ToString();
									string strName = i.ToString() + j.ToString();

									if (strUserAnswer == strN)
									{
										Response.Write(
                                            "<tr><td class='ExamItemAnswer' colspan='3'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input onclick='CheckStyle(this)' type='Radio' checked='checked' id='RAnswer" +
                                            strij + "' name='RAnswer" + strName + "'>  <label for='RAnswer" + strij + "' >" + strN + "." + strAnswer[n] +
                                            "</label></td></tr>");
									}
									else
									{
										Response.Write(
											"<tr><td class='ExamItemAnswer' colspan='3'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input onclick='CheckStyle(this)' type='Radio' id='RAnswer" +
                                            strij + "' name='RAnswer" + strName + "'> <label for='RAnswer" +strij + "' >" + strN + "." + strAnswer[n] +
											"</label></td></tr>");
									}
								}
							}
                            else if(paperItem.TypeId == PrjPub.ITEMTYPE_FILLBLANKDETAIL)
                            {

                                string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
                                for (int n = 0; n < strAnswer.Length; n++)
                                {
                                    string strN = intToString(n + 1);
                                    string strij = "-" + paperItem.RandomExamItemId + "-" + i.ToString() + "-" + j.ToString() +
                                                   "-" + n.ToString();
                                    string strName = i.ToString() + j.ToString();

                                    if(n == 0)
                                    {
                                        int row = strAnswer.Length%2 == 0 ? strAnswer.Length/2 : strAnswer.Length/2 + 1;
                                        Response.Write("<tr><td id='Item" + i + j + "' class='ExamItem' style='width:10%;vertical-align: top' RowSpan='" + row + "'><a name='Test" + i + j + "' id='Test" + i + j + "'></a>"
                                        + "&nbsp;(" + z + ").&nbsp;"
                                        + "<a href='#Test" + i + j + "' id='Empty" + i + j + "' onclick='clickEmpty(this)'style='cursor: hand;' title='清空选择'>"
                                        + "<img src='../images/clear.png'  style='border:0'/></a>"
                                        + "</td>");
                                    }

                                    if(n%2 == 0 && n!=0)
                                    {
                                        Response.Write("<tr>");
                                    }

                                    if (strUserAnswer == strN)
                                    {
                                        Response.Write(
                                            "<td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input onclick='CheckStyle(this)' type='Radio' checked='checked' id='RAnswer" +
                                            strij + "' name='RAnswer" + strName + "'><label for='RsAnswer" + strij + "'>" + strN + "." + strAnswer[n] +
                                            "</label></td>");
                                    }
                                    else
                                    {
                                        Response.Write(
                                            "<td class='ExamItemAnswer'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input onclick='CheckStyle(this)' type='Radio' id='RAnswer" +
                                            strij + "' name='RAnswer" + strName + "'><label for='RAnswer" + strij + "'> " + strN + "." + strAnswer[n] +
                                            "</label></td>");
                                    }

                                    if (n % 2 == 1)
                                    {
                                        Response.Write("</tr>");
                                    }
                                }

                                z++;
                            }
						}
					}
					Response.Write("</table>");
				}
				//<input id='btnEmpty' class='buttonLong' name='btnEmpty' type='button' value='标记未做试题' onclick='CheckEmpty()'/>&nbsp;&nbsp;&nbsp;&nbsp;
				Response.Write(" <div class='ExamButton'><input id='btnClose' class='button' name='btnSave' type='button' value='提交答卷'  onclick='SaveRecord()'/> </div><br><br><br><br><br><br>");
				ClientScript.RegisterStartupScript(GetType(), "StartStyle", "<script>StartStyle()</script>");

			}
			else
			{
				SessionSet.PageMessage = "未找到记录！";
			}
		}

		protected void ItemAnswerChangeCallBack_Callback(object sender, CallBackEventArgs e)
		{
		    try
		    {
                RandomExamResultAnswerCurrentBLL objAnswerCurrentBll = new RandomExamResultAnswerCurrentBLL();
		        RandomExamResultAnswerCurrent objAnswerCurrent =
		            objAnswerCurrentBll.GetExamResultAnswerCurrent(
		                Convert.ToInt32(ViewState["RandomExamResultID"].ToString()), Convert.ToInt32(e.Parameters[0]));
                //objAnswerCurrent.RandomExamResultId = Convert.ToInt32(ViewState["RandomExamResultID"].ToString());
                //objAnswerCurrent.RandomExamItemId = Convert.ToInt32(e.Parameters[0]);
                objAnswerCurrent.JudgeStatusId = 0;
                objAnswerCurrent.JudgeRemark = string.Empty;
                objAnswerCurrent.ExamTime = 0;
                
                if(e.Parameters[3] == "radio")
                {
                    objAnswerCurrent.Answer = e.Parameters[1];
                }
                else
                {
                    if(e.Parameters[2] == "true")
                    {
                        objAnswerCurrent.Answer = (objAnswerCurrent.Answer+"|" + e.Parameters[1]).TrimStart('|');
                    }
                    else
                    {
                        objAnswerCurrent.Answer =
                            (("|" + objAnswerCurrent.Answer + "|").Replace("|" + e.Parameters[1] + "|", "|")).TrimStart(
                                '|').TrimEnd('|');
                    }
                }


                objAnswerCurrentBll.UpdateExamResultAnswerCurrent(objAnswerCurrent);

                RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
                RailExam.Model.RandomExamResultCurrent objResultCurrent =
                    objResultCurrentBll.GetRandomExamResult(Convert.ToInt32(ViewState["RandomExamResultID"].ToString()));
                objResultCurrent.ExamTime = Convert.ToInt32(ViewState["LastExamTime"].ToString()) + GetSecondBetweenTwoDate(DateTime.Now, DateTime.Parse(ViewState["BeginTime"].ToString()));
                objResultCurrentBll.UpdateRandomExamResultCurrent(objResultCurrent);


                //OracleAccess db = new OracleAccess();
                //string sqlCommand = "USP_Random_EXAM_ANSWER_Cur_U";
                //OracleParameter para1 = new OracleParameter("p_random_exam_result_id", OracleType.Number);
                //para1.Value = objAnswerCurrent.RandomExamResultId;
                //OracleParameter para2 = new OracleParameter("p_random_exam_item_id", OracleType.Number);
                //para2.Value = objAnswerCurrent.RandomExamItemId;
                //OracleParameter para3 = new OracleParameter("p_answer", OracleType.NVarChar);
                //para3.Value = objAnswerCurrent.Answer;
                //OracleParameter para4 = new OracleParameter("p_exam_time", OracleType.Number);
                //para4.Value = objAnswerCurrent.ExamTime;
                //OracleParameter para5 = new OracleParameter("p_judge_remark", OracleType.NVarChar);
                //para5.Value = objAnswerCurrent.JudgeRemark;
                //IDataParameter[] paras = new IDataParameter[] { para1, para2, para3, para4, para5};
                //db.ExecuteNonQueryPro(sqlCommand, paras);
            }
		    catch
		    {
                Response.Write("<script>window.parent.parent.location='/RailExamBao/Common/Error.aspx?error=考试出现异常，请重启微机重新登录考试！'</script>");
		    }
		}

		protected void Callback1_Callback(object sender, CallBackEventArgs e)
		{
		    try
		    {
                string strId = ViewState["ExamID"].ToString();
                RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
                RailExam.Model.RandomExamResultCurrent objResultCurrent =
                    objResultCurrentBll.GetRandomExamResult(Convert.ToInt32(ViewState["RandomExamResultID"].ToString()));

                if (objResultCurrent.RandomExamId == 0)
                {
                    hfStart.Value = "2";
                }
                else
                {
                    objResultCurrent.ExamTime = Convert.ToInt32(ViewState["LastExamTime"].ToString()) + GetSecondBetweenTwoDate(DateTime.Now, DateTime.Parse(ViewState["BeginTime"].ToString()));
                    objResultCurrentBll.UpdateRandomExamResultCurrent(objResultCurrent);

                    RandomExamBLL objBll = new RandomExamBLL();
                    RailExam.Model.RandomExam objRandomExam = objBll.GetExam(Convert.ToInt32(strId));
                    hfStart.Value = objRandomExam.IsStart.ToString();
                }

                if (hfStart.Value == "2")
                {
                    RandomExamResultBLL objResultBll = new RandomExamResultBLL();
                    RandomExamResult objResult =
                        objResultBll.GetNewRandomExamResultByExamineeID(Convert.ToInt32(ViewState["EmployeeID"].ToString()),
                                                                        Convert.ToInt32(strId));

                    hfResultID.Value = objResult.RandomExamResultId.ToString();
                }
            }
		    catch
		    {
		        hfStart.Value = "-1";
            }

            hfResultID.RenderControl(e.Output);
			hfStart.RenderControl(e.Output);
		}

		private void AddResultToDB(string strId)
		{
			RandomExamSubjectBLL subjectBLL = new RandomExamSubjectBLL();
			RandomExamItemBLL randomItemBLL = new RandomExamItemBLL();
			RandomExamResultCurrentBLL randomExamResultBLL = new RandomExamResultCurrentBLL();
			RandomExamStrategyBLL strategyBLL = new RandomExamStrategyBLL();

			RandomExamResultAnswerCurrentBLL randomExamResultAnswerBLL = new RandomExamResultAnswerCurrentBLL();
			IList<RandomExamItem> randomExamItems = new List<RandomExamItem>();
			RailExam.Model.RandomExamResultCurrent randomExamResult = new RailExam.Model.RandomExamResultCurrent();

			randomExamResult.RandomExamId = int.Parse(strId);
			randomExamResult.AutoScore = 0;
			randomExamResult.BeginDateTime = DateTime.Parse(ViewState["BeginTime"].ToString());
			randomExamResult.CurrentDateTime = DateTime.Parse(ViewState["BeginTime"].ToString());
			randomExamResult.ExamTime = 0;
			randomExamResult.EndDateTime = DateTime.Parse(ViewState["BeginTime"].ToString());
			randomExamResult.Score = 0;
			randomExamResult.OrganizationId = int.Parse(ViewState["OrgID"].ToString());
			randomExamResult.Memo = "";
			randomExamResult.StatusId = 1;
			randomExamResult.AutoScore = 0;
			randomExamResult.CorrectRate = 0;
			randomExamResult.ExamineeId = int.Parse(ViewState["EmployeeID"].ToString());

			int nRandomExamResultPK = randomExamResultBLL.AddRandomExamResultCurrent(randomExamResult);
		    string strSql = "select a.* from Computer_Room a "
		                    + "inner join Computer_Server b on a.Computer_Server_ID=b.Computer_Server_ID "
		                    + "where Computer_Server_No='" + PrjPub.ServerNo + "'";
            OracleAccess db = new OracleAccess();
		    DataSet ds = db.RunSqlDataSet(strSql);
		    string computerRoomID = string.Empty;
            if(ds.Tables[0].Rows.Count > 0)
            {
                computerRoomID = ds.Tables[0].Rows[0]["Computer_Room_ID"].ToString();
            }

		    strSql = @"insert into Random_Exam_Result_Detail values("
		             + nRandomExamResultPK + "," + int.Parse(strId) + ","
		             + ViewState["EmployeeID"].ToString() + ",0,null,null,null,Random_Exam_Result_Detail_Seq.Nextval,"
		             + "0,null," + (computerRoomID == string.Empty ? "null" : computerRoomID) + ",null,null,null,null)";
            db.ExecuteNonQuery(strSql);

			ViewState["RandomExamResultID"] = nRandomExamResultPK;

			IList<RandomExamSubject> randomExamSubjects = subjectBLL.GetRandomExamSubjectByRandomExamId(int.Parse(strId));

			Hashtable hashTableItemIds = new Hashtable();
			Hashtable htRandomExamItemIds = new Hashtable();
			for (int i = 0; i < randomExamSubjects.Count; i++)
			{
				int nSubjectId = randomExamSubjects[i].RandomExamSubjectId;
				// int nItemCount = randomExamSubjects[i].ItemCount;

				IList<RandomExamStrategy> strategys = strategyBLL.GetRandomExamStrategys(nSubjectId);
				for (int j = 0; j < strategys.Count; j++)
				{
					int nStrategyId = strategys[j].RandomExamStrategyId;
					int nItemCount = strategys[j].ItemCount;
					IList<RandomExamItem> itemList = randomItemBLL.GetItemsByStrategyId(nStrategyId, Convert.ToInt32(ViewState["Year"].ToString()));
					Random ObjRandom = new Random();
					Hashtable hashTable = new Hashtable();
					Hashtable hashTableCount = new Hashtable();
					while (hashTable.Count < nItemCount)
					{
						int k = ObjRandom.Next(itemList.Count);
						hashTableCount[k] = k;
						int itemID = itemList[k].ItemId;
						int examItemID = itemList[k].RandomExamItemId;
						if (!hashTableItemIds.ContainsKey(itemID))
						{
							hashTable[examItemID] = examItemID;
							hashTableItemIds[itemID] = itemID;
							htRandomExamItemIds[examItemID] = examItemID;
						}
						if (hashTableCount.Count == itemList.Count && hashTable.Count < nItemCount)
						{
							SessionSet.PageMessage = "随机考试在设定的取题范围内的试题量不够，请重新设置取题范围！";
							return;
						}
					}
				}
			}

			string strAll = "";
			foreach (int key in htRandomExamItemIds.Keys)
			{
				if (strAll == "")
				{
					strAll += htRandomExamItemIds[key].ToString();
				}
				else
				{
					strAll += "," + htRandomExamItemIds[key].ToString();
				}
			}
			randomExamResultAnswerBLL.AddExamResultAnswerCurrent(nRandomExamResultPK, strAll);
		}

		#region Common function
		private int GetSecondBetweenTwoDate(DateTime dt1, DateTime dt2)
		{
			int i1 = dt1.Hour * 3600 + dt1.Minute * 60 + dt1.Second;
			int i2 = dt2.Hour * 3600 + dt2.Minute * 60 + dt2.Second;

			return i1 - i2;
		}

		private string GetNo(int i)
		{
			string strReturn = "";
			switch (i.ToString())
			{
				case "0":
					strReturn = "一";
					break;
				case "1":
					strReturn = "二";
					break;
				case "2":
					strReturn = "三";
					break;
				case "3":
					strReturn = "四";
					break;
				case "4":
					strReturn = "五";
					break;
				case "5":
					strReturn = "六";
					break;
				case "6":
					strReturn = "七";
					break;
				case "7":
					strReturn = "八";
					break;
				case "8":
					strReturn = "九";
					break;
				case "9":
					strReturn = "十";
					break;
				case "10":
					strReturn = "十一";
					break;
				case "11":
					strReturn = "十二";
					break;
				case "12":
					strReturn = "十三";
					break;
				case "13":
					strReturn = "十四";
					break;
				case "14":
					strReturn = "十五";
					break;
				case "15":
					strReturn = "十六";
					break;
				case "16":
					strReturn = "十七";
					break;
				case "17":
					strReturn = "十八";
					break;
				case "18":
					strReturn = "十九";
					break;
				case "19":
					strReturn = "二十";
					break;
			}
			return strReturn;
		}

		private string intToString(int intCol)
		{
			if (intCol < 27)
			{
				return Convert.ToChar(intCol + 64).ToString();
			}
			else
			{
				return Convert.ToChar((intCol - 1) / 26 + 64).ToString() + Convert.ToChar((intCol - 1) % 26 + 64 + 1).ToString();
			}
		}

        private IList<RandomExamItem> GetSubjectItems(IList<RandomExamItem> objList, int subjectId)
        {
            IList<RandomExamItem> newItems = new List<RandomExamItem>();

            foreach (RandomExamItem randomExamItem in objList)
            {
                if (randomExamItem.SubjectId == subjectId)
                {
                    newItems.Add(randomExamItem);
                }
            }

            return newItems;
        }
		#endregion
	}
}
