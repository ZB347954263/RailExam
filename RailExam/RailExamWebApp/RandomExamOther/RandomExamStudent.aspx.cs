using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using Microsoft.Office.Interop.Owc11;
using RailExam.BLL;
using System.Collections.Generic;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExamOther
{
	public partial class RandomExamStudent : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
				ButtonOutPutAll.Attributes.Add("onclick", "return confirm('您确定要移除全部考生吗 ？');");

				ViewState["mode"] = Request.QueryString.Get("mode");
				ViewState["startmode"] = Request.QueryString.Get("startmode");
				if (ViewState["mode"].ToString() == "ReadOnly")
				{
					this.btnInput.Visible = false;
					this.ButtonOutPut.Visible = false;
					this.ButtonOutPutAll.Visible = false;
				}

				string strId = Request.QueryString.Get("id");
				if (strId != null && strId != "")
				{
					RandomExamArrangeBLL eaBll = new RandomExamArrangeBLL();
					IList<RailExam.Model.RandomExamArrange> ExamArranges = eaBll.GetRandomExamArranges(int.Parse(strId));

					if (ExamArranges.Count > 0)
					{
						ViewState["ChooseId"] = ExamArranges[0].UserIds;
						ViewState["UpdateMode"] = 1;
					}
					else
					{
						ViewState["ChooseId"] = "";
						ViewState["UpdateMode"] = 0;
					}

					RandomExamBLL objBll = new RandomExamBLL();
					bool hasTrainClass = objBll.GetExam(Convert.ToInt32(strId)).HasTrainClass;
					if (hasTrainClass)
					{
						btnInput.Enabled = false;
						ButtonOutPut.Enabled = false;
						ButtonOutPutAll.Enabled = false;
					}
				}
				BindChoosedGrid(ViewState["ChooseId"].ToString());

			}

			string strRefresh = Request.Form.Get("Refresh");
			if (strRefresh != "" && strRefresh != null)
			{
				string strId = Request.QueryString.Get("id");
				if (strId != null && strId != "")
				{
					RandomExamArrangeBLL eaBll = new RandomExamArrangeBLL();
					IList<RailExam.Model.RandomExamArrange> ExamArranges =
						eaBll.GetRandomExamArranges(int.Parse(strId));

					if (ExamArranges.Count > 0)
					{
						ViewState["ChooseId"] = ExamArranges[0].UserIds;
						ViewState["UpdateMode"] = 1;
					}
					else
					{
						ViewState["ChooseId"] = "";
						ViewState["UpdateMode"] = 0;
					}
				}
				BindChoosedGrid(ViewState["ChooseId"].ToString());
			}

			string strChooseExamID = Request.Form.Get("ChooseExamID");
			if (strChooseExamID != "" && strChooseExamID != null)
			{
				RandomExamArrangeBLL objBll = new RandomExamArrangeBLL();
				IList<RailExam.Model.RandomExamArrange> ExamArranges = objBll.GetRandomExamArranges(int.Parse(Request.QueryString.Get("id")));
				IList<RailExam.Model.RandomExamArrange> objList = objBll.GetRandomExamArranges(int.Parse(strChooseExamID));
				if (ExamArranges.Count > 0)
				{
					string[] str = ExamArranges[0].UserIds.Split(',');
					if (objList.Count > 0)
					{
						for (int i = 0; i < str.Length; i++)
						{
							if (("," + objList[0].UserIds + ",").IndexOf("," + str[i] + ",") != -1)
							{
								objList[0].UserIds = ("," + objList[0].UserIds + ",").Replace("," + str[i] + ",", ",");
							}
						}

						if (ExamArranges[0].UserIds == "")
						{
							ViewState["ChooseId"] = objList[0].UserIds.TrimEnd(',').TrimStart(',');
						}
						else
						{
							if (objList[0].UserIds.TrimEnd(',').TrimStart(',') == "")
							{
								ViewState["ChooseId"] = ExamArranges[0].UserIds;
							}
							else
							{
								ViewState["ChooseId"] = ExamArranges[0].UserIds + "," + objList[0].UserIds.TrimEnd(',').TrimStart(',');
							}
						}
					}
					else
					{
						ViewState["ChooseId"] = ExamArranges[0].UserIds;
					}
					ViewState["UpdateMode"] = 1;
				}
				else
				{
					if (objList.Count > 0)
					{
						ViewState["ChooseId"] = objList[0].UserIds;
					}
					else
					{
						ViewState["ChooseId"] = "";
					}
					ViewState["UpdateMode"] = 0;
				}
				BindChoosedGrid(ViewState["ChooseId"].ToString());
				SaveChoose();
			}
		}

		private void HasExamId()
		{
			string strExamId = Request.QueryString.Get("id");

			//已经参加考试的考生自动填充上
			RandomExamResultBLL reBll = new RandomExamResultBLL();
			IList<RailExam.Model.RandomExamResult> examResults = reBll.GetRandomExamResultByExamID(int.Parse(strExamId));
			string strId = "";
			for (int i = 0; i < examResults.Count; i++)
			{
				string strEmId = examResults[i].ExamineeId.ToString();

				if (strId.Length == 0)
				{
					strId += strEmId;
				}
				else
				{
					strId += "," + strEmId;
				}
			}
			ViewState["HasExamId"] = strId; ;
		}

		private void BindChoosedGrid(string strId)
		{
			HasExamId();

			string strExamId = Request.QueryString.Get("id");
			//已经参加考试的考生自动填充上

			RandomExamResultBLL reBll = new RandomExamResultBLL();
			IList<RailExam.Model.RandomExamResult> examResults = reBll.GetRandomExamResultByExamID(int.Parse(strExamId));

			for (int i = 0; i < examResults.Count; i++)
			{
				string strEmId = examResults[i].ExamineeId.ToString();
				string strOldAllId = "," + strId + ",";
				if (strOldAllId.IndexOf("," + strEmId + ",") == -1)
				{
					if (strId.Length == 0)
					{
						strId += strEmId;
					}
					else
					{
						strId = strEmId + "," + strId;
					}
				}
			}

			EmployeeBLL psBLL = new EmployeeBLL();
			DataSet ds = new DataSet();

			string[] str = strId.Split(',');
			IList<Employee> objList = new List<Employee>();

			if (str[0] != "")
			{
				for (int i = 0; i < str.Length; i++)
				{
					Employee obj = psBLL.GetChooseEmployeeInfo(str[i]);
					obj.RowNum = i + 1;
					objList.Add(obj);
				}

				ds.Tables.Add(ConvertToDataTable((IList)objList));
				gvChoose.DataSource = objList;
				gvChoose.DataBind();
			}
			else
			{
				BindEmptyGrid2();
			}
		}

		private void BindEmptyGrid2()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add(new DataColumn("RowNum", typeof(string)));
			dt.Columns.Add(new DataColumn("EmployeeID", typeof(int)));
			dt.Columns.Add(new DataColumn("OrgName", typeof(string)));
			dt.Columns.Add(new DataColumn("WorkNo", typeof(string)));
			dt.Columns.Add(new DataColumn("EmployeeName", typeof(string)));
			dt.Columns.Add(new DataColumn("PostName", typeof(string)));

			DataRow dr = dt.NewRow();

			dr["EmployeeID"] = 0;
			dr["OrgName"] = "";
			dr["WorkNo"] = "";
			dr["EmployeeName"] = "";
			dr["PostName"] = "";
			dr["RowNum"] = "";
			dt.Rows.Add(dr);

			gvChoose.DataSource = dt;
			gvChoose.DataBind();

			CheckBox CheckBox1 = (CheckBox)this.gvChoose.Rows[0].FindControl("chkSelect2");
			CheckBox1.Visible = false;
		}

		public static DataTable ConvertToDataTable(IList list)
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

		protected void ButtonOutPut_Click(object sender, EventArgs e)
		{
			string strAllId = ViewState["ChooseId"].ToString();
			if (strAllId == "")
			{
				return;
			}

			string strOldAllId = "," + strAllId + ",";
			for (int i = 0; i < this.gvChoose.Rows.Count; i++)
			{
				CheckBox CheckBox1 = (CheckBox)this.gvChoose.Rows[i].FindControl("chkSelect2");
				string strEmId = ((Label)this.gvChoose.Rows[i].FindControl("LabelEmployeeID")).Text;
				if (CheckBox1.Checked)
				{
					strOldAllId = strOldAllId.Replace(strEmId + ",", "");
				}
			}

			int n = strOldAllId.Length;
			if (n == 1)
			{
				ViewState["ChooseId"] = "";
			}
			else
			{
				ViewState["ChooseId"] = strOldAllId.Substring(1, n - 2);
			}
			BindChoosedGrid(ViewState["ChooseId"].ToString());

			SaveChoose();
		}

		protected void ButtonOutPutAll_Click(object sender, EventArgs e)
		{
			ViewState["ChooseId"] = "";

			BindChoosedGrid(ViewState["ChooseId"].ToString());

			SaveChoose();
		}

		protected void btnLast_Click(object sender, ImageClickEventArgs e)
		{
			string strId = Request.QueryString.Get("id");
			string strStartMode = ViewState["startmode"].ToString();
			string strFlag = "";

			if (ViewState["mode"].ToString() == "Insert")
			{
				strFlag = "Edit";
			}
			else if (ViewState["mode"].ToString() == "Edit")
			{
				RandomExamResultBLL reBll = new RandomExamResultBLL();
				IList<RailExam.Model.RandomExamResult> examResults = reBll.GetRandomExamResultByExamID(Convert.ToInt32(strId));

				if (examResults.Count > 0)
				{
					strFlag = "ReadOnly";
				}
				else
				{
					strFlag = ViewState["mode"].ToString();
				}
			}
			else
			{
				strFlag = ViewState["mode"].ToString();
			}


			string strItemType = "";
			RandomExamSubjectBLL objSubjectBll = new RandomExamSubjectBLL();
			IList<RandomExamSubject> objSubjectList = objSubjectBll.GetRandomExamSubjectByRandomExamId(Convert.ToInt32(strId));
			foreach (RandomExamSubject subject in objSubjectList)
			{
				if (strItemType == "")
				{
					strItemType = subject.ItemTypeId.ToString();
				}
				else
				{
					strItemType = strItemType + "|" + subject.ItemTypeId;
				}
			}
			Response.Redirect("/RailExamBao/RandomExamOther/RandomExamStrategyInfo.aspx?startmode=" + strStartMode + "&mode=" + strFlag + "&itemType=" + strItemType + "&id=" + strId);
		}

		protected void SaveChoose()
		{
			string strId = Request.QueryString.Get("id");

			string strEndId = "";

			for (int i = 0; i < this.gvChoose.Rows.Count; i++)
			{
				string strEmId = ((Label)this.gvChoose.Rows[i].FindControl("LabelEmployeeID")).Text;

				if (strEndId.Length == 0)
				{
					strEndId += strEmId;
				}
				else
				{
					if (strEndId == "0")
					{
						strEndId = strEmId;
					}
					else
					{
						strEndId += "," + strEmId;
					}
				}
			}

			if (strEndId == "")
			{
				strEndId = "0";
			}

			if (ViewState["UpdateMode"] != null && ViewState["UpdateMode"].ToString() == "0")
			{
				RandomExamArrange examArrange = new RandomExamArrange();
				examArrange.RandomExamId = int.Parse(strId);
				examArrange.UserIds = strEndId;
				examArrange.Memo = "";
				RandomExamArrangeBLL examArrangeBLL = new RandomExamArrangeBLL();
				examArrangeBLL.AddRandomExamArrange(examArrange);
				ViewState["UpdateMode"] = 1;
				SessionSet.PageMessage = "保存成功！";
				return;
			}

			if (ViewState["UpdateMode"] != null && ViewState["UpdateMode"].ToString() == "1")
			{
				RandomExamArrangeBLL examArrangeBLL = new RandomExamArrangeBLL();
				examArrangeBLL.UpdateRandomExamArrange(int.Parse(strId), strEndId);
				SessionSet.PageMessage = "保存成功！";
				return;
			}
		}

		protected void btnCancel_Click(object sender, ImageClickEventArgs e)
		{
			if (ViewState["mode"].ToString() == "Edit")
			{
				//Response.Write("<script>window.opener.form1.Refresh.value = 'true',window.opener.form1.submit();window.close();</script>");
				Response.Write("<script>top.returnValue='true';top.window.close();</script>");
			}
			else if (ViewState["mode"].ToString() == "Insert")
			{
				//Response.Write("<script>window.opener.form1.Refresh.value = 'true',window.opener.form1.submit();window.close();</script>");
				Response.Write("<script>top.returnValue='true';top.window.close();</script>");
			}
			else
			{
				Response.Write("<script>window.close();</script>");
			}
		}

		protected void btnExcel_Click(object sender, EventArgs e)
		{
			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam objRandomExam = objBll.GetExam(Convert.ToInt32(Request.QueryString.Get("id")));

			SpreadsheetClass xlsheet = new SpreadsheetClass();
			Worksheet ws = (Worksheet)xlsheet.Worksheets[1];
			ws.Cells.Font.set_Size(10);
			ws.Cells.Font.set_Name("宋体");

			ws.Cells[1, 1] = objRandomExam.ExamName + " 参加考试学员名单";
			Range rang1 = ws.get_Range(ws.Cells[1, 1], ws.Cells[1, 7]);
			rang1.set_MergeCells(true);
			rang1.set_HorizontalAlignment(XlHAlign.xlHAlignCenter);
			rang1.Font.set_Name("宋体");


			//write headertext
			ws.Cells[2, 1] = "序号";
			((Range)ws.Cells[2, 1]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);


			ws.Cells[2, 2] = "姓名";
			ws.get_Range(ws.Cells[2, 2], ws.Cells[2, 2]).set_MergeCells(true);
			ws.get_Range(ws.Cells[2, 2], ws.Cells[2, 2]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

			ws.Cells[2, 3] = "员工编码";
			ws.get_Range(ws.Cells[2, 3], ws.Cells[2, 3]).set_MergeCells(true);
			ws.get_Range(ws.Cells[2, 3], ws.Cells[2, 3]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

			ws.Cells[2, 4] = "职名";
			ws.get_Range(ws.Cells[2, 4], ws.Cells[2, 4]).set_MergeCells(true);
			ws.get_Range(ws.Cells[2, 4], ws.Cells[2, 4]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

			ws.Cells[2, 5] = "组织机构";
			ws.get_Range(ws.Cells[2, 5], ws.Cells[2, 7]).set_MergeCells(true);
			ws.get_Range(ws.Cells[2, 5], ws.Cells[2, 7]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

			for (int j = 0; j < gvChoose.Rows.Count; j++)
			{
				ws.Cells[3 + j, 1] = ((Label)gvChoose.Rows[j].FindControl("lblNo")).Text;

				ws.Cells[3 + j, 2] = ((Label)gvChoose.Rows[j].FindControl("LabelName")).Text;
				ws.get_Range(ws.Cells[3 + j, 2], ws.Cells[3 + j, 2]).set_MergeCells(true);
				ws.get_Range(ws.Cells[3 + j, 2], ws.Cells[3 + j, 2]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);

				ws.Cells[3 + j, 3] = "'" + ((Label)gvChoose.Rows[j].FindControl("LabelWorkNo")).Text;
				ws.get_Range(ws.Cells[3 + j, 3], ws.Cells[3 + j, 3]).set_MergeCells(true);
				ws.get_Range(ws.Cells[3 + j, 3], ws.Cells[3 + j, 3]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);


				ws.Cells[3 + j, 4] = ((Label)gvChoose.Rows[j].FindControl("LabelPostName")).Text;
				ws.get_Range(ws.Cells[3 + j, 4], ws.Cells[3 + j, 4]).set_MergeCells(true);
				ws.get_Range(ws.Cells[3 + j, 4], ws.Cells[3 + j, 4]).set_HorizontalAlignment(XlHAlign.xlHAlignLeft);

				ws.Cells[3 + j, 5] = ((Label)gvChoose.Rows[j].FindControl("Labelorgid")).Text;
				ws.get_Range(ws.Cells[3 + j, 5], ws.Cells[3 + j, 7]).set_MergeCells(true);
				ws.get_Range(ws.Cells[3 + j, 5], ws.Cells[3 + j, 7]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);
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

				FileInfo file = new FileInfo(path);
				this.Response.Clear();
				this.Response.Buffer = true;
				this.Response.Charset = "utf-7";
				this.Response.ContentEncoding = Encoding.UTF7;
				// 添加头信息，为"文件下载/另存为"对话框指定默认文件名
				this.Response.AddHeader("Content-Disposition",
										"attachment; filename=" + HttpUtility.UrlEncode(objRandomExam.ExamName + "参加考试学员名单") + ".xls");
				// 添加头信息，指定文件大小，让浏览器能够显示下载进度
				this.Response.AddHeader("Content-Length", file.Length.ToString());

				// 指定返回的是一个不能被客户端读取的流，必须被下载
				this.Response.ContentType = "application/ms-excel";

				// 把文件流发送到客户端
				this.Response.WriteFile(file.FullName);
			}
			catch
			{
				SessionSet.PageMessage = "系统错误，导出Excel文件失败！";
			}
		}
	}
}
