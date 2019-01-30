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
    public partial class TrainCourseEdit : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strID = Request.QueryString.Get("CourseID");
                string strName = Request.QueryString.Get("name");

                ViewState["StandardID"] = Request.QueryString.Get("StandardID");

                if (strID != null && strID != "")
                {//Update
                    ViewState["CourseID"] = strID;
                    ViewState["CourseName"] = strName;
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
            IList<RailExam.Model.TrainCourse> objList = objTrainCourseBll.GetTrainCourseInfo(0, Convert.ToInt32(ViewState["StandardID"].ToString()), 0, "", "", "", 0, true, "", 0, "", 0, 200, ""); ;

            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "--请选择--";
            ddlCourse.Items.Add(item);

            if (objList.Count > 0)
            {
                foreach (RailExam.Model.TrainCourse trainCourse in objList)
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

        private void GetCourseInfo(string strID)
        {
            TrainCourseBLL objTrainCourseBll = new TrainCourseBLL();
            RailExam.Model.TrainCourse trainCourse = new RailExam.Model.TrainCourse();
            trainCourse = objTrainCourseBll.GetTrainCourseInfo(Convert.ToInt32(strID));

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
            TrainCourseBLL objTrainCourseBll = new TrainCourseBLL();
            RailExam.Model.TrainCourse trainCourse = new RailExam.Model.TrainCourse();

            string strID = Request.QueryString.Get("CourseID");
            if (strID != null && strID != "")
            {
                trainCourse.TrainCourseID = Convert.ToInt32(strID);
                trainCourse.StandardID = Convert.ToInt32(ViewState["StandardID"].ToString());
                trainCourse.CourseNo = Convert.ToInt32(ViewState["CourseNo"].ToString());
                trainCourse.CourseName = txtCourseName.Text;
                trainCourse.Description = txtDescription.Text;
                trainCourse.StudyDemand = txtStudyDemand.Text;
                trainCourse.StudyHours = Convert.ToDecimal(txtHour.Text);
                trainCourse.HasExam = chkExam.Checked;
                trainCourse.ExamForm = txtExam.Text;
                trainCourse.RequireCourseID = Convert.ToInt32(ddlCourse.SelectedValue);
                trainCourse.Memo = txtMemo.Text;

                objTrainCourseBll.UpdateTrainCourse(trainCourse);
            }
            else
            {
                trainCourse.StandardID = Convert.ToInt32(ViewState["StandardID"].ToString());
                trainCourse.CourseName = txtCourseName.Text;
                trainCourse.Description = txtDescription.Text;
                trainCourse.StudyDemand = txtStudyDemand.Text;
                trainCourse.StudyHours = Convert.ToDecimal(txtHour.Text);
                trainCourse.HasExam = chkExam.Checked;
                trainCourse.ExamForm = txtExam.Text;
                trainCourse.RequireCourseID = Convert.ToInt32(ddlCourse.SelectedValue);
                trainCourse.Memo = txtMemo.Text;

                objTrainCourseBll.AddTrainCourse(trainCourse);
            }

            Response.Redirect("TrainCourseBook.aspx?StandardID=" + trainCourse.StandardID + "&CourseID=" + trainCourse.TrainCourseID + "&CourseName=" + trainCourse.CourseName);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.opener.form1.Refresh.value='true' ;window.opener.form1.submit();window.close();</script>");
        }
    }
}