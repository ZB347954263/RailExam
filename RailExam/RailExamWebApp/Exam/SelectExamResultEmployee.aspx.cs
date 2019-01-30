using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
using System.IO;
using Microsoft.Office.Interop.Owc11;

namespace RailExamWebApp.Exam
{
	public partial class SelectExamResultEmployee : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (PrjPub.IsWuhan())
				{
					gradesGrid.Levels[0].Columns[2].HeadingText = "Ա������";
					lblWorkNo.Text = "�ϸ�֤��";
				}
				else
				{
					gradesGrid.Levels[0].Columns[2].HeadingText = "���ʱ��";
					lblWorkNo.Text = "���ʱ��";
				}

				DataSet ds = GetDataSet();
				if (ds != null && ds.Tables[0].Rows.Count > 0)
				{
					gradesGrid.DataSource = ds;
					gradesGrid.DataBind();
				}
                //else
                //{
                //    SessionSet.PageMessage = "���ݴ���";
                //    return;
                //}
			}
			else
			{
				if (Request.Form.Get("OutPut") != null && Request.Form.Get("OutPut") != "")
				{
                    OutputData(Request.Form.Get("OutPut"), Request.Form.Get("OutPutOrgID"));
				}
			}
		}

		protected DataSet GetDataSet()
		{
			string qsTypeId = Request.QueryString.Get("type");
			string qsExamId = Request.QueryString.Get("eid");
			int orgID = Convert.ToInt32(Request.QueryString.Get("OrgID"));
			DataSet ds = new DataSet();

			if (!string.IsNullOrEmpty(qsTypeId) && !string.IsNullOrEmpty(qsExamId))
			{
				int nOrganizationId = string.IsNullOrEmpty(hfOrganizationId.Value) ? PrjPub.DEFAULT_INT_IN_DB : int.Parse(hfOrganizationId.Value);
				string strExamineeName = string.IsNullOrEmpty(txtExamineeName.Text) ? string.Empty : txtExamineeName.Text;
				string strWorkNo = string.IsNullOrEmpty(txtWorkNo.Text) ? string.Empty : txtWorkNo.Text;
				decimal dScoreLower = string.IsNullOrEmpty(txtScoreLower.Text) ? 0 : decimal.Parse(txtScoreLower.Text);
				decimal dScoreUpper = string.IsNullOrEmpty(txtScoreUpper.Text) ? 500 : decimal.Parse(txtScoreUpper.Text);
				int nExamStatusId = string.IsNullOrEmpty(ddlStatusId.SelectedValue) ? PrjPub.DEFAULT_INT_IN_DB : int.Parse(ddlStatusId.SelectedValue);
				string strOrganizationName = string.IsNullOrEmpty(txtOrganizationName.Text) ? string.Empty : txtOrganizationName.Text;

				IList<RailExam.Model.ExamResult> examResults = null;
				ExamResultBLL bllExamResult = new ExamResultBLL();

                try
                {
                    if (orgID == 1)
                    {
                        examResults = bllExamResult.GetExamResults(int.Parse(qsExamId), strOrganizationName, strExamineeName, strWorkNo,dScoreLower,
                             dScoreUpper, nExamStatusId);
                    }
                    else
                    {
                        examResults = bllExamResult.GetExamResultsByOrgID(
							 int.Parse(qsExamId), strOrganizationName, strExamineeName, strWorkNo, dScoreLower,
                             dScoreUpper, nExamStatusId, orgID);
                    }
                }
                catch
                {
                    Pub.ShowErrorPage("�޷�����վ�η�����������վ�η������Ƿ���Լ����������Ƿ�������");
                }

				ExamResultStatusBLL bllExamResultStatus = new ExamResultStatusBLL();
				IList<RailExam.Model.ExamResultStatus> examResultStatuses = bllExamResultStatus.GetExamResultStatuses();

				DataTable dtExamResults = ConvertToDataTable((IList)examResults);
				DataTable dtExamResultStatuses = ConvertToDataTable((IList)examResultStatuses);

				if (dtExamResults != null)
				{
					ds.Tables.Add(dtExamResults);
				}
				else
				{
					ds.Tables.Add(ConvertToDataTable(typeof(RailExam.Model.ExamResult)));
				}

				ds.Tables.Add(dtExamResultStatuses);
				ds.Relations.Add(ds.Tables["ExamResultStatus"].Columns["ExamResultStatusId"],
					ds.Tables["ExamResult"].Columns["StatusId"]);
			}

			return ds;
		}

		protected static DataTable ConvertToDataTable(IList list)
		{
			DataTable dt = null;

			if (list.Count > 0)
			{
				dt = new DataTable(list[0].GetType().Name);
				Type type = list[0].GetType();

				foreach (PropertyInfo pi in type.GetProperties())
				{
					dt.Columns.Add(pi.Name, pi.PropertyType);
				}

				DataRow dr = null;
				foreach (Object o in list)
				{
					dr = dt.NewRow();

					foreach (DataColumn dc in dt.Columns)
					{
						dr[dc] = type.GetProperty(dc.ColumnName).GetValue(o, null);
					}
					dt.Rows.Add(dr);
				}
				dt.AcceptChanges();
			}

			return dt;
		}

		protected static DataTable ConvertToDataTable(Type type)
		{
			DataTable dt = new DataTable(type.Name);

			foreach (PropertyInfo pi in type.GetProperties())
			{
				dt.Columns.Add(pi.Name, pi.PropertyType);
			}
			dt.AcceptChanges();

			return dt;
		}


		protected void btnOutPut_Click(object sender, ImageClickEventArgs e)
		{
			DataSet ds = GetDataSet();
			DataTable dt = ds.Tables[0];

			int orgID = Convert.ToInt32(Request.QueryString.Get("OrgID"));
			string OrgName = "";
			if (orgID != 1)
			{
				OrganizationBLL orgBll = new OrganizationBLL();
				Organization org = orgBll.GetOrganization(orgID);
				OrgName = org.ShortName;
			}

			string strExamName = "";
			string strExamTime = "";
			string qsExamId = Request.QueryString.Get("eid");
			ExamBLL examBll = new ExamBLL();
			RailExam.Model.Exam exam = examBll.GetExam(int.Parse(qsExamId));
			if (exam != null)
			{
				strExamName = exam.ExamName;
				strExamTime = exam.BeginTime.ToString() + "--" + exam.EndTime.ToString();
			}

			SpreadsheetClass xlsheet = new SpreadsheetClass();
			Worksheet ws = (Worksheet)xlsheet.Worksheets[1];
			ws.Cells.Font.set_Size(10);
			ws.Cells.Font.set_Name("����");

			ws.Cells[1, 1] = PrjPub.GetRailName()+ OrgName;
			Range rang1 = ws.get_Range(ws.Cells[1, 1], ws.Cells[2, 13]);
			rang1.set_MergeCells(true);
			rang1.set_HorizontalAlignment(XlHAlign.xlHAlignCenter);
			rang1.Font.set_Bold(true);
			rang1.Font.set_Size(17);
			rang1.Font.set_Name("����");


			ws.Cells[3, 1] = strExamName + "ѧԱ�ɼ��ǼǱ�";

			rang1 = ws.get_Range(ws.Cells[3, 1], ws.Cells[3, 13]);
			rang1.set_MergeCells(true);
			rang1.set_HorizontalAlignment(XlHAlign.xlHAlignCenter);
			rang1.Font.set_Bold(true);
			rang1.Font.set_Size(12);
			rang1.Font.set_Name("����");

			ws.Cells[4, 1] = "�������ڣ� " + strExamTime;
			rang1 = ws.get_Range(ws.Cells[4, 1], ws.Cells[4, 13]);
			rang1.set_MergeCells(true);
			rang1.set_HorizontalAlignment(XlHAlign.xlHAlignRight);
			rang1.Font.set_Name("����");


			//write headertext
			ws.Cells[5, 1] = "���";
			((Range)ws.Cells[5, 1]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);


			ws.Cells[5, 2] = "����";
			ws.get_Range(ws.Cells[5, 2], ws.Cells[5, 4]).set_MergeCells(true);
			ws.get_Range(ws.Cells[5, 2], ws.Cells[5, 4]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

			ws.Cells[5, 5] = "��֯���������䣩";
			ws.get_Range(ws.Cells[5, 5], ws.Cells[5, 7]).set_MergeCells(true);
			ws.get_Range(ws.Cells[5, 5], ws.Cells[5, 7]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

			ws.Cells[5, 8] = "ְ��";
			ws.get_Range(ws.Cells[5, 8], ws.Cells[5, 10]).set_MergeCells(true);
			ws.get_Range(ws.Cells[5, 8], ws.Cells[5, 10]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

			ws.Cells[5, 11] = "����";
			ws.get_Range(ws.Cells[5, 11], ws.Cells[5, 13]).set_MergeCells(true);
			ws.get_Range(ws.Cells[5, 11], ws.Cells[5, 13]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);


			decimal decScore = 0;
			for (int j = 0; j < dt.Rows.Count; j++)
			{
				ws.Cells[6 + j, 1] = j + 1;

				ws.Cells[6 + j, 2] = dt.Rows[j]["ExamineeName"].ToString();
				ws.get_Range(ws.Cells[6 + j, 2], ws.Cells[6 + j, 4]).set_MergeCells(true);
				ws.get_Range(ws.Cells[6 + j, 2], ws.Cells[6 + j, 4]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);

				ws.Cells[6 + j, 5] = dt.Rows[j]["OrganizationName"].ToString();
				ws.get_Range(ws.Cells[6 + j, 5], ws.Cells[6 + j, 7]).set_MergeCells(true);
				ws.get_Range(ws.Cells[6 + j, 5], ws.Cells[6 + j, 7]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);


				ws.Cells[6 + j, 8] = dt.Rows[j]["PostName"].ToString();
				ws.get_Range(ws.Cells[6 + j, 8], ws.Cells[6 + j, 10]).set_MergeCells(true);
				ws.get_Range(ws.Cells[6 + j, 8], ws.Cells[6 + j, 10]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);

				decScore += decimal.Parse(dt.Rows[j]["Score"].ToString());
				ws.Cells[6 + j, 11] = dt.Rows[j]["Score"].ToString();
				ws.get_Range(ws.Cells[6 + j, 11], ws.Cells[6 + j, 13]).set_MergeCells(true);
				ws.get_Range(ws.Cells[6 + j, 11], ws.Cells[6 + j, 13]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);
			}

			decimal dec1 = 0;
			if (dt.Rows.Count > 0)
			{
				dec1 = decScore / dt.Rows.Count;
			}

			ws.Cells[6 + dt.Rows.Count, 1] = "ƽ���֣�";
			((Range)ws.Cells[5, 1]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

			ws.Cells[6 + dt.Rows.Count, 2] = dec1.ToString("0.00");
			ws.get_Range(ws.Cells[6 + dt.Rows.Count, 2], ws.Cells[6 + dt.Rows.Count, 13]).set_MergeCells(true);
			ws.get_Range(ws.Cells[6 + dt.Rows.Count, 2], ws.Cells[6 + dt.Rows.Count, 13]).set_HorizontalAlignment(XlHAlign.xlHAlignRight);



			for (int k = 1; k <= 13; k++)
			{
				for (int j = 5; j <= 6 + dt.Rows.Count; j++)
				{
					((Range)ws.Cells[j, k]).BorderAround(1, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, Missing.Value);
				}
			}

			ws.Name = "1-1";
			ws.Cells.Columns.AutoFit();

			try
			{
				((Worksheet)xlsheet.Worksheets[1]).Activate();

				string path = Server.MapPath("../Excel/Excel.xls");
				if (File.Exists(path))
					File.Delete(path);
				xlsheet.Export(path, SheetExportActionEnum.ssExportActionNone, SheetExportFormat.ssExportAsAppropriate);

				System.IO.FileInfo file = new System.IO.FileInfo(path);
				this.Response.Clear();
				this.Response.Buffer = true;
				this.Response.Charset = "utf-7";
				this.Response.ContentEncoding = System.Text.Encoding.UTF7;
				// ���ͷ��Ϣ��Ϊ"�ļ�����/���Ϊ"�Ի���ָ��Ĭ���ļ���
				this.Response.AddHeader("Content-Disposition", "attachment; filename=" + "Excel.xls");
				// ���ͷ��Ϣ��ָ���ļ���С����������ܹ���ʾ���ؽ���
				this.Response.AddHeader("Content-Length", file.Length.ToString());

				// ָ�����ص���һ�����ܱ��ͻ��˶�ȡ���������뱻����
				this.Response.ContentType = "application/ms-excel";

				// ���ļ������͵��ͻ���
				this.Response.WriteFile(file.FullName);
			}
			catch
			{
				SessionSet.PageMessage = "ϵͳ���󣬵���Excel�ļ�ʧ�ܣ�";
			}
		}

		private void SetHeaderText(Range rang, string headertxt)
		{
			rang.set_MergeCells(true);
			rang.Value2 = headertxt;
			rang.set_HorizontalAlignment(XlHAlign.xlHAlignCenter);
			rang.set_VerticalAlignment(XlVAlign.xlVAlignCenter);
			rang.BorderAround(1, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, Missing.Value);
		}

		protected void btnSearch_Click(object sender, ImageClickEventArgs e)
		{
			DataSet ds = GetDataSet();
			//if (ds != null &&ds.Tables[0].Rows.Count >0)
			//{
			gradesGrid.DataSource = ds;
			gradesGrid.DataBind();
			//}
		}

		protected void btnOutPutWord_Click(object sender, ImageClickEventArgs e)
		{
			string qsExamId = Request.QueryString.Get("eid");
			IList<RailExam.Model.ExamResult> examResults = null;
			ExamResultBLL bllExamResult = new ExamResultBLL();
			examResults = bllExamResult.GetExamResultByExamID(int.Parse(qsExamId));

			string str = "";
			//string fileName = "���Գɼ�";
			for (int i = 0; i < examResults.Count; i++)
			{
				//if (i == 0)
				//{
				//    fileName = examResults[i].ExamName + "�ɼ�";
				//}

				str += GetExamOutString(examResults[i].ExamResultId.ToString(),examResults[i].OrganizationId.ToString());
			}

			Response.Clear();
			Response.Charset = "utf-7";
			Response.Buffer = true;
			EnableViewState = false;
			Response.AppendHeader("Content-Disposition", "attachment;filename=" + "ExamResult" + ".doc");

			Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-7");
			Response.ContentType = "application/ms-word";
			Response.Write(str);
			Response.End();
		}


		private void OutputData(string strId,string orgid)
		{
			string str = GetExamOutString(strId,orgid);
			Response.Clear();
			Response.Charset = "utf-7";
			Response.Buffer = true;
			EnableViewState = false;
			Response.AppendHeader("Content-Disposition", "attachment;filename=ExamResult.doc");

			Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-7");
			Response.ContentType = "application/ms-word";
			Response.Write(str);
			Response.End();
		}

		private string GetExamOutString(string strId,string orgid)
		{
            int orgID = Convert.ToInt32(orgid);
			ExamResultBLL examResultBLL = new ExamResultBLL();
			PaperBLL kBLL = new PaperBLL();
		    RailExam.Model.ExamResult examResult = new RailExam.Model.ExamResult();
            if(PrjPub.IsServerCenter)
            {
                examResult = examResultBLL.GetExamResultByOrgID(Convert.ToInt32(strId),orgID);
            }
            else
            {
                examResult = examResultBLL.GetExamResult(Convert.ToInt32(strId));
            }

		    RailExam.Model.Paper paper = null;
			EmployeeBLL ebLL = new EmployeeBLL();
			RailExam.Model.Employee Employee = ebLL.GetEmployee(examResult.ExamineeId);
			paper = kBLL.GetPaper(examResult.PaperId);

			PaperItemBLL paperItemBLL = new PaperItemBLL();
			IList<RailExam.Model.PaperItem> paperItems = paperItemBLL.GetItemsByPaperId(paper.PaperId);
			int nItemCount = paperItems.Count;

			decimal nTotalScore = 0;

			for (int i = 0; i < paperItems.Count; i++)
			{
				nTotalScore += paperItems[i].Score;
			}



			string strOrgName = Employee.OrgName;
			string strStationName = "";
			string strOrgName1 = "";
			int n = strOrgName.IndexOf("-");
			if (n != -1)
			{
				strStationName = strOrgName.Substring(0, n);
				strOrgName1 = strOrgName.Substring(n + 1);
			}
			else
			{
				strStationName = strOrgName;
				strOrgName1 = "";
			}

			string str = "<div style='text-align:center;font-size:18pt;'>"+ PrjPub.GetRailName() +"�����Ծ�</div>";
            str += "<div style='text-align:left;font-size:10.5pt;'>��������:" + paper.PaperName + " </div>";
            str += "<div style='text-align:right;font-size:10.5pt;'>�ܹ�" + nItemCount + "�⣬�� " + nTotalScore + "��</div>";
            str += "<div style='text-align:center;'><table width='100%' border='1' cellpadding='0' cellspacing='0' >";
            str += "<tr><td  width='7%' style='font-size:10.5pt' >��λ:</td>";
            str += "<td  width='26%' style='font-size:10.5pt' align='left' >" + strStationName + "</td>";
            str += "<td  width='7%' style='font-size:10.5pt' >����:</td>";
            str += "<td  width='27%' style='font-size:10.5pt' align='left' >" + strOrgName1 + "</td>";
            str += "<td  width='7%' style='font-size:10.5pt' >ְ��:</td>";
            str += "<td  width='26%' style='font-size:10.5pt' align='left' >" + Employee.PostName + "</td></tr>";
            str += "<tr><td  width='7%' style='font-size:10.5pt' >����:</td>";
            str += "<td  width='26%' style='font-size:10.5pt' align='left' >" + Employee.EmployeeName + "</td>";
            str += "<td  width='7%' style='font-size:10.5pt' >ʱ��:</td>";
            str += "<td  width='27%' style='font-size:10.5pt' align='left' >" + examResult.BeginDateTime.ToString("yyyy-MM-dd HH:mm") + "</td>";
            str += "<td  width='7%' style='font-size:10.5pt' >�ɼ�:</td>";
            str += "<td  width='26%' style='font-size:10.5pt' align='left' >" + examResult.Score + "</td></tr></table></div>";
            str += "<br>";
            str += GetFillExamPaperString(strId, orgid);

            string strReplace;
            if (PrjPub.IsServerCenter)
            {
                strReplace = "http://" + ConfigurationManager.AppSettings["ServerIP"] + "/RailExamBao/";
            }
            else
            {
                strReplace = "http://" + ConfigurationManager.AppSettings["StationIP"] + "/RailExamBao/";
            }
            str = str.Replace("/RailExamBao/", strReplace);

            return str;
		}

		protected string GetFillExamPaperString(string strId,string orgid)
		{
            int orgID = Convert.ToInt32(orgid);
			string strPaperString = "";
			PaperItemBLL kBLL = new PaperItemBLL();
			PaperSubjectBLL kBSLL = new PaperSubjectBLL();
			ExamResultBLL examResultBLL = new ExamResultBLL();
			ExamResultAnswerBLL examResultAnswerBLL = new ExamResultAnswerBLL();
			RailExam.Model.ExamResult examResult = examResultBLL.GetExamResultByOrgID(Convert.ToInt32(strId),orgID);
			IList<RailExam.Model.PaperSubject> PaperSubjects = kBSLL.GetPaperSubjectByPaperIdByOrgID(examResult.PaperId,orgID);
			RailExam.Model.PaperSubject paperSubject = null;
			IList<RailExam.Model.PaperItem> PaperItems = null;
            IList<RailExam.Model.ExamResultAnswer> examResultAnswers = examResultAnswerBLL.GetExamResultAnswersByOrgID(examResult.ExamResultIDStation,orgID);

			for (int i = 0; i < PaperSubjects.Count; i++)
			{
				paperSubject = PaperSubjects[i];
				PaperItems = kBLL.GetItemsByPaperSubjectId(paperSubject.PaperSubjectId);
				strPaperString += " <table width='100%' border='0' cellpadding='0' cellspacing='0'>";
				strPaperString += " <tr><td  style='font-size:14pt'>";
				strPaperString += " " + GetNo(i) + "";
				strPaperString += ".&nbsp;" + paperSubject.SubjectName + "";
				strPaperString += "  ����" + paperSubject.ItemCount + "�⣬��" + paperSubject.TotalScore + "�֣�</td></tr >";

				if (PaperItems != null)
				{
					for (int j = 0; j < PaperItems.Count; j++)
					{
						RailExam.Model.PaperItem paperItem = PaperItems[j];
						int k = j + 1;

						strPaperString += "<tr > <td style='font-size:10.5pt'>&nbsp;&nbsp;&nbsp;"
									   + k + ".&nbsp; " + paperItem.Content + "&nbsp;&nbsp;��" + paperItem.Score +
									   "�֣�</td></tr >";

						// ��֯�û���
						RailExam.Model.ExamResultAnswer theExamResultAnswer = null;
						string[] strUserAnswers = new string[0];
						string strUserAnswer = string.Empty;

						foreach (RailExam.Model.ExamResultAnswer resultAnswer in examResultAnswers)
						{
							if (resultAnswer.PaperItemId == paperItem.PaperItemId)
							{
								theExamResultAnswer = resultAnswer;
								break;
							}
						}

						// ���ӱ��޼�¼������ҳ�����


						if (theExamResultAnswer == null)
						{
							SessionSet.PageMessage = "���ݴ���";
						}

						// ������֯������
						if (theExamResultAnswer.Answer != null)
						{
							strUserAnswers = theExamResultAnswer.Answer.Split(new char[] { '|' });
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

						//��ѡ


						if (paperSubject.ItemTypeId == 2)
						{
							string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
							for (int n = 0; n < strAnswer.Length; n++)
							{
								string strN = intToString(n + 1);
								string strij = "-" + paperItem.PaperItemId + "-" + i.ToString() + "-"
											   + j.ToString() + "-" + n.ToString();
								string strName = i.ToString() + j.ToString();

								strPaperString += " <tr ><td style='font-size:10.5pt'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; "
											   + strN + "." + strAnswer[n] + "</td></tr >";
							}
						}
						else
						{
							//��ѡ

							string[] strAnswer = paperItem.SelectAnswer.Split(new char[] { '|' });
							for (int n = 0; n < strAnswer.Length; n++)
							{
								string strN = intToString(n + 1);
								string strij = "-" + paperItem.PaperItemId + "-" + i.ToString() + "-" + j.ToString()
											   + "-" + n.ToString();
								string strName = i.ToString() + j.ToString();

								strPaperString += "<tr > <td style='font-size:10.5pt'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; "
											   + strN + "." + strAnswer[n] + "</td></tr >";
							}
						}

						// ��֯��ȷ��
						string[] strRightAnswers = paperItem.StandardAnswer.Split(new char[] { '|' });
						string strRightAnswer = "";
						for (int n = 0; n < strRightAnswers.Length; n++)
						{
							string strN = intToString(int.Parse(strRightAnswers[n]) + 1);
							if (n == 0)
							{
								strRightAnswer += strN;
							}
							else
							{
								strRightAnswer += "," + strN;
							}
						}

						strPaperString += " <tr><td style='font-size:10.5pt'>&nbsp;&nbsp;&nbsp;���׼�𰸣�"
									   + "<span id='span-" + paperItem.PaperItemId + "-0' name='span-" +
									   paperItem.PaperItemId
									   + "'>" + strRightAnswer + "</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�����𰸣�"
									   + "<span id='span-" + paperItem.PaperItemId + "-1' name='span-" +
									   paperItem.PaperItemId
									   + "'>" + strUserAnswer + "</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;�÷֣�&nbsp;" + theExamResultAnswer.JudgeScore.ToString() + "</td></tr>";
					}
				}
				strPaperString += " </table> ";
			}

			return strPaperString;
		}

		private string GetNo(int i)
		{
			string strReturn = string.Empty;

			switch (i.ToString())
			{
				case "0": strReturn = "һ";
					break;
				case "1": strReturn = "��";
					break;
				case "2": strReturn = "��";
					break;
				case "3": strReturn = "��";
					break;
				case "4": strReturn = "��";
					break;
				case "5": strReturn = "��";
					break;
				case "6": strReturn = "��";
					break;
				case "7": strReturn = "��";
					break;
				case "8": strReturn = "��";
					break;
				case "9": strReturn = "ʮ";
					break;
				case "10": strReturn = "ʮһ";
					break;
				case "11": strReturn = "ʮ��";
					break;
				case "12": strReturn = "ʮ��";
					break;
				case "13": strReturn = "ʮ��";
					break;
				case "14": strReturn = "ʮ��";
					break;
				case "15": strReturn = "ʮ��";
					break;
				case "16": strReturn = "ʮ��";
					break;
				case "17": strReturn = "ʮ��";
					break;
				case "18": strReturn = "ʮ��";
					break;
				case "19": strReturn = "��ʮ";
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
	}
}
