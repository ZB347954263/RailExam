using System;
using System.Collections.Generic;
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
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class SelectRandomExamArrange : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
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

                        string strSql = "select Org_ID,Short_Name from Org where level_num=2 order by Order_Index";
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

                        if (PrjPub.CurrentLoginUser.RoleID != 1)
                        {
                            if(hasOrg)
                            {
                                ddlOrg.SelectedValue = PrjPub.CurrentLoginUser.StationOrgID.ToString();
                                ddlOrg.Enabled = false;
                            }
                            else
                            {
                                ddlOrg.SelectedValue = "0";
                            }
                        }
                        else
                        {
                            ddlOrg.SelectedValue = "0";
                        }

                        ddlOrg_SelectedIndexChanged(null, null);
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
                        ViewState["HasTrainClass"] = "1";
                    }
                    else
                    {
                        ViewState["HasTrainClass"] = "0";
                    }
                }
                BindChoosedGrid(ViewState["ChooseId"].ToString());
            }

            string strRefreshArrange = Request.Form.Get("RefreshArrange");
            if (strRefreshArrange != "" && strRefreshArrange != null)
            {
                BindChoosedGrid(ViewState["ChooseId"].ToString());

                OracleAccess db = new OracleAccess();
                string strSql;
                if (CheckIsAllArrange())
                {
                    strSql = " update Random_Exam set Is_All_Arrange=1 where Random_Exam_ID=" +
                               Request.QueryString.Get("id");
                    db.ExecuteNonQuery(strSql);
                }
            }

            if (Request.Form.Get("StudentInfo") != null && Request.Form.Get("StudentInfo") != "" )
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

        protected void ddlOrg_SelectedIndexChanged(object sender,EventArgs e)
        {
            string strSql = "select Org_ID,Short_Name from Org a "
                + " where Parent_ID =" +ddlOrg.SelectedValue + " and level_num=3 order by order_index";
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

        protected  void btnQuery_Click(object sender, EventArgs e)
        {
            BindChoosedGrid(ViewState["ChooseId"].ToString());
        }

        private string  GetSql()
        {
            string strSql = "";

            if(ddlOrg.SelectedValue != "0")
            {
                if(ddlWorkShop.SelectedValue !="0")
                {
                    strSql += " and  c.id_path || '/' like '/1/" + ddlOrg.SelectedValue + "/" + ddlWorkShop.SelectedValue + "/%'";
                }
                else
                {
                    strSql += " and  c.id_path || '/' like '/1/" + ddlOrg.SelectedValue + "/%'";
                }
            }

            if(!string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                strSql += " and Employee_Name like '%" + txtName.Text.Trim() + "%'";
            }

            if(!string.IsNullOrEmpty(txtPinyin.Text.Trim()))
            {
                strSql += " and Pinyin_Code ='" + txtPinyin.Text.Trim().ToUpper() + "'";
            }

            return strSql;
        }

        private void HasExamId()
        {
            string strExamId = Request.QueryString.Get("id");

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

        private bool CheckIsAllArrange()
        {

            OracleAccess db = new OracleAccess();
            string strSql = "select * from Random_Exam_Arrange where Random_Exam_ID=" + Request.QueryString.Get("id");
            string[] strTotal = db.RunSqlDataSet(strSql).Tables[0].Rows[0]["User_Ids"].ToString().Split(',');

            string strNow = string.Empty;
            strSql = "select * from Random_Exam_Arrange_detail where Random_Exam_ID=" + Request.QueryString.Get("id");
            DataTable dt = db.RunSqlDataSet(strSql).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                if(strNow == string.Empty)
                {
                    strNow = dr["User_Ids"].ToString();
                }
                else
                {
                    strNow += "," + dr["User_Ids"];
                }
            }
            string[] str = strNow.Split(',');

            if (str.Length != strTotal.Length)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void BindChoosedGrid(string strId)
        {
            HasExamId();
            string strExamId = Request.QueryString.Get("id");

            EmployeeBLL psBLL = new EmployeeBLL();
            DataSet ds = new DataSet();

            string[] str = strId.Split(',');
            IList<Employee> objList = new List<Employee>();

            if (str[0] != "")
            {
                OracleAccess db = new OracleAccess();
                string strSql;

                string strQuery = GetSql();
                ArrayList objEmloyeeList = new ArrayList();
                if(!string.IsNullOrEmpty(strQuery))
                {
                    IList<Employee> objSelectList = psBLL.GetEmployeeByWhereClause("1=1" + strQuery);

                    foreach (Employee employee in objSelectList)
                    {
                        objEmloyeeList.Add(employee.EmployeeID.ToString());
                    }
                }

                OrganizationBLL orgBll = new OrganizationBLL();
                for (int i = 0; i < str.Length; i++)
                {
                    Employee obj = psBLL.GetChooseEmployeeInfo(str[i]);
                    obj.RowNum = i + 1;

                    if(PrjPub.CurrentLoginUser.RoleID != 1)
                    {
                         if(orgBll.GetStationOrgID(obj.OrgID) == PrjPub.CurrentLoginUser.StationOrgID)
                        {
                            if (objEmloyeeList.Count > 0)
                             {
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
                             else
                             {
                                 objList.Add(obj);
                             }
                        }  
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(strQuery))
                        {
                            if (objEmloyeeList.IndexOf(str[i])>=0)
                            {
                                objList.Add(obj);
                            }
                        }
                        else
                        {
                            objList.Add(obj);
                        }
                    }
                }

                if (objList.Count > 0)
                {
                    ds.Tables.Add(ConvertToDataTable((IList) objList));

                    if (ViewState["Sort"] != null)
                    {
                        ds.Tables[0].DefaultView.Sort = ViewState["Sort"].ToString();
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
            if (ViewState["Sort"] != null)
            {
                if (ViewState["Sort"].ToString().Replace(" desc", "") == e.SortExpression)
                {
                    if (ViewState["Sort"].ToString().IndexOf("desc") >= 0)
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

            for (int i = 0; i < gvChoose.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["EmployeeID"] = ((Label)gvChoose.Rows[i].Cells[1].FindControl("LabelEmployeeID")).Text;
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


        public void btnCheckAll_Click(object sender, EventArgs e)
        {
            string argument = Request.Form["__EVENTARGUMENT"];
            bool chk = Convert.ToBoolean(argument);

            for (int i = 0; i < this.gvChoose.Rows.Count; i++)
            {
                CheckBox CheckBox1 = (CheckBox)this.gvChoose.Rows[i].FindControl("chkSelect2");
                CheckBox1.Checked = chk;
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

            ClientScript.RegisterStartupScript(GetType(), "jsSelectFirstNode", @"SelectComputeRoom(" + strId + ",'" + strEndId + "')", true);
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

            if (PrjPub.IsWuhan())
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

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            OracleAccess db = new OracleAccess();

            string strSql = "update Computer_Room set Is_Use=1 where Computer_Room_ID"
                 + " in (select Computer_Room_ID from Random_Exam_Arrange_Detail "
                 + " where Random_Exam_ID=" + Request.QueryString.Get("id") + ")";
            db.ExecuteNonQuery(strSql);

            if (ViewState["mode"].ToString() == "Edit")
            {
                Response.Write("<script>window.opener.form1.Refresh.value = 'true',window.opener.form1.submit();window.close();</script>");
            }
        }
    }
}
