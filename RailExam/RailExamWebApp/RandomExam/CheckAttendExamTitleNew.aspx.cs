using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DSunSoft.Web.UI;
using RailExam.BLL;
using RailExam.Model;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.RandomExam
{
    public partial class CheckAttendExamTitleNew : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && !CallBack1.IsCallback)
            {
                //string strExamId = Request.QueryString.Get("id");
                ViewState["BeginTime"] = DateTime.Now.ToString();
                ViewState["StudentID"] = Request.QueryString.Get("employeeID");

                string strSql = "select * from System_Exam where System_Exam_ID=1";
                OracleAccess db = new OracleAccess();
                DataTable dt = db.RunSqlDataSet(strSql).Tables[0];

                int examtime;
                int examNumber;
                if(dt.Rows.Count > 0)
                {
                    examtime = Convert.ToInt32(dt.Rows[0]["Exam_Time"]);
                    examNumber = Convert.ToInt32(dt.Rows[0]["Exam_Number"]);
                }
                else
                {
                    examtime = 10;
                    examNumber = 10;
                }
                ViewState["ExamTime"] = examtime.ToString();
                ViewState["ExamNumber"] = examNumber.ToString();

                HiddenFieldExamTime.Value = DateTime.Now.AddMinutes(examtime).ToString();
                HfExamTime.Value = (examtime * 60).ToString();

                FillPage();
            }
        }


        protected void FillPage()
        {
            //获取考试基本信息
            lblTitle.Text = "模拟考试";

            // 用于前台JS判断是否完成全部试题
            hfPaperItemsCount.Value = ViewState["ExamNumber"].ToString();
            lblTitleRight.Text = "总共" + ViewState["ExamNumber"] + "题";


            string strSql = "select a.*,GetStationOrgID(org_id) StationOrgID from Employee a "
                    + " where Employee_ID=" + Request.QueryString.Get("employeeID");
            OracleAccess db = new OracleAccess();

            DataRow dr = db.RunSqlDataSet(strSql).Tables[0].Rows[0];

            lblWorkNo.Text = dr["Work_No"].ToString();
            lblIDCard.Text = dr["Identity_CardNo"].ToString();
            lblSex.Text = dr["Sex"].ToString();

            EmployeeBLL objEmployeebll = new EmployeeBLL();
            Employee objEmployee = objEmployeebll.GetEmployee(Convert.ToInt32(Request.QueryString.Get("employeeID")));
            lblOrgName.Text = objEmployee.OrgName;
            lblPost.Text = objEmployee.PostName;
            lblName.Text = objEmployee.EmployeeName;
        }

        protected void CallBack1_Callback(object sender, ComponentArt.Web.UI.CallBackEventArgs e)
        {
            lblServerDateTime.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            lblServerDateTime.RenderControl(e.Output);
        }
    }
}
