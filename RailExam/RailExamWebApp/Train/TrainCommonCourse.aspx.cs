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
    public partial class TrainCommonCourse : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            btnAddTrainCourse.Attributes.Add("onclick", "AddRecord();");
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
            RailExam.Model.TrainCourse objTrainCourse = trainCourseBLL.GetTrainCourseInfo(int.Parse(e.Parameters[0]));

            string str = Request.QueryString.Get("id");

            if (e.Parameters[1] == "MoveUp")
            {
                IList<RailExam.Model.TrainCourse> objTrainCourseList =
                    trainCourseBLL.GetTrainCourseInfo(0, -1, objTrainCourse.CourseNo - 1, "", "", "",
                                                         0, false, "", 0, "", 0, 200, "");

                foreach (RailExam.Model.TrainCourse course in objTrainCourseList)
                {
                    course.CourseNo++;
                    trainCourseBLL.UpdateTrainCourse(course);
                }

                objTrainCourse.CourseNo--;
                trainCourseBLL.UpdateTrainCourse(objTrainCourse);
            }
            if (e.Parameters[1] == "MoveDown")
            {
                IList<RailExam.Model.TrainCourse> objTrainCourseList =
                  trainCourseBLL.GetTrainCourseInfo(0, -1, objTrainCourse.CourseNo + 1, "", "", "",
                                                       0, false, "", 0, "", 0, 200, "");

                foreach (RailExam.Model.TrainCourse course in objTrainCourseList)
                {
                    course.CourseNo--;
                    trainCourseBLL.UpdateTrainCourse(course);
                }

                objTrainCourse.CourseNo++;
                trainCourseBLL.UpdateTrainCourse(objTrainCourse);
            }
        }
    }
}