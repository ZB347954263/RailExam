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
using ComponentArt.Web.UI;
using DSunSoft.Web.UI;

namespace RailExamWebApp.Train
{
    public partial class TrainCourse : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string str = Request.QueryString.Get("id");

                TrainStandardBLL objTrainStandardBLL = new TrainStandardBLL();
                int nTypeID = objTrainStandardBLL.GetTrainStandardInfo(Convert.ToInt32(str)).TypeID;

                TrainTypeBLL objTrainTypeBLL = new TrainTypeBLL();
                int nLevel = objTrainTypeBLL.GetTrainTypeInfo(nTypeID).LevelNum;

                if (nLevel == 1)
                {
                    Grid1.Visible = false;
                    btnAddTrainCourse.Visible = false;
                }
                else
                {
                    Grid1.Visible = true;
                    btnAddTrainCourse.Visible = true;
                }
            }

            string strRefresh = Request.Form.Get("Refresh");
            if (strRefresh != null & strRefresh != "")
            {
                Grid1.DataBind();
            }

            string strDeleteID = Request.Form.Get("DeleteID");
            if (strDeleteID != null & strDeleteID != "")
            {
                DeleteCourse(strDeleteID);
                Grid1.DataBind();
            }

            btnAddTrainCourse.Attributes.Add("onclick", "AddRecord(" + Request.QueryString.Get("id") + ");");
        }

        private static void DeleteCourse(string strID)
        {
            TrainCourseBLL objTrainCourseBllBll = new TrainCourseBLL();
            objTrainCourseBllBll.DeleteTrainCourse(Convert.ToInt32(strID));
        }

        protected void tvTrainCourseMoveCallBack_Callback(object sender, CallBackEventArgs e)
        {
            TrainCourseBLL trainCourseBLL = new TrainCourseBLL();
            RailExam.Model.TrainCourse trainCourse = trainCourseBLL.GetTrainCourseInfo(int.Parse(e.Parameters[0]));

            if (trainCourse.CourseNo != 1 && e.Parameters[1] == "CanMoveUp")
            {
                hfCanMove.Value = "true";
                hfCanMove.RenderControl(e.Output);
            }
            else if (trainCourse.CourseNo != Grid1.Items.Count && e.Parameters[1] == "CanMoveDown")
            {
                hfCanMove.Value = "true";
                hfCanMove.RenderControl(e.Output);
            }
        }

        protected void tvTrainCourseChangeCallBack_Callback(object sender, CallBackEventArgs e)
        {
            TrainCourseBLL trainCourseBLL = new TrainCourseBLL();
            RailExam.Model.TrainCourse trainCourse = trainCourseBLL.GetTrainCourseInfo(int.Parse(e.Parameters[0]));

            string str = Request.QueryString.Get("id");

            if (e.Parameters[1] == "MoveUp")
            {
                IList<RailExam.Model.TrainCourse> trainCourseList =
                    trainCourseBLL.GetTrainCourseInfo(0, Convert.ToInt32(str), trainCourse.CourseNo - 1, "", "", "",
                                                         0, false, "", 0, "", 0, 200, "");

                foreach (RailExam.Model.TrainCourse course in trainCourseList)
                {
                    course.CourseNo++;
                    trainCourseBLL.UpdateTrainCourse(course);
                }

                trainCourse.CourseNo--;
                trainCourseBLL.UpdateTrainCourse(trainCourse);
            }
            if (e.Parameters[1] == "MoveDown")
            {
                IList<RailExam.Model.TrainCourse> trainCourseList =
                  trainCourseBLL.GetTrainCourseInfo(0, Convert.ToInt32(str), trainCourse.CourseNo + 1, "", "", "",
                                                       0, false, "", 0, "", 0, 200, "");

                foreach (RailExam.Model.TrainCourse course in trainCourseList)
                {
                    course.CourseNo--;
                    trainCourseBLL.UpdateTrainCourse(course);
                }

                trainCourse.CourseNo++;
                trainCourseBLL.UpdateTrainCourse(trainCourse);
            }
        }
    }
}