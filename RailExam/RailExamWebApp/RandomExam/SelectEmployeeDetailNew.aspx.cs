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

namespace RailExamWebApp.Exam
{
    public partial class SelectEmployeeDetailNew : PageBase
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
                    btnSelectComputeRoom.Visible = false;
                }

                string strId = Request.QueryString.Get("id");
                if (strId != null && strId != "")
                {
                    hfExamID.Value = strId;
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

                    string strSql = "select Org_ID,Short_Name from Org where level_num=2 order by Parent_ID,Order_Index";
                    OracleAccess db = new OracleAccess();
                    DataSet ds = db.RunSqlDataSet(strSql);
                    ListItem item = new ListItem();
                    item.Text = "--请选择--";
                    item.Value = "0";
                    ddlOrg.Items.Add(item);
                    bool hasOrg = false;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        item = new ListItem();
                        item.Text = dr["Short_Name"].ToString();
                        item.Value = dr["org_ID"].ToString();
                        ddlOrg.Items.Add(item);

                        if (PrjPub.CurrentLoginUser.StationOrgID.ToString() == dr["org_ID"].ToString())
                        {
                            hasOrg = true;
                        }
                    }

                    //if (PrjPub.CurrentLoginUser.RoleID != 1)
                    //{
                    //    if (hasOrg)
                    //    {
                    //        ddlOrg.SelectedValue = PrjPub.CurrentLoginUser.StationOrgID.ToString();
                    //        //ddlOrg.Enabled = false;
                    //    }
                    //    else
                    //    {
                    //        ddlOrg.SelectedValue = "0";
                    //    }
                    //}
                    //else
                    //{
                    //    ddlOrg.SelectedValue = "0";
                    //}

                    ddlOrg.SelectedValue = "0";

                    //ddlOrg_SelectedIndexChanged(null, null);

					RandomExamBLL objBll = new RandomExamBLL();
					bool hasTrainClass = objBll.GetExam(Convert.ToInt32(strId)).HasTrainClass;
					if (hasTrainClass)
					{
						ViewState["HasTrainClass"] = "1";
						btnInput.Enabled = false;
						//ButtonOutPut.Enabled = false;
						ButtonOutPutAll.Enabled = false;
					}
					else
					{
						ViewState["HasTrainClass"] = "0";
					}
                }
                BindChoosedGrid(ViewState["ChooseId"].ToString());

            }

            //string strChooseID = Request.Form.Get("ChooseID");
            //if (strChooseID != "" && strChooseID != null)
            //{
            //    ViewState["ChooseId"] = strChooseID;
            //    BindChoosedGrid(ViewState["ChooseId"].ToString());
            //    SaveChoose();
            //}

            string strRefresh = Request.Form.Get("Refresh");
            if(strRefresh != "" && strRefresh != null)
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
            if(strChooseExamID != "" && strChooseExamID != null)
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
                             if (("," + objList[0].UserIds+",").IndexOf("," + str[i]+",") != -1)
                             {
                                 objList[0].UserIds = ("," + objList[0].UserIds + ",").Replace("," + str[i] + ",", ",");
                             }
                         }

						 if(ExamArranges[0].UserIds == "")
						 {
							 ViewState["ChooseId"] = objList[0].UserIds.TrimEnd(',').TrimStart(',');
						 }
						 else
						 {
							 if(objList[0].UserIds.TrimEnd(',').TrimStart(',') == "")
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
                     if(objList.Count > 0)
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

             string strRefreshArrange = Request.Form.Get("RefreshArrange");
             if (strRefreshArrange != "" && strRefreshArrange != null)
             {
                 BindChoosedGrid(ViewState["ChooseId"].ToString());

                 if (CheckIsAllArrange())
                 {
                     string strSql = " update Random_Exam set Is_All_Arrange=1 where Random_Exam_ID=" +
                                Request.QueryString.Get("id");
                     OracleAccess db = new OracleAccess();
                     db.ExecuteNonQuery(strSql);
                 }
             }

             if (Request.Form.Get("StudentInfo") != null && Request.Form.Get("StudentInfo") != "")
             {
                 DownloadStudentInfoExcel();
             }
         }

         private void DownloadStudentInfoExcel()
         {
             string path = Server.MapPath("/RailExamBao/Excel/Excel.xls");

             RandomExamBLL objBll = new RandomExamBLL();
             RailExam.Model.RandomExam objRandomExam = objBll.GetExam(Convert.ToInt32(Request.QueryString.Get("id")));

             if (File.Exists(path))
             {
                 FileInfo file = new FileInfo(path);
                 this.Response.Clear();
                 this.Response.Buffer = true;
                 this.Response.Charset = "utf-7";
                 this.Response.ContentEncoding = Encoding.UTF7;
                 // 添加头信息，为"文件下载/另存为"对话框指定默认文件名
                 this.Response.AddHeader("Content-Disposition",
                                         "attachment; filename=" + HttpUtility.UrlEncode(objRandomExam.ExamName + "参加考试学员名单") +
                                         ".xls");
                 // 添加头信息，指定文件大小，让浏览器能够显示下载进度
                 this.Response.AddHeader("Content-Length", file.Length.ToString());

                 // 指定返回的是一个不能被客户端读取的流，必须被下载
                 this.Response.ContentType = "application/ms-excel";

                 // 把文件流发送到客户端
                 this.Response.WriteFile(file.FullName);
             }
         }


        private void HasExamId()
        {
            string strExamId = Request.QueryString.Get("id");

            //已经参加考试的考生自动填充上
            //RandomExamResultBLL reBll = new RandomExamResultBLL();
            //IList<RailExam.Model.RandomExamResult> examResults = reBll.GetRandomExamResultByExamID(int.Parse(strExamId));
            //string strId = "";
            //for (int i = 0; i < examResults.Count; i++)
            //{
            //    string strEmId = examResults[i].ExamineeId.ToString();

            //    if (strId.Length == 0)
            //    {
            //        strId += strEmId;
            //    }
            //    else
            //    {
            //        strId += "," + strEmId;
            //    }
            //}

            //已经被安排微机教室并已经生成试卷的考生
            string strSql = "select * from Random_Exam_Arrange_Detail "
                            + " where Random_Exam_ID=" + strExamId
                            + " and Computer_Room_ID in (select distinct Computer_Room_ID "
                            + " from Computer_Room where Computer_Server_ID in ("
                            + " select b.Computer_Server_ID from Random_Exam_Computer_Server a "
                            + " inner join Computer_Server b on to_char(a.Computer_Server_No)=b.Computer_Server_No"
                            + " where a.Random_Exam_ID=" + strExamId + " and a.Has_Paper=1))";
            OracleAccess db = new OracleAccess();
            DataSet ds = db.RunSqlDataSet(strSql);
            string strId = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string strEmId = ds.Tables[0].Rows[i]["User_Ids"].ToString();

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
                        strId = strEmId+ "," +strId ;
                    }
                }
            }

            EmployeeBLL psBLL = new EmployeeBLL();
            DataSet ds = new DataSet();

            //string strIDs = "," + strId + ",";
            //if (strIDs.Length > 2000)
            //{
            //    ds = psBLL.GetEmployeesByEmployeeIdS(strIDs);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        gvChoose.DataSource = ds;
            //        gvChoose.DataBind();
            //    }
            //    else
            //    {
            //        BindEmptyGrid2();
            //    }
            //}
            //else
            //{
            //    IList<Employee> objList = psBLL.GetEmployeesByEmployeeId(strIDs);
            //    if (objList.Count > 0)
            //    {
            //        ds.Tables.Add(ConvertToDataTable((IList)objList));
            //        gvChoose.DataSource = objList;
            //        gvChoose.DataBind();
            //    }
            //    else
            //    {
            //        BindEmptyGrid2();
            //    }
            //}

            string[] str = strId.Split(',');
            IList<Employee> objList = new List<Employee>() ;

            if(str[0] != "")
            {
                OracleAccess db = new OracleAccess();
                string strSql;

                string strQuery = GetSql();
                ArrayList objEmloyeeList = new ArrayList();
                if (!string.IsNullOrEmpty(strQuery))
                {
                    IList<Employee> objSelectList = psBLL.GetEmployeeByWhereClause("1=1" + strQuery);

                    foreach (Employee employee in objSelectList)
                    {
                        objEmloyeeList.Add(employee.EmployeeID.ToString());
                    }
                }

                for (int i = 0; i < str.Length; i++)
                {
                    if (string.IsNullOrEmpty(str[i]))
                    {
                        continue;
                    }

                    Employee obj = psBLL.GetChooseEmployeeInfo(str[i]);
                    obj.RowNum = i + 1;
                    //if(string.IsNullOrEmpty(obj.WorkNo))
                    //{
                    //    strSql = "select identity_cardno from Employee where Employee_ID=" + str[i];
                    //    obj.WorkNo = db.RunSqlDataSet(strSql).Tables[0].Rows[0][0].ToString();
                    //}

                    if (!string.IsNullOrEmpty(strQuery))
                    {
                        if (objEmloyeeList.IndexOf(str[i]) >= 0)
                        {
                            objList.Add(obj);
                        }
                    }
                    else
                    {
                        objList.Add(obj);
                    }
                }

                if (objList.Count > 0)
                {
                    ds.Tables.Add(ConvertToDataTable((IList) objList));

                    if (ViewState["Sort"] != null)
                    {
                        try
                        {
                            ds.Tables[0].DefaultView.Sort = ViewState["Sort"].ToString();
                        }
                        catch
                        {
                        }
                    }

                    DataColumn dc = ds.Tables[0].Columns.Add("ComputeRoom");

                    strSql = "select a.*,c.Short_Name||'-'||b.Computer_Room_Name as ComputeRoom "
                             + " from Random_Exam_Arrange_Detail a "
                             + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
                             + " inner join Org c on b.Org_ID=c.Org_ID"
                             + " where Random_Exam_ID='" + strExamId + "'";
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
            else
            {
                BindEmptyGrid2();
            }
        }

		protected void gvChoose_OnSorting(object sender, GridViewSortEventArgs e)
		{
			if(ViewState["Sort"] != null)
			{
				if(ViewState["Sort"].ToString().Replace(" desc","") == e.SortExpression)
				{
					if(ViewState["Sort"].ToString().IndexOf("desc")>=0)
					{
						ViewState["Sort"] = ViewState["Sort"].ToString().Replace(" desc", "");
					}
					else
					{

						ViewState["Sort"] = ViewState["Sort"].ToString() + " desc";
					}
					
				}
				else
				{
					ViewState["Sort"] = e.SortExpression;
				}
			}
			else
			{
				ViewState["Sort"] = e.SortExpression;
			}

			DataTable dt = new DataTable();
			dt.Columns.Add(new DataColumn("RowNum", typeof(int)));
			dt.Columns.Add(new DataColumn("EmployeeID", typeof(int)));
			dt.Columns.Add(new DataColumn("OrgName", typeof(string)));
            dt.Columns.Add(new DataColumn("StrWorkNo", typeof(string)));
			dt.Columns.Add(new DataColumn("EmployeeName", typeof(string)));
			dt.Columns.Add(new DataColumn("PostName", typeof(string)));
            dt.Columns.Add(new DataColumn("ComputeRoom", typeof(string)));

			for(int i=0; i<gvChoose.Rows.Count; i++)
			{
				DataRow dr = dt.NewRow();
				dr["EmployeeID"] = ((Label) gvChoose.Rows[i].Cells[1].FindControl("LabelEmployeeID")).Text;
				dr["RowNum"] = Convert.ToInt32(((Label)gvChoose.Rows[i].Cells[1].FindControl("lblNo")).Text);
				dr["EmployeeName"] = ((Label)gvChoose.Rows[i].Cells[1].FindControl("LabelName")).Text; 
                dr["StrWorkNo"] = ((Label)gvChoose.Rows[i].Cells[1].FindControl("LabelWorkNo")).Text; 
				dr["OrgName"] = ((Label)gvChoose.Rows[i].Cells[1].FindControl("Labelorgid")).Text; 
				dr["PostName"] = ((Label)gvChoose.Rows[i].Cells[1].FindControl("LabelPostName")).Text;
                dr["ComputeRoom"] = ((Label)gvChoose.Rows[i].Cells[1].FindControl("lblComputeRoom")).Text; 
				dt.Rows.Add(dr);
			}

			dt.DefaultView.Sort = ViewState["Sort"].ToString();

			gvChoose.DataSource = dt;
			gvChoose.DataBind();
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

                    if(strRemove== string.Empty)
                    {
                        strRemove = strEmId;
                    }
                    else
                    {
                        strRemove += ","+strEmId;
                    }
                }
            }

            int n = strOldAllId.Length;
            if (n == 1)
            {
				if (ViewState["HasTrainClass"].ToString() == "1")
				{
					SessionSet.PageMessage = "培训班考试不能移除全部考生！";
					return;
				}
                ViewState["ChooseId"] = "";
            }
            else
            {
                ViewState["ChooseId"] = strOldAllId.Substring(1, n - 2);
            }

            //移除考生需要清楚考生安排明细
            string strID = Request.QueryString.Get("id");
            OracleAccess db = new OracleAccess();
            //查询当前考试所有考生安排明细
            string strSql = "select * from Random_Exam_Arrange_Detail where Random_Exam_ID=" + strID;
            DataSet dsOther = db.RunSqlDataSet(strSql);

            //遍历当前需要移除的考生信息，查询考生安排明细是否存在当前需要移除的考生，如果存在则需修改去除该考生
            string[] str = strRemove.Split(',');
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

                    db.ExecuteNonQuery(strSql);
                }
            }

            //移除User_Ids为空的考生安排明细
            strSql = "delete from Random_Exam_Arrange_Detail where Random_Exam_ID=" + strID +
                     " and  User_Ids is null";
            db.ExecuteNonQuery(strSql);

            //删除没有相关微机教室的Random_Exam_Computer_Server数据
            strSql =
                @"delete from random_exam_computer_server 
                    where Random_Exam_ID=" + strID + @"
                    and Computer_Server_No not in (select  to_number(c.Computer_Server_No)
                    from Random_Exam_Arrange_Detail a
                    inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID
                    inner join Computer_Server c on b.Computer_Server_ID=c.Computer_Server_ID
                    where Random_Exam_ID=" + strID + @")";
            db.ExecuteNonQuery(strSql);

            BindChoosedGrid(ViewState["ChooseId"].ToString());

            SaveChoose();
        }

        protected void ButtonOutPutAll_Click(object sender, EventArgs e)
        {
            ViewState["ChooseId"] = "";

            OracleAccess db = new OracleAccess();
            string strSql = "delete  from Random_Exam_Arrange_Detail where Random_Exam_ID=" +
                            Request.QueryString.Get("id");
            db.ExecuteNonQuery(strSql);

            //删除没有相关微机教室的Random_Exam_Computer_Server数据
            strSql =
                @"delete from random_exam_computer_server 
                    where Random_Exam_ID=" + Request.QueryString.Get("id");
            db.ExecuteNonQuery(strSql);

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

			if(PrjPub.IsWuhan())
			{
				Response.Redirect("RandomExamManageThird.aspx?startmode=" + strStartMode + "&mode=" + strFlag + "&id=" + strId);
			}
			else
			{
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
        }

        private string GetSql()
        {
            string strSql = "";

            if (ddlOrg.SelectedValue != "0")
            {
                if (ddlWorkShop.SelectedValue != "0")
                {
                    strSql += " and  c.id_path || '/' like '/1/" + ddlOrg.SelectedValue + "/" + ddlWorkShop.SelectedValue + "/%'";
                }
                else
                {
                    strSql += " and  c.id_path || '/' like '/1/" + ddlOrg.SelectedValue + "/%'";
                }
            }

            if (!string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                strSql += " and Employee_Name like '%" + txtName.Text.Trim() + "%'";
            }

            if (!string.IsNullOrEmpty(txtPinyin.Text.Trim()))
            {
                strSql += " and Pinyin_Code ='" + txtPinyin.Text.Trim().ToUpper() + "'";
            }

            return strSql;
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            BindChoosedGrid(ViewState["ChooseId"].ToString());
        }

        protected void ddlOrg_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strSql = "select Org_ID,Short_Name from Org a "
                + " where Parent_ID =" + ddlOrg.SelectedValue + " and level_num=3 order by order_index";
            OracleAccess db = new OracleAccess();
            DataSet ds = db.RunSqlDataSet(strSql);

            ddlWorkShop.Items.Clear();

            ListItem item = new ListItem();
            item.Text = "--请选择--";
            item.Value = "0";
            ddlWorkShop.Items.Add(item);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                item = new ListItem();
                item.Text = dr["Short_Name"].ToString();
                item.Value = dr["org_ID"].ToString();
                ddlWorkShop.Items.Add(item);
            }
        }

        protected void SaveChoose()
        {
            string strId = Request.QueryString.Get("id");

            string strEndId = ViewState["ChooseId"].ToString();

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
            //            strEndId  = strEmId;
            //        }
            //        else
            //        {
            //            strEndId += "," + strEmId;
            //        }
            //    }
            //}

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
            //检查是否全部学员安排了微机教室
            if (PrjPub.IsServerCenter)
            {
                if (!CheckIsAllArrange())
                {
                    SessionSet.PageMessage = "有考生未安排微机教室！";
                    return;
                }

                string strSql = " update Random_Exam set Is_All_Arrange=1 where Random_Exam_ID=" +
                                Request.QueryString.Get("id");
                OracleAccess db = new OracleAccess();
                db.ExecuteNonQuery(strSql);

                strSql = "update Computer_Room set Is_Use=1 where Computer_Room_ID"
                         + " in (select Computer_Room_ID from Random_Exam_Arrange_Detail "
                         + " where Random_Exam_ID=" + Request.QueryString.Get("id") + ")";
                db.ExecuteNonQuery(strSql);
            }



            if (ViewState["mode"].ToString() == "Edit")
            {

					Response.Write("<script>window.opener.form1.Refresh.value = 'true',window.opener.form1.submit();window.close();</script>");

            }
            else if (ViewState["mode"].ToString() == "Insert")
            {
					Response.Write(
						"<script>window.opener.form1.Refresh.value = 'true',window.opener.form1.submit();window.close();</script>");
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

			if(PrjPub.IsWuhan())
			{
				ws.Cells[2, 3] = "员工编码";
			}
			else
			{
				ws.Cells[2, 3] = "工资编号";
			}
			ws.get_Range(ws.Cells[2, 3], ws.Cells[2, 3]).set_MergeCells(true);
			ws.get_Range(ws.Cells[2, 3], ws.Cells[2, 3]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

			ws.Cells[2, 4] = "职名";
			ws.get_Range(ws.Cells[2, 4], ws.Cells[2, 4]).set_MergeCells(true);
			ws.get_Range(ws.Cells[2, 4], ws.Cells[2, 4]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

			ws.Cells[2, 5] = "组织机构";
			ws.get_Range(ws.Cells[2, 5], ws.Cells[2, 7]).set_MergeCells(true);
			ws.get_Range(ws.Cells[2, 5], ws.Cells[2, 7]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

            ws.Cells[2, 8] = "考试地点";
            ws.get_Range(ws.Cells[2, 8], ws.Cells[2, 10]).set_MergeCells(true);
            ws.get_Range(ws.Cells[2, 8], ws.Cells[2, 10]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);

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

                ws.Cells[3 + j, 8] = ((Label)gvChoose.Rows[j].FindControl("lblComputeRoom")).Text;
                ws.get_Range(ws.Cells[3 + j, 8], ws.Cells[3 + j, 10]).set_MergeCells(true);
                ws.get_Range(ws.Cells[3 + j, 8], ws.Cells[3 + j, 10]).set_HorizontalAlignment(XlHAlign.xlHAlignCenter);
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

        protected void btnSelectComputeRoom_Click(object sender, EventArgs e)
        {
            string strId = Request.QueryString.Get("id");

            string strEndId = "";

            for (int i = 0; i < this.gvChoose.Rows.Count; i++)
            {
                CheckBox CheckBox1 = (CheckBox)this.gvChoose.Rows[i].FindControl("chkSelect2");
                string strEmId = ((Label)this.gvChoose.Rows[i].FindControl("LabelEmployeeID")).Text;
                if (CheckBox1.Checked)
                {
                    if (strEndId.Length == 0)
                    {
                        strEndId += strEmId;
                    }
                    else
                    {
                        strEndId += "," + strEmId;
                    }
                }
            }

            strEndId = strEndId.Replace(",", "|");

            ClientScript.RegisterStartupScript(GetType(), "jsSelectFirstNode", @"SelectComputeRoom("+ strId +",'"+ strEndId +"')", true);
        }


        private bool CheckIsAllArrange()
        {
            int count = 0;
            for (int i = 0; i < this.gvChoose.Rows.Count; i++)
            {
                string strEmId = ((Label)this.gvChoose.Rows[i].FindControl("LabelEmployeeID")).Text;
                string strCom = ((Label)this.gvChoose.Rows[i].FindControl("lblComputeRoom")).Text;

                if (string.IsNullOrEmpty(strCom) && strEmId != "0")
                {
                    count = 1;
                    break;
                }
            }

            if(count==1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void btnCheckAll_Click(object sender,EventArgs e)
        {
            string argument = Request.Form["__EVENTARGUMENT"];
            bool chk = Convert.ToBoolean(argument);

            for (int i = 0; i < this.gvChoose.Rows.Count; i++)
            {
                CheckBox CheckBox1 = (CheckBox)this.gvChoose.Rows[i].FindControl("chkSelect2");
                CheckBox1.Checked = chk;
            }
        }
    }
}
