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
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
	public partial class RandomExamStatisticDetail : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
                ViewState["RandomExamItemID"] = Request.QueryString.Get("RandomExamItemID");
				ViewState["BookID"] = Request.QueryString.Get("BookID");
				ViewState["ChapterID"] = Request.QueryString.Get("ChapterID");
				ViewState["RangeType"] = Request.QueryString.Get("RangeType");
				ViewState["Begin"] = Request.QueryString.Get("BeginDate");
				ViewState["End"] = Request.QueryString.Get("EndDate");
				ViewState["ExamID"] = Request.QueryString.Get("ExamID");
				ViewState["EmployeeID"] = Request.QueryString.Get("EmployeeID");

                RandomExamBLL objbll = new RandomExamBLL();
                RailExam.Model.RandomExam objRandomExam = objbll.GetExam(Convert.ToInt32(ViewState["ExamID"]));
                int year = objRandomExam.BeginTime.Year;

                string strSql = "select a.*,GetBookChapterName(Chapter_ID) ChapterName from Random_Exam_Item_" + year + @" a
                            where Random_Exam_Item_ID=" +
		                    ViewState["RandomExamItemID"];
                OracleAccess db = new OracleAccess();
		        DataSet ds = db.RunSqlDataSet(strSql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblBookChapter.Text = ds.Tables[0].Rows[0]["ChapterName"].ToString();
                }

			    int itemID = Convert.ToInt32(ViewState["RandomExamItemID"].ToString());
				int bookID = Convert.ToInt32(ViewState["BookID"].ToString());
				int chapterID = Convert.ToInt32(ViewState["ChapterID"].ToString());
				int typeID = Convert.ToInt32(ViewState["RangeType"].ToString());
				int examID = Convert.ToInt32(ViewState["ExamID"].ToString());
				int employeeID = Convert.ToInt32(ViewState["EmployeeID"].ToString());

				RandomExamStatisticBLL objBll = new RandomExamStatisticBLL();
				IList<RailExam.Model.RandomExamStatistic> objList = null;
				if (typeID != 0 && employeeID == 0)
				{
				    int orgID = -1;
                    if (PrjPub.CurrentLoginUser.SuitRange == 1)
                    {
                        orgID = -1;
                    }
                    else
                    {
                        orgID = PrjPub.CurrentLoginUser.StationOrgID;
                    }

					DateTime begin = Convert.ToDateTime(ViewState["Begin"].ToString());
					DateTime end = Convert.ToDateTime(ViewState["End"].ToString());
					objList = objBll.GetErrorItemInfoByItemID(bookID, chapterID, typeID, examID, begin, end, orgID, -1, itemID);
				}
				else if(typeID == 0 && employeeID ==0)
				{
					objList = objBll.GetErrorItemInfoByItemID(bookID, chapterID, typeID, examID, DateTime.Today,DateTime.Today, -1, -1,itemID);
				}
				else if(employeeID != 0)
				{
                    objList = objBll.GetErrorItemInfoByItemID(bookID, chapterID, typeID, examID, DateTime.Today, DateTime.Today, -1, employeeID, itemID);
				}

				foreach (RailExam.Model.RandomExamStatistic statistic in objList)
				{
                    
					if(statistic.Answer != "")
					{
					    string[] str = statistic.Answer.Split('|');

					    string strAnswer = string.Empty;
                        for (int i = 0; i < str.Length;i++ )
                        {
                            strAnswer += strAnswer == string.Empty ? intToString(Convert.ToInt32(str[i]) + 1) : "|" + intToString(Convert.ToInt32(str[i]) + 1);
                        }

					    statistic.Answer = strAnswer;
					}

                    if (statistic.StandardAnswer != "")
                    {
                        string[] str = statistic.StandardAnswer.Split('|');

                        string strAnswer = string.Empty;
                        for (int i = 0; i < str.Length; i++)
                        {
                            strAnswer += strAnswer == string.Empty ? intToString(Convert.ToInt32(str[i]) + 1) : "|" + intToString(Convert.ToInt32(str[i]) + 1);
                        }

                        statistic.StandardAnswer = strAnswer;
                    }
				}

				examsGrid.DataSource = objList;
				examsGrid.DataBind();
			}
		}

		public void FillContent()
		{
			RandomExamBLL objbll = new RandomExamBLL();
		    RailExam.Model.RandomExam obj = objbll.GetExam(Convert.ToInt32(ViewState["ExamID"]));
		    int year = obj.BeginTime.Year;

		    string strSql = "select * from Random_Exam_Item_" + year + " where Random_Exam_Item_ID=" +
		                    ViewState["RandomExamItemID"];
            OracleAccess db = new OracleAccess();
		    DataSet ds = db.RunSqlDataSet(strSql);
            if(ds.Tables[0].Rows.Count>0)
            {
                Response.Write(ds.Tables[0].Rows[0]["Content"].ToString());
            }
		}

		public void FillAnswer()
		{
            RandomExamBLL objbll = new RandomExamBLL();
            RailExam.Model.RandomExam obj = objbll.GetExam(Convert.ToInt32(ViewState["ExamID"]));
            int year = obj.BeginTime.Year;

            string strSql = "select * from Random_Exam_Item_" + year + " where Random_Exam_Item_ID=" +
                            ViewState["RandomExamItemID"];
            OracleAccess db = new OracleAccess();
            DataSet ds = db.RunSqlDataSet(strSql);

            if (ds.Tables[0].Rows.Count == 0)
            {
                return;
            }

		    DataRow dr = ds.Tables[0].Rows[0];

		    Response.Write("<table class='contentTable'>");

			if (Convert.ToInt32(dr["Type_ID"].ToString())== 2)   //多选
			{
                string[] strAnswer = dr["Select_Answer"].ToString().Split(new char[] { '|' });
				for (int n = 0; n < strAnswer.Length; n++)
				{
					string strN = intToString(n + 1);
					string strij =  n.ToString();
					string strName = n.ToString();

                    if (("|" + dr["Standard_Answer"] + "|").IndexOf("|" + n + "|") != -1)
					{
						Response.Write(
							 "<tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input type='checkbox' checked='checked' id='Answer" +
							 strij + "' name='Answer" + strName + "' disabled='disabled' > " + strN + "." + strAnswer[n] +
							 "</td></tr>");
						lblAnswer.Text = lblAnswer.Text+strN;
					}
					else
					{
						Response.Write(
							 "<tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input type='checkbox' id='Answer" +
							 strij + "' name='Answer" + strName + "' disabled='disabled' > " + strN + "." + strAnswer[n] +
							 "</td></tr>");
					}
				}
			}
			else    //单选
			{

                string[] strAnswer = dr["Select_Answer"].ToString().Split(new char[] { '|' });
				for (int n = 0; n < strAnswer.Length; n++)
				{
					string strN = intToString(n + 1);
					string strij =  n.ToString();
					string strName = n.ToString();

                    if (dr["Standard_Answer"].ToString()== n.ToString())
					{
						Response.Write(
							"<tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input  type='Radio' checked='checked' id='RAnswer" +
							strij + "' name='RAnswer" + strName + "' disabled='disabled'> " + strN + "." + strAnswer[n] +
							"</td></tr>");
						lblAnswer.Text = strN;
					}
					else
					{
						Response.Write(
							"<tr><td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <input type='Radio' id='RAnswer" +
							strij + "' name='RAnswer" + strName + "' disabled='disabled'> " + strN + "." + strAnswer[n] +
							"</td></tr>");
					}
				}

			}
			Response.Write("</table>");
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
	}
}
