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
using RailExam.DAL;
using RailExam.Model;
using RailExam.BLL;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using DSunSoft.Data;
using DSunSoft.Web.Global;
using DSunSoft.Web.UI;
using Oracle.DataAccess.Client;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp
{
    public partial class Index : PageBase
    {
		public string BBSUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!PrjPub.IsServerCenter)
            {
                RefreshSnapShotBLL objBll = new RefreshSnapShotBLL();
                if (!objBll.IsExistsRefreshSnapShot("BOOK", "MATERIALIZED VIEW"))
                {
                    Response.Redirect("RefreshDataDefault.aspx");
                }    
            }

			BBSUrl = System.Configuration.ConfigurationManager.AppSettings["BBCUrl"].ToString();
            if (!IsPostBack)
            {
                BindGrid();
            }
        }       

        private void BindGrid()
        {
            GridView2.DataBind();

            if (PrjPub.StudentID != null && PrjPub.StudentID != "")
            {
                string struesrId = PrjPub.StudentID;
                ExamBLL bkLL = new ExamBLL();
                IList<RailExam.Model.Exam> ExamList = bkLL.GetExamByUserId(struesrId, 0, PrjPub.ServerNo);
                this.GridView4.DataSource = ExamList;
                this.GridView4.DataBind();

             

                //即将考试
                IList<RailExam.Model.Exam> ExamList1 = bkLL.GetComingExamByUserId(struesrId);

                this.GridView3.DataSource = ExamList1;
                this.GridView3.DataBind();

                //历史考试
                IList<RailExam.Model.Exam> ExamList2 = bkLL.GetHistoryExamByUserId(struesrId);
                this.GridView5.DataSource = ExamList2;
                this.GridView5.DataBind();

                gvExamResult.Visible = true;
            }
            else
            {
                ExamBLL bkLL = new ExamBLL();
                IList<RailExam.Model.Exam> ExamList = bkLL.GetNowExam();
                this.GridView4.DataSource = ExamList;
                this.GridView4.DataBind();

                IList<RailExam.Model.Exam> ExamList1 = bkLL.GetComingExam();
                this.GridView3.DataSource = ExamList1;
                this.GridView3.DataBind();

                IList<RailExam.Model.Exam> ExamList2 = bkLL.GetHistoryExam();
                this.GridView5.DataSource = ExamList2;
                this.GridView5.DataBind();
                gvExamResult.Visible = false;
            }
        }

        protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Label1 = (Label)e.Row.FindControl("labExamId");
                Label Label2 = (Label)e.Row.FindControl("labPaperId");
                Label Label3 = (Label)e.Row.FindControl("labelT3");

                Label Label4 = (Label)e.Row.FindControl("labelT1");

                Label Label5 = (Label)e.Row.FindControl("labelT2");
                Label3.Text = Label4.Text + " － " + Label5.Text;

                if (PrjPub.StudentID != null && PrjPub.StudentID != "")
                {
                    LinkButton btnDelete = (LinkButton)e.Row.FindControl("link1");

                    btnDelete.Attributes.Add("onclick", "AttendExam(" + Label1.Text + "," + Label2.Text + ");");
                }
                else
                {
                    LinkButton btnDelete = (LinkButton)e.Row.FindControl("link1");
                    btnDelete.Enabled = false;
                }
            }
        }

        protected void ImageButtonLogin_Click(object sender, ImageClickEventArgs e)
        {
            LoginUserBLL loginUserBLL = new LoginUserBLL();
            LoginUser loginUser;

            if (PrjPub.IsServerCenter)
            {
                loginUser = loginUserBLL.GetLoginUser(txtUserID.Text, txtPassword.Text, 0);
            }
            else
            {
                loginUser = loginUserBLL.GetLoginUser(txtUserID.Text, txtPassword.Text, 1);
            } 
            
            if (loginUser == null)
            {
                SessionSet.PageMessage = "您输入的用户名或密码不正确！";
                return;
            }

            PrjPub.CurrentStudent = loginUser;
            PrjPub.WelcomeInfo = loginUser.OrgName + "：" + loginUser.EmployeeName + "，您好！";
            PrjPub.StudentID = loginUser.EmployeeID.ToString();
            Session["StudentOrdID"] = loginUser.OrgID;
           
            //控件显示
            lblUserName.Text = "单&nbsp;&nbsp;位：";
            lblOrg.Text = loginUser.OrgName;

            lblPassword.Text = "姓&nbsp;&nbsp;名：";
            lblEmployeeName.Text = loginUser.EmployeeName;

            lblWorkNo1.Visible = true;
            lblWorkNo2.Visible = true;
            EmployeeBLL objEmployeeBll =new EmployeeBLL();
            lblWorkNo2.Text = objEmployeeBll.GetEmployee(loginUser.EmployeeID).WorkNo;

            string strPost = loginUser.PostName;

            PostBLL objPostBll = new PostBLL();
            Post objPost = objPostBll.GetPost(loginUser.PostID);
            Post objPostType = objPostBll.GetPost(objPost.ParentId);
            string strType = objPostType.PostName;
            Post objPostSystem = objPostBll.GetPost(objPostType.ParentId);
            string strSystem = objPostSystem.PostName;

            lblFullName.Text = strSystem + "-" + strType + "-" + strPost;

            txtUserID.Visible = false;
            lblOrg.Visible = true;
            txtPassword.Visible = false;
            lblEmployeeName.Visible = true;
            lblPostName.Visible = true;
            lblFullName.Visible = true;

            ImageButtonLogin.Visible = false;
            ImageButtonLogout.Visible = true;

            btnStudyBook.Visible = false;
            btnStudyCourse.Visible = false;
            btnSelectTrainType.Visible = true;
            btnStudy.Visible = true;

            BindGrid();
        }

        protected void ImageButtonLogout_Click(object sender, ImageClickEventArgs e)
        {
            PrjPub.CurrentStudent = null;
            PrjPub.WelcomeInfo = string.Empty;
            PrjPub.StudentID = string.Empty;
            Session.Remove("StudentOrdID");

            //控件显示
            lblUserName.Text = "用户名：";
            txtUserID.Text = string.Empty;

            lblPassword.Text = "密  码：";

            txtUserID.Visible = true;
            lblOrg.Visible = false;
            txtPassword.Visible = true;

            lblEmployeeName.Visible = false;
            lblPostName.Visible = false;
            lblFullName.Visible = false;

            ImageButtonLogin.Visible = true;
            ImageButtonLogout.Visible = false;

            lblWorkNo1.Visible = false;
            lblWorkNo2.Visible = false;

            btnStudyBook.Visible = true;
            btnStudyCourse.Visible = true;
            btnSelectTrainType.Visible = false;
            btnStudy.Visible = false;


            BindGrid();
        }

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Label1 = (Label)e.Row.FindControl("labExamId");
                Label Label2 = (Label)e.Row.FindControl("labPaperId");
                Label Label3 = (Label)e.Row.FindControl("labelT3");
                Label Label4 = (Label)e.Row.FindControl("labelT1");
                Label Label5 = (Label)e.Row.FindControl("labelT2");
                Label3.Text = Label4.Text + " － " + Label5.Text;
            }
        }

        protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Label1 = (Label)e.Row.FindControl("labExamId");
                Label Label2 = (Label)e.Row.FindControl("labPaperId");
                Label Label3 = (Label)e.Row.FindControl("labelT3");
                Label Label4 = (Label)e.Row.FindControl("labelT1");
                Label Label5 = (Label)e.Row.FindControl("labelT2");
                Label3.Text = Label4.Text + " － " + Label5.Text;
            }
        }
    }
}