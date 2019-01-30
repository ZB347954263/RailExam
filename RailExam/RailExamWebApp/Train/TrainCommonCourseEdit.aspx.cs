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
using RailExam.BLL;
using RailExam.Model;
using DSunSoft.Web.UI;

namespace RailExamWebApp.Train
{
    public partial class TrainCommonCourseEdit : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strID = Request.QueryString.Get("id");
                string strName = Request.QueryString.Get("name");

                if (strID != null && strID != "")
                {//Update
                    BindCourse(strID);
                    GetCourseInfo(strID);
                }
                else
                {//Add
                    BindCourse("");
                }
            }
        }

        private void BindCourse(string strID)
        {
            TrainCourseBLL objTrainCourseBll = new TrainCourseBLL();
            IList<RailExam.Model.TrainCourse> trainCourseList = objTrainCourseBll.GetTrainCommondCourseInfo();

            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "--请选择--";
            ddlCourse.Items.Add(item);

            if (trainCourseList.Count > 0)
            {
                foreach (RailExam.Model.TrainCourse trainCourse in trainCourseList)
                {
                    if (trainCourse.StandardID == 0)
                    {
                        if (trainCourse.TrainCourseID.ToString() != strID)
                        {
                            ListItem items = new ListItem();
                            items.Value = trainCourse.TrainCourseID.ToString();
                            items.Text = trainCourse.CourseName;
                            ddlCourse.Items.Add(items);
                        }
                    }
                }
            }
        }

        private void GetCourseInfo(string strID)
        {
            TrainCourseBLL trainCourseBLL = new TrainCourseBLL();
            RailExam.Model.TrainCourse trainCourse = new RailExam.Model.TrainCourse();
            trainCourse = trainCourseBLL.GetTrainCourseInfo(Convert.ToInt32(strID));

            txtCourseName.Text = trainCourse.CourseName;
            txtDescription.Text = trainCourse.Description;
            txtStudyDemand.Text = trainCourse.StudyDemand;
            txtHour.Text = trainCourse.StudyHours.ToString();
            chkExam.Checked = trainCourse.HasExam;
            txtExam.Text = trainCourse.ExamForm;
            ddlCourse.SelectedValue = trainCourse.RequireCourseID.ToString();
            txtMemo.Text = trainCourse.Memo;
            ViewState["CourseNo"] = trainCourse.CourseNo;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            TrainCourseBLL trainCourseBLL = new TrainCourseBLL();
            RailExam.Model.TrainCourse trainCourse = new RailExam.Model.TrainCourse();

            trainCourse.CourseName = txtCourseName.Text;
            trainCourse.Description = txtDescription.Text;
            trainCourse.StudyDemand = txtStudyDemand.Text;
            trainCourse.StudyHours = Convert.ToDecimal(txtHour.Text);
            trainCourse.HasExam = chkExam.Checked;
            trainCourse.ExamForm = txtExam.Text;
            trainCourse.RequireCourseID = Convert.ToInt32(ddlCourse.SelectedValue);
            trainCourse.Memo = txtMemo.Text;

            string strID = Request.QueryString.Get("id");
            if (strID != null && strID != "")
            {
                trainCourse.TrainCourseID = Convert.ToInt32(strID);
                trainCourse.CourseNo = Convert.ToInt32(ViewState["CourseNo"].ToString());

                trainCourseBLL.UpdateTrainCourse(trainCourse);
            }
            else
            {
                trainCourse.StandardID = 0;

                trainCourseBLL.AddTrainCourse(trainCourse);
            }

            Response.Redirect("TrainCourseBook.aspx?CourseID=" + trainCourse.TrainCourseID + "&CourseName=" + trainCourse.CourseName);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.opener.form1.Refresh.value='true';window.opener.form1.submit();window.close();</script>");
        }
    }
}