using System;
using System.Collections.Generic;
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
using RailExam.BLL;
using RailExam.Model;

namespace RailExamWebApp.RandomExam
{
	public partial class RandomExamStudent : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				for (int i = 2007; i <= DateTime.Today.Year; i++)
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
			}

			string strEmployeeID = Request.Form.Get("employee");
			if (strEmployeeID != null && strEmployeeID != "")
			{
				ViewState["EmployeeID"] = strEmployeeID;
				hfEmployeeID.Value = strEmployeeID;
				EmployeeBLL objBll = new EmployeeBLL();
				txtEmployee.Text = objBll.GetEmployee(Convert.ToInt32(strEmployeeID)).EmployeeName;
			}
		}

		protected void btnSelect_Click(object sender, EventArgs e)
		{
			int employeeID = Convert.ToInt32(ViewState["EmployeeID"].ToString());

			DateTime begin, end;
			try
			{
				begin = Convert.ToDateTime(ddlYearSmall.SelectedValue + "-" + ddlMonthSmall.SelectedValue + "-" + "01");
				end = Convert.ToDateTime(ddlYearBig.SelectedValue + "-" + ddlMonthBig.SelectedValue + "-" + "01").AddMonths(1);
				hfBegin.Value = begin.ToString("yyyy-MM-dd");
				hfEnd.Value = end.ToString("yyyy-MM-dd");
			}
			catch
			{
				SessionSet.PageMessage = "日期选择不正确！";
				return;
			}

			if (employeeID == 0)
			{
				SessionSet.PageMessage = "请选择学员！";
				return;
			}

			RandomExamResultBLL objBll = new RandomExamResultBLL();

			string str = "and a.Begin_Time>to_date('" + hfBegin.Value + "','yyyy-MM-dd') and a.Begin_Time<=to_date('" + hfEnd.Value + "','yyyy-MM-dd') and a.EXAMINEE_ID=" + employeeID;

			if(ddlExamMode.SelectedValue != "0")
			{
				str += " and c.EXAM_STYLE=" + ddlExamMode.SelectedValue;
			}
		    IList<RailExam.Model.RandomExamResult> objList = objBll.GetRandomExamResults(str);
		    foreach (RandomExamResult randomExamResult in objList)
		    {
                randomExamResult.ExamTimeName = randomExamResult.ExamTime / 60 + "分" + randomExamResult.ExamTime % 60 + "秒";
		    }

			examsGrid.DataSource=objList;
			examsGrid.DataBind();

            if (examsGrid.DataSource != null)
            {
                Session["StudentExamInfo"] = examsGrid.DataSource;
                hfIsRef.Value = "true";
            }
		}

        protected void btnExcels_Click(object sender, EventArgs e)
        {
            if (hfRefreshExcel.Value == "true" && hfIsRef.Value == "true")
            {
                string strName = "学员考试成绩";
                string filename = Server.MapPath("/RailExamBao/Excel/" + strName + ".xls");

                if (File.Exists(filename))
                {
                    FileInfo file = new FileInfo(filename);
                    this.Response.Clear();
                    this.Response.Buffer = true;
                    this.Response.Charset = "utf-7";
                    this.Response.ContentEncoding = Encoding.UTF7;
                    // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                    this.Response.AddHeader("Content-Disposition",
                                            "attachment; filename=" + HttpUtility.UrlEncode(strName) + ".xls");
                    // 添加头信息，指定文件大小，让浏览器能够显示下载进度

                    this.Response.AddHeader("Content-Length", file.Length.ToString());

                    // 指定返回的是一个不能被客户端读取的流，必须被下载

                    this.Response.ContentType = "application/ms-excel";

                    // 把文件流发送到客户端

                    this.Response.WriteFile(file.FullName);
                }
            }
        }
	}
}
