using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using System.Collections.Generic;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
	public partial class RandomExamStatistic : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				for (int i = 2007; i <= DateTime.Today.Year;i++ )
				{
					ListItem item = new ListItem();
					item.Text = i.ToString();
					item.Value = i.ToString();
					ddlYearSmall.Items.Add(item);
				}

				for (int i = 2007; i <= DateTime.Today.Year; i++)
				{
					ListItem item = new ListItem();
					item.Text = i.ToString();
					item.Value = i.ToString();
					ddlYearBig.Items.Add(item);
				}

				for (int i = 1; i <= 12; i++)
				{
					ListItem item = new ListItem();
					item.Text = i.ToString();
					item.Value = i.ToString();
					ddlMonthSmall.Items.Add(item);
				}

				for (int i = 1; i <= 12; i++)
				{
					ListItem item = new ListItem();
					item.Text = i.ToString();
					item.Value = i.ToString();
					ddlMonthBig.Items.Add(item);
				}

				ddlYearSmall.SelectedValue = DateTime.Today.Year.ToString();
				ddlYearBig.SelectedValue = DateTime.Today.Year.ToString();
				ddlMonthBig.SelectedValue = DateTime.Today.Month.ToString();
				ddlMonthSmall.SelectedValue = "1";

				ViewState["ExamID"] = "0";
				hfExamID.Value = "0";
				ViewState["EmployeeID"] = "0";
				hfEmployeeID.Value = "0";

				ImgEmployee.Visible = false;
				ImgExam.Visible = false;
			}
			string strExamID = Request.Form.Get("ChooseExamID");
			if(strExamID != null && strExamID != "")
			{
				ViewState["ExamID"] = strExamID;
				hfExamID.Value = strExamID;
				RandomExamBLL objBll = new RandomExamBLL();
				txtExam.Text = objBll.GetExam(Convert.ToInt32(strExamID)).ExamName;
			}

			string strEmployeeID = Request.Form.Get("employee");
			if(strEmployeeID !=null && strEmployeeID != "")
			{
				ViewState["EmployeeID"] = strEmployeeID;
				hfEmployeeID.Value = strEmployeeID;
				EmployeeBLL objBll = new EmployeeBLL();
				txtEmployee.Text = objBll.GetEmployee(Convert.ToInt32(strEmployeeID)).EmployeeName;
			}

			if(HfBookId.Value != "" && (HfRangeType.Value == "3" || HfRangeType.Value == "4"))
			{
				txtBookChapter.Text = HfRangeName.Value;
			}		 
		}

		protected void btnSelect_Click(object sender, EventArgs e)
		{
		    btnQuery.Enabled = false;
			int bookID = 0;
			int chapterID = 0;
			int typeID = 0;
			int examID = Convert.ToInt32(ViewState["ExamID"].ToString());
			int employeeID = Convert.ToInt32(ViewState["EmployeeID"].ToString());

			int orgID = 0;

			DateTime begin, end;
			try
			{
				begin = Convert.ToDateTime(ddlYearSmall.SelectedValue + "-" + ddlMonthSmall.SelectedValue + "-" + "01");
				end = Convert.ToDateTime(ddlYearBig.SelectedValue + "-" + ddlMonthBig.SelectedValue + "-" + "01").AddMonths(1);
				hfBegin.Value = begin.ToShortDateString();
				hfEnd.Value = end.ToShortDateString();
			}
			catch
			{
				SessionSet.PageMessage = "����ѡ����ȷ��";
				return;
			}

			if(rbnBook.Checked)
			{
				if (HfBookId.Value == "")
				{
					SessionSet.PageMessage = "�̲��½ڲ���Ϊ�գ�";
					return;
				}

				bookID = Convert.ToInt32(HfBookId.Value);
				chapterID = Convert.ToInt32(HfChapterId.Value);
				typeID = Convert.ToInt32(HfRangeType.Value);
				if(PrjPub.CurrentLoginUser.SuitRange == 1)
				{
					orgID = -1;					
				}
				else
				{
					orgID = PrjPub.CurrentLoginUser.StationOrgID;
				}

				RandomExamStatisticBLL objBll = new RandomExamStatisticBLL();
				IList<RailExam.Model.RandomExamStatistic> objList =
					objBll.GetErrorItemInfo(bookID, chapterID, typeID, examID==0?-1:examID, begin, end,orgID);

				examsGrid.DataSource = objList;
				examsGrid.DataBind();
			}
			else if(rbnExam.Checked)
			{
				if(ViewState["ExamID"].ToString() == "0")
				{
					SessionSet.PageMessage = "��ѡ���ԣ�";
					return;
				}
				bookID = Convert.ToInt32(HfBookId.Value);
				chapterID = Convert.ToInt32(HfChapterId.Value);
				typeID = Convert.ToInt32(HfRangeType.Value);
				if (PrjPub.CurrentLoginUser.SuitRange == 1)
				{
                    orgID = -1;			
				}
				else
				{
					orgID = PrjPub.CurrentLoginUser.StationOrgID;
				}

				RandomExamStatisticBLL objBll = new RandomExamStatisticBLL();
				IList<RailExam.Model.RandomExamStatistic> objList =
					objBll.GetErrorItemInfo(bookID, chapterID, typeID, examID, begin, end, orgID);
				examsGrid.DataSource = objList;
				examsGrid.DataBind();
			}
			else if(rbnEmployee.Checked)
			{
				if(employeeID == 0)
				{
					SessionSet.PageMessage = "��ѡ��ѧԱ��";
					return;
				}

				RandomExamStatisticBLL objBll = new RandomExamStatisticBLL();
				IList<RailExam.Model.RandomExamStatistic> objList =
					objBll.GetErrorItemInfoByEmployeeID(employeeID, begin, end);
				examsGrid.DataSource = objList;
				examsGrid.DataBind();
			}
			if (examsGrid.DataSource != null)
			{
				Session["dtExamStatistic"] = examsGrid.DataSource;
				hfIsRef.Value = "true";
			}

            btnQuery.Enabled = true;
		}

		protected void rbnExam_CheckedChanged(object sender, EventArgs e)
		{
			//ddlYearBig.Enabled = false;
			//ddlMonthBig.Enabled = false;
			//ddlMonthSmall.Enabled = false;
			//ddlYearSmall.Enabled = false;
			ImgSelectChapterName.Visible = false;
			ImgEmployee.Visible = false;
			ImgExam.Visible = true;
			txtBookChapter.Text = "";
			HfBookId.Value = "0";
			HfChapterId.Value = "0";
			HfRangeType.Value = "0";
			txtEmployee.Text = "";
			hfEmployeeID.Value = "0";
			examsGrid.DataSource = null;
			examsGrid.DataBind();
			hfIsRef.Value = "";
 		}

		protected void rbnEmployee_CheckedChanged(object sender, EventArgs e)
		{
			//ddlYearBig.Enabled = false;
			//ddlMonthBig.Enabled = false;
			//ddlMonthSmall.Enabled = false;
			//ddlYearSmall.Enabled = false;
			ImgSelectChapterName.Visible = false;
			ImgExam.Visible = false;
			ImgEmployee.Visible = true;
			txtBookChapter.Text = "";
			HfBookId.Value = "0";
			HfChapterId.Value = "0";
			HfRangeType.Value = "0";
			txtExam.Text = "";
			hfExamID.Value = "0";
			examsGrid.DataSource = null;
			examsGrid.DataBind();
			hfIsRef.Value = "";
 		}

		protected void rbnBook_CheckedChanged(object sender, EventArgs e)
		{
			ImgExam.Visible = false;
			ImgEmployee.Visible = false;
			ddlYearBig.Enabled = true;
			ddlMonthBig.Enabled = true;
			ddlMonthSmall.Enabled = true;
			ddlYearSmall.Enabled = true;
			ImgSelectChapterName.Visible = true;
			txtEmployee.Text = "";
			hfEmployeeID.Value = "0";
			txtExam.Text = "";
			hfExamID.Value = "0";
			examsGrid.DataSource = null;
			examsGrid.DataBind();
			hfIsRef.Value = "";
		}

		private void DownloadExcel(string strName)
		{
			string filename = Server.MapPath("/RailExamBao/Excel/" + strName + ".xls");

			if (File.Exists(filename))
			{
				FileInfo file = new FileInfo(filename);
				this.Response.Clear();
				this.Response.Buffer = true;
				this.Response.Charset = "utf-7";
				this.Response.ContentEncoding = Encoding.UTF7;
				// ���ͷ��Ϣ��Ϊ"�ļ�����/���Ϊ"�Ի���ָ��Ĭ���ļ���
				this.Response.AddHeader("Content-Disposition",
										"attachment; filename=" + HttpUtility.UrlEncode(strName) + ".xls");
				// ���ͷ��Ϣ��ָ���ļ���С����������ܹ���ʾ���ؽ���

				this.Response.AddHeader("Content-Length", file.Length.ToString());

				// ָ�����ص���һ�����ܱ��ͻ��˶�ȡ���������뱻����

				this.Response.ContentType = "application/ms-excel";

				// ���ļ������͵��ͻ���

				this.Response.WriteFile(file.FullName);
			}
		}

		protected void btnExcels_Click(object sender, EventArgs e)
		{
			if (hfRefreshExcel.Value == "true" && hfIsRef.Value=="true")
			{
				DownloadExcel("���������Ϣ");
			}
		}
	}
}
