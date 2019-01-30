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

namespace RailExamWebApp.Online.Study
{
    public partial class ReadCourseDetail : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            string strIDPath = Request.QueryString["id"];

            CoursewareBLL coursewareBLL = new CoursewareBLL();
            IList<RailExam.Model.Courseware> coursewares = new List<RailExam.Model.Courseware>();

            if (Request.QueryString.Get("type") == "Courseware")
            {
                coursewares = coursewareBLL.GetCoursewaresByCoursewareTypeID(Convert.ToInt32(strIDPath),1);
            }
            else
            {
                coursewares = coursewareBLL.GetCoursewaresByTrainTypeID(Convert.ToInt32(strIDPath), 1);
            }

            foreach (RailExam.Model.Courseware courseware in coursewares)
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
            Grid1.DataSource = coursewares;
            Grid1.DataBind();
        }
    }
}
