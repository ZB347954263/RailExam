using System;
using System.Collections.Generic;
using DSunSoft.Web.UI;
using RailExam.BLL;

namespace RailExamWebApp.Online.Study
{
    public partial class StudyCourseware : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strTrainType = Request.QueryString.Get("TrainTypeID");
                string strCoursewareType = Request.QueryString.Get("CoursewareTypeID");
                string strPostID = Request.QueryString.Get("PostID");
                string strOrgID = Request.QueryString.Get("OrgID");
                string strLeader = Request.QueryString.Get("Leader");
                string strTech = Request.QueryString.Get("Tech");

                ViewState["PostID"] = strPostID;
                ViewState["OrgID"] = strOrgID;
                ViewState["Leader"] = strLeader;
                ViewState["Tech"] = strTech;

                if (strTrainType != "" && strTrainType != null)
                {
                    ViewState["TrainTypeID"] = strTrainType;
                    BindGridByTrainTypeID();
                }

                if (strCoursewareType != "" && strCoursewareType != null)
                {
                    ViewState["CoursewareTypeID"] = strCoursewareType;
                    BindGridByCoursewareTypeID();
                }
            }
        }

        private void BindGridByTrainTypeID()
        {
            int trainTypeID = Convert.ToInt32(ViewState["TrainTypeID"].ToString());
            int postID = Convert.ToInt32(ViewState["PostID"].ToString());
            int orgID = Convert.ToInt32(ViewState["OrgID"].ToString());
            int techID = Convert.ToInt32(ViewState["Tech"].ToString());
            int leader = Convert.ToInt32(ViewState["Leader"].ToString());

            CoursewareBLL coursewareBLL = new CoursewareBLL();
            IList<RailExam.Model.Courseware> coursewareList = coursewareBLL.GetStudyCoursewareInfoByTrainTypeID(trainTypeID,orgID,postID,leader,techID,0);


            if (coursewareList.Count > 0)
            {
                foreach (RailExam.Model.Courseware courseware in coursewareList)
                {
                    //if (courseware.CoursewareName.Length <= 15)
                    //{
                        courseware.CoursewareName = "<a onclick=OpenIndex('" + courseware.CoursewareID + "')  href=#  title=" + courseware.CoursewareName + ">" + courseware.CoursewareName+ "</a>";
                    //}
                    //else
                    //{
                    //    courseware.CoursewareName = "<a onclick=OpenIndex('" + courseware.Url + "')  href=#  title=" + courseware.CoursewareName + ">" + courseware.CoursewareName.Substring(0, 15) + "...</a>";
                    //}
                }
                gvCourse.DataSource = coursewareList;
                gvCourse.DataBind();
            }
        }

        private void BindGridByCoursewareTypeID()
        {
            int typeID= Convert.ToInt32(ViewState["CoursewareTypeID"].ToString());
            int postID = Convert.ToInt32(ViewState["PostID"].ToString());
            int orgID = Convert.ToInt32(ViewState["OrgID"].ToString());
            int techID = Convert.ToInt32(ViewState["Tech"].ToString());
            int leader = Convert.ToInt32(ViewState["Leader"].ToString());

            CoursewareBLL coursewareBLL = new CoursewareBLL();
            IList<RailExam.Model.Courseware> coursewareList = coursewareBLL.GetStudyCoursewareInfoByTypeID(typeID, orgID, postID, leader, techID, 0);


            if (coursewareList.Count > 0)
            {
                foreach (RailExam.Model.Courseware courseware in coursewareList)
                {
                    //if (courseware.CoursewareName.Length <= 15)
                    //{
                    courseware.CoursewareName = "<a onclick=OpenIndex('" + courseware.CoursewareID + "')  href=#  title=" + courseware.CoursewareName + ">" + courseware.CoursewareName + "</a>";
                    //}
                    //else
                    //{
                    //    courseware.CoursewareName = "<a onclick=OpenIndex('" + courseware.Url + "')  href=#  title=" + courseware.CoursewareName + ">" + courseware.CoursewareName.Substring(0, 15) + "...</a>";
                    //}
                }
                gvCourse.DataSource = coursewareList;
                gvCourse.DataBind();
            }
        }
    }
}
