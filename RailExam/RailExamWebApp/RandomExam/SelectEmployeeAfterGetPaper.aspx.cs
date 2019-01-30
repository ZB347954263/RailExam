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

namespace RailExamWebApp.RandomExam
{
	public partial class SelectEmployeeAfterGetPaper : PageBase
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
                try
                {
                    OracleAccess db1 = new OracleAccess(ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString);
                    db1.RunSqlDataSet("select * from Org where level_num=2");
                }
                catch
                {
                    Response.Write("<script>alert('当前站段服务器无法访问路局服务器，不能临时添加考生！');window.close();</script>");
                    return;
                }

				ViewState["ExamID"] = Request.QueryString.Get("RandomExamID");
				string strId = Request.QueryString.Get("RandomExamID");
				if (strId != null && strId != "")
				{
					RandomExamArrangeBLL eaBll = new RandomExamArrangeBLL();
					IList<RailExam.Model.RandomExamArrange> ExamArranges = eaBll.GetRandomExamArranges(int.Parse(strId));

					if (ExamArranges.Count > 0)
					{
                        string strSql = "select a.* from Random_Exam_Arrange_Detail a "
                               + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
                               + " inner join Computer_Server c on c.Computer_server_ID=b.Computer_Server_ID"
                               + " where c.Computer_Server_No='" + PrjPub.ServerNo + "' "
                               + " and Random_Exam_ID=" + strId;

                        OracleAccess db = new OracleAccess();
                        DataSet ds = db.RunSqlDataSet(strSql);

					    string strChooseID=string.Empty;
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

                        GetAddIs(strId, ExamArranges[0].UserIds);

                        ViewState["ChooseId"] = strChooseID;
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

			string strRefresh = Request.Form.Get("Refresh");
			if (strRefresh != "" && strRefresh != null)
			{
				string strId = ViewState["ExamID"].ToString();
				if (strId != null && strId != "")
				{
					RandomExamArrangeBLL eaBll = new RandomExamArrangeBLL();
					IList<RailExam.Model.RandomExamArrange> ExamArranges = eaBll.GetRandomExamArranges(int.Parse(strId));

					if (ExamArranges.Count > 0)
					{
                        //string strAddIds = string.Empty;

                        //查询出考试所有考生安排明细
                        OracleAccess db = new OracleAccess();
                        /*
                        string strSql = "select * from Random_Exam_Arrange_Detail where Random_Exam_ID=" + strId;
                        DataSet dsAll = db.RunSqlDataSet(strSql);

                        //遍历添加考生后的考生主表，查询出添加了哪些考生
                        string[] str = ExamArranges[0].UserIds.Split(',');
                        for (int i = 0; i < str.Length; i++)
                        {
                            string strReplace = "," + str[i] + ",";
                            DataRow[] drs = dsAll.Tables[0].Select("','+User_Ids+',' like '%" + strReplace + "%'");

                            //如果在所有考生安排明细中，查询不到当前编辑的考生ID，说明该考生为新添加
                            if (drs.Length == 0)
                            {
                                if (strAddIds == string.Empty)
                                {
                                    strAddIds += str[i];
                                }
                                else
                                {
                                    strAddIds += "," + str[i];
                                }
                            }
                        }

                        if(hfAddIds.Value == "")
                        {
                            hfAddIds.Value = strAddIds.Replace(",", "|");
                        }
                        else
                        {
                            hfAddIds.Value += "|" + strAddIds.Replace(",", "|");
                        }

                        ViewState["AddIds"] = hfAddIds.Value;
                         * */

                        //查询考试在当前服务器下当前考试的考生信息
                        string strSql = "select a.* from Random_Exam_Arrange_Detail a "
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

                        GetAddIs(strId, ExamArranges[0].UserIds);

                        /*

                        //如果添加了考生，则默认将该考生的考场安排在本站段的某一个微机教室
                        if(ds.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(strAddIds))
                        {
                            if(PrjPub.IsServerCenter)
                            {
                                strSql = "update Random_Exam_Arrange_Detail "
                                     + "set User_ids = User_ids || '," + strAddIds + "' "
                                     + "where   Random_Exam_Arrange_Detail_ID =" + ds.Tables[0].Rows[0]["Random_Exam_Arrange_Detail_ID"];
                                db.ExecuteNonQuery(strSql);
                            }
                            else
                            {
                                eaBll.UpdateRandomExamArrangeDetailToServer(Convert.ToInt32(ds.Tables[0].Rows[0]["Random_Exam_Arrange_Detail_ID"]), strAddIds);
                            }
                                                    

                            strChooseID = strChooseID + "," + strAddIds;
                        }
                         * */

                        ViewState["ChooseId"] = strChooseID;
                        ViewState["UpdateMode"] = 1;
					}
					else
					{
						ViewState["ChooseId"] = "";
						ViewState["UpdateMode"] = 0;
					}

                    BindChoosedGrid(ViewState["ChooseId"].ToString());
				}
			}
		}

        private string GetAddIs(string strId,string userIds)
        {
            HasExamId();

            string strHasExamId = ViewState["HasExamId"].ToString();

            string strAddIds = string.Empty;

            //查询出考试所有考生安排明细
            OracleAccess db = new OracleAccess();
            string strSql = "select a.* from Random_Exam_Arrange_Detail a "
                               + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
                               + " inner join Computer_Server c on c.Computer_server_ID=b.Computer_Server_ID"
                               + " where c.Computer_Server_No='" + PrjPub.ServerNo + "' "
                               + " and Random_Exam_ID=" + strId;
            DataSet dsAll = db.RunSqlDataSet(strSql);

            string strNow = "";
            foreach (DataRow dr in dsAll.Tables[0].Rows)
            {
                if(strNow == "")
                {
                    strNow += dr["User_Ids"].ToString();
                }
                else
                {
                    strNow += "," + dr["User_Ids"];
                }
            }

            //遍历当前单位考生安排明细，查询出添加了哪些考生
            string[] str = strNow.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                string strReplace = "," + str[i] + ",";

                //如果某考生不在已生成试卷的考生中，则该考生为新增
                if ((","+strHasExamId+",").IndexOf(strReplace) == -1)
                {
                    if (strAddIds == string.Empty)
                    {
                        strAddIds += str[i];
                    }
                    else
                    {
                        strAddIds += "," + str[i];
                    }
                }
            }
            hfAddIds.Value = strAddIds.Replace(",", "|");
            ViewState["AddIds"] = hfAddIds.Value;
            return strAddIds;
        }

		protected void SaveChoose()
		{
			string strId = ViewState["ExamID"].ToString();

            string strEndId = "";

            //for (int i = 0; i < this.gvChoose.Rows.Count; i++)
            //{
            //    string strEmId = ((Label)this.gvChoose.Rows[i].FindControl("LabelEmployeeID")).Text;

            //    if (strEndId.Length == 0)
            //    {
            //        strEndId += strEmId;
            //    }
            //    else
            //    {
            //        if (strEndId == "0")
            //        {
            //            strEndId = strEmId;
            //        }
            //        else
            //        {
            //            strEndId += "," + strEmId;
            //        }
            //    }
            //}
            //if (ViewState["HasExamId"].ToString() != "" && strEndId != "")
            //{
            //    strEndId = ViewState["HasExamId"].ToString() + "," + strEndId;
            //}
            //else
            //{
            //    strEndId = "";
            //}

            //if (strEndId == "")
            //{
            //    strEndId = "0";
            //}

            OracleAccess db = new OracleAccess();
            //查询考试在当前站段考试的考生信息
		    string strSql = "select a.* from Random_Exam_Arrange_Detail a "
                            + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
		                    + " where Random_Exam_ID=" + strId;
            DataSet ds = db.RunSqlDataSet(strSql);

		    foreach (DataRow dr in ds.Tables[0].Rows)
		    {
                if (strEndId.Length == 0)
                {
                    strEndId += dr["User_Ids"].ToString();
                }
                else
                {
                    strEndId += "," + dr["User_Ids"];
                }
		    }


			//新增
			if (ViewState["UpdateMode"] != null && ViewState["UpdateMode"].ToString() == "0")
			{
				RandomExamArrange examArrange = new RandomExamArrange();
				examArrange.RandomExamId = int.Parse(strId);
				examArrange.UserIds = strEndId;
				examArrange.Memo = "";
				RandomExamArrangeBLL examArrangeBLL = new RandomExamArrangeBLL();
				if(!PrjPub.IsServerCenter)
				{
					examArrangeBLL.AddRandomExamArrangeToServer(examArrange);
				}
				else
				{
					examArrangeBLL.AddRandomExamArrange(examArrange);
				}
				ViewState["UpdateMode"] = 1;
				SessionSet.PageMessage = "保存成功！";
				return;
			}

			//修改
			if (ViewState["UpdateMode"] != null && ViewState["UpdateMode"].ToString() == "1")
			{
				RandomExamArrangeBLL examArrangeBLL = new RandomExamArrangeBLL();
				if (!PrjPub.IsServerCenter)
				{
					examArrangeBLL.UpdateRandomExamArrangeToServer(int.Parse(strId), strEndId);
				}
				else
				{
					examArrangeBLL.UpdateRandomExamArrange(int.Parse(strId), strEndId);
				}
				SessionSet.PageMessage = "保存成功！";
				return;
			}
		}

		//查找已生成试卷的人员信息
		private void HasExamId()
		{
			string strExamId = ViewState["ExamID"].ToString();

			RandomExamResultCurrentBLL objResultCurrentBll = new RandomExamResultCurrentBLL();
			IList<RandomExamResultCurrent> examResults = objResultCurrentBll.GetRandomExamResultInfo(Convert.ToInt32(strExamId)); 
			string strId = "";
			for (int i = 0; i < examResults.Count; i++)
			{
				string strEmId = examResults[i].ExamineeId.ToString();

				if((","+strId+",").IndexOf(","+strEmId+",") < 0 )
				{
					if (strId.Length == 0)
					{
						strId += strEmId;
					}
					else
					{
						strId += "," + strEmId;
					}
				}
			}
			ViewState["HasExamId"] = strId;
		}

		private void BindChoosedGrid(string strId)
		{
			HasExamId();

            //string[] str = ViewState["HasExamId"].ToString().Split(',');
            //for (int i = 0; i < str.Length; i++)
            //{
            //    string strEmId = str[i];
            //    string strOldAllId = "," + strId + ",";
            //    if (strOldAllId.IndexOf("," + strEmId + ",") == -1)
            //    {
            //        if (strId.Length == 0)
            //        {
            //            strId += strEmId;
            //        }
            //        else
            //        {
            //            strId = strEmId + "," + strId;
            //        }
            //    }
            //}

			EmployeeBLL psBLL = new EmployeeBLL();
			DataSet ds = new DataSet();

			IList<Employee> objList = new List<Employee>();

            //str = strId.Split(',');
            //if (str[0] != "")
            //{
            //    int n = 0;
            //    for (int i = str.Length-1; i >=0 ; i--)
            //    {
            //        Employee obj = psBLL.GetChooseEmployeeInfo(str[i]);
            //        obj.RowNum = n+1;
            //        objList.Add(obj);
            //        n++;
            //    }
            //    ds.Tables.Add(ConvertToDataTable((IList)objList));
            //    gvChoose.DataSource = objList;
            //    gvChoose.DataBind();
            //}
            //else
            //{
            //    BindEmptyGrid2();
            //}

            string[] str1 = strId.Split(',');

            if (str1[0] != "")
            {
                OracleAccess db = new OracleAccess();
                string strSql;
                for (int i = 0; i < str1.Length; i++)
                {
                    if(string.IsNullOrEmpty(str1[i]))
                    {
                        continue;
                    }

                    Employee obj = psBLL.GetChooseEmployeeInfo(str1[i]);
                    obj.RowNum = i + 1;
                    //if (string.IsNullOrEmpty(obj.WorkNo))
                    //{
                    //    strSql = "select identity_cardno from Employee where Employee_ID=" + str1[i];
                    //    obj.WorkNo = db.RunSqlDataSet(strSql).Tables[0].Rows[0][0].ToString();
                    //}
                    objList.Add(obj);
                }

                ds.Tables.Add(ConvertToDataTable((IList)objList));

                if (ViewState["Sort"] != null)
                {
                    ds.Tables[0].DefaultView.Sort = ViewState["Sort"].ToString();
                }

                DataColumn dc = ds.Tables[0].Columns.Add("ComputeRoom");

                strSql = "select a.*,c.Short_Name||'-'||b.Computer_Room_Name as ComputeRoom "
                    + " from Random_Exam_Arrange_Detail a "
                    + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
                    + " inner join Org c on b.Org_ID=c.Org_ID "
                    + " where Random_Exam_ID='" + ViewState["ExamID"] + "'";
                DataSet dsDetail = db.RunSqlDataSet(strSql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string strUser = "," + dr["EmployeeID"] + ",";
                    DataRow[] drs = dsDetail.Tables[0].Select("','+User_Ids+',' like '%" + strUser + "%'");

                    if (drs.Length > 0)
                    {
                        dr["ComputeRoom"] = drs[0]["ComputeRoom"].ToString();
                    }
                    else
                    {
                        dr["ComputeRoom"] = string.Empty;
                    }
                }

                gvChoose.DataSource = ds;
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
			dt.Columns.Add(new DataColumn("StrWorkNo", typeof(string)));
			dt.Columns.Add(new DataColumn("EmployeeName", typeof(string)));
			dt.Columns.Add(new DataColumn("PostName", typeof(string)));
            dt.Columns.Add(new DataColumn("ComputeRoom", typeof(string)));

			DataRow dr = dt.NewRow();

			dr["EmployeeID"] = 0;
			dr["OrgName"] = "";
			dr["StrWorkNo"] = "";
			dr["EmployeeName"] = "";
			dr["PostName"] = "";
			dr["RowNum"] = "";
            dr["ComputeRoom"] = "";
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
		    string strRemove = string.Empty;
			for (int i = 0; i < this.gvChoose.Rows.Count; i++)
			{
				CheckBox CheckBox1 = (CheckBox)this.gvChoose.Rows[i].FindControl("chkSelect2");
				string strEmId = ((Label)this.gvChoose.Rows[i].FindControl("LabelEmployeeID")).Text;
				if (CheckBox1.Checked)
				{
					strOldAllId = strOldAllId.Replace(strEmId + ",", "");

                    if (strRemove == string.Empty)
                    {
                        strRemove = strEmId;
                    }
                    else
                    {
                        strRemove += "," + strEmId;
                    }
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

            //移除考生需要清楚考生安排明细
            string strID = ViewState["ExamID"].ToString();
            OracleAccess db = new OracleAccess();
            //查询当前考试所有考生安排明细
            string strSql = "select * from Random_Exam_Arrange_Detail where Random_Exam_ID=" + strID;
            DataSet dsOther = db.RunSqlDataSet(strSql);

            //遍历当前需要移除的考生信息，查询考生安排明细是否存在当前需要移除的考生，如果存在则需修改去除该考生
            string[] str = strRemove.ToString().Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                string strReplace = "," + str[i] + ",";
                DataRow[] drs = dsOther.Tables[0].Select("','+User_Ids+',' like '%" + strReplace + "%'");

                if (drs.Length > 0)
                {
                    strSql = "update Random_Exam_Arrange_Detail "
                             + "set User_ids = substr(Replace(','||User_ids||',','" + strReplace +
                             "',','),2,length(Replace(','||User_ids||',','" + strReplace + "',','))-2) "
                             + "where  ','|| User_ids || ',' like '%" + strReplace + "%' and Random_Exam_ID=" +
                             strID;

                    if (PrjPub.IsServerCenter)
                    {
                        db.ExecuteNonQuery(strSql);
                    }
                    else
                    {
                        string strConn = ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString;
                        OracleAccess dbCenter = new OracleAccess(strConn);
                        dbCenter.ExecuteNonQuery(strSql);

                        RandomExamArrangeBLL objBll =new RandomExamArrangeBLL();
                        objBll.RefreshRandomExamArrange();
                    }
                }

                hfAddIds.Value =
                    ("," + (hfAddIds.Value.Replace("|", ",")) + ",").Replace(strReplace, ",").TrimStart(',').TrimEnd(',').Replace(",","|");
            }

			BindChoosedGrid(ViewState["ChooseId"].ToString());

			SaveChoose();
		}

		protected void btnExcel_Click(object sender, EventArgs e)
		{
			RandomExamBLL objBll = new RandomExamBLL();
			RailExam.Model.RandomExam objRandomExam = objBll.GetExam(Convert.ToInt32(ViewState["ExamID"].ToString()));

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

				ws.Cells[3 + j, 3] = ((Label)gvChoose.Rows[j].FindControl("LabelWorkNo")).Text;
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

				// 指定返回的是一个不能被客户端读取的流，必须被下载s
				this.Response.ContentType = "application/ms-excel";

				// 把文件流发送到客户端
				this.Response.WriteFile(file.FullName);
			}
			catch
			{
				SessionSet.PageMessage = "系统错误，导出Excel文件失败！";
			}
		}

		protected void btnCancel_Click(object sender, EventArgs e)
		{
            //移除考生需要清楚考生安排明细
            string strID = ViewState["ExamID"].ToString();
            OracleAccess db = new OracleAccess();
            //查询当前考试所有考生安排明细
            string strSql = "select * from Random_Exam_Arrange_Detail where Random_Exam_ID=" + strID;
            DataSet dsOther = db.RunSqlDataSet(strSql);

            //遍历当前需要移除的考生信息，查询考生安排明细是否存在当前需要移除的考生，如果存在则需修改去除该考生
            string[] str = ViewState["AddIds"].ToString().Split('|');
            for (int i = 0; i < str.Length; i++)
            {
                string strReplace = "," + str[i] + ",";
                DataRow[] drs = dsOther.Tables[0].Select("','+User_Ids+',' like '%" + strReplace + "%'");

                if (drs.Length > 0)
                {
                    strSql = "update Random_Exam_Arrange_Detail "
                             + "set User_ids = substr(Replace(','||User_ids||',','" + strReplace +
                             "',','),2,length(Replace(','||User_ids||',','" + strReplace + "',','))-2) "
                             + "where  ','|| User_ids || ',' like '%" + strReplace + "%' and Random_Exam_ID=" + strID;

                    if (PrjPub.IsServerCenter)
                    {
                        db.ExecuteNonQuery(strSql);
                    }
                    else
                    {
                        string strConn = ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString;
                        OracleAccess dbCenter = new OracleAccess(strConn);
                        dbCenter.ExecuteNonQuery(strSql);

                        RandomExamArrangeBLL objBll = new RandomExamArrangeBLL();
                        objBll.RefreshRandomExamArrange();
                    }
                }
            }

            //查询考试在当前站段考试的考生信息
            strSql = "select a.* from Random_Exam_Arrange_Detail a "
                     + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
		             + " where Random_Exam_ID=" + ViewState["ExamID"];
            DataSet ds = db.RunSqlDataSet(strSql);

		    string strEndId = string.Empty;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (strEndId.Length == 0)
                {
                    strEndId += dr["User_Ids"].ToString();
                }
                else
                {
                    strEndId += "," + dr["User_Ids"];
                }
            }

			RandomExamArrangeBLL examArrangeBLL = new RandomExamArrangeBLL();
			if (!PrjPub.IsServerCenter)
			{
                examArrangeBLL.UpdateRandomExamArrangeToServer(int.Parse(ViewState["ExamID"].ToString()), strEndId);
			}
			else
			{
                examArrangeBLL.UpdateRandomExamArrange(int.Parse(ViewState["ExamID"].ToString()), strEndId);
			}


			Response.Write("<script>top.returnValue ='true';top.close();</script>");
		}
	}
}
