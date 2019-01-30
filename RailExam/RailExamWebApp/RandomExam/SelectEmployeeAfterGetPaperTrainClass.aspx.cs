using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.OracleClient;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class SelectEmployeeAfterGetPaperTrainClass : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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

                BindGrid();
                
                OracleAccess db = new OracleAccess();
                string strSql = "select b.Short_Name ||'-'|| Computer_Room_Name ComputerRoomName, "
                    +"Computer_Room_ID "
                    +"from Computer_Room a "
                    +"inner join Org b on a.Org_ID=b.Org_ID "
                    + "inner join Computer_Server c on a.Computer_Server_ID=c.Computer_Server_ID "
                    +" where Computer_Server_No='" + PrjPub.ServerNo+"'";
                DataSet ds = db.RunSqlDataSet(strSql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ListItem item = new ListItem();
                    item.Text = dr["ComputerRoomName"].ToString();
                    item.Value = dr["Computer_Room_ID"].ToString();
                    ddlComputerRoom.Items.Add(item);
                }
            }
        }

        private void BindGrid()
        {
            string strId = Request.QueryString.Get("RandomExamID");

            OracleAccess db = new OracleAccess();
            OracleAccess dbCenter = new OracleAccess(System.Configuration.ConfigurationManager.ConnectionStrings["OracleCenter"].ConnectionString);
            string strSql = "select * from Random_Exam_Train_Class where Random_Exam_ID=" + strId;
            DataRow drClass = db.RunSqlDataSet(strSql).Tables[0].Rows[0];
            string strClassId = drClass["Train_Class_ID"].ToString();

            //从路局获取最新的考生安排信息
            strSql = "select * from Random_Exam_Arrange where Random_Exam_ID=" + strId;
            DataRow drUser = dbCenter.RunSqlDataSet(strSql).Tables[0].Rows[0];
            string strUserId = drUser["User_Ids"].ToString();

            string strResult = string.Empty;
            if (!PrjPub.IsServerCenter)
            {
                strSql = "begin dbms_mview.refresh('Zj_Train_Plan_Employee','?');  end;";
                db.ExecuteNonQuery(strSql);

                //获取培训班的考生信息
                strSql =
                    @"select a.Employee_ID EmployeeID,RowNum,b.Employee_Name EmployeeName,
                                    case when b.Work_No is null then b.identity_CardNo end StrWorkNo,
                                    getorgname(b.Org_id) OrgName,c.Post_Name PostName
                                    from Zj_Train_Plan_Employee a
                                    inner join Employee b on a.Employee_ID=b.Employee_ID
                                    inner join Post c on b.Post_ID=c.Post_ID
                                    where a.Train_Class_ID=" + strClassId;
//                                    + @" and a.Employee_ID not in (" + strUserId +")"+
//                                    @" and a.Employee_ID not in (select Employee_ID from ZJ_Train_Class_Subject_Result@link_sf 
//                                    where Train_Class_ID=" + strClassId + @" and IsPass=1)";

               //获取培训班的已通过考试的人员信息
               strResult =
                    @"select Employee_ID EmployeeID from ZJ_Train_Class_Subject_Result@link_sf 
                                    where Train_Class_ID=" + strClassId + @" and IsPass=1";
            }
            else
            {
                strSql =
                    @"select a.Employee_ID EmployeeID,RowNum,b.Employee_Name EmployeeName,
                                    case when b.Work_No is null then b.identity_CardNo end StrWorkNo,
                                    getorgname(b.Org_id) OrgName,c.Post_Name PostName
                                    from Zj_Train_Plan_Employee a
                                    inner join Employee b on a.Employee_ID=b.Employee_ID
                                    inner join Post c on b.Post_ID=c.Post_ID
                                    where a.Train_Class_ID=" + strClassId;  
//                                    +@" and a.Employee_ID not in (" + strUserId +")"+
//                                    @" and Employee_ID not in (select Employee_ID from ZJ_Train_Class_Subject_Result 
//                                    where Train_Class_ID=" + strClassId + @" and IsPass=1)";

                 strResult =
                        @"select Employee_ID EmployeeID from ZJ_Train_Class_Subject_Result
                    where Train_Class_ID=" + strClassId + @" and IsPass=1"; //
            }



            DataTable dt = db.RunSqlDataSet(strSql).Tables[0];
            DataTable dtResult = db.RunSqlDataSet(strResult).Tables[0];
            string strResultEmployee = string.Empty;
            foreach(DataRow drResult in dtResult.Rows)
            {
                 if(strResultEmployee == string.Empty)
                 {
                     strResultEmployee = drResult["EmployeeID"].ToString();
                 }
                 else
                 {
                     strResultEmployee += ","+drResult["EmployeeID"];
                 }
            }

            if(dt.Rows.Count > 0)
            {
                DataColumn dc = dt.Columns.Add("ComputeRoom");

                //从路局获取最新的考生安排详细信息
                strSql = "select a.*,c.Short_Name||'-'||b.Computer_Room_Name as ComputeRoom "
                    + " from Random_Exam_Arrange_Detail a "
                    + " inner join Computer_Room b on a.Computer_Room_ID=b.Computer_Room_ID"
                    + " inner join Org c on b.Org_ID=c.Org_ID"
                    + " where Random_Exam_ID='" + strId + "'";
                DataSet dsDetail = dbCenter.RunSqlDataSet(strSql);

                DataTable dtNow = dt.Clone();

                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    string strUser = "," + dr["EmployeeID"] + ",";
                    if (("," + strUserId + ",").IndexOf(strUser) >= 0 || ("," + strResultEmployee + ",").IndexOf(strUser) >= 0)
                    {
                        continue;
                    }
                    else
                    {
                        DataRow drNow = dtNow.NewRow();
                        drNow["EmployeeID"] = Convert.ToInt32(dr["EmployeeID"].ToString());
                        drNow["RowNum"] = i;
                        drNow["EmployeeName"] = dr["EmployeeName"].ToString();
                        drNow["StrWorkNo"] = dr["StrWorkNo"].ToString();
                        drNow["OrgName"] = dr["OrgName"].ToString();
                        drNow["PostName"] = dr["PostName"].ToString();
                        dtNow.Rows.Add(drNow);

                        i++;
                    }

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

                gvChoose.DataSource = dtNow;
                gvChoose.DataBind();
            }
            else
            {
                BindEmptyGrid();
            }
        }

        private void BindEmptyGrid()
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

        protected void btnLast_Click(object sender, EventArgs e)
        {
            string strId = Request.QueryString.Get("RandomExamID");

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

            string strNewID=string.Empty;
            if (!string.IsNullOrEmpty(strEndId))
            {
                //插入安排明细表
                OracleAccess db = new OracleAccess();
                string strSql = "select *　from Random_Exam_Arrange where Random_Exam_ID=" + strId;
                DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

                strSql = "select * from Random_Exam_Arrange_Detail where Random_Exam_ID=" + strId +
                         " and Computer_Room_ID=" + ddlComputerRoom.SelectedValue;
                DataSet dsRoom = db.RunSqlDataSet(strSql);

                if (dsRoom.Tables[0].Rows.Count > 0)
                {
                    //objBll.UpdateRandomExamArrangeToServer(int.Parse(strId), dr["User_Ids"]+","+strEndId.Replace("|",","));
                    OracleParameter para1 = new OracleParameter("p_User_Ids", OracleType.Clob);
                    OracleParameter para2 = new OracleParameter("p_random_Exam_ID", OracleType.Number);
                    para2.Value = Convert.ToInt32(strId);
                    IDataParameter[] paras = new IDataParameter[] { para1, para2 };
                    Pub.RunAddProcedure(true, "USP_RANDOM_EXAM_ARRANGE_U", paras,
                                            System.Text.Encoding.Unicode.GetBytes(dr["User_Ids"] + "," + strEndId.Replace("|", ",")));

                    strNewID = dsRoom.Tables[0].Rows[0]["User_Ids"] + "," + strEndId.Replace("|", ",");

                    if(!PrjPub.IsServerCenter)
                    {
                        strSql = "delete from Random_Exam_Arrange_Detail@link_sf where Random_Exam_ID=" + strId +
                                    " and Computer_Room_ID=" + ddlComputerRoom.SelectedValue;
                    }
                    else
                    {
                        strSql = "delete from Random_Exam_Arrange_Detail where Random_Exam_ID=" + strId +
                                     " and Computer_Room_ID=" + ddlComputerRoom.SelectedValue;
                    }
                    db.ExecuteNonQuery(strSql);
                }

                XmlDocument doc = new XmlDocument();
                //Request.PhysicalApplicationPath取得config文件路径
                doc.Load(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "web.config"));
                XmlNode node = doc.SelectSingleNode("configuration/dataConfiguration/@defaultDatabase");
                string value = node.Value;
                int id = 0;
                if (value == "Oracle")
                {
                    OracleParameter para1 = new OracleParameter("p_User_Ids", OracleType.Clob);
                    OracleParameter para2 = new OracleParameter("p_random_exam_arrange_de_id", OracleType.Number);
                    para2.Direction = ParameterDirection.Output;
                    OracleParameter para3 = new OracleParameter("p_random_exam_arrange_Id", OracleType.Number);
                    para3.Value = Convert.ToInt32(dr["Random_Exam_Arrange_ID"]);
                    OracleParameter para4 = new OracleParameter("p_random_Exam_ID", OracleType.Number);
                    para4.Value = Convert.ToInt32(strId);
                    OracleParameter para5 = new OracleParameter("p_computer_room_id", OracleType.Number);
                    para5.Value = Convert.ToInt32(ddlComputerRoom.SelectedValue);

                    IDataParameter[] paras = new IDataParameter[] {para1, para2, para3, para4, para5};
                    id =
                        Pub.RunAddProcedure(true, "USP_RANDOM_EXAM_ARRANGE_DE_I", paras,
                                            System.Text.Encoding.Unicode.GetBytes(strNewID));
                }
            }

            ClientScript.RegisterStartupScript(GetType(), "jsSelectFirstNode", @"GetPaper(" + strId + ",'" + strEndId + "')", true);
        }
    }
}
