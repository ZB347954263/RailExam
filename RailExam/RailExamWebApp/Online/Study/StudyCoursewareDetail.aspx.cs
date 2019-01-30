using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using RailExam.BLL;
using RailExamWebApp.Common.Class;

namespace RailExamWebApp.Online.Study
{
    public partial class StudyCoursewareDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TrainEmployeeBLL objTrainEmployeeBll = new TrainEmployeeBLL();

            if (PrjPub.StudentID == null)
            {
                Response.Write("<script>alert('已超时，请注销后重新登录！');window.close();</script>");
                return;
            }
            ViewState["TrainTypeID"] = objTrainEmployeeBll.GetTrainEmployeeByEmployeeID(PrjPub.CurrentStudent.EmployeeID).TrainTypeID.ToString();

            if (ViewState["TrainTypeID"].ToString() == "0")
            {
				Response.Write("<script>alert('请选择培训类别！');window.close();</script>");
                return;
            }

            BindGrid();
        }

        private void BindGrid()
        {
            bool isGroup = PrjPub.CurrentStudent.IsGroupLearder;
            int tech = PrjPub.CurrentStudent.TechnicianTypeID;

            CoursewareBLL coursewareBLL = new CoursewareBLL();
            IList<RailExam.Model.Courseware> coursewareList = coursewareBLL.GetEmployeeStudyCoursewareInfo(Convert.ToInt32(ViewState["TrainTypeID"].ToString()), PrjPub.CurrentStudent.OrgID, PrjPub.CurrentStudent.PostID,isGroup,tech, 0);

            if (coursewareList.Count > 0)
            {
                foreach (RailExam.Model.Courseware courseware in coursewareList)
                {
                    if (courseware.CoursewareName.Length <= 15)
                    {
                        courseware.CoursewareName = "<a href=" + courseware.Url + " target='_blank' title=" + courseware.CoursewareName + ">" + courseware.CoursewareName;
                    }
                    else
                    {
                        courseware.CoursewareName = "<a href=" + courseware.Url + " target='_blank' title=" + courseware.CoursewareName + ">" + courseware.CoursewareName.Substring(0, 15) + "...";
                    }
                }
                gvCourse.DataSource = coursewareList;
                gvCourse.DataBind();
            }
        }
    }
}
